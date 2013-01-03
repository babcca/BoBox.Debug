﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BoBox.Graph
{
    using System.Diagnostics.Contracts;
    using System.Runtime.Serialization;
    using BoBox.Graph.Interface;

    [DataContract]
    public class Graph : IGraph, IEdgesAndVerticesCollection
    {
        private readonly IList<IEdge<IVertex>> edges_ = new List<IEdge<IVertex>>();
        private readonly IList<IVertex> vertices_ = new List<IVertex>();
        private IList<IVertex> verticesList_ = null;

        private IVertex source_;
        private IVertex target_;

        public Graph()
        {
            GraphDeep = 0;
            GraphId = Utils.IdGenerator.UniqueId;

            var source = new Vertex();
            source.GenerateVertexId();
            source.ParentGraph = this;
            source_ = source;

            var target = new Vertex();
            target.GenerateVertexId();
            target.ParentGraph = this;
            target_ = target;
        }

        [IgnoreDataMember]
        public Int32 GraphDeep { get; private set; }

        [DataMember]
        public Int32 GraphId { get; private set; }

        [IgnoreDataMember]
        public IVertex Source 
        { 
            get { return source_; }
        }
        
        [IgnoreDataMember]
        public IVertex Target 
        {
            get { return target_; }            
        }

        [IgnoreDataMember]
        public IEnumerable<IVertex> VerticesList
        {
            get 
            {
                if (verticesList_ == null)
                {
                    verticesList_ = LinearizeVertices();
                }
                return verticesList_;
            }
        }
        
        [DataMember(Order=0)]        
        public IEnumerable<IVertex> Vertices
        {
            get { return vertices_; }
        }

        [DataMember(Order=1)]
        public Int32 VerticesCount
        {
            get { return VerticesList.Count(); }
            set { Utils.IdGenerator.Seed = value + 10; }
        }

        [DataMember(Order = 2)]
        public IEnumerable<IEdge<IVertex>> Edges
        {
            get { return edges_; }
        }

        public void AddVertex(IVertex vertex)
        {
            Contract.Requires(vertex != null);
            vertex.ParentGraph = this;
            vertices_.Add(vertex);
        }

        /*
        public void AddEdge(IEdge<IVertex> edge)
        {            
            var from = edge.Source;
            var to = edge.Destination;

            var deepDiff = from.ParentGraph.GraphDeep - to.ParentGraph.GraphDeep;
            var sameSurface = deepDiff == 0; // jsme na stejne rovine, rovny smer
            var fromIsUnderTo = deepDiff < 0; // vstupujem do podgrafu smereme nahoru
            var fromUpperTo = deepDiff > 0; // vyspupujeme z podgrafu smerem dolu


            var sameGraph = from.ParentGraph.GraphId == to.ParentGraph.GraphId;

            if (sameGraph)
            {
                from.AddOutEdge(to);
            }           
            else if (fromIsUnderTo)
            {
                if (to.ParentGraph is IVertex)
                {
                    from.AddOutEdge((IVertex) to.ParentGraph);
                }

                to.ParentGraph.Source.AddOutEdge(to);
            }
            else if (from.ParentGraph.GraphDeep > to.ParentGraph.GraphDeep)
            {
                if (from.ParentGraph is IVertex)
                {
                    // Povede bud do bodu
                    ((IVertex)from.ParentGraph).AddOutEdge(to);
                    // nebo na kraj bohu
                    ((IVertex)from.ParentGraph).AddOutEdge((IVertex)to.ParentGraph);
                }
                from.AddOutEdge(from.ParentGraph.Target);
            }
            else
            {
                throw new Exception("Unresolved graph relationship");
            }
            edges_.Add(edge);
        }
        */

        public void AddEdge(IEdge<IVertex> edge)
        {
            edge.Source.AddOutEdge(edge.Destination);
        }

        public void AddEdge(Int32 from, Int32 to)
        {
            try
            {
                var fromVertex = VerticesListSelector(vertices => vertices.Where(v => v.VertexId == from).First());
                var toVertex = VerticesListSelector(vertices => vertices.Where(v => v.VertexId == to).First());
                var edge = new Edge(fromVertex, toVertex);
                AddEdge(edge);
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex);
            }

            
        }

        public IVertex VerticesListSelector(Func<IEnumerable<IVertex>, IVertex> where)
        {
            return where(VerticesList);
        }        

        public void BuildGraphStructure()
        {
            var ver = LinearizeVertices();
            foreach (var edge in edges_)
            {
                ver.Where(v => v.VertexId == edge.Source.VertexId);
                var from =  edge.Source;
                var to = edge.Destination;
                if (from.ParentGraph.GraphDeep == to.ParentGraph.GraphDeep)
                {
                    from.AddOutEdge(to);                    
                }
                else if (from.ParentGraph.GraphDeep < to.ParentGraph.GraphDeep)
                {
                    from.AddOutEdge(to);
                    to.ParentGraph.Source.AddOutEdge(to);
                }
                else if (from.ParentGraph.GraphDeep > to.ParentGraph.GraphDeep)
                {
                    from.AddOutEdge(to);
                    from.AddOutEdge(from.ParentGraph.Target);
                }

            }
        }


        public IList<IVertex> LinearizeVertices()
        {
            IList<IVertex> verticesList = new List<IVertex>();            
            LinearizeVertices(this, verticesList);

            return verticesList;
        }

        private void LinearizeVertices(IVerticesCollection graph, IList<IVertex> verticesList)
        {
            foreach (var vertex in graph.Vertices)
            {
                verticesList.Add(vertex);
                if (vertex is IVerticesCollection)
                {
                    LinearizeVertices((IVerticesCollection)vertex, verticesList);
                }                                
            }
        }        
    }
}
