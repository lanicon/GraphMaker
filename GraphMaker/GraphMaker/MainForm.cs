using System;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using GraphMaker.Extensions;
using GraphMaker.Model;
using GraphMaker.UI;

namespace GraphMaker
{
    public partial class MainForm : Form
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

        private UiGraph graph;

        private ClickStates clickState = ClickStates.NoClick;

        private NodesEdges nodesEdgesState = NodesEdges.Nodes;

        private NodesEdges mouseOn = NodesEdges.None;

        private int x, y;

        private INode selectedNode;

        private INode clickedNode;

        private IEdge selectedEdge;

        private IEdge clickedEdge;

        private int fontImpactId = 3, fontCambriaId = 3;

        public MainForm()
        {
            InitializeComponent();

            graph = UiGraph.New();
            graph.Changed += OnGraphUpdate;

            for (int i = 0; i < FontFamily.Families.Length; i++)
            {
                if (FontFamily.Families[i].Name == "Impact")
                    fontImpactId = i;
                if (FontFamily.Families[i].Name == "Cambria")
                    fontCambriaId = i;
            }
        }

        private void OnGraphUpdate(GraphOperation operation, object obj)
        {
            switch (operation)
            {
                case GraphOperation.AddNode:
                    break;
                case GraphOperation.AddEdge:
                    cbEdgeSizeChange.Items.Add(obj);
                    break;
                case GraphOperation.DeleteNode:
                    break;
                case GraphOperation.DeleteEdge:
                    cbEdgeSizeChange.Items.Remove(obj);
                    break;
            }
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
                        if (clickedNode != null)
                        {
                            graph.DeleteNode(clickedNode);
                        }
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
                        }
                        break;

                    case ClickStates.Delete:
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
                var changeEdge = (IEdge)cbEdgeSizeChange.SelectedItem;
                changeEdge.Length = (int)nudEdgeSizeChange.Value;
            }
            draw();
        }

        private void nudEdgeSizeChange_KeyUp(object sender, KeyEventArgs e)
        {
            if (cbEdgeSizeChange.SelectedItem != null)
            {
                var changeEdge = (IEdge)cbEdgeSizeChange.SelectedItem;
                changeEdge.Length = (int)nudEdgeSizeChange.Value;
            }
            draw();
        }

        private void RecursiveAlg_Click(object sender, EventArgs e)
        {
            var count = graph.CCcountRecursiveDFS();
            if (count == 1)
                MessageBox.Show("1 компонента связности.");
            else
                MessageBox.Show(count + " компонент(ы) связности.");
        }

        private void StackAlg_Click(object sender, EventArgs e)
        {
            var count = graph.CCcountStackDFS();
            if (count == 1)
                MessageBox.Show("1 компонента связности.");
            else
                MessageBox.Show(count + " компонент(ы) связности.");
        }

        private void rbNodes_CheckedChanged(object sender, EventArgs e)
        {
            nodesEdgesState = NodesEdges.Nodes;
        }

        private void showComponentsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            edgesColorBlack();
            Color[] colors = { Color.Green, Color.Yellow, Color.Purple, Color.Brown, Color.Pink, Color.Aqua,
            Color.AntiqueWhite, Color.Crimson, Color.DarkGray, Color.DarkOrange, Color.DeepPink, Color.DarkViolet,
            Color.DeepSkyBlue, Color.Fuchsia, Color.Gold, Color.IndianRed, Color.Lime, Color.Maroon};
            var listOfComponents = graph.GetListOfComponents();
            //string answer = string.Empty;
            int i=0;
            foreach (var component in listOfComponents)
            {
                //answer += "компонента: ";
                foreach (var node in component)
                {
                    //answer += node.Number + " ";
                    graph.NodeInfos[node].Color = colors[i];
                }
                //answer += "\n";

                if (i == (colors.Length - 1))
                    i = 0;
                else
                    i++;
            }
            draw();
            var count = listOfComponents.Count;
            if (count == 1)
                MessageBox.Show("1 компонента связности.");
            else
                MessageBox.Show(count + " компонент(ы) связности.");
        }

        private void MST_Click(object sender, EventArgs e)
        {
            edgesColorBlack();
            try
            {
                var minTree = graph.MinTreePrim();
                if(minTree != null)
                {
                    MessageBox.Show(graph.PrintEdges(minTree));
                }
                foreach (var node in graph.Nodes)
                    graph.NodeInfos[node].Color = Color.White;

                Color color = Color.Blue;
                if (minTree != null)
                    foreach (var edge in minTree)
                    {
                        var edgeInfo = graph.EdgeInfos[edge];
                        edgeInfo.Color = color;
                    }
            }
            catch(Exception exep)
            {
                MessageBox.Show(exep.Message);
            }
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
                    string json;
                    try
                    {
                        json = UiGraph.Serialize(graph);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"Ошибка при сериализации графа: {ex.Message}");
                        return;
                    }
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
                    UiGraph deserializedGraph;
                    try
                    {
                        deserializedGraph = UiGraph.Deserialize(json);
                        if (deserializedGraph == null)
                        {
                            throw new ArgumentException("Файл пуст.");
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"Не удалось десериализовать граф из указанного файла: {ex.Message}");
                        return;
                    }
                    graph = deserializedGraph;
                    graph.Changed += OnGraphUpdate;
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
            if (graph.Nodes.Any() && SaveFileIfNecessary() == DialogResult.Cancel)
            {
                return;
            }
            cbEdgeSizeChange.Items.Clear();
            graph = UiGraph.New();
            graph.Changed += OnGraphUpdate;
            draw();
        }

        private DialogResult SaveFileIfNecessary()
        {
            var dialogResult = MessageBox.Show("Сохранить файл? Все несохраненные изменения будут потеряны.", "",
                MessageBoxButtons.YesNoCancel);

            if (dialogResult == DialogResult.Yes)
            {
                SaveGraphToFile();
            }

            return dialogResult;
        }

        private INode spSelectedNode1;
        private INode spSelectedNode2;
        private bool selectionMode = false;

        private void shortestPath_Click(object sender, EventArgs e)
        {
            edgesColorBlack();
            foreach (var node in graph.NodeInfos)
            {
                node.Value.Color = Color.White;
            }
            spSelectedNode1 = null;
            spSelectedNode2 = null;
            if (selectionMode)
            {
                selectionMode = false;
                this.imDrawSpace.MouseDown += new System.Windows.Forms.MouseEventHandler(this.imDrawSpace_MouseDown);
                this.imDrawSpace.MouseUp += new System.Windows.Forms.MouseEventHandler(this.imDrawSpace_MouseUp);
                this.imDrawSpace.MouseClick -= new System.Windows.Forms.MouseEventHandler(this.selectNodes);
                shortestPath_ToolMenuStrip.BackColor = Color.White;
                gbShortestPath.Visible = false;
                MessageBox.Show("Выбор вершин отменен.");
            }
            else
            {
                selectionMode = true;
                this.imDrawSpace.MouseDown -= new System.Windows.Forms.MouseEventHandler(this.imDrawSpace_MouseDown);
                this.imDrawSpace.MouseUp -= new System.Windows.Forms.MouseEventHandler(this.imDrawSpace_MouseUp);
                this.imDrawSpace.MouseClick += new System.Windows.Forms.MouseEventHandler(this.selectNodes);
                shortestPath_ToolMenuStrip.BackColor = Color.Red;
                gbShortestPath.Visible = true;
                tbShortestPath.Text = @"Выберите первую вершину.
Для отмены нажмите кнопку еще раз.";
            }
        }
        
        private void selectNodes(object sender, MouseEventArgs e)
        {
            if (NodesEdges.Nodes == mouseOn)
            {
                if (e.Button == MouseButtons.Left)
                {
                    if (spSelectedNode1 == null)
                    {
                        spSelectedNode1 = selectedNode;
                        graph.NodeInfos[spSelectedNode1].Color = Color.Green;
                        tbShortestPath.Text = "Выберите вторую вершину.";
                    }
                    else
                    {
                        if (selectedNode == spSelectedNode1)
                        {
                            tbShortestPath.Text = "Выберите другую вершину.";
                        }
                        else
                        {
                            spSelectedNode2 = selectedNode;
                            graph.NodeInfos[spSelectedNode2].Color = Color.Red;
                            findShortestPath();
                        }
                    }
                }
            }
        }

        private void findShortestPath()
        {
            gbShortestPath.Visible = false;
            selectionMode = false;
            this.imDrawSpace.MouseDown += new System.Windows.Forms.MouseEventHandler(this.imDrawSpace_MouseDown);
            this.imDrawSpace.MouseUp += new System.Windows.Forms.MouseEventHandler(this.imDrawSpace_MouseUp);
            this.imDrawSpace.MouseClick -= new System.Windows.Forms.MouseEventHandler(this.selectNodes);
            var shortestPath = graph.GetShortestPath(spSelectedNode1, spSelectedNode2);
            shortestPath_ToolMenuStrip.BackColor = Color.White;
            if (shortestPath == null)
            {
                MessageBox.Show("Между данными вершинами нет пути.");
            }
            else
            {
                var outStr = "";
                var path = 0;
                foreach (var edge in shortestPath)
                {
                    graph.EdgeInfos[edge].Color = Color.Blue;
                    outStr += edge.ToString() + " (" + edge.Length + ")\n";
                    path += edge.Length;
                }
                MessageBox.Show("Длина кратчайшего пути = " + path + "\n" + outStr);
            }            
        }

        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (e.CloseReason == CloseReason.UserClosing && graph.Nodes.Any())
            {
                SaveFileIfNecessary();
            }
        }

        private void edgesColorBlack()
        {
            foreach (var edge in graph.EdgeInfos)
            {
                edge.Value.Color = Color.Black;
            }
        }
        
        private void draw()
        {
            var buffer = new Bitmap(imDrawSpace.Width, imDrawSpace.Height);
            var bufferGraphics = Graphics.FromImage(buffer);
            var size = trackBarNodeSize.Value;
            bufferGraphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;

            //отрисовка ребер
            foreach (var edge in graph.Edges)
            {
                var edgeInfo = graph.EdgeInfos[edge];
                int width = (cbEdgeSizeChange.SelectedItem as IEdge == edge) ? 3 : 1;
                
                var pen = (edge == selectedEdge) ? new Pen(Color.Red,width) : new Pen(edgeInfo.Color,width);

                bufferGraphics.DrawLine(pen, edgeInfo.First.X, edgeInfo.First.Y, edgeInfo.Second.X, edgeInfo.Second.Y);

                int lowestX = edgeInfo.First.X < edgeInfo.Second.X ? edgeInfo.First.X : edgeInfo.Second.X;
                int lowestY = edgeInfo.First.Y < edgeInfo.Second.Y ? edgeInfo.First.Y : edgeInfo.Second.Y;
                Point M = new Point(lowestX + Math.Abs(edgeInfo.First.X - edgeInfo.Second.X) / 2, lowestY + Math.Abs(edgeInfo.First.Y - edgeInfo.Second.Y) / 2);
                Pen basicPen = new Pen(Color.Black, 1);
                int fontSize = 13;
                Font font = new Font(FontFamily.Families[fontCambriaId], fontSize);  // Font: Cambria
                string text = edge.Length.ToString();
                bufferGraphics.DrawString(text, font, basicPen.Brush, M);

            }


            //отрисовка вершин
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
                Font font = new Font(FontFamily.Families[fontImpactId], fontSize);  // Font: Impact
                string text = node.Number.ToString();
                bufferGraphics.DrawString(text, font, basicPen.Brush, nodeInfo.X - text.Length * fontSize / 2, nodeInfo.Y - fontSize / 2);
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