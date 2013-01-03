using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Runtime.Serialization;

namespace BoBoxEntities
{
    public class Graph
    {
        [IgnoreDataMember]
        public IList<Node> Nodes { get; set; }

        [IgnoreDataMember]
        public Node Source { get; set; }

        [IgnoreDataMember]
        public Node Target { get; set; }
    }

    public class Node
    {
        [IgnoreDataMember]
        public int GraphLayer { get; set; }

        [IgnoreDataMember]
        public Graph ParentGraph { get; set; }
    }

    public class Subgraph : Node
    {
        public Graph Graph { get; set; }

        public Subgraph()
        {
            Graph = new Graph();
            //Graph.Source = new Node() { GraphLayer = GraphLayer + 1, ParentGraph = 
        }
    }

    public class Bla
    {
        public static void B()
        {
            var g = new Graph();
            g.Nodes.Add(new Node() { ParentGraph = g, GraphLayer = 0 });
            g.Nodes.Add(new Node() { ParentGraph = g, GraphLayer = 0 });

            var s = new Subgraph() { ParentGraph = g, GraphLayer = 0 };
            
        }
    }
}
