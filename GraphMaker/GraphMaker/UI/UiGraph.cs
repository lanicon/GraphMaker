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
        private readonly IGraph instance;

        private readonly Dictionary<IEdge, EdgeInfo> edgeInfos = new Dictionary<IEdge, EdgeInfo>();

        private readonly Dictionary<INode, NodeInfo> nodeInfos = new Dictionary<INode, NodeInfo>();

        public event GraphChangeEvent Changed
        {
            add    => this.instance.Changed += value;
            remove => this.instance.Changed -= value;
        }

        [JsonIgnore]
        public IReadOnlyList<INode> Nodes => this.instance.Nodes;

        [JsonIgnore]
        public IReadOnlyList<IEdge> Edges => this.instance.Edges;

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

            this.instance = (IGraph)Activator.CreateInstance(graphType);
        }

        public INode AddNode(int x, int y, Color color)
        {
            var node = this.instance.AddNode();
            nodeInfos[node] = new NodeInfo(x, y, color);
            return node;
        }

        public IEdge AddEdge(INode first, INode second, int length, Color color)
        {
            var edge = this.instance.AddEdge(first, second, length);
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
            this.instance.DeleteNode(node);
        }

        public void DeleteEdge(IEdge edge)
        {
            edgeInfos.Remove(edge);
            this.instance.DeleteEdge(edge);
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
