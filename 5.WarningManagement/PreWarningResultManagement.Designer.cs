namespace _5.WarningManagement
{
    partial class PreWarningResultManagement
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(PreWarningResultManagement));
            FarPoint.Win.ComplexBorder complexBorder1 = new FarPoint.Win.ComplexBorder(new FarPoint.Win.ComplexBorderSide(FarPoint.Win.ComplexBorderSideStyle.ThinLine));
            FarPoint.Win.Spread.CellType.GeneralCellType generalCellType1 = new FarPoint.Win.Spread.CellType.GeneralCellType();
            FarPoint.Win.ComplexBorder complexBorder2 = new FarPoint.Win.ComplexBorder(new FarPoint.Win.ComplexBorderSide(FarPoint.Win.ComplexBorderSideStyle.ThinLine));
            FarPoint.Win.ComplexBorder complexBorder3 = new FarPoint.Win.ComplexBorder(new FarPoint.Win.ComplexBorderSide(FarPoint.Win.ComplexBorderSideStyle.ThinLine));
            FarPoint.Win.ComplexBorder complexBorder4 = new FarPoint.Win.ComplexBorder(new FarPoint.Win.ComplexBorderSide(FarPoint.Win.ComplexBorderSideStyle.ThinLine));
            FarPoint.Win.ComplexBorder complexBorder5 = new FarPoint.Win.ComplexBorder(new FarPoint.Win.ComplexBorderSide(FarPoint.Win.ComplexBorderSideStyle.ThinLine));
            FarPoint.Win.ComplexBorder complexBorder6 = new FarPoint.Win.ComplexBorder(new FarPoint.Win.ComplexBorderSide(FarPoint.Win.ComplexBorderSideStyle.ThinLine));
            FarPoint.Win.Spread.CellType.CheckBoxCellType checkBoxCellType1 = new FarPoint.Win.Spread.CellType.CheckBoxCellType();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.btnPrint = new System.Windows.Forms.ToolStripButton();
            this.toolStripButton4 = new System.Windows.Forms.ToolStripButton();
            this.btnAdd = new System.Windows.Forms.ToolStripButton();
            this.btnUpdate = new System.Windows.Forms.ToolStripButton();
            this.btnDelete = new System.Windows.Forms.ToolStripButton();
            this.toolStripButton6 = new System.Windows.Forms.ToolStripButton();
            this.btnExit = new System.Windows.Forms.ToolStripButton();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.fpRules = new FarPoint.Win.Spread.FpSpread();
            this.fpRules_Sheet1 = new FarPoint.Win.Spread.SheetView();
            this.toolStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.fpRules)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.fpRules_Sheet1)).BeginInit();
            this.SuspendLayout();
            // 
            // toolStrip1
            // 
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.btnPrint,
            this.toolStripButton4,
            this.btnAdd,
            this.btnUpdate,
            this.btnDelete,
            this.toolStripButton6,
            this.btnExit});
            this.toolStrip1.LayoutStyle = System.Windows.Forms.ToolStripLayoutStyle.Flow;
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(828, 24);
            this.toolStrip1.TabIndex = 0;
            this.toolStrip1.TabStop = true;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // btnPrint
            // 
            this.btnPrint.Image = ((System.Drawing.Image)(resources.GetObject("btnPrint.Image")));
            this.btnPrint.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnPrint.Name = "btnPrint";
            this.btnPrint.Size = new System.Drawing.Size(52, 21);
            this.btnPrint.Text = "打印";
            this.btnPrint.Click += new System.EventHandler(this.btnPrint_Click);
            // 
            // toolStripButton4
            // 
            this.toolStripButton4.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButton4.Image")));
            this.toolStripButton4.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton4.Name = "toolStripButton4";
            this.toolStripButton4.Size = new System.Drawing.Size(52, 21);
            this.toolStripButton4.Text = "导出";
            // 
            // btnAdd
            // 
            this.btnAdd.Image = ((System.Drawing.Image)(resources.GetObject("btnAdd.Image")));
            this.btnAdd.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnAdd.Name = "btnAdd";
            this.btnAdd.Size = new System.Drawing.Size(52, 21);
            this.btnAdd.Text = "添加";
            // 
            // btnUpdate
            // 
            this.btnUpdate.Enabled = false;
            this.btnUpdate.Image = ((System.Drawing.Image)(resources.GetObject("btnUpdate.Image")));
            this.btnUpdate.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnUpdate.Name = "btnUpdate";
            this.btnUpdate.Size = new System.Drawing.Size(52, 21);
            this.btnUpdate.Text = "修改";
            // 
            // btnDelete
            // 
            this.btnDelete.Enabled = false;
            this.btnDelete.Image = ((System.Drawing.Image)(resources.GetObject("btnDelete.Image")));
            this.btnDelete.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnDelete.Name = "btnDelete";
            this.btnDelete.Size = new System.Drawing.Size(52, 21);
            this.btnDelete.Text = "删除";
            // 
            // toolStripButton6
            // 
            this.toolStripButton6.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButton6.Image")));
            this.toolStripButton6.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton6.Name = "toolStripButton6";
            this.toolStripButton6.Size = new System.Drawing.Size(52, 21);
            this.toolStripButton6.Text = "刷新";
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
            // statusStrip1
            // 
            this.statusStrip1.Location = new System.Drawing.Point(0, 521);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(828, 22);
            this.statusStrip1.TabIndex = 2;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // fpRules
            // 
            this.fpRules.AccessibleDescription = "fpRules, Sheet1, Row 0, Column 0, 选择";
            this.fpRules.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.fpRules.ColumnSplitBoxPolicy = FarPoint.Win.Spread.SplitBoxPolicy.Never;
            this.fpRules.Location = new System.Drawing.Point(12, 28);
            this.fpRules.Name = "fpRules";
            this.fpRules.RowSplitBoxPolicy = FarPoint.Win.Spread.SplitBoxPolicy.Never;
            this.fpRules.Sheets.AddRange(new FarPoint.Win.Spread.SheetView[] {
            this.fpRules_Sheet1});
            this.fpRules.Size = new System.Drawing.Size(804, 490);
            this.fpRules.TabIndex = 1;
            this.fpRules.SetViewportLeftColumn(0, 0, 2);
            this.fpRules.SetViewportTopRow(0, 0, 1);
            this.fpRules.SetActiveViewport(0, -1, -1);
            // 
            // fpRules_Sheet1
            // 
            this.fpRules_Sheet1.Reset();
            fpRules_Sheet1.SheetName = "Sheet1";
            // Formulas and custom names must be loaded with R1C1 reference style
            this.fpRules_Sheet1.ReferenceStyle = FarPoint.Win.Spread.Model.ReferenceStyle.R1C1;
            fpRules_Sheet1.ColumnCount = 6;
            fpRules_Sheet1.RowCount = 4;
            this.fpRules_Sheet1.AutoSortFrozenRows = false;
            this.fpRules_Sheet1.Cells.Get(0, 0).Border = complexBorder1;
            this.fpRules_Sheet1.Cells.Get(0, 0).CellType = generalCellType1;
            this.fpRules_Sheet1.Cells.Get(0, 0).Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Bold);
            this.fpRules_Sheet1.Cells.Get(0, 0).Value = "选择";
            this.fpRules_Sheet1.Cells.Get(0, 0).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            this.fpRules_Sheet1.Cells.Get(0, 1).Border = complexBorder2;
            this.fpRules_Sheet1.Cells.Get(0, 1).Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Bold);
            this.fpRules_Sheet1.Cells.Get(0, 1).Value = "巷道名称";
            this.fpRules_Sheet1.Cells.Get(0, 1).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            this.fpRules_Sheet1.Cells.Get(0, 2).Border = complexBorder3;
            this.fpRules_Sheet1.Cells.Get(0, 2).Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Bold);
            this.fpRules_Sheet1.Cells.Get(0, 2).Value = "预警日期";
            this.fpRules_Sheet1.Cells.Get(0, 3).Border = complexBorder4;
            this.fpRules_Sheet1.Cells.Get(0, 3).Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Bold);
            this.fpRules_Sheet1.Cells.Get(0, 3).Value = "预警班次";
            this.fpRules_Sheet1.Cells.Get(0, 3).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            this.fpRules_Sheet1.Cells.Get(0, 4).Border = complexBorder5;
            this.fpRules_Sheet1.Cells.Get(0, 4).Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Bold);
            this.fpRules_Sheet1.Cells.Get(0, 4).Value = "超限预警";
            this.fpRules_Sheet1.Cells.Get(0, 4).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            this.fpRules_Sheet1.Cells.Get(0, 5).Border = complexBorder6;
            this.fpRules_Sheet1.Cells.Get(0, 5).Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Bold);
            this.fpRules_Sheet1.Cells.Get(0, 5).Value = "突出预警";
            this.fpRules_Sheet1.Cells.Get(0, 5).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            this.fpRules_Sheet1.Cells.Get(1, 1).RowSpan = 3;
            this.fpRules_Sheet1.Cells.Get(1, 1).Value = "1301巷";
            this.fpRules_Sheet1.Cells.Get(1, 2).RowSpan = 3;
            this.fpRules_Sheet1.Cells.Get(1, 3).Value = "早班";
            this.fpRules_Sheet1.Cells.Get(1, 4).Value = "黄色";
            this.fpRules_Sheet1.Cells.Get(1, 5).Value = "正常";
            this.fpRules_Sheet1.Cells.Get(2, 3).Value = "中班";
            this.fpRules_Sheet1.Cells.Get(2, 4).Value = "红色";
            this.fpRules_Sheet1.Cells.Get(2, 5).Value = "正常";
            this.fpRules_Sheet1.Cells.Get(3, 3).Value = "晚班";
            this.fpRules_Sheet1.Cells.Get(3, 4).Value = "正常";
            this.fpRules_Sheet1.Cells.Get(3, 5).Value = "正常";
            this.fpRules_Sheet1.Columns.Get(0).CellType = checkBoxCellType1;
            this.fpRules_Sheet1.Columns.Get(0).Locked = false;
            this.fpRules_Sheet1.Columns.Get(0).Width = 51F;
            this.fpRules_Sheet1.Columns.Get(1).AllowAutoSort = true;
            this.fpRules_Sheet1.Columns.Get(1).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.fpRules_Sheet1.Columns.Get(1).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            this.fpRules_Sheet1.Columns.Get(1).Width = 200F;
            this.fpRules_Sheet1.Columns.Get(2).AllowAutoSort = true;
            this.fpRules_Sheet1.Columns.Get(2).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            this.fpRules_Sheet1.Columns.Get(2).Width = 120F;
            this.fpRules_Sheet1.Columns.Get(3).AllowAutoSort = true;
            this.fpRules_Sheet1.Columns.Get(3).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            this.fpRules_Sheet1.Columns.Get(3).Width = 100F;
            this.fpRules_Sheet1.Columns.Get(4).AllowAutoSort = true;
            this.fpRules_Sheet1.Columns.Get(4).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            this.fpRules_Sheet1.Columns.Get(4).Width = 139F;
            this.fpRules_Sheet1.Columns.Get(5).AllowAutoSort = true;
            this.fpRules_Sheet1.Columns.Get(5).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            this.fpRules_Sheet1.Columns.Get(5).Width = 128F;
            this.fpRules_Sheet1.DefaultStyle.HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.fpRules_Sheet1.DefaultStyle.NoteIndicatorColor = System.Drawing.Color.Red;
            this.fpRules_Sheet1.DefaultStyle.Parent = "DataAreaDefault";
            this.fpRules_Sheet1.DefaultStyle.VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Top;
            this.fpRules_Sheet1.FrozenColumnCount = 2;
            this.fpRules_Sheet1.FrozenRowCount = 1;
            this.fpRules_Sheet1.RowHeader.Columns.Default.Resizable = false;
            this.fpRules_Sheet1.Rows.Get(0).Font = new System.Drawing.Font("宋体", 18F);
            this.fpRules_Sheet1.Rows.Get(0).Height = 30F;
            this.fpRules_Sheet1.Rows.Get(0).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.fpRules_Sheet1.Rows.Get(0).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            this.fpRules_Sheet1.SelectionPolicy = FarPoint.Win.Spread.Model.SelectionPolicy.Single;
            this.fpRules_Sheet1.SelectionUnit = FarPoint.Win.Spread.Model.SelectionUnit.Row;
            this.fpRules_Sheet1.ReferenceStyle = FarPoint.Win.Spread.Model.ReferenceStyle.A1;
            // 
            // PreWarningResultManagement
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(828, 543);
            this.Controls.Add(this.fpRules);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.toolStrip1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "PreWarningResultManagement";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "预警结果管理";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.fpRules)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.fpRules_Sheet1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripButton btnPrint;
        private System.Windows.Forms.ToolStripButton toolStripButton4;
        private System.Windows.Forms.ToolStripButton btnAdd;
        private System.Windows.Forms.ToolStripButton btnUpdate;
        private System.Windows.Forms.ToolStripButton btnDelete;
        private System.Windows.Forms.ToolStripButton toolStripButton6;
        private System.Windows.Forms.ToolStripButton btnExit;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private FarPoint.Win.Spread.FpSpread fpRules;
        private FarPoint.Win.Spread.SheetView fpRules_Sheet1;

    }
}