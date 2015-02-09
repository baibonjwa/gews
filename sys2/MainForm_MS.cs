using System;
using System.Windows.Forms;
using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.Controls;
using ESRI.ArcGIS.Display;
using ESRI.ArcGIS.esriSystem;
using GIS;
using GIS.Common;
using GIS.HdProc;
using LibAbout;
using LibCommon;
using LibCommonControl;
using LibCommonForm;
using LibDatabase;
using LibPanels;

namespace sys2
{
    public partial class MainForm_MS : MainFrm
    {
        private GIS_FileMenu m_FileMenu = new GIS_FileMenu();

        public MainForm_MS()
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
            Log.Debug("[MS]...Loading Mxd file......");
            ///文件菜单
            this.mapControl_MS.LoadMxFile(Application.StartupPath + "\\" + GIS_Const.DEFAULT_MXD_FILE);
            Log.Debug("[MS]...Finished loading Mxd file......");
            statusStrip1.AxMap = mapControl_MS;
            m_FileMenu.AxMapControl = this.mapControl_MS; //传入MapControl控件  

            //////////////////////////////////////////////////////
            ///绘制基本图元工具条
            ///加载测试数据   
            IMapControl3 mapControl = (IMapControl3)this.mapControl_MS.Object;
            IToolbarControl toolbarControl = (IToolbarControl)this.toolBar_MS.Object;

            //绑定控件
            this.toolBar_MS.SetBuddyControl(mapControl);
            this.tocControl_MS.SetBuddyControl(mapControl);

            //获得工作空间
            //string strProvide = "Provider=Microsoft.Jet.OLEDB.4.0; Data Source=";
            //string strDataSource = Application.StartupPath + @"\GasWarning-ChengZhuang.mdb";
            //MDBOperation.GetODbConnection(strProvide + strDataSource);
            //IWorkspaceFactory workspaceFactory = new AccessWorkspaceFactory();
            //IWorkspace workspace = workspaceFactory.OpenFromFile(strDataSource, 0);

            //给全局变量赋值
            //给全局变量赋值
            DataEditCommon.g_tbCtlEdit = toolbarControl;
            DataEditCommon.g_pAxMapControl = this.mapControl_MS;
            DataEditCommon.g_axTocControl = this.tocControl_MS;
            DataEditCommon.load();

            //添加Toolbar
            this.toolBar_MS.AddToolbarDef(new GIS_ToolbarView());
            this.toolBar_MS.AddToolbarDef(new GIS_ToolbarEdit(this.mapControl_MS, mapControl, toolbarControl, DataEditCommon.g_pCurrentWorkSpace));
            //this.toolBar_GM.AddToolbarDef(new GIS_ToolbarSpecial());
            this.toolBar_MS.AddToolbarDef(new GIS_ToolbarModify());
            this.toolBar_MS.AddToolbarDef(new GIS_ToolbarBasic());
            //地图操作初始化
            Global.SetInitialParams(this.mapControl_MS.ActiveView);

            Log.Debug("[MS]...Finished Constructing Main Form......");
        }

        private void MainForm_MS_Load(object sender, EventArgs e)
        {
            //浮动工具条中文设置
            DXSeting.floatToolsLoadSet();
        }

        private void MainForm_MS_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (GIS.Common.DataEditCommon.hasEdit())
            {
                if (DialogResult.Yes == MessageBox.Show(@"您有未保存的编辑，确定要退出系统吗?", "系统提示", MessageBoxButtons.YesNo, MessageBoxIcon.Warning))
                {
                    e.Cancel = false;
                }
                else
                {
                    e.Cancel = true;
                }
                return;
            }
            if (DialogResult.Yes == MessageBox.Show(@"您确定要退出系统吗?", "系统提示", MessageBoxButtons.YesNo, MessageBoxIcon.Warning))
            {
                e.Cancel = false;
            }
            else
            {
                e.Cancel = true;
            }
        }

        private void MainForm_MS_FormClosed(object sender, FormClosedEventArgs e)
        {
            Application.Exit();
        }

        #region ******系统设置******

        //密码修改
        private void mniPasswordChange_Click(object sender, EventArgs e)
        {
            PasswordUpdate pu = new PasswordUpdate();
            pu.Show();
        }
        #endregion

        //用户详细信息管理
        private void mniUserInfoMana_Click_1(object sender, EventArgs e)
        {
            UserInformationDetailsManagementFather uidmf = new UserInformationDetailsManagementFather();
            uidmf.ShowDialog();
        }

        //打开矿图
        private void mniOpenMineMap_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
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

        //导出为CAD
        private void mniDCCAD_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            m_FileMenu.ExportCAD();
        }
        private void mniDCShape_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            m_FileMenu.ExportShape();
        }
        //导出为Pdf或图片
        private void mniDCTPPDF_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            m_FileMenu.ExportPicPdf();
        }

        //打印
        private void mniPrint_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            //PrintDialog pd = new PrintDialog();
            //if (DialogResult.OK == pd.ShowDialog())
            //{

            //}
            m_FileMenu.Print();
        }

        //退出
        private void mniQuit_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            this.Close();
        }

        //掘进进尺日报
        private void mniJJJCRB_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            DayReportJjManagement jj = new DayReportJjManagement();
            jj.Show();
        }

        //回采进尺日报
        private void mniHCJCRB_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            DayReportHcManagement hc = new DayReportHcManagement();
            hc.Show();
        }

        //停采线
        private void mniTCX_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            StopLineManagement slm = new StopLineManagement();
            slm.Show(this);
        }

        //回采进度图管理
        private void mniHCJDTGL_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
        }

        //查询掘进进尺月报表
        private void mniCXJJJCYBB_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
        }

        //查询回采进尺月报表
        private void mniCXHCJCYBB_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
        }

        //数据库设置
        private void mniDatabaseSet_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            DatabaseManagement dm = new DatabaseManagement(DATABASE_TYPE.MiningSchedulingDB);
            dm.Show();
        }

        //人员信息管理
        private void mniUserInfoMana_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            UserInformationDetailsManagementFather uidmf = new UserInformationDetailsManagementFather();
            uidmf.ShowDialog();
        }

        //部门信息管理
        private void mniDepartment_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            DepartmentInformation di = new DepartmentInformation();
            di.ShowDialog();
        }

        //用户信息管理
        private void mniUserLoginInfoMana_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            UserLoginInformationManagement ulim = new UserLoginInformationManagement();
            ulim.ShowDialog();
        }

        //用户组信息管理
        private void mniUserGroupInfoMana_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            UserGroupInformationManagement ugm = new UserGroupInformationManagement();
            ugm.ShowDialog();
        }

        //队别管理
        private void mniTeamManage_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            TeamManagement tm = new TeamManagement();
            tm.Show();
        }

        //队别管理
        private void mniShiftsSetting_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            WorkTime wt = new WorkTime();
            wt.Show();
        }

        //帮助文件
        private void mniHelpFile_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            string strHelpFilePath = System.Windows.Forms.Application.StartupPath + Const_MS.System2_Help_File;
            try
            {
                System.Diagnostics.Process.Start(strHelpFilePath);
            }
            catch
            {
                Alert.alert("系统找不到帮助文件或帮助文件无效");
            }
        }

        //关于
        private void mniAbout_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            Const.strPicturepath = System.Windows.Forms.Application.StartupPath + Const_MS.Picture_Name;
            About libabout = new About(this.ProductName, this.ProductVersion);
            libabout.ShowDialog();
        }

        //打开矿图浮动工具条
        private void mniOpenMineMapFloat_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            this.mniOpenMineMap_ItemClick(null, null);
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

        //停采线浮动工具条
        private void mniTCXFloat_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            this.mniTCX_ItemClick(null, null);
        }

        //回采进度图管理浮动工具条
        private void mniHCJDTGLFloat_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            this.mniHCJDTGL_ItemClick(null, null);
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

        //系统设置浮动工具条
        private void mniDatabaseSetFloat_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            this.mniDatabaseSet_ItemClick(null, null);
        }

        //部门信息浮动工具条
        private void mniDepartmentFloat_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            this.mniDepartment_ItemClick(null, null);
        }

        //用户信息管理浮动工具条
        private void mniUserLoginInfoManaFloat_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            this.mniUserLoginInfoMana_ItemClick(null, null);
        }

        //用户组信息管理浮动工具条
        private void mniUserGroupInfoManaFloat_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            this.mniUserGroupInfoMana_ItemClick(null, null);
        }

        //队别管理浮动工具条
        private void mniTeamManageFloat_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            this.mniTeamManage_ItemClick(null, null);
        }

        //班次管理浮动工具条
        private void mniShiftsSettingFloat_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            this.mniShiftsSetting_ItemClick(null, null);
        }
        //人员信息管理浮动工具条
        private void mniUserInfoManaFloat_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            UserInformationDetailsManagementFather uidmf = new UserInformationDetailsManagementFather();
            uidmf.ShowDialog();
        }
        //导出CAD浮动工具条
        private void mniDCCADFloat_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            this.mniDCCAD_ItemClick(null, null);
        }

        //导出为图片及Pdf浮动工具条
        private void mniDCTPPDFFloat_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            this.mniDCTPPDF_ItemClick(null, null);
        }

        // 横川信息
        private void barBtnItemHengChuan_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
        }

        private void mniSkinSetting_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            DXSkinSetting ss = new DXSkinSetting();
            ss.ShowDialog();
        }

        private void tocControl_MS_OnMouseDown(object sender, ITOCControlEvents_OnMouseDownEvent e)
        {
            if (e.button != 2) return;//左键则跳出

            esriTOCControlItem item = esriTOCControlItem.esriTOCControlItemNone;
            IBasicMap map = null;
            ILayer layer = null;
            object unk = null; object data = null;

            //判断选择类型，确定右键菜单的位置
            this.tocControl_MS.HitTest(e.x, e.y, ref item, ref map, ref layer, ref unk, ref data);
            if (layer == null)
                return;
            //确认所选项 
            if (item == esriTOCControlItem.esriTOCControlItemMap)
                this.tocControl_MS.SelectItem(map, null);
            else
                this.tocControl_MS.SelectItem(layer, null);//20140224 有bug

            //设置图层的CustomProperty（在自定义图层命令中使用）
            this.mapControl_MS.CustomProperty = layer;

            //弹出菜单
            GIS.LayersManager.LayersManagerMap menuMap = new GIS.LayersManager.LayersManagerMap();
            menuMap.SetHook(this.mapControl_MS);
            GIS.LayersManager.LayersManagerLayer menuLayer = new GIS.LayersManager.LayersManagerLayer();
            menuLayer.SetHook(this.mapControl_MS);
            if (item == esriTOCControlItem.esriTOCControlItemMap) //选中的为地图
                menuMap.PopupMenu(e.x, e.y, this.tocControl_MS.hWnd);
            if (item == esriTOCControlItem.esriTOCControlItemLayer) //选中的为图层
                menuLayer.PopupMenu(e.x, e.y, this.tocControl_MS.hWnd);
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
                this.mapControl_MS.ActiveView.Refresh();
                this.tocControl_MS.Refresh();
            }
        }
    }
}
