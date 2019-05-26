using System.Collections.Generic;

namespace CzechCheckers
{
    public interface IPiece
    {
        /// <summary>
        /// Defines the way how the piece moves
        /// </summary>
        /// <param name="move">Move to be decided whether it is valid or not (ignoring other pieces)</param>
        /// <returns></returns>
        bool CanMove(Move move);

        /// <summary>
        /// Defines the way how the piece jumps
        /// </summary>
        /// <param name="move">Jump to be decided whether it is valid or not (ignoring other pieces)</param>
        /// <returns></returns>
        bool CanJump(Move move);

        /// <summary>
        /// Generator of the collection of fields where the piece can move (ignoring other pieces) from field
        /// </summary>
        /// <param name="from">Starting field</param>
        /// <returns></returns>
        IEnumerable<Field> PossibleMoves(Field from);

        /// <summary>
        /// Generator of the collection of fields where the piece can jump (ignoring other pieces) from field
        /// </summary>
        /// <param name="from">Starting field</param>
        /// <returns></returns>
        IEnumerable<Field> PossibleJumps(Field from);

        Color Color { get; }
    }
}
