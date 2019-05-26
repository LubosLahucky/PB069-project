using System.Collections.Generic;
using System.Linq;

namespace CzechCheckers
{
    public abstract class PieceBase : IPiece
    {
        public Color Color { get; }

        public PieceBase(Color color)
        {
            Color = color;
        }

        public override string ToString()
        {
            return Color.ToString().First() + "";
        }

        public abstract bool CanMove(Move move);

        public abstract bool CanJump(Move move);

        public abstract IEnumerable<Field> PossibleMoves(Field from);

        public abstract IEnumerable<Field> PossibleJumps(Field from);
        
    }
}
