using System.Collections.Generic;

namespace GraphMaker.Model
{
    public class Graph : IGraph
    {
        public INode AddNode()
        {
            throw new System.NotImplementedException();
        }

        public void DeleteNode(INode v)
        {
            throw new System.NotImplementedException();
        }

        public IEdge AddEdge(INode v1, INode v2)
        {
            throw new System.NotImplementedException();
        }

        public void DeleteEdge(INode v1, INode v2)
        {
            throw new System.NotImplementedException();
        }

        public List<INode> Nodes { get; }
        public List<IEdge> Edges { get; }
        public event GraphChangeEvent Changed;
    }
}
