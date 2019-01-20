using System.Collections.Generic;
using System.Linq;

namespace GraphMaker.Model
{
    public class Graph : IGraph
    {
        public INode this[int index] => Nodes[index];

        public List<INode> Nodes { get; } = new List<INode>();

        public List<IEdge> Edges
        {
            get
            {
                return Nodes
                    .SelectMany(n => n.IncidentEdges)
                    .Distinct()
                    .ToList();
            }
        }

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
            // Удаляем все инцидентные рёбра (вместе с ними и вершины)
            for (var i = v.IncidentEdges.Count - 1; i >= 0; i--)
            {
                var edge = v.IncidentEdges[i];
                Node.Disconnect(edge);
            }

            Nodes.Remove(v);
            Changed?.Invoke();
        }

        public IEdge AddEdge(INode v1, INode v2)
        {
            var edge = Node.Connect(v1, v2);
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
