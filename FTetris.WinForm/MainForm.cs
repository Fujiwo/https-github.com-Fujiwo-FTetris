using FTetris.Model;
using System.Windows.Forms;

namespace FTetris.WinForm
{
    public partial class MainForm : Form
    {
        const int           interval   = 300;

        readonly GameBoard  gameBoard  = new GameBoard();
        readonly Timer      timer      = new Timer();

        public MainForm()
        {
            InitializeComponent();
            gameBoardView.DataContext = gameBoard;
            timer.Interval            = interval;
            SetHandlers();
        }

        void SetHandlers()
        {
            timer.Tick                 += (sender, e) => gameBoard.Step();

            gameBoard.GameStarted      += ()          => timer.Start();
            gameBoard.GameOver         += ()          => timer.Stop ();
            gameBoard.NextPolyominoSet += polyomino   => nextPolyominoStatusLabel.Text = polyomino.Index.ToString();
            gameBoard.ScoreUpdated     += score       => scoreStatusLabel        .Text = score          .ToString();
        }
    }
}
