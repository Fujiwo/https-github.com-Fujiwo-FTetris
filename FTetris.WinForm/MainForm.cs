using FTetris.Model;
using System.Windows.Forms;

namespace FTetris.WinForm
{
    public partial class MainForm : Form
    {
        const int      interval = 300;

        readonly Game  game     = new Game ();
        readonly Timer timer    = new Timer();

        public MainForm()
        {
            InitializeComponent();
            gameBoardView.Focus();

            gameBoardView     .DataContext = game.GameBoard;
            polyominoBoardView.DataContext = game.PolyominoBoard;
            timer.Interval                 = interval;
            SetHandlers();
        }

        void SetHandlers()
        {
            timer.Tick                      += (sender, e) => game.GameBoard.Step();

            game.GameBoard.GameStarted      += ()          => timer.Start();
            game.GameBoard.GameOver         += ()          => timer.Stop ();
            game.GameBoard.NextPolyominoSet += polyomino   => game.PolyominoBoard.Place(polyomino);
            game.GameBoard.ScoreUpdated     += score       => scoreText.Text = score.ToString();
        }
    }
}
