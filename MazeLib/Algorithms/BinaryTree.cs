using MazeLib.Factory;
using MazeLib.Interfaces;
using System.Linq;
using System;

namespace MazeLib.Algorithms
{
    public class BinaryTree : IAlgorithm
    {
        private Random random = new Random();

        public void Carve(IMaze maze)
        {
            _ = maze ?? throw new ArgumentNullException(nameof(maze));
            
            foreach (Cell cell in maze.Grid)
            {
                var neighbours = maze.GetNeighbours(cell)
                    .Where(c => c.Column == cell.Column + 1 || c.Row == cell.Row - 1)
                    .ToList();

                if(neighbours.Any())
                {
                    maze.ConnectCells(cell, neighbours[random.Next(neighbours.Count())]);
                }          
            }
        }
    }
}