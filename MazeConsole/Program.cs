using System;
using System.Linq;
using MazeLib.Factory;
using MazeLib.Solvers;
using MazeLib.Utilities;

namespace MazeConsole
{
    class Program
    {
        static void Main(string[] args)
        {
            var random = new Random();

            var algorithmType = MazeFactory.Instance.Algorithms[
                    random.Next(maxValue: MazeFactory.Instance.Algorithms.Count)];
                    
            var maze = MazeFactory.Instance.GenerateCarvedMaze(algorithmType, 15, 15);
            // var maze = MazeFactory.Instance.GenerateMaze(15, 15);
            // var algorithm = MazeFactory.Instance.CreateAlgorithm(algorithmType);
            // algorithm.Carve(maze);

            // var solver = new Dijkstra();
            // solver.Solve(maze, maze.Grid.First());
            // MazeFactory.Instance.DrawMaze(maze, new DrawConsoleDistances(solver));
            MazeFactory.Instance.DrawMaze(maze, new DrawMazeConsole());
            Console.WriteLine($"Algorithm: {algorithmType} {maze}");
        }
    }
}
