using System.Collections.Generic;

namespace MazeLib.Interfaces
{
    public interface ISolveMazeLib
    {
        IList<int> Distances { get; set; }
    }
}