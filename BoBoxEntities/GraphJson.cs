using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Runtime.Serialization;
using System.ComponentModel;
using BoBox.Interfaces;

namespace BoBox.Entities
{

    [DataContract]    
    public class GraphJson : IGraph
    {
        [DataMember(Name = "Nodes")]
        public IList<INode> MetaNodes { get; set; }

        [DataMember(Name="NodesCount")]
        public int MetaNodesCount { get; set; }
       
        [DataMember(Name = "Edges")]
        public IList<EdgeJson> MetaEdges { get; private set; }
    }

    [DataContract]
    public class SubgraphJson : IGraph,INode
    {
        [DataMember(Name = "Nodes")]
        public IList<INode> MetaNodes { get; set; }

        [DataMember]
        public string Type { get { return "subgraph"; } }

        public int ParentId { get; set; }

        public void Visit(IVisitor visitor)
        {
            throw new NotImplementedException();
        }
    }

    [DataContract]
    public class NodeJson : INode
    {
        [DataMember]
        public string Type { get { return "node"; } }

        [DataMember]
        public Int64 Id { get; set; }

        public int ParentId { get; set; }

        public void Visit(IVisitor visitor)
        {
            throw new NotImplementedException();
        }        
    }

    [DataContract]
    public class EdgeJson
    {
        [DataMember]
        public Int64 From { get; set; }
        [DataMember]
        public Int64 To { get; set; }
        [DataMember]
        public string Label { get; set; }
    }

    public class NodeFactory
    {
        public static INode JsonNodeFactory(string type)
        {
            switch (type.ToLower())
            {
                case "node":
                    return new NodeJson();
                case "subgraph":
                    return new SubgraphJson();
                default:
                    throw new Exception(string.Format("Type {0} is not supported", type));
            }
        }
    }
}
