using FTetris.Model;
using System;

namespace FTetris.Console
{
    class CellView
    {
        public StatefulCell DataContext { get; set; } = null;

        public Point<int> Point { get; set; }

        public ConsoleColor Color {
            get { return Converter.StateIndexToColor(DataContext.StateIndex); }
        }

        public void Write()
        { ConsoleWriter.Write(Color, Color == ConsoleColor.Black ? '　' : '■'); }
    }
}
