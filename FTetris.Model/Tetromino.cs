using System;
using System.Collections.Generic;
using System.Linq;

namespace FTetris.Model
{
    public enum PolyominoIndex
    {
        // http://sansu-seijin.jp/blog/archives/947
        None, I, N, Z, O, J, T, L
    }

    public class Tetromono //: Polyomino
    {
        static Random random = new Random();

        public PolyominoIndex Index { get; set; } = PolyominoIndex.I;

        bool[,] shape;

        readonly Dictionary<PolyominoIndex, bool[,]> table = new Dictionary<PolyominoIndex, bool[,]> {
            [PolyominoIndex.I] = new[,] {{ false, false, false, false },
                                         { true , true , true , true  },
                                         { false, false, false, false },
                                         { false, false, false, false }},
            [PolyominoIndex.N] = new[,] {{ false, false, true , false },
                                         { false, true , true , false },
                                         { false, true , false, false },
                                         { false, false, false, false }},
            [PolyominoIndex.Z] = new[,] {{ false, true , false, false },
                                         { false, true , true , false },
                                         { false, false, true , false },
                                         { false, false, false, false }},
            [PolyominoIndex.O] = new[,] {{ false, false, false, false },
                                         { false, true , true , false },
                                         { false, true , true , false },
                                         { false, false, false, false }},
            [PolyominoIndex.J] = new[,] {{ false, false, false, false },
                                         { false, false, true , false },
                                         { true , true , true , false },
                                         { false, false, false, false }},
            [PolyominoIndex.T] = new[,] {{ false, false, false, false },
                                         { false, false, true , false },
                                         { false, true , true , false },
                                         { false, false, true , false }},
            [PolyominoIndex.L] = new[,] {{ false, false, false, false },
                                         { true , true , true , false },
                                         { false, false, true , false },
                                         { false, false, false, false }}
        };

        public Point<int> Position { get; set; }

        public Size<int> Size
        {
            get { return shape.Size(); }
        }

        public Tetromono()
        {
            Index = GetRandomPolyominoIndex();
            shape     = (bool[,])table[Index].Clone();
        }

        public Point<int> GetPosition(Point<int> point)
        { return GetPosition(Position, point); }

        public static Point<int> GetPosition(Point<int> position, Point<int> point)
        { return position.Add(point); }

        public IEnumerable<Point<int>> AllPoints => shape.AllPoints();

        public bool this[Point<int> point]
        {
            get { return shape.Get(point); }
        }

        public bool Move(PolyominoIndex[,] cellsClone, Point<int> position)
        {
            Erase(cellsClone);
            return Place(cellsClone, position);
        }

        public void Erase(PolyominoIndex[,] cellsClone)
        {
            AllPoints.Where(point => this[point])
                     .ForEach(point => cellsClone.Set(GetPosition(point), PolyominoIndex.None));
        }

        public bool Place(PolyominoIndex[,] cellsClone, Point<int> position)
        {
            var placeablePoints = PlaceablePoints(shape, cellsClone, position);
            if (placeablePoints == null)
                return false;
            placeablePoints.ForEach(point => cellsClone.Set(Tetromono.GetPosition(position, point), Index));
            Position = position;
            return true;
        }

        public override string ToString()
        { return Index.ToString(); }

        bool[,] Turn(bool clockwise = true)
        { return shape.Turn(clockwise); }

        public bool Turn(PolyominoIndex[,] cellsClone, bool clockwise = true)
        {
            Erase(cellsClone);

            var newShape = Turn(clockwise);
            var placeablePoints = PlaceablePoints(newShape, cellsClone, Position);
            if (placeablePoints == null)
                return false;
            placeablePoints.ForEach(point => cellsClone.Set(Tetromono.GetPosition(Position, point), Index));
            shape = newShape;
            return true;
        }

        static IEnumerable<Point<int>> PlaceablePoints(bool[,] shape, PolyominoIndex[,] cellsClone, Point<int> position)
        {
            var exsitingPoints = shape.AllPoints().Where(point => shape.Get(point)).ToList();
            PolyominoIndex shapeIndex;
            var placeablePoints = exsitingPoints.Where(point => cellsClone.TryGet(GetPosition(position, point), out shapeIndex) ? shapeIndex == PolyominoIndex.None : false).ToList();
            return exsitingPoints.Count == placeablePoints.Count ? placeablePoints : null;
        }

        PolyominoIndex GetRandomPolyominoIndex()
        { return (PolyominoIndex)(random.Next(table.Count) + 1); }
    }
}
