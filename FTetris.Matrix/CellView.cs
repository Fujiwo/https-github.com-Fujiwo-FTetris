using FTetris.Model;
using System;

namespace FTetris.Matrix
{
    class CellView
    {
        public Cell DataContext { get; set; } = null;

        public Point<int> Point { get; set; }

        public void Write()
        { Console.Write((int)DataContext.Index); }
    }
}
