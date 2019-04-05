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
            Player white = new Player { Name = "Human", Color = FigureColor.WHITE };
            Player black = new Player { Name = "Human2", Color = FigureColor.BLACK };

            /*
            var board = StandardBoards.GetEmptyBoard();
            Field from = new Field { Row = 2, Column = 2 };
            board[from] = new Pawn(FigureColor.WHITE);
            board[new Field { Row = 3, Column = 1 }] = new Pawn(FigureColor.BLACK);
            board[new Field { Row = 2, Column = 0 }] = new Pawn(FigureColor.WHITE);

            foreach (var field in Helpers.AllFields().Where(to => board.IsMoveValid(new Move(from, to), FigureColor.WHITE)))
                Console.WriteLine(field);
            */

            new Game(white, black, StandardBoards.Get2v2Board()).Start();
        }
    }
}
