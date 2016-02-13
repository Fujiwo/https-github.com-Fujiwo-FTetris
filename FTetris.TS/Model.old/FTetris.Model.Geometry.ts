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

        //public static getRandomNumber(miminum: number, maximum: number): number {
        //    return Random.nextDoubleBetween(miminum, maximum);
        //    //return VisualBasicRnd.nextDoubleBetween(miminum, maximum);
        //    //return XorShift128.nextDoubleBetween(miminum, maximum);
        //}
    }

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
        
        public subtract(size: Size): Point {
            return new Point(this.x - size.width, this.y - size.height);
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

        public divide(value: number): Size {
            return new Size(this.width / value, this.height / value);
        }
    }
}
