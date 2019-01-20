using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GraphMaker
{
    public delegate void GraphChangeEvent();

    public interface IGraph
    {
        INode AddNode();

        void DeleteNode(INode v);

        IEdge AddEdge(INode v1, INode v2);

        void DeleteEdge(IEdge edge);

        List<INode> Nodes { get; }

        List<IEdge> Edges { get; }

        event GraphChangeEvent Changed;
    }
}
