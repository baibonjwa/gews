using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using LibEntity;
using LibBusiness;
using LibCommon;
using LibCommonForm;
using GIS.HdProc;
using ESRI.ArcGIS.Geometry;
using ESRI.ArcGIS.Geodatabase;
using LibCommonControl;
namespace _3.GeologyMeasure
{
    /// <summary>
    /// 回采进尺矫正
    /// </summary>
    public partial class TunnelHCJZEntering : BaseForm
    {
        /**********变量声明***********/
        DataGridViewCell[] dgvc = new DataGridViewCell[8];
        string[] dr = new string[8];
        int doing = 0;
        int _tmpRowIndex = -1;
        int _itemCount = 0;
        TunnelEntity _tunnelEntity = new TunnelEntity();
        WireInfoEntity wireInfoEntity = new WireInfoEntity();
        WirePointInfoEntity[] wpiEntity;
        int[] _arr = new int[5];
        DataSet _dsWirePoint = new DataSet();
        int _tunnelID;
        double _tmpDouble = 0;

        WorkingFaceEntity workingFace = null; // 工作面
        TunnelEntity tunnelZY = null;  // 主运
        TunnelEntity tunnelFY = null;  // 辅运顺槽
        TunnelEntity tunnelQY = null; // 切眼
        /*****************************/
        public TunnelHCJZEntering()
        {
            InitializeComponent();
            //日期
            dtpDate.Value = DateTime.Now.Date;
            //
            //设置窗体格式
            LibCommon.FormDefaultPropertiesSetter.SetEnteringFormDefaultProperties(this, "回采工作面校正");

            //this.selectTunnelUserControl1.init(this.MainForm);
            //自定义控件巷道过滤
            //this.selectTunnelUserControl1.SetFilterOn(TunnelTypeEnum.TUNNELLING);
            //自定义控件初始化
            //this.selectTunnelUserControl1.setCurSelectedID(_arr);
            //this.selectTunnelUserControl1.setCurSelectedID(_arr);
            //自定义控件初始化
            WorkingFaceSelectEntity workingFaceSelectEntity = WorkingFaceSelect.SelectWorkingFace(DayReportHCDbConstNames.TABLE_NAME);
            if (workingFaceSelectEntity != null)
            {
                _arr = new int[5];
                _arr[0] = workingFaceSelectEntity.MineID;
                _arr[1] = workingFaceSelectEntity.HorizontalID;
                _arr[2] = workingFaceSelectEntity.MiningAreaID;

                this.selectWorkingFaceControl1.setCurSelectedID(_arr);
            }
            else
            {
                this.selectWorkingFaceControl1.loadMineName();
            }
            // 注册委托事件
            this.selectWorkingFaceControl1.WorkingFaceNameChanged += NameChangeEvent;
        }

        private void NameChangeEvent(object sender, WorkingFaceEventArgs e)
        {
            updateWorkingFaceInfo(this.selectWorkingFaceControl1.IWorkingFaceId);
        }

        private void updateWorkingFaceInfo(int workingFaceId)
        {
            workingFace = BasicInfoManager.getInstance().getWorkingFaceById(workingFaceId);

            workingFace.tunnelSet = BasicInfoManager.getInstance().getTunnelSetByDataSet(TunnelInfoBLL.selectTunnelByWorkingFaceId(workingFace.WorkingFaceID));
            Dictionary<TunnelTypeEnum, TunnelEntity> tList = TunnelUtils.getTunnelDict(workingFace);
            if (tList.Count < 1)
                return;
            tunnelZY = tList[TunnelTypeEnum.STOPING_ZY];
            tunnelFY = tList[TunnelTypeEnum.STOPING_FY];
            tunnelQY = tList[TunnelTypeEnum.STOPING_QY];
        }
        /// <summary>
        /// 验证
        /// </summary>
        /// <returns></returns>
        private bool check()
        {
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

        private void btnOK_Click(object sender, EventArgs e)
        {
            // 验证
            if (!this.check())
            {
                DialogResult = DialogResult.None;
                return;
            }
            DialogResult = DialogResult.OK;
            //_tunnelEntity.TunnelID = selectTunnelUserControl1.ITunnelId;
            //_tunnelEntity = TunnelInfoBLL.selectTunnelInfoByTunnelID(_tunnelEntity.TunnelID);
            //验证
            if (!check())
            {
                DialogResult = DialogResult.None;
                return;
            }
            List<IPoint> coordinates = new List<IPoint>();
            for (int i = 0; i < this.dgrdvWire.Rows.Count - 1; i++)
            {
                double x = Convert.ToDouble(this.dgrdvWire.Rows[i].Cells[1].Value);
                double y = Convert.ToDouble(this.dgrdvWire.Rows[i].Cells[2].Value);
                double z = Convert.ToDouble(this.dgrdvWire.Rows[i].Cells[3].Value);
                IPoint pnt = new PointClass();
                pnt.X = x;
                pnt.Y = y;
                pnt.Z = z;
                coordinates.Add(pnt);
            }
            if (workingFace != null)
            {

                TunnelHcJz(coordinates, tunnelZY.TunnelID.ToString(), tunnelFY.TunnelID.ToString(), tunnelQY.TunnelID.ToString(), workingFace.WorkingFaceID,tunnelZY.TunnelWid,tunnelFY.TunnelWid,tunnelQY.TunnelWid);
            }
        }

        /// <summary>
        /// 巷道回采校正
        /// </summary>
        private void TunnelHcJz(List<IPoint> pnts,string hd1,string hd2,string hd3,int workingfaceid,double zywid,double fywid,double qywid)
        {
            //查询对应的巷道信息
            Dictionary<string, string> hdids = new Dictionary<string, string>();
            hdids.Add(GIS.GIS_Const.FIELD_HDID, hd1);
            List<Tuple<IFeature, IGeometry, Dictionary<string, string>>> selobjs1 = Global.commonclss.SearchFeaturesByGeoAndText(Global.centerlyr, hdids);
            IPolyline pline1 = selobjs1[0].Item2 as IPolyline;

            hdids[GIS.GIS_Const.FIELD_HDID] = hd2;
            List<Tuple<IFeature, IGeometry, Dictionary<string, string>>> selobjs2 = Global.commonclss.SearchFeaturesByGeoAndText(Global.centerlyr, hdids);
            IPolyline pline2 = selobjs2[0].Item2 as IPolyline;

            hdids[GIS.GIS_Const.FIELD_HDID] = hd3;
            List<Tuple<IFeature, IGeometry, Dictionary<string, string>>> selobjs3 = Global.commonclss.SearchFeaturesByGeoAndText(Global.centerlyr, hdids);
            IPolyline pline3 = selobjs3[0].Item2 as IPolyline;

            //删除原来的采掘区
            hdids[GIS.GIS_Const.FIELD_HDID] = hd1 + "_" + hd2;
            string sql = "\"" + GIS.GIS_Const.FIELD_HDID + "\"='" + hd1 + "_" + hd2 + "' AND \"" + GIS.GIS_Const.FIELD_BS + "\"=0";
            Global.commonclss.DelFeatures(Global.hcqlyr, sql);
            //查询对应的回采区
            for (int k = 0; k < pnts.Count; k++)
            {
                //导线点
                IPoint pnt = pnts[k];
                double jzx = pnt.X;
                double jzy = pnt.Y;
                //构造采掘区对象
                int xh = 0;
                List<Tuple<IFeature, IGeometry, Dictionary<string, string>>> selobjshcqs = Global.commonclss.SearchFeaturesByGeoAndText(Global.hcqlyr, hdids);
                Dictionary<string, List<IPoint>> oldpnts = null;
                if (selobjshcqs.Count != 0)
                {
                    oldpnts = Global.commonclss.getCoordinates(selobjshcqs[0].Item2 as IPolygon, pline1, pline2, pline3, zywid, fywid);
                    xh = Convert.ToInt32(selobjshcqs[0].Item3[GIS.GIS_Const.FIELD_XH]);
                }
                IPointCollection pntcol = new PolygonClass();
                if (selobjshcqs.Count == 0)
                {
                    //计算校正点距离切眼的距离
                    IPoint outp = new PointClass();
                    double distancealong = 0.0;
                    double distancefrom = 0.0;
                    bool bres = false;
                    pline3.QueryPointAndDistance(esriSegmentExtension.esriNoExtension, pnt, false, outp, ref distancealong, ref distancefrom, ref bres);
                    //根据距离绘制回采面
                    pntcol = Global.hcjsclass.GetBackPolygonArea(pline1, pline2, pline3, zywid,fywid,qywid, distancefrom, 0);
                }
                else
                {
                    //IPolygon hcreg = selobjshcqs[0].Item2 as IPolygon;
                    //Dictionary<string, List<IPoint>> fourpnts = Global.commonclss.getCoordinates(hcreg, pline1, pline2, pline3, Global.linespace, Global.linespace);
                    //List<IPoint> listpnts = fourpnts["1"];
                    IPoint pntcenter = new PointClass();
                    pntcenter.PutCoords((oldpnts["1"][0].X + oldpnts["1"][1].X) / 2, (oldpnts["1"][0].Y + oldpnts["1"][1].Y) / 2);
                    pntcenter.Z = 0;
                    //double hccd1 = Math.Sqrt(Math.Pow((pnt.X - pntcenter.X), 2) + Math.Pow((pnt.Y - pntcenter.Y), 2));
                    //查询回采方向 这里没有设置传入的切眼 可能会出错 需要调试2014-9-23
                    int dirflag = 0;
                    dirflag = Global.commonclss.GetDirectionByPnt(pline3, pntcenter);
                    pline3 = new PolylineClass();
                    if (oldpnts == null)
                        return;
                    pline3.FromPoint = oldpnts["1"][1];
                    pline3.ToPoint = oldpnts["1"][0];
                    //计算校正点距离切眼的距离
                    IPoint outp = new PointClass();
                    double distancealong = 0.0;
                    double distancefrom = 0.0;
                    bool bres = false;
                    pline3.QueryPointAndDistance(esriSegmentExtension.esriNoExtension, pnt, false, outp, ref distancealong, ref distancefrom, ref bres);
                    //根据距离绘制回采面
                    pntcol = Global.hcjsclass.GetBackPolygonArea(pline1, pline2, pline3, zywid,fywid,qywid, distancefrom, dirflag);
                }
                List<IPoint> pnthccols = new List<IPoint>();
                for (int i = 0; i < pntcol.PointCount - 1; i++)
                {
                    pnthccols.Add(pntcol.get_Point(i));
                }
                Dictionary<string, string> dics = new Dictionary<string, string>();
                dics[GIS.GIS_Const.FIELD_ID] = "0";
                dics[GIS.GIS_Const.FIELD_BS] = "1";
                dics[GIS.GIS_Const.FIELD_HDID] = hdids[GIS.GIS_Const.FIELD_HDID];
                dics[GIS.GIS_Const.FIELD_XH] = (xh + 1).ToString();
                Global.cons.AddHangdaoToLayer(pnthccols, dics, Global.hcqlyr);
                //将当前点写入到对应的工作面表中
                IPoint prevPnt = pntcol.get_Point(pntcol.PointCount - 1);
                if (prevPnt != null)
                {
                    workingFace.Coordinate = new CoordinateEntity(prevPnt.X, prevPnt.Y, 0.0);
                    bool bres = LibBusiness.WorkingFaceBLL.updateWorkingfaceXYZ(workingFace);
                }
                //根据点查询60米范围内的地质构造的信息
                List<int> hd_ids = new List<int>();
                hd_ids.Add(Convert.ToInt16(hd1));
                hd_ids.Add(Convert.ToInt16(hd2));
                hd_ids.Add(Convert.ToInt16(hd3));
                Dictionary<string, List<GeoStruct>> geostructs = Global.commonclss.GetStructsInfos(prevPnt, hd_ids);
                if (geostructs == null) return;
                GeologySpaceBLL.deleteGeologySpaceEntityInfos(workingFace.WorkingFaceID);//删除对应工作面ID的地质构造信息
                foreach (string key in geostructs.Keys)
                {
                    List<GeoStruct> geoinfos = geostructs[key];
                    string geo_type = key;
                    for (int i = 0; i < geoinfos.Count; i++)
                    {
                        GeoStruct tmp = geoinfos[i];

                        GeologySpaceEntity geologyspaceEntity = new GeologySpaceEntity();
                        geologyspaceEntity.WorkSpaceID = workingFace.WorkingFaceID;
                        geologyspaceEntity.TectonicType = Convert.ToInt32(key);
                        geologyspaceEntity.TectonicID = tmp.geoinfos[GIS.GIS_Const.FIELD_BID].ToString();
                        geologyspaceEntity.Distance = tmp.dist;
                        geologyspaceEntity.onDateTime = DateTime.Now.ToShortDateString();

                        GeologySpaceBLL.insertGeologySpaceEntityInfo(geologyspaceEntity);
                    }
                }
            }
        }
        private void btnCancel_Click(object sender, EventArgs e)
        {
            //关闭窗体
            this.Close();
        }
    }
}
