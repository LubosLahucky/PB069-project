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
            int direction = color == Color.WHITE ? 1 : -1;
            return toRow == fromRow + direction && (toCol == fromCol - 1 || toCol == fromCol + 1);  
        }

        public override string ToString()
        {
            return base.ToString() + "P";
        }
    }
}
