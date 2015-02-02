namespace sys3
{
    partial class WireInfoEntering
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
            this.selectTunnelUserControl1 = new LibCommonForm.SelectTunnelUserControl();
            this.lblWireName = new System.Windows.Forms.Label();
            this.txtWireName = new System.Windows.Forms.TextBox();
            this.dgrdvWire = new System.Windows.Forms.DataGridView();
            this.txtWirePointID = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.txtCoordinateX = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.txtCoordinateY = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.txtCoordinateZ = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.txtDistanceFromTheLeft = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.txtDistanceFromTheRight = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.txtDistanceFromTop = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.txtDistanceFromBottom = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.插入ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.复制ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.剪切ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.粘贴ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.上移ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.下移ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.btnSubmit = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.lblWireLevel = new System.Windows.Forms.Label();
            this.lblMeasureDate = new System.Windows.Forms.Label();
            this.lblVobserver = new System.Windows.Forms.Label();
            this.txtWireLevel = new System.Windows.Forms.TextBox();
            this.dtpMeasureDate = new System.Windows.Forms.DateTimePicker();
            this.dtpCountDate = new System.Windows.Forms.DateTimePicker();
            this.lblCounter = new System.Windows.Forms.Label();
            this.lblCountDate = new System.Windows.Forms.Label();
            this.dtpCheckDate = new System.Windows.Forms.DateTimePicker();
            this.lblChecker = new System.Windows.Forms.Label();
            this.lblCheckDate = new System.Windows.Forms.Label();
            this.cboVobserver = new System.Windows.Forms.ComboBox();
            this.cboCounter = new System.Windows.Forms.ComboBox();
            this.cboChecker = new System.Windows.Forms.ComboBox();
            this.label5 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.btnAdd = new System.Windows.Forms.Button();
            this.btnCopy = new System.Windows.Forms.Button();
            this.btnPaste = new System.Windows.Forms.Button();
            this.btnMoveUp = new System.Windows.Forms.Button();
            this.btnMoveDown = new System.Windows.Forms.Button();
            this.btnDel = new System.Windows.Forms.Button();
            this.btnTXT = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dgrdvWire)).BeginInit();
            this.contextMenuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // selectTunnelUserControl1
            // 
            this.selectTunnelUserControl1.ITunnelId = 0;
            this.selectTunnelUserControl1.Location = new System.Drawing.Point(12, 10);
            this.selectTunnelUserControl1.MainForm = null;
            this.selectTunnelUserControl1.Name = "selectTunnelUserControl1";
            this.selectTunnelUserControl1.Size = new System.Drawing.Size(583, 187);
            this.selectTunnelUserControl1.STunnelName = null;
            this.selectTunnelUserControl1.TabIndex = 36;
            // 
            // lblWireName
            // 
            this.lblWireName.AutoSize = true;
            this.lblWireName.Location = new System.Drawing.Point(19, 203);
            this.lblWireName.Name = "lblWireName";
            this.lblWireName.Size = new System.Drawing.Size(65, 12);
            this.lblWireName.TabIndex = 1;
            this.lblWireName.Text = "导线名称：";
            // 
            // txtWireName
            // 
            this.txtWireName.Location = new System.Drawing.Point(90, 199);
            this.txtWireName.Name = "txtWireName";
            this.txtWireName.Size = new System.Drawing.Size(119, 21);
            this.txtWireName.TabIndex = 2;
            // 
            // dgrdvWire
            // 
            this.dgrdvWire.AllowDrop = true;
            this.dgrdvWire.AllowUserToOrderColumns = true;
            this.dgrdvWire.AllowUserToResizeRows = false;
            this.dgrdvWire.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.dgrdvWire.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgrdvWire.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.txtWirePointID,
            this.txtCoordinateX,
            this.txtCoordinateY,
            this.txtCoordinateZ,
            this.txtDistanceFromTheLeft,
            this.txtDistanceFromTheRight,
            this.txtDistanceFromTop,
            this.txtDistanceFromBottom});
            this.dgrdvWire.Location = new System.Drawing.Point(11, 264);
            this.dgrdvWire.MultiSelect = false;
            this.dgrdvWire.Name = "dgrdvWire";
            this.dgrdvWire.RowTemplate.Height = 23;
            this.dgrdvWire.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgrdvWire.Size = new System.Drawing.Size(765, 219);
            this.dgrdvWire.TabIndex = 17;
            this.dgrdvWire.CellMouseDown += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.dgrdvWire_CellMouseDown);
            this.dgrdvWire.CellMouseMove += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.dgrdvWire_CellMouseDown);
            this.dgrdvWire.RowEnter += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgrdvWire_RowEnter);
            this.dgrdvWire.RowPostPaint += new System.Windows.Forms.DataGridViewRowPostPaintEventHandler(this.dgrdvWire_RowPostPaint);
            this.dgrdvWire.SelectionChanged += new System.EventHandler(this.dgrdvWire_SelectionChanged);
            this.dgrdvWire.DragDrop += new System.Windows.Forms.DragEventHandler(this.dgrdvWire_DragDrop);
            this.dgrdvWire.DragEnter += new System.Windows.Forms.DragEventHandler(this.dgrdvWire_DragEnter);
            // 
            // txtWirePointID
            // 
            this.txtWirePointID.Frozen = true;
            this.txtWirePointID.HeaderText = "导线点编号";
            this.txtWirePointID.MaxInputLength = 15;
            this.txtWirePointID.Name = "txtWirePointID";
            this.txtWirePointID.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.txtWirePointID.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.txtWirePointID.Width = 90;
            // 
            // txtCoordinateX
            // 
            this.txtCoordinateX.Frozen = true;
            this.txtCoordinateX.HeaderText = "坐标X";
            this.txtCoordinateX.Name = "txtCoordinateX";
            this.txtCoordinateX.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.txtCoordinateX.Width = 90;
            // 
            // txtCoordinateY
            // 
            this.txtCoordinateY.Frozen = true;
            this.txtCoordinateY.HeaderText = "坐标Y";
            this.txtCoordinateY.Name = "txtCoordinateY";
            this.txtCoordinateY.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.txtCoordinateY.Width = 90;
            // 
            // txtCoordinateZ
            // 
            this.txtCoordinateZ.Frozen = true;
            this.txtCoordinateZ.HeaderText = "坐标Z";
            this.txtCoordinateZ.Name = "txtCoordinateZ";
            this.txtCoordinateZ.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.txtCoordinateZ.Width = 90;
            // 
            // txtDistanceFromTheLeft
            // 
            this.txtDistanceFromTheLeft.Frozen = true;
            this.txtDistanceFromTheLeft.HeaderText = "距左帮距离";
            this.txtDistanceFromTheLeft.Name = "txtDistanceFromTheLeft";
            this.txtDistanceFromTheLeft.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.txtDistanceFromTheLeft.Width = 90;
            // 
            // txtDistanceFromTheRight
            // 
            this.txtDistanceFromTheRight.Frozen = true;
            this.txtDistanceFromTheRight.HeaderText = "距右帮距离";
            this.txtDistanceFromTheRight.Name = "txtDistanceFromTheRight";
            this.txtDistanceFromTheRight.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.txtDistanceFromTheRight.Width = 90;
            // 
            // txtDistanceFromTop
            // 
            this.txtDistanceFromTop.Frozen = true;
            this.txtDistanceFromTop.HeaderText = "距顶板距离";
            this.txtDistanceFromTop.Name = "txtDistanceFromTop";
            this.txtDistanceFromTop.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.txtDistanceFromTop.Width = 90;
            // 
            // txtDistanceFromBottom
            // 
            this.txtDistanceFromBottom.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.txtDistanceFromBottom.HeaderText = "距底板距离";
            this.txtDistanceFromBottom.Name = "txtDistanceFromBottom";
            this.txtDistanceFromBottom.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.插入ToolStripMenuItem,
            this.toolStripSeparator1,
            this.复制ToolStripMenuItem,
            this.剪切ToolStripMenuItem,
            this.粘贴ToolStripMenuItem,
            this.toolStripSeparator2,
            this.上移ToolStripMenuItem,
            this.下移ToolStripMenuItem});
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(101, 148);
            // 
            // 插入ToolStripMenuItem
            // 
            this.插入ToolStripMenuItem.Name = "插入ToolStripMenuItem";
            this.插入ToolStripMenuItem.Size = new System.Drawing.Size(100, 22);
            this.插入ToolStripMenuItem.Text = "插入";
            this.插入ToolStripMenuItem.Click += new System.EventHandler(this.插入ToolStripMenuItem_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(97, 6);
            // 
            // 复制ToolStripMenuItem
            // 
            this.复制ToolStripMenuItem.Name = "复制ToolStripMenuItem";
            this.复制ToolStripMenuItem.Size = new System.Drawing.Size(100, 22);
            this.复制ToolStripMenuItem.Text = "复制";
            this.复制ToolStripMenuItem.Click += new System.EventHandler(this.复制ToolStripMenuItem_Click);
            // 
            // 剪切ToolStripMenuItem
            // 
            this.剪切ToolStripMenuItem.Name = "剪切ToolStripMenuItem";
            this.剪切ToolStripMenuItem.Size = new System.Drawing.Size(100, 22);
            this.剪切ToolStripMenuItem.Text = "剪切";
            this.剪切ToolStripMenuItem.Click += new System.EventHandler(this.剪切ToolStripMenuItem_Click);
            // 
            // 粘贴ToolStripMenuItem
            // 
            this.粘贴ToolStripMenuItem.Name = "粘贴ToolStripMenuItem";
            this.粘贴ToolStripMenuItem.Size = new System.Drawing.Size(100, 22);
            this.粘贴ToolStripMenuItem.Text = "粘贴";
            this.粘贴ToolStripMenuItem.Click += new System.EventHandler(this.粘贴ToolStripMenuItem_Click);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(97, 6);
            // 
            // 上移ToolStripMenuItem
            // 
            this.上移ToolStripMenuItem.Name = "上移ToolStripMenuItem";
            this.上移ToolStripMenuItem.Size = new System.Drawing.Size(100, 22);
            this.上移ToolStripMenuItem.Text = "上移";
            this.上移ToolStripMenuItem.Click += new System.EventHandler(this.上移ToolStripMenuItem_Click);
            // 
            // 下移ToolStripMenuItem
            // 
            this.下移ToolStripMenuItem.Name = "下移ToolStripMenuItem";
            this.下移ToolStripMenuItem.Size = new System.Drawing.Size(100, 22);
            this.下移ToolStripMenuItem.Text = "下移";
            this.下移ToolStripMenuItem.Click += new System.EventHandler(this.下移ToolStripMenuItem_Click);
            // 
            // btnSubmit
            // 
            this.btnSubmit.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnSubmit.Location = new System.Drawing.Point(620, 489);
            this.btnSubmit.Name = "btnSubmit";
            this.btnSubmit.Size = new System.Drawing.Size(75, 23);
            this.btnSubmit.TabIndex = 24;
            this.btnSubmit.Text = "提交";
            this.btnSubmit.UseVisualStyleBackColor = true;
            this.btnSubmit.Click += new System.EventHandler(this.btnSubmit_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Location = new System.Drawing.Point(701, 489);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 23);
            this.btnCancel.TabIndex = 25;
            this.btnCancel.Text = "取消";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // lblWireLevel
            // 
            this.lblWireLevel.AutoSize = true;
            this.lblWireLevel.Location = new System.Drawing.Point(19, 233);
            this.lblWireLevel.Name = "lblWireLevel";
            this.lblWireLevel.Size = new System.Drawing.Size(65, 12);
            this.lblWireLevel.TabIndex = 3;
            this.lblWireLevel.Text = "导线级别：";
            // 
            // lblMeasureDate
            // 
            this.lblMeasureDate.AutoSize = true;
            this.lblMeasureDate.Location = new System.Drawing.Point(240, 234);
            this.lblMeasureDate.Name = "lblMeasureDate";
            this.lblMeasureDate.Size = new System.Drawing.Size(65, 12);
            this.lblMeasureDate.TabIndex = 7;
            this.lblMeasureDate.Text = "测量日期：";
            // 
            // lblVobserver
            // 
            this.lblVobserver.AutoSize = true;
            this.lblVobserver.Location = new System.Drawing.Point(240, 205);
            this.lblVobserver.Name = "lblVobserver";
            this.lblVobserver.Size = new System.Drawing.Size(65, 12);
            this.lblVobserver.TabIndex = 5;
            this.lblVobserver.Text = "观 测 者：";
            // 
            // txtWireLevel
            // 
            this.txtWireLevel.Location = new System.Drawing.Point(90, 230);
            this.txtWireLevel.Name = "txtWireLevel";
            this.txtWireLevel.Size = new System.Drawing.Size(119, 21);
            this.txtWireLevel.TabIndex = 4;
            // 
            // dtpMeasureDate
            // 
            this.dtpMeasureDate.CustomFormat = "yyyy/MM/dd";
            this.dtpMeasureDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpMeasureDate.Location = new System.Drawing.Point(311, 230);
            this.dtpMeasureDate.Name = "dtpMeasureDate";
            this.dtpMeasureDate.Size = new System.Drawing.Size(118, 21);
            this.dtpMeasureDate.TabIndex = 8;
            this.dtpMeasureDate.Value = new System.DateTime(2013, 12, 2, 9, 30, 14, 0);
            // 
            // dtpCountDate
            // 
            this.dtpCountDate.CustomFormat = "yyyy/MM/dd";
            this.dtpCountDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpCountDate.Location = new System.Drawing.Point(526, 230);
            this.dtpCountDate.Name = "dtpCountDate";
            this.dtpCountDate.Size = new System.Drawing.Size(118, 21);
            this.dtpCountDate.TabIndex = 12;
            this.dtpCountDate.Value = new System.DateTime(2013, 12, 2, 9, 30, 14, 0);
            // 
            // lblCounter
            // 
            this.lblCounter.AutoSize = true;
            this.lblCounter.Location = new System.Drawing.Point(455, 205);
            this.lblCounter.Name = "lblCounter";
            this.lblCounter.Size = new System.Drawing.Size(65, 12);
            this.lblCounter.TabIndex = 9;
            this.lblCounter.Text = "计 算 者：";
            // 
            // lblCountDate
            // 
            this.lblCountDate.AutoSize = true;
            this.lblCountDate.Location = new System.Drawing.Point(455, 234);
            this.lblCountDate.Name = "lblCountDate";
            this.lblCountDate.Size = new System.Drawing.Size(65, 12);
            this.lblCountDate.TabIndex = 11;
            this.lblCountDate.Text = "计算日期：";
            // 
            // dtpCheckDate
            // 
            this.dtpCheckDate.CustomFormat = "yyyy/MM/dd";
            this.dtpCheckDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpCheckDate.Location = new System.Drawing.Point(748, 228);
            this.dtpCheckDate.Name = "dtpCheckDate";
            this.dtpCheckDate.Size = new System.Drawing.Size(118, 21);
            this.dtpCheckDate.TabIndex = 16;
            this.dtpCheckDate.Value = new System.DateTime(2013, 12, 2, 9, 30, 14, 0);
            // 
            // lblChecker
            // 
            this.lblChecker.AutoSize = true;
            this.lblChecker.Location = new System.Drawing.Point(674, 204);
            this.lblChecker.Name = "lblChecker";
            this.lblChecker.Size = new System.Drawing.Size(65, 12);
            this.lblChecker.TabIndex = 13;
            this.lblChecker.Text = "校 核 者：";
            // 
            // lblCheckDate
            // 
            this.lblCheckDate.AutoSize = true;
            this.lblCheckDate.Location = new System.Drawing.Point(674, 232);
            this.lblCheckDate.Name = "lblCheckDate";
            this.lblCheckDate.Size = new System.Drawing.Size(65, 12);
            this.lblCheckDate.TabIndex = 15;
            this.lblCheckDate.Text = "校核日期：";
            // 
            // cboVobserver
            // 
            this.cboVobserver.FormattingEnabled = true;
            this.cboVobserver.Location = new System.Drawing.Point(311, 201);
            this.cboVobserver.Name = "cboVobserver";
            this.cboVobserver.Size = new System.Drawing.Size(120, 20);
            this.cboVobserver.TabIndex = 6;
            // 
            // cboCounter
            // 
            this.cboCounter.FormattingEnabled = true;
            this.cboCounter.Location = new System.Drawing.Point(526, 201);
            this.cboCounter.Name = "cboCounter";
            this.cboCounter.Size = new System.Drawing.Size(118, 20);
            this.cboCounter.TabIndex = 10;
            // 
            // cboChecker
            // 
            this.cboChecker.FormattingEnabled = true;
            this.cboChecker.Location = new System.Drawing.Point(748, 200);
            this.cboChecker.Name = "cboChecker";
            this.cboChecker.Size = new System.Drawing.Size(118, 20);
            this.cboChecker.TabIndex = 14;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.ForeColor = System.Drawing.Color.Red;
            this.label5.Location = new System.Drawing.Point(611, 86);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(11, 12);
            this.label5.TabIndex = 33;
            this.label5.Text = "*";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.ForeColor = System.Drawing.Color.Red;
            this.label1.Location = new System.Drawing.Point(215, 205);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(11, 12);
            this.label1.TabIndex = 32;
            this.label1.Text = "*";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.ForeColor = System.Drawing.Color.Red;
            this.label2.Location = new System.Drawing.Point(119, 267);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(11, 12);
            this.label2.TabIndex = 26;
            this.label2.Text = "*";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.ForeColor = System.Drawing.Color.Red;
            this.label3.Location = new System.Drawing.Point(192, 267);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(11, 12);
            this.label3.TabIndex = 27;
            this.label3.Text = "*";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.ForeColor = System.Drawing.Color.Red;
            this.label4.Location = new System.Drawing.Point(284, 267);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(11, 12);
            this.label4.TabIndex = 28;
            this.label4.Text = "*";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.ForeColor = System.Drawing.Color.Red;
            this.label6.Location = new System.Drawing.Point(380, 267);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(11, 12);
            this.label6.TabIndex = 29;
            this.label6.Text = "*";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.ForeColor = System.Drawing.Color.Red;
            this.label7.Location = new System.Drawing.Point(484, 267);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(11, 12);
            this.label7.TabIndex = 30;
            this.label7.Text = "*";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.ForeColor = System.Drawing.Color.Red;
            this.label8.Location = new System.Drawing.Point(575, 267);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(11, 12);
            this.label8.TabIndex = 31;
            this.label8.Text = "*";
            // 
            // btnAdd
            // 
            this.btnAdd.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnAdd.Enabled = false;
            this.btnAdd.Location = new System.Drawing.Point(799, 265);
            this.btnAdd.Name = "btnAdd";
            this.btnAdd.Size = new System.Drawing.Size(75, 23);
            this.btnAdd.TabIndex = 18;
            this.btnAdd.Text = "插入";
            this.btnAdd.UseVisualStyleBackColor = true;
            this.btnAdd.Click += new System.EventHandler(this.btnAdd_Click);
            // 
            // btnCopy
            // 
            this.btnCopy.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCopy.Enabled = false;
            this.btnCopy.Location = new System.Drawing.Point(799, 304);
            this.btnCopy.Name = "btnCopy";
            this.btnCopy.Size = new System.Drawing.Size(75, 23);
            this.btnCopy.TabIndex = 19;
            this.btnCopy.Text = "复制";
            this.btnCopy.UseVisualStyleBackColor = true;
            this.btnCopy.Click += new System.EventHandler(this.btnCopy_Click);
            // 
            // btnPaste
            // 
            this.btnPaste.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnPaste.Enabled = false;
            this.btnPaste.Location = new System.Drawing.Point(799, 343);
            this.btnPaste.Name = "btnPaste";
            this.btnPaste.Size = new System.Drawing.Size(75, 23);
            this.btnPaste.TabIndex = 20;
            this.btnPaste.Text = "粘贴";
            this.btnPaste.UseVisualStyleBackColor = true;
            this.btnPaste.Click += new System.EventHandler(this.btnPaste_Click);
            // 
            // btnMoveUp
            // 
            this.btnMoveUp.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnMoveUp.Enabled = false;
            this.btnMoveUp.Location = new System.Drawing.Point(799, 421);
            this.btnMoveUp.Name = "btnMoveUp";
            this.btnMoveUp.Size = new System.Drawing.Size(75, 23);
            this.btnMoveUp.TabIndex = 22;
            this.btnMoveUp.Text = "上移";
            this.btnMoveUp.UseVisualStyleBackColor = true;
            this.btnMoveUp.Click += new System.EventHandler(this.btnMoveUp_Click);
            // 
            // btnMoveDown
            // 
            this.btnMoveDown.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnMoveDown.Enabled = false;
            this.btnMoveDown.Location = new System.Drawing.Point(799, 460);
            this.btnMoveDown.Name = "btnMoveDown";
            this.btnMoveDown.Size = new System.Drawing.Size(75, 23);
            this.btnMoveDown.TabIndex = 23;
            this.btnMoveDown.Text = "下移";
            this.btnMoveDown.UseVisualStyleBackColor = true;
            this.btnMoveDown.Click += new System.EventHandler(this.btnMoveDown_Click);
            // 
            // btnDel
            // 
            this.btnDel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnDel.Enabled = false;
            this.btnDel.Location = new System.Drawing.Point(799, 382);
            this.btnDel.Name = "btnDel";
            this.btnDel.Size = new System.Drawing.Size(75, 23);
            this.btnDel.TabIndex = 21;
            this.btnDel.Text = "删除";
            this.btnDel.UseVisualStyleBackColor = true;
            this.btnDel.Click += new System.EventHandler(this.btnDel_Click);
            // 
            // btnTXT
            // 
            this.btnTXT.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnTXT.Location = new System.Drawing.Point(511, 489);
            this.btnTXT.Name = "btnTXT";
            this.btnTXT.Size = new System.Drawing.Size(75, 23);
            this.btnTXT.TabIndex = 34;
            this.btnTXT.Text = "读取txt";
            this.btnTXT.UseVisualStyleBackColor = true;
            this.btnTXT.Click += new System.EventHandler(this.btnTXT_Click);
            // 
            // WireInfoEntering
            // 
            this.AcceptButton = this.btnSubmit;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnCancel;
            this.ClientSize = new System.Drawing.Size(886, 524);
            this.Controls.Add(this.btnTXT);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.btnMoveDown);
            this.Controls.Add(this.btnDel);
            this.Controls.Add(this.btnPaste);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.btnMoveUp);
            this.Controls.Add(this.btnCopy);
            this.Controls.Add(this.btnAdd);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.cboCounter);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.lblChecker);
            this.Controls.Add(this.cboVobserver);
            this.Controls.Add(this.lblCounter);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.lblVobserver);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.cboChecker);
            this.Controls.Add(this.btnSubmit);
            this.Controls.Add(this.dtpCheckDate);
            this.Controls.Add(this.dgrdvWire);
            this.Controls.Add(this.lblCheckDate);
            this.Controls.Add(this.txtWireLevel);
            this.Controls.Add(this.lblCountDate);
            this.Controls.Add(this.dtpCountDate);
            this.Controls.Add(this.lblWireLevel);
            this.Controls.Add(this.dtpMeasureDate);
            this.Controls.Add(this.lblWireName);
            this.Controls.Add(this.lblMeasureDate);
            this.Controls.Add(this.txtWireName);
            this.Controls.Add(this.selectTunnelUserControl1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "WireInfoEntering";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "导线信息";
            this.Load += new System.EventHandler(this.WireInfoEntering_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgrdvWire)).EndInit();
            this.contextMenuStrip1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblWireName;
        private System.Windows.Forms.TextBox txtWireName;
        private System.Windows.Forms.DataGridView dgrdvWire;
        private System.Windows.Forms.Button btnSubmit;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Label lblWireLevel;
        private System.Windows.Forms.Label lblMeasureDate;
        private System.Windows.Forms.Label lblVobserver;
        private System.Windows.Forms.TextBox txtWireLevel;
        private System.Windows.Forms.DateTimePicker dtpMeasureDate;
        private System.Windows.Forms.DateTimePicker dtpCountDate;
        private System.Windows.Forms.Label lblCounter;
        private System.Windows.Forms.Label lblCountDate;
        private System.Windows.Forms.DateTimePicker dtpCheckDate;
        private System.Windows.Forms.Label lblChecker;
        private System.Windows.Forms.Label lblCheckDate;
        private LibCommonForm.SelectTunnelUserControl selectTunnelUserControl1;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.ToolStripMenuItem 插入ToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripMenuItem 复制ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 剪切ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 粘贴ToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripMenuItem 上移ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 下移ToolStripMenuItem;
        private System.Windows.Forms.ComboBox cboVobserver;
        private System.Windows.Forms.ComboBox cboCounter;
        private System.Windows.Forms.ComboBox cboChecker;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Button btnAdd;
        private System.Windows.Forms.Button btnCopy;
        private System.Windows.Forms.Button btnPaste;
        private System.Windows.Forms.Button btnMoveUp;
        private System.Windows.Forms.Button btnMoveDown;
        private System.Windows.Forms.Button btnDel;
        private System.Windows.Forms.DataGridViewTextBoxColumn txtWirePointID;
        private System.Windows.Forms.DataGridViewTextBoxColumn txtCoordinateX;
        private System.Windows.Forms.DataGridViewTextBoxColumn txtCoordinateY;
        private System.Windows.Forms.DataGridViewTextBoxColumn txtCoordinateZ;
        private System.Windows.Forms.DataGridViewTextBoxColumn txtDistanceFromTheLeft;
        private System.Windows.Forms.DataGridViewTextBoxColumn txtDistanceFromTheRight;
        private System.Windows.Forms.DataGridViewTextBoxColumn txtDistanceFromTop;
        private System.Windows.Forms.DataGridViewTextBoxColumn txtDistanceFromBottom;
        private System.Windows.Forms.Button btnTXT;
    }
}