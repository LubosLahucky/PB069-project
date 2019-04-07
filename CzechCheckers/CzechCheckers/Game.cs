using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CzechCheckers
{
    class Game
    {
        private readonly Player whitePlayer;
        private readonly Player blackPlayer;
        private readonly Board board;

        private Player playerOnTurn;

        public Game(Player whitePlayer, Player blackPlayer, Board board)
        {
            this.whitePlayer = whitePlayer;
            this.blackPlayer = blackPlayer;
            this.board = board;
            playerOnTurn = whitePlayer;
        }

        public void Start()
        {
            while (true)
            {
                Console.Clear();
                Console.WriteLine(board);
                Console.WriteLine();

                if (CheckWin())
                {
                    break;
                }

                Move();
            }
        }

        private void Move()
        {
            Move move;
            if (playerOnTurn is ComputerPlayer computer)
            {
                move = computer.GenerateMove(board);
            }
            else
            {
                move = LoadMove();
            }

            board.Move(move, playerOnTurn.Color);

            if (board.IsTurnOver())
            {
                playerOnTurn = playerOnTurn == whitePlayer ? blackPlayer : whitePlayer;
            }
        }

        private bool CheckWin()
        {
            if (!board.HasPlayerAnyMoves(playerOnTurn.Color))
            {
                Console.WriteLine($"Hráč {playerOnTurn.Name} prohrál.");
                return true;
            }
            return false;
        }

        private Move LoadMove()
        {
            Move move;
            bool validMove = false;
            do
            {
                Console.WriteLine($"Hráč {playerOnTurn.Name} ({playerOnTurn.Color}) je na tahu.");
                Console.Write("fromCol = ");
                char.TryParse(Console.ReadLine(), out char fromCol);
                Console.Write("fromRow = ");
                int.TryParse(Console.ReadLine(), out int fromRow);
                Console.Write("toCol = ");
                char.TryParse(Console.ReadLine(), out char toCol);
                Console.Write("toRow = ");
                int.TryParse(Console.ReadLine(), out int toRow);

                move = new Move
                {
                    From = new Field
                    {
                        Column = char.ToUpper(fromCol) - 'A',
                        Row = fromRow - 1,
                    },
                    To = new Field
                    {
                        Column = char.ToUpper(toCol) - 'A',
                        Row = toRow - 1
                    }
                };

                validMove = board.IsMoveValid(move, playerOnTurn.Color);
                if (!validMove)
                {
                    Console.WriteLine("Neplatný tah!");
                }
            }
            while (!validMove);

            return move;
        }
    }
}
