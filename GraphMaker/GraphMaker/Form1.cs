using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using GraphMaker.Model;
using GraphMaker.Extensions;

namespace GraphMaker
{
    public partial class Form1 : Form
    {
        private class NodeInfo
        {
            public int X { get; set; }

            public int Y { get; set; }

            public Color Color { get; set; }
        }

        private class EdgeInfo
        {
            public NodeInfo From { get; set; }

            public NodeInfo To { get; set; }

            public Color Color { get; set; }
        }


        private enum NodesEdges
        {
            Nodes,
            Edges,
            None
        }

        private enum ClickStates
        {
            Add,
            Delete,
            Move,
            NoClick
        }

        private readonly IGraph graph = new Graph();

        private readonly Dictionary<IEdge, EdgeInfo> edgeInfos = new Dictionary<IEdge, EdgeInfo>();

        private readonly Dictionary<INode, NodeInfo> nodeInfos = new Dictionary<INode, NodeInfo>();

        private ClickStates clickState = ClickStates.NoClick;

        private NodesEdges nodesEdgesState = NodesEdges.Nodes;

        private NodesEdges mouseOn = NodesEdges.None;

        private int x, y;

        private INode selectedNode;

        private INode clickedNode;

        private IEdge selectedEdge;

        private IEdge clickedEdge;

        public Form1()
        {
            InitializeComponent();
        }

        private INode AddNode(int x, int y, Color color)
        {
            var node = graph.AddNode();

            nodeInfos[node] = new NodeInfo
            {
                Color = color,
                X = x,
                Y = y
            };

            return node;
        }

        private IEdge AddEdge(INode from, INode to, Color color)
        {
            // TODO: change default length
            var edge = graph.AddEdge(from, to, 1);

            edgeInfos[edge] = new EdgeInfo
            {
                From = nodeInfos[from],
                To = nodeInfos[to],
                Color = color
            };

            return edge;
        }

        private void imDrawSpace_MouseDown(object sender, MouseEventArgs e)
        {
            if (nodesEdgesState == NodesEdges.Nodes)
            {
                switch(mouseOn)
                {
                    case NodesEdges.None:
                        if (e.Button == MouseButtons.Left)
                            clickState = ClickStates.Add;
                        break;
                    case NodesEdges.Nodes:
                        if (e.Button == MouseButtons.Left)
                        {
                            clickedNode = selectedNode;
                            clickState = ClickStates.Move;
                        }

                        if (e.Button == MouseButtons.Right)
                        {
                            clickedNode = selectedNode;
                            clickState = ClickStates.Delete;
                        }
                        break;
                }
            }
            else
            {
                switch(mouseOn)
                {
                    case NodesEdges.Edges:
                        if (e.Button == MouseButtons.Right)
                        {
                            clickedEdge = selectedEdge;
                            clickState = ClickStates.Delete;
                        }
                        break;
                    case NodesEdges.Nodes:
                        if (e.Button == MouseButtons.Left)
                        {
                            clickedNode = selectedNode;
                            clickState = ClickStates.Add;
                        }
                        break;
                }
            }
        }

        private void imDrawSpace_MouseUp(object sender, MouseEventArgs e)
        {
            if (nodesEdgesState == NodesEdges.Nodes)
                switch (clickState)
                {
                    case ClickStates.Add:
                        clickedNode = AddNode(x, y, Color.Black);
                        break;
                    case ClickStates.Delete:
                        if (clickedNode != null) graph.DeleteNode(clickedNode);
                        break;
                    case ClickStates.Move:
                        break;
                }
            else
                switch (clickState)
                {
                    case ClickStates.Add:
                        if (selectedNode != null && selectedNode != clickedNode)
                            AddEdge(clickedNode, selectedNode, Color.Black);
                        break;

                    case ClickStates.Delete:
                            graph.DeleteEdge(clickedEdge);
                        break;
                }
            clickedNode = null;
            clickedEdge = null;
            clickState = ClickStates.NoClick;
            cbEdgeSizeChange.Items.Clear();
            foreach (var edge in graph.Edges)
                cbEdgeSizeChange.Items.Add(edge);
            if (cbEdgeSizeChange.SelectedIndex == -1)
                nudEdgeSizeChange.Value = 1;
            draw();
        }

        private void pDrawSpace_MouseMove(object sender, MouseEventArgs e)
        {
            x = e.X;
            y = e.Y;

            if (clickState == ClickStates.Move)
            {
                var nodeInfo = nodeInfos[clickedNode];
                nodeInfo.X = x;
                nodeInfo.Y = y;
                draw();
                return;
            }

            var size = trackBarNodeSize.Value;
            selectedNode = null;
            selectedEdge = null;
            mouseOn = NodesEdges.None;
            foreach (var node in graph.Nodes)
            {
                var nodeInfo = nodeInfos[node];
                if (Math.Abs(nodeInfo.X - x) < size / 2 && Math.Abs(nodeInfo.Y - y) < size / 2 && selectedNode == null)
                {
                    mouseOn = NodesEdges.Nodes;
                    nodeInfo.Color = Color.Red;
                    selectedNode = node;
                }
                else
                {
                    nodeInfo.Color = Color.Black;
                }
            }

            foreach (var edge in graph.Edges)
            {
                var edgeInfo = edgeInfos[edge];
                if (pointOnEdge(x, y, edgeInfo) && mouseOn != NodesEdges.Nodes)
                {
                    edgeInfo.Color = Color.Red;
                    mouseOn = NodesEdges.Edges;
                    selectedEdge = edge;
                }
                else
                {
                    edgeInfo.Color = Color.Black;
                }
            }

            draw();
        }

        private bool pointOnEdge(int x, int y, EdgeInfo edge)
        {
            const float e = 0.1f;
            var onLine = false;
            var onSementX = false;
            var onSementY = false;
            if (edge.To.X == edge.From.X)
            {
                onLine = Math.Abs(x - edge.From.X) <= 1;
                onSementX = x - edge.From.X <= 5;
                onSementY = y <= edge.From.Y && y >= edge.To.Y || y <= edge.To.Y && y >= edge.From.Y;
            }
            else if (edge.To.Y == edge.From.Y)
            {
                onLine = Math.Abs(y - edge.From.Y) <= 10;
                onSementX = x <= edge.From.X && x >= edge.To.X || x <= edge.To.X && x >= edge.From.X;
                onSementY = y - edge.From.Y <= 5;
            }
            else
            {
                onLine = Math.Abs((x - edge.From.X) / (float)(edge.To.X - edge.From.X) -
                                    (y - edge.From.Y) / (float)(edge.To.Y - edge.From.Y)) <= e;

                onSementX = x <= edge.From.X && x >= edge.To.X || x <= edge.To.X && x >= edge.From.X;
                onSementY = y <= edge.From.Y && y >= edge.To.Y || y <= edge.To.Y && y >= edge.From.Y;
            }
            return onLine && onSementX && onSementY;
        }

        private void trackBarNodeSize_ValueChanged(object sender, EventArgs e)
        {
            draw();
        }

        private void rbEdges_CheckedChanged(object sender, EventArgs e)
        {
            nodesEdgesState = NodesEdges.Edges;
        }

        private void cbEdgeSizeChange_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbEdgeSizeChange.SelectedItem != null)
                nudEdgeSizeChange.Value = ((IEdge)cbEdgeSizeChange.SelectedItem).Length;
        }

        private void nudEdgeSizeChange_ValueChanged(object sender, EventArgs e)
        {
            if (cbEdgeSizeChange.SelectedItem != null)
            {
                var changeEdge = (IEdge)cbEdgeSizeChange.SelectedItem;
                changeEdge.Length = (int)nudEdgeSizeChange.Value;
            }
        }

        private void RecursiveAlg_Click(object sender, EventArgs e)
        {
            MessageBox.Show(graph.CCcountRecursiveDFS().ToString() + " компонент(ы) связности");
        }

        private void StackAlg_Click(object sender, EventArgs e)
        {
            MessageBox.Show(graph.CCcountStackDFS().ToString() + " компонент(ы) связности");
        }

        private void rbNodes_CheckedChanged(object sender, EventArgs e)
        {
            nodesEdgesState = NodesEdges.Nodes;
        }

        private void draw()
        {
            var buffer = new Bitmap(imDrawSpace.Width, imDrawSpace.Height);
            var bufferGraphics = Graphics.FromImage(buffer);
            var size = trackBarNodeSize.Value;
            foreach (var node in graph.Nodes)
            {
                var nodeInfo = nodeInfos[node];
                var pen = new Pen(nodeInfo.Color);
                bufferGraphics.DrawEllipse(pen, nodeInfo.X - size / 2, nodeInfo.Y - size / 2, size, size);
            }

            foreach (var edge in graph.Edges)
            {
                var edgeInfo = edgeInfos[edge];
                var pen = new Pen(edgeInfo.Color);
                bufferGraphics.DrawLine(pen, edgeInfo.From.X, edgeInfo.From.Y, edgeInfo.To.X, edgeInfo.To.Y);
            }

            //в процессе добавления ребра
            if (clickState == ClickStates.Add && nodesEdgesState == NodesEdges.Edges && clickedNode != null)
            {
                var clickedNodeInfo = nodeInfos[clickedNode];
                var pen = new Pen(Color.Black);
                bufferGraphics.DrawLine(pen, clickedNodeInfo.X, clickedNodeInfo.Y, x, y);
            }

            imDrawSpace.Image = buffer;
        }
    }
}