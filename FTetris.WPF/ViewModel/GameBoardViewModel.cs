using FTetris.Model;

namespace FTetris.WPF.ViewModel
{
    class GameBoardViewModel : CellBoardViewModel
    {
        public new GameBoard DataContext {
            get { return (GameBoard)base.DataContext; }
        }

        public GameBoardViewModel(GameBoard gameBoard) : base(gameBoard)
        {
            //DataContext = gameBoard;
            //Cells       = new CellViewModel[gameBoard.ActualCells.GetLength(0),
            //                                gameBoard.ActualCells.GetLength(1)];
            //gameBoard.ActualCells.ForEach((point, cell) => Cells.Set(point, new CellViewModel { Point = point, DataContext = cell }));
        }

        public void Start()
        { DataContext.Start(); }

        public void MoveRight()
        { DataContext.MoveRight(); }

        public void MoveLeft()
        { DataContext.MoveLeft(); }

        public void Turn(bool clockwise = true)
        { DataContext.Turn(clockwise); }

        public void Drop()
        { DataContext.Drop(); }
    }
}
