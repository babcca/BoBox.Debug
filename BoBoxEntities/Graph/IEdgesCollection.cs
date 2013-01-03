using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BoBox.Graph.Interface
{
    public interface IEdgesCollection
    {
        IEnumerable<IEdge<IVertex>> Edges { get; }
        //void AddEdge(IEdge<IVertex> edge);
    }

    public interface IEdgesAndVerticesCollection : IVerticesCollection, IEdgesCollection
    {
    }
}
