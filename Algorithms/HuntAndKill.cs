using System;
using System.Linq;
using Mazes.interfaces;

namespace Mazes.Algorithms
{
    public class HuntAndKill : IAlgorithm
    {
        private Random random = new Random();
        public void Carve(IMaze maze)
        {
            var current = maze.Grid[random.Next(maze.Grid.Count)];

            while(current != null)
            {
                var unvisited = 
                    maze.GetNeighbours(current).
                        Where(c => !c.Connected.Any()).
                        ToList();
                
                if(unvisited.Any())
                {
                    var next = unvisited[random.Next(unvisited.Count())];
                    maze.ConnectCells(current, next);
                    current = next;
                }
                else
                {
                    current = null;

                    var next = maze.Grid.Where((c) => 
                    {
                        return !c.Connected.Any() && 
                            maze.GetNeighbours(c)
                                .Any(l => l.Connected.Any());
                    }).FirstOrDefault();                    

                    if(next != null)
                    {
                        current = next;
                        next = maze.GetNeighbours(current)
                            .Where(c => c.Connected.Any()).First();
                        maze.ConnectCells(current, next);
                    }
                }
            }
        }
    }
}