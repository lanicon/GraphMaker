using System.Collections.Generic;
using System.Drawing;

namespace GraphMaker.UI
{
    public class EdgeInfo
    {
        public NodeInfo First { get; }

        public NodeInfo Second { get; }

        public Color Color { get; set; }

        public EdgeInfo(NodeInfo first, NodeInfo second, Color color)
        {
            First = first;
            Second = second;
            Color = color;
        }

        public override bool Equals(object obj)
        {
            var info = obj as EdgeInfo;
            return info != null &&
                   EqualityComparer<NodeInfo>.Default.Equals(First, info.First) &&
                   EqualityComparer<NodeInfo>.Default.Equals(Second, info.Second) &&
                   EqualityComparer<Color>.Default.Equals(Color, info.Color);
        }

        public override int GetHashCode()
        {
            var hashCode = 1398590958;
            hashCode = hashCode * -1521134295 + EqualityComparer<NodeInfo>.Default.GetHashCode(First);
            hashCode = hashCode * -1521134295 + EqualityComparer<NodeInfo>.Default.GetHashCode(Second);
            hashCode = hashCode * -1521134295 + EqualityComparer<Color>.Default.GetHashCode(Color);
            return hashCode;
        }
    }
}
