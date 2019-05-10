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
            
            instance.DrawMaze(solved, new DrawConsoleDistances(solver));
            Console.WriteLine($"Algorithm: {solved.Algorithm} {solved}");

            var shortPath = solved.Solver.FindPath(solved, solved.Grid.Last());
            var path = solved.Solver.FindLongestPath(solved);
            var deadEnds = solver.DeadEnds(solved);
            var junctions = solver.Junctions(solved);
            var crossroads = solver.Crossroads(solved);
            var branches = solver.Branches(solved);
            var terminations = solver.Terminations(solved);
            var passages = solver.Passages(solved);
            var valence = solver.Valence(solved);
            Console.WriteLine($"shortest Path: {String.Join(", ", shortPath)}");
            Console.WriteLine($"Longest Path: {String.Join(", ", path)}");
            Console.WriteLine($"Dead Ends ({deadEnds.Count()}): {String.Join(", ", deadEnds)}");
            Console.WriteLine($"Junctions ({junctions.Count()}): {String.Join(", ", junctions)}");
            Console.WriteLine($"Crossroads ({crossroads.Count()}): {String.Join(", ", crossroads)}");
            Console.WriteLine($"branches: {branches}");
            Console.WriteLine($"Terminations: {terminations}");
            Console.WriteLine($"Passages: {passages}");
            Console.WriteLine($"Valence: {valence}");
        }
    }
}
