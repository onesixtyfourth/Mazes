using System.Collections.Generic;

namespace MazeLib.Interfaces
{
    public interface ISolveMazes
    {
        IList<int> Distances { get; set; }
    }
}