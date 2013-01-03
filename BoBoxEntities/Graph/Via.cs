using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BoBox.Graph
{
    using BoBox.Graph.Interface;
    using System.Runtime.Serialization;

    [DataContract]
    public class Via : Vertex
    {
        [DataMember]
        public override string Type { get { return "via"; } }
    }
}
