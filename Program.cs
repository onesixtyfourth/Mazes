using System;
using Mazes.Factory;
using Mazes.utils;

namespace Mazes
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var factory = MazeFactory.Instance;
            var random = new Random();

            var algo = factory.Algorithms[random.Next(maxValue: factory.Algorithms.Count)];
            var maze = factory.GenerateCarvedMaze(algo);

            factory.DrawMaze(maze, new DrawMazeConsole());
            Console.WriteLine($"Algorithm: {algo} {maze}");
        }
    }
}
