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
                { new Pawn(FigureColor.WHITE), null, new Pawn(FigureColor.WHITE), null, new Pawn(FigureColor.WHITE), null, new Pawn(FigureColor.WHITE), null },
                { null, new Pawn(FigureColor.WHITE), null, new Pawn(FigureColor.WHITE), null, new Pawn(FigureColor.WHITE), null, new Pawn(FigureColor.WHITE) },
                { null, null, null, null, null, null, null, null },
                { null, null, null, null, null, null, null, null },
                { null, null, null, null, null, null, null, null },
                { null, null, null, null, null, null, null, null },
                { new Pawn(FigureColor.BLACK), null, new Pawn(FigureColor.BLACK), null, new Pawn(FigureColor.BLACK), null, new Pawn(FigureColor.BLACK), null },
                { null, new Pawn(FigureColor.BLACK), null, new Pawn(FigureColor.BLACK), null, new Pawn(FigureColor.BLACK), null, new Pawn(FigureColor.BLACK) },
            };

            Player white = new Player { Name = "Human", Color = FigureColor.WHITE };
            Player black = new Player { Name = "Human2", Color = FigureColor.BLACK };
            Board board = new Board(fields);

            new Game(white, black, board).Start();
        }
    }
}
