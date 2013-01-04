using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Runtime.Serialization;

namespace BoBox.Graph
{
    using BoBox.Graph.Interface;
    using System.Diagnostics.Contracts;

    [DataContract]
    public class Subgraph : ISubgraph, ISourceTargetGraph, IType
    {
        #region IVertex implementation
        
        private readonly IList<IVertex> edgesOut_ = new List<IVertex>();
        private readonly IList<IVertex> edgesIn_ = new List<IVertex>();        

        [IgnoreDataMember]
        public Int32 VertexId { get; private set; }

        [IgnoreDataMember]
        public bool Visible { get; set; }

        public IEnumerable<IVertex> EdgesOut
        {
            get { return edgesOut_; }
        }

        public IEnumerable<IVertex> EdgesIn
        {
            get { return edgesIn_; }
        }

        public void AddOutEdge(IVertex targetVertex)
        {
            edgesOut_.Add(targetVertex);
        }
        public void AddInEdge(IVertex sourceVertex)
        {
            edgesIn_.Add(sourceVertex);
        }
        public IParentGraph ParentGraph { get; set; }
        #endregion

        #region IGraph implementation
        private readonly IList<IVertex> vertices_ = new List<IVertex>();
        private readonly IList<IVertex> sources_ = new List<IVertex>();
        private readonly IList<IVertex> targets_= new List<IVertex>();
        private readonly IVertex source_ = new Vertex();
        private readonly IVertex target_ = new Vertex();

        [IgnoreDataMember]
        public Int32 GraphDeep { get; private set; }
        
        [IgnoreDataMember]
        public Int32 GraphId { get; private set; }
        
        [IgnoreDataMember]
        public IEnumerable<IVertex> AllVertices { get {
            return new List<IVertex>();
        } }                 

        [DataMember]
        public IEnumerable<IVertex> Vertices
        {
            get { return vertices_; }
        }

        #endregion

        #region IType implementation
        [DataMember]
        public string Type { get { return "subgraph"; } }
        #endregion

        public bool isSubgraph { get; set; }
        public Subgraph()
        {
            GraphDeep = 0;
            GraphId = Utils.IdGenerator.UniqueId;
            VertexId = Utils.IdGenerator.UniqueId;                      
        }
        
        public void AddVertex(IVertex vertex)
        {
            Contract.Requires(vertex != null);
            vertex.ParentGraph = this;
            vertices_.Add(vertex);
        }


        public IEnumerable<IVertex> Sources
        {
            get { return sources_; }
        }

        public IEnumerable<IVertex> Targets
        {
            get { return targets_; }
        }

        public void AddTargetVertex(IVertex source)
        {
            sources_.Add(source);
        }

        public void AddSourceVertex(IVertex target)
        {
            targets_.Add(target);
        }
                                
    }
}
