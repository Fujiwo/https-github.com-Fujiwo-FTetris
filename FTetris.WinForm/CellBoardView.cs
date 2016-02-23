using FTetris.Model;
using System;
using System.Drawing;
using System.Windows.Forms;

namespace FTetris.WinForm
{
    public partial class CellBoardView : UserControl
    {
        CellBoard dataContext = null;

        public CellBoard DataContext {
            get { return dataContext; }
            set {
                if (value != dataContext) {
                    dataContext = value;
                    Initialize(value);
                }
            }
        }

        internal CellView[,] Cells { get; set; }

        public CellBoardView()
        {
            this.Load        += (_, __) => OnLoad();
            this.SizeChanged += (_, __) => OnSizeChanged();
            this.Paint       += (_, e ) => OnPaint(e.Graphics);
        }

        void Initialize(CellBoard cellBoard)
        {
            Cells = TwoDimensionalArrayExtension.Create<CellView>(cellBoard.ActualCells.Size());
            cellBoard.ActualCells.ForEach((point, cell) => {
                var cellView = new CellView { Point = point, DataContext = cell };
                cellView.IndexChanged += OnCellViewIndexChanged;
                Cells.Set(point, cellView);
            });
        }

        public Rectangle GetCellPosition(Size wholeSize, Point<int> point)
        { return Cells.Get(point).GetPosition(wholeSize, DataContext.ActualSize); }

        void OnLoad()
        { SetSize(); }

        void OnSizeChanged()
        {
            Invalidate();
            SetSize();
        }

        void OnPaint(Graphics graphics)
        { Cells?.ToSequence()?.ForEach(cell => cell.Paint(graphics)); }

        void OnCellViewIndexChanged(CellView cellView)
        {
            using (var graphics = CreateGraphics()) {
                cellView.Paint(graphics);
            }
        }

        void SetSize()
        {
            if (Cells != null) {
                var clientSize = ClientSize;
                Cells.ForEach((point, cell) => cell.Position = GetCellPosition(clientSize, point));
            }
        }
    }
}
