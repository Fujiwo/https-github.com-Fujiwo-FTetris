namespace FTetris.Model
{
    //export const enum PolyominoIndex {
    //    // http://sansu-seijin.jp/blog/archives/947
    //    None, I, N, Z, O, J, T, L
    //}

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
        private defaultWidth  = 10;
        private defaultHeight = 32;

        public get size(): Size { return new Size(this.defaultWidth, this.defaultHeight); }

        private _cells: Cell[][] = null;

        public get cells(): Cell[][] { return this._cells; }

        public get cellsClone(): number[][] {
            var cellsClone = TwoDimensionalArray.create<number>(this.size);
            TwoDimensionalArray.forEach(this.cells, (point, cell) => TwoDimensionalArray.set(cellsClone, point, cell.index));
            return cellsClone;
        }

        public set cellsClone(value: number[][]) {
            TwoDimensionalArray.forEach(this.cells, (point, cell) => cell.index = TwoDimensionalArray.get(value, point));
        }

        public constructor() {
            this._cells = TwoDimensionalArray.create<Cell>(this.size);
            TwoDimensionalArray.forEach(this.cells, (point, cell) => TwoDimensionalArray.set(this.cells, point, new Cell()));
        }

        public clear(): void { TwoDimensionalArray.forEach(this.cells, (point, cell) => cell.index = 0); }
    }

    export class MaskedCellBoard extends CellBoard {
        private _topMask: number = 2;

        public get topMask(): number      { return this._topMask;  }
        public set topMask(value: number) { this._topMask = value; }

        public get visibleSize(): Size { return new Size(this.size.width, this.size.height - this.topMask ); }

        public get visibleCells(): Cell[][] {
            var visibleCells = TwoDimensionalArray.create<Cell>(this.visibleSize);
            for (var x = 0; x < this.visibleSize.width; x++) {
                for (var y = 0; y < this.visibleSize.height; y++)
                    visibleCells[x][y] = this.cells[x][y + this.topMask];
            }
            return visibleCells;
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
            super();
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

    export class Game {
        public gameStarted     : (                    ) => void = null;
        public gameOver        : (                    ) => void = null;
        public nextPolyominoSet: (polyomino: Tetromono) => void = null;
        public scoreUpdated    : (score    : number   ) => void = null;

        private _board: GameBoard = new GameBoard();

        public get board(): GameBoard { return this._board; }

        public get nextPolyomino(): Tetromono { return this.board.nextPolyomino; }

        public get score(): number { return this.board.score; }


        public constructor() {
            this.board.gameStarted      = ()        => { if (this.gameStarted      != null) this.gameStarted     (         ); }
            this.board.gameOver         = ()        => { if (this.gameOver         != null) this.gameOver        (         ); }
            this.board.nextPolyominoSet = polyomino => { if (this.nextPolyominoSet != null) this.nextPolyominoSet(polyomino); }
            this.board.scoreUpdated     = score     => { if (this.scoreUpdated     != null) this.scoreUpdated    (score    ); }
        }

        public start    (                         ): void { this.board.start    (         ); }
        public step     (                         ): void { this.board.step     (         ); }
        public moveLeft (                         ): void { this.board.moveLeft (         ); }
        public moveRight(                         ): void { this.board.moveRight(         ); }
        public turn     (clockwise: boolean = true): void { this.board.turn     (clockwise); }
        public drop     (                         ): void { this.board.drop     (         ); }
    }
}