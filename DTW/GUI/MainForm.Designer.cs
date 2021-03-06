﻿namespace GUI
{
    partial class MainForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.exitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.dataToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.openDataToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.saveDataToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.clearDataToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.editToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.changePeriodToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.matchingToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.openPatternWindowToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.toolStripStatusLabel1 = new System.Windows.Forms.ToolStripStatusLabel();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.saveFileDialog1 = new System.Windows.Forms.SaveFileDialog();
            this.candleStickChart1 = new GUIComponents.CandleStickChart();
            this.listBox1 = new System.Windows.Forms.ListBox();
            this.menuStrip1.SuspendLayout();
            this.statusStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem,
            this.dataToolStripMenuItem,
            this.editToolStripMenuItem,
            this.matchingToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(994, 28);
            this.menuStrip1.TabIndex = 0;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // fileToolStripMenuItem
            // 
            this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.exitToolStripMenuItem});
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            this.fileToolStripMenuItem.Size = new System.Drawing.Size(44, 24);
            this.fileToolStripMenuItem.Text = "File";
            // 
            // exitToolStripMenuItem
            // 
            this.exitToolStripMenuItem.Name = "exitToolStripMenuItem";
            this.exitToolStripMenuItem.Size = new System.Drawing.Size(108, 26);
            this.exitToolStripMenuItem.Text = "Exit";
            this.exitToolStripMenuItem.Click += new System.EventHandler(this.exitToolStripMenuItem_Click);
            // 
            // dataToolStripMenuItem
            // 
            this.dataToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.openDataToolStripMenuItem,
            this.saveDataToolStripMenuItem,
            this.clearDataToolStripMenuItem});
            this.dataToolStripMenuItem.Name = "dataToolStripMenuItem";
            this.dataToolStripMenuItem.Size = new System.Drawing.Size(53, 24);
            this.dataToolStripMenuItem.Text = "Data";
            // 
            // openDataToolStripMenuItem
            // 
            this.openDataToolStripMenuItem.Name = "openDataToolStripMenuItem";
            this.openDataToolStripMenuItem.Size = new System.Drawing.Size(154, 26);
            this.openDataToolStripMenuItem.Text = "&Open data";
            this.openDataToolStripMenuItem.Click += new System.EventHandler(this.openDataToolStripMenuItem_Click);
            // 
            // saveDataToolStripMenuItem
            // 
            this.saveDataToolStripMenuItem.Name = "saveDataToolStripMenuItem";
            this.saveDataToolStripMenuItem.Size = new System.Drawing.Size(154, 26);
            this.saveDataToolStripMenuItem.Text = "&Save data";
            this.saveDataToolStripMenuItem.Click += new System.EventHandler(this.saveDataToolStripMenuItem_Click);
            // 
            // clearDataToolStripMenuItem
            // 
            this.clearDataToolStripMenuItem.Name = "clearDataToolStripMenuItem";
            this.clearDataToolStripMenuItem.Size = new System.Drawing.Size(154, 26);
            this.clearDataToolStripMenuItem.Text = "Clear data";
            this.clearDataToolStripMenuItem.Click += new System.EventHandler(this.clearDataToolStripMenuItem_Click);
            // 
            // editToolStripMenuItem
            // 
            this.editToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.changePeriodToolStripMenuItem});
            this.editToolStripMenuItem.Name = "editToolStripMenuItem";
            this.editToolStripMenuItem.Size = new System.Drawing.Size(47, 24);
            this.editToolStripMenuItem.Text = "Edit";
            // 
            // changePeriodToolStripMenuItem
            // 
            this.changePeriodToolStripMenuItem.Name = "changePeriodToolStripMenuItem";
            this.changePeriodToolStripMenuItem.Size = new System.Drawing.Size(182, 26);
            this.changePeriodToolStripMenuItem.Text = "Change period";
            this.changePeriodToolStripMenuItem.Click += new System.EventHandler(this.changePeriodToolStripMenuItem_Click);
            // 
            // matchingToolStripMenuItem
            // 
            this.matchingToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.openPatternWindowToolStripMenuItem});
            this.matchingToolStripMenuItem.Name = "matchingToolStripMenuItem";
            this.matchingToolStripMenuItem.Size = new System.Drawing.Size(83, 24);
            this.matchingToolStripMenuItem.Text = "Matching";
            // 
            // openPatternWindowToolStripMenuItem
            // 
            this.openPatternWindowToolStripMenuItem.Name = "openPatternWindowToolStripMenuItem";
            this.openPatternWindowToolStripMenuItem.Size = new System.Drawing.Size(228, 26);
            this.openPatternWindowToolStripMenuItem.Text = "Open pattern window";
            this.openPatternWindowToolStripMenuItem.Click += new System.EventHandler(this.openPatternWindowToolStripMenuItem_Click);
            // 
            // statusStrip1
            // 
            this.statusStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripStatusLabel1});
            this.statusStrip1.Location = new System.Drawing.Point(0, 345);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(994, 22);
            this.statusStrip1.TabIndex = 1;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // toolStripStatusLabel1
            // 
            this.toolStripStatusLabel1.Name = "toolStripStatusLabel1";
            this.toolStripStatusLabel1.Size = new System.Drawing.Size(0, 17);
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.FileName = "openFileDialog1";
            this.openFileDialog1.Filter = "Finam csv|*.csv";
            this.openFileDialog1.Title = "Open data file";
            // 
            // saveFileDialog1
            // 
            this.saveFileDialog1.Filter = "Finam csv|*.csv";
            this.saveFileDialog1.Title = "Save data file";
            // 
            // candleStickChart1
            // 
            this.candleStickChart1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.candleStickChart1.IsAutoScrollRange = true;
            this.candleStickChart1.IsEnableWheelZoom = false;
            this.candleStickChart1.IsShowHScrollBar = true;
            this.candleStickChart1.IsShowPointValues = true;
            this.candleStickChart1.Location = new System.Drawing.Point(0, 32);
            this.candleStickChart1.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.candleStickChart1.Name = "candleStickChart1";
            this.candleStickChart1.ScrollGrace = 0D;
            this.candleStickChart1.ScrollMaxX = 0D;
            this.candleStickChart1.ScrollMaxY = 0D;
            this.candleStickChart1.ScrollMaxY2 = 0D;
            this.candleStickChart1.ScrollMinX = 0D;
            this.candleStickChart1.ScrollMinY = 0D;
            this.candleStickChart1.ScrollMinY2 = 0D;
            this.candleStickChart1.Size = new System.Drawing.Size(484, 309);
            this.candleStickChart1.TabIndex = 2;
            this.candleStickChart1.UseExtendedPrintDialog = true;
            this.candleStickChart1.CandlesSelected += new GUIComponents.CandleStickChart.CandlesSelectedHandler(this.candleStickChart1_CandlesSelected);
            // 
            // listBox1
            // 
            this.listBox1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.listBox1.FormattingEnabled = true;
            this.listBox1.ItemHeight = 16;
            this.listBox1.Location = new System.Drawing.Point(491, 32);
            this.listBox1.Name = "listBox1";
            this.listBox1.Size = new System.Drawing.Size(503, 292);
            this.listBox1.TabIndex = 3;
            this.listBox1.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.listBox1_MouseDoubleClick);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(994, 367);
            this.Controls.Add(this.listBox1);
            this.Controls.Add(this.candleStickChart1);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.menuStrip1);
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "MainForm";
            this.Text = "Patterns matcher";
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem exitToolStripMenuItem;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel1;
        private System.Windows.Forms.OpenFileDialog openFileDialog1;
        private System.Windows.Forms.SaveFileDialog saveFileDialog1;
        private System.Windows.Forms.ToolStripMenuItem dataToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem openDataToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem saveDataToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem clearDataToolStripMenuItem;
        private GUIComponents.CandleStickChart candleStickChart1;
        private System.Windows.Forms.ToolStripMenuItem editToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem changePeriodToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem matchingToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem openPatternWindowToolStripMenuItem;
        private System.Windows.Forms.ListBox listBox1;
    }
}

