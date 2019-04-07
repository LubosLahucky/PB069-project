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

        private IFigure MultiJumping { get; set; }
        
        private readonly IFigure[,] fields;
        
        public Board(IFigure[,] fields)
        {
            this.fields = fields;
        }

        public IFigure this[Field field]
        {
            get => fields[field.Row, field.Column];
            set => fields[field.Row, field.Column] = value;
        }
        
        public bool Move(Move move, FigureColor colorOnTurn)
        {
            if (!IsMoveValid(move, colorOnTurn))
            {
                return false;
            }

            bool jump = RemoveFirstObstacleBetween(move.From, move.To);

            IFigure figure = this[move.From];
            this[move.From] = null;

            if ((move.To.Row == MinRow || move.To.Row == MaxRow) && figure is Pawn)
            {
                this[move.To] = new Queen(figure.Color);
                MultiJumping = null;
            }
            else
            {
                this[move.To] = figure;
                MultiJumping = (jump && FigureMustJump(move.To)) ? figure : null;
            }

            return true;
        }

        public bool IsMoveValid(Move move, FigureColor colorOnTurn)
        {
            if (!CheckMoveBounds(move))
            {
                return false;
            }

            IFigure figure = this[move.From];
            if (figure == null 
                || this[move.To] != null
                || figure.Color != colorOnTurn
                || (!IsTurnOver() && !IsFigureMultiJumping(figure)))
            {
                return false;
            }

            if (PlayerMustJump(colorOnTurn))
            {
                if (!(figure is Queen) && QueenMustJump(colorOnTurn))
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

        public bool PlayerMustJump(FigureColor color)
        {
            if (MultiJumping?.Color == color)
            {
                return true;
            }
            foreach (Field from in PlayerFields(color))
            { 
                IFigure figure = this[from];
                if (FigureMustJump(from))
                { 
                    return true;
                }
            }
            return false;
        }

        public bool QueenMustJump(FigureColor color)
        {
            if (MultiJumping?.Color == color && MultiJumping is Queen)
            {
                return true;
            }
            foreach (Field from in PlayerFields(color).Where(field => this[field] is Queen))
            {
                IFigure figure = this[from];
                if (FigureMustJump(from))
                {
                    return true;
                }
            }
            return false;
        }

        private bool CheckMove(Move move)
        {
            var figure = this[move.From];
            return IsTurnOver() 
                && figure.CanMove(move)
                && CountObstaclesBetween(move.From, move.To) == 0;
        }

        private bool CheckJump(Move move)
        {
            var figure = this[move.From];
            var firstObstacle = FindFirstObstacleBetween(move.From, move.To);
            return figure.CanJump(move)
                && !firstObstacle.Equals(Field.Invalid)
                && this[firstObstacle].Color != figure.Color
                && CountObstaclesBetween(move.From, move.To) == 1;
        }

        public bool FigureMustJump(Field from)
        {
            var figure = this[from];
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

        public bool HasPlayerAnyMoves(FigureColor color)
        {
            return PlayerFields(color).Any(from => HasFigureAnyMoves(from));
        }

        public bool HasFigureAnyMoves(Field from)
        {
            if (!CheckFieldBounds(from))
            {
                return false;
            }

            var figure = this[from];
            if (figure == null)
            {
                return false;
            }

            return figure.PossibleMoves(from)
                .Concat(figure.PossibleJumps(from))
                .Any(to => IsMoveValid(new Move(from, to), figure.Color));
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

        private bool IsFigureMultiJumping(IFigure figure)
        {
            return MultiJumping == figure;
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

        public IEnumerable<Field> PlayerFields(FigureColor color)
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
