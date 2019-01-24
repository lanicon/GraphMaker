using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using GraphMaker.Model;

namespace GraphMaker.UI
{ 
    public class UiGraph : IGraph
    {
        private const int DefaultX = 0;

        private const int DefaultY = 0;

        private readonly Color DefaultNodeColor = Color.Black;

        private readonly Color DefaultEdgeColor = Color.Black;

        private readonly IGraph graph;

        private readonly Dictionary<IEdge, EdgeInfo> edgeInfos = new Dictionary<IEdge, EdgeInfo>();

        private readonly Dictionary<INode, NodeInfo> nodeInfos = new Dictionary<INode, NodeInfo>();

        public event GraphChangeEvent Changed;

        public IReadOnlyList<INode> Nodes => graph.Nodes;

        public IReadOnlyList<IEdge> Edges => graph.Edges;

        public EdgeInfo GetEdgeInfo(IEdge edge) => edgeInfos[edge];

        public NodeInfo GetNodeInfo(INode node) => nodeInfos[node];

        public UiGraph(Type graphType)
        {
            if (!graphType.GetInterfaces().Contains(typeof(IGraph)))
            {
                throw new ArgumentException(nameof(graphType));
            }

            this.graph = (IGraph)Activator.CreateInstance(graphType);
            this.graph.Changed += Changed;
        }

        public INode AddNode(int x, int y, Color color)
        {
            var node = graph.AddNode();

            nodeInfos[node] = new NodeInfo
            {
                Color = color,
                X = x,
                Y = y
            };

            return node;
        }

        public IEdge AddEdge(INode first, INode second, int length, Color color)
        {
            var edge = graph.AddEdge(first, second, length);

            edgeInfos[edge] = new EdgeInfo
            {
                From = nodeInfos[first],
                To = nodeInfos[second],
                Color = color
            };

            return edge;
        }

        public INode AddNode()
        {
            return AddNode(DefaultX, DefaultY, DefaultNodeColor);
        }

        public IEdge AddEdge(INode first, INode second, int length)
        {
            return AddEdge(first, second, length, DefaultEdgeColor);
        }

        public void DeleteNode(INode node)
        {
            graph.DeleteNode(node);
        }

        public void DeleteEdge(IEdge edge)
        {
            graph.DeleteEdge(edge);
        }
    }
}
