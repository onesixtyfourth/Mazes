using System.Collections.Generic;
using MazeLib.Interfaces;
using System.Linq;
using System;

namespace MazeLib.Factory
{
    public class Maze : IMaze
    {
        public IList<Cell> Grid { get; set; }

        public int Size { get{ return Width * Height;}}

        public int Width { get; private set; }

        public int Height {get; private set;}

        public Maze(int width, int height)
        {
            Width = width;
            Height = height;
            ResetGrid();
        }

        public void ResetGrid()
        {
            Grid = new List<Cell>();
            for(int i = 0; i < Height; ++i)
            {
                for(int j = 0; j < Width; ++j)
                {
                    Grid.Add(new Cell(i, j));
                }
            }
        }

        public Cell GetCell(int row, int column) => Grid[(row * Width) + column];

        public IList<Cell> GetRow(int row) => Grid.Where(c => c.Row == row).ToList();

        public IList<Cell> GetColumn(int column) => Grid.Where(c => c.Column == column).ToList();

        public IList<Cell> GetNeighbours(Cell cell)
        {
            return Grid.Where((c) => 
            {
                var result = false;
                var dx = cell.Column - c.Column;
                var dy = cell.Row - c.Row;
                var distance = Math.Sqrt(dx * dx + dy * dy);

                if(distance == 1 && (c.Row == cell.Row || c.Column == cell.Column))
                {
                    result = true;
                }
                return result;
            }).ToList();
        }

        public HashSet<Cell> GetNeighbours(IList<Cell> cells)
        {
            var neighbours = new HashSet<Cell>();
            
            foreach(Cell cell in cells)
            {
                GetNeighbours(cell).ToList().ForEach(c => neighbours.Add(c));
            }
            return neighbours;
        }

        public HashSet<Cell> GetConnectedCells(IList<Cell> path)
        {
            var connected = new HashSet<Cell>();

            foreach(Cell cell in path)
            {
                connected.UnionWith(cell.Connected);
            }
            return connected;
        }

        public void ConnectCells(Cell first, Cell second)
        {
            first.ConnectCell(second);
            second.ConnectCell(first);
        }

        public void connectPath(List<Cell> path)
        {
            _ = path ?? throw new ArgumentNullException(nameof(path));

            for(int index = 0; index < path.Count() - 1; ++index)
            {
                ConnectCells(path[index], path[index + 1]);
            }            
        }

        public override string ToString() => $"Width: {Width}, Height: {Height}";        
    }
}