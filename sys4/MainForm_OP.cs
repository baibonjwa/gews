using System;
using System.Windows.Forms;
using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.Controls;
using ESRI.ArcGIS.esriSystem;
using ESRI.ArcGIS.Geometry;
using GIS;
using GIS.Common;
using GIS.SpecialGraphic;
using LibAbout;
using LibCommon;
using LibCommonForm;
using _4.OutburstPrevention;

namespace sys4
{
    public partial class MainForm_OP : Form
    {
        private string strPath = Application.StartupPath;
        private GIS_FileMenu m_FileMenu = new GIS_FileMenu();
        //public const string m_SYSTEMNAME = "工作面动态防突管理系统";
        private string m_mapDocumentName = string.Empty;

        public MainForm_OP()
        {
            ESRI.ArcGIS.RuntimeManager.Bind(ESRI.ArcGIS.ProductCode.EngineOrDesktop);
            IAoInitialize aoini = new AoInitializeClass();
            esriLicenseStatus licenseStatus = (esriLicenseStatus)aoini.IsProductCodeAvailable(esriLicenseProductCode.esriLicenseProductCodeStandard);
            if (licenseStatus == esriLicenseStatus.esriLicenseAvailable)
            {
                licenseStatus = (esriLicenseStatus)aoini.Initialize(esriLicenseProductCode.esriLicenseProductCodeStandard);
            }
            //LicenseInitializer license = new LicenseInitializer();
            //bool islicense=license.InitializeApplication();
            InitializeComponent();

            ///文件菜单
            this.mapControl_OP.LoadMxFile(Application.StartupPath + "\\" + GIS_Const.DEFAULT_MXD_FILE);
            this.statusStrip1.AxMap = mapControl_OP;
            m_FileMenu.AxMapControl = this.mapControl_OP; //传入MapControl控件    

            ///绘制基本图元工具条
            ///加载测试数据   
            IMapControl3 mapControl = (IMapControl3)this.mapControl_OP.Object;
            IToolbarControl toolbarControl = (IToolbarControl)toolbar_OP.Object;

            //绑定控件
            this.toolbar_OP.SetBuddyControl(mapControl);
            this.tocControl_OP.SetBuddyControl(mapControl);


            //给全局变量赋值
            DataEditCommon.g_tbCtlEdit = toolbarControl;
            DataEditCommon.g_pAxMapControl = this.mapControl_OP;
            DataEditCommon.g_axTocControl = this.tocControl_OP;
            DataEditCommon.load();


            //添加Toolbar
            this.toolbar_OP.AddToolbarDef(new GIS_ToolbarView());
            this.toolbar_OP.AddToolbarDef(new GIS_ToolbarEdit(this.mapControl_OP, mapControl, toolbarControl, DataEditCommon.g_pCurrentWorkSpace));
            this.toolbar_OP.AddToolbarDef(new GIS_ToolbarModify());
            this.toolbar_OP.AddToolbarDef(new GIS_ToolbarBasic());

        }
        #region 窗体事件
        private void MainForm_OP_Load(object sender, EventArgs e)
        {
            //浮动工具条中文设置
            DXSeting.floatToolsLoadSet();
        }

        private void MainForm_OP_FormClosing(object sender, FormClosingEventArgs e)
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

        private void MainForm_OP_FormClosed(object sender, FormClosedEventArgs e)
        {
            Application.Exit();
        }
        #endregion
        #region  ////////////数据管理
        /// <summary>
        /// 瓦斯压力点
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void mniWSYLD_Click(object sender, EventArgs e)
        {
            m_currentButton = 0;

            GasPressureInfoManagement gasPressureInfoManagement = new GasPressureInfoManagement();
            gasPressureInfoManagement.Show();
        }

        /// <summary>
        /// 瓦斯含量点
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void mniWSHLD_Click(object sender, EventArgs e)
        {
            m_currentButton = 0;

            GasContentInfoManagement GasContentInfoManagementForm = new GasContentInfoManagement();
            GasContentInfoManagementForm.Show();
        }

        /// <summary>
        /// 瓦斯涌出量点
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void mniWSYCLD_Click(object sender, EventArgs e)
        {
            m_currentButton = 0;

            GasGushQuantityInfoManagement gasGushQuantityInfoManagementForm = new GasGushQuantityInfoManagement();
            gasGushQuantityInfoManagementForm.Show();
        }
        #endregion
        #region  ////////////系统设置
        /// <summary>
        /// 数据库设置
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void mniDatabaseSet_Click(object sender, EventArgs e)
        {
            DatabaseManagement databaseManagementForm = new DatabaseManagement(LibDatabase.DATABASE_TYPE.GasEmissionDB);
            databaseManagementForm.ShowDialog();
        }

        /// <summary>
        /// 密码修改
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void mniPasswordChange_Click(object sender, EventArgs e)
        {
            PasswordUpdate passwordUpdateForm = new PasswordUpdate();
            passwordUpdateForm.ShowDialog();
        }

        #endregion
        #region ******文件******

        //打开矿图
        private void mniOpenMineMap_Click(object sender, EventArgs e)
        {
            m_currentButton = 0;
            m_FileMenu.OpenMapDocument();
        }
        //保存
        private void mniSave_Click(object sender, EventArgs e)
        {
        }
        //另存为
        private void mniSaveAs_Click(object sender, EventArgs e)
        {
            m_currentButton = 0;
            m_FileMenu.SaveAs();
        }
        //导出
        private void mniExport_Click(object sender, EventArgs e)
        {
            m_currentButton = 0;
        }
        private void mniDCShape_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            m_FileMenu.ExportShape();
        }
        private void mniDCTPPDF_Click(object sender, EventArgs e)
        {
            m_currentButton = 0;
            m_FileMenu.ExportPicPdf();
        }

        private void mniDCCAD_Click(object sender, EventArgs e)
        {
            m_currentButton = 0;
            m_FileMenu.ExportCAD();
        }

        //打印
        private void mniPrint_Click(object sender, EventArgs e)
        {
            m_currentButton = 0;
        }
        //退出
        private void mniQuit_Click(object sender, EventArgs e)
        {
            m_currentButton = 0;
            m_FileMenu.Exit();
        }

        #endregion
        #region 工具
        //K1值
        private void mniK1_Click(object sender, EventArgs e)
        {
            m_currentButton = 0;

            K1ValueManagement k1ValueManagement = new K1ValueManagement();
            k1ValueManagement.Show();
        }
        //瓦斯压力等值线绘制
        private void mniWSYLDZXHZ_Click(object sender, EventArgs e)
        {
            m_currentButton = 0;

            MakeContours frmMakeContours = new MakeContours();
            frmMakeContours.m_layerName = "GAS_PRESSURE_CONTOUR";
            frmMakeContours.m_layerAliasName = "瓦斯压力等值线";
            frmMakeContours.Show();
        }
        //瓦斯含量等值线绘制
        private void mniWSHLDZXHZ_Click(object sender, EventArgs e)
        {
            m_currentButton = 0;

            MakeContours frmMakeContours = new MakeContours();
            frmMakeContours.m_layerName = "GAS_CONTENT_CONTOUR";
            frmMakeContours.m_layerAliasName = "瓦斯含量等值线";
            frmMakeContours.Show();
        }
        //瓦斯涌出量等值线绘制
        private void mniWSYCLDZXHZ_Click(object sender, EventArgs e)
        {
            m_currentButton = 0;

            MakeContours frmMakeContours = new MakeContours();
            frmMakeContours.m_layerName = "GUSH_QUANTITY_CONTOUR";
            frmMakeContours.m_layerAliasName = "瓦斯涌出量等值线";
            frmMakeContours.Show();
        }
        //瓦斯地质图绘制
        private void mniWSDZTHZ_Click(object sender, EventArgs e)
        {
            m_currentButton = 0;
        }
        //区域措施
        private void mniAreaMeasures_Click(object sender, EventArgs e)
        {
            m_currentButton = 0;
        }
        //局部措施
        private void mniLocalMeasures_Click(object sender, EventArgs e)
        {
            m_currentButton = 0;
        }
        //防突钻孔设计
        private void mniFTZKSJ_Click(object sender, EventArgs e)
        {
            m_currentButton = 0;
        }
        //瓦斯压力点绘制
        private void mniWSYLDHZ_Click(object sender, EventArgs e)
        {
            m_currentButton = 1;
            mapControl_OP.CurrentTool = null;
        }
        //瓦斯含量点绘制
        private void mniWSHLDHZ_Click(object sender, EventArgs e)
        {
            m_currentButton = 2;
            mapControl_OP.CurrentTool = null;
        }
        //瓦斯涌出量点绘制
        private void mniWSYCLDHZ_Click(object sender, EventArgs e)
        {
            m_currentButton = 3;
            mapControl_OP.CurrentTool = null;
        }
        //反算瓦斯压力/含量
        private void mniFSWSYLHHL_Click(object sender, EventArgs e)
        {
            m_currentButton = 0;
        }
        //煤层透气性系数
        private void mniMCTQXXS_Click(object sender, EventArgs e)
        {
            m_currentButton = 0;
        }
        //突出指标(D、K)
        private void mniTCZB_Click(object sender, EventArgs e)
        {
            m_currentButton = 0;
        }
        //抽放管路阻力计算
        private void mniCFGLZLJS_Click(object sender, EventArgs e)
        {
            m_currentButton = 0;
        }
        //抽放泵选型
        private void mniCFBXX_Click(object sender, EventArgs e)
        {
            m_currentButton = 0;
        }
        //部门信息管理
        private void mniDepartmentInfoMana_Click(object sender, EventArgs e)
        {
            LibCommonForm.DepartmentInformation di = new LibCommonForm.DepartmentInformation();
            di.ShowDialog();
        }
        //用户信息管理
        private void mniUserInfoMana_Click(object sender, EventArgs e)
        {
            UserLoginInformationManagement ulim = new UserLoginInformationManagement();
            ulim.ShowDialog();
        }
        //用户组信息管理
        private void mniUserGroupInfoMana_Click(object sender, EventArgs e)
        {
            LibCommonForm.UserGroupInformationManagement ugm = new LibCommonForm.UserGroupInformationManagement();
            ugm.ShowDialog();
        }
        //帮助文件
        private void mniHelpFile_Click(object sender, EventArgs e)
        {
            m_currentButton = 0;
        }
        //关于
        private void mniAbout_Click(object sender, EventArgs e)
        {
            m_currentButton = 0;
        }
        //瓦斯抽放管径
        private void mniWSCFGJ_Click(object sender, EventArgs e)
        {
            m_currentButton = 0;
        }
        #endregion
        #region 绘制图元

        private int m_currentButton = 0;
        private void tsBtnWSYLD_Click(object sender, EventArgs e)
        {
            m_currentButton = 1;
            mapControl_OP.CurrentTool = null;
        }

        private void tsBtnWSHLD_Click(object sender, EventArgs e)
        {
            m_currentButton = 2;
            mapControl_OP.CurrentTool = null;
        }

        private void tsBtnWSYCLD_Click(object sender, EventArgs e)
        {
            m_currentButton = 3;
            mapControl_OP.CurrentTool = null;
        }

        private void tsBtnWSYLDZX_Click(object sender, EventArgs e)
        {
            m_currentButton = 0;//点击其他菜单或按钮时，设置该值为0，避免点击MapControl响应mapControl_OP_OnMouseDown事件

            MakeContours frmMakeContours = new MakeContours();
            frmMakeContours.m_layerName = "GAS_PRESSURE_CONTOUR";
            frmMakeContours.m_layerAliasName = "瓦斯压力等值线";
            frmMakeContours.Show();
        }

        private void tsBtnWSHLDZX_Click(object sender, EventArgs e)
        {
            m_currentButton = 0;

            MakeContours frmMakeContours = new MakeContours();
            frmMakeContours.m_layerName = "GAS_CONTENT_CONTOUR";
            frmMakeContours.m_layerAliasName = "瓦斯含量等值线";
            frmMakeContours.Show();
        }

        private void tsBtnWSYCLDZX_Click(object sender, EventArgs e)
        {
            m_currentButton = 0;

            MakeContours frmMakeContours = new MakeContours();
            frmMakeContours.m_layerName = "GUSH_QUANTITY_CONTOUR";
            frmMakeContours.m_layerAliasName = "瓦斯涌出量等值线";
            frmMakeContours.Show();
        }
        #endregion
        #region MapControls点击事件
        private void mapControl_OP_OnMouseDown(object sender, IMapControlEvents2_OnMouseDownEvent e)
        {
            if (e.button == 1 && m_currentButton > 0)
            {
                //弹出右键菜单
                //DataEditCommon.contextMenu.PopupMenu(e.x, e.y, this.mapControl_OP.hWnd);
                IPoint pt = this.mapControl_OP.ActiveView.ScreenDisplay.DisplayTransformation.ToMapPoint(e.x, e.y);
                pt = GIS.GraphicEdit.SnapSetting.getSnapPoint(pt);
                switch (m_currentButton)
                {
                    case 1:
                        GasPressureInfoEntering gasPressureInfoEnteringForm = new GasPressureInfoEntering();
                        gasPressureInfoEnteringForm.GasPressurePoint = pt;
                        gasPressureInfoEnteringForm.ShowDialog();//绘制瓦斯压力点
                        m_currentButton = 0;//解除当前按钮
                        break;
                    case 2:
                        GasContentInfoEntering gasContentInfoEnteringForm = new GasContentInfoEntering();
                        gasContentInfoEnteringForm.GasContentPoint = pt;
                        gasContentInfoEnteringForm.ShowDialog();//绘制瓦斯含量点
                        m_currentButton = 0;//解除当前按钮
                        break;
                    case 3:
                        GasGushQuantityInfoEntering gasGushQuantityInfoEnteringForm = new GasGushQuantityInfoEntering();
                        gasGushQuantityInfoEnteringForm.GasGushQuantityPoint = pt;
                        gasGushQuantityInfoEnteringForm.ShowDialog();//绘制瓦斯涌出量点
                        m_currentButton = 0;//解除当前按钮
                        break;
                    default:
                        break;
                }
            }
        }
        private void mapControl_OP_OnMouseMove(object sender, IMapControlEvents2_OnMouseMoveEvent e)
        {
            if (m_currentButton == 1 || m_currentButton == 2 || m_currentButton == 3)
            {
                IPoint pt = this.mapControl_OP.ActiveView.ScreenDisplay.DisplayTransformation.ToMapPoint(e.x, e.y);
                pt = GIS.GraphicEdit.SnapSetting.getSnapPoint(pt);
            }
        }
        private void mapControl_OP_OnMapReplaced(object sender, IMapControlEvents2_OnMapReplacedEvent e)
        {
            //获得当前地图文档
            m_mapDocumentName = this.mapControl_OP.DocumentFilename;

            //如果没有地图文档，保存按钮不可用并清除状态栏
            //if (m_mapDocumentName == string.Empty)
            //{
            //    mniSave.Enabled = false;
            //    statusBarXY.Text = string.Empty;
            //}
            //else
            //{
            //    //保存菜单可用并设置状态栏内容
            //    mniSave.Enabled = true;
            //    statusBarXY.Text = "当前文件：" + System.IO.Path.GetFileName(m_mapDocumentName);
            //}
        }

        #endregion
        #region TOCControls点击事件

        /// <summary>
        /// 右键弹出图层管理菜单，进行图层管理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tocControl_OP_OnMouseDown(object sender, ITOCControlEvents_OnMouseDownEvent e)
        {
            if (e.button != 2)
                return;//左键则跳出

            esriTOCControlItem item = esriTOCControlItem.esriTOCControlItemNone;
            IBasicMap map = null;
            ILayer layer = null;
            object unk = null;
            object data = null;

            //判断选择类型，确定右键菜单的位置
            this.tocControl_OP.HitTest(e.x, e.y, ref item, ref map, ref layer, ref unk, ref data);
            if (layer == null)
                return;
            //确认所选项 
            if (item == esriTOCControlItem.esriTOCControlItemMap)
                this.tocControl_OP.SelectItem(map, null);
            else
                this.tocControl_OP.SelectItem(layer, null);//20140224 有bug

            //设置图层的CustomProperty（在自定义图层命令中使用）
            this.mapControl_OP.CustomProperty = layer;

            //弹出菜单
            GIS.LayersManager.LayersManagerMap menuMap = new GIS.LayersManager.LayersManagerMap();
            menuMap.SetHook(this.mapControl_OP);
            GIS.LayersManager.LayersManagerLayer menuLayer = new GIS.LayersManager.LayersManagerLayer();
            menuLayer.SetHook(this.mapControl_OP);
            if (item == esriTOCControlItem.esriTOCControlItemMap)
                //选中的为地图
                menuMap.PopupMenu(e.x, e.y, this.tocControl_OP.hWnd);
            if (item == esriTOCControlItem.esriTOCControlItemLayer)
                //选中的为图层
                menuLayer.PopupMenu(e.x, e.y, this.tocControl_OP.hWnd);
        }

        /// <summary>
        /// 双击图层符号，修改整个图层符号类型
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tocControl_OP_OnDoubleClick(object sender, ITOCControlEvents_OnDoubleClickEvent e)
        {
            //esriTOCControlItem tocControlItem = esriTOCControlItem.esriTOCControlItemNone;
            //ILayer pLayer = null;
            //IBasicMap pBasicMap = null;
            //object unk = null;
            //object data = null;
            //if (e.button == 1)
            //{
            //    //判断点击的为哪种Item
            //    tocControl_OP.HitTest(e.x, e.y, ref tocControlItem, ref pBasicMap, ref pLayer, ref unk,
            //    ref data);
            //    //只有图层可设置符号
            //    if (tocControlItem == esriTOCControlItem.esriTOCControlItemLegendClass)
            //    {
            //        ESRI.ArcGIS.Carto.ILegendClass pLegendClass = new LegendClassClass();
            //        ESRI.ArcGIS.Carto.ILegendGroup pLegendGroup = new LegendGroupClass();
            //        if (unk is ILegendGroup)
            //        {
            //            pLegendGroup = (ILegendGroup)unk;
            //        }
            //        pLegendClass = pLegendGroup.get_Class((int)data);
            //        ISymbol pSymbol = pLegendClass.Symbol;
            //        ISymbolSelector pSymbolSelector = new SymbolSelectorClass();
            //        pSymbolSelector.AddSymbol(pSymbol);
            //        bool bOK = pSymbolSelector.SelectSymbol(0);
            //        if (bOK)
            //        {
            //            pLegendClass.Symbol = pSymbolSelector.GetSymbolAt(0);
            //        }

            //        this.mapControl_OP.ActiveView.Refresh();
            //        this.tocControl_OP.Refresh();
            //    }
            //}
        }

        #endregion
        #region ******文件******
        //打开矿图
        private void mniOpenMineMap_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            m_currentButton = 0;
            m_FileMenu.OpenMapDocument();
        }

        //保存
        private void mniSave_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            m_currentButton = 0;
            m_FileMenu.Save();
        }

        //另存为
        private void mniSaveAs_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            m_currentButton = 0;
            m_FileMenu.SaveAs();
        }

        private void mniExport_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            m_currentButton = 0;
        }

        //导出为CAD
        private void mniDCCAD_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            m_currentButton = 0;
            m_FileMenu.ExportCAD();
        }

        //导出为Pdf或图片
        private void mniDCTPPDF_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            m_currentButton = 0;
            m_FileMenu.ExportPicPdf();
        }

        //打印
        private void mniPrint_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            m_currentButton = 0;
            m_FileMenu.Print();
        }

        //退出
        private void mniQuit_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            m_currentButton = 0;
            m_FileMenu.Exit();
        }

        #endregion
        #region ******数据管理******
        //瓦斯含量点
        private void mniWSHLD_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            m_currentButton = 0;

            GasContentInfoManagement GasContentInfoManagementForm = new GasContentInfoManagement();
            GasContentInfoManagementForm.Show();
        }
        //瓦斯压力点
        private void mniWSYLD_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            m_currentButton = 0;

            GasPressureInfoManagement gasPressureInfoManagement = new GasPressureInfoManagement();
            gasPressureInfoManagement.Show();
        }
        //瓦斯涌出量点
        private void mniWSYCLD_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            m_currentButton = 0;

            GasGushQuantityInfoManagement gasGushQuantityInfoManagementForm = new GasGushQuantityInfoManagement();
            gasGushQuantityInfoManagementForm.Show();
        }

        //K1值
        private void mniK1_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            m_currentButton = 0;

            K1ValueManagement k1ValueManagement = new K1ValueManagement();
            k1ValueManagement.Show();
        }

        #endregion
        #region ******绘图******

        //瓦斯压力等值线绘制
        private void mniWSYLDZXHZ_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            m_currentButton = 0;

            MakeContours frmMakeContours = new MakeContours();
            frmMakeContours.m_layerName = "GAS_PRESSURE_CONTOUR";
            frmMakeContours.m_layerAliasName = "瓦斯压力等值线";
            frmMakeContours.Show();
        }

        //瓦斯含量等值线绘制
        private void mniWSHLDZXHZ_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            m_currentButton = 0;

            MakeContours frmMakeContours = new MakeContours();
            frmMakeContours.m_layerName = "GAS_CONTENT_CONTOUR";
            frmMakeContours.m_layerAliasName = "瓦斯含量等值线";
            frmMakeContours.Show();
        }

        //瓦斯涌出量等值线绘制
        private void mniWSYCLDZXHZ_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            m_currentButton = 0;

            MakeContours frmMakeContours = new MakeContours();
            frmMakeContours.m_layerName = "GUSH_QUANTITY_CONTOUR";
            frmMakeContours.m_layerAliasName = "瓦斯涌出量等值线";
            frmMakeContours.Show();
        }

        //瓦斯地质图绘制
        private void mniWSDZTHZ_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            m_currentButton = 0;
        }

        //瓦斯压力点绘制
        private void mniWSYLDHZ_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            m_currentButton = 1;
            mapControl_OP.CurrentTool = null;
            //GIS.BasicGraphic.DrawWSD wsd = new GIS.BasicGraphic.DrawWSD();
            //wsd.OnCreate(DataEditCommon.g_pAxMapControl.Object);
            //ICommand command=null;
            //command =new GIS.BasicGraphic.DrawWSD();
            //command.OnCreate(Global.MainMap);
            //    if (command.Enabled)
            //    {

            //        DataEditCommon.g_pAxMapControl.CurrentTool = (ITool)command;
            //    }

        }

        //瓦斯含量点绘制
        private void mniWSHLDHZ_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            m_currentButton = 2;
            mapControl_OP.CurrentTool = null;
        }

        //瓦斯涌出量点绘制
        private void mniWSYCLDHZ_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            m_currentButton = 3;
            mapControl_OP.CurrentTool = null;
        }
        #endregion
        #region ******防突措施******
        //区域措施
        private void mniAreaMeasures_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            m_currentButton = 0;
        }

        //局部措施
        private void mniLocalMeasures_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            m_currentButton = 0;
        }

        //防突钻孔设计
        private void mniFTZKSJ_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            m_currentButton = 0;
        }
        #endregion
        #region ******参数计算******
        //瓦斯压力点绘制
        private void mniWSYL_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            m_currentButton = 1;
        }

        //瓦斯含量点绘制
        private void mniWSHL_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            m_currentButton = 2;
        }

        //瓦斯涌出量点绘制
        private void mniWSYCL_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            m_currentButton = 3;
        }

        //反算瓦斯压力/含量
        private void mniFSWSYLHHL_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            m_currentButton = 0;
        }

        //煤层透气性系数
        private void mniMCTQXXS_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            m_currentButton = 0;
        }

        //突出指标(D、K)
        private void mniTCZB_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            m_currentButton = 0;
        }

        //抽放管路阻力计算
        private void mniCFGLZLJS_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            m_currentButton = 0;
        }

        //瓦斯抽放管径
        private void mniWSCFGJ_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            m_currentButton = 0;
        }

        //抽放泵选型
        private void mniCFBXX_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            m_currentButton = 0;
        }
        #endregion
        #region ******帮助******
        //帮助文件
        private void mniHelpFile_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            string strHelpFilePath = System.Windows.Forms.Application.StartupPath + Const_OP.System4_Help_File;
            System.Diagnostics.Process.Start(strHelpFilePath);
        }

        //关于
        private void mniAbout_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            Const.strPicturepath = System.Windows.Forms.Application.StartupPath + Const_OP.Picture_Name;
            About libabout = new About(this.ProductName, this.ProductVersion);
            libabout.ShowDialog();
        }
        #endregion
        #region ******系统设置******

        //数据库设置
        private void mniDatabaseSet_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            DatabaseManagement databaseManagementForm = new DatabaseManagement(LibDatabase.DATABASE_TYPE.GasEmissionDB);
            databaseManagementForm.ShowDialog();
        }

        //人员信息管理
        private void mniEmployeeInfoMan_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            UserInformationDetailsManagementFather uidmf = new UserInformationDetailsManagementFather();
            uidmf.ShowDialog();
        }

        //部门信息管理
        private void mniDepartmentInfoMana_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            LibCommonForm.DepartmentInformation di = new LibCommonForm.DepartmentInformation();
            di.ShowDialog();
        }

        //用户信息管理
        private void mniUserInfoMana_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            UserLoginInformationManagement ulim = new UserLoginInformationManagement();
            ulim.ShowDialog();
        }

        //用户组信息管理
        private void mniUserGroupInfoMana_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            LibCommonForm.UserGroupInformationManagement ugm = new LibCommonForm.UserGroupInformationManagement();
            ugm.ShowDialog();
        }
        #endregion
        #region ******文件浮动工具条******
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

        //导出为CAD浮动工具条
        private void mniDCCADFloat_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            this.mniDCCAD_ItemClick(null, null);
        }

        //导出为PDF或图片浮动工具条
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
        #region ******数据管理浮动工具条******
        //瓦斯含量点
        private void mniWSHLDFloat_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            this.mniWSHLD_ItemClick(null, null);
        }

        //瓦斯压力点
        private void mniWSYLDFloat_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            this.mniWSYLD_ItemClick(null, null);
        }

        //瓦斯涌出量点
        private void mniWSYCLDFloat_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            this.mniWSYCLD_ItemClick(null, null);
        }

        //K1值
        private void mniK1Float_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            this.mniK1_ItemClick(null, null);
        }
        #endregion
        #region ******绘图浮动工具条******

        //瓦斯压力等值线绘制浮动工具条
        private void mniWSYLDZXHZFloat_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            this.mniWSYLDZXHZ_ItemClick(null, null);
        }
        //瓦斯含量等值线绘制浮动工具条
        private void mniWSHLDZXHZFloat_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            this.mniWSHLDZXHZ_ItemClick(null, null);
        }
        //瓦斯涌出量等值线绘制浮动工具条
        private void mniWSYCLDZXHZFloat_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            this.mniWSYCLDZXHZ_ItemClick(null, null);
        }
        //瓦斯地质图绘制浮动工具条
        private void mniWSDZTHZFloat_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            this.mniWSDZTHZ_ItemClick(null, null);
        }
        //瓦斯压力点绘制浮动工具条
        private void mniWSYLDHZFloat_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            this.mniWSYLDHZ_ItemClick(null, null);
        }
        //瓦斯含量点绘制浮动工具条
        private void mniWSHLDHZFloat_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            this.mniWSHLDHZ_ItemClick(null, null);
        }
        //瓦斯涌出量点绘制浮动工具条   
        private void mniWSYCLDHZFloat_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            this.mniWSYCLDHZ_ItemClick(null, null);
        }
        #endregion
        #region ******防突措施浮动工具条******
        //区域措施浮动工具条
        private void mniAreaMeasuresFloat_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            this.mniAreaMeasures_ItemClick(null, null);
        }
        //局部措施浮动工具条
        private void mniLocalMeasuresFloat_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            this.mniLocalMeasures_ItemClick(null, null);
        }
        //防突钻孔设计
        private void mniFTZKSJFloat_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            this.mniFTZKSJ_ItemClick(null, null);
        }
        #endregion
        #region ******系统设置浮动工具条******
        //数据库设置浮动工具条
        private void mniDatabaseSetFloat_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            this.mniDatabaseSet_ItemClick(null, null);
        }

        //人员信息管理浮动工具条
        private void mniEmployeeInfoManFloat_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            this.mniEmployeeInfoMan_ItemClick(null, null);
        }

        //部门信息管理浮动工具条
        private void mniDepartmentInfoManaFloat_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            this.mniDepartmentInfoMana_ItemClick(null, null);
        }

        //用户信息管理浮动工具条
        private void mniUserInfoManaFloat_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            this.mniUserInfoMana_ItemClick(null, null);
        }

        //用户组信息管理浮动工具条
        private void mniUserGroupInfoManaFloat_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            this.mniUserGroupInfoMana_ItemClick(null, null);
        }
        #endregion
        #region ******参数计算浮动工具条******
        //瓦斯压力点绘制浮动工具条
        private void mniWSYLFloat_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            this.mniWSYL_ItemClick(null, null);
        }

        //瓦斯含量点绘制浮动工具条
        private void mniWSHLFloat_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            this.mniWSHL_ItemClick(null, null);
        }

        //瓦斯涌出量点绘制浮动工具条
        private void mniWSYCLFloat_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            this.mniWSYCL_ItemClick(null, null);
        }

        //反算瓦斯压力/含量浮动工具条
        private void mniFSWSYLHHLFloat_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            this.mniFSWSYLHHL_ItemClick(null, null);
        }

        //煤层透气性系数浮动工具条
        private void mniMCTQXXSFloat_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            this.mniMCTQXXS_ItemClick(null, null);
        }

        //突出指标(D、K)浮动工具条
        private void mniTCZBFloat_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            this.mniTCZB_ItemClick(null, null);
        }

        //抽放管路阻力计算浮动工具条
        private void mniCFGLZLJSFloat_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            this.mniCFGLZLJS_ItemClick(null, null);
        }

        //瓦斯抽放管径浮动工具条
        private void mniWSCFGJFloat_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            this.mniWSCFGJ_ItemClick(null, null);
        }

        //抽放泵选型浮动工具条
        private void mniCFBXXFloat_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            this.mniCFBXX_ItemClick(null, null);
        }
        #endregion
    }
}
