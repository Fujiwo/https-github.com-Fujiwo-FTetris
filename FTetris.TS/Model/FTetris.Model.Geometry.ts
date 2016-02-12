namespace FTetris.Model {
    export class Point   {
        private _x: number = 0;

        public get x(): number      { return this._x;  }
        public set x(value: number) { this._x = value; }

        private _y: number = 0;

        public get y(): number      { return this._y;  }
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
    }
 
    export class Size {
        private _width: number = 0;

        public get width(): number      { return this._width;   }
        public set width(value: number) { this._width = value;  }

        private _height: number = 0;

        public get height(): number      { return this._height;  }
        public set height(value: number) { this._height = value; }

        public constructor(width: number, height: number) {
            this.width  = width ;
            this.height = height;
        }

        public equals(size: Size): boolean { return this.width == size.width && this.height == size.height; }

        public invert(): Size { return new Size(this.height, this.width); }
    }
}
