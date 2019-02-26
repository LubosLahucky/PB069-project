using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CzechCheckers
{
    class Game
    {
        private readonly Player whitePlayer;
        private readonly Player blackPlayer;
        private readonly Board board;

        public Game(Player whitePlayer, Player blackPlayer, Board board)
        {
            this.whitePlayer = whitePlayer;
            this.blackPlayer = blackPlayer;
            this.board = board;
        }

        public void Start()
        {
            // TODO
        }
    }
}
