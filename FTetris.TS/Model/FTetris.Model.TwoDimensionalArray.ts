namespace FTetris.Model {
    export class TwoDimensionalArray {
        public static create<T>(size: Size): T[][] {
            var array = new Array<Array<T>>(size.width);
            array.forEach((value, index) => array[index] = new Array<T>(size.height));
            return array;
        }

        public static allPoints<T>(array: T[][]): Point[] {
            var points = new Array<Point>();
            array.forEach((yArray, x) => yArray.forEach((element, y) => points.push(new Point(x, y))));
            return points;
        }

        public static get<T>(array: T[][], point: Point): T { return array[point.x][point.y]; }

        public static tryGet<T>(array: T[][], point: Point): [boolean, T] {
            return point.x < 0 || point.x > array         .length ||
                   point.y < 0 || point.y > array[point.x].length
                   ? [false, null]
                   : [true, TwoDimensionalArray.get(array, point)];
        }

        public static set<T>(array: T[][], point: Point, value: T): void { array[point.x][point.y] = value; }

        public static trySet<T>(array: T[][], point: Point, value: T): boolean {
            if (point.x < 0 || point.x > array.length ||
                point.y < 0 || point.y > array[point.x].length)
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

        public static size<T>(array: T[][]): Size  {
            var width = array.length;
            return new Size(width, width == 0 ? 0 : array[0].length);
        }

        public static forEach<T>(array: T[][], action: (Point, T) => void): void
        { TwoDimensionalArray.allPoints(array).forEach(point => action(point, TwoDimensionalArray.get(array, point))); }

        public static toSequence<T>(array: T[][]): T[]
        { return Enumerable.select(TwoDimensionalArray.allPoints(array), point => TwoDimensionalArray.get(array, point)); }

        public static selectMany<T>(array: T[][]): (Point | T)[][]
        {
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
                                        clockwise ? new Point(size.height - point.y, point.x)
                                                  : new Point(point.x, size.width  - point.x),
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
}
