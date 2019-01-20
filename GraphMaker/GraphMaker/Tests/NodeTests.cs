using FluentAssertions;
using GraphMaker.Model;
using NUnit.Framework;

namespace GraphMaker.Tests
{
    [TestFixture]
    public class NodeTests
    {
        private INode node1, node2, node3;

        private IEdge edge12, edge13, edge23;

        [SetUp]
        public void SetUp()
        {
            node1 = new Node(1);
            node2 = new Node(2);
            node3 = new Node(3);
        }

        [Test]
        public void Connect_Once()
        {
            var edge12 = Node.Connect(node1, node2);
            node1.IncidentNodes.Should().BeEquivalentTo(node2);
            node2.IncidentNodes.Should().BeEquivalentTo(node1);
            node1.IncidentEdges.Should().BeEquivalentTo(edge12);
        }

        [Test]
        public void Connect_Twice()
        {
            var edge12 = Node.Connect(node1, node2);
            var edge21 = Node.Connect(node2, node1);
            node1.IncidentNodes.Should().BeEquivalentTo(node2);
            node2.IncidentNodes.Should().BeEquivalentTo(node1);
            node1.IncidentEdges.Should().BeEquivalentTo(edge12);
        }
    }
}
