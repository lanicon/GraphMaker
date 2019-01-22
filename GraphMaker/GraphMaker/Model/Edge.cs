using System;

namespace GraphMaker.Model
{
    public class Edge : IEdge
    {
        public INode First { get; }

        public INode Second { get; }

        public int Length { get; set; }

        public Edge(INode first, INode second, int length) : this(first, second)
        {
            Length = length;
        }

        public Edge(INode first, INode second)
        {
            if (first == second)
            {
                throw new ArgumentException("the beginning and end of the edge are the same");
            }

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

        public override string ToString()
        {
            return $"{First.Number}<->{Second.Number}";
        }
    }
}