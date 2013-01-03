using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ServiceStack.Text;
using BoBoxEntities;

using BoBox.Entities;
using BoBox.Entities.Abstract;
using BoBox.Interfaces;


namespace BoBoxDebugTest
{
    using System.Runtime.Serialization;
    
    [DataContract]
    public class V : BoBox.Graph.Vertex
    {
        public V(Int32 id)
        {
            this.VertexId = id;
        }

        [DataMember]
        public string L { get; set; }

        public override string ToString()
        {
            return string.Format("{0}", L);
        }
    }




    class Program
    {
        
        

        static void Main(string[] args)
        {
            
            BoBox.Graph.Graph root = new BoBox.Graph.Graph();
            

            
            BoBox.Graph.Subgraph s1 = new BoBox.Graph.Subgraph();
            BoBox.Graph.Subgraph s2 = new BoBox.Graph.Subgraph();

            BoBox.Graph.Subgraph s3 = new BoBox.Graph.Subgraph();

            var s = new V(0) { L = "1" };
            root.AddVertex(s);

            s1.AddVertex(new V(1) { L = "3" });
            s1.AddVertex(new V(2) { L = "14" });
            
            s2.AddVertex(new V(3) { L = "10" });
            s2.AddVertex(new V(4) { L = "11" });
            s2.AddVertex(new V(5) { L = "12" });
            s2.AddVertex(new V(6) { L = "13" });

            s1.AddVertex(new V(7) { L = "6" });

            
            var t = new V(8) { L = "15" };
            root.AddVertex(t);

            s3.AddVertex(new V(9) { L = "N" });

            root.AddVertex(s1);
            root.AddVertex(s3);
            s1.AddVertex(s2);            

            root.AddEdge(0, 1);
            root.AddEdge(1, 2);
            root.AddEdge(2, 3);
            root.AddEdge(3, 4);
            root.AddEdge(3, 5);
            root.AddEdge(4, 6);
            root.AddEdge(5, 6);
            root.AddEdge(5, 9);
            root.AddEdge(6, 7);
            root.AddEdge(7, 8);
           
            // topology order
            // Ziskat z grafu, s a t a nasmerovat na ne source a target
            root.Source.AddOutEdge(s);
            t.AddOutEdge(root.Target);

            BFS(root.Source);
            GraphBuilder.Serialize<BoBox.Graph.Graph>(root, "kuk3.bab");


            //MakeGraph("kuk.bab");
            //var g2 = GraphBuilder.Deserialize<GraphJson>("kuk2.bab");                      
        }

        static void BFS(BoBox.Graph.Interface.IVertex source)
        {
            Queue<BoBox.Graph.Interface.IVertex> queue_ = new Queue<BoBox.Graph.Interface.IVertex>();
            BFS_InsertChildren(source.EdgesOut, queue_);

            while (queue_.Count > 0)
            {
                var vertex = queue_.Dequeue();
                BFS_InsertChildren(vertex.EdgesOut, queue_);
                // neco udelej
                if (vertex is BoBox.Graph.Interface.IGraph)
                {
                    Console.WriteLine();
                    BFS(((BoBox.Graph.Interface.IGraph)vertex).Source);
                    Console.WriteLine();
                }
                else
                {
                    Console.Write("{0} ", vertex.VertexId);
                }
                
            }

        }

        static void BFS_InsertChildren(IEnumerable<BoBox.Graph.Interface.IVertex> children, Queue<BoBox.Graph.Interface.IVertex> queue)
        {
            foreach (var child in children)
            {
                queue.Enqueue(child);
            }
        }

        static void MakeGraph(string path)
        {
            /*
            NodeList nodes = new NodeList(10);

            MetaGraph g = new MetaGraph();

            g.MetaNodes.Add(nodes.GetNodeById(1));
            g.MetaNodes.Add(nodes.GetNodeById(9));


            Subgraph sg1 = new Subgraph();
            sg1.MetaNodes.Add(nodes.GetNodeById(2));
            sg1.MetaNodes.Add(nodes.GetNodeById(3));
            sg1.MetaNodes.Add(nodes.GetNodeById(8));

            Subgraph sg2 = new Subgraph();
            sg2.MetaNodes.Add(nodes.GetNodeById(4));
            sg2.MetaNodes.Add(nodes.GetNodeById(5));
            sg2.MetaNodes.Add(nodes.GetNodeById(6));
            sg2.MetaNodes.Add(nodes.GetNodeById(7));

            sg1.MetaNodes.Add(sg2);

            g.MetaNodes.Add(sg1);

            g.AddEdges(1, 2);
            g.AddEdges(2, 3);
            g.AddEdges(3, 4);
            g.AddEdges(4, 5);
            g.AddEdges(4, 6);
            g.AddEdges(5, 7);
            g.AddEdges(6, 7);
            g.AddEdges(7, 8);
            g.AddEdges(8, 9);

            GraphBuilder.Serialize(g, path);
            */
        }
    }

    class A
    {
        public IList<BoxJson> Boxes { get; set; }
    }

    class GraphBuildTest
    {
        public void BuildFromString()
        {
           var c= @"
""graph"":
{
    ""node"":{""Id"":1, ""Label"":""Label A""},
    ""node"":{""Id"":2, ""Label"":""Label B""},
    ""subgraph"": 
    {
        ""node"":{""Id"":3, ""Label"":""Label C""},
        ""node"":{""Id"":4, ""Label"":""Label E""}
    },
    ""node"":{""Id"":5, ""Label"":""Label F""},
    ""node"":{""Id"":6, ""Label"":""Label G""},
    ""node"":{""Id"":7, ""Label"":""Label H ""},
    ""edges"":
    {
        1:[2],
        2:[3,4],
        3:[5],
        4:[6],
        5:[7],
        6:[7]
    }
}
";
            var a = @"{
  ""boxes"":[{""id"":""1"", ""subboxes"":[], ""edges"":[2,3]},
  {""id"":""2"", ""subboxes"":[], ""edges"":[4]},
  {""id"":""3"", ""subboxes"":[], ""edges"":[4]},  
  {""id"":""4"", ""subboxes"":[], ""edges"":[]}]}
";
            var box = a.FromJson<A>();

  //          BoBoxGraph.Graph g = new BoBoxGraph.Graph(box, 5);
            
        }
        public void Build()
        {
            Box b0 = new Box() { Id = 0, Subboxes = new List<int> { 1, 2, 3, 4 } };
            Box b1 = new Box() { Id = 1, Edges = new List<int>() { 2, 3 } };
            Box b2 = new Box() { Id = 2, Edges = new List<int>() { 4 } };
            Box b3 = new Box() { Id = 3, Edges = new List<int>() { 4 } };
            Box b4 = new Box() { Id = 4, Edges = new List<int>() { } };

            GraphBuilder2 g = new GraphBuilder2(4);
            g.InsertBox(b0);
            g.InsertBox(b1);
            g.InsertBox(b2);
            g.InsertBox(b3);
            g.InsertBox(b4);


        }
    }
}
