using System;
using Newtonsoft.Json;

namespace GraphMaker.Model
{
    public class Edge : IEdge
    {
        [JsonProperty(TypeNameHandling = TypeNameHandling.Objects)]
        public INode First { get; private set; }

        [JsonProperty(TypeNameHandling = TypeNameHandling.Objects)]
        public INode Second { get; private set; }

        [JsonProperty]
        public int Length { get; set; }

        [JsonConstructor]
        private Edge() { }

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
            var minByNumber = first;
            var maxByNumber = second;
            if (first.Number > second.Number)
            {
                minByNumber = second;
                maxByNumber = first;
            }
            First = minByNumber;
            Second = maxByNumber;
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