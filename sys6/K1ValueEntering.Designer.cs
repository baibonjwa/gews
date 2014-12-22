namespace UnderTerminal
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(K1ValueEntering));
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.btnDeleteCell = new System.Windows.Forms.ToolStripMenuItem();
            this.btnDeleteRow = new System.Windows.Forms.ToolStripMenuItem();
            this.btnSubmit = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.label7 = new System.Windows.Forms.Label();
            this.dgrdvK1Value = new System.Windows.Forms.DataGridView();
            this.label1 = new System.Windows.Forms.Label();
            this.tbCoordinateX = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.tbCoordinateY = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.tbCoordinateZ = new System.Windows.Forms.TextBox();
            this.DryK1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.WetK1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Sg = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Sv = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.q = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.boreholeDeep = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dtpTime = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dtpTypeInTime = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.btnDel = new System.Windows.Forms.DataGridViewButtonColumn();
            this.contextMenuStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgrdvK1Value)).BeginInit();
            this.SuspendLayout();
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.btnDeleteCell,
            this.btnDeleteRow});
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(131, 48);
            // 
            // btnDeleteCell
            // 
            this.btnDeleteCell.Name = "btnDeleteCell";
            this.btnDeleteCell.Size = new System.Drawing.Size(130, 22);
            this.btnDeleteCell.Text = "删除单元格";
            this.btnDeleteCell.Click += new System.EventHandler(this.btnDeleteCell_Click);
            // 
            // btnDeleteRow
            // 
            this.btnDeleteRow.Name = "btnDeleteRow";
            this.btnDeleteRow.Size = new System.Drawing.Size(130, 22);
            this.btnDeleteRow.Text = "删除行";
            this.btnDeleteRow.Click += new System.EventHandler(this.btnDeleteRow_Click);
            // 
            // btnSubmit
            // 
            this.btnSubmit.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnSubmit.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.btnSubmit.Location = new System.Drawing.Point(628, 474);
            this.btnSubmit.Name = "btnSubmit";
            this.btnSubmit.Size = new System.Drawing.Size(75, 23);
            this.btnSubmit.TabIndex = 2;
            this.btnSubmit.Text = "提交";
            this.btnSubmit.UseVisualStyleBackColor = true;
            this.btnSubmit.Click += new System.EventHandler(this.btnSubmit_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Location = new System.Drawing.Point(709, 474);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 23);
            this.btnCancel.TabIndex = 3;
            this.btnCancel.Text = "取消";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.ForeColor = System.Drawing.Color.Red;
            this.label7.Location = new System.Drawing.Point(476, 146);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(11, 12);
            this.label7.TabIndex = 10;
            this.label7.Text = "*";
            // 
            // dgrdvK1Value
            // 
            this.dgrdvK1Value.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.dgrdvK1Value.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgrdvK1Value.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.DryK1,
            this.WetK1,
            this.Sg,
            this.Sv,
            this.q,
            this.boreholeDeep,
            this.dtpTime,
            this.dtpTypeInTime,
            this.btnDel});
            this.dgrdvK1Value.Location = new System.Drawing.Point(14, 123);
            this.dgrdvK1Value.MultiSelect = false;
            this.dgrdvK1Value.Name = "dgrdvK1Value";
            this.dgrdvK1Value.RowTemplate.Height = 23;
            this.dgrdvK1Value.ScrollBars = System.Windows.Forms.ScrollBars.None;
            this.dgrdvK1Value.Size = new System.Drawing.Size(770, 343);
            this.dgrdvK1Value.TabIndex = 1;
            this.dgrdvK1Value.CellBeginEdit += new System.Windows.Forms.DataGridViewCellCancelEventHandler(this.dgrdvK1Value_CellBeginEdit);
            this.dgrdvK1Value.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgrdvK1Value_CellContentClick);
            this.dgrdvK1Value.CellEnter += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgrdvK1Value_CellEnter);
            this.dgrdvK1Value.CellLeave += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgrdvK1Value_CellLeave);
            this.dgrdvK1Value.CellMouseDown += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.dgrdvK1Value_CellMouseDown);
            this.dgrdvK1Value.RowEnter += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgrdvK1Value_RowEnter);
            this.dgrdvK1Value.RowsAdded += new System.Windows.Forms.DataGridViewRowsAddedEventHandler(this.dgrdvK1Value_RowsAdded);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(36, 92);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(47, 12);
            this.label1.TabIndex = 11;
            this.label1.Text = "拾取点X";
            // 
            // tbCoordinateX
            // 
            this.tbCoordinateX.Location = new System.Drawing.Point(89, 89);
            this.tbCoordinateX.Name = "tbCoordinateX";
            this.tbCoordinateX.Size = new System.Drawing.Size(100, 21);
            this.tbCoordinateX.TabIndex = 12;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(203, 92);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(47, 12);
            this.label2.TabIndex = 11;
            this.label2.Text = "拾取点Y";
            // 
            // tbCoordinateY
            // 
            this.tbCoordinateY.Location = new System.Drawing.Point(256, 89);
            this.tbCoordinateY.Name = "tbCoordinateY";
            this.tbCoordinateY.Size = new System.Drawing.Size(100, 21);
            this.tbCoordinateY.TabIndex = 12;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(372, 92);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(47, 12);
            this.label3.TabIndex = 11;
            this.label3.Text = "拾取点Z";
            // 
            // tbCoordinateZ
            // 
            this.tbCoordinateZ.Location = new System.Drawing.Point(425, 89);
            this.tbCoordinateZ.Name = "tbCoordinateZ";
            this.tbCoordinateZ.Size = new System.Drawing.Size(100, 21);
            this.tbCoordinateZ.TabIndex = 12;
            // 
            // DryK1
            // 
            this.DryK1.Frozen = true;
            this.DryK1.HeaderText = "干煤K1值";
            this.DryK1.MaxInputLength = 15;
            this.DryK1.Name = "DryK1";
            this.DryK1.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.DryK1.Width = 70;
            // 
            // WetK1
            // 
            this.WetK1.Frozen = true;
            this.WetK1.HeaderText = "湿煤K1值";
            this.WetK1.MaxInputLength = 15;
            this.WetK1.Name = "WetK1";
            this.WetK1.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.WetK1.Width = 70;
            // 
            // Sg
            // 
            this.Sg.Frozen = true;
            this.Sg.HeaderText = "Sg值";
            this.Sg.MaxInputLength = 15;
            this.Sg.Name = "Sg";
            this.Sg.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.Sg.Width = 70;
            // 
            // Sv
            // 
            this.Sv.Frozen = true;
            this.Sv.HeaderText = "Sv值";
            this.Sv.MaxInputLength = 15;
            this.Sv.Name = "Sv";
            this.Sv.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.Sv.Width = 80;
            // 
            // q
            // 
            this.q.Frozen = true;
            this.q.HeaderText = "q值";
            this.q.MaxInputLength = 15;
            this.q.Name = "q";
            this.q.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.q.Width = 80;
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
            this.dtpTime.Width = 125;
            // 
            // dtpTypeInTime
            // 
            this.dtpTypeInTime.HeaderText = "录入时间";
            this.dtpTypeInTime.MaxInputLength = 20;
            this.dtpTypeInTime.Name = "dtpTypeInTime";
            this.dtpTypeInTime.ReadOnly = true;
            this.dtpTypeInTime.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.dtpTypeInTime.Width = 125;
            // 
            // btnDel
            // 
            this.btnDel.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.btnDel.HeaderText = "删除";
            this.btnDel.Name = "btnDel";
            this.btnDel.Text = "删除";
            this.btnDel.UseColumnTextForButtonValue = true;
            // 
            // K1ValueEntering
            // 
            this.AcceptButton = this.btnSubmit;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnCancel;
            this.ClientSize = new System.Drawing.Size(794, 571);
            this.ControlBox = false;
            this.Controls.Add(this.tbCoordinateZ);
            this.Controls.Add(this.tbCoordinateY);
            this.Controls.Add(this.tbCoordinateX);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnSubmit);
            this.Controls.Add(this.dgrdvK1Value);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "K1ValueEntering";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "K1值";
            this.Load += new System.EventHandler(this.K1Value_Load);
            this.contextMenuStrip1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgrdvK1Value)).EndInit();
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
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox tbCoordinateX;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox tbCoordinateY;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox tbCoordinateZ;
        private System.Windows.Forms.DataGridViewTextBoxColumn DryK1;
        private System.Windows.Forms.DataGridViewTextBoxColumn WetK1;
        private System.Windows.Forms.DataGridViewTextBoxColumn Sg;
        private System.Windows.Forms.DataGridViewTextBoxColumn Sv;
        private System.Windows.Forms.DataGridViewTextBoxColumn q;
        private System.Windows.Forms.DataGridViewTextBoxColumn boreholeDeep;
        private System.Windows.Forms.DataGridViewTextBoxColumn dtpTime;
        private System.Windows.Forms.DataGridViewTextBoxColumn dtpTypeInTime;
        private System.Windows.Forms.DataGridViewButtonColumn btnDel;
    }
}