using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using MazeLib.Interfaces;

namespace MazeLib.Factory
{
    public class MazeFactory
    {
        private const string AlgorithmNamespace = "MazeLib.Algorithms";

        public static MazeFactory Instance { get; } = new MazeFactory();

        // Explicit static constructor to tell C# compiler
        // not to mark type as beforefieldinit   http://csharpindepth.com/Articles/General/Singleton.aspx (#4)
        static MazeFactory() { }
        private MazeFactory() { } 

        public IList<Type> Algorithms
        {
            get
            {
                return Assembly.GetExecutingAssembly()
                    .GetExportedTypes()
                    .Where(n => n.Namespace == AlgorithmNamespace && n.IsClass)
                    .ToList();
            }
        }

        public IAlgorithm CreateAlgorithm(Type algorithm)
        {
            _ = algorithm ?? throw new ArgumentNullException(nameof(algorithm));

            return (IAlgorithm)Activator.CreateInstance(algorithm, true);
        }

        public IMaze GenerateMaze(int width = Maze.DEFAULT_WIDTH, int height = Maze.DEFAULT_WIDTH)
        {
            return new Maze(width, height);
        }

        public IMaze GenerateCarvedMaze(Type algorithm, 
                                        int width = Maze.DEFAULT_WIDTH, 
                                        int height = Maze.DEFAULT_WIDTH)
        {
            _ = algorithm ?? throw new ArgumentNullException(nameof(algorithm));

            var maze = new Maze(width, height);
            var algo = CreateAlgorithm(algorithm);
            algo.Carve(maze);
            return maze;
        }

        public SolvedMaze GenerateCarveAndSolve(Type algorithm, 
                                        ISolveMazes solver, 
                                        int width = Maze.DEFAULT_WIDTH, 
                                        int height = Maze.DEFAULT_WIDTH)
        {
            _ = algorithm ?? throw new ArgumentNullException(nameof(algorithm));
            _ = solver ?? throw new ArgumentNullException(nameof(solver));

            var maze = GenerateMaze(width, height);
            var algo = CreateAlgorithm(algorithm); 

            return new SolvedMaze((Maze)maze, algo, solver);
        }

        public void CarveMaze(IMaze maze, IAlgorithm algorithm)
        {
            _ = maze ?? throw new ArgumentNullException(nameof(maze));
            _ = algorithm ?? throw new ArgumentNullException(nameof(algorithm));

            algorithm.Carve(maze);
        }

        public void SolveMaze(IMaze maze, ISolveMazes solver)
        {
            _ = maze ?? throw new ArgumentNullException(nameof(maze));
            _ = solver ?? throw new ArgumentNullException(nameof(solver));

            solver.Solve(maze, maze.Grid[0]);
        }

        public void DrawMaze(IMaze maze, IDrawMazes draw)
        {
            _ = maze ?? throw new ArgumentNullException(nameof(maze));
            _ = draw ?? throw new ArgumentNullException(nameof(draw));

            draw.DrawMaze(maze);
        }
    }
}