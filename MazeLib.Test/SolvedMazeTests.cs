using System;
using Xunit;
using MazeLib.Factory;
using MazeLib.Solvers;

namespace MazeLib.Test
{
    public class SolvedMazeTests
    {
        [Fact]
        public void constructorNullMazeThrows()
        {
            var algorithm = MazeFactory.Instance.CreateAlgorithm(
                    Type.GetType("MazeLib.Algorithms.BinaryTree, MazeLib"));

            Assert.Throws<ArgumentNullException>(() => 
                new SolvedMaze(null, algorithm, new Dijkstra()));
        }

        [Fact]
        public void ConstructorNullAlgorithmThrows()
        {
            var maze = new Maze();
            Assert.Throws<ArgumentNullException>(() => 
                new SolvedMaze(maze, null, new Dijkstra()));
        }

        [Fact]
        public void ConstructorNullSolverThrows()
        {
            var maze = new Maze();
            var algorithm = MazeFactory.Instance.CreateAlgorithm(
                    Type.GetType("MazeLib.Algorithms.BinaryTree, MazeLib"));

            Assert.Throws<ArgumentNullException>(() => 
                new SolvedMaze(maze, algorithm, null));
        }

        [Fact]
        public void ConstructorCreatesCorrectObject()
        {
            var maze = new Maze();
            var algorithm = MazeFactory.Instance.CreateAlgorithm(
                    Type.GetType("MazeLib.Algorithms.BinaryTree, MazeLib"));            
            var solved = new SolvedMaze(maze, algorithm, new Dijkstra());

            Assert.NotNull(solved);
            Assert.NotNull(solved.Solver);
            Assert.IsType(Type.GetType("MazeLib.Algorithms.BinaryTree, MazeLib"), solved.Algorithm);
            Assert.Equal(solved.Size, maze.Size);
        }
        
    }
}