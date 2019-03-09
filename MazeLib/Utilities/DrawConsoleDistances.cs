using System;
using System.Linq;
using System.Text;
using MazeLib.Factory;
using MazeLib.Interfaces;

namespace MazeLib.Utilitiess
{
    public class DrawConsoleDistances : IDrawMazeLib
    {
        private const string CORNER = "+";

        private const string HORIZONTAL = "---";

        private const string VERTICAL = "|";

	    private const string VERTPASS = " ";

        private IMaze maze;

        private ISolveMazeLib solver;

        public DrawConsoleDistances(ISolveMazeLib solver)
        {
            this.solver = solver;
        }

        public void DrawMaze(IMaze maze)
        {
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

        public string DrawRow(int row)
        {
            var output = new StringBuilder();

            foreach(Cell cell in maze.Grid.Where(c => c.Row.Equals(row)))
            {

                if( cell.Connected.Where(c => c.Column < cell.Column).Any() )
                {
                    output.Append($"{VERTPASS}{solver.Distances[maze.Grid.IndexOf(cell)],3:D}");
                }
                else
                {
                    output.Append($"{VERTICAL}{solver.Distances[maze.Grid.IndexOf(cell)],3:D}");    
                }
            }

            output.Append(VERTICAL);
            output.AppendLine();

            foreach(Cell cell in maze.Grid.Where(c => c.Row.Equals(row)))
            {
                if(cell.Connected.Where(c => c.Row > cell.Row).Any())
                {
                     output.Append($"{CORNER}   "); 
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