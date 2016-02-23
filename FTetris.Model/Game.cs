namespace FTetris.Model
{
    public class Game
    {
        public GameBoard      GameBoard      { get; } = new GameBoard();
        public PolyominoBoard PolyominoBoard { get; } = new PolyominoBoard();

        public Game()
        { GameBoard.NextPolyominoSet += polyomino => PolyominoBoard.Place(polyomino);  }
    }
}
