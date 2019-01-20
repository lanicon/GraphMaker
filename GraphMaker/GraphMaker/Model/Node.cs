using System;
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

        public List<IEdge> IncidentEdges { get; set; } = new List<IEdge>();

        public List<INode> GetIncidentNodes()
        {
            return IncidentEdges
                .Select(z => z.OtherNode(this))
                .ToList();
        }

        public static IEdge Connect(INode v1, INode v2)
        {
            var edge = new Edge(v1, v2);
            v1.IncidentEdges.Add(edge);
            v2.IncidentEdges.Add(edge);
            return edge;
        }

        public static void Disconnect(IEdge edge)
        {
            edge.First.IncidentEdges.Remove(edge);
            edge.Second.IncidentEdges.Remove(edge);
        }
    }
}
