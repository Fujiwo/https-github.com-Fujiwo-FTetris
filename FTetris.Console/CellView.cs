using FTetris.Model;
using System;

namespace FTetris.Console
{
    class CellView
    {
        public Cell DataContext { get; set; } = null;

        public Point<int> Point { get; set; }

        public ConsoleColor Color {
            get { return Converter.PolyominoIndexToColor(DataContext.Index); }
        }

        public void Write()
        { ConsoleWriter.Write(Color, Color == ConsoleColor.Black ? '　' : '■'); }
    }
}
