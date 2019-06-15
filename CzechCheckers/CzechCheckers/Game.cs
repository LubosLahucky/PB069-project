using Newtonsoft.Json;
using System;

namespace CzechCheckers
{
    public class Game
    {
        public Player WhitePlayer { get; set; }
        public Player BlackPlayer { get; set; }
        public Player PlayerOnMove { get; set; }

        [JsonIgnore]
        public Player PlayerNotOnMove => PlayerOnMove.Equals(WhitePlayer) ? BlackPlayer : WhitePlayer;
    
        public Board Board { get; set; }

        public Game() { }

        public Game(Player whitePlayer, Player blackPlayer, Board board)
        {
            WhitePlayer = whitePlayer;
            BlackPlayer = blackPlayer;
            PlayerOnMove = whitePlayer;
            Board = board;
        }

        public bool TryMove(Field from, Field to)
        {
            bool success = Board.TryMove(new Move(from, to), PlayerOnMove.Color);
            if (success && Board.IsTurnOver())
            {
                NextTurn();
            }
            return success;
        }

        public void NextTurn()
        {
            PlayerOnMove = PlayerNotOnMove;

            if (IsGameOver())
            {
                EndGame(PlayerNotOnMove);
                return;
            }

            ComputerTurn();
        }

        private bool IsGameOver()
        {
            return !Board.HasPlayerAnyMoves(PlayerOnMove.Color);
        }

        public void ComputerTurn()
        {
            if (!(PlayerOnMove is ComputerPlayer computerPlayer))
                return;

            do
            {
                var move = computerPlayer.GenerateMove(Board);
                Board.TryMove(move, PlayerOnMove.Color);
            }
            while (!Board.IsTurnOver());

            NextTurn();
        }

        public void Start()
        {
            ComputerTurn();
        }

        public void EndGame(Player winner)
        {
            OnGameEnded(new GameEndedEventArgs
            {
                Winner = winner
            });
        }

        protected virtual void OnGameEnded(GameEndedEventArgs e)
        {
            GameEnded?.Invoke(this, e);
        }

        public event EventHandler<GameEndedEventArgs> GameEnded;
    }
}
