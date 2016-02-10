using FTetris.Model;
using System.Linq;
using System;

namespace FTetris.Console
{
    class GameBoardView
    {
        public GameBoard DataContext { get; private set; }
        public CellView[,] Cells { get; set; }

        public GameBoardView(GameBoard gameBoard)
        {
            DataContext = gameBoard;

            Cells = new CellView[gameBoard.VisibleCells.GetLength(0),
                                 gameBoard.VisibleCells.GetLength(1)];
            gameBoard.VisibleCells.ForEach((point, cell) => Cells.Set(point, new CellView { Point = point, DataContext = cell }));
        }

        public void Start()
        { DataContext.Start(); }

        public void Write()
        {
            System.Console.Clear();
            WriteTitle();
            WriteGameBoard();
        }

        void WriteTitle()
        { ConsoleWriter.WriteLine($"FTetris \tNext: {DataContext.NextPentomino.ShapeKind} \tPoint: {DataContext.Score}"); }

        void WriteGameBoard()
        { Enumerable.Range(0, Cells.GetLength(1)).ForEach(WriteLine); }

        void WriteLine(int y)
        {
            Enumerable.Range(0, Cells.GetLength(0)).ForEach(x => Cells[x, y].Write());
            ConsoleWriter.WriteLine();
        }
    }
}
