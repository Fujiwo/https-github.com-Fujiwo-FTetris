using FTetris.WPF.ViewModel;
using System.Windows;
using System.Windows.Controls;

namespace FTetris.WPF
{
    public partial class PolyominoBoardView : UserControl
    {
        internal new PolyominoBoardViewModel DataContext {
            get { return panel.DataContext as PolyominoBoardViewModel; }
            set { panel.DataContext = value; }
        }

        public PolyominoBoardView()
        {
            InitializeComponent();
        }

        void OnSizeChanged(object sender, SizeChangedEventArgs e)
        {
            if (DataContext != null)
                DataContext.Size = new Size(width: ActualWidth, height: ActualHeight);
        }
    }
}
