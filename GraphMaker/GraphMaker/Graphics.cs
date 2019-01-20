using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GraphMaker
{
    class gNode
    {
        public INode node { get; set; }
        public int x { get; set; }
        public int y { get; set; }
        public Color color { get; set; }
        public gNode(int x, int y, Color color)
        {
            this.x = x;
            this.y = y;
            this.color = color;
        }
    }

    class gEdge
    {
        public gNode node1 { get; set; }
        public gNode node2 { get; set; }
        public Color color { get; set; }
        public gEdge(gNode node1, gNode node2, Color color)
        {
            this.node1 = node1;
            this.node2 = node2;
            this.color = color;
        }
    }
}
