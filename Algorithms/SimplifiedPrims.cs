using System;
using System.Collections.Generic;
using System.Linq;
using Mazes.factory;
using Mazes.interfaces;

namespace Mazes.Algorithms
{
    public class SimplifiedPrims : IAlgorithm
    {
        private List<Cell> active = new List<Cell>();
        private Random random = new Random();

        public void Carve(IMaze maze)
        {
            active.Add(maze.Grid[random.Next(maze.Grid.Count)]);

            while(active.Any())
            {
                var current = active[random.Next(active.Count)];
                var neighbours = maze.GetNeighbours(current).
                                Where(c => !c.Connected.Any()).
                                ToList();
                
                if(neighbours.Any())
                {
                    var next = neighbours[random.Next(neighbours.Count)];
                    maze.ConnectCells(current, next);
                    active.Add(next);
                }
                else
                {
                    active.Remove(current);
                }
            }
        }
    }
}