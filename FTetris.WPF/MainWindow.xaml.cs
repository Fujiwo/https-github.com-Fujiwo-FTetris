using System.Windows;
using System.Windows.Input;

namespace FTetris.WPF
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        { InitializeComponent(); }

        void OnPreviewKeyDown(object sender, KeyEventArgs e)
        { gameBoardView.OnPreviewKeyDown(sender, e); }
    }
}
