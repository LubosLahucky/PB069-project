using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CzechCheckers
{
    abstract class FigureBase : IFigure
    {
        public Color Color { get; }

        public FigureBase(Color color)
        {
            Color = color;
        }

        public override string ToString()
        {
            return Color.ToString().First() + "";
        }

        public abstract bool CanMove(int fromCol, int fromRow, int toCol, int toRow);

        public abstract bool CanJump(int fromCol, int fromRow, int toCol, int toRow);
    }
}
