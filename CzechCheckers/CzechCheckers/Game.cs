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

        private Player onTurn;

        public Game(Player whitePlayer, Player blackPlayer, Board board)
        {
            this.whitePlayer = whitePlayer;
            this.blackPlayer = blackPlayer;
            this.board = board;
            onTurn = whitePlayer;
        }

        public void Start()
        {
            while (true)
            { 
                Console.Clear();
                Console.WriteLine(board);
                Console.WriteLine();
                Move();
            }
        }

        private void Move()
        {
            bool validMove = false;
            do
            {
                Console.WriteLine($"Hráč {onTurn.Name} ({onTurn.Color}) je na tahu.");
                Console.Write("fromCol = ");
                char.TryParse(Console.ReadLine(), out char fromCol);
                Console.Write("fromRow = ");
                int.TryParse(Console.ReadLine(), out int fromRow);
                Console.Write("toCol = ");
                char.TryParse(Console.ReadLine(), out char toCol);
                Console.Write("toRow = ");
                int.TryParse(Console.ReadLine(), out int toRow);
                validMove = board.Move(char.ToUpper(fromCol) - 'A', fromRow - 1, char.ToUpper(toCol) - 'A', toRow - 1);
                if (!validMove)
                {
                    Console.WriteLine("Neplatný tah!");
                }
            }
            while (!validMove);

            if (board.IsTurnOver())
            {
                onTurn = onTurn == whitePlayer ? blackPlayer : whitePlayer;
            }
        }
    }
}
