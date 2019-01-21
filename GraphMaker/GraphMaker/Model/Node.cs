using System.Collections.Generic;
using System.Linq;

namespace GraphMaker.Model
{
    public class Node : INode
    { 
        public Node(int number)
        {
            Number = number;
        }

        public int Number { get; }

        public List<IEdge> IncidentEdges { get; } = new List<IEdge>();

        public List<INode> IncidentNodes
        {
            get
            {
                return IncidentEdges
                    .Select(z => z.OtherNode(this))
                    .ToList();
            }
        }

        public static IEdge Connect(INode node1, INode node2)
        {
            // Если вершины уже соединены, то просто вернём нужное ребро
            if (node1.IncidentNodes.Contains(node2))
            {
                return node1
                    .IncidentEdges
                    .First(e => e.First == node1 && e.Second == node2 ||
                                e.First == node2 && e.Second == node1);
            }
            var edge = new Edge(node1, node2);
            node1.IncidentEdges.Add(edge);
            node2.IncidentEdges.Add(edge);
            return edge;
        }

        public static void Disconnect(IEdge edge)
        {
            edge.First.IncidentEdges.Remove(edge);
            edge.Second.IncidentEdges.Remove(edge);
        }

        public override string ToString()
        {
            return Number.ToString();
        }
    }
}
