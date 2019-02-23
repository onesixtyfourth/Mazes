using Mazes.interfaces;
using Mazes.factory;
using System.Linq;
using System;
using System.Collections.Generic;

namespace Mazes.Algorithms
{
    public class SideWinder : IAlgorithm
    {
        private Random random = new Random();

        public void Carve(IMaze maze)
        {
            for(int i = 0; i < maze.Height; ++i)
            {
                var run = new List<Cell>();
                foreach(Cell cell in maze.GetRow(i))
                {
                    if(run.Count() > 0)
                    {
                        maze.ConnectCells(cell, run[run.Count() - 1]);
                    }
                    run.Add(cell);                    

                    if(i != maze.Height - 1)
                    {
                        if(random.NextDouble() >= 0.5)
                        {
                            CloseRun(run, maze);                   
                        }
                    }
                }

                if(run.Any())
                {
                    CloseRun(run, maze);
                }
            }
        }

        private void CloseRun(List<Cell> run, IMaze maze)
        {
            var chosen = run[random.Next(run.Count())];

            if(chosen.Row + 1 < maze.Height)
            {
                var connectTo = maze.GetCell((chosen.Row + 1), chosen.Column);
                maze.ConnectCells(chosen, connectTo);
            }  
            run.Clear();
        }
    }
}