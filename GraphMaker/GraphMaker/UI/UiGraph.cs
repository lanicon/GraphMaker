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

        public IReadOnlyList<INode> Nodes => this.graph.Nodes;

        public IReadOnlyList<IEdge> Edges => this.graph.Edges;

        public IReadOnlyDictionary<IEdge, EdgeInfo> EdgeInfos => edgeInfos;

        public IReadOnlyDictionary<INode, NodeInfo> NodeInfos => nodeInfos;

        public UiGraph(Type graphType)
        {
            if (graphType == typeof(UiGraph))
            {
                throw new ArgumentException("type should not be UiGraph");
            }

            if (!graphType.GetInterfaces().Contains(typeof(IGraph)))
            {
                throw new ArgumentException("type must implement IGraph interface");
            }

            if (graphType.GetConstructor(Type.EmptyTypes) == null)
            {
                throw new ArgumentException("type must have a default constructor");
            }

            this.graph = (IGraph)Activator.CreateInstance(graphType);
            this.graph.Changed += Changed;
        }

        public INode AddNode(int x, int y, Color color)
        {
            var node = this.graph.AddNode();

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
            var edge = this.graph.AddEdge(first, second, length);

            edgeInfos[edge] = new EdgeInfo
            {
                First = nodeInfos[first],
                Second = nodeInfos[second],
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
            this.graph.DeleteNode(node);
        }

        public void DeleteEdge(IEdge edge)
        {
            this.graph.DeleteEdge(edge);
        }
    }
}
