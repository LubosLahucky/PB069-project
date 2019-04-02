using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CzechCheckers
{
    abstract class FigureBase : IFigure
    {
        public FigureColor Color { get; }

        public FigureBase(FigureColor color)
        {
            Color = color;
        }

        public override string ToString()
        {
            return Color.ToString().First() + "";
        }

        public abstract bool CanMove(Move move);

        public abstract bool CanJump(Move move);
    }
}
