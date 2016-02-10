using FTetris.Model;
using System.Windows.Forms;

namespace FTetris.WinForm
{
    public partial class MainForm : Form
    {
        const int interval = 300;

        readonly Game  game  = new Game ();
        readonly Timer timer = new Timer();

        public MainForm()
        {
            InitializeComponent();
            gameBoardView.DataContext = game.Board;
            SetHandlers();
        }

        void SetHandlers()
        {
            timer.Tick             += (sender, e) => game.Step();

            game .GameStarted      += ()          => timer.Start();
            game .GameOver         += ()          => timer.Stop ();
            game .NextPentominoSet += pentomino   => nextPentominoStatusLabel.Text = pentomino.ShapeKind.ToString();
            game .ScoreUpdated     += score       => scoreStatusLabel        .Text = score              .ToString();
        }
    }
}
