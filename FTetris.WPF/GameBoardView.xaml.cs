using FTetris.WPF.ViewModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace FTetris.WPF
{
    public partial class GameBoardView : UserControl
    {
        internal new GameBoardViewModel DataContext
        {
            get { return panel.DataContext as GameBoardViewModel; }
            set { panel.DataContext = value; }
        }

        public GameBoardView()
        { InitializeComponent(); }

        public void OnPreviewKeyDown(object sender, KeyEventArgs e)
        {
            switch (e.Key) {
                case Key.Left : DataContext.MoveLeft (     ); break;
                case Key.Right: DataContext.MoveRight(     ); break;
                case Key.Up   : DataContext.Turn     (     ); break;
                case Key.Down : DataContext.Turn     (false); break;
                case Key.Space: DataContext.Start    (     ); break;
            }
        }

        void OnSizeChanged(object sender, SizeChangedEventArgs e)
        {
            if (DataContext != null)
                DataContext.Size = new Size(width: ActualWidth, height: ActualHeight);
        }
    }
}
