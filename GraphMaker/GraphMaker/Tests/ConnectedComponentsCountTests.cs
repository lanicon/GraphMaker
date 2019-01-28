using GraphMaker.Model;
using NUnit.Framework;
using GraphMaker.Extensions;

namespace GraphMaker.Tests
{
    [TestFixture]
    public class ConnectedComponentsCountTests
    {
        [Test, TestCaseSource(typeof(TestsFactory), nameof(TestsFactory.TestCases))]
        public void Test(
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

        private class TestsFactory
        {
            public static TestCaseData[] TestCases => new[]
            {
                new TestCaseData(0, new (int, int)[0], 0),
                new TestCaseData(1, new (int, int)[0], 1), 
                new TestCaseData(6, new[] {(0, 1), (1, 2), (1, 3), (4, 5)}, 2),
                new TestCaseData(7, new[] {(0, 1), (1, 2), (1, 3), (4, 5)}, 3),
                new TestCaseData(7, new (int, int)[0], 7),
                new TestCaseData(4, new[] {(0, 1), (0, 2), (0, 3), (1, 2), (1, 3), (2, 3)}, 1), 
            };
        }
    }
}
