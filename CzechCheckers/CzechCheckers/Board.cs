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

        private IFigure LastJumped { get; set; }
        
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

        public bool Move(Move move)
        {
            if (!IsMoveValid(move))
            {
                return false;
            }

            bool jump = RemoveFirstObstacleBetween(move.From, move.To);

            IFigure figure = this[move.From];
            this[move.From] = null;

            if ((move.To.Row == MinRow || move.To.Row == MaxRow) && figure is Pawn)
            {
                this[move.To] = new Queen(figure.Color);
                LastJumped = null;
            }
            else
            {
                this[move.To] = figure;
                LastJumped = (jump && FigureHasToJump(move.To.Column, move.To.Row)) ? this[move.To] : null;
            }
            return true;
        }

        public bool IsTurnOver()
        {
            return LastJumped == null;
        }

        public bool IsMoveValid(Move move)
        {
            if (!CheckMoveBounds(move))
            {
                return false;
            }

            IFigure figure = this[move.From];
            if (figure == null || this[move.To] != null)
            {
                return false;
            }

            if (!IsTurnOver() && !IsFigureMultiJumping(figure))
            {
                return false;
            }

            // TODO: Pawn has to jump, Queen has to jump

            int obstacles = CountObstaclesBetween(move.From, move.To);
            switch (obstacles)
            {
                case 0: return IsTurnOver() && figure.CanMove(move);
                case 1: return figure.Color != this[FindFirstObstacleBetween(move.From, move.To)].Color && figure.CanJump(move);
                
            }
            return false;
        }

        public bool PlayerHasToJump(FigureColor color)
        {
            if (LastJumped?.Color == color)
            {
                return true;
            }

            for (int col = 0; col < MaxCol; ++col)
            {
                for (int row = 0; row < MaxRow; ++row)
                {
                    IFigure figure = fields[row, col];
                    if (figure != null && figure.Color == color && FigureHasToJump(col, row))
                        return true;
                }
            }
            return false;
        }

        public bool FigureHasToJump(int fromCol, int fromRow)
        {
            for (int toCol = 0; toCol < MaxCol; ++toCol)
            {
                for (int toRow = 0; toRow < MaxRow; ++toRow)
                {
                    Move move = new Move(fromRow, fromCol, toRow, toCol);
                    if (fields[fromRow, fromCol].CanJump(move) 
                        && IsMoveValid(move))
                        return true;
                }
            }
            return false;
        }

        public bool HasPlayerAnyMoves(FigureColor color)
        {
            foreach (Field field in PlayerFields(color))
            {
                if (HasFigureAnyMoves(field))
                {
                    return true;
                }
            }
            return false;
        }

        public bool HasFigureAnyMoves(Field from)
        {
            var figure = this[from];
            foreach (Field to in figure.PossibleMoves(from))
            {
                var move = new Move(from, to);
                if (IsMoveValid(move))
                {
                    return true;
                }
            }
            return false;
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

        private int CountObstaclesBetween(Field from, Field to)
        {
            int stepCol = from.Column < to.Column ? 1 : -1;
            int stepRow = from.Row < to.Row ? 1 : -1;
            int startCol = from.Column + stepCol;
            int startRow = from.Row + stepRow;

            int obstacles = 0;

            for (int col = startCol, row = startRow;
                col != to.Column && row != to.Row;
                col += stepCol, row += stepRow)
            { 
                if (fields[row, col] != null)
                {
                    obstacles++;
                }
            }
            return obstacles;
        }

        private Field FindFirstObstacleBetween(Field from, Field to)
        {
            int stepCol = from.Column < to.Column ? 1 : -1;
            int stepRow = from.Row < to.Row ? 1 : -1;

            for (int col = from.Column + stepCol, row = from.Row + stepRow;
                col != to.Column && row != to.Row;
                col += stepCol, row += stepRow)
            {
                if (fields[row, col] != null)
                {
                    return new Field { Column = col, Row = row };
                }
            }
            return Field.Invalid;
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
            return LastJumped == figure;
        }

        public static bool CheckFieldBounds(Field field)
        {
            return field.Row >= MinRow && field.Row <= MaxRow
                && field.Column >= MinCol && field.Column <= MaxCol;
        }

        public static bool CheckMoveBounds(Move move)
        {
            return CheckFieldBounds(move.From)
                && CheckFieldBounds(move.To);
        }

        private void AddLetters(ref string output)
        {
            output += "  ";
            for (char c = 'A'; c <= MaxCol + 'A'; ++c)
            {
                output += "  " + c;
            }
        }

        public IEnumerable<Field> AllFields()
        {
            for (int row = 0; row < MaxRow; ++row)
            {
                for (int col = 0; col < MaxCol; ++col)
                {
                    yield return new Field { Row = row, Column = col }; 
                }
            }
        }

        public IEnumerable<Field> NonEmptyFields()
        {
            return AllFields().Where(field => this[field] != null);
        }

        public IEnumerable<Field> PlayerFields(FigureColor color)
        {
            return AllFields().Where(field => this[field]?.Color == color);
        }
    }
}
