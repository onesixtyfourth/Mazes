using System.Collections.Generic;

namespace Mazes.Interfaces
{
    public interface ISolveMazes
    {
        IList<int> Distances { get; set; }
    }
}