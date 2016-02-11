namespace FTetris.Model
{
    public class CellBoard
    {
        const int defaultWidth  = 10;
        const int defaultHeight = 32;

        public Size<int> Size { get; } = new Size<int> { Width = defaultWidth, Height = defaultHeight };

        public Cell[,] Cells { get; private set; }

        public PolyominoIndex[,] CellsClone
        {
            get {
                var cellsClone = new PolyominoIndex[Size.Width, Size.Height];
                Cells.ForEach((point, cell) => cellsClone.Set(point, cell.Index));
                return cellsClone;
            }
            set {
                Cells.ForEach((point, cell) => cell.Index = value.Get(point));
            }
        }

        public CellBoard()
        {
            Cells = new Cell[Size.Width, Size.Height];
            Cells.ForEach((point, cell) => Cells.Set(point, new Cell()));
        }

        public void Clear()
        { Cells.ForEach((point, cell) => cell.Index = 0); }
    }

    public class MaskedCellBoard : CellBoard
    {
        public int TopMask { get; set; } = 2;

        public Size<int> VisibleSize => new Size<int> { Width = base.Size.Width, Height = base.Size.Height - TopMask };

        public Cell[,] VisibleCells {
            get {
                var visibleCells = new Cell[VisibleSize.Width, VisibleSize.Height];
                for (var x = 0; x < VisibleSize.Width; x++) {
                    for (var y = 0; y < VisibleSize.Height; y++)
                        visibleCells[x, y] = base.Cells[x, y + TopMask];
                }
                return visibleCells;
            }
        }
    }
}
