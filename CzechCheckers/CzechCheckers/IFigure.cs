using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CzechCheckers
{
    interface IFigure
    {
        /// <summary>
        /// Defines the way how the figure moves
        /// </summary>
        /// <param name="move">Move to be decided whether it is valid or not (on infinite empty board)</param>
        /// <returns></returns>
        bool CanMove(Move move);

        /// <summary>
        /// Defines the way how the figure jumps
        /// </summary>
        /// <param name="move">Jump to be decided whether it is valid or not (on infinite empty board)</param>
        /// <returns></returns>
        bool CanJump(Move move);

        FigureColor Color { get; }
    }
}
