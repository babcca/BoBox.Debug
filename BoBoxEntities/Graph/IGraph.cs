using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BoBox.Graph.Interface
{
    public interface ISourceTargetGraph<TVertex>
        where TVertex : IVertex
    {
        IEnumerable<TVertex> Sources { get; }
        IEnumerable<TVertex> Targets { get; }
        
        void AddTargetVertex(TVertex source);        
        void AddSourceVertex(TVertex target);
    }

    public interface IParentGraph<TSourceTargetVertex> : ISourceTargetGraph<TSourceTargetVertex>
        where TSourceTargetVertex : IVertex
    {
        Int32 GraphId { get; }        
    }

    public interface IHasParent<TSourceTargetVertex>
        where TSourceTargetVertex : IVertex
    {
        IParentGraph<TSourceTargetVertex> ParentGraph { get; set; }
    }


    public interface IGraph
    {
        Int32 GraphId { get; }
        //IEnumerable<IVertex> Vertices { get; }          
        Int32 GraphDeep { get; }
        IVertex Source { get; }
        IVertex Target { get; }
    }
}
