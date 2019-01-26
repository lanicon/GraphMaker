using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using GraphMaker.Infrastructure;
using GraphMaker.Model;
using Newtonsoft.Json;

namespace GraphMaker.UI
{ 
    public class UiGraph : IGraph
    {
        private const int DefaultX = 0;

        private const int DefaultY = 0;

        private readonly Color DefaultNodeColor = Color.Black;

        private readonly Color DefaultEdgeColor = Color.Black;

        [JsonProperty(TypeNameHandling = TypeNameHandling.Objects)]
        private readonly IGraph graph;

        private readonly Dictionary<IEdge, EdgeInfo> edgeInfos = new Dictionary<IEdge, EdgeInfo>();

        private readonly Dictionary<INode, NodeInfo> nodeInfos = new Dictionary<INode, NodeInfo>();

        public event GraphChangeEvent Changed;

        [JsonIgnore]
        public IReadOnlyList<INode> Nodes => this.graph.Nodes;

        [JsonIgnore]
        public IReadOnlyList<IEdge> Edges => this.graph.Edges;

        [JsonProperty(TypeNameHandling = TypeNameHandling.Objects)]
        public IReadOnlyDictionary<IEdge, EdgeInfo> EdgeInfos => edgeInfos;

        [JsonProperty(TypeNameHandling = TypeNameHandling.Objects)]
        public IReadOnlyDictionary<INode, NodeInfo> NodeInfos => nodeInfos;

        [JsonConstructor]
        private UiGraph() : this(typeof(Graph)) { }

        public UiGraph(Type graphType)
        {
            if (graphType == typeof(UiGraph))
            {
                throw new ArgumentException("type should not be UiGraph");
            }

            if (!graphType.GetInterfaces().Contains(typeof(IGraph)))
            {
                throw new ArgumentException("type must implement IGraph interface");
            }

            if (graphType.GetConstructor(Type.EmptyTypes) == null)
            {
                throw new ArgumentException("type must have a default constructor");
            }

            this.graph = (IGraph)Activator.CreateInstance(graphType);
            this.graph.Changed += Changed;
        }

        public INode AddNode(int x, int y, Color color)
        {
            var node = this.graph.AddNode();
            nodeInfos[node] = new NodeInfo(x, y, color);
            return node;
        }

        public IEdge AddEdge(INode first, INode second, int length, Color color)
        {
            var edge = this.graph.AddEdge(first, second, length);
            edgeInfos[edge] = new EdgeInfo(nodeInfos[first], NodeInfos[second], color);
            return edge;
        }

        public INode AddNode()
        {
            return AddNode(DefaultX, DefaultY, DefaultNodeColor);
        }

        public IEdge AddEdge(INode first, INode second, int length)
        {
            return AddEdge(first, second, length, DefaultEdgeColor);
        }

        public void DeleteNode(INode node)
        {
            nodeInfos.Remove(node);
            this.graph.DeleteNode(node);
        }

        public void DeleteEdge(IEdge edge)
        {
            edgeInfos.Remove(edge);
            this.graph.DeleteEdge(edge);
        }

        private static readonly JsonSerializerSettings jsonSettings = new JsonSerializerSettings()
        {
            PreserveReferencesHandling = PreserveReferencesHandling.Objects,
            ReferenceLoopHandling = ReferenceLoopHandling.Serialize,
            TypeNameHandling = TypeNameHandling.All,
            Formatting = Formatting.Indented,
            ContractResolver = new DictionaryAsArrayResolver()
        };

        public static string Serialize(UiGraph graph)
        {
            return JsonConvert.SerializeObject(graph, jsonSettings);
        }

        public static UiGraph Deserialize(string json)
        {
            return JsonConvert.DeserializeObject<UiGraph>(json, jsonSettings);
        }

        public static UiGraph New()
        {
            return new UiGraph();
        }
    }
}
