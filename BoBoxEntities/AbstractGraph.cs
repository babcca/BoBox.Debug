using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.ComponentModel;
using System.Runtime.Serialization;
using BoBox.Interfaces;

namespace BoBox.Entities.Abstract
{
    public class AbstractGraph : IGraph
    {
        [IgnoreDataMember]
        protected IList<INode> metaNodes_ = new BindingList<INode>();
        //protected List<INode> metaNodes_ = new List<INode>();

        [IgnoreDataMember]
        protected List<IEdge> metaEdges_ = new List<IEdge>();


        public AbstractGraph()
        {
            
                
        }

        public void AddMetaNode(object sender, ListChangedEventArgs e)
        {
            var a = "";
            //metaNodes_.Add(node);
        }

        public void Visit(IVisitor visitor)
        {
            throw new NotImplementedException();
        }
    }
}
