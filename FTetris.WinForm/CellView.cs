using FTetris.Model;
using System;
using System.Drawing;

namespace FTetris.WinForm
{
    class CellView
    {
        public event Action<CellView> StateChanged;

        StatefulCell dataContext = null;

        public StatefulCell DataContext {
            get { return dataContext; }
            set {
                if (value != dataContext) {
                    if (dataContext != null)
                        dataContext.StateChanged -= OnDataContextStateChanged;
                    dataContext = value;
                    value.StateChanged += OnDataContextStateChanged;
                }
            }
        }

        public Point<int> Point { get; set; }
        public Rectangle Position { get; set; }

        public Brush Color {
            get { return Converter.StateIndexToBrush(DataContext.StateIndex); }
        }

        public Rectangle GetPosition(Size wholeSize, Size<int> wholeNumber)
        { return GetPosition(wholeSize, wholeNumber, Point); }

        public static Rectangle GetPosition(Size wholeSize, Size<int> wholeNumber, Point<int> point)
        {
            var width  = wholeSize.Width  / wholeNumber.Width ;
            var height = wholeSize.Height / wholeNumber.Height;
            return new Rectangle(x     : width  * point.X,
                                 y     : height * point.Y,
                                 width : width           ,
                                 height: height          );
        }

        public void Paint(Graphics graphics)
        { graphics.FillRectangle(Color, Position); }

        void OnDataContextStateChanged(StatefulCell cell, int stateIndex)
        { StateChanged?.Invoke(this); }
    }
}
