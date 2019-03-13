using System;
using System.Collections.Generic;
using System.Linq;
using MazeLib.Factory;
using MazeLib.Interfaces;

namespace MazeLib.Algorithms
{
    public class GrowingTree : IAlgorithm
    {//First attempt at passing selection methods to the algorithm!
    // private modifier as causing issues with reflection by adding a class
    // that can't be instantiated.
        private delegate Cell SelectCell(List<Cell> cells);
        private SelectCell Selector;
        private List<Cell> active = new List<Cell>();
        private Random random = new Random();

        public GrowingTree()
        {
            Selector = RandomSelection;
        }

        public void Carve(IMaze maze)
        {
            _ = maze ?? throw new ArgumentNullException(nameof(maze));
            
            active.Add(maze.Grid[random.Next(maze.Grid.Count)]);

            while(active.Any())
            {
                var current = active[random.Next(active.Count)];
                var neighbours = maze.GetNeighbours(current).
                                Where(c => !c.Connected.Any()).
                                ToList();
                
                if(neighbours.Any())
                {
                    var next = Selector(neighbours);
                    maze.ConnectCells(current, next);
                    active.Add(next);
                }
                else
                {
                    active.Remove(current);
                }
            }
        }

        private Cell RandomSelection(List<Cell> cells)
        {
            return cells[random.Next(cells.Count)];
        }
    }
}