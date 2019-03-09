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

        public bool Move(int fromCol, int fromRow, int toCol, int toRow)
        {
            if (!IsMoveValid(fromCol, fromRow, toCol, toRow))
            {
                return false;
            }
            bool jump = RemoveObstacle(fromCol, fromRow, toCol, toRow);
            IFigure figure = fields[fromRow, fromCol];
            fields[fromRow, fromCol] = null;
            if ((toRow == MinRow || toRow == MaxRow) && figure is Pawn)
            {
                fields[toRow, toCol] = new Queen(figure.Color);
            }
            else
            {
                fields[toRow, toCol] = figure;
            }

            LastJumped = (jump && FigureHasToJump(toCol, toRow)) ? fields[toCol, toRow] : null;
            return true;
        }

        public bool IsTurnOver()
        {
            return LastJumped == null;
        }

        public bool IsMoveValid(int fromCol, int fromRow, int toCol, int toRow)
        {
            if (fromRow < MinRow || fromRow > MaxRow
                || fromCol < MinCol || fromCol > MaxCol
                || toRow < MinRow || toRow > MaxRow
                || toCol < MinCol || toCol > MaxCol)
            {
                return false;
            }

            if (fields[fromRow, fromCol] == null || fields[toRow, toCol] != null)
            {
                return false;
            }

            if (!IsTurnOver() && !IsFigureMultiJumping(fromCol, fromRow))
            {
                return false;
            }

            // TODO: Pawn has to jump, Queen has to jump

            int obstacles = CountObstaclesBetween(fromCol, fromRow, toCol, toRow);
            switch (obstacles)
            {
                case 0:
                {
                    if (!IsTurnOver())
                    {
                        return false;
                    }
                    return fields[fromRow, fromCol].CanMove(fromCol, fromRow, toCol, toRow);
                }
                case 1:
                {
                    Tuple<int, int> firstObstacle = FirstObstacleBetween(fromCol, fromRow, toCol, toRow);
                    if (fields[fromRow, fromCol].Color == fields[firstObstacle.Item1, firstObstacle.Item2].Color)
                    {
                        return false;
                    }
                    return fields[fromRow, fromCol].CanJump(fromCol, fromRow, toCol, toRow);
                }
                default: return false;
            }
        }

        public bool PlayerHasToJump(Color color)
        {
            if (LastJumped?.Color == color)
            {
                return true;
            }

            for (int col = 0; col < MaxCol; ++col)
            {
                for (int row = 0; row < MaxRow; ++row)
                {
                    IFigure figure = fields[col, row];
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
                    if (fields[fromCol, fromRow].CanJump(fromCol, fromRow, toCol, toRow) 
                        && IsMoveValid(fromCol, fromRow, toCol, toRow))
                        return true;
                }
            }
            return false;
        }

        public bool PlayerHasAnyMoves(Color color)
        {
            for (int col = 0; col < MaxCol; ++col)
            {
                for (int row = 0; row < MaxRow; ++row)
                {
                    IFigure figure = fields[col, row];
                    if (figure != null && figure.Color == color && FigureHasAnyMoves(col, row))
                        return true;
                }
            }
            return false;
        }

        public bool FigureHasAnyMoves(int fromCol, int fromRow)
        {
            for (int toCol = 0; toCol < MaxCol; ++toCol)
            {
                for (int toRow = 0; toRow < MaxRow; ++toRow)
                {
                    if (IsMoveValid(fromCol, fromRow, toCol, toRow))
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

        private int CountObstaclesBetween(int fromCol, int fromRow, int toCol, int toRow)
        {
            int stepCol = fromCol < toCol ? 1 : -1;
            int stepRow = fromRow < toRow ? 1 : -1;
            int startCol = fromCol + stepCol;
            int startRow = fromRow + stepRow;

            int obstacles = 0;

            for (int col = startCol, row = startRow;
                col != toCol && row != toRow;
                col += stepCol, row += stepRow)
            { 
                if (fields[row, col] != null)
                {
                    obstacles++;
                }
            }
            return obstacles;
        }

        private Tuple<int, int> FirstObstacleBetween(int fromCol, int fromRow, int toCol, int toRow)
        {
            int stepCol = fromCol < toCol ? 1 : -1;
            int stepRow = fromRow < toRow ? 1 : -1;
            int startCol = fromCol + stepCol;
            int startRow = fromRow + stepRow;

            for (int col = startCol, row = startRow;
                col != toCol && row != toRow;
                col += stepCol, row += stepRow)
            {
                if (fields[row, col] != null)
                {
                    return new Tuple<int, int>(row, col);
                }
            }
            return new Tuple<int, int>(-1, -1);
        }

        private bool RemoveObstacle(int fromCol, int fromRow, int toCol, int toRow)
        {
            Tuple<int, int> firstObstacle = FirstObstacleBetween(fromCol, fromRow, toCol, toRow);
            if (!firstObstacle.Equals(new Tuple<int, int>(-1, -1)))
            {
                fields[firstObstacle.Item1, firstObstacle.Item2] = null;
                return true;
            }
            return false;
        }

        private bool IsFigureMultiJumping(int fromCol, int fromRow)
        {
            return fields[fromCol, fromRow] == LastJumped;
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
