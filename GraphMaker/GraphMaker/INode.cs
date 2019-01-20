using System.Collections.Generic;

namespace GraphMaker
{
    public interface INode
    {
        int Number { get; }

        List<IEdge> IncidentEdges { get; }

        List<INode> IncidentNodes { get; }
    }
}
