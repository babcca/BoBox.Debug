using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;


namespace BoBox.Interfaces
{
    using NodeId = Int64;

    public interface ITyped
    {
        string Type { get; }
    }

    public interface IEdge
    {
        Int64 From { get; set; }
        Int64 To { get; set; }
        string Label { get; set; }
    }
    
    public interface IVisitor
    {
    }

    public interface IVisited
    {
        void Visit(IVisitor visitor);
    }
 
    public interface INode : ITyped
    {
        int ParentId { get; set; }
        //int Deep { get; set; }
    }

    public interface IGraph
    {
        //void AddMetaNode(INode node);
    }

    public interface ISubgraph : IGraph, INode, ITyped
    {
    }
}

