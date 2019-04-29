using System;
using Xunit;
using MazeLib.Factory;

namespace MazeLib.Test
{
    
    public class CellTests
    {
        [Fact]
        public void ConstructorCreatesCorrectly()
        {
            var aCell = new Cell(1, 1);
            Assert.True(aCell != null);
        }

        [Fact]
        public void ConstructedCellHasEmptyHasSet()
        {
            var aCell = new Cell(1, 1);
            Assert.Empty(aCell.Connected);
        }

        [Theory]
        [InlineData(-1, 0)]
        [InlineData(0, -1)]
        public void NegativeParametersThrows(int first, int second)
        {
             Assert.Throws<ArgumentException>(() => new Cell(first, second));
        }

        [Fact]
        public void NewCellIsAddedToConnected()
        {
            var aCell = new Cell(1, 1);
            var anotherCell = new Cell(2, 1);
            aCell.ConnectCell(anotherCell);

            Assert.True(aCell.Connected.Contains(anotherCell));
        }

        [Fact]
        public void EqualsMethodSymmetricAndReflexive()
        {
            var cellOne = new Cell(1, 1);
            var cellTwo = cellOne;

            Assert.True(cellOne == cellTwo);
            Assert.True(cellOne.Equals(cellTwo));
            Assert.True(cellTwo.Equals(cellOne));
        }

        [Fact]
        public void EqualsMethodTransitive()
        {
            var cellOne = new Cell(1, 1);
            var cellTwo = cellOne;
            var cellThree = cellTwo;

            Assert.True(cellOne.Equals(cellTwo));
            Assert.True(cellTwo.Equals(cellThree));
        }

        [Fact]
        public void EqualsMethodFailsCorrectly()
        {
            var cellOne = new Cell(1, 1);
            var cellTwo = new Cell(2, 2);

            Assert.False(cellOne.Equals(cellTwo));
        }
        
    }
}