using System.Collections.Generic;
using System.Linq;

namespace GraphMaker.Model
{
    public class Graph : IGraph
    {
        // Все вершины в графе нумеруются с 0 и никогда не перенумеруются
        // (счётчик всегда увеличивается)
        private int numberCounter = 0;

        private List<INode> nodes { get; } = new List<INode>();

        public event GraphChangeEvent Changed;

        public INode this[int index] => nodes[index];

        public IReadOnlyList<INode> Nodes => nodes;

        public IReadOnlyList<IEdge> Edges
        {
            get
            {
                return nodes
                    .SelectMany(n => n.IncidentEdges)
                    .Distinct()
                    .ToList();
            }
        }

        public INode AddNode()
        {
            var node = new Node(numberCounter++);
            nodes.Add(node);
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

            nodes.Remove(v);
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
