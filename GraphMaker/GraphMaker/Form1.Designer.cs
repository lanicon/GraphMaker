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
            this.tbXY = new System.Windows.Forms.TextBox();
            this.trackBarNodeSize = new System.Windows.Forms.TrackBar();
            this.imDrawSpace = new System.Windows.Forms.PictureBox();
            this.rbNodes = new System.Windows.Forms.RadioButton();
            this.rbEdges = new System.Windows.Forms.RadioButton();
            ((System.ComponentModel.ISupportInitialize)(this.trackBarNodeSize)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.imDrawSpace)).BeginInit();
            this.SuspendLayout();
            // 
            // tbXY
            // 
            this.tbXY.Location = new System.Drawing.Point(12, 12);
            this.tbXY.Name = "tbXY";
            this.tbXY.Size = new System.Drawing.Size(100, 20);
            this.tbXY.TabIndex = 0;
            // 
            // trackBarNodeSize
            // 
            this.trackBarNodeSize.Location = new System.Drawing.Point(754, 12);
            this.trackBarNodeSize.Maximum = 50;
            this.trackBarNodeSize.Minimum = 10;
            this.trackBarNodeSize.Name = "trackBarNodeSize";
            this.trackBarNodeSize.Size = new System.Drawing.Size(104, 45);
            this.trackBarNodeSize.TabIndex = 1;
            this.trackBarNodeSize.Value = 30;
            this.trackBarNodeSize.ValueChanged += new System.EventHandler(this.trackBarNodeSize_ValueChanged);
            // 
            // imDrawSpace
            // 
            this.imDrawSpace.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.imDrawSpace.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.imDrawSpace.Location = new System.Drawing.Point(0, 63);
            this.imDrawSpace.Name = "imDrawSpace";
            this.imDrawSpace.Size = new System.Drawing.Size(870, 415);
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
            this.rbNodes.Location = new System.Drawing.Point(131, 13);
            this.rbNodes.Name = "rbNodes";
            this.rbNodes.Size = new System.Drawing.Size(56, 17);
            this.rbNodes.TabIndex = 3;
            this.rbNodes.TabStop = true;
            this.rbNodes.Text = "Nodes";
            this.rbNodes.UseVisualStyleBackColor = true;
            this.rbNodes.CheckedChanged += new System.EventHandler(this.radioButton1_CheckedChanged);
            // 
            // rbEdges
            // 
            this.rbEdges.AutoSize = true;
            this.rbEdges.Location = new System.Drawing.Point(131, 36);
            this.rbEdges.Name = "rbEdges";
            this.rbEdges.Size = new System.Drawing.Size(55, 17);
            this.rbEdges.TabIndex = 4;
            this.rbEdges.Text = "Edges";
            this.rbEdges.UseVisualStyleBackColor = true;
            this.rbEdges.CheckedChanged += new System.EventHandler(this.rbEdges_CheckedChanged);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(870, 478);
            this.Controls.Add(this.rbEdges);
            this.Controls.Add(this.rbNodes);
            this.Controls.Add(this.imDrawSpace);
            this.Controls.Add(this.trackBarNodeSize);
            this.Controls.Add(this.tbXY);
            this.Name = "Form1";
            this.Text = "Form1";
            ((System.ComponentModel.ISupportInitialize)(this.trackBarNodeSize)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.imDrawSpace)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.TextBox tbXY;
        private System.Windows.Forms.TrackBar trackBarNodeSize;
        private System.Windows.Forms.PictureBox imDrawSpace;
        private System.Windows.Forms.RadioButton rbNodes;
        private System.Windows.Forms.RadioButton rbEdges;
    }
}

