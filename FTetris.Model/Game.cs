using System;

namespace FTetris.Model
{
    public class Game
    {
        public event Action            GameStarted     ;
        public event Action            GameOver        ;
        public event Action<Tetromono> NextPolyominoSet;
        public event Action<int      > ScoreUpdated    ;

        public GameBoard Board { get; } = new GameBoard();

        public Tetromono NextPolyomino => Board.NextPolyomino;
        public int Score => Board.Score;

        public Game()
        {
            Board.GameStarted      += ()        => GameStarted     ?.Invoke(         );
            Board.GameOver         += ()        => GameOver        ?.Invoke(         );
            Board.NextPolyominoSet += polyomino => NextPolyominoSet?.Invoke(polyomino);
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

        public void Drop()
        { Board.Drop(); }
    }
}
