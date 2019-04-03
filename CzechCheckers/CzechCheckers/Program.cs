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
            var from = new Field { Row = 0, Column = 0 };
            board[from] = new Queen(FigureColor.WHITE);
            foreach (var field in board[from].PossibleMoves(from))
                Console.WriteLine(field);
            */

            new Game(white, black, StandardBoards.Get2v2Board()).Start();
        }
    }
}
