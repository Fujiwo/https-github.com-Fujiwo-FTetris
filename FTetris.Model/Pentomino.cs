using System;
using System.Collections.Generic;
using System.Linq;

namespace FTetris.Model
{
    public class Pentomino
    {
        static Random random = new Random();

        public enum ShapeType
        {
            // https://en.wikipedia.org/wiki/Pentomino
            F = 1, I, L, N, P, T, U, V, W, X, Y, Z
        }

        public ShapeType ShapeKind { get; set; } = ShapeType.F;

        bool[,] shape;

        readonly Dictionary<ShapeType, bool[,]> table = new Dictionary<ShapeType, bool[,]> {
            [ShapeType.F] = new[,] {{ false, false, false, false, false },
                                    { false, false, true , false, false },
                                    { false, true , true , true , false },
                                    { false, true , false, false, false },
                                    { false, false, false, false, false }},
            [ShapeType.I] = new[,] {{ false, false, false, false, false },
                                    { false, false, false, false, false },
                                    { true , true , true , true , true  },
                                    { false, false, false, false, false },
                                    { false, false, false, false, false }},
            [ShapeType.L] = new[,] {{ false, false, false, false, false },
                                    { false, false, false, false, false },
                                    { true , true , true , true , false },
                                    { false, false, false, true , false },
                                    { false, false, false, false, false }},
            [ShapeType.N] = new[,] {{ false, false, false, false, false },
                                    { false, false, true , true , false },
                                    { true , true , true , false, false },
                                    { false, false, false, false, false },
                                    { false, false, false, false, false }},
            [ShapeType.P] = new[,] {{ false, false, false, false, false },
                                    { false, false, false, false, false },
                                    { false, true , true , true , false },
                                    { false, true , true , false, false },
                                    { false, false, false, false, false }},
            [ShapeType.T] = new[,] {{ false, false, false, false, false },
                                    { false, true , false, false, false },
                                    { false, true , true , true , false },
                                    { false, true , false, false, false },
                                    { false, false, false, false, false }},
            [ShapeType.U] = new[,] {{ false, false, false, false, false },
                                    { false, true , true , false, false },
                                    { false, false, true , false, false },
                                    { false, true , true , false, false },
                                    { false, false, false, false, false }},
            [ShapeType.V] = new[,] {{ false, false, false, false, false },
                                    { false, true , true , true , false },
                                    { false, false, false, true , false },
                                    { false, false, false, true , false },
                                    { false, false, false, false, false }},
            [ShapeType.W] = new[,] {{ false, false, false, false, false },
                                    { false, true , true , false, false },
                                    { false, false, true , true , false },
                                    { false, false, false, true , false },
                                    { false, false, false, false, false }},
            [ShapeType.X] = new[,] {{ false, false, false, false, false },
                                    { false, false, true , false, false },
                                    { false, true , true , true , false },
                                    { false, false, true , false, false },
                                    { false, false, false, false, false }},
            [ShapeType.Y] = new[,] {{ false, false, false, false, false },
                                    { false, false, true , false, false },
                                    { false, true , true , true , true  },
                                    { false, false, false, false, false },
                                    { false, false, false, false, false }},
            [ShapeType.Z] = new[,] {{ false, false, false, false, false },
                                    { false, true , false, false, false },
                                    { false, true , true , true , false },
                                    { false, false, false, true , false },
                                    { false, false, false, false, false }}
        };

        public int Width
        {
            get { return shape.GetLength(0); }
        }

        public int Height
        {
            get { return shape.GetLength(1); }
        }

        public Point<int> Position { get; set; }

        public Pentomino()
        {
            ShapeKind = GetRandomShapeType();
            shape = (bool[,])table[ShapeKind].Clone();
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

        public bool Move(int[,] cellsClone, Point<int> position)
        {
            Erase(cellsClone);
            return Place(cellsClone, position);
        }

        public void Erase(int[,] cellsClone)
        {
            AllPoints.Where(point => this[point])
                     .ForEach(point => cellsClone.Set(GetPosition(point), 0));
        }

        public bool Place(int[,] cellsClone, Point<int> position)
        {
            var placeablePoints = PlaceablePoints(shape, cellsClone, position);
            if (placeablePoints == null)
                return false;
            placeablePoints.ForEach(point => cellsClone.Set(Pentomino.GetPosition(position, point), (int)ShapeKind));
            Position = position;
            return true;
        }

        bool[,] Turn(bool clockwise = true)
        { return shape.Turn(clockwise); }

        public bool Turn(int[,] cellsClone, bool clockwise = true)
        {
            Erase(cellsClone);

            var newShape        = Turn(clockwise);
            var placeablePoints = PlaceablePoints(newShape, cellsClone, Position);
            if (placeablePoints == null)
                return false;
            placeablePoints.ForEach(point => cellsClone.Set(Pentomino.GetPosition(Position, point), (int)ShapeKind));
            shape = newShape;
            return true;
        }

        static IEnumerable<Point<int>> PlaceablePoints(bool[, ] shape, int[,] cellsClone, Point<int> position)
        {
            var exsitingPoints  = shape.AllPoints().Where(point => shape.Get(point)).ToList();
            int status;
            var placeablePoints = exsitingPoints.Where(point => cellsClone.TryGet(GetPosition(position, point), out status) ? status == 0 : false).ToList();
            return exsitingPoints.Count == placeablePoints.Count ? placeablePoints : null;
        }

        ShapeType GetRandomShapeType()
        { return (ShapeType)(random.Next(table.Count) + 1); }
    }
}
