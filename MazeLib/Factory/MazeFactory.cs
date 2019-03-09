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
        
        private const int DEFAULT_WIDTH = 5;

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

        public IAlgorithm CreateAlgorithm(Type type)
        {
            _ = type ?? throw new ArgumentNullException(nameof(type));
            return (IAlgorithm)Activator.CreateInstance(type, true);
        }

        public IMaze GenerateMaze(int width = DEFAULT_WIDTH, int height = DEFAULT_WIDTH)
        {
            return new Maze(width, height);
        }

        public IMaze GenerateCarvedMaze(Type algorithm, 
                                        int width = DEFAULT_WIDTH, 
                                        int height = DEFAULT_WIDTH)
        {
            var maze = new Maze(width, height);
            var algo = CreateAlgorithm(algorithm);
            algo.Carve(maze);
            return maze;
        }

        public void DrawMaze(IMaze maze, IDrawMazeLib draw)
        {
            draw.DrawMaze(maze);
        }
    }
}