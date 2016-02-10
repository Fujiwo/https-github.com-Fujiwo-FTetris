using FTetris.Model;
using Microsoft.Practices.Prism.Mvvm;
using System;
using System.Windows.Threading;

namespace FTetris.WPF.ViewModel
{
    class GameViewModel : BindableBase
    {
        const int interval = 300;

        readonly Game            game  = new Game();
        readonly DispatcherTimer timer = new DispatcherTimer { Interval = TimeSpan.FromMilliseconds(interval) };

        public GameBoardViewModel GameBoardViewModel { get; private set; }

        string nextPentomino = string.Empty;

        public string NextPentomino {
            get { return nextPentomino; }
            set { SetProperty(ref nextPentomino, value); }
        }

        string score = "0";

        public string Score {
            get { return score; }
            set { SetProperty(ref score, value); }
        }

        public GameViewModel()
        {
            GameBoardViewModel = new GameBoardViewModel(game.Board);
            SetHanders();
        }

        private void SetHanders()
        {
            timer.Tick            += (sender, e)   => game.Step();

            game.GameStarted      += ()            => timer.Start();
            game.GameOver         += ()            => timer.Stop ();
            game.NextPentominoSet += nextPentomino => NextPentomino = nextPentomino.ShapeKind.ToString();
            game.ScoreUpdated     += score         => Score         = score                  .ToString();
        }
    }
}
