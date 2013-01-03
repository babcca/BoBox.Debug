using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Windows;
using System.Windows.Controls;


namespace BoBox.Controls
{
    using BoBox.Graph.Interface;

    public class LayoutControl : GraphLayout<BoBox.Graph.Interface.IVerticesCollection>
    {

    }

    public class GraphControl : GraphLayout<BoBox.Graph.Graph>
    {
        public GraphControl()
        {
            if (System.ComponentModel.DesignerProperties.GetIsInDesignMode(this))
            {
                BoBox.Graph.Graph root = new BoBox.Graph.Graph();
                root.AddVertex(new BoBox.Graph.Box());
                
                BoBox.Graph.Subgraph s1 = new BoBox.Graph.Subgraph();
                s1.AddVertex(new BoBox.Graph.Box());
                BoBox.Graph.Subgraph s2 = new BoBox.Graph.Subgraph();
                s2.AddVertex(new BoBox.Graph.Box());

                s1.AddVertex(s2);

                root.AddVertex(s1);
                root.AddVertex(new BoBox.Graph.Box());
                root.AddVertex(new BoBox.Graph.Via());

                

                Graph = root;
            }
        }
    }


    public class GraphLayout<TGraph> : Panel
        where TGraph : IVerticesCollection
    {
        private readonly IList<VertexControl> vertices = new List<VertexControl>();
        private BoBox.Algorithms.ILayoutAlgorithm<TGraph> layout;
        
        #region Dependenci Property        
        public TGraph Graph
        {
            get { return (TGraph)GetValue(GraphProperty); }
            set { SetValue(GraphProperty, value); }
        }

        public static readonly DependencyProperty GraphProperty =
            DependencyProperty.Register("Graph", typeof(TGraph), typeof(GraphLayout<TGraph>), new FrameworkPropertyMetadata(new Graph.Graph(), FrameworkPropertyMetadataOptions.AffectsRender, Graph_PropertyChanged));

        protected static void Graph_PropertyChanged(DependencyObject obj, DependencyPropertyChangedEventArgs e)
        {
            var self = (GraphLayout<TGraph>)obj;
            self.OnGraphUpdate();
        }

        #endregion

        private void OnGraphUpdate()
        {
            // [MARK]
            layout = new BoBox.Algorithms.Layout.Random.RandomLayoutAlgorithm<TGraph>(Graph);
            layout.Compute();
            
            // Earse all controls
            vertices.Clear();
            Children.Clear();

            // Create graph controls
            // [TODO] Make it better, Visitr, Factory
            foreach (var vertex in Graph.Vertices)
            {
                if (vertex is BoBox.Graph.Subgraph)
                {
                    var v = (BoBox.Graph.Subgraph)vertex;
                    var control = new Vertices.SubgraphControl(v);
                    control.X = layout.VertexPositions[v].X;
                    control.Y = layout.VertexPositions[v].Y;
                    vertices.Add(control);
                    Children.Add(control);
                }
                else if (vertex is BoBox.Graph.Box)
                {
                    var v = (BoBox.Graph.Box)vertex;
                    var control = new Vertices.BoxControl(v);
                    control.Envelopes.Enqueue(new Commands.Envelope() { Time = 1, Type = "Data" });
                    control.Envelopes.Enqueue(new Commands.Envelope() { Time = 5, Type = "Data" });
                    control.Envelopes.Enqueue(new Commands.Envelope() { Time = 8, Type = "Data" });
                    control.Envelopes.Enqueue(new Commands.Envelope() { Time = 9, Type = "Data" });
                    control.Envelopes.Enqueue(new Commands.Envelope() { Time = 11, Type = "Data" });

                    control.X = layout.VertexPositions[v].X;
                    control.Y = layout.VertexPositions[v].Y;                    
                    
                    vertices.Add(control);
                    Children.Add(control);
                }
                else if (vertex is BoBox.Graph.Via)
                {
                    var v = (BoBox.Graph.Via)vertex;
                    var control = new Vertices.ViaControl(v);

                    control.X = layout.VertexPositions[v].X;
                    control.Y = layout.VertexPositions[v].Y;
                    
                    vertices.Add(control);
                    Children.Add(control);
                }                                
            }
   
            // Update layput
            OnUpdateLayout();
        }

        private void OnUpdateLayout()
        {
            // Recompute layout
            ComputeLayout();
        }

        private void ComputeLayout()
        {
            UpdateLayout();
        }
        
        // http://msdn.microsoft.com/en-us/library/ms754152.aspx#Panels_custom_panel_elements
        /// <summary>
        /// Slouzi pro urceni velikosti uzlu
        /// </summary>
        /// <param name="constraint"></param>
        /// <returns></returns>
        protected override Size MeasureOverride(Size constraint)
        {                        
            double sizeX = 0;
            double sizeY = 0;


            foreach (UIElement child in InternalChildren)
            {
                child.Measure(constraint);
                if (child is VertexControl)
                {
                    var vertex = (VertexControl)child;

                    sizeX = Math.Max(sizeX, child.DesiredSize.Width + vertex.X);
                    sizeY = Math.Max(sizeY, child.DesiredSize.Height + vertex.Y);
                }
                else
                {
                    sizeX = Math.Max(sizeX, child.DesiredSize.Width);
                    sizeY = Math.Max(sizeY, child.DesiredSize.Height);
                }
            }

            return new Size(sizeX, sizeY);
        }

        /// <summary>
        /// Slouzi pro umisteni uzlu
        /// </summary>
        /// <param name="arrangeSize"></param>
        /// <returns></returns>
        protected override Size ArrangeOverride(Size arrangeSize)
        {            
            foreach (var vertex in vertices)
	        {
                vertex.Arrange(new Rect(new Point(vertex.X, vertex.Y), vertex.DesiredSize));
        	}

            return arrangeSize; // Returns the final Arranged size
        }
    }
}
