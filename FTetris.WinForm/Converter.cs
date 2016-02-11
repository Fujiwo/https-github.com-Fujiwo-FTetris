using FTetris.Model;
using System.Diagnostics.Contracts;
using System.Drawing;

namespace FTetris.WinForm
{
    static class Converter
    {
        class Palette
        {
            static readonly Color[] colors = new[] {
                Color.Black  ,
                Color.Red    ,
                Color.Green  ,
                Color.Yellow ,
                Color.Blue   ,
                Color.Magenta,
                Color.Cyan   ,
                Color.White
            };

            static readonly Brush[] brushes;

            public static int ColorNumber { get { return colors.Length; } }

            static Palette()
            {
                brushes = new Brush[ColorNumber];
                colors.ForEach((index, color) => brushes[index] = new SolidBrush(ToColor(index)));
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
