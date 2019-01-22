using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GraphMaker.Model;


namespace GraphMaker
{
    public static class GraphExtensions
    {
        public static List<IEdge> MinPath(this IGraph graph, INode v1, INode v2)
        {
            throw new NotImplementedException();
        }

        public static List<IEdge> MST(this IGraph graph)
        {
            throw new NotImplementedException();
        }

        private static void StackDFS(Node node, bool[] used)
        {
            Stack<Node> nodes = new Stack<Node>();
            nodes.Push(node);

            while (nodes.Count != 0)
            {
                var currNode = nodes.Pop();
                used[currNode.Number] = true;

                foreach (var incNode in currNode.IncidentNodes)
                {
                    if (!used[incNode.Number])
                    {
                        nodes.Push((Node)incNode);
                    }
                }
            }
        }

        public static int CCcountStackDFS(this IGraph graph)
        {
            var listOfNodes = graph.Nodes;
            bool[] used = new bool[listOfNodes.Count];
            int ConnectedComponentCount = 0;

            foreach (var node in listOfNodes)
            {
                if(!used[node.Number])
                {
                    ConnectedComponentCount++;
                    StackDFS((Node)node, used);
                }
            }
            return ConnectedComponentCount;
        }

        private static void RecursiveDFS(Node node, bool[] used)
        {
            foreach (var incNode in node.IncidentNodes)
            {
                if (!used[incNode.Number])
                {
                    used[incNode.Number] = true;
                    RecursiveDFS((Node)incNode, used);
                }
            }
        }

        public static int CCcountRecursiveDFS(this IGraph graph)
        {
            var listOfNodes = graph.Nodes;
            bool[] used = new bool[listOfNodes.Count];
            int ConnectedComponentCount = 0;

            foreach (var node in listOfNodes)
            {
                if (!used[node.Number])
                {
                    ConnectedComponentCount++;
                    used[node.Number] = true;
                    RecursiveDFS((Node)node, used);
                }
            }
            return ConnectedComponentCount;
        }
    }
}
