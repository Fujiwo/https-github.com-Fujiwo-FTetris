using FTetris.Model;
using Microsoft.Practices.Prism.Mvvm;
using System;
using System.Windows.Threading;

namespace FTetris.WPF.ViewModel
{
    class GameViewModel : BindableBase
    {
        const int interval                      = 300;

        readonly Game            game  = new Game();
        //readonly GameBoard       gameBoard      = new GameBoard();
        //readonly PolyominoBoard  polyominoBoard = new PolyominoBoard();
        readonly DispatcherTimer timer = new DispatcherTimer { Interval = TimeSpan.FromMilliseconds(interval) };

        GameBoard      GameBoard      => game.GameBoard     ;
        PolyominoBoard PolyominoBoard => game.PolyominoBoard;

        public GameBoardViewModel      GameBoardViewModel      { get; private set; }
        public PolyominoBoardViewModel PolyominoBoardViewModel { get; private set; }

        Tetromono nextPolyomino = null;

        public Tetromono NextPolyomino {
            get { return nextPolyomino; }
            set {
                SetProperty(ref nextPolyomino, value);
                //OnPropertyChanged(() => NextPolyominoText);
            }
        }

        //public string NextPolyominoText {
        //    get { return NextPolyomino == null ? string.Empty : NextPolyomino.Index.ToString(); }
        //}

        string score = "0";

        public string Score {
            get { return score; }
            set { SetProperty(ref score, value); }
        }

        public GameViewModel()
        {
            GameBoardViewModel      = new GameBoardViewModel     (GameBoard     );
            PolyominoBoardViewModel = new PolyominoBoardViewModel(PolyominoBoard);
            SetHanders();
        }

        private void SetHanders()
        {
            timer.Tick                 += (sender, e)   => GameBoard.Step();

            GameBoard.GameStarted      += ()            => timer.Start();
            GameBoard.GameOver         += ()            => timer.Stop ();
            GameBoard.NextPolyominoSet += nextPolyomino => NextPolyomino = nextPolyomino;
            GameBoard.ScoreUpdated     += score         => Score         = score.ToString();
        }
    }
}
