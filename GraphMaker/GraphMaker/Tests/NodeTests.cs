using FluentAssertions;
using GraphMaker.Model;
using NUnit.Framework;

namespace GraphMaker.Tests
{
    [TestFixture]
    public class NodeTests
    {
        private Node node1, node2, node3;

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
        public void Connect_Twice_ShouldConnectOnce()
        {
            var edge12 = Node.Connect(node1, node2);
            var edge21 = Node.Connect(node2, node1);

            node1.IncidentNodes.Should().BeEquivalentTo(node2);
            node2.IncidentNodes.Should().BeEquivalentTo(node1);

            node1.IncidentEdges.Should().BeEquivalentTo(edge12);
            node2.IncidentEdges.Should().BeEquivalentTo(edge12);

            edge21.Should().Be(edge21);
        }

        [Test]
        public void Connect_CompleteGraphK3()
        {
            var edge12 = Node.Connect(node1, node2);
            var edge13 = Node.Connect(node1, node3);
            var edge23 = Node.Connect(node2, node3);

            node1.IncidentNodes.Should().BeEquivalentTo(node2, node3);
            node2.IncidentNodes.Should().BeEquivalentTo(node1, node3);
            node3.IncidentNodes.Should().BeEquivalentTo(node1, node2);

            node1.IncidentEdges.Should().BeEquivalentTo(edge12, edge13);
            node2.IncidentEdges.Should().BeEquivalentTo(edge12, edge23);
            node3.IncidentEdges.Should().BeEquivalentTo(edge13, edge23);
        }

        [Test]
        public void Disconnect_Once()
        {
            var edge12 = Node.Connect(node1, node2);

            Node.Disconnect(edge12);

            node1.IncidentNodes.Should().BeEmpty();
            node2.IncidentNodes.Should().BeEmpty();

            node1.IncidentEdges.Should().BeEmpty();
            node2.IncidentEdges.Should().BeEmpty();
        }

        [Test]
        public void Disconnect_Twice_ShouldNotThrowException()
        {
            var edge12 = Node.Connect(node1, node2);

            Node.Disconnect(edge12);
            Assert.DoesNotThrow(() => Node.Disconnect(edge12));

            node1.IncidentNodes.Should().BeEmpty();
            node2.IncidentNodes.Should().BeEmpty();

            node1.IncidentEdges.Should().BeEmpty();
            node2.IncidentEdges.Should().BeEmpty();
        }
    }
}
