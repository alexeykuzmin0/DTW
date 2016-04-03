namespace GUI
{
    partial class PatternForm
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
            this.candleStickChart1 = new GUIComponents.CandleStickChart();
            this.SuspendLayout();
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
            this.candleStickChart1.Location = new System.Drawing.Point(1, 30);
            this.candleStickChart1.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.candleStickChart1.Name = "candleStickChart1";
            this.candleStickChart1.ScrollGrace = 0D;
            this.candleStickChart1.ScrollMaxX = 0D;
            this.candleStickChart1.ScrollMaxY = 0D;
            this.candleStickChart1.ScrollMaxY2 = 0D;
            this.candleStickChart1.ScrollMinX = 0D;
            this.candleStickChart1.ScrollMinY = 0D;
            this.candleStickChart1.ScrollMinY2 = 0D;
            this.candleStickChart1.Size = new System.Drawing.Size(279, 220);
            this.candleStickChart1.TabIndex = 0;
            this.candleStickChart1.UseExtendedPrintDialog = true;
            // 
            // PatternForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(282, 253);
            this.Controls.Add(this.candleStickChart1);
            this.Name = "PatternForm";
            this.Text = "Pattern to search";
            this.ResumeLayout(false);

        }

        #endregion

        public GUIComponents.CandleStickChart candleStickChart1;
    }
}