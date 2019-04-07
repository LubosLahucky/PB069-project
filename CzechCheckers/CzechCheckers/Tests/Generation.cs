using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CzechCheckers.Tests
{
    [TestClass]
    public class Generation
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

        #region Moves
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
        public void QueenMidMoves()
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
        public void QueenLeftTopEdge1Moves()
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
        public void QueenLeftTopEdge2Moves()
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
        public void QueenLeftBottomEdgeMoves()
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
        public void QueenRightBottomEdge1Moves()
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
        public void QueenRightBottomEdge2Moves()
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
        public void QueenRightBottomEdgeMoves()
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
        #endregion

        #region Jumps
        [TestMethod]
        public void WhitePawnJumps()
        {
            Field from = new Field { Row = 2, Column = 2 };
            IFigure figure = new Pawn(FigureColor.WHITE);
            var correct = new Field[]
            {
                new Field { Row = 4, Column = 0 },
                new Field { Row = 4, Column = 4 }
            };
            Assert.IsTrue(CheckFigureJumps(from, figure, correct));
        }

        [TestMethod]
        public void BlackPawnJumps()
        {
            Field from = new Field { Row = 2, Column = 2 };
            IFigure figure = new Pawn(FigureColor.BLACK);
            var correct = new Field[]
            {
                new Field { Row = 0, Column = 0 },
                new Field { Row = 0, Column = 4 }
            };
            Assert.IsTrue(CheckFigureJumps(from, figure, correct));
        }

        [TestMethod]
        public void WhitePawnJumpsLeftEdge()
        {
            Field from = new Field { Row = 2, Column = 0 };
            IFigure figure = new Pawn(FigureColor.WHITE);
            var correct = new Field[]
            {
                new Field { Row = 4, Column = 2 }
            };
            Assert.IsTrue(CheckFigureJumps(from, figure, correct));
        }

        [TestMethod]
        public void BlackPawnJumpsLeftEdge()
        {
            Field from = new Field { Row = 2, Column = 0 };
            IFigure figure = new Pawn(FigureColor.BLACK);
            var correct = new Field[]
            {
                new Field { Row = 0, Column = 2 }
            };
            Assert.IsTrue(CheckFigureJumps(from, figure, correct));
        }

        [TestMethod]
        public void WhitePawnJumpsRightEdge()
        {
            Field from = new Field { Row = 2, Column = 7 };
            IFigure figure = new Pawn(FigureColor.WHITE);
            var correct = new Field[]
            {
                new Field { Row = 4, Column = 5 }
            };
            Assert.IsTrue(CheckFigureJumps(from, figure, correct));
        }

        [TestMethod]
        public void BlackPawnJumpsRightEdge()
        {
            Field from = new Field { Row = 2, Column = 7 };
            IFigure figure = new Pawn(FigureColor.BLACK);
            var correct = new Field[]
            {
                new Field { Row = 0, Column = 5 }
            };
            Assert.IsTrue(CheckFigureJumps(from, figure, correct));
        }

        [TestMethod]
        public void QueenMidJumps()
        {
            Field from = new Field { Row = 3, Column = 3 };
            IFigure figure = new Queen(FigureColor.WHITE);
            var correct = new Field[]
            {
                new Field { Row = 0, Column = 0 },
                new Field { Row = 1, Column = 1 },
                new Field { Row = 5, Column = 5 },
                new Field { Row = 6, Column = 6 },
                new Field { Row = 7, Column = 7 },
                new Field { Row = 6, Column = 0 },
                new Field { Row = 5, Column = 1 },
                new Field { Row = 1, Column = 5 },
                new Field { Row = 0, Column = 6 },
            };
            Assert.IsTrue(CheckFigureJumps(from, figure, correct));
        }

        [TestMethod]
        public void QueenLeftTopEdge1Jumps()
        {
            Field from = new Field { Row = 6, Column = 0 };
            IFigure figure = new Queen(FigureColor.WHITE);
            var correct = new Field[]
            {
                new Field { Row = 4, Column = 2 },
                new Field { Row = 3, Column = 3 },
                new Field { Row = 2, Column = 4 },
                new Field { Row = 1, Column = 5 },
                new Field { Row = 0, Column = 6 },
            };
            Assert.IsTrue(CheckFigureJumps(from, figure, correct));
        }

        [TestMethod]
        public void QueenLeftTopEdge2Jumps()
        {
            Field from = new Field { Row = 7, Column = 1 };
            IFigure figure = new Queen(FigureColor.WHITE);
            var correct = new Field[]
            {
                new Field { Row = 5, Column = 3 },
                new Field { Row = 4, Column = 4 },
                new Field { Row = 3, Column = 5 },
                new Field { Row = 2, Column = 6 },
                new Field { Row = 1, Column = 7 },
            };
            Assert.IsTrue(CheckFigureJumps(from, figure, correct));
        }

        [TestMethod]
        public void QueenLeftBottomEdgeJumps()
        {
            Field from = new Field { Row = 0, Column = 0 };
            IFigure figure = new Queen(FigureColor.WHITE);
            var correct = new Field[]
            {
                new Field { Row = 2, Column = 2 },
                new Field { Row = 3, Column = 3 },
                new Field { Row = 4, Column = 4 },
                new Field { Row = 5, Column = 5 },
                new Field { Row = 6, Column = 6 },
                new Field { Row = 7, Column = 7 },
            };
            Assert.IsTrue(CheckFigureJumps(from, figure, correct));
        }

        [TestMethod]
        public void QueenRightBottomEdge1Jumps()
        {
            Field from = new Field { Row = 0, Column = 6 };
            IFigure figure = new Queen(FigureColor.WHITE);
            var correct = new Field[]
            {
                new Field { Row = 6, Column = 0 },
                new Field { Row = 5, Column = 1 },
                new Field { Row = 4, Column = 2 },
                new Field { Row = 3, Column = 3 },
                new Field { Row = 2, Column = 4 },
            };
            Assert.IsTrue(CheckFigureJumps(from, figure, correct));
        }

        [TestMethod]
        public void QueenRightBottomEdge2Jumps()
        {
            Field from = new Field { Row = 1, Column = 7 };
            IFigure figure = new Queen(FigureColor.WHITE);
            var correct = new Field[]
            {
                new Field { Row = 7, Column = 1 },
                new Field { Row = 6, Column = 2 },
                new Field { Row = 5, Column = 3 },
                new Field { Row = 4, Column = 4 },
                new Field { Row = 3, Column = 5 },
            };
            Assert.IsTrue(CheckFigureJumps(from, figure, correct));
        }

        [TestMethod]
        public void QueenRightBottomEdgeJumps()
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
            };
            Assert.IsTrue(CheckFigureJumps(from, figure, correct));
        }

        #endregion

        private bool CheckFigureMoves(Field from, IFigure figure, params Field[] correct)
        {
            board[from] = figure;
            var possibleMoves = figure.PossibleMoves(from);
            return ContainsSameValues(possibleMoves, correct);
        }

        private bool CheckFigureJumps(Field from, IFigure figure, params Field[] correct)
        {
            board[from] = figure;
            var possibleJumps = figure.PossibleJumps(from);
            return ContainsSameValues(possibleJumps, correct);
        }

        private bool ContainsSameValues(IEnumerable<Field> generated, params Field[] correct)
        {
            return generated.Count() == correct.Count()
                && !generated.Except(correct).Any();
        }
    }
}
