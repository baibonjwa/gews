#region

using System;
using System.Windows.Forms;
using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.Controls;
using ESRI.ArcGIS.esriSystem;
using ESRI.ArcGIS.SystemUI;
using GIS;
using GIS.Common;
using GIS.HdProc;
using GIS.View;
using LibAbout;
using LibCommon;
using LibCommonControl;
using LibCommonForm;
using _3.GeologyMeasure;

#endregion
namespace sys3
{
    public partial class MainForm_GM : MainFrm
    {
        private GIS_FileMenu m_FileMenu = new GIS_FileMenu();
        private string m_mapDocumentName = string.Empty;
        public MainForm_GM()
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

            //defaultLookAndFeel1.LookAndFeel.UseWindowsXPTheme = false;
            //defaultLookAndFeel1.LookAndFeel.Style = DevExpress.LookAndFeel.LookAndFeelStyle.Skin;
            //defaultLookAndFeel1.LookAndFeel.SkinName = 

            Log.Debug("[GM]....Ready to load MXD file.....");
            ///文件菜单
            this.mapControl_GM.LoadMxFile(Application.StartupPath + "\\" + GIS_Const.DEFAULT_MXD_FILE);
            //this.mapControl_GM.LoadMxFile(Application.StartupPath + "\\local.mxd");
            Log.Debug("[GM]....Finished to load MXD file.....");
            statusStrip1.AxMap = mapControl_GM;
            m_FileMenu.AxMapControl = this.mapControl_GM; //传入MapControl控件                      

            ///加载数据   
            IMapControl3 mapControl = (IMapControl3)this.mapControl_GM.Object;
            IToolbarControl toolbarControl = (IToolbarControl)toolBar_GM.Object;

            //绑定控件
            this.toolBar_GM.SetBuddyControl(mapControl);
            this.tocControl_GM.SetBuddyControl(mapControl);

            //给全局变量赋值
            DataEditCommon.g_tbCtlEdit = toolbarControl;
            DataEditCommon.g_pAxMapControl = this.mapControl_GM;
            DataEditCommon.g_axTocControl = this.tocControl_GM;
            DataEditCommon.load();

            //添加Toolbar
            this.toolBar_GM.AddToolbarDef(new GIS_ToolbarView());
            this.toolBar_GM.AddToolbarDef(new GIS_ToolbarEdit(this.mapControl_GM, mapControl, toolbarControl, DataEditCommon.g_pCurrentWorkSpace));
            //this.toolBar_GM.AddToolbarDef(new GIS_ToolbarSpecial());
            this.toolBar_GM.AddToolbarDef(new GIS_ToolbarModify());
            this.toolBar_GM.AddToolbarDef(new GIS_ToolbarBasic());

            //给GIS工程的全局变量赋值
            Global.SetInitialParams(this.mapControl_GM.ActiveView);

            Log.Debug("[GM]....Construction finished.");
        }
        #region 窗体事件
        private void MainForm_GM_Load(object sender, EventArgs e)
        {
            //浮动工具条中文设置
            DXSeting.floatToolsLoadSet();

            Log.Debug("[GM]....Loading finished.");
        }

        private void MainForm_GM_FormClosing(object sender, FormClosingEventArgs e)
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

        private void MainForm_GM_FormClosed(object sender, FormClosedEventArgs e)
        {
            Application.Exit();
        }
        #endregion
        #region MAPControls点击事件

        private void mapControl_GM_OnSelectionChanged(object sender, EventArgs e)
        {
            mapControl_GM.Focus();
        }
        private void mapControl_GM_OnKeyUp(object sender, IMapControlEvents2_OnKeyUpEvent e)
        {
            if (e.keyCode == (int)Keys.Delete && mapControl_GM.CurrentTool != null && !mapControl_GM.CurrentTool.GetType().FullName.Equals("ESRI.ArcGIS.Controls.ControlsEditingEditToolClass"))
            {
                DataEditCommon.DeleteAllSelection();
            }
        }
        private void mapControl_GM_OnMapReplaced(object sender, IMapControlEvents2_OnMapReplacedEvent e)
        {
            //获得当前地图文档
            m_mapDocumentName = this.mapControl_GM.DocumentFilename;

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
        private void tocControl_GM_OnMouseDown(object sender, ITOCControlEvents_OnMouseDownEvent e)
        {
            if (e.button != 2) return;//左键则跳出

            esriTOCControlItem item = esriTOCControlItem.esriTOCControlItemNone;
            IBasicMap map = null;
            ILayer layer = null;
            object unk = null; object data = null;

            //判断选择类型，确定右键菜单的位置
            this.tocControl_GM.HitTest(e.x, e.y, ref item, ref map, ref layer, ref unk, ref data);
            if (layer == null)
                return;
            //确认所选项 
            if (item == esriTOCControlItem.esriTOCControlItemMap)
                this.tocControl_GM.SelectItem(map, null);
            else
                this.tocControl_GM.SelectItem(layer, null);//20140224 有bug

            //设置图层的CustomProperty（在自定义图层命令中使用）
            this.mapControl_GM.CustomProperty = layer;

            //弹出菜单
            GIS.LayersManager.LayersManagerMap menuMap = new GIS.LayersManager.LayersManagerMap();
            menuMap.SetHook(this.mapControl_GM);
            GIS.LayersManager.LayersManagerLayer menuLayer = new GIS.LayersManager.LayersManagerLayer();
            menuLayer.SetHook(this.mapControl_GM);
            if (item == esriTOCControlItem.esriTOCControlItemMap) //选中的为地图
                menuMap.PopupMenu(e.x, e.y, this.tocControl_GM.hWnd);
            if (item == esriTOCControlItem.esriTOCControlItemLayer) //选中的为图层
                menuLayer.PopupMenu(e.x, e.y, this.tocControl_GM.hWnd);
        }

        /// <summary>
        /// 双击图层符号，修改整个图层符号类型
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tocControl_GM_OnDoubleClick(object sender, ITOCControlEvents_OnDoubleClickEvent e)
        {
            //esriTOCControlItem tocControlItem = esriTOCControlItem.esriTOCControlItemNone;
            //ILayer pLayer = null;
            //IBasicMap pBasicMap = null;
            //object unk = null;
            //object data = null;
            //if (e.button == 1)
            //{
            //    //判断点击的为哪种Item
            //    tocControl_GM.HitTest(e.x, e.y, ref tocControlItem, ref pBasicMap, ref pLayer, ref unk,
            //        ref data);
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

            //        this.mapControl_GM.ActiveView.Refresh();
            //        this.tocControl_GM.Refresh();
            //    }
            //}
        }

        #endregion
        #region******文件******
        //打开矿图
        private void mniOpenFile_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
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
        private void mniDCShape_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            m_FileMenu.ExportShape();
        }
        //导出CAD文件
        private void mniDCCAD_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            m_FileMenu.ExportCAD();
        }

        //导出Pdf或图片
        private void mniDCTPPDF_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            m_FileMenu.ExportPicPdf();
        }
        private void mniPrint_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            m_FileMenu.Print();
        }
        //退出
        private void mniQuit_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            m_FileMenu.Exit();
        }
        #endregion
        #region ******编辑******

        //撤销
        private void mniUndo_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            toolBar_GM.GetItem(toolBar_GM.Find("GIS.GraphicEdit.UndoEdit")).Command.OnClick();
        }

        //重做
        private void mniRedo_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            toolBar_GM.GetItem(toolBar_GM.Find("GIS.GraphicEdit.RedoEdit")).Command.OnClick();
        }

        //查看属性
        private void mniPropertyInspector_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            MyMapHelp.SetCurrentTool((ICommand)new AttributeQueryEdit());
        }

        //点选
        private void mniClick_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            MyMapHelp.SetCurrentTool((ICommand)new GIS.GraphicEdit.FeatureSelect());
        }

        //框选
        private void mniRegion_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            MyMapHelp.SetCurrentTool((ICommand)new GIS.GraphicEdit.FeatureSelect());
        }

        //查询距离
        private void mniDist_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            MyMapHelp.SetCurrentTool((ICommand)new MeasureDistance());
        }

        //查询面积
        private void mniArea_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            MyMapHelp.SetCurrentTool((ICommand)new MeasureArea());
        }

        //对象捕捉
        private void mniObjectSnaps_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            //MyMapHelp.SetCurrentTool((ICommand)new GIS.GraphicEdit.SnapSetting());
            toolBar_GM.GetItem(toolBar_GM.Find("GIS.GraphicEdit.SnapSetting")).Command.OnClick();

        }
        #endregion
        #region ******基础图元绘制******
        //点
        private void mniPoint_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            MyMapHelp.SetCurrentTool((ICommand)new GIS.BasicGraphic.AddPoint());
        }

        //直线
        private void mniLine_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            MyMapHelp.SetCurrentTool((ICommand)new AddStraightFeatureLine());
        }

        //多段线
        private void mniPolyline_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            MyMapHelp.SetCurrentTool((ICommand)new GIS.BasicGraphic.AddFeatureLine());
        }

        //样条线
        private void mniSpline_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            MyMapHelp.SetCurrentTool((ICommand)new AddBezierCurve());
        }

        //矩形
        private void mniRect_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            MyMapHelp.SetCurrentTool((ICommand)new AddRectangle());
        }

        //文字
        private void mniText_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            MyMapHelp.SetCurrentTool((ICommand)new AddText());
        }

        //圆
        private void mniCircle_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            MyMapHelp.SetCurrentTool((ICommand)new AddCircle());
        }

        //圆弧
        private void mniArc_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            MyMapHelp.SetCurrentTool((ICommand)new AddArc());
        }

        //椭圆
        private void mniEllipse_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            MyMapHelp.SetCurrentTool((ICommand)new AddEllipse());
        }

        //填充图案
        private void mniHatch_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            MyMapHelp.SetCurrentTool((ICommand)new GIS.BasicGraphic.AddPolygon());
        }
        #endregion
        #region ******专业图元绘制******
        //设计巷道管理
        private void mniSJHDGL_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            TunnelInfoManagement tm = new TunnelInfoManagement(this);
            tm.Show();
        }

        //导线管理
        private void mniDXGL_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            WireInfoManagement wm = new WireInfoManagement();
            wm.Show();
        }

        //从龙软数据库导入
        private void mniCLRSJKDRDXSJK_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            //TunnelInfoManagement tim = new TunnelInfoManagement();
            //tim.Show();
            //ImportLRData ilrd = new ImportLRData();
            //ilrd.Show();

        }

        //掘进面管理
        private void mniJJHDGL_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            TunnelJjManagement tjjm = new TunnelJjManagement();
            tjjm.Show();
        }

        //回采面管理
        private void mniHCHDGL_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            TunnelHCManagement thcm = new TunnelHCManagement(this);
            thcm.Show();
        }
        //横川
        private void mniHENGCHUANHDGL_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            TunnelHChuanManagement formHChuanManagement = new TunnelHChuanManagement(this);
            formHChuanManagement.Show(this);
        }
        //井筒
        private void mniJT_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            PitshaftInfoManagement pitshaftInfoManagementForm = new PitshaftInfoManagement();
            pitshaftInfoManagementForm.Show(this);
        }

        //硐室
        private void mniDS_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            //在地图上绘制硐室
            GIS.AddDongshi addDongshiTool = new AddDongshi();
            addDongshiTool.OnCreate(this.mapControl_GM.Object);
            this.mapControl_GM.CurrentTool = addDongshiTool;
        }

        //揭露断层
        private void mniJLDC_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            FaultageInfoManagement fim = new FaultageInfoManagement();
            fim.Show();
        }

        //推断断层
        private void mniTDDC_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            BigFaultageInfoManagement bfim = new BigFaultageInfoManagement();
            bfim.Show(this);
        }

        //陷落柱
        private void mniXLZ_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            CollapsePillarsManagement cpm = new CollapsePillarsManagement();
            cpm.Show();
        }

        //勘探钻孔
        private void mniKTZK_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            BoreholeInfoManagement bim = new BoreholeInfoManagement();
            bim.Show(this);
        }

        //勘探线
        private void mniKTX_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            ProspectingLineInfoManagement prospectingLineInfoManagementForm = new ProspectingLineInfoManagement();
            prospectingLineInfoManagementForm.Show();
        }

        //井下抽采孔
        private void mniJXCCK_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {

        }

        //小柱状
        private void mniXZZ_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            GIS.SpecialGraphic.FrmNewXZZ frm = new GIS.SpecialGraphic.FrmNewXZZ();
            frm.Show(this);
        }

        //地面抽采孔
        private void mniDMCCK_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {

        }

        //石门剖面图
        private void mniSMPMT_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {

        }

        //巷道剖面图
        private void mniHDPMT_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {

        }

        //综合柱状图
        private void mniZHZZT_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            GIS.SpecialGraphic.FrmZhzzt frm = new GIS.SpecialGraphic.FrmZhzzt();
            frm.Show(this);
        }

        //煤层底板等高线
        private void mniMCDBDGX_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {

        }

        //埋深等高线
        private void mniMSDGX_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {

        }

        //煤层管理
        private void mniMCGL_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            CommonManagement commonManagementForm = new CommonManagement(5, 0);
            if (DialogResult.OK == commonManagementForm.ShowDialog())
            {

            }
        }
        //掘进巷道校正
        private void miniJUEJINJZ_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            TunnelJZEntering tunnelJzEntering = new TunnelJZEntering();
            tunnelJzEntering.ShowDialog();
        }
        //回采巷道校正
        private void miniHCHDJZ_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            TunnelHCJZEntering tunnelHcJzEntering = new TunnelHCJZEntering();
            tunnelHcJzEntering.ShowDialog();
        }
        #endregion
        #region ******修改******
        //移动
        private void mniMove_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            MyMapHelp.SetCurrentTool((ICommand)new GIS.GraphicModify.FeatureMoveEdit());
        }

        //旋转
        private void mniRotate_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            MyMapHelp.SetCurrentTool((ICommand)new RotateTool());
        }

        //复制
        private void mniCopy_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            //MyMapHelp.SetCurrentTool((ICommand)new GIS.GraphicModify.EditCopyCommand());
            toolBar_GM.GetItem(toolBar_GM.Find("GIS.GraphicModify.EditCopyCommand")).Command.OnClick();
        }

        //镜像
        private void mniMirror_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            MyMapHelp.SetCurrentTool((ICommand)new MirrorFeature());
        }

        //删除
        private void mniDelete_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            //MyMapHelp.SetCurrentTool((ICommand)new GIS.GraphicModify.DeleteFeature());
            toolBar_GM.GetItem(toolBar_GM.Find("GIS.GraphicModify.DeleteFeature")).Command.OnClick();
        }

        //裁剪
        private void mniTailor_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            MyMapHelp.SetCurrentTool((ICommand)new TrimLineTool());
        }

        //延伸
        private void mniExtend_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            MyMapHelp.SetCurrentTool((ICommand)new ExtendTool());
        }

        //线型
        private void mniLineType_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {

        }

        //线宽
        private void mniLineWeight_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {

        }

        //颜色
        private void mniColor_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {

        }
        #endregion
        #region ******视图******
        //放大
        private void mniMagnify_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            MyMapHelp.SetCurrentTool((ICommand)new ZoomInTool());
        }

        //缩小
        private void mniShrink_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            MyMapHelp.SetCurrentTool((ICommand)new ZoomOutTool());
        }

        //全局显示
        private void mniGlobal_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            //new ESRI.ArcGIS.Controls.ControlsMapFullExtentCommand().OnClick();
            toolBar_GM.GetItem(toolBar_GM.Find("GIS.View.FullExtentCommand")).Command.OnClick();
        }

        //上一视图
        private void mniPreviousView_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            toolBar_GM.GetItem(toolBar_GM.Find("GIS.View.ExtentBackCommand")).Command.OnClick();
        }

        //下一视图
        private void mniNextView_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            toolBar_GM.GetItem(toolBar_GM.Find("GIS.View.ExtentForwardCommand")).Command.OnClick();
        }

        //平移
        private void mniOffset_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            MyMapHelp.SetCurrentTool((ICommand)new PanTool());
        }

        //局部视图
        private void mniBrokenView_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            MyMapHelp.SetCurrentTool((ICommand)new LocalView());
        }

        //全局视图
        private void mniOverallView_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            toolBar_GM.GetItem(toolBar_GM.Find("GIS.View.ClearView")).Command.OnClick();
        }
        #endregion
        #region ******出图******

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
            DepartmentInformation di = new DepartmentInformation();
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
            UserGroupInformationManagement ugm = new UserGroupInformationManagement();
            ugm.ShowDialog();
        }
        #endregion
        #region ******帮助******

        //帮助文件
        private void mniHelpFile_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            string strHelpFilePath = System.Windows.Forms.Application.StartupPath + Const_GM.System3_Help_File;
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
            Const.strPicturepath = System.Windows.Forms.Application.StartupPath + Const_GM.Picture_Name;
            About libabout = new About(this.ProductName, this.ProductVersion);
            libabout.ShowDialog();
        }
        #endregion
        #region ******文件浮动工具条******
        //打开矿图浮动工具条
        private void mniOpenFileFloat_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            this.mniOpenFile_ItemClick(null, null);
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

        //导出为图片或Pdf
        private void mniDCTPPDFFloat_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            this.mniDCTPPDF_ItemClick(null, null);
        }

        //退出浮动工具条
        private void mniQuitFloat_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            this.mniQuit_ItemClick(null, null);
        }
        #endregion
        #region ******编辑浮动工具条******
        //撤销浮动工具条
        private void mniUndoFloat_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            mniUndo_ItemClick(sender, e);
        }

        //重做浮动工具条
        private void mniRedoFloat_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            mniRedo_ItemClick(sender, e);
        }

        //属性查看浮动工具条
        private void mniPropertyInspectorFloat_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            mniPropertyInspector_ItemClick(sender, e);
        }

        //点选浮动工具条
        private void mniClickFloat_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            mniClick_ItemClick(sender, e);
        }

        //框选浮动工具条
        private void mniRegionFloat_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            mniRegion_ItemClick(sender, e);
        }

        //查询距离浮动工具条
        private void mniDistFloat_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            mniDist_ItemClick(sender, e);
        }

        //查询面积浮动工具条
        private void mniAreaFloat_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            mniArea_ItemClick(sender, e);
        }

        //对象捕捉浮动工具条
        private void mniObjectSnapsFloat_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            mniObjectSnaps_ItemClick(sender, e);
        }
        #endregion
        #region ******基础图元绘制浮动工具条******
        //点浮动工具条
        private void mniPointFloat_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            mniPoint_ItemClick(sender, e);
        }

        //直线浮动工具条
        private void mniLineFloat_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            mniLine_ItemClick(sender, e);
        }

        //多段线浮动工具条
        private void mniPolylineFloat_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            mniPolyline_ItemClick(sender, e);
        }

        //样条线浮动工具条
        private void mniSplineFloat_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            mniSpline_ItemClick(sender, e);
        }

        //矩形浮动工具条
        private void mniRectFloat_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            mniRect_ItemClick(sender, e);
        }

        //文字浮动工具条
        private void mniTextFloat_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            mniText_ItemClick(sender, e);
        }

        //圆浮动工具条
        private void mniCircleFloat_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            mniCircle_ItemClick(sender, e);
        }

        //圆弧浮动工具条
        private void mniArcFloat_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            mniArc_ItemClick(sender, e);
        }

        //椭圆浮动工具条
        private void mniEllipseFloat_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            mniEllipse_ItemClick(sender, e);
        }

        //填充图案浮动工具条
        private void mniHatchFloat_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            mniHatch_ItemClick(sender, e);
        }
        #endregion
        #region ******专业图元管理浮动工具条******
        //设计巷道管理浮动工具条
        private void mniSJHDGLFloat_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            this.mniSJHDGL_ItemClick(null, null);
        }
        //导线管理浮动工具条
        private void mniDXGLFloat_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            this.mniDXGL_ItemClick(null, null);
        }
        //从龙软数据库导入导线数据浮动工具条
        private void mniCLRSJKDRDXSJKFloat_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            this.mniCLRSJKDRDXSJK_ItemClick(null, null);
        }
        //掘进巷道管理浮动工具条
        private void mniJJHDGLFloat_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            this.mniJJHDGL_ItemClick(null, null);
        }

        //回采巷道管理浮动工具条
        private void mniHCHDGLFloat_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            this.mniHCHDGL_ItemClick(null, null);
        }
        //横川
        private void mniHCHUANGLFloat_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            this.mniHENGCHUANHDGL_ItemClick(null, null);
        }
        //井筒管理浮动工具条
        private void mniJTFloat_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            this.mniJT_ItemClick(null, null);
        }

        //硐室浮动工具条
        private void mniDSFloat_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            this.mniDS_ItemClick(null, null);
        }

        //揭露断层浮动工具条
        private void mniJLDCFloat_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            this.mniJLDC_ItemClick(null, null);
        }

        //推断断层浮动工具条
        private void mniTDDCFloat_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            this.mniTDDC_ItemClick(null, null);
        }

        //陷落柱浮动工具条
        private void mniXLZFloat_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            //this.mniXLZ_ItemClick(null, null);
            ICommand command = new GIS.SpecialGraphic.DrawXLZ();
            command.OnCreate(mapControl_GM.Object);
            if (command.Enabled)
                mapControl_GM.CurrentTool = (ITool)command;
        }

        //勘探钻孔浮动工具条
        private void mniKTZKFloat_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            mniKTZK_ItemClick(sender, e);
            return;
            //ICommand command = new GIS.SpecialGraphic.DrawPoint();
            //command.OnCreate(mapControl_GM.Object);
            //if (command.Enabled)
            //    mapControl_GM.CurrentTool = (ITool)command;
        }

        //勘探线浮动工具条
        private void mniKTXFloat_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            this.mniKTX_ItemClick(null, null);
        }
        //小柱状浮动工具条
        private void mniXZZFloat_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            this.mniXZZ_ItemClick(null, null);
        }
        //煤层底板等高线浮动工具条
        private void mniMCDBDGXFloat_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            mniMCDBDGX_ItemClick(sender, e);
        }

        //埋深等高线浮动工具条
        private void mniMSDGXFloat_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            mniMSDGX_ItemClick(sender, e);
        }

        //煤层管理浮动工具条
        private void mniMCGLFloat_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            this.mniMCGL_ItemClick(null, null);
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
        #region ******视图浮动工具条******
        private void mniMagnifyFloat_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            mniMagnify_ItemClick(sender, e);
        }

        private void mniShrinkFloat_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            mniShrink_ItemClick(sender, e);
        }

        private void mniGlobalFloat_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            mniGlobal_ItemClick(sender, e);
        }

        private void mniPreviousViewFloat_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            mniPreviousView_ItemClick(sender, e);
        }

        private void mniNextViewFloat_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            mniNextView_ItemClick(sender, e);
        }

        private void mniOffsetFloat_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            mniOffset_ItemClick(sender, e);
        }

        private void mniBrokenViewFloat_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            mniBrokenView_ItemClick(sender, e);
        }

        private void mniOverallViewFloat_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            mniOverallView_ItemClick(sender, e);
        }
        #endregion
        #region ******修改浮动工具条******
        private void mniMoveFloat_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            mniMove_ItemClick(sender, e);
        }

        private void mniRotateFloat_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            mniRotate_ItemClick(sender, e);
        }

        private void mniCopyFloat_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            mniCopy_ItemClick(sender, e);
        }

        private void mniMirrorFloat_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            mniMirror_ItemClick(sender, e);
        }

        private void mniDeleteFloat_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            mniDelete_ItemClick(sender, e);
        }

        private void mniTailorFloat_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            mniTailor_ItemClick(sender, e);
        }

        private void mniExtendFloat_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            mniExtend_ItemClick(sender, e);
        }

        private void mniLineTypeFloat_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            mniLineType_ItemClick(sender, e);
        }

        private void mniLineWeightFloat_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            mniLineWeight_ItemClick(sender, e);
        }

        private void mniColorFloat_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            mniColor_ItemClick(sender, e);
        }
        #endregion
    }
}