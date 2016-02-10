namespace FTetris.Model
{
    public class StatefulCellBoard
    {
        const int defaultStateNumber =  8;
        const int defaultWidth       = 10;
        const int defaultHeight      = 32;

        public Size<int> Size { get; } = new Size<int> { Width = defaultWidth, Height = defaultHeight };

        public StatefulCell[,] Cells { get; private set; }

        public int[,] CellsClone
        {
            get {
                var cellsClone = new int[Size.Width, Size.Height];
                Cells.ForEach((point, cell) => cellsClone.Set(point, cell.StateIndex));
                return cellsClone;
            }
            set {
                Cells.ForEach((point, cell) => cell.StateIndex = value.Get(point));
            }
        }

        public StatefulCellBoard(int stateNumber = defaultStateNumber)
        {
            Cells = new StatefulCell[Size.Width, Size.Height];
            Cells.ForEach((point, cell) => Cells.Set(point, new StatefulCell()));
        }

        public void Clear()
        { Cells.ForEach((point, cell) => cell.StateIndex = 0); }
    }

    public class MaskedStatefulCellBoard : StatefulCellBoard
    {
        public int TopMask { get; set; } = 2;

        public Size<int> VisibleSize => new Size<int> { Width = base.Size.Width, Height = base.Size.Height - TopMask };

        public StatefulCell[,] VisibleCells {
            get {
                var visibleCells = new StatefulCell[VisibleSize.Width, VisibleSize.Height];
                for (var x = 0; x < VisibleSize.Width; x++) {
                    for (var y = 0; y < VisibleSize.Height; y++)
                        visibleCells[x, y] = base.Cells[x, y + TopMask];
                }
                return visibleCells;
            }
        }
    }
}
