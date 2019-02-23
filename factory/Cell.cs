using System.Collections.Generic;

namespace Mazes.factory
{//TODO override equals and hash for correct operation in a hashset.
    public class Cell
    {
        public int Row { get; set; }
        public int Column { get; set; }

        public HashSet<Cell> Connected {get; set;} = new HashSet<Cell>();

        public Cell(int row, int column)
        {
            Row = row;
            Column = column;
        }

        public void ConnectCell(Cell cell) => Connected.Add(cell);

        public override string ToString() => $"Row: {Row}, Column: {Column}";
    }
}