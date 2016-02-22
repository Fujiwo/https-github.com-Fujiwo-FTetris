using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;

namespace FTetris.Model
{
    public static class TwoDimensionalArrayExtension
    {
        public static IEnumerable<Point<int>> AllPoints<T>(this T[,] @this)
        {
            return from x in Enumerable.Range(0, @this.GetLength(0))
                   from y in Enumerable.Range(0, @this.GetLength(1))
                   select new Point<int> { X = x, Y = y };
        }

        public static T Get<T>(this T[,] @this, Point<int> point)
        { return @this[point.X, point.Y]; }

        public static bool TryGet<T>(this T[,] @this, Point<int> point, out T value)
        {
            if (point.X < 0 || point.X > @this.GetUpperBound(0) ||
                point.Y < 0 || point.Y > @this.GetUpperBound(1)) {
                value = default(T);
                return false;
            }
            value = @this.Get(point);
            return true;
        }

        public static void Set<T>(this T[,] @this, Point<int> point, T value)
        { @this[point.X, point.Y] = value; }

        public static bool TrySet<T>(this T[,] @this, Point<int> point, T value)
        {
            if (point.X < 0 || point.X > @this.GetUpperBound(0) ||
                point.Y < 0 || point.Y > @this.GetUpperBound(1))
                return false;
            @this.Set(point, value);
            return true;
        }

        public static IEnumerable<T> GetRow<T>(this T[,] @this, int y)
        {
            Contract.Assert(y >= @this.GetLowerBound(1) && y <= @this.GetUpperBound(1));
            return Enumerable.Range(0, @this.GetLength(0)).Select(x => @this[x, y]);
        }

        public static IEnumerable<T> GetColumn<T>(this T[,] @this, int x)
        {
            Contract.Assert(x >= @this.GetLowerBound(0) && x <= @this.GetUpperBound(0));
            return Enumerable.Range(0, @this.GetLength(1)).Select(y => @this[x, y]);
        }

        public static Size<int> Size<T>(this T[,] @this)
        { return new Size<int> { Width  = @this.GetLength(0),
                                 Height = @this.GetLength(1) }; }

        public static void ForEach<T>(this T[,] @this, Action<Point<int>, T> action)
        { @this.AllPoints().ForEach(point => action(point, @this.Get(point))); }

        public static IEnumerable<T> ToSequence<T>(this T[,] @this)
        { return @this.AllPoints().Select(point => @this.Get(point)); }

        public static IEnumerable<Tuple<Point<int>, T>> SelectMany<T>(this T[,] @this)
        { return @this.AllPoints().Select(point => new Tuple<Point<int>, T>(point, @this.Get(point))); }

        public static T[,] Turn<T>(this T[,] @this, bool clockwise = true)
        {
            var array = new T[@this.GetLength(1), @this.GetLength(0)];
            @this.ForEach((point, item) => array.Set(clockwise ? new Point<int> { X = @this.GetUpperBound(1) - point.Y, Y = point.X }
                                                               : new Point<int> { X = point.Y, Y = @this.GetUpperBound(0) - point.X }, item));
            return array;
        }

        public static bool IsEqual<T>(this T[,] @this, T[,] array)
        { return @this.Size().Equals(array.Size()) &&
                 @this.SelectMany().All(pointedItem => pointedItem.Item2.Equals(array.Get(pointedItem.Item1))); }

        public static T[,] Create<T>(Size<int> size)
        { return new T[size.Width, size.Height]; }
    }
}
