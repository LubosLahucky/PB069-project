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
            IFigure figure = fields[fromCol, fromRow];
            fields[fromCol, fromRow] = null;
            Pawn pawn = figure as Pawn;
            if (pawn != null && toRow == MinRow || toRow == MaxRow)
            {
                fields[toCol, toRow] = new Queen(pawn.color);
            }
            else
            { 
                fields[toCol, toRow] = figure;
            }
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

            return fields[fromRow, fromCol].CanMove(fromCol, fromRow, toCol, toRow); 
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
