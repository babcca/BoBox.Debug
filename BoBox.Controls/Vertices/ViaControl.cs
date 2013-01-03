using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Windows;
using System.Windows.Controls;
using BoBox.Graph.Interface;

namespace BoBox.Controls.Vertices
{
    public class ViaControl : VertexControl
    {
        static ViaControl()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(ViaControl), new FrameworkPropertyMetadata(typeof(ViaControl)));
        }

        public ViaControl()
            : base()
        { }

        public ViaControl(BoBox.Graph.Via box)
            : base(box)
        {
            Label = string.Format("Via {0}", Vertex.VertexId.ToString());
        }
    }
}
