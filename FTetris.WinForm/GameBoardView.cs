using FTetris.Model;
using System;
using System.Drawing;
using System.Windows.Forms;

namespace FTetris.WinForm
{
    public partial class GameBoardView : UserControl
    {
        GameBoard dataContext = null;
        public GameBoard DataContext {
            get { return dataContext; }
            set {
                if (value != dataContext) {
                    dataContext = value;
                    Initialize(value);
                }
            }
        }

        internal CellView[,] Cells { get; set; }

        public GameBoardView()
        {
            InitializeComponent();
        }

        void Initialize(GameBoard gameBoard)
        {
            Cells = new CellView[gameBoard.VisibleCells.GetLength(0),
                                 gameBoard.VisibleCells.GetLength(1)];
            gameBoard.VisibleCells.ForEach((point, cell) =>
            {
                var cellView = new CellView { Point = point, DataContext = cell };
                cellView.IndexChanged += OnCellViewIndexChanged;
                Cells.Set(point, cellView);
            });
        }

        public Rectangle GetCellPosition(Size wholeSize, Point<int> point)
        { return Cells.Get(point).GetPosition(wholeSize, DataContext.VisibleSize); }

        void OnLoad(object sender, EventArgs e)
        { SetSize(); }

        void OnSizeChanged(object sender, EventArgs e)
        {
            Invalidate();
            SetSize();
        }

        void OnPaint(object sender, PaintEventArgs e)
        { Cells?.ToSequence()?.ForEach(cell => cell.Paint(e.Graphics)); }

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

        void OnPreviousKeyDown(object sender, PreviewKeyDownEventArgs e)
        {
            switch (e.KeyCode) {
                case Keys.Left : DataContext.MoveLeft (     ); break;
                case Keys.Right: DataContext.MoveRight(     ); break;
                case Keys.Up   : DataContext.Turn     (     ); break;
                case Keys.Down : DataContext.Turn     (false); break;
                case Keys.Space: DataContext.Drop     (     ); break;
                case Keys.Enter: DataContext.Start    (     ); break;
            }
        }
    }
}
