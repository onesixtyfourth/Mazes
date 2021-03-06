using System;
using Xunit;
using MazeLib.Factory;
using MazeLib.Utilities;
using MazeLib.Solvers;

namespace MazeLib.Test
{
    public class MazeFactoryTests
    {
        [Fact]
        public void InstanceReturnsSameObject()
        {
            var factoryOne = MazeFactory.Instance;
            var factoryTwo = MazeFactory.Instance;

            Assert.True(factoryOne == factoryTwo);
        }
        
        [Fact]
        public void CreateAlgorithNullArgumentThrows()
        {
            Assert.Throws<ArgumentNullException>(() => MazeFactory.Instance.CreateAlgorithm(null));
        }

        [Fact]
        public void CreateAlgorithmReturnsCorrectType()
        {
            var algoType = MazeFactory.Instance.CreateAlgorithm(
                Type.GetType("MazeLib.Algorithms.BinaryTree, MazeLib"));
            
            Assert.True(algoType.GetType().Equals(
                Type.GetType("MazeLib.Algorithms.BinaryTree, MazeLib")));
        }

        [Fact]
        public void GenerateMazeDefaultReturnsCorrectSizeMaze()
        {
            var theMaze = MazeFactory.Instance.GenerateMaze();

            Assert.True(theMaze.Size == 4);
        }

        [Fact]
        public void GenerateMazeCreatesCorrectlysizedMaze()
        {
            var width = 10;
            var height = 10;
            var theMaze = MazeFactory.Instance.GenerateMaze(width, height);

            Assert.True(theMaze.Size == width * height);
        }

        [Fact]
        public void GenerateCarvedMazeNullAlgorithmThrows()
        {
            Assert.Throws<ArgumentNullException>(
                () => MazeFactory.Instance.GenerateCarvedMaze(null));
        }

        [Fact]
        public void GenerateCarvedMazeDefaultsReturnsCorrectly()
        {
            var theMaze = MazeFactory.Instance.GenerateCarvedMaze(
                Type.GetType("MazeLib.Algorithms.BinaryTree, MazeLib"));

            Assert.True(theMaze.Size == 4);
            foreach(var cell in theMaze.Grid)
            {
                Assert.NotEmpty(cell.Connected);
            }
        }

        [Fact]
        public void DrawMazeNullMazeThrows()
        {
            Assert.Throws<ArgumentNullException>(
                () => MazeFactory.Instance.DrawMaze(null, new DrawMazeConsole()));
        }

        [Fact]
        public void DrawMazeNullAlgorithmThrows()
        {
            Assert.Throws<ArgumentNullException>(
                () => MazeFactory.Instance.DrawMaze(new Maze(2, 2), null));
        }

        [Fact]
        public void GenerateCarveAndSolveNullalgorithmThrows()
        {
            Assert.Throws<ArgumentNullException>(() => 
                MazeFactory.Instance.GenerateCarveAndSolve(null, 
                                                            new Dijkstra(), 
                                                            2, 2));
        }

        [Fact]
        public void GenerateCarveAndSolveNullSolverThrows()
        {
            Assert.Throws<ArgumentNullException>(() => 
                MazeFactory.Instance.GenerateCarveAndSolve(
                    Type.GetType("MazeLib.Algorithms.BinaryTree, MazeLib"), 
                    null, 
                    2, 2));
        }

        [Fact]
        public void GenerateCarveAndSolveInvalidDimensionThrows()
        {
            Assert.Throws<ArgumentException>(() => 
                MazeFactory.Instance.GenerateCarveAndSolve(
                    Type.GetType("MazeLib.Algorithms.BinaryTree, MazeLib"), 
                    new Dijkstra(),
                    0, 0));
        }

        [Fact]
        public void GenerateCarveAndSolveCreatesCorrectly()
        {
            var solvedMaze = MazeFactory.Instance.GenerateCarveAndSolve(
                Type.GetType("MazeLib.Algorithms.BinaryTree, MazeLib"),
                new Dijkstra(),
                5, 5
            );

            Assert.NotNull(solvedMaze);
            Assert.NotNull(solvedMaze.Solver);
            Assert.NotNull(solvedMaze.Algorithm);
            Assert.Equal(solvedMaze.Size, solvedMaze.Width * solvedMaze.Height);
        }
    }
}