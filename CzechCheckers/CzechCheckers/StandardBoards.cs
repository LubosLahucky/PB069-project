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
            return new Board(new IFigure[Board.MaxRow - Board.MinRow + 1, Board.MaxCol - Board.MinCol + 1]);
        }

        public static Board Get2v2Board()
        {
            return new Board(new IFigure[Board.MaxRow - Board.MinRow + 1, Board.MaxCol - Board.MinCol + 1]
            {
                { new Pawn(FigureColor.WHITE), null, new Pawn(FigureColor.WHITE), null, new Pawn(FigureColor.WHITE), null, new Pawn(FigureColor.WHITE), null },
                { null, new Pawn(FigureColor.WHITE), null, new Pawn(FigureColor.WHITE), null, new Pawn(FigureColor.WHITE), null, new Pawn(FigureColor.WHITE) },
                { null, null, null, null, null, null, null, null },
                { null, null, null, null, null, null, null, null },
                { null, null, null, null, null, null, null, null },
                { null, null, null, null, null, null, null, null },
                { new Pawn(FigureColor.BLACK), null, new Pawn(FigureColor.BLACK), null, new Pawn(FigureColor.BLACK), null, new Pawn(FigureColor.BLACK), null },
                { null, new Pawn(FigureColor.BLACK), null, new Pawn(FigureColor.BLACK), null, new Pawn(FigureColor.BLACK), null, new Pawn(FigureColor.BLACK) },
            });
        }

        public static Board Get3v3Board()
        {
            return new Board(new IFigure[Board.MaxRow - Board.MinRow + 1, Board.MaxCol - Board.MinCol + 1]
            {
                { new Pawn(FigureColor.WHITE), null, new Pawn(FigureColor.WHITE), null, new Pawn(FigureColor.WHITE), null, new Pawn(FigureColor.WHITE), null },
                { null, new Pawn(FigureColor.WHITE), null, new Pawn(FigureColor.WHITE), null, new Pawn(FigureColor.WHITE), null, new Pawn(FigureColor.WHITE) },
                { new Pawn(FigureColor.WHITE), null, new Pawn(FigureColor.WHITE), null, new Pawn(FigureColor.WHITE), null, new Pawn(FigureColor.WHITE), null },
                { null, null, null, null, null, null, null, null },
                { null, null, null, null, null, null, null, null },
                { null, new Pawn(FigureColor.BLACK), null, new Pawn(FigureColor.BLACK), null, new Pawn(FigureColor.BLACK), null, new Pawn(FigureColor.BLACK) },
                { new Pawn(FigureColor.BLACK), null, new Pawn(FigureColor.BLACK), null, new Pawn(FigureColor.BLACK), null, new Pawn(FigureColor.BLACK), null },
                { null, new Pawn(FigureColor.BLACK), null, new Pawn(FigureColor.BLACK), null, new Pawn(FigureColor.BLACK), null, new Pawn(FigureColor.BLACK) },
            });
        }
    }
}
