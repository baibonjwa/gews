namespace LibPanels
{
    partial class K1ValueEntering
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
            this.btnCancel = new System.Windows.Forms.Button();
            this.btnSubmit = new System.Windows.Forms.Button();
            this.dgrdvK1Value = new System.Windows.Forms.DataGridView();
            this.valueK1Dry = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.valueK1Wet = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Sg = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Sv = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Q = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.boreholeDeep = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dtpTime = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dtpTypeInTime = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.btnDel = new System.Windows.Forms.DataGridViewButtonColumn();
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip();
            this.btnDeleteCell = new System.Windows.Forms.ToolStripMenuItem();
            this.btnDeleteRow = new System.Windows.Forms.ToolStripMenuItem();
            this.label7 = new System.Windows.Forms.Label();
            this.selectTunnelSimple1 = new LibCommonForm.SelectTunnelSimple();
            ((System.ComponentModel.ISupportInitialize)(this.dgrdvK1Value)).BeginInit();
            this.contextMenuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnCancel
            // 
            this.btnCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Location = new System.Drawing.Point(781, 338);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 23);
            this.btnCancel.TabIndex = 3;
            this.btnCancel.Text = "取消";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // btnSubmit
            // 
            this.btnSubmit.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnSubmit.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.btnSubmit.Location = new System.Drawing.Point(700, 338);
            this.btnSubmit.Name = "btnSubmit";
            this.btnSubmit.Size = new System.Drawing.Size(75, 23);
            this.btnSubmit.TabIndex = 2;
            this.btnSubmit.Text = "提交";
            this.btnSubmit.UseVisualStyleBackColor = true;
            this.btnSubmit.Click += new System.EventHandler(this.btnSubmit_Click);
            // 
            // dgrdvK1Value
            // 
            this.dgrdvK1Value.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dgrdvK1Value.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgrdvK1Value.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.valueK1Dry,
            this.valueK1Wet,
            this.Sg,
            this.Sv,
            this.Q,
            this.boreholeDeep,
            this.dtpTime,
            this.dtpTypeInTime,
            this.btnDel});
            this.dgrdvK1Value.Location = new System.Drawing.Point(14, 56);
            this.dgrdvK1Value.MultiSelect = false;
            this.dgrdvK1Value.Name = "dgrdvK1Value";
            this.dgrdvK1Value.RowTemplate.Height = 23;
            this.dgrdvK1Value.ScrollBars = System.Windows.Forms.ScrollBars.None;
            this.dgrdvK1Value.Size = new System.Drawing.Size(842, 274);
            this.dgrdvK1Value.TabIndex = 1;
            this.dgrdvK1Value.CellBeginEdit += new System.Windows.Forms.DataGridViewCellCancelEventHandler(this.dgrdvK1Value_CellBeginEdit);
            this.dgrdvK1Value.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgrdvK1Value_CellContentClick);
            this.dgrdvK1Value.CellEnter += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgrdvK1Value_CellEnter);
            this.dgrdvK1Value.CellLeave += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgrdvK1Value_CellLeave);
            this.dgrdvK1Value.CellMouseDown += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.dgrdvK1Value_CellMouseDown);
            this.dgrdvK1Value.RowEnter += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgrdvK1Value_RowEnter);
            this.dgrdvK1Value.RowsAdded += new System.Windows.Forms.DataGridViewRowsAddedEventHandler(this.dgrdvK1Value_RowsAdded);
            // 
            // valueK1Dry
            // 
            this.valueK1Dry.Frozen = true;
            this.valueK1Dry.HeaderText = "干煤K1值";
            this.valueK1Dry.MaxInputLength = 15;
            this.valueK1Dry.Name = "valueK1Dry";
            this.valueK1Dry.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.valueK1Dry.Width = 80;
            // 
            // valueK1Wet
            // 
            this.valueK1Wet.Frozen = true;
            this.valueK1Wet.HeaderText = "湿煤K1值";
            this.valueK1Wet.MaxInputLength = 15;
            this.valueK1Wet.Name = "valueK1Wet";
            this.valueK1Wet.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.valueK1Wet.Width = 80;
            // 
            // Sg
            // 
            this.Sg.HeaderText = "Sg值";
            this.Sg.Name = "Sg";
            // 
            // Sv
            // 
            this.Sv.HeaderText = "Sv值";
            this.Sv.Name = "Sv";
            // 
            // Q
            // 
            this.Q.HeaderText = "q值";
            this.Q.Name = "Q";
            // 
            // boreholeDeep
            // 
            this.boreholeDeep.HeaderText = "孔深";
            this.boreholeDeep.MaxInputLength = 15;
            this.boreholeDeep.Name = "boreholeDeep";
            this.boreholeDeep.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.boreholeDeep.Width = 70;
            // 
            // dtpTime
            // 
            this.dtpTime.HeaderText = "记录时间";
            this.dtpTime.MaxInputLength = 20;
            this.dtpTime.Name = "dtpTime";
            this.dtpTime.ReadOnly = true;
            this.dtpTime.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.dtpTime.Width = 140;
            // 
            // dtpTypeInTime
            // 
            this.dtpTypeInTime.HeaderText = "录入时间";
            this.dtpTypeInTime.MaxInputLength = 20;
            this.dtpTypeInTime.Name = "dtpTypeInTime";
            this.dtpTypeInTime.ReadOnly = true;
            this.dtpTypeInTime.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.dtpTypeInTime.Width = 140;
            // 
            // btnDel
            // 
            this.btnDel.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.btnDel.HeaderText = "删除";
            this.btnDel.Name = "btnDel";
            this.btnDel.Text = "删除";
            this.btnDel.UseColumnTextForButtonValue = true;
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.btnDeleteCell,
            this.btnDeleteRow});
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(137, 48);
            // 
            // btnDeleteCell
            // 
            this.btnDeleteCell.Name = "btnDeleteCell";
            this.btnDeleteCell.Size = new System.Drawing.Size(136, 22);
            this.btnDeleteCell.Text = "删除单元格";
            this.btnDeleteCell.Click += new System.EventHandler(this.btnDeleteCell_Click);
            // 
            // btnDeleteRow
            // 
            this.btnDeleteRow.Name = "btnDeleteRow";
            this.btnDeleteRow.Size = new System.Drawing.Size(136, 22);
            this.btnDeleteRow.Text = "删除行";
            this.btnDeleteRow.Click += new System.EventHandler(this.btnDeleteRow_Click);
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.ForeColor = System.Drawing.Color.Red;
            this.label7.Location = new System.Drawing.Point(570, 91);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(11, 12);
            this.label7.TabIndex = 10;
            this.label7.Text = "*";
            // 
            // selectTunnelSimple1
            // 
            this.selectTunnelSimple1.Location = new System.Drawing.Point(14, 12);
            this.selectTunnelSimple1.Name = "selectTunnelSimple1";
            this.selectTunnelSimple1.SelectedTunnel = null;
            this.selectTunnelSimple1.Size = new System.Drawing.Size(219, 38);
            this.selectTunnelSimple1.TabIndex = 19;
            // 
            // K1ValueEntering
            // 
            this.AcceptButton = this.btnSubmit;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnCancel;
            this.ClientSize = new System.Drawing.Size(866, 371);
            this.Controls.Add(this.selectTunnelSimple1);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnSubmit);
            this.Controls.Add(this.dgrdvK1Value);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "K1ValueEntering";
            this.Text = "K1值";
            this.Load += new System.EventHandler(this.K1Value_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgrdvK1Value)).EndInit();
            this.contextMenuStrip1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Button btnSubmit;
        private System.Windows.Forms.DataGridView dgrdvK1Value;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.ToolStripMenuItem btnDeleteCell;
        private System.Windows.Forms.ToolStripMenuItem btnDeleteRow;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.DataGridViewTextBoxColumn valueK1Dry;
        private System.Windows.Forms.DataGridViewTextBoxColumn valueK1Wet;
        private System.Windows.Forms.DataGridViewTextBoxColumn Sg;
        private System.Windows.Forms.DataGridViewTextBoxColumn Sv;
        private System.Windows.Forms.DataGridViewTextBoxColumn Q;
        private System.Windows.Forms.DataGridViewTextBoxColumn boreholeDeep;
        private System.Windows.Forms.DataGridViewTextBoxColumn dtpTime;
        private System.Windows.Forms.DataGridViewTextBoxColumn dtpTypeInTime;
        private System.Windows.Forms.DataGridViewButtonColumn btnDel;
        private LibCommonForm.SelectTunnelSimple selectTunnelSimple1;
    }
}