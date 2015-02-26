namespace _5.WarningManagement
{
    partial class SetTunnelPreWarningRules
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SetTunnelPreWarningRules));
            this.panel1 = new System.Windows.Forms.Panel();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.btnApply = new System.Windows.Forms.ToolStripButton();
            this.btnCancel = new System.Windows.Forms.ToolStripButton();
            this.btnSelAll = new System.Windows.Forms.ToolStripButton();
            this.btnCancelSelAll = new System.Windows.Forms.ToolStripButton();
            this.btnExit = new System.Windows.Forms.ToolStripButton();
            this.selectTunnelSimple1 = new LibCommonForm.SelectTunnelSimple();
            this.lblMessage = new System.Windows.Forms.Label();
            this._panelRules = new System.Windows.Forms.Panel();
            this.panel1.SuspendLayout();
            this.toolStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this._panelRules);
            this.panel1.Controls.Add(this.selectTunnelSimple1);
            this.panel1.Controls.Add(this.lblMessage);
            this.panel1.Controls.Add(this.toolStrip1);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(909, 684);
            this.panel1.TabIndex = 43;
            // 
            // toolStrip1
            // 
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.btnApply,
            this.btnCancel,
            this.btnSelAll,
            this.btnCancelSelAll,
            this.btnExit});
            this.toolStrip1.LayoutStyle = System.Windows.Forms.ToolStripLayoutStyle.Flow;
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(909, 24);
            this.toolStrip1.TabIndex = 0;
            this.toolStrip1.TabStop = true;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // btnApply
            // 
            this.btnApply.Image = ((System.Drawing.Image)(resources.GetObject("btnApply.Image")));
            this.btnApply.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnApply.Name = "btnApply";
            this.btnApply.Size = new System.Drawing.Size(52, 21);
            this.btnApply.Text = "应用";
            this.btnApply.Click += new System.EventHandler(this.btnApply_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.Image = ((System.Drawing.Image)(resources.GetObject("btnCancel.Image")));
            this.btnCancel.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(52, 21);
            this.btnCancel.Text = "取消";
            // 
            // btnSelAll
            // 
            this.btnSelAll.Image = ((System.Drawing.Image)(resources.GetObject("btnSelAll.Image")));
            this.btnSelAll.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnSelAll.Name = "btnSelAll";
            this.btnSelAll.Size = new System.Drawing.Size(100, 21);
            this.btnSelAll.Text = "全选所有规则";
            this.btnSelAll.Click += new System.EventHandler(this.btnSelAll_Click);
            // 
            // btnCancelSelAll
            // 
            this.btnCancelSelAll.Image = ((System.Drawing.Image)(resources.GetObject("btnCancelSelAll.Image")));
            this.btnCancelSelAll.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnCancelSelAll.Name = "btnCancelSelAll";
            this.btnCancelSelAll.Size = new System.Drawing.Size(124, 21);
            this.btnCancelSelAll.Text = "取消全选所有规则";
            this.btnCancelSelAll.Click += new System.EventHandler(this.btnCancelSelAll_Click);
            // 
            // btnExit
            // 
            this.btnExit.Image = ((System.Drawing.Image)(resources.GetObject("btnExit.Image")));
            this.btnExit.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnExit.Name = "btnExit";
            this.btnExit.Size = new System.Drawing.Size(52, 21);
            this.btnExit.Text = "退出";
            this.btnExit.Click += new System.EventHandler(this.btnExit_Click);
            // 
            // selectTunnelSimple1
            // 
            this.selectTunnelSimple1.ITunnelId = 0;
            this.selectTunnelSimple1.Location = new System.Drawing.Point(12, 27);
            this.selectTunnelSimple1.Name = "selectTunnelSimple1";
            this.selectTunnelSimple1.Size = new System.Drawing.Size(325, 38);
            this.selectTunnelSimple1.TabIndex = 4;
            // 
            // lblMessage
            // 
            this.lblMessage.AutoSize = true;
            this.lblMessage.Font = new System.Drawing.Font("SimSun", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblMessage.Location = new System.Drawing.Point(372, 38);
            this.lblMessage.Name = "lblMessage";
            this.lblMessage.Size = new System.Drawing.Size(28, 14);
            this.lblMessage.TabIndex = 5;
            this.lblMessage.Text = "MSG";
            // 
            // _panelRules
            // 
            this._panelRules.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this._panelRules.Location = new System.Drawing.Point(0, 71);
            this._panelRules.Name = "_panelRules";
            this._panelRules.Size = new System.Drawing.Size(906, 610);
            this._panelRules.TabIndex = 6;
            // 
            // SetTunnelPreWarningRules
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(909, 684);
            this.Controls.Add(this.panel1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.IsMdiContainer = true;
            this.Name = "SetTunnelPreWarningRules";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.SetTunnelPreWarningRules_Load);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripButton btnApply;
        private System.Windows.Forms.ToolStripButton btnSelAll;
        private System.Windows.Forms.ToolStripButton btnExit;
        private System.Windows.Forms.ToolStripButton btnCancelSelAll;
        private System.Windows.Forms.ToolStripButton btnCancel;
        private LibCommonForm.SelectTunnelSimple selectTunnelSimple1;
        private System.Windows.Forms.Label lblMessage;
        private System.Windows.Forms.Panel _panelRules;





    }
}