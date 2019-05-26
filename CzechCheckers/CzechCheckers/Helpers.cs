using System;
using System.Collections.Generic;

namespace CzechCheckers
{
    public sealed class Helpers
    {
        private Helpers() { }

        public static IEnumerable<Field> DiagonalPath(Field from, Field to)
        {
            if (!IsSameDiagonal(from, to))
            {
                yield break;
            }

            int directionRow = from.Row < to.Row ? 1 : -1;
            int directionCol = from.Column < to.Column ? 1 : -1;
            int maxDistance = HorizontalDistance(from, to);

            for (int distance = 1; distance <= maxDistance; ++distance)
            {
                yield return new Field
                {
                    Row = from.Row + directionRow * distance,
                    Column = from.Column + directionCol * distance
                };
            }
        }

        public static IEnumerable<Field> AllFields()
        {
            for (int row = 0; row <= Board.MaxRow; ++row)
            {
                for (int col = 0; col <= Board.MaxCol; ++col)
                {
                    yield return new Field { Row = row, Column = col };
                }
            }
        }

        public static bool IsSameDiagonal(Field from, Field to)
        {
            return HorizontalDistance(from, to) == VerticalDistance(from, to);
        }

        public static int HorizontalDistance(Field from, Field to)
        {
            return Math.Abs(from.Column - to.Column);
        }

        public static int VerticalDistance(Field from, Field to)
        {
            return Math.Abs(from.Row - to.Row);
        }
    }
}
