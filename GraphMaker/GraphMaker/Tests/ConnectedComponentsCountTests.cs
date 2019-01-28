using System.Linq;
using NUnit.Framework;
using GraphMaker.Extensions;
using System.Collections.Generic;
using FluentAssertions;

namespace GraphMaker.Tests
{
    [TestFixture]
    public class ConnectedComponentsCountTests
    {
        [Test, TestCaseSource(typeof(TestsFactory), nameof(TestsFactory.GetCountTestCases))]
        public void GetNumberOfConnectedComponents(
            int nodesCount,
            (int First, int Second)[] incidentEdges,
            int expectedCount)
        {
            var graph = TestHelper.CreateImmutableGraphMock(nodesCount, incidentEdges);

            var actualStackCount = graph.CCcountStackDFS();
            Assert.AreEqual(expectedCount, actualStackCount, "CCcountStackDFS");

            var actualRecursiveCount = graph.CCcountRecursiveDFS();
            Assert.AreEqual(expectedCount, actualRecursiveCount, "CCcountRecursiveDFS");
        }

        [Test, TestCaseSource(typeof(TestsFactory), nameof(TestsFactory.GetComponentsTestCases))]
        public void GetComponentsTest(
            int nodesCount,
            (int First, int Second)[] incidentEdges,
            List<List<int>> expectedComponents)
        {
            var graph = TestHelper.CreateImmutableGraphMock(nodesCount, incidentEdges);
            var actualComponents = graph
                .GetListOfComponents()
                .Select(component => component.Select(node => node.Number))
                .ToList();
            actualComponents.Count.Should().Be(expectedComponents.Count);
            foreach (var component in actualComponents)
            {
                expectedComponents.Should().ContainEquivalentOf(component);
            }
        }

        private class TestsFactory
        {
            private static (int, int)[] Edges(params (int, int)[] edges) => edges;

            private static List<List<int>> Components(params List<int>[] components) => components.ToList();

            private static List<int> Component(params int[] nodeNumbers) => nodeNumbers.ToList();

            public static TestCaseData[] GetCountTestCases => new[]
            {
                new TestCaseData(0,
                    Edges(),
                    0),

                new TestCaseData(1,
                    Edges(),
                    1),

                new TestCaseData(4,
                    Edges((0, 1), (0, 2), (0, 3), (1, 2), (1, 3), (2, 3)),
                    1),

                new TestCaseData(6,
                    Edges((0, 1), (1, 2), (1, 3), (4, 5)),
                    2),

                new TestCaseData(7,
                    Edges((0, 1), (1, 2), (1, 3), (4, 5)),
                    3),

                new TestCaseData(7,
                    Edges(),
                    7)
            };

            public static TestCaseData[] GetComponentsTestCases => new[]
            {
                new TestCaseData(0,
                    Edges(),
                    Components()),

                new TestCaseData(1,
                    Edges(),
                    Components(Component(0))),

                new TestCaseData(4,
                    Edges((0, 1), (0, 2), (0, 3), (1, 2), (1, 3), (2, 3)),
                    Components(Component(0, 1, 2, 3))),

                new TestCaseData(5,
                    Edges(),
                    Components(Component(0), Component(1), Component(2), Component(3), Component(4))),

                new TestCaseData(6,
                    Edges((0, 1), (1, 2), (1, 3), (4, 5)),
                    Components(Component(0, 1, 2, 3), Component(4, 5))),

                new TestCaseData(7,
                    Edges((0, 1), (1, 2), (1, 3), (4, 5)),
                    Components(Component(0, 1, 2, 3), Component(4, 5), Component(6)))           
            };
        }
    }
}
