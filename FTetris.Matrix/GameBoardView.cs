using FTetris.Model;
using System.Linq;
using System;

namespace FTetris.Matrix
{
    class GameBoardView
    {
        public GameBoard DataContext { get; private set; }
        public CellView[,] Cells { get; set; }

        public GameBoardView(GameBoard gameBoard)
        {
            DataContext = gameBoard;

            Cells = new CellView[gameBoard.ActualCells.GetLength(0),
                                 gameBoard.ActualCells.GetLength(1)];
            gameBoard.ActualCells.ForEach((point, cell) => Cells.Set(point, new CellView { Point = point, DataContext = cell }));
        }

        public void Start()
        { DataContext.Start(); }

        public void Write()
        {
            Console.BackgroundColor = ConsoleColor.Black;
            Console.ForegroundColor = ConsoleColor.Green;
            Console.Clear();
            WriteTitle();
            WriteGameBoard();
        }

        void WriteTitle()
        { Console.WriteLine($"FTetris \tNext: {DataContext.NextPolyomino.Index} \tPoint: {DataContext.Score}"); }

        void WriteGameBoard()
        { Enumerable.Range(0, Cells.GetLength(1)).ForEach(WriteLine); }

        void WriteLine(int y)
        {
            Enumerable.Range(0, Cells.GetLength(0)).ForEach(x => Cells[x, y].Write());
            Console.WriteLine();
        }
    }
}
