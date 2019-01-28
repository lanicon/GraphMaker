using System.Collections.Generic;

namespace GraphMaker.Model
{
    public enum GraphOperation
    {
        AddNode,
        AddEdge,
        DeleteNode,
        DeleteEdge
    };

    public delegate void GraphChangeEvent(GraphOperation operation);

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
