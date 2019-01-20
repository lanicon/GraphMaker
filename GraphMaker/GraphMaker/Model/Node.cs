using System.Collections.Generic;

namespace GraphMaker.Model
{
    public class Node : INode
    {
        public int Number { get; }
        public IReadOnlyList<IEdge> GetIncidentNodes { get; }
        public IReadOnlyList<IEdge> GetIncidentEdges { get; }
    }
}
