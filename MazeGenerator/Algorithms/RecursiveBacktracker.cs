using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Mazes.Factory;
using Mazes.Interfaces;

namespace Mazes.Algorithms
{
    public class RecursiveBacktracker : IAlgorithm
    {
        private Random random = new Random();

        public void Carve(IMaze maze)
        {
            var stack = new Stack<Cell>();
            stack.Push(maze.Grid[random.Next(maze.Grid.Count)]);

            while(stack.Count > 0)
            {
                var unvisited = 
                    maze.GetNeighbours(stack.Peek()).
                        Where(c => !c.Connected.Any()).
                        ToList();

                if(unvisited.Any())
                {
                    var next = unvisited[random.Next(unvisited.Count())];
                    maze.ConnectCells(stack.Peek(), next);
                    stack.Push(next);
                }
                else
                {
                    stack.Pop();
                }
            }
        }
    }
}