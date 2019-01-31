using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;

namespace GraphMaker.Model
{
    public class Graph : IGraph
    {
        // Все вершины в графе нумеруются с 0 и никогда не перенумеруются
        // (счётчик всегда увеличивается)
        [JsonProperty]
        private int numberCounter = 0;

        [JsonIgnore]
        private List<INode> nodes { get; } = new List<INode>();

        public event GraphChangeEvent Changed;

        public INode this[int index] => nodes[index];

        [JsonProperty]
        public IReadOnlyList<INode> Nodes => nodes;

        [JsonIgnore]
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

        protected virtual void OnChanged(GraphOperation operation, object obj)
        {
            Changed?.Invoke(operation, obj);
        }

        public INode AddNode()
        {
            var node = new Node(numberCounter++);
            nodes.Add(node);
            OnChanged(GraphOperation.AddNode, node);
            return node;
        }

        public void DeleteNode(INode node)
        {
            // Удаляем все инцидентные рёбра (вместе с ними и вершины)
            for (var i = node.IncidentEdges.Count - 1; i >= 0; i--)
            {
                var edge = node.IncidentEdges[i];
                DeleteEdge(edge);
            }

            nodes.Remove(node);
            OnChanged(GraphOperation.DeleteNode, node);
        }

        public IEdge AddEdge(INode first, INode second, int length = 1)
        {
            var isIncident = first.IncidentNodes.Contains(second);
            var edge = Node.Connect(first, second, length);
            if (!isIncident)
            {
                OnChanged(GraphOperation.AddEdge, edge);
            }
            return edge;
        }

        public void DeleteEdge(IEdge edge)
        {
            Node.Disconnect(edge);
            OnChanged(GraphOperation.DeleteEdge, edge);
        }
    }
}
