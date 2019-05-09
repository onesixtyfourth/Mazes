using System;
using System.Linq;
using System.Text;
using MazeLib.Factory;
using MazeLib.Interfaces;

namespace MazeLib.Utilities
{
    public class DrawConsoleDistances : IDrawMazes
    {
        private const string CORNER = "+";

        private const string HORIZONTAL = "----";

        private const string VERTICAL = "|";

	    private const string VERTPASS = " "; 

        private IMaze maze;

        private ISolveMazes solver;

        public DrawConsoleDistances(ISolveMazes solver)
        {
            _ = solver ?? throw new ArgumentNullException(nameof(solver));

            this.solver = solver;
        }

        public void DrawMaze(IMaze maze)
        {
            _ = maze ?? throw new ArgumentNullException(nameof(maze));
            
            this.maze = maze;
            var output = new StringBuilder();

            for(int i = 0; i < maze.Width; ++i)
            {
                output.Append($"{CORNER}{HORIZONTAL}");
            }

            output.Append(CORNER);
            Console.WriteLine(output.ToString());

            for(int i = 0; i < maze.Height; ++i)
            {
                Console.WriteLine(DrawRow(i));
            }
        }

        private string DrawRow(int row)
        {
            var output = new StringBuilder();

            foreach(Cell cell in maze.Grid.Where(c => c.Row.Equals(row)))
            {

                if( cell.Connected.Where(c => c.Column < cell.Column).Any() )
                {
                    output.Append($"{VERTPASS}{solver.Distances[maze.Grid.IndexOf(cell)],-4:D}");
                }
                else
                {
                    output.Append($"{VERTICAL}{solver.Distances[maze.Grid.IndexOf(cell)],-4:D}");    
                }
            }

            output.Append(VERTICAL);
            output.AppendLine();

            foreach(Cell cell in maze.Grid.Where(c => c.Row.Equals(row)))
            {
                if(cell.Connected.Where(c => c.Row > cell.Row).Any())
                {
                     output.Append($"{CORNER}    "); 
                }
                else
                {
                     output.Append($"{CORNER}{HORIZONTAL}"); 
                }
            }
            
            output.Append(CORNER);
            return output.ToString();
        }
    }
}