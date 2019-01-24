using System.Collections.Generic;

namespace GraphMaker.Model
{
    public interface INode
    {
        int Number { get; }

        List<IEdge> IncidentEdges { get; }

        List<INode> IncidentNodes { get; }
    }
}
