namespace FTetris.Model {
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

        //public static isEven<T, S>(array: T[], predicate: (T) => S): boolean {
        //    if (array.length == 0)
        //        return true;
        //    var value = array[0];
        //    return Enumerable.all(array, element => predicate(element) == value);
        //}
    }
}
