using System;
using System.Linq;
using System.Collections.Generic;
using Xunit;
using MazeLib.Factory;

namespace MazeLib.Test
{
    public class MazeTests
    {
        [Fact]
        public void ConstructorCreatesMazeObject()
        {
            var maze = new Maze(2, 2);
            Assert.True(maze != null && maze.Grid.Count == maze.Size);
        }

        [Theory]
        [InlineData(1, 2)]
        [InlineData(2, 1)]
        public void InvalidConstructorArgumentThrows(int width, int height)
        {
            Assert.Throws<ArgumentException>(() => new Maze(width, height));
        }
        
        [Fact]
        public void GetCellreturnsCorrectObject()
        {
            var maze = new Maze(5, 5);
            var cell = maze.GetCell(2, 1);

            Assert.True(cell.Row == 2 && cell.Column == 1);
        }

        [Fact]
        public void GetRowReturnsCorrectList()
        {
            var maze = new Maze(2, 2);
            var row = maze.GetRow(0);

            Assert.True(row.Count == 2 && row.Sum(x => x.Row) == 0);
        }

        [Fact]
        public void GetColumnReturnsCorrectList()
        {
            var maze = new Maze(2, 2);
            var col = maze.GetColumn(0);

            Assert.True(col.Count == 2 && col.Sum(x => x.Column) == 0);
        }

        [Fact]
        public void GetNeighboursNullCellArgumentThrows()
        {
            var maze = new Maze(2, 2);
            Cell cell = null;

            Assert.Throws<ArgumentNullException>(() => maze.GetNeighbours(cell));
        }

        [Fact]
        public void GetNeighboursReturnsCorrectSet()
        {
            var maze = new Maze(2, 2);
            var neighbours = maze.GetNeighbours(maze.GetCell(0, 0));

            Assert.True(neighbours.Count == 2 && !neighbours.Contains(maze.GetCell(0, 0)));
        }

        [Fact]
        public void GetNeighboursNullListArgumentThrows()
        {
            var maze = new Maze(2, 2);
            List<Cell> cellList = null;

            Assert.Throws<ArgumentNullException>(() => maze.GetNeighbours(cellList));
        }

        [Fact]
        public void GetNeighboursListReturnsCorrectSet()
        {
            var maze = new Maze(2, 2);
            var cellList = new List<Cell>();
            cellList.Add(maze.GetCell(0, 1));
            cellList.Add(maze.GetCell(1, 1));
            var neighbours = maze.GetNeighbours(cellList);

            Assert.True(neighbours.Count == 2);
            Assert.False(neighbours.Contains(cellList[0]));
            Assert.False(neighbours.Contains(cellList[1]));
        }

        [Fact]
        public void GetConnectedCellsNullArgumentThrows()
        {
            var maze = new Maze(2, 2);
            List<Cell> cellList = null;

            Assert.Throws<ArgumentNullException>(() => maze.GetConnectedCells(cellList));
        }

        [Fact]
        public void GetConnectedCellsReturnsCorrectSet()
        {
            var maze = new Maze(2, 2);
            var cellList = new List<Cell>();
            
            maze.ConnectCells(maze.GetCell(0, 0), maze.GetCell(0, 1));
            maze.ConnectCells(maze.GetCell(0, 0), maze.GetCell(1, 0));

            cellList.Add(maze.GetCell(0, 0));
            var connected = maze.GetConnectedCells(cellList);

            Assert.True(connected.Count == 2);
            Assert.False(connected.Contains(maze.GetCell(0, 0)));
        }

        [Fact]
        public void ConnectCellsNullFirstArgumentThrows()
        {
            var maze = new Maze(2, 2);

            Assert.Throws<ArgumentNullException>(() => maze.ConnectCells(null, maze.GetCell(0, 1)));
        }

        [Fact]
        public void ConnectCellNullSecondArgumentThrows()
        {
            var maze = new Maze(2, 2);

            Assert.Throws<ArgumentNullException>(() => maze.ConnectCells(maze.GetCell(0, 0), null));
        }

        [Fact]
        public void ConnectCellCreatesEdgeForCells()
        {
            var maze = new Maze(2, 2);
            maze.ConnectCells(maze.GetCell(0, 0), maze.GetCell(0, 1));

            var cellOne = maze.GetCell(0, 0);
            var cellTwo = maze.GetCell(0, 1);

            Assert.True(cellOne.Connected.Count == 1 && cellOne.Connected.First().Equals(cellTwo));
            Assert.True(cellTwo.Connected.Count == 1 && cellTwo.Connected.First().Equals(cellOne));
        }

        [Fact]
        public void ConnectPathNullArgumentThrows()
        {
             var maze = new Maze(2, 2);
            List<Cell> cellList = null;

            Assert.Throws<ArgumentNullException>(() => maze.ConnectPath(cellList));             
        }

        [Fact]
        public void ConnectPathTwoCellsConnected()
        {
            var maze = new Maze(2, 2);
            var cells = new List<Cell>();
            cells.Add(maze.GetCell(0, 0));
            cells.Add(maze.GetCell(0, 1));

            maze.ConnectPath(cells);

            var cellOne = maze.GetCell(0, 0);
            var cellTwo = maze.GetCell(0, 1);

            
            Assert.True(cellOne.Connected.Count == 1 && cellOne.Connected.First().Equals(cellTwo));
            Assert.True(cellTwo.Connected.Count == 1 && cellTwo.Connected.First().Equals(cellOne));
        }

        [Fact]
        public void EqualsMethodReturnsFalseCorrectly()
        {
            var mazeOne = new Maze(2, 2);
            var mazeTwo = new Maze(2, 2);

            Assert.False(mazeOne.Equals(mazeTwo));
        }

        [Fact]
        public void EqualsMethodSymmetricAndReflexive()
        {
            var mazeOne = new Maze(2, 2);
            var mazeTwo = mazeOne;

            Assert.True(mazeOne == mazeTwo);
            Assert.True(mazeOne.Equals(mazeTwo));
            Assert.True(mazeTwo.Equals(mazeOne));
        }

        [Fact]
        public void EqualsMethodTransitive()
        {
            var mazeOne = new Maze(2, 2);
            var mazeTwo = mazeOne;
            var mazeThree = mazeTwo;

            Assert.True(mazeOne.Equals(mazeTwo));
            Assert.True(mazeTwo.Equals(mazeThree));
            Assert.True(mazeThree.Equals(mazeTwo));
        }

        [Fact]
        public void GetHashCodeProducesuniqueResult()
        {
            var mazeOne = new Maze(2, 2);
            var mazeTwo = new Maze(2, 2);

            Assert.False(mazeOne.GetHashCode() == mazeTwo.GetHashCode());
        }
    }
}