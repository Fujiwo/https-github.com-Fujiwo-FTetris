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
                //Color.FromRgb(r: 0xff, g: 0x80, b: 0x00),
                //Color.FromRgb(r: 0xff, g: 0x00, b: 0x80),
                //Color.FromRgb(r: 0x00, g: 0xff, b: 0x80),
                //Color.FromRgb(r: 0x80, g: 0xff, b: 0x00),
                //Color.FromRgb(r: 0x80, g: 0x00, b: 0xff),
                //Color.FromRgb(r: 0x00, g: 0x80, b: 0xff)
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

            public static Brush ToBrush(int index)
            {
                Contract.Assert(index >= 0 && index < ColorNumber);
                return brushes[index];
            }
        }

        public static Brush StateIndexToBrush(int colorIndex)
        { return Palette.ToBrush(colorIndex); }
    }
}
