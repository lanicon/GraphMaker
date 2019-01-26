using GraphMaker.Model;
using NUnit.Framework;
using GraphMaker.Extensions;

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
            foreach (var edge in incidentEdges)
            {
                var first = nodes[edge.First];
                var second = nodes[edge.Second];
                graph.AddEdge(first, second, edge.Length);
            }

            return graph;
        }

        [Test, TestCaseSource(typeof(TestsFactory), nameof(TestsFactory.TestCases))]
        public void Test(int nodesCount, (int First, int Second, int Length)[] incidentEdges, int expectedCount)
        {
            var graph = MakeGraph(nodesCount, incidentEdges);

            graph.GetShortestPath(graph.Nodes[0], graph.Nodes[4]);
        }

        private class TestsFactory
        {
            public static TestCaseData[] TestCases => new[]
            {
                new TestCaseData(5, new[] {(0, 1, 3), (0, 2, 1), (0, 3, 4), (1, 2, 1), (1, 4, 1), (2, 3, 6), (3, 4, 5)}, 1),
            };
        }
    }
}
