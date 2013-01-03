using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


namespace BoBox.Graph
{
    using BoBox.Graph.Interface;
    using System.Runtime.Serialization;

    [DataContract]
    public class Edge : IEdge<IVertex>
    {
        
        [IgnoreDataMember]
        public IVertex Source { get; private set; }
        
        [IgnoreDataMember]
        public IVertex Destination { get; private set; }

        [DataMember]
        public string Label { get; set; }

        [DataMember]
        public Int32 SourceId { get { return Source.VertexId; } }

        [DataMember]
        public Int32 DestinationId { get { return Destination.VertexId; } }

        public Edge(IVertex source, IVertex destination)
        {
            Source = source;
            Destination = destination;
        }

        public override string ToString()
        {
            return string.Format("{0} --> {1} [{2}]", SourceId, DestinationId, Label);
        }

    }
}
