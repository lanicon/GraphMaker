using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GraphMaker
{
    public partial class Form1 : Form
    {
        enum NodesEdges { Nodes, Edges, None};
        enum ClickStates { Add, Delete, Move, NoClick };
        ClickStates clickState = ClickStates.NoClick;
        NodesEdges nodesEdgesState = NodesEdges.Nodes;
        NodesEdges mouseOn = NodesEdges.None;
        int x, y;
        List<gNode> nodes;
        List<gEdge> edges;
        gNode selectedNode;
        gNode clickedNode;
        gEdge selectedEdge;
        gEdge clickedEdge;
        public Form1()
        {
            InitializeComponent();
            nodes = new List<gNode>();
            edges = new List<gEdge>();
            nodes.Add(new gNode(100, 100, Color.Black));
            nodes.Add(new gNode(200, 200, Color.Black));
            edges.Add(new gEdge(nodes[0], nodes[1], Color.Black));
        }


        private void imDrawSpace_MouseDown(object sender, MouseEventArgs e)
        {
            if(nodesEdgesState == NodesEdges.Nodes)
            {
                if(mouseOn == NodesEdges.None)
                {
                    if(e.Button == System.Windows.Forms.MouseButtons.Left)
                    {
                        clickedNode = new gNode(x, y, Color.Black);
                        clickState = ClickStates.Add;
                    }
                }
                else
                {
                    if (e.Button == System.Windows.Forms.MouseButtons.Left)
                    {
                        clickedNode = selectedNode;
                        clickState = ClickStates.Move;
                    }
                    if (e.Button == System.Windows.Forms.MouseButtons.Right)
                    {
                        clickedNode = selectedNode;
                        clickState = ClickStates.Delete;
                    }

                }
            }
            else
            {
                if(mouseOn == NodesEdges.Edges)
                {
                    if (e.Button == System.Windows.Forms.MouseButtons.Right)
                    {
                        clickedEdge = selectedEdge;
                        clickState = ClickStates.Delete;
                    }
                }
                else
                {
                    if (mouseOn == NodesEdges.Nodes)
                        if (e.Button == System.Windows.Forms.MouseButtons.Left)
                        {
                            clickedNode = selectedNode;
                            clickState = ClickStates.Add;
                        }
                }
            }
        }

        private void imDrawSpace_MouseUp(object sender, MouseEventArgs e)
        {
            if(nodesEdgesState == NodesEdges.Nodes)
                switch (clickState)
                {
                    case ClickStates.Add:
                        nodes.Add(clickedNode);
                        break;
                    case ClickStates.Delete:
                        nodes.Remove(clickedNode);
                        break;
                    case ClickStates.Move:
                        break;
                }
            else
            {
                switch(clickState)
                {
                    case ClickStates.Add:
                        if(selectedNode != null)
                            edges.Add(new gEdge(clickedNode, selectedNode, Color.Black));
                        break;
                    case ClickStates.Delete:
                        edges.Remove(clickedEdge);
                        break;
                }
            }
            clickedNode = null;
            clickedEdge = null;
            clickState = ClickStates.NoClick;
            draw();
        }

        private void pDrawSpace_MouseMove(object sender, MouseEventArgs e)
        {
            x = e.X;
            y = e.Y;
            tbXY.Text=$"{x}:{y}";

            if(clickState == ClickStates.Move)
            {
                clickedNode.x = x;
                clickedNode.y = y;
                draw();
                return;
            }

            int size = trackBarNodeSize.Value;
            selectedNode = null;
            selectedEdge = null;
            mouseOn = NodesEdges.None;
            foreach (var node in nodes)
            {
                if (Math.Abs(node.x - x) < size / 2 && Math.Abs(node.y - y) < size / 2 && selectedNode == null)
                {
                    mouseOn = NodesEdges.Nodes;
                    node.color = Color.Red;
                    selectedNode = node;
                }
                else
                    node.color = Color.Black;
            }
            foreach (var edge in edges)
            {
                if (pointOnEdge(x, y, edge) && mouseOn != NodesEdges.Nodes)
                {
                    edge.color = Color.Red;
                    mouseOn = NodesEdges.Edges;
                    selectedEdge = edge;
                }
                else
                    edge.color = Color.Black;
            }
            draw();
        }

        private bool pointOnEdge(int x, int y, gEdge edge)
        {
            bool onLine = Math.Abs((x - edge.node1.x) / (float)(edge.node2.x - edge.node1.x) - (y - edge.node1.y) / (float)(edge.node2.y - edge.node1.y)) <= 0.1;
            bool onSementX = (x <= edge.node1.x && x >= edge.node2.x) || (x <= edge.node2.x && x >= edge.node1.x);
            bool onSementY = (y <= edge.node1.y && y >= edge.node2.y) || (y <= edge.node2.y && y >= edge.node1.y);
            return onLine && onSementX && onSementY;
        }

        private void trackBarNodeSize_ValueChanged(object sender, EventArgs e)
        {
            draw();
        }


        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
            nodesEdgesState = NodesEdges.Nodes;
        }

        private void rbEdges_CheckedChanged(object sender, EventArgs e)
        {
            nodesEdgesState = NodesEdges.Edges;
        }

        void draw()
        {
            Bitmap buffer = new Bitmap(imDrawSpace.Width, imDrawSpace.Height);
            Graphics bufferGraphics = Graphics.FromImage(buffer); 
            int size = trackBarNodeSize.Value;
            foreach (var node in nodes)
            {
                Pen pen = new Pen(node.color);
                bufferGraphics.DrawEllipse(pen, node.x - size / 2, node.y - size / 2, size, size);
            }
            foreach( var edge in edges)
            {
                Pen pen = new Pen(edge.color);
                bufferGraphics.DrawLine(pen, edge.node1.x, edge.node1.y, edge.node2.x, edge.node2.y);
            }
            if(clickState == ClickStates.Add && nodesEdgesState == NodesEdges.Edges && clickedNode != null)
            {
                Pen pen = new Pen(Color.Black);
                bufferGraphics.DrawLine(pen, clickedNode.x, clickedNode.y, x, y);
            }
            imDrawSpace.Image = buffer;
        }
    }
}
