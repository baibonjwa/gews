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
using LibBusiness.CommonBLL;
using System.Collections;
using LibCommonControl;
using LibCommonForm;
using GIS.HdProc;
using ESRI.ArcGIS.Geodatabase;
using ESRI.ArcGIS.Geometry;


namespace _3.GeologyMeasure
{
    /// <summary>
    /// 回采面
    /// </summary>
    public partial class TunnelHCEntering : BaseForm
    {
        #region ******变量声明******;
        // 主运
        Tunnel tunnelZY = null;
        // 辅运
        Tunnel tunnelFY = null;
        // 切眼
        Tunnel tunnelQY = null;

        HashSet<Tunnel> tunnelSet = new HashSet<Tunnel>();

        List<Tunnel> otherTunnelList = new List<Tunnel>();

        WorkingFace workingFace = null;

        int[] intArr = new int[5];

        ArrayList listTunnelEntity = new ArrayList();
        #endregion ******变量声明******

        public TunnelHCEntering(MainFrm mainFrm)
        {
            this.MainForm = mainFrm;

            InitializeComponent();

            //窗体属性设置
            LibCommon.FormDefaultPropertiesSetter.SetEnteringFormDefaultProperties(this, Const_GM.TUNNEL_HC_ADD);

            //默认回采未完毕
            rbtnHCN.Checked = true;

            //默认工作制式选择
            if (WorkTimeBLL.getDefaultWorkTime() == Const_MS.WORK_TIME_38)
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
        /// 构造方法
        /// </summary>
        /// <param name="tunnelHCEntity">回采面实体</param>
        public TunnelHCEntering(WorkingFace tunnelHCEntity, MainFrm mainFrm)
        {
            this.MainForm = mainFrm;
            this.workingFace = LibCommon.ObjectCopier.Clone<WorkingFace>(tunnelHCEntity);
            this.Text = Const_GM.TUNNEL_HC_CHANGE;
            InitializeComponent();

            LibCommon.FormDefaultPropertiesSetter.SetEnteringFormDefaultProperties(this, Const_GM.TUNNEL_HC_CHANGE);
        }

        private void Form_TunnelHCEntering_Load(object sender, EventArgs e)
        {
            //绑定队别名称
            bindTeamInfo();

            if (this.Text == Const_GM.TUNNEL_HC_CHANGE)
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
        /// 设置班次名称
        /// </summary>
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

        /// <summary>
        /// 绑定修改信息
        /// </summary>
        private void bindInfo()
        {
            workingFace.tunnelSet = BasicInfoManager.getInstance().getTunnelSetByDataSet(TunnelInfoBLL.selectTunnelByWorkingFaceId(workingFace.WorkingFaceID));
            intArr[0] = workingFace.MiningArea.Horizontal.Mine.MineId;
            intArr[1] = workingFace.MiningArea.Horizontal.HorizontalId;
            intArr[2] = workingFace.MiningArea.MiningAreaId;
            intArr[3] = workingFace.WorkingFaceID;

            string otherTunnel = "";
            foreach (Tunnel tunnel in workingFace.tunnelSet)
            {
                if (tunnel.TunnelType == TunnelTypeEnum.STOPING_ZY)
                    tunnelZY = tunnel;//主运顺槽
                else if (tunnel.TunnelType == TunnelTypeEnum.STOPING_FY)
                    tunnelFY = tunnel;//辅运顺槽
                else if (tunnel.TunnelType == TunnelTypeEnum.STOPING_QY)
                    tunnelQY = tunnel;//开切眼
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


            foreach (var i in otherTunnelList)
            {
                listBox_Browse.Items.Add(new TunnelSimple(i.TunnelID, i.TunnelName));
            }
            listBox_Browse.DisplayMember = "Name";
            listBox_Browse.ValueMember = "Id";
            //其它巷道
            //string[] sArray = new string[10];
            //sArray = SplitString(tunnelHCEntity.TunnelID);
            //foreach (string i in sArray)
            //{
            //    if ((i != "") && (i != null))
            //    {
            //        int iTunnelID = Convert.ToInt16(i);
            //        listBox_Browse.Items.Add(TunnelInfoBLL.selectTunnelInfoByTunnelID(iTunnelID).TunnelName);
            //    }
            //}

            //队别名称
            cboTeamName.Text = BasicInfoManager.getInstance().getTeamNameById(workingFace.TeamNameID);

            //开始日期
            dtpStartDate.Value = DateTimeUtil.validateDTPDateTime((System.DateTime)this.workingFace.StartDate);

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
                dtpStopDate.Value = (System.DateTime)workingFace.StopDate;
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
            string[] sArray = new string[10];

            if ((p != null) && (p != ""))
            {
                sArray = p.Split(',');
            }

            return sArray;
        }

        /// <summary>
        /// 选择工作面
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
                intArr[3] = workingFace.WorkingFaceID;
            }
        }

        /// <summary>
        /// 主运顺槽按钮
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

            SelectTunnelDlg tunnelChoose = new SelectTunnelDlg(intArr, this.MainForm, 2);

            //巷道选择完毕
            if (DialogResult.OK == tunnelChoose.ShowDialog())
            {
                if (tunnelZY != null)
                {
                    Tunnel ent = BasicInfoManager.getInstance().getTunnelByID(tunnelZY.TunnelID);
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
        /// 辅运顺槽
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
            SelectTunnelDlg tunnelChoose = new SelectTunnelDlg(intArr, this.MainForm, 2);

            //巷道选择完毕
            if (DialogResult.OK == tunnelChoose.ShowDialog())
            {
                if (tunnelFY != null)
                {
                    Tunnel ent = BasicInfoManager.getInstance().getTunnelByID(tunnelFY.TunnelID);
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
        /// 开切眼
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
            SelectTunnelDlg tunnelChoose = new SelectTunnelDlg(intArr, this.MainForm, 2);

            //巷道选择完毕
            if (DialogResult.OK == tunnelChoose.ShowDialog())
            {
                if (tunnelQY != null)
                {
                    Tunnel ent = BasicInfoManager.getInstance().getTunnelByID(tunnelQY.TunnelID);
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
        /// 回采巷道实体赋值
        /// </summary>
        private void bindTunnelHCEntity()
        {
            if (workingFace == null) return;

            //队别
            workingFace.TeamNameID = Convert.ToInt32(cboTeamName.SelectedValue);
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
            if (rbtnHCY.Checked == true)
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
        /// 绑定队别名称
        /// </summary>
        private void bindTeamInfo()
        {
            cboTeamName.Items.Clear();
            DataSet ds = TeamBLL.selectTeamInfo();
            cboTeamName.DataSource = ds.Tables[0];
            cboTeamName.DisplayMember = TeamDbConstNames.TEAM_NAME;
            cboTeamName.ValueMember = TeamDbConstNames.ID;
        }

        /// <summary>
        /// 绑定班次
        /// </summary>
        private void bindWorkTimeFirstTime()
        {
            DataSet dsWorkTime;
            if (rbtn38.Checked)
            {
                dsWorkTime = WorkTimeBLL.returnWorkTime(rbtn38.Text);
            }
            else
            {
                dsWorkTime = WorkTimeBLL.returnWorkTime(rbtn46.Text);
            }
            for (int i = 0; i < dsWorkTime.Tables[0].Rows.Count; i++)
            {
                cboWorkTime.Items.Add(dsWorkTime.Tables[0].Rows[i][WorkTimeDbConstNames.WORK_TIME_NAME].ToString());
            }
        }

        /// <summary>
        /// 提交
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
                else
                {
                    DialogResult = DialogResult.OK;
                }
                bindTunnelHCEntity();

                bool bResult = TunnelInfoBLL.setTunnelAsHC(workingFace, tunnelSet);

                //添加
                if (this.Text == Const_GM.TUNNEL_HC_ADD && bResult)
                {
                    //添加回采进尺图上显示信息
                    AddHcjc(tunnelZY.TunnelID, tunnelFY.TunnelID, tunnelQY.TunnelID, tunnelZY.TunnelWid, tunnelFY.TunnelWid, tunnelQY.TunnelWid);
                }

                //修改
                if (this.Text == Const_GM.TUNNEL_HC_CHANGE && bResult)
                {
                    //修改回采进尺图上显示信息，更新工作面信息表
                    UpdateHcjc(tunnelZY.TunnelID, tunnelFY.TunnelID, tunnelQY.TunnelID, tunnelFY.TunnelWid, tunnelFY.TunnelWid, tunnelQY.TunnelWid);
                }

                if (bResult)
                {
                    Alert.alert("回采工作面关联成功！");
                    //TODO:成功后事件
                }
            }
            catch (Exception)
            {
                Alert.alert("回采工作面关联失败，请检查回采工作面是否符合标准！");
            }
        }

        /// <summary>
        /// 添加初始化时的回采进尺
        /// </summary>
        private void AddHcjc(int hd1, int hd2, int qy, double zywid, double fywid, double qywid)
        {
            //已经存在回采进尺的，计算回采进尺点，保存到工作面表中，同时将绘制回采
            double initialHc = 0.0;
            if (this.InitialHCLen.Text != "")
                double.TryParse(this.InitialHCLen.Text, out initialHc);
            if (initialHc > 0)
            {
                Dictionary<string, string> dics = new Dictionary<string, string>();
                dics[GIS.GIS_Const.FIELD_ID] = "0";
                dics[GIS.GIS_Const.FIELD_BS] = "1";
                dics[GIS.GIS_Const.FIELD_HDID] = hd1.ToString() + "_" + hd2.ToString();
                dics[GIS.GIS_Const.FIELD_XH] = "1";
                dics[GIS.GIS_Const.FIELD_BID] = IDGenerator.NewBindingID().ToString();
                IPoint pos = new PointClass();
                Dictionary<string, List<GeoStruct>> dzxlist = Global.cons.DrawHDHC(hd1.ToString(), hd2.ToString(),
                    qy.ToString(), initialHc, zywid, fywid, qywid, 1, Global.searchlen, dics, true, null, out pos);

                //工作面信息提交
                if (pos == null)
                    return;

                workingFace.Coordinate = new CoordinateEntity(pos.X, pos.Y, 0.0);

                LibBusiness.WorkingFaceBLL.updateWorkingFaceInfo(workingFace);
                //添加地质构造信息到数据库表中
                if (dzxlist.Count > 0)
                {
                    GeologySpaceBLL.deleteGeologySpaceEntityInfos(workingFace.WorkingFaceID); //删除工作面ID对应的地质构造信息
                    foreach (string key in dzxlist.Keys)
                    {
                        List<GeoStruct> geoinfos = dzxlist[key];
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
        }

        /// <summary>
        /// 修改回采进尺面上显示信息
        /// </summary>
        private void UpdateHcjc(int hd1, int hd2, int qy, double zywid, double fywid, double qywid)
        {
            //已经存在回采进尺的，计算回采进尺点，保存到工作面表中，同时将绘制回采
            double initialHc = 0.0;

            if (this.InitialHCLen.Text != "")
                double.TryParse(this.InitialHCLen.Text, out initialHc);
            if (initialHc > 0)
            {
                Dictionary<string, string> dics = new Dictionary<string, string>();
                dics[GIS.GIS_Const.FIELD_ID] = "0";
                dics[GIS.GIS_Const.FIELD_BS] = "1";
                dics[GIS.GIS_Const.FIELD_HDID] = hd1.ToString() + "_" + hd2.ToString();
                dics[GIS.GIS_Const.FIELD_XH] = "1";
                IPoint pos = new PointClass();
                Dictionary<string, List<GeoStruct>> dzxlist = Global.cons.DrawHDHC(hd1.ToString(), hd2.ToString(),
                    qy.ToString(), initialHc, zywid, fywid, qywid, 1, Global.searchlen, dics, false, null, out pos);

                if (null == pos)
                {
                    Log.Debug("[回采面关联]: pos is null.");
                    return;
                }

                //工作面信息提交
                workingFace.Coordinate = new CoordinateEntity(pos.X, pos.Y, 0.0);

                LibBusiness.WorkingFaceBLL.updateWorkingFaceInfo(workingFace);
                //更新地质构造表
                if (dzxlist.Count > 0)
                {
                    GeologySpaceBLL.deleteGeologySpaceEntityInfos(workingFace.WorkingFaceID); //删除对应工作面ID的地质构造信息
                    foreach (string key in dzxlist.Keys)
                    {
                        List<GeoStruct> geoinfos = dzxlist[key];
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
        }

        /// <summary>
        /// 工作制式选择
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void rbtn38_CheckedChanged(object sender, EventArgs e)
        {
            //三八制
            if (rbtn38.Checked)
            {
                cboWorkTime.DataSource = null;
                DataSet dsWorkTime = WorkTimeBLL.returnWorkTime(Const_MS.WORK_TIME_38);
                cboWorkTime.DataSource = dsWorkTime.Tables[0];
                cboWorkTime.DisplayMember = WorkTimeDbConstNames.WORK_TIME_NAME;
                cboWorkTime.ValueMember = WorkTimeDbConstNames.WORK_TIME_ID;
            }
            // 设置班次名称
            setWorkTimeName();
        }

        /// <summary>
        /// 工作制式选择
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void rbtn46_CheckedChanged(object sender, EventArgs e)
        {
            //四六制
            if (rbtn46.Checked)
            {
                cboWorkTime.DataSource = null;
                DataSet dsWorkTime = WorkTimeBLL.returnWorkTime(Const_MS.WORK_TIME_46);
                cboWorkTime.DataSource = dsWorkTime.Tables[0];
                cboWorkTime.DisplayMember = WorkTimeDbConstNames.WORK_TIME_NAME;
                cboWorkTime.ValueMember = WorkTimeDbConstNames.WORK_TIME_ID;
            }
            // 设置班次名称
            setWorkTimeName();
        }

        /// <summary>
        /// 验证
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
            else
            {
                cboTeamName.BackColor = Const.NO_ERROR_FIELD_COLOR;
            }
            //验证成功
            return true;
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
        /// 添加其他巷道按钮
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button_ChooseAdd_Click(object sender, EventArgs e)
        {
            ////巷道选择窗体
            SelectTunnelDlg tunnelChoose;
            tunnelChoose = new SelectTunnelDlg(this.MainForm);
            if (DialogResult.OK == tunnelChoose.ShowDialog())
            {
                //添加信息到listBox
                TunnelSimple ts = new TunnelSimple(tunnelChoose.tunnelId, tunnelChoose.tunnelName);
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
                Tunnel ent = BasicInfoManager.getInstance().getTunnelByID(Convert.ToInt32(((TunnelSimple)listBox_Browse.SelectedItem).Id));
                ent.TunnelType = TunnelTypeEnum.OTHER;
                tunnelSet.Add(ent);

                listBox_Browse.Items.Remove(listBox_Browse.SelectedItem);
            }
        }
    }
}
