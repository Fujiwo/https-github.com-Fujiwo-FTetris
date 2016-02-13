/// <reference path="FTetris.Model.ts" />
/// <reference path="scripts/typings/threejs/three.d.ts" />

namespace FTetris.View {
    class Converter {
        private static colors: THREE.Color[] = [
            new THREE.Color(0.4, 0.3, 0.2),
            new THREE.Color(1.0, 0.0, 0.0),
            new THREE.Color(0.0, 1.0, 0.0),
            new THREE.Color(1.0, 1.0, 0.0),
            new THREE.Color(0.0, 0.0, 1.0),
            new THREE.Color(1.0, 0.0, 1.0),
            new THREE.Color(0.0, 1.0, 1.0),
            new THREE.Color(1.0, 0.9, 0.8)
        ];

        public static ToColor(index: number): THREE.Color {
            return this.colors[index];
        }
    }

    export class CellView {
        private cubeDistance = 1.0;
        private cubeSize     = this.cubeDistance * 0.9;

        private _dataContext: Model.Cell;

        private get dataContext(): Model.Cell { return this._dataContext; }
        private set dataContext(value : Model.Cell) {
            if (value != this._dataContext) {
                this._dataContext = value;
                value.indexChanged = (cell, index) => this.onDataContextIndexChanged(cell, index);
            }
        }

        private onDataContextIndexChanged(cell: Model.Cell, index: number): void {
            this.setColor(Converter.ToColor(index));
        }

        private _mesh: THREE.Mesh;

        public get mesh(): THREE.Mesh { return this._mesh; }

        private setColor(value: THREE.Color) {
            (<THREE.MeshPhongMaterial>this.mesh.material).color.set(value);
        }

        private _point: Model.Point;

        private get point(): Model.Point       { return this._point;  }
        private set point(value: Model.Point)  { this._point = value; }

        public constructor(wholeSize: Model.Size, point: Model.Point, cell: Model.Cell) {
            this.dataContext = cell;
            this.point       = point;
            this._mesh       = CellView.createCube(this.getPosition(wholeSize), this.cubeSize);
            this.setColor(Converter.ToColor(cell.index));
        }

        private getPosition(wholeSize: Model.Size): THREE.Vector3 {
            return new THREE.Vector3(this.cubeDistance * (this.point.x - wholeSize.width  / 2),
                                     this.cubeDistance * (wholeSize.height / 2 - this.point.y),
                                     this.cubeDistance                                        );
        }

        private static createCube(position: THREE.Vector3, size: number): THREE.Mesh {
            var material = new THREE.MeshPhongMaterial({ specular: 0xffffff, shininess: 30.0, metal: true, transparent: true, opacity: 0.75 });
            var cube     = new THREE.Mesh(new THREE.CubeGeometry(size, size, size), material);
            cube.position.set(position.x, position.y, position.z);
            return cube;
        }
    }

    export class Scene {
        private scene   : THREE.Scene  = new THREE.Scene();
        private _camera : THREE.Camera = null;
        private renderer: THREE.WebGLRenderer;
        private controls: THREE.OrbitControls;

        protected initialize(element: HTMLElement): void {
            if (!Detector.webgl)
                Detector.addGetWebGLMessage();

            this._camera = this.createCamera();
            this.controls = new THREE.OrbitControls(this.camera);

            var lights = this.createLights();
            if (lights != null)
                lights.forEach((light, _, __) => this.scene.add(light));

            var objects = this.createObjects();
            if (objects != null)
                objects.forEach(object => this.scene.add(object));

            this.renderer = Scene.createRenderer(element);
            this.render();

            window.addEventListener('resize', () => this.onResize(), false);
        }

        protected get camera(): THREE.Camera { return this._camera; }

        protected onResize(): void {
            this.renderer.setSize(window.innerWidth, window.innerHeight);
        }

        protected onRender(): void {
            this.controls.update();
            this.renderer.clear();
            this.renderer.render(this.scene, this.camera);
        }

        protected createCamera(): THREE.PerspectiveCamera {
            throw new Error("You must implement initializeCamera.");
            return null;
        }

        protected createLights(): THREE.Light[] {
            return null;
        }

        protected createObjects(): THREE.Object3D[] {
            return null;
        }

        private static createRenderer(element: HTMLElement): THREE.WebGLRenderer {
            var renderer = new THREE.WebGLRenderer();
            renderer.setSize(window.innerWidth, window.innerHeight);
            element.appendChild(renderer.domElement);
            return renderer;
        }

        private render(): void {
            requestAnimationFrame(() => this.render());
            this.onRender();
        }
    }

    export class TurningScene extends Scene {
        cameraDistance = 24.0;

        protected onResize(): void {
            super.onResize();
            (<THREE.PerspectiveCamera>this.camera).aspect = TurningScene.getCameraAspect();
            (<THREE.PerspectiveCamera>this.camera).updateProjectionMatrix();
        }

        protected onRender(): void {
            super.onRender();
            if (this.camera != null)
                TurningScene.setCameraAngle(this.camera, TurningScene.getCameraAngle(this.camera) + 1.0);
        }

        protected createCamera(): THREE.PerspectiveCamera {
            var camera = new THREE.PerspectiveCamera(75.0, TurningScene.getCameraAspect(), 0.1, 1000.0);
            TurningScene.setCameraDistance(camera, this.cameraDistance);
            TurningScene.setCameraAngle(camera, 0.0);
            //camera.lookAt(new THREE.Vector3(0.0, 0.0, 0.0));
            return camera;
        }

        private static getCameraAngle(camera: THREE.Camera): number {
            return camera.position.x == 0.0 && camera.position.z == 0.0
                ? 0.0
                : Model.Mathematics.radianToDegree(Math.atan2(camera.position.x, camera.position.z));
        }

        private static setCameraAngle(camera: THREE.Camera, cameraAngle: number): void {
            let radian         = Model.Mathematics.degreeToRadian(cameraAngle);
            let cameraDistance = TurningScene.getCameraDistance(camera);
            camera.position.x  = cameraDistance * Math.sin(radian);
            camera.position.z  = cameraDistance * Math.cos(radian);
        }

        private static getCameraDistance(camera: THREE.Camera): number {
            return TurningScene.getDistance(new THREE.Vector2(camera.position.x, camera.position.z), new THREE.Vector2(0.0, 0.0));
        }

        private static setCameraDistance(camera: THREE.Camera, distance: number): void {
            let cameraAngle   = TurningScene.getCameraAngle(camera);
            camera.position.x = 0.0;
            camera.position.z = distance;
            TurningScene.setCameraAngle(camera, cameraAngle);
        }

        private static getCameraAspect(): number {
            return window.innerWidth / window.innerHeight;
        }

        private static getDistance(position1: THREE.Vector2, position2: THREE.Vector2): number {
            return position1.sub(position2).length();
        }
    }

    export class GameBoardView extends TurningScene {
        private _dataContext: Model.GameBoard = null;
        private cellViews: CellView[][] = null;

        public constructor(element: HTMLElement, gameBoard: Model.GameBoard) {
            super();
            this._dataContext = gameBoard;
            this.cellViews = Model.TwoDimensionalArray.create<CellView>(gameBoard.visibleSize);
            Model.TwoDimensionalArray.forEach(gameBoard.visibleCells, (point, cell) => Model.TwoDimensionalArray.set(this.cellViews, point, new CellView(gameBoard.visibleSize, point, cell)));
            this.initialize(element);
        }

        public onKeyDown(keyCode: number): boolean {
            switch (keyCode) {
                case 13 /* Enter */: this.start    (     ); return true;
                case 32 /* Space */: this.drop     (     ); return true;
                case 37 /* Left  */: this.moveLeft (     ); return true;
                case 38 /* Up    */: this.turn     (     ); return true;
                case 39 /* Right */: this.moveRight(     ); return true;
                case 40 /* Down  */: this.turn     (false); return true;
            }
            return false;
        }

        public onKeyUp(keyCode: number): boolean {
            switch (keyCode) {
                case 13 /* Enter */:
                case 32 /* Space */:
                case 37 /* Left  */:
                case 38 /* Up    */:
                case 39 /* Right */:
                case 40 /* Down  */:
                    return true;
            }
            return false;
        }

        protected createLights(): THREE.Light[] {
            var directionalLight = new THREE.DirectionalLight(0xcccccc);
            //directionalLight.position.set(1.0, 1.0, 1.0);
            return [directionalLight, new THREE.AmbientLight(0x888888)];
        }

        protected createObjects(): THREE.Object3D[] {
            return Model.Enumerable.select(Model.TwoDimensionalArray.toSequence(this.cellViews), cellView => cellView.mesh);
        }

        private start    (                         ): void { this._dataContext.start    (         ); }
        private moveRight(                         ): void { this._dataContext.moveRight(         ); }
        private moveLeft (                         ): void { this._dataContext.moveLeft (         ); }
        private turn     (clockwise: boolean = true): void { this._dataContext.turn     (clockwise); }
        private drop(): void { this._dataContext.drop(); }
    }

    export class Application {
        game                         = new Model.Game();
        gameBoardView: GameBoardView = null;

        public constructor() {
            this.game.scoreUpdated     = score         => this.setScore        (score        );
            this.game.nextPolyominoSet = nextPolyomino => this.setNextPolyomino(nextPolyomino);

            document.addEventListener("keydown", e => { if (this.gameBoardView != null && this.gameBoardView.onKeyDown(e.keyCode)) e.returnValue = false });
            document.addEventListener("keyup", e => { if (this.gameBoardView != null && this.gameBoardView.onKeyUp(e.keyCode)) e.returnValue = false });
            document.addEventListener("DOMContentLoaded",
                () => this.gameBoardView = new GameBoardView(document.getElementById("canvas"), this.game.board));

            setInterval(() => this.game.step(), 1000);
        }

        private setScore(score: number): void {
            document.getElementById("score").innerText = score.toString();
        }

        private setNextPolyomino(nextPolyomino: Model.Tetromono): void {
            document.getElementById("nextPolyomino").innerText = nextPolyomino.toString();
        }
    }
}
