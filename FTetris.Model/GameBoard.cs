using System;
using System.Collections.Generic;
using System.Linq;

namespace FTetris.Model
{
    class ScoreBoard
    {
        public event Action<int> ScoreUpdated;

        const int defaultScorePoint = 10;

        int scorePoint;
        int score     ;

        public int Score {
            get { return score; }
            private set {
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
            Score      = 0;
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

    public class GameBoard : MaskedStatefulCellBoard
    {
        public event Action            GameStarted     ;
        public event Action            GameOver        ;
        public event Action<Pentomino> NextPentominoSet;
        public event Action<int      > ScoreUpdated    ;

        ScoreBoard scoreBoard = new ScoreBoard();

        public int Score => scoreBoard.Score;

        Pentomino nextPentomino;

        public Pentomino NextPentomino {
            get { return nextPentomino; }
            private set {
                if (value != nextPentomino) {
                    nextPentomino = value;
                    NextPentominoSet?.Invoke(nextPentomino);
                }
            }
        }

        Pentomino currentPentomino = new Pentomino();

        public GameBoard()
        { scoreBoard.ScoreUpdated += score => ScoreUpdated?.Invoke(score); }

        public void Start()
        {
            Clear();
            NextPentomino = new Pentomino();
            Place(currentPentomino);
            scoreBoard.Reset();
            GameStarted?.Invoke();
        }

        public void Step()
        {
            if (!Down(currentPentomino)) {
                Try();
                if (!PlaceNextPentomino())
                    GameOver?.Invoke();
            }
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
                             .Where(y => {
                                        var row = Cells.GetRow(y);
                                        return row.Any(cell => cell.StateIndex != 0) && row.IsEven(cell => cell.StateIndex);
                                    });
        }

        static void RemoveRow(int[,] cellsClone, int y)
        {
            for (var yIndex = y; yIndex > 0; yIndex--)
                Enumerable.Range(0, cellsClone.GetLength(0)).ForEach(x => cellsClone[x, yIndex] = cellsClone[x, yIndex - 1]);
            Enumerable.Range(0, cellsClone.GetLength(0)).ForEach(x => cellsClone[x, 0] = 0);
        }

        public bool MoveLeft()
        { return MoveLeft(currentPentomino); }

        public bool MoveRight()
        { return MoveRight(currentPentomino); }

        public bool Turn(bool clockwise = true)
        { return Turn(currentPentomino, clockwise); }

        bool Place(Pentomino pentomino)
        {
            var position = new Point<int> { X = (Size.Width - pentomino.Width) / 2, Y = 0 };
            var cellsClone = CellsClone;
            if (pentomino.Place(cellsClone, position)) {
                CellsClone = cellsClone;
                return true;
            }
            return false;
        }

        bool Down(Pentomino pentomino)
        { return Move(new Point<int> { X = pentomino.Position.X, Y = pentomino.Position.Y + 1 }, pentomino); }

        bool MoveLeft(Pentomino pentomino)
        { return Move(new Point<int> { X = pentomino.Position.X - 1, Y = pentomino.Position.Y }, pentomino); }

        bool MoveRight(Pentomino pentomino)
        { return Move(new Point<int> { X = pentomino.Position.X + 1, Y = pentomino.Position.Y }, pentomino); }

        bool Turn(Pentomino currentPentomino, bool clockwise = true)
        {
            var cellsClone = CellsClone;
            if (currentPentomino.Turn(cellsClone, clockwise)) {
                CellsClone = cellsClone;
                return true;
            }
            return false;
        }

        bool Move(Point<int> position, Pentomino pentomino)
        {
            var cellsClone = CellsClone;
            if (pentomino.Move(cellsClone, position)) {
                CellsClone = cellsClone;
                return true;
            }
            return false;
        }

        bool PlaceNextPentomino()
        {
            currentPentomino = NextPentomino;
            if (Place(currentPentomino)) {
                NextPentomino = new Pentomino();
                return true;
            }
            return false;
        }
    }
}
