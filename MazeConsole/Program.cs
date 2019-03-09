using System;
using MazeLib.Factory;
using MazeLib.Utilities;

namespace MazeConsole
{
    class Program
    {
        static void Main(string[] args)
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
