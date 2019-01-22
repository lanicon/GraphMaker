using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentAssertions;
using GraphMaker.Model;
using NUnit.Framework;

namespace GraphMaker.Tests
{
    [TestFixture]
    class ConnectedComponentsCountTests
    {
        [Test]
        public void TestRecursiveDFS_TwoCC()
        {
            IGraph graph = new Graph();
            int defaultLength = 3;
            const int nodeCount = 6;
            for (int i = 0; i < nodeCount; i++)
                graph.AddNode();
            var nodes = graph.Nodes;
            graph.AddEdge(nodes[0], nodes[1], defaultLength);
            graph.AddEdge(nodes[1], nodes[2], defaultLength);
            graph.AddEdge(nodes[1], nodes[3], defaultLength);
            graph.AddEdge(nodes[4], nodes[5], defaultLength);


            var actual = graph.CCcountRecursiveDFS();
            int expected = 2;
            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void TestRecursiveDFS_ThreeCC()
        {
            IGraph graph = new Graph();
            int defaultLength = 3;
            const int nodeCount = 7;
            for (int i = 0; i < nodeCount; i++)
                graph.AddNode();
            var nodes = graph.Nodes;
            graph.AddEdge(nodes[0], nodes[1], defaultLength);
            graph.AddEdge(nodes[1], nodes[2], defaultLength);
            graph.AddEdge(nodes[1], nodes[3], defaultLength);
            graph.AddEdge(nodes[4], nodes[5], defaultLength);


            var actual = graph.CCcountRecursiveDFS();
            int expected = 3;
            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void TestStackDFS_TwoCC()
        {
            IGraph graph = new Graph();
            int defaultLength = 3;
            const int nodeCount = 6;
            for (int i = 0; i < nodeCount; i++)
                graph.AddNode();
            var nodes = graph.Nodes;
            graph.AddEdge(nodes[0], nodes[1], defaultLength);
            graph.AddEdge(nodes[1], nodes[2], defaultLength);
            graph.AddEdge(nodes[1], nodes[3], defaultLength);
            graph.AddEdge(nodes[4], nodes[5], defaultLength);


            var actual = graph.CCcountStackDFS();
            int expected = 2;
            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void TestStackDFS_ThreeCC()
        {
            IGraph graph = new Graph();
            int defaultLength = 3;
            const int nodeCount = 7;
            for (int i = 0; i < nodeCount; i++)
                graph.AddNode();
            var nodes = graph.Nodes;
            graph.AddEdge(nodes[0], nodes[1], defaultLength);
            graph.AddEdge(nodes[1], nodes[2], defaultLength);
            graph.AddEdge(nodes[1], nodes[3], defaultLength);
            graph.AddEdge(nodes[4], nodes[5], defaultLength);


            var actual = graph.CCcountStackDFS();
            int expected = 3;
            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void TestStackDFS_SevenCC()
        {
            IGraph graph = new Graph();
            const int nodeCount = 7;
            for (int i = 0; i < nodeCount; i++)
                graph.AddNode();

            var actual = graph.CCcountStackDFS();
            int expected = 7;
            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void TestStackDFS_ZeroCC()
        {
            IGraph graph = new Graph();
            var nodes = graph.Nodes;

            var actual = graph.CCcountStackDFS();
            int expected = 0;
            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void TestRecursiveDFS_ZeroCC()
        {
            IGraph graph = new Graph();
            var nodes = graph.Nodes;

            var actual = graph.CCcountRecursiveDFS();
            int expected = 0;
            Assert.AreEqual(expected, actual);
        }
    }
}
