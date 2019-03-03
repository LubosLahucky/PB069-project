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
            RemoveObstacle(fromCol, fromRow, toCol, toRow);
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
            // TODO: Multihop
            return true;
        }

        public bool IsMoveValid(int fromCol, int fromRow, int toCol, int toRow)
        {
            if (fields[fromRow, fromCol] == null || fields[toRow, toCol] != null)
            {
                return false;
            }

            if (fromRow < MinRow || fromRow > MaxRow
                || fromCol < MinCol || fromRow > MaxCol
                || toRow < MinRow || toRow > MaxRow
                || toCol < MinCol || toCol > MaxCol)
            {
                return false;
            }

            int obstacles = CountObstaclesBetween(fromCol, fromRow, toCol, toRow);
            switch (obstacles)
            {
                case 0: return fields[fromRow, fromCol].CanMove(fromCol, fromRow, toCol, toRow);
                case 1:
                {
                    Tuple<int, int> firstObstacle = FirstObstacleBetween(fromCol, fromRow, toCol, toRow);
                    if (fields[fromRow, fromCol].Color == fields[firstObstacle.Item1, firstObstacle.Item2].Color)
                    {
                        return false;
                    }
                    return fields[fromRow, fromCol].CanJump(fromCol, fromRow, firstObstacle.Item2, firstObstacle.Item1, toCol, toRow);
                }
                default: return false;
            }
        }

        public bool FigureHasToJump(int col, int row)
        {
            // TODO
            if (fields[row, col] == null)
            {
                return false;
            }
            return false;
        }

        public bool PlayerHasToJump(Color color)
        {
            // TODO
            return false;
        }

        public bool PlayerHasAnyMoves(Color color)
        {
            // TODO
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

        private void RemoveObstacle(int fromCol, int fromRow, int toCol, int toRow)
        {
            Tuple<int, int> firstObstacle = FirstObstacleBetween(fromCol, fromRow, toCol, toRow);
            if (!firstObstacle.Equals(new Tuple<int, int>(-1, -1)))
            {
                fields[firstObstacle.Item1, firstObstacle.Item2] = null;
            }
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
