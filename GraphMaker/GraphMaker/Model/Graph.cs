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

        protected virtual void OnChanged()
        {
            Changed?.Invoke();
        }

        public INode AddNode()
        {
            var node = new Node(numberCounter++);
            nodes.Add(node);
            OnChanged();
            return node;
        }

        public void DeleteNode(INode node)
        {
            // Удаляем все инцидентные рёбра (вместе с ними и вершины)
            for (var i = node.IncidentEdges.Count - 1; i >= 0; i--)
            {
                var edge = node.IncidentEdges[i];
                Node.Disconnect(edge);
            }

            nodes.Remove(node);
            OnChanged();
        }

        public IEdge AddEdge(INode first, INode second, int length = 1)
        {
            var edge = Node.Connect(first, second, length);
            OnChanged();
            return edge;
        }

        public void DeleteEdge(IEdge edge)
        {
            Node.Disconnect(edge);
            OnChanged();
        }
    }
}
