using System.Collections.Generic;
using System.Drawing;

namespace GraphMaker.UI
{
    public class NodeInfo
    {
        public int X { get; set; }

        public int Y { get; set; }

        public Color Color { get; set; }
            
        public NodeInfo(int x, int y, Color color)
        {
            X = x;
            Y = y;
            Color = color;
        }

        public override bool Equals(object obj)
        {
            var info = obj as NodeInfo;
            return info != null &&
                   X == info.X &&
                   Y == info.Y &&
                   EqualityComparer<Color>.Default.Equals(Color, info.Color);
        }

        public override int GetHashCode()
        {
            var hashCode = -196163389;
            hashCode = hashCode * -1521134295 + X.GetHashCode();
            hashCode = hashCode * -1521134295 + Y.GetHashCode();
            hashCode = hashCode * -1521134295 + EqualityComparer<Color>.Default.GetHashCode(Color);
            return hashCode;
        }
    }
}
