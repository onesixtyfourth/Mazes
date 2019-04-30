using System;
using System.Linq;
using MazeLib.Factory;
using MazeLib.Solvers;
using MazeLib.Utilities;

using System.Collections.Generic;

namespace MazeConsole
{
    class Program
    {
        static void Main(string[] args)
        {
            var random = new Random();

            var algorithmType = MazeFactory.Instance.Algorithms[
                    random.Next(maxValue: MazeFactory.Instance.Algorithms.Count)];
                    
            var maze = MazeFactory.Instance.GenerateCarvedMaze(algorithmType, 5, 5);
            MazeFactory.Instance.DrawMaze(maze, new DrawMazeConsole());
            Console.WriteLine($"Algorithm: {algorithmType} {maze}");
        }
    }
}
