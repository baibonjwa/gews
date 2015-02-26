namespace sys1
{
    partial class MonitoringDataAnalysis
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MonitoringDataAnalysis));
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this._dgvData = new System.Windows.Forms.DataGridView();
            this._Value = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this._Time = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this._rbtnHistory = new System.Windows.Forms.RadioButton();
            this.gbTunnel = new System.Windows.Forms.GroupBox();
            this.selectTunnelUserControl1 = new LibCommonForm.SelectTunnelUserControl();
            this._ckbSetMarks = new System.Windows.Forms.CheckBox();
            this._txtSpeed = new System.Windows.Forms.TextBox();
            this.lblFbase = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.tChart1 = new Steema.TeeChart.TChart();
            this.fastLine1 = new Steema.TeeChart.Styles.FastLine();
            this._gbProbeSel = new System.Windows.Forms.GroupBox();
            this.label7 = new System.Windows.Forms.Label();
            this._lblProbeName = new System.Windows.Forms.Label();
            this._lblProbeStyle = new System.Windows.Forms.Label();
            this._lstProbeName = new System.Windows.Forms.ListBox();
            this._lstProbeStyle = new System.Windows.Forms.ListBox();
            this._gbHistory = new System.Windows.Forms.GroupBox();
            this._dateTimeStart = new System.Windows.Forms.DateTimePicker();
            this._btnQuery = new System.Windows.Forms.Button();
            this._dateTimeEnd = new System.Windows.Forms.DateTimePicker();
            this.label3 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this._gbRealTime = new System.Windows.Forms.GroupBox();
            this._btnStart = new System.Windows.Forms.Button();
            this._rbtnRealtime = new System.Windows.Forms.RadioButton();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this._dgvData)).BeginInit();
            this.gbTunnel.SuspendLayout();
            this._gbProbeSel.SuspendLayout();
            this._gbHistory.SuspendLayout();
            this._gbRealTime.SuspendLayout();
            this.groupBox4.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)));
            this.groupBox1.Controls.Add(this._dgvData);
            this.groupBox1.Location = new System.Drawing.Point(13, 5);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(299, 500);
            this.groupBox1.TabIndex = 6;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "数值列表";
            // 
            // _dgvData
            // 
            this._dgvData.AllowDrop = true;
            this._dgvData.AllowUserToAddRows = false;
            this._dgvData.AllowUserToDeleteRows = false;
            this._dgvData.AllowUserToResizeColumns = false;
            this._dgvData.AllowUserToResizeRows = false;
            this._dgvData.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this._dgvData.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this._dgvData.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this._Value,
            this._Time});
            this._dgvData.Dock = System.Windows.Forms.DockStyle.Fill;
            this._dgvData.Location = new System.Drawing.Point(3, 17);
            this._dgvData.Name = "_dgvData";
            this._dgvData.ReadOnly = true;
            this._dgvData.RowHeadersVisible = false;
            this._dgvData.RowTemplate.Height = 23;
            this._dgvData.Size = new System.Drawing.Size(293, 480);
            this._dgvData.TabIndex = 0;
            // 
            // _Value
            // 
            this._Value.FillWeight = 87.066F;
            this._Value.HeaderText = "值";
            this._Value.Name = "_Value";
            this._Value.ReadOnly = true;
            // 
            // _Time
            // 
            this._Time.FillWeight = 168.934F;
            this._Time.HeaderText = "时间";
            this._Time.Name = "_Time";
            this._Time.ReadOnly = true;
            // 
            // timer1
            // 
            this.timer1.Interval = 1000;
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // _rbtnHistory
            // 
            this._rbtnHistory.AutoSize = true;
            this._rbtnHistory.Location = new System.Drawing.Point(8, 33);
            this._rbtnHistory.Name = "_rbtnHistory";
            this._rbtnHistory.Size = new System.Drawing.Size(95, 16);
            this._rbtnHistory.TabIndex = 0;
            this._rbtnHistory.Text = "历史数据分析";
            this._rbtnHistory.UseVisualStyleBackColor = true;
            this._rbtnHistory.Click += new System.EventHandler(this._rbtnHistory_Click);
            // 
            // gbTunnel
            // 
            this.gbTunnel.Controls.Add(this.selectTunnelUserControl1);
            this.gbTunnel.Location = new System.Drawing.Point(318, 5);
            this.gbTunnel.Name = "gbTunnel";
            this.gbTunnel.Size = new System.Drawing.Size(605, 199);
            this.gbTunnel.TabIndex = 0;
            this.gbTunnel.TabStop = false;
            this.gbTunnel.Text = "【传感器所在巷道】";
            // 
            // selectTunnelUserControl1
            // 
            this.selectTunnelUserControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.selectTunnelUserControl1.Location = new System.Drawing.Point(3, 17);
            this.selectTunnelUserControl1.Name = "selectTunnelUserControl1";
            this.selectTunnelUserControl1.Size = new System.Drawing.Size(599, 179);
            this.selectTunnelUserControl1.TabIndex = 0;
            // 
            // _ckbSetMarks
            // 
            this._ckbSetMarks.AutoSize = true;
            this._ckbSetMarks.Location = new System.Drawing.Point(553, 19);
            this._ckbSetMarks.Name = "_ckbSetMarks";
            this._ckbSetMarks.Size = new System.Drawing.Size(96, 16);
            this._ckbSetMarks.TabIndex = 0;
            this._ckbSetMarks.Text = "显示数据标记";
            this._ckbSetMarks.UseVisualStyleBackColor = true;
            this._ckbSetMarks.Click += new System.EventHandler(this._ckbSetMarks_Click);
            // 
            // _txtSpeed
            // 
            this._txtSpeed.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this._txtSpeed.Location = new System.Drawing.Point(89, 57);
            this._txtSpeed.Name = "_txtSpeed";
            this._txtSpeed.Size = new System.Drawing.Size(43, 21);
            this._txtSpeed.TabIndex = 2;
            this._txtSpeed.Text = "1";
            this._txtSpeed.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // lblFbase
            // 
            this.lblFbase.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lblFbase.AutoSize = true;
            this.lblFbase.Location = new System.Drawing.Point(6, 60);
            this.lblFbase.Name = "lblFbase";
            this.lblFbase.Size = new System.Drawing.Size(89, 12);
            this.lblFbase.TabIndex = 1;
            this.lblFbase.Text = "数据传输频率：";
            // 
            // label4
            // 
            this.label4.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(136, 60);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(11, 12);
            this.label4.TabIndex = 3;
            this.label4.Text = "s";
            // 
            // tChart1
            // 
            // 
            // 
            // 
            this.tChart1.Aspect.View3D = false;
            // 
            // 
            // 
            // 
            // 
            // 
            // 
            // 
            // 
            this.tChart1.Axes.Bottom.Labels.DateTimeFormat = "yyyy/MM/dd HH:mm:ss";
            this.tChart1.Axes.Bottom.Labels.MultiLine = true;
            // 
            // 
            // 
            this.tChart1.Axes.Bottom.Title.Lines = new string[] {
        ""};
            // 
            // 
            // 
            // 
            // 
            // 
            // 
            // 
            // 
            // 
            // 
            // 
            // 
            // 
            // 
            this.tChart1.Axes.Left.Title.Font.Shadow.Brush.Color = System.Drawing.Color.FromArgb(((int)(((byte)(254)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.tChart1.Axes.Left.Title.Font.Shadow.Width = 0;
            this.tChart1.Axes.Left.Title.Lines = new string[] {
        ""};
            // 
            // 
            // 
            this.tChart1.Axes.Right.Visible = false;
            // 
            // 
            // 
            this.tChart1.Axes.Top.Visible = false;
            this.tChart1.Cursor = System.Windows.Forms.Cursors.Default;
            // 
            // 
            // 
            this.tChart1.Header.Lines = new string[] {
        "瓦斯预警实时监控"};
            // 
            // 
            // 
            this.tChart1.Legend.CurrentPage = false;
            // 
            // 
            // 
            // 
            // 
            // 
            this.tChart1.Legend.Shadow.Brush.Color = System.Drawing.Color.FromArgb(((int)(((byte)(254)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.tChart1.Legend.Visible = false;
            this.tChart1.Location = new System.Drawing.Point(344, 236);
            this.tChart1.Name = "tChart1";
            // 
            // 
            // 
            this.tChart1.Page.ScaleLastPage = false;
            // 
            // 
            // 
            // 
            // 
            // 
            this.tChart1.Panel.Shadow.Height = 0;
            this.tChart1.Panel.Shadow.Width = 0;
            this.tChart1.Series.Add(this.fastLine1);
            this.tChart1.Size = new System.Drawing.Size(821, 208);
            this.tChart1.TabIndex = 5;
            // 
            // 
            // 
            // 
            // 
            // 
            this.tChart1.Walls.Back.Visible = false;
            // 
            // 
            // 
            this.tChart1.Walls.Bottom.Visible = false;
            // 
            // 
            // 
            this.tChart1.Walls.Left.Visible = false;
            // 
            // 
            // 
            // 
            // 
            // 
            this.tChart1.Walls.Right.Brush.Color = System.Drawing.Color.Silver;
            this.tChart1.Walls.Right.Visible = false;
            this.tChart1.Walls.Visible = false;
            // 
            // fastLine1
            // 
            // 
            // 
            // 
            this.fastLine1.LinePen.Color = System.Drawing.Color.Red;
            this.fastLine1.Title = "快速线1";
            // 
            // 
            // 
            this.fastLine1.XValues.DataMember = "X";
            this.fastLine1.XValues.Order = Steema.TeeChart.Styles.ValueListOrder.Ascending;
            // 
            // 
            // 
            this.fastLine1.YValues.DataMember = "Y";
            // 
            // _gbProbeSel
            // 
            this._gbProbeSel.Controls.Add(this.label7);
            this._gbProbeSel.Controls.Add(this._lblProbeName);
            this._gbProbeSel.Controls.Add(this._lblProbeStyle);
            this._gbProbeSel.Controls.Add(this._lstProbeName);
            this._gbProbeSel.Controls.Add(this._lstProbeStyle);
            this._gbProbeSel.Location = new System.Drawing.Point(929, 5);
            this._gbProbeSel.Name = "_gbProbeSel";
            this._gbProbeSel.Size = new System.Drawing.Size(199, 164);
            this._gbProbeSel.TabIndex = 1;
            this._gbProbeSel.TabStop = false;
            this._gbProbeSel.Text = "【传感器选择】";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(91, 92);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(17, 12);
            this.label7.TabIndex = 4;
            this.label7.Text = ">>";
            // 
            // _lblProbeName
            // 
            this._lblProbeName.AutoSize = true;
            this._lblProbeName.Location = new System.Drawing.Point(112, 33);
            this._lblProbeName.Name = "_lblProbeName";
            this._lblProbeName.Size = new System.Drawing.Size(65, 12);
            this._lblProbeName.TabIndex = 2;
            this._lblProbeName.Text = "传感器名称";
            // 
            // _lblProbeStyle
            // 
            this._lblProbeStyle.AutoSize = true;
            this._lblProbeStyle.Location = new System.Drawing.Point(6, 33);
            this._lblProbeStyle.Name = "_lblProbeStyle";
            this._lblProbeStyle.Size = new System.Drawing.Size(65, 12);
            this._lblProbeStyle.TabIndex = 0;
            this._lblProbeStyle.Text = "传感器类型";
            // 
            // _lstProbeName
            // 
            this._lstProbeName.FormattingEnabled = true;
            this._lstProbeName.ItemHeight = 12;
            this._lstProbeName.Location = new System.Drawing.Point(114, 48);
            this._lstProbeName.Name = "_lstProbeName";
            this._lstProbeName.Size = new System.Drawing.Size(79, 112);
            this._lstProbeName.TabIndex = 3;
            this._lstProbeName.MouseUp += new System.Windows.Forms.MouseEventHandler(this._lstProbeName_MouseUp);
            // 
            // _lstProbeStyle
            // 
            this._lstProbeStyle.FormattingEnabled = true;
            this._lstProbeStyle.ItemHeight = 12;
            this._lstProbeStyle.Location = new System.Drawing.Point(6, 48);
            this._lstProbeStyle.Name = "_lstProbeStyle";
            this._lstProbeStyle.Size = new System.Drawing.Size(79, 112);
            this._lstProbeStyle.TabIndex = 1;
            this._lstProbeStyle.MouseUp += new System.Windows.Forms.MouseEventHandler(this._lstProbeStyle_MouseUp);
            // 
            // _gbHistory
            // 
            this._gbHistory.Controls.Add(this._dateTimeStart);
            this._gbHistory.Controls.Add(this._btnQuery);
            this._gbHistory.Controls.Add(this._dateTimeEnd);
            this._gbHistory.Controls.Add(this.label3);
            this._gbHistory.Controls.Add(this.label1);
            this._gbHistory.Controls.Add(this._rbtnHistory);
            this._gbHistory.Location = new System.Drawing.Point(1334, 5);
            this._gbHistory.Name = "_gbHistory";
            this._gbHistory.Size = new System.Drawing.Size(250, 164);
            this._gbHistory.TabIndex = 3;
            this._gbHistory.TabStop = false;
            this._gbHistory.Text = "【历史数据分析】";
            // 
            // _dateTimeStart
            // 
            this._dateTimeStart.CustomFormat = "yyyy/MM/dd   HH:mm:ss";
            this._dateTimeStart.Enabled = false;
            this._dateTimeStart.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this._dateTimeStart.Location = new System.Drawing.Point(65, 60);
            this._dateTimeStart.Name = "_dateTimeStart";
            this._dateTimeStart.Size = new System.Drawing.Size(164, 21);
            this._dateTimeStart.TabIndex = 2;
            // 
            // _btnQuery
            // 
            this._btnQuery.Enabled = false;
            this._btnQuery.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this._btnQuery.Location = new System.Drawing.Point(162, 137);
            this._btnQuery.Name = "_btnQuery";
            this._btnQuery.Size = new System.Drawing.Size(82, 21);
            this._btnQuery.TabIndex = 5;
            this._btnQuery.Text = "查询";
            this._btnQuery.UseVisualStyleBackColor = true;
            this._btnQuery.Click += new System.EventHandler(this._btnQuery_Click);
            // 
            // _dateTimeEnd
            // 
            this._dateTimeEnd.CustomFormat = "yyyy/MM/dd   HH:mm:ss";
            this._dateTimeEnd.Enabled = false;
            this._dateTimeEnd.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this._dateTimeEnd.Location = new System.Drawing.Point(65, 88);
            this._dateTimeEnd.Name = "_dateTimeEnd";
            this._dateTimeEnd.Size = new System.Drawing.Size(164, 21);
            this._dateTimeEnd.TabIndex = 4;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(6, 94);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(65, 12);
            this.label3.TabIndex = 3;
            this.label3.Text = "结束时间：";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(6, 66);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(65, 12);
            this.label1.TabIndex = 1;
            this.label1.Text = "开始时间：";
            // 
            // _gbRealTime
            // 
            this._gbRealTime.Controls.Add(this._btnStart);
            this._gbRealTime.Controls.Add(this._rbtnRealtime);
            this._gbRealTime.Controls.Add(this._txtSpeed);
            this._gbRealTime.Controls.Add(this.lblFbase);
            this._gbRealTime.Controls.Add(this.label4);
            this._gbRealTime.Location = new System.Drawing.Point(1134, 5);
            this._gbRealTime.Name = "_gbRealTime";
            this._gbRealTime.Size = new System.Drawing.Size(194, 164);
            this._gbRealTime.TabIndex = 2;
            this._gbRealTime.TabStop = false;
            this._gbRealTime.Text = "【实时数据监控】";
            // 
            // _btnStart
            // 
            this._btnStart.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this._btnStart.Location = new System.Drawing.Point(106, 137);
            this._btnStart.Name = "_btnStart";
            this._btnStart.Size = new System.Drawing.Size(82, 21);
            this._btnStart.TabIndex = 4;
            this._btnStart.Text = "开始";
            this._btnStart.UseVisualStyleBackColor = true;
            this._btnStart.Click += new System.EventHandler(this._btnStart_Click);
            // 
            // _rbtnRealtime
            // 
            this._rbtnRealtime.AutoSize = true;
            this._rbtnRealtime.Checked = true;
            this._rbtnRealtime.Location = new System.Drawing.Point(6, 33);
            this._rbtnRealtime.Name = "_rbtnRealtime";
            this._rbtnRealtime.Size = new System.Drawing.Size(95, 16);
            this._rbtnRealtime.TabIndex = 0;
            this._rbtnRealtime.TabStop = true;
            this._rbtnRealtime.Text = "实时数据监控";
            this._rbtnRealtime.UseVisualStyleBackColor = true;
            this._rbtnRealtime.Click += new System.EventHandler(this._rbtnRealtime_Click);
            // 
            // groupBox4
            // 
            this.groupBox4.Controls.Add(this._ckbSetMarks);
            this.groupBox4.Location = new System.Drawing.Point(929, 162);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(655, 41);
            this.groupBox4.TabIndex = 4;
            this.groupBox4.TabStop = false;
            // 
            // MonitoringDataAnalysis
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1500, 517);
            this.Controls.Add(this._gbRealTime);
            this.Controls.Add(this._gbHistory);
            this.Controls.Add(this._gbProbeSel);
            this.Controls.Add(this.tChart1);
            this.Controls.Add(this.groupBox4);
            this.Controls.Add(this.gbTunnel);
            this.Controls.Add(this.groupBox1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "MonitoringDataAnalysis";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "监控数据分析";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.groupBox1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this._dgvData)).EndInit();
            this.gbTunnel.ResumeLayout(false);
            this._gbProbeSel.ResumeLayout(false);
            this._gbProbeSel.PerformLayout();
            this._gbHistory.ResumeLayout(false);
            this._gbHistory.PerformLayout();
            this._gbRealTime.ResumeLayout(false);
            this._gbRealTime.PerformLayout();
            this.groupBox4.ResumeLayout(false);
            this.groupBox4.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.DataGridView _dgvData;
        private System.Windows.Forms.DataGridViewTextBoxColumn _Value;
        private System.Windows.Forms.DataGridViewTextBoxColumn _Time;
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.RadioButton _rbtnHistory;
        private System.Windows.Forms.GroupBox gbTunnel;
        private System.Windows.Forms.CheckBox _ckbSetMarks;
        private System.Windows.Forms.TextBox _txtSpeed;
        private System.Windows.Forms.Label lblFbase;
        private System.Windows.Forms.Label label4;
        private Steema.TeeChart.Tools.PageNumber pageNumber1;
        private Steema.TeeChart.TChart tChart1;
        private LibCommonForm.SelectTunnelUserControl selectTunnelUserControl1;
        private System.Windows.Forms.GroupBox _gbProbeSel;
        private System.Windows.Forms.GroupBox _gbHistory;
        private System.Windows.Forms.Button _btnQuery;
        private System.Windows.Forms.DateTimePicker _dateTimeEnd;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.GroupBox _gbRealTime;
        private System.Windows.Forms.RadioButton _rbtnRealtime;
        private System.Windows.Forms.DateTimePicker _dateTimeStart;
        private System.Windows.Forms.Button _btnStart;
        private System.Windows.Forms.ListBox _lstProbeStyle;
        private System.Windows.Forms.Label _lblProbeStyle;
        private System.Windows.Forms.ListBox _lstProbeName;
        private System.Windows.Forms.Label _lblProbeName;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.GroupBox groupBox4;
        private Steema.TeeChart.Styles.FastLine fastLine1;

    }
}