using FTetris.Model;
using Microsoft.Practices.Prism.Mvvm;
using System.Collections.Generic;
using System.Windows;
using System;

namespace FTetris.WPF.ViewModel
{
    class GameBoardViewModel : BindableBase
    {
        public GameBoard DataContext { get; private set; }

        Size size;

        public Size Size {
            get { return size; }
            set {
                Cells.ForEach((point, cell) => cell.Position = GetCellPosition(value, point));
                SetProperty(ref size, value);
                OnPropertyChanged(() => Items);
            }
        }

        public CellViewModel[, ] Cells { get; set; }
        public IEnumerable<CellViewModel> Items => Cells.ToSequence();

        public GameBoardViewModel(GameBoard gameBoard)
        {
            DataContext = gameBoard;
            Cells       = new CellViewModel[gameBoard.VisibleCells.GetLength(0),
                                            gameBoard.VisibleCells.GetLength(1)];
            gameBoard.VisibleCells.ForEach((point, cell) => Cells.Set(point, new CellViewModel { Point = point, DataContext = cell }));
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

        public Rect GetCellPosition(Size wholeSize, Point<int> point)
        { return Cells.Get(point).GetPosition(wholeSize, DataContext.VisibleSize); }
    }
}
