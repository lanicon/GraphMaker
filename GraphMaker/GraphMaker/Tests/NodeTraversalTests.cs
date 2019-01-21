using System.Collections.Generic;
using System.Linq;
using FluentAssertions;
using GraphMaker.Extensions;
using GraphMaker.Model;
using NUnit.Framework;

namespace GraphMaker.Tests
{
    [TestFixture]
    public class NodeTraversalTests
    {
        private INode node1, node2, node3, node4, node5;

        [SetUp]
        public void SetUp()
        {
            node1 = new Node(1);
            node2 = new Node(2);
            node3 = new Node(3);
            node4 = new Node(4);
            node5 = new Node(5);
        }

        private INode SingleNodeGraph => node1;

        private INode K5
        {
            get
            {
                var nodes = new List<INode>()
                {
                    node1, node2, node3, node4, node5
                };
                for (var i = 0; i < nodes.Count; i++)
                {
                    for (var j = 0; j < i; j++)
                    {
                        Node.Connect(nodes[j], nodes[i]);
                    }
                }
                return nodes[0];
            }
        }

        private INode HouseGraph
        {
            get
            {
                Node.Connect(node1, node2);
                Node.Connect(node1, node3);
                Node.Connect(node2, node4);
                Node.Connect(node2, node5);
                Node.Connect(node3, node4);
                Node.Connect(node3, node5);

                return node1;
            }
        }

        [Test]
        public void DepthSearch_OnOneNode()
        {
            SingleNodeGraph.DepthSearch()
                .Should()
                .BeEquivalentTo(node1);
        }

        [Test]
        [Timeout(1000)]
        public void DepthSearch_K5()
        {
            var traversalOrder = K5.DepthSearch();
            traversalOrder[0].Should().Be(node1);
            traversalOrder.Should().BeEquivalentTo(node1, node2, node3, node4, node5);
        }

        [Test]
        public void DepthSearch_HouseGraph()
        {
            var traversalOrder = HouseGraph.DepthSearch();
            var possibleTraversals = new List<List<INode>>
            {
                new List<INode>() { node1, node2, node4, node3, node5 },
                new List<INode>() { node1, node2, node5, node3, node4 },
                new List<INode>() { node1, node3, node4, node2, node5 },
                new List<INode>() { node1, node3, node5, node2, node4 },
            };
            possibleTraversals
                .Should()
                .Contain(t => t.SequenceEqual(traversalOrder));
        }

        [Test]
        public void BreadthSearch_OnOneNode()
        {
            SingleNodeGraph.BreadthSearch()
                .Should()
                .BeEquivalentTo(node1);
        }

        [Test]
        [Timeout(1000)]
        public void BreadthSearch_K5()
        {
            var traversalOrder = K5.BreadthSearch();
            traversalOrder[0].Should().Be(node1);
            traversalOrder.Should().BeEquivalentTo(node1, node2, node3, node4, node5);
        }

        [Test]
        public void BreadthSearch_HouseGraph()
        {
            var traversalOrder = HouseGraph.BreadthSearch();
            var possibleTraversals = new List<List<INode>>
            {
                new List<INode>() { node1, node2, node3, node4, node5 },
                new List<INode>() { node1, node2, node3, node5, node4 },
                new List<INode>() { node1, node3, node2, node4, node5 },
                new List<INode>() { node1, node3, node2, node5, node4 },
            };
            possibleTraversals
                .Should()
                .Contain(t => t.SequenceEqual(traversalOrder));
        }
    }
}
