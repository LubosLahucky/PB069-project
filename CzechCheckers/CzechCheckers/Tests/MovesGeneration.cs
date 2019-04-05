using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CzechCheckers.Tests
{
    [TestClass]
    public class MovesGeneration
    {
        private Board board;

        [TestInitialize]
        public void SetUp()
        {
            board = StandardBoards.GetEmptyBoard();
        }

        [TestCleanup]
        public void TearDown()
        {
            board = null;
        }

        [TestMethod]
        public void WhitePawnMoves()
        {
            Field from = new Field { Row = 2, Column = 2 };
            IFigure figure = new Pawn(FigureColor.WHITE);
            var correct = new Field[]
            {
                new Field { Row = 3, Column = 1 },
                new Field { Row = 3, Column = 3 }
            };
            Assert.IsTrue(CheckFigureMoves(from, figure, correct));
        }

        [TestMethod]
        public void BlackPawnMoves()
        {
            Field from = new Field { Row = 2, Column = 2 };
            IFigure figure = new Pawn(FigureColor.BLACK);
            var correct = new Field[]
            {
                new Field { Row = 1, Column = 1 },
                new Field { Row = 1, Column = 3 }
            };
            Assert.IsTrue(CheckFigureMoves(from, figure, correct));
        }

        [TestMethod]
        public void WhitePawnMovesLeftEdge()
        {
            Field from = new Field { Row = 2, Column = 0 };
            IFigure figure = new Pawn(FigureColor.WHITE);
            var correct = new Field[]
            {
                new Field { Row = 3, Column = 1 }
            };
            Assert.IsTrue(CheckFigureMoves(from, figure, correct));
        }

        [TestMethod]
        public void BlackPawnMovesLeftEdge()
        {
            Field from = new Field { Row = 2, Column = 0 };
            IFigure figure = new Pawn(FigureColor.BLACK);
            var correct = new Field[]
            {
                new Field { Row = 1, Column = 1 }
            };
            Assert.IsTrue(CheckFigureMoves(from, figure, correct));
        }

        [TestMethod]
        public void WhitePawnMovesRightEdge()
        {
            Field from = new Field { Row = 2, Column = 7 };
            IFigure figure = new Pawn(FigureColor.WHITE);
            var correct = new Field[]
            {
                new Field { Row = 3, Column = 6 }
            };
            Assert.IsTrue(CheckFigureMoves(from, figure, correct));
        }

        [TestMethod]
        public void BlackPawnMovesRightEdge()
        {
            Field from = new Field { Row = 2, Column = 7 };
            IFigure figure = new Pawn(FigureColor.BLACK);
            var correct = new Field[]
            {
                new Field { Row = 1, Column = 6 }
            };
            Assert.IsTrue(CheckFigureMoves(from, figure, correct));
        }

        [TestMethod]
        public void QueenMid()
        {
            Field from = new Field { Row = 3, Column = 3 };
            IFigure figure = new Queen(FigureColor.WHITE);
            var correct = new Field[]
            {
                new Field { Row = 0, Column = 0 },
                new Field { Row = 1, Column = 1 },
                new Field { Row = 2, Column = 2 },
                new Field { Row = 4, Column = 4 },
                new Field { Row = 5, Column = 5 },
                new Field { Row = 6, Column = 6 },
                new Field { Row = 7, Column = 7 },
                new Field { Row = 6, Column = 0 },
                new Field { Row = 5, Column = 1 },
                new Field { Row = 4, Column = 2 },
                new Field { Row = 2, Column = 4 },
                new Field { Row = 1, Column = 5 },
                new Field { Row = 0, Column = 6 },
            };
            Assert.IsTrue(CheckFigureMoves(from, figure, correct));
        }

        [TestMethod]
        public void QueenLeftTopEdge1()
        {
            Field from = new Field { Row = 6, Column = 0 };
            IFigure figure = new Queen(FigureColor.WHITE);
            var correct = new Field[]
            {
                new Field { Row = 7, Column = 1 },
                new Field { Row = 5, Column = 1 },
                new Field { Row = 4, Column = 2 },
                new Field { Row = 3, Column = 3 },
                new Field { Row = 2, Column = 4 },
                new Field { Row = 1, Column = 5 },
                new Field { Row = 0, Column = 6 },
            };
            Assert.IsTrue(CheckFigureMoves(from, figure, correct));
        }

        [TestMethod]
        public void QueenLeftTopEdge2()
        {
            Field from = new Field { Row = 7, Column = 1 };
            IFigure figure = new Queen(FigureColor.WHITE);
            var correct = new Field[]
            {
                new Field { Row = 6, Column = 0 },
                new Field { Row = 6, Column = 2 },
                new Field { Row = 5, Column = 3 },
                new Field { Row = 4, Column = 4 },
                new Field { Row = 3, Column = 5 },
                new Field { Row = 2, Column = 6 },
                new Field { Row = 1, Column = 7 },
            };
            Assert.IsTrue(CheckFigureMoves(from, figure, correct));
        }

        [TestMethod]
        public void QueenLeftBottomEdge()
        {
            Field from = new Field { Row = 0, Column = 0 };
            IFigure figure = new Queen(FigureColor.WHITE);
            var correct = new Field[]
            {
                new Field { Row = 1, Column = 1 },
                new Field { Row = 2, Column = 2 },
                new Field { Row = 3, Column = 3 },
                new Field { Row = 4, Column = 4 },
                new Field { Row = 5, Column = 5 },
                new Field { Row = 6, Column = 6 },
                new Field { Row = 7, Column = 7 },
            };
            Assert.IsTrue(CheckFigureMoves(from, figure, correct));
        }

        [TestMethod]
        public void QueenRightBottomEdge1()
        {
            Field from = new Field { Row = 0, Column = 6 };
            IFigure figure = new Queen(FigureColor.WHITE);
            var correct = new Field[]
            {
                new Field { Row = 1, Column = 7 },
                new Field { Row = 6, Column = 0 },
                new Field { Row = 5, Column = 1 },
                new Field { Row = 4, Column = 2 },
                new Field { Row = 3, Column = 3 },
                new Field { Row = 2, Column = 4 },
                new Field { Row = 1, Column = 5 },
            };
            Assert.IsTrue(CheckFigureMoves(from, figure, correct));
        }

        [TestMethod]
        public void QueenRightBottomEdge2()
        {
            Field from = new Field { Row = 1, Column = 7 };
            IFigure figure = new Queen(FigureColor.WHITE);
            var correct = new Field[]
            {
                new Field { Row = 0, Column = 6 },
                new Field { Row = 7, Column = 1 },
                new Field { Row = 6, Column = 2 },
                new Field { Row = 5, Column = 3 },
                new Field { Row = 4, Column = 4 },
                new Field { Row = 3, Column = 5 },
                new Field { Row = 2, Column = 6 },
            };
            Assert.IsTrue(CheckFigureMoves(from, figure, correct));
        }

        [TestMethod]
        public void QueenRightBottomEdge()
        {
            Field from = new Field { Row = 7, Column = 7 };
            IFigure figure = new Queen(FigureColor.WHITE);
            var correct = new Field[]
            {
                new Field { Row = 0, Column = 0 },
                new Field { Row = 1, Column = 1 },
                new Field { Row = 2, Column = 2 },
                new Field { Row = 3, Column = 3 },
                new Field { Row = 4, Column = 4 },
                new Field { Row = 5, Column = 5 },
                new Field { Row = 6, Column = 6 },
            };
            Assert.IsTrue(CheckFigureMoves(from, figure, correct));
        }

        private bool CheckFigureMoves(Field from, IFigure figure, params Field[] correct)
        {
            board[from] = figure;

            var possibleMoves = figure.PossibleMoves(from);

            return possibleMoves.Count() == correct.Count()
                && !possibleMoves.Except(correct).Any();
        }
    }
}
