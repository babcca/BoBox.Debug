using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Windows;
using System.Windows.Controls;
using BoBox.Graph.Interface;

namespace BoBox.Controls.Vertices
{
    public class BoxControl : VertexControl
    {
        static BoxControl()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(BoxControl), new FrameworkPropertyMetadata(typeof(BoxControl)));
        }

        public BoxControl()
            : base()
        { }

        public BoxControl(BoBox.Graph.Box box)
            : base(box)
        {
            Label = string.Format("Box {0}", Vertex.VertexId.ToString());
        }        
    }
}
