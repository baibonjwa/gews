using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Castle.ActiveRecord;
using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.Geodatabase;
using ESRI.ArcGIS.Geometry;
using GIS;
using GIS.Common;
using GIS.HdProc;
using GIS.SpecialGraphic;
using LibBusiness;
using LibCommon;
using LibCommonControl;
using LibCommonForm;
using LibEntity;
using LibGeometry;
using LibSocket;
using Point = ESRI.ArcGIS.Geometry.Point;
using TunnelDefaultSelect = LibEntity.TunnelDefaultSelect;

namespace sys3
{
    public partial class WireInfoEntering : Form
    {
        /**********变量声明***********/
        private readonly int[] _arr = new int[5];
        private readonly DataGridViewCell[] dgvc = new DataGridViewCell[8];
        private readonly string[] dr = new string[8];
        private WirePoint[] wirePoints;
        private int _itemCount;
        private double _tmpDouble;
        private int _tmpRowIndex = -1;
        private int _tunnelID;
        private int doing;
        private Tunnel tunnelEntity = new Tunnel();
        private Wire wireEntity = new Wire();
        /*****************************/


        private Wire Wire { get; set; }

        /// <summary>
        ///     构造方法
        /// </summary>
        public WireInfoEntering()
        {

            InitializeComponent();
            FormDefaultPropertiesSetter.SetEnteringFormDefaultProperties(this, Const_GM.WIRE_INFO_ADD);
            //自定义控件初始化
            TunnelDefaultSelect tunnelDefaultSelectEntity =
                LibBusiness.TunnelDefaultSelect.selectDefaultTunnel(Wire.TableName);
            if (tunnelDefaultSelectEntity != null)
            {
            }
            else
            {
                selectTunnelUserControl1.LoadData();
            }
            // 注册委托事件
            //selectTunnelUserControl1.TunnelNameChanged +=
            //    InheritTunnelNameChanged;
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
        ///     构造方法
        /// </summary>
        /// <param name="wire"></param>
        public WireInfoEntering(Wire wire)
        {
            // 初始化主窗体变量
            Wire = wire;
            InitializeComponent();
            // 加载需要修改的导线数据
            loadWireInfoData();

            FormDefaultPropertiesSetter.SetEnteringFormDefaultProperties(this, Const_GM.WIRE_INFO_CHANGE);
            //this.selectTunnelUserControl1.setCurSelectedID(_arr);
            _tunnelID = _arr[4];

            // 注册委托事件
            //selectTunnelUserControl1.TunnelNameChanged +=
            //    InheritTunnelNameChanged;

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
        ///     委托事件
        /// </summary>
        /// <param name="sender"></param>
        //private void InheritTunnelNameChanged(object sender, TunnelEventArgs e)
        //{
        //    AutoChangeWireName();
        //}

        /// <summary>
        ///     修改绑定数据
        /// </summary>
        private void loadWireInfoData()
        {
            _itemCount = 0;
            wirePoints = WirePoint.FindAllByWireId(wireEntity.WireId);
            if (wirePoints.Length > 0)
            {
                for (int i = 0; i < wirePoints.Length; i++)
                {
                    dgrdvWire[0, i].Value = wirePoints[i].WirePointId;
                    dgrdvWire[1, i].Value = wirePoints[i].CoordinateX;
                    dgrdvWire[2, i].Value = wirePoints[i].CoordinateY;
                    dgrdvWire[3, i].Value = wirePoints[i].CoordinateZ;
                    dgrdvWire[4, i].Value = wirePoints[i].LeftDis;
                    dgrdvWire[5, i].Value = wirePoints[i].RightDis;
                    dgrdvWire[6, i].Value = wirePoints[i].TopDis;
                    dgrdvWire[7, i].Value = wirePoints[i].BottomDis;
                    _itemCount++;
                }
            }
            txtWireName.Text = wireEntity.WireName;
            txtWireLevel.Text = wireEntity.WireLevel;
            dtpMeasureDate.Value = wireEntity.MeasureDate;
            cboVobserver.Text = wireEntity.Vobserver;
            cboVobserver.Text = wireEntity.Vobserver;
            cboCounter.Text = wireEntity.Counter;
            cboCounter.Text = wireEntity.Counter;
            dtpCountDate.Value = wireEntity.CountDate;
            cboChecker.Text = wireEntity.Checker;
            cboChecker.Text = wireEntity.Checker;
            dtpCheckDate.Value = wireEntity.CheckDate;
        }

        /// <summary>
        ///     提交
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnSubmit_Click(object sender, EventArgs e)
        {
            if (!Check())
            {
                DialogResult = DialogResult.None;
                return;
            }

            Wire wire;
            using (new SessionScope())
            {
                wire = Wire.FindOneByTunnelId(selectTunnelUserControl1.SelectedTunnel.TunnelId);
            }

            wire.WirePoints = insertWireInfo();

            //判断导线点录入个数是否小于2
            if (Text == Const_GM.WIRE_INFO_ADD)
            {
                //获取巷道对应导线信息
                if (wire != null)
                {
                    if (dgrdvWire.Rows.Count < 3) //添加时最后有一个空行
                    {
                        Alert.alert(Const_GM.WIRE_INFO_MSG_POINT_MUST_MORE_THAN_TWO);
                        return;
                    }
                }
            }


            List<WirePoint> lstWirePointInfoEnt;


            string sADDorCHANGE = "";
            if (Text == Const_GM.WIRE_INFO_ADD)
            {
                sADDorCHANGE = "ADD";
                /// 2014.2.26 lyf 绘制导线点和巷道，下同
                lstWirePointInfoEnt = new List<WirePoint>();
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
                        Tunnel tunnel = Tunnel.Find(_tunnelID);
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
                        dics = ConstructDics(tunnel, out hdwid);
                        AddHdbyPnts(lstWirePointInfoEnt, dics, hdwid);
                    }
                }
            }

            if (Text == Const_GM.WIRE_INFO_CHANGE)
            {
                sADDorCHANGE = "CHANGE";
                /// 2014.2.26 lyf
                WirePoint[] wirePointEnt = updateWireInfo();
                lstWirePointInfoEnt = new List<WirePoint>();
                if (wirePointEnt != null)
                {
                    lstWirePointInfoEnt = wirePointEnt.ToList();
                    DrawWirePoint(lstWirePointInfoEnt, sADDorCHANGE);

                    DialogResult dlgResult = MessageBox.Show("是否同时更新巷道图形？", "提示", MessageBoxButtons.YesNo,
                        MessageBoxIcon.Question, MessageBoxDefaultButton.Button1);
                    if (dlgResult == DialogResult.Yes)
                    {
                        //DrawTunnel(lstWirePointInfoEnt, sADDorCHANGE);
                        Tunnel tunnel = Tunnel.Find(_tunnelID);
                        double hdwid = 0.0;
                        dics = ConstructDics(tunnel, out hdwid);
                        if (tunnel != null)
                        {
                            UpdateHdbyPnts(lstWirePointInfoEnt, dics, hdwid);
                        }
                    }
                }
            }
            DialogResult = DialogResult.OK;
        }

        private Dictionary<string, string> ConstructDics(Tunnel tunnel, out double hdwid)
        {
            //巷道信息赋值
            hdwid = 0.0;
            var flds = new Dictionary<string, string>();
            flds.Add(GIS_Const.FIELD_HDID, tunnelEntity.TunnelId.ToString());
            List<Tuple<IFeature, IGeometry, Dictionary<string, string>>> selobjs =
                Global.commonclss.SearchFeaturesByGeoAndText(Global.centerfdlyr, flds);

            int xh = 0;
            if (selobjs.Count > 0)
                xh = Convert.ToInt16(selobjs[0].Item3[GIS_Const.FIELD_XH]) + 1;
            string bid = "", hdname = "";
            if (tunnel != null)
            {
                bid = tunnel.BindingId;
                hdname = tunnel.TunnelName;
                hdwid = tunnel.TunnelWid;
            }
            dics.Clear();
            dics.Add(GIS_Const.FIELD_HDID, tunnelEntity.TunnelId.ToString());
            dics.Add(GIS_Const.FIELD_ID, "0");
            dics.Add(GIS_Const.FIELD_BS, "1");
            dics.Add(GIS_Const.FIELD_BID, bid);
            dics.Add(GIS_Const.FIELD_HDNAME, hdname);
            dics.Add(GIS_Const.FIELD_XH, (xh + 1).ToString());
            return dics;
        }

        /// <summary>
        ///     导线实体赋值
        /// </summary>
        private void setWireInfoEntity()
        {
            wireEntity.Tunnel = selectTunnelUserControl1.SelectedTunnel;
            ;
            //tunnelEntity = TunnelInfoBLL.selectTunnelInfoByTunnelID(wireEntity.Tunnel);
            //导线名称
            wireEntity.WireName = txtWireName.Text;
            //导线级别
            wireEntity.WireLevel = txtWireLevel.Text;
            //测量日期
            wireEntity.MeasureDate = dtpMeasureDate.Value;
            //观测者
            wireEntity.Vobserver = cboVobserver.Text;
            //txtVobserver.Text;
            //计算者
            wireEntity.Counter = cboCounter.Text;
            //txtCounter.Text;
            //计算日期
            wireEntity.CountDate = dtpCountDate.Value;
            //校核者
            wireEntity.Checker = cboChecker.Text;
            //txtChecker.Text
            //校核日期
            wireEntity.CheckDate = dtpCheckDate.Value;
        }

        /// <summary>
        ///     导线点实体赋值
        /// </summary>
        /// <param name="i">Datagridview行号</param>
        /// <returns>导线点实体</returns>
        private WirePoint setWirePointEntity(int i)
        {
            // 最后一行为空行时，跳出循环
            if (i == dgrdvWire.RowCount - 1)
            {
                return null;
            }
            // 创建导线点实体
            var wirePointInfoEntity = new WirePoint();
            if (Text == Const_GM.WIRE_INFO_CHANGE)
            {
                if (i < wirePoints.Length)
                {
                    wirePointInfoEntity.WirePointId = wirePoints[i].WirePointId;
                }
            }

            //导线点编号
            if (dgrdvWire.Rows[i].Cells[0] != null)
            {
                wirePointInfoEntity.WirePointName = dgrdvWire.Rows[i].Cells[0].Value.ToString();
            }
            //坐标X
            if (dgrdvWire.Rows[i].Cells[1].Value != null)
            {
                if (double.TryParse(dgrdvWire.Rows[i].Cells[1].Value.ToString(), out _tmpDouble))
                {
                    wirePointInfoEntity.CoordinateX = _tmpDouble;
                    _tmpDouble = 0;
                }
            }
            //坐标Y
            if (dgrdvWire.Rows[i].Cells[2].Value != null)
            {
                if (double.TryParse(dgrdvWire.Rows[i].Cells[2].Value.ToString(), out _tmpDouble))
                {
                    wirePointInfoEntity.CoordinateY = _tmpDouble;
                    _tmpDouble = 0;
                }
            }
            //坐标Z
            if (dgrdvWire.Rows[i].Cells[3].Value != null)
            {
                if (double.TryParse(dgrdvWire.Rows[i].Cells[3].Value.ToString(), out _tmpDouble))
                {
                    wirePointInfoEntity.CoordinateZ = _tmpDouble;
                    _tmpDouble = 0;
                }
            }
            //距左帮距离
            if (dgrdvWire.Rows[i].Cells[4].Value != null)
            {
                if (double.TryParse(dgrdvWire.Rows[i].Cells[4].Value.ToString(), out _tmpDouble))
                {
                    wirePointInfoEntity.LeftDis = _tmpDouble;
                    _tmpDouble = 0;
                }
            }
            //距右帮距离
            if (dgrdvWire.Rows[i].Cells[5].Value != null)
            {
                if (double.TryParse(dgrdvWire.Rows[i].Cells[5].Value.ToString(), out _tmpDouble))
                {
                    wirePointInfoEntity.RightDis = _tmpDouble;
                    _tmpDouble = 0;
                }
            }
            //距顶板距离
            if (dgrdvWire.Rows[i].Cells[6].Value != null)
            {
                if (double.TryParse(dgrdvWire.Rows[i].Cells[6].Value.ToString(), out _tmpDouble))
                {
                    wirePointInfoEntity.TopDis = _tmpDouble;
                    _tmpDouble = 0;
                }
            }
            //距底板距离
            if (dgrdvWire.Rows[i].Cells[7].Value != null)
            {
                if (double.TryParse(dgrdvWire.Rows[i].Cells[7].Value.ToString(), out _tmpDouble))
                {
                    wirePointInfoEntity.BottomDis = _tmpDouble;
                    _tmpDouble = 0;
                }
            }
            wirePointInfoEntity.Wire.WireId = wireEntity.WireId;

            return wirePointInfoEntity;
        }

        /// <summary>
        ///     2014.2.26 lyf 修改函数，返回导线点List，为绘制导线点图形
        /// </summary>
        /// <returns>导线点List</returns>
        private List<WirePoint> insertWireInfo()
        {
            setWireInfoEntity();

            DialogResult = DialogResult.OK;
            //导线信息登陆
            bool bResult = false;
            //无导线时插入
            if (Wire.FindOneByTunnelId(tunnelEntity.TunnelId) == null)
            {
                LibBusiness.TunnelDefaultSelect.InsertDefaultTunnel(Wire.TableName,
                    selectTunnelUserControl1.SelectedTunnel.TunnelId);
                wireEntity.Save();
                var msg = new UpdateWarningDataMsg(Const.INVALID_ID, selectTunnelUserControl1.SelectedTunnel.TunnelId,
                    Wire.TableName, OPERATION_TYPE.ADD, wireEntity.MeasureDate);
                SocketUtil.SendMsg2Server(msg);
            }
            //导线存在时跳过
            else
            {
                bResult = true;
            }
            //导线编号
            wireEntity.WireId = Wire.FindOneByTunnelId(wireEntity.Tunnel.TunnelId).WireId;
            //导线点信息登陆
            var wirePointInfoEntityList = new List<WirePoint>();
            for (int i = 0; i < dgrdvWire.RowCount; i++)
            {
                var wirePointInfoEntity = new WirePoint();

                wirePointInfoEntity = setWirePointEntity(i);

                if (wirePointInfoEntity == null)
                {
                    break;
                }

                wirePointInfoEntity.BindingId = IDGenerator.NewBindingID();

                wirePointInfoEntityList.Add(wirePointInfoEntity);
            }

            if (bResult)
            {
                foreach (WirePoint wirePointInfoEntity in wirePointInfoEntityList)
                {
                    wirePointInfoEntity.Save();
                    var msg = new UpdateWarningDataMsg(Const.INVALID_ID, selectTunnelUserControl1.SelectedTunnel.TunnelId,
                        Wire.TableName, OPERATION_TYPE.ADD, wireEntity.MeasureDate);
                    SocketUtil.SendMsg2Server(msg);
                }
            }
            return wirePointInfoEntityList;
        }

        /// <summary>
        ///     2014.2.26 lyf 修改函数，返回导线点List，为绘制导线点图形
        /// </summary>
        /// <returns>导线点List</returns>
        private WirePoint[] updateWireInfo()
        {
            setWireInfoEntity();
            var wirePointInfoEntity = new WirePoint();
            var wirePointInfoEnt = new WirePoint[dgrdvWire.RowCount - 1];
            for (int i = 0; i < dgrdvWire.RowCount - 1; i++)
            {
                // 创建导线点实体

                wirePointInfoEntity = setWirePointEntity(i);
                if (wirePointInfoEntity == null)
                {
                    break;
                }

                wirePointInfoEnt[i] = wirePointInfoEntity;
            }

            //导线信息登陆
            _tunnelID = selectTunnelUserControl1.SelectedTunnel.TunnelId;
            LibBusiness.TunnelDefaultSelect.UpdateDefaultTunnel(Wire.TableName,
                selectTunnelUserControl1.SelectedTunnel.TunnelId);
            wireEntity.Tunnel = Tunnel.Find(_tunnelID);
            wireEntity.Save();
            //导线点信息登陆
            for (int j = 0; j < dgrdvWire.Rows.Count - 1; j++)
            {
                if (j < wirePoints.Length)
                {
                    //修改导线点
                    wirePointInfoEnt[j].Save();
                    wireEntity.Save();
                    //socket
                    var msg = new UpdateWarningDataMsg(Const.INVALID_ID, selectTunnelUserControl1.SelectedTunnel.TunnelId,
                        Wire.TableName, OPERATION_TYPE.UPDATE, wireEntity.MeasureDate);
                    SocketUtil.SendMsg2Server(msg);
                }
                else
                {
                    //超出数量部分做添加操作
                    //BindingID
                    wirePointInfoEnt[j].BindingId = IDGenerator.NewBindingID();
                    //添加导线点
                    wirePointInfoEnt[j].Save();
                    //socket

                    var msg = new UpdateWarningDataMsg(Const.INVALID_ID, selectTunnelUserControl1.SelectedTunnel.TunnelId,
                       Wire.TableName, OPERATION_TYPE.ADD, wireEntity.MeasureDate);
                    SocketUtil.SendMsg2Server(msg);
                }
            }

            //导线点实体
            //当条数少于导线点个数时，多于部分做删除处理
            if (dgrdvWire.Rows.Count <= _itemCount)
            {
                for (int i = dgrdvWire.Rows.Count - 1; i < _itemCount; i++)
                {
                    wirePointInfoEntity.WirePointId =
                        Convert.ToInt32(wirePoints[i].WirePointId);
                    wireEntity.WireId =
                        Convert.ToInt32(
                            wirePoints[i].Wire.WireId);
                    //只剩一个空行时，即所有导线点信息全被删除时
                    //删除导线，导线点
                    if (dgrdvWire.Rows.Count == 1)
                    {
                        wireEntity.Delete();
                    }
                    //只删除多于导线点
                    else
                    {
                        wirePointInfoEntity.Delete();
                    }
                }
            }
            //返回导线点信息组
            return wirePointInfoEnt;
        }

        /// <summary>
        ///     取消
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnCancel_Click(object sender, EventArgs e)
        {
            // 关闭窗口
            Close();
        }

        /// <summary>
        ///     验证画面入力数据
        /// </summary>
        /// <returns>验证结果：true 通过验证, false未通过验证</returns>
        private bool Check()
        {
            for (int i = 0; i < dgrdvWire.Rows.Count; i++)
            {
                dgrdvWire.BackgroundColor = Const.NO_ERROR_FIELD_COLOR;
            }
            //// 判断巷道信息是否选择
            if (selectTunnelUserControl1.SelectedTunnel == null)
            {
                Alert.alert(Const.MSG_PLEASE_CHOOSE + Const_GM.TUNNEL + Const.SIGN_EXCLAMATION_MARK);
                return false;
            }
            if (Validator.IsEmpty(txtWireName.Text))
            {
                txtWireName.BackColor = Const.ERROR_FIELD_COLOR;
                Alert.alert(Const.MSG_PLEASE_TYPE_IN + Const_GM.WIRE_NAME + Const.SIGN_EXCLAMATION_MARK);
                return false;
            }
            txtWireName.BackColor = Const.NO_ERROR_FIELD_COLOR;
            // 判断导线点编号是否入力
            if (dgrdvWire.Rows.Count - 1 == 0)
            {
                Alert.alert(Const.MSG_PLEASE_TYPE_IN + Const_GM.WIRE_POINT_ID + Const.SIGN_EXCLAMATION_MARK);
                return false;
            }
            //dgrdvWire内部判断
            for (int i = 0; i < dgrdvWire.RowCount; i++)
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

                ////判断导线点编号是否存在
                //if (Text == Const_GM.WIRE_INFO_ADD)
                //{
                //    //导线点是否存在
                //    if (WirePoint.ExistsByWirePointIdInWireInfo(wireEntity.WireId,
                //        dgrdvWire.Rows[i].Cells[0].Value.ToString()))
                //    {
                //        cell.Style.BackColor = Const.ERROR_FIELD_COLOR;
                //        Alert.alert(Const_GM.WIRE_POINT_ID + Const.MSG_ALREADY_HAVE + Const.SIGN_EXCLAMATION_MARK);
                //        return false;
                //    }
                //    cell.Style.BackColor = Const.NO_ERROR_FIELD_COLOR;
                //}
                ////判断导线点编号是否有输入重复
                //for (int j = 0; j < i; j++)
                //{
                //    if (dgrdvWire[0, j].Value.ToString() == dgrdvWire[0, i].Value.ToString())
                //    {
                //        cell.Style.BackColor = Const.ERROR_FIELD_COLOR;
                //        dgrdvWire[0, j].Style.BackColor = Const.ERROR_FIELD_COLOR;
                //        Alert.alert(Const_GM.WIRE_POINT_ID + Const.MSG_DOUBLE_EXISTS + Const.SIGN_EXCLAMATION_MARK);
                //        return false;
                //    }
                //    cell.Style.BackColor = Const.NO_ERROR_FIELD_COLOR;
                //    dgrdvWire[0, j].Style.BackColor = Const.NO_ERROR_FIELD_COLOR;
                //}

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


        /// <summary>
        ///     右键插入
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void 插入ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            dgrdvWire.Rows.Insert(selectionIdx, 1);
        }

        /// <summary>
        ///     右键复制
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
        ///     右键剪切
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
        ///     右键粘贴
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
        ///     右键上移
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void 上移ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var dgvr = new DataGridViewRow();
            dgvr = dgrdvWire.Rows[selectionIdx];
            dgrdvWire.Rows.RemoveAt(selectionIdx);
            dgrdvWire.Rows.Insert(selectionIdx - 1, dgvr);
            dgrdvWire.CurrentCell = dgrdvWire.Rows[selectionIdx - 1].Cells[0];
        }

        /// <summary>
        ///     右键下移
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void 下移ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var dgvr = new DataGridViewRow();
            dgvr = dgrdvWire.Rows[selectionIdx];
            dgrdvWire.Rows.RemoveAt(selectionIdx);
            dgrdvWire.Rows.Insert(selectionIdx + 1, dgvr);
            dgrdvWire.CurrentCell = dgrdvWire.Rows[selectionIdx + 1].Cells[0];
        }

        /// <summary>
        ///     显示行号
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void dgrdvWire_RowPostPaint(object sender, DataGridViewRowPostPaintEventArgs e)
        {
            var rectangle = new Rectangle(e.RowBounds.Location.X,
                e.RowBounds.Location.Y, dgrdvWire.RowHeadersWidth - 4, e.RowBounds.Height);

            TextRenderer.DrawText(e.Graphics, (e.RowIndex + 1).ToString(),
                dgrdvWire.RowHeadersDefaultCellStyle.Font, rectangle,
                dgrdvWire.RowHeadersDefaultCellStyle.ForeColor,
                TextFormatFlags.VerticalCenter | TextFormatFlags.Right);
        }

        /// <summary>
        ///     初始化
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void WireInfoEntering_Load(object sender, EventArgs e)
        {
            if (Text == Const_GM.WIRE_INFO_ADD)
            {
                selectTunnelUserControl1.LoadData();
                dtpMeasureDate.Value = DateTime.Now;
                dtpCountDate.Value = DateTime.Now;
                dtpCheckDate.Value = DateTime.Now;
            }
            else
            {
                selectTunnelUserControl1.LoadData(Wire.Tunnel);
            }
        }

        /// <summary>
        ///     datagridview进入行时，按钮可操作性
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void dgrdvWire_RowEnter(object sender, DataGridViewCellEventArgs e)
        {
            _tmpRowIndex = e.RowIndex;
            selectionIdx = e.RowIndex;
            btnAdd.Enabled = true;
            btnDel.Enabled = true;
            for (int i = 0; i < dgvc.Length; i++)
            {
                if (dgvc[i] != null)
                {
                    break;
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
                btnMoveUp.Enabled = false;
                btnDel.Enabled = false;
            }
            else
            {
                btnDel.Enabled = true;
            }
        }

        /// <summary>
        ///     复制按钮
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
        }

        /// <summary>
        ///     粘贴按钮
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnPaste_Click(object sender, EventArgs e)
        {
            dgrdvWire.Rows.Insert(dgrdvWire.Rows[_tmpRowIndex].Index, dr);
        }

        /// <summary>
        ///     添加按钮
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
        ///     删除按钮
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnDel_Click(object sender, EventArgs e)
        {
            if (dgrdvWire.Rows.Count > 1 && dgrdvWire.CurrentRow.Index < dgrdvWire.Rows.Count - 1)
                dgrdvWire.Rows.RemoveAt(_tmpRowIndex);
        }

        /// <summary>
        ///     上移按钮
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

            var dgvr = new DataGridViewRow();
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
        ///     下移按钮
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnMoveDown_Click(object sender, EventArgs e)
        {
            _tmpRowIndex = dgrdvWire.SelectedRows[0].Index;
            var dgvr = new DataGridViewRow();
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
            var ofd = new OpenFileDialog { RestoreDirectory = true, Filter = @"文本文件(*.txt)|*.txt|所有文件(*.*)|*.*" };
            if (ofd.ShowDialog() == DialogResult.OK)
            {
                string fileName = ofd.SafeFileName;
                if (fileName != null)
                {
                    string[] strs = fileName.Split('-');
                    string workingFaceName = strs[0];
                    string tunnelName = strs[1].Split('.')[0];
                    using (new SessionScope())
                    {
                        WorkingFace workingFace = WorkingFace.FindByWorkingFaceName(workingFaceName);
                        var tunnel = workingFace.Tunnels.First(u => u.TunnelName == tunnelName);
                        selectTunnelUserControl1.LoadData(tunnel);
                    }
                    txtWireName.Text = tunnelName.Split('.').Length > 0 ? tunnelName.Split('.')[0] + "导线点" : tunnelName + "导线点";
                }

                var sr = new StreamReader(ofd.FileName, Encoding.GetEncoding("GB2312"));
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

        #region ******datagridview鼠标拖动排序******

        private DataGridViewRow ddr = new DataGridViewRow();
        private DataGridViewRow nr = new DataGridViewRow();
        private int selectionIdx = -1;

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
                    剪切ToolStripMenuItem.Enabled = false;
                    复制ToolStripMenuItem.Enabled = false;
                    上移ToolStripMenuItem.Enabled = false;
                    下移ToolStripMenuItem.Enabled = false;
                }
                else
                {
                    剪切ToolStripMenuItem.Enabled = true;
                    复制ToolStripMenuItem.Enabled = true;
                    上移ToolStripMenuItem.Enabled = true;
                    下移ToolStripMenuItem.Enabled = true;
                }
            }
            else
            {
                插入ToolStripMenuItem.Enabled = false;
                剪切ToolStripMenuItem.Enabled = false;
                复制ToolStripMenuItem.Enabled = false;
                上移ToolStripMenuItem.Enabled = false;
                下移ToolStripMenuItem.Enabled = false;
                粘贴ToolStripMenuItem.Enabled = false;
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
                var row = (DataGridViewRow)e.Data.GetData(typeof(DataGridViewRow));
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
        ///     鼠标落点获取行号
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

        #region 绘制导线点和巷道图形

        private Dictionary<string, string> dics = new Dictionary<string, string>(); //属性字典
        private List<IPoint> dpts = new List<IPoint>();
        private List<IPoint> leftpts = new List<IPoint>(); //记录左侧平行线坐标
        private List<IPoint> rightpts = new List<IPoint>(); //记录右侧平行线坐标

        /// <summary>
        ///     通过（关键/导线）点绘制巷道
        /// </summary>
        /// <param name="wirepntcols">导线信息列表</param>
        /// <param name="dics">巷道属性</param>
        /// <param name="hdwid">巷道宽度</param>
        private void AddHdbyPnts(List<WirePoint> wirepntcols, Dictionary<string, string> dics, double hdwid)
        {
            List<IPoint> rightresults = null;
            List<IPoint> leftresults = null;
            List<IPoint> results = null;

            if (wirepntcols == null || wirepntcols.Count == 0)
                return;

            var pntcols = new List<IPoint>();
            for (int i = 0; i < wirepntcols.Count; i++)
            {
                IPoint pnt = new PointClass();
                pnt.X = wirepntcols[i].CoordinateX;
                pnt.Y = wirepntcols[i].CoordinateY;
                pnt.Z = wirepntcols[i].CoordinateZ;
                pntcols.Add(pnt);
            }

            Global.cons.AddHangdaoToLayer(pntcols, dics, Global.pntlyr, wirepntcols); //添加中线上的点到导线点图层中
            Global.cons.AddDxdLines(pntcols, dics, Global.pntlinlyr, wirepntcols); //添加导线点线图层符号
            Global.cons.AddHangdaoToLayer(pntcols, dics, Global.centerlyr); //添加中心线到线图层中
            Global.cons.AddFDLineToLayer(pntcols, dics, Global.centerfdlyr, 1); //添加分段中心线到中心线分段图层中

            //#################计算交点坐标######################
            rightpts = Global.cons.GetLRParallelPnts(pntcols, hdwid, 1); //右侧平行线上的端点串
            leftpts = Global.cons.GetLRParallelPnts(pntcols, hdwid, 0); //左侧平行线上的端点串

            //rightresults = Global.cons.CalculateRegPnts(rightpts);
            //leftresults = Global.cons.CalculateRegPnts(leftpts);
            //results = Global.cons.ConstructPnts(rightresults, leftresults);
            results = Global.cons.ConstructPnts(rightpts, leftpts);

            ////在巷道面显示面中绘制巷道面  
            Global.cons.AddHangdaoToLayer(results, dics, Global.hdfdfulllyr); //添加巷道到巷道图层中
            //Global.cons.AddFDRegToLayer(rightresults, leftresults, pntcols, dics, Global.hdfdlyr);
            //Global.cons.AddHangdaoToLayer(rightpts, dics, Global.pntlyr);
            //Global.cons.AddHangdaoToLayer(leftpts, dics, Global.pntlyr);
            Global.cons.AddFDRegToLayer(rightpts, leftpts, pntcols, dics, Global.hdfdlyr, hdwid);
            Global.pActiveView.Refresh();
        }

        /// <summary>
        ///     更新巷道
        /// </summary>
        /// <param name="wirepntcols"></param>
        /// <param name="dics"></param>
        private void UpdateHdbyPnts(List<WirePoint> wirepntcols, Dictionary<string, string> dics, double hdwid)
        {
            List<IPoint> rightresults = null;
            List<IPoint> leftresults = null;
            List<IPoint> results = null;

            if (wirepntcols == null || wirepntcols.Count == 0)
                return;

            var pntcols = new List<IPoint>();
            for (int i = 0; i < wirepntcols.Count; i++)
            {
                IPoint pnt = new PointClass();
                pnt.X = wirepntcols[i].CoordinateX;
                pnt.Y = wirepntcols[i].CoordinateY;
                pnt.Z = wirepntcols[i].CoordinateZ;
                pntcols.Add(pnt);
            }
            //清除图层上对应的信息
            string sql = "\"" + GIS_Const.FIELD_HDID + "\"='" + wireEntity.Tunnel + "'";
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
            Global.cons.AddHangdaoToLayer(pntcols, dics, Global.pntlyr, wirepntcols); //添加中线上的点到导线点图层中
            Global.cons.AddDxdLines(pntcols, dics, Global.pntlinlyr, wirepntcols); //添加导线点线
            Global.cons.AddHangdaoToLayer(pntcols, dics, Global.centerlyr); //添加中心线到线图层中
            Global.cons.AddFDLineToLayer(pntcols, dics, Global.centerfdlyr, 1); //添加分段中心线到中心线分段图层中
            //#################计算交点坐标######################
            rightpts = Global.cons.GetLRParallelPnts(pntcols, hdwid, 1); //右侧平行线上的端点串
            leftpts = Global.cons.GetLRParallelPnts(pntcols, hdwid, 0); //左侧平行线上的端点串
            //rightresults = Global.cons.CalculateRegPnts(rightpts);
            //leftresults = Global.cons.CalculateRegPnts(leftpts);
            //results = Global.cons.ConstructPnts(rightresults, leftresults);
            //Global.cons.AddHangdaoToLayer(rightpts, dics, Global.pntlyr);
            //Global.cons.AddHangdaoToLayer(leftpts, dics, Global.pntlyr);
            results = Global.cons.ConstructPnts(rightpts, leftpts);
            //在巷道面显示面中绘制巷道面  
            //Global.cons.AddHangdaoToLayer(rightpts, dics, Global.pntlyr);
            //Global.cons.AddHangdaoToLayer(leftpts, dics, Global.pntlyr);
            Global.cons.AddHangdaoToLayer(results, dics, Global.hdfdfulllyr); //添加巷道到巷道图层中
            //Global.cons.AddFDRegToLayer(rightresults, leftresults, pntcols, dics, Global.hdfdlyr);
            Global.cons.AddFDRegToLayer(rightpts, leftpts, pntcols, dics, Global.hdfdlyr, hdwid);
            Global.pActiveView.Refresh();
        }

        /// <summary>
        ///     根据坐标绘制导线点
        /// </summary>
        /// <param name="lstWPIE">导线坐标（List）</param>
        private void DrawWirePoint(List<WirePoint> lstWPIE, string addOrChange)
        {
            var wirePtInfo = new WirePoint();
            IPoint pt = new Point();

            //找到导线点图层
            IMap map = new MapClass();
            map = DataEditCommon.g_pMap;
            string layerName = LayerNames.DEFALUT_WIRE_PT; //“默认_导线点”图层
            IFeatureLayer featureLayer = new FeatureLayerClass();
            featureLayer = LayerHelper.GetLayerByName(map, layerName); ///获得图层

            if (featureLayer == null)
            {
                MessageBox.Show("没有找到" + layerName + "图层，将不能绘制导线点。", "提示", MessageBoxButtons.OK);
                return;
            }

            var drawWirePt = new DrawTunnels();
            //修改导线点操作，要先删除原有导线点要素
            if (addOrChange == "CHANGE")
            {
                for (int i = 0; i < lstWPIE.Count; i++)
                {
                    wirePtInfo = lstWPIE[i];
                    DataEditCommon.DeleteFeatureByBId(featureLayer, wirePtInfo.BindingId);
                }
            }

            for (int i = 0; i < lstWPIE.Count; i++)
            {
                wirePtInfo = lstWPIE[i];
                pt.X = wirePtInfo.CoordinateX;
                pt.Y = wirePtInfo.CoordinateY;
                pt.Z = wirePtInfo.CoordinateZ;

                drawWirePt.CreatePoint(featureLayer, pt, wirePtInfo.BindingId); ///绘制点
            }
        }

        /// <summary>
        ///     根据导线点坐标绘制巷道
        /// </summary>
        /// <param name="lstWPIE"></param>
        private void DrawTunnel(List<WirePoint> lstWPIE, string addOrChange)
        {
            ///根据导线点计算巷道边线点
            WirePoint[] arrayWPt = { };
            arrayWPt = lstWPIE.ToArray();
            Vector3_DW[] verticesLeftBtmRet = null;
            Vector3_DW[] verticesRightBtmRet = null;

            var tunnelPtsCal = new TunnelPointsCalculation();
            bool isCalSuccess = tunnelPtsCal.CalcLeftAndRightVertics(arrayWPt, ref verticesLeftBtmRet,
                ref verticesRightBtmRet);

            if (!isCalSuccess)
            {
                MessageBox.Show("根据导线点计算巷道未成功！");
            }
            else
            {
                //找到巷道图层
                IMap map = new MapClass();
                map = DataEditCommon.g_pMap;
                string layerName = LayerNames.DEFALUT_TUNNEL; //“默认_巷道”图层
                IFeatureLayer featureLayer = new FeatureLayerClass();
                featureLayer = LayerHelper.GetLayerByName(map, layerName); //获得图层

                if (featureLayer == null)
                {
                    MessageBox.Show("没有找到" + layerName + "图层，将不能绘制巷道。", "提示", MessageBoxButtons.OK);
                    return;
                }

                var drawWirePt = new DrawTunnels();
                //修改导线点操作，要先删除依据原有导线点所生成的巷道要素
                if (addOrChange == "CHANGE")
                {
                    DataEditCommon.DeleteFeatureByBId(featureLayer, _tunnelID.ToString());
                }

                //绘制巷道左边线
                var lstLeftBtmRet = new List<IPoint>();
                lstLeftBtmRet = GetTunnelPts(verticesLeftBtmRet);

                if (lstLeftBtmRet == null) return;

                drawWirePt.CreateLine(featureLayer, lstLeftBtmRet, _tunnelID);

                //绘制巷道右边线
                var lstRightBtmRet = new List<IPoint>();
                lstRightBtmRet = GetTunnelPts(verticesRightBtmRet);
                drawWirePt.CreateLine(featureLayer, lstRightBtmRet, _tunnelID);
            }
        }

        /// <summary>
        ///     获得导线边线点坐标集
        /// </summary>
        /// <param name="verticesBtmRet">Vector3_DW数据</param>
        /// <returns>导线边线点坐标集List</returns>
        private List<IPoint> GetTunnelPts(Vector3_DW[] verticesBtmRet)
        {
            var lstBtmRet = new List<IPoint>();
            try
            {
                Vector3_DW vector3dw;
                IPoint pt;
                for (int i = 0; i < verticesBtmRet.Length; i++)
                {
                    vector3dw = new Vector3_DW();
                    vector3dw = verticesBtmRet[i];
                    pt = new PointClass();
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

        private void btnMultTxt_Click(object sender, EventArgs e)
        {

        }
    }
}