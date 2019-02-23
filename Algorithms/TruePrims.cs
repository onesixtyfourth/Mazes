using System;
using System.Collections.Generic;
using System.Linq;
using Mazes.factory;
using Mazes.interfaces;

namespace Mazes.Algorithms
{
    public class TruePrims : IAlgorithm
    {
        private List<Cell> active = new List<Cell>();
        private Random random = new Random();

        public void Carve(IMaze maze)
        {
            var costs = CalculateCosts(maze.Grid);
            active.Add(maze.Grid[random.Next(maze.Grid.Count)]);

            while(active.Any())
            {
                var current = LowestCost(active, costs);
                var neighbours = maze.GetNeighbours(current).
                                Where(c => !c.Connected.Any()).
                                ToList();
                
                if(neighbours.Any())
                {
                    var next = LowestCost(neighbours, costs);
                    maze.ConnectCells(current, next);
                    active.Add(next);
                }
                else
                {
                    active.Remove(current);
                }
            }
        }

        private Cell LowestCost(List<Cell> potentials, Dictionary<Cell, int> costs)
        {
            var cost = Int32.MaxValue;
            Cell current = null;

            foreach(var cell in potentials)
            {
                if(costs[cell] < cost)
                {
                    cost = costs[cell];
                    current = cell;
                }
            }

            return current;
        }

        private Dictionary<Cell, int> CalculateCosts(IList<Cell> grid)
        {
            var costs = new Dictionary<Cell, int>();

            foreach(var cell in grid)
            {
                costs.Add(cell, random.Next(100));
            }

            return costs;
        }
    }
}