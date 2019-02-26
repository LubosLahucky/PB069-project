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
            return toRow == fromRow + 1 && (toCol == fromCol - 1 || toCol == fromCol + 1);  
        }

        public override string ToString()
        {
            return base.ToString() + "P";
        }
    }
}
