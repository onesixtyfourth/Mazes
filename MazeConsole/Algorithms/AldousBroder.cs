using System;
using Mazes.Interfaces;

namespace Mazes.Algorithms
{
    public class AldousBroder : IAlgorithm
    {
        private Random random = new Random();

        public void Carve(IMaze maze)
        {
            var currentCell = maze.Grid[random.Next(maze.Grid.Count)];
            var unvisitedCells = maze.Grid.Count - 1;

            while(unvisitedCells > 0)
            {
                var neighbours = maze.GetNeighbours(currentCell);
                var nextCell = neighbours[random.Next(neighbours.Count)];

                if(nextCell.Connected.Count == 0)
                {
                    maze.ConnectCells(currentCell, nextCell);
                    --unvisitedCells;
                }

                currentCell = nextCell;
            }
        }
    }
}