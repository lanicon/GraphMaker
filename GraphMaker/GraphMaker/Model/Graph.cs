using System.Collections.Generic;

namespace GraphMaker.Model
{
    public class Graph : IGraph
    {
        public INode this[int index] => Nodes[index];

        public List<INode> Nodes { get; } = new List<INode>();

        public List<IEdge> Edges { get; } = new List<IEdge>();

        public event GraphChangeEvent Changed;

        public INode AddNode()
        {
            var node = new Node(Nodes.Count);
            Nodes.Add(node);
            Changed?.Invoke();
            return node;
        }

        public void DeleteNode(INode v)
        {
            Nodes.Remove(v);
            Changed?.Invoke();
        }

        public IEdge AddEdge(INode v1, INode v2)
        {
            var edge = Node.Connect(v1, v2);
            Edges.Add(edge);
            Changed?.Invoke();
            return edge;
        }

        public void DeleteEdge(IEdge edge)
        {
            Node.Disconnect(edge);
            Changed?.Invoke();
        }
    }
}
