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
            from.Row += Color == FigureColor.WHITE ? 1 : -1;
            from.Column += 1;
            if (Board.CheckFieldBounds(from))
            { 
                yield return from;
            }
            from.Column -= 2;
            if (Board.CheckFieldBounds(from))
            {
                yield return from;
            }
        }
    }
}
