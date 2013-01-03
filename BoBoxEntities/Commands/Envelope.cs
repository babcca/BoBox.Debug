using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Runtime.Serialization;

namespace BoBox.Commands
{
    [DataContract]
    public class Envelope
    {
        [DataMember]
        public Int64 Time { get; set; }

        [DataMember]
        public string Type { get; set; }        
    }
}
