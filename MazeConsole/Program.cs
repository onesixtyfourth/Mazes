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
            var instance = MazeFactory.Instance;

            var algorithm = instance.Algorithms[random.Next(maxValue: instance.Algorithms.Count)];            
            var solver = new Dijkstra();
            var solved = instance.GenerateCarveAndSolve(algorithm, solver, 5, 5);
            
            instance.DrawMaze(solved, new DrawMazeConsole());
            Console.WriteLine($"Algorithm: {solved.Algorithm} {solved}");
        }
    }
}
