using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BoBox.Graph.Interface
{
    public interface IMetaVertex
    {
    }

    public interface IVertex : IVertex<Vertex>
    {
    }

    public interface IVertex<TSourceTargetVertex> : IHasParent<TSourceTargetVertex>
    {
        Int32 VertexId { get; }
        IEnumerable<IVertex<TSourceTargetVertex>> EdgesOut { get; }
        IEnumerable<IVertex<TSourceTargetVertex>> EdgesIn { get; }

        void AddOutEdge(IVertex<TSourceTargetVertex> targetVertex);
        void AddInEdge(IVertex<TSourceTargetVertex> sourceVertex);
    }

    public interface IBox : IVertex
    {
    }

    public interface IType
    {
        string Type { get; }
    }
}
