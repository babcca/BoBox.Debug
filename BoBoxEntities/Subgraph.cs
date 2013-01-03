using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using BoBox.Interfaces;
using BoBox.Utils;
using System.Runtime.Serialization;
using BoBox.Entities.Abstract;

namespace BoBox.Entities
{
    [DataContract]
    public class Subgraph 
    {
        [DataMember]
        public string Type { get { return "subgraph"; } }


        public Subgraph()
        {

        }

        public int ParentId { get; set; }
    }
}
