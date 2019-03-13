using System;
using System.Collections.Generic;
using System.Linq;
using MazeLib.Factory;
using MazeLib.Interfaces;

namespace MazeLib.Algorithms
{
    public class Kruskals : IAlgorithm
    {
        private List<HashSet<Cell>> cellSets = new List<HashSet<Cell>>();
        private Random random = new Random();

        public void Carve(IMaze maze)
        {
            _ = maze ?? throw new ArgumentNullException(nameof(maze));
            
            initCellSets(maze.Grid);

            while(cellSets.Count > 1)
            {
                var currentSet = cellSets[random.Next(cellSets.Count)].ToArray();
                var current =   currentSet[random.Next(currentSet.Length)]; 

                var neighbours = maze.GetNeighbours(current);
                var potential = neighbours[random.Next(neighbours.Count)];

                joinSets(maze, current, potential);
            }
        }

        private void joinSets(IMaze maze, Cell current, Cell potential)
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

        private void initCellSets(IList<Cell> cellList)
        {
            foreach( var cell in cellList)
            {
                var temp = new HashSet<Cell>();
                temp.Add(cell);
                cellSets.Add(temp);
            }
        }
    }
}