using GraphMaker.Model;
using NUnit.Framework;
using GraphMaker.Extensions;
using System.Collections.Generic;

namespace GraphMaker.Tests
{
    [TestFixture]
    public class ShortestPathTest
    {

        private IGraph MakeGraph(int nodesCount, params (int First, int Second, int Length)[] incidentEdges)
        {
            IGraph graph = new Graph();

            for (var i = 0; i < nodesCount; i++)
            {
                graph.AddNode();
            }

            var nodes = graph.Nodes;
            foreach (var (First, Second, Length) in incidentEdges)
            {
                var first = nodes[First];
                var second = nodes[Second];
                graph.AddEdge(first, second, Length);
            }

            return graph;
        }

        [Test, TestCaseSource(typeof(TestsFactory), nameof(TestsFactory.TestCases))]
        public void Test(int nodesCount,int s, int e ,(int First, int Second, int Length)[] incidentEdges, int[] expectedPath)
        {
            var graph = MakeGraph(nodesCount, incidentEdges);

            var actualPath = graph.GetShortestPath(graph.Nodes[s], graph.Nodes[e]);

            List<IEdge> pathInEdges = new List<IEdge>();

            foreach(var i in expectedPath)
            {
                pathInEdges.Add(graph.Edges[i]);
            }

            Assert.AreEqual(pathInEdges, actualPath, "Test1");
        }

        private class TestsFactory
        {
            public static TestCaseData[] TestCases => new[]
            {
                //new TestCaseData(6, 0, 4, new[] {(0, 1, 7), (0, 2, 9), (0, 5, 14), (1, 2, 10), (1, 3, 15), (2, 3, 11), (2, 5, 2), (3, 4, 6), (4, 5, 9)}, new[] { 1, 6, 8 }),
                //new TestCaseData(5, 4, 0, new[] {(0, 1, 3), (0, 2, 1), (0, 3, 4), (1, 2, 1), (1, 4, 1), (2, 3, 6), (3, 4, 5)}, new[] { 4, 3, 1 }),
                //new TestCaseData(5, 0, 4, new[] {(0, 1, 3), (0, 2, 1), (0, 3, 4), (1, 2, 1), (1, 4, 1), (2, 3, 6), (3, 4, 5)}, new[] { 1, 3, 4 }),
                new TestCaseData(4, 0, 3, new[] {(0, 1, 1), (0, 2, 1), (1, 3, 3), (2, 3, 3) }, new[] { 0, 2 }),
            };
        }
    }
}
