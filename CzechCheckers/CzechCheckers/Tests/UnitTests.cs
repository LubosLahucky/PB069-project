using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CzechCheckers.Tests
{
    [TestClass]
    public class BasicMovement
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

        private bool CheckFigureMoves(Field from, IFigure figure, params Field[] correct)
        {
            board[from] = figure;

            var possibleMoves = figure.PossibleMoves(from);

            return possibleMoves.Count() == correct.Count() && !possibleMoves.Except(correct).Any(); 
        }
    }
}
