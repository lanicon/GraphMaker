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
                var currentNode = nodes[i];
                if(nodePaths[currentNode].Node == end)    
                {
                    continue;
                }
                var iEdges = new List<IEdge>(nodePaths[currentNode].Node.IncidentEdges);
                iEdges.Sort((x, y) => x.Length.CompareTo(y.Length));
                foreach (var incidentEdge in iEdges)
                {
                    var icidentNode = incidentEdge.OtherNode(nodePaths[currentNode].Node).Number;
                    if (!nodePaths.ContainsKey(icidentNode))
                    {
                        var newPath = new List<IEdge>(nodePaths[currentNode].Path)
                        {
                            incidentEdge
                        };
                        nodes.Add(icidentNode);
                        nodePaths.Add(icidentNode, 
                            new PathToNode(incidentEdge.OtherNode(nodePaths[currentNode].Node), 
                            nodePaths[currentNode].MinimalLenght + incidentEdge.Length, newPath));
                    }
                    else if(!nodePaths[icidentNode].Viseted)
                    {
                        if (incidentEdge.Length + nodePaths[currentNode].MinimalLenght < nodePaths[icidentNode].MinimalLenght)
                        {
                            var newPathToNode = new PathToNode(nodePaths[icidentNode].Node, 
                                nodePaths[currentNode].MinimalLenght + incidentEdge.Length, new List<IEdge>(nodePaths[currentNode].Path));
                            newPathToNode.Path.Add(incidentEdge);
                            nodePaths[icidentNode] = newPathToNode;
                        }
                    }
                }
                var t = nodePaths[currentNode];
                t.Viseted = true;
                nodePaths[currentNode] = t;
            }
            return nodePaths[end.Number].Path;
        }
    }
}
