using System;
using Mazes.Factory;
using Mazes.Utilities;

namespace Mazes
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var random = new Random();

            var algo = MazeFactory.Instance.Algorithms[
                    random.Next(maxValue: MazeFactory.Instance.Algorithms.Count)];
                    
            var maze = MazeFactory.Instance.GenerateCarvedMaze(algo);

            MazeFactory.Instance.DrawMaze(maze, new DrawMazeConsole());
            Console.WriteLine($"Algorithm: {algo} {maze}");
        }
    }
}
