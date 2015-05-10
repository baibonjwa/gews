namespace LibCommonForm
{
    partial class DepartmentInformation
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(DepartmentInformation));
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.tsBtnPrint = new System.Windows.Forms.ToolStripButton();
            this.tsBtnExport = new System.Windows.Forms.ToolStripButton();
            this.tsBtnAdd = new System.Windows.Forms.ToolStripButton();
            this.btnUpdate = new System.Windows.Forms.ToolStripButton();
            this.tsBtnDel = new System.Windows.Forms.ToolStripButton();
            this.tsBtnRefresh = new System.Windows.Forms.ToolStripButton();
            this.tsBtnExit = new System.Windows.Forms.ToolStripButton();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.printPreviewDialog1 = new System.Windows.Forms.PrintPreviewDialog();
            this.gcDepartment = new DevExpress.XtraGrid.GridControl();
            this.gridView1 = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.gridColumn1 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn2 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn3 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn4 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn5 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.controlNavigator1 = new DevExpress.XtraEditors.ControlNavigator();
            this.saveFileDialog1 = new System.Windows.Forms.SaveFileDialog();
            this.toolStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gcDepartment)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).BeginInit();
            this.SuspendLayout();
            // 
            // toolStrip1
            // 
            this.toolStrip1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.toolStrip1.Dock = System.Windows.Forms.DockStyle.None;
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsBtnPrint,
            this.tsBtnExport,
            this.tsBtnAdd,
            this.btnUpdate,
            this.tsBtnDel,
            this.tsBtnRefresh,
            this.tsBtnExit});
            this.toolStrip1.LayoutStyle = System.Windows.Forms.ToolStripLayoutStyle.Flow;
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(365, 24);
            this.toolStrip1.TabIndex = 0;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // tsBtnPrint
            // 
            this.tsBtnPrint.Image = ((System.Drawing.Image)(resources.GetObject("tsBtnPrint.Image")));
            this.tsBtnPrint.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsBtnPrint.Name = "tsBtnPrint";
            this.tsBtnPrint.Size = new System.Drawing.Size(52, 21);
            this.tsBtnPrint.Text = "打印";
            this.tsBtnPrint.Click += new System.EventHandler(this.tsBtnPrint_Click);
            // 
            // tsBtnExport
            // 
            this.tsBtnExport.Image = ((System.Drawing.Image)(resources.GetObject("tsBtnExport.Image")));
            this.tsBtnExport.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsBtnExport.Name = "tsBtnExport";
            this.tsBtnExport.Size = new System.Drawing.Size(52, 21);
            this.tsBtnExport.Text = "导出";
            this.tsBtnExport.Click += new System.EventHandler(this.tsBtnExport_Click);
            // 
            // tsBtnAdd
            // 
            this.tsBtnAdd.Image = ((System.Drawing.Image)(resources.GetObject("tsBtnAdd.Image")));
            this.tsBtnAdd.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsBtnAdd.Name = "tsBtnAdd";
            this.tsBtnAdd.Size = new System.Drawing.Size(52, 21);
            this.tsBtnAdd.Text = "添加";
            this.tsBtnAdd.Click += new System.EventHandler(this.tsBtnAdd_Click);
            // 
            // btnUpdate
            // 
            this.btnUpdate.Image = ((System.Drawing.Image)(resources.GetObject("btnUpdate.Image")));
            this.btnUpdate.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnUpdate.Name = "btnUpdate";
            this.btnUpdate.Size = new System.Drawing.Size(52, 21);
            this.btnUpdate.Text = "修改";
            this.btnUpdate.Click += new System.EventHandler(this.btnUpdate_Click);
            // 
            // tsBtnDel
            // 
            this.tsBtnDel.Image = ((System.Drawing.Image)(resources.GetObject("tsBtnDel.Image")));
            this.tsBtnDel.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsBtnDel.Name = "tsBtnDel";
            this.tsBtnDel.Size = new System.Drawing.Size(52, 21);
            this.tsBtnDel.Text = "删除";
            this.tsBtnDel.Click += new System.EventHandler(this.tsBtnDel_Click);
            // 
            // tsBtnRefresh
            // 
            this.tsBtnRefresh.Image = ((System.Drawing.Image)(resources.GetObject("tsBtnRefresh.Image")));
            this.tsBtnRefresh.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsBtnRefresh.Name = "tsBtnRefresh";
            this.tsBtnRefresh.Size = new System.Drawing.Size(52, 21);
            this.tsBtnRefresh.Text = "刷新";
            this.tsBtnRefresh.Click += new System.EventHandler(this.tsBtnRefresh_Click);
            // 
            // tsBtnExit
            // 
            this.tsBtnExit.Image = ((System.Drawing.Image)(resources.GetObject("tsBtnExit.Image")));
            this.tsBtnExit.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsBtnExit.Name = "tsBtnExit";
            this.tsBtnExit.Size = new System.Drawing.Size(52, 21);
            this.tsBtnExit.Text = "退出";
            this.tsBtnExit.Click += new System.EventHandler(this.tsBtnExit_Click);
            // 
            // statusStrip1
            // 
            this.statusStrip1.Location = new System.Drawing.Point(0, 507);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(970, 22);
            this.statusStrip1.TabIndex = 50;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // printPreviewDialog1
            // 
            this.printPreviewDialog1.AutoScrollMargin = new System.Drawing.Size(0, 0);
            this.printPreviewDialog1.AutoScrollMinSize = new System.Drawing.Size(0, 0);
            this.printPreviewDialog1.ClientSize = new System.Drawing.Size(400, 300);
            this.printPreviewDialog1.Enabled = true;
            this.printPreviewDialog1.Icon = ((System.Drawing.Icon)(resources.GetObject("printPreviewDialog1.Icon")));
            this.printPreviewDialog1.Name = "printPreviewDialog1";
            this.printPreviewDialog1.Visible = false;
            // 
            // gcDepartment
            // 
            this.gcDepartment.Cursor = System.Windows.Forms.Cursors.Default;
            this.gcDepartment.Location = new System.Drawing.Point(12, 27);
            this.gcDepartment.MainView = this.gridView1;
            this.gcDepartment.Name = "gcDepartment";
            this.gcDepartment.Size = new System.Drawing.Size(946, 447);
            this.gcDepartment.TabIndex = 51;
            this.gcDepartment.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gridView1});
            // 
            // gridView1
            // 
            this.gridView1.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.gridColumn1,
            this.gridColumn2,
            this.gridColumn3,
            this.gridColumn4,
            this.gridColumn5});
            this.gridView1.GridControl = this.gcDepartment;
            this.gridView1.Name = "gridView1";
            // 
            // gridColumn1
            // 
            this.gridColumn1.Caption = "部门名称";
            this.gridColumn1.FieldName = "Name";
            this.gridColumn1.Name = "gridColumn1";
            this.gridColumn1.Visible = true;
            this.gridColumn1.VisibleIndex = 0;
            // 
            // gridColumn2
            // 
            this.gridColumn2.Caption = "部门电话";
            this.gridColumn2.FieldName = "Tel";
            this.gridColumn2.Name = "gridColumn2";
            this.gridColumn2.Visible = true;
            this.gridColumn2.VisibleIndex = 1;
            // 
            // gridColumn3
            // 
            this.gridColumn3.Caption = "部门Email";
            this.gridColumn3.FieldName = "Email";
            this.gridColumn3.Name = "gridColumn3";
            this.gridColumn3.Visible = true;
            this.gridColumn3.VisibleIndex = 2;
            // 
            // gridColumn4
            // 
            this.gridColumn4.Caption = "员工人数";
            this.gridColumn4.FieldName = "Staff";
            this.gridColumn4.Name = "gridColumn4";
            this.gridColumn4.Visible = true;
            this.gridColumn4.VisibleIndex = 3;
            // 
            // gridColumn5
            // 
            this.gridColumn5.Caption = "备注";
            this.gridColumn5.FieldName = "Remark";
            this.gridColumn5.Name = "gridColumn5";
            this.gridColumn5.Visible = true;
            this.gridColumn5.VisibleIndex = 4;
            // 
            // controlNavigator1
            // 
            this.controlNavigator1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.controlNavigator1.Location = new System.Drawing.Point(12, 480);
            this.controlNavigator1.Name = "controlNavigator1";
            this.controlNavigator1.NavigatableControl = this.gcDepartment;
            this.controlNavigator1.Size = new System.Drawing.Size(311, 24);
            this.controlNavigator1.TabIndex = 85;
            this.controlNavigator1.Text = "controlNavigator1";
            this.controlNavigator1.TextLocation = DevExpress.XtraEditors.NavigatorButtonsTextLocation.Center;
            this.controlNavigator1.TextStringFormat = "记录 {0} / {1}";
            // 
            // DepartmentInformation
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(970, 529);
            this.Controls.Add(this.controlNavigator1);
            this.Controls.Add(this.gcDepartment);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.toolStrip1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "DepartmentInformation";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "部门信息管理";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.DepartmentInformation_Load);
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gcDepartment)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripButton tsBtnPrint;
        private System.Windows.Forms.ToolStripButton tsBtnExport;
        private System.Windows.Forms.ToolStripButton tsBtnAdd;
        private System.Windows.Forms.ToolStripButton tsBtnDel;
        private System.Windows.Forms.ToolStripButton tsBtnRefresh;
        private System.Windows.Forms.ToolStripButton tsBtnExit;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.PrintPreviewDialog printPreviewDialog1;
        private DevExpress.XtraGrid.GridControl gcDepartment;
        private DevExpress.XtraGrid.Views.Grid.GridView gridView1;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn1;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn2;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn3;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn4;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn5;
        private DevExpress.XtraEditors.ControlNavigator controlNavigator1;
        private System.Windows.Forms.SaveFileDialog saveFileDialog1;
        private System.Windows.Forms.ToolStripButton btnUpdate;
    }
}