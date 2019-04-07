using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CzechCheckers.Tests
{
    [TestClass]
    public class Validation
    {
        private Board board;

        [TestInitialize]
        public void SetUp()
        {
            board = StandardBoards.GetEmptyBoard();
        }

        [TestCleanup]
        public void CleanUp()
        {
            board = null;
        }

        [TestMethod]
        public void BasicWhitePawnMoves()
        {
            Field from = new Field { Row = 3, Column = 3 };
            board[from] = new Pawn(FigureColor.WHITE);
            var correct = new Field[]
            {
                new Field { Row = 4, Column = 4 },
                new Field { Row = 4, Column = 2 }
            };
            Assert.IsTrue(CheckFigureMoves(from, FigureColor.WHITE, correct));
        }

        [TestMethod]
        public void BasicBlackPawnMoves()
        {
            Field from = new Field { Row = 3, Column = 3 };
            board[from] = new Pawn(FigureColor.BLACK);
            var correct = new Field[]
            {
                new Field { Row = 2, Column = 4 },
                new Field { Row = 2, Column = 2 }
            };
            Assert.IsTrue(CheckFigureMoves(from, FigureColor.BLACK, correct));
        }

        [TestMethod]
        public void WhitePawnBlocked1()
        {
            Field from = new Field { Row = 3, Column = 3 };
            board[from] = new Pawn(FigureColor.WHITE);
            board[new Field { Row = 4, Column = 4 }] = new Pawn(FigureColor.WHITE);
            var correct = new Field[]
            {
                new Field { Row = 4, Column = 2 }
            };
            Assert.IsTrue(CheckFigureMoves(from, FigureColor.WHITE, correct));
        }

        [TestMethod]
        public void WhitePawnBlocked2()
        {
            Field from = new Field { Row = 3, Column = 3 };
            board[from] = new Pawn(FigureColor.WHITE);
            board[new Field { Row = 4, Column = 4 }] = new Pawn(FigureColor.WHITE);
            board[new Field { Row = 4, Column = 2 }] = new Pawn(FigureColor.BLACK);
            var correct = new Field[]
            {
                new Field { Row = 5, Column = 1 }
            };
            Assert.IsTrue(CheckFigureMoves(from, FigureColor.WHITE, correct));
        }

        [TestMethod]
        public void WhitePawnBlocked3()
        {
            Field from = new Field { Row = 3, Column = 3 };
            board[from] = new Pawn(FigureColor.WHITE);
            board[new Field { Row = 4, Column = 4 }] = new Pawn(FigureColor.WHITE);
            board[new Field { Row = 4, Column = 2 }] = new Pawn(FigureColor.WHITE);
            Assert.IsTrue(CheckFigureMoves(from, FigureColor.WHITE, new Field[0]));
        }

        [TestMethod]
        public void BlackPawnBlocked1()
        {
            Field from = new Field { Row = 3, Column = 3 };
            board[from] = new Pawn(FigureColor.BLACK);
            board[new Field { Row = 2, Column = 4 }] = new Pawn(FigureColor.BLACK);
            var correct = new Field[]
            {
                new Field { Row = 2, Column = 2 }
            };
            Assert.IsTrue(CheckFigureMoves(from, FigureColor.BLACK, correct));
        }

        [TestMethod]
        public void BlackPawnBlocked2()
        {
            Field from = new Field { Row = 3, Column = 3 };
            board[from] = new Pawn(FigureColor.BLACK);
            board[new Field { Row = 2, Column = 4 }] = new Pawn(FigureColor.BLACK);
            board[new Field { Row = 2, Column = 2 }] = new Pawn(FigureColor.WHITE);
            var correct = new Field[]
            {
                new Field { Row = 1, Column = 1 }
            };
            Assert.IsTrue(CheckFigureMoves(from, FigureColor.BLACK, correct));
        }

        [TestMethod]
        public void BlackPawnBlocked3()
        {
            Field from = new Field { Row = 3, Column = 3 };
            board[from] = new Pawn(FigureColor.BLACK);
            board[new Field { Row = 2, Column = 4 }] = new Pawn(FigureColor.BLACK);
            board[new Field { Row = 2, Column = 2 }] = new Pawn(FigureColor.BLACK);
            Assert.IsTrue(CheckFigureMoves(from, FigureColor.BLACK, new Field[0]));
        }

        [TestMethod]
        public void WhitePawnEdge()
        {
            Field from = new Field { Row = 3, Column = 7 };
            board[from] = new Pawn(FigureColor.WHITE);
            var correct = new Field[]
            {
                new Field { Row = 4, Column = 6 }
            };
            Assert.IsTrue(CheckFigureMoves(from, FigureColor.WHITE, correct));
        }

        [TestMethod]
        public void WhitePawnEdgeBlocked()
        {
            Field from = new Field { Row = 3, Column = 7 };
            board[from] = new Pawn(FigureColor.WHITE);
            board[new Field { Row = 4, Column = 6 }] = new Pawn(FigureColor.WHITE);
            Assert.IsTrue(CheckFigureMoves(from, FigureColor.WHITE, new Field[0]));
        }

        [TestMethod]
        public void BlackPawnEdge()
        {
            Field from = new Field { Row = 3, Column = 7 };
            board[from] = new Pawn(FigureColor.BLACK);
            var correct = new Field[]
            {
                new Field { Row = 2, Column = 6 }
            };
            Assert.IsTrue(CheckFigureMoves(from, FigureColor.BLACK, correct));
        }

        [TestMethod]
        public void BlackPawnEdgeBlocked()
        {
            Field from = new Field { Row = 3, Column = 7 };
            board[from] = new Pawn(FigureColor.BLACK);
            board[new Field { Row = 2, Column = 6 }] = new Pawn(FigureColor.BLACK);
            Assert.IsTrue(CheckFigureMoves(from, FigureColor.BLACK, new Field[0]));
        }

        [TestMethod]
        public void WhiteQueenMid()
        {
            Field from = new Field { Row = 3, Column = 3};
            board[from] = new Queen(FigureColor.WHITE);
            var correct = new Field[]
            {
                new Field { Row = 6, Column = 0 },
                new Field { Row = 5, Column = 1 },
                new Field { Row = 4, Column = 2 },
                new Field { Row = 2, Column = 4 },
                new Field { Row = 1, Column = 5 },
                new Field { Row = 0, Column = 6 },
                new Field { Row = 0, Column = 0 },
                new Field { Row = 1, Column = 1 },
                new Field { Row = 2, Column = 2 },
                new Field { Row = 4, Column = 4 },
                new Field { Row = 5, Column = 5 },
                new Field { Row = 6, Column = 6 },
                new Field { Row = 7, Column = 7 },
            };
            Assert.IsTrue(CheckFigureMoves(from, FigureColor.WHITE, correct));
        }

        [TestMethod]
        public void WhiteQueenBlocked1()
        {
            Field from = new Field { Row = 3, Column = 3 };
            board[from] = new Queen(FigureColor.WHITE);
            board[new Field { Row = 4, Column = 4 }] = new Pawn(FigureColor.WHITE);
            var correct = new Field[]
            {
                new Field { Row = 6, Column = 0 },
                new Field { Row = 5, Column = 1 },
                new Field { Row = 4, Column = 2 },
                new Field { Row = 2, Column = 4 },
                new Field { Row = 1, Column = 5 },
                new Field { Row = 0, Column = 6 },
                new Field { Row = 0, Column = 0 },
                new Field { Row = 1, Column = 1 },
                new Field { Row = 2, Column = 2 }
            };
            Assert.IsTrue(CheckFigureMoves(from, FigureColor.WHITE, correct));
        }

        [TestMethod]
        public void WhiteQueenBlocked2()
        {
            Field from = new Field { Row = 3, Column = 3 };
            board[from] = new Queen(FigureColor.WHITE);
            board[new Field { Row = 2, Column = 2 }] = new Pawn(FigureColor.WHITE);
            board[new Field { Row = 4, Column = 4 }] = new Pawn(FigureColor.WHITE);
            var correct = new Field[]
            {
                new Field { Row = 6, Column = 0 },
                new Field { Row = 5, Column = 1 },
                new Field { Row = 4, Column = 2 },
                new Field { Row = 2, Column = 4 },
                new Field { Row = 1, Column = 5 },
                new Field { Row = 0, Column = 6 }
            };
            Assert.IsTrue(CheckFigureMoves(from, FigureColor.WHITE, correct));
        }

        [TestMethod]
        public void WhiteQueenBlockedJumps()
        {
            Field from = new Field { Row = 3, Column = 3 };
            board[from] = new Queen(FigureColor.WHITE);
            board[new Field { Row = 2, Column = 2 }] = new Pawn(FigureColor.WHITE);
            board[new Field { Row = 4, Column = 4 }] = new Pawn(FigureColor.WHITE);
            board[new Field { Row = 1, Column = 1 }] = new Pawn(FigureColor.BLACK);
            board[new Field { Row = 2, Column = 4 }] = new Pawn(FigureColor.BLACK);
            board[new Field { Row = 4, Column = 2 }] = new Pawn(FigureColor.BLACK);
            board[new Field { Row = 5, Column = 1 }] = new Pawn(FigureColor.BLACK);
            board[new Field { Row = 6, Column = 6 }] = new Pawn(FigureColor.BLACK);
            var correct = new Field[]
            {
                new Field { Row = 1, Column = 5 },
                new Field { Row = 0, Column = 6 }
            };
            Assert.IsTrue(CheckFigureMoves(from, FigureColor.WHITE, correct));
        }

        [TestMethod]
        public void BlackQueenBlockedJumps()
        {
            Field from = new Field { Row = 3, Column = 3 };
            board[from] = new Queen(FigureColor.BLACK);
            board[new Field { Row = 2, Column = 2 }] = new Pawn(FigureColor.BLACK);
            board[new Field { Row = 4, Column = 4 }] = new Pawn(FigureColor.BLACK);
            board[new Field { Row = 1, Column = 1 }] = new Pawn(FigureColor.WHITE);
            board[new Field { Row = 2, Column = 4 }] = new Pawn(FigureColor.WHITE);
            board[new Field { Row = 4, Column = 2 }] = new Pawn(FigureColor.WHITE);
            board[new Field { Row = 5, Column = 1 }] = new Pawn(FigureColor.WHITE);
            board[new Field { Row = 6, Column = 6 }] = new Pawn(FigureColor.WHITE);
            var correct = new Field[]
            {
                new Field { Row = 1, Column = 5 },
                new Field { Row = 0, Column = 6 }
            };
            Assert.IsTrue(CheckFigureMoves(from, FigureColor.BLACK, correct));
        }

        [TestMethod]
        public void BlackQueenMid()
        {
            Field from = new Field { Row = 3, Column = 3 };
            board[from] = new Queen(FigureColor.BLACK);
            var correct = new Field[]
            {
                new Field { Row = 6, Column = 0 },
                new Field { Row = 5, Column = 1 },
                new Field { Row = 4, Column = 2 },
                new Field { Row = 2, Column = 4 },
                new Field { Row = 1, Column = 5 },
                new Field { Row = 0, Column = 6 },
                new Field { Row = 0, Column = 0 },
                new Field { Row = 1, Column = 1 },
                new Field { Row = 2, Column = 2 },
                new Field { Row = 4, Column = 4 },
                new Field { Row = 5, Column = 5 },
                new Field { Row = 6, Column = 6 },
                new Field { Row = 7, Column = 7 },
            };
            Assert.IsTrue(CheckFigureMoves(from, FigureColor.BLACK, correct));
        }

        [TestMethod]
        public void WhiteQueenBlockedJumpsEdge()
        {
            Field from = new Field { Row = 6, Column = 0 };
            board[from] = new Queen(FigureColor.WHITE);
            board[new Field { Row = 2, Column = 4 }] = new Pawn(FigureColor.BLACK);
            board[new Field { Row = 3, Column = 3 }] = new Pawn(FigureColor.BLACK);
            board[new Field { Row = 5, Column = 1 }] = new Pawn(FigureColor.BLACK);
            board[new Field { Row = 7, Column = 1 }] = new Pawn(FigureColor.BLACK);
            var correct = new Field[]
            {
                new Field { Row = 4, Column = 2 }
            };
            Assert.IsTrue(CheckFigureMoves(from, FigureColor.WHITE, correct));
        }

        [TestMethod]
        public void BlackQueenBlockedJumpsEdge()
        {
            Field from = new Field { Row = 1, Column = 7 };
            board[from] = new Queen(FigureColor.BLACK);
            board[new Field { Row = 0, Column = 6 }] = new Pawn(FigureColor.WHITE);
            board[new Field { Row = 2, Column = 6 }] = new Pawn(FigureColor.WHITE);
            board[new Field { Row = 4, Column = 4 }] = new Pawn(FigureColor.WHITE);
            board[new Field { Row = 5, Column = 3 }] = new Pawn(FigureColor.WHITE);
            var correct = new Field[]
            {
                new Field { Row = 3, Column = 5 }
            };
            Assert.IsTrue(CheckFigureMoves(from, FigureColor.BLACK, correct));
        }

        [TestMethod]
        public void WhiteQueenNotOnTurn()
        {
            Field from = new Field { Row = 3, Column = 3 };
            board[from] = new Queen(FigureColor.WHITE);
            Assert.IsTrue(CheckFigureMoves(from, FigureColor.BLACK, new Field[0]));
        }

        [TestMethod]
        public void BlackQueenNotOnTurn()
        {
            Field from = new Field { Row = 3, Column = 3 };
            board[from] = new Queen(FigureColor.BLACK);
            Assert.IsTrue(CheckFigureMoves(from, FigureColor.WHITE, new Field[0]));
        }

        [TestMethod]
        public void WhitePawnJumpOrMove()
        {
            Field from = new Field { Row = 2, Column = 2 };
            board[from] = new Pawn(FigureColor.WHITE);
            board[new Field { Row = 3, Column = 1 }] = new Pawn(FigureColor.BLACK);
            var correct = new Field[]
            {
                new Field { Row = 4, Column = 0 }
            };
            Assert.IsTrue(CheckFigureMoves(from, FigureColor.WHITE, correct));
        }

        [TestMethod]
        public void BlackPawnJumpOrMove()
        {
            Field from = new Field { Row = 3, Column = 1 };
            board[from] = new Pawn(FigureColor.BLACK);
            board[new Field { Row = 2, Column = 2 }] = new Pawn(FigureColor.WHITE);
            var correct = new Field[]
            {
                new Field { Row = 1, Column = 3 }
            };
            Assert.IsTrue(CheckFigureMoves(from, FigureColor.BLACK, correct));
        }

        [TestMethod]
        public void WhitePawnWhenQueenJumping()
        {
            Field from = new Field { Row = 2, Column = 2 };
            board[from] = new Pawn(FigureColor.WHITE);
            board[new Field { Row = 3, Column = 1 }] = new Pawn(FigureColor.BLACK);
            board[new Field { Row = 2, Column = 0 }] = new Queen(FigureColor.WHITE);
            Assert.IsTrue(CheckFigureMoves(from, FigureColor.WHITE, new Field[0]));
        }

        [TestMethod]
        public void BlackPawnWhenQueenJumping()
        {
            Field from = new Field { Row = 3, Column = 1 };
            board[from] = new Pawn(FigureColor.BLACK);
            board[new Field { Row = 2, Column = 2 }] = new Pawn(FigureColor.WHITE);
            board[new Field { Row = 0, Column = 0 }] = new Queen(FigureColor.BLACK);
            Assert.IsTrue(CheckFigureMoves(from, FigureColor.WHITE, new Field[0]));
        }

        [TestMethod]
        public void WhitePawnMultiJumping()
        {
            var whitePawnStart = new Field { Row = 2, Column = 2 };
            var secondWhitePawn = new Field { Row = 4, Column = 2 };
            board[whitePawnStart] = new Pawn(FigureColor.WHITE);
            board[secondWhitePawn] = new Pawn(FigureColor.WHITE);
            board[new Field { Row = 3, Column = 3 }] = new Pawn(FigureColor.BLACK);
            board[new Field { Row = 5, Column = 3 }] = new Pawn(FigureColor.BLACK);
            var firstJumpEnd = new Field { Row = 4, Column = 4 };
            board.Move(new Move(whitePawnStart, firstJumpEnd), FigureColor.WHITE);
            var correct = new Field[]
            {
                new Field { Row = 6, Column = 2 }
            };
            Assert.IsTrue(CheckFigureMoves(firstJumpEnd, FigureColor.WHITE, correct));
            Assert.IsTrue(CheckFigureMoves(secondWhitePawn, FigureColor.WHITE, new Field[0]));
        }

        private bool CheckFigureMoves(Field from, FigureColor colorOnTurn, params Field[] correct)
        {
            return correct.All(to => board.IsMoveValid(new Move(from, to), colorOnTurn))
                && Helpers.AllFields().Except(correct).All(to => !board.IsMoveValid(new Move(from, to), colorOnTurn));
        }
    }
}
 