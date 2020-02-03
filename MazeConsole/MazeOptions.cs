using System.Threading.Tasks;
using CommandLine;
using System;
using MazeLib.Factory;
using MazeLib.Solvers;
using MazeLib.Utilities;

namespace MazeConsole
{
    [Verb("generate", HelpText = "Generate a new maze")]
    public class MazeOptions
    {
        [Option('a', "algo", Required = false, Default = "RecursiveBacktracker", HelpText = "The algorithm to use for carving the new maze")]
        public string Algorithm { get; set; }

        [Option('w', "width", Required = false, Default = 5, HelpText = "The width for the maze")]
        public int Width { get; set; }

        [Option('h', "height", Required = false, Default = 5, HelpText = "The height for the maze")]
        public int Height { get; set; }

        public string AlgoNamespace { get; set; } = "MazeLib.Algorithms";

        public async Task DoWork()
        {
            var instance = MazeFactory.Instance;
            var algoType = Type.GetType($"{AlgoNamespace}.{Algorithm}, MazeLib");
            var solver = new Dijkstra();
            var solved = instance.GenerateCarveAndSolve(algoType, solver, Width, Height);

            instance.DrawMaze(solved, new DrawConsoleDistances(solver));
            Console.WriteLine($"Algorithm: {solved.Algorithm} {solved}");
        }
    }
}