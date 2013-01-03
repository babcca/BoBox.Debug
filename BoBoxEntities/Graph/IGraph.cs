using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BoBox.Graph.Interface
{
    public interface IGraph
    {
        Int32 GraphId { get; }
        //IEnumerable<IVertex> Vertices { get; }          
        Int32 GraphDeep { get; }
        IVertex Source { get; }
        IVertex Target { get; }
    }
}
