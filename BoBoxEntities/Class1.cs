using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Runtime.Serialization;

namespace BoBoxEntities
{
    [DataContract]
    public class BoxJson
    {
        [DataMember]
        public int Id { get; set; }
        [DataMember]
        public IList<BoxJson> SubBoxes { get; set; }
        [DataMember]
        public IList<int> Edges { get; set; }
    }


    public class Envelope
    {
        public int Id { get; set; }
        public string Data { get; set; }
    }

    public class Box
    {
        public int Id { get; set; }        
        public IList<int> Edges { get; set; }
        public IList<int> Subboxes { get; set; }
    }

    class BoundleBox
    {
        IList<int> Boxes { get; set; }
    }

    public class GraphBuilder2
    {
        public IList<Box> Boxes { get; set; }        

        public GraphBuilder2(int boxCount)
        {
            Boxes = new List<Box>(boxCount + 1);
        }

        public void InsertBox(Box box)
        {
            Boxes[box.Id] = box;
        }
    }
}
