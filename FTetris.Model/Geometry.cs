namespace FTetris.Model
{
    public struct Point<T>
    {
        public T X;
        public T Y;

        public Point<T> Invert {
            get { return new Point<T> { X = Y, Y = X }; }
        }
    }

    public struct Size<T>
    {
        public T Width ;
        public T Height;
    }

    public class PointedItem<T>
    {
        public Point<int> Point { get; set; }
        public T          Item  { get; set; }
    }

    public static class GeometryExtension
    {
        public static Point<int> Add(this Point<int> @this, Point<int> point)
        {
            return new Point<int> { X = @this.X + point.X, Y = @this.Y + point.Y };
        }
    }
}
