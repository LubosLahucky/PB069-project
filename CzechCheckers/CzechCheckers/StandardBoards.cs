using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CzechCheckers
{
    sealed class StandardBoards
    {
        private StandardBoards() { }

        public static Board GetEmptyBoard()
        {
            return new Board(new IPiece[Board.MaxRow - Board.MinRow + 1, Board.MaxCol - Board.MinCol + 1]);
        }

        public static Board Get2v2Board()
        {
            return new Board(new IPiece[Board.MaxRow - Board.MinRow + 1, Board.MaxCol - Board.MinCol + 1]
            {
                { new Pawn(Color.White), null, new Pawn(Color.White), null, new Pawn(Color.White), null, new Pawn(Color.White), null },
                { null, new Pawn(Color.White), null, new Pawn(Color.White), null, new Pawn(Color.White), null, new Pawn(Color.White) },
                { null, null, null, null, null, null, null, null },
                { null, null, null, null, null, null, null, null },
                { null, null, null, null, null, null, null, null },
                { null, null, null, null, null, null, null, null },
                { new Pawn(Color.Black), null, new Pawn(Color.Black), null, new Pawn(Color.Black), null, new Pawn(Color.Black), null },
                { null, new Pawn(Color.Black), null, new Pawn(Color.Black), null, new Pawn(Color.Black), null, new Pawn(Color.Black) },
            });
        }

        public static Board Get3v3Board()
        {
            return new Board(new IPiece[Board.MaxRow - Board.MinRow + 1, Board.MaxCol - Board.MinCol + 1]
            {
                { new Pawn(Color.White), null, new Pawn(Color.White), null, new Pawn(Color.White), null, new Pawn(Color.White), null },
                { null, new Pawn(Color.White), null, new Pawn(Color.White), null, new Pawn(Color.White), null, new Pawn(Color.White) },
                { new Pawn(Color.White), null, new Pawn(Color.White), null, new Pawn(Color.White), null, new Pawn(Color.White), null },
                { null, null, null, null, null, null, null, null },
                { null, null, null, null, null, null, null, null },
                { null, new Pawn(Color.Black), null, new Pawn(Color.Black), null, new Pawn(Color.Black), null, new Pawn(Color.Black) },
                { new Pawn(Color.Black), null, new Pawn(Color.Black), null, new Pawn(Color.Black), null, new Pawn(Color.Black), null },
                { null, new Pawn(Color.Black), null, new Pawn(Color.Black), null, new Pawn(Color.Black), null, new Pawn(Color.Black) },
            });
        }
    }
}
