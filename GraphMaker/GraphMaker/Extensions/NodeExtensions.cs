using System.Collections.Generic;
using System.Linq;
using GraphMaker.Model;

namespace GraphMaker.Extensions
{
    public static class NodeExtensions
    {
        public static List<INode> DepthSearch(this INode startNode)
        {
            var visited = new HashSet<INode>();
            var stack = new Stack<INode>();
            var result = new List<INode>();
            stack.Push(startNode);
            while (stack.Count != 0)
            {
                var node = stack.Pop();
                if (visited.Contains(node))
                {
                    continue;
                }
                visited.Add(node);
                result.Add(node);
                foreach (var nextNode in node.IncidentNodes.Where(n => !visited.Contains(n)))
                {
                    stack.Push(nextNode);
                }
            }
            return result;
        }

        public static List<INode> BreadthSearch(this INode startNode)
        {
            var visited = new HashSet<INode>();
            var queue = new Queue<INode>();
            var result = new List<INode>();
            queue.Enqueue(startNode);
            visited.Add(startNode);
            while (queue.Count != 0)
            {
                var node = queue.Dequeue();
                result.Add(node);
                foreach (var nextNode in node.IncidentNodes.Where(n => !visited.Contains(n)))
                {
                    visited.Add(nextNode);
                    queue.Enqueue(nextNode);
                }
            }
            return result;
        }
    }
}