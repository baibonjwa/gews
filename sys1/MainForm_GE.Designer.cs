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
            this.components = new System.ComponentModel.Container();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainFormGe));
            this.ss_GE = new System.Windows.Forms.StatusStrip();
            this.barManager1 = new DevExpress.XtraBars.BarManager(this.components);
            this.bar2 = new DevExpress.XtraBars.Bar();
            this.barSubItem4 = new DevExpress.XtraBars.BarSubItem();
            this.bbiExit = new DevExpress.XtraBars.BarButtonItem();
            this.barSubItem5 = new DevExpress.XtraBars.BarSubItem();
            this.bbiBadDataEdit = new DevExpress.XtraBars.BarButtonItem();
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
            this.mniHelpFile = new DevExpress.XtraBars.BarButtonItem();
            this._DXbtAbout = new DevExpress.XtraBars.BarButtonItem();
            this.mniSystemSet = new DevExpress.XtraBars.Bar();
            this.mniDatabaseSetFloat = new DevExpress.XtraBars.BarButtonItem();
            this.mniUserInfoManaFloat = new DevExpress.XtraBars.BarButtonItem();
            this.mniDepartmentFloat = new DevExpress.XtraBars.BarButtonItem();
            this.mniUserLoginInfoManaFloat = new DevExpress.XtraBars.BarButtonItem();
            this.mniUserGroupInfoManaFloat = new DevExpress.XtraBars.BarButtonItem();
            this.barDockControlTop = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlBottom = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlLeft = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlRight = new DevExpress.XtraBars.BarDockControl();
            this.imageList1 = new System.Windows.Forms.ImageList(this.components);
            this.barButtonItem4 = new DevExpress.XtraBars.BarButtonItem();
            this.repositoryItemHyperLinkEdit1 = new DevExpress.XtraEditors.Repository.RepositoryItemHyperLinkEdit();
            this.repositoryItemFontEdit1 = new DevExpress.XtraEditors.Repository.RepositoryItemFontEdit();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this._dgvData = new System.Windows.Forms.DataGridView();
            this._Value = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this._Time = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.gbTunnel = new System.Windows.Forms.GroupBox();
            this.label7 = new System.Windows.Forms.Label();
            this.selectTunnelSimple1 = new LibCommonForm.SelectTunnelSimple();
            this._lblProbeName = new System.Windows.Forms.Label();
            this._lstProbeName = new System.Windows.Forms.ListBox();
            this._lstProbeStyle = new System.Windows.Forms.ListBox();
            this._lblProbeStyle = new System.Windows.Forms.Label();
            this._btnQuery = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this._rbtnHistory = new System.Windows.Forms.RadioButton();
            this.gb03 = new System.Windows.Forms.GroupBox();
            this._lblLoading = new System.Windows.Forms.Label();
            this._btnAfterDay = new System.Windows.Forms.Button();
            this._btnNow = new System.Windows.Forms.Button();
            this._btnBeforeDay = new System.Windows.Forms.Button();
            this._dateTimeEnd = new System.Windows.Forms.DateTimePicker();
            this._dateTimeStart = new System.Windows.Forms.DateTimePicker();
            this.gb01 = new System.Windows.Forms.GroupBox();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this._rbtnRealtime = new System.Windows.Forms.RadioButton();
            this.picBoxLogo = new System.Windows.Forms.PictureBox();
            this.panel1 = new System.Windows.Forms.Panel();
            this._ckbSetMarks3 = new System.Windows.Forms.CheckBox();
            this._ckbSetMarks2 = new System.Windows.Forms.CheckBox();
            this._ckbSetMarks1 = new System.Windows.Forms.CheckBox();
            this.tChartN = new Steema.TeeChart.TChart();
            this.fastLine3 = new Steema.TeeChart.Styles.FastLine();
            this.tChartT2 = new Steema.TeeChart.TChart();
            this.fastLine2 = new Steema.TeeChart.Styles.FastLine();
            this.tChartM = new Steema.TeeChart.TChart();
            this.fastLine1 = new Steema.TeeChart.Styles.FastLine();
            this.barButtonItem6 = new DevExpress.XtraBars.BarButtonItem();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.barManager1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemHyperLinkEdit1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemFontEdit1)).BeginInit();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this._dgvData)).BeginInit();
            this.gbTunnel.SuspendLayout();
            this.gb03.SuspendLayout();
            this.gb01.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picBoxLogo)).BeginInit();
            this.panel1.SuspendLayout();
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
            // barManager1
            // 
            this.barManager1.Bars.AddRange(new DevExpress.XtraBars.Bar[] {
            this.bar2,
            this.mniSystemSet});
            this.barManager1.DockControls.Add(this.barDockControlTop);
            this.barManager1.DockControls.Add(this.barDockControlBottom);
            this.barManager1.DockControls.Add(this.barDockControlLeft);
            this.barManager1.DockControls.Add(this.barDockControlRight);
            this.barManager1.Form = this;
            this.barManager1.Images = this.imageList1;
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
            this.barSubItem5,
            this.bbiBadDataEdit});
            this.barManager1.MainMenu = this.bar2;
            this.barManager1.MaxItemId = 45;
            this.barManager1.RepositoryItems.AddRange(new DevExpress.XtraEditors.Repository.RepositoryItem[] {
            this.repositoryItemHyperLinkEdit1,
            this.repositoryItemFontEdit1});
            // 
            // bar2
            // 
            this.bar2.BarName = "Main menu";
            this.bar2.DockCol = 0;
            this.bar2.DockRow = 0;
            this.bar2.DockStyle = DevExpress.XtraBars.BarDockStyle.Top;
            this.bar2.LinksPersistInfo.AddRange(new DevExpress.XtraBars.LinkPersistInfo[] {
            new DevExpress.XtraBars.LinkPersistInfo(this.barSubItem4),
            new DevExpress.XtraBars.LinkPersistInfo(this.barSubItem5),
            new DevExpress.XtraBars.LinkPersistInfo(this.barSubItem1),
            new DevExpress.XtraBars.LinkPersistInfo(this.barSubItem2),
            new DevExpress.XtraBars.LinkPersistInfo(this.barSubItem3)});
            this.bar2.OptionsBar.AllowQuickCustomization = false;
            this.bar2.OptionsBar.DrawDragBorder = false;
            this.bar2.OptionsBar.MultiLine = true;
            this.bar2.OptionsBar.UseWholeRow = true;
            this.bar2.Text = "主菜单";
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
            // barSubItem5
            // 
            this.barSubItem5.Caption = "编辑";
            this.barSubItem5.Id = 43;
            this.barSubItem5.LinksPersistInfo.AddRange(new DevExpress.XtraBars.LinkPersistInfo[] {
            new DevExpress.XtraBars.LinkPersistInfo(this.bbiBadDataEdit)});
            this.barSubItem5.Name = "barSubItem5";
            // 
            // bbiBadDataEdit
            // 
            this.bbiBadDataEdit.Caption = "智能识别";
            this.bbiBadDataEdit.Id = 44;
            this.bbiBadDataEdit.Name = "bbiBadDataEdit";
            this.bbiBadDataEdit.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.bbiBadDataEdit_ItemClick);
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
            new DevExpress.XtraBars.LinkPersistInfo(this.mniHelpFile),
            new DevExpress.XtraBars.LinkPersistInfo(this._DXbtAbout)});
            this.barSubItem3.Name = "barSubItem3";
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
            // mniSystemSet
            // 
            this.mniSystemSet.BarName = "Custom 2";
            this.mniSystemSet.DockCol = 0;
            this.mniSystemSet.DockRow = 1;
            this.mniSystemSet.DockStyle = DevExpress.XtraBars.BarDockStyle.Top;
            this.mniSystemSet.FloatLocation = new System.Drawing.Point(32, 220);
            this.mniSystemSet.FloatSize = new System.Drawing.Size(507, 31);
            this.mniSystemSet.LinksPersistInfo.AddRange(new DevExpress.XtraBars.LinkPersistInfo[] {
            new DevExpress.XtraBars.LinkPersistInfo(DevExpress.XtraBars.BarLinkUserDefines.PaintStyle, this.mniDatabaseSetFloat, DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph),
            new DevExpress.XtraBars.LinkPersistInfo(DevExpress.XtraBars.BarLinkUserDefines.PaintStyle, this.mniUserInfoManaFloat, DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph),
            new DevExpress.XtraBars.LinkPersistInfo(DevExpress.XtraBars.BarLinkUserDefines.PaintStyle, this.mniDepartmentFloat, DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph),
            new DevExpress.XtraBars.LinkPersistInfo(DevExpress.XtraBars.BarLinkUserDefines.PaintStyle, this.mniUserLoginInfoManaFloat, DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph),
            new DevExpress.XtraBars.LinkPersistInfo(DevExpress.XtraBars.BarLinkUserDefines.PaintStyle, this.mniUserGroupInfoManaFloat, DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph)});
            this.mniSystemSet.OptionsBar.AllowQuickCustomization = false;
            this.mniSystemSet.OptionsBar.AllowRename = true;
            this.mniSystemSet.Text = "系统设置";
            this.mniSystemSet.DockChanged += new System.EventHandler(this.mniSystemSet_DockChanged);
            // 
            // mniDatabaseSetFloat
            // 
            this.mniDatabaseSetFloat.Caption = "数据库设置";
            this.mniDatabaseSetFloat.Id = 28;
            this.mniDatabaseSetFloat.ImageIndex = 4;
            this.mniDatabaseSetFloat.Name = "mniDatabaseSetFloat";
            this.mniDatabaseSetFloat.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.mniDatabaseSetFloat_ItemClick);
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
            // mniUserLoginInfoManaFloat
            // 
            this.mniUserLoginInfoManaFloat.Caption = "用户信息管理";
            this.mniUserLoginInfoManaFloat.Id = 29;
            this.mniUserLoginInfoManaFloat.ImageIndex = 5;
            this.mniUserLoginInfoManaFloat.Name = "mniUserLoginInfoManaFloat";
            this.mniUserLoginInfoManaFloat.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.mniUserLoginInfoManaFloat_ItemClick);
            // 
            // mniUserGroupInfoManaFloat
            // 
            this.mniUserGroupInfoManaFloat.Caption = "用户组信息管理";
            this.mniUserGroupInfoManaFloat.Id = 18;
            this.mniUserGroupInfoManaFloat.ImageIndex = 6;
            this.mniUserGroupInfoManaFloat.Name = "mniUserGroupInfoManaFloat";
            this.mniUserGroupInfoManaFloat.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.mniUserGroupInfoManaFloat_ItemClick);
            // 
            // barDockControlTop
            // 
            this.barDockControlTop.CausesValidation = false;
            this.barDockControlTop.Dock = System.Windows.Forms.DockStyle.Top;
            this.barDockControlTop.Location = new System.Drawing.Point(0, 0);
            this.barDockControlTop.Size = new System.Drawing.Size(1162, 55);
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
            this.barDockControlLeft.Location = new System.Drawing.Point(0, 55);
            this.barDockControlLeft.Size = new System.Drawing.Size(0, 580);
            // 
            // barDockControlRight
            // 
            this.barDockControlRight.CausesValidation = false;
            this.barDockControlRight.Dock = System.Windows.Forms.DockStyle.Right;
            this.barDockControlRight.Location = new System.Drawing.Point(1162, 55);
            this.barDockControlRight.Size = new System.Drawing.Size(0, 580);
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
            // barButtonItem4
            // 
            this.barButtonItem4.Caption = "用户信息管理";
            this.barButtonItem4.Id = 17;
            this.barButtonItem4.Name = "barButtonItem4";
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
            this.groupBox1.Location = new System.Drawing.Point(15, 382);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(218, 172);
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
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Tahoma", 9F);
            dataGridViewCellStyle1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(32)))), ((int)(((byte)(31)))), ((int)(((byte)(53)))));
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this._dgvData.DefaultCellStyle = dataGridViewCellStyle1;
            this._dgvData.Dock = System.Windows.Forms.DockStyle.Fill;
            this._dgvData.Location = new System.Drawing.Point(3, 17);
            this._dgvData.Name = "_dgvData";
            this._dgvData.ReadOnly = true;
            this._dgvData.RowHeadersVisible = false;
            this._dgvData.RowTemplate.Height = 23;
            this._dgvData.Size = new System.Drawing.Size(212, 152);
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
            // gbTunnel
            // 
            this.gbTunnel.Controls.Add(this.label7);
            this.gbTunnel.Controls.Add(this.selectTunnelSimple1);
            this.gbTunnel.Controls.Add(this._lblProbeName);
            this.gbTunnel.Controls.Add(this._lstProbeName);
            this.gbTunnel.Controls.Add(this._lstProbeStyle);
            this.gbTunnel.Controls.Add(this._lblProbeStyle);
            this.gbTunnel.Location = new System.Drawing.Point(15, 60);
            this.gbTunnel.Name = "gbTunnel";
            this.gbTunnel.Size = new System.Drawing.Size(219, 151);
            this.gbTunnel.TabIndex = 8;
            this.gbTunnel.TabStop = false;
            this.gbTunnel.Text = "【传感器选择】";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(101, 98);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(17, 12);
            this.label7.TabIndex = 23;
            this.label7.Text = ">>";
            // 
            // selectTunnelSimple1
            // 
            this.selectTunnelSimple1.Location = new System.Drawing.Point(11, 15);
            this.selectTunnelSimple1.Name = "selectTunnelSimple1";
            this.selectTunnelSimple1.SelectedTunnel = null;
            this.selectTunnelSimple1.Size = new System.Drawing.Size(204, 33);
            this.selectTunnelSimple1.TabIndex = 0;
            // 
            // _lblProbeName
            // 
            this._lblProbeName.AutoSize = true;
            this._lblProbeName.Location = new System.Drawing.Point(122, 51);
            this._lblProbeName.Name = "_lblProbeName";
            this._lblProbeName.Size = new System.Drawing.Size(65, 12);
            this._lblProbeName.TabIndex = 21;
            this._lblProbeName.Text = "传感器名称";
            // 
            // _lstProbeName
            // 
            this._lstProbeName.FormattingEnabled = true;
            this._lstProbeName.ItemHeight = 12;
            this._lstProbeName.Location = new System.Drawing.Point(130, 65);
            this._lstProbeName.Name = "_lstProbeName";
            this._lstProbeName.Size = new System.Drawing.Size(79, 76);
            this._lstProbeName.TabIndex = 22;
            this._lstProbeName.MouseUp += new System.Windows.Forms.MouseEventHandler(this._lstProbeName_SelectedIndexChanged);
            // 
            // _lstProbeStyle
            // 
            this._lstProbeStyle.FormattingEnabled = true;
            this._lstProbeStyle.ItemHeight = 12;
            this._lstProbeStyle.Location = new System.Drawing.Point(16, 65);
            this._lstProbeStyle.Name = "_lstProbeStyle";
            this._lstProbeStyle.Size = new System.Drawing.Size(80, 76);
            this._lstProbeStyle.TabIndex = 20;
            this._lstProbeStyle.MouseUp += new System.Windows.Forms.MouseEventHandler(this._lstProbeStyle_SelectedIndexChanged);
            // 
            // _lblProbeStyle
            // 
            this._lblProbeStyle.AutoSize = true;
            this._lblProbeStyle.Location = new System.Drawing.Point(18, 51);
            this._lblProbeStyle.Name = "_lblProbeStyle";
            this._lblProbeStyle.Size = new System.Drawing.Size(65, 12);
            this._lblProbeStyle.TabIndex = 19;
            this._lblProbeStyle.Text = "传感器类型";
            // 
            // _btnQuery
            // 
            this._btnQuery.Enabled = false;
            this._btnQuery.Location = new System.Drawing.Point(147, 94);
            this._btnQuery.Name = "_btnQuery";
            this._btnQuery.Size = new System.Drawing.Size(63, 21);
            this._btnQuery.TabIndex = 5;
            this._btnQuery.Text = "查询";
            this._btnQuery.UseVisualStyleBackColor = true;
            this._btnQuery.Click += new System.EventHandler(this._btnQuery_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(11, 45);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(65, 12);
            this.label3.TabIndex = 3;
            this.label3.Text = "结束时间：";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(11, 15);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(65, 12);
            this.label1.TabIndex = 1;
            this.label1.Text = "开始时间：";
            // 
            // _rbtnHistory
            // 
            this._rbtnHistory.AutoSize = true;
            this._rbtnHistory.Location = new System.Drawing.Point(123, 11);
            this._rbtnHistory.Name = "_rbtnHistory";
            this._rbtnHistory.Size = new System.Drawing.Size(95, 16);
            this._rbtnHistory.TabIndex = 0;
            this._rbtnHistory.Text = "历史数据查询";
            this._rbtnHistory.UseVisualStyleBackColor = true;
            this._rbtnHistory.Click += new System.EventHandler(this._rbtnHistory_Click);
            // 
            // gb03
            // 
            this.gb03.Controls.Add(this._lblLoading);
            this.gb03.Controls.Add(this._btnAfterDay);
            this.gb03.Controls.Add(this._btnNow);
            this.gb03.Controls.Add(this._btnBeforeDay);
            this.gb03.Controls.Add(this._dateTimeEnd);
            this.gb03.Controls.Add(this._dateTimeStart);
            this.gb03.Controls.Add(this.label1);
            this.gb03.Controls.Add(this.label3);
            this.gb03.Controls.Add(this._btnQuery);
            this.gb03.Location = new System.Drawing.Point(15, 258);
            this.gb03.Name = "gb03";
            this.gb03.Size = new System.Drawing.Size(218, 119);
            this.gb03.TabIndex = 23;
            this.gb03.TabStop = false;
            // 
            // _lblLoading
            // 
            this._lblLoading.AutoSize = true;
            this._lblLoading.ForeColor = System.Drawing.Color.Red;
            this._lblLoading.Location = new System.Drawing.Point(30, 99);
            this._lblLoading.Name = "_lblLoading";
            this._lblLoading.Size = new System.Drawing.Size(83, 12);
            this._lblLoading.TabIndex = 9;
            this._lblLoading.Text = "数据加载中...";
            this._lblLoading.Visible = false;
            // 
            // _btnAfterDay
            // 
            this._btnAfterDay.Enabled = false;
            this._btnAfterDay.Location = new System.Drawing.Point(147, 67);
            this._btnAfterDay.Name = "_btnAfterDay";
            this._btnAfterDay.Size = new System.Drawing.Size(63, 21);
            this._btnAfterDay.TabIndex = 8;
            this._btnAfterDay.Text = "后一天>>";
            this._btnAfterDay.UseVisualStyleBackColor = true;
            this._btnAfterDay.Click += new System.EventHandler(this._btnAfterDay_Click);
            // 
            // _btnNow
            // 
            this._btnNow.Enabled = false;
            this._btnNow.Location = new System.Drawing.Point(83, 67);
            this._btnNow.Name = "_btnNow";
            this._btnNow.Size = new System.Drawing.Size(59, 21);
            this._btnNow.TabIndex = 8;
            this._btnNow.Text = "当前日期";
            this._btnNow.UseVisualStyleBackColor = true;
            this._btnNow.Click += new System.EventHandler(this._btnNow_Click);
            // 
            // _btnBeforeDay
            // 
            this._btnBeforeDay.Enabled = false;
            this._btnBeforeDay.Location = new System.Drawing.Point(14, 67);
            this._btnBeforeDay.Name = "_btnBeforeDay";
            this._btnBeforeDay.Size = new System.Drawing.Size(64, 21);
            this._btnBeforeDay.TabIndex = 8;
            this._btnBeforeDay.Text = "<<前一天";
            this._btnBeforeDay.UseVisualStyleBackColor = true;
            this._btnBeforeDay.Click += new System.EventHandler(this._btnBeforeDay_Click);
            // 
            // _dateTimeEnd
            // 
            this._dateTimeEnd.CustomFormat = "yyyy/MM/dd   HH:mm:ss";
            this._dateTimeEnd.Enabled = false;
            this._dateTimeEnd.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this._dateTimeEnd.Location = new System.Drawing.Point(61, 43);
            this._dateTimeEnd.Name = "_dateTimeEnd";
            this._dateTimeEnd.Size = new System.Drawing.Size(151, 21);
            this._dateTimeEnd.TabIndex = 7;
            this._dateTimeEnd.Value = new System.DateTime(2014, 5, 7, 23, 59, 59, 0);
            // 
            // _dateTimeStart
            // 
            this._dateTimeStart.CustomFormat = "yyyy/MM/dd   HH:mm:ss";
            this._dateTimeStart.Enabled = false;
            this._dateTimeStart.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this._dateTimeStart.Location = new System.Drawing.Point(61, 14);
            this._dateTimeStart.Name = "_dateTimeStart";
            this._dateTimeStart.Size = new System.Drawing.Size(151, 21);
            this._dateTimeStart.TabIndex = 6;
            this._dateTimeStart.Value = new System.DateTime(2014, 5, 7, 0, 0, 0, 0);
            // 
            // gb01
            // 
            this.gb01.Controls.Add(this.groupBox3);
            this.gb01.Controls.Add(this._rbtnHistory);
            this.gb01.Controls.Add(this._rbtnRealtime);
            this.gb01.Location = new System.Drawing.Point(15, 216);
            this.gb01.Name = "gb01";
            this.gb01.Size = new System.Drawing.Size(218, 32);
            this.gb01.TabIndex = 21;
            this.gb01.TabStop = false;
            // 
            // groupBox3
            // 
            this.groupBox3.Location = new System.Drawing.Point(3, 40);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(221, 44);
            this.groupBox3.TabIndex = 24;
            this.groupBox3.TabStop = false;
            // 
            // _rbtnRealtime
            // 
            this._rbtnRealtime.AutoSize = true;
            this._rbtnRealtime.Checked = true;
            this._rbtnRealtime.Location = new System.Drawing.Point(15, 11);
            this._rbtnRealtime.Name = "_rbtnRealtime";
            this._rbtnRealtime.Size = new System.Drawing.Size(95, 16);
            this._rbtnRealtime.TabIndex = 0;
            this._rbtnRealtime.TabStop = true;
            this._rbtnRealtime.Text = "实时数据监控";
            this._rbtnRealtime.UseVisualStyleBackColor = true;
            this._rbtnRealtime.Click += new System.EventHandler(this._rbtnRealtime_Click_1);
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
            this.panel1.Controls.Add(this._ckbSetMarks2);
            this.panel1.Controls.Add(this._ckbSetMarks1);
            this.panel1.Controls.Add(this.tChartN);
            this.panel1.Controls.Add(this.tChartT2);
            this.panel1.Controls.Add(this.tChartM);
            this.panel1.Controls.Add(this.gb03);
            this.panel1.Controls.Add(this.picBoxLogo);
            this.panel1.Controls.Add(this.gb01);
            this.panel1.Controls.Add(this.gbTunnel);
            this.panel1.Controls.Add(this.groupBox1);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(0, 55);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1162, 558);
            this.panel1.TabIndex = 2;
            // 
            // _ckbSetMarks3
            // 
            this._ckbSetMarks3.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this._ckbSetMarks3.AutoSize = true;
            this._ckbSetMarks3.Location = new System.Drawing.Point(1065, 237);
            this._ckbSetMarks3.Name = "_ckbSetMarks3";
            this._ckbSetMarks3.Size = new System.Drawing.Size(96, 16);
            this._ckbSetMarks3.TabIndex = 27;
            this._ckbSetMarks3.Text = "显示数据标记";
            this._ckbSetMarks3.UseVisualStyleBackColor = true;
            this._ckbSetMarks3.Click += new System.EventHandler(this._ckbSetMarks3_Click);
            // 
            // _ckbSetMarks2
            // 
            this._ckbSetMarks2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this._ckbSetMarks2.AutoSize = true;
            this._ckbSetMarks2.Location = new System.Drawing.Point(1065, 75);
            this._ckbSetMarks2.Name = "_ckbSetMarks2";
            this._ckbSetMarks2.Size = new System.Drawing.Size(96, 16);
            this._ckbSetMarks2.TabIndex = 27;
            this._ckbSetMarks2.Text = "显示数据标记";
            this._ckbSetMarks2.UseVisualStyleBackColor = true;
            this._ckbSetMarks2.Click += new System.EventHandler(this._ckbSetMarks2_Click);
            // 
            // _ckbSetMarks1
            // 
            this._ckbSetMarks1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this._ckbSetMarks1.AutoSize = true;
            this._ckbSetMarks1.Location = new System.Drawing.Point(1065, 409);
            this._ckbSetMarks1.Name = "_ckbSetMarks1";
            this._ckbSetMarks1.Size = new System.Drawing.Size(96, 16);
            this._ckbSetMarks1.TabIndex = 27;
            this._ckbSetMarks1.Text = "显示数据标记";
            this._ckbSetMarks1.UseVisualStyleBackColor = true;
            this._ckbSetMarks1.Click += new System.EventHandler(this._ckbSetMarks1_Click);
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
            this.tChartN.Axes.Bottom.Ticks.Length = 10;
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
            this.tChartN.Location = new System.Drawing.Point(238, 230);
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
            this.tChartN.Size = new System.Drawing.Size(915, 143);
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
            // tChartT2
            // 
            this.tChartT2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            // 
            // 
            // 
            this.tChartT2.Aspect.ColorPaletteIndex = -1;
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
            this.tChartT2.Axes.Bottom.Grid.Color = System.Drawing.Color.Gray;
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
            this.tChartT2.Axes.Bottom.Ticks.Length = 2;
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
            this.tChartT2.Axes.Depth.Grid.Color = System.Drawing.Color.Gray;
            // 
            // 
            // 
            this.tChartT2.Axes.Depth.Ticks.Length = 2;
            // 
            // 
            // 
            // 
            // 
            // 
            this.tChartT2.Axes.Left.Grid.Color = System.Drawing.Color.Gray;
            // 
            // 
            // 
            this.tChartT2.Axes.Left.Labels.RoundFirstLabel = false;
            // 
            // 
            // 
            this.tChartT2.Axes.Left.Ticks.Length = 2;
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
            this.tChartT2.Axes.Right.Grid.Color = System.Drawing.Color.Gray;
            // 
            // 
            // 
            this.tChartT2.Axes.Right.Labels.RoundFirstLabel = false;
            // 
            // 
            // 
            this.tChartT2.Axes.Right.Ticks.Length = 2;
            this.tChartT2.Axes.Right.Visible = false;
            // 
            // 
            // 
            // 
            // 
            // 
            this.tChartT2.Axes.Top.Grid.Color = System.Drawing.Color.Gray;
            // 
            // 
            // 
            this.tChartT2.Axes.Top.Labels.RoundFirstLabel = false;
            // 
            // 
            // 
            this.tChartT2.Axes.Top.Ticks.Length = 2;
            this.tChartT2.Axes.Top.Visible = false;
            this.tChartT2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(235)))), ((int)(((byte)(236)))), ((int)(((byte)(239)))));
            this.tChartT2.Cursor = System.Windows.Forms.Cursors.Default;
            // 
            // 
            // 
            this.tChartT2.Header.Lines = new string[] {
        "T2瓦斯浓度平均增加值Q"};
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
            // 
            // 
            // 
            // 
            // 
            // 
            this.tChartT2.Legend.Symbol.Pen.Visible = false;
            this.tChartT2.Legend.Visible = false;
            this.tChartT2.Location = new System.Drawing.Point(237, 67);
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
            this.tChartT2.Panel.Gradient.EndColor = System.Drawing.Color.Yellow;
            this.tChartT2.Panel.Gradient.MiddleColor = System.Drawing.Color.Empty;
            this.tChartT2.Panel.Gradient.StartColor = System.Drawing.Color.White;
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
            this.tChartT2.Size = new System.Drawing.Size(915, 154);
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
            // tChartM
            // 
            this.tChartM.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
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
            this.tChartM.Axes.Bottom.Ticks.Length = 10;
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
            this.tChartM.Location = new System.Drawing.Point(238, 414);
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
            this.tChartM.Size = new System.Drawing.Size(915, 149);
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
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.MainForm_GE_FormClosing);
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.MainForm_GE_FormClosed);
            this.Load += new System.EventHandler(this.MainForm_GE_Load);
            ((System.ComponentModel.ISupportInitialize)(this.barManager1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemHyperLinkEdit1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemFontEdit1)).EndInit();
            this.groupBox1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this._dgvData)).EndInit();
            this.gbTunnel.ResumeLayout(false);
            this.gbTunnel.PerformLayout();
            this.gb03.ResumeLayout(false);
            this.gb03.PerformLayout();
            this.gb01.ResumeLayout(false);
            this.gb01.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picBoxLogo)).EndInit();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.StatusStrip ss_GE;
        private DevExpress.XtraBars.BarManager barManager1;
        private DevExpress.XtraBars.BarSubItem barSubItem1;
        private DevExpress.XtraBars.BarButtonItem mniSensorManagement;
        private DevExpress.XtraBars.BarButtonItem mniSensorDataManage;
        private DevExpress.XtraBars.BarSubItem barSubItem2;
        private DevExpress.XtraBars.BarButtonItem mniDatabaseSet;
        private DevExpress.XtraBars.BarButtonItem mniUserInfoMana;
        private DevExpress.XtraBars.BarButtonItem mniDepartment;
        private DevExpress.XtraBars.BarButtonItem mniUserLoginInfoMana;
        private DevExpress.XtraBars.BarButtonItem mniUserGroupInfoMana;
        private DevExpress.XtraBars.BarSubItem barSubItem3;
        private DevExpress.XtraBars.BarButtonItem mniHelpFile;
        private DevExpress.XtraBars.BarButtonItem mniAbout;
        private DevExpress.XtraBars.BarDockControl barDockControlTop;
        private DevExpress.XtraBars.BarDockControl barDockControlBottom;
        private DevExpress.XtraBars.BarDockControl barDockControlLeft;
        private DevExpress.XtraBars.BarDockControl barDockControlRight;
        private DevExpress.XtraBars.Bar mniSystemSet;
        private DevExpress.XtraBars.Bar bar2;
        private System.Windows.Forms.ImageList imageList1;
        private DevExpress.XtraBars.BarButtonItem mniUserInfoManaFloat;
        private DevExpress.XtraBars.BarButtonItem mniDepartmentFloat;
        private DevExpress.XtraBars.BarButtonItem barButtonItem4;
        private DevExpress.XtraBars.BarButtonItem mniUserGroupInfoManaFloat;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.PictureBox picBoxLogo;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label _lblProbeName;
        private System.Windows.Forms.ListBox _lstProbeStyle;
        private System.Windows.Forms.ListBox _lstProbeName;
        private System.Windows.Forms.Label _lblProbeStyle;
        private System.Windows.Forms.RadioButton _rbtnRealtime;
        private System.Windows.Forms.Button _btnQuery;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.RadioButton _rbtnHistory;
        private System.Windows.Forms.GroupBox gbTunnel;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.DataGridView _dgvData;
        private System.Windows.Forms.DataGridViewTextBoxColumn _Value;
        private System.Windows.Forms.DataGridViewTextBoxColumn _Time;
        private DevExpress.XtraBars.BarButtonItem barButtonItem6;
        private DevExpress.XtraBars.BarButtonItem mniDatabaseSetFloat;
        private DevExpress.XtraBars.BarButtonItem mniUserLoginInfoManaFloat;
        private DevExpress.XtraEditors.Repository.RepositoryItemHyperLinkEdit repositoryItemHyperLinkEdit1;
        private DevExpress.XtraEditors.Repository.RepositoryItemFontEdit repositoryItemFontEdit1;
        private System.Windows.Forms.GroupBox gb01;
        private System.Windows.Forms.GroupBox gb03;
        private System.Windows.Forms.DateTimePicker _dateTimeEnd;
        private System.Windows.Forms.DateTimePicker _dateTimeStart;
        private System.Windows.Forms.Timer timer1;
        private LibCommonForm.SelectTunnelSimple selectTunnelSimple1;
        private System.Windows.Forms.GroupBox groupBox3;
        private Steema.TeeChart.TChart tChartN;
        private Steema.TeeChart.Styles.FastLine fastLine3;
        private DevExpress.XtraBars.BarButtonItem _DXbtAbout;
        private Steema.TeeChart.TChart tChartM;
        private Steema.TeeChart.Styles.FastLine fastLine1;
        private System.Windows.Forms.CheckBox _ckbSetMarks1;
        private Steema.TeeChart.TChart tChartT2;
        private Steema.TeeChart.Styles.FastLine fastLine2;
        private System.Windows.Forms.CheckBox _ckbSetMarks2;
        private System.Windows.Forms.CheckBox _ckbSetMarks3;
        private System.Windows.Forms.Button _btnAfterDay;
        private System.Windows.Forms.Button _btnBeforeDay;
        private System.Windows.Forms.Button _btnNow;
        private DevExpress.XtraBars.BarButtonItem bbiMonitorSetting;
        private DevExpress.XtraBars.BarSubItem barSubItem4;
        private DevExpress.XtraBars.BarButtonItem bbiExit;
        private DevExpress.XtraBars.BarSubItem barSubItem5;
        private DevExpress.XtraBars.BarButtonItem bbiBadDataEdit;
        private System.Windows.Forms.Label _lblLoading;

    }
}