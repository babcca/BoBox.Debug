using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BoBox.Graph.Interface
{
    public interface ISourceTargetGraph
    {
        IEnumerable<IVertex> Sources { get; }
        IEnumerable<IVertex> Targets { get; }

        void AddTargetVertex(IVertex source);
        void AddSourceVertex(IVertex target);
    }

    public interface IParentGraph : ISourceTargetGraph
    {
        Int32 GraphId { get; }        
    }

    public interface IHasParent
    {
        IParentGraph ParentGraph { get; set; }
    }


    public interface IGraph
    {
        Int32 GraphId { get; }
        //IEnumerable<IVertex> Vertices { get; }          
        Int32 GraphDeep { get; }
    }
}
