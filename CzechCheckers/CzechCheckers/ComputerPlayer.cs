using System.Linq;

namespace CzechCheckers
{
    public class ComputerPlayer : Player
    {
        // TODO: better strategy
        public Move GenerateMove(Board board)
        {
            var from = board.MovableFields(Color).First();
            var to = board.PieceMoves(from, Color).First();
            return new Move(from, to);
        }
    }
}
