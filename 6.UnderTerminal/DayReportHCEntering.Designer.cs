namespace UnderTerminal
{
    partial class DayReportHCEntering
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(DayReportHCEntering));
            this.dgrdvDayReportHC = new System.Windows.Forms.DataGridView();
            this.cboWorkTime = new System.Windows.Forms.DataGridViewComboBoxColumn();
            this.cboWorkInfo = new System.Windows.Forms.DataGridViewComboBoxColumn();
            this.Column2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column4 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.rbtn46 = new System.Windows.Forms.RadioButton();
            this.rbtn38 = new System.Windows.Forms.RadioButton();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.btnSubmit = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.dtpDate = new System.Windows.Forms.DateTimePicker();
            this.label1 = new System.Windows.Forms.Label();
            this.cboSubmitter = new System.Windows.Forms.ComboBox();
            this.label8 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.cboTeamName = new System.Windows.Forms.ComboBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            ((System.ComponentModel.ISupportInitialize)(this.dgrdvDayReportHC)).BeginInit();
            this.groupBox2.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // dgrdvDayReportHC
            // 
            this.dgrdvDayReportHC.AllowUserToResizeColumns = false;
            this.dgrdvDayReportHC.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.dgrdvDayReportHC.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgrdvDayReportHC.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.cboWorkTime,
            this.cboWorkInfo,
            this.Column2,
            this.Column4});
            this.dgrdvDayReportHC.Location = new System.Drawing.Point(57, 146);
            this.dgrdvDayReportHC.MultiSelect = false;
            this.dgrdvDayReportHC.Name = "dgrdvDayReportHC";
            this.dgrdvDayReportHC.RowTemplate.Height = 23;
            this.dgrdvDayReportHC.Size = new System.Drawing.Size(628, 169);
            this.dgrdvDayReportHC.TabIndex = 10;
            this.dgrdvDayReportHC.CellEndEdit += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgrdvDayReportHC_CellEndEdit);
            this.dgrdvDayReportHC.CellLeave += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgrdvDayReportHC_CellLeave);
            this.dgrdvDayReportHC.RowEnter += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgrdvDayReportHC_RowEnter);
            this.dgrdvDayReportHC.RowLeave += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgrdvDayReportHC_RowLeave);
            // 
            // cboWorkTime
            // 
            this.cboWorkTime.HeaderText = "班次";
            this.cboWorkTime.Name = "cboWorkTime";
            this.cboWorkTime.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            // 
            // cboWorkInfo
            // 
            this.cboWorkInfo.HeaderText = "工作内容";
            this.cboWorkInfo.Items.AddRange(new object[] {
            "回采",
            "支护",
            "停工",
            "打钻",
            "出煤出矸",
            "其他"});
            this.cboWorkInfo.Name = "cboWorkInfo";
            this.cboWorkInfo.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            // 
            // Column2
            // 
            this.Column2.HeaderText = "进尺";
            this.Column2.MaxInputLength = 18;
            this.Column2.Name = "Column2";
            this.Column2.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.Column2.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // Column4
            // 
            this.Column4.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.Column4.HeaderText = "备注";
            this.Column4.MaxInputLength = 50;
            this.Column4.Name = "Column4";
            this.Column4.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.Column4.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // rbtn46
            // 
            this.rbtn46.AutoSize = true;
            this.rbtn46.Location = new System.Drawing.Point(148, 17);
            this.rbtn46.Name = "rbtn46";
            this.rbtn46.Size = new System.Drawing.Size(59, 16);
            this.rbtn46.TabIndex = 1;
            this.rbtn46.Text = "四六制";
            this.rbtn46.UseVisualStyleBackColor = true;
            // 
            // rbtn38
            // 
            this.rbtn38.AutoSize = true;
            this.rbtn38.Checked = true;
            this.rbtn38.Location = new System.Drawing.Point(41, 17);
            this.rbtn38.Name = "rbtn38";
            this.rbtn38.Size = new System.Drawing.Size(59, 16);
            this.rbtn38.TabIndex = 0;
            this.rbtn38.TabStop = true;
            this.rbtn38.Text = "三八制";
            this.rbtn38.UseVisualStyleBackColor = true;
            this.rbtn38.CheckedChanged += new System.EventHandler(this.rbtn38_CheckedChanged);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.rbtn46);
            this.groupBox2.Controls.Add(this.rbtn38);
            this.groupBox2.Location = new System.Drawing.Point(57, 52);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(248, 45);
            this.groupBox2.TabIndex = 1;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "制式";
            // 
            // btnSubmit
            // 
            this.btnSubmit.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnSubmit.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.btnSubmit.Location = new System.Drawing.Point(531, 338);
            this.btnSubmit.Name = "btnSubmit";
            this.btnSubmit.Size = new System.Drawing.Size(75, 23);
            this.btnSubmit.TabIndex = 11;
            this.btnSubmit.Text = "提交";
            this.btnSubmit.UseVisualStyleBackColor = true;
            this.btnSubmit.Click += new System.EventHandler(this.btnSubmit_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Location = new System.Drawing.Point(612, 338);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 23);
            this.btnCancel.TabIndex = 12;
            this.btnCancel.Text = "取消";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // dtpDate
            // 
            this.dtpDate.CustomFormat = "yyyy-MM-dd";
            this.dtpDate.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpDate.Location = new System.Drawing.Point(116, 111);
            this.dtpDate.Name = "dtpDate";
            this.dtpDate.Size = new System.Drawing.Size(189, 21);
            this.dtpDate.TabIndex = 3;
            this.dtpDate.Value = new System.DateTime(2013, 12, 11, 0, 0, 0, 0);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(14, 17);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(53, 12);
            this.label1.TabIndex = 4;
            this.label1.Text = "队  别：";
            // 
            // cboSubmitter
            // 
            this.cboSubmitter.FormattingEnabled = true;
            this.cboSubmitter.Location = new System.Drawing.Point(73, 52);
            this.cboSubmitter.MaxLength = 15;
            this.cboSubmitter.Name = "cboSubmitter";
            this.cboSubmitter.Size = new System.Drawing.Size(101, 20);
            this.cboSubmitter.TabIndex = 8;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(14, 55);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(53, 12);
            this.label8.TabIndex = 7;
            this.label8.Text = "填报人：";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(57, 115);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(65, 12);
            this.label3.TabIndex = 2;
            this.label3.Text = "创建日期：";
            // 
            // cboTeamName
            // 
            this.cboTeamName.FormattingEnabled = true;
            this.cboTeamName.Location = new System.Drawing.Point(73, 13);
            this.cboTeamName.MaxLength = 15;
            this.cboTeamName.Name = "cboTeamName";
            this.cboTeamName.Size = new System.Drawing.Size(101, 20);
            this.cboTeamName.TabIndex = 5;
            this.cboTeamName.SelectedIndexChanged += new System.EventHandler(this.cboTeamName_SelectedIndexChanged);
            this.cboTeamName.TextChanged += new System.EventHandler(this.cboTeamName_TextChanged);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.ForeColor = System.Drawing.Color.Red;
            this.label4.Location = new System.Drawing.Point(381, 170);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(11, 12);
            this.label4.TabIndex = 14;
            this.label4.Text = "*";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.ForeColor = System.Drawing.Color.Red;
            this.label6.Location = new System.Drawing.Point(180, 17);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(11, 12);
            this.label6.TabIndex = 16;
            this.label6.Text = "*";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.label6);
            this.groupBox1.Controls.Add(this.cboSubmitter);
            this.groupBox1.Controls.Add(this.label8);
            this.groupBox1.Controls.Add(this.cboTeamName);
            this.groupBox1.Location = new System.Drawing.Point(325, 52);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(218, 78);
            this.groupBox1.TabIndex = 2;
            this.groupBox1.TabStop = false;
            // 
            // DayReportHCEntering
            // 
            this.AcceptButton = this.btnSubmit;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnCancel;
            this.ClientSize = new System.Drawing.Size(794, 439);
            this.ControlBox = false;
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.dtpDate);
            this.Controls.Add(this.dgrdvDayReportHC);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.btnSubmit);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "DayReportHCEntering";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "回采日报";
            ((System.ComponentModel.ISupportInitialize)(this.dgrdvDayReportHC)).EndInit();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView dgrdvDayReportHC;
        private System.Windows.Forms.RadioButton rbtn46;
        private System.Windows.Forms.RadioButton rbtn38;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Button btnSubmit;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.DateTimePicker dtpDate;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox cboSubmitter;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ComboBox cboTeamName;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.DataGridViewComboBoxColumn cboWorkTime;
        private System.Windows.Forms.DataGridViewComboBoxColumn cboWorkInfo;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column2;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column4;

    }
}