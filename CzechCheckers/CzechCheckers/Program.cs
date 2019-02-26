using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CzechCheckers
{
    class Program
    {
        static void Main(string[] args)
        {
            IFigure[,] fields = new IFigure[Board.MaxRow - Board.MinRow + 1, Board.MaxCol - Board.MinCol + 1]
            {
                { new Pawn(Color.WHITE), null, new Pawn(Color.WHITE), null, new Pawn(Color.WHITE), null, new Pawn(Color.WHITE), null },
                { null, new Pawn(Color.WHITE), null, new Pawn(Color.WHITE), null, new Pawn(Color.WHITE), null, new Pawn(Color.WHITE) },
                { null, null, null, null, null, null, null, null },
                { null, null, null, null, null, null, null, null },
                { null, null, null, null, null, null, null, null },
                { null, null, null, null, null, null, null, null },
                { new Pawn(Color.BLACK), null, new Pawn(Color.BLACK), null, new Pawn(Color.BLACK), null, new Pawn(Color.BLACK), null },
                { null, new Pawn(Color.BLACK), null, new Pawn(Color.BLACK), null, new Pawn(Color.BLACK), null, new Pawn(Color.BLACK) },
            };

            Board board = new Board(fields);
            Console.WriteLine(board);
        }
    }
}
