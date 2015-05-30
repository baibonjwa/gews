using LibCommonForm;

namespace LibPanels
{
    partial class GeologicStructureInfoManagement
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(GeologicStructureInfoManagement));
            DevExpress.XtraGrid.GridLevelNode gridLevelNode1 = new DevExpress.XtraGrid.GridLevelNode();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.tsBtnPrint = new System.Windows.Forms.ToolStripButton();
            this.tsBtnExport = new System.Windows.Forms.ToolStripButton();
            this.tsBtnAdd = new System.Windows.Forms.ToolStripButton();
            this.tsBtnModify = new System.Windows.Forms.ToolStripButton();
            this.tsBtnDel = new System.Windows.Forms.ToolStripButton();
            this.tsBtnRefresh = new System.Windows.Forms.ToolStripButton();
            this.tsBtnExit = new System.Windows.Forms.ToolStripButton();
            this.gcGeologicStructure = new DevExpress.XtraGrid.GridControl();
            this.bandedGridView1 = new DevExpress.XtraGrid.Views.BandedGrid.BandedGridView();
            this.gridBand1 = new DevExpress.XtraGrid.Views.BandedGrid.GridBand();
            this.bandedGridColumn2 = new DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn();
            this.bandedGridColumn1 = new DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn();
            this.gridBand3 = new DevExpress.XtraGrid.Views.BandedGrid.GridBand();
            this.bandedGridColumn10 = new DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn();
            this.bandedGridColumn13 = new DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn();
            this.bandedGridColumn7 = new DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn();
            this.bandedGridColumn11 = new DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn();
            this.bandedGridColumn15 = new DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn();
            this.bandedGridColumn9 = new DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn();
            this.bandedGridColumn8 = new DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn();
            this.bandedGridColumn12 = new DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn();
            this.bandedGridColumn14 = new DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn();
            this.bandedGridColumn16 = new DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn();
            this.gridBand4 = new DevExpress.XtraGrid.Views.BandedGrid.GridBand();
            this.bandedGridColumn6 = new DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn();
            this.bandedGridColumn19 = new DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn();
            this.bandedGridColumn17 = new DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn();
            this.bandedGridColumn20 = new DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn();
            this.bandedGridColumn18 = new DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn();
            this.controlNavigator1 = new DevExpress.XtraEditors.ControlNavigator();
            this.saveFileDialog1 = new System.Windows.Forms.SaveFileDialog();
            this.toolStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gcGeologicStructure)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.bandedGridView1)).BeginInit();
            this.SuspendLayout();
            // 
            // statusStrip1
            // 
            this.statusStrip1.Location = new System.Drawing.Point(0, 544);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(944, 22);
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
            this.toolStrip1.Size = new System.Drawing.Size(944, 24);
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
            // gcGeologicStructure
            // 
            this.gcGeologicStructure.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.gcGeologicStructure.Cursor = System.Windows.Forms.Cursors.Default;
            gridLevelNode1.RelationName = "Level1";
            this.gcGeologicStructure.LevelTree.Nodes.AddRange(new DevExpress.XtraGrid.GridLevelNode[] {
            gridLevelNode1});
            this.gcGeologicStructure.Location = new System.Drawing.Point(10, 23);
            this.gcGeologicStructure.MainView = this.bandedGridView1;
            this.gcGeologicStructure.Name = "gcGeologicStructure";
            this.gcGeologicStructure.Size = new System.Drawing.Size(923, 495);
            this.gcGeologicStructure.TabIndex = 81;
            this.gcGeologicStructure.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.bandedGridView1});
            // 
            // bandedGridView1
            // 
            this.bandedGridView1.Bands.AddRange(new DevExpress.XtraGrid.Views.BandedGrid.GridBand[] {
            this.gridBand1,
            this.gridBand3,
            this.gridBand4});
            this.bandedGridView1.Columns.AddRange(new DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn[] {
            this.bandedGridColumn2,
            this.bandedGridColumn1,
            this.bandedGridColumn6,
            this.bandedGridColumn7,
            this.bandedGridColumn8,
            this.bandedGridColumn9,
            this.bandedGridColumn10,
            this.bandedGridColumn11,
            this.bandedGridColumn12,
            this.bandedGridColumn13,
            this.bandedGridColumn14,
            this.bandedGridColumn15,
            this.bandedGridColumn16,
            this.bandedGridColumn17,
            this.bandedGridColumn18,
            this.bandedGridColumn19,
            this.bandedGridColumn20});
            this.bandedGridView1.GridControl = this.gcGeologicStructure;
            this.bandedGridView1.Name = "bandedGridView1";
            this.bandedGridView1.OptionsBehavior.Editable = false;
            this.bandedGridView1.CustomColumnDisplayText += new DevExpress.XtraGrid.Views.Base.CustomColumnDisplayTextEventHandler(this.bandedGridView1_CustomColumnDisplayText);
            // 
            // gridBand1
            // 
            this.gridBand1.AppearanceHeader.Options.UseTextOptions = true;
            this.gridBand1.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.gridBand1.Caption = "巷道信息";
            this.gridBand1.Columns.Add(this.bandedGridColumn2);
            this.gridBand1.Columns.Add(this.bandedGridColumn1);
            this.gridBand1.Name = "gridBand1";
            this.gridBand1.VisibleIndex = 0;
            this.gridBand1.Width = 110;
            // 
            // bandedGridColumn2
            // 
            this.bandedGridColumn2.Caption = "工作面名称";
            this.bandedGridColumn2.FieldName = "Tunnel.WorkingFace.WorkingFaceName";
            this.bandedGridColumn2.Name = "bandedGridColumn2";
            this.bandedGridColumn2.Visible = true;
            this.bandedGridColumn2.Width = 54;
            // 
            // bandedGridColumn1
            // 
            this.bandedGridColumn1.Caption = "巷道名称";
            this.bandedGridColumn1.FieldName = "Tunnel.TunnelName";
            this.bandedGridColumn1.Name = "bandedGridColumn1";
            this.bandedGridColumn1.Visible = true;
            this.bandedGridColumn1.Width = 56;
            // 
            // gridBand3
            // 
            this.gridBand3.AppearanceHeader.Options.UseTextOptions = true;
            this.gridBand3.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.gridBand3.Caption = "地质构造预警指标";
            this.gridBand3.Columns.Add(this.bandedGridColumn10);
            this.gridBand3.Columns.Add(this.bandedGridColumn13);
            this.gridBand3.Columns.Add(this.bandedGridColumn7);
            this.gridBand3.Columns.Add(this.bandedGridColumn11);
            this.gridBand3.Columns.Add(this.bandedGridColumn15);
            this.gridBand3.Columns.Add(this.bandedGridColumn9);
            this.gridBand3.Columns.Add(this.bandedGridColumn8);
            this.gridBand3.Columns.Add(this.bandedGridColumn12);
            this.gridBand3.Columns.Add(this.bandedGridColumn14);
            this.gridBand3.Columns.Add(this.bandedGridColumn16);
            this.gridBand3.Name = "gridBand3";
            this.gridBand3.VisibleIndex = 1;
            this.gridBand3.Width = 526;
            // 
            // bandedGridColumn10
            // 
            this.bandedGridColumn10.Caption = "顶板破碎";
            this.bandedGridColumn10.FieldName = "RoofBroken";
            this.bandedGridColumn10.Name = "bandedGridColumn10";
            this.bandedGridColumn10.Visible = true;
            this.bandedGridColumn10.Width = 51;
            // 
            // bandedGridColumn13
            // 
            this.bandedGridColumn13.Caption = "顶板条件发生变化";
            this.bandedGridColumn13.FieldName = "RoofChange";
            this.bandedGridColumn13.Name = "bandedGridColumn13";
            this.bandedGridColumn13.Visible = true;
            this.bandedGridColumn13.Width = 51;
            // 
            // bandedGridColumn7
            // 
            this.bandedGridColumn7.Caption = "无计划揭露构造";
            this.bandedGridColumn7.FieldName = "NoPlanStructure";
            this.bandedGridColumn7.Name = "bandedGridColumn7";
            this.bandedGridColumn7.Visible = true;
            this.bandedGridColumn7.Width = 51;
            // 
            // bandedGridColumn11
            // 
            this.bandedGridColumn11.Caption = "煤层松软";
            this.bandedGridColumn11.FieldName = "CoalSeamSoft";
            this.bandedGridColumn11.Name = "bandedGridColumn11";
            this.bandedGridColumn11.Visible = true;
            this.bandedGridColumn11.Width = 51;
            // 
            // bandedGridColumn15
            // 
            this.bandedGridColumn15.Caption = "夹矸位置急剧变化";
            this.bandedGridColumn15.FieldName = "GangueLocationChange";
            this.bandedGridColumn15.Name = "bandedGridColumn15";
            this.bandedGridColumn15.Visible = true;
            this.bandedGridColumn15.Width = 51;
            // 
            // bandedGridColumn9
            // 
            this.bandedGridColumn9.Caption = "黄色预警措施无效";
            this.bandedGridColumn9.FieldName = "YellowRuleInvalid";
            this.bandedGridColumn9.Name = "bandedGridColumn9";
            this.bandedGridColumn9.Visible = true;
            this.bandedGridColumn9.Width = 51;
            // 
            // bandedGridColumn8
            // 
            this.bandedGridColumn8.Caption = "过构造时措施无效";
            this.bandedGridColumn8.FieldName = "PassedStructureRuleInvalid";
            this.bandedGridColumn8.Name = "bandedGridColumn8";
            this.bandedGridColumn8.Visible = true;
            this.bandedGridColumn8.Width = 51;
            // 
            // bandedGridColumn12
            // 
            this.bandedGridColumn12.Caption = "工作面煤层处于分叉、合层状态";
            this.bandedGridColumn12.FieldName = "CoalSeamBranch";
            this.bandedGridColumn12.Name = "bandedGridColumn12";
            this.bandedGridColumn12.Visible = true;
            this.bandedGridColumn12.Width = 51;
            // 
            // bandedGridColumn14
            // 
            this.bandedGridColumn14.Caption = "工作面夹矸突然变薄或消失";
            this.bandedGridColumn14.FieldName = "GangueDisappear";
            this.bandedGridColumn14.Name = "bandedGridColumn14";
            this.bandedGridColumn14.Visible = true;
            this.bandedGridColumn14.Width = 51;
            // 
            // bandedGridColumn16
            // 
            this.bandedGridColumn16.Caption = "备注";
            this.bandedGridColumn16.Name = "bandedGridColumn16";
            this.bandedGridColumn16.Visible = true;
            this.bandedGridColumn16.Width = 67;
            // 
            // gridBand4
            // 
            this.gridBand4.AppearanceHeader.Options.UseTextOptions = true;
            this.gridBand4.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.gridBand4.Caption = "录入信息";
            this.gridBand4.Columns.Add(this.bandedGridColumn6);
            this.gridBand4.Columns.Add(this.bandedGridColumn19);
            this.gridBand4.Columns.Add(this.bandedGridColumn17);
            this.gridBand4.Columns.Add(this.bandedGridColumn20);
            this.gridBand4.Columns.Add(this.bandedGridColumn18);
            this.gridBand4.Name = "gridBand4";
            this.gridBand4.VisibleIndex = 2;
            this.gridBand4.Width = 266;
            // 
            // bandedGridColumn6
            // 
            this.bandedGridColumn6.Caption = "提交日期";
            this.bandedGridColumn6.FieldName = "Datetime";
            this.bandedGridColumn6.Name = "bandedGridColumn6";
            this.bandedGridColumn6.Visible = true;
            this.bandedGridColumn6.Width = 52;
            // 
            // bandedGridColumn19
            // 
            this.bandedGridColumn19.Caption = "填报人";
            this.bandedGridColumn19.FieldName = "Submitter";
            this.bandedGridColumn19.Name = "bandedGridColumn19";
            this.bandedGridColumn19.Visible = true;
            this.bandedGridColumn19.Width = 52;
            // 
            // bandedGridColumn17
            // 
            this.bandedGridColumn17.Caption = "工作制式";
            this.bandedGridColumn17.FieldName = "WorkStyle";
            this.bandedGridColumn17.Name = "bandedGridColumn17";
            this.bandedGridColumn17.Visible = true;
            this.bandedGridColumn17.Width = 52;
            // 
            // bandedGridColumn20
            // 
            this.bandedGridColumn20.Caption = "队别";
            this.bandedGridColumn20.FieldName = "TeamName";
            this.bandedGridColumn20.Name = "bandedGridColumn20";
            this.bandedGridColumn20.Visible = true;
            this.bandedGridColumn20.Width = 52;
            // 
            // bandedGridColumn18
            // 
            this.bandedGridColumn18.Caption = "班次";
            this.bandedGridColumn18.FieldName = "WorkTime";
            this.bandedGridColumn18.Name = "bandedGridColumn18";
            this.bandedGridColumn18.Visible = true;
            this.bandedGridColumn18.Width = 58;
            // 
            // controlNavigator1
            // 
            this.controlNavigator1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.controlNavigator1.Location = new System.Drawing.Point(10, 520);
            this.controlNavigator1.Name = "controlNavigator1";
            this.controlNavigator1.NavigatableControl = this.gcGeologicStructure;
            this.controlNavigator1.Size = new System.Drawing.Size(267, 21);
            this.controlNavigator1.TabIndex = 84;
            this.controlNavigator1.Text = "controlNavigator1";
            this.controlNavigator1.TextLocation = DevExpress.XtraEditors.NavigatorButtonsTextLocation.Center;
            this.controlNavigator1.TextStringFormat = "记录 {0} / {1}";
            // 
            // saveFileDialog1
            // 
            this.saveFileDialog1.Filter = "Excel 2007|*.xlsx|Excel 2003兼容|*.xls|PDF文档|*.pdf|TXT文档|*.txt";
            // 
            // GeologicStructureInfoManagement
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(944, 566);
            this.Controls.Add(this.controlNavigator1);
            this.Controls.Add(this.gcGeologicStructure);
            this.Controls.Add(this.toolStrip1);
            this.Controls.Add(this.statusStrip1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "GeologicStructureInfoManagement";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "井下数据瓦斯管理";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.GeologicStructureInfoManagement_Load);
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gcGeologicStructure)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.bandedGridView1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripButton tsBtnPrint;
        private System.Windows.Forms.ToolStripButton tsBtnExport;
        private System.Windows.Forms.ToolStripButton tsBtnAdd;
        private System.Windows.Forms.ToolStripButton tsBtnModify;
        private System.Windows.Forms.ToolStripButton tsBtnDel;
        private System.Windows.Forms.ToolStripButton tsBtnRefresh;
        private System.Windows.Forms.ToolStripButton tsBtnExit;
        private DevExpress.XtraGrid.GridControl gcGeologicStructure;
        private DevExpress.XtraGrid.Views.BandedGrid.BandedGridView bandedGridView1;
        private DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn bandedGridColumn2;
        private DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn bandedGridColumn1;
        private DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn bandedGridColumn10;
        private DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn bandedGridColumn13;
        private DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn bandedGridColumn7;
        private DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn bandedGridColumn11;
        private DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn bandedGridColumn15;
        private DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn bandedGridColumn9;
        private DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn bandedGridColumn8;
        private DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn bandedGridColumn12;
        private DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn bandedGridColumn14;
        private DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn bandedGridColumn16;
        private DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn bandedGridColumn6;
        private DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn bandedGridColumn19;
        private DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn bandedGridColumn17;
        private DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn bandedGridColumn20;
        private DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn bandedGridColumn18;
        private DevExpress.XtraEditors.ControlNavigator controlNavigator1;
        private DevExpress.XtraGrid.Views.BandedGrid.GridBand gridBand1;
        private DevExpress.XtraGrid.Views.BandedGrid.GridBand gridBand3;
        private DevExpress.XtraGrid.Views.BandedGrid.GridBand gridBand4;
        private System.Windows.Forms.SaveFileDialog saveFileDialog1;
    }
}