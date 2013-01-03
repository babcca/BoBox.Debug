using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BoBox.Graph.Interface
{
    public interface IVerticesCollection
    {
        IEnumerable<IVertex> Vertices { get; }
        void AddVertex(IVertex vertex);
    }
}
