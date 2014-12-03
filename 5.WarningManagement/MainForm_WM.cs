using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using DevExpress.Data.Mask;
using DevExpress.XtraPrinting.Export.Pdf;
using GIS.HdProc;
using LibBusiness;
using LibCommonForm;
using LibDatabase;
using ESRI.ArcGIS.Controls;
using ESRI.ArcGIS.Geodatabase;
using GIS;
using ESRI.ArcGIS.DataSourcesGDB;
using ESRI.ArcGIS.SystemUI;
using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.Display;
using ESRI.ArcGIS.DisplayUI;
using GIS.Common;
using GIS.FileMenu;
using LibCommon;
using LibCommonControl;
using LibAbout;
using LibConfig;
using DevExpress.Skins;
using DevExpress.LookAndFeel;
using DevExpress.UserSkins;
using DevExpress.XtraEditors;
using LibPanels;
using _2.MiningScheduling;
using LibSocket;
using ESRI.ArcGIS.esriSystem;
namespace _5.WarningManagement
{
    public partial class MainForm_WM : MainFrm
    {
        //最新预警结果
        static private PreWarningLastedResultQuery _latestWarningResult = null;
        public static void CleanLatestWarningResult()
        {
            _latestWarningResult.Close();
            _latestWarningResult.Dispose();
            _latestWarningResult = null;
        }

        static public PreWarningLastedResultQuery GetLatestWarningResultInstance()
        {
            //单例模式
            if (_latestWarningResult == null)
            {
                _latestWarningResult = new PreWarningLastedResultQuery();
            }
            return _latestWarningResult;
        }

        static public void ReleaseLatestWarningResultInstance()
        {
            _latestWarningResult.Close();
            _latestWarningResult.Dispose();
            _latestWarningResult = null;
        }

        private GIS_FileMenu m_FileMenu = new GIS_FileMenu();

        private GIS.Warning.FlashWarningPoints m_flashWarningPoints = null;  //预警点闪烁command类


        public MainForm_WM()
        {
            ESRI.ArcGIS.RuntimeManager.Bind(ESRI.ArcGIS.ProductCode.EngineOrDesktop);
            IAoInitialize aoini = new AoInitializeClass();
            esriLicenseStatus licenseStatus = (esriLicenseStatus)aoini.IsProductCodeAvailable(esriLicenseProductCode.esriLicenseProductCodeStandard);
            if (licenseStatus == esriLicenseStatus.esriLicenseAvailable)
            {
                licenseStatus = (esriLicenseStatus)aoini.Initialize(esriLicenseProductCode.esriLicenseProductCodeStandard);
            }
            InitializeComponent();

            base.doInitilization();

            //////////////////////////////////////////////////////
            ///文件菜单
            this.mapControl_WM.LoadMxFile(Application.StartupPath + "\\" + GIS_Const.DEFAULT_MXD_FILE);
            //this.mapControl_WM.LoadMxFile(Application.StartupPath + "\\local.mxd");
            toolStrip1.AxMap = mapControl_WM;
            m_FileMenu.AxMapControl = this.mapControl_WM; //传入MapControl控件   

            stateMonitor1.doInitialization(this);
            stateMonitor1.Start();
            stateMonitor1.Type = StateMonitor.LocationType.BottomRight;
            stateMonitor1.X = 5;
            stateMonitor1.Y = 2;


            //////////////////////////////////////////////////////
            ///绘制基本图元工具条
            ///加载测试数 
            IMapControl3 mapControl = (IMapControl3)this.mapControl_WM.Object;
            IToolbarControl toolbarControl = (IToolbarControl)toolBar_WM.Object;

            //绑定控件
            this.toolBar_WM.SetBuddyControl(mapControl);
            this.tocControl_WM.SetBuddyControl(mapControl);

            //获得工作空间
            //string strProvide = "Provider=Microsoft.Jet.OLEDB.4.0; Data Source=";
            //string strDataSource = Application.StartupPath + @"\GasWarning-ChengZhuang.mdb";
            //MDBOperation.GetODbConnection(strProvide + strDataSource);
            //IWorkspaceFactory workspaceFactory = new AccessWorkspaceFactory();
            //IWorkspace workspace = workspaceFactory.OpenFromFile(strDataSource, 0);

            //this.toolBar_WM.AddItem("esriControls.ControlsMapNavigationToolbar");
            //this.toolBar_WM.AddItem("esriControls.ControlsMapRefreshViewCommand", 0, -1, false, 0, esriCommandStyles.esriCommandStyleIconOnly);

            //给全局变量赋值
            DataEditCommon.g_tbCtlEdit = toolbarControl;
            DataEditCommon.g_pAxMapControl = this.mapControl_WM;
            DataEditCommon.g_axTocControl = this.tocControl_WM;
            DataEditCommon.load();

            //添加Toolbar
            this.toolBar_WM.AddToolbarDef(new GIS_ToolbarView());
            this.toolBar_WM.AddToolbarDef(new GIS_ToolbarEdit(this.mapControl_WM, mapControl, toolbarControl, DataEditCommon.g_pCurrentWorkSpace));
            //this.toolBar_WM.AddToolbarDef(new GIS_ToolbarSpecial());
            this.toolBar_WM.AddToolbarDef(new GIS_ToolbarModify());
            this.toolBar_WM.AddToolbarDef(new GIS_ToolbarBasic());

            Global.SetInitialParams(this.mapControl_WM.ActiveView);

        }

        private delegate void ShowDelegate(UpdateWarningResultMessage data);

        /// <summary>
        /// 预警结果更新响应函数,参数无用
        /// </summary>
        /// <param name="data"></param>
        static private void UpdateWarningResultUI(UpdateWarningResultMessage data)
        {
            if (GetLatestWarningResultInstance().InvokeRequired)
            {
                GetLatestWarningResultInstance().Invoke(new ShowDelegate(UpdateWarningResultUI), data);
            }
            else
            {
                //如果使用Show方法则会卡住，不知为何！！！--->原因：http://blog.csdn.net/linjf520/article/details/8199635
                //目前使用ShowDialog方法也能操作其他窗体，但如果不手动关闭窗体，Socket会一直等待窗体关闭后才能够继续接收消息
                //GetLatestWarningResultInstance().LoadValue(GetLatestWarningResultInstance().GetLastPreWarningTime());
                GetLatestWarningResultInstance().LoadValue(data.DTime);
                //GetLatestWarningResultInstance().Show();
                //ShowLatestWarningResult();
                //GetLatestWarningResultInstance().Show();
            }
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            this.Show();
            Application.DoEvents();
            //你的代码写在这里.
            ShowLatestWarningResult();
        }

        private void MainForm_WM_Load(object sender, EventArgs e)
        {
            //注册更新预警结果事件
            _clientSocket.OnMsgUpdateWarningResult += new LibSocket.ClientSocket.UpdateWarnigResultHandler(UpdateWarningResultUI);
            SocketMessage msg = new SocketMessage(COMMAND_ID.REGISTER_WARNING_RESULT_NOTIFICATION_ALL, DateTime.Now);
            SendMsg2Server(msg);

            //浮动工具条中文设置
            DXSeting.floatToolsLoadSet();

            ShowLatestWarningResult();
        }

        private void MainForm_WM_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (GIS.Common.DataEditCommon.hasEdit())
            {
                if (DialogResult.Yes == MessageBox.Show("您有未保存的编辑，确定要退出系统吗?", "系统提示", MessageBoxButtons.YesNo, MessageBoxIcon.Warning))
                {
                    e.Cancel = false;
                }
                else
                {
                    e.Cancel = true;
                }
                return;
            }
            if (DialogResult.Yes == MessageBox.Show("您确定要退出系统吗?", "系统提示", MessageBoxButtons.YesNo, MessageBoxIcon.Warning))
            {
                e.Cancel = false;
                Application.ExitThread();
                Application.Exit();
            }
            else
            {
                e.Cancel = true;
            }
        }

        private void MainForm_WM_FormClosed(object sender, FormClosedEventArgs e)
        {
            Application.ExitThread();
            Application.Exit();
        }

        #region ******文件******
        //打开矿图
        private void mniOpen_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            m_FileMenu.OpenMapDocument();
        }

        //保存
        private void mniSave_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            m_FileMenu.Save();
        }

        //另存为
        private void mniSaveAs_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            m_FileMenu.SaveAs();
        }

        //导出CAD
        private void mniDCCAD_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            m_FileMenu.ExportCAD();
        }

        //导出PDF或图片
        private void mniDCTPPDF_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            m_FileMenu.ExportPicPdf();
        }

        //打印
        private void mniPrint_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            ////打印事件
            //PrintDialog pd = new PrintDialog();
            //if (DialogResult.OK == pd.ShowDialog())
            //{
            //    //TODO:打印成功
            //}
            m_FileMenu.Print();
        }
        //退出
        private void mniQuit_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            m_FileMenu.Exit();
        }
        #endregion
        #region ******预警结果******
        //预警结果
        private void mniYJJG_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            ShowLatestWarningResult();
        }

        static private void ShowLatestWarningResult()
        {
            //显示最新预警结果
            //GetLatestWarningResultInstance().TopMost = true;
            GetLatestWarningResultInstance().Show();
            GetLatestWarningResultInstance().BringToFront();

        }

        //历史预警结果查询
        private void mniLSYJJGCX_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            PreWarningResultQuery preWarningResultQueryForm = new PreWarningResultQuery();
            preWarningResultQueryForm.Show();
        }

        //区域预警图
        private void mniQYYJT_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            GIS.Warning.FrmWarningPolygon frm = new GIS.Warning.FrmWarningPolygon();
            frm.Show(this);
            //ProgressBar.Progress bar = new ProgressBar.Progress();
            //int showtime = 2;
            //bar.SetTotalTime(showtime);
            //bar.ShowDialog();

            //EnableLayer("Spline_region", true);
        }


        #region Yanger_xy 临时使用
        public void EnableLayer(string strLayerName, bool visible)
        {
            //设置图层名称
            for (int intI = 0; intI < mapControl_WM.LayerCount; intI++)
            {
                if (mapControl_WM.Map.Layer[intI].Name == strLayerName)
                {
                    mapControl_WM.Map.Layer[intI].Visible = visible;
                    break;
                }
            }
            mapControl_WM.ActiveView.Refresh();
        }
        #endregion

        //工作面预警图
        private void mniGZMYJT_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            //ProgressBar.Progress bar = new ProgressBar.Progress();
            //int showtime = 2;
            //bar.SetTotalTime(showtime);
            //bar.ShowDialog();

            //EnableLayer("spline", true);
        }


        //预警点闪烁
        private void mniYJDSS_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (m_flashWarningPoints == null)
            {
                m_flashWarningPoints = new GIS.Warning.FlashWarningPoints();
                m_flashWarningPoints.OnCreate(this.mapControl_WM.Object);
                m_flashWarningPoints.FlashInterver = 6000;   //闪烁间隔时间 6秒
            }

            if (m_flashWarningPoints.FlashStatus == false)
            {
                m_flashWarningPoints.UpdateEarlyWarningPoint();
                m_flashWarningPoints.FlashStatus = true;
            }
            else
                m_flashWarningPoints.FlashStatus = false;
        }
        #endregion
        #region ******预警规则设置******
        //瓦斯超限预警规则设置
        private void mniWSCXYJGZSZ_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            SetTunnelPreWarningRules dlg = new SetTunnelPreWarningRules(RULE_TYPE_WARNING_TYPE_FILTER.OUT_OF_LIMIT, this);
            dlg.ShowDialog();
        }

        //瓦斯突出预警规则设置
        private void mniWSTCYJGZSZ_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            SetTunnelPreWarningRules dlg = new SetTunnelPreWarningRules(RULE_TYPE_WARNING_TYPE_FILTER.OUTBURST, this);
            dlg.ShowDialog();
        }

        //预警规则管理
        private void PreWarningRulesManagement_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            PreWarningRulesManagement rulesMan = new PreWarningRulesManagement();
            rulesMan.ShowDialog();
        }
        #endregion
        #region ******预警数据管理******
        //瓦斯信息
        private void mniWSXX_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            GasInfoManagement gdm = new GasInfoManagement(this);
            gdm.Show();
        }

        //地质构造信息
        private void mniDZGZXX_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            GeologicStructureInfoManagement geologicStructrueManagement = new GeologicStructureInfoManagement(this);
            geologicStructrueManagement.Show();
        }

        //煤层赋存信息
        private void mniMCFCXX_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            CoalExistenceInfoManagement cem = new CoalExistenceInfoManagement(this);
            cem.Show();
        }

        //通风信息
        private void mniTFXX_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            VentilationInfoManagement vim = new VentilationInfoManagement(this);
            vim.Show();
        }

        //管理信息
        private void mniManagementInfo_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            ManagementInfoManagement mm = new ManagementInfoManagement(this);
            mm.Show();
        }
        #endregion
        #region ******采掘进度数据******
        //掘进进尺日报
        private void mniJJJCRB_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            DayReportJJManagement djjm = new DayReportJJManagement(this);
            djjm.Show();
        }

        //回采进尺日报
        private void mniHCJCRB_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            DayReportHCManagement dhcm = new DayReportHCManagement(this);
            dhcm.Show();
        }

        //回采进度图管理
        private void mniHCFDTGL_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {

        }
        //查询掘进进尺月报表
        private void mniCXJJJCYBB_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            DayReportJJManagement djjm = new DayReportJJManagement(this, "掘进进尺月报表管理", "掘进进尺月报表");
            djjm.Show();
        }

        //查询回采进尺月报表
        private void mniCXHCJCYBB_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            DayReportJJManagement djjm = new DayReportJJManagement(this, "回采进尺月报表管理", "回采进尺月报表");
            djjm.Show();
        }

        #endregion
        #region ******传感器管理******
        //掘进面传感器管理
        private void mniJJMCGQGL_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            ProbeInfoManagement probeInfoManagement = new ProbeInfoManagement(this);
            probeInfoManagement.Show();

        }

        //回采面传感器管理
        private void mniHCMCGQGL_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            ProbeInfoManagement probeInfoManagement = new ProbeInfoManagement(this);
            probeInfoManagement.Show();
        }

        //其它地点传感器管理
        private void mniQTDDCGQGL_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            ProbeInfoManagement probeInfoManagement = new ProbeInfoManagement(this);
            probeInfoManagement.Show();
        }
        #endregion
        #region ******短信管理******
        //收件人管理
        private void mniSJRGL_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {

        }

        //手动发送短信
        private void mniSDFSDX_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            ShortMessage _shortMessage = new ShortMessage();
            _shortMessage.ShowDialog();
        }
        //自动发送短信
        private void mniZDFSDX_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {

        }
        #endregion
        #region ******系统设置******
        //数据库设置
        private void mniDatabaseSet_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            DatabaseManagement dm = new DatabaseManagement(DATABASE_TYPE.WarningManagementDB);
            dm.Show();
        }

        //人员信息管理
        private void mniStuffInfoMana_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            UserInformationDetailsManagementFather uidmf = new UserInformationDetailsManagementFather();
            uidmf.ShowDialog();
        }

        //部门管理
        private void mniDepartmentInfoMana_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            DepartmentInformation dm = new DepartmentInformation();
            dm.Show();
        }

        //用户信息管理
        private void mniUserInfoManaFloat_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            UserLoginInformationManagement ulim = new UserLoginInformationManagement();
            ulim.ShowDialog();
        }

        //用户组信息管理
        private void mniUserGroupInfoMana_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            UserGroupInformationManagement ugm = new UserGroupInformationManagement();
            ugm.Show();
        }

        //对别管理
        private void mniTeamManage_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            TeamManagement teamManagement = new TeamManagement();
            teamManagement.Show();
        }

        //班次管理
        private void mniShiftsSetting_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            WorkTime workTime = new WorkTime(this);
            workTime.Show();
        }
        #endregion
        #region ******帮助******
        //帮助文件
        private void mniHelpFile_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            string strHelpFilePath = System.Windows.Forms.Application.StartupPath + Const_WM.System5_Help_File;
            try
            {
                System.Diagnostics.Process.Start(strHelpFilePath);
            }
            catch
            {
                Alert.alert("帮助文件未找到或已损坏");
            }
        }

        //关于
        private void mniAbout_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            Const.strPicturepath = System.Windows.Forms.Application.StartupPath + Const_WM.Picture_Name;
            LibAbout.About libabout = new About(this.ProductName, this.ProductVersion);
            libabout.ShowDialog();
        }
        #endregion



        #region ******文件浮动工具条******
        //打开矿图浮动工具条
        private void mniOpenFloat_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            this.mniOpen_ItemClick(null, null);
        }

        //保存浮动工具条
        private void mniSaveFloat_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            this.mniSave_ItemClick(null, null);
        }

        //另存为浮动工具条
        private void mniSaveAsFloat_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            this.mniSaveAs_ItemClick(null, null);
        }

        //导出CAD浮动工具条
        private void mniDCCADFloat_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            this.mniDCCAD_ItemClick(null, null);
        }

        //导出Pdf或图片
        private void mniDCTPPDFFloat_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            this.mniDCTPPDF_ItemClick(null, null);
        }
        //打印浮动工具条
        private void mniPrintFloat_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            this.mniPrint_ItemClick(null, null);
        }

        //退出浮动工具条
        private void mniQuitFloat_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            this.mniQuit_ItemClick(null, null);
        }
        #endregion
        #region ******预警结果浮动工具条******

        //预警结果浮动工具条
        private void mniYJJGFloat_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {

            ////显示最新预警结果
            ShowLatestWarningResult();
        }

        //历史预警结果查询浮动工具条
        private void mniLSYJJGCXFloat_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            this.mniLSYJJGCX_ItemClick(null, null);
        }


        //区域预警图浮动工具条
        private void mniQYYJTFloat_ItemClick_1(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            this.mniQYYJT_ItemClick(null, null);
        }

        //工作面预警图浮动工具条
        private void mniGZMYJTFloat_ItemClick_1(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            this.mniGZMYJT_ItemClick(null, null);
        }

        //预警点闪烁浮动工具条
        private void mniYJDSSFloat_ItemClick_1(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            this.mniYJDSS_ItemClick(null, null);
        }
        #endregion
        #region ******预警规则设置浮动工具条******
        //瓦斯超限预警规则设置浮动工具条
        private void mniWSCXYJGZSZFloat_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            this.mniWSCXYJGZSZ_ItemClick(null, null);
        }

        //瓦斯突出预警规则设置浮动工具条
        private void mniWSTCYJGZSZFloat_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            this.mniWSTCYJGZSZ_ItemClick(null, null);
        }

        //预警规则管理浮动工具条
        private void PreWarningRulesManagementFloat_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            this.PreWarningRulesManagement_ItemClick(null, null);
        }
        #endregion
        #region ******预警数据管理浮动工具条******
        //瓦斯信息浮动工具条
        private void mniWSXXFloat_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            this.mniWSXX_ItemClick(null, null);
        }

        //地质构造信息浮动工具条
        private void mniDZGZXXFloat_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            this.mniDZGZXX_ItemClick(null, null);
        }

        //煤层赋存信息浮动工具条
        private void mniMCFCXXFloat_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            this.mniMCFCXX_ItemClick(null, null);
        }

        //通风信息浮动工具条
        private void mniTFXXFloat_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            this.mniTFXX_ItemClick(null, null);
        }

        //管理信息浮动工具条
        private void mniManagementInfoFloat_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            this.mniManagementInfo_ItemClick(null, null);
        }
        #endregion
        #region ******采掘进度数据浮动工具条******
        //掘进进尺日报浮动工具条
        private void mniJJJCRBFloat_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            this.mniJJJCRB_ItemClick(null, null);
        }

        //回采进尺日报浮动工具条
        private void mniHCJCRBFloat_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            this.mniHCJCRB_ItemClick(null, null);
        }

        //回采进度图管理浮动工具条
        private void mniHCFDTGLFloat_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            this.mniHCFDTGL_ItemClick(null, null);
        }

        //查询掘进进尺月报表浮动工具条
        private void mniCXJJJCYBBFloat_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            this.mniCXJJJCYBB_ItemClick(null, null);
        }

        //查询回采进尺月报表浮动工具条
        private void mniCXHCJCYBBFloat_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            this.mniCXHCJCYBB_ItemClick(null, null);
        }
        #endregion
        #region ******传感器管理浮动工具条******
        //掘进面传感器管理
        private void mniJJMCGQGLFloat_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            this.mniJJMCGQGL_ItemClick(null, null);
        }

        //回采面传感器管理
        private void mniHCMCGQGLFloat_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            this.mniHCMCGQGL_ItemClick(null, null);
        }

        //其它地点传感器管理
        private void mniQTDDCGQGLFloat_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            this.mniQTDDCGQGL_ItemClick(null, null);
        }
        #endregion
        #region ******短信管理浮动工具条******
        //收件人管理
        private void mniSJRGLFloat_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            this.mniSJRGL_ItemClick(null, null);
        }

        //手动发送短信
        private void mniSDFSDXFloat_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            ShortMessage _shortMessage = new ShortMessage();
            _shortMessage.ShowDialog();
        }

        //自动发送短信
        private void mniZDFSDXFloat_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            this.mniZDFSDX_ItemClick(null, null);
        }
        #endregion
        #region ******系统设置浮动工具条******
        //数据库设置浮动工具条
        private void mniDatabaseSetFloat_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            this.mniDatabaseSet_ItemClick(null, null);
        }

        //人员信息管理浮动工具条
        private void mniStuffInfoManaFloat_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            this.mniStuffInfoMana_ItemClick(null, null);
        }

        //部门管理浮动工具条
        private void mniDepartmentInfoManaFloat_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            this.mniDepartmentInfoMana_ItemClick(null, null);
        }

        //用户信息管理浮动工具条
        private void mniUserInfoMana_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            this.mniUserInfoManaFloat_ItemClick(null, null);
        }

        //用户组信息管理浮动工具条
        private void mniUserGroupInfoManaFloat_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            this.mniUserGroupInfoMana_ItemClick(null, null);
        }

        //对别管理浮动工具条
        private void mniTeamManageFloat_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            this.mniTeamManage_ItemClick(null, null);
        }

        //班次管理浮动工具条
        private void mniShiftsSettingFloat_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            this.mniShiftsSetting_ItemClick(null, null);
        }
        #endregion

        /**
         *  瓦斯预警结果分析
         */
        private void barBtnGasAnalysis_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            WarningResultAnalysis wra = new WarningResultAnalysis(LibCommon.WarningReasonItems.瓦斯, this);
            wra.Show();
        }

        /**
         *  煤层赋存预警结果分析
         */
        private void barBtnCoalAnalysis_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            WarningResultAnalysis wra = new WarningResultAnalysis(LibCommon.WarningReasonItems.煤层赋存, this);
            wra.Show();
        }

        // 通风预警结果分析
        private void barBtnVentilationAnalysis_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            WarningResultAnalysis wra = new WarningResultAnalysis(LibCommon.WarningReasonItems.通风, this);
            wra.Show();
        }

        // 管理因素预警结果分析
        private void barBtnManagementAnalysis_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            WarningResultAnalysis wra = new WarningResultAnalysis(LibCommon.WarningReasonItems.管理因素, this);
            wra.Show();
        }

        // 地质构造预警结果分析
        private void barBtnGeologyAnalysis_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            WarningResultAnalysis wra = new WarningResultAnalysis(LibCommon.WarningReasonItems.地质构造, this);
            wra.Show();
        }

        #region TOCControls点击事件

        /// <summary>
        /// 右键弹出图层管理菜单，进行图层管理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tocControl_WM_OnMouseDown(object sender, ITOCControlEvents_OnMouseDownEvent e)
        {
            if (e.button != 2)
                return;//左键则跳出

            esriTOCControlItem item = esriTOCControlItem.esriTOCControlItemNone;
            IBasicMap map = null;
            ILayer layer = null;
            object unk = null;
            object data = null;

            //判断选择类型，确定右键菜单的位置
            this.tocControl_WM.HitTest(e.x, e.y, ref item, ref map, ref layer, ref unk, ref data);
            if (layer == null)
                return;
            //确认所选项 
            if (item == esriTOCControlItem.esriTOCControlItemMap)
                this.tocControl_WM.SelectItem(map, null);
            else
                this.tocControl_WM.SelectItem(layer, null);//20140224 有bug

            //设置图层的CustomProperty（在自定义图层命令中使用）
            this.mapControl_WM.CustomProperty = layer;

            //弹出菜单
            GIS.LayersManager.LayersManagerMap menuMap = new GIS.LayersManager.LayersManagerMap();
            menuMap.SetHook(this.mapControl_WM);
            GIS.LayersManager.LayersManagerLayer menuLayer = new GIS.LayersManager.LayersManagerLayer();
            menuLayer.SetHook(this.mapControl_WM);
            if (item == esriTOCControlItem.esriTOCControlItemMap)
                //选中的为地图
                menuMap.PopupMenu(e.x, e.y, this.tocControl_WM.hWnd);
            if (item == esriTOCControlItem.esriTOCControlItemLayer)
                //选中的为图层
                menuLayer.PopupMenu(e.x, e.y, this.tocControl_WM.hWnd);
            if (item == esriTOCControlItem.esriTOCControlItemLegendClass)//选中的为图例
            {
                return;
                ESRI.ArcGIS.Carto.ILegendClass pLC = new LegendClassClass();
                ESRI.ArcGIS.Carto.ILegendGroup pLG = new LegendGroupClass();
                if (unk is ILegendGroup)
                {
                    pLG = (ILegendGroup)unk;
                }
                pLC = pLG.get_Class((int)data);
                ISymbol pSym;
                pSym = pLC.Symbol;
                ESRI.ArcGIS.DisplayUI.ISymbolSelector pSS = new
                ESRI.ArcGIS.DisplayUI.SymbolSelectorClass();
                bool bOK = false;
                pSS.AddSymbol(pSym);
                bOK = pSS.SelectSymbol(0);
                if (bOK)
                {
                    pLC.Symbol = pSS.GetSymbolAt(0);
                }
                this.mapControl_WM.ActiveView.Refresh();
                this.tocControl_WM.Refresh();
            }
        }
        #endregion

        private void mniDCShape_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            m_FileMenu.ExportShape();
        }
    }
}
