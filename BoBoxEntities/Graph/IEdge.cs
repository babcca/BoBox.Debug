using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BoBox.Graph.Interface
{
    public interface IEdge<out TVertex>
        where TVertex: IVertex
    {
        TVertex Source { get; }
        TVertex Destination { get; }
    }
}
