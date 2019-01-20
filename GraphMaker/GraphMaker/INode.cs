using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GraphMaker
{
    public interface INode
    {
        int Number { get; }

        List<IEdge> IncidentEdges { get; }

        List<INode> IncidentNodes { get; }
    }
}
