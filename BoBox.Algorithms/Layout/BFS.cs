using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BoBox.Algorithms.Layout
{
    using BoBox.Graph.Interface;

    public class BFS<TGraph> : LayoutAlgorithmBase<TGraph>
        where TGraph : IVerticesCollection
    {
        public BFS(TGraph graph)
            : base(graph)
        {
        }

        protected override void InternalCompute()
        {
            // find source using topological sort   
            List<List<IVertex>> layers = new List<List<IVertex>>();
            var source = VisitedGraph.Vertices.First();
            layers.Add(new List<IVertex>() { source });
            /*
            while (layers.Last().Count > 0)
            List<IVertex> newLayer = new List<IVertex>();
            foreach (var vertex in layers.Last())
            {
                newLayer.AddRange(vertex.EdgesOut);
            }
            layers.Add(newLayer);


            

            Queue<IVertex> queue = new Queue<IVertex>();
            queue.Enqueue(source);
            */
            
        }
    }
}
