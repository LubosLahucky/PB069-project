using System.Collections.Generic;
using System.Linq;

namespace CzechCheckers
{
    public class ComputerPlayer : Player
    {
        public Color OpponentColor => Color == Color.White ? Color.Black : Color.White;

        public Move GenerateMove(Board board)
        {
            (Move, double) bestMove = (default(Move), 0.0);
            var moves = board.PlayerMoves(Color);
            foreach (var move in moves)
            {
                var boardCopy = board.Copy();
                boardCopy.Move(move);
                while (!boardCopy.IsTurnOver())
                {
                    boardCopy.Move(GenerateMove(boardCopy));
                }

                var opponentMoves = boardCopy.PlayerMoves(OpponentColor);
                if (!opponentMoves.Any())
                    return move;

                double ratio = opponentMoves
                    .Min(opponentMove => CalculateScoreRatio(boardCopy
                        .Copy()
                        .Move(opponentMove)));

                if (ratio >= bestMove.Item2)
                {
                    bestMove = (move, ratio);
                }
            }

            return bestMove.Item1;
        }

        public double CalculateScoreRatio(Board board)
        {
            return PlayerScore(board, Color) / (double)PlayerScore(board, OpponentColor);
        }

        public int PlayerScore(Board board, Color color)
        {
            if (!board.HasPlayerAnyMoves(color))
                return 0;

            if (!board.HasPlayerAnyMoves(color == Color.White ? Color.Black : Color.White))
                return 100;

            return board.PlayerFields(color).Sum(field => board[field] is Queen ? 3 : 1);
        }
    }
}
