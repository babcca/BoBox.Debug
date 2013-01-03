using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BoBox.Graph.Interface
{
    public interface IVertex
    {
        Int32 VertexId { get; }
        bool Visible { get; set; }
        // Zmena, vsechny vrholy jsou v nejakem podgrafu,
        // Struktura tedy vypada takto:
        // Graf drzi podgraf (kontejner), do ktereho vklada veskere vrcholy

        //IGraph ParentGraph { get; set; }
        IGraph ParentGraph { get; set;  }        

        IEnumerable<IVertex> EdgesOut { get; }
        IEnumerable<IVertex> EdgesIn { get; }        

        void AddOutEdge(IVertex to);
    }

    public interface IBox : IVertex
    {
    }

    public interface IType
    {
        string Type { get; }
    }
}
