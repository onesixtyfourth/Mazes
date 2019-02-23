using System;
using System.Collections.Generic;
using System.Linq;
using Mazes.Factory;
using Mazes.Interfaces;

namespace Mazes.Algorithms
{
    public class Ellers : IAlgorithm
    {
        private List<HashSet<Cell>> cellSets = new List<HashSet<Cell>>();
        private Random random = new Random();
        private IMaze maze;

        public void Carve(IMaze maze)
        {
            this.maze = maze;
            for(int i = 0; i < maze.Height; ++i)
            {
                initRow(maze.GetRow(i));
                int onethird = cellSets.Count / 3;
                ReduceSets(random.Next(onethird, onethird * 2));

                if(i < maze.Height - 1)
                {
                    LinkToNextRow();
                }                
            }

            ReduceSets();
        }

        private void ReduceSets(int max = 1)
        {
            while(cellSets.Count > max)
            {
                var cellSet = cellSets[random.Next(cellSets.Count)];
                var rowNumber = cellSet.Max(c => c.Row);

                var potentials = maze.GetNeighbours(cellSet.ToList())
                            .Where( c => c.Row == rowNumber);
                    

                var next = potentials.FirstOrDefault(c => !cellSet.Contains(c));
                var current = maze.GetNeighbours(next).FirstOrDefault(c => cellSet.Contains(c));
                   
                joinSets(current, next);
                maze.ConnectCells(current, next);           
            }
        }

        private void LinkToNextRow()
        {
            foreach(var set in cellSets)
            {
                var rowNumber = set.Max(c => c.Row);
                var potentials = set.Where(c => c.Row == rowNumber);
                var numConnections = random.Next(1, potentials.Count());
                
                var connections = 0;
                while(connections < numConnections)
                {
                    var current = potentials.ToArray()[random.Next(potentials.Count())];
                    var next = maze.GetNeighbours(current).FirstOrDefault(c => c.Row > rowNumber);

                    if(next != null)
                    {
                        maze.ConnectCells(current, next);
                        set.Add(next);
                        potentials.ToList().Remove(current);
                        connections++;
                    }   
                }
            }
        }

        private void initRow(IList<Cell> cells)
        {
            foreach(var cell in cells)
            {
                if(!cellSets.Any(s => s.Contains(cell)))
                {
                    var temp = new HashSet<Cell>();
                    temp.Add(cell);
                    cellSets.Add(temp);
                }
            }
        }

        private void joinSets(Cell current, Cell potential)
        {
            var currentSet = cellSets.FirstOrDefault(s => s.Contains(current));
            var nextSet = cellSets.FirstOrDefault(s => s.Contains(potential));

            if(!currentSet.Equals(nextSet))
            {
                maze.ConnectCells(current, potential);
                foreach(var cell in nextSet)
                {
                    currentSet.Add(cell);
                }

                cellSets.Remove(nextSet);
                nextSet.Clear();
            }
        }
    }
}