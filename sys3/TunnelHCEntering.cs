using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Windows.Forms;
using ESRI.ArcGIS.Geometry;
using GIS;
using GIS.HdProc;
using LibBusiness;
using LibBusiness.CommonBLL;
using LibCommon;
using LibCommonControl;
using LibCommonForm;
using LibEntity;

namespace _3.GeologyMeasure
{
    /// <summary>
    ///     回采面
    /// </summary>
    public partial class TunnelHCEntering : BaseForm
    {
        #region ******变量声明******;

        // 主运
        // 辅运
        private readonly int[] intArr = new int[5];
        private readonly List<Tunnel> otherTunnelList = new List<Tunnel>();
        private readonly HashSet<Tunnel> tunnelSet = new HashSet<Tunnel>();
        private Tunnel tunnelFY;
        // 切眼
        private Tunnel tunnelQY;
        private Tunnel tunnelZY;

        private WorkingFace workingFace;

        #endregion ******变量声明******

        public TunnelHCEntering(MainFrm mainFrm)
        {
            MainForm = mainFrm;

            InitializeComponent();

            //窗体属性设置
            FormDefaultPropertiesSetter.SetEnteringFormDefaultProperties(this, Const_GM.TUNNEL_HC_ADD);

            //默认回采未完毕
            rbtnHCN.Checked = true;

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


        /// <summary>
        ///     构造方法
        /// </summary>
        /// <param name="tunnelHCEntity">回采面实体</param>
        public TunnelHCEntering(WorkingFace tunnelHCEntity, MainFrm mainFrm)
        {
            MainForm = mainFrm;
            workingFace = ObjectCopier.Clone(tunnelHCEntity);
            Text = Const_GM.TUNNEL_HC_CHANGE;
            InitializeComponent();

            FormDefaultPropertiesSetter.SetEnteringFormDefaultProperties(this, Const_GM.TUNNEL_HC_CHANGE);
        }

        private void Form_TunnelHCEntering_Load(object sender, EventArgs e)
        {
            //绑定队别名称
            bindTeamInfo();

            if (Text == Const_GM.TUNNEL_HC_CHANGE)
            {
                bindInfo();
            }
            if (rbtnHCN.Checked)
            {
                dtpStopDate.Enabled = false;
            }

            cboTeamName.DropDownStyle = ComboBoxStyle.DropDownList;

            cboWorkTime.DropDownStyle = ComboBoxStyle.DropDownList;

            btnChooseOther.Enabled = false;
        }

        /// <summary>
        ///     设置班次名称
        /// </summary>
        private void setWorkTimeName()
        {
            string strWorkTimeName = "";
            string sysDateTime = DateTime.Now.ToString("HH:mm:ss");
            if (rbtn38.Checked)
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

        /// <summary>
        ///     绑定修改信息
        /// </summary>
        private void bindInfo()
        {
            workingFace.TunnelSet =
                BasicInfoManager.getInstance()
                    .getTunnelSetByDataSet(TunnelInfoBLL.selectTunnelByWorkingFaceId(workingFace.WorkingFaceId));
            intArr[0] = workingFace.MiningArea.Horizontal.Mine.MineId;
            intArr[1] = workingFace.MiningArea.Horizontal.HorizontalId;
            intArr[2] = workingFace.MiningArea.MiningAreaId;
            intArr[3] = workingFace.WorkingFaceId;

            string otherTunnel = "";
            foreach (Tunnel tunnel in workingFace.TunnelSet)
            {
                if (tunnel.TunnelType == TunnelTypeEnum.STOPING_ZY)
                    tunnelZY = tunnel; //主运顺槽
                else if (tunnel.TunnelType == TunnelTypeEnum.STOPING_FY)
                    tunnelFY = tunnel; //辅运顺槽
                else if (tunnel.TunnelType == TunnelTypeEnum.STOPING_QY)
                    tunnelQY = tunnel; //开切眼
                else
                {
                    if (tunnel.TunnelType == TunnelTypeEnum.STOPING_OTHER)
                    {
                        otherTunnelList.Add(tunnel);
                        otherTunnel += " " + tunnel.TunnelName; //其他关联巷道
                    }
                }
            }

            btnChooseWF.Text = workingFace.WorkingFaceName;

            //主运顺槽
            btnChooseZY.Text = tunnelZY != null ? tunnelZY.TunnelName : "";
            //辅运顺槽
            btnChooseFY.Text = tunnelFY != null ? tunnelFY.TunnelName : "";
            //开切眼
            btnChooseQY.Text = tunnelQY != null ? tunnelQY.TunnelName : "";


            foreach (Tunnel i in otherTunnelList)
            {
                listBox_Browse.Items.Add(new TunnelSimple(i.TunnelId, i.TunnelName));
            }
            listBox_Browse.DisplayMember = "Name";
            listBox_Browse.ValueMember = "WirePointName";
            //其它巷道
            //string[] sArray = new string[10];
            //sArray = SplitString(tunnelHCEntity.Tunnel);
            //foreach (string i in sArray)
            //{
            //    if ((i != "") && (i != null))
            //    {
            //        int iTunnelID = Convert.ToInt16(i);
            //        listBox_Browse.Items.Add(TunnelInfoBLL.selectTunnelInfoByTunnelID(iTunnelID).TunnelName);
            //    }
            //}

            //队别名称
            cboTeamName.Text = BasicInfoManager.getInstance().getTeamNameById(workingFace.TeamNameId);

            //开始日期
            dtpStartDate.Value = DateTimeUtil.validateDTPDateTime((DateTime)workingFace.StartDate);

            //是否回采完毕
            if (workingFace.IsFinish == 1)
            {
                rbtnHCY.Checked = true;
            }
            else
            {
                rbtnHCN.Checked = true;
            }
            //停工日期
            if (workingFace.IsFinish == 1)
            {
                dtpStopDate.Value = (DateTime)workingFace.StopDate;
            }
            //工作制式
            if (workingFace.WorkStyle == rbtn38.Text)
            {
                rbtn38.Checked = true;
            }
            else
            {
                rbtn46.Checked = true;
            }
            //班次
            cboWorkTime.Text = workingFace.WorkTime;
        }

        private string[] SplitString(string p)
        {
            var sArray = new string[10];

            if ((p != null) && (p != ""))
            {
                sArray = p.Split(',');
            }

            return sArray;
        }

        /// <summary>
        ///     选择工作面
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnChooseWF_Click(object sender, EventArgs e)
        {
            //巷道选择窗体
            SelectWorkingFaceDlg wfChoose = null;

            wfChoose = new SelectWorkingFaceDlg(WorkingfaceTypeEnum.HC, WorkingfaceTypeEnum.OTHER);
            //第一次选择巷道
            //if (workingFace == null)
            //{
            //    wfChoose = new SelectWorkingFaceDlg(WorkingfaceTypeEnum.HC);
            //}
            ////非第一次选择巷道
            //else
            //{
            //    wfChoose = new SelectWorkingFaceDlg();
            //}

            //巷道选择完毕
            if (DialogResult.OK == wfChoose.ShowDialog())
            {
                //巷道选择按钮Text改变
                btnChooseWF.Text = wfChoose.workFaceName;
                //实体赋值
                workingFace = BasicInfoManager.getInstance().getWorkingFaceById(wfChoose.workFaceId);
                intArr[0] = workingFace.MiningArea.Horizontal.Mine.MineId;
                intArr[1] = workingFace.MiningArea.Horizontal.HorizontalId;
                intArr[2] = workingFace.MiningArea.MiningAreaId;
                intArr[3] = workingFace.WorkingFaceId;
            }
        }

        /// <summary>
        ///     主运顺槽按钮
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnChooseZY_Click(object sender, EventArgs e)
        {
            if (workingFace == null)
            {
                Alert.alert("请先选择工作面");
                return;
            }

            var tunnelChoose = new SelectTunnelDlg(intArr, MainForm, 2);

            //巷道选择完毕
            if (DialogResult.OK == tunnelChoose.ShowDialog())
            {
                if (tunnelZY != null)
                {
                    Tunnel ent = BasicInfoManager.getInstance().getTunnelByID(tunnelZY.TunnelId);
                    ent.TunnelType = TunnelTypeEnum.OTHER;
                    tunnelSet.Add(ent);
                }

                //巷道选择按钮Text改变
                btnChooseZY.Text = tunnelChoose.tunnelName;
                //实体赋值
                tunnelZY = BasicInfoManager.getInstance().getTunnelByID(tunnelChoose.tunnelId);
                if (tunnelZY != null)
                {
                    tunnelZY.TunnelType = TunnelTypeEnum.STOPING_ZY;
                    tunnelSet.Add(tunnelZY);
                }
            }
        }

        /// <summary>
        ///     辅运顺槽
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnChooseFY_Click(object sender, EventArgs e)
        {
            if (workingFace == null)
            {
                Alert.alert("请先选择工作面");
                return;
            }

            //巷道选择窗体
            var tunnelChoose = new SelectTunnelDlg(intArr, MainForm, 2);

            //巷道选择完毕
            if (DialogResult.OK == tunnelChoose.ShowDialog())
            {
                if (tunnelFY != null)
                {
                    Tunnel ent = BasicInfoManager.getInstance().getTunnelByID(tunnelFY.TunnelId);
                    ent.TunnelType = TunnelTypeEnum.OTHER;
                    tunnelSet.Add(ent);
                }

                //巷道选择按钮Text改变
                btnChooseFY.Text = tunnelChoose.tunnelName;
                //实体赋值

                tunnelFY = BasicInfoManager.getInstance().getTunnelByID(tunnelChoose.tunnelId);
                if (tunnelFY != null)
                {
                    tunnelFY.TunnelType = TunnelTypeEnum.STOPING_FY;
                    tunnelSet.Add(tunnelFY);
                }
            }
        }

        /// <summary>
        ///     开切眼
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnChooseQY_Click(object sender, EventArgs e)
        {
            if (workingFace == null)
            {
                Alert.alert("请先选择工作面");
                return;
            }

            //巷道选择窗体
            var tunnelChoose = new SelectTunnelDlg(intArr, MainForm, 2);

            //巷道选择完毕
            if (DialogResult.OK == tunnelChoose.ShowDialog())
            {
                if (tunnelQY != null)
                {
                    Tunnel ent = BasicInfoManager.getInstance().getTunnelByID(tunnelQY.TunnelId);
                    ent.TunnelType = TunnelTypeEnum.OTHER;
                    tunnelSet.Add(ent);
                }

                //巷道选择按钮Text改变
                btnChooseQY.Text = tunnelChoose.tunnelName;
                //实体赋值
                tunnelQY = BasicInfoManager.getInstance().getTunnelByID(tunnelChoose.tunnelId);
                if (tunnelQY != null)
                {
                    tunnelQY.TunnelType = TunnelTypeEnum.STOPING_QY;
                    tunnelSet.Add(tunnelQY);
                }
            }
        }

        /// <summary>
        ///     回采巷道实体赋值
        /// </summary>
        private void bindTunnelHCEntity()
        {
            if (workingFace == null) return;

            //队别
            workingFace.TeamNameId = Convert.ToInt32(cboTeamName.SelectedValue);
            //开工日期
            workingFace.StartDate = dtpStartDate.Value;
            //是否停工
            if (rbtnHCY.Checked)
            {
                workingFace.IsFinish = 1;
            }
            if (rbtnHCN.Checked)
            {
                workingFace.IsFinish = 0;
            }
            //停工日期
            if (rbtnHCY.Checked)
            {
                workingFace.StopDate = dtpStopDate.Value;
            }
            //工作制式
            if (rbtn38.Checked)
            {
                workingFace.WorkStyle = rbtn38.Text;
            }
            if (rbtn46.Checked)
            {
                workingFace.WorkStyle = rbtn46.Text;
            }
            //班次
            workingFace.WorkTime = cboWorkTime.Text;
        }

        /// <summary>
        ///     绑定队别名称
        /// </summary>
        private void bindTeamInfo()
        {
            cboTeamName.Items.Clear();
            Team[] team = Team.FindAll();
            foreach (Team t in team)
            {
                cboTeamName.Items.Add(t.TeamName);
            }
        }

        /// <summary>
        ///     提交
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnSubmit_Click(object sender, EventArgs e)
        {
            try
            {
                if (!check())
                {
                    DialogResult = DialogResult.None;
                    return;
                }
                DialogResult = DialogResult.OK;
                bindTunnelHCEntity();


                workingFace.Save();
                foreach (var i in tunnelSet)
                {
                    i.Save();
                }

                //添加
                if (Text == Const_GM.TUNNEL_HC_ADD)
                {
                    //添加回采进尺图上显示信息
                    AddHcjc(tunnelZY.TunnelId, tunnelFY.TunnelId, tunnelQY.TunnelId, tunnelZY.TunnelWid,
                        tunnelFY.TunnelWid, tunnelQY.TunnelWid);
                }

                //修改
                if (Text == Const_GM.TUNNEL_HC_CHANGE)
                {
                    //修改回采进尺图上显示信息，更新工作面信息表
                    UpdateHcjc(tunnelZY.TunnelId, tunnelFY.TunnelId, tunnelQY.TunnelId, tunnelFY.TunnelWid,
                        tunnelFY.TunnelWid, tunnelQY.TunnelWid);
                }

                Alert.alert("回采工作面关联成功！");

            }
            catch (Exception)
            {
                Alert.alert("回采工作面关联失败，请检查回采工作面是否符合标准！");
            }
        }

        /// <summary>
        ///     添加初始化时的回采进尺
        /// </summary>
        private void AddHcjc(int hd1, int hd2, int qy, double zywid, double fywid, double qywid)
        {
            //已经存在回采进尺的，计算回采进尺点，保存到工作面表中，同时将绘制回采
            double initialHc = 0.0;
            if (InitialHCLen.Text != "")
                double.TryParse(InitialHCLen.Text, out initialHc);
            if (initialHc > 0)
            {
                var dics = new Dictionary<string, string>();
                dics[GIS_Const.FIELD_ID] = "0";
                dics[GIS_Const.FIELD_BS] = "1";
                dics[GIS_Const.FIELD_HDID] = hd1 + "_" + hd2;
                dics[GIS_Const.FIELD_XH] = "1";
                dics[GIS_Const.FIELD_BID] = IDGenerator.NewBindingID();
                IPoint pos = new PointClass();
                Dictionary<string, List<GeoStruct>> dzxlist = Global.cons.DrawHDHC(hd1.ToString(), hd2.ToString(),
                    qy.ToString(), initialHc, zywid, fywid, qywid, 1, Global.searchlen, dics, true, null, out pos);

                //工作面信息提交
                if (pos == null)
                    return;

                workingFace.Coordinate = new Coordinate(pos.X, pos.Y, 0.0);

                WorkingFaceBLL.updateWorkingFaceInfo(workingFace);
                //添加地质构造信息到数据库表中
                if (dzxlist.Count > 0)
                {
                    GeologySpaceBll.DeleteGeologySpaceEntityInfos(workingFace.WorkingFaceId); //删除工作面ID对应的地质构造信息
                    foreach (string key in dzxlist.Keys)
                    {
                        List<GeoStruct> geoinfos = dzxlist[key];
                        string geo_type = key;
                        for (int i = 0; i < geoinfos.Count; i++)
                        {
                            GeoStruct tmp = geoinfos[i];

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
        }

        /// <summary>
        ///     修改回采进尺面上显示信息
        /// </summary>
        private void UpdateHcjc(int hd1, int hd2, int qy, double zywid, double fywid, double qywid)
        {
            //已经存在回采进尺的，计算回采进尺点，保存到工作面表中，同时将绘制回采
            double initialHc = 0.0;

            if (InitialHCLen.Text != "")
                double.TryParse(InitialHCLen.Text, out initialHc);
            if (initialHc > 0)
            {
                var dics = new Dictionary<string, string>();
                dics[GIS_Const.FIELD_ID] = "0";
                dics[GIS_Const.FIELD_BS] = "1";
                dics[GIS_Const.FIELD_HDID] = hd1 + "_" + hd2;
                dics[GIS_Const.FIELD_XH] = "1";
                IPoint pos = new PointClass();
                Dictionary<string, List<GeoStruct>> dzxlist = Global.cons.DrawHDHC(hd1.ToString(), hd2.ToString(),
                    qy.ToString(), initialHc, zywid, fywid, qywid, 1, Global.searchlen, dics, false, null, out pos);

                if (null == pos)
                {
                    Log.Debug("[回采面关联]: pos is null.");
                    return;
                }

                //工作面信息提交
                workingFace.Coordinate = new Coordinate(pos.X, pos.Y, 0.0);

                WorkingFaceBLL.updateWorkingFaceInfo(workingFace);
                //更新地质构造表
                if (dzxlist.Count > 0)
                {
                    GeologySpaceBll.DeleteGeologySpaceEntityInfos(workingFace.WorkingFaceId); //删除对应工作面ID的地质构造信息
                    foreach (string key in dzxlist.Keys)
                    {
                        List<GeoStruct> geoinfos = dzxlist[key];
                        string geo_type = key;
                        for (int i = 0; i < geoinfos.Count; i++)
                        {
                            GeoStruct tmp = geoinfos[i];

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
        }

        /// <summary>
        ///     工作制式选择
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

        /// <summary>
        ///     工作制式选择
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void rbtn46_CheckedChanged(object sender, EventArgs e)
        {
            //四六制
            DataBindUtil.LoadWorkTime(cboWorkTime,
                rbtn38.Checked ? Const_MS.WORK_GROUP_ID_38 : Const_MS.WORK_GROUP_ID_46);
            // 设置班次名称
            setWorkTimeName();
        }

        /// <summary>
        ///     验证
        /// </summary>
        /// <returns>是否验证通过</returns>
        private bool check()
        {
            ////巷道选择
            //if (tunnelHCEntity.TunnelID_ZY == 0 && tunnelHCEntity.TunnelID_FY == 0 && tunnelHCEntity.TunnelID_KQY == 0)
            //{
            //    Alert.alert(Const.MSG_PLEASE_CHOOSE + Const_GM.TUNNEL + Const.SIGN_EXCLAMATION_MARK);
            //    return false;
            //}
            //主运顺槽选择
            if (tunnelZY == null)
            {
                Alert.alert(Const.MSG_PLEASE_CHOOSE + Const_GM.MAIN_TUNNEL + Const.SIGN_EXCLAMATION_MARK);
                return false;
            }
            //辅运顺槽选择
            if (tunnelFY == null)
            {
                Alert.alert(Const.MSG_PLEASE_CHOOSE + Const_GM.SECOND_TUNNEL + Const.SIGN_EXCLAMATION_MARK);
                return false;
            }
            //开切眼选择
            if (tunnelQY == null)
            {
                Alert.alert(Const.MSG_PLEASE_CHOOSE + Const_GM.OOC_TUNNEL + Const.SIGN_EXCLAMATION_MARK);
                return false;
            }

            //队别为空
            if (!Check.isEmpty(cboTeamName, Const_MS.TEAM_NAME))
            {
                cboTeamName.BackColor = Const.ERROR_FIELD_COLOR;
                return false;
            }
            cboTeamName.BackColor = Const.NO_ERROR_FIELD_COLOR;
            //验证成功
            return true;
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

        private void rbtnHCY_CheckedChanged(object sender, EventArgs e)
        {
            if (rbtnHCY.Checked)
            {
                dtpStopDate.Enabled = true;
            }
            else
            {
                dtpStopDate.Enabled = false;
            }
        }

        private void radioButton_Add_CheckedChanged(object sender, EventArgs e)
        {
            btnChooseOther.Enabled = true;
        }

        /// <summary>
        ///     添加其他巷道按钮
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button_ChooseAdd_Click(object sender, EventArgs e)
        {
            ////巷道选择窗体
            SelectTunnelDlg tunnelChoose;
            tunnelChoose = new SelectTunnelDlg(MainForm);
            if (DialogResult.OK == tunnelChoose.ShowDialog())
            {
                //添加信息到listBox
                var ts = new TunnelSimple(tunnelChoose.tunnelId, tunnelChoose.tunnelName);
                listBox_Browse.Items.Add(ts);
                Tunnel ent = BasicInfoManager.getInstance().getTunnelByID(tunnelChoose.tunnelId);
                if (ent != null)
                {
                    ent.TunnelType = TunnelTypeEnum.STOPING_OTHER;
                    ent.WorkingFace = workingFace;
                    tunnelSet.Add(ent);
                }
            }
        }

        private void listBox_Browse_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Delete)
            {
                Tunnel ent =
                    BasicInfoManager.getInstance()
                        .getTunnelByID(Convert.ToInt32(((TunnelSimple)listBox_Browse.SelectedItem).Id));
                ent.TunnelType = TunnelTypeEnum.OTHER;
                tunnelSet.Add(ent);

                listBox_Browse.Items.Remove(listBox_Browse.SelectedItem);
            }
        }
    }
}