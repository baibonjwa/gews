namespace UnderTerminal
{
    partial class CurveMonitor
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(CurveMonitor));
            this.ss_GE = new System.Windows.Forms.StatusStrip();
            this.barDockControlTop = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlBottom = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlLeft = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlRight = new DevExpress.XtraBars.BarDockControl();
            this.imageList1 = new System.Windows.Forms.ImageList();
            this.mniUserInfoManaFloat = new DevExpress.XtraBars.BarButtonItem();
            this.mniDepartmentFloat = new DevExpress.XtraBars.BarButtonItem();
            this.barButtonItem4 = new DevExpress.XtraBars.BarButtonItem();
            this.mniUserGroupInfoManaFloat = new DevExpress.XtraBars.BarButtonItem();
            this.mniDatabaseSetFloat = new DevExpress.XtraBars.BarButtonItem();
            this.mniUserLoginInfoManaFloat = new DevExpress.XtraBars.BarButtonItem();
            this._DXbtAbout = new DevExpress.XtraBars.BarButtonItem();
            this.repositoryItemHyperLinkEdit1 = new DevExpress.XtraEditors.Repository.RepositoryItemHyperLinkEdit();
            this.repositoryItemFontEdit1 = new DevExpress.XtraEditors.Repository.RepositoryItemFontEdit();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this._dgvData = new System.Windows.Forms.DataGridView();
            this._Value = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this._Time = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this._btnStart = new System.Windows.Forms.Button();
            this.panel1 = new System.Windows.Forms.Panel();
            this.label1 = new System.Windows.Forms.Label();
            this.cbxSensors = new System.Windows.Forms.ComboBox();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.tChartT2 = new Steema.TeeChart.TChart();
            this.fastLine2 = new Steema.TeeChart.Styles.FastLine();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.tChartN = new Steema.TeeChart.TChart();
            this.fastLine3 = new Steema.TeeChart.Styles.FastLine();
            this.tabPage3 = new System.Windows.Forms.TabPage();
            this.tChartM = new Steema.TeeChart.TChart();
            this.fastLine1 = new Steema.TeeChart.Styles.FastLine();
            this.btnSwitch = new System.Windows.Forms.Button();
            this.btnExit = new System.Windows.Forms.Button();
            this._ckbSetMarks = new System.Windows.Forms.CheckBox();
            this.barButtonItem6 = new DevExpress.XtraBars.BarButtonItem();
            this.timer1 = new System.Windows.Forms.Timer();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemHyperLinkEdit1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemFontEdit1)).BeginInit();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this._dgvData)).BeginInit();
            this.panel1.SuspendLayout();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.tabPage2.SuspendLayout();
            this.tabPage3.SuspendLayout();
            this.SuspendLayout();
            // 
            // ss_GE
            // 
            this.ss_GE.Location = new System.Drawing.Point(0, 539);
            this.ss_GE.Name = "ss_GE";
            this.ss_GE.Size = new System.Drawing.Size(784, 22);
            this.ss_GE.TabIndex = 1;
            this.ss_GE.Text = "statusStrip1";
            // 
            // barDockControlTop
            // 
            this.barDockControlTop.CausesValidation = false;
            this.barDockControlTop.Dock = System.Windows.Forms.DockStyle.Top;
            this.barDockControlTop.Location = new System.Drawing.Point(0, 0);
            this.barDockControlTop.Size = new System.Drawing.Size(784, 0);
            // 
            // barDockControlBottom
            // 
            this.barDockControlBottom.CausesValidation = false;
            this.barDockControlBottom.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.barDockControlBottom.Location = new System.Drawing.Point(0, 561);
            this.barDockControlBottom.Size = new System.Drawing.Size(784, 0);
            // 
            // barDockControlLeft
            // 
            this.barDockControlLeft.CausesValidation = false;
            this.barDockControlLeft.Dock = System.Windows.Forms.DockStyle.Left;
            this.barDockControlLeft.Location = new System.Drawing.Point(0, 0);
            this.barDockControlLeft.Size = new System.Drawing.Size(0, 561);
            // 
            // barDockControlRight
            // 
            this.barDockControlRight.CausesValidation = false;
            this.barDockControlRight.Dock = System.Windows.Forms.DockStyle.Right;
            this.barDockControlRight.Location = new System.Drawing.Point(784, 0);
            this.barDockControlRight.Size = new System.Drawing.Size(0, 561);
            // 
            // imageList1
            // 
            this.imageList1.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageList1.ImageStream")));
            this.imageList1.TransparentColor = System.Drawing.Color.Transparent;
            this.imageList1.Images.SetKeyName(0, "部门信息管理32X32.ico");
            this.imageList1.Images.SetKeyName(1, "传感器管理32X32.ico");
            this.imageList1.Images.SetKeyName(2, "工作面瓦斯涌出动态特征管理系统主程序Logo32X32.ico");
            this.imageList1.Images.SetKeyName(3, "监控数据分析32X32.ico");
            this.imageList1.Images.SetKeyName(4, "数据库设置32X32.ico");
            this.imageList1.Images.SetKeyName(5, "用户信息管理32X32.ico");
            this.imageList1.Images.SetKeyName(6, "用户组信息管理32X32.ico");
            this.imageList1.Images.SetKeyName(7, "帮助.png");
            this.imageList1.Images.SetKeyName(8, "关于.png");
            this.imageList1.Images.SetKeyName(9, "user_edit.png");
            // 
            // mniUserInfoManaFloat
            // 
            this.mniUserInfoManaFloat.Id = 46;
            this.mniUserInfoManaFloat.Name = "mniUserInfoManaFloat";
            // 
            // mniDepartmentFloat
            // 
            this.mniDepartmentFloat.Id = 47;
            this.mniDepartmentFloat.Name = "mniDepartmentFloat";
            // 
            // barButtonItem4
            // 
            this.barButtonItem4.Caption = "用户信息管理";
            this.barButtonItem4.Id = 17;
            this.barButtonItem4.Name = "barButtonItem4";
            // 
            // mniUserGroupInfoManaFloat
            // 
            this.mniUserGroupInfoManaFloat.Id = 48;
            this.mniUserGroupInfoManaFloat.Name = "mniUserGroupInfoManaFloat";
            // 
            // mniDatabaseSetFloat
            // 
            this.mniDatabaseSetFloat.Id = 49;
            this.mniDatabaseSetFloat.Name = "mniDatabaseSetFloat";
            // 
            // mniUserLoginInfoManaFloat
            // 
            this.mniUserLoginInfoManaFloat.Id = 50;
            this.mniUserLoginInfoManaFloat.Name = "mniUserLoginInfoManaFloat";
            // 
            // _DXbtAbout
            // 
            this._DXbtAbout.Id = 51;
            this._DXbtAbout.Name = "_DXbtAbout";
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
            // groupBox1
            // 
            this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)));
            this.groupBox1.Controls.Add(this._dgvData);
            this.groupBox1.Location = new System.Drawing.Point(9, 38);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(220, 499);
            this.groupBox1.TabIndex = 7;
            this.groupBox1.TabStop = false;
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
            this._dgvData.Size = new System.Drawing.Size(214, 479);
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
            // _btnStart
            // 
            this._btnStart.Location = new System.Drawing.Point(15, 9);
            this._btnStart.Name = "_btnStart";
            this._btnStart.Size = new System.Drawing.Size(82, 34);
            this._btnStart.TabIndex = 4;
            this._btnStart.Text = "开始";
            this._btnStart.UseVisualStyleBackColor = true;
            this._btnStart.Click += new System.EventHandler(this._btnStart_Click);
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.label1);
            this.panel1.Controls.Add(this.cbxSensors);
            this.panel1.Controls.Add(this.tabControl1);
            this.panel1.Controls.Add(this.btnSwitch);
            this.panel1.Controls.Add(this.btnExit);
            this.panel1.Controls.Add(this._btnStart);
            this.panel1.Controls.Add(this._ckbSetMarks);
            this.panel1.Controls.Add(this.groupBox1);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(784, 539);
            this.panel1.TabIndex = 2;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(321, 21);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(71, 12);
            this.label1.TabIndex = 30;
            this.label1.Text = "选择传感器:";
            // 
            // cbxSensors
            // 
            this.cbxSensors.FormattingEnabled = true;
            this.cbxSensors.Location = new System.Drawing.Point(400, 16);
            this.cbxSensors.Name = "cbxSensors";
            this.cbxSensors.Size = new System.Drawing.Size(104, 20);
            this.cbxSensors.TabIndex = 29;
            this.cbxSensors.SelectedIndexChanged += new System.EventHandler(this.cbxSensors_SelectedIndexChanged);
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Controls.Add(this.tabPage3);
            this.tabControl1.Location = new System.Drawing.Point(232, 55);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(549, 481);
            this.tabControl1.TabIndex = 28;
            // 
            // tabPage1
            // 
            this.tabPage1.BackColor = System.Drawing.SystemColors.ButtonFace;
            this.tabPage1.Controls.Add(this.tChartT2);
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(541, 455);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "T2瓦斯浓度平均增加值Q";
            // 
            // tChartT2
            // 
            this.tChartT2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            // 
            // 
            // 
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
            this.tChartT2.Axes.Bottom.Labels.CustomSize = 27;
            this.tChartT2.Axes.Bottom.Labels.DateTimeFormat = "yyyy/MM/dd HH:mm:ss";
            this.tChartT2.Axes.Bottom.Labels.MultiLine = true;
            this.tChartT2.Axes.Bottom.Labels.RoundFirstLabel = false;
            this.tChartT2.Axes.Bottom.Labels.Separation = 100;
            // 
            // 
            // 
            this.tChartT2.Axes.Bottom.Title.Lines = new string[] {
        ""};
            // 
            // 
            // 
            // 
            // 
            // 
            this.tChartT2.Axes.Left.Labels.RoundFirstLabel = false;
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
            this.tChartT2.Axes.Right.Labels.RoundFirstLabel = false;
            this.tChartT2.Axes.Right.Visible = false;
            // 
            // 
            // 
            // 
            // 
            // 
            this.tChartT2.Axes.Top.Labels.RoundFirstLabel = false;
            this.tChartT2.Axes.Top.Visible = false;
            this.tChartT2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(235)))), ((int)(((byte)(236)))), ((int)(((byte)(239)))));
            this.tChartT2.Cursor = System.Windows.Forms.Cursors.Default;
            // 
            // 
            // 
            this.tChartT2.Header.Lines = new string[] {
        "瓦斯浓度平均增加值Q"};
            // 
            // 
            // 
            this.tChartT2.Legend.CurrentPage = false;
            // 
            // 
            // 
            // 
            // 
            // 
            this.tChartT2.Legend.Shadow.Brush.Color = System.Drawing.Color.FromArgb(((int)(((byte)(254)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.tChartT2.Legend.Visible = false;
            this.tChartT2.Location = new System.Drawing.Point(3, 6);
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
            this.tChartT2.Panel.Brush.Color = System.Drawing.Color.FromArgb(((int)(((byte)(235)))), ((int)(((byte)(236)))), ((int)(((byte)(239)))));
            // 
            // 
            // 
            // 
            // 
            // 
            this.tChartT2.Panel.Shadow.Brush.Color = System.Drawing.Color.FromArgb(((int)(((byte)(254)))), ((int)(((byte)(169)))), ((int)(((byte)(169)))), ((int)(((byte)(169)))));
            this.tChartT2.Panel.Shadow.Height = 0;
            this.tChartT2.Panel.Shadow.Width = 0;
            this.tChartT2.Series.Add(this.fastLine2);
            this.tChartT2.Size = new System.Drawing.Size(537, 443);
            this.tChartT2.TabIndex = 6;
            // 
            // 
            // 
            // 
            // 
            // 
            this.tChartT2.Walls.Back.Visible = false;
            // 
            // 
            // 
            this.tChartT2.Walls.Bottom.Visible = false;
            // 
            // 
            // 
            this.tChartT2.Walls.Left.Visible = false;
            // 
            // 
            // 
            // 
            // 
            // 
            this.tChartT2.Walls.Right.Brush.Color = System.Drawing.Color.Silver;
            this.tChartT2.Walls.Right.Visible = false;
            this.tChartT2.Walls.Visible = false;
            // 
            // fastLine2
            // 
            // 
            // 
            // 
            this.fastLine2.LinePen.Color = System.Drawing.Color.Red;
            this.fastLine2.Title = "快速线1";
            // 
            // 
            // 
            this.fastLine2.XValues.DataMember = "X";
            this.fastLine2.XValues.DateTime = true;
            this.fastLine2.XValues.Order = Steema.TeeChart.Styles.ValueListOrder.Ascending;
            // 
            // 
            // 
            this.fastLine2.YValues.DataMember = "Y";
            // 
            // tabPage2
            // 
            this.tabPage2.BackColor = System.Drawing.SystemColors.ButtonFace;
            this.tabPage2.Controls.Add(this.tChartN);
            this.tabPage2.Location = new System.Drawing.Point(4, 22);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(541, 455);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "瓦斯浓度变化值N";
            // 
            // tChartN
            // 
            this.tChartN.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            // 
            // 
            // 
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
            this.tChartN.Axes.Bottom.Labels.CustomSize = 27;
            this.tChartN.Axes.Bottom.Labels.DateTimeFormat = "yyyy/MM/dd HH:mm:ss";
            this.tChartN.Axes.Bottom.Labels.MultiLine = true;
            this.tChartN.Axes.Bottom.Labels.RoundFirstLabel = false;
            this.tChartN.Axes.Bottom.Labels.Separation = 100;
            // 
            // 
            // 
            this.tChartN.Axes.Bottom.Title.Lines = new string[] {
        ""};
            // 
            // 
            // 
            // 
            // 
            // 
            this.tChartN.Axes.Left.Labels.RoundFirstLabel = false;
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
            this.tChartN.Axes.Right.Labels.RoundFirstLabel = false;
            this.tChartN.Axes.Right.Visible = false;
            // 
            // 
            // 
            // 
            // 
            // 
            this.tChartN.Axes.Top.Labels.RoundFirstLabel = false;
            this.tChartN.Axes.Top.Visible = false;
            this.tChartN.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(235)))), ((int)(((byte)(236)))), ((int)(((byte)(239)))));
            this.tChartN.Cursor = System.Windows.Forms.Cursors.Default;
            // 
            // 
            // 
            this.tChartN.Header.Lines = new string[] {
        "同一工序条件下瓦斯浓度变化值N"};
            // 
            // 
            // 
            this.tChartN.Legend.CurrentPage = false;
            // 
            // 
            // 
            // 
            // 
            // 
            this.tChartN.Legend.Shadow.Brush.Color = System.Drawing.Color.FromArgb(((int)(((byte)(254)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.tChartN.Legend.Visible = false;
            this.tChartN.Location = new System.Drawing.Point(0, 3);
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
            this.tChartN.Panel.Brush.Color = System.Drawing.Color.FromArgb(((int)(((byte)(235)))), ((int)(((byte)(236)))), ((int)(((byte)(239)))));
            // 
            // 
            // 
            // 
            // 
            // 
            this.tChartN.Panel.Shadow.Brush.Color = System.Drawing.Color.FromArgb(((int)(((byte)(254)))), ((int)(((byte)(169)))), ((int)(((byte)(169)))), ((int)(((byte)(169)))));
            this.tChartN.Panel.Shadow.Height = 0;
            this.tChartN.Panel.Shadow.Width = 0;
            this.tChartN.Series.Add(this.fastLine3);
            this.tChartN.Size = new System.Drawing.Size(535, 449);
            this.tChartN.TabIndex = 6;
            // 
            // 
            // 
            // 
            // 
            // 
            this.tChartN.Walls.Back.Visible = false;
            // 
            // 
            // 
            this.tChartN.Walls.Bottom.Visible = false;
            // 
            // 
            // 
            this.tChartN.Walls.Left.Visible = false;
            // 
            // 
            // 
            // 
            // 
            // 
            this.tChartN.Walls.Right.Brush.Color = System.Drawing.Color.Silver;
            this.tChartN.Walls.Right.Visible = false;
            this.tChartN.Walls.Visible = false;
            // 
            // fastLine3
            // 
            // 
            // 
            // 
            this.fastLine3.LinePen.Color = System.Drawing.Color.Red;
            this.fastLine3.Title = "快速线1";
            // 
            // 
            // 
            this.fastLine3.XValues.DataMember = "X";
            this.fastLine3.XValues.DateTime = true;
            this.fastLine3.XValues.Order = Steema.TeeChart.Styles.ValueListOrder.Ascending;
            // 
            // 
            // 
            this.fastLine3.YValues.DataMember = "Y";
            // 
            // tabPage3
            // 
            this.tabPage3.BackColor = System.Drawing.SystemColors.ButtonFace;
            this.tabPage3.Controls.Add(this.tChartM);
            this.tabPage3.Location = new System.Drawing.Point(4, 22);
            this.tabPage3.Name = "tabPage3";
            this.tabPage3.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage3.Size = new System.Drawing.Size(541, 455);
            this.tabPage3.TabIndex = 2;
            this.tabPage3.Text = "瓦斯浓度原始数据M";
            // 
            // tChartM
            // 
            this.tChartM.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            // 
            // 
            // 
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
            this.tChartM.Axes.Bottom.Labels.CustomSize = 27;
            this.tChartM.Axes.Bottom.Labels.DateTimeFormat = "yyyy/MM/dd HH:mm:ss";
            this.tChartM.Axes.Bottom.Labels.MultiLine = true;
            this.tChartM.Axes.Bottom.Labels.RoundFirstLabel = false;
            this.tChartM.Axes.Bottom.Labels.Separation = 100;
            // 
            // 
            // 
            this.tChartM.Axes.Bottom.Title.Lines = new string[] {
        ""};
            // 
            // 
            // 
            // 
            // 
            // 
            this.tChartM.Axes.Left.Labels.RoundFirstLabel = false;
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
            this.tChartM.Axes.Right.Labels.RoundFirstLabel = false;
            this.tChartM.Axes.Right.Visible = false;
            // 
            // 
            // 
            // 
            // 
            // 
            this.tChartM.Axes.Top.Labels.RoundFirstLabel = false;
            this.tChartM.Axes.Top.Visible = false;
            this.tChartM.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(235)))), ((int)(((byte)(236)))), ((int)(((byte)(239)))));
            this.tChartM.Cursor = System.Windows.Forms.Cursors.Default;
            // 
            // 
            // 
            this.tChartM.Header.Lines = new string[] {
        "监控系统原始数据M"};
            // 
            // 
            // 
            this.tChartM.Legend.CurrentPage = false;
            // 
            // 
            // 
            // 
            // 
            // 
            this.tChartM.Legend.Shadow.Brush.Color = System.Drawing.Color.FromArgb(((int)(((byte)(254)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.tChartM.Legend.Visible = false;
            this.tChartM.Location = new System.Drawing.Point(3, 6);
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
            this.tChartM.Panel.Brush.Color = System.Drawing.Color.FromArgb(((int)(((byte)(235)))), ((int)(((byte)(236)))), ((int)(((byte)(239)))));
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
            this.tChartM.Size = new System.Drawing.Size(535, 446);
            this.tChartM.TabIndex = 26;
            // 
            // 
            // 
            // 
            // 
            // 
            this.tChartM.Walls.Back.Visible = false;
            // 
            // 
            // 
            this.tChartM.Walls.Bottom.Visible = false;
            // 
            // 
            // 
            this.tChartM.Walls.Left.Visible = false;
            // 
            // 
            // 
            // 
            // 
            // 
            this.tChartM.Walls.Right.Brush.Color = System.Drawing.Color.Silver;
            this.tChartM.Walls.Right.Visible = false;
            this.tChartM.Walls.Visible = false;
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
            this.fastLine1.XValues.DateTime = true;
            this.fastLine1.XValues.Order = Steema.TeeChart.Styles.ValueListOrder.Ascending;
            // 
            // 
            // 
            this.fastLine1.YValues.DataMember = "Y";
            // 
            // btnSwitch
            // 
            this.btnSwitch.Location = new System.Drawing.Point(112, 9);
            this.btnSwitch.Name = "btnSwitch";
            this.btnSwitch.Size = new System.Drawing.Size(82, 34);
            this.btnSwitch.TabIndex = 4;
            this.btnSwitch.Text = "切换";
            this.btnSwitch.UseVisualStyleBackColor = true;
            this.btnSwitch.Click += new System.EventHandler(this.btnSwitch_Click);
            // 
            // btnExit
            // 
            this.btnExit.Location = new System.Drawing.Point(210, 9);
            this.btnExit.Name = "btnExit";
            this.btnExit.Size = new System.Drawing.Size(82, 34);
            this.btnExit.TabIndex = 4;
            this.btnExit.Text = "退出";
            this.btnExit.UseVisualStyleBackColor = true;
            this.btnExit.Click += new System.EventHandler(this.btnExit_Click);
            // 
            // _ckbSetMarks
            // 
            this._ckbSetMarks.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this._ckbSetMarks.AutoSize = true;
            this._ckbSetMarks.Location = new System.Drawing.Point(676, 17);
            this._ckbSetMarks.Name = "_ckbSetMarks";
            this._ckbSetMarks.Size = new System.Drawing.Size(96, 16);
            this._ckbSetMarks.TabIndex = 27;
            this._ckbSetMarks.Text = "显示数据标记";
            this._ckbSetMarks.UseVisualStyleBackColor = true;
            this._ckbSetMarks.Click += new System.EventHandler(this._ckbSetMarks_Click);
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
            // CurveMonitor
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(784, 561);
            this.ControlBox = false;
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.ss_GE);
            this.Controls.Add(this.barDockControlLeft);
            this.Controls.Add(this.barDockControlRight);
            this.Controls.Add(this.barDockControlBottom);
            this.Controls.Add(this.barDockControlTop);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.IsMdiContainer = true;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "CurveMonitor";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "KJGEW110 工作面瓦斯涌出动态特征管理系统";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemHyperLinkEdit1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemFontEdit1)).EndInit();
            this.groupBox1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this._dgvData)).EndInit();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabPage2.ResumeLayout(false);
            this.tabPage3.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.StatusStrip ss_GE;
        private DevExpress.XtraBars.BarDockControl barDockControlTop;
        private DevExpress.XtraBars.BarDockControl barDockControlBottom;
        private DevExpress.XtraBars.BarDockControl barDockControlLeft;
        private DevExpress.XtraBars.BarDockControl barDockControlRight;
        private System.Windows.Forms.ImageList imageList1;
        private DevExpress.XtraBars.BarButtonItem mniUserInfoManaFloat;
        private DevExpress.XtraBars.BarButtonItem mniDepartmentFloat;
        private DevExpress.XtraBars.BarButtonItem barButtonItem4;
        private DevExpress.XtraBars.BarButtonItem mniUserGroupInfoManaFloat;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button _btnStart;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.DataGridView _dgvData;
        private System.Windows.Forms.DataGridViewTextBoxColumn _Value;
        private System.Windows.Forms.DataGridViewTextBoxColumn _Time;
        private DevExpress.XtraBars.BarButtonItem barButtonItem6;
        private DevExpress.XtraBars.BarButtonItem mniDatabaseSetFloat;
        private DevExpress.XtraBars.BarButtonItem mniUserLoginInfoManaFloat;
        private DevExpress.XtraEditors.Repository.RepositoryItemHyperLinkEdit repositoryItemHyperLinkEdit1;
        private DevExpress.XtraEditors.Repository.RepositoryItemFontEdit repositoryItemFontEdit1;
        private System.Windows.Forms.Timer timer1;
        private Steema.TeeChart.TChart tChartN;
        private Steema.TeeChart.Styles.FastLine fastLine3;
        private DevExpress.XtraBars.BarButtonItem _DXbtAbout;
        private Steema.TeeChart.TChart tChartM;
        private Steema.TeeChart.Styles.FastLine fastLine1;
        private Steema.TeeChart.TChart tChartT2;
        private Steema.TeeChart.Styles.FastLine fastLine2;
        private System.Windows.Forms.CheckBox _ckbSetMarks;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.TabPage tabPage3;
        private System.Windows.Forms.Button btnExit;
        private System.Windows.Forms.ComboBox cbxSensors;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnSwitch;
    }
}