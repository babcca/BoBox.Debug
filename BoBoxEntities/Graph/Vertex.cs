using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BoBox.Graph
{
    using BoBox.Graph.Interface;
    using System.Runtime.Serialization;

    [DataContract]
    public class Vertex : IVertex, IType
    {
        private readonly IList<IVertex> edgesOut_ = new List<IVertex>();
        private readonly IList<IVertex> edgesIn_ = new List<IVertex>();
        
        [IgnoreDataMember]
        public IGraph ParentGraph { get; set; }

        [IgnoreDataMember]
        public bool Visible { get; set; }

        [DataMember]
        public virtual string Type { get { return "vertex"; } }
        
        [DataMember]
        public Int32 VertexId { get; protected set; }

        public Vertex()
        {
            GenerateVertexId();
        }

        public void GenerateVertexId()
        {
            VertexId = Utils.IdGenerator.UniqueId;
        }


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
    

IEnumerable<IVertex<IVertex>>  IVertex<IVertex>.EdgesOut
{
	get { throw new NotImplementedException(); }
}

IEnumerable<IVertex<IVertex>>  IVertex<IVertex>.EdgesIn
{
	get { throw new NotImplementedException(); }
}

IParentGraph<IVertex>  IVertex<IVertex>.ParentGraph
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

IParentGraph<IVertex>  IHasParent<IVertex>.ParentGraph
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


public void  AddInEdge(IVertex<IVertex> sourceVertex)
{
 	throw new NotImplementedException();
}


public void AddOutEdge(IVertex<IVertex> targetVertex)
{
    throw new NotImplementedException();
}
    }
}
