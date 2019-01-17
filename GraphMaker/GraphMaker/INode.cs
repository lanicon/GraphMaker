using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GraphMaker
{
    public interface INode
    {
        List<IEdge> Edges { get; set; }
        List<IEdge> GetIncidentEdges();
    }
}
