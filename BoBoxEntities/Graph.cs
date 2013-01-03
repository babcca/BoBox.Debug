using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using BoBox.Interfaces;
using BoBox.Utils;
using BoBox.Entities.Abstract;
using System.Runtime.Serialization;

namespace BoBox.Entities
{
    [DataContract]
    public class MetaGraph : AbstractGraph
    {
        public void AddEdges(Int64 from, Int64 to, string label = null)
        {
            
        }

    }


    public class NodeList
    {
        private INode[] nodeList;

        public NodeList(Int64 nodes)
        {
            nodeList = new Node[nodes];
            for (int i = 0; i < nodes; ++i)
            {
                nodeList[i] = new Node() { Id = i };
            }
        }

        public INode GetNodeById(Int64 id)
        {
            return nodeList[id];
        }
    }
}
