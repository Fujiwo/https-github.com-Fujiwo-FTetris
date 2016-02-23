using FTetris.Model;
using Microsoft.Practices.Prism.Mvvm;
using System.Windows;
using System.Windows.Media;

namespace FTetris.WPF.ViewModel
{
    class CellViewModel : BindableBase
    {
        Cell dataContext = null;

        public Cell DataContext {
            get { return dataContext; }
            set {
                if (value != dataContext) {
                    if (dataContext != null)
                        dataContext.IndexChanged -= OnDataContextIndexChanged;
                    dataContext = value;
                    value.IndexChanged += OnDataContextIndexChanged;
                }
            }
        }

        public Point<int> Point { get; set; }
        public Rect Position { get; set; }
        public Thickness Margin => new Thickness(left: Position.Left, top: Position.Top, right: 0.0, bottom: 0.0);

        public Brush Color {
            get { return Converter.PolyominoIndexToBrush(DataContext.Index); }
        }

        public Rect GetPosition(Size wholeSize, Size<int> wholeNumber)
        { return GetPosition(wholeSize, wholeNumber, Point); }

        static Rect GetPosition(Size wholeSize, Size<int> wholeNumber, Point<int> point)
        {
            var width  = wholeSize.Width  / wholeNumber.Width;
            var height = wholeSize.Height / wholeNumber.Height;
            return new Rect(x     : width  * point.X,
                            y     : height * point.Y,
                            width : width           ,
                            height: height          );
        }

        void OnDataContextIndexChanged(Cell cell, PolyominoIndex polyominoIndex)
        { OnPropertyChanged(() => Color); }
    }
}
