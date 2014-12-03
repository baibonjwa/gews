namespace UnderTerminal
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SValueEntering));
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.btnDeleteCell = new System.Windows.Forms.ToolStripMenuItem();
            this.btnDeleteRow = new System.Windows.Forms.ToolStripMenuItem();
            this.btnSubmit = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.dgrdvSValue = new System.Windows.Forms.DataGridView();
            this.Coordinate = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.CoordinateY = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.CoordinateZ = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ValueSg = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ValueSv = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Valueq = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.BoreholeDeep = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.DryK1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.WetK1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dtpTime = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dtpTypeInTime = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.btnDel = new System.Windows.Forms.DataGridViewButtonColumn();
            this.contextMenuStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgrdvSValue)).BeginInit();
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
            this.btnSubmit.Location = new System.Drawing.Point(602, 491);
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
            this.btnCancel.Location = new System.Drawing.Point(683, 491);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 23);
            this.btnCancel.TabIndex = 3;
            this.btnCancel.Text = "取消";
            this.btnCancel.UseVisualStyleBackColor = true;
            // 
            // dgrdvSValue
            // 
            this.dgrdvSValue.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgrdvSValue.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Coordinate,
            this.CoordinateY,
            this.CoordinateZ,
            this.ValueSg,
            this.ValueSv,
            this.Valueq,
            this.BoreholeDeep,
            this.DryK1,
            this.WetK1,
            this.dtpTime,
            this.dtpTypeInTime,
            this.btnDel});
            this.dgrdvSValue.Location = new System.Drawing.Point(12, 163);
            this.dgrdvSValue.Name = "dgrdvSValue";
            this.dgrdvSValue.RowTemplate.Height = 23;
            this.dgrdvSValue.ScrollBars = System.Windows.Forms.ScrollBars.None;
            this.dgrdvSValue.Size = new System.Drawing.Size(756, 302);
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
            this.Coordinate.Width = 60;
            // 
            // CoordinateY
            // 
            this.CoordinateY.HeaderText = "拾取点Y";
            this.CoordinateY.MaxInputLength = 15;
            this.CoordinateY.Name = "CoordinateY";
            this.CoordinateY.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.CoordinateY.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.CoordinateY.Width = 50;
            // 
            // CoordinateZ
            // 
            this.CoordinateZ.HeaderText = "拾取点Z";
            this.CoordinateZ.MaxInputLength = 15;
            this.CoordinateZ.Name = "CoordinateZ";
            this.CoordinateZ.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.CoordinateZ.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.CoordinateZ.Width = 50;
            // 
            // ValueSg
            // 
            this.ValueSg.HeaderText = "Sg值";
            this.ValueSg.MaxInputLength = 10;
            this.ValueSg.Name = "ValueSg";
            this.ValueSg.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.ValueSg.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.ValueSg.Width = 50;
            // 
            // ValueSv
            // 
            this.ValueSv.HeaderText = "Sv值";
            this.ValueSv.MaxInputLength = 10;
            this.ValueSv.Name = "ValueSv";
            this.ValueSv.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.ValueSv.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.ValueSv.Width = 50;
            // 
            // Valueq
            // 
            this.Valueq.HeaderText = "q值";
            this.Valueq.MaxInputLength = 10;
            this.Valueq.Name = "Valueq";
            this.Valueq.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.Valueq.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.Valueq.Width = 50;
            // 
            // BoreholeDeep
            // 
            this.BoreholeDeep.HeaderText = "孔深";
            this.BoreholeDeep.MaxInputLength = 15;
            this.BoreholeDeep.Name = "BoreholeDeep";
            this.BoreholeDeep.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.BoreholeDeep.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.BoreholeDeep.Width = 50;
            // 
            // DryK1
            // 
            this.DryK1.FillWeight = 50F;
            this.DryK1.HeaderText = "干煤K1";
            this.DryK1.Name = "DryK1";
            this.DryK1.Width = 70;
            // 
            // WetK1
            // 
            this.WetK1.FillWeight = 50F;
            this.WetK1.HeaderText = "湿煤K1";
            this.WetK1.Name = "WetK1";
            this.WetK1.Width = 70;
            // 
            // dtpTime
            // 
            this.dtpTime.HeaderText = "记录时间";
            this.dtpTime.MaxInputLength = 20;
            this.dtpTime.Name = "dtpTime";
            this.dtpTime.ReadOnly = true;
            this.dtpTime.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.dtpTime.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // dtpTypeInTime
            // 
            this.dtpTypeInTime.HeaderText = "录入时间";
            this.dtpTypeInTime.MaxInputLength = 20;
            this.dtpTypeInTime.Name = "dtpTypeInTime";
            this.dtpTypeInTime.ReadOnly = true;
            this.dtpTypeInTime.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // btnDel
            // 
            this.btnDel.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.btnDel.HeaderText = "删除";
            this.btnDel.Name = "btnDel";
            this.btnDel.Text = "删除";
            this.btnDel.UseColumnTextForButtonValue = true;
            // 
            // SValueEntering
            // 
            this.AcceptButton = this.btnSubmit;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnCancel;
            this.ClientSize = new System.Drawing.Size(784, 561);
            this.ControlBox = false;
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnSubmit);
            this.Controls.Add(this.dgrdvSValue);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "SValueEntering";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "S值";
            this.Load += new System.EventHandler(this.SValue_Load);
            this.contextMenuStrip1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgrdvSValue)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btnSubmit;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.ToolStripMenuItem btnDeleteCell;
        private System.Windows.Forms.ToolStripMenuItem btnDeleteRow;
        private System.Windows.Forms.DataGridView dgrdvSValue;
        private System.Windows.Forms.DataGridViewTextBoxColumn Coordinate;
        private System.Windows.Forms.DataGridViewTextBoxColumn CoordinateY;
        private System.Windows.Forms.DataGridViewTextBoxColumn CoordinateZ;
        private System.Windows.Forms.DataGridViewTextBoxColumn ValueSg;
        private System.Windows.Forms.DataGridViewTextBoxColumn ValueSv;
        private System.Windows.Forms.DataGridViewTextBoxColumn Valueq;
        private System.Windows.Forms.DataGridViewTextBoxColumn BoreholeDeep;
        private System.Windows.Forms.DataGridViewTextBoxColumn DryK1;
        private System.Windows.Forms.DataGridViewTextBoxColumn WetK1;
        private System.Windows.Forms.DataGridViewTextBoxColumn dtpTime;
        private System.Windows.Forms.DataGridViewTextBoxColumn dtpTypeInTime;
        private System.Windows.Forms.DataGridViewButtonColumn btnDel;
    }
}