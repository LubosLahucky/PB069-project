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
        /// <param name="move">Move to be decided whether it is valid or not (ignoring other figues)</param>
        /// <returns></returns>
        bool CanMove(Move move);

        /// <summary>
        /// Defines the way how the figure jumps
        /// </summary>
        /// <param name="move">Jump to be decided whether it is valid or not (ignoring other figues)</param>
        /// <returns></returns>
        bool CanJump(Move move);

        /// <summary>
        /// Generator of the collection of fields where the figure can move (ignoring other figures) from field
        /// </summary>
        /// <param name="from">Starting field</param>
        /// <returns></returns>
        IEnumerable<Field> PossibleMoves(Field from);

        /// <summary>
        /// Generator of the collection of fields where the figure can jump (ignoring other figures) from field
        /// </summary>
        /// <param name="from">Starting field</param>
        /// <returns></returns>
        IEnumerable<Field> PossibleJumps(Field from);

        FigureColor Color { get; }
    }
}
