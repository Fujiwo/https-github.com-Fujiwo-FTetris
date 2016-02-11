using System;

namespace FTetris.Model
{
    public class Cell
    {
        public event Action<Cell, PolyominoIndex> IndexChanged;

        PolyominoIndex index = 0;

        public PolyominoIndex Index {
            get { return index; }
            set {
                if (value != index) {
                    index = value;
                    IndexChanged?.Invoke(this, index);
                }
            }
        }
    }
}
