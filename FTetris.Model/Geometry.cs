namespace FTetris.Model
{
    public struct Point<T>
    {
        public T X;
        public T Y;

        public Point<T> Invert {
            get { return new Point<T> { X = Y, Y = X }; }
        }

        public override bool Equals(object item)
        { return X.Equals(((Point<T>)item).X) &&
                 Y.Equals(((Point<T>)item).Y); }

        public override int GetHashCode()
        { return X.GetHashCode() + Y.GetHashCode(); }
    }

    public struct Size<T>
    {
        public T Width ;
        public T Height;

        public override bool Equals(object item)
        { return Width .Equals(((Size<T>)item).Width ) &&
                 Height.Equals(((Size<T>)item).Height); }

        public override int GetHashCode()
        { return Width.GetHashCode() + Height.GetHashCode(); }
    }

    public static class GeometryExtension
    {
        public static Point<int> Add(this Point<int> @this, Point<int> point)
        { return new Point<int> { X = @this.X + point.X, Y = @this.Y + point.Y }; }

        public static Point<int> Add(this Point<int> @this, Size<int> size)
        { return new Point<int> { X = @this.X + size.Width, Y = @this.Y + size.Height }; }

        public static Size<int> Subtract(this Size<int> @this, Size<int> size)
        { return new Size<int> { Width = @this.Width - size.Width, Height = @this.Width - size.Height }; }

        public static Size<int> Divide(this Size<int> @this, int value)
        { return new Size<int> { Width = @this.Width / value, Height = @this.Width / value }; }
    }
}
