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

namespace BoBox.Controls
{
    /// <summary>
    /// Follow steps 1a or 1b and then 2 to use this custom control in a XAML file.
    ///
    /// Step 1a) Using this custom control in a XAML file that exists in the current project.
    /// Add this XmlNamespace attribute to the root element of the markup file where it is 
    /// to be used:
    ///
    ///     xmlns:MyNamespace="clr-namespace:BoBox.Controls"
    ///
    ///
    /// Step 1b) Using this custom control in a XAML file that exists in a different project.
    /// Add this XmlNamespace attribute to the root element of the markup file where it is 
    /// to be used:
    ///
    ///     xmlns:MyNamespace="clr-namespace:BoBox.Controls;assembly=BoBox.Controls"
    ///
    /// You will also need to add a project reference from the project where the XAML file lives
    /// to this project and Rebuild to avoid compilation errors:
    ///
    ///     Right click on the target project in the Solution Explorer and
    ///     "Add Reference"->"Projects"->[Select this project]
    ///
    ///
    /// Step 2)
    /// Go ahead and use your control in the XAML file.
    ///
    ///     <MyNamespace:CustomControl1/>
    ///
    /// </summary>
    /// VertexControl je dekorator nad IVertex
    public abstract class VertexControl : Control
    {
        static VertexControl()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(VertexControl), new FrameworkPropertyMetadata(typeof(VertexControl)));
        }

        
        public VertexControl()
            : base()
        {
            Envelopes = new Queue<BoBox.Commands.Envelope>();           
        }

        public VertexControl(Graph.Interface.IVertex vertex)
            : this()
        {
            Vertex = vertex;
            
        }
        

        #region IVertex decorator
        public Graph.Interface.IVertex Vertex { get; set; }

        public Int32 VertexId { get { return Vertex.VertexId; } }        
        #endregion

        #region Dependency properties
        public double X { get; set; }
        public double Y { get; set; }
        /*
        public double X
        {            
            get { return (double)GetValue(XProperty); }
            set { SetValue(XProperty, value); }        
        }

        public static readonly DependencyProperty XProperty =
            DependencyProperty.Register("X", typeof(double), typeof(VertexControl), new UIPropertyMetadata(0));

        public double Y
        {
            get { return (double)GetValue(YProperty); }
            set { SetValue(YProperty, value); }
        }

        public static readonly DependencyProperty YProperty =
            DependencyProperty.Register("Y", typeof(double), typeof(VertexControl), new UIPropertyMetadata(0));
        */
        public string Label
        {
            get { return (string)GetValue(LabelProperty); }
            set { SetValue(LabelProperty, value); }
        }

        public static readonly DependencyProperty LabelProperty =
            DependencyProperty.Register("Label", typeof(string), typeof(VertexControl), new UIPropertyMetadata(null));

        public Queue<BoBox.Commands.Envelope> Envelopes
        {
            get { return (Queue<BoBox.Commands.Envelope>)GetValue(EnvelopesProperty); }
            set { SetValue(EnvelopesProperty, value); }
        }

        public static readonly DependencyProperty EnvelopesProperty =
            DependencyProperty.Register("Envelopes", typeof(Queue<BoBox.Commands.Envelope>), typeof(VertexControl), new UIPropertyMetadata(null));

        #endregion      
    }
}
