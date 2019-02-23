using System;
using Mazes.Factory;
using Mazes.interfaces;

namespace Mazes.Algorithms
{
    public class RecursiveDivision : IAlgorithm
    {
        private IMaze maze;
        private Random random = new Random();

        public void Carve(IMaze maze)
        {
            this.maze = maze;
            Divide(maze.Grid[0], maze.Grid[maze.Grid.Count - 1]);
        }

        private void Divide(Cell start, Cell end)
        {
            var width = end.Column - start.Column;
            var height = end.Row - start.Row;
            var numCells = (width + 1) * (height + 1);

            if(numCells > 1)
            {
                var split = int.MinValue;

                var leftStart = start;
                Cell leftEnd = null;

                Cell rightStart = null;
                var rightEnd = end;

                if(width > height)
                {//split across width                    
                    split = (int)Math.Floor((double)width / 2);
                    leftEnd = maze.GetCell(end.Row, start.Column + split);
                    rightStart = maze.GetCell(start.Row, start.Column + split + 1);
                }
                else
                {//split across height
                    split = (int)Math.Floor((double)height / 2);
                    leftEnd = maze.GetCell(start.Row + split, end.Column);
                    rightStart = maze.GetCell(start.Row + split + 1, start.Column);
                }

                Divide(leftStart, leftEnd);
                Divide(rightStart, rightEnd);
                
                JoinDivisions(leftStart, leftEnd, rightStart, rightEnd);
            }
        }

        private void JoinDivisions(Cell leftStart, Cell leftEnd, Cell rightStart, Cell rightEnd)
        {
            var selected = int.MinValue;
            Cell left = null;
            Cell right = null;

            if(rightStart.Column > leftEnd.Column)
            {
                selected = random.Next( leftStart.Row, leftEnd.Row );
                left = maze.GetCell(selected, leftEnd.Column);
                right = maze.GetCell(selected, rightStart.Column);
            }
            else
            {
                selected = random.Next( leftStart.Column, leftEnd.Column);
                left = maze.GetCell(leftEnd.Row, selected);
                right = maze.GetCell(rightStart.Row, selected);
            }

            maze.ConnectCells(left, right);
        }
    }
}