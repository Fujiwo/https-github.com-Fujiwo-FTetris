using FTetris.Model;
using Microsoft.Practices.Prism.Mvvm;
using System.Collections.Generic;
using System.Windows;
using System;

namespace FTetris.WPF.ViewModel
{
    class CellBoardViewModel : BindableBase
    {
        protected CellBoard DataContext { get; set; }

        Size size;

        public Size Size {
            get { return size; }
            set {
                Cells.ForEach((point, cell) => cell.Position = GetCellPosition(value, point));
                SetProperty(ref size, value);
                OnPropertyChanged(() => Items);
            }
        }

        public CellViewModel[,] Cells { get; set; }
        public IEnumerable<CellViewModel> Items => Cells.ToSequence();

        public CellBoardViewModel(CellBoard cellBoard)
        {
            DataContext = cellBoard;
            Cells = new CellViewModel[cellBoard.ActualSize.Width,
                                      cellBoard.ActualSize.Height];
            cellBoard.ActualCells.ForEach((point, cell) => Cells.Set(point, new CellViewModel { Point = point, DataContext = cell }));
        }

        protected Rect GetCellPosition(Size wholeSize, Point<int> point)
        { return Cells.Get(point).GetPosition(wholeSize, DataContext.ActualSize); }
    }

    class GameBoardViewModel : CellBoardViewModel
    {
        public new GameBoard DataContext {
            get         { return (GameBoard)base.DataContext; }
            private set { base.DataContext = value;           }
        }

        public GameBoardViewModel(GameBoard gameBoard) : base(gameBoard)
        {
            //DataContext = gameBoard;
            //Cells       = new CellViewModel[gameBoard.ActualCells.GetLength(0),
            //                                gameBoard.ActualCells.GetLength(1)];
            //gameBoard.ActualCells.ForEach((point, cell) => Cells.Set(point, new CellViewModel { Point = point, DataContext = cell }));
        }

        public void Start()
        { DataContext.Start();  }

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
