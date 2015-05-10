using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Windows.Forms;
using ESRI.ArcGIS.Geometry;
using GIS;
using GIS.HdProc;
using LibBusiness;
using LibCommon;
using LibEntity;

namespace sys3
{
    /// <summary>
    ///     回采进尺矫正
    /// </summary>
    public partial class TunnelHCJZEntering : Form
    {
        private int[] _arr = new int[5];
        private DataSet _dsWirePoint = new DataSet();
        private int _itemCount = 0;
        private double _tmpDouble = 0;
        private int _tmpRowIndex = -1;
        private Tunnel _tunnelEntity = new Tunnel();
        private int _tunnelID;
        private DataGridViewCell[] dgvc = new DataGridViewCell[8];
        private int doing = 0;
        private string[] dr = new string[8];
        private WirePoint[] wpiEntity;
        /**********变量声明***********/
        private readonly Wire wireEntity = new Wire();
        /*****************************/

        public TunnelHCJZEntering()
        {
            InitializeComponent();
            //日期
            dtpDate.Value = DateTime.Now.Date;
            //
            //设置窗体格式
            FormDefaultPropertiesSetter.SetEnteringFormDefaultProperties(this, "回采工作面校正");

            //this.selectTunnelUserControl1.init(this.MainForm);
            //自定义控件巷道过滤
            //this.selectTunnelUserControl1.SetFilterOn(TunnelTypeEnum.TUNNELLING);
            //自定义控件初始化
            //this.selectTunnelUserControl1.setCurSelectedID(_arr);
            //this.selectTunnelUserControl1.setCurSelectedID(_arr);
            //自定义控件初始化
            //LibEntity.WorkingFaceSelect workingFaceSelectEntity = LibBusiness.WorkingFaceSelect.SelectWorkingFace(DayReportHCDbConstNames.TABLE_NAME);
            //if (workingFaceSelectEntity != null)
            //{
            //    _arr = new int[5];
            //    _arr[0] = workingFaceSelectEntity.MineID;
            //    _arr[1] = workingFaceSelectEntity.HorizontalID;
            //    _arr[2] = workingFaceSelectEntity.MiningAreaID;

            //    this.selectWorkingFaceControl1.setCurSelectedID(_arr);
            //}
            //else
            //{
            selectWorkingFaceControl1.LoadData();
            //}
            // 注册委托事件
            //selectWorkingFaceControl1.WorkingFaceNameChanged += NameChangeEvent;
        }

        //private void NameChangeEvent(object sender, WorkingFaceEventArgs e)
        //{
        //    updateWorkingFaceInfo(selectWorkingFaceControl1.IWorkingFaceId);
        //}

        //private void updateWorkingFaceInfo(int workingFaceId)
        //{
        //    workingFace = WorkingFace.Find(workingFaceId);
        //    tunnelZY = workingFace.Tunnels.First(u => u.TunnelType == TunnelTypeEnum.STOPING_ZY);
        //    tunnelFY = workingFace.Tunnels.First(u => u.TunnelType == TunnelTypeEnum.STOPING_FY);
        //    tunnelQY = workingFace.Tunnels.First(u => u.TunnelType == TunnelTypeEnum.STOPING_QY);
        //}

        /// <summary>
        ///     验证
        /// </summary>
        /// <returns></returns>
        private bool check()
        {
            // 判断导线点编号是否入力
            if (dgrdvWire.Rows.Count - 1 == 0)
            {
                Alert.alert(Const.MSG_PLEASE_TYPE_IN + Const_GM.WIRE_POINT_ID + Const.SIGN_EXCLAMATION_MARK);
                return false;
            }
            //dgrdvWire内部判断
            for (var i = 0; i < dgrdvWire.RowCount; i++)
            {
                // 最后一行为空行时，跳出循环
                if (i == dgrdvWire.RowCount - 1)
                {
                    break;
                }
                var cell = dgrdvWire.Rows[i].Cells[0] as DataGridViewTextBoxCell;
                // 判断导线点编号是否入力
                if (cell.Value == null)
                {
                    cell.Style.BackColor = Const.ERROR_FIELD_COLOR;
                    Alert.alert(Const_GM.WIRE_POINT_ID + Const.MSG_NOT_NULL + Const.SIGN_EXCLAMATION_MARK);
                    return false;
                }
                cell.Style.BackColor = Const.NO_ERROR_FIELD_COLOR;
                //判断导线点编号是否存在
                if (Text == Const_GM.WIRE_INFO_ADD)
                {
                    //导线点是否存在
                    if (WirePoint.ExistsByWirePointIdInWireInfo(wireEntity.WireId,
                        dgrdvWire.Rows[i].Cells[0].Value.ToString()))
                    {
                        cell.Style.BackColor = Const.ERROR_FIELD_COLOR;
                        Alert.alert(Const_GM.WIRE_POINT_ID + Const.MSG_ALREADY_HAVE + Const.SIGN_EXCLAMATION_MARK);
                        return false;
                    }
                    cell.Style.BackColor = Const.NO_ERROR_FIELD_COLOR;
                }
                //判断导线点编号是否有输入重复
                for (var j = 0; j < i; j++)
                {
                    if (dgrdvWire[0, j].Value.ToString() == dgrdvWire[0, i].Value.ToString())
                    {
                        cell.Style.BackColor = Const.ERROR_FIELD_COLOR;
                        dgrdvWire[0, j].Style.BackColor = Const.ERROR_FIELD_COLOR;
                        Alert.alert(Const_GM.WIRE_POINT_ID + Const.MSG_DOUBLE_EXISTS + Const.SIGN_EXCLAMATION_MARK);
                        return false;
                    }
                    cell.Style.BackColor = Const.NO_ERROR_FIELD_COLOR;
                    dgrdvWire[0, j].Style.BackColor = Const.NO_ERROR_FIELD_COLOR;
                }

                //判断坐标X是否入力
                cell = dgrdvWire.Rows[i].Cells[1] as DataGridViewTextBoxCell;
                if (cell.Value == null)
                {
                    cell.Style.BackColor = Const.ERROR_FIELD_COLOR;
                    Alert.alert(Const.rowNotNull(i, Const_GM.X));
                    return false;
                }
                cell.Style.BackColor = Const.NO_ERROR_FIELD_COLOR;

                // 判断坐标X是否为数字
                if (!Validator.IsNumeric(cell.Value.ToString()))
                {
                    cell.Style.BackColor = Const.ERROR_FIELD_COLOR;
                    Alert.alert(Const.rowMustBeNumber(i, Const_GM.X));
                    return false;
                }
                cell.Style.BackColor = Const.NO_ERROR_FIELD_COLOR;
                //判断坐标Y是否入力
                cell = dgrdvWire.Rows[i].Cells[2] as DataGridViewTextBoxCell;
                if (cell.Value == null)
                {
                    cell.Style.BackColor = Const.ERROR_FIELD_COLOR;
                    Alert.alert(Const.rowNotNull(i, Const_GM.Y));
                    return false;
                }
                cell.Style.BackColor = Const.NO_ERROR_FIELD_COLOR;

                // 判断坐标Y是否为数字
                if (!Validator.IsNumeric(cell.Value.ToString()))
                {
                    cell.Style.BackColor = Const.ERROR_FIELD_COLOR;
                    Alert.alert(Const.rowMustBeNumber(i, Const_GM.Y));
                    return false;
                }
                cell.Style.BackColor = Const.NO_ERROR_FIELD_COLOR;
                //判断坐标Z是否入力
                cell = dgrdvWire.Rows[i].Cells[3] as DataGridViewTextBoxCell;
                if (cell.Value == null)
                {
                    cell.Style.BackColor = Const.ERROR_FIELD_COLOR;
                    Alert.alert(Const.rowNotNull(i, Const_GM.Z));
                    return false;
                }
                cell.Style.BackColor = Const.NO_ERROR_FIELD_COLOR;

                // 判断坐标Z是否为数字
                if (!Validator.IsNumeric(cell.Value.ToString()))
                {
                    cell.Style.BackColor = Const.ERROR_FIELD_COLOR;
                    Alert.alert(Const.rowMustBeNumber(i, Const_GM.Z));
                    return false;
                }
                cell.Style.BackColor = Const.NO_ERROR_FIELD_COLOR;
                //判断距左帮距离是否入力
                cell = dgrdvWire.Rows[i].Cells[4] as DataGridViewTextBoxCell;
                if (cell.Value == null)
                {
                    cell.Style.BackColor = Const.ERROR_FIELD_COLOR;
                    Alert.alert(Const.rowNotNull(i, Const_GM.DISTANCE_TO_LEFT));
                    return false;
                }
                cell.Style.BackColor = Const.NO_ERROR_FIELD_COLOR;

                // 判断距左帮距离是否为数字
                if (!Validator.IsNumeric(cell.Value.ToString()))
                {
                    cell.Style.BackColor = Const.ERROR_FIELD_COLOR;
                    Alert.alert(Const.rowMustBeNumber(i, Const_GM.DISTANCE_TO_LEFT));
                    return false;
                }
                cell.Style.BackColor = Const.NO_ERROR_FIELD_COLOR;
                //判断距右帮距离是否入力
                cell = dgrdvWire.Rows[i].Cells[5] as DataGridViewTextBoxCell;
                if (cell.Value == null)
                {
                    cell.Style.BackColor = Const.ERROR_FIELD_COLOR;
                    Alert.alert(Const.rowNotNull(i, Const_GM.DISTANCE_TO_RIGHT));
                    return false;
                }
                cell.Style.BackColor = Const.NO_ERROR_FIELD_COLOR;

                // 判断距右帮距离是否为数字
                if (!Validator.IsNumeric(cell.Value.ToString()))
                {
                    cell.Style.BackColor = Const.ERROR_FIELD_COLOR;
                    Alert.alert(Const.rowMustBeNumber(i, Const_GM.DISTANCE_TO_RIGHT));
                    return false;
                }
                cell.Style.BackColor = Const.NO_ERROR_FIELD_COLOR;
                cell = dgrdvWire.Rows[i].Cells[6] as DataGridViewTextBoxCell;
                // 判断距顶板距离是否为数字
                if (cell.Value != null && !Validator.IsNumeric(cell.Value.ToString()))
                {
                    cell.Style.BackColor = Const.ERROR_FIELD_COLOR;
                    Alert.alert(Const.rowMustBeNumber(i, Const_GM.DISTANCE_TO_TOP));
                    return false;
                }
                cell.Style.BackColor = Const.NO_ERROR_FIELD_COLOR;
                cell = dgrdvWire.Rows[i].Cells[7] as DataGridViewTextBoxCell;
                // 判断距底板距离是否为数字
                if (cell.Value != null && !Validator.IsNumeric(cell.Value.ToString()))
                {
                    cell.Style.BackColor = Const.ERROR_FIELD_COLOR;
                    Alert.alert(Const.rowMustBeNumber(i, Const_GM.DISTANCE_TO_BOTTOM));
                    return false;
                }
                cell.Style.BackColor = Const.NO_ERROR_FIELD_COLOR;
            }
            //验证成功
            return true;
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            // 验证
            if (!check())
            {
                DialogResult = DialogResult.None;
                return;
            }
            DialogResult = DialogResult.OK;
            //_tunnelEntity.Tunnel = selectTunnelUserControl1.ITunnelId;
            //_tunnelEntity = TunnelInfoBLL.selectTunnelInfoByTunnelID(_tunnelEntity.Tunnel);
            //验证
            if (!check())
            {
                DialogResult = DialogResult.None;
                return;
            }
            var coordinates = new List<IPoint>();
            for (var i = 0; i < dgrdvWire.Rows.Count - 1; i++)
            {
                var x = Convert.ToDouble(dgrdvWire.Rows[i].Cells[1].Value);
                var y = Convert.ToDouble(dgrdvWire.Rows[i].Cells[2].Value);
                var z = Convert.ToDouble(dgrdvWire.Rows[i].Cells[3].Value);
                IPoint pnt = new PointClass();
                pnt.X = x;
                pnt.Y = y;
                pnt.Z = z;
                coordinates.Add(pnt);
            }
            var workingFace = selectWorkingFaceControl1.SelectedWorkingFace;
            if (workingFace != null)
            {
                var tunnelZY = workingFace.Tunnels.First(u => u.TunnelType == TunnelTypeEnum.STOPING_ZY);
                var tunnelFY = workingFace.Tunnels.First(u => u.TunnelType == TunnelTypeEnum.STOPING_FY);
                var tunnelQY = workingFace.Tunnels.First(u => u.TunnelType == TunnelTypeEnum.STOPING_QY);
                TunnelHcJz(coordinates, tunnelZY.TunnelId.ToString(), tunnelFY.TunnelId.ToString(),
                    tunnelQY.TunnelId.ToString(), workingFace.WorkingFaceId, tunnelZY.TunnelWid, tunnelFY.TunnelWid,
                    tunnelQY.TunnelWid);
            }
        }

        /// <summary>
        ///     巷道回采校正
        /// </summary>
        private void TunnelHcJz(List<IPoint> pnts, string hd1, string hd2, string hd3, int workingfaceid, double zywid,
            double fywid, double qywid)
        {
            //查询对应的巷道信息
            var hdids = new Dictionary<string, string>();
            hdids.Add(GIS_Const.FIELD_HDID, hd1);
            var selobjs1 =
                Global.commonclss.SearchFeaturesByGeoAndText(Global.centerlyr, hdids);
            var pline1 = selobjs1[0].Item2 as IPolyline;

            hdids[GIS_Const.FIELD_HDID] = hd2;
            var selobjs2 =
                Global.commonclss.SearchFeaturesByGeoAndText(Global.centerlyr, hdids);
            var pline2 = selobjs2[0].Item2 as IPolyline;

            hdids[GIS_Const.FIELD_HDID] = hd3;
            var selobjs3 =
                Global.commonclss.SearchFeaturesByGeoAndText(Global.centerlyr, hdids);
            var pline3 = selobjs3[0].Item2 as IPolyline;

            //删除原来的采掘区
            hdids[GIS_Const.FIELD_HDID] = hd1 + "_" + hd2;
            var sql = "\"" + GIS_Const.FIELD_HDID + "\"='" + hd1 + "_" + hd2 + "' AND \"" + GIS_Const.FIELD_BS +
                      "\"=0";
            Global.commonclss.DelFeatures(Global.hcqlyr, sql);
            //查询对应的回采区
            for (var k = 0; k < pnts.Count; k++)
            {
                //导线点
                var pnt = pnts[k];
                var jzx = pnt.X;
                var jzy = pnt.Y;
                //构造采掘区对象
                var xh = 0;
                var selobjshcqs =
                    Global.commonclss.SearchFeaturesByGeoAndText(Global.hcqlyr, hdids);
                Dictionary<string, List<IPoint>> oldpnts = null;
                if (selobjshcqs.Count != 0)
                {
                    oldpnts = Global.commonclss.getCoordinates(selobjshcqs[0].Item2 as IPolygon, pline1, pline2, pline3,
                        zywid, fywid);
                    xh = Convert.ToInt32(selobjshcqs[0].Item3[GIS_Const.FIELD_XH]);
                }
                IPointCollection pntcol = new PolygonClass();
                if (selobjshcqs.Count == 0)
                {
                    //计算校正点距离切眼的距离
                    IPoint outp = new PointClass();
                    var distancealong = 0.0;
                    var distancefrom = 0.0;
                    var bres = false;
                    pline3.QueryPointAndDistance(esriSegmentExtension.esriNoExtension, pnt, false, outp,
                        ref distancealong, ref distancefrom, ref bres);
                    //根据距离绘制回采面
                    pntcol = Global.hcjsclass.GetBackPolygonArea(pline1, pline2, pline3, zywid, fywid, qywid,
                        distancefrom, 0);
                }
                else
                {
                    //IPolygon hcreg = selobjshcqs[0].Item2 as IPolygon;
                    //Dictionary<string, List<IPoint>> fourpnts = Global.commonclss.getCoordinates(hcreg, pline1, pline2, pline3, Global.linespace, Global.linespace);
                    //List<IPoint> listpnts = fourpnts["1"];
                    IPoint pntcenter = new PointClass();
                    pntcenter.PutCoords((oldpnts["1"][0].X + oldpnts["1"][1].X) / 2,
                        (oldpnts["1"][0].Y + oldpnts["1"][1].Y) / 2);
                    pntcenter.Z = 0;
                    //double hccd1 = Math.Sqrt(Math.Pow((pnt.X - pntcenter.X), 2) + Math.Pow((pnt.Y - pntcenter.Y), 2));
                    //查询回采方向 这里没有设置传入的切眼 可能会出错 需要调试2014-9-23
                    var dirflag = 0;
                    dirflag = Global.commonclss.GetDirectionByPnt(pline3, pntcenter);
                    pline3 = new PolylineClass();
                    if (oldpnts == null)
                        return;
                    pline3.FromPoint = oldpnts["1"][1];
                    pline3.ToPoint = oldpnts["1"][0];
                    //计算校正点距离切眼的距离
                    IPoint outp = new PointClass();
                    var distancealong = 0.0;
                    var distancefrom = 0.0;
                    var bres = false;
                    pline3.QueryPointAndDistance(esriSegmentExtension.esriNoExtension, pnt, false, outp,
                        ref distancealong, ref distancefrom, ref bres);
                    //根据距离绘制回采面
                    pntcol = Global.hcjsclass.GetBackPolygonArea(pline1, pline2, pline3, zywid, fywid, qywid,
                        distancefrom, dirflag);
                }
                var pnthccols = new List<IPoint>();
                for (var i = 0; i < pntcol.PointCount - 1; i++)
                {
                    pnthccols.Add(pntcol.get_Point(i));
                }
                var dics = new Dictionary<string, string>();
                dics[GIS_Const.FIELD_ID] = "0";
                dics[GIS_Const.FIELD_BS] = "1";
                dics[GIS_Const.FIELD_HDID] = hdids[GIS_Const.FIELD_HDID];
                dics[GIS_Const.FIELD_XH] = (xh + 1).ToString();
                Global.cons.AddHangdaoToLayer(pnthccols, dics, Global.hcqlyr);
                //将当前点写入到对应的工作面表中
                var prevPnt = pntcol.get_Point(pntcol.PointCount - 1);
                var workingFace = selectWorkingFaceControl1.SelectedWorkingFace;
                if (prevPnt != null)
                {
                    workingFace.SetCoordinate(prevPnt.X, prevPnt.Y, 0.0);
                    workingFace.Save();
                }
                //根据点查询60米范围内的地质构造的信息
                var hd_ids = new List<int>();
                hd_ids.Add(Convert.ToInt16(hd1));
                hd_ids.Add(Convert.ToInt16(hd2));
                hd_ids.Add(Convert.ToInt16(hd3));
                var geostructs = Global.commonclss.GetStructsInfos(prevPnt, hd_ids);
                if (geostructs == null) return;
                GeologySpace.DeleteAll(
                    GeologySpace.FindAllByProperty("WorkingFace.WorkingFaceId", workingFace.WorkingFaceId)
                        .Select(u => u.WorkingFace.WorkingFaceId));
                foreach (var key in geostructs.Keys)
                {
                    var geoinfos = geostructs[key];
                    var geo_type = key;
                    for (var i = 0; i < geoinfos.Count; i++)
                    {
                        var tmp = geoinfos[i];

                        var geologyspaceEntity = new GeologySpace();
                        geologyspaceEntity.WorkingFace = workingFace;
                        geologyspaceEntity.TectonicType = Convert.ToInt32(key);
                        geologyspaceEntity.TectonicId = tmp.geoinfos[GIS_Const.FIELD_BID];
                        geologyspaceEntity.Distance = tmp.dist;
                        geologyspaceEntity.OnDateTime = DateTime.Now.ToShortDateString();

                        geologyspaceEntity.Save();
                    }
                }
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            //关闭窗体
            Close();
        }
    }
}