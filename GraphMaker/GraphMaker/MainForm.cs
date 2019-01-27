using System;
using System.Drawing;
using System.IO;
using System.Reflection;
using System.Resources;
using System.Windows.Forms;
using GraphMaker.Extensions;
using GraphMaker.Model;
using GraphMaker.UI;

namespace GraphMaker
{
    public partial class Form1 : Form
    {
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

        private const int DefaultLength = 1;

        private UiGraph graph = UiGraph.New();

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
        
        private void imDrawSpace_MouseDown(object sender, MouseEventArgs e)
        {
            if (nodesEdgesState == NodesEdges.Nodes)
                switch (mouseOn)
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
                    case NodesEdges.Edges:
                        if (e.Button == MouseButtons.Left)
                        {
                            clickedEdge = selectedEdge;
                            cbEdgeSizeChange.SelectedItem = selectedEdge;
                        }
                        break;
                }
            else
                switch (mouseOn)
                {
                    case NodesEdges.Edges:
                        if (e.Button == MouseButtons.Right)
                        {
                            clickedEdge = selectedEdge;
                            clickState = ClickStates.Delete;
                        }
                        if(e.Button == MouseButtons.Left)
                        {
                            clickedEdge = selectedEdge;
                            cbEdgeSizeChange.SelectedItem = selectedEdge;
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

        private void imDrawSpace_MouseUp(object sender, MouseEventArgs e)
        {
            if (nodesEdgesState == NodesEdges.Nodes)
                switch (clickState)
                {
                    case ClickStates.Add:
                        clickedNode = graph.AddNode(x, y, Color.White);
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
                        {
                            IEdge edge = graph.AddEdge(clickedNode, selectedNode, DefaultLength);
                            cbEdgeSizeChange.Items.Add(edge);
                        }
                        break;

                    case ClickStates.Delete:
                        cbEdgeSizeChange.Items.Remove(clickedEdge);
                        graph.DeleteEdge(clickedEdge);
                        break;
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

            if (clickState == ClickStates.Move)
            {
                var nodeInfo = graph.NodeInfos[clickedNode];
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
                var nodeInfo = graph.NodeInfos[node];
                if (Math.Abs(nodeInfo.X - x) < size / 2 && Math.Abs(nodeInfo.Y - y) < size / 2 && selectedNode == null)
                {
                    mouseOn = NodesEdges.Nodes;
                    selectedNode = node;
                }
            }

            foreach (var edge in graph.Edges)
            {
                var edgeInfo = graph.EdgeInfos[edge];
                if (pointOnEdge(x, y, edgeInfo) && mouseOn != NodesEdges.Nodes && selectedEdge == null)
                {
                    mouseOn = NodesEdges.Edges;
                    selectedEdge = edge;
                }
            }

            draw();
        }

        private bool pointOnEdge(int x, int y, EdgeInfo edge)
        {
            const int d = 10;
            var onLine = false;
            var onSementX = false;
            var onSementY = false;
            if (edge.Second.X == edge.First.X)
            {
                onLine = Math.Abs(x - edge.First.X) <= 1;
                onSementX = Math.Abs(x - edge.First.X) <= 5;
                onSementY = y <= edge.First.Y && y >= edge.Second.Y || y <= edge.Second.Y && y >= edge.First.Y;
                return onLine && onSementX && onSementY;
            }
            else if (edge.Second.Y == edge.First.Y)
            {
                onLine = Math.Abs(y - edge.First.Y) <= 1;
                onSementX = x <= edge.First.X && x >= edge.Second.X || x <= edge.Second.X && x >= edge.First.X;
                onSementY = Math.Abs(y - edge.First.Y) <= 5;
                return onLine && onSementX && onSementY;
            }
            else
            {
                float k = (edge.Second.Y - edge.First.Y) / (float)(edge.Second.X - edge.First.X);
                float b = (-edge.First.X * (edge.Second.Y - edge.First.Y)) / (float)(edge.Second.X - edge.First.X) + edge.First.Y;


                bool top = y <= (k * x + b + d);
                bool bottom = y >= (k * x + b - d);
                bool left, right;
                if (edge.First.Y < edge.Second.Y)
                {
                    left = y >= ((edge.First.X - x) / k + edge.First.Y);
                    right = y <= ((edge.Second.X - x) / k + edge.Second.Y);
                }
                else
                {
                    left = y >= ((edge.Second.X - x) / k + edge.Second.Y);
                    right = y <= ((edge.First.X - x) / k + edge.First.Y);
                }
                return top && bottom && left && right;
            }

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
                nudEdgeSizeChange.Value = ((IEdge) cbEdgeSizeChange.SelectedItem).Length;
            draw();
        }

        private void nudEdgeSizeChange_ValueChanged(object sender, EventArgs e)
        {
            if (cbEdgeSizeChange.SelectedItem != null)
            {
                var changeEdge = (IEdge) cbEdgeSizeChange.SelectedItem;
                changeEdge.Length = (int) nudEdgeSizeChange.Value;
            }
        }

        private void RecursiveAlg_Click(object sender, EventArgs e)
        {
            MessageBox.Show(graph.CCcountRecursiveDFS() + " компонент(ы) связности");
        }

        private void StackAlg_Click(object sender, EventArgs e)
        {
            MessageBox.Show(graph.CCcountStackDFS() + " компонент(ы) связности");
        }

        private void rbNodes_CheckedChanged(object sender, EventArgs e)
        {
            nodesEdgesState = NodesEdges.Nodes;
        }

        private void showComponentsToolStripMenuItem_Click(object sender, EventArgs e)
        {

            Color[] colors = { Color.Blue, Color.Green, Color.Yellow, Color.Purple, Color.Brown, Color.Pink, Color.RoyalBlue };
            var listOfComponents = graph.GetListOfComponents();
            string answer = string.Empty;
            int i=0;
            foreach (var component in listOfComponents)
            {
                answer += "компонента: ";
                foreach (var node in component)
                {
                    answer += node.Number + " ";
                    graph.NodeInfos[node].Color = colors[i];
                }
                answer += "\n";

                if (i == (colors.Length - 1))
                    i = 0;
                else
                    i++;
            }
            draw();
            MessageBox.Show(listOfComponents.Count + " компонент(ы) связности\n" + answer);
            // Тут сделает Ярик, как ему удобнее использовать этот метод, чтобы отобразить компоненты
        }
        private void SaveFile_Click(object sender, EventArgs e)
        {
            SaveGraphToFile();
        }

        private void SaveGraphToFile()
        {
            using (var saveFileDialog = new SaveFileDialog())
            {
                saveFileDialog.Filter = "Json files (*.json)|*.json|Text files (*.txt)|*.txt";
                saveFileDialog.FilterIndex = 1;
                saveFileDialog.RestoreDirectory = true;

                if (saveFileDialog.ShowDialog() == DialogResult.OK)
                {
                    var fileName = saveFileDialog.FileName;
                    var json = UiGraph.Serialize(graph);
                    File.WriteAllText(fileName, json);
                    draw();
                }
            }
        }

        private void OpenFile_Click(object sender, EventArgs e)
        {
            using (var openFileDialog = new OpenFileDialog())
            {
                openFileDialog.Filter = "Json files (*.json)|*.json|Text files (*.txt)|*.txt";
                openFileDialog.FilterIndex = 1;
                openFileDialog.RestoreDirectory = true;

                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    var fileName = openFileDialog.FileName;
                    var json = File.ReadAllText(fileName);
                    graph = UiGraph.Deserialize(json);
                    selectedEdge = null;
                    clickedEdge = null;
                    cbEdgeSizeChange.Items.Clear();
                    foreach (var edge in graph.Edges)
                        cbEdgeSizeChange.Items.Add(edge);
                    if (cbEdgeSizeChange.SelectedIndex == -1)
                        nudEdgeSizeChange.Value = 1;
                    draw();
                }
            }
        }

        private void CreateNewFile_Click(object sender, EventArgs e)
        {
            var dialogResult = MessageBox.Show("Сохранить файл? Все несохраненные изменения будут потеряны.", "", MessageBoxButtons.YesNoCancel);
            switch (dialogResult)
            {
                case DialogResult.Cancel:
                    return;
                case DialogResult.Yes:
                    SaveGraphToFile();
                    break;
            }
            graph = UiGraph.New();
            draw();
        }

        private void draw()
        {
            var buffer = new Bitmap(imDrawSpace.Width, imDrawSpace.Height);
            var bufferGraphics = Graphics.FromImage(buffer);
            var size = trackBarNodeSize.Value;


            //отрисовка ребер
            foreach (var edge in graph.Edges)
            {
                var edgeInfo = graph.EdgeInfos[edge];
                var pen = (edge == selectedEdge) ? new Pen(Color.Red) : new Pen(edgeInfo.Color);
                bufferGraphics.DrawLine(pen, edgeInfo.First.X, edgeInfo.First.Y, edgeInfo.Second.X, edgeInfo.Second.Y);
            }


            //отрисовка вершинд
            foreach (var node in graph.Nodes)
            {
                var nodeInfo = graph.NodeInfos[node];
                var pen = (node == selectedNode) ? new Pen(Color.Red) : new Pen(nodeInfo.Color);
                int x = nodeInfo.X - size / 2;
                int y = nodeInfo.Y - size / 2;
                bufferGraphics.FillEllipse(pen.Brush, x, y, size, size);
                Pen basicPen = new Pen(Color.Black);
                bufferGraphics.DrawEllipse(basicPen, x, y, size, size);
                int fontSize = 10;
                Font font = new Font(FontFamily.GenericSerif, fontSize);
                string text = node.Number.ToString();
                bufferGraphics.DrawString(text, font, basicPen.Brush, nodeInfo.X - text.Length*fontSize / 2, nodeInfo.Y - fontSize/3);
            }

            //отрисовка квадрата вокруг выбранного ребра
            if (cbEdgeSizeChange.SelectedItem as IEdge != null)
            {
                EdgeInfo edgeInfo = graph.EdgeInfos[cbEdgeSizeChange.SelectedItem as IEdge];
                int x = edgeInfo.First.X < edgeInfo.Second.X ? edgeInfo.First.X : edgeInfo.Second.X;
                int y = edgeInfo.First.Y < edgeInfo.Second.Y ? edgeInfo.First.Y : edgeInfo.Second.Y;
                int width = Math.Abs(edgeInfo.First.X - edgeInfo.Second.X);
                width = width == 0 ? 2 : width;
                int height = Math.Abs(edgeInfo.First.Y - edgeInfo.Second.Y);
                height = height == 0 ? 2 : height;
                Pen pen = new Pen(Color.Blue);
                Rectangle rect = new Rectangle(x, y, width, height);
                bufferGraphics.DrawRectangle(pen, rect);
            }

            //в процессе добавления ребра
            if (clickState == ClickStates.Add && nodesEdgesState == NodesEdges.Edges && clickedNode != null)
            {
                var clickedNodeInfo = graph.NodeInfos[clickedNode];
                var pen = new Pen(Color.Black);
                bufferGraphics.DrawLine(pen, clickedNodeInfo.X, clickedNodeInfo.Y, x, y);
            }

            imDrawSpace.Image = buffer;
        }
    }
}