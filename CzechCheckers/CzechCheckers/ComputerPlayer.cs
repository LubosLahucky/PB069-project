using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CzechCheckers
{
    class ComputerPlayer : Player
    {
        public Move GenerateMove(Board board)
        {
            var from = board.MovableFields(Color).First();
            var to = board.PieceMoves(from).First();
            return new Move(from, to);
        }
    }
}
