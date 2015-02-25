namespace _3.GeologyMeasure
{
    partial class TunnelJZEntering
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
            this.dgrdvWire = new System.Windows.Forms.DataGridView();
            this.txtWirePointID = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.txtCoordinateX = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.txtCoordinateY = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.txtCoordinateZ = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.txtDistanceFromTheLeft = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.txtDistanceFromTheRight = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.txtDistanceFromTop = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.txtDistanceFromBottom = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.btnCancel = new System.Windows.Forms.Button();
            this.btnOK = new System.Windows.Forms.Button();
            this.label5 = new System.Windows.Forms.Label();
            this.dtpDate = new System.Windows.Forms.DateTimePicker();
            this.label3 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.selectTunnelUserControl1 = new LibCommonForm.SelectTunnelUserControl();
            ((System.ComponentModel.ISupportInitialize)(this.dgrdvWire)).BeginInit();
            this.SuspendLayout();
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
            this.dgrdvWire.Location = new System.Drawing.Point(12, 235);
            this.dgrdvWire.MultiSelect = false;
            this.dgrdvWire.Name = "dgrdvWire";
            this.dgrdvWire.RowTemplate.Height = 23;
            this.dgrdvWire.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgrdvWire.Size = new System.Drawing.Size(746, 149);
            this.dgrdvWire.TabIndex = 18;
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
            // btnCancel
            // 
            this.btnCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Location = new System.Drawing.Point(683, 406);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 23);
            this.btnCancel.TabIndex = 35;
            this.btnCancel.Text = "取消";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // btnOK
            // 
            this.btnOK.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnOK.Location = new System.Drawing.Point(602, 406);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(75, 23);
            this.btnOK.TabIndex = 34;
            this.btnOK.Text = "确定";
            this.btnOK.UseVisualStyleBackColor = true;
            this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.ForeColor = System.Drawing.Color.Red;
            this.label5.Location = new System.Drawing.Point(601, 125);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(11, 12);
            this.label5.TabIndex = 39;
            this.label5.Text = "*";
            // 
            // dtpDate
            // 
            this.dtpDate.CustomFormat = "yyyy-MM-dd";
            this.dtpDate.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpDate.Location = new System.Drawing.Point(71, 17);
            this.dtpDate.Name = "dtpDate";
            this.dtpDate.Size = new System.Drawing.Size(135, 21);
            this.dtpDate.TabIndex = 41;
            this.dtpDate.Value = new System.DateTime(2013, 12, 11, 13, 58, 43, 0);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(12, 21);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(65, 12);
            this.label3.TabIndex = 40;
            this.label3.Text = "校正日期：";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.ForeColor = System.Drawing.Color.Red;
            this.label1.Location = new System.Drawing.Point(212, 21);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(11, 12);
            this.label1.TabIndex = 42;
            this.label1.Text = "*";
            // 
            // selectTunnelUserControl1
            // 
            this.selectTunnelUserControl1.Location = new System.Drawing.Point(12, 49);
            this.selectTunnelUserControl1.MainForm = null;
            this.selectTunnelUserControl1.Name = "selectTunnelUserControl1";
            this.selectTunnelUserControl1.Size = new System.Drawing.Size(583, 187);
            this.selectTunnelUserControl1.TabIndex = 36;
            this.selectTunnelUserControl1.Load += new System.EventHandler(this.selectTunnelUserControl1_Load);
            // 
            // TunnelJZEntering
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(770, 441);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.dtpDate);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.selectTunnelUserControl1);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnOK);
            this.Controls.Add(this.dgrdvWire);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "TunnelJZEntering";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "掘进巷道校正";
            ((System.ComponentModel.ISupportInitialize)(this.dgrdvWire)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView dgrdvWire;
        private System.Windows.Forms.DataGridViewTextBoxColumn txtWirePointID;
        private System.Windows.Forms.DataGridViewTextBoxColumn txtCoordinateX;
        private System.Windows.Forms.DataGridViewTextBoxColumn txtCoordinateY;
        private System.Windows.Forms.DataGridViewTextBoxColumn txtCoordinateZ;
        private System.Windows.Forms.DataGridViewTextBoxColumn txtDistanceFromTheLeft;
        private System.Windows.Forms.DataGridViewTextBoxColumn txtDistanceFromTheRight;
        private System.Windows.Forms.DataGridViewTextBoxColumn txtDistanceFromTop;
        private System.Windows.Forms.DataGridViewTextBoxColumn txtDistanceFromBottom;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Button btnOK;
        private LibCommonForm.SelectTunnelUserControl selectTunnelUserControl1;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.DateTimePicker dtpDate;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label1;
    }
}