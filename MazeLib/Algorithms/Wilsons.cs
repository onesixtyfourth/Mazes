using System;
using System.Collections.Generic;
using System.Linq;
using MazeLib.Factory;
using MazeLib.Interfaces;

namespace MazeLib.Algorithms
{
    public class Wilsons : IAlgorithm
    {
        private Random random = new Random();

        public void Carve(IMaze maze)
        {
            _ = maze ?? throw new ArgumentNullException(nameof(maze));
            
            var targets = new List<Cell>();
            targets.Add(maze.Grid[random.Next(maze.Grid.Count)]);

            while(maze.Grid.Any(c => !c.Connected.Any()))
            {
                var unvisited = maze.Grid.Where(
                            c => !c.Connected.Any()).ToList();

                targets.AddRange(maze.Grid.Where(
                            c => c.Connected.Any()));

                var path = new List<Cell>();
                var currentCell = unvisited[random.Next(unvisited.Count())];
                path.Add(currentCell);

                while(!targets.Contains(currentCell))
                {                    
                    var neighbours = maze.GetNeighbours(currentCell);
                    currentCell = neighbours[random.Next(neighbours.Count())];

                    if(path.Contains(currentCell))
                    {
                        var index = path.IndexOf(currentCell);
                        path.RemoveRange(index, path.Count() - index);
                    }

                    path.Add(currentCell);
                }

                maze.ConnectPath(path);
                targets.Clear();
            }
        }
    }
}