using FTetris.Model;
using System.Diagnostics.Contracts;
using System.Windows.Media;

namespace FTetris.WPF.ViewModel
{
    static class Converter
    {
        static class Palette
        {
            static readonly Color[] colors = new[] {
                Colors.Black  ,
                Colors.Red    ,
                Colors.Green  ,
                Colors.Yellow ,
                Colors.Blue   ,
                Colors.Magenta,
                Colors.Cyan   ,
                Colors.White
            };

            static readonly Brush[] brushes;

            public static int ColorNumber { get { return colors.Length; } }

            static Palette()
            {
                brushes = new Brush[ColorNumber];
                colors.ForEach((index, color) => brushes[index] = new SolidColorBrush(ToColor(index)));
            }

            static Color ToColor(int index)
            {
                Contract.Assert(index >= 0 && index < ColorNumber);
                return colors[index];
            }

            public static Brush ToBrush(PolyominoIndex polyominoIndex)
            {
                Contract.Assert((int)polyominoIndex >= 0 && (int)polyominoIndex < ColorNumber);
                return brushes[(int)polyominoIndex];
            }
        }

        public static Brush PolyominoIndexToBrush(PolyominoIndex polyominoIndex)
        { return Palette.ToBrush(polyominoIndex); }
    }
}
