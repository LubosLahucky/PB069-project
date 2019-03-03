using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CzechCheckers
{
    abstract class FigureBase : IFigure
    {
        private readonly Color color;

        public Color Color
        {
            get
            {
                return color;
            }
        }

        public FigureBase(Color color)
        {
            this.color = color;
        }

        public override string ToString()
        {
            return color.ToString().First() + "";
        }

        public abstract bool CanMove(int fromCol, int fromRow, int toCol, int toRow);
        public abstract bool CanJump(int fromCol, int fromRow, int throughCol, int throughRow, int toCol, int toRow);
    }
}
