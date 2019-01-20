using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GraphMaker
{
    public interface IEdge
    {
        INode First { get; set; }

        INode Second { get; set; }

        int Length { get; set; }

        INode OtherNode(INode node);

        bool IsIncident(INode node);
    }
}
