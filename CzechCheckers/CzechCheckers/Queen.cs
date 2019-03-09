using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CzechCheckers
{
    class Queen : FigureBase
    {
        public Queen(Color color) : base(color)
        {
        }

        public override bool CanMove(int fromCol, int fromRow, int toCol, int toRow)
        {
            return IsSameDiagonal(fromCol, fromRow, toCol, toRow)
                && Distance(fromCol, fromRow, toCol, toRow) >= 2;
        }

        public override bool CanJump(int fromCol, int fromRow, int toCol, int toRow)
        {
            return CanMove(fromCol, fromRow, toCol, toRow) 
                && Distance(fromCol, fromRow, toCol, toRow) >= 4;
        }

        private bool IsSameDiagonal(int fromCol, int fromRow, int toCol, int toRow)
        {
            return (Math.Abs(fromCol - toCol) == Math.Abs(fromRow - toRow));
        }

        private int Distance(int fromCol, int fromRow, int toCol, int toRow)
        {
            return Math.Abs(fromCol - toCol) + Math.Abs(fromRow - toRow);
        }

        public override string ToString()
        {
            return base.ToString() + "Q";
        }
    }
}
