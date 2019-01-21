using System.Collections.Generic;

namespace GraphMaker
{
    public delegate void GraphChangeEvent();

    public interface IGraph
    {
        INode AddNode();

        void DeleteNode(INode v);

        IEdge AddEdge(INode v1, INode v2);

        void DeleteEdge(IEdge edge);

        IReadOnlyList<INode> Nodes { get; }

        IReadOnlyList<IEdge> Edges { get; }

        event GraphChangeEvent Changed;
    }
}
