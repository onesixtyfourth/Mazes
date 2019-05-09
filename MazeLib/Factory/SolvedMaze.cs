using System;
using System.Collections.Generic;
using MazeLib.Interfaces;

namespace MazeLib.Factory
{
    public class SolvedMaze : IMaze
    {
        private IMaze maze;

        public int Width => maze.Width;

        public int Height => maze.Height;

        public int Size => maze.Size;

        public IList<Cell> Grid => maze.Grid;

        public ISolveMazes Solver { get; private set; }

        public IAlgorithm Algorithm { get; private set; }

        public SolvedMaze(Maze maze, IAlgorithm algorithm, ISolveMazes solver)
        {
            _ = maze ?? throw new ArgumentNullException(nameof(maze));
            _ = algorithm ?? throw new ArgumentNullException(nameof(algorithm));
            _ = solver ?? throw new ArgumentNullException(nameof(solver));

            this.maze = maze;
            Algorithm = algorithm;
            Solver = solver;

            Algorithm.Carve(maze);
            Solver.Solve(maze, Grid[0]);
        }

        public void ConnectCells(Cell first, Cell second) => maze.ConnectCells(first, second);

        public void ConnectPath(List<Cell> path) => maze.ConnectPath(path);
        
        public Cell GetCell(int row, int column) => maze.GetCell(row, column);
        
        public IList<Cell> GetColumn(int column) => maze.GetColumn(column);
        
        public HashSet<Cell> GetConnectedCells(IList<Cell> path) => maze.GetConnectedCells(path);
        
        public IList<Cell> GetNeighbours(Cell cell) => maze.GetNeighbours(cell);
        
        public HashSet<Cell> GetNeighbours(IList<Cell> cells) => maze.GetNeighbours(cells);
        
        public IList<Cell> GetRow(int row) => maze.GetRow(row);        

        public void ResetGrid() => maze.ResetGrid();

        public override string ToString() => maze.ToString();

        //TODO needs more thought
        public override bool Equals(object obj) => maze.Equals(obj);
        public override int GetHashCode() => maze.GetHashCode();
    }
}