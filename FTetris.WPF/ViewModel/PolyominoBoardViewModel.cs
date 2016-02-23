using FTetris.Model;

namespace FTetris.WPF.ViewModel
{
    class PolyominoBoardViewModel : CellBoardViewModel
    {
        public new CellBoard DataContext {
            get { return (CellBoard)base.DataContext; }
        }

        public PolyominoBoardViewModel(PolyominoBoard polyominoBoard) : base(polyominoBoard)
        { }
    }
}
