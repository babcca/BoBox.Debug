using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;

namespace BoBox.Algorithms.Layout.Random
{
    using BoBox.Graph.Interface;

    public class RandomLayoutAlgorithm<TGraph> : LayoutAlgorithmBase<TGraph>
        where TGraph : IVerticesCollection        
    {

        public RandomLayoutAlgorithm(TGraph graph)
            : base(graph)
        {
        }

        protected override void  InternalCompute()
        {
            var random = new System.Random((int) DateTime.Now.Ticks);

            foreach (var vertex in VisitedGraph.Vertices)
            {
                if (vertex is ISubgraph)
                {
                    //var graph = vertex as IVerticesCollection;
                    //if (!graph.IsCollapsed)
                    //{
                    //    var b = new RandomLayoutAlgorithm<ISubgraph>(graph);
                    //}

                    double x = random.Next(0, 800);
                    double y = random.Next(0, 600);
                    VertexPositions[vertex] = new Point(x, y);

                }
                else
                {

                    double x = random.Next(0, 800);
                    double y = random.Next(0, 600);
                    VertexPositions[vertex] = new Point(x, y);
                }
            }
        }
    }
}
