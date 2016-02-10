using FTetris.Model;
using System.Timers;
using System;

namespace FTetris.Console
{
    class GameView
    {
        const double interval = 300.0;

        readonly Game  game  = new Game();
        readonly Timer timer = new Timer(interval: interval);

        public GameBoardView GameBoardView { get; private set; }

        public GameView()
        {
            Usage();
            GameBoardView = new GameBoardView(game.Board);
            timer.Elapsed  += (sender, e) => Step();
            game .GameOver += (         ) => timer.Stop();
            Loop();
        }

        static void Usage()
        {
            const string usage = "FTetris (Space: Start ←: Left →: Right ↑: Turn Right ↓: Turn Left)";
            ConsoleWriter.WriteLine(usage);
        }

        void Loop()
        {
            for (; ;) {
                var key = System.Console.ReadKey();
                switch (key.Key) {
                    case ConsoleKey.LeftArrow : game.MoveLeft (     ); break;
                    case ConsoleKey.RightArrow: game.MoveRight(     ); break;
                    case ConsoleKey.UpArrow   : game.Turn     (     ); break;  
                    case ConsoleKey.DownArrow : game.Turn     (false); break;  
                    case ConsoleKey.Spacebar  : Start         (     ); break;  
                }
            }
        }

        void Start()
        {
            game.Start();
            timer.Start();
        }

        void Step()
        {
            game.Step();
            Write();
        }

        void Write()
        { GameBoardView.Write(); }
    }
}
