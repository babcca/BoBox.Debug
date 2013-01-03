using System.Collections.Generic;
using System.Windows;
using BoBox.Graph.Interface;

namespace BoBox.Algorithms
{

    public abstract class LayoutAlgorithmBase<TGraph> : AlgorithmBase, ILayoutAlgorithm<TGraph>
        where TGraph : IVerticesCollection 
    {
        private TGraph graph_;
        private readonly Dictionary<IVertex, Point> vertexPositions_ = new Dictionary<IVertex, Point>();

        public IDictionary<IVertex, Point> VertexPositions
        {
            get { return vertexPositions_; }
        }

        public TGraph VisitedGraph
        {
            get { return graph_; }            
        }

        public LayoutAlgorithmBase(TGraph graph)
        {
            graph_ = graph;
        }
    }

    public abstract class AlgorithmBase : IAlgorithm
    {
        public void Compute()
        {
            BeginCompute();
            InternalCompute();
            EndCompute();
        }

        protected abstract void InternalCompute();


        protected virtual void BeginCompute()
        {
            System.Diagnostics.Debug.WriteLine("Computing start");
        }

        protected virtual void EndCompute()
        {
            System.Diagnostics.Debug.WriteLine("Computing end");
        }
    }


    public interface ILayoutAlgorithm<TGraph> : IGraphAlgorithm<TGraph>
        where TGraph : IVerticesCollection
    {
        IDictionary<IVertex, Point> VertexPositions { get; }
    }


    public interface IGraphAlgorithm<TGraph> : IAlgorithm
    {
        TGraph VisitedGraph { get; }
    }

    public interface IAlgorithm
    {
        /// <summary>
        /// Begin algorithm computing
        /// </summary>
        void Compute();
    }
}
