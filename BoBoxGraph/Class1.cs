using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using BoBoxEntities;

namespace BoBoxGraph
{

    public class Graph
    {
        private IList<BoxJson> Boxes;        
        private BoxJson MainBox;
        public Graph(BoxJson root, int count)
        {
            Boxes = new List<BoxJson>(count);
            MainBox = root;
            Linearize(new List<BoxJson>() { MainBox });
        }

        private void Linearize(IList<BoxJson> boxes)
        {                        
            foreach (var box in boxes)
            {
                Boxes[box.Id] = box;
                Linearize(box.SubBoxes);
            }
        }

        public string Draw(IList<BoxJson> boxes)
        {
            StringBuilder sb = new StringBuilder();
            foreach (var box in boxes)
            {
                sb.AppendFormat("{0} : [{1}]", box.Id, Draw(box.SubBoxes));
            }
            return sb.ToString();
        }
    }
}
