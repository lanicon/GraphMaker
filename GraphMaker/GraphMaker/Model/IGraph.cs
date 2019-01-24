using System.Collections.Generic;

namespace GraphMaker.Model
{
    public delegate void GraphChangeEvent();

    public interface IGraph
    {
        INode AddNode();

        void DeleteNode(INode node);

        IEdge AddEdge(INode first, INode second, int length);

        void DeleteEdge(IEdge edge);

        IReadOnlyList<INode> Nodes { get; }

        IReadOnlyList<IEdge> Edges { get; }

        event GraphChangeEvent Changed;
    }
}
