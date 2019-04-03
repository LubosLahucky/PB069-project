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
            return IsSameDiagonal(move)
                && Distance(move) >= 2;
        }

        public override bool CanJump(Move move)
        {
            return CanMove(move) 
                && Distance(move) >= 4;
        }

        private bool IsSameDiagonal(Move move)
        {
            return Math.Abs(move.From.Column - move.To.Column) == Math.Abs(move.From.Row - move.To.Row);
        }

        private int Distance(Move move)
        {
            return Math.Abs(move.From.Column - move.To.Column) + Math.Abs(move.From.Row - move.To.Row);
        }

        public override string ToString()
        {
            return base.ToString() + "Q";
        }

        public override IEnumerable<Field> PossibleMoves(Field from)
        {
            var maxDistance = Math.Max(Board.MaxRow, Board.MaxCol);
            for (int distance = 1; distance <= maxDistance; ++distance)
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
