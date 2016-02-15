using FTetris.Model;
using System.Timers;
using System;

namespace FTetris.Console
{
    class GameView
    {
        const double interval        = 300.0;

        readonly GameBoard gameBoard = new GameBoard();
        readonly Timer     timer     = new Timer(interval: interval);

        public GameBoardView GameBoardView { get; private set; }

        public GameView()
        {
            Usage();
            GameBoardView = new GameBoardView(gameBoard);
            timer    .Elapsed  += (sender, e) => Step();
            gameBoard.GameOver += (         ) => timer.Stop();
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
                    case ConsoleKey.LeftArrow : gameBoard.MoveLeft (     ); break;
                    case ConsoleKey.RightArrow: gameBoard.MoveRight(     ); break;
                    case ConsoleKey.UpArrow   : gameBoard.Turn     (     ); break;  
                    case ConsoleKey.DownArrow : gameBoard.Turn     (false); break;  
                    case ConsoleKey.Spacebar  : gameBoard.Drop     (     ); break;  
                    case ConsoleKey.Enter     : Start              (     ); break;  
                }
            }
        }

        void Start()
        {
            gameBoard.Start();
            timer.Start();
        }

        void Step()
        {
            gameBoard.Step();
            Write();
        }

        void Write()
        { GameBoardView.Write(); }
    }
}
