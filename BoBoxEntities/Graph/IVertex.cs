using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BoBox.Graph.Interface
{

    public interface IVertex: IHasParent        
    {
        Int32 VertexId { get; }
        IEnumerable<IVertex> EdgesOut { get; }
        IEnumerable<IVertex> EdgesIn { get; }

        void AddOutEdge(IVertex targetVertex);
        void AddInEdge(IVertex sourceVertex);
    }

    public interface IBox : IVertex
    {
    }

    public interface IType
    {
        string Type { get; }
    }
}
