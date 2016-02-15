using FTetris.Model;
using Microsoft.Practices.Prism.Mvvm;
using System;
using System.Windows.Threading;

namespace FTetris.WPF.ViewModel
{
    class GameViewModel : BindableBase
    {
        const int interval                  = 300;

        readonly GameBoard       gameBoard  = new GameBoard();
        readonly DispatcherTimer timer      = new DispatcherTimer { Interval = TimeSpan.FromMilliseconds(interval) };

        public GameBoardViewModel GameBoardViewModel { get; private set; }

        string nextPolyomino = string.Empty;

        public string NextPolyomino {
            get { return nextPolyomino; }
            set { SetProperty(ref nextPolyomino, value); }
        }

        string score = "0";

        public string Score {
            get { return score; }
            set { SetProperty(ref score, value); }
        }

        public GameViewModel()
        {
            GameBoardViewModel = new GameBoardViewModel(gameBoard);
            SetHanders();
        }

        private void SetHanders()
        {
            timer.Tick                 += (sender, e)   => gameBoard.Step();

            gameBoard.GameStarted      += ()            => timer.Start();
            gameBoard.GameOver         += ()            => timer.Stop ();
            gameBoard.NextPolyominoSet += nextPolyomino => NextPolyomino = nextPolyomino.Index.ToString();
            gameBoard.ScoreUpdated     += score         => Score         = score              .ToString();
        }
    }
}
