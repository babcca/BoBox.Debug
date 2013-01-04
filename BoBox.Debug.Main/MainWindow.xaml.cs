using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace BoBox.Debug.Main
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            
            BoBox.Graph.Graph root = new BoBox.Graph.Graph();
            var v1 = new BoBox.Graph.Box();
            root.AddVertex(v1);

            BoBox.Graph.Subgraph s1 = new BoBox.Graph.Subgraph();
            var sv1 = new BoBox.Graph.Box();
            s1.AddVertex(sv1);
            BoBox.Graph.Subgraph s2 = new BoBox.Graph.Subgraph();
            var sv2 = new BoBox.Graph.Box();
            s2.AddVertex(sv2);

            s1.AddVertex(s2);

            root.AddVertex(s1);
            var v2 = new BoBox.Graph.Box();
            root.AddVertex(v2);
            root.AddVertex(new BoBox.Graph.Via());

            root.AddEdge(v1.VertexId, sv1.VertexId);
            root.AddEdge(sv1.VertexId, sv2.VertexId);
            //root.AddEdge(sv2.VertexId, v2.VertexId);
            MyCanvas.Graph = root;
            //var v = new BoBox.Controls.VertexControl() { Label = "Kuk svete", Background = Brushes.Aqua };
            //MyCanvas.Children.Add(v);
            
        }
    }
}
