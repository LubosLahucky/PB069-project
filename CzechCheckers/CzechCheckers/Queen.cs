using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CzechCheckers
{
    class Queen : FigureBase
    {
        public Queen(FigureColor color) : base(color)
        {
        }

        public override bool CanMove(Move move)
        {
            return Helpers.IsSameDiagonal(move.From, move.To)
                && Helpers.HorizontalDistance(move.From, move.To) >= 1;
        }

        public override bool CanJump(Move move)
        {
            return Helpers.IsSameDiagonal(move.From, move.To)
                && Helpers.HorizontalDistance(move.From, move.To) >= 2;
        }

        public override string ToString()
        {
            return base.ToString() + "Q";
        }

        public override IEnumerable<Field> PossibleMoves(Field from)
        {
            foreach (var field in DiagonalFields(from, 1))
                yield return field;
        }

        public override IEnumerable<Field> PossibleJumps(Field from)
        {
            foreach (var field in DiagonalFields(from, 2))
                yield return field;
        }

        private IEnumerable<Field> DiagonalFields(Field from, int initDistance)
        {
            var maxDistance = Math.Max(Board.MaxRow, Board.MaxCol);
            for (int distance = initDistance; distance <= maxDistance; ++distance)
            {
                int[,] directions = new int[,]
                {
                    { 1, 1},  // top right
                    { 1, -1}, // top left
                    { -1, 1}, // bottom right
                    { -1, -1} // bottom left
                };

                for (int i = 0; i < directions.GetLength(0); ++i)
                {
                    Field field = new Field
                    {
                        Row = from.Row + directions[i, 0] * distance,
                        Column = from.Column + directions[i, 1] * distance
                    };
                    if (Board.CheckFieldBounds(field))
                    {
                        yield return field;
                    }
                }
            }
        }
    }
}
