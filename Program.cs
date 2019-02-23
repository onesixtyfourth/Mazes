using System;
using Mazes.factory;
using Mazes.solvers;
using Mazes.utils;

namespace Mazes
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var factory = MazeFactory.Instance;
            var random = new Random();

            var algoList = factory.Algorithms;

            var algo = factory.Algorithms[random.Next(maxValue: factory.Algorithms.Count)];
            // var algo = Type.GetType("Mazes.Algorithms.RecursiveDivision");
            var maze = factory.GenerateCarvedMaze(algo);

            // var solver = new Dijkstra();
            // solver.Solve(maze, maze.Grid[0]); 

            factory.DrawMaze(maze, new DrawMazeConsole());
            Console.WriteLine($"Algorithm: {algo} {maze}");

            // Console.WriteLine($"{maze.ToString()}");
        }
    }
}
