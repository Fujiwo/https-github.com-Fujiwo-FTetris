using System.Diagnostics.Contracts;

namespace FTetris.Model
{
    public class PolyominoBoard : CellBoard
    {
        Tetromono polyomino = null;

        public Tetromono Polyomino {
            get { return polyomino; }
            set {
                if (value != polyomino) {
                    Clear();
                    Place(value);
                }
            }
        }

        public PolyominoBoard(Size<int> size) : base(size)
        {}

        bool Place(Tetromono polyomino)
        {
            Contract.Assert(polyomino.Size.Width  <= Size.Width  &&
                            polyomino.Size.Height <= Size.Height);

            var position = new Point<int>().Add(Size.Subtract(polyomino.Size).Divide(2));
            var cellsClone = CellsClone;
            if (polyomino.Place(cellsClone, position)) {
                CellsClone = cellsClone;
                return true;
            }
            return false;
        }
    }
}
