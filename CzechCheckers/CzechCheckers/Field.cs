﻿namespace CzechCheckers
{
    public struct Field
    {
        public static readonly Field Invalid = new Field { Column = -1, Row = -1 };

        public int Column { get; set; }
        public int Row { get; set; }

        public bool IsValid() => !Equals(Invalid);

        public bool IsBlack() => ((Row + Column) & 1) != 1;

        public override string ToString() => $"[{(char)('A' + Column)}:{Row + 1}]";
    }
}
