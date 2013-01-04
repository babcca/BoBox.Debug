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
    public class Subgraph : ISubgraph, IHasParent<IVertex>, IParentGraph<IVertex>, ISourceTargetGraph<IVertex>, IType
    {
        #region IVertex implementation
        
        private readonly IList<IVertex> edgesOut_ = new List<IVertex>();
        private readonly IList<IVertex> edgesIn_ = new List<IVertex>();

        private IGraph parentGraph_;
        [IgnoreDataMember]
        public IGraph ParentGraph
        {
            get
            { return parentGraph_; }
            set
            {
                parentGraph_ = value;
                GraphDeep = value.GraphDeep + 1;
            }
        }

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

        public void AddOutEdge(IVertex to)
        {
            edgesOut_.Add(to);
        }
        #endregion

        #region IGraph implementation
        private readonly IList<IVertex> vertices_ = new List<IVertex>();
        private IVertex source_;
        private IVertex target_;

        [IgnoreDataMember]
        public Int32 GraphDeep { get; private set; }
        
        [IgnoreDataMember]
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
            
            var source = new Vertex();
            source.GenerateVertexId();
            source.ParentGraph = this;
            source_ = source;

            var target = new Vertex();
            target.GenerateVertexId();
            target.ParentGraph = this;
            target_ = target;

        }
        
        public void AddVertex(IVertex vertex)
        {
            Contract.Requires(vertex != null);
            vertex.ParentGraph = this;
            vertices_.Add(vertex);
        }

        IParentGraph<IVertex> IHasParent<IVertex>.ParentGraph
        {
            get
            {
                throw new NotImplementedException();
            }
            set
            {
                throw new NotImplementedException();
            }
        }

        public IEnumerable<IVertex> Sources
        {
            get { throw new NotImplementedException(); }
        }

        public IEnumerable<IVertex> Targets
        {
            get { throw new NotImplementedException(); }
        }

        public void AddTargetVertex(IVertex source)
        {
            throw new NotImplementedException();
        }

        public void AddSourceVertex(IVertex target)
        {
            throw new NotImplementedException();
        }


        IEnumerable<IVertex<IVertex>> IVertex<IVertex>.EdgesOut
        {
            get { throw new NotImplementedException(); }
        }

        IEnumerable<IVertex<IVertex>> IVertex<IVertex>.EdgesIn
        {
            get { throw new NotImplementedException(); }
        }

        public void AddOutEdge(IVertex<IVertex> targetVertex)
        {
            throw new NotImplementedException();
        }

        public void AddInEdge(IVertex<IVertex> sourceVertex)
        {
            throw new NotImplementedException();
        }
    }
}
