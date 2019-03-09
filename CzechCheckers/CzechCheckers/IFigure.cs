using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CzechCheckers
{
    interface IFigure
    {
        /* Checks whether the figure can move from coordinates (fromCol, fromRow) to coordinates (toCol, toRow)  
         */
        bool CanMove(int fromCol, int fromRow, int toCol, int toRow);

        /* Checkes whether the figure can jump from coordinates (fromCol, fromRow) and land on coordinates (toCol, toRow)  
         */
        bool CanJump(int fromCol, int fromRow, int toCol, int toRow);

        Color Color { get; }
    }
}
