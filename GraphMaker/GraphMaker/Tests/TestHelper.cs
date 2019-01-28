using System;
using System.Collections.Generic;
using System.Linq;
using GraphMaker.Model;
using Moq;

namespace GraphMaker.Tests
{
    public static class TestHelper
    {
        public static IGraph CreateImmutableGraphMock(int nodesCount,
            params (int First, int Second)[] incidentEdges)
        {
            var edges = incidentEdges.Select(e => (e.First, e.Second, 1)).ToArray();
            return CreateImmutableGraphMock(nodesCount, edges);
        }

        public static IGraph CreateImmutableGraphMock(int nodesCount,
            params (int First, int Second, int Length)[] incidentEdges)
        {
            var nodes = new List<INode>();
            var numberCounter = 0;
            for (var i = 0; i < nodesCount; i++)
            {
                var newNode = CreateNode(numberCounter++);
                nodes.Add(newNode);
            }

            var edges = new List<IEdge>();
            foreach (var edge in incidentEdges)
            {
                var first = nodes[edge.First];
                var second = nodes[edge.Second];
                var newEdge = Connect(first, second, edge.Length);
                edges.Add(newEdge);
            }

            var graphMock = new Mock<IGraph>();
            graphMock.Setup(g => g.Edges).Returns(edges);
            graphMock.Setup(g => g.Nodes).Returns(nodes);
            return graphMock.Object;
        }

        private static INode CreateNode(int number)
        {
            var mockNode = new Mock<INode>();
            var incidentEdges = new List<IEdge>();
            var incidentNodes = new List<INode>();
            mockNode.Setup(n => n.Number).Returns(number);
            mockNode.Setup(n => n.IncidentEdges).Returns(incidentEdges);
            mockNode.Setup(n => n.IncidentNodes).Returns(incidentNodes);
            return mockNode.Object;
        }

        private static IEdge Connect(INode first, INode second, int length)
        {
            var edge = first.IncidentEdges
                .FirstOrDefault(e => e.First == first  && e.Second == second || 
                                     e.First == second && e.Second == first);
            if (edge != null)
            {
                return edge;
            }
            var edgeMock = new Mock<IEdge>();
            edgeMock.Setup(e => e.First).Returns(first);
            edgeMock.Setup(e => e.Second).Returns(second);
            edgeMock.Setup(e => e.Length).Returns(length);
            edgeMock.Setup(e => e.IsIncident(It.IsAny<INode>())).Returns<INode>(n => n == first || n == second);
            edgeMock.Setup(e => e.OtherNode(It.IsAny<INode>()))
                .Returns<INode>(n => n == first ? second : n == second ? first : throw new ArgumentException());

            first.IncidentNodes.Add(second);
            first.IncidentEdges.Add(edgeMock.Object);
            second.IncidentNodes.Add(first);
            second.IncidentEdges.Add(edgeMock.Object);
            return edgeMock.Object;
        }
    }
}