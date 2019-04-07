using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CzechCheckers
{
    class Pawn : FigureBase
    {
        public Pawn(FigureColor color) : base(color)
        {
        }

        public override bool CanMove(Move move)
        {
            return IsDiagonalStep(move, 1);
        }

        public override bool CanJump(Move move)
        {
            return IsDiagonalStep(move, 2);
        }

        private bool IsDiagonalStep(Move move, int distance)
        {
            int direction = Color == FigureColor.WHITE ? distance : -distance;
            return move.To.Row == move.From.Row + direction 
                && Math.Abs(move.To.Column - move.From.Column) == distance;
        }

        public override string ToString()
        {
            return base.ToString() + "P";
        }

        public override IEnumerable<Field> PossibleMoves(Field from)
        {
            foreach (var field in DiagonalStepsForward(from, 1))
                yield return field;
        }

        public override IEnumerable<Field> PossibleJumps(Field from)
        {
            foreach (var field in DiagonalStepsForward(from, 2))
                yield return field;
        }

        private IEnumerable<Field> DiagonalStepsForward(Field from, int distance)
        {
            int verticalDirection = Color == FigureColor.WHITE ? 1 : -1;
            int[] horizontalDirections = new int[]
            {
                -1, // left
                1   // right
            };
            foreach (int horizontalDirection in horizontalDirections)
            {
                var field = new Field
                {
                    Row = from.Row + verticalDirection * distance,
                    Column = from.Column + horizontalDirection * distance
                };
                if (Board.CheckFieldBounds(field))
                {
                    yield return field;
                }
            }
        }
    }
}
