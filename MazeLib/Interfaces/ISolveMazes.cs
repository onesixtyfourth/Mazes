using System.Collections.Generic;
using MazeLib.Factory;

namespace MazeLib.Interfaces
{
    public interface ISolveMazes
    {
        IList<int> Distances { get; set; }
        IList<int> Solve(IMaze maze, Cell start);
    }
}