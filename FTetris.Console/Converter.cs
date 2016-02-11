using FTetris.Model;
using System;
using System.Diagnostics.Contracts;

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
                ConsoleColor.White
            };

            public static int ColorNumber { get { return colors.Length; } }

            public static ConsoleColor ToColor(PolyominoIndex polyominoIndex)
            {
                Contract.Assert((int)polyominoIndex >= 0 && (int)polyominoIndex < ColorNumber);
                return colors[(int)polyominoIndex];
            }
        }

        public static ConsoleColor PolyominoIndexToColor(PolyominoIndex polyominoIndex)
        { return Palette.ToColor(polyominoIndex); }
    }
}
