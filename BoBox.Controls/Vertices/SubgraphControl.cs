using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Windows;
using System.Windows.Controls;
using BoBox.Graph.Interface;

namespace BoBox.Controls.Vertices
{
    public class SubgraphControl : VertexControl
    {
        static SubgraphControl()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(SubgraphControl), new FrameworkPropertyMetadata(typeof(SubgraphControl)));
        }

        public SubgraphControl()
            : base()
        { }

        public SubgraphControl(ISubgraph subgraph)
            : base((IVertex) subgraph)
        {
            Graph = (IVerticesCollection)subgraph;
            Label = string.Format("subgraph {0}", Vertex.VertexId.ToString());
        }
        
        #region IGraph decorator
        #endregion

        public IVerticesCollection Graph
        {
            get { return (IVerticesCollection)GetValue(GraphProperty); }
            set { SetValue(GraphProperty, value); }
        }

        public static readonly DependencyProperty GraphProperty =
            DependencyProperty.Register("Graph", typeof(IVerticesCollection), typeof(SubgraphControl), new UIPropertyMetadata(null));
        
        public bool IsExpanded
        {
            get { return (bool)GetValue(IsExpandedProperty); }
            set { SetValue(IsExpandedProperty, value); }
        }

        public static readonly DependencyProperty IsExpandedProperty =
            DependencyProperty.Register("IsExpanded", typeof(bool), typeof(SubgraphControl), new UIPropertyMetadata(false));
       
    }
}
