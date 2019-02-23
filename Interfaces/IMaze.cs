using Mazes.Factory;
using System.Collections.Generic;

namespace Mazes.Interfaces
{
    public interface IMaze
    {
        int Width { get;  }
        int Height {get; }
        int Size{get;}    
        IList<Cell> Grid { get; set;}   
        void ResetGrid();
        Cell GetCell(int row, int column);
        IList<Cell> GetRow(int row);
        IList<Cell> GetColumn(int column);
        IList<Cell> GetNeighbours(Cell cell);
        HashSet<Cell> GetNeighbours(IList<Cell> cells);
        HashSet<Cell> GetConnectedCells(IList<Cell> path);
        void ConnectCells(Cell first, Cell second);
        void connectPath(List<Cell> path);
    }
}