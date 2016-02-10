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
                Color.FromArgb(alpha: 0xff, red: 0xff, green: 0x80, blue: 0x00),
                Color.FromArgb(alpha: 0xff, red: 0xff, green: 0x00, blue: 0x80),
                Color.FromArgb(alpha: 0xff, red: 0x00, green: 0xff, blue: 0x80),
                Color.FromArgb(alpha: 0xff, red: 0x80, green: 0xff, blue: 0x00),
                Color.FromArgb(alpha: 0xff, red: 0x80, green: 0x00, blue: 0xff),
                Color.FromArgb(alpha: 0xff, red: 0x00, green: 0x80, blue: 0xff)
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
