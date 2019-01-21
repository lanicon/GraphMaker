using System;
using FluentAssertions;
using GraphMaker.Model;
using NUnit.Framework;

namespace GraphMaker.Tests
{
    [TestFixture]
    public class EdgeTests
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
        public void EdgeCreation()
        {
            var edge = new Edge(node1, node2);
            edge.First.Should().Be(node1);
            edge.Second.Should().Be(node2);
            edge.Length.Should().Be(0);
        }

        [Test]
        public void EdgeCreation_ShouldNotCreateLoop()
        {
            Assert.Throws<ArgumentException>(() => new Edge(node1, node1));
        }

        [Test]
        public void OtherNode_OnNonIncidentNode_ThrowsArgumentException()
        {
            Assert.Throws<ArgumentException>(() => { new Edge(node1, node2).OtherNode(node3); });
        }

        [Test]
        public void OtherNode_OnIncidentNode()
        {
            var edge = new Edge(node1, node2);
            edge.OtherNode(node1).Should().Be(node2);
            edge.OtherNode(node2).Should().Be(node1);
        }

        [Test]
        public void IsIncident_OnNonIncidentNode_ShouldBeFalse()
        {
            new Edge(node1, node2).IsIncident(node3).Should().BeFalse();
        }

        [Test]
        public void IsIncident_OnIncidentNode_ShouldBeTrue()
        {
            new Edge(node1, node2).IsIncident(node1).Should().BeTrue();
        }
    }
}
