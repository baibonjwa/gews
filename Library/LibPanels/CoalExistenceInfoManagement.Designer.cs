using LibCommonForm;

namespace LibPanels
{
    partial class CoalExistenceInfoManagement
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(CoalExistenceInfoManagement));
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.tsBtnPrint = new System.Windows.Forms.ToolStripButton();
            this.tsBtnExport = new System.Windows.Forms.ToolStripButton();
            this.tsBtnAdd = new System.Windows.Forms.ToolStripButton();
            this.tsBtnModify = new System.Windows.Forms.ToolStripButton();
            this.tsBtnDel = new System.Windows.Forms.ToolStripButton();
            this.tsBtnRefresh = new System.Windows.Forms.ToolStripButton();
            this.tsBtnExit = new System.Windows.Forms.ToolStripButton();
            this.gcCoalExistence = new DevExpress.XtraGrid.GridControl();
            this.bandedGridView1 = new DevExpress.XtraGrid.Views.BandedGrid.BandedGridView();
            this.bandedGridColumn17 = new DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn();
            this.bandedGridColumn21 = new DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn();
            this.bandedGridColumn18 = new DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn();
            this.bandedGridColumn19 = new DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn();
            this.bandedGridColumn20 = new DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn();
            this.bandedGridColumn24 = new DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn();
            this.bandedGridColumn23 = new DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn();
            this.bandedGridColumn26 = new DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn();
            this.bandedGridColumn22 = new DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn();
            this.bandedGridColumn25 = new DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn();
            this.bandedGridColumn27 = new DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn();
            this.bandedGridColumn28 = new DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn();
            this.bandedGridColumn30 = new DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn();
            this.bandedGridColumn29 = new DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn();
            this.bandedGridColumn31 = new DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn();
            this.bandedGridColumn32 = new DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn();
            this.controlNavigator1 = new DevExpress.XtraEditors.ControlNavigator();
            this.bandedGridColumn1 = new DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn();
            this.gridBand1 = new DevExpress.XtraGrid.Views.BandedGrid.GridBand();
            this.gridBand2 = new DevExpress.XtraGrid.Views.BandedGrid.GridBand();
            this.gridBand7 = new DevExpress.XtraGrid.Views.BandedGrid.GridBand();
            this.gridBand15 = new DevExpress.XtraGrid.Views.BandedGrid.GridBand();
            this.toolStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gcCoalExistence)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.bandedGridView1)).BeginInit();
            this.SuspendLayout();
            // 
            // statusStrip1
            // 
            this.statusStrip1.Location = new System.Drawing.Point(0, 653);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Padding = new System.Windows.Forms.Padding(1, 0, 16, 0);
            this.statusStrip1.Size = new System.Drawing.Size(1111, 22);
            this.statusStrip1.TabIndex = 79;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // toolStrip1
            // 
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsBtnPrint,
            this.tsBtnExport,
            this.tsBtnAdd,
            this.tsBtnModify,
            this.tsBtnDel,
            this.tsBtnRefresh,
            this.tsBtnExit});
            this.toolStrip1.LayoutStyle = System.Windows.Forms.ToolStripLayoutStyle.Flow;
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(1111, 24);
            this.toolStrip1.TabIndex = 80;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // tsBtnPrint
            // 
            this.tsBtnPrint.Image = ((System.Drawing.Image)(resources.GetObject("tsBtnPrint.Image")));
            this.tsBtnPrint.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsBtnPrint.Name = "tsBtnPrint";
            this.tsBtnPrint.Size = new System.Drawing.Size(52, 21);
            this.tsBtnPrint.Text = "打印";
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
            // tsBtnModify
            // 
            this.tsBtnModify.Image = ((System.Drawing.Image)(resources.GetObject("tsBtnModify.Image")));
            this.tsBtnModify.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsBtnModify.Name = "tsBtnModify";
            this.tsBtnModify.Size = new System.Drawing.Size(52, 21);
            this.tsBtnModify.Text = "修改";
            this.tsBtnModify.Click += new System.EventHandler(this.tsBtnModify_Click);
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
            // 
            // tsBtnExit
            // 
            this.tsBtnExit.Image = ((System.Drawing.Image)(resources.GetObject("tsBtnExit.Image")));
            this.tsBtnExit.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsBtnExit.Name = "tsBtnExit";
            this.tsBtnExit.Size = new System.Drawing.Size(52, 21);
            this.tsBtnExit.Text = "退出";
            this.tsBtnExit.Click += new System.EventHandler(this.tsBtnExit_Click_1);
            // 
            // gcCoalExistence
            // 
            this.gcCoalExistence.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.gcCoalExistence.Cursor = System.Windows.Forms.Cursors.Default;
            this.gcCoalExistence.Location = new System.Drawing.Point(12, 27);
            this.gcCoalExistence.MainView = this.bandedGridView1;
            this.gcCoalExistence.Name = "gcCoalExistence";
            this.gcCoalExistence.Size = new System.Drawing.Size(1087, 593);
            this.gcCoalExistence.TabIndex = 81;
            this.gcCoalExistence.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.bandedGridView1});
            // 
            // bandedGridView1
            // 
            this.bandedGridView1.Bands.AddRange(new DevExpress.XtraGrid.Views.BandedGrid.GridBand[] {
            this.gridBand1,
            this.gridBand2,
            this.gridBand7,
            this.gridBand15});
            this.bandedGridView1.Columns.AddRange(new DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn[] {
            this.bandedGridColumn17,
            this.bandedGridColumn18,
            this.bandedGridColumn19,
            this.bandedGridColumn20,
            this.bandedGridColumn21,
            this.bandedGridColumn22,
            this.bandedGridColumn23,
            this.bandedGridColumn24,
            this.bandedGridColumn25,
            this.bandedGridColumn26,
            this.bandedGridColumn27,
            this.bandedGridColumn28,
            this.bandedGridColumn29,
            this.bandedGridColumn30,
            this.bandedGridColumn31,
            this.bandedGridColumn32,
            this.bandedGridColumn1});
            this.bandedGridView1.GridControl = this.gcCoalExistence;
            this.bandedGridView1.Name = "bandedGridView1";
            // 
            // bandedGridColumn17
            // 
            this.bandedGridColumn17.Caption = "巷道名称";
            this.bandedGridColumn17.FieldName = "Tunnel.TunnelName";
            this.bandedGridColumn17.Name = "bandedGridColumn17";
            this.bandedGridColumn17.Visible = true;
            this.bandedGridColumn17.Width = 85;
            // 
            // bandedGridColumn21
            // 
            this.bandedGridColumn21.Caption = "提交日期";
            this.bandedGridColumn21.FieldName = "Datetime";
            this.bandedGridColumn21.Name = "bandedGridColumn21";
            this.bandedGridColumn21.RowCount = 2;
            this.bandedGridColumn21.Visible = true;
            this.bandedGridColumn21.Width = 74;
            // 
            // bandedGridColumn18
            // 
            this.bandedGridColumn18.Caption = "X";
            this.bandedGridColumn18.FieldName = "CoordinateX";
            this.bandedGridColumn18.Name = "bandedGridColumn18";
            this.bandedGridColumn18.Visible = true;
            this.bandedGridColumn18.Width = 32;
            // 
            // bandedGridColumn19
            // 
            this.bandedGridColumn19.Caption = "Y";
            this.bandedGridColumn19.FieldName = "CoordinateY";
            this.bandedGridColumn19.Name = "bandedGridColumn19";
            this.bandedGridColumn19.Visible = true;
            this.bandedGridColumn19.Width = 32;
            // 
            // bandedGridColumn20
            // 
            this.bandedGridColumn20.Caption = "Z";
            this.bandedGridColumn20.FieldName = "CoordinateZ";
            this.bandedGridColumn20.Name = "bandedGridColumn20";
            this.bandedGridColumn20.Visible = true;
            this.bandedGridColumn20.Width = 53;
            // 
            // bandedGridColumn24
            // 
            this.bandedGridColumn24.Caption = "煤层走向倾角突然急剧变化";
            this.bandedGridColumn24.FieldName = "IsTowardsChange";
            this.bandedGridColumn24.Name = "bandedGridColumn24";
            this.bandedGridColumn24.Visible = true;
            this.bandedGridColumn24.Width = 164;
            // 
            // bandedGridColumn23
            // 
            this.bandedGridColumn23.Caption = "软分层（构造煤）层位是否发生变化";
            this.bandedGridColumn23.FieldName = "IsLevelChange";
            this.bandedGridColumn23.Name = "bandedGridColumn23";
            this.bandedGridColumn23.Visible = true;
            this.bandedGridColumn23.Width = 217;
            // 
            // bandedGridColumn26
            // 
            this.bandedGridColumn26.Caption = "软分层（构造煤）厚度";
            this.bandedGridColumn26.FieldName = "TectonicCoalThick";
            this.bandedGridColumn26.Name = "bandedGridColumn26";
            this.bandedGridColumn26.Visible = true;
            this.bandedGridColumn26.Width = 134;
            // 
            // bandedGridColumn22
            // 
            this.bandedGridColumn22.Caption = "是否层理紊乱";
            this.bandedGridColumn22.FieldName = "IsLevelDisorder";
            this.bandedGridColumn22.Name = "bandedGridColumn22";
            this.bandedGridColumn22.Visible = true;
            this.bandedGridColumn22.Width = 57;
            // 
            // bandedGridColumn25
            // 
            this.bandedGridColumn25.Caption = "煤厚变化";
            this.bandedGridColumn25.FieldName = "CoalThickChange";
            this.bandedGridColumn25.Name = "bandedGridColumn25";
            this.bandedGridColumn25.Visible = true;
            this.bandedGridColumn25.Width = 78;
            // 
            // bandedGridColumn27
            // 
            this.bandedGridColumn27.Caption = "煤体破坏类型";
            this.bandedGridColumn27.FieldName = "CoalDistoryLevel";
            this.bandedGridColumn27.Name = "bandedGridColumn27";
            this.bandedGridColumn27.Visible = true;
            this.bandedGridColumn27.Width = 83;
            // 
            // bandedGridColumn28
            // 
            this.bandedGridColumn28.Caption = "工作制式";
            this.bandedGridColumn28.FieldName = "WorkStyle";
            this.bandedGridColumn28.Name = "bandedGridColumn28";
            this.bandedGridColumn28.Visible = true;
            this.bandedGridColumn28.Width = 20;
            // 
            // bandedGridColumn30
            // 
            this.bandedGridColumn30.Caption = "队别";
            this.bandedGridColumn30.FieldName = "TeamName";
            this.bandedGridColumn30.Name = "bandedGridColumn30";
            this.bandedGridColumn30.Visible = true;
            this.bandedGridColumn30.Width = 20;
            // 
            // bandedGridColumn29
            // 
            this.bandedGridColumn29.Caption = "班次";
            this.bandedGridColumn29.FieldName = "WorkTime";
            this.bandedGridColumn29.Name = "bandedGridColumn29";
            this.bandedGridColumn29.Visible = true;
            this.bandedGridColumn29.Width = 20;
            // 
            // bandedGridColumn31
            // 
            this.bandedGridColumn31.Caption = "填报人";
            this.bandedGridColumn31.FieldName = "Submitter";
            this.bandedGridColumn31.Name = "bandedGridColumn31";
            this.bandedGridColumn31.Visible = true;
            this.bandedGridColumn31.Width = 20;
            // 
            // bandedGridColumn32
            // 
            this.bandedGridColumn32.Caption = "备注";
            this.bandedGridColumn32.Name = "bandedGridColumn32";
            this.bandedGridColumn32.Visible = true;
            this.bandedGridColumn32.Width = 25;
            // 
            // controlNavigator1
            // 
            this.controlNavigator1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.controlNavigator1.Location = new System.Drawing.Point(12, 626);
            this.controlNavigator1.Name = "controlNavigator1";
            this.controlNavigator1.NavigatableControl = this.gcCoalExistence;
            this.controlNavigator1.Size = new System.Drawing.Size(311, 24);
            this.controlNavigator1.TabIndex = 82;
            this.controlNavigator1.Text = "controlNavigator1";
            this.controlNavigator1.TextLocation = DevExpress.XtraEditors.NavigatorButtonsTextLocation.Center;
            this.controlNavigator1.TextStringFormat = "记录 {0} / {1}";
            // 
            // bandedGridColumn1
            // 
            this.bandedGridColumn1.Caption = "工作面名称";
            this.bandedGridColumn1.FieldName = "Tunnel.WorkingFace.WorkingFaceName";
            this.bandedGridColumn1.Name = "bandedGridColumn1";
            this.bandedGridColumn1.Visible = true;
            // 
            // gridBand1
            // 
            this.gridBand1.AppearanceHeader.Options.UseTextOptions = true;
            this.gridBand1.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.gridBand1.Caption = "巷道信息";
            this.gridBand1.Columns.Add(this.bandedGridColumn1);
            this.gridBand1.Columns.Add(this.bandedGridColumn17);
            this.gridBand1.Name = "gridBand1";
            this.gridBand1.VisibleIndex = 0;
            this.gridBand1.Width = 160;
            // 
            // gridBand2
            // 
            this.gridBand2.AppearanceHeader.Options.UseTextOptions = true;
            this.gridBand2.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.gridBand2.Caption = "坐标";
            this.gridBand2.Columns.Add(this.bandedGridColumn18);
            this.gridBand2.Columns.Add(this.bandedGridColumn19);
            this.gridBand2.Columns.Add(this.bandedGridColumn20);
            this.gridBand2.Name = "gridBand2";
            this.gridBand2.VisibleIndex = 1;
            this.gridBand2.Width = 117;
            // 
            // gridBand7
            // 
            this.gridBand7.AppearanceHeader.Options.UseTextOptions = true;
            this.gridBand7.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.gridBand7.Caption = "煤层赋存预警指标";
            this.gridBand7.Columns.Add(this.bandedGridColumn24);
            this.gridBand7.Columns.Add(this.bandedGridColumn23);
            this.gridBand7.Columns.Add(this.bandedGridColumn26);
            this.gridBand7.Columns.Add(this.bandedGridColumn25);
            this.gridBand7.Columns.Add(this.bandedGridColumn27);
            this.gridBand7.Columns.Add(this.bandedGridColumn22);
            this.gridBand7.Name = "gridBand7";
            this.gridBand7.VisibleIndex = 2;
            this.gridBand7.Width = 733;
            // 
            // gridBand15
            // 
            this.gridBand15.AppearanceHeader.Options.UseTextOptions = true;
            this.gridBand15.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.gridBand15.Caption = "提交信息";
            this.gridBand15.Columns.Add(this.bandedGridColumn21);
            this.gridBand15.Columns.Add(this.bandedGridColumn30);
            this.gridBand15.Columns.Add(this.bandedGridColumn31);
            this.gridBand15.Columns.Add(this.bandedGridColumn29);
            this.gridBand15.Columns.Add(this.bandedGridColumn28);
            this.gridBand15.Name = "gridBand15";
            this.gridBand15.VisibleIndex = 3;
            this.gridBand15.Width = 154;
            // 
            // CoalExistenceInfoManagement
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1111, 675);
            this.Controls.Add(this.controlNavigator1);
            this.Controls.Add(this.gcCoalExistence);
            this.Controls.Add(this.toolStrip1);
            this.Controls.Add(this.statusStrip1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "CoalExistenceInfoManagement";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "井下数据煤层赋存管理";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.MineDataManagement_Load);
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gcCoalExistence)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.bandedGridView1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripButton tsBtnPrint;
        private System.Windows.Forms.ToolStripButton tsBtnAdd;
        private System.Windows.Forms.ToolStripButton tsBtnModify;
        private System.Windows.Forms.ToolStripButton tsBtnDel;
        private System.Windows.Forms.ToolStripButton tsBtnRefresh;
        private System.Windows.Forms.ToolStripButton tsBtnExit;
        private System.Windows.Forms.ToolStripButton tsBtnExport;
        private DevExpress.XtraGrid.GridControl gcCoalExistence;
        private DevExpress.XtraEditors.ControlNavigator controlNavigator1;
        private DevExpress.XtraGrid.Views.BandedGrid.BandedGridView bandedGridView1;
        private DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn bandedGridColumn17;
        private DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn bandedGridColumn21;
        private DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn bandedGridColumn18;
        private DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn bandedGridColumn19;
        private DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn bandedGridColumn20;
        private DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn bandedGridColumn24;
        private DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn bandedGridColumn23;
        private DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn bandedGridColumn26;
        private DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn bandedGridColumn22;
        private DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn bandedGridColumn25;
        private DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn bandedGridColumn27;
        private DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn bandedGridColumn28;
        private DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn bandedGridColumn30;
        private DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn bandedGridColumn29;
        private DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn bandedGridColumn31;
        private DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn bandedGridColumn32;
        private DevExpress.XtraGrid.Views.BandedGrid.GridBand gridBand1;
        private DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn bandedGridColumn1;
        private DevExpress.XtraGrid.Views.BandedGrid.GridBand gridBand2;
        private DevExpress.XtraGrid.Views.BandedGrid.GridBand gridBand7;
        private DevExpress.XtraGrid.Views.BandedGrid.GridBand gridBand15;
    }
}