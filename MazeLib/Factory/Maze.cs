using System.Collections.Generic;
using MazeLib.Interfaces;
using System.Linq;
using System;

namespace MazeLib.Factory
{
    public class Maze : IMaze
    {
        public IList<Cell> Grid { get; private set; } = new List<Cell>();    

        public int Width { get; private set; }

        public int Height {get; private set;}

        public int Size { get{ return Width * Height;}}

        public const int DEFAULT_WIDTH = 2;

        public Maze(int width = DEFAULT_WIDTH, int height = DEFAULT_WIDTH)
        {
            if(width < DEFAULT_WIDTH || height < DEFAULT_WIDTH)
            {
                throw new ArgumentException($"width: {width} or height: {height} were < {DEFAULT_WIDTH}");
            }
            
            Width = width;
            Height = height;
            ResetGrid();
        }

        public void ResetGrid()
        {
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
            _ = cell ?? throw new ArgumentNullException(nameof(cell));

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
            _ = cells ?? throw new ArgumentNullException(nameof(cells));

            var neighbours = new HashSet<Cell>();
            
            foreach(Cell cell in cells)
            {
                GetNeighbours(cell).ToList().ForEach(c => neighbours.Add(c));
            }

            neighbours.ExceptWith(cells);
            return neighbours;
        }

        public HashSet<Cell> GetConnectedCells(IList<Cell> path)
        {
            _ = path ?? throw new ArgumentNullException(nameof(path));

            var connected = new HashSet<Cell>();

            foreach(Cell cell in path)
            {
                connected.UnionWith(cell.Connected);
            }

            connected.ExceptWith(path);
            return connected;
        }

        public void ConnectCells(Cell first, Cell second)
        {
            _ = first ?? throw new ArgumentNullException(nameof(first));
            _ = second ?? throw new ArgumentNullException(nameof(second));

            first.ConnectCell(second);
            second.ConnectCell(first);
        }

        public void ConnectPath(List<Cell> path)
        {
            _ = path ?? throw new ArgumentNullException(nameof(path));

            for(int index = 0; index < path.Count() - 1; ++index)
            {
                ConnectCells(path[index], path[index + 1]);
            }            
        }

        public override string ToString() => $"Width: {Width}, Height: {Height}";   

        public override bool Equals(object obj)
        {
            var returnValue = false;
            var aMaze = obj as Maze;

            if(!Object.ReferenceEquals(null, aMaze))
            {
                returnValue = Width == aMaze.Width
                    && Height == aMaze.Height
                    && Grid == aMaze.Grid;
            }

            return returnValue;
        }   

        public override int GetHashCode()
        {
            return new {Width, Height, Grid}.GetHashCode();
        }  
    }
}