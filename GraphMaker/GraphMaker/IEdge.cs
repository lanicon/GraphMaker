using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GraphMaker
{
    public interface IEdge
    {
        INode Node1 { get; set; }
        INode Node2 { get; set; }
        int Length { get; set; }
    }
}
