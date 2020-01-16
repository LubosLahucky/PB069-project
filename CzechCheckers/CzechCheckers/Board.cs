using System;
using System.Collections.Generic;
using System.Linq;

namespace CzechCheckers
{
    public class Board
    {
        public const int MinRow = 0;
        public const int MaxRow = 7;
        public const int MinCol = 0;
        public const int MaxCol = 7;

        public IPiece MultiJumping { get; set; }

        public IPiece[,] fields;

        public Board() { }

        public Board(IPiece[,] fields)
        {
            this.fields = fields;
        }

        public IPiece this[Field field]
        {
            get => fields[field.Row, field.Column];
            set => fields[field.Row, field.Column] = value;
        }

        public Board Move(Move move)
        {
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
                MultiJumping = (jump && CanPieceJump(move.To)) ? piece : null;
            }

            return this;
        }

        public bool TryMove(Move move, Color colorOnTurn)
        {
            if (!IsMoveValid(move, colorOnTurn))
            {
                return false;
            }
            Move(move);
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
                if (!CheckJump(move))
                {
                    return false;
                }
                if (CanPieceMultijump(move.From))
                {
                    var boardCopy = Copy();
                    boardCopy.Move(move);
                    if (!boardCopy.CanPieceJump(move.To))
                    {
                        return false;
                    }
                }
                return true;
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
                if (CanPieceJump(from))
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

            /*
            foreach (Field from in PlayerFields(color).Where(field => this[field] is Queen))
            {
                IPiece piece = this[from];
                if (CanPieceJump(from))
                {
                    return true;
                }
            }
            
            return false;
            */

            return PlayerFields(color).Where(field => this[field] is Queen).Any(from => CanPieceJump(from));
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

        public IEnumerable<Field> PieceJumps(Field from)
        {
            var piece = this[from];

            foreach (Field to in piece.PossibleJumps(from).Where(field => this[field] == null))
            {
                var move = new Move(from, to);
                if (CheckJump(move))
                {
                    yield return to;
                }
            }
        }

        public bool CanPieceJump(Field from)
        {
            return PieceJumps(from).Any();
        }

        public bool CanPieceMultijump(Field from)
        {
            foreach (Field to in PieceJumps(from))
            {
                var boardCopy = Copy();
                var move = new Move(from, to);
                boardCopy.Move(move);
                if (boardCopy.PieceJumps(to).Any())
                {
                    return true;
                }
            }
            return false;
        }

        public bool HasPlayerAnyMoves(Color color)
        {
            return PlayerFields(color).Any(from => HasPieceAnyMoves(from, color));
        }

        public IEnumerable<Move> PlayerMoves(Color color)
        {
            foreach (var from in PlayerFields(color))
            {
                foreach (var to in PieceMoves(from, color))
                {
                    yield return new Move(from, to);
                }
            }
        }

        public bool HasPieceAnyMoves(Field from, Color colorOnTurn)
        {
            return PieceMoves(from, colorOnTurn).Any();
        }

        public IEnumerable<Field> PieceMoves(Field from, Color colorOnTurn)
        {
            if (!CheckFieldBounds(from))
            {
                yield break;
            }

            var piece = this[from];
            if (piece == null)
            {
                yield break;
            }

            var possibleMovesAndJumps = piece.PossibleMoves(from)
                .Concat(piece.PossibleJumps(from))
                .Where(to => IsMoveValid(new Move(from, to), colorOnTurn));

            foreach (Field field in possibleMovesAndJumps)
                yield return field;
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

        public uint GetMaxJumpLength(Field field)
        {
            var piece = this[field];
            if (piece == null)
                return 0;

            uint maxLength = 0;
            var possibleFields = PieceMoves(field, piece.Color);

            foreach (var possibleField in possibleFields)
            {
                Board boardCopy = Copy();
                boardCopy.Move(new Move(field, possibleField));
                if (boardCopy.IsTurnOver())
                {
                    maxLength = Math.Max(maxLength, 1);
                }
                else
                {
                    maxLength = Math.Max(maxLength, 1 + boardCopy.GetMaxJumpLength(possibleField));
                }
            }

            return maxLength;
        }

        public Board Copy()
        {
            IPiece[,] fieldsCopy = new IPiece[fields.GetUpperBound(0) + 1, fields.GetUpperBound(1) + 1];
            Array.Copy(fields, fieldsCopy, fields.Length);
            return new Board(fieldsCopy);
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

        public IEnumerable<Field> MovableFields(Color color)
        {
            return PlayerFields(color).Where(from => HasPieceAnyMoves(from, color));
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