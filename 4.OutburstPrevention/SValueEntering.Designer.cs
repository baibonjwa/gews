namespace _4.OutburstPrevention
{
    partial class SValueEntering
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
            this.dgrdvSValue = new System.Windows.Forms.DataGridView();
            this.Coordinate = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.CoordinateY = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.CoordinateZ = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ValueSg = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ValueSv = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Valueq = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.BoreholeDeep = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dtpTime = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dtpTypeInTime = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.btnDel = new System.Windows.Forms.DataGridViewButtonColumn();
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.btnDeleteCell = new System.Windows.Forms.ToolStripMenuItem();
            this.btnDeleteRow = new System.Windows.Forms.ToolStripMenuItem();
            this.btnSubmit = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.selectTunnelUserControl1 = new LibCommonForm.SelectTunnelUserControl();
            this.label8 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.dgrdvSValue)).BeginInit();
            this.contextMenuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // dgrdvSValue
            // 
            this.dgrdvSValue.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.dgrdvSValue.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgrdvSValue.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Coordinate,
            this.CoordinateY,
            this.CoordinateZ,
            this.ValueSg,
            this.ValueSv,
            this.Valueq,
            this.BoreholeDeep,
            this.dtpTime,
            this.dtpTypeInTime,
            this.btnDel});
            this.dgrdvSValue.Location = new System.Drawing.Point(12, 197);
            this.dgrdvSValue.Name = "dgrdvSValue";
            this.dgrdvSValue.RowTemplate.Height = 23;
            this.dgrdvSValue.ScrollBars = System.Windows.Forms.ScrollBars.None;
            this.dgrdvSValue.Size = new System.Drawing.Size(879, 209);
            this.dgrdvSValue.TabIndex = 1;
            this.dgrdvSValue.CellBeginEdit += new System.Windows.Forms.DataGridViewCellCancelEventHandler(this.dgrdvSValue_CellBeginEdit);
            this.dgrdvSValue.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgrdvSValue_CellContentClick);
            this.dgrdvSValue.CellEnter += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgrdvSValue_CellEnter);
            this.dgrdvSValue.CellLeave += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgrdvSValue_CellLeave);
            this.dgrdvSValue.CellMouseDown += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.dgrdvSValue_CellMouseDown);
            this.dgrdvSValue.RowEnter += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgrdvSValue_RowEnter);
            this.dgrdvSValue.RowsAdded += new System.Windows.Forms.DataGridViewRowsAddedEventHandler(this.dgrdvSValue_RowsAdded);
            // 
            // Coordinate
            // 
            this.Coordinate.HeaderText = "拾取点X";
            this.Coordinate.MaxInputLength = 15;
            this.Coordinate.Name = "Coordinate";
            this.Coordinate.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.Coordinate.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.Coordinate.Width = 70;
            // 
            // CoordinateY
            // 
            this.CoordinateY.HeaderText = "拾取点Y";
            this.CoordinateY.MaxInputLength = 15;
            this.CoordinateY.Name = "CoordinateY";
            this.CoordinateY.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.CoordinateY.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.CoordinateY.Width = 70;
            // 
            // CoordinateZ
            // 
            this.CoordinateZ.HeaderText = "拾取点Z";
            this.CoordinateZ.MaxInputLength = 15;
            this.CoordinateZ.Name = "CoordinateZ";
            this.CoordinateZ.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.CoordinateZ.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.CoordinateZ.Width = 70;
            // 
            // ValueSg
            // 
            this.ValueSg.HeaderText = "Sg值";
            this.ValueSg.MaxInputLength = 15;
            this.ValueSg.Name = "ValueSg";
            this.ValueSg.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.ValueSg.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.ValueSg.Width = 70;
            // 
            // ValueSv
            // 
            this.ValueSv.HeaderText = "Sv值";
            this.ValueSv.MaxInputLength = 15;
            this.ValueSv.Name = "ValueSv";
            this.ValueSv.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.ValueSv.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.ValueSv.Width = 70;
            // 
            // Valueq
            // 
            this.Valueq.HeaderText = "q值";
            this.Valueq.MaxInputLength = 15;
            this.Valueq.Name = "Valueq";
            this.Valueq.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.Valueq.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.Valueq.Width = 70;
            // 
            // BoreholeDeep
            // 
            this.BoreholeDeep.HeaderText = "孔深";
            this.BoreholeDeep.MaxInputLength = 15;
            this.BoreholeDeep.Name = "BoreholeDeep";
            this.BoreholeDeep.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.BoreholeDeep.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.BoreholeDeep.Width = 70;
            // 
            // dtpTime
            // 
            this.dtpTime.HeaderText = "记录时间";
            this.dtpTime.MaxInputLength = 20;
            this.dtpTime.Name = "dtpTime";
            this.dtpTime.ReadOnly = true;
            this.dtpTime.Resizable = System.Windows.Forms.DataGridViewTriState.False;
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
            // btnSubmit
            // 
            this.btnSubmit.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnSubmit.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.btnSubmit.Location = new System.Drawing.Point(735, 408);
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
            this.btnCancel.Location = new System.Drawing.Point(816, 408);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 23);
            this.btnCancel.TabIndex = 3;
            this.btnCancel.Text = "取消";
            this.btnCancel.UseVisualStyleBackColor = true;
            // 
            // selectTunnelUserControl1
            // 
            this.selectTunnelUserControl1.ITunnelId = 0;
            this.selectTunnelUserControl1.Location = new System.Drawing.Point(12, 3);
            this.selectTunnelUserControl1.Name = "selectTunnelUserControl1";
            this.selectTunnelUserControl1.Size = new System.Drawing.Size(584, 188);
            this.selectTunnelUserControl1.STunnelName = null;
            this.selectTunnelUserControl1.TabIndex = 0;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.ForeColor = System.Drawing.Color.Red;
            this.label8.Location = new System.Drawing.Point(602, 202);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(11, 12);
            this.label8.TabIndex = 11;
            this.label8.Text = "*";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.ForeColor = System.Drawing.Color.Red;
            this.label7.Location = new System.Drawing.Point(509, 202);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(11, 12);
            this.label7.TabIndex = 10;
            this.label7.Text = "*";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.ForeColor = System.Drawing.Color.Red;
            this.label2.Location = new System.Drawing.Point(247, 202);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(11, 12);
            this.label2.TabIndex = 6;
            this.label2.Text = "*";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.ForeColor = System.Drawing.Color.Red;
            this.label1.Location = new System.Drawing.Point(178, 202);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(11, 12);
            this.label1.TabIndex = 5;
            this.label1.Text = "*";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.ForeColor = System.Drawing.Color.Red;
            this.label6.Location = new System.Drawing.Point(106, 202);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(11, 12);
            this.label6.TabIndex = 4;
            this.label6.Text = "*";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.ForeColor = System.Drawing.Color.Red;
            this.label9.Location = new System.Drawing.Point(742, 202);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(11, 12);
            this.label9.TabIndex = 12;
            this.label9.Text = "*";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.ForeColor = System.Drawing.Color.Red;
            this.label3.Location = new System.Drawing.Point(296, 201);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(23, 12);
            this.label3.TabIndex = 13;
            this.label3.Text = "(*)";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.ForeColor = System.Drawing.Color.Red;
            this.label4.Location = new System.Drawing.Point(365, 201);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(23, 12);
            this.label4.TabIndex = 14;
            this.label4.Text = "(*)";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.ForeColor = System.Drawing.Color.Red;
            this.label5.Location = new System.Drawing.Point(433, 201);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(23, 12);
            this.label5.TabIndex = 15;
            this.label5.Text = "(*)";
            // 
            // SValueEntering
            // 
            this.AcceptButton = this.btnSubmit;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnCancel;
            this.ClientSize = new System.Drawing.Size(903, 442);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnSubmit);
            this.Controls.Add(this.selectTunnelUserControl1);
            this.Controls.Add(this.dgrdvSValue);
            this.Name = "SValueEntering";
            this.Text = "SValue";
            this.Load += new System.EventHandler(this.SValue_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgrdvSValue)).EndInit();
            this.contextMenuStrip1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView dgrdvSValue;
        private System.Windows.Forms.Button btnSubmit;
        private System.Windows.Forms.Button btnCancel;
        private LibCommonForm.SelectTunnelUserControl selectTunnelUserControl1;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.ToolStripMenuItem btnDeleteCell;
        private System.Windows.Forms.ToolStripMenuItem btnDeleteRow;
        private System.Windows.Forms.DataGridViewTextBoxColumn Coordinate;
        private System.Windows.Forms.DataGridViewTextBoxColumn CoordinateY;
        private System.Windows.Forms.DataGridViewTextBoxColumn CoordinateZ;
        private System.Windows.Forms.DataGridViewTextBoxColumn ValueSg;
        private System.Windows.Forms.DataGridViewTextBoxColumn ValueSv;
        private System.Windows.Forms.DataGridViewTextBoxColumn Valueq;
        private System.Windows.Forms.DataGridViewTextBoxColumn BoreholeDeep;
        private System.Windows.Forms.DataGridViewTextBoxColumn dtpTime;
        private System.Windows.Forms.DataGridViewTextBoxColumn dtpTypeInTime;
        private System.Windows.Forms.DataGridViewButtonColumn btnDel;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
    }
}