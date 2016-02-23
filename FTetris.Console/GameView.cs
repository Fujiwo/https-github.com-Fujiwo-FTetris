using FTetris.Model;
using System.Timers;
using System;

namespace FTetris.Console
{
    class GameView
    {
        const double   interval = 300.0;

        readonly Game  game     = new Game();
        readonly Timer timer    = new Timer(interval: interval);

        public GameBoardView GameBoardView { get; private set; }

        public GameView()
        {
            Usage();
            GameBoardView = new GameBoardView(game.GameBoard);
            timer.Elapsed  += (sender, e) => Step();
            game.GameBoard.GameOver += (         ) => timer.Stop();
            Loop();
        }

        static void Usage()
        {
            const string usage = "FTetris (Enter: Start ←: Left →: Right ↑: Turn Right ↓: Turn Left Space: Drop)";
            ConsoleWriter.WriteLine(usage);
        }

        void Loop()
        {
            for (; ;) {
                var key = System.Console.ReadKey();
                switch (key.Key) {
                    case ConsoleKey.LeftArrow : game.GameBoard.MoveLeft (     ); break;
                    case ConsoleKey.RightArrow: game.GameBoard.MoveRight(     ); break;
                    case ConsoleKey.UpArrow   : game.GameBoard.Turn     (     ); break;  
                    case ConsoleKey.DownArrow : game.GameBoard.Turn     (false); break;  
                    case ConsoleKey.Spacebar  : game.GameBoard.Drop     (     ); break;  
                    case ConsoleKey.Enter     : Start                   (     ); break;  
                }
            }
        }

        void Start()
        {
            game.GameBoard.Start();
            timer.Start();
        }

        void Step()
        {
            game.GameBoard.Step();
            Write();
        }

        void Write()
        { GameBoardView.Write(); }
    }
}
