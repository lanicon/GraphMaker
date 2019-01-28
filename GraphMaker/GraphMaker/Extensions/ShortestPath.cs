using System.Collections.Generic;
using GraphMaker.Model;

namespace GraphMaker.Extensions
{
    public static partial class GraphExtensions
    {
        struct PathToNode
        {
            public INode Node { get; set; }
            public List<IEdge> Path { get; set; }
            public int MinimalLenght { get; set; }
            public bool Viseted { get; set; }

            public PathToNode(INode _node, int _lenght, List<IEdge> _path)
            {
                Node = _node;
                MinimalLenght = _lenght;
                Path = _path;
                Viseted = false;
            }
        }

        public static List<IEdge> GetShortestPath(this IGraph graph, INode start, INode end)
        {
            List<PathToNode> nodePaths = new List<PathToNode>() { new PathToNode(start, 0, new List<IEdge>()) };
            for(int i = 0; i < nodePaths.Count; i++)
            {
                var iEdges = new List<IEdge>(nodePaths[i].Node.IncidentEdges);
                iEdges.Sort((x, y) => x.Length.CompareTo(y.Length));
                foreach (var incidentEdge in iEdges)
                {
                    var secondNodePathIndex = nodePaths.FindIndex(x => x.Node == incidentEdge.OtherNode(nodePaths[i].Node));
                    if (secondNodePathIndex < 0)
                    {
                        var newPath = new List<IEdge>(nodePaths[i].Path)
                        {
                            incidentEdge
                        };
                        nodePaths.Add(new PathToNode(incidentEdge.OtherNode(nodePaths[i].Node), 
                            nodePaths[i].MinimalLenght + incidentEdge.Length, newPath));
                    }
                    else if(!nodePaths[secondNodePathIndex].Viseted)
                    {
                        if (nodePaths[i].MinimalLenght + incidentEdge.Length < nodePaths[secondNodePathIndex].MinimalLenght)
                        {
                            var newPathToNode = new PathToNode(nodePaths[secondNodePathIndex].Node, 
                                nodePaths[i].MinimalLenght + incidentEdge.Length, new List<IEdge>(nodePaths[i].Path));
                            newPathToNode.Path.Add(incidentEdge);
                            nodePaths[secondNodePathIndex] = newPathToNode;
                        }
                    }
                }
                var t = nodePaths[i];
                t.Viseted = true;
                nodePaths[nodePaths.FindIndex(x => x.Equals(nodePaths[i]))] = t;
            }
            return nodePaths.Find(x => x.Node == end && x.Viseted == true).Path;
        }
    }
}
