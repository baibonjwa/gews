using System.Windows.Forms;
using LibCommonControl;

namespace sys1
{
    partial class MainFormGe : Form
    {
        private const string configFileName = "sys.properties";

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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainFormGe));
            this.ss_GE = new System.Windows.Forms.StatusStrip();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.dgvData = new System.Windows.Forms.DataGridView();
            this._Value = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this._Time = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.gbTunnel = new System.Windows.Forms.GroupBox();
            this.lstProbeType = new System.Windows.Forms.ListBox();
            this.lstProbeName = new System.Windows.Forms.ListBox();
            this.label7 = new System.Windows.Forms.Label();
            this.selectTunnelSimple1 = new LibCommonForm.SelectTunnelSimple();
            this._lblProbeName = new System.Windows.Forms.Label();
            this._lblProbeStyle = new System.Windows.Forms.Label();
            this.btnQuery = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.rbtnHistory = new System.Windows.Forms.RadioButton();
            this.gb03 = new System.Windows.Forms.GroupBox();
            this.lblLoading = new System.Windows.Forms.Label();
            this.btnAfterDay = new System.Windows.Forms.Button();
            this.btnNow = new System.Windows.Forms.Button();
            this.btnBeforeDay = new System.Windows.Forms.Button();
            this.dateTimeEnd = new System.Windows.Forms.DateTimePicker();
            this.dateTimeStart = new System.Windows.Forms.DateTimePicker();
            this.rbtnRealtime = new System.Windows.Forms.RadioButton();
            this.picBoxLogo = new System.Windows.Forms.PictureBox();
            this.panel1 = new System.Windows.Forms.Panel();
            this._ckbSetMarks3 = new System.Windows.Forms.CheckBox();
            this._ckbSetMarks2 = new System.Windows.Forms.CheckBox();
            this._ckbSetMarks1 = new System.Windows.Forms.CheckBox();
            this.tChartN = new Steema.TeeChart.TChart();
            this.line1 = new Steema.TeeChart.Styles.Line();
            this.tChartT2 = new Steema.TeeChart.TChart();
            this.line2 = new Steema.TeeChart.Styles.Line();
            this.tChartM = new Steema.TeeChart.TChart();
            this.fastLine1 = new Steema.TeeChart.Styles.FastLine();
            this.barButtonItem6 = new DevExpress.XtraBars.BarButtonItem();
            this.timer1 = new System.Windows.Forms.Timer();
            this.repositoryItemHyperLinkEdit1 = new DevExpress.XtraEditors.Repository.RepositoryItemHyperLinkEdit();
            this.repositoryItemFontEdit1 = new DevExpress.XtraEditors.Repository.RepositoryItemFontEdit();
            this.barSubItem1 = new DevExpress.XtraBars.BarSubItem();
            this.mniSensorManagement = new DevExpress.XtraBars.BarButtonItem();
            this.mniSensorDataManage = new DevExpress.XtraBars.BarButtonItem();
            this.bbiMonitorSetting = new DevExpress.XtraBars.BarButtonItem();
            this.barSubItem2 = new DevExpress.XtraBars.BarSubItem();
            this.mniDatabaseSet = new DevExpress.XtraBars.BarButtonItem();
            this.mniUserInfoMana = new DevExpress.XtraBars.BarButtonItem();
            this.mniDepartment = new DevExpress.XtraBars.BarButtonItem();
            this.mniUserLoginInfoMana = new DevExpress.XtraBars.BarButtonItem();
            this.mniUserGroupInfoMana = new DevExpress.XtraBars.BarButtonItem();
            this.barSubItem3 = new DevExpress.XtraBars.BarSubItem();
            this.barButtonItem1 = new DevExpress.XtraBars.BarButtonItem();
            this.mniHelpFile = new DevExpress.XtraBars.BarButtonItem();
            this._DXbtAbout = new DevExpress.XtraBars.BarButtonItem();
            this.mniUserInfoManaFloat = new DevExpress.XtraBars.BarButtonItem();
            this.mniDepartmentFloat = new DevExpress.XtraBars.BarButtonItem();
            this.barButtonItem4 = new DevExpress.XtraBars.BarButtonItem();
            this.mniUserGroupInfoManaFloat = new DevExpress.XtraBars.BarButtonItem();
            this.mniDatabaseSetFloat = new DevExpress.XtraBars.BarButtonItem();
            this.mniUserLoginInfoManaFloat = new DevExpress.XtraBars.BarButtonItem();
            this.barSubItem4 = new DevExpress.XtraBars.BarSubItem();
            this.bbiExit = new DevExpress.XtraBars.BarButtonItem();
            this.bar2 = new DevExpress.XtraBars.Bar();
            this.barDockControlTop = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlBottom = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlLeft = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlRight = new DevExpress.XtraBars.BarDockControl();
            this.barManager1 = new DevExpress.XtraBars.BarManager();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvData)).BeginInit();
            this.gbTunnel.SuspendLayout();
            this.gb03.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picBoxLogo)).BeginInit();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemHyperLinkEdit1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemFontEdit1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.barManager1)).BeginInit();
            this.SuspendLayout();
            // 
            // ss_GE
            // 
            this.ss_GE.Location = new System.Drawing.Point(0, 613);
            this.ss_GE.Name = "ss_GE";
            this.ss_GE.Size = new System.Drawing.Size(1162, 22);
            this.ss_GE.TabIndex = 1;
            this.ss_GE.Text = "statusStrip1";
            // 
            // groupBox1
            // 
            this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.groupBox1.Controls.Add(this.dgvData);
            this.groupBox1.Location = new System.Drawing.Point(15, 382);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(218, 181);
            this.groupBox1.TabIndex = 7;
            this.groupBox1.TabStop = false;
            // 
            // dgvData
            // 
            this.dgvData.AllowDrop = true;
            this.dgvData.AllowUserToAddRows = false;
            this.dgvData.AllowUserToDeleteRows = false;
            this.dgvData.AllowUserToResizeColumns = false;
            this.dgvData.AllowUserToResizeRows = false;
            this.dgvData.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvData.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvData.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this._Value,
            this._Time});
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Tahoma", 9F);
            dataGridViewCellStyle1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(32)))), ((int)(((byte)(31)))), ((int)(((byte)(53)))));
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgvData.DefaultCellStyle = dataGridViewCellStyle1;
            this.dgvData.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvData.Location = new System.Drawing.Point(3, 17);
            this.dgvData.Name = "dgvData";
            this.dgvData.ReadOnly = true;
            this.dgvData.RowHeadersVisible = false;
            this.dgvData.RowTemplate.Height = 23;
            this.dgvData.Size = new System.Drawing.Size(212, 161);
            this.dgvData.TabIndex = 0;
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
            // gbTunnel
            // 
            this.gbTunnel.Controls.Add(this.lstProbeType);
            this.gbTunnel.Controls.Add(this.lstProbeName);
            this.gbTunnel.Controls.Add(this.label7);
            this.gbTunnel.Controls.Add(this.selectTunnelSimple1);
            this.gbTunnel.Controls.Add(this._lblProbeName);
            this.gbTunnel.Controls.Add(this._lblProbeStyle);
            this.gbTunnel.Location = new System.Drawing.Point(15, 65);
            this.gbTunnel.Name = "gbTunnel";
            this.gbTunnel.Size = new System.Drawing.Size(219, 151);
            this.gbTunnel.TabIndex = 8;
            this.gbTunnel.TabStop = false;
            this.gbTunnel.Text = "【传感器选择】";
            // 
            // lstProbeType
            // 
            this.lstProbeType.FormattingEnabled = true;
            this.lstProbeType.ItemHeight = 12;
            this.lstProbeType.Location = new System.Drawing.Point(13, 68);
            this.lstProbeType.Name = "lstProbeType";
            this.lstProbeType.Size = new System.Drawing.Size(80, 76);
            this.lstProbeType.TabIndex = 20;
            this.lstProbeType.SelectedIndexChanged += new System.EventHandler(this.lstProbeType_SelectedIndexChanged);
            // 
            // lstProbeName
            // 
            this.lstProbeName.FormattingEnabled = true;
            this.lstProbeName.ItemHeight = 12;
            this.lstProbeName.Location = new System.Drawing.Point(122, 68);
            this.lstProbeName.Name = "lstProbeName";
            this.lstProbeName.Size = new System.Drawing.Size(79, 76);
            this.lstProbeName.TabIndex = 22;
            this.lstProbeName.SelectedIndexChanged += new System.EventHandler(this.lstProbeName_SelectedIndexChanged);
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(99, 98);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(17, 12);
            this.label7.TabIndex = 23;
            this.label7.Text = ">>";
            // 
            // selectTunnelSimple1
            // 
            this.selectTunnelSimple1.BackColor = System.Drawing.Color.Transparent;
            this.selectTunnelSimple1.Location = new System.Drawing.Point(7, 15);
            this.selectTunnelSimple1.Name = "selectTunnelSimple1";
            this.selectTunnelSimple1.SelectedTunnel = null;
            this.selectTunnelSimple1.Size = new System.Drawing.Size(204, 33);
            this.selectTunnelSimple1.TabIndex = 0;
            // 
            // _lblProbeName
            // 
            this._lblProbeName.AutoSize = true;
            this._lblProbeName.Location = new System.Drawing.Point(121, 51);
            this._lblProbeName.Name = "_lblProbeName";
            this._lblProbeName.Size = new System.Drawing.Size(65, 12);
            this._lblProbeName.TabIndex = 21;
            this._lblProbeName.Text = "传感器名称";
            // 
            // _lblProbeStyle
            // 
            this._lblProbeStyle.AutoSize = true;
            this._lblProbeStyle.Location = new System.Drawing.Point(14, 51);
            this._lblProbeStyle.Name = "_lblProbeStyle";
            this._lblProbeStyle.Size = new System.Drawing.Size(65, 12);
            this._lblProbeStyle.TabIndex = 19;
            this._lblProbeStyle.Text = "传感器类型";
            // 
            // btnQuery
            // 
            this.btnQuery.Enabled = false;
            this.btnQuery.Location = new System.Drawing.Point(147, 105);
            this.btnQuery.Name = "btnQuery";
            this.btnQuery.Size = new System.Drawing.Size(63, 21);
            this.btnQuery.TabIndex = 5;
            this.btnQuery.Text = "查询";
            this.btnQuery.UseVisualStyleBackColor = true;
            this.btnQuery.Click += new System.EventHandler(this._btnQuery_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(6, 54);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(65, 12);
            this.label3.TabIndex = 3;
            this.label3.Text = "结束时间：";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(6, 24);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(65, 12);
            this.label1.TabIndex = 1;
            this.label1.Text = "开始时间：";
            // 
            // rbtnHistory
            // 
            this.rbtnHistory.AutoSize = true;
            this.rbtnHistory.Location = new System.Drawing.Point(124, 225);
            this.rbtnHistory.Name = "rbtnHistory";
            this.rbtnHistory.Size = new System.Drawing.Size(95, 16);
            this.rbtnHistory.TabIndex = 0;
            this.rbtnHistory.Text = "历史数据查询";
            this.rbtnHistory.UseVisualStyleBackColor = true;
            this.rbtnHistory.CheckedChanged += new System.EventHandler(this.rbtnHistory_CheckedChanged);
            // 
            // gb03
            // 
            this.gb03.Controls.Add(this.lblLoading);
            this.gb03.Controls.Add(this.btnAfterDay);
            this.gb03.Controls.Add(this.btnNow);
            this.gb03.Controls.Add(this.btnBeforeDay);
            this.gb03.Controls.Add(this.dateTimeEnd);
            this.gb03.Controls.Add(this.dateTimeStart);
            this.gb03.Controls.Add(this.label1);
            this.gb03.Controls.Add(this.label3);
            this.gb03.Controls.Add(this.btnQuery);
            this.gb03.Location = new System.Drawing.Point(15, 241);
            this.gb03.Name = "gb03";
            this.gb03.Size = new System.Drawing.Size(218, 139);
            this.gb03.TabIndex = 23;
            this.gb03.TabStop = false;
            // 
            // lblLoading
            // 
            this.lblLoading.AutoSize = true;
            this.lblLoading.ForeColor = System.Drawing.Color.Red;
            this.lblLoading.Location = new System.Drawing.Point(22, 111);
            this.lblLoading.Name = "lblLoading";
            this.lblLoading.Size = new System.Drawing.Size(83, 12);
            this.lblLoading.TabIndex = 9;
            this.lblLoading.Text = "数据加载中...";
            this.lblLoading.Visible = false;
            // 
            // btnAfterDay
            // 
            this.btnAfterDay.Enabled = false;
            this.btnAfterDay.Location = new System.Drawing.Point(147, 78);
            this.btnAfterDay.Name = "btnAfterDay";
            this.btnAfterDay.Size = new System.Drawing.Size(63, 21);
            this.btnAfterDay.TabIndex = 8;
            this.btnAfterDay.Text = "后一天>>";
            this.btnAfterDay.UseVisualStyleBackColor = true;
            this.btnAfterDay.Click += new System.EventHandler(this._btnAfterDay_Click);
            // 
            // btnNow
            // 
            this.btnNow.Enabled = false;
            this.btnNow.Location = new System.Drawing.Point(83, 78);
            this.btnNow.Name = "btnNow";
            this.btnNow.Size = new System.Drawing.Size(59, 21);
            this.btnNow.TabIndex = 8;
            this.btnNow.Text = "当前日期";
            this.btnNow.UseVisualStyleBackColor = true;
            this.btnNow.Click += new System.EventHandler(this._btnNow_Click);
            // 
            // btnBeforeDay
            // 
            this.btnBeforeDay.Enabled = false;
            this.btnBeforeDay.Location = new System.Drawing.Point(14, 78);
            this.btnBeforeDay.Name = "btnBeforeDay";
            this.btnBeforeDay.Size = new System.Drawing.Size(64, 21);
            this.btnBeforeDay.TabIndex = 8;
            this.btnBeforeDay.Text = "<<前一天";
            this.btnBeforeDay.UseVisualStyleBackColor = true;
            this.btnBeforeDay.Click += new System.EventHandler(this._btnBeforeDay_Click);
            // 
            // dateTimeEnd
            // 
            this.dateTimeEnd.CustomFormat = "yyyy/MM/dd   HH:mm:ss";
            this.dateTimeEnd.Enabled = false;
            this.dateTimeEnd.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dateTimeEnd.Location = new System.Drawing.Point(61, 50);
            this.dateTimeEnd.Name = "dateTimeEnd";
            this.dateTimeEnd.Size = new System.Drawing.Size(151, 21);
            this.dateTimeEnd.TabIndex = 7;
            this.dateTimeEnd.Value = new System.DateTime(2014, 5, 7, 23, 59, 59, 0);
            // 
            // dateTimeStart
            // 
            this.dateTimeStart.CustomFormat = "yyyy/MM/dd   HH:mm:ss";
            this.dateTimeStart.Enabled = false;
            this.dateTimeStart.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dateTimeStart.Location = new System.Drawing.Point(61, 20);
            this.dateTimeStart.Name = "dateTimeStart";
            this.dateTimeStart.Size = new System.Drawing.Size(151, 21);
            this.dateTimeStart.TabIndex = 6;
            this.dateTimeStart.Value = new System.DateTime(2014, 5, 7, 0, 0, 0, 0);
            // 
            // rbtnRealtime
            // 
            this.rbtnRealtime.AutoSize = true;
            this.rbtnRealtime.Checked = true;
            this.rbtnRealtime.Location = new System.Drawing.Point(25, 225);
            this.rbtnRealtime.Name = "rbtnRealtime";
            this.rbtnRealtime.Size = new System.Drawing.Size(95, 16);
            this.rbtnRealtime.TabIndex = 0;
            this.rbtnRealtime.TabStop = true;
            this.rbtnRealtime.Text = "实时数据监控";
            this.rbtnRealtime.UseVisualStyleBackColor = true;
            this.rbtnRealtime.CheckedChanged += new System.EventHandler(this.rbtnRealtime_CheckedChanged);
            // 
            // picBoxLogo
            // 
            this.picBoxLogo.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.picBoxLogo.Image = ((System.Drawing.Image)(resources.GetObject("picBoxLogo.Image")));
            this.picBoxLogo.Location = new System.Drawing.Point(3, 3);
            this.picBoxLogo.Name = "picBoxLogo";
            this.picBoxLogo.Size = new System.Drawing.Size(1159, 57);
            this.picBoxLogo.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.picBoxLogo.TabIndex = 20;
            this.picBoxLogo.TabStop = false;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this._ckbSetMarks3);
            this.panel1.Controls.Add(this.rbtnHistory);
            this.panel1.Controls.Add(this._ckbSetMarks2);
            this.panel1.Controls.Add(this.rbtnRealtime);
            this.panel1.Controls.Add(this._ckbSetMarks1);
            this.panel1.Controls.Add(this.tChartN);
            this.panel1.Controls.Add(this.tChartT2);
            this.panel1.Controls.Add(this.tChartM);
            this.panel1.Controls.Add(this.gb03);
            this.panel1.Controls.Add(this.picBoxLogo);
            this.panel1.Controls.Add(this.gbTunnel);
            this.panel1.Controls.Add(this.groupBox1);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(0, 24);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1162, 589);
            this.panel1.TabIndex = 2;
            // 
            // _ckbSetMarks3
            // 
            this._ckbSetMarks3.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this._ckbSetMarks3.AutoSize = true;
            this._ckbSetMarks3.BackColor = System.Drawing.Color.White;
            this._ckbSetMarks3.Location = new System.Drawing.Point(1050, 250);
            this._ckbSetMarks3.Name = "_ckbSetMarks3";
            this._ckbSetMarks3.Size = new System.Drawing.Size(96, 16);
            this._ckbSetMarks3.TabIndex = 27;
            this._ckbSetMarks3.Text = "显示数据标记";
            this._ckbSetMarks3.UseVisualStyleBackColor = false;
            this._ckbSetMarks3.Click += new System.EventHandler(this._ckbSetMarks3_Click);
            // 
            // _ckbSetMarks2
            // 
            this._ckbSetMarks2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this._ckbSetMarks2.AutoSize = true;
            this._ckbSetMarks2.BackColor = System.Drawing.Color.White;
            this._ckbSetMarks2.Location = new System.Drawing.Point(1050, 80);
            this._ckbSetMarks2.Name = "_ckbSetMarks2";
            this._ckbSetMarks2.Size = new System.Drawing.Size(96, 16);
            this._ckbSetMarks2.TabIndex = 27;
            this._ckbSetMarks2.Text = "显示数据标记";
            this._ckbSetMarks2.UseVisualStyleBackColor = false;
            this._ckbSetMarks2.Click += new System.EventHandler(this._ckbSetMarks2_Click);
            // 
            // _ckbSetMarks1
            // 
            this._ckbSetMarks1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this._ckbSetMarks1.AutoSize = true;
            this._ckbSetMarks1.BackColor = System.Drawing.Color.White;
            this._ckbSetMarks1.Location = new System.Drawing.Point(1050, 416);
            this._ckbSetMarks1.Name = "_ckbSetMarks1";
            this._ckbSetMarks1.Size = new System.Drawing.Size(96, 16);
            this._ckbSetMarks1.TabIndex = 27;
            this._ckbSetMarks1.Text = "显示数据标记";
            this._ckbSetMarks1.UseVisualStyleBackColor = false;
            this._ckbSetMarks1.Click += new System.EventHandler(this._ckbSetMarks1_Click);
            // 
            // tChartN
            // 
            this.tChartN.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            // 
            // 
            // 
            this.tChartN.Aspect.ColorPaletteIndex = -1;
            this.tChartN.Aspect.ThemeIndex = 2;
            this.tChartN.Aspect.View3D = false;
            // 
            // 
            // 
            // 
            // 
            // 
            // 
            // 
            // 
            this.tChartN.Axes.Bottom.AxisPen.Width = 1;
            // 
            // 
            // 
            this.tChartN.Axes.Bottom.Grid.Centered = true;
            this.tChartN.Axes.Bottom.Grid.Color = System.Drawing.Color.Black;
            this.tChartN.Axes.Bottom.Grid.Style = System.Drawing.Drawing2D.DashStyle.Solid;
            // 
            // 
            // 
            this.tChartN.Axes.Bottom.Labels.CustomSize = 27;
            this.tChartN.Axes.Bottom.Labels.DateTimeFormat = "yyyy/MM/dd HH:mm:ss";
            // 
            // 
            // 
            this.tChartN.Axes.Bottom.Labels.Font.Name = "Times New Roman";
            this.tChartN.Axes.Bottom.Labels.Font.Size = 10;
            this.tChartN.Axes.Bottom.Labels.MultiLine = true;
            this.tChartN.Axes.Bottom.Labels.RoundFirstLabel = false;
            this.tChartN.Axes.Bottom.Labels.Separation = 100;
            // 
            // 
            // 
            this.tChartN.Axes.Bottom.MinorTicks.Visible = false;
            // 
            // 
            // 
            this.tChartN.Axes.Bottom.Ticks.Color = System.Drawing.Color.Black;
            this.tChartN.Axes.Bottom.Ticks.Length = 2;
            // 
            // 
            // 
            this.tChartN.Axes.Bottom.TicksInner.Visible = false;
            // 
            // 
            // 
            // 
            // 
            // 
            this.tChartN.Axes.Bottom.Title.Font.Name = "Times New Roman";
            this.tChartN.Axes.Bottom.Title.Lines = new string[] {
        ""};
            // 
            // 
            // 
            // 
            // 
            // 
            this.tChartN.Axes.Depth.AxisPen.Width = 1;
            // 
            // 
            // 
            this.tChartN.Axes.Depth.Grid.Color = System.Drawing.Color.Black;
            this.tChartN.Axes.Depth.Grid.Style = System.Drawing.Drawing2D.DashStyle.Solid;
            // 
            // 
            // 
            // 
            // 
            // 
            this.tChartN.Axes.Depth.Labels.Font.Name = "Times New Roman";
            this.tChartN.Axes.Depth.Labels.Font.Size = 10;
            // 
            // 
            // 
            this.tChartN.Axes.Depth.MinorTicks.Visible = false;
            // 
            // 
            // 
            this.tChartN.Axes.Depth.Ticks.Color = System.Drawing.Color.Black;
            this.tChartN.Axes.Depth.Ticks.Length = 2;
            // 
            // 
            // 
            this.tChartN.Axes.Depth.TicksInner.Visible = false;
            // 
            // 
            // 
            // 
            // 
            // 
            this.tChartN.Axes.Depth.Title.Font.Name = "Times New Roman";
            // 
            // 
            // 
            // 
            // 
            // 
            this.tChartN.Axes.Left.AxisPen.Width = 1;
            // 
            // 
            // 
            this.tChartN.Axes.Left.Grid.Color = System.Drawing.Color.Black;
            this.tChartN.Axes.Left.Grid.Style = System.Drawing.Drawing2D.DashStyle.Solid;
            // 
            // 
            // 
            // 
            // 
            // 
            this.tChartN.Axes.Left.Labels.Font.Name = "Times New Roman";
            this.tChartN.Axes.Left.Labels.Font.Size = 10;
            this.tChartN.Axes.Left.Labels.RoundFirstLabel = false;
            // 
            // 
            // 
            this.tChartN.Axes.Left.MinorTicks.Visible = false;
            // 
            // 
            // 
            this.tChartN.Axes.Left.Ticks.Color = System.Drawing.Color.Black;
            this.tChartN.Axes.Left.Ticks.Length = 2;
            // 
            // 
            // 
            this.tChartN.Axes.Left.TicksInner.Visible = false;
            // 
            // 
            // 
            // 
            // 
            // 
            this.tChartN.Axes.Left.Title.Font.Name = "Times New Roman";
            // 
            // 
            // 
            // 
            // 
            // 
            this.tChartN.Axes.Left.Title.Font.Shadow.Brush.Color = System.Drawing.Color.FromArgb(((int)(((byte)(254)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.tChartN.Axes.Left.Title.Font.Shadow.Width = 0;
            this.tChartN.Axes.Left.Title.Lines = new string[] {
        ""};
            // 
            // 
            // 
            // 
            // 
            // 
            this.tChartN.Axes.Right.AxisPen.Width = 1;
            // 
            // 
            // 
            this.tChartN.Axes.Right.Grid.Color = System.Drawing.Color.Black;
            this.tChartN.Axes.Right.Grid.Style = System.Drawing.Drawing2D.DashStyle.Solid;
            // 
            // 
            // 
            // 
            // 
            // 
            this.tChartN.Axes.Right.Labels.Font.Name = "Times New Roman";
            this.tChartN.Axes.Right.Labels.Font.Size = 10;
            this.tChartN.Axes.Right.Labels.RoundFirstLabel = false;
            // 
            // 
            // 
            this.tChartN.Axes.Right.MinorTicks.Visible = false;
            // 
            // 
            // 
            this.tChartN.Axes.Right.Ticks.Color = System.Drawing.Color.Black;
            this.tChartN.Axes.Right.Ticks.Length = 2;
            // 
            // 
            // 
            this.tChartN.Axes.Right.TicksInner.Visible = false;
            // 
            // 
            // 
            // 
            // 
            // 
            this.tChartN.Axes.Right.Title.Font.Name = "Times New Roman";
            this.tChartN.Axes.Right.Visible = false;
            // 
            // 
            // 
            // 
            // 
            // 
            this.tChartN.Axes.Top.AxisPen.Width = 1;
            // 
            // 
            // 
            this.tChartN.Axes.Top.Grid.Color = System.Drawing.Color.Black;
            this.tChartN.Axes.Top.Grid.Style = System.Drawing.Drawing2D.DashStyle.Solid;
            // 
            // 
            // 
            // 
            // 
            // 
            this.tChartN.Axes.Top.Labels.Font.Name = "Times New Roman";
            this.tChartN.Axes.Top.Labels.Font.Size = 10;
            this.tChartN.Axes.Top.Labels.RoundFirstLabel = false;
            // 
            // 
            // 
            this.tChartN.Axes.Top.MinorTicks.Visible = false;
            // 
            // 
            // 
            this.tChartN.Axes.Top.Ticks.Color = System.Drawing.Color.Black;
            this.tChartN.Axes.Top.Ticks.Length = 2;
            // 
            // 
            // 
            this.tChartN.Axes.Top.TicksInner.Visible = false;
            // 
            // 
            // 
            // 
            // 
            // 
            this.tChartN.Axes.Top.Title.Font.Name = "Times New Roman";
            this.tChartN.Axes.Top.Visible = false;
            this.tChartN.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(235)))), ((int)(((byte)(236)))), ((int)(((byte)(239)))));
            this.tChartN.Cursor = System.Windows.Forms.Cursors.Default;
            // 
            // 
            // 
            // 
            // 
            // 
            // 
            // 
            // 
            this.tChartN.Header.Font.Brush.Color = System.Drawing.Color.Black;
            this.tChartN.Header.Font.Name = "Times New Roman";
            this.tChartN.Header.Font.Size = 12;
            this.tChartN.Header.Lines = new string[] {
        "同一工序条件下瓦斯浓度变化值N"};
            // 
            // 
            // 
            this.tChartN.Legend.CurrentPage = false;
            // 
            // 
            // 
            this.tChartN.Legend.Font.Name = "Times New Roman";
            this.tChartN.Legend.Font.Size = 10;
            // 
            // 
            // 
            this.tChartN.Legend.Pen.Visible = false;
            // 
            // 
            // 
            // 
            // 
            // 
            this.tChartN.Legend.Shadow.Brush.Color = System.Drawing.Color.FromArgb(((int)(((byte)(254)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.tChartN.Legend.Shadow.Height = 0;
            this.tChartN.Legend.Shadow.Width = 0;
            // 
            // 
            // 
            this.tChartN.Legend.Symbol.DefaultPen = false;
            // 
            // 
            // 
            this.tChartN.Legend.Symbol.Pen.Visible = false;
            this.tChartN.Legend.Transparent = true;
            this.tChartN.Legend.Visible = false;
            this.tChartN.Location = new System.Drawing.Point(238, 241);
            this.tChartN.Name = "tChartN";
            // 
            // 
            // 
            this.tChartN.Page.ScaleLastPage = false;
            // 
            // 
            // 
            // 
            // 
            // 
            this.tChartN.Panel.Bevel.Outer = Steema.TeeChart.Drawing.BevelStyles.None;
            // 
            // 
            // 
            this.tChartN.Panel.Brush.Color = System.Drawing.Color.White;
            // 
            // 
            // 
            this.tChartN.Panel.Gradient.EndColor = System.Drawing.Color.Yellow;
            this.tChartN.Panel.Gradient.MiddleColor = System.Drawing.Color.Empty;
            this.tChartN.Panel.Gradient.StartColor = System.Drawing.Color.White;
            // 
            // 
            // 
            this.tChartN.Panel.Pen.Visible = true;
            // 
            // 
            // 
            // 
            // 
            // 
            this.tChartN.Panel.Shadow.Brush.Color = System.Drawing.Color.FromArgb(((int)(((byte)(254)))), ((int)(((byte)(169)))), ((int)(((byte)(169)))), ((int)(((byte)(169)))));
            this.tChartN.Panel.Shadow.Height = 0;
            this.tChartN.Panel.Shadow.Width = 0;
            this.tChartN.Series.Add(this.line1);
            this.tChartN.Size = new System.Drawing.Size(915, 160);
            this.tChartN.TabIndex = 6;
            // 
            // 
            // 
            // 
            // 
            // 
            this.tChartN.Walls.Back.ApplyDark = false;
            // 
            // 
            // 
            this.tChartN.Walls.Back.Brush.Color = System.Drawing.Color.White;
            this.tChartN.Walls.Back.Size = 8;
            this.tChartN.Walls.Back.Transparent = false;
            this.tChartN.Walls.Back.Visible = false;
            // 
            // 
            // 
            this.tChartN.Walls.Bottom.ApplyDark = false;
            this.tChartN.Walls.Bottom.Size = 8;
            this.tChartN.Walls.Bottom.Visible = false;
            // 
            // 
            // 
            this.tChartN.Walls.Left.ApplyDark = false;
            // 
            // 
            // 
            this.tChartN.Walls.Left.Brush.Color = System.Drawing.Color.White;
            this.tChartN.Walls.Left.Size = 8;
            this.tChartN.Walls.Left.Visible = false;
            // 
            // 
            // 
            this.tChartN.Walls.Right.ApplyDark = false;
            // 
            // 
            // 
            this.tChartN.Walls.Right.Brush.Color = System.Drawing.Color.White;
            this.tChartN.Walls.Right.Size = 8;
            this.tChartN.Walls.Right.Visible = false;
            this.tChartN.Walls.Visible = false;
            // 
            // line1
            // 
            // 
            // 
            // 
            this.line1.Brush.Color = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            // 
            // 
            // 
            this.line1.LinePen.Color = System.Drawing.Color.FromArgb(((int)(((byte)(153)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            // 
            // 
            // 
            // 
            // 
            // 
            this.line1.Marks.Arrow.Color = System.Drawing.Color.Black;
            // 
            // 
            // 
            this.line1.Marks.Font.Name = "Times New Roman";
            this.line1.Marks.Font.Size = 10;
            this.line1.Marks.Transparent = true;
            // 
            // 
            // 
            // 
            // 
            // 
            this.line1.Pointer.Brush.Color = System.Drawing.Color.FromArgb(((int)(((byte)(254)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.line1.Pointer.Style = Steema.TeeChart.Styles.PointerStyles.Rectangle;
            this.line1.Title = "折线图1";
            // 
            // 
            // 
            this.line1.XValues.DataMember = "X";
            this.line1.XValues.Order = Steema.TeeChart.Styles.ValueListOrder.Ascending;
            // 
            // 
            // 
            this.line1.YValues.DataMember = "Y";
            // 
            // tChartT2
            // 
            this.tChartT2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            // 
            // 
            // 
            this.tChartT2.Aspect.ColorPaletteIndex = -1;
            this.tChartT2.Aspect.ThemeIndex = 2;
            this.tChartT2.Aspect.View3D = false;
            // 
            // 
            // 
            // 
            // 
            // 
            // 
            // 
            // 
            this.tChartT2.Axes.Bottom.AxisPen.Width = 1;
            // 
            // 
            // 
            this.tChartT2.Axes.Bottom.Grid.Centered = true;
            this.tChartT2.Axes.Bottom.Grid.Color = System.Drawing.Color.Black;
            this.tChartT2.Axes.Bottom.Grid.Style = System.Drawing.Drawing2D.DashStyle.Solid;
            // 
            // 
            // 
            this.tChartT2.Axes.Bottom.Labels.CustomSize = 27;
            this.tChartT2.Axes.Bottom.Labels.DateTimeFormat = "yyyy/MM/dd HH:mm:ss";
            // 
            // 
            // 
            this.tChartT2.Axes.Bottom.Labels.Font.Name = "Times New Roman";
            this.tChartT2.Axes.Bottom.Labels.Font.Size = 10;
            this.tChartT2.Axes.Bottom.Labels.MultiLine = true;
            this.tChartT2.Axes.Bottom.Labels.RoundFirstLabel = false;
            this.tChartT2.Axes.Bottom.Labels.Separation = 100;
            // 
            // 
            // 
            this.tChartT2.Axes.Bottom.MinorTicks.Visible = false;
            // 
            // 
            // 
            this.tChartT2.Axes.Bottom.Ticks.Color = System.Drawing.Color.Black;
            this.tChartT2.Axes.Bottom.Ticks.Length = 2;
            // 
            // 
            // 
            this.tChartT2.Axes.Bottom.TicksInner.Visible = false;
            // 
            // 
            // 
            // 
            // 
            // 
            this.tChartT2.Axes.Bottom.Title.Font.Name = "Times New Roman";
            this.tChartT2.Axes.Bottom.Title.Lines = new string[] {
        ""};
            // 
            // 
            // 
            // 
            // 
            // 
            this.tChartT2.Axes.Depth.AxisPen.Width = 1;
            // 
            // 
            // 
            this.tChartT2.Axes.Depth.Grid.Color = System.Drawing.Color.Black;
            this.tChartT2.Axes.Depth.Grid.Style = System.Drawing.Drawing2D.DashStyle.Solid;
            // 
            // 
            // 
            // 
            // 
            // 
            this.tChartT2.Axes.Depth.Labels.Font.Name = "Times New Roman";
            this.tChartT2.Axes.Depth.Labels.Font.Size = 10;
            // 
            // 
            // 
            this.tChartT2.Axes.Depth.MinorTicks.Visible = false;
            // 
            // 
            // 
            this.tChartT2.Axes.Depth.Ticks.Color = System.Drawing.Color.Black;
            this.tChartT2.Axes.Depth.Ticks.Length = 2;
            // 
            // 
            // 
            this.tChartT2.Axes.Depth.TicksInner.Visible = false;
            // 
            // 
            // 
            // 
            // 
            // 
            this.tChartT2.Axes.Depth.Title.Font.Name = "Times New Roman";
            // 
            // 
            // 
            // 
            // 
            // 
            this.tChartT2.Axes.Left.AxisPen.Width = 1;
            // 
            // 
            // 
            this.tChartT2.Axes.Left.Grid.Color = System.Drawing.Color.Black;
            this.tChartT2.Axes.Left.Grid.Style = System.Drawing.Drawing2D.DashStyle.Solid;
            // 
            // 
            // 
            // 
            // 
            // 
            this.tChartT2.Axes.Left.Labels.Font.Name = "Times New Roman";
            this.tChartT2.Axes.Left.Labels.Font.Size = 10;
            this.tChartT2.Axes.Left.Labels.RoundFirstLabel = false;
            // 
            // 
            // 
            this.tChartT2.Axes.Left.MinorTicks.Visible = false;
            // 
            // 
            // 
            this.tChartT2.Axes.Left.Ticks.Color = System.Drawing.Color.Black;
            this.tChartT2.Axes.Left.Ticks.Length = 2;
            // 
            // 
            // 
            this.tChartT2.Axes.Left.TicksInner.Visible = false;
            // 
            // 
            // 
            // 
            // 
            // 
            this.tChartT2.Axes.Left.Title.Font.Name = "Times New Roman";
            // 
            // 
            // 
            // 
            // 
            // 
            this.tChartT2.Axes.Left.Title.Font.Shadow.Brush.Color = System.Drawing.Color.FromArgb(((int)(((byte)(254)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.tChartT2.Axes.Left.Title.Font.Shadow.Width = 0;
            this.tChartT2.Axes.Left.Title.Lines = new string[] {
        ""};
            // 
            // 
            // 
            // 
            // 
            // 
            this.tChartT2.Axes.Right.AxisPen.Width = 1;
            // 
            // 
            // 
            this.tChartT2.Axes.Right.Grid.Color = System.Drawing.Color.Black;
            this.tChartT2.Axes.Right.Grid.Style = System.Drawing.Drawing2D.DashStyle.Solid;
            // 
            // 
            // 
            // 
            // 
            // 
            this.tChartT2.Axes.Right.Labels.Font.Name = "Times New Roman";
            this.tChartT2.Axes.Right.Labels.Font.Size = 10;
            this.tChartT2.Axes.Right.Labels.RoundFirstLabel = false;
            // 
            // 
            // 
            this.tChartT2.Axes.Right.MinorTicks.Visible = false;
            // 
            // 
            // 
            this.tChartT2.Axes.Right.Ticks.Color = System.Drawing.Color.Black;
            this.tChartT2.Axes.Right.Ticks.Length = 2;
            // 
            // 
            // 
            this.tChartT2.Axes.Right.TicksInner.Visible = false;
            // 
            // 
            // 
            // 
            // 
            // 
            this.tChartT2.Axes.Right.Title.Font.Name = "Times New Roman";
            this.tChartT2.Axes.Right.Visible = false;
            // 
            // 
            // 
            // 
            // 
            // 
            this.tChartT2.Axes.Top.AxisPen.Width = 1;
            // 
            // 
            // 
            this.tChartT2.Axes.Top.Grid.Color = System.Drawing.Color.Black;
            this.tChartT2.Axes.Top.Grid.Style = System.Drawing.Drawing2D.DashStyle.Solid;
            // 
            // 
            // 
            // 
            // 
            // 
            this.tChartT2.Axes.Top.Labels.Font.Name = "Times New Roman";
            this.tChartT2.Axes.Top.Labels.Font.Size = 10;
            this.tChartT2.Axes.Top.Labels.RoundFirstLabel = false;
            // 
            // 
            // 
            this.tChartT2.Axes.Top.MinorTicks.Visible = false;
            // 
            // 
            // 
            this.tChartT2.Axes.Top.Ticks.Color = System.Drawing.Color.Black;
            this.tChartT2.Axes.Top.Ticks.Length = 2;
            // 
            // 
            // 
            this.tChartT2.Axes.Top.TicksInner.Visible = false;
            // 
            // 
            // 
            // 
            // 
            // 
            this.tChartT2.Axes.Top.Title.Font.Name = "Times New Roman";
            this.tChartT2.Axes.Top.Visible = false;
            this.tChartT2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(235)))), ((int)(((byte)(236)))), ((int)(((byte)(239)))));
            this.tChartT2.Cursor = System.Windows.Forms.Cursors.Default;
            // 
            // 
            // 
            // 
            // 
            // 
            // 
            // 
            // 
            this.tChartT2.Header.Font.Brush.Color = System.Drawing.Color.Black;
            this.tChartT2.Header.Font.Name = "Times New Roman";
            this.tChartT2.Header.Font.Size = 12;
            this.tChartT2.Header.Lines = new string[] {
        "T2瓦斯浓度平均增加值Q"};
            // 
            // 
            // 
            this.tChartT2.Legend.CurrentPage = false;
            // 
            // 
            // 
            this.tChartT2.Legend.Font.Name = "Times New Roman";
            this.tChartT2.Legend.Font.Size = 10;
            // 
            // 
            // 
            this.tChartT2.Legend.Pen.Visible = false;
            // 
            // 
            // 
            // 
            // 
            // 
            this.tChartT2.Legend.Shadow.Brush.Color = System.Drawing.Color.FromArgb(((int)(((byte)(254)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.tChartT2.Legend.Shadow.Height = 0;
            this.tChartT2.Legend.Shadow.Width = 0;
            // 
            // 
            // 
            this.tChartT2.Legend.Symbol.DefaultPen = false;
            // 
            // 
            // 
            this.tChartT2.Legend.Symbol.Pen.Visible = false;
            this.tChartT2.Legend.Transparent = true;
            this.tChartT2.Legend.Visible = false;
            this.tChartT2.Location = new System.Drawing.Point(238, 73);
            this.tChartT2.Name = "tChartT2";
            // 
            // 
            // 
            this.tChartT2.Page.ScaleLastPage = false;
            // 
            // 
            // 
            // 
            // 
            // 
            this.tChartT2.Panel.Bevel.Outer = Steema.TeeChart.Drawing.BevelStyles.None;
            // 
            // 
            // 
            this.tChartT2.Panel.Brush.Color = System.Drawing.Color.White;
            // 
            // 
            // 
            this.tChartT2.Panel.Gradient.EndColor = System.Drawing.Color.Yellow;
            this.tChartT2.Panel.Gradient.MiddleColor = System.Drawing.Color.Empty;
            this.tChartT2.Panel.Gradient.StartColor = System.Drawing.Color.White;
            // 
            // 
            // 
            this.tChartT2.Panel.Pen.Visible = true;
            // 
            // 
            // 
            // 
            // 
            // 
            this.tChartT2.Panel.Shadow.Brush.Color = System.Drawing.Color.FromArgb(((int)(((byte)(254)))), ((int)(((byte)(169)))), ((int)(((byte)(169)))), ((int)(((byte)(169)))));
            this.tChartT2.Panel.Shadow.Height = 0;
            this.tChartT2.Panel.Shadow.Width = 0;
            this.tChartT2.Series.Add(this.line2);
            this.tChartT2.Size = new System.Drawing.Size(915, 160);
            this.tChartT2.TabIndex = 6;
            // 
            // 
            // 
            // 
            // 
            // 
            this.tChartT2.Walls.Back.ApplyDark = false;
            // 
            // 
            // 
            this.tChartT2.Walls.Back.Brush.Color = System.Drawing.Color.White;
            this.tChartT2.Walls.Back.Size = 8;
            this.tChartT2.Walls.Back.Transparent = false;
            this.tChartT2.Walls.Back.Visible = false;
            // 
            // 
            // 
            this.tChartT2.Walls.Bottom.ApplyDark = false;
            this.tChartT2.Walls.Bottom.Size = 8;
            this.tChartT2.Walls.Bottom.Visible = false;
            // 
            // 
            // 
            this.tChartT2.Walls.Left.ApplyDark = false;
            // 
            // 
            // 
            this.tChartT2.Walls.Left.Brush.Color = System.Drawing.Color.White;
            this.tChartT2.Walls.Left.Size = 8;
            this.tChartT2.Walls.Left.Visible = false;
            // 
            // 
            // 
            this.tChartT2.Walls.Right.ApplyDark = false;
            // 
            // 
            // 
            this.tChartT2.Walls.Right.Brush.Color = System.Drawing.Color.White;
            this.tChartT2.Walls.Right.Size = 8;
            this.tChartT2.Walls.Right.Visible = false;
            this.tChartT2.Walls.Visible = false;
            // 
            // line2
            // 
            // 
            // 
            // 
            this.line2.Brush.Color = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            // 
            // 
            // 
            this.line2.LinePen.Color = System.Drawing.Color.FromArgb(((int)(((byte)(153)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            // 
            // 
            // 
            // 
            // 
            // 
            this.line2.Marks.Arrow.Color = System.Drawing.Color.Black;
            // 
            // 
            // 
            this.line2.Marks.Font.Name = "Times New Roman";
            this.line2.Marks.Font.Size = 10;
            this.line2.Marks.Transparent = true;
            // 
            // 
            // 
            // 
            // 
            // 
            this.line2.Pointer.Brush.Color = System.Drawing.Color.FromArgb(((int)(((byte)(254)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.line2.Pointer.Style = Steema.TeeChart.Styles.PointerStyles.Rectangle;
            this.line2.Title = "折线图1";
            // 
            // 
            // 
            this.line2.XValues.DataMember = "X";
            this.line2.XValues.Order = Steema.TeeChart.Styles.ValueListOrder.Ascending;
            // 
            // 
            // 
            this.line2.YValues.DataMember = "Y";
            // 
            // tChartM
            // 
            this.tChartM.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            // 
            // 
            // 
            this.tChartM.Aspect.ColorPaletteIndex = -1;
            this.tChartM.Aspect.ThemeIndex = 2;
            this.tChartM.Aspect.View3D = false;
            // 
            // 
            // 
            // 
            // 
            // 
            // 
            // 
            // 
            this.tChartM.Axes.Bottom.AxisPen.Width = 1;
            // 
            // 
            // 
            this.tChartM.Axes.Bottom.Grid.Centered = true;
            this.tChartM.Axes.Bottom.Grid.Color = System.Drawing.Color.Black;
            this.tChartM.Axes.Bottom.Grid.Style = System.Drawing.Drawing2D.DashStyle.Solid;
            // 
            // 
            // 
            this.tChartM.Axes.Bottom.Labels.CustomSize = 27;
            this.tChartM.Axes.Bottom.Labels.DateTimeFormat = "yyyy/MM/dd HH:mm:ss";
            // 
            // 
            // 
            this.tChartM.Axes.Bottom.Labels.Font.Name = "Times New Roman";
            this.tChartM.Axes.Bottom.Labels.Font.Size = 10;
            this.tChartM.Axes.Bottom.Labels.MultiLine = true;
            this.tChartM.Axes.Bottom.Labels.RoundFirstLabel = false;
            this.tChartM.Axes.Bottom.Labels.Separation = 100;
            // 
            // 
            // 
            this.tChartM.Axes.Bottom.MinorTicks.Visible = false;
            // 
            // 
            // 
            this.tChartM.Axes.Bottom.Ticks.Color = System.Drawing.Color.Black;
            this.tChartM.Axes.Bottom.Ticks.Length = 2;
            // 
            // 
            // 
            this.tChartM.Axes.Bottom.TicksInner.Visible = false;
            // 
            // 
            // 
            // 
            // 
            // 
            this.tChartM.Axes.Bottom.Title.Font.Name = "Times New Roman";
            this.tChartM.Axes.Bottom.Title.Lines = new string[] {
        ""};
            // 
            // 
            // 
            // 
            // 
            // 
            this.tChartM.Axes.Depth.AxisPen.Width = 1;
            // 
            // 
            // 
            this.tChartM.Axes.Depth.Grid.Color = System.Drawing.Color.Black;
            this.tChartM.Axes.Depth.Grid.Style = System.Drawing.Drawing2D.DashStyle.Solid;
            // 
            // 
            // 
            // 
            // 
            // 
            this.tChartM.Axes.Depth.Labels.Font.Name = "Times New Roman";
            this.tChartM.Axes.Depth.Labels.Font.Size = 10;
            // 
            // 
            // 
            this.tChartM.Axes.Depth.MinorTicks.Visible = false;
            // 
            // 
            // 
            this.tChartM.Axes.Depth.Ticks.Color = System.Drawing.Color.Black;
            this.tChartM.Axes.Depth.Ticks.Length = 2;
            // 
            // 
            // 
            this.tChartM.Axes.Depth.TicksInner.Visible = false;
            // 
            // 
            // 
            // 
            // 
            // 
            this.tChartM.Axes.Depth.Title.Font.Name = "Times New Roman";
            // 
            // 
            // 
            // 
            // 
            // 
            this.tChartM.Axes.Left.AxisPen.Width = 1;
            // 
            // 
            // 
            this.tChartM.Axes.Left.Grid.Color = System.Drawing.Color.Black;
            this.tChartM.Axes.Left.Grid.Style = System.Drawing.Drawing2D.DashStyle.Solid;
            // 
            // 
            // 
            // 
            // 
            // 
            this.tChartM.Axes.Left.Labels.Font.Name = "Times New Roman";
            this.tChartM.Axes.Left.Labels.Font.Size = 10;
            this.tChartM.Axes.Left.Labels.RoundFirstLabel = false;
            // 
            // 
            // 
            this.tChartM.Axes.Left.MinorTicks.Visible = false;
            // 
            // 
            // 
            this.tChartM.Axes.Left.Ticks.Color = System.Drawing.Color.Black;
            this.tChartM.Axes.Left.Ticks.Length = 2;
            // 
            // 
            // 
            this.tChartM.Axes.Left.TicksInner.Visible = false;
            // 
            // 
            // 
            // 
            // 
            // 
            this.tChartM.Axes.Left.Title.Font.Name = "Times New Roman";
            // 
            // 
            // 
            // 
            // 
            // 
            this.tChartM.Axes.Left.Title.Font.Shadow.Brush.Color = System.Drawing.Color.FromArgb(((int)(((byte)(254)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.tChartM.Axes.Left.Title.Font.Shadow.Width = 0;
            this.tChartM.Axes.Left.Title.Lines = new string[] {
        ""};
            // 
            // 
            // 
            // 
            // 
            // 
            this.tChartM.Axes.Right.AxisPen.Width = 1;
            // 
            // 
            // 
            this.tChartM.Axes.Right.Grid.Color = System.Drawing.Color.Black;
            this.tChartM.Axes.Right.Grid.Style = System.Drawing.Drawing2D.DashStyle.Solid;
            // 
            // 
            // 
            // 
            // 
            // 
            this.tChartM.Axes.Right.Labels.Font.Name = "Times New Roman";
            this.tChartM.Axes.Right.Labels.Font.Size = 10;
            this.tChartM.Axes.Right.Labels.RoundFirstLabel = false;
            // 
            // 
            // 
            this.tChartM.Axes.Right.MinorTicks.Visible = false;
            // 
            // 
            // 
            this.tChartM.Axes.Right.Ticks.Color = System.Drawing.Color.Black;
            this.tChartM.Axes.Right.Ticks.Length = 2;
            // 
            // 
            // 
            this.tChartM.Axes.Right.TicksInner.Visible = false;
            // 
            // 
            // 
            // 
            // 
            // 
            this.tChartM.Axes.Right.Title.Font.Name = "Times New Roman";
            this.tChartM.Axes.Right.Visible = false;
            // 
            // 
            // 
            // 
            // 
            // 
            this.tChartM.Axes.Top.AxisPen.Width = 1;
            // 
            // 
            // 
            this.tChartM.Axes.Top.Grid.Color = System.Drawing.Color.Black;
            this.tChartM.Axes.Top.Grid.Style = System.Drawing.Drawing2D.DashStyle.Solid;
            // 
            // 
            // 
            // 
            // 
            // 
            this.tChartM.Axes.Top.Labels.Font.Name = "Times New Roman";
            this.tChartM.Axes.Top.Labels.Font.Size = 10;
            this.tChartM.Axes.Top.Labels.RoundFirstLabel = false;
            // 
            // 
            // 
            this.tChartM.Axes.Top.MinorTicks.Visible = false;
            // 
            // 
            // 
            this.tChartM.Axes.Top.Ticks.Color = System.Drawing.Color.Black;
            this.tChartM.Axes.Top.Ticks.Length = 2;
            // 
            // 
            // 
            this.tChartM.Axes.Top.TicksInner.Visible = false;
            // 
            // 
            // 
            // 
            // 
            // 
            this.tChartM.Axes.Top.Title.Font.Name = "Times New Roman";
            this.tChartM.Axes.Top.Visible = false;
            this.tChartM.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(235)))), ((int)(((byte)(236)))), ((int)(((byte)(239)))));
            this.tChartM.Cursor = System.Windows.Forms.Cursors.Default;
            // 
            // 
            // 
            // 
            // 
            // 
            // 
            // 
            // 
            this.tChartM.Header.Font.Brush.Color = System.Drawing.Color.Black;
            this.tChartM.Header.Font.Name = "Times New Roman";
            this.tChartM.Header.Font.Size = 12;
            this.tChartM.Header.Lines = new string[] {
        "监控系统原始数据M"};
            // 
            // 
            // 
            this.tChartM.Legend.CurrentPage = false;
            // 
            // 
            // 
            this.tChartM.Legend.Font.Name = "Times New Roman";
            this.tChartM.Legend.Font.Size = 10;
            // 
            // 
            // 
            this.tChartM.Legend.Pen.Visible = false;
            // 
            // 
            // 
            // 
            // 
            // 
            this.tChartM.Legend.Shadow.Brush.Color = System.Drawing.Color.FromArgb(((int)(((byte)(254)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.tChartM.Legend.Shadow.Height = 0;
            this.tChartM.Legend.Shadow.Width = 0;
            // 
            // 
            // 
            this.tChartM.Legend.Symbol.DefaultPen = false;
            // 
            // 
            // 
            this.tChartM.Legend.Symbol.Pen.Visible = false;
            this.tChartM.Legend.Transparent = true;
            this.tChartM.Legend.Visible = false;
            this.tChartM.Location = new System.Drawing.Point(238, 407);
            this.tChartM.Name = "tChartM";
            // 
            // 
            // 
            this.tChartM.Page.ScaleLastPage = false;
            // 
            // 
            // 
            // 
            // 
            // 
            this.tChartM.Panel.Bevel.Outer = Steema.TeeChart.Drawing.BevelStyles.None;
            // 
            // 
            // 
            this.tChartM.Panel.Brush.Color = System.Drawing.Color.White;
            // 
            // 
            // 
            this.tChartM.Panel.Gradient.EndColor = System.Drawing.Color.Yellow;
            this.tChartM.Panel.Gradient.MiddleColor = System.Drawing.Color.Empty;
            this.tChartM.Panel.Gradient.StartColor = System.Drawing.Color.White;
            // 
            // 
            // 
            this.tChartM.Panel.Pen.Visible = true;
            // 
            // 
            // 
            // 
            // 
            // 
            this.tChartM.Panel.Shadow.Brush.Color = System.Drawing.Color.FromArgb(((int)(((byte)(254)))), ((int)(((byte)(169)))), ((int)(((byte)(169)))), ((int)(((byte)(169)))));
            this.tChartM.Panel.Shadow.Height = 0;
            this.tChartM.Panel.Shadow.Width = 0;
            this.tChartM.Series.Add(this.fastLine1);
            this.tChartM.Size = new System.Drawing.Size(915, 153);
            this.tChartM.TabIndex = 26;
            // 
            // 
            // 
            // 
            // 
            // 
            this.tChartM.Walls.Back.ApplyDark = false;
            // 
            // 
            // 
            this.tChartM.Walls.Back.Brush.Color = System.Drawing.Color.White;
            this.tChartM.Walls.Back.Size = 8;
            this.tChartM.Walls.Back.Transparent = false;
            this.tChartM.Walls.Back.Visible = false;
            // 
            // 
            // 
            this.tChartM.Walls.Bottom.ApplyDark = false;
            this.tChartM.Walls.Bottom.Size = 8;
            this.tChartM.Walls.Bottom.Visible = false;
            // 
            // 
            // 
            this.tChartM.Walls.Left.ApplyDark = false;
            // 
            // 
            // 
            this.tChartM.Walls.Left.Brush.Color = System.Drawing.Color.White;
            this.tChartM.Walls.Left.Size = 8;
            this.tChartM.Walls.Left.Visible = false;
            // 
            // 
            // 
            this.tChartM.Walls.Right.ApplyDark = false;
            // 
            // 
            // 
            this.tChartM.Walls.Right.Brush.Color = System.Drawing.Color.White;
            this.tChartM.Walls.Right.Size = 8;
            this.tChartM.Walls.Right.Visible = false;
            this.tChartM.Walls.Visible = false;
            // 
            // fastLine1
            // 
            // 
            // 
            // 
            this.fastLine1.LinePen.Color = System.Drawing.Color.Red;
            // 
            // 
            // 
            // 
            // 
            // 
            this.fastLine1.Marks.Arrow.Color = System.Drawing.Color.Black;
            // 
            // 
            // 
            this.fastLine1.Marks.Font.Name = "Times New Roman";
            this.fastLine1.Marks.Font.Size = 10;
            this.fastLine1.Marks.Transparent = true;
            this.fastLine1.Title = "快速线1";
            // 
            // 
            // 
            this.fastLine1.XValues.DataMember = "X";
            this.fastLine1.XValues.DateTime = true;
            this.fastLine1.XValues.Order = Steema.TeeChart.Styles.ValueListOrder.Ascending;
            // 
            // 
            // 
            this.fastLine1.YValues.DataMember = "Y";
            // 
            // barButtonItem6
            // 
            this.barButtonItem6.Caption = "barButtonItem6";
            this.barButtonItem6.Id = 19;
            this.barButtonItem6.Name = "barButtonItem6";
            // 
            // timer1
            // 
            this.timer1.Interval = 1000;
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // repositoryItemHyperLinkEdit1
            // 
            this.repositoryItemHyperLinkEdit1.AutoHeight = false;
            this.repositoryItemHyperLinkEdit1.Name = "repositoryItemHyperLinkEdit1";
            // 
            // repositoryItemFontEdit1
            // 
            this.repositoryItemFontEdit1.AutoHeight = false;
            this.repositoryItemFontEdit1.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.repositoryItemFontEdit1.Name = "repositoryItemFontEdit1";
            // 
            // barSubItem1
            // 
            this.barSubItem1.Caption = "管理(&T)";
            this.barSubItem1.Id = 0;
            this.barSubItem1.ItemShortcut = new DevExpress.XtraBars.BarShortcut((System.Windows.Forms.Keys.Alt | System.Windows.Forms.Keys.T));
            this.barSubItem1.LinksPersistInfo.AddRange(new DevExpress.XtraBars.LinkPersistInfo[] {
            new DevExpress.XtraBars.LinkPersistInfo(this.mniSensorManagement),
            new DevExpress.XtraBars.LinkPersistInfo(this.mniSensorDataManage),
            new DevExpress.XtraBars.LinkPersistInfo(this.bbiMonitorSetting)});
            this.barSubItem1.Name = "barSubItem1";
            // 
            // mniSensorManagement
            // 
            this.mniSensorManagement.Caption = "传感器管理(&B)...";
            this.mniSensorManagement.Id = 3;
            this.mniSensorManagement.ImageIndex = 1;
            this.mniSensorManagement.ItemShortcut = new DevExpress.XtraBars.BarShortcut((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.E));
            this.mniSensorManagement.Name = "mniSensorManagement";
            this.mniSensorManagement.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.mniSensorManagement_ItemClick);
            // 
            // mniSensorDataManage
            // 
            this.mniSensorDataManage.Caption = "传感器数据管理(&E)...";
            this.mniSensorDataManage.Id = 4;
            this.mniSensorDataManage.ItemShortcut = new DevExpress.XtraBars.BarShortcut((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.B));
            this.mniSensorDataManage.Name = "mniSensorDataManage";
            this.mniSensorDataManage.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
            this.mniSensorDataManage.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.mniSensorDataManage_ItemClick);
            // 
            // bbiMonitorSetting
            // 
            this.bbiMonitorSetting.Caption = "监控参数设置";
            this.bbiMonitorSetting.Id = 39;
            this.bbiMonitorSetting.Name = "bbiMonitorSetting";
            this.bbiMonitorSetting.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.bbiMonitorSetting_ItemClick);
            // 
            // barSubItem2
            // 
            this.barSubItem2.Caption = "其他(&S)";
            this.barSubItem2.Id = 1;
            this.barSubItem2.ItemShortcut = new DevExpress.XtraBars.BarShortcut((System.Windows.Forms.Keys.Alt | System.Windows.Forms.Keys.S));
            this.barSubItem2.LinksPersistInfo.AddRange(new DevExpress.XtraBars.LinkPersistInfo[] {
            new DevExpress.XtraBars.LinkPersistInfo(this.mniDatabaseSet),
            new DevExpress.XtraBars.LinkPersistInfo(this.mniUserInfoMana),
            new DevExpress.XtraBars.LinkPersistInfo(this.mniDepartment),
            new DevExpress.XtraBars.LinkPersistInfo(this.mniUserLoginInfoMana),
            new DevExpress.XtraBars.LinkPersistInfo(this.mniUserGroupInfoMana)});
            this.barSubItem2.Name = "barSubItem2";
            // 
            // mniDatabaseSet
            // 
            this.mniDatabaseSet.Caption = "数据库设置(&D)...";
            this.mniDatabaseSet.Id = 5;
            this.mniDatabaseSet.ImageIndex = 4;
            this.mniDatabaseSet.ItemShortcut = new DevExpress.XtraBars.BarShortcut((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.D));
            this.mniDatabaseSet.Name = "mniDatabaseSet";
            this.mniDatabaseSet.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.mniDatabaseSet_ItemClick);
            // 
            // mniUserInfoMana
            // 
            this.mniUserInfoMana.Caption = "人员信息管理(&U)...";
            this.mniUserInfoMana.Id = 8;
            this.mniUserInfoMana.ImageIndex = 9;
            this.mniUserInfoMana.ItemShortcut = new DevExpress.XtraBars.BarShortcut((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.U));
            this.mniUserInfoMana.Name = "mniUserInfoMana";
            this.mniUserInfoMana.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.mniUserInfoMana_ItemClick);
            // 
            // mniDepartment
            // 
            this.mniDepartment.Caption = "部门信息管理(&R)...";
            this.mniDepartment.Id = 9;
            this.mniDepartment.ImageIndex = 0;
            this.mniDepartment.ItemShortcut = new DevExpress.XtraBars.BarShortcut((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.R));
            this.mniDepartment.Name = "mniDepartment";
            this.mniDepartment.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.mniDepartment_ItemClick);
            // 
            // mniUserLoginInfoMana
            // 
            this.mniUserLoginInfoMana.Caption = "用户信息管理(&W)...";
            this.mniUserLoginInfoMana.Id = 10;
            this.mniUserLoginInfoMana.ImageIndex = 5;
            this.mniUserLoginInfoMana.ItemShortcut = new DevExpress.XtraBars.BarShortcut((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.W));
            this.mniUserLoginInfoMana.Name = "mniUserLoginInfoMana";
            this.mniUserLoginInfoMana.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.mniUserLoginInfoMana_ItemClick);
            // 
            // mniUserGroupInfoMana
            // 
            this.mniUserGroupInfoMana.Caption = "用户组信息管理(&K)...";
            this.mniUserGroupInfoMana.Id = 11;
            this.mniUserGroupInfoMana.ImageIndex = 6;
            this.mniUserGroupInfoMana.ItemShortcut = new DevExpress.XtraBars.BarShortcut((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.K));
            this.mniUserGroupInfoMana.Name = "mniUserGroupInfoMana";
            this.mniUserGroupInfoMana.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.mniUserGroupInfoMana_ItemClick);
            // 
            // barSubItem3
            // 
            this.barSubItem3.Caption = "帮助(&H)";
            this.barSubItem3.Id = 2;
            this.barSubItem3.ItemShortcut = new DevExpress.XtraBars.BarShortcut((System.Windows.Forms.Keys.Alt | System.Windows.Forms.Keys.H));
            this.barSubItem3.LinksPersistInfo.AddRange(new DevExpress.XtraBars.LinkPersistInfo[] {
            new DevExpress.XtraBars.LinkPersistInfo(this.barButtonItem1),
            new DevExpress.XtraBars.LinkPersistInfo(this.mniHelpFile),
            new DevExpress.XtraBars.LinkPersistInfo(this._DXbtAbout)});
            this.barSubItem3.Name = "barSubItem3";
            // 
            // barButtonItem1
            // 
            this.barButtonItem1.Caption = "检查更新";
            this.barButtonItem1.Id = 45;
            this.barButtonItem1.Name = "barButtonItem1";
            this.barButtonItem1.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.barButtonItem1_ItemClick);
            // 
            // mniHelpFile
            // 
            this.mniHelpFile.Caption = "帮助文件(&H)...";
            this.mniHelpFile.Id = 12;
            this.mniHelpFile.ImageIndex = 7;
            this.mniHelpFile.ItemShortcut = new DevExpress.XtraBars.BarShortcut(System.Windows.Forms.Keys.F1);
            this.mniHelpFile.Name = "mniHelpFile";
            this.mniHelpFile.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.mniHelpFile_ItemClick);
            // 
            // _DXbtAbout
            // 
            this._DXbtAbout.Caption = "关于(A)...";
            this._DXbtAbout.Id = 38;
            this._DXbtAbout.ItemShortcut = new DevExpress.XtraBars.BarShortcut((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.K));
            this._DXbtAbout.Name = "_DXbtAbout";
            this._DXbtAbout.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this._DXbtAbout_ItemClick);
            // 
            // mniUserInfoManaFloat
            // 
            this.mniUserInfoManaFloat.Caption = "人员信息管理";
            this.mniUserInfoManaFloat.Id = 15;
            this.mniUserInfoManaFloat.ImageIndex = 9;
            this.mniUserInfoManaFloat.Name = "mniUserInfoManaFloat";
            this.mniUserInfoManaFloat.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.mniUserInfoManaFloat_ItemClick);
            // 
            // mniDepartmentFloat
            // 
            this.mniDepartmentFloat.Caption = "部门信息管理";
            this.mniDepartmentFloat.Id = 16;
            this.mniDepartmentFloat.ImageIndex = 0;
            this.mniDepartmentFloat.Name = "mniDepartmentFloat";
            this.mniDepartmentFloat.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.mniDepartmentFloat_ItemClick);
            // 
            // barButtonItem4
            // 
            this.barButtonItem4.Caption = "用户信息管理";
            this.barButtonItem4.Id = 17;
            this.barButtonItem4.Name = "barButtonItem4";
            // 
            // mniUserGroupInfoManaFloat
            // 
            this.mniUserGroupInfoManaFloat.Caption = "用户组信息管理";
            this.mniUserGroupInfoManaFloat.Id = 18;
            this.mniUserGroupInfoManaFloat.ImageIndex = 6;
            this.mniUserGroupInfoManaFloat.Name = "mniUserGroupInfoManaFloat";
            this.mniUserGroupInfoManaFloat.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.mniUserGroupInfoManaFloat_ItemClick);
            // 
            // mniDatabaseSetFloat
            // 
            this.mniDatabaseSetFloat.Caption = "数据库设置";
            this.mniDatabaseSetFloat.Id = 28;
            this.mniDatabaseSetFloat.ImageIndex = 4;
            this.mniDatabaseSetFloat.Name = "mniDatabaseSetFloat";
            this.mniDatabaseSetFloat.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.mniDatabaseSetFloat_ItemClick);
            // 
            // mniUserLoginInfoManaFloat
            // 
            this.mniUserLoginInfoManaFloat.Caption = "用户信息管理";
            this.mniUserLoginInfoManaFloat.Id = 29;
            this.mniUserLoginInfoManaFloat.ImageIndex = 5;
            this.mniUserLoginInfoManaFloat.Name = "mniUserLoginInfoManaFloat";
            this.mniUserLoginInfoManaFloat.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.mniUserLoginInfoManaFloat_ItemClick);
            // 
            // barSubItem4
            // 
            this.barSubItem4.Caption = "文件";
            this.barSubItem4.Id = 41;
            this.barSubItem4.LinksPersistInfo.AddRange(new DevExpress.XtraBars.LinkPersistInfo[] {
            new DevExpress.XtraBars.LinkPersistInfo(this.bbiExit)});
            this.barSubItem4.Name = "barSubItem4";
            // 
            // bbiExit
            // 
            this.bbiExit.Caption = "退出";
            this.bbiExit.Id = 42;
            this.bbiExit.Name = "bbiExit";
            this.bbiExit.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.bbiExit_ItemClick);
            // 
            // bar2
            // 
            this.bar2.BarName = "Main menu";
            this.bar2.DockCol = 0;
            this.bar2.DockRow = 0;
            this.bar2.DockStyle = DevExpress.XtraBars.BarDockStyle.Top;
            this.bar2.LinksPersistInfo.AddRange(new DevExpress.XtraBars.LinkPersistInfo[] {
            new DevExpress.XtraBars.LinkPersistInfo(this.barSubItem4),
            new DevExpress.XtraBars.LinkPersistInfo(this.barSubItem1),
            new DevExpress.XtraBars.LinkPersistInfo(this.barSubItem3)});
            this.bar2.OptionsBar.AllowQuickCustomization = false;
            this.bar2.OptionsBar.DrawDragBorder = false;
            this.bar2.OptionsBar.MultiLine = true;
            this.bar2.OptionsBar.UseWholeRow = true;
            this.bar2.Text = "主菜单";
            // 
            // barDockControlTop
            // 
            this.barDockControlTop.CausesValidation = false;
            this.barDockControlTop.Dock = System.Windows.Forms.DockStyle.Top;
            this.barDockControlTop.Location = new System.Drawing.Point(0, 0);
            this.barDockControlTop.Size = new System.Drawing.Size(1162, 24);
            // 
            // barDockControlBottom
            // 
            this.barDockControlBottom.CausesValidation = false;
            this.barDockControlBottom.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.barDockControlBottom.Location = new System.Drawing.Point(0, 635);
            this.barDockControlBottom.Size = new System.Drawing.Size(1162, 0);
            // 
            // barDockControlLeft
            // 
            this.barDockControlLeft.CausesValidation = false;
            this.barDockControlLeft.Dock = System.Windows.Forms.DockStyle.Left;
            this.barDockControlLeft.Location = new System.Drawing.Point(0, 24);
            this.barDockControlLeft.Size = new System.Drawing.Size(0, 611);
            // 
            // barDockControlRight
            // 
            this.barDockControlRight.CausesValidation = false;
            this.barDockControlRight.Dock = System.Windows.Forms.DockStyle.Right;
            this.barDockControlRight.Location = new System.Drawing.Point(1162, 24);
            this.barDockControlRight.Size = new System.Drawing.Size(0, 611);
            // 
            // barManager1
            // 
            this.barManager1.Bars.AddRange(new DevExpress.XtraBars.Bar[] {
            this.bar2});
            this.barManager1.DockControls.Add(this.barDockControlTop);
            this.barManager1.DockControls.Add(this.barDockControlBottom);
            this.barManager1.DockControls.Add(this.barDockControlLeft);
            this.barManager1.DockControls.Add(this.barDockControlRight);
            this.barManager1.Form = this;
            this.barManager1.Items.AddRange(new DevExpress.XtraBars.BarItem[] {
            this.barSubItem1,
            this.barSubItem2,
            this.barSubItem3,
            this.mniSensorManagement,
            this.mniSensorDataManage,
            this.mniDatabaseSet,
            this.mniUserInfoMana,
            this.mniDepartment,
            this.mniUserLoginInfoMana,
            this.mniUserGroupInfoMana,
            this.mniHelpFile,
            this.mniUserInfoManaFloat,
            this.mniDepartmentFloat,
            this.barButtonItem4,
            this.mniUserGroupInfoManaFloat,
            this.mniDatabaseSetFloat,
            this.mniUserLoginInfoManaFloat,
            this._DXbtAbout,
            this.bbiMonitorSetting,
            this.barSubItem4,
            this.bbiExit,
            this.barButtonItem1});
            this.barManager1.MainMenu = this.bar2;
            this.barManager1.MaxItemId = 46;
            this.barManager1.RepositoryItems.AddRange(new DevExpress.XtraEditors.Repository.RepositoryItem[] {
            this.repositoryItemHyperLinkEdit1,
            this.repositoryItemFontEdit1});
            // 
            // MainFormGe
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1162, 635);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.ss_GE);
            this.Controls.Add(this.barDockControlLeft);
            this.Controls.Add(this.barDockControlRight);
            this.Controls.Add(this.barDockControlBottom);
            this.Controls.Add(this.barDockControlTop);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "MainFormGe";
            this.Text = "KJGEW110 工作面瓦斯涌出动态特征管理系统";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.MainForm_GE_FormClosed);
            this.Load += new System.EventHandler(this.MainForm_GE_Load);
            this.groupBox1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvData)).EndInit();
            this.gbTunnel.ResumeLayout(false);
            this.gbTunnel.PerformLayout();
            this.gb03.ResumeLayout(false);
            this.gb03.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picBoxLogo)).EndInit();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemHyperLinkEdit1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemFontEdit1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.barManager1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.StatusStrip ss_GE;
        private DevExpress.XtraBars.BarButtonItem mniAbout;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.PictureBox picBoxLogo;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label _lblProbeName;
        private System.Windows.Forms.ListBox lstProbeType;
        private System.Windows.Forms.ListBox lstProbeName;
        private System.Windows.Forms.Label _lblProbeStyle;
        private System.Windows.Forms.RadioButton rbtnRealtime;
        private System.Windows.Forms.Button btnQuery;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.RadioButton rbtnHistory;
        private System.Windows.Forms.GroupBox gbTunnel;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.DataGridView dgvData;
        private System.Windows.Forms.DataGridViewTextBoxColumn _Value;
        private System.Windows.Forms.DataGridViewTextBoxColumn _Time;
        private DevExpress.XtraBars.BarButtonItem barButtonItem6;
        private System.Windows.Forms.GroupBox gb03;
        private System.Windows.Forms.DateTimePicker dateTimeEnd;
        private System.Windows.Forms.DateTimePicker dateTimeStart;
        private System.Windows.Forms.Timer timer1;
        private LibCommonForm.SelectTunnelSimple selectTunnelSimple1;
        private Steema.TeeChart.TChart tChartN;
        private Steema.TeeChart.TChart tChartM;
        private Steema.TeeChart.Styles.FastLine fastLine1;
        private System.Windows.Forms.CheckBox _ckbSetMarks1;
        private Steema.TeeChart.TChart tChartT2;
        private System.Windows.Forms.CheckBox _ckbSetMarks2;
        private System.Windows.Forms.CheckBox _ckbSetMarks3;
        private System.Windows.Forms.Button btnAfterDay;
        private System.Windows.Forms.Button btnBeforeDay;
        private System.Windows.Forms.Button btnNow;
        private System.Windows.Forms.Label lblLoading;
        private DevExpress.XtraEditors.Repository.RepositoryItemHyperLinkEdit repositoryItemHyperLinkEdit1;
        private DevExpress.XtraEditors.Repository.RepositoryItemFontEdit repositoryItemFontEdit1;
        private DevExpress.XtraBars.BarSubItem barSubItem1;
        private DevExpress.XtraBars.BarButtonItem mniSensorManagement;
        private DevExpress.XtraBars.BarButtonItem mniSensorDataManage;
        private DevExpress.XtraBars.BarButtonItem bbiMonitorSetting;
        private DevExpress.XtraBars.BarSubItem barSubItem2;
        private DevExpress.XtraBars.BarButtonItem mniDatabaseSet;
        private DevExpress.XtraBars.BarButtonItem mniUserInfoMana;
        private DevExpress.XtraBars.BarButtonItem mniDepartment;
        private DevExpress.XtraBars.BarButtonItem mniUserLoginInfoMana;
        private DevExpress.XtraBars.BarButtonItem mniUserGroupInfoMana;
        private DevExpress.XtraBars.BarSubItem barSubItem3;
        private DevExpress.XtraBars.BarButtonItem barButtonItem1;
        private DevExpress.XtraBars.BarButtonItem mniHelpFile;
        private DevExpress.XtraBars.BarButtonItem _DXbtAbout;
        private DevExpress.XtraBars.BarButtonItem mniUserInfoManaFloat;
        private DevExpress.XtraBars.BarButtonItem mniDepartmentFloat;
        private DevExpress.XtraBars.BarButtonItem barButtonItem4;
        private DevExpress.XtraBars.BarButtonItem mniUserGroupInfoManaFloat;
        private DevExpress.XtraBars.BarButtonItem mniDatabaseSetFloat;
        private DevExpress.XtraBars.BarButtonItem mniUserLoginInfoManaFloat;
        private DevExpress.XtraBars.BarSubItem barSubItem4;
        private DevExpress.XtraBars.BarButtonItem bbiExit;
        private DevExpress.XtraBars.Bar bar2;
        private DevExpress.XtraBars.BarDockControl barDockControlTop;
        private DevExpress.XtraBars.BarDockControl barDockControlBottom;
        private DevExpress.XtraBars.BarDockControl barDockControlLeft;
        private DevExpress.XtraBars.BarDockControl barDockControlRight;
        private DevExpress.XtraBars.BarManager barManager1;
        private Steema.TeeChart.Styles.Line line1;
        private Steema.TeeChart.Styles.Line line2;

    }
}