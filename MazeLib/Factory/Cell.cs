using System;
using System.Collections.Generic;

namespace MazeLib.Factory
{
    public class Cell
    {
        public int Row { get; private set; }
        public int Column { get; private set; }

        public HashSet<Cell> Connected {get; private set;} = new HashSet<Cell>();


        public Cell(int row, int column)
        {
            if(row < 0 || column < 0)
            {
                throw new ArgumentException($"Row: {row} or column: {column} is less than 0 (zero)");
            }
            
            Row = row;
            Column = column;
        }

        public void ConnectCell(Cell cell) => Connected.Add(cell);

        public override string ToString() => $"[{Row}:{Column}]";

        public override bool Equals(object value)
        {
            var returnValue = false;
            var aCell = value as Cell;

            if(!Object.ReferenceEquals(null, aCell))
            {
                returnValue = Row == aCell.Row
                    && Column == aCell.Column
                    && Connected == aCell.Connected;
            }
            return returnValue;
        }

        public override int GetHashCode()
        {
            return new {Row, Column, Connected}.GetHashCode();
        }
    }
}