﻿using System;
using System.Collections.Generic;
using System.Linq;

namespace FTetris.Model
{
    public class GameBoard : MaskedStatefulCellBoard
    {
        class ScoreBoard
        {
            public event Action<int> ScoreUpdated;

            const int defaultScorePoint = 10;

            int scorePoint;
            int score;

            public int Score
            {
                get { return score; }
                private set
                {
                    if (value != score) {
                        score = value;
                        ScoreUpdated?.Invoke(value);
                    }
                }
            }

            public ScoreBoard()
            { Reset(); }

            public void Reset()
            {
                Score = 0;
                scorePoint = defaultScorePoint;
            }

            public void StartAdding()
            { scorePoint = defaultScorePoint; }

            public void Add()
            {
                Score += scorePoint;
                scorePoint *= 2;
            }
        }

        public event Action            GameStarted     ;
        public event Action            GameOver        ;
        public event Action<Tetromono> NextPentominoSet;
        public event Action<int      > ScoreUpdated    ;

        ScoreBoard scoreBoard = new ScoreBoard();

        public int Score => scoreBoard.Score;

        Tetromono nextPentomino;

        public Tetromono NextPentomino {
            get { return nextPentomino; }
            private set {
                if (value != nextPentomino) {
                    nextPentomino = value;
                    NextPentominoSet?.Invoke(nextPentomino);
                }
            }
        }

        Tetromono currentPentomino = new Tetromono();

        bool IsStarted { get; set; } = false;

        public GameBoard()
        { scoreBoard.ScoreUpdated += score => ScoreUpdated?.Invoke(score); }

        public void Start()
        {
            Clear();
            NextPentomino = new Tetromono();
            Place(currentPentomino);
            scoreBoard.Reset();
            IsStarted = true;
            GameStarted?.Invoke();
        }

        public void Step()
        {
            if (IsStarted)
                Down();
        }

        public bool MoveLeft()
        { return IsStarted && MoveLeft(currentPentomino); }

        public bool MoveRight()
        { return IsStarted && MoveRight(currentPentomino); }

        public bool Turn(bool clockwise = true)
        { return IsStarted && Turn(currentPentomino, clockwise); }

        void End()
        {
            IsStarted = false;
            GameOver?.Invoke();
        }

        public void Drop()
        {
            while (IsStarted && Down())
                ;
        }

        bool Down()
        {
            if (Down(currentPentomino))
                return true;
            Try();
            if (!PlaceNextPentomino())
                End();
            return false;
        }

        void Try()
        {
            var fullRows = GetFullRows().ToList();
            if (fullRows.Count > 0) {
                scoreBoard.StartAdding();
                var cellsClone = CellsClone;
                fullRows.ForEach(y => {
                    RemoveRow(cellsClone, y);
                    scoreBoard.Add();
                });
                CellsClone = cellsClone;
            }
        }

        IEnumerable<int> GetFullRows()
        {
            return Enumerable.Range(0, Cells.GetLength(1))
                             .Where(y => Cells.GetRow(y).All(cell => cell.StateIndex != 0));
        }

        static void RemoveRow(int[,] cellsClone, int y)
        {
            for (var yIndex = y; yIndex > 0; yIndex--)
                Enumerable.Range(0, cellsClone.GetLength(0)).ForEach(x => cellsClone[x, yIndex] = cellsClone[x, yIndex - 1]);
            Enumerable.Range(0, cellsClone.GetLength(0)).ForEach(x => cellsClone[x, 0] = 0);
        }

        bool Place(Tetromono polyomino)
        {
            var position = new Point<int> { X = (Size.Width - polyomino.Width) / 2, Y = 0 };
            var cellsClone = CellsClone;
            if (polyomino.Place(cellsClone, position)) {
                CellsClone = cellsClone;
                return true;
            }
            return false;
        }

        bool Down(Tetromono polyomino)
        { return Move(new Point<int> { X = polyomino.Position.X, Y = polyomino.Position.Y + 1 }, polyomino); }

        bool MoveLeft(Tetromono polyomino)
        { return Move(new Point<int> { X = polyomino.Position.X - 1, Y = polyomino.Position.Y }, polyomino); }

        bool MoveRight(Tetromono polyomino)
        { return Move(new Point<int> { X = polyomino.Position.X + 1, Y = polyomino.Position.Y }, polyomino); }

        bool Turn(Tetromono currentPentomino, bool clockwise = true)
        {
            var cellsClone = CellsClone;
            if (currentPentomino.Turn(cellsClone, clockwise)) {
                CellsClone = cellsClone;
                return true;
            }
            return false;
        }

        bool Move(Point<int> position, Tetromono polyomino)
        {
            var cellsClone = CellsClone;
            if (polyomino.Move(cellsClone, position)) {
                CellsClone = cellsClone;
                return true;
            }
            return false;
        }

        bool PlaceNextPentomino()
        {
            currentPentomino = NextPentomino;
            if (Place(currentPentomino)) {
                NextPentomino = new Tetromono();
                return true;
            }
            return false;
        }
    }
}
