using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.Geodatabase;
using ESRI.ArcGIS.Geometry;
using GIS.Common;
using LibCommonControl;
using LibCommonForm;
using LibEntity;
using LibBusiness;
using LibCommon;
using LibBusiness.CommonBLL;
using ESRI.ArcGIS.Geometry;
using GIS.HdProc;
using ESRI.ArcGIS.Geodatabase;

namespace _3.GeologyMeasure
{
    public partial class TunnelHChuanEntering : BaseForm
    {
        /************变量定义*****************/
        TunnelHChuan tunnelHChuanEntity = new TunnelHChuan();
        TunnelHChuan tmpTunnelHChuanEntity = new TunnelHChuan();
        Tunnel tunnelEntity = new Tunnel();
        TunnelHChuanManagement frmStop;
        /*************************************/

        public TunnelHChuanEntering(MainFrm mainFrm, TunnelHChuanManagement frmhc)
        {
            this.MainForm = mainFrm;
            frmStop = frmhc;
            InitializeComponent();

            //窗体属性设置
            LibCommon.FormDefaultPropertiesSetter.SetEnteringFormDefaultProperties(this, Const_GM.TUNNEL_HCHUAN_ADD);

            //默认回采未完毕
            rbtnHChuanN.Checked = true;

            //默认工作制式选择
            if (WorkingTimeDefault.FindFirst().DefaultWorkTimeGroupId == Const_MS.WORK_GROUP_ID_38)
            {
                rbtn38.Checked = true;
            }
            else
            {
                rbtn46.Checked = true;
            }
            ////返回当前
            //cboWorkTime.Text = WorkTime.returnSysWorkTime(rbtn38.Checked ? Const_MS.WORK_TIME_38 : Const_MS.WORK_TIME_46);
            // 设置班次名称
            setWorkTimeName();
        }

        public TunnelHChuanEntering(TunnelHChuan tunnelHChuanEntity, MainFrm mainFrm, TunnelHChuanManagement frmhc)
        {
            this.MainForm = mainFrm;
            frmStop = frmhc;
            this.tunnelHChuanEntity = tunnelHChuanEntity;
            this.tmpTunnelHChuanEntity = tunnelHChuanEntity;

            InitializeComponent();

            LibCommon.FormDefaultPropertiesSetter.SetEnteringFormDefaultProperties(this, Const_GM.TUNNEL_HCHUAN_CHANGE);
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

        private void btnSubmit_Click(object sender, EventArgs e)
        {
            if (!check())
            {
                return;
            }
            bindTunnelHChuanEntity();

            bool bResult = false;

            int id;

            //检查导线点I与导线点II对应文本框中的数据是否有效

            //构造横穿点串
            List<IPoint> pntcols = new List<IPoint>();
            IPoint pnt0 = new PointClass();
            pnt0.X = Convert.ToDouble(this.textBox_X1.Text);
            pnt0.Y = Convert.ToDouble(this.textBox_Y1.Text);
            pnt0.Z = Convert.ToDouble(this.textBox_Z1.Text);
            pntcols.Add(pnt0);

            IPoint pnt1 = new PointClass();
            pnt1.X = Convert.ToDouble(this.textBox_X2.Text);
            pnt1.Y = Convert.ToDouble(this.textBox_Y2.Text);
            pnt1.Z = Convert.ToDouble(this.textBox_Z2.Text);
            pntcols.Add(pnt1);
            double hcwid = Convert.ToDouble(txtWidth.Text);
            //添加
            if (this.Text == Const_GM.TUNNEL_HCHUAN_ADD)
            {
                id = TunnelHChuanBLL.insertTunnelHChuan(tunnelHChuanEntity);
                bResult = id > -1;
                //添加横穿
                if (id > 0)
                {
                    frmStop.refreshAdd();
                    tunnelHChuanEntity.Id = id;
                    AddHChuanjc(tunnelHChuanEntity, pntcols, hcwid);
                    //bResult = DrawHengChuanPolygon(tunnelHChuanEntity);
                }
            }
            //修改
            if (this.Text == Const_GM.TUNNEL_HCHUAN_CHANGE)
            {
                if (tmpTunnelHChuanEntity.TunnelId1 != 0)
                {
                    tunnelHChuanEntity.TunnelId1 = tmpTunnelHChuanEntity.TunnelId1;
                }
                if (tmpTunnelHChuanEntity.TunnelId2 != 0)
                {
                    tunnelHChuanEntity.TunnelId2 = tmpTunnelHChuanEntity.TunnelId2;
                }

                bResult = TunnelHChuanBLL.updateTunnelHChuan(tunnelHChuanEntity);
                //修改回采进尺图上显示信息，更新工作面信息表
                if (bResult)
                {
                    frmStop.refreshUpdate();

                    DataEditCommon.DeleteFeatureByWhereClause(Global.hdfdfulllyr, "bid='" + tunnelHChuanEntity.Id + "'");
                    DataEditCommon.DeleteFeatureByWhereClause(Global.hdfdlyr, "bid='" + tunnelHChuanEntity.Id + "'");
                    AddHChuanjc(tunnelHChuanEntity, pntcols, hcwid);
                    //bResult = RedrawHengChuanPolygon(tunnelHChuanEntity);
                }
            }
            if (bResult)
            {
                //TODO:成功后事件
                DataEditCommon.g_pMyMapCtrl.ActiveView.PartialRefresh(esriViewDrawPhase.esriViewBackground, null, null);
                Close();
            }
            //暂时保留，是否设置为横川类型。
            //TunnelInfoBLL.setTunnelAsHChuan(tunnelHChuanEntity);
            return;
        }

        private bool DrawHengChuanPolygon(TunnelHChuan entity)
        {
            //找到导线点图层
            IMap map = new MapClass();
            map = DataEditCommon.g_pMap;
            string layerName = GIS.LayerNames.DEFALUT_HENGCHUAN;//“默认_导线点”图层
            IFeatureLayer featureLayer = new FeatureLayerClass();
            featureLayer = GIS.Common.LayerHelper.GetLayerByName(map, layerName);//获得图层

            if (featureLayer == null)
            {
                MessageBox.Show("没有找到" + layerName + "图层，将不能绘制导线点。", "提示", MessageBoxButtons.OK);
                return false;
            }

            var point1 = new PointClass { X = entity.X1, Y = entity.Y1, Z = entity.Z1, ZAware = true };
            var point2 = new PointClass { X = entity.X2, Y = entity.Y2, Z = entity.Z2, ZAware = true };
            var line = new LineClass { FromPoint = point1, ToPoint = point2 };
            var polygon = new PolygonClass { ZAware = true };
            var width = entity.Width / 2;
            polygon.AddPoint(ConstructPoint(point2, line.Angle / Deg2Rad + 90, width));
            polygon.AddPoint(ConstructPoint(point2, line.Angle / Deg2Rad - 90, width));
            polygon.AddPoint(ConstructPoint(point1, line.Angle / Deg2Rad + 180 + 90, width));
            polygon.AddPoint(ConstructPoint(point1, line.Angle / Deg2Rad + 180 - 90, width));
            polygon.SimplifyPreserveFromTo();

            var list = new List<ziduan>
            {
                new ziduan("BID",entity.Id.ToString()),
                new ziduan("BID0",entity.TunnelId1.ToString()),
                new ziduan("BID1",entity.TunnelId2.ToString()),
                new ziduan("NAME",entity.NameHChuan)
            };

            var feature = DataEditCommon.CreateNewFeature(featureLayer, polygon, list);

            return feature != null;
        }
        private bool RedrawHengChuanPolygon(TunnelHChuan entity)
        {
            //找到导线点图层
            IMap map = new MapClass();
            map = DataEditCommon.g_pMap;
            string layerName = GIS.LayerNames.DEFALUT_HENGCHUAN;//“默认_导线点”图层
            IFeatureLayer featureLayer = new FeatureLayerClass();
            featureLayer = GIS.Common.LayerHelper.GetLayerByName(map, layerName);//获得图层

            if (featureLayer == null)
            {
                MessageBox.Show("没有找到" + layerName + "图层，将不能绘制导线点。", "提示", MessageBoxButtons.OK);
                return false;
            }

            var point1 = new PointClass { X = entity.X1, Y = entity.Y1, Z = entity.Z1, ZAware = true };
            var point2 = new PointClass { X = entity.X2, Y = entity.Y2, Z = entity.Z2, ZAware = true };
            var line = new LineClass { FromPoint = point1, ToPoint = point2 };
            var polygon = new PolygonClass { ZAware = true };
            var width = entity.Width / 2;
            polygon.AddPoint(ConstructPoint(point2, line.Angle / Deg2Rad + 90, width));
            polygon.AddPoint(ConstructPoint(point2, line.Angle / Deg2Rad - 90, width));
            polygon.AddPoint(ConstructPoint(point1, line.Angle / Deg2Rad + 180 + 90, width));
            polygon.AddPoint(ConstructPoint(point1, line.Angle / Deg2Rad + 180 - 90, width));
            polygon.SimplifyPreserveFromTo();

            var list = new List<ziduan>
            {
                new ziduan("BID",entity.Id.ToString()),
                new ziduan("BID0",entity.TunnelId1.ToString()),
                new ziduan("BID1",entity.TunnelId2.ToString()),
                new ziduan("NAME",entity.NameHChuan)
            };

            var feature = DataEditCommon.ModifyFeature(featureLayer, entity.Id, polygon, list);
            return feature != null;
        }

        private const double Deg2Rad = Math.PI / 180.0;
        private IPoint ConstructPoint(IPoint point, double angle, double width)
        {
            var point3 = new PointClass { ZAware = true };
            point3.ConstructAngleDistance(point, angle * Deg2Rad, width);
            return point3;
        }

        /// <summary>
        /// 添加横穿进尺
        /// </summary>
        private void AddHChuanjc(TunnelHChuan tunnel, List<IPoint> pntcols, double wid)
        {
            if (this.textBox_Name.Text == "")
            {
                MessageBox.Show("请输入横穿名称！", "系统提示");
                return;
            }

            Dictionary<string, string> dics = new Dictionary<string, string>();
            dics[GIS.GIS_Const.FIELD_ID] = "0";
            dics[GIS.GIS_Const.FIELD_BID] = tunnel.Id.ToString();
            dics[GIS.GIS_Const.FIELD_BS] = "2";
            dics[GIS.GIS_Const.FIELD_HDID] = tunnel.TunnelId1.ToString() + "_" + tunnel.TunnelId2.ToString();
            dics[GIS.GIS_Const.FIELD_XH] = "-1";
            dics[GIS.GIS_Const.FIELD_NAME] = this.textBox_Name.Text;

            //绘制横穿
            List<IPoint> rightpts = Global.cons.GetLRParallelPnts(pntcols, wid, 1);//右侧平行线上的端点串
            List<IPoint> leftpts = Global.cons.GetLRParallelPnts(pntcols, wid, 0);//左侧平行线上的端点串
            List<IPoint> results = Global.cons.ConstructPnts(rightpts, leftpts);
            Global.cons.AddHangdaoToLayer(results, dics, Global.hdfdfulllyr);
            Global.cons.AddHangdaoToLayer(results, dics, Global.hdfdlyr);
        }

        private void bindTunnelHChuanEntity()
        {
            if (textBox_Name.Text.Trim() != "")
            {
                tunnelHChuanEntity.NameHChuan = textBox_Name.Text;
            }
            if (textBox_X1.Text.Trim() != "")
            {
                tunnelHChuanEntity.X1 = Convert.ToDouble(textBox_X1.Text);
            }
            if (textBox_Y1.Text.Trim() != "")
            {
                tunnelHChuanEntity.Y1 = Convert.ToDouble(textBox_Y1.Text);
            }
            if (textBox_Z1.Text.Trim() != "")
            {
                tunnelHChuanEntity.Z1 = Convert.ToDouble(textBox_Z1.Text);
            }
            if (textBox_X2.Text.Trim() != "")
            {
                tunnelHChuanEntity.X2 = Convert.ToDouble(textBox_X2.Text);
            }
            if (textBox_Y2.Text.Trim() != "")
            {
                tunnelHChuanEntity.Y2 = Convert.ToDouble(textBox_Y2.Text);
            }
            if (textBox_Z2.Text.Trim() != "")
            {
                tunnelHChuanEntity.Z2 = Convert.ToDouble(textBox_Z2.Text);
            }
            if (textBox_Azimuth.Text.Trim() != "")
            {
                tunnelHChuanEntity.Azimuth = Convert.ToDouble(textBox_Azimuth.Text);
            }
            //队别
            tunnelHChuanEntity.Team.TeamId = Convert.ToInt32(cboTeamName.SelectedValue);
            //开工日期
            tunnelHChuanEntity.StartDate = dtpStartDate.Value;
            //是否停工
            if (rbtnHChuanY.Checked)
            {
                tunnelHChuanEntity.IsFinish = 1;
            }
            if (rbtnHChuanN.Checked)
            {
                tunnelHChuanEntity.IsFinish = 0;
            }
            //停工日期
            if (rbtnHChuanY.Checked == true)
            {
                tunnelHChuanEntity.StopDate = dtpStopDate.Value;
            }
            //工作制式
            if (rbtn38.Checked)
            {
                tunnelHChuanEntity.WorkStyle = rbtn38.Text;
            }
            if (rbtn46.Checked)
            {
                tunnelHChuanEntity.WorkStyle = rbtn46.Text;
            }

            if (!string.IsNullOrWhiteSpace(txtWidth.Text))
            {
                tunnelHChuanEntity.Width = Convert.ToDouble(txtWidth.Text);
            }

            //班次
            tunnelHChuanEntity.WorkTime = cboWorkTime.Text;

            //状态
            tunnelHChuanEntity.State = cmbTunnelState.Text;
        }
        /// <summary>
        /// 验证
        /// </summary>
        /// <returns>是否验证通过</returns>
        private bool check()
        {
            //巷道选择
            if (tunnelHChuanEntity.TunnelId1 == 0 && tunnelHChuanEntity.TunnelId2 == 0)
            {
                Alert.alert(Const.MSG_PLEASE_CHOOSE + Const_GM.TUNNEL + Const.SIGN_EXCLAMATION_MARK);
                return false;
            }
            //关联巷道1选择
            if (tunnelHChuanEntity.TunnelId1 == 0)
            {
                Alert.alert(Const.MSG_PLEASE_CHOOSE + Const_GM.MAIN_TUNNEL + Const.SIGN_EXCLAMATION_MARK);
                return false;
            }
            //关联巷道2选择
            if (tunnelHChuanEntity.TunnelId2 == 0)
            {
                Alert.alert(Const.MSG_PLEASE_CHOOSE + Const_GM.SECOND_TUNNEL + Const.SIGN_EXCLAMATION_MARK);
                return false;
            }

            //巷道重复选择验证
            if (tunnelHChuanEntity.TunnelId1 == tunnelHChuanEntity.TunnelId2)
            {
                Alert.alert(Const_GM.TUNNEL_HC_MSG_TUNNEL_DOUBLE_CHOOSE);
                return false;
            }
            //是否为掘进巷道验证
            //关联巷道1
            if (this.Text == Const_GM.TUNNEL_HCHUAN_ADD)
            {
                tunnelEntity.TunnelId = tunnelHChuanEntity.TunnelId1;
            }
            if (this.Text == Const_GM.TUNNEL_HCHUAN_CHANGE)
            {
                tunnelEntity.TunnelId = tmpTunnelHChuanEntity.TunnelId1;
            }
            if (tmpTunnelHChuanEntity.TunnelId1 != tunnelHChuanEntity.TunnelId1)
            {
                //检查巷道是否为掘进巷道
                if (tunnelEntity.WorkingFace.WorkingfaceTypeEnum == WorkingfaceTypeEnum.JJ)
                {
                    Alert.alert(btnTunnelChoose1.Text + Const_GM.TUNNEL_HC_MSG_TUNNEL_IS_JJ);
                    return false;
                }
                //检查巷道是否为回采巷道
                if (tunnelEntity.WorkingFace.WorkingfaceTypeEnum == WorkingfaceTypeEnum.HC)
                {
                    Alert.alert(btnTunnelChoose1.Text + Const_GM.TUNNEL_HC_MSG_TUNNEL_IS_HC);
                    return false;
                }
            }
            //关联巷道2
            if (this.Text == Const_GM.TUNNEL_HCHUAN_ADD)
            {
                tunnelEntity.TunnelId = tunnelHChuanEntity.TunnelId2;
            }
            if (this.Text == Const_GM.TUNNEL_HCHUAN_CHANGE)
            {
                tunnelEntity.TunnelId = tmpTunnelHChuanEntity.TunnelId2;
            }
            if (tmpTunnelHChuanEntity.TunnelId2 != tunnelHChuanEntity.TunnelId2)
            {
                //检查巷道是否为掘进巷道
                if (tunnelEntity.WorkingFace.WorkingfaceTypeEnum == WorkingfaceTypeEnum.JJ)
                {
                    Alert.alert(btnTunnelChoose2.Text + Const_GM.TUNNEL_HC_MSG_TUNNEL_IS_JJ);
                    return false;
                }
                //检查巷道是否为回采巷道
                if (tunnelEntity.WorkingFace.WorkingfaceTypeEnum == WorkingfaceTypeEnum.HC)
                {
                    Alert.alert(btnTunnelChoose2.Text + Const_GM.TUNNEL_HC_MSG_TUNNEL_IS_HC);
                    return false;
                }
            }
            if (textBox_X1.Text.Trim() == "")
            {
                textBox_X1.BackColor = Const.ERROR_FIELD_COLOR;
                return false;
            }
            else
            {
                if (!Validator.IsNumeric(textBox_X1.Text))
                {
                    textBox_X1.BackColor = Const.ERROR_FIELD_COLOR;
                    return false;
                }
                else
                {
                    textBox_X1.BackColor = Const.NO_ERROR_FIELD_COLOR;
                }
            }
            if (textBox_Y1.Text.Trim() == "")
            {
                textBox_Y1.BackColor = Const.ERROR_FIELD_COLOR;
                return false;
            }
            else
            {
                if (!Validator.IsNumeric(textBox_Y1.Text))
                {
                    textBox_Y1.BackColor = Const.ERROR_FIELD_COLOR;
                    return false;
                }
                else
                {
                    textBox_Y1.BackColor = Const.NO_ERROR_FIELD_COLOR;
                }
            }
            if (textBox_Z1.Text.Trim() == "")
            {

            }
            else
            {
                if (!Validator.IsNumeric(textBox_Z1.Text))
                {
                    textBox_Z1.BackColor = Const.ERROR_FIELD_COLOR;
                    return false;
                }
                else
                {
                    textBox_Z1.BackColor = Const.NO_ERROR_FIELD_COLOR;
                }
            }
            if (textBox_X2.Text.Trim() == "")
            {

            }
            else
            {
                if (!Validator.IsNumeric(textBox_X2.Text))
                {
                    textBox_X2.BackColor = Const.ERROR_FIELD_COLOR;
                    return false;
                }
                else
                {
                    textBox_X2.BackColor = Const.NO_ERROR_FIELD_COLOR;
                }
            }
            if (textBox_Y2.Text.Trim() == "")
            {

            }
            else
            {
                if (!Validator.IsNumeric(textBox_Y2.Text))
                {
                    textBox_Y2.BackColor = Const.ERROR_FIELD_COLOR;
                    return false;
                }
                else
                {
                    textBox_Y2.BackColor = Const.NO_ERROR_FIELD_COLOR;
                }
            }
            if (textBox_Z2.Text.Trim() == "")
            {

            }
            else
            {
                if (!Validator.IsNumeric(textBox_Z2.Text))
                {
                    textBox_Z2.BackColor = Const.ERROR_FIELD_COLOR;
                    return false;
                }
                else
                {
                    textBox_Z2.BackColor = Const.NO_ERROR_FIELD_COLOR;
                }
            }
            if (textBox_Azimuth.Text.Trim() == "")
            {

            }
            else
            {
                if (!Validator.IsNumeric(textBox_Azimuth.Text))
                {
                    textBox_Azimuth.BackColor = Const.ERROR_FIELD_COLOR;
                    return false;
                }
                else
                {
                    textBox_Azimuth.BackColor = Const.NO_ERROR_FIELD_COLOR;
                }
            }
            if (txtWidth.Text.Trim() == "")
            {
                txtWidth.BackColor = Const.ERROR_FIELD_COLOR;
                return false;
            }
            else
            {
                if (!Validator.IsNumeric(txtWidth.Text))
                {
                    txtWidth.BackColor = Const.ERROR_FIELD_COLOR;
                    return false;
                }
                else
                {
                    txtWidth.BackColor = Const.NO_ERROR_FIELD_COLOR;
                }
            }
            //队别为空
            if (!Check.isEmpty(cboTeamName, Const_MS.TEAM_NAME))
            {
                cboTeamName.BackColor = Const.ERROR_FIELD_COLOR;
                return false;
            }
            else
            {
                cboTeamName.BackColor = Const.NO_ERROR_FIELD_COLOR;
            }
            //状态为空
            if (!Check.isEmpty(cmbTunnelState, ""))
            {
                cmbTunnelState.BackColor = Const.ERROR_FIELD_COLOR;
                return false;
            }
            else
            {
                cmbTunnelState.BackColor = Const.NO_ERROR_FIELD_COLOR;
            }

            //验证成功
            return true;
        }

        private void btnTunnelChoose2_Click(object sender, EventArgs e)
        {
            //巷道选择窗体
            SelectTunnelDlg tunnelChoose;
            //第一次选择巷道时给巷道实体赋值，用于下条巷道选择时的控件选择定位
            if (tunnelHChuanEntity.TunnelId2 != 0)
            {
                tunnelEntity.TunnelId = tunnelHChuanEntity.TunnelId2;
                //tunnelEntity = TunnelInfoBLL.selectTunnelInfoByTunnelID(tunnelEntity.Tunnel);
            }
            //第一次选择巷道
            if (tunnelEntity.TunnelId == 0)
            {
                tunnelChoose = new SelectTunnelDlg(this.MainForm);
            }
            //非第一次选择巷道
            else
            {
                tunnelChoose = new SelectTunnelDlg(tunnelEntity, this.MainForm);
            }
            //巷道选择完毕
            if (DialogResult.OK == tunnelChoose.ShowDialog())
            {
                //巷道选择按钮Text改变
                btnTunnelChoose2.Text = tunnelChoose.tunnelName;
                //实体赋值
                if (this.Text == Const_GM.TUNNEL_HCHUAN_CHANGE)
                {
                    tmpTunnelHChuanEntity.TunnelId2 = tunnelChoose.tunnelId;
                }
                if (this.Text == Const_GM.TUNNEL_HCHUAN_ADD)
                {
                    tunnelHChuanEntity.TunnelId2 = tunnelChoose.tunnelId;
                }
                //巷道实体赋值，用于下次巷道选择
                tunnelEntity = BasicInfoManager.getInstance().getTunnelByID(tunnelChoose.tunnelId);
            }
        }
        /// <summary>
        /// 工作制式选择
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void rbtn38_CheckedChanged(object sender, EventArgs e)
        {
            DataBindUtil.LoadWorkTime(cboWorkTime,
                rbtn38.Checked ? Const_MS.WORK_GROUP_ID_38 : Const_MS.WORK_GROUP_ID_46);
            // 设置班次名称
            setWorkTimeName();
        }

        private void setWorkTimeName()
        {
            string strWorkTimeName = "";
            string sysDateTime = DateTime.Now.ToString("HH:mm:ss");
            if (this.rbtn38.Checked == true)
            {
                strWorkTimeName = MineDataSimpleBLL.selectWorkTimeNameByWorkTimeGroupIdAndSysTime(1, sysDateTime);
            }
            else
            {
                strWorkTimeName = MineDataSimpleBLL.selectWorkTimeNameByWorkTimeGroupIdAndSysTime(2, sysDateTime);
            }

            if (strWorkTimeName != null && strWorkTimeName != "")
            {
                cboWorkTime.Text = strWorkTimeName;
            }
        }

        private void rbtn46_CheckedChanged(object sender, EventArgs e)
        {
            DataBindUtil.LoadWorkTime(cboWorkTime,
                rbtn38.Checked ? Const_MS.WORK_GROUP_ID_38 : Const_MS.WORK_GROUP_ID_46);
            // 设置班次名称
            setWorkTimeName();
        }

        private void TunnelHChuanEntering_Load(object sender, EventArgs e)
        {
            //绑定队别名称
            bindTeamInfo();

            if (this.Text == Const_GM.TUNNEL_HCHUAN_CHANGE)
            {
                bindInfo();
            }
            if (rbtnHChuanN.Checked)
            {
                dtpStopDate.Enabled = false;
            }

            cboTeamName.DropDownStyle = ComboBoxStyle.DropDownList;

            cboWorkTime.DropDownStyle = ComboBoxStyle.DropDownList;
        }

        private void bindInfo()
        {
            //关联巷道1
            Tunnel tunnel = Tunnel.Find(tunnelHChuanEntity.TunnelId1);
            if (tunnel != null)
                btnTunnelChoose1.Text = tunnel.TunnelName;
            //关联巷道2
            tunnel = Tunnel.Find(tunnelHChuanEntity.TunnelId2);
            if (tunnel != null)
                btnTunnelChoose2.Text = tunnel.TunnelName;
            textBox_Name.Text = tunnelHChuanEntity.NameHChuan;
            txtWidth.Text = tunnelHChuanEntity.Width.ToString();
            textBox_X1.Text = Convert.ToString(tunnelHChuanEntity.X1);
            textBox_Y1.Text = Convert.ToString(tunnelHChuanEntity.Y1);
            textBox_Z1.Text = Convert.ToString(tunnelHChuanEntity.Z1);
            textBox_X2.Text = Convert.ToString(tunnelHChuanEntity.X2);
            textBox_Y2.Text = Convert.ToString(tunnelHChuanEntity.Y2);
            textBox_Z2.Text = Convert.ToString(tunnelHChuanEntity.Z2);
            textBox_Azimuth.Text = Convert.ToString(tunnelHChuanEntity.Azimuth);

            tunnelEntity.TunnelId = tunnelHChuanEntity.TunnelId1;
            //队别名称
            cboTeamName.Text = tunnelHChuanEntity.Team.TeamName;

            //开始日期
            dtpStartDate.Value = tunnelHChuanEntity.StartDate;
            //是否回采完毕
            if (tunnelHChuanEntity.IsFinish == 1)
            {
                rbtnHChuanY.Checked = true;
            }
            else
            {
                rbtnHChuanN.Checked = true;
            }
            //停工日期
            if (tunnelHChuanEntity.IsFinish == 1)
            {
                dtpStopDate.Value = tunnelHChuanEntity.StopDate;
            }
            //工作制式
            if (tunnelHChuanEntity.WorkStyle == rbtn38.Text)
            {
                rbtn38.Checked = true;
            }
            else
            {
                rbtn46.Checked = true;
            }
            //班次
            cboWorkTime.Text = tunnelHChuanEntity.WorkTime;
            //状态
            cmbTunnelState.Text = tunnelHChuanEntity.State;
        }

        private void bindTeamInfo()
        {
            cboTeamName.Items.Clear();
            Team[] team = Team.FindAll();
            foreach (Team t in team)
            {
                cboTeamName.Items.Add(t.TeamName);
            }
        }

        private void rbtnHChuanY_CheckedChanged(object sender, EventArgs e)
        {
            if (rbtnHChuanY.Checked)
            {
                dtpStopDate.Enabled = true;
            }
            else
            {
                dtpStopDate.Enabled = false;
            }
        }

        private void btnTunnelChoose1_Click(object sender, EventArgs e)
        {
            //巷道选择窗体
            SelectTunnelDlg tunnelChoose;
            //第一次选择巷道时给巷道实体赋值，用于下条巷道选择时的控件选择定位
            if (tunnelHChuanEntity.TunnelId1 != 0)
            {
                tunnelEntity.TunnelId = tunnelHChuanEntity.TunnelId1;
                //tunnelEntity = TunnelInfoBLL.selectTunnelInfoByTunnelID(tunnelEntity.Tunnel);
            }
            //第一次选择巷道
            if (tunnelEntity.TunnelId == 0)
            {
                tunnelChoose = new SelectTunnelDlg(this.MainForm);
            }
            //非第一次选择巷道
            else
            {
                tunnelChoose = new SelectTunnelDlg(tunnelEntity, this.MainForm);
            }
            //巷道选择完毕
            if (DialogResult.OK == tunnelChoose.ShowDialog())
            {
                //巷道选择按钮Text改变
                btnTunnelChoose1.Text = tunnelChoose.tunnelName;
                //实体赋值
                if (this.Text == Const_GM.TUNNEL_HCHUAN_CHANGE)
                {
                    tmpTunnelHChuanEntity.TunnelId1 = tunnelChoose.tunnelId;
                }
                if (this.Text == Const_GM.TUNNEL_HCHUAN_ADD)
                {
                    tunnelHChuanEntity.TunnelId1 = tunnelChoose.tunnelId;
                }
                //巷道实体赋值，用于下次巷道选择
                tunnelEntity = BasicInfoManager.getInstance().getTunnelByID(tunnelChoose.tunnelId);
            }
        }

        private void btnQD_Click(object sender, EventArgs e)
        {
            GIS.Common.DataEditCommon.PickUpPoint(textBox_X1, textBox_Y1);
        }

        private void btnZD_Click(object sender, EventArgs e)
        {
            GIS.Common.DataEditCommon.PickUpPoint(textBox_X2, textBox_Y2);
        }
    }
}
