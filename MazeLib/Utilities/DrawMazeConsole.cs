using System;
using System.Linq;
using System.Text;
using MazeLib.Factory;
using MazeLib.Interfaces;

namespace MazeLib.Utilities
{
    public class DrawMazeConsole : IDrawMazes
    {
        private const string CORNER = "+";

        private const string HORIZONTAL = "----";

        private const string VERTICAL = "|";

        private const string BODY = "    ";

	    private const string VERTPASS = " ";

        private IMaze maze;

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
                    output.Append($"{VERTPASS}{BODY}");
                }
                else
                {
                    output.Append($"{VERTICAL}{BODY}");    
                }
            }

            output.Append(VERTICAL);
            output.AppendLine();

            foreach(Cell cell in maze.Grid.Where(c => c.Row.Equals(row)))
            {
                if(cell.Connected.Where(c => c.Row > cell.Row).Any())
                {
                     output.Append($"{CORNER}{BODY}"); 
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