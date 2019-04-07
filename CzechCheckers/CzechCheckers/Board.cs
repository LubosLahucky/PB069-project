using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CzechCheckers
{
    class Board
    {
        public const int MinRow = 0;
        public const int MaxRow = 7;
        public const int MinCol = 0;
        public const int MaxCol = 7;

        private IPiece MultiJumping { get; set; }
        
        private readonly IPiece[,] fields;
        
        public Board(IPiece[,] fields)
        {
            this.fields = fields;
        }

        public IPiece this[Field field]
        {
            get => fields[field.Row, field.Column];
            set => fields[field.Row, field.Column] = value;
        }
        
        public bool Move(Move move, Color colorOnTurn)
        {
            if (!IsMoveValid(move, colorOnTurn))
            {
                return false;
            }

            bool jump = RemoveFirstObstacleBetween(move.From, move.To);

            IPiece piece = this[move.From];
            this[move.From] = null;

            if ((move.To.Row == MinRow || move.To.Row == MaxRow) && piece is Pawn)
            {
                this[move.To] = new Queen(piece.Color);
                MultiJumping = null;
            }
            else
            {
                this[move.To] = piece;
                MultiJumping = (jump && PieceMustJump(move.To)) ? piece : null;
            }

            return true;
        }

        public bool IsMoveValid(Move move, Color colorOnTurn)
        {
            if (!CheckMoveBounds(move))
            {
                return false;
            }

            IPiece piece = this[move.From];
            if (piece == null 
                || this[move.To] != null
                || piece.Color != colorOnTurn
                || (!IsTurnOver() && !IsPieceMultiJumping(piece)))
            {
                return false;
            }

            if (PlayerMustJump(colorOnTurn))
            {
                if (!(piece is Queen) && QueenMustJump(colorOnTurn))
                {
                    return false;
                }
                return CheckJump(move);
            }

            return CheckMove(move);
        }
        
        public bool IsTurnOver()
        {
            return MultiJumping == null;
        }

        public bool PlayerMustJump(Color color)
        {
            if (MultiJumping?.Color == color)
            {
                return true;
            }
            foreach (Field from in PlayerFields(color))
            { 
                IPiece piece = this[from];
                if (PieceMustJump(from))
                { 
                    return true;
                }
            }
            return false;
        }

        public bool QueenMustJump(Color color)
        {
            if (MultiJumping?.Color == color && MultiJumping is Queen)
            {
                return true;
            }
            foreach (Field from in PlayerFields(color).Where(field => this[field] is Queen))
            {
                IPiece piece = this[from];
                if (PieceMustJump(from))
                {
                    return true;
                }
            }
            return false;
        }

        private bool CheckMove(Move move)
        {
            var piece = this[move.From];
            return IsTurnOver() 
                && piece.CanMove(move)
                && CountObstaclesBetween(move.From, move.To) == 0;
        }

        private bool CheckJump(Move move)
        {
            var piece = this[move.From];
            var firstObstacle = FindFirstObstacleBetween(move.From, move.To);
            return piece.CanJump(move)
                && !firstObstacle.Equals(Field.Invalid)
                && this[firstObstacle].Color != piece.Color
                && CountObstaclesBetween(move.From, move.To) == 1;
        }

        public bool PieceMustJump(Field from)
        {
            var piece = this[from];
            foreach (Field to in EmptyFields())
            {
                var move = new Move(from, to);
                if (CheckJump(move))
                {
                    return true;
                }
            }
            return false;
        }

        public bool HasPlayerAnyMoves(Color color)
        {
            return PlayerFields(color).Any(from => HasPieceAnyMoves(from));
        }

        public bool HasPieceAnyMoves(Field from)
        {
            if (!CheckFieldBounds(from))
            {
                return false;
            }

            var piece = this[from];
            if (piece == null)
            {
                return false;
            }

            return piece.PossibleMoves(from)
                .Concat(piece.PossibleJumps(from))
                .Any(to => IsMoveValid(new Move(from, to), piece.Color));
        }

        private int CountObstaclesBetween(Field from, Field to)
        {
            return NonEmptyFieldsBetween(from, to).Count(); 
        }

        private Field FindFirstObstacleBetween(Field from, Field to)
        {
            var nonEmpty = NonEmptyFieldsBetween(from, to);
            return nonEmpty.Any() ? nonEmpty.First() : Field.Invalid;
        }

        private IEnumerable<Field> NonEmptyFieldsBetween(Field from, Field to)
        {
            return Helpers.DiagonalPath(from, to).Where(field => this[field] != null);
        }
        
        private bool RemoveFirstObstacleBetween(Field from, Field to)
        {
            Field firstObstacle = FindFirstObstacleBetween(from, to);
            if (firstObstacle.IsValid())
            {
                this[firstObstacle] = null;
                return true;
            }
            return false;
        }

        private bool IsPieceMultiJumping(IPiece piece)
        {
            return MultiJumping == piece;
        }

        public static bool CheckMoveBounds(Move move)
        {
            return CheckFieldBounds(move.From)
                && CheckFieldBounds(move.To);
        }

        public static bool CheckFieldBounds(Field field)
        {
            return field.Row >= MinRow && field.Row <= MaxRow
                && field.Column >= MinCol && field.Column <= MaxCol;
        }

        public IEnumerable<Field> NonEmptyFields()
        {
            return Helpers.AllFields().Except(EmptyFields());
        }

        public IEnumerable<Field> EmptyFields()
        {
            return Helpers.AllFields().Where(field => this[field] == null);
        }

        public IEnumerable<Field> PlayerFields(Color color)
        {
            return Helpers.AllFields().Where(field => this[field]?.Color == color);
        }

        public override string ToString()
        {
            string output = "";
            AddLetters(ref output);
            output += "\n  =========================  \n";

            for (int row = MaxRow; row >= MinRow; --row)
            {
                output += row + 1 + " ";
                for (int col = MinCol; col <= MaxCol; ++col)
                {
                    output += "|";
                    output += fields[row, col] == null ? "  " : $"{fields[row, col]}";
                }
                output += "| " + (row + 1);
                output += "\n  =========================  \n";
            }

            AddLetters(ref output);
            return output;
        }

        private void AddLetters(ref string output)
        {
            output += "  ";
            for (char c = 'A'; c <= MaxCol + 'A'; ++c)
            {
                output += "  " + c;
            }
        }

    }
}
