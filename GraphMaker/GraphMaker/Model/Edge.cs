using System;

namespace GraphMaker.Model
{
    public class Edge : IEdge
    {
        public INode First { get; set; }

        public INode Second { get; set; }

        public int Length { get; set; }

        public Edge(INode first, INode second)
        {
            First = first;
            Second = second;
        }

        public INode OtherNode(INode node)
        {
            if (!IsIncident(node))
            {
                throw new ArgumentException(nameof(node));
            }

            return node == First ? Second : First;
        }

        public bool IsIncident(INode node)
        {
            return node == First || node == Second;
        }
    }
}
