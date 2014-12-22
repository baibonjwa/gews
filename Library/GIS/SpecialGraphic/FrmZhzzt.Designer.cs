namespace GIS.SpecialGraphic
{
    partial class FrmZhzzt
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmZhzzt));
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.tStripBtnNew = new System.Windows.Forms.ToolStripButton();
            this.tStripBtnOpen = new System.Windows.Forms.ToolStripButton();
            this.tStripBtnSave = new System.Windows.Forms.ToolStripButton();
            this.tStripBtnClose = new System.Windows.Forms.ToolStripButton();
            this.axMapControlZZT = new ESRI.ArcGIS.Controls.AxMapControl();
            this.toolStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.axMapControlZZT)).BeginInit();
            this.SuspendLayout();
            // 
            // toolStrip1
            // 
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tStripBtnNew,
            this.tStripBtnOpen,
            this.tStripBtnSave,
            this.tStripBtnClose});
            this.toolStrip1.LayoutStyle = System.Windows.Forms.ToolStripLayoutStyle.HorizontalStackWithOverflow;
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(756, 25);
            this.toolStrip1.TabIndex = 0;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // tStripBtnNew
            // 
            this.tStripBtnNew.Image = global::GIS.Properties.Resources.xinjian;
            this.tStripBtnNew.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tStripBtnNew.Name = "tStripBtnNew";
            this.tStripBtnNew.Size = new System.Drawing.Size(52, 22);
            this.tStripBtnNew.Text = "新建";
            this.tStripBtnNew.Click += new System.EventHandler(this.tStripBtnNew_Click);
            // 
            // tStripBtnOpen
            // 
            this.tStripBtnOpen.Image = global::GIS.Properties.Resources.TableExcel16;
            this.tStripBtnOpen.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tStripBtnOpen.Name = "tStripBtnOpen";
            this.tStripBtnOpen.Size = new System.Drawing.Size(52, 22);
            this.tStripBtnOpen.Text = "打开";
            this.tStripBtnOpen.Click += new System.EventHandler(this.tStripBtnOpen_Click);
            // 
            // tStripBtnSave
            // 
            this.tStripBtnSave.Image = global::GIS.Properties.Resources.GenericSave_B_16;
            this.tStripBtnSave.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tStripBtnSave.Name = "tStripBtnSave";
            this.tStripBtnSave.Size = new System.Drawing.Size(73, 22);
            this.tStripBtnSave.Text = "保存为...";
            this.tStripBtnSave.Click += new System.EventHandler(this.tStripBtnSave_Click);
            // 
            // tStripBtnClose
            // 
            this.tStripBtnClose.Image = global::GIS.Properties.Resources.tuichu;
            this.tStripBtnClose.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tStripBtnClose.Name = "tStripBtnClose";
            this.tStripBtnClose.Size = new System.Drawing.Size(52, 22);
            this.tStripBtnClose.Text = "退出";
            this.tStripBtnClose.Click += new System.EventHandler(this.tStripBtnClose_Click);
            // 
            // axMapControlZZT
            // 
            this.axMapControlZZT.Dock = System.Windows.Forms.DockStyle.Fill;
            this.axMapControlZZT.Location = new System.Drawing.Point(0, 25);
            this.axMapControlZZT.Name = "axMapControlZZT";
            this.axMapControlZZT.OcxState = ((System.Windows.Forms.AxHost.State)(resources.GetObject("axMapControlZZT.OcxState")));
            this.axMapControlZZT.Size = new System.Drawing.Size(756, 539);
            this.axMapControlZZT.TabIndex = 1;
            this.axMapControlZZT.SizeChanged += new System.EventHandler(this.axMapControlZZT_SizeChanged);
            // 
            // FrmZhzzt
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(756, 564);
            this.Controls.Add(this.axMapControlZZT);
            this.Controls.Add(this.toolStrip1);
            this.Name = "FrmZhzzt";
            this.ShowIcon = false;
            this.Text = "柱状图";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.FrmZhzzt_Load);
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.axMapControlZZT)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripButton tStripBtnNew;
        private System.Windows.Forms.ToolStripButton tStripBtnOpen;
        private System.Windows.Forms.ToolStripButton tStripBtnSave;
        private System.Windows.Forms.ToolStripButton tStripBtnClose;
        private ESRI.ArcGIS.Controls.AxMapControl axMapControlZZT;
    }
}