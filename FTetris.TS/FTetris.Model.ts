namespace FTetris.Model {
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
    }

    export class Point {
        private _x: number = 0;

        public get x(): number { return this._x; }
        public set x(value: number) { this._x = value; }

        private _y: number = 0;

        public get y(): number { return this._y; }
        public set y(value: number) { this._y = value; }

        public constructor(x: number, y: number) {
            this.x = x;
            this.y = y;
        }

        public equals(point: Point): boolean {
            return this.x == point.x && this.y == point.y;
        }

        public add(point: Point): Point {
            return new Point(this.x + point.x, this.y + point.y);
        }

        public addSize(size: Size): Point {
            return new Point(this.x + size.width, this.y + size.height);
        }

        public subtract(size: Size): Point {
            return new Point(this.x - size.width, this.y - size.height);
        }
    }

    export class Size {
        private _width: number = 0;

        public get width(): number { return this._width; }
        public set width(value: number) { this._width = value; }

        private _height: number = 0;

        public get height(): number { return this._height; }
        public set height(value: number) { this._height = value; }

        public constructor(width: number, height: number) {
            this.width = width;
            this.height = height;
        }

        public equals(size: Size): boolean { return this.width == size.width && this.height == size.height; }

        public invert(): Size { return new Size(this.height, this.width); }

        public subtract(size: Size): Size {
            return new Size(this.width - size.width, this.height - size.height);
        }

        public divide(value: number): Size {
            return new Size(this.width / value, this.height / value);
        }
    }

    export class Enumerable {
        public static range(start: number, count: number): number[] {
            var array = new Array<number>();
            for (var value = start; value < start + count; value++)
                array.push(value);
            return array;
        }

        public static where<T>(array: T[], predicate: (T) => boolean): T[] {
            var resultArray = new Array<T>();
            array.forEach(value => {
                if (predicate(value))
                    resultArray.push(value);
            });
            return resultArray;
        }

        public static select<T, S>(array: T[], predicate: (T) => S): S[] {
            var resultArray = new Array<S>();
            array.forEach(value => resultArray.push(predicate(value)));
            return resultArray;
        }

        public static all<T>(array: T[], predicate: (T) => boolean): boolean {
            for (var index = 0; index < array.length; index++) {
                if (!predicate(array[index]))
                    return false;
            }
            return true;
        }
    }

    export class TwoDimensionalArray {
        public static create<T>(size: Size): T[][] {
            var array = new Array<Array<T>>(size.width);
            for (var index = 0; index < size.width; index++)
                array[index] = new Array<T>(size.height);
            return array;
        }

        public static allPoints<T>(array: T[][]): Point[] {
            var points = new Array<Point>();
            for (var x = 0; x < array.length; x++) {
                for (var y = 0; y < array[x].length; y++)
                    points.push(new Point(x, y));
            }
            //array.forEach((yArray, x) => yArray.forEach((element, y) => points.push(new Point(x, y))));
            return points;
        }

        public static get<T>(array: T[][], point: Point): T { return array[point.x][point.y]; }

        public static tryGet<T>(array: T[][], point: Point): [boolean, T] {
            return point.x < 0 || point.x >= array.length ||
                point.y < 0 || point.y >= array[point.x].length
                ? [false, null]
                : [true, TwoDimensionalArray.get(array, point)];
        }

        public static set<T>(array: T[][], point: Point, value: T): void { array[point.x][point.y] = value; }

        public static trySet<T>(array: T[][], point: Point, value: T): boolean {
            if (point.x < 0 || point.x >= array.length ||
                point.y < 0 || point.y >= array[point.x].length)
                return false;
            TwoDimensionalArray.set(array, point, value);
            return true;
        }

        public static getRow<T>(array: T[][], y: number): T[] {
            return Enumerable.select(Enumerable.range(0, array.length), x => array[x][y]);
        }

        public static getColumn<T>(array: T[][], x: number): T[] {
            return Enumerable.select(Enumerable.range(0, array[x].length), y => array[x][y]);
        }

        public static size<T>(array: T[][]): Size {
            var width = array.length;
            return new Size(width, width == 0 ? 0 : array[0].length);
        }

        public static forEach<T>(array: T[][], action: (Point, T) => void): void
        { TwoDimensionalArray.allPoints(array).forEach(point => action(point, TwoDimensionalArray.get(array, point))); }

        public static toSequence<T>(array: T[][]): T[]
        { return Enumerable.select(TwoDimensionalArray.allPoints(array), point => TwoDimensionalArray.get(array, point)); }

        public static selectMany<T>(array: T[][]): (Point | T)[][] {
            //var allPoints: Point[] = TwoDimensionalArray.allPoints(array);
            //var selected = Enumerable.select(allPoints, (point: Point) => {
            //    var element = TwoDimensionalArray.get(array, point);
            //    return [point, element];
            //});
            //return selected;

            return Enumerable.select(TwoDimensionalArray.allPoints(array), point => [point, TwoDimensionalArray.get(array, point)]);
        }

        public static turn<T>(array: T[][], clockwise: boolean = true): T[][] {
            var size = TwoDimensionalArray.size(array);
            var turnedArray = TwoDimensionalArray.create<T>(size.invert());
            TwoDimensionalArray.forEach(array, (point, element) => {
                TwoDimensionalArray.set(turnedArray,
                    clockwise ? new Point(size.height - 1 - point.y, point.x)
                        : new Point(point.y, size.width - 1 - point.x),
                    element);
            });
            return turnedArray;
        }

        public static clone<T>(array: T[][]): T[][] {
            var clonedArray = TwoDimensionalArray.create<T>(TwoDimensionalArray.size(array));
            TwoDimensionalArray.forEach(array, (point, element) => TwoDimensionalArray.set(clonedArray, point, element));
            return clonedArray;
        }
    }

    export class Tetromono {
        private _index: number = 1;

        public get index(): number      { return this._index;  }
        public set index(value: number) { this._index = value; }

        private shape: boolean[][];

        private table: boolean[][][] = [
            [[ false, false, false, false ],
             [ false, false, false, false ],
             [ false, false, false, false ],
             [ false, false, false, false ]],
            [[ false, false, false, false ],
             [ true , true , true , true  ],
             [ false, false, false, false ],
             [ false, false, false, false ]],
            [[ false, false, true , false ],
             [ false, true , true , false ],
             [ false, true , false, false ],
             [ false, false, false, false ]],
            [[ false, true , false, false ],
             [ false, true , true , false ],
             [ false, false, true , false ],
             [ false, false, false, false ]],
            [[ false, false, false, false ],
             [ false, true , true , false ],
             [ false, true , true , false ],
             [ false, false, false, false ]],
            [[ false, false, false, false ],
             [ false, false, true , false ],
             [ true , true , true , false ],
             [ false, false, false, false ]],
            [[ false, false, false, false ],
             [ false, false, true , false ],
             [ false, true , true , false ],
             [ false, false, true , false ]],
            [[ false, false, false, false ],
             [ true , true , true , false ],
             [ false, false, true , false ],
             [ false, false, false, false ]]
        ];

        private _position: Point;

        public get position(): Point      { return this._position;  }
        public set position(value: Point) { this._position = value; }

        public get size(): Size { return TwoDimensionalArray.size(this.shape); }

        public constructor() {
           this.index = this.getRandomIndex();
           this.shape = TwoDimensionalArray.clone<boolean>(this.table[this.index]);
        }

        public getPosition(point: Point): Point
        { return Tetromono.getPosition(this.position, point); }

        public static getPosition(position: Point, point: Point): Point
        { return position.add(point); }

        public get allPoints(): Point[] { return TwoDimensionalArray.allPoints(this.shape); }

        public get(point: Point): boolean
        { return TwoDimensionalArray.get(this.shape, point); }

        public move(cellsClone: number[][], position: Point): boolean {
            this.erase(cellsClone);
            return this.place(cellsClone, position);
        }

        public erase(cellsClone: number[][]): void {
            Enumerable.where(this.allPoints, point => this.get(point))
                      .forEach(point => TwoDimensionalArray.set(cellsClone, this.getPosition(point), 0));
        }

        public place(cellsClone: number[][], position: Point): boolean {
            var placeablePoints = Tetromono.placeablePoints(this.shape, cellsClone, position);
            if (placeablePoints == null)
                return false;
            placeablePoints.forEach(point => TwoDimensionalArray.set(cellsClone, Tetromono.getPosition(position, point), this.index));
            this.position = position;
            return true;
        }

        private _turn(clockwise: boolean = true): boolean[][]
        { return TwoDimensionalArray.turn(this.shape, clockwise); }

        public turn(cellsClone: number[][], clockwise: boolean = true): boolean {
            this.erase(cellsClone);

            var newShape = this._turn(clockwise);
            var placeablePoints = Tetromono.placeablePoints(newShape, cellsClone, this.position);
            if (placeablePoints == null)
                return false;
            placeablePoints.forEach(point => TwoDimensionalArray.set(cellsClone, Tetromono.getPosition(this.position, point), this.index));
            this.shape = newShape;
            return true;
        }

        public toString(): string {
            var text = ["None", "I", "N", "Z", "O", "J", "T", "L"];
            return text[this.index];
        }

        static placeablePoints(shape: boolean[][], cellsClone: number[][], position: Point): Point[] {
            var exsitingPoints  = Enumerable.where(TwoDimensionalArray.allPoints(shape), point => TwoDimensionalArray.get(shape, point));
            var placeablePoints = Enumerable.where(exsitingPoints, point => {
                var value = TwoDimensionalArray.tryGet(cellsClone, Tetromono.getPosition(position, point));
                return value[0] ? value[1] == 0 : false;
            });
            return exsitingPoints.length == placeablePoints.length ? placeablePoints : null;
        }

        private getRandomIndex(): number
        { return Math.floor(Math.random() * (this.table.length - 1)) + 1; }
    }

    export class Cell {
        public indexChanged: (cell: Cell, index: number) => void = null;

        private _index: number = 0;

        public get index(): number { return this._index; }
        public set index(value: number) {
            if (value != this._index) {
                this._index = value;
                if (this.indexChanged != null)
                    this.indexChanged(this, value);
            }
        }

        public constructor(index: number = 0) {
            this.index = index;
        }
    }

    export class CellBoard {
        private _size;

        public get size(): Size { return this._size; }

        public get actualSize(): Size { return this.size; }

        private _cells: Cell[][] = null;

        public get cells(): Cell[][] { return this._cells; }

        public get actualCells(): Cell[][] { return this.cells; }

        public get cellsClone(): number[][] {
            var cellsClone = TwoDimensionalArray.create<number>(this.size);
            TwoDimensionalArray.forEach(this.cells, (point, cell) => TwoDimensionalArray.set(cellsClone, point, cell.index));
            return cellsClone;
        }

        public set cellsClone(value: number[][]) {
            TwoDimensionalArray.forEach(this.cells, (point, cell) => cell.index = TwoDimensionalArray.get(value, point));
        }

        public constructor(size: Size) {
            this._size  = size;
            this._cells = TwoDimensionalArray.create<Cell>(this.size);
            TwoDimensionalArray.forEach(this.cells, (point, cell) => TwoDimensionalArray.set(this.cells, point, new Cell()));
        }

        public clear(): void { TwoDimensionalArray.forEach(this.cells, (point, cell) => cell.index = 0); }
    }

    export class MaskedCellBoard extends CellBoard {
        private _topMask: number = 2;

        public get topMask(): number      { return this._topMask;  }
        public set topMask(value: number) { this._topMask = value; }

        public get actualSize(): Size { return new Size(this.size.width, this.size.height - this.topMask ); }

        public get actualCells(): Cell[][] {
            var actualCells = TwoDimensionalArray.create<Cell>(this.actualSize);
            TwoDimensionalArray.allPoints(actualCells)
                               .forEach(point => TwoDimensionalArray.set(actualCells, point, TwoDimensionalArray.get(this.cells, point.addSize(new Size(0, this.topMask)))));
            return actualCells;
        }

        public constructor(size: Size) {
            super(size);
        }
    }

    class ScoreBoard {
        public scoreUpdated: (score: number) => void = null;

        private defaultScorePoint: number = 10;

        private scorePoint: number = 0;

        private _score: number = 0;

        public get score(): number { return this._score; }
        public set score(value: number) {
            if (value != this._score) {
                this._score = value;
                if (this.scoreUpdated != null)
                    this.scoreUpdated(value);
            }
        }

        public constructor() { this.reset(); }

        public reset(): void {
            this.score = 0;
            this.scorePoint = this.defaultScorePoint;
        }

        public startAdding(): void { this.scorePoint = this.defaultScorePoint; }

        public add(): void {
            this.score      += this.scorePoint;
            this.scorePoint *= 2;
        }
    }

    const GameBoardDefaultWidth  = 10;
    const GameBoardDefaultHeight = 32;

    export class GameBoard extends MaskedCellBoard {
        public gameStarted     : (                    ) => void = null;
        public gameOver        : (                    ) => void = null;
        public nextPolyominoSet: (polyomino: Tetromono) => void = null;
        public scoreUpdated    : (score    : number   ) => void = null;

        private scoreBoard: ScoreBoard = new ScoreBoard();

        public get score(): number { return this.scoreBoard.score; }

        private _nextPolyomino: Tetromono = null;

        public get nextPolyomino(): Tetromono { return this._nextPolyomino; }

        private setNextPolyomino(value: Tetromono) {
            if (value != this._nextPolyomino) {
                this._nextPolyomino = value;
                if (this.nextPolyominoSet != null)
                    this.nextPolyominoSet(value);
            }
        }

        private currentPolyomino: Tetromono = new Tetromono();

        private isStarted: boolean = false;

        public constructor() {
            super(new Size(GameBoardDefaultWidth, GameBoardDefaultHeight));
            this.scoreBoard.scoreUpdated = score => {
                if (this.scoreUpdated != null)
                    this.scoreUpdated(score);
            }
        }

        public start(): void {
            this.clear();
            this.setNextPolyomino(new Tetromono());
            this.place(this.currentPolyomino);
            this.scoreBoard.reset();
            this.isStarted = true;
            if (this.gameStarted != null)
                this.gameStarted();
        }

        public step(): void {
            if (this.isStarted)
                this.down();
        }

        public moveLeft(): boolean { return this.isStarted && this._moveLeft(this.currentPolyomino); }
        public moveRight(): boolean { return this.isStarted && this._moveRight(this.currentPolyomino); }
        public turn(clockwise: boolean = true): boolean { return this.isStarted && this._turn(this.currentPolyomino, clockwise); }

        private end(): void {
            this.isStarted = false;
            if (this.gameOver != null)
               this.gameOver();
        }

        public drop(): void {
            while (this.isStarted && this.down())
                ;
        }

        private down(): boolean {
            if (this._down(this.currentPolyomino))
                return true;
            this.try();
            if (!this.placeNextPolyomino())
                this.end();
            return false;
        }

        private try(): void {
            var fullRows = this.getFullRows();
            if (fullRows.length > 0) {
                this.scoreBoard.startAdding();
                var cellsClone = this.cellsClone;
                fullRows.forEach(y => {
                    GameBoard.removeRow(cellsClone, y);
                    this.scoreBoard.add();
                });
                this.cellsClone = cellsClone;
            }
        }

        private getFullRows(): number[] {
            return Enumerable.where(Enumerable.range(0, TwoDimensionalArray.size(this.cells).height),
                                    y => Enumerable.all(TwoDimensionalArray.getRow(this.cells, y), cell => cell.index != 0));
        }

        private static removeRow(cellsClone: number[][], y: number): void {
            for (var yIndex = y; yIndex > 0; yIndex--)
                Enumerable.range(0, TwoDimensionalArray.size(cellsClone).width).forEach(x => cellsClone[x][yIndex] = cellsClone[x][yIndex - 1]);
            Enumerable.range(0, TwoDimensionalArray.size(cellsClone).width).forEach(x => cellsClone[x][0] = 0);
        }

        private place(polyomino: Tetromono): boolean {
            var position = new Point((this.size.width - polyomino.size.width) / 2, 0);
            var cellsClone = this.cellsClone;
            if (polyomino.place(cellsClone, position)) {
                this.cellsClone = cellsClone;
                return true;
            }
            return false;
        }

        private _down(polyomino: Tetromono): boolean { return this.move(new Point(polyomino.position.x, polyomino.position.y + 1), polyomino); }

        private _moveLeft(polyomino: Tetromono): boolean { return this.move(new Point(polyomino.position.x - 1, polyomino.position.y), polyomino); }

        private _moveRight(polyomino: Tetromono): boolean { return this.move(new Point(polyomino.position.x + 1, polyomino.position.y), polyomino); }

        private _turn(currentPolyomino: Tetromono, clockwise: boolean = true): boolean {
            var cellsClone = this.cellsClone;
            if (currentPolyomino.turn(cellsClone, clockwise)) {
                this.cellsClone = cellsClone;
                return true;
            }
            return false;
        }

        private move(position: Point, polyomino: Tetromono): boolean {
            var cellsClone = this.cellsClone;
            if (polyomino.move(cellsClone, position)) {
                this.cellsClone = cellsClone;
                return true;
            }
            return false;
        }

        private placeNextPolyomino(): boolean {
            this.currentPolyomino = this.nextPolyomino;
            if (this.place(this.currentPolyomino)) {
                this.setNextPolyomino(new Tetromono());
                return true;
            }
            return false;
        }
    }

    export class PolyominoBoard extends CellBoard
    {
        public constructor() {
            super(new Tetromono().size);
        }

        public place(polyomino: Tetromono): boolean
        {
            this.clear();

            var position = new Point(0, 0).addSize(this.size.subtract(polyomino.size).divide(2));
            var cellsClone = this.cellsClone;
            if (polyomino.place(cellsClone, position)) {
                this.cellsClone = cellsClone;
                return true;
            }
            return false;
        }
    }

    export class Game {
        public nextPolyominoSet: (polyomino: Tetromono) => void = null;

        _gameBoard: GameBoard = new GameBoard();

        public get gameBoard(): GameBoard { return this._gameBoard; }

        _polyominoBoard: PolyominoBoard = new PolyominoBoard();
         
        public get polyominoBoard(): PolyominoBoard { return this._polyominoBoard; }

        public constructor()
        {
            this.gameBoard.nextPolyominoSet = polyomino => {
                this.polyominoBoard.place(polyomino);
                if (this.nextPolyominoSet != null)
                    this.nextPolyominoSet(polyomino);
            }
        }
    }
}
