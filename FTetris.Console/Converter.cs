using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FTetris.Console
{
    static class Converter
    {
        public static class Palette
        {
            static readonly ConsoleColor[] colors = new[] {
                ConsoleColor.Black      ,
                ConsoleColor.Red        ,
                ConsoleColor.Green      ,
                ConsoleColor.Yellow     ,
                ConsoleColor.Blue       ,
                ConsoleColor.Magenta    ,
                ConsoleColor.Cyan       ,
                ConsoleColor.DarkRed    ,
                ConsoleColor.DarkGreen  ,
                ConsoleColor.DarkYellow ,
                ConsoleColor.DarkBlue   ,
                ConsoleColor.DarkMagenta,
                ConsoleColor.DarkCyan
            };

            public static int ColorNumber { get { return colors.Length; } }

            public static ConsoleColor ToColor(int index)
            {
                Contract.Assert(index >= 0 && index < ColorNumber);
                return colors[index];
            }
        }

        public static ConsoleColor StateIndexToColor(int colorIndex)
        { return Palette.ToColor(colorIndex); }
    }
}
