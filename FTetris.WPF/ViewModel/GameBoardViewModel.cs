using FTetris.Model;

namespace FTetris.WPF.ViewModel
{
    class GameBoardViewModel : CellBoardViewModel
    {
        public new GameBoard DataContext {
            get { return (GameBoard)base.DataContext; }
        }

        public GameBoardViewModel(GameBoard gameBoard) : base(gameBoard)
        {}

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
