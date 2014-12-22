// ******************************************************************
// 概  述：导线信息添加修改
// 作  者：宋英杰
// 创建日期：2014/3/11
// 版本号：V1.0
// 版本信息：
// V1.0 新建
// ******************************************************************
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using LibBusiness;
using LibCommon;
using LibEntity;
using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.esriSystem;
using LibGeometry;
using ESRI.ArcGIS.Geometry;
using GIS.Common;
using LibCommonControl;
using LibCommonForm;
using LibSocket;
using ESRI.ArcGIS.Geodatabase;
using System.Text.RegularExpressions;
using GIS;
using GIS.HdProc;
using System.IO;

namespace _3.GeologyMeasure
{
    public partial class WireInfoEntering : BaseForm
    {
        /**********变量声明***********/
        DataGridViewCell[] dgvc = new DataGridViewCell[8];
        string[] dr = new string[8];
        int doing = 0;
        int _tmpRowIndex = -1;
        int _itemCount = 0;
        Tunnel tunnelEntity = new Tunnel();
        WireInfo wireInfoEntity = new WireInfo();
        WirePointInfo[] wpiEntity;
        int[] _arr = new int[5];
        DataSet _dsWirePoint = new DataSet();
        int _tunnelID;
        double _tmpDouble = 0;
        /*****************************/

        /// <summary>
        /// 构造方法
        /// </summary>
        public WireInfoEntering(MainFrm mainFrm)
        {
            this.MainForm = mainFrm;
            InitializeComponent();
            LibCommon.FormDefaultPropertiesSetter.SetEnteringFormDefaultProperties(this, Const_GM.WIRE_INFO_ADD);
            //自定义控件初始化
            LibEntity.TunnelDefaultSelect tunnelDefaultSelectEntity = LibBusiness.TunnelDefaultSelect.selectDefaultTunnel(WireInfoDbConstNames.TABLE_NAME);
            if (tunnelDefaultSelectEntity != null)
            {
                _arr = new int[5];
                _arr[0] = tunnelDefaultSelectEntity.MineID;
                _arr[1] = tunnelDefaultSelectEntity.HorizontalID;
                _arr[2] = tunnelDefaultSelectEntity.MiningAreaID;
                _arr[3] = tunnelDefaultSelectEntity.WorkingFaceID;
                this.selectTunnelUserControl1.setCurSelectedID(_arr);
            }
            else
            {
                this.selectTunnelUserControl1.loadMineName();
            }
            // 注册委托事件
            this.selectTunnelUserControl1.TunnelNameChanged +=
                InheritTunnelNameChanged;
            ////巷道信息赋值
            //Dictionary<string, string> flds = new Dictionary<string, string>();
            //flds.Add(GIS_Const.FIELD_HDID, _tunnelID.ToString());

            //List<Tuple<IFeature, IGeometry, Dictionary<string, string>>> selobjs = Global.commonclss.SearchFeaturesByGeoAndText(Global.centerfdlyr, flds);

            //// 序号
            //int xh = 0;
            //if (selobjs.Count > 0)
            //    xh = Convert.ToInt16(selobjs[0].Item3[GIS_Const.FIELD_XH]) + 1;
            //string bid = "";
            //string hdname = "";
            //DataSet dst = LibBusiness.TunnelInfoBLL.selectOneTunnelInfoByTunnelID(_tunnelID);
            //if (dst.Tables[0].Rows.Count > 0)
            //{
            //    bid = dst.Tables[0].Rows[0][LibBusiness.TunnelInfoDbConstNames.BINDINGID].ToString();
            //    hdname = dst.Tables[0].Rows[0][LibBusiness.TunnelInfoDbConstNames.TUNNEL_NAME].ToString();
            //}

            //dics.Clear();
            //dics.Add(GIS_Const.FIELD_HDID, _tunnelID.ToString());
            //dics.Add(GIS_Const.FIELD_ID, "0");
            //dics.Add(GIS_Const.FIELD_BS, "1");
            //dics.Add(GIS_Const.FIELD_BID, bid);
            //dics.Add(GIS_Const.FIELD_NAME, hdname);
            //dics.Add(GIS_Const.FIELD_XH, xh.ToString());
        }

        /// <summary>
        /// 构造方法
        /// </summary>
        /// <param name="array">矿井信息数组</param>
        /// <param name="wireInfoEntity">导线实体</param>
        public WireInfoEntering(int[] array, WireInfo wireInfoEntity, MainFrm mainFrm)
        {
            // 初始化主窗体变量
            this.MainForm = mainFrm;
            this.wireInfoEntity = wireInfoEntity;
            _arr = array;
            InitializeComponent();

            // 加载需要修改的导线数据
            loadWireInfoData();

            LibCommon.FormDefaultPropertiesSetter.SetEnteringFormDefaultProperties(this, Const_GM.WIRE_INFO_CHANGE);
            this.selectTunnelUserControl1.setCurSelectedID(_arr);
            _tunnelID = _arr[4];

            // 注册委托事件
            this.selectTunnelUserControl1.TunnelNameChanged +=
                InheritTunnelNameChanged;

            //巷道信息赋值
            //Dictionary<string, string> flds = new Dictionary<string, string>();
            //flds.Add(GIS_Const.FIELD_HDID, _tunnelID.ToString());
            //List<Tuple<IFeature, IGeometry, Dictionary<string, string>>> selobjs = Global.commonclss.SearchFeaturesByGeoAndText(Global.centerfdlyr, flds);
            //int xh = 0;
            //string bid = "";
            //string hdname = "";
            //DataSet dst=LibBusiness.TunnelInfoBLL.selectOneTunnelInfoByTunnelID(_tunnelID);
            //if (dst.Tables[0].Rows.Count > 0)
            //{
            //    bid = dst.Tables[0].Rows[0][LibBusiness.TunnelInfoDbConstNames.BINDINGID].ToString();
            //    hdname = dst.Tables[0].Rows[0][LibBusiness.TunnelInfoDbConstNames.TUNNEL_NAME].ToString();
            //}
            //if (selobjs.Count > 0)
            //    xh = Convert.ToInt16(selobjs[0].Item3[GIS_Const.FIELD_XH]) + 1;

            //dics.Clear();
            //dics.Add(GIS_Const.FIELD_HDID, _tunnelID.ToString());
            //dics.Add(GIS_Const.FIELD_ID, "0");
            //dics.Add(GIS_Const.FIELD_BS, "1");
            //dics.Add(GIS.GIS_Const.FIELD_BID, bid);
            //dics.Add(GIS_Const.FIELD_HDNAME, hdname);
            //dics.Add(GIS_Const.FIELD_XH, (xh + 1).ToString());
        }

        /// <summary>
        /// 委托事件
        /// </summary>
        /// <param name="sender"></param>
        private void InheritTunnelNameChanged(object sender, TunnelEventArgs e)
        {
            AutoChangeWireName();
        }

        /// <summary>
        /// 修改绑定数据
        /// </summary>
        private void loadWireInfoData()
        {
            addInfo();
            _itemCount = 0;
            _dsWirePoint = WirePointBLL.selectAllWirePointInfo(wireInfoEntity.WireInfoID);
            if (_dsWirePoint.Tables[0].Rows.Count > 0)
            {
                wpiEntity = new WirePointInfo[_dsWirePoint.Tables[0].Rows.Count];
                dgrdvWire.RowCount = wpiEntity.Length + 1;
                for (int i = 0; i < wpiEntity.Length; i++)
                {
                    int wpiID = Convert.ToInt32(_dsWirePoint.Tables[0].Rows[i][WirePointDbConstNames.ID].ToString());
                    wpiEntity[i] = WirePointBLL.returnWirePointInfo(wpiID);

                    dgrdvWire[0, i].Value = wpiEntity[i].WirePointID;
                    dgrdvWire[1, i].Value = wpiEntity[i].CoordinateX;
                    dgrdvWire[2, i].Value = wpiEntity[i].CoordinateY;
                    dgrdvWire[3, i].Value = wpiEntity[i].CoordinateZ;
                    dgrdvWire[4, i].Value = wpiEntity[i].LeftDis;
                    dgrdvWire[5, i].Value = wpiEntity[i].RightDis;
                    dgrdvWire[6, i].Value = wpiEntity[i].TopDis;
                    dgrdvWire[7, i].Value = wpiEntity[i].BottomDis;
                    _itemCount++;
                }
            }
            txtWireName.Text = wireInfoEntity.WireName;
            txtWireLevel.Text = wireInfoEntity.WireLevel;
            dtpMeasureDate.Value = wireInfoEntity.MeasureDate;
            cboVobserver.Text = wireInfoEntity.Vobserver;
            cboVobserver.Text = wireInfoEntity.Vobserver;
            cboCounter.Text = wireInfoEntity.Counter;
            cboCounter.Text = wireInfoEntity.Counter;
            dtpCountDate.Value = wireInfoEntity.CountDate;
            cboChecker.Text = wireInfoEntity.Checker;
            cboChecker.Text = wireInfoEntity.Checker;
            dtpCheckDate.Value = wireInfoEntity.CheckDate;
        }

        /// <summary>
        /// 提交
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnSubmit_Click(object sender, EventArgs e)
        {
            //去掉无用空行
            for (int i = 0; i < dgrdvWire.RowCount - 1; i++)
            {
                if (this.dgrdvWire.Rows[i].Cells[0].Value == null &&
                    this.dgrdvWire.Rows[i].Cells[1].Value == null &&
                    this.dgrdvWire.Rows[i].Cells[2].Value == null &&
                    this.dgrdvWire.Rows[i].Cells[3].Value == null &&
                    this.dgrdvWire.Rows[i].Cells[4].Value == null &&
                    this.dgrdvWire.Rows[i].Cells[5].Value == null &&
                    this.dgrdvWire.Rows[i].Cells[6].Value == null &&
                    this.dgrdvWire.Rows[i].Cells[7].Value == null)
                {
                    this.dgrdvWire.Rows.RemoveAt(i);
                }
            }

            if (!this.check())
            {
                DialogResult = DialogResult.None;
                return;
            }

            //判断导线点录入个数是否小于2
            if (this.Text == Const_GM.WIRE_INFO_ADD)
            {
                DataSet ds = new DataSet();
                //获取巷道ID
                tunnelEntity.TunnelId = selectTunnelUserControl1.ITunnelId;
                //获取巷道对应导线信息
                ds = WireInfoBLL.selectAllWireInfo(tunnelEntity);
                if (ds.Tables[0].Rows.Count > 0)
                {
                    if (Convert.ToString(ds.Tables[0].Rows[0][WireInfoDbConstNames.WIRE_NAME]) == txtWireName.Text)
                    {
                        for (int i = 0; i < dgrdvWire.Rows.Count; i++)
                        {
                            if (WirePointBLL.isWirePointNameExist(dgrdvWire[0, i].Value.ToString()))
                            {
                                Alert.alert(Const_GM.WIRE_INFO_MSG_WIRE_POINT_NAME_DOUBLE);
                                return;
                            }
                        }
                    }
                    else if (dgrdvWire.Rows.Count < 3)  //添加时最后有一个空行
                    {
                        Alert.alert(Const_GM.WIRE_INFO_MSG_POINT_MUST_MORE_THAN_TWO);
                        return;
                    }
                }
            }
            DialogResult = DialogResult.OK;

            List<WirePointInfo> lstWirePointInfoEnt;
            string sADDorCHANGE = "";
            if (this.Text == Const_GM.WIRE_INFO_ADD)
            {
                sADDorCHANGE = "ADD";
                /// 2014.2.26 lyf 绘制导线点和巷道，下同
                lstWirePointInfoEnt = new List<WirePointInfo>();
                lstWirePointInfoEnt = insertWireInfo();
                if (lstWirePointInfoEnt != null)
                {
                    DrawWirePoint(lstWirePointInfoEnt, sADDorCHANGE);

                    DialogResult dlgResult = MessageBox.Show("是否同时绘制巷道？", "提示", MessageBoxButtons.YesNo,
                        MessageBoxIcon.Question, MessageBoxDefaultButton.Button1);
                    if (dlgResult == DialogResult.Yes)
                    {

                        //DrawTunnel(lstWirePointInfoEnt, sADDorCHANGE);
                        //巷道信息赋值
                        //Dictionary<string, string> flds = new Dictionary<string, string>();
                        //flds.Add(GIS_Const.FIELD_HDID, tunnelEntity.Tunnel.ToString());
                        //List<Tuple<IFeature, IGeometry, Dictionary<string, string>>> selobjs = Global.commonclss.SearchFeaturesByGeoAndText(Global.centerfdlyr, flds);

                        //// 序号
                        //int xh = 0;
                        //if (selobjs.Count > 0)
                        //    xh = Convert.ToInt16(selobjs[0].Item3[GIS_Const.FIELD_XH]) + 1;
                        //string bid = "";
                        //string hdname = "";
                        //double hdwid = Global.linespace;//给个默认的值
                        DataSet dst = LibBusiness.TunnelInfoBLL.selectOneTunnelInfoByTunnelID(_tunnelID);
                        //if (dst.Tables[0].Rows.Count > 0)
                        //{
                        //    bid = dst.Tables[0].Rows[0][LibBusiness.TunnelInfoDbConstNames.BINDINGID].ToString();
                        //    hdname = dst.Tables[0].Rows[0][LibBusiness.TunnelInfoDbConstNames.TUNNEL_NAME].ToString();
                        //    hdwid = Convert.ToDouble(dst.Tables[0].Rows[0][LibBusiness.TunnelInfoDbConstNames.TUNNEL_WID]);
                        //}
                        //dics.Clear();
                        //dics.Add(GIS_Const.FIELD_HDID, tunnelEntity.Tunnel.ToString());
                        //dics.Add(GIS_Const.FIELD_ID, "0");
                        //dics.Add(GIS_Const.FIELD_BS, "1");
                        //dics.Add(GIS.GIS_Const.FIELD_BID, bid);
                        //dics.Add(GIS_Const.FIELD_HDNAME,hdname);
                        //dics.Add(GIS_Const.FIELD_XH, (xh + 1).ToString());

                        // 绘制巷道
                        double hdwid = 0.0;
                        dics = ConstructDics(dst, out hdwid);
                        AddHdbyPnts(lstWirePointInfoEnt, dics, hdwid);
                    }
                }
            }

            if (this.Text == Const_GM.WIRE_INFO_CHANGE)
            {
                sADDorCHANGE = "CHANGE";
                /// 2014.2.26 lyf
                WirePointInfo[] wirePointInfoEnt = updateWireInfo();
                lstWirePointInfoEnt = new List<WirePointInfo>();
                if (wirePointInfoEnt != null)
                {
                    lstWirePointInfoEnt = wirePointInfoEnt.ToList<WirePointInfo>();
                    DrawWirePoint(lstWirePointInfoEnt, sADDorCHANGE);

                    DialogResult dlgResult = MessageBox.Show("是否同时更新巷道图形？", "提示", MessageBoxButtons.YesNo,
                                                MessageBoxIcon.Question, MessageBoxDefaultButton.Button1);
                    if (dlgResult == DialogResult.Yes)
                    {
                        //DrawTunnel(lstWirePointInfoEnt, sADDorCHANGE);
                        DataSet dst = LibBusiness.TunnelInfoBLL.selectOneTunnelInfoByTunnelID(_tunnelID);
                        double hdwid = 0.0;
                        dics = ConstructDics(dst, out hdwid);
                        if (dst.Tables[0].Rows.Count > 0)
                        {
                            UpdateHdbyPnts(lstWirePointInfoEnt, dics, hdwid);
                        }
                    }

                }
            }
        }

        private Dictionary<string, string> ConstructDics(DataSet dst, out double hdwid)
        {
            //巷道信息赋值
            hdwid = 0.0;
            Dictionary<string, string> flds = new Dictionary<string, string>();
            flds.Add(GIS_Const.FIELD_HDID, tunnelEntity.TunnelId.ToString());
            List<Tuple<IFeature, IGeometry, Dictionary<string, string>>> selobjs = Global.commonclss.SearchFeaturesByGeoAndText(Global.centerfdlyr, flds);

            int xh = 0;
            if (selobjs.Count > 0)
                xh = Convert.ToInt16(selobjs[0].Item3[GIS_Const.FIELD_XH]) + 1;
            string bid = "", hdname = "";
            if (dst.Tables[0].Rows.Count > 0)
            {
                bid = dst.Tables[0].Rows[0][LibBusiness.TunnelInfoDbConstNames.BINDINGID].ToString();
                hdname = dst.Tables[0].Rows[0][LibBusiness.TunnelInfoDbConstNames.TUNNEL_NAME].ToString();
                hdwid = Convert.ToDouble(dst.Tables[0].Rows[0][LibBusiness.TunnelInfoDbConstNames.TUNNEL_WID]);
            }
            dics.Clear();
            dics.Add(GIS_Const.FIELD_HDID, tunnelEntity.TunnelId.ToString());
            dics.Add(GIS_Const.FIELD_ID, "0");
            dics.Add(GIS_Const.FIELD_BS, "1");
            dics.Add(GIS.GIS_Const.FIELD_BID, bid);
            dics.Add(GIS_Const.FIELD_HDNAME, hdname);
            dics.Add(GIS_Const.FIELD_XH, (xh + 1).ToString());
            return dics;
        }
        #region 绘制导线点和巷道图形
        private List<ESRI.ArcGIS.Geometry.IPoint> dpts = new List<ESRI.ArcGIS.Geometry.IPoint>();
        private List<ESRI.ArcGIS.Geometry.IPoint> leftpts = new List<ESRI.ArcGIS.Geometry.IPoint>();//记录左侧平行线坐标
        private List<ESRI.ArcGIS.Geometry.IPoint> rightpts = new List<ESRI.ArcGIS.Geometry.IPoint>();//记录右侧平行线坐标
        private Dictionary<string, string> dics = new Dictionary<string, string>();//属性字典

        /// <summary>
        /// 通过（关键/导线）点绘制巷道
        /// </summary>
        /// <param name="wirepntcols">导线信息列表</param>
        /// <param name="dics">巷道属性</param>
        /// <param name="hdwid">巷道宽度</param>
        private void AddHdbyPnts(List<WirePointInfo> wirepntcols, Dictionary<string, string> dics, double hdwid)
        {
            List<IPoint> rightresults = null;
            List<IPoint> leftresults = null;
            List<IPoint> results = null;

            if (wirepntcols == null || wirepntcols.Count == 0)
                return;

            List<IPoint> pntcols = new List<IPoint>();
            for (int i = 0; i < wirepntcols.Count; i++)
            {
                IPoint pnt = new PointClass();
                pnt.X = wirepntcols[i].CoordinateX;
                pnt.Y = wirepntcols[i].CoordinateY;
                pnt.Z = wirepntcols[i].CoordinateZ;
                pntcols.Add(pnt);
            }

            Global.cons.AddHangdaoToLayer(pntcols, dics, Global.pntlyr, wirepntcols);//添加中线上的点到导线点图层中
            Global.cons.AddDxdLines(pntcols, dics, Global.pntlinlyr, wirepntcols);//添加导线点线图层符号
            Global.cons.AddHangdaoToLayer(pntcols, dics, Global.centerlyr);//添加中心线到线图层中
            Global.cons.AddFDLineToLayer(pntcols, dics, Global.centerfdlyr, 1);//添加分段中心线到中心线分段图层中

            //#################计算交点坐标######################
            rightpts = Global.cons.GetLRParallelPnts(pntcols, hdwid, 1);//右侧平行线上的端点串
            leftpts = Global.cons.GetLRParallelPnts(pntcols, hdwid, 0);//左侧平行线上的端点串

            //rightresults = Global.cons.CalculateRegPnts(rightpts);
            //leftresults = Global.cons.CalculateRegPnts(leftpts);
            //results = Global.cons.ConstructPnts(rightresults, leftresults);
            results = Global.cons.ConstructPnts(rightpts, leftpts);

            ////在巷道面显示面中绘制巷道面  
            Global.cons.AddHangdaoToLayer(results, dics, Global.hdfdfulllyr);//添加巷道到巷道图层中
            //Global.cons.AddFDRegToLayer(rightresults, leftresults, pntcols, dics, Global.hdfdlyr);
            //Global.cons.AddHangdaoToLayer(rightpts, dics, Global.pntlyr);
            //Global.cons.AddHangdaoToLayer(leftpts, dics, Global.pntlyr);
            Global.cons.AddFDRegToLayer(rightpts, leftpts, pntcols, dics, Global.hdfdlyr, hdwid);
            Global.pActiveView.Refresh();
        }

        /// <summary>
        /// 更新巷道
        /// </summary>
        /// <param name="wirepntcols"></param>
        /// <param name="dics"></param>
        private void UpdateHdbyPnts(List<WirePointInfo> wirepntcols, Dictionary<string, string> dics, double hdwid)
        {
            List<IPoint> rightresults = null;
            List<IPoint> leftresults = null;
            List<IPoint> results = null;

            if (wirepntcols == null || wirepntcols.Count == 0)
                return;

            List<IPoint> pntcols = new List<IPoint>();
            for (int i = 0; i < wirepntcols.Count; i++)
            {
                IPoint pnt = new PointClass();
                pnt.X = wirepntcols[i].CoordinateX;
                pnt.Y = wirepntcols[i].CoordinateY;
                pnt.Z = wirepntcols[i].CoordinateZ;
                pntcols.Add(pnt);
            }
            //清除图层上对应的信息
            string sql = "\"" + GIS.GIS_Const.FIELD_HDID + "\"='" + wireInfoEntity.TunnelID.ToString() + "'";
            Global.commonclss.DelFeatures(Global.pntlyr, sql);
            Global.commonclss.DelFeatures(Global.pntlinlyr, sql);
            Global.commonclss.DelFeatures(Global.centerlyr, sql);
            Global.commonclss.DelFeatures(Global.centerfdlyr, sql);
            Global.commonclss.DelFeatures(Global.hdfdfulllyr, sql);
            Global.commonclss.DelFeatures(Global.hdfdlyr, sql);
            Global.commonclss.DelFeatures(Global.dslyr, sql);
            //Global.commonclss.DelFeaturesByQueryFilter(Global.pntlyr, sql);
            //Global.commonclss.DelFeaturesByQueryFilter(Global.pntlinlyr, sql);
            //Global.commonclss.DelFeaturesByQueryFilter(Global.centerlyr, sql);
            //Global.commonclss.DelFeaturesByQueryFilter(Global.centerfdlyr, sql);
            //Global.commonclss.DelFeaturesByQueryFilter(Global.hdfdfulllyr, sql);
            //Global.commonclss.DelFeaturesByQueryFilter(Global.hdfdlyr, sql);
            //Global.commonclss.DelFeaturesByQueryFilter(Global.dslyr, sql);
            //重新添加
            Global.cons.AddHangdaoToLayer(pntcols, dics, Global.pntlyr, wirepntcols);//添加中线上的点到导线点图层中
            Global.cons.AddDxdLines(pntcols, dics, Global.pntlinlyr, wirepntcols);//添加导线点线
            Global.cons.AddHangdaoToLayer(pntcols, dics, Global.centerlyr);//添加中心线到线图层中
            Global.cons.AddFDLineToLayer(pntcols, dics, Global.centerfdlyr, 1);//添加分段中心线到中心线分段图层中
            //#################计算交点坐标######################
            rightpts = Global.cons.GetLRParallelPnts(pntcols, hdwid, 1);//右侧平行线上的端点串
            leftpts = Global.cons.GetLRParallelPnts(pntcols, hdwid, 0);//左侧平行线上的端点串
            //rightresults = Global.cons.CalculateRegPnts(rightpts);
            //leftresults = Global.cons.CalculateRegPnts(leftpts);
            //results = Global.cons.ConstructPnts(rightresults, leftresults);
            //Global.cons.AddHangdaoToLayer(rightpts, dics, Global.pntlyr);
            //Global.cons.AddHangdaoToLayer(leftpts, dics, Global.pntlyr);
            results = Global.cons.ConstructPnts(rightpts, leftpts);
            //在巷道面显示面中绘制巷道面  
            //Global.cons.AddHangdaoToLayer(rightpts, dics, Global.pntlyr);
            //Global.cons.AddHangdaoToLayer(leftpts, dics, Global.pntlyr);
            Global.cons.AddHangdaoToLayer(results, dics, Global.hdfdfulllyr);//添加巷道到巷道图层中
            //Global.cons.AddFDRegToLayer(rightresults, leftresults, pntcols, dics, Global.hdfdlyr);
            Global.cons.AddFDRegToLayer(rightpts, leftpts, pntcols, dics, Global.hdfdlyr, hdwid);
            Global.pActiveView.Refresh();
        }

        /// <summary>
        /// 根据坐标绘制导线点
        /// </summary>
        /// <param name="lstWPIE">导线坐标（List）</param>
        private void DrawWirePoint(List<WirePointInfo> lstWPIE, string addOrChange)
        {
            WirePointInfo wirePtInfo = new WirePointInfo();
            ESRI.ArcGIS.Geometry.IPoint pt = new ESRI.ArcGIS.Geometry.Point();

            //找到导线点图层
            IMap map = new MapClass();
            map = DataEditCommon.g_pMap;
            string layerName = GIS.LayerNames.DEFALUT_WIRE_PT;//“默认_导线点”图层
            IFeatureLayer featureLayer = new FeatureLayerClass();
            featureLayer = GIS.Common.LayerHelper.GetLayerByName(map, layerName);///获得图层

            if (featureLayer == null)
            {
                MessageBox.Show("没有找到" + layerName + "图层，将不能绘制导线点。", "提示", MessageBoxButtons.OK);
                return;
            }

            GIS.SpecialGraphic.DrawTunnels drawWirePt = new GIS.SpecialGraphic.DrawTunnels();
            //修改导线点操作，要先删除原有导线点要素
            if (addOrChange == "CHANGE")
            {
                for (int i = 0; i < lstWPIE.Count; i++)
                {
                    wirePtInfo = lstWPIE[i];
                    DataEditCommon.DeleteFeatureByBId(featureLayer, wirePtInfo.BindingID);
                }
            }

            for (int i = 0; i < lstWPIE.Count; i++)
            {
                wirePtInfo = lstWPIE[i];
                pt.X = wirePtInfo.CoordinateX;
                pt.Y = wirePtInfo.CoordinateY;
                pt.Z = wirePtInfo.CoordinateZ;

                drawWirePt.CreatePoint(featureLayer, pt, wirePtInfo.BindingID);///绘制点
            }
        }

        /// <summary>
        /// 根据导线点坐标绘制巷道
        /// </summary>
        /// <param name="lstWPIE"></param>
        private void DrawTunnel(List<WirePointInfo> lstWPIE, string addOrChange)
        {
            ///根据导线点计算巷道边线点
            WirePointInfo[] arrayWPtInfo = new WirePointInfo[] { };
            arrayWPtInfo = lstWPIE.ToArray();
            Vector3_DW[] verticesLeftBtmRet = null;
            Vector3_DW[] verticesRightBtmRet = null;

            TunnelPointsCalculation tunnelPtsCal = new TunnelPointsCalculation();
            bool isCalSuccess = tunnelPtsCal.CalcLeftAndRightVertics(arrayWPtInfo, ref verticesLeftBtmRet, ref  verticesRightBtmRet);

            if (!isCalSuccess)
            {
                MessageBox.Show("根据导线点计算巷道未成功！");
            }
            else
            {
                //找到巷道图层
                IMap map = new MapClass();
                map = DataEditCommon.g_pMap;
                string layerName = GIS.LayerNames.DEFALUT_TUNNEL;//“默认_巷道”图层
                IFeatureLayer featureLayer = new FeatureLayerClass();
                featureLayer = GIS.Common.LayerHelper.GetLayerByName(map, layerName);//获得图层

                if (featureLayer == null)
                {
                    MessageBox.Show("没有找到" + layerName + "图层，将不能绘制巷道。", "提示", MessageBoxButtons.OK);
                    return;
                }

                GIS.SpecialGraphic.DrawTunnels drawWirePt = new GIS.SpecialGraphic.DrawTunnels();
                //修改导线点操作，要先删除依据原有导线点所生成的巷道要素
                if (addOrChange == "CHANGE")
                {
                    DataEditCommon.DeleteFeatureByBId(featureLayer, _tunnelID.ToString());
                }

                //绘制巷道左边线
                List<IPoint> lstLeftBtmRet = new List<IPoint>();
                lstLeftBtmRet = GetTunnelPts(verticesLeftBtmRet);

                if (lstLeftBtmRet == null) return;

                drawWirePt.CreateLine(featureLayer, lstLeftBtmRet, _tunnelID);

                //绘制巷道右边线
                List<ESRI.ArcGIS.Geometry.IPoint> lstRightBtmRet = new List<ESRI.ArcGIS.Geometry.IPoint>();
                lstRightBtmRet = GetTunnelPts(verticesRightBtmRet);
                drawWirePt.CreateLine(featureLayer, lstRightBtmRet, _tunnelID);
            }
        }

        /// <summary>
        /// 获得导线边线点坐标集
        /// </summary>
        /// <param name="verticesBtmRet">Vector3_DW数据</param>
        /// <returns>导线边线点坐标集List</returns>
        private List<ESRI.ArcGIS.Geometry.IPoint> GetTunnelPts(Vector3_DW[] verticesBtmRet)
        {
            List<ESRI.ArcGIS.Geometry.IPoint> lstBtmRet = new List<ESRI.ArcGIS.Geometry.IPoint>();
            try
            {
                Vector3_DW vector3dw;
                ESRI.ArcGIS.Geometry.IPoint pt;
                for (int i = 0; i < verticesBtmRet.Length; i++)
                {
                    vector3dw = new Vector3_DW();
                    vector3dw = verticesBtmRet[i];
                    pt = new ESRI.ArcGIS.Geometry.PointClass();
                    pt.X = vector3dw.X;
                    pt.Y = vector3dw.Y;
                    pt.Z = vector3dw.Z;
                    if (!lstBtmRet.Contains(pt))
                    {
                        lstBtmRet.Add(pt);
                    }
                }

                return lstBtmRet;
            }
            catch
            {
                return null;
            }
        }

        #endregion 绘制导线点和巷道图形

        /// <summary>
        /// 导线实体赋值
        /// </summary>
        private void setWireInfoEntity()
        {
            wireInfoEntity.TunnelID = selectTunnelUserControl1.ITunnelId; ;
            //tunnelEntity = TunnelInfoBLL.selectTunnelInfoByTunnelID(wireInfoEntity.Tunnel);
            //导线名称
            wireInfoEntity.WireName = txtWireName.Text;
            //导线级别
            wireInfoEntity.WireLevel = txtWireLevel.Text;
            //测量日期
            wireInfoEntity.MeasureDate = dtpMeasureDate.Value;
            //观测者
            wireInfoEntity.Vobserver = cboVobserver.Text;
            //txtVobserver.Text;
            //计算者
            wireInfoEntity.Counter = cboCounter.Text;
            //txtCounter.Text;
            //计算日期
            wireInfoEntity.CountDate = dtpCountDate.Value;
            //校核者
            wireInfoEntity.Checker = cboChecker.Text;
            //txtChecker.Text
            //校核日期
            wireInfoEntity.CheckDate = dtpCheckDate.Value;
        }

        /// <summary>
        /// 导线点实体赋值
        /// </summary>
        /// <param name="i">Datagridview行号</param>
        /// <returns>导线点实体</returns>
        private WirePointInfo setWirePointEntity(int i)
        {
            // 最后一行为空行时，跳出循环
            if (i == this.dgrdvWire.RowCount - 1)
            {
                return null;
            }
            // 创建导线点实体
            WirePointInfo wirePointInfoEntity = new WirePointInfo();
            if (this.Text == Const_GM.WIRE_INFO_CHANGE)
            {
                if (i < wpiEntity.Length)
                {
                    wirePointInfoEntity.ID = wpiEntity[i].ID;
                }
            }

            //导线点编号
            if (this.dgrdvWire.Rows[i].Cells[0] != null)
            {
                wirePointInfoEntity.WirePointID = this.dgrdvWire.Rows[i].Cells[0].Value.ToString();
            }
            //坐标X
            if (this.dgrdvWire.Rows[i].Cells[1].Value != null)
            {
                if (double.TryParse(this.dgrdvWire.Rows[i].Cells[1].Value.ToString(), out _tmpDouble))
                {
                    wirePointInfoEntity.CoordinateX = _tmpDouble;
                    _tmpDouble = 0;
                }
            }
            //坐标Y
            if (this.dgrdvWire.Rows[i].Cells[2].Value != null)
            {
                if (double.TryParse(this.dgrdvWire.Rows[i].Cells[2].Value.ToString(), out _tmpDouble))
                {
                    wirePointInfoEntity.CoordinateY = _tmpDouble;
                    _tmpDouble = 0;
                }
            }
            //坐标Z
            if (this.dgrdvWire.Rows[i].Cells[3].Value != null)
            {
                if (double.TryParse(this.dgrdvWire.Rows[i].Cells[3].Value.ToString(), out _tmpDouble))
                {
                    wirePointInfoEntity.CoordinateZ = _tmpDouble;
                    _tmpDouble = 0;
                }
            }
            //距左帮距离
            if (this.dgrdvWire.Rows[i].Cells[4].Value != null)
            {
                if (double.TryParse(this.dgrdvWire.Rows[i].Cells[4].Value.ToString(), out _tmpDouble))
                {
                    wirePointInfoEntity.LeftDis = _tmpDouble;
                    _tmpDouble = 0;
                }
            }
            //距右帮距离
            if (this.dgrdvWire.Rows[i].Cells[5].Value != null)
            {
                if (double.TryParse(this.dgrdvWire.Rows[i].Cells[5].Value.ToString(), out _tmpDouble))
                {
                    wirePointInfoEntity.RightDis = _tmpDouble;
                    _tmpDouble = 0;
                }
            }
            //距顶板距离
            if (this.dgrdvWire.Rows[i].Cells[6].Value != null)
            {
                if (double.TryParse(this.dgrdvWire.Rows[i].Cells[6].Value.ToString(), out _tmpDouble))
                {
                    wirePointInfoEntity.TopDis = _tmpDouble;
                    _tmpDouble = 0;
                }
            }
            //距底板距离
            if (this.dgrdvWire.Rows[i].Cells[7].Value != null)
            {
                if (double.TryParse(this.dgrdvWire.Rows[i].Cells[7].Value.ToString(), out _tmpDouble))
                {
                    wirePointInfoEntity.BottomDis = _tmpDouble;
                    _tmpDouble = 0;
                }
            }
            wirePointInfoEntity.WireInfoID = wireInfoEntity.WireInfoID;

            return wirePointInfoEntity;
        }

        /// <summary>
        /// 2014.2.26 lyf 修改函数，返回导线点List，为绘制导线点图形
        /// </summary>
        /// <returns>导线点List</returns>
        private List<WirePointInfo> insertWireInfo()
        {
            setWireInfoEntity();

            DialogResult = DialogResult.OK;
            //导线信息登陆
            bool bResult = false;
            //无导线时插入
            if (WireInfoBLL.selectAllWireInfo(tunnelEntity).Tables[0].Rows.Count == 0)
            {
                LibBusiness.TunnelDefaultSelect.InsertDefaultTunnel(WireInfoDbConstNames.TABLE_NAME, selectTunnelUserControl1.ITunnelId);
                bResult = WireInfoBLL.insertWireInfo(wireInfoEntity);
                if (bResult)
                {
                    UpdateWarningDataMsg msg = new UpdateWarningDataMsg(Const.INVALID_ID, selectTunnelUserControl1.ITunnelId,
                        WireInfoDbConstNames.TABLE_NAME, OPERATION_TYPE.ADD, wireInfoEntity.MeasureDate);
                    this.MainForm.SendMsg2Server(msg);
                }
            }
            //导线存在时跳过
            else
            {
                bResult = true;
            }
            //导线编号
            wireInfoEntity.WireInfoID = Convert.ToInt32(WireInfoBLL.selectAllWireInfo(BasicInfoManager.getInstance().getTunnelByID(wireInfoEntity.TunnelID)).Tables[0].Rows[0][WireInfoDbConstNames.ID]);
            //导线点信息登陆
            List<WirePointInfo> wirePointInfoEntityList = new List<WirePointInfo>();
            for (int i = 0; i < this.dgrdvWire.RowCount; i++)
            {
                WirePointInfo wirePointInfoEntity = new WirePointInfo();

                wirePointInfoEntity = setWirePointEntity(i);

                if (wirePointInfoEntity == null)
                {
                    break;
                }

                wirePointInfoEntity.BindingID = IDGenerator.NewBindingID();

                wirePointInfoEntityList.Add(wirePointInfoEntity);
            }

            if (bResult)
            {
                foreach (WirePointInfo wirePointInfoEntity in wirePointInfoEntityList)
                {
                    bResult = WirePointBLL.insertWirePointInfo(wirePointInfoEntity);
                    if (bResult)
                    {
                        UpdateWarningDataMsg msg = new UpdateWarningDataMsg(Const.INVALID_ID, selectTunnelUserControl1.ITunnelId,
                            WireInfoDbConstNames.TABLE_NAME, OPERATION_TYPE.ADD, wireInfoEntity.MeasureDate);
                        this.MainForm.SendMsg2Server(msg);
                    }
                }
            }
            return wirePointInfoEntityList;
        }

        /// <summary>
        /// 2014.2.26 lyf 修改函数，返回导线点List，为绘制导线点图形
        /// </summary>
        /// <returns>导线点List</returns>
        private WirePointInfo[] updateWireInfo()
        {
            setWireInfoEntity();

            WirePointInfo[] wirePointInfoEnt = new WirePointInfo[dgrdvWire.RowCount - 1];
            for (int i = 0; i < dgrdvWire.RowCount - 1; i++)
            {
                // 创建导线点实体
                WirePointInfo wirePointInfoEntity = new WirePointInfo();
                wirePointInfoEntity = setWirePointEntity(i);
                if (wirePointInfoEntity == null)
                {
                    break;
                }

                wirePointInfoEnt[i] = wirePointInfoEntity;

            }

            //导线信息登陆
            _tunnelID = selectTunnelUserControl1.ITunnelId;
            LibBusiness.TunnelDefaultSelect.UpdateDefaultTunnel(WireInfoDbConstNames.TABLE_NAME, selectTunnelUserControl1.ITunnelId);
            bool bResult = WireInfoBLL.updateWireInfo(wireInfoEntity, _tunnelID);
            //导线点信息登陆
            if (bResult)
            {
                for (int j = 0; j < dgrdvWire.Rows.Count - 1; j++)
                {
                    if (j < _dsWirePoint.Tables[0].Rows.Count)
                    {
                        //修改导线点
                        bResult = WirePointBLL.updateWirePointInfo(wirePointInfoEnt[j], wireInfoEntity);
                        //socket
                        if (bResult)
                        {
                            UpdateWarningDataMsg msg = new UpdateWarningDataMsg(Const.INVALID_ID, selectTunnelUserControl1.ITunnelId,
                                WireInfoDbConstNames.TABLE_NAME, OPERATION_TYPE.UPDATE, wireInfoEntity.MeasureDate);
                            this.MainForm.SendMsg2Server(msg);
                        }
                    }
                    else
                    {
                        //超出数量部分做添加操作
                        //BindingID
                        wirePointInfoEnt[j].BindingID = IDGenerator.NewBindingID();
                        //添加导线点
                        bResult = WirePointBLL.insertWirePointInfo(wirePointInfoEnt[j]);
                        //socket
                        if (bResult)
                        {
                            UpdateWarningDataMsg msg = new UpdateWarningDataMsg(Const.INVALID_ID, selectTunnelUserControl1.ITunnelId,
                                WireInfoDbConstNames.TABLE_NAME, OPERATION_TYPE.ADD, wireInfoEntity.MeasureDate);
                            this.MainForm.SendMsg2Server(msg);
                        }
                    }
                }

                //导线点实体
                WirePointInfo wirePointInfoEntity = new WirePointInfo();
                //当条数少于导线点个数时，多于部分做删除处理
                if (dgrdvWire.Rows.Count <= _itemCount)
                {
                    for (int i = dgrdvWire.Rows.Count - 1; i < _itemCount; i++)
                    {
                        wirePointInfoEntity.ID = Convert.ToInt32(_dsWirePoint.Tables[0].Rows[i][WirePointDbConstNames.ID].ToString());
                        wireInfoEntity.WireInfoID = Convert.ToInt32(_dsWirePoint.Tables[0].Rows[i][WirePointDbConstNames.WIRE_INFO_ID].ToString());
                        //只剩一个空行时，即所有导线点信息全被删除时
                        //删除导线，导线点
                        if (dgrdvWire.Rows.Count == 1)
                        {
                            bResult = WireInfoBLL.deleteWireInfo(wireInfoEntity);
                            bResult = WirePointBLL.deleteWirePointInfo(wirePointInfoEntity);
                        }
                        //只删除多于导线点
                        else
                        {
                            bResult = WirePointBLL.deleteWirePointInfo(wirePointInfoEntity);
                        }
                    }
                }
            }
            //返回导线点信息组
            return wirePointInfoEnt;
        }

        /// <summary>
        /// 取消
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnCancel_Click(object sender, EventArgs e)
        {
            // 关闭窗口
            this.Close();
        }

        /// <summary>
        /// 验证画面入力数据
        /// </summary>
        /// <returns>验证结果：true 通过验证, false未通过验证</returns>
        private bool check()
        {
            for (int i = 0; i < dgrdvWire.Rows.Count; i++)
            {
                dgrdvWire.BackgroundColor = Const.NO_ERROR_FIELD_COLOR;
            }
            //// 判断巷道信息是否选择
            if (selectTunnelUserControl1.ITunnelId == 0)
            {
                Alert.alert(Const.MSG_PLEASE_CHOOSE + Const_GM.TUNNEL + Const.SIGN_EXCLAMATION_MARK);
                return false;
            }
            if (this.Text == Const_GM.WIRE_INFO_ADD && !AutoChangeWireName())
            {
                return false;
            }
            if (Validator.IsEmpty(txtWireName.Text))
            {
                txtWireName.BackColor = Const.ERROR_FIELD_COLOR;
                Alert.alert(Const.MSG_PLEASE_TYPE_IN + Const_GM.WIRE_NAME + Const.SIGN_EXCLAMATION_MARK);
                return false;
            }
            else
            {
                txtWireName.BackColor = Const.NO_ERROR_FIELD_COLOR;
            }
            //判断导线名称是否含有特殊字符
            if (!Check.checkSpecialCharacters(txtWireName, Const_GM.WIRE_NAME))
            {
                return false;
            }
            // 判断导线点编号是否入力
            if (this.dgrdvWire.Rows.Count - 1 == 0)
            {
                Alert.alert(Const.MSG_PLEASE_TYPE_IN + Const_GM.WIRE_POINT_ID + Const.SIGN_EXCLAMATION_MARK);
                return false;
            }
            //dgrdvWire内部判断
            for (int i = 0; i < this.dgrdvWire.RowCount; i++)
            {
                // 最后一行为空行时，跳出循环
                if (i == dgrdvWire.RowCount - 1)
                {
                    break;
                }
                DataGridViewTextBoxCell cell = dgrdvWire.Rows[i].Cells[0] as DataGridViewTextBoxCell;
                // 判断导线点编号是否入力
                if (cell.Value == null)
                {
                    cell.Style.BackColor = Const.ERROR_FIELD_COLOR;
                    Alert.alert(Const_GM.WIRE_POINT_ID + Const.MSG_NOT_NULL + Const.SIGN_EXCLAMATION_MARK);
                    return false;
                }
                else
                {
                    cell.Style.BackColor = Const.NO_ERROR_FIELD_COLOR;
                }

                //判断导线点编号是否存在
                if (this.Text == Const_GM.WIRE_INFO_ADD)
                {
                    //导线点是否存在
                    if (new WireInfoBLL().isWirePointExist(dgrdvWire.Rows[i].Cells[0].Value.ToString(), wireInfoEntity.WireInfoID))
                    {
                        cell.Style.BackColor = Const.ERROR_FIELD_COLOR;
                        Alert.alert(Const_GM.WIRE_POINT_ID + Const.MSG_ALREADY_HAVE + Const.SIGN_EXCLAMATION_MARK);
                        return false;
                    }
                    else
                    {
                        cell.Style.BackColor = Const.NO_ERROR_FIELD_COLOR;
                    }
                }
                //判断导线点编号是否有输入重复
                for (int j = 0; j < i; j++)
                {
                    if (dgrdvWire[0, j].Value.ToString() == dgrdvWire[0, i].Value.ToString())
                    {
                        cell.Style.BackColor = Const.ERROR_FIELD_COLOR;
                        dgrdvWire[0, j].Style.BackColor = Const.ERROR_FIELD_COLOR;
                        Alert.alert(Const_GM.WIRE_POINT_ID + Const.MSG_DOUBLE_EXISTS + Const.SIGN_EXCLAMATION_MARK);
                        return false;
                    }
                    else
                    {
                        cell.Style.BackColor = Const.NO_ERROR_FIELD_COLOR;
                        dgrdvWire[0, j].Style.BackColor = Const.NO_ERROR_FIELD_COLOR;
                    }
                }

                //判断坐标X是否入力
                cell = dgrdvWire.Rows[i].Cells[1] as DataGridViewTextBoxCell;
                if (cell.Value == null)
                {
                    cell.Style.BackColor = Const.ERROR_FIELD_COLOR;
                    Alert.alert(Const.rowNotNull(i, Const_GM.X));
                    return false;
                }
                else
                {
                    cell.Style.BackColor = Const.NO_ERROR_FIELD_COLOR;
                }

                // 判断坐标X是否为数字
                if (!Validator.IsNumeric(cell.Value.ToString()))
                {
                    cell.Style.BackColor = Const.ERROR_FIELD_COLOR;
                    Alert.alert(Const.rowMustBeNumber(i, Const_GM.X));
                    return false;
                }
                else
                {
                    cell.Style.BackColor = Const.NO_ERROR_FIELD_COLOR;
                }
                //判断坐标Y是否入力
                cell = dgrdvWire.Rows[i].Cells[2] as DataGridViewTextBoxCell;
                if (cell.Value == null)
                {
                    cell.Style.BackColor = Const.ERROR_FIELD_COLOR;
                    Alert.alert(Const.rowNotNull(i, Const_GM.Y));
                    return false;
                }
                else
                {
                    cell.Style.BackColor = Const.NO_ERROR_FIELD_COLOR;
                }

                // 判断坐标Y是否为数字
                if (!Validator.IsNumeric(cell.Value.ToString()))
                {
                    cell.Style.BackColor = Const.ERROR_FIELD_COLOR;
                    Alert.alert(Const.rowMustBeNumber(i, Const_GM.Y));
                    return false;
                }
                else
                {
                    cell.Style.BackColor = Const.NO_ERROR_FIELD_COLOR;
                }
                //判断坐标Z是否入力
                cell = dgrdvWire.Rows[i].Cells[3] as DataGridViewTextBoxCell;
                if (cell.Value == null)
                {
                    cell.Style.BackColor = Const.ERROR_FIELD_COLOR;
                    Alert.alert(Const.rowNotNull(i, Const_GM.Z));
                    return false;
                }
                else
                {
                    cell.Style.BackColor = Const.NO_ERROR_FIELD_COLOR;
                }

                // 判断坐标Z是否为数字
                if (!Validator.IsNumeric(cell.Value.ToString()))
                {
                    cell.Style.BackColor = Const.ERROR_FIELD_COLOR;
                    Alert.alert(Const.rowMustBeNumber(i, Const_GM.Z));
                    return false;
                }
                else
                {
                    cell.Style.BackColor = Const.NO_ERROR_FIELD_COLOR;
                }
                //判断距左帮距离是否入力
                cell = dgrdvWire.Rows[i].Cells[4] as DataGridViewTextBoxCell;
                if (cell.Value == null)
                {
                    cell.Style.BackColor = Const.ERROR_FIELD_COLOR;
                    Alert.alert(Const.rowNotNull(i, Const_GM.DISTANCE_TO_LEFT));
                    return false;
                }
                else
                {
                    cell.Style.BackColor = Const.NO_ERROR_FIELD_COLOR;
                }

                // 判断距左帮距离是否为数字
                if (!Validator.IsNumeric(cell.Value.ToString()))
                {
                    cell.Style.BackColor = Const.ERROR_FIELD_COLOR;
                    Alert.alert(Const.rowMustBeNumber(i, Const_GM.DISTANCE_TO_LEFT));
                    return false;
                }
                else
                {
                    cell.Style.BackColor = Const.NO_ERROR_FIELD_COLOR;
                }
                //判断距右帮距离是否入力
                cell = dgrdvWire.Rows[i].Cells[5] as DataGridViewTextBoxCell;
                if (cell.Value == null)
                {
                    cell.Style.BackColor = Const.ERROR_FIELD_COLOR;
                    Alert.alert(Const.rowNotNull(i, Const_GM.DISTANCE_TO_RIGHT));
                    return false;
                }
                else
                {
                    cell.Style.BackColor = Const.NO_ERROR_FIELD_COLOR;
                }

                // 判断距右帮距离是否为数字
                if (!Validator.IsNumeric(cell.Value.ToString()))
                {
                    cell.Style.BackColor = Const.ERROR_FIELD_COLOR;
                    Alert.alert(Const.rowMustBeNumber(i, Const_GM.DISTANCE_TO_RIGHT));
                    return false;
                }
                else
                {
                    cell.Style.BackColor = Const.NO_ERROR_FIELD_COLOR;
                }
                cell = dgrdvWire.Rows[i].Cells[6] as DataGridViewTextBoxCell;
                // 判断距顶板距离是否为数字
                if (cell.Value != null && !Validator.IsNumeric(cell.Value.ToString()))
                {
                    cell.Style.BackColor = Const.ERROR_FIELD_COLOR;
                    Alert.alert(Const.rowMustBeNumber(i, Const_GM.DISTANCE_TO_TOP));
                    return false;
                }
                else
                {
                    cell.Style.BackColor = Const.NO_ERROR_FIELD_COLOR;
                }
                cell = dgrdvWire.Rows[i].Cells[7] as DataGridViewTextBoxCell;
                // 判断距底板距离是否为数字
                if (cell.Value != null && !Validator.IsNumeric(cell.Value.ToString()))
                {
                    cell.Style.BackColor = Const.ERROR_FIELD_COLOR;
                    Alert.alert(Const.rowMustBeNumber(i, Const_GM.DISTANCE_TO_BOTTOM));
                    return false;
                }
                else
                {
                    cell.Style.BackColor = Const.NO_ERROR_FIELD_COLOR;
                }
            }
            //验证成功
            return true;
        }

        #region ******datagridview鼠标拖动排序******
        int selectionIdx = -1;
        DataGridViewRow nr = new DataGridViewRow();
        DataGridViewRow ddr = new DataGridViewRow();
        private void dgrdvWire_SelectionChanged(object sender, EventArgs e)
        {
            if (dgrdvWire.Rows.Count <= selectionIdx)
            {
                selectionIdx = dgrdvWire.Rows.Count - 1;
                dgrdvWire.Rows[selectionIdx].Selected = true;
            }
        }

        private void dgrdvWire_CellMouseDown(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                selectionIdx = e.RowIndex;
                nr = dgrdvWire.Rows[selectionIdx];
            }
            //上移按钮可用性
            if (e.RowIndex <= 0)
            {
                上移ToolStripMenuItem.Enabled = false;
            }
            else
            {
                上移ToolStripMenuItem.Enabled = true;
            }
            //下移按钮可用性
            if (e.RowIndex >= dgrdvWire.Rows.Count - 1)
            {
                下移ToolStripMenuItem.Enabled = false;
            }
            else
            {
                下移ToolStripMenuItem.Enabled = true;
            }
            //剪切时粘贴后粘贴按钮消失
            if (doing == 0)
            {
                粘贴ToolStripMenuItem.Visible = false;
            }
            //复制时粘贴按钮不消失
            else
            {
                粘贴ToolStripMenuItem.Visible = true;
            }
            if (e.RowIndex > -1)
            {
                if (e.Button == MouseButtons.Right)
                {
                    dgrdvWire.Rows[e.RowIndex].Selected = true;
                }
                if (dgrdvWire[0, e.RowIndex].Value == null &&
                    dgrdvWire[1, e.RowIndex].Value == null &&
                    dgrdvWire[2, e.RowIndex].Value == null &&
                    dgrdvWire[3, e.RowIndex].Value == null &&
                    dgrdvWire[4, e.RowIndex].Value == null &&
                    dgrdvWire[5, e.RowIndex].Value == null &&
                    dgrdvWire[6, e.RowIndex].Value == null &&
                    dgrdvWire[7, e.RowIndex].Value == null)
                {
                    this.剪切ToolStripMenuItem.Enabled = false;
                    this.复制ToolStripMenuItem.Enabled = false;
                    this.上移ToolStripMenuItem.Enabled = false;
                    this.下移ToolStripMenuItem.Enabled = false;
                }
                else
                {
                    this.剪切ToolStripMenuItem.Enabled = true;
                    this.复制ToolStripMenuItem.Enabled = true;
                    this.上移ToolStripMenuItem.Enabled = true;
                    this.下移ToolStripMenuItem.Enabled = true;
                }
            }
            else
            {
                this.插入ToolStripMenuItem.Enabled = false;
                this.剪切ToolStripMenuItem.Enabled = false;
                this.复制ToolStripMenuItem.Enabled = false;
                this.上移ToolStripMenuItem.Enabled = false;
                this.下移ToolStripMenuItem.Enabled = false;
                this.粘贴ToolStripMenuItem.Enabled = false;
            }
        }

        private void dgrdvWire_CellMouseMove(object sender, DataGridViewCellMouseEventArgs e)
        {
            if ((e.Clicks < 2) && (e.Button == MouseButtons.Left))
            {
                if ((e.ColumnIndex == -1) && (e.RowIndex > -1))
                    dgrdvWire.DoDragDrop(dgrdvWire.Rows[e.RowIndex], DragDropEffects.Move);
            }
        }

        private void dgrdvWire_DragDrop(object sender, DragEventArgs e)
        {
            int idx = GetRowFromPoint(e.X, e.Y);
            if (idx < 0) return;

            if (e.Data.GetDataPresent(typeof(DataGridViewRow)))
            {
                DataGridViewRow row = (DataGridViewRow)e.Data.GetData(typeof(DataGridViewRow));
                ddr = dgrdvWire.Rows[idx];
                dgrdvWire.Rows.RemoveAt(selectionIdx);
                dgrdvWire.Rows.Insert(idx, nr);
                dgrdvWire.ClearSelection();
            }

        }

        private void dgrdvWire_DragEnter(object sender, DragEventArgs e)
        {
            e.Effect = DragDropEffects.Move;
        }
        /// <summary>
        /// 鼠标落点获取行号
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <returns></returns>
        private int GetRowFromPoint(int x, int y)
        {
            for (int i = 0; i < dgrdvWire.RowCount; i++)
            {
                Rectangle rec = dgrdvWire.GetRowDisplayRectangle(i, false);

                if (dgrdvWire.RectangleToScreen(rec).Contains(x, y))
                    return i;
            }

            return -1;
        }
        #endregion
        /// <summary>
        /// 添加自动转变修改
        /// </summary>
        /// <returns>是否转变</returns>
        private bool AutoChangeWireName()
        {
            DataSet ds = new DataSet();
            //获取巷道ID
            tunnelEntity.TunnelId = selectTunnelUserControl1.ITunnelId;
            //获取巷道对应导线信息
            ds = WireInfoBLL.selectAllWireInfo(tunnelEntity);
            if (ds.Tables[0].Rows.Count > 0)
            {
                if (Convert.ToString(ds.Tables[0].Rows[0][WireInfoDbConstNames.WIRE_NAME]) != txtWireName.Text && this.Text == Const_GM.WIRE_INFO_ADD)
                {
                    //所选巷道已绑定导线，是否跳转到修改导线
                    if (Alert.confirm(Const_GM.TUNNEL_CHOOSE_FIRST + Convert.ToString(ds.Tables[0].Rows[0][WireInfoDbConstNames.WIRE_NAME]) +
                        Const_GM.TUNNEL_CHOOSE_MIDDLE + Convert.ToString(ds.Tables[0].Rows[0][WireInfoDbConstNames.WIRE_NAME]) + Const_GM.TUNNEL_CHOOSE_LAST))
                    {
                        //窗体名称改为修改巷道
                        this.Text = Const_GM.WIRE_INFO_CHANGE;
                        _tunnelID = this.selectTunnelUserControl1.ITunnelId;
                        //绑定信息
                        txtWireName.Text = Convert.ToString(ds.Tables[0].Rows[0][WireInfoDbConstNames.WIRE_NAME]);
                        this.txtWireLevel.Text = Convert.ToString(ds.Tables[0].Rows[0][WireInfoDbConstNames.WIRE_LEVEL]);
                        this.cboVobserver.Text = Convert.ToString(ds.Tables[0].Rows[0][WireInfoDbConstNames.VOBSERVER]);
                        this.cboCounter.Text = Convert.ToString(ds.Tables[0].Rows[0][WireInfoDbConstNames.COUNTER]);
                        this.cboChecker.Text = Convert.ToString(ds.Tables[0].Rows[0][WireInfoDbConstNames.CHECKER]);

                        DataGridViewRow[] dgvr = new DataGridViewRow[dgrdvWire.Rows.Count - 1];
                        for (int i = 0; i < dgrdvWire.Rows.Count - 1; i++)
                        {
                            dgvr[i] = dgrdvWire.Rows[i];
                        }
                        dgrdvWire.Rows.Clear();
                        //巷道信息
                        tunnelEntity = BasicInfoManager.getInstance().getTunnelByID(tunnelEntity.TunnelId);
                        //导线ID
                        wireInfoEntity.WireInfoID = Convert.ToInt32(WireInfoBLL.selectAllWireInfo(tunnelEntity).Tables[0].Rows[0][WireInfoDbConstNames.ID]);
                        //导线信息
                        wireInfoEntity = WireInfoBLL.selectAllWireInfo(wireInfoEntity.WireInfoID);
                        _arr[0] = tunnelEntity.WorkingFace.MiningArea.Horizontal.Mine.MineId;
                        _arr[1] = tunnelEntity.WorkingFace.MiningArea.Horizontal.HorizontalId;
                        _arr[2] = tunnelEntity.WorkingFace.MiningArea.MiningAreaId;
                        _arr[3] = tunnelEntity.WorkingFace.WorkingFaceID;
                        _arr[4] = tunnelEntity.TunnelId;
                        //绑定修改信息
                        loadWireInfoData();

                        for (int i = 0; i < dgvr.Length; i++)
                        {
                            dgrdvWire.Rows.Add(dgvr[i]);
                            selectionIdx = dgrdvWire.CurrentRow.Index;
                        }
                    }
                    //巷道已绑定导线，请重新选择巷道
                    else
                    {
                        Alert.alert(Const_GM.WIRE_INFO_MSG_TUNNEL_ALREADY_BIND_WIRE);
                        return false;
                    }
                }
                return true;
            }
            return true;
        }

        /// <summary>
        /// 右键插入
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void 插入ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            dgrdvWire.Rows.Insert(selectionIdx, 1);
        }

        /// <summary>
        /// 右键复制
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void 复制ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            doing = 2;
            dgrdvWire.Rows[selectionIdx].Cells.CopyTo(dgvc, 0);
            for (int i = 0; i < dgvc.Length; i++)
            {
                dr[i] = dgvc[i].Value.ToString();
            }
            _tmpRowIndex = selectionIdx;
        }

        /// <summary>
        /// 右键剪切
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void 剪切ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            doing = 1;
            dgrdvWire.Rows[selectionIdx].Cells.CopyTo(dgvc, 0);
            for (int i = 0; i < dgvc.Length; i++)
            {
                dr[i] = dgvc[i].Value.ToString();
            }
            _tmpRowIndex = selectionIdx;
        }

        /// <summary>
        /// 右键粘贴
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void 粘贴ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (doing == 1)
            {
                dgrdvWire.Rows.RemoveAt(_tmpRowIndex);
            }
            dgrdvWire.Rows.Insert(selectionIdx, dr);
            if (doing == 1)
            {
                doing = 0;
            }
            else
            {
                doing = 2;
            }
        }

        /// <summary>
        /// 右键上移
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void 上移ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DataGridViewRow dgvr = new DataGridViewRow();
            dgvr = dgrdvWire.Rows[selectionIdx];
            dgrdvWire.Rows.RemoveAt(selectionIdx);
            dgrdvWire.Rows.Insert(selectionIdx - 1, dgvr);
            dgrdvWire.CurrentCell = dgrdvWire.Rows[selectionIdx - 1].Cells[0];
        }

        /// <summary>
        /// 右键下移
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void 下移ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DataGridViewRow dgvr = new DataGridViewRow();
            dgvr = dgrdvWire.Rows[selectionIdx];
            dgrdvWire.Rows.RemoveAt(selectionIdx);
            dgrdvWire.Rows.Insert(selectionIdx + 1, dgvr);
            dgrdvWire.CurrentCell = dgrdvWire.Rows[selectionIdx + 1].Cells[0];
        }

        /// <summary>
        /// 显示行号
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void dgrdvWire_RowPostPaint(object sender, DataGridViewRowPostPaintEventArgs e)
        {
            System.Drawing.Rectangle rectangle = new System.Drawing.Rectangle(e.RowBounds.Location.X,
                e.RowBounds.Location.Y, dgrdvWire.RowHeadersWidth - 4, e.RowBounds.Height);

            TextRenderer.DrawText(e.Graphics, (e.RowIndex + 1).ToString(),
                dgrdvWire.RowHeadersDefaultCellStyle.Font, rectangle,
                dgrdvWire.RowHeadersDefaultCellStyle.ForeColor,
                TextFormatFlags.VerticalCenter | TextFormatFlags.Right);
        }

        /// <summary>
        /// 初始化
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void WireInfoEntering_Load(object sender, EventArgs e)
        {
            if (this.Text == Const_GM.WIRE_INFO_ADD)
            {
                addInfo();
            }
        }

        /// <summary>
        /// 添加
        /// </summary>
        private void addInfo()
        {
            bindNames();
            dtpMeasureDate.Value = DateTime.Now;
            dtpCountDate.Value = DateTime.Now;
            dtpCheckDate.Value = DateTime.Now;
        }

        /// <summary>
        /// combobox绑定
        /// </summary>
        private void bindNames()
        {
            DataSet ds = new DataSet();
            DataSet ds2 = new DataSet();
            DataSet ds3 = new DataSet();
            ds = UserInformationDetailsManagementBLL.GetUserInformationDetailsDS();
            ds2 = UserInformationDetailsManagementBLL.GetUserInformationDetailsDS();
            ds3 = UserInformationDetailsManagementBLL.GetUserInformationDetailsDS();
            cboVobserver.DataSource = ds.Tables[0];
            cboVobserver.DisplayMember = UserInformationDetailsManagementDbConstNames.USER_NAME;
            cboVobserver.ValueMember = UserInformationDetailsManagementDbConstNames.ID;
            cboVobserver.SelectedIndex = -1;
            cboCounter.DataSource = ds2.Tables[0];
            cboCounter.DisplayMember = UserInformationDetailsManagementDbConstNames.USER_NAME;
            cboCounter.ValueMember = UserInformationDetailsManagementDbConstNames.ID;
            cboCounter.SelectedIndex = -1;
            cboChecker.DataSource = ds3.Tables[0];
            cboChecker.DisplayMember = UserInformationDetailsManagementDbConstNames.USER_NAME;
            cboChecker.ValueMember = UserInformationDetailsManagementDbConstNames.ID;
            cboChecker.SelectedIndex = -1;
        }

        /// <summary>
        /// datagridview进入行时，按钮可操作性
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void dgrdvWire_RowEnter(object sender, DataGridViewCellEventArgs e)
        {
            _tmpRowIndex = e.RowIndex;
            selectionIdx = e.RowIndex;
            btnAdd.Enabled = true;
            btnCopy.Enabled = true;
            btnDel.Enabled = true;
            for (int i = 0; i < dgvc.Length; i++)
            {
                if (dgvc[i] != null)
                {
                    btnPaste.Enabled = true;
                    break;
                }
                else
                {
                    btnPaste.Enabled = false;
                }
            }
            if (e.RowIndex == 0)
            {
                btnMoveUp.Enabled = false;
            }
            else
            {
                btnMoveUp.Enabled = true;
            }
            if (e.RowIndex > dgrdvWire.Rows.Count - 3)
            {
                btnMoveDown.Enabled = false;
            }
            else
            {
                btnMoveDown.Enabled = true;
            }
            if (e.RowIndex == dgrdvWire.NewRowIndex)
            {
                btnCopy.Enabled = false;
                btnMoveUp.Enabled = false;
                btnDel.Enabled = false;
            }
            else
            {
                btnCopy.Enabled = true;
                btnDel.Enabled = true;
            }
            if (dgrdvWire[0, e.RowIndex].Value == null &&
                dgrdvWire[1, e.RowIndex].Value == null &&
                dgrdvWire[2, e.RowIndex].Value == null &&
                dgrdvWire[3, e.RowIndex].Value == null &&
                dgrdvWire[4, e.RowIndex].Value == null &&
                dgrdvWire[5, e.RowIndex].Value == null &&
                dgrdvWire[6, e.RowIndex].Value == null &&
                dgrdvWire[7, e.RowIndex].Value == null)
            {
                btnCopy.Enabled = false;
            }
        }

        /// <summary>
        /// 复制按钮
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnCopy_Click(object sender, EventArgs e)
        {
            dgrdvWire.Rows[_tmpRowIndex].Cells.CopyTo(dgvc, 0);
            for (int i = 0; i < dgvc.Length; i++)
            {
                dr[i] = dgvc[i].Value.ToString();
            }
            btnPaste.Enabled = true;
        }

        /// <summary>
        /// 粘贴按钮
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnPaste_Click(object sender, EventArgs e)
        {
            dgrdvWire.Rows.Insert(dgrdvWire.Rows[_tmpRowIndex].Index, dr);
        }

        /// <summary>
        /// 添加按钮
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnAdd_Click(object sender, EventArgs e)
        {
            dgrdvWire.Rows.Insert(dgrdvWire.CurrentRow.Index, 1);
            dgrdvWire.Focus();
            dgrdvWire.Rows[dgrdvWire.CurrentRow.Index - 1].Cells[0].Selected = true;
        }

        /// <summary>
        /// 删除按钮
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnDel_Click(object sender, EventArgs e)
        {
            if (dgrdvWire.Rows.Count > 1 && dgrdvWire.CurrentRow.Index < dgrdvWire.Rows.Count - 1)
                dgrdvWire.Rows.RemoveAt(_tmpRowIndex);
        }

        /// <summary>
        /// 上移按钮
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnMoveUp_Click(object sender, EventArgs e)
        {
            bool isLast = false;
            if (_tmpRowIndex == dgrdvWire.Rows.Count - 2)
            {
                isLast = true;
            }
            else
            {
                isLast = false;
            }
            if (_tmpRowIndex == dgrdvWire.Rows.Count - 1)
            {
                _tmpRowIndex = dgrdvWire.SelectedRows[0].Index;
            }

            DataGridViewRow dgvr = new DataGridViewRow();
            dgvr = dgrdvWire.Rows[_tmpRowIndex];
            dgrdvWire.Rows.RemoveAt(_tmpRowIndex);

            if (_tmpRowIndex == 0)
                dgrdvWire.Rows.Insert(0, dgvr);
            else
                dgrdvWire.Rows.Insert(_tmpRowIndex - 1, dgvr);

            dgrdvWire.Rows[_tmpRowIndex].Selected = false;

            if (isLast && dgrdvWire.Rows.Count > 3)
            {
                dgrdvWire.Rows[_tmpRowIndex - 2].Selected = true;
                dgrdvWire.CurrentCell = dgrdvWire.Rows[_tmpRowIndex - 2].Cells[0];
                btnMoveDown_Click(sender, e);
            }
        }

        /// <summary>
        /// 下移按钮
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnMoveDown_Click(object sender, EventArgs e)
        {
            _tmpRowIndex = dgrdvWire.SelectedRows[0].Index;
            DataGridViewRow dgvr = new DataGridViewRow();
            dgvr = dgrdvWire.Rows[_tmpRowIndex];
            dgrdvWire.Rows.RemoveAt(_tmpRowIndex);
            dgrdvWire.Rows[_tmpRowIndex].Selected = true;
            dgrdvWire.Rows.Insert(_tmpRowIndex + 1, dgvr);
            dgrdvWire.Rows[_tmpRowIndex].Selected = false;
            dgrdvWire.Rows[_tmpRowIndex + 1].Selected = true;
            dgrdvWire.CurrentCell = dgrdvWire.Rows[_tmpRowIndex + 1].Cells[0];
        }

        private void btnTXT_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.InitialDirectory = @"C:\Users\happybai\Desktop\巷道录入（11.06）";
            ofd.RestoreDirectory = true;
            ofd.Filter = "文本文件(*.txt)|*.txt|所有文件(*.*)|*.*";
            //ofd.ShowDialog();
            if (ofd.ShowDialog() == DialogResult.OK)
            {
                string aa = ofd.FileName;
                string[] temp = aa.Split('-');
                string caiqu = temp[0];
                string hangname = temp[1];
                txtWireName.Text = hangname.Split('.').Length > 0 ? hangname.Split('.')[0] + "导线点" : hangname + "导线点";
                StreamReader sr = new StreamReader(@aa, Encoding.GetEncoding("GB2312"));
                string duqu;
                while ((duqu = sr.ReadLine()) != null)
                {
                    string[] temp1 = duqu.Split('|');
                    string daoxianname = temp1[0];
                    string daoxianx = temp1[1];
                    string daoxiany = temp1[2];
                    dgrdvWire.Rows.Add(1);
                    dgrdvWire[0, dgrdvWire.Rows.Count - 2].Value = daoxianname;
                    dgrdvWire[1, dgrdvWire.Rows.Count - 2].Value = daoxianx;
                    dgrdvWire[2, dgrdvWire.Rows.Count - 2].Value = daoxiany;
                    dgrdvWire[3, dgrdvWire.Rows.Count - 2].Value = "0";
                    dgrdvWire[4, dgrdvWire.Rows.Count - 2].Value = "2.5";
                    dgrdvWire[5, dgrdvWire.Rows.Count - 2].Value = "2.5";
                }

            }

        }
    }
}
