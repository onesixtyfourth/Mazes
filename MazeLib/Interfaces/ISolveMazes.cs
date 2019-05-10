using System.Collections.Generic;
using MazeLib.Factory;

namespace MazeLib.Interfaces
{
    public interface ISolveMazes
    {
        IList<int> Distances { get; set; }
        IList<int> Solve(IMaze maze, Cell start);
        IList<Cell> FindPath(IMaze maze, Cell goal);
        IList<Cell> FindLongestPath(IMaze maze);
        IList<Cell> DeadEnds(IMaze maze);
        IList<Cell> Junctions(IMaze maze);
        IList<Cell> Crossroads(IMaze maze);
        int Branches(IMaze maze);
        int Terminations(IMaze maze);
        int Passages(IMaze maze);
        int Valence(IMaze maze);
    }
}