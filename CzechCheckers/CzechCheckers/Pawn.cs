using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CzechCheckers
{
    class Pawn : FigureBase
    {
        public Pawn(Color color) : base(color)
        {
        }

        public override bool CanMove(int fromCol, int fromRow, int toCol, int toRow)
        {
            return IsDiagonalStep(fromCol, fromRow, toCol, toRow, 1);
        }

        public override bool CanJump(int fromCol, int fromRow, int throughCol, int throughRow, int toCol, int toRow)
        {
            return IsDiagonalStep(fromCol, fromRow, throughCol, throughRow, 1) && IsDiagonalStep(fromCol, fromRow, toCol, toRow, 2);
        }

        private bool IsDiagonalStep(int fromCol, int fromRow, int toCol, int toRow, int distance)
        {
            int direction = Color == Color.WHITE ? distance : -distance;
            return toRow == fromRow + direction && (toCol == fromCol - distance || toCol == fromCol + distance);
        }

        public override string ToString()
        {
            return base.ToString() + "P";
        }
    }
}
