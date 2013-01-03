using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;




namespace BoBoxGraph.Struct
{
    using BoBoxVertextId = Int64;

    class BoBoxGraphStruct
    {
        public BoBoxGraphStruct(BoBoxVertextId id) 
        {
            this.id = id;
        }

        public BoBoxVertextId Id { get { return Id; } }
        BoBoxVertextId id;
        IList<BoBoxGraphStruct> vertices = new List<BoBoxGraphStruct>();
        IDictionary<BoBoxVertextId, BoBoxVertextId> Edges;

        public void GetInputEdges() { }
        public void GetOutputEdges() { }
        
        public void AddEdge(BoBoxVertextId from, params BoBoxVertextId[] targets)
        {
            foreach (var target in targets)
            {
                AddEdge(from, target);
            }
        }

        public void AddEdge(BoBoxVertextId from, BoBoxVertextId to)
        {

        }
        
        public void AddNode(BoBoxGraphStruct node)
        {
            vertices.Add(node); 
        }
    }

    class Node : BoBoxGraphStruct
    {
        public Node(BoBoxVertextId id)
            : base(id) { }
    }

    class Test
    {
        public static void MakeGraph()
        {                        
            // Zname-li dopredu pocet nod, potom je mozne vytvorit pole, kde budou jednotlive nody u sebe
            BoBoxGraphStruct subGraph = new BoBoxGraphStruct(7);
            subGraph.AddNode(new Node(2));
            subGraph.AddNode(new Node(3));
            subGraph.AddNode(new Node(4));
            subGraph.AddNode(new Node(5));


            BoBoxGraphStruct mainGraph = new BoBoxGraphStruct(8);
            mainGraph.AddNode(new Node(1));
            mainGraph.AddNode(subGraph);
            mainGraph.AddNode(new Node(6));

            mainGraph.AddEdge(1, 2);
            mainGraph.AddEdge(2, 3, 4);
            mainGraph.AddEdge(3, 5);
            mainGraph.AddEdge(4, 5);
            mainGraph.AddEdge(5, 6);
        }
    }
}
