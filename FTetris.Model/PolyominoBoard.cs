using System.Diagnostics.Contracts;

namespace FTetris.Model
{
    public class PolyominoBoard : CellBoard
    {
        public PolyominoBoard() : base(new Tetromono().Size)
        {}

        public bool Place(Tetromono polyomino)
        {
            Contract.Assert(polyomino.Size.Width  <= Size.Width  &&
                            polyomino.Size.Height <= Size.Height);

            Clear();
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
