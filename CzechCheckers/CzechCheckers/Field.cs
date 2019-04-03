using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CzechCheckers
{
    struct Field
    {
        public static readonly Field Invalid = new Field { Column = -1, Row = -1 };

        public int Column { get; set; }
        public int Row { get; set; }

        public bool IsValid() => !Equals(Invalid);

        public override string ToString() => $"[{(char)('A' + Column)}:{Row}]";
    }
}
