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
            List<int> nodes = new List<int>();
            Dictionary<int, PathToNode> nodePaths = new Dictionary<int, PathToNode>();
            nodePaths.Add(start.Number, new PathToNode(start, 0, new List<IEdge>()));
            nodes.Add(start.Number);
            for(int i = 0; i < nodes.Count; i++)
            {
                var currentNodeIndex = nodes[i];
                if(nodePaths[currentNodeIndex].Node == end)    
                {
                    continue;
                }
                var iEdges = new List<IEdge>(nodePaths[currentNodeIndex].Node.IncidentEdges);
                iEdges.Sort((x, y) => x.Length.CompareTo(y.Length));
                foreach (var incidentEdge in iEdges)
                {
                    var icidentNodeIndex = incidentEdge.OtherNode(nodePaths[currentNodeIndex].Node).Number;
                    if (!nodePaths.ContainsKey(icidentNodeIndex))
                    {
                        var newPath = new List<IEdge>(nodePaths[currentNodeIndex].Path)
                        {
                            incidentEdge
                        };
                        nodes.Add(icidentNodeIndex);
                        nodePaths.Add(icidentNodeIndex, 
                            new PathToNode(incidentEdge.OtherNode(nodePaths[currentNodeIndex].Node), 
                            nodePaths[currentNodeIndex].MinimalLenght + incidentEdge.Length, newPath));
                    }
                    else if(!nodePaths[icidentNodeIndex].Viseted)
                    {
                        if (incidentEdge.Length + nodePaths[currentNodeIndex].MinimalLenght < nodePaths[icidentNodeIndex].MinimalLenght)
                        {
                            var newPathToNode = new PathToNode(nodePaths[icidentNodeIndex].Node, 
                                nodePaths[currentNodeIndex].MinimalLenght + incidentEdge.Length, new List<IEdge>(nodePaths[currentNodeIndex].Path));
                            newPathToNode.Path.Add(incidentEdge);
                            nodePaths[icidentNodeIndex] = newPathToNode;
                        }
                    }
                }
                var t = nodePaths[currentNodeIndex];
                t.Viseted = true;
                nodePaths[currentNodeIndex] = t;
            }
            return nodePaths[end.Number].Path;
        }
    }
}
