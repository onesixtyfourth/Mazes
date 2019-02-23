using System.Collections.Generic;
using System.Linq;
using Mazes.factory;
using Mazes.interfaces;

namespace Mazes.solvers
{
    public class Dijkstra : ISolveMazes
    {
        private const int UNVISITED = -1;

        public IList<int> Distances { get; set; }

        public IList<int> Solve(IMaze maze, Cell start)
        {
            Distances = Enumerable.Repeat(UNVISITED, maze.Size).ToList();
            var currentDistance = Distances[maze.Grid.IndexOf(start)] = 0;   

            var frontier = maze.GetConnectedCells(new List<Cell>(){start});

            while(Distances.Contains(UNVISITED))
            {
                foreach(Cell cell in frontier)
                {
                    var index = maze.Grid.IndexOf(cell);
                    if (Distances[maze.Grid.IndexOf(cell)] == UNVISITED)
                    {
                        Distances[index] = currentDistance + 1;
                    }
                }

                currentDistance++;
                frontier = maze.GetConnectedCells(frontier.ToList());
            }

            return Distances;
        }

        public IList<Cell> FindPath(Cell start, Cell end)
        {
            var path = new List<Cell>();

            return path;
        }
    }
}