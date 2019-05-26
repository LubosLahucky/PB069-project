namespace CzechCheckers
{
    public struct Move
    {
        public Field From { get; set; }
        public Field To { get; set; }

        public Move(Field from, Field to)
        {
            From = from;
            To = to;
        }

        public Move(int fromRow, int fromCol, int toRow, int toCol)
            : this(new Field { Row = fromRow, Column = fromCol },
                  new Field { Row = toRow, Column = toCol })
        { }
    }
}
