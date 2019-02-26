using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CzechCheckers
{
    abstract class FigureBase : IFigure
    {
        public readonly Color color;

        public FigureBase(Color color)
        {
            this.color = color;
        }

        public override string ToString()
        {
            return color.ToString().First() + "";
        }

        public abstract bool CanMove(int fromCol, int fromRow, int toCol, int toRow);
    }
}
