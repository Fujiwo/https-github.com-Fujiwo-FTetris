namespace FTetris.Model
{
    public class CellBoard
    {
        public Size<int> Size { get; private set; }

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

        public CellBoard(Size<int> size)
        {
            Size = size;
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
                visibleCells.AllPoints()
                            .ForEach(point => visibleCells.Set(point, Cells.Get(point.Add(new Size<int> { Width = 0, Height = TopMask }))));
                return visibleCells;
            }
        }

        public MaskedCellBoard(Size<int> size) : base(size)
        {}
    }
}
