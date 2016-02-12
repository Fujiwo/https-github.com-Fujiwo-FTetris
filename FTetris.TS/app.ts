/// <reference path="scripts/typings/threejs/three.d.ts" />

namespace FK.Lib {
    const INTEGER32_MAXVALUE = 2147483647.0;

    export class Random {
        public static nextDouble(): number {
            return Math.random();
        }

        public static nextDoubleBetween(miminum: number, maximum: number): number {
            return miminum + Random.nextDouble() * (maximum - miminum);
        }
    }

    export class VisualBasicRnd {
        private static seed: number = 327680;

        public static setSeed(seed: number): void {
            VisualBasicRnd.seed = seed;
        }

        public static nextDouble(): number {
            VisualBasicRnd.seed = VisualBasicRnd.seed * 16598013 + 12820163 & 16777215;
            return VisualBasicRnd.seed / (16777215 + 1.0);
        }

        public static nextDoubleBetween(miminum: number, maximum: number): number {
            return miminum + VisualBasicRnd.nextDouble() * (maximum - miminum);
        }
    }

    export class XorShift128 {
        private static seed = {
            x: 123456789,
            y: 362436069,
            z: 521288629,
            w: +new Date() // 88675123
        };

        public static setSeed(seed: number): void {
            XorShift128.seed.w = seed;
        }

        public static nextInteger(): number {
            var t = XorShift128.seed.x ^ (XorShift128.seed.x << 11);
            XorShift128.seed.x = XorShift128.seed.y;
            XorShift128.seed.y = XorShift128.seed.z;
            XorShift128.seed.z = XorShift128.seed.w;
            XorShift128.seed.w = (XorShift128.seed.w ^ (XorShift128.seed.w >>> 19)) ^ (t ^ (t >>> 8));
            return XorShift128.seed.w;
        }

        public static nextDouble(): number {
            var w = XorShift128.nextInteger();
            return (w + (INTEGER32_MAXVALUE + 1.0)) / ((INTEGER32_MAXVALUE + 1.0) * 2.0);
        }

        public static nextDoubleBetween(miminum: number, maximum: number): number {
            return miminum + XorShift128.nextDouble() * (maximum - miminum);
        }
    }

    export class Mathematics {
        public static degreeToRadian(degree: number): number {
            return degree / 180.0 * Math.PI;
        }

        public static radianToDegree(radian: number): number {
            return radian / Math.PI * 180.0;
        }

        public static roundDegree(degree: number): number {
            while (degree < 0.0)
                degree += 360.0;
            while (degree > 360.0)
                degree -= 360.0;
            return degree;
        }

        public static getRandomNumber(miminum: number, maximum: number): number {
            return Random.nextDoubleBetween(miminum, maximum);
            //return VisualBasicRnd.nextDoubleBetween(miminum, maximum);
            //return XorShift128.nextDoubleBetween(miminum, maximum);
        }
    }

    export class Scene {
        private scene: THREE.Scene = new THREE.Scene();
        private camera: THREE.Camera = null;
        private renderer: THREE.WebGLRenderer;
        private controls: THREE.OrbitControls;

        public constructor(element: HTMLElement) {
            if (!Detector.webgl)
                Detector.addGetWebGLMessage();

            this.camera = this.createCamera();
            this.controls = new THREE.OrbitControls(this.camera);

            var lights = this.createLights();
            if (lights != null)
                lights.forEach((light, _, __) => this.scene.add(light));

            var objects = this.createObjects();
            if (objects != null)
                objects.forEach((object, _, __) => this.scene.add(object));

            this.renderer = Scene.createRenderer(element);
            this.render();

            window.addEventListener('resize', () => this.onResize(), false);
        }

        protected getCamera(): THREE.Camera {
            return this.camera;
        }

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
}

namespace RandomTest {
    const CAMERA_DISTANCE = 10.0;
    const CUBE_SIZE = CAMERA_DISTANCE * 0.75;
    const POINT_NUMBER = 100000;
    const POINT_SIZE = 0.01;

    export class MyScene extends FK.Lib.Scene {
        //private cameraAngle: number;

        protected createCamera(): THREE.PerspectiveCamera {
            var camera = new THREE.PerspectiveCamera(75.0, MyScene.getCameraAspect(), 0.1, 1000.0);
            MyScene.setCameraDistance(camera, CAMERA_DISTANCE);
            MyScene.setCameraAngle(camera, 0.0);
            //camera.lookAt(new THREE.Vector3(0.0, 0.0, 0.0));
            return camera;
        }

        protected createLights(): THREE.Light[] {
            var directionalLight = new THREE.DirectionalLight(0xcccccc);
            directionalLight.position = new THREE.Vector3(1.0, 1.0, 1.0);
            return [directionalLight, new THREE.AmbientLight(0x888888)];
        }

        protected createObjects(): THREE.Object3D[] {
            var cube = new THREE.Mesh(new THREE.CubeGeometry(CUBE_SIZE, CUBE_SIZE, CUBE_SIZE),
                new THREE.MeshPhongMaterial({ color: 0x888888, specular: 0xffffff, shininess: 5.0, transparent: true, opacity: 0.25 }));
            return [cube, MyScene.createPoints(CUBE_SIZE)];
        }

        private static createPoints(cubeSize: number): THREE.PointCloud {
            var pointsGeometry = new THREE.Geometry();
            for (var index = 0; index < POINT_NUMBER; index++)
                pointsGeometry.vertices.push(MyScene.getRandomVector3(cubeSize / 2.0));
            return new THREE.PointCloud(pointsGeometry,
                new THREE.PointCloudMaterial({ size: POINT_SIZE, color: 0xf0e0b0 }));
        }

        protected onResize(): void {
            super.onResize();
            (<THREE.PerspectiveCamera>this.getCamera()).aspect = MyScene.getCameraAspect();
            (<THREE.PerspectiveCamera>this.getCamera()).updateProjectionMatrix();
        }

        protected onRender(): void {
            super.onRender();
            if (this.getCamera() != null)
                MyScene.setCameraAngle(this.getCamera(), MyScene.getCameraAngle(this.getCamera()) + 1.0);
        }

        private static getCameraAngle(camera: THREE.Camera): number {
            return camera.position.x == 0.0 && camera.position.z == 0.0
                ? 0.0
                : FK.Lib.Mathematics.radianToDegree(Math.atan2(camera.position.x, camera.position.z));
        }

        private static setCameraAngle(camera: THREE.Camera, cameraAngle: number): void {
            let radian = FK.Lib.Mathematics.degreeToRadian(cameraAngle);
            let cameraDistance = MyScene.getCameraDistance(camera);
            camera.position.x = cameraDistance * Math.sin(radian);
            camera.position.z = cameraDistance * Math.cos(radian);
        }

        private static getCameraDistance(camera: THREE.Camera): number {
            return MyScene.getDistance(new THREE.Vector2(camera.position.x, camera.position.z), new THREE.Vector2(0.0, 0.0));
        }

        private static setCameraDistance(camera: THREE.Camera, distance: number): void {
            let cameraAngle = MyScene.getCameraAngle(camera);
            camera.position.x = 0.0;
            camera.position.z = distance;
            MyScene.setCameraAngle(camera, cameraAngle);
        }

        private static getCameraAspect(): number {
            return window.innerWidth / window.innerHeight;
        }

        private static getRandomVector3(size: number): THREE.Vector3 {
            return new THREE.Vector3(
                FK.Lib.Mathematics.getRandomNumber(-size, size),
                FK.Lib.Mathematics.getRandomNumber(-size, size),
                FK.Lib.Mathematics.getRandomNumber(-size, size)
            );
        }

        private static getDistance(position1: THREE.Vector2, position2: THREE.Vector2): number {
            return position1.sub(position2).length();
        }
    }

    export class Application {
        public constructor() {
            document.addEventListener("DOMContentLoaded",
                () => new MyScene(document.getElementById("canvas")));
        }
    }
}

new RandomTest.Application();
