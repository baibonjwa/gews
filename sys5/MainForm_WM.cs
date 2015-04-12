using System;
using System.Diagnostics;
using System.Windows.Forms;
using AutoUpdaterDotNET;
using DevExpress.XtraBars;
using ESRI.ArcGIS;
using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.Controls;
using ESRI.ArcGIS.Display;
using ESRI.ArcGIS.DisplayUI;
using ESRI.ArcGIS.esriSystem;
using GIS;
using GIS.Common;
using GIS.HdProc;
using GIS.LayersManager;
using GIS.Warning;
using LibAbout;
using LibCommon;
using LibCommonForm;
using LibConfig;
using LibDatabase;
using LibPanels;
using LibSocket;
using sys2;
using _5.WarningManagement;

namespace sys5
{
    public partial class MainFormWm : Form
    {
        //最新预警结果
        private static PreWarningLastedResultQuery _latestWarningResult;
        private FlashWarningPoints m_flashWarningPoints; //预警点闪烁command类
        private readonly GIS_FileMenu m_FileMenu = new GIS_FileMenu();

        public MainFormWm()
        {
            RuntimeManager.Bind(ProductCode.EngineOrDesktop);
            IAoInitialize aoini = new AoInitializeClass();
            var licenseStatus =
                aoini.IsProductCodeAvailable(esriLicenseProductCode.esriLicenseProductCodeStandard);
            if (licenseStatus == esriLicenseStatus.esriLicenseAvailable)
            {
                licenseStatus = aoini.Initialize(esriLicenseProductCode.esriLicenseProductCodeStandard);
            }
            InitializeComponent();
        }

        public static void CleanLatestWarningResult()
        {
            _latestWarningResult.Close();
            _latestWarningResult.Dispose();
            _latestWarningResult = null;
        }

        public static PreWarningLastedResultQuery GetLatestWarningResultInstance()
        {
            //单例模式
            return _latestWarningResult ?? (_latestWarningResult = new PreWarningLastedResultQuery());
        }

        public static void ReleaseLatestWarningResultInstance()
        {
            _latestWarningResult.Close();
            _latestWarningResult.Dispose();
            _latestWarningResult = null;
        }

        /// <summary>
        ///     预警结果更新响应函数,参数无用
        /// </summary>
        /// <param name="data"></param>
        private static void UpdateWarningResultUi(UpdateWarningResultMessage data)
        {
            if (GetLatestWarningResultInstance().InvokeRequired)
            {
                GetLatestWarningResultInstance().Invoke(new ShowDelegate(UpdateWarningResultUi), data);
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
            Show();
            Application.DoEvents();
            //你的代码写在这里.
            ShowLatestWarningResult();
        }

        private void MainForm_WM_Load(object sender, EventArgs e)
        {
            SocketUtil.DoInitilization();
            //////////////////////////////////////////////////////
            ///文件菜单
            mapControl_WM.LoadMxFile(Application.StartupPath + "\\" +
                                     ConfigManager.Instance.getValueByKey(ConfigConst.CONFIG_MXD_FILE));
            //this.mapControl_WM.LoadMxFile(Application.StartupPath + "\\local.mxd");
            toolStrip1.AxMap = mapControl_WM;
            m_FileMenu.AxMapControl = mapControl_WM; //传入MapControl控件   


            //////////////////////////////////////////////////////
            //绘制基本图元工具条
            //加载测试数 
            var mapControl = (IMapControl3) mapControl_WM.Object;
            var toolbarControl = (IToolbarControl) toolBar_WM.Object;

            //绑定控件
            toolBar_WM.SetBuddyControl(mapControl);
            tocControl_WM.SetBuddyControl(mapControl);

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
            DataEditCommon.g_pAxMapControl = mapControl_WM;
            DataEditCommon.g_axTocControl = tocControl_WM;
            DataEditCommon.load();

            //添加Toolbar
            //toolBar_WM.AddToolbarDef(new GIS_ToolbarView());
            //toolBar_WM.AddToolbarDef(new GIS_ToolbarEdit(mapControl_WM, mapControl, toolbarControl,
            //    DataEditCommon.g_pCurrentWorkSpace));
            ////this.toolBar_WM.AddToolbarDef(new GIS_ToolbarSpecial());
            //toolBar_WM.AddToolbarDef(new GIS_ToolbarModify());
            //toolBar_WM.AddToolbarDef(new GIS_ToolbarBasic());
            AddToolBar.Addtool(mapControl_WM, mapControl, toolbarControl, DataEditCommon.g_pCurrentWorkSpace);

            Global.SetInitialParams(mapControl_WM.ActiveView);


            AutoUpdater.Start("http://bltmld.vicp.cc:8090/sys5/update.xml");
            //注册更新预警结果事件

            SocketUtil.GetClientSocketInstance().OnMsgUpdateWarningResult += UpdateWarningResultUi;
            var msg = new SocketMessage(COMMAND_ID.REGISTER_WARNING_RESULT_NOTIFICATION_ALL, DateTime.Now);
            SocketUtil.SendMsg2Server(msg);

            //浮动工具条中文设置
            DXSeting.floatToolsLoadSet();

            ShowLatestWarningResult();
        }

        private void MainForm_WM_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (DataEditCommon.hasEdit())
            {
                if (DialogResult.Yes ==
                    MessageBox.Show(@"您有未保存的编辑，确定要退出系统吗?", "系统提示", MessageBoxButtons.YesNo, MessageBoxIcon.Warning))
                {
                    e.Cancel = false;
                }
                else
                {
                    e.Cancel = true;
                }
                return;
            }
            if (DialogResult.Yes ==
                MessageBox.Show(@"您确定要退出系统吗?", "系统提示", MessageBoxButtons.YesNo, MessageBoxIcon.Warning))
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

        /**
         *  瓦斯预警结果分析
         */

        private void barBtnGasAnalysis_ItemClick(object sender, ItemClickEventArgs e)
        {
            var wra = new WarningResultAnalysis(WarningReasonItems.瓦斯);
            wra.Show();
        }

        /**
         *  煤层赋存预警结果分析
         */

        private void barBtnCoalAnalysis_ItemClick(object sender, ItemClickEventArgs e)
        {
            var wra = new WarningResultAnalysis(WarningReasonItems.煤层赋存);
            wra.Show();
        }

        // 通风预警结果分析
        private void barBtnVentilationAnalysis_ItemClick(object sender, ItemClickEventArgs e)
        {
            var wra = new WarningResultAnalysis(WarningReasonItems.通风);
            wra.Show();
        }

        // 管理因素预警结果分析
        private void barBtnManagementAnalysis_ItemClick(object sender, ItemClickEventArgs e)
        {
            var wra = new WarningResultAnalysis(WarningReasonItems.管理因素);
            wra.Show();
        }

        // 地质构造预警结果分析
        private void barBtnGeologyAnalysis_ItemClick(object sender, ItemClickEventArgs e)
        {
            var wra = new WarningResultAnalysis(WarningReasonItems.地质构造);
            wra.Show();
        }

        private void mniDCShape_ItemClick(object sender, ItemClickEventArgs e)
        {
            m_FileMenu.ExportShape();
        }

        #region TOCControls点击事件

        /// <summary>
        ///     右键弹出图层管理菜单，进行图层管理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tocControl_WM_OnMouseDown(object sender, ITOCControlEvents_OnMouseDownEvent e)
        {
            if (e.button != 2)
                return; //左键则跳出

            var item = esriTOCControlItem.esriTOCControlItemNone;
            IBasicMap map = null;
            ILayer layer = null;
            object unk = null;
            object data = null;

            //判断选择类型，确定右键菜单的位置
            tocControl_WM.HitTest(e.x, e.y, ref item, ref map, ref layer, ref unk, ref data);
            if (layer == null)
                return;
            //确认所选项 
            if (item == esriTOCControlItem.esriTOCControlItemMap)
                tocControl_WM.SelectItem(map, null);
            else
                tocControl_WM.SelectItem(layer, null); //20140224 有bug

            //设置图层的CustomProperty（在自定义图层命令中使用）
            mapControl_WM.CustomProperty = layer;

            //弹出菜单
            var menuMap = new LayersManagerMap();
            menuMap.SetHook(mapControl_WM);
            var menuLayer = new LayersManagerLayer();
            menuLayer.SetHook(mapControl_WM);
            if (item == esriTOCControlItem.esriTOCControlItemMap)
                //选中的为地图
                menuMap.PopupMenu(e.x, e.y, tocControl_WM.hWnd);
            if (item == esriTOCControlItem.esriTOCControlItemLayer)
                //选中的为图层
                menuLayer.PopupMenu(e.x, e.y, tocControl_WM.hWnd);
            if (item == esriTOCControlItem.esriTOCControlItemLegendClass) //选中的为图例
            {
                return;
                ILegendClass pLC = new LegendClassClass();
                ILegendGroup pLG = new LegendGroupClass();
                if (unk is ILegendGroup)
                {
                    pLG = (ILegendGroup) unk;
                }
                pLC = pLG.get_Class((int) data);
                ISymbol pSym;
                pSym = pLC.Symbol;
                ISymbolSelector pSS = new
                    SymbolSelectorClass();
                var bOK = false;
                pSS.AddSymbol(pSym);
                bOK = pSS.SelectSymbol(0);
                if (bOK)
                {
                    pLC.Symbol = pSS.GetSymbolAt(0);
                }
                mapControl_WM.ActiveView.Refresh();
                tocControl_WM.Refresh();
            }
        }

        #endregion

        private void bbiCheckUpdate_ItemClick(object sender, ItemClickEventArgs e)
        {
            AutoUpdater.CheckAtOnce = true;
            AutoUpdater.Start("http://bltmld.vicp.cc:8090/sys5/update.xml");
        }

        private delegate void ShowDelegate(UpdateWarningResultMessage data);

        #region ******文件******

        //打开矿图
        private void mniOpen_ItemClick(object sender, ItemClickEventArgs e)
        {
            m_FileMenu.OpenMapDocument();
        }

        //保存
        private void mniSave_ItemClick(object sender, ItemClickEventArgs e)
        {
            m_FileMenu.Save();
        }

        //另存为
        private void mniSaveAs_ItemClick(object sender, ItemClickEventArgs e)
        {
            m_FileMenu.SaveAs();
        }

        //导出CAD
        private void mniDCCAD_ItemClick(object sender, ItemClickEventArgs e)
        {
            m_FileMenu.ExportCAD();
        }

        //导出PDF或图片
        private void mniDCTPPDF_ItemClick(object sender, ItemClickEventArgs e)
        {
            m_FileMenu.ExportPicPdf();
        }

        //打印
        private void mniPrint_ItemClick(object sender, ItemClickEventArgs e)
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
        private void mniQuit_ItemClick(object sender, ItemClickEventArgs e)
        {
            m_FileMenu.Exit();
        }

        #endregion

        #region ******预警结果******

        //预警结果
        private void mniYJJG_ItemClick(object sender, ItemClickEventArgs e)
        {
            ShowLatestWarningResult();
        }

        private static void ShowLatestWarningResult()
        {
            //显示最新预警结果
            //GetLatestWarningResultInstance().TopMost = true;
            GetLatestWarningResultInstance().Show();
            GetLatestWarningResultInstance().BringToFront();
        }

        //历史预警结果查询
        private void mniLSYJJGCX_ItemClick(object sender, ItemClickEventArgs e)
        {
            var preWarningResultQueryForm = new PreWarningResultQuery();
            preWarningResultQueryForm.Show();
        }

        //区域预警图
        private void mniQYYJT_ItemClick(object sender, ItemClickEventArgs e)
        {
            var frm = new FrmWarningPolygon();
            frm.Show(this);
            //ProgressBar.Progress bar = new ProgressBar.Progress();
            //int showtime = 2;
            //bar.SetTotalTime(showtime);
            //bar.ShowDialog();

            //EnableLayer("Spline_region", true);
        }

        //工作面预警图
        private void mniGZMYJT_ItemClick(object sender, ItemClickEventArgs e)
        {
            //ProgressBar.Progress bar = new ProgressBar.Progress();
            //int showtime = 2;
            //bar.SetTotalTime(showtime);
            //bar.ShowDialog();

            //EnableLayer("spline", true);
        }


        //预警点闪烁
        private void mniYJDSS_ItemClick(object sender, ItemClickEventArgs e)
        {
            if (m_flashWarningPoints == null)
            {
                m_flashWarningPoints = new FlashWarningPoints();
                m_flashWarningPoints.OnCreate(mapControl_WM.Object);
                m_flashWarningPoints.FlashInterver = 6000; //闪烁间隔时间 6秒
            }

            if (m_flashWarningPoints.FlashStatus == false)
            {
                m_flashWarningPoints.UpdateEarlyWarningPoint();
                m_flashWarningPoints.FlashStatus = true;
            }
            else
                m_flashWarningPoints.FlashStatus = false;
        }

        #region Yanger_xy 临时使用

        public void EnableLayer(string strLayerName, bool visible)
        {
            //设置图层名称
            for (var intI = 0; intI < mapControl_WM.LayerCount; intI++)
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

        #endregion

        #region ******预警规则设置******

        //瓦斯超限预警规则设置
        private void mniWSCXYJGZSZ_ItemClick(object sender, ItemClickEventArgs e)
        {
            var dlg = new SetTunnelPreWarningRules(RULE_TYPE_WARNING_TYPE_FILTER.OUT_OF_LIMIT);
            dlg.ShowDialog();
        }

        //瓦斯突出预警规则设置
        private void mniWSTCYJGZSZ_ItemClick(object sender, ItemClickEventArgs e)
        {
            var dlg = new SetTunnelPreWarningRules(RULE_TYPE_WARNING_TYPE_FILTER.OUTBURST);
            dlg.ShowDialog();
        }

        //预警规则管理
        private void PreWarningRulesManagement_ItemClick(object sender, ItemClickEventArgs e)
        {
            var rulesMan = new PreWarningRulesManagement();
            rulesMan.ShowDialog();
        }

        #endregion

        #region ******预警数据管理******

        //瓦斯信息
        private void mniWSXX_ItemClick(object sender, ItemClickEventArgs e)
        {
            var gdm = new GasInfoManagement();
            gdm.Show();
        }

        //地质构造信息
        private void mniDZGZXX_ItemClick(object sender, ItemClickEventArgs e)
        {
            var geologicStructrueManagement = new GeologicStructureInfoManagement();
            geologicStructrueManagement.Show();
        }

        //煤层赋存信息
        private void mniMCFCXX_ItemClick(object sender, ItemClickEventArgs e)
        {
            var cem = new CoalExistenceInfoManagement();
            cem.Show();
        }

        //通风信息
        private void mniTFXX_ItemClick(object sender, ItemClickEventArgs e)
        {
            var vim = new VentilationInfoManagement();
            vim.Show();
        }

        //管理信息
        private void mniManagementInfo_ItemClick(object sender, ItemClickEventArgs e)
        {
            var mm = new ManagementInfoManagement();
            mm.Show();
        }

        #endregion

        #region ******采掘进度数据******

        //掘进进尺日报
        private void mniJJJCRB_ItemClick(object sender, ItemClickEventArgs e)
        {
            var djjm = new DayReportJjManagement();
            djjm.Show();
        }

        //回采进尺日报
        private void mniHCJCRB_ItemClick(object sender, ItemClickEventArgs e)
        {
            var dhcm = new DayReportHcManagement();
            dhcm.Show();
        }

        //回采进度图管理
        private void mniHCFDTGL_ItemClick(object sender, ItemClickEventArgs e)
        {
        }

        //查询掘进进尺月报表
        private void mniCXJJJCYBB_ItemClick(object sender, ItemClickEventArgs e)
        {
            var djjm = new DayReportJjManagement();
            djjm.Show();
        }

        //查询回采进尺月报表
        private void mniCXHCJCYBB_ItemClick(object sender, ItemClickEventArgs e)
        {
            var djjm = new DayReportJjManagement();
            djjm.Show();
        }

        #endregion

        #region ******传感器管理******

        //掘进面传感器管理
        private void mniJJMCGQGL_ItemClick(object sender, ItemClickEventArgs e)
        {
            var probeInfoManagement = new ProbeInfoManagement();
            probeInfoManagement.Show();
        }

        //回采面传感器管理
        private void mniHCMCGQGL_ItemClick(object sender, ItemClickEventArgs e)
        {
            var probeInfoManagement = new ProbeInfoManagement();
            probeInfoManagement.Show();
        }

        //其它地点传感器管理
        private void mniQTDDCGQGL_ItemClick(object sender, ItemClickEventArgs e)
        {
            var probeInfoManagement = new ProbeInfoManagement();
            probeInfoManagement.Show();
        }

        #endregion

        #region ******短信管理******

        //收件人管理
        private void mniSJRGL_ItemClick(object sender, ItemClickEventArgs e)
        {
        }

        //手动发送短信
        private void mniSDFSDX_ItemClick(object sender, ItemClickEventArgs e)
        {
            var _shortMessage = new ShortMessage();
            _shortMessage.ShowDialog();
        }

        //自动发送短信
        private void mniZDFSDX_ItemClick(object sender, ItemClickEventArgs e)
        {
        }

        #endregion

        #region ******系统设置******

        //数据库设置
        private void mniDatabaseSet_ItemClick(object sender, ItemClickEventArgs e)
        {
            var dm = new DatabaseManagement(DATABASE_TYPE.WarningManagementDB);
            dm.Show();
        }

        //人员信息管理
        private void mniStuffInfoMana_ItemClick(object sender, ItemClickEventArgs e)
        {
            var uidmf = new UserInformationDetailsManagementFather();
            uidmf.ShowDialog();
        }

        //部门管理
        private void mniDepartmentInfoMana_ItemClick(object sender, ItemClickEventArgs e)
        {
            var dm = new DepartmentInformation();
            dm.Show();
        }

        //用户信息管理
        private void mniUserInfoManaFloat_ItemClick(object sender, ItemClickEventArgs e)
        {
            var ulim = new UserLoginInformationManagement();
            ulim.ShowDialog();
        }

        //用户组信息管理
        private void mniUserGroupInfoMana_ItemClick(object sender, ItemClickEventArgs e)
        {
            var ugm = new UserGroupInformationManagement();
            ugm.Show();
        }

        //对别管理
        private void mniTeamManage_ItemClick(object sender, ItemClickEventArgs e)
        {
            var teamManagement = new TeamManagement();
            teamManagement.Show();
        }

        //班次管理
        private void mniShiftsSetting_ItemClick(object sender, ItemClickEventArgs e)
        {
            var workTime = new WorkTime();
            workTime.Show();
        }

        #endregion

        #region ******帮助******

        //帮助文件
        private void mniHelpFile_ItemClick(object sender, ItemClickEventArgs e)
        {
            var strHelpFilePath = Application.StartupPath + Const_WM.System5_Help_File;
            try
            {
                Process.Start(strHelpFilePath);
            }
            catch
            {
                Alert.alert("帮助文件未找到或已损坏");
            }
        }

        //关于
        private void mniAbout_ItemClick(object sender, ItemClickEventArgs e)
        {
            Const.strPicturepath = Application.StartupPath + Const_WM.Picture_Name;
            var libabout = new About(ProductName, ProductVersion);
            libabout.ShowDialog();
        }

        #endregion

        #region ******文件浮动工具条******

        //打开矿图浮动工具条
        private void mniOpenFloat_ItemClick(object sender, ItemClickEventArgs e)
        {
            mniOpen_ItemClick(null, null);
        }

        //保存浮动工具条
        private void mniSaveFloat_ItemClick(object sender, ItemClickEventArgs e)
        {
            mniSave_ItemClick(null, null);
        }

        //另存为浮动工具条
        private void mniSaveAsFloat_ItemClick(object sender, ItemClickEventArgs e)
        {
            mniSaveAs_ItemClick(null, null);
        }

        //导出CAD浮动工具条
        private void mniDCCADFloat_ItemClick(object sender, ItemClickEventArgs e)
        {
            mniDCCAD_ItemClick(null, null);
        }

        //导出Pdf或图片
        private void mniDCTPPDFFloat_ItemClick(object sender, ItemClickEventArgs e)
        {
            mniDCTPPDF_ItemClick(null, null);
        }

        //打印浮动工具条
        private void mniPrintFloat_ItemClick(object sender, ItemClickEventArgs e)
        {
            mniPrint_ItemClick(null, null);
        }

        //退出浮动工具条
        private void mniQuitFloat_ItemClick(object sender, ItemClickEventArgs e)
        {
            mniQuit_ItemClick(null, null);
        }

        #endregion

        #region ******预警结果浮动工具条******

        //预警结果浮动工具条
        private void mniYJJGFloat_ItemClick(object sender, ItemClickEventArgs e)
        {
            ////显示最新预警结果
            ShowLatestWarningResult();
        }

        //历史预警结果查询浮动工具条
        private void mniLSYJJGCXFloat_ItemClick(object sender, ItemClickEventArgs e)
        {
            mniLSYJJGCX_ItemClick(null, null);
        }


        //区域预警图浮动工具条
        private void mniQYYJTFloat_ItemClick_1(object sender, ItemClickEventArgs e)
        {
            mniQYYJT_ItemClick(null, null);
        }

        //工作面预警图浮动工具条
        private void mniGZMYJTFloat_ItemClick_1(object sender, ItemClickEventArgs e)
        {
            mniGZMYJT_ItemClick(null, null);
        }

        //预警点闪烁浮动工具条
        private void mniYJDSSFloat_ItemClick_1(object sender, ItemClickEventArgs e)
        {
            mniYJDSS_ItemClick(null, null);
        }

        #endregion

        #region ******预警规则设置浮动工具条******

        //瓦斯超限预警规则设置浮动工具条
        private void mniWSCXYJGZSZFloat_ItemClick(object sender, ItemClickEventArgs e)
        {
            mniWSCXYJGZSZ_ItemClick(null, null);
        }

        //瓦斯突出预警规则设置浮动工具条
        private void mniWSTCYJGZSZFloat_ItemClick(object sender, ItemClickEventArgs e)
        {
            mniWSTCYJGZSZ_ItemClick(null, null);
        }

        //预警规则管理浮动工具条
        private void PreWarningRulesManagementFloat_ItemClick(object sender, ItemClickEventArgs e)
        {
            PreWarningRulesManagement_ItemClick(null, null);
        }

        #endregion

        #region ******预警数据管理浮动工具条******

        //瓦斯信息浮动工具条
        private void mniWSXXFloat_ItemClick(object sender, ItemClickEventArgs e)
        {
            mniWSXX_ItemClick(null, null);
        }

        //地质构造信息浮动工具条
        private void mniDZGZXXFloat_ItemClick(object sender, ItemClickEventArgs e)
        {
            mniDZGZXX_ItemClick(null, null);
        }

        //煤层赋存信息浮动工具条
        private void mniMCFCXXFloat_ItemClick(object sender, ItemClickEventArgs e)
        {
            mniMCFCXX_ItemClick(null, null);
        }

        //通风信息浮动工具条
        private void mniTFXXFloat_ItemClick(object sender, ItemClickEventArgs e)
        {
            mniTFXX_ItemClick(null, null);
        }

        //管理信息浮动工具条
        private void mniManagementInfoFloat_ItemClick(object sender, ItemClickEventArgs e)
        {
            mniManagementInfo_ItemClick(null, null);
        }

        #endregion

        #region ******采掘进度数据浮动工具条******

        //掘进进尺日报浮动工具条
        private void mniJJJCRBFloat_ItemClick(object sender, ItemClickEventArgs e)
        {
            mniJJJCRB_ItemClick(null, null);
        }

        //回采进尺日报浮动工具条
        private void mniHCJCRBFloat_ItemClick(object sender, ItemClickEventArgs e)
        {
            mniHCJCRB_ItemClick(null, null);
        }

        //回采进度图管理浮动工具条
        private void mniHCFDTGLFloat_ItemClick(object sender, ItemClickEventArgs e)
        {
            mniHCFDTGL_ItemClick(null, null);
        }

        //查询掘进进尺月报表浮动工具条
        private void mniCXJJJCYBBFloat_ItemClick(object sender, ItemClickEventArgs e)
        {
            mniCXJJJCYBB_ItemClick(null, null);
        }

        //查询回采进尺月报表浮动工具条
        private void mniCXHCJCYBBFloat_ItemClick(object sender, ItemClickEventArgs e)
        {
            mniCXHCJCYBB_ItemClick(null, null);
        }

        #endregion

        #region ******传感器管理浮动工具条******

        //掘进面传感器管理
        private void mniJJMCGQGLFloat_ItemClick(object sender, ItemClickEventArgs e)
        {
            mniJJMCGQGL_ItemClick(null, null);
        }

        //回采面传感器管理
        private void mniHCMCGQGLFloat_ItemClick(object sender, ItemClickEventArgs e)
        {
            mniHCMCGQGL_ItemClick(null, null);
        }

        //其它地点传感器管理
        private void mniQTDDCGQGLFloat_ItemClick(object sender, ItemClickEventArgs e)
        {
            mniQTDDCGQGL_ItemClick(null, null);
        }

        #endregion

        #region ******短信管理浮动工具条******

        //收件人管理
        private void mniSJRGLFloat_ItemClick(object sender, ItemClickEventArgs e)
        {
            mniSJRGL_ItemClick(null, null);
        }

        //手动发送短信
        private void mniSDFSDXFloat_ItemClick(object sender, ItemClickEventArgs e)
        {
            var _shortMessage = new ShortMessage();
            _shortMessage.ShowDialog();
        }

        //自动发送短信
        private void mniZDFSDXFloat_ItemClick(object sender, ItemClickEventArgs e)
        {
            mniZDFSDX_ItemClick(null, null);
        }

        #endregion

        #region ******系统设置浮动工具条******

        //数据库设置浮动工具条
        private void mniDatabaseSetFloat_ItemClick(object sender, ItemClickEventArgs e)
        {
            mniDatabaseSet_ItemClick(null, null);
        }

        //人员信息管理浮动工具条
        private void mniStuffInfoManaFloat_ItemClick(object sender, ItemClickEventArgs e)
        {
            mniStuffInfoMana_ItemClick(null, null);
        }

        //部门管理浮动工具条
        private void mniDepartmentInfoManaFloat_ItemClick(object sender, ItemClickEventArgs e)
        {
            mniDepartmentInfoMana_ItemClick(null, null);
        }

        //用户信息管理浮动工具条
        private void mniUserInfoMana_ItemClick(object sender, ItemClickEventArgs e)
        {
            mniUserInfoManaFloat_ItemClick(null, null);
        }

        //用户组信息管理浮动工具条
        private void mniUserGroupInfoManaFloat_ItemClick(object sender, ItemClickEventArgs e)
        {
            mniUserGroupInfoMana_ItemClick(null, null);
        }

        //对别管理浮动工具条
        private void mniTeamManageFloat_ItemClick(object sender, ItemClickEventArgs e)
        {
            mniTeamManage_ItemClick(null, null);
        }

        //班次管理浮动工具条
        private void mniShiftsSettingFloat_ItemClick(object sender, ItemClickEventArgs e)
        {
            mniShiftsSetting_ItemClick(null, null);
        }

        #endregion
    }
}