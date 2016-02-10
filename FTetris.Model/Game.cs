using System;

namespace FTetris.Model
{
    public class Game
    {
        public event Action            GameStarted     ;
        public event Action            GameOver        ;
        public event Action<Tetromono> NextPentominoSet;
        public event Action<int      > ScoreUpdated    ;

        public GameBoard Board { get; } = new GameBoard();

        public Tetromono NextPentomino => Board.NextPentomino;
        public int Score => Board.Score;

        public Game()
        {
            Board.GameStarted      += ()        => GameStarted     ?.Invoke(         );
            Board.GameOver         += ()        => GameOver        ?.Invoke(         );
            Board.NextPentominoSet += polyomino => NextPentominoSet?.Invoke(polyomino);
            Board.ScoreUpdated     += score     => ScoreUpdated    ?.Invoke(score    );
        }

        public void Start()
        { Board.Start(); }

        public void Step()
        { Board.Step(); }

        public bool MoveLeft()
        { return Board.MoveLeft(); }

        public bool MoveRight()
        { return Board.MoveRight(); }

        public bool Turn(bool clockwise = true)
        { return Board.Turn(clockwise); }
    }
}
