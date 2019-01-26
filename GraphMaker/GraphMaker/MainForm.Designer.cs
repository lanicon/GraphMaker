namespace GraphMaker
{
    partial class Form1
    {
        /// <summary>
        /// Обязательная переменная конструктора.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Освободить все используемые ресурсы.
        /// </summary>
        /// <param name="disposing">истинно, если управляемый ресурс должен быть удален; иначе ложно.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Код, автоматически созданный конструктором форм Windows

        /// <summary>
        /// Требуемый метод для поддержки конструктора — не изменяйте 
        /// содержимое этого метода с помощью редактора кода.
        /// </summary>
        private void InitializeComponent()
        {
            this.trackBarNodeSize = new System.Windows.Forms.TrackBar();
            this.imDrawSpace = new System.Windows.Forms.PictureBox();
            this.rbNodes = new System.Windows.Forms.RadioButton();
            this.rbEdges = new System.Windows.Forms.RadioButton();
            this.msGraphMaker = new System.Windows.Forms.MenuStrip();
            this.файлToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.CreateNewFile = new System.Windows.Forms.ToolStripMenuItem();
            this.OpenFile = new System.Windows.Forms.ToolStripMenuItem();
            this.SaveFile = new System.Windows.Forms.ToolStripMenuItem();
            this.нахождениеМинимальногоToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.нахождениеМинимальногоОстовногоДереваToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.нахождениеКоличестваКомпонентСвязностиToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.RecursiveAlg = new System.Windows.Forms.ToolStripMenuItem();
            this.StackAlg = new System.Windows.Forms.ToolStripMenuItem();
            this.gbRadioButtons = new System.Windows.Forms.GroupBox();
            this.gbNodeSize = new System.Windows.Forms.GroupBox();
            this.gbEdgeSizeChange = new System.Windows.Forms.GroupBox();
            this.nudEdgeSizeChange = new System.Windows.Forms.NumericUpDown();
            this.cbEdgeSizeChange = new System.Windows.Forms.ComboBox();
            this.showComponentsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            ((System.ComponentModel.ISupportInitialize)(this.trackBarNodeSize)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.imDrawSpace)).BeginInit();
            this.msGraphMaker.SuspendLayout();
            this.gbRadioButtons.SuspendLayout();
            this.gbNodeSize.SuspendLayout();
            this.gbEdgeSizeChange.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudEdgeSizeChange)).BeginInit();
            this.SuspendLayout();
            // 
            // trackBarNodeSize
            // 
            this.trackBarNodeSize.Location = new System.Drawing.Point(8, 33);
            this.trackBarNodeSize.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.trackBarNodeSize.Maximum = 50;
            this.trackBarNodeSize.Minimum = 10;
            this.trackBarNodeSize.Name = "trackBarNodeSize";
            this.trackBarNodeSize.Size = new System.Drawing.Size(139, 56);
            this.trackBarNodeSize.TabIndex = 1;
            this.trackBarNodeSize.Value = 30;
            this.trackBarNodeSize.ValueChanged += new System.EventHandler(this.trackBarNodeSize_ValueChanged);
            // 
            // imDrawSpace
            // 
            this.imDrawSpace.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.imDrawSpace.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.imDrawSpace.Location = new System.Drawing.Point(16, 137);
            this.imDrawSpace.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.imDrawSpace.Name = "imDrawSpace";
            this.imDrawSpace.Size = new System.Drawing.Size(1101, 529);
            this.imDrawSpace.TabIndex = 2;
            this.imDrawSpace.TabStop = false;
            this.imDrawSpace.MouseDown += new System.Windows.Forms.MouseEventHandler(this.imDrawSpace_MouseDown);
            this.imDrawSpace.MouseMove += new System.Windows.Forms.MouseEventHandler(this.pDrawSpace_MouseMove);
            this.imDrawSpace.MouseUp += new System.Windows.Forms.MouseEventHandler(this.imDrawSpace_MouseUp);
            // 
            // rbNodes
            // 
            this.rbNodes.AutoSize = true;
            this.rbNodes.Checked = true;
            this.rbNodes.Location = new System.Drawing.Point(8, 52);
            this.rbNodes.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.rbNodes.Name = "rbNodes";
            this.rbNodes.Size = new System.Drawing.Size(70, 21);
            this.rbNodes.TabIndex = 3;
            this.rbNodes.TabStop = true;
            this.rbNodes.Text = "Nodes";
            this.rbNodes.UseVisualStyleBackColor = true;
            this.rbNodes.CheckedChanged += new System.EventHandler(this.rbNodes_CheckedChanged);
            // 
            // rbEdges
            // 
            this.rbEdges.AutoSize = true;
            this.rbEdges.Location = new System.Drawing.Point(8, 23);
            this.rbEdges.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.rbEdges.Name = "rbEdges";
            this.rbEdges.Size = new System.Drawing.Size(69, 21);
            this.rbEdges.TabIndex = 4;
            this.rbEdges.Text = "Edges";
            this.rbEdges.UseVisualStyleBackColor = true;
            this.rbEdges.CheckedChanged += new System.EventHandler(this.rbEdges_CheckedChanged);
            // 
            // msGraphMaker
            // 
            this.msGraphMaker.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.msGraphMaker.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.файлToolStripMenuItem,
            this.нахождениеМинимальногоToolStripMenuItem,
            this.нахождениеМинимальногоОстовногоДереваToolStripMenuItem,
            this.нахождениеКоличестваКомпонентСвязностиToolStripMenuItem});
            this.msGraphMaker.Location = new System.Drawing.Point(0, 0);
            this.msGraphMaker.Name = "msGraphMaker";
            this.msGraphMaker.Padding = new System.Windows.Forms.Padding(8, 2, 0, 2);
            this.msGraphMaker.Size = new System.Drawing.Size(1133, 28);
            this.msGraphMaker.TabIndex = 5;
            this.msGraphMaker.Text = "menuStrip1";
            // 
            // файлToolStripMenuItem
            // 
            this.файлToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.CreateNewFile,
            this.OpenFile,
            this.SaveFile});
            this.файлToolStripMenuItem.Name = "файлToolStripMenuItem";
            this.файлToolStripMenuItem.Size = new System.Drawing.Size(57, 24);
            this.файлToolStripMenuItem.Text = "Файл";
            // 
            // CreateNewFile
            // 
            this.CreateNewFile.Name = "CreateNewFile";
            this.CreateNewFile.Size = new System.Drawing.Size(289, 26);
            this.CreateNewFile.Text = "Создать новый файл";
            // 
            // OpenFile
            // 
            this.OpenFile.Name = "OpenFile";
            this.OpenFile.Size = new System.Drawing.Size(289, 26);
            this.OpenFile.Text = "Открыть существующий файл";
            // 
            // SaveFile
            // 
            this.SaveFile.Name = "SaveFile";
            this.SaveFile.Size = new System.Drawing.Size(289, 26);
            this.SaveFile.Text = "Сохранить файл";
            // 
            // нахождениеМинимальногоToolStripMenuItem
            // 
            this.нахождениеМинимальногоToolStripMenuItem.Name = "нахождениеМинимальногоToolStripMenuItem";
            this.нахождениеМинимальногоToolStripMenuItem.Size = new System.Drawing.Size(303, 24);
            this.нахождениеМинимальногоToolStripMenuItem.Text = "Нахождение минимального расстояния";
            // 
            // нахождениеМинимальногоОстовногоДереваToolStripMenuItem
            // 
            this.нахождениеМинимальногоОстовногоДереваToolStripMenuItem.Name = "нахождениеМинимальногоОстовногоДереваToolStripMenuItem";
            this.нахождениеМинимальногоОстовногоДереваToolStripMenuItem.Size = new System.Drawing.Size(348, 24);
            this.нахождениеМинимальногоОстовногоДереваToolStripMenuItem.Text = "Нахождение минимального остовного дерева";
            // 
            // нахождениеКоличестваКомпонентСвязностиToolStripMenuItem
            // 
            this.нахождениеКоличестваКомпонентСвязностиToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.RecursiveAlg,
            this.StackAlg,
            this.showComponentsToolStripMenuItem});
            this.нахождениеКоличестваКомпонентСвязностиToolStripMenuItem.Name = "нахождениеКоличестваКомпонентСвязностиToolStripMenuItem";
            this.нахождениеКоличестваКомпонентСвязностиToolStripMenuItem.Size = new System.Drawing.Size(346, 24);
            this.нахождениеКоличестваКомпонентСвязностиToolStripMenuItem.Text = "Нахождение количества компонент связности";
            // 
            // RecursiveAlg
            // 
            this.RecursiveAlg.Name = "RecursiveAlg";
            this.RecursiveAlg.Size = new System.Drawing.Size(258, 26);
            this.RecursiveAlg.Text = "Реккурсивный алгоритм";
            this.RecursiveAlg.Click += new System.EventHandler(this.RecursiveAlg_Click);
            // 
            // StackAlg
            // 
            this.StackAlg.Name = "StackAlg";
            this.StackAlg.Size = new System.Drawing.Size(258, 26);
            this.StackAlg.Text = "Алгоритм на стеке";
            this.StackAlg.Click += new System.EventHandler(this.StackAlg_Click);
            // 
            // gbRadioButtons
            // 
            this.gbRadioButtons.Controls.Add(this.rbEdges);
            this.gbRadioButtons.Controls.Add(this.rbNodes);
            this.gbRadioButtons.Location = new System.Drawing.Point(16, 33);
            this.gbRadioButtons.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.gbRadioButtons.Name = "gbRadioButtons";
            this.gbRadioButtons.Padding = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.gbRadioButtons.Size = new System.Drawing.Size(92, 96);
            this.gbRadioButtons.TabIndex = 6;
            this.gbRadioButtons.TabStop = false;
            // 
            // gbNodeSize
            // 
            this.gbNodeSize.Controls.Add(this.trackBarNodeSize);
            this.gbNodeSize.Location = new System.Drawing.Point(116, 33);
            this.gbNodeSize.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.gbNodeSize.Name = "gbNodeSize";
            this.gbNodeSize.Padding = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.gbNodeSize.Size = new System.Drawing.Size(157, 96);
            this.gbNodeSize.TabIndex = 7;
            this.gbNodeSize.TabStop = false;
            this.gbNodeSize.Text = "Размер вершин";
            // 
            // gbEdgeSizeChange
            // 
            this.gbEdgeSizeChange.Controls.Add(this.nudEdgeSizeChange);
            this.gbEdgeSizeChange.Controls.Add(this.cbEdgeSizeChange);
            this.gbEdgeSizeChange.Location = new System.Drawing.Point(281, 33);
            this.gbEdgeSizeChange.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.gbEdgeSizeChange.Name = "gbEdgeSizeChange";
            this.gbEdgeSizeChange.Padding = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.gbEdgeSizeChange.Size = new System.Drawing.Size(189, 96);
            this.gbEdgeSizeChange.TabIndex = 8;
            this.gbEdgeSizeChange.TabStop = false;
            this.gbEdgeSizeChange.Text = "Измение веса ребра";
            // 
            // nudEdgeSizeChange
            // 
            this.nudEdgeSizeChange.Location = new System.Drawing.Point(8, 57);
            this.nudEdgeSizeChange.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.nudEdgeSizeChange.Maximum = new decimal(new int[] {
            1000,
            0,
            0,
            0});
            this.nudEdgeSizeChange.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.nudEdgeSizeChange.Name = "nudEdgeSizeChange";
            this.nudEdgeSizeChange.Size = new System.Drawing.Size(160, 22);
            this.nudEdgeSizeChange.TabIndex = 10;
            this.nudEdgeSizeChange.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.nudEdgeSizeChange.ValueChanged += new System.EventHandler(this.nudEdgeSizeChange_ValueChanged);
            // 
            // cbEdgeSizeChange
            // 
            this.cbEdgeSizeChange.DropDownHeight = 42;
            this.cbEdgeSizeChange.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbEdgeSizeChange.FormattingEnabled = true;
            this.cbEdgeSizeChange.IntegralHeight = false;
            this.cbEdgeSizeChange.ItemHeight = 16;
            this.cbEdgeSizeChange.Location = new System.Drawing.Point(8, 23);
            this.cbEdgeSizeChange.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.cbEdgeSizeChange.Name = "cbEdgeSizeChange";
            this.cbEdgeSizeChange.Size = new System.Drawing.Size(160, 24);
            this.cbEdgeSizeChange.TabIndex = 0;
            this.cbEdgeSizeChange.SelectedIndexChanged += new System.EventHandler(this.cbEdgeSizeChange_SelectedIndexChanged);
            // 
            // showComponentsToolStripMenuItem
            // 
            this.showComponentsToolStripMenuItem.Name = "showComponentsToolStripMenuItem";
            this.showComponentsToolStripMenuItem.Size = new System.Drawing.Size(258, 26);
            this.showComponentsToolStripMenuItem.Text = "Отобразить компоненты";
            this.showComponentsToolStripMenuItem.Click += new System.EventHandler(this.showComponentsToolStripMenuItem_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(1133, 681);
            this.Controls.Add(this.gbEdgeSizeChange);
            this.Controls.Add(this.gbNodeSize);
            this.Controls.Add(this.gbRadioButtons);
            this.Controls.Add(this.imDrawSpace);
            this.Controls.Add(this.msGraphMaker);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MainMenuStrip = this.msGraphMaker;
            this.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.Name = "Form1";
            this.Text = "Graph Maker";
            ((System.ComponentModel.ISupportInitialize)(this.trackBarNodeSize)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.imDrawSpace)).EndInit();
            this.msGraphMaker.ResumeLayout(false);
            this.msGraphMaker.PerformLayout();
            this.gbRadioButtons.ResumeLayout(false);
            this.gbRadioButtons.PerformLayout();
            this.gbNodeSize.ResumeLayout(false);
            this.gbNodeSize.PerformLayout();
            this.gbEdgeSizeChange.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.nudEdgeSizeChange)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.TrackBar trackBarNodeSize;
        private System.Windows.Forms.PictureBox imDrawSpace;
        private System.Windows.Forms.RadioButton rbNodes;
        private System.Windows.Forms.RadioButton rbEdges;
        private System.Windows.Forms.MenuStrip msGraphMaker;
        private System.Windows.Forms.ToolStripMenuItem файлToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem нахождениеМинимальногоToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem нахождениеМинимальногоОстовногоДереваToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem нахождениеКоличестваКомпонентСвязностиToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem CreateNewFile;
        private System.Windows.Forms.ToolStripMenuItem OpenFile;
        private System.Windows.Forms.ToolStripMenuItem SaveFile;
        private System.Windows.Forms.GroupBox gbRadioButtons;
        private System.Windows.Forms.GroupBox gbNodeSize;
        private System.Windows.Forms.GroupBox gbEdgeSizeChange;
        private System.Windows.Forms.NumericUpDown nudEdgeSizeChange;
        private System.Windows.Forms.ComboBox cbEdgeSizeChange;
        private System.Windows.Forms.ToolStripMenuItem RecursiveAlg;
        private System.Windows.Forms.ToolStripMenuItem StackAlg;
        private System.Windows.Forms.ToolStripMenuItem showComponentsToolStripMenuItem;
    }
}

