using System.Collections.Generic;
using FluentAssertions;
using GraphMaker.Model;
using NUnit.Framework;

namespace GraphMaker.Tests
{
    [TestFixture]
    public class GraphTests
    {
        private Graph graph;

        [SetUp]
        public void SetUp()
        {
            graph = new Graph();
        }

        [Test]
        public void AddNode_Once()
        {
            var node1 = graph.AddNode();
            var node2 = graph.AddNode();
            var edge12 = graph.AddEdge(node1, node2);

            graph.Nodes.Should().BeEquivalentTo(node1, node2);
            graph.Edges.Should().BeEquivalentTo(edge12);
        }

        [Test]
        public void AddNode_10()
        {
            var nodes = new List<INode>();

            for (var i = 0; i < 10; i++)
            {
                var node = graph.AddNode();
                nodes.Add(node);
            }

            graph.Nodes.Should().BeEquivalentTo(nodes);
            graph.Edges.Should().BeEmpty();
        }

        [Test]
        public void DeleteNode_Once()
        {
            var node = graph.AddNode();

            graph.DeleteNode(node);

            graph.Nodes.Should().BeEmpty();
            graph.Edges.Should().BeEmpty();
        }

        [Test]
        public void DeleteNode_OnEmptyGraph()
        {
            var node = graph.AddNode();

            graph.DeleteNode(node);
            graph.DeleteNode(node);

            graph.Nodes.Should().BeEmpty();
            graph.Edges.Should().BeEmpty();
        }

        [Test]
        public void DeleteNode_ShouldRemoveAllIncidentEdges()
        {
            var node1 = graph.AddNode();
            var node2 = graph.AddNode();
            var node3 = graph.AddNode();

            graph.AddEdge(node1, node2);
            graph.AddEdge(node1, node3);
            graph.DeleteNode(node1);

            graph.Nodes.Should().BeEquivalentTo(node2, node3);
            graph.Edges.Should().BeEmpty();
        }

        [Test]
        public void GraphChanged_ShouldBeCalledFromAllMethods()
        {
            var lastOperation = GraphOperation.None;
            object lastObject = null;
            graph.Changed += (operation, obj) =>
            {
                lastOperation = operation;
                lastObject = obj;
            };

            var node1 = graph.AddNode();
            lastOperation.Should().Be(GraphOperation.AddNode);
            lastObject.Should().Be(node1);

            var node2 = graph.AddNode();
            lastOperation.Should().Be(GraphOperation.AddNode);
            lastObject.Should().Be(node2);

            var edge12 = graph.AddEdge(node1, node2);
            lastOperation.Should().Be(GraphOperation.AddEdge);
            lastObject.Should().Be(edge12);

            graph.DeleteEdge(edge12);
            lastOperation.Should().Be(GraphOperation.DeleteEdge);
            lastObject.Should().Be(edge12);

            graph.DeleteNode(node1);
            lastOperation.Should().Be(GraphOperation.DeleteNode);
            lastObject.Should().Be(node1);
        }

        [Test]
        public void AddEdge_Twice_ShouldNotCreateDuplicates()
        {
            var node1 = graph.AddNode();
            var node2 = graph.AddNode();

            var edge12 = graph.AddEdge(node1, node2);
            graph.AddEdge(node1, node2);

            graph.Nodes.Should().BeEquivalentTo(node1, node2);
            graph.Edges.Should().BeEquivalentTo(edge12);
        }
    }
}
