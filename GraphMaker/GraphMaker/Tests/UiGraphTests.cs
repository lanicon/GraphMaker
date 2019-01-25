using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Runtime.Serialization.Formatters;
using System.Text.RegularExpressions;
using FluentAssertions;
using GraphMaker.Model;
using GraphMaker.UI;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using NUnit.Framework;

namespace GraphMaker.Tests
{
    [TestFixture]
    public class UiGraphTests
    {
        private class GraphWithoutDefaultCtor : IGraph
        {
            public GraphWithoutDefaultCtor(int nodesCount) { }

            public IReadOnlyList<INode> Nodes { get; }

            public IReadOnlyList<IEdge> Edges { get; }

            public event GraphChangeEvent Changed;

            public INode AddNode()
            {
                throw new NotImplementedException();
            }

            public void DeleteNode(INode node)
            {
                throw new NotImplementedException();
            }

            public IEdge AddEdge(INode first, INode second, int length)
            {
                throw new NotImplementedException();
            }

            public void DeleteEdge(IEdge edge)
            {
                throw new NotImplementedException();
            }
        }

        private UiGraph graph;

        [SetUp]
        public void SetUp()
        {
            graph = new UiGraph(typeof(Graph));
        }

        [Test]
        public void Create_ThrowExceptionOnInvalidType()
        {
            Assert.Throws<ArgumentException>(() => new UiGraph(typeof(int)));
            Assert.Throws<ArgumentException>(() => new UiGraph(typeof(UiGraph)));
            Assert.Throws<ArgumentException>(() => new UiGraph(typeof(GraphWithoutDefaultCtor)));
        }

        [Test]
        public void AddNode_ShouldSaveNodeInfo()
        {
            var node = graph.AddNode(0, 0, Color.Black);
            graph.NodeInfos[node]
                .Should()
                .Be(new NodeInfo(0, 0, Color.Black));
        }

        [Test]
        public void AddEdge_ShouldSaveEdgeInfo()
        {
            var node1 = graph.AddNode();
            var node2 = graph.AddNode();
            var edge12 = graph.AddEdge(node1, node2, 42, Color.Red);
            graph.EdgeInfos[edge12]
                .Should()
                .Be(new EdgeInfo(graph.NodeInfos[node1], graph.NodeInfos[node2], Color.Red));
        }

        [Test]
        public void DeleteNode_ShouldDeleteNodeInfo()
        {
            var node = graph.AddNode();
            graph.DeleteNode(node);
            Assert.Throws<KeyNotFoundException>(() =>
            {
                var nodeInfo = graph.NodeInfos[node];
            });
        }

        [Test]
        public void DeleteNode_Twice_ShouldNotThrowException()
        {
            var node = graph.AddNode();
            graph.DeleteNode(node);
            Assert.DoesNotThrow(() => graph.DeleteNode(node));
        }

        [Test]
        public void DeleteEdge_ShouldDeleteEdgeInfo()
        {
            var node1 = graph.AddNode();
            var node2 = graph.AddNode();
            var edge12 = graph.AddEdge(node1, node2, 1, Color.Black);
            graph.DeleteEdge(edge12);
            Assert.Throws<KeyNotFoundException>(() =>
            {
                var edgeInfo = graph.EdgeInfos[edge12];
            });
        }

        [Test]
        public void DeleteEdge_Twice_ShouldNotThrowException()
        {
            var node1 = graph.AddNode();
            var node2 = graph.AddNode();
            var edge12 = graph.AddEdge(node1, node2, 1, Color.Black);
            graph.DeleteEdge(edge12);
            Assert.DoesNotThrow(() => graph.DeleteEdge(edge12));
        }

        [Test]
        public void ChangeEdgeInfo_ShouldBeApplied()
        {
            var node1 = graph.AddNode();
            var node2 = graph.AddNode();
            var edge12 = graph.AddEdge(node1, node2, 10, Color.Red);
            var edgeInfo = graph.EdgeInfos[edge12];
            edgeInfo.Color = Color.Blue;
            graph.EdgeInfos[edge12]
                .Should()
                .Be(new EdgeInfo(graph.NodeInfos[node1], graph.NodeInfos[node2], Color.Blue));

        }

        [Test]
        public void ChangeNodeInfo_ShouldBeApplied()
        {
            var node = graph.AddNode(0, 0, Color.Red);
            var nodeInfo = graph.NodeInfos[node];
            nodeInfo.X = 42;
            nodeInfo.Y = 43;
            nodeInfo.Color = Color.Blue;
            graph.NodeInfos[node]
                .Should()
                .Be(new NodeInfo(42, 43, Color.Blue));
        }

        [Test]
        public void ShouldBeSerializable()
        {
            var node1 = graph.AddNode(0, 0, Color.Red);
            var node2 = graph.AddNode(1, 1, Color.Green);
            graph.AddEdge(node1, node2, 42, Color.Blue);

            var json1 = UiGraph.Serialize(graph);
            var deserializedGraph = UiGraph.Deserialize(json1);
            var json2 = UiGraph.Serialize(deserializedGraph);
            json1.Should().Be(json2);
        }
    }
}
