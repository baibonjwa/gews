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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(DepartmentInformation));
            FarPoint.Win.ComplexBorder complexBorder1 = new FarPoint.Win.ComplexBorder(new FarPoint.Win.ComplexBorderSide(FarPoint.Win.ComplexBorderSideStyle.ThinLine));
            FarPoint.Win.ComplexBorder complexBorder2 = new FarPoint.Win.ComplexBorder(new FarPoint.Win.ComplexBorderSide(FarPoint.Win.ComplexBorderSideStyle.ThinLine));
            FarPoint.Win.ComplexBorder complexBorder3 = new FarPoint.Win.ComplexBorder(new FarPoint.Win.ComplexBorderSide(FarPoint.Win.ComplexBorderSideStyle.ThinLine));
            FarPoint.Win.ComplexBorder complexBorder4 = new FarPoint.Win.ComplexBorder(new FarPoint.Win.ComplexBorderSide(FarPoint.Win.ComplexBorderSideStyle.ThinLine));
            FarPoint.Win.ComplexBorder complexBorder5 = new FarPoint.Win.ComplexBorder(new FarPoint.Win.ComplexBorderSide(FarPoint.Win.ComplexBorderSideStyle.ThinLine));
            FarPoint.Win.ComplexBorder complexBorder6 = new FarPoint.Win.ComplexBorder(new FarPoint.Win.ComplexBorderSide(FarPoint.Win.ComplexBorderSideStyle.ThinLine));
            FarPoint.Win.ComplexBorder complexBorder7 = new FarPoint.Win.ComplexBorder(new FarPoint.Win.ComplexBorderSide(FarPoint.Win.ComplexBorderSideStyle.ThinLine));
            FarPoint.Win.ComplexBorder complexBorder8 = new FarPoint.Win.ComplexBorder(new FarPoint.Win.ComplexBorderSide(FarPoint.Win.ComplexBorderSideStyle.ThinLine));
            FarPoint.Win.ComplexBorder complexBorder9 = new FarPoint.Win.ComplexBorder(new FarPoint.Win.ComplexBorderSide(FarPoint.Win.ComplexBorderSideStyle.ThinLine));
            FarPoint.Win.ComplexBorder complexBorder10 = new FarPoint.Win.ComplexBorder(new FarPoint.Win.ComplexBorderSide(FarPoint.Win.ComplexBorderSideStyle.ThinLine));
            FarPoint.Win.ComplexBorder complexBorder11 = new FarPoint.Win.ComplexBorder(new FarPoint.Win.ComplexBorderSide(FarPoint.Win.ComplexBorderSideStyle.ThinLine));
            FarPoint.Win.ComplexBorder complexBorder12 = new FarPoint.Win.ComplexBorder(new FarPoint.Win.ComplexBorderSide(FarPoint.Win.ComplexBorderSideStyle.ThinLine));
            FarPoint.Win.Spread.CellType.TextCellType textCellType1 = new FarPoint.Win.Spread.CellType.TextCellType();
            FarPoint.Win.Spread.CellType.TextCellType textCellType2 = new FarPoint.Win.Spread.CellType.TextCellType();
            FarPoint.Win.Spread.CellType.TextCellType textCellType3 = new FarPoint.Win.Spread.CellType.TextCellType();
            FarPoint.Win.Spread.CellType.TextCellType textCellType4 = new FarPoint.Win.Spread.CellType.TextCellType();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.tsBtnPrint = new System.Windows.Forms.ToolStripButton();
            this.tsBtnExport = new System.Windows.Forms.ToolStripButton();
            this.tsBtnAdd = new System.Windows.Forms.ToolStripButton();
            this.tsBtnDel = new System.Windows.Forms.ToolStripButton();
            this.tsBtnRefresh = new System.Windows.Forms.ToolStripButton();
            this.tsBtnExit = new System.Windows.Forms.ToolStripButton();
            this._fpDepartmentInfo = new FarPoint.Win.Spread.FpSpread();
            this.contextMenuStripRightButton = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.添加ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.删除ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.全选ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.刷新ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this._fpDepartmentInfo_Sheet1 = new FarPoint.Win.Spread.SheetView();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this._btnCancel = new System.Windows.Forms.Button();
            this._btnOK = new System.Windows.Forms.Button();
            this.printPreviewDialog1 = new System.Windows.Forms.PrintPreviewDialog();
            this.propertyGrid = new System.Windows.Forms.PropertyGrid();
            this._chkSelAll = new System.Windows.Forms.CheckBox();
            this.toolStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this._fpDepartmentInfo)).BeginInit();
            this.contextMenuStripRightButton.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this._fpDepartmentInfo_Sheet1)).BeginInit();
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
            this.tsBtnDel,
            this.tsBtnRefresh,
            this.tsBtnExit});
            this.toolStrip1.LayoutStyle = System.Windows.Forms.ToolStripLayoutStyle.Flow;
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(313, 24);
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
            // _fpDepartmentInfo
            // 
            this._fpDepartmentInfo.AccessibleDescription = "_fpDepartmentInfo, Sheet1, Row 0, Column 0, 部门信息";
            this._fpDepartmentInfo.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this._fpDepartmentInfo.BorderCollapse = FarPoint.Win.Spread.BorderCollapse.Collapse;
            this._fpDepartmentInfo.ColumnSplitBoxPolicy = FarPoint.Win.Spread.SplitBoxPolicy.AsNeeded;
            this._fpDepartmentInfo.ContextMenuStrip = this.contextMenuStripRightButton;
            this._fpDepartmentInfo.Location = new System.Drawing.Point(12, 27);
            this._fpDepartmentInfo.Name = "_fpDepartmentInfo";
            this._fpDepartmentInfo.RowSplitBoxPolicy = FarPoint.Win.Spread.SplitBoxPolicy.AsNeeded;
            this._fpDepartmentInfo.SelectionBlockOptions = ((FarPoint.Win.Spread.SelectionBlockOptions)(((FarPoint.Win.Spread.SelectionBlockOptions.Cells | FarPoint.Win.Spread.SelectionBlockOptions.Rows)
                        | FarPoint.Win.Spread.SelectionBlockOptions.Columns)));
            this._fpDepartmentInfo.Sheets.AddRange(new FarPoint.Win.Spread.SheetView[] {
            this._fpDepartmentInfo_Sheet1});
            this._fpDepartmentInfo.Size = new System.Drawing.Size(694, 448);
            this._fpDepartmentInfo.TabIndex = 1;
            this._fpDepartmentInfo.SelectionChanged += new FarPoint.Win.Spread.SelectionChangedEventHandler(this.fpDepartmentInfo_SelectionChanged);
            this._fpDepartmentInfo.CellClick += new FarPoint.Win.Spread.CellClickEventHandler(this.fpDepartmentInfo_CellClick);
            this._fpDepartmentInfo.ButtonClicked += new FarPoint.Win.Spread.EditorNotifyEventHandler(this.fpDepartmentInfo_ButtonClicked);
            this._fpDepartmentInfo.SetViewportLeftColumn(0, 0, 1);
            this._fpDepartmentInfo.SetViewportTopRow(0, 0, 2);
            this._fpDepartmentInfo.SetActiveViewport(0, -1, -1);
            // 
            // contextMenuStripRightButton
            // 
            this.contextMenuStripRightButton.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.添加ToolStripMenuItem,
            this.删除ToolStripMenuItem,
            this.全选ToolStripMenuItem,
            this.刷新ToolStripMenuItem});
            this.contextMenuStripRightButton.Name = "contextMenuStripRightButton";
            this.contextMenuStripRightButton.Size = new System.Drawing.Size(101, 92);
            // 
            // 添加ToolStripMenuItem
            // 
            this.添加ToolStripMenuItem.Name = "添加ToolStripMenuItem";
            this.添加ToolStripMenuItem.Size = new System.Drawing.Size(100, 22);
            this.添加ToolStripMenuItem.Text = "添加";
            this.添加ToolStripMenuItem.Click += new System.EventHandler(this.添加ToolStripMenuItem_Click);
            // 
            // 删除ToolStripMenuItem
            // 
            this.删除ToolStripMenuItem.Name = "删除ToolStripMenuItem";
            this.删除ToolStripMenuItem.Size = new System.Drawing.Size(100, 22);
            this.删除ToolStripMenuItem.Text = "删除";
            this.删除ToolStripMenuItem.Click += new System.EventHandler(this.删除ToolStripMenuItem_Click);
            // 
            // 全选ToolStripMenuItem
            // 
            this.全选ToolStripMenuItem.Name = "全选ToolStripMenuItem";
            this.全选ToolStripMenuItem.Size = new System.Drawing.Size(100, 22);
            this.全选ToolStripMenuItem.Text = "全选";
            this.全选ToolStripMenuItem.Click += new System.EventHandler(this.全选ToolStripMenuItem_Click);
            // 
            // 刷新ToolStripMenuItem
            // 
            this.刷新ToolStripMenuItem.Name = "刷新ToolStripMenuItem";
            this.刷新ToolStripMenuItem.Size = new System.Drawing.Size(100, 22);
            this.刷新ToolStripMenuItem.Text = "刷新";
            this.刷新ToolStripMenuItem.Click += new System.EventHandler(this.刷新ToolStripMenuItem_Click);
            // 
            // _fpDepartmentInfo_Sheet1
            // 
            this._fpDepartmentInfo_Sheet1.Reset();
            _fpDepartmentInfo_Sheet1.SheetName = "Sheet1";
            // Formulas and custom names must be loaded with R1C1 reference style
            this._fpDepartmentInfo_Sheet1.ReferenceStyle = FarPoint.Win.Spread.Model.ReferenceStyle.R1C1;
            _fpDepartmentInfo_Sheet1.ColumnCount = 15;
            _fpDepartmentInfo_Sheet1.RowCount = 50;
            this._fpDepartmentInfo_Sheet1.Cells.Get(0, 0).Border = complexBorder1;
            this._fpDepartmentInfo_Sheet1.Cells.Get(0, 0).ColumnSpan = 6;
            this._fpDepartmentInfo_Sheet1.Cells.Get(0, 0).Font = new System.Drawing.Font("宋体", 16F, System.Drawing.FontStyle.Bold);
            this._fpDepartmentInfo_Sheet1.Cells.Get(0, 0).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this._fpDepartmentInfo_Sheet1.Cells.Get(0, 0).Locked = true;
            this._fpDepartmentInfo_Sheet1.Cells.Get(0, 0).Value = "部门信息";
            this._fpDepartmentInfo_Sheet1.Cells.Get(0, 0).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            this._fpDepartmentInfo_Sheet1.Cells.Get(0, 1).Border = complexBorder2;
            this._fpDepartmentInfo_Sheet1.Cells.Get(0, 1).Font = new System.Drawing.Font("宋体", 16F, System.Drawing.FontStyle.Bold);
            this._fpDepartmentInfo_Sheet1.Cells.Get(0, 1).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this._fpDepartmentInfo_Sheet1.Cells.Get(0, 1).Locked = true;
            this._fpDepartmentInfo_Sheet1.Cells.Get(0, 1).Value = "部门信息";
            this._fpDepartmentInfo_Sheet1.Cells.Get(0, 1).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            this._fpDepartmentInfo_Sheet1.Cells.Get(0, 2).Border = complexBorder3;
            this._fpDepartmentInfo_Sheet1.Cells.Get(0, 2).Locked = true;
            this._fpDepartmentInfo_Sheet1.Cells.Get(0, 2).Tag = "部门电话";
            this._fpDepartmentInfo_Sheet1.Cells.Get(0, 3).Border = complexBorder4;
            this._fpDepartmentInfo_Sheet1.Cells.Get(0, 3).Locked = true;
            this._fpDepartmentInfo_Sheet1.Cells.Get(0, 3).Tag = "部门Email";
            this._fpDepartmentInfo_Sheet1.Cells.Get(0, 4).Border = complexBorder5;
            this._fpDepartmentInfo_Sheet1.Cells.Get(0, 4).Locked = true;
            this._fpDepartmentInfo_Sheet1.Cells.Get(0, 4).Tag = "员工人数";
            this._fpDepartmentInfo_Sheet1.Cells.Get(0, 5).Border = complexBorder6;
            this._fpDepartmentInfo_Sheet1.Cells.Get(0, 5).Locked = true;
            this._fpDepartmentInfo_Sheet1.Cells.Get(1, 0).Border = complexBorder7;
            this._fpDepartmentInfo_Sheet1.Cells.Get(1, 0).Locked = true;
            this._fpDepartmentInfo_Sheet1.Cells.Get(1, 0).Value = "选择";
            this._fpDepartmentInfo_Sheet1.Cells.Get(1, 0).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            this._fpDepartmentInfo_Sheet1.Cells.Get(1, 1).Border = complexBorder8;
            this._fpDepartmentInfo_Sheet1.Cells.Get(1, 1).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Left;
            this._fpDepartmentInfo_Sheet1.Cells.Get(1, 1).Locked = true;
            this._fpDepartmentInfo_Sheet1.Cells.Get(1, 1).Value = "部门名称";
            this._fpDepartmentInfo_Sheet1.Cells.Get(1, 1).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            this._fpDepartmentInfo_Sheet1.Cells.Get(1, 2).Border = complexBorder9;
            this._fpDepartmentInfo_Sheet1.Cells.Get(1, 2).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Left;
            this._fpDepartmentInfo_Sheet1.Cells.Get(1, 2).Locked = true;
            this._fpDepartmentInfo_Sheet1.Cells.Get(1, 2).Value = "部门电话";
            this._fpDepartmentInfo_Sheet1.Cells.Get(1, 2).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            this._fpDepartmentInfo_Sheet1.Cells.Get(1, 3).Border = complexBorder10;
            this._fpDepartmentInfo_Sheet1.Cells.Get(1, 3).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Left;
            this._fpDepartmentInfo_Sheet1.Cells.Get(1, 3).Locked = true;
            this._fpDepartmentInfo_Sheet1.Cells.Get(1, 3).Value = "部门Email";
            this._fpDepartmentInfo_Sheet1.Cells.Get(1, 3).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            this._fpDepartmentInfo_Sheet1.Cells.Get(1, 4).Border = complexBorder11;
            this._fpDepartmentInfo_Sheet1.Cells.Get(1, 4).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Left;
            this._fpDepartmentInfo_Sheet1.Cells.Get(1, 4).Locked = true;
            this._fpDepartmentInfo_Sheet1.Cells.Get(1, 4).Value = "员工人数";
            this._fpDepartmentInfo_Sheet1.Cells.Get(1, 4).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            this._fpDepartmentInfo_Sheet1.Cells.Get(1, 5).Border = complexBorder12;
            this._fpDepartmentInfo_Sheet1.Cells.Get(1, 5).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Left;
            this._fpDepartmentInfo_Sheet1.Cells.Get(1, 5).Locked = true;
            this._fpDepartmentInfo_Sheet1.Cells.Get(1, 5).Value = "备注";
            this._fpDepartmentInfo_Sheet1.Cells.Get(1, 5).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            this._fpDepartmentInfo_Sheet1.Cells.Get(1, 6).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this._fpDepartmentInfo_Sheet1.Cells.Get(1, 6).Locked = false;
            this._fpDepartmentInfo_Sheet1.Cells.Get(1, 6).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            this._fpDepartmentInfo_Sheet1.Cells.Get(2, 0).CellType = textCellType1;
            this._fpDepartmentInfo_Sheet1.Cells.Get(2, 0).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this._fpDepartmentInfo_Sheet1.Cells.Get(2, 0).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            this._fpDepartmentInfo_Sheet1.Cells.Get(2, 1).Locked = true;
            this._fpDepartmentInfo_Sheet1.Cells.Get(2, 2).CellType = textCellType2;
            this._fpDepartmentInfo_Sheet1.Cells.Get(2, 2).Locked = true;
            this._fpDepartmentInfo_Sheet1.Cells.Get(2, 3).Locked = true;
            this._fpDepartmentInfo_Sheet1.Cells.Get(2, 4).Locked = true;
            this._fpDepartmentInfo_Sheet1.Cells.Get(2, 5).Locked = true;
            this._fpDepartmentInfo_Sheet1.Cells.Get(3, 1).Locked = true;
            this._fpDepartmentInfo_Sheet1.Cells.Get(3, 2).Locked = true;
            this._fpDepartmentInfo_Sheet1.Cells.Get(3, 3).Locked = true;
            this._fpDepartmentInfo_Sheet1.Cells.Get(3, 4).Locked = true;
            this._fpDepartmentInfo_Sheet1.Cells.Get(3, 5).Locked = true;
            this._fpDepartmentInfo_Sheet1.Cells.Get(4, 1).Locked = true;
            this._fpDepartmentInfo_Sheet1.Cells.Get(4, 2).Locked = true;
            this._fpDepartmentInfo_Sheet1.Cells.Get(4, 3).Locked = true;
            this._fpDepartmentInfo_Sheet1.Cells.Get(4, 4).Locked = true;
            this._fpDepartmentInfo_Sheet1.Cells.Get(4, 5).Locked = true;
            this._fpDepartmentInfo_Sheet1.Cells.Get(5, 1).Locked = true;
            this._fpDepartmentInfo_Sheet1.Cells.Get(5, 2).Locked = true;
            this._fpDepartmentInfo_Sheet1.Cells.Get(5, 3).Locked = true;
            this._fpDepartmentInfo_Sheet1.Cells.Get(5, 4).Locked = true;
            this._fpDepartmentInfo_Sheet1.Cells.Get(5, 5).Locked = true;
            this._fpDepartmentInfo_Sheet1.Cells.Get(6, 1).Locked = true;
            this._fpDepartmentInfo_Sheet1.Cells.Get(6, 2).Locked = true;
            this._fpDepartmentInfo_Sheet1.Cells.Get(6, 3).Locked = true;
            this._fpDepartmentInfo_Sheet1.Cells.Get(6, 4).Locked = true;
            this._fpDepartmentInfo_Sheet1.Cells.Get(6, 5).Locked = true;
            this._fpDepartmentInfo_Sheet1.Cells.Get(7, 1).Locked = true;
            this._fpDepartmentInfo_Sheet1.Cells.Get(7, 2).Locked = true;
            this._fpDepartmentInfo_Sheet1.Cells.Get(7, 3).Locked = true;
            this._fpDepartmentInfo_Sheet1.Cells.Get(7, 4).Locked = true;
            this._fpDepartmentInfo_Sheet1.Cells.Get(7, 5).Locked = true;
            this._fpDepartmentInfo_Sheet1.Cells.Get(8, 1).Locked = true;
            this._fpDepartmentInfo_Sheet1.Cells.Get(8, 2).Locked = true;
            this._fpDepartmentInfo_Sheet1.Cells.Get(8, 3).Locked = true;
            this._fpDepartmentInfo_Sheet1.Cells.Get(8, 4).Locked = true;
            this._fpDepartmentInfo_Sheet1.Cells.Get(8, 5).Locked = true;
            this._fpDepartmentInfo_Sheet1.Cells.Get(9, 1).Locked = true;
            this._fpDepartmentInfo_Sheet1.Cells.Get(9, 2).Locked = true;
            this._fpDepartmentInfo_Sheet1.Cells.Get(9, 3).Locked = true;
            this._fpDepartmentInfo_Sheet1.Cells.Get(9, 4).Locked = true;
            this._fpDepartmentInfo_Sheet1.Cells.Get(9, 5).Locked = true;
            this._fpDepartmentInfo_Sheet1.Cells.Get(10, 1).Locked = true;
            this._fpDepartmentInfo_Sheet1.Cells.Get(10, 2).Locked = true;
            this._fpDepartmentInfo_Sheet1.Cells.Get(10, 3).Locked = true;
            this._fpDepartmentInfo_Sheet1.Cells.Get(10, 4).Locked = true;
            this._fpDepartmentInfo_Sheet1.Cells.Get(10, 5).Locked = true;
            this._fpDepartmentInfo_Sheet1.Cells.Get(11, 1).Locked = true;
            this._fpDepartmentInfo_Sheet1.Cells.Get(11, 2).Locked = true;
            this._fpDepartmentInfo_Sheet1.Cells.Get(11, 3).Locked = true;
            this._fpDepartmentInfo_Sheet1.Cells.Get(11, 4).Locked = true;
            this._fpDepartmentInfo_Sheet1.Cells.Get(11, 5).Locked = true;
            this._fpDepartmentInfo_Sheet1.Cells.Get(12, 1).Locked = true;
            this._fpDepartmentInfo_Sheet1.Cells.Get(12, 2).Locked = true;
            this._fpDepartmentInfo_Sheet1.Cells.Get(12, 3).Locked = true;
            this._fpDepartmentInfo_Sheet1.Cells.Get(12, 4).Locked = true;
            this._fpDepartmentInfo_Sheet1.Cells.Get(12, 5).Locked = true;
            this._fpDepartmentInfo_Sheet1.Cells.Get(13, 1).Locked = true;
            this._fpDepartmentInfo_Sheet1.Cells.Get(13, 2).Locked = true;
            this._fpDepartmentInfo_Sheet1.Cells.Get(13, 3).Locked = true;
            this._fpDepartmentInfo_Sheet1.Cells.Get(13, 4).Locked = true;
            this._fpDepartmentInfo_Sheet1.Cells.Get(13, 5).Locked = true;
            this._fpDepartmentInfo_Sheet1.Cells.Get(14, 1).Locked = true;
            this._fpDepartmentInfo_Sheet1.Cells.Get(14, 2).Locked = true;
            this._fpDepartmentInfo_Sheet1.Cells.Get(14, 3).Locked = true;
            this._fpDepartmentInfo_Sheet1.Cells.Get(14, 4).Locked = true;
            this._fpDepartmentInfo_Sheet1.Cells.Get(14, 5).Locked = true;
            this._fpDepartmentInfo_Sheet1.Cells.Get(15, 1).Locked = true;
            this._fpDepartmentInfo_Sheet1.Cells.Get(15, 2).Locked = true;
            this._fpDepartmentInfo_Sheet1.Cells.Get(15, 3).Locked = true;
            this._fpDepartmentInfo_Sheet1.Cells.Get(15, 4).Locked = true;
            this._fpDepartmentInfo_Sheet1.Cells.Get(15, 5).Locked = true;
            this._fpDepartmentInfo_Sheet1.Cells.Get(16, 1).Locked = true;
            this._fpDepartmentInfo_Sheet1.Cells.Get(16, 2).Locked = true;
            this._fpDepartmentInfo_Sheet1.Cells.Get(16, 3).Locked = true;
            this._fpDepartmentInfo_Sheet1.Cells.Get(16, 4).Locked = true;
            this._fpDepartmentInfo_Sheet1.Cells.Get(16, 5).Locked = true;
            this._fpDepartmentInfo_Sheet1.Cells.Get(17, 1).Locked = true;
            this._fpDepartmentInfo_Sheet1.Cells.Get(17, 2).Locked = true;
            this._fpDepartmentInfo_Sheet1.Cells.Get(17, 3).Locked = true;
            this._fpDepartmentInfo_Sheet1.Cells.Get(17, 4).Locked = true;
            this._fpDepartmentInfo_Sheet1.Cells.Get(17, 5).Locked = true;
            this._fpDepartmentInfo_Sheet1.Cells.Get(18, 1).Locked = true;
            this._fpDepartmentInfo_Sheet1.Cells.Get(18, 2).Locked = true;
            this._fpDepartmentInfo_Sheet1.Cells.Get(18, 3).Locked = true;
            this._fpDepartmentInfo_Sheet1.Cells.Get(18, 4).Locked = true;
            this._fpDepartmentInfo_Sheet1.Cells.Get(18, 5).Locked = true;
            this._fpDepartmentInfo_Sheet1.Cells.Get(19, 1).Locked = true;
            this._fpDepartmentInfo_Sheet1.Cells.Get(19, 2).Locked = true;
            this._fpDepartmentInfo_Sheet1.Cells.Get(19, 3).Locked = true;
            this._fpDepartmentInfo_Sheet1.Cells.Get(19, 4).Locked = true;
            this._fpDepartmentInfo_Sheet1.Cells.Get(19, 5).Locked = true;
            this._fpDepartmentInfo_Sheet1.Cells.Get(20, 1).Locked = true;
            this._fpDepartmentInfo_Sheet1.Cells.Get(20, 2).Locked = true;
            this._fpDepartmentInfo_Sheet1.Cells.Get(20, 3).Locked = true;
            this._fpDepartmentInfo_Sheet1.Cells.Get(20, 4).Locked = true;
            this._fpDepartmentInfo_Sheet1.Cells.Get(20, 5).Locked = true;
            this._fpDepartmentInfo_Sheet1.Cells.Get(21, 1).Locked = true;
            this._fpDepartmentInfo_Sheet1.Cells.Get(21, 2).Locked = true;
            this._fpDepartmentInfo_Sheet1.Cells.Get(21, 3).Locked = true;
            this._fpDepartmentInfo_Sheet1.Cells.Get(21, 4).Locked = true;
            this._fpDepartmentInfo_Sheet1.Cells.Get(21, 5).Locked = true;
            this._fpDepartmentInfo_Sheet1.Cells.Get(22, 1).Locked = true;
            this._fpDepartmentInfo_Sheet1.Cells.Get(22, 2).Locked = true;
            this._fpDepartmentInfo_Sheet1.Cells.Get(22, 3).Locked = true;
            this._fpDepartmentInfo_Sheet1.Cells.Get(22, 4).Locked = true;
            this._fpDepartmentInfo_Sheet1.Cells.Get(22, 5).Locked = true;
            this._fpDepartmentInfo_Sheet1.Cells.Get(23, 1).Locked = true;
            this._fpDepartmentInfo_Sheet1.Cells.Get(23, 2).Locked = true;
            this._fpDepartmentInfo_Sheet1.Cells.Get(23, 3).Locked = true;
            this._fpDepartmentInfo_Sheet1.Cells.Get(23, 4).Locked = true;
            this._fpDepartmentInfo_Sheet1.Cells.Get(23, 5).Locked = true;
            this._fpDepartmentInfo_Sheet1.Cells.Get(24, 1).Locked = true;
            this._fpDepartmentInfo_Sheet1.Cells.Get(24, 2).Locked = true;
            this._fpDepartmentInfo_Sheet1.Cells.Get(24, 3).Locked = true;
            this._fpDepartmentInfo_Sheet1.Cells.Get(24, 4).Locked = true;
            this._fpDepartmentInfo_Sheet1.Cells.Get(24, 5).Locked = true;
            this._fpDepartmentInfo_Sheet1.Cells.Get(25, 1).Locked = true;
            this._fpDepartmentInfo_Sheet1.Cells.Get(25, 2).Locked = true;
            this._fpDepartmentInfo_Sheet1.Cells.Get(25, 3).Locked = true;
            this._fpDepartmentInfo_Sheet1.Cells.Get(25, 4).Locked = true;
            this._fpDepartmentInfo_Sheet1.Cells.Get(25, 5).Locked = true;
            this._fpDepartmentInfo_Sheet1.Cells.Get(26, 1).Locked = true;
            this._fpDepartmentInfo_Sheet1.Cells.Get(26, 2).Locked = true;
            this._fpDepartmentInfo_Sheet1.Cells.Get(26, 3).Locked = true;
            this._fpDepartmentInfo_Sheet1.Cells.Get(26, 4).Locked = true;
            this._fpDepartmentInfo_Sheet1.Cells.Get(26, 5).Locked = true;
            this._fpDepartmentInfo_Sheet1.Cells.Get(27, 1).Locked = true;
            this._fpDepartmentInfo_Sheet1.Cells.Get(27, 2).Locked = true;
            this._fpDepartmentInfo_Sheet1.Cells.Get(27, 3).Locked = true;
            this._fpDepartmentInfo_Sheet1.Cells.Get(27, 4).Locked = true;
            this._fpDepartmentInfo_Sheet1.Cells.Get(27, 5).Locked = true;
            this._fpDepartmentInfo_Sheet1.Cells.Get(28, 1).Locked = true;
            this._fpDepartmentInfo_Sheet1.Cells.Get(28, 2).Locked = true;
            this._fpDepartmentInfo_Sheet1.Cells.Get(28, 3).Locked = true;
            this._fpDepartmentInfo_Sheet1.Cells.Get(28, 4).Locked = true;
            this._fpDepartmentInfo_Sheet1.Cells.Get(28, 5).Locked = true;
            this._fpDepartmentInfo_Sheet1.Cells.Get(29, 1).Locked = true;
            this._fpDepartmentInfo_Sheet1.Cells.Get(29, 2).Locked = true;
            this._fpDepartmentInfo_Sheet1.Cells.Get(29, 3).Locked = true;
            this._fpDepartmentInfo_Sheet1.Cells.Get(29, 4).Locked = true;
            this._fpDepartmentInfo_Sheet1.Cells.Get(29, 5).Locked = true;
            this._fpDepartmentInfo_Sheet1.Cells.Get(30, 1).Locked = true;
            this._fpDepartmentInfo_Sheet1.Cells.Get(30, 2).Locked = true;
            this._fpDepartmentInfo_Sheet1.Cells.Get(30, 3).Locked = true;
            this._fpDepartmentInfo_Sheet1.Cells.Get(30, 4).Locked = true;
            this._fpDepartmentInfo_Sheet1.Cells.Get(30, 5).Locked = true;
            this._fpDepartmentInfo_Sheet1.Cells.Get(31, 1).Locked = true;
            this._fpDepartmentInfo_Sheet1.Cells.Get(31, 2).Locked = true;
            this._fpDepartmentInfo_Sheet1.Cells.Get(31, 3).Locked = true;
            this._fpDepartmentInfo_Sheet1.Cells.Get(31, 4).Locked = true;
            this._fpDepartmentInfo_Sheet1.Cells.Get(31, 5).Locked = true;
            this._fpDepartmentInfo_Sheet1.Cells.Get(32, 1).Locked = true;
            this._fpDepartmentInfo_Sheet1.Cells.Get(32, 2).Locked = true;
            this._fpDepartmentInfo_Sheet1.Cells.Get(32, 3).Locked = true;
            this._fpDepartmentInfo_Sheet1.Cells.Get(32, 4).Locked = true;
            this._fpDepartmentInfo_Sheet1.Cells.Get(32, 5).Locked = true;
            this._fpDepartmentInfo_Sheet1.Cells.Get(33, 1).Locked = true;
            this._fpDepartmentInfo_Sheet1.Cells.Get(33, 2).Locked = true;
            this._fpDepartmentInfo_Sheet1.Cells.Get(33, 3).Locked = true;
            this._fpDepartmentInfo_Sheet1.Cells.Get(33, 4).Locked = true;
            this._fpDepartmentInfo_Sheet1.Cells.Get(33, 5).Locked = true;
            this._fpDepartmentInfo_Sheet1.Cells.Get(34, 1).Locked = true;
            this._fpDepartmentInfo_Sheet1.Cells.Get(34, 2).Locked = true;
            this._fpDepartmentInfo_Sheet1.Cells.Get(34, 3).Locked = true;
            this._fpDepartmentInfo_Sheet1.Cells.Get(34, 4).Locked = true;
            this._fpDepartmentInfo_Sheet1.Cells.Get(34, 5).Locked = true;
            this._fpDepartmentInfo_Sheet1.Cells.Get(35, 1).Locked = true;
            this._fpDepartmentInfo_Sheet1.Cells.Get(35, 2).Locked = true;
            this._fpDepartmentInfo_Sheet1.Cells.Get(35, 3).Locked = true;
            this._fpDepartmentInfo_Sheet1.Cells.Get(35, 4).Locked = true;
            this._fpDepartmentInfo_Sheet1.Cells.Get(35, 5).Locked = true;
            this._fpDepartmentInfo_Sheet1.Cells.Get(36, 1).Locked = true;
            this._fpDepartmentInfo_Sheet1.Cells.Get(36, 2).Locked = true;
            this._fpDepartmentInfo_Sheet1.Cells.Get(36, 3).Locked = true;
            this._fpDepartmentInfo_Sheet1.Cells.Get(36, 4).Locked = true;
            this._fpDepartmentInfo_Sheet1.Cells.Get(36, 5).Locked = true;
            this._fpDepartmentInfo_Sheet1.Cells.Get(37, 1).Locked = true;
            this._fpDepartmentInfo_Sheet1.Cells.Get(37, 2).Locked = true;
            this._fpDepartmentInfo_Sheet1.Cells.Get(37, 3).Locked = true;
            this._fpDepartmentInfo_Sheet1.Cells.Get(37, 4).Locked = true;
            this._fpDepartmentInfo_Sheet1.Cells.Get(37, 5).Locked = true;
            this._fpDepartmentInfo_Sheet1.Cells.Get(38, 1).Locked = true;
            this._fpDepartmentInfo_Sheet1.Cells.Get(38, 2).Locked = true;
            this._fpDepartmentInfo_Sheet1.Cells.Get(38, 3).Locked = true;
            this._fpDepartmentInfo_Sheet1.Cells.Get(38, 4).Locked = true;
            this._fpDepartmentInfo_Sheet1.Cells.Get(38, 5).Locked = true;
            this._fpDepartmentInfo_Sheet1.Cells.Get(39, 1).Locked = true;
            this._fpDepartmentInfo_Sheet1.Cells.Get(39, 2).Locked = true;
            this._fpDepartmentInfo_Sheet1.Cells.Get(39, 3).Locked = true;
            this._fpDepartmentInfo_Sheet1.Cells.Get(39, 4).Locked = true;
            this._fpDepartmentInfo_Sheet1.Cells.Get(39, 5).Locked = true;
            this._fpDepartmentInfo_Sheet1.Cells.Get(40, 1).Locked = true;
            this._fpDepartmentInfo_Sheet1.Cells.Get(40, 2).Locked = true;
            this._fpDepartmentInfo_Sheet1.Cells.Get(40, 3).Locked = true;
            this._fpDepartmentInfo_Sheet1.Cells.Get(40, 4).Locked = true;
            this._fpDepartmentInfo_Sheet1.Cells.Get(40, 5).Locked = true;
            this._fpDepartmentInfo_Sheet1.Cells.Get(41, 1).Locked = true;
            this._fpDepartmentInfo_Sheet1.Cells.Get(41, 2).Locked = true;
            this._fpDepartmentInfo_Sheet1.Cells.Get(41, 3).Locked = true;
            this._fpDepartmentInfo_Sheet1.Cells.Get(41, 4).Locked = true;
            this._fpDepartmentInfo_Sheet1.Cells.Get(41, 5).Locked = true;
            this._fpDepartmentInfo_Sheet1.Cells.Get(42, 1).Locked = true;
            this._fpDepartmentInfo_Sheet1.Cells.Get(42, 2).Locked = true;
            this._fpDepartmentInfo_Sheet1.Cells.Get(42, 3).Locked = true;
            this._fpDepartmentInfo_Sheet1.Cells.Get(42, 4).Locked = true;
            this._fpDepartmentInfo_Sheet1.Cells.Get(42, 5).Locked = true;
            this._fpDepartmentInfo_Sheet1.Cells.Get(43, 1).Locked = true;
            this._fpDepartmentInfo_Sheet1.Cells.Get(43, 2).Locked = true;
            this._fpDepartmentInfo_Sheet1.Cells.Get(43, 3).Locked = true;
            this._fpDepartmentInfo_Sheet1.Cells.Get(43, 4).Locked = true;
            this._fpDepartmentInfo_Sheet1.Cells.Get(43, 5).Locked = true;
            this._fpDepartmentInfo_Sheet1.Cells.Get(44, 1).Locked = true;
            this._fpDepartmentInfo_Sheet1.Cells.Get(44, 2).Locked = true;
            this._fpDepartmentInfo_Sheet1.Cells.Get(44, 3).Locked = true;
            this._fpDepartmentInfo_Sheet1.Cells.Get(44, 4).Locked = true;
            this._fpDepartmentInfo_Sheet1.Cells.Get(44, 5).Locked = true;
            this._fpDepartmentInfo_Sheet1.Cells.Get(45, 1).Locked = true;
            this._fpDepartmentInfo_Sheet1.Cells.Get(45, 2).Locked = true;
            this._fpDepartmentInfo_Sheet1.Cells.Get(45, 3).Locked = true;
            this._fpDepartmentInfo_Sheet1.Cells.Get(45, 4).Locked = true;
            this._fpDepartmentInfo_Sheet1.Cells.Get(45, 5).Locked = true;
            this._fpDepartmentInfo_Sheet1.Cells.Get(46, 1).Locked = true;
            this._fpDepartmentInfo_Sheet1.Cells.Get(46, 2).Locked = true;
            this._fpDepartmentInfo_Sheet1.Cells.Get(46, 3).Locked = true;
            this._fpDepartmentInfo_Sheet1.Cells.Get(46, 4).Locked = true;
            this._fpDepartmentInfo_Sheet1.Cells.Get(46, 5).Locked = true;
            this._fpDepartmentInfo_Sheet1.Cells.Get(47, 1).Locked = true;
            this._fpDepartmentInfo_Sheet1.Cells.Get(47, 2).Locked = true;
            this._fpDepartmentInfo_Sheet1.Cells.Get(47, 3).Locked = true;
            this._fpDepartmentInfo_Sheet1.Cells.Get(47, 4).Locked = true;
            this._fpDepartmentInfo_Sheet1.Cells.Get(47, 5).Locked = true;
            this._fpDepartmentInfo_Sheet1.Cells.Get(48, 1).Locked = true;
            this._fpDepartmentInfo_Sheet1.Cells.Get(48, 2).Locked = true;
            this._fpDepartmentInfo_Sheet1.Cells.Get(48, 3).Locked = true;
            this._fpDepartmentInfo_Sheet1.Cells.Get(48, 4).Locked = true;
            this._fpDepartmentInfo_Sheet1.Cells.Get(48, 5).Locked = true;
            this._fpDepartmentInfo_Sheet1.Cells.Get(49, 1).Locked = true;
            this._fpDepartmentInfo_Sheet1.Cells.Get(49, 2).Locked = true;
            this._fpDepartmentInfo_Sheet1.Cells.Get(49, 3).Locked = true;
            this._fpDepartmentInfo_Sheet1.Cells.Get(49, 4).Locked = true;
            this._fpDepartmentInfo_Sheet1.Cells.Get(49, 5).Locked = true;
            this._fpDepartmentInfo_Sheet1.Columns.Get(0).CellType = textCellType3;
            this._fpDepartmentInfo_Sheet1.Columns.Get(0).Locked = false;
            this._fpDepartmentInfo_Sheet1.Columns.Get(0).Resizable = false;
            this._fpDepartmentInfo_Sheet1.Columns.Get(0).Width = 33F;
            this._fpDepartmentInfo_Sheet1.Columns.Get(1).Locked = true;
            this._fpDepartmentInfo_Sheet1.Columns.Get(1).Tag = "部门名称";
            this._fpDepartmentInfo_Sheet1.Columns.Get(1).Width = 160F;
            this._fpDepartmentInfo_Sheet1.Columns.Get(2).CellType = textCellType4;
            this._fpDepartmentInfo_Sheet1.Columns.Get(2).Locked = true;
            this._fpDepartmentInfo_Sheet1.Columns.Get(2).Tag = "部门电话";
            this._fpDepartmentInfo_Sheet1.Columns.Get(2).Width = 140F;
            this._fpDepartmentInfo_Sheet1.Columns.Get(3).Locked = true;
            this._fpDepartmentInfo_Sheet1.Columns.Get(3).Tag = "部门Email";
            this._fpDepartmentInfo_Sheet1.Columns.Get(3).Width = 150F;
            this._fpDepartmentInfo_Sheet1.Columns.Get(4).Locked = true;
            this._fpDepartmentInfo_Sheet1.Columns.Get(4).Tag = "员工人数";
            this._fpDepartmentInfo_Sheet1.Columns.Get(4).Width = 80F;
            this._fpDepartmentInfo_Sheet1.Columns.Get(5).Locked = true;
            this._fpDepartmentInfo_Sheet1.Columns.Get(5).Tag = "备注";
            this._fpDepartmentInfo_Sheet1.Columns.Get(5).Width = 180F;
            this._fpDepartmentInfo_Sheet1.Columns.Get(6).Locked = true;
            this._fpDepartmentInfo_Sheet1.Columns.Get(6).Width = 84F;
            this._fpDepartmentInfo_Sheet1.FrozenColumnCount = 1;
            this._fpDepartmentInfo_Sheet1.FrozenRowCount = 2;
            this._fpDepartmentInfo_Sheet1.RowHeader.Columns.Default.Resizable = false;
            this._fpDepartmentInfo_Sheet1.Rows.Get(0).Height = 46F;
            this._fpDepartmentInfo_Sheet1.Rows.Get(0).Locked = true;
            this._fpDepartmentInfo_Sheet1.Rows.Get(1).Locked = true;
            this._fpDepartmentInfo_Sheet1.ReferenceStyle = FarPoint.Win.Spread.Model.ReferenceStyle.A1;
            // 
            // statusStrip1
            // 
            this.statusStrip1.Location = new System.Drawing.Point(0, 507);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(970, 22);
            this.statusStrip1.TabIndex = 50;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // _btnCancel
            // 
            this._btnCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this._btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this._btnCancel.Location = new System.Drawing.Point(883, 481);
            this._btnCancel.Name = "_btnCancel";
            this._btnCancel.Size = new System.Drawing.Size(75, 23);
            this._btnCancel.TabIndex = 5;
            this._btnCancel.Text = "取消";
            this._btnCancel.UseVisualStyleBackColor = true;
            this._btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // _btnOK
            // 
            this._btnOK.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this._btnOK.DialogResult = System.Windows.Forms.DialogResult.OK;
            this._btnOK.Location = new System.Drawing.Point(802, 481);
            this._btnOK.Name = "_btnOK";
            this._btnOK.Size = new System.Drawing.Size(75, 23);
            this._btnOK.TabIndex = 4;
            this._btnOK.Text = "确定";
            this._btnOK.UseVisualStyleBackColor = true;
            this._btnOK.Click += new System.EventHandler(this.btnOK_Click);
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
            // propertyGrid
            // 
            this.propertyGrid.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.propertyGrid.Location = new System.Drawing.Point(712, 27);
            this.propertyGrid.Name = "propertyGrid";
            this.propertyGrid.Size = new System.Drawing.Size(248, 448);
            this.propertyGrid.TabIndex = 3;
            this.propertyGrid.PropertyValueChanged += new System.Windows.Forms.PropertyValueChangedEventHandler(this.propertyGrid_PropertyValueChanged);
            // 
            // _chkSelAll
            // 
            this._chkSelAll.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this._chkSelAll.AutoSize = true;
            this._chkSelAll.Location = new System.Drawing.Point(59, 481);
            this._chkSelAll.Name = "_chkSelAll";
            this._chkSelAll.Size = new System.Drawing.Size(78, 16);
            this._chkSelAll.TabIndex = 2;
            this._chkSelAll.Text = "全选/不选";
            this._chkSelAll.UseVisualStyleBackColor = true;
            this._chkSelAll.CheckedChanged += new System.EventHandler(this.chkSelAll_CheckedChanged);
            // 
            // DepartmentInformation
            // 
            this.AcceptButton = this._btnOK;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this._btnCancel;
            this.ClientSize = new System.Drawing.Size(970, 529);
            this.Controls.Add(this._chkSelAll);
            this.Controls.Add(this.propertyGrid);
            this.Controls.Add(this._btnCancel);
            this.Controls.Add(this._btnOK);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this._fpDepartmentInfo);
            this.Controls.Add(this.toolStrip1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "DepartmentInformation";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "部门信息管理";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.DepartmentInformation_Load);
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this._fpDepartmentInfo)).EndInit();
            this.contextMenuStripRightButton.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this._fpDepartmentInfo_Sheet1)).EndInit();
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
        private FarPoint.Win.Spread.FpSpread _fpDepartmentInfo;
        private FarPoint.Win.Spread.SheetView _fpDepartmentInfo_Sheet1;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.Button _btnCancel;
        private System.Windows.Forms.Button _btnOK;
        private System.Windows.Forms.PrintPreviewDialog printPreviewDialog1;
        private System.Windows.Forms.PropertyGrid propertyGrid;
        private System.Windows.Forms.CheckBox _chkSelAll;
        private System.Windows.Forms.ContextMenuStrip contextMenuStripRightButton;
        private System.Windows.Forms.ToolStripMenuItem 添加ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 删除ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 全选ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 刷新ToolStripMenuItem;
    }
}