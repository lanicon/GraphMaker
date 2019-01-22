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
            var callsCount = 0;
            graph.Changed += () => callsCount++;

            var node1 = graph.AddNode();
            var node2 = graph.AddNode();
            callsCount.Should().Be(2, "AddNode didn't call Changed event");

            var edge12 = graph.AddEdge(node1, node2);
            callsCount.Should().Be(3, "AddEdge didn't call Changed event");

            graph.DeleteEdge(edge12);
            callsCount.Should().Be(4, "DeleteEdge didn't call Changed event");

            graph.DeleteNode(node1);
            callsCount.Should().Be(5, "DeleteNode didn't call Changed event");
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
