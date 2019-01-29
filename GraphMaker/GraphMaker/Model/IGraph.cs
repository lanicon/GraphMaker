using System.Collections.Generic;

namespace GraphMaker.Model
{
    public enum GraphOperation
    {
        None,
        AddNode,
        AddEdge,
        DeleteNode,
        DeleteEdge
    };

    public delegate void GraphChangeEvent(GraphOperation operation, object obj);

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
