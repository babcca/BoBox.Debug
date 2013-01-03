using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using BoBox.Interfaces;
using System.Runtime.Serialization;


namespace BoBox.Entities
{
    [DataContract]        
    public class Node : INode
    {
        [DataMember]
        public string Type { get { return "node"; } }
        
        [DataMember]
        public Int64 Id { get; set; }

        [IgnoreDataMember]
        public int ParentId { get; set; }

        [IgnoreDataMember]
        public int Deep { get; set; }

        public void Visit(IVisitor visitor)
        {
            throw new NotImplementedException();
        }        
    }    

}
