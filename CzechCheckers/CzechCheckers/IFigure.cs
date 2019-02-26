using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CzechCheckers
{
    interface IFigure
    {
        /* Checks whether a figure can move from coordinates (fromCol, fromRow) to coordinates (toCol, toRow)  
         */
        bool CanMove(int fromCol, int fromRow, int toCol, int toRow);
    }
}
