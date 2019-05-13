using System;
using System.Collections.Generic;
using System.Linq;
using MazeLib.Factory;
using MazeLib.Interfaces;

namespace MazeLib.Solvers
{
    public class Dijkstra : ISolveMazes
    {
        private const int UNVISITED = -1;
        private const int DEADEND_PASSAGES = 1;
        private const int JUNCTION_PASSAGES = 3;
        private const int CROSSROAD_PASSAGES = 4;

        public IList<int> Distances { get; set; }
       
        public IList<int> Solve(IMaze maze, Cell start)
        {
            _ = maze ?? throw new ArgumentNullException(nameof(maze));
            _ = start ?? throw new ArgumentNullException(nameof(start));

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

        public IList<Cell> FindPath(IMaze maze, Cell goal)
        {
            var path = new List<Cell>();
            var start = maze.Grid[Distances.Single(i => i == 0)];
            var currentDistance = Distances[maze.Grid.IndexOf(goal)];

            path.Add(goal);

            do
            {
                var potential = path.Last().Connected
                    .Where(c => Distances[maze.Grid.IndexOf(c)] < currentDistance);
                path.Add(potential.First());
                currentDistance = Distances[maze.Grid.IndexOf(path.Last())];

            }while( path.Last() != start);            

            return path;
        }

        public IList<Cell> FindLongestPath(IMaze maze)
        {
            var furthest = Distances.IndexOf(Distances.Max());

            return FindPath(maze, maze.Grid[furthest]);
        }

        public IList<Cell> DeadEnds(IMaze maze) => 
            maze.Grid.Where(c => c.Connected.Count() == DEADEND_PASSAGES).ToList();

        public IList<Cell> Junctions(IMaze maze) => 
            maze.Grid.Where(c => c.Connected.Count() == JUNCTION_PASSAGES).ToList();

        public IList<Cell> Crossroads(IMaze maze) => 
            maze.Grid.Where(c => c.Connected.Count() == CROSSROAD_PASSAGES).ToList();

        public int Branches(IMaze maze) => 
            Junctions(maze).Count() + (Crossroads(maze).Count * 2);

        public int Terminations(IMaze maze) => Branches(maze) + 2;

        public int Passages(IMaze maze)
        {
            return ((Junctions(maze).Count() * JUNCTION_PASSAGES) + 
                    (Crossroads(maze).Count() * CROSSROAD_PASSAGES) + 
                    Terminations(maze)) / 2;
        }

        public int Valence(IMaze maze) => (Terminations(maze) - Branches(maze) - 2) / 2;
    }
}