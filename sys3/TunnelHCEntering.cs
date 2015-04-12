using System;
using System.Collections.Generic;
using System.Windows.Forms;
using ESRI.ArcGIS.Geometry;
using GIS;
using GIS.HdProc;
using LibBusiness;
using LibBusiness.CommonBLL;
using LibCommon;
using LibCommonForm;
using LibEntity;

namespace sys3
{
    /// <summary>
    ///     回采面
    /// </summary>
    public partial class TunnelHcEntering : Form
    {
        public TunnelHcEntering()
        {
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
            SetWorkTimeName();
        }

        /// <summary>
        ///     构造方法
        /// </summary>
        /// <param name="tunnelHcEntity">回采面实体</param>
        public TunnelHcEntering(WorkingFace tunnelHcEntity)
        {
            _workingFace = ObjectCopier.Clone(tunnelHcEntity);
            Text = Const_GM.TUNNEL_HC_CHANGE;
            InitializeComponent();

            FormDefaultPropertiesSetter.SetEnteringFormDefaultProperties(this, Const_GM.TUNNEL_HC_CHANGE);
        }

        private void Form_TunnelHCEntering_Load(object sender, EventArgs e)
        {
            //绑定队别名称
            DataBindUtil.LoadTeam(cboTeamName);

            if (Text == Const_GM.TUNNEL_HC_CHANGE)
            {
                BindInfo();
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
        private void SetWorkTimeName()
        {
            var strWorkTimeName = "";
            var sysDateTime = DateTime.Now.ToString("HH:mm:ss");
            strWorkTimeName = MineDataSimpleBLL.selectWorkTimeNameByWorkTimeGroupIdAndSysTime(rbtn38.Checked ? 1 : 2,
                sysDateTime);

            if (!string.IsNullOrEmpty(strWorkTimeName))
            {
                cboWorkTime.Text = strWorkTimeName;
            }
        }

        /// <summary>
        ///     绑定修改信息
        /// </summary>
        private void BindInfo()
        {
            foreach (var tunnel in _workingFace.Tunnels)
            {
                if (tunnel.TunnelType == TunnelTypeEnum.STOPING_ZY)
                    _tunnelZy = tunnel; //主运顺槽
                else if (tunnel.TunnelType == TunnelTypeEnum.STOPING_FY)
                    _tunnelFy = tunnel; //辅运顺槽
                else if (tunnel.TunnelType == TunnelTypeEnum.STOPING_QY)
                    _tunnelQy = tunnel; //开切眼
                else
                {
                    if (tunnel.TunnelType == TunnelTypeEnum.STOPING_OTHER)
                    {
                        _otherTunnelList.Add(tunnel);
                    }
                }
            }

            btnChooseWF.Text = _workingFace.WorkingFaceName;

            //主运顺槽
            btnChooseZY.Text = _tunnelZy != null ? _tunnelZy.TunnelName : "";
            //辅运顺槽
            btnChooseFY.Text = _tunnelFy != null ? _tunnelFy.TunnelName : "";
            //开切眼
            btnChooseQY.Text = _tunnelQy != null ? _tunnelQy.TunnelName : "";


            foreach (var i in _otherTunnelList)
            {
                listBox_Browse.Items.Add(i);
            }
            //TODO:这块有些问题
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
            cboTeamName.Text = _workingFace.Team.TeamName;

            //开始日期
            dtpStartDate.Value = DateTimeUtil.validateDTPDateTime(_workingFace.StartDate);

            //是否回采完毕
            if (_workingFace.IsFinish == 1)
            {
                rbtnHCY.Checked = true;
            }
            else
            {
                rbtnHCN.Checked = true;
            }
            //停工日期
            if (_workingFace.IsFinish == 1)
            {
                dtpStopDate.Value = _workingFace.StopDate;
            }
            //工作制式
            if (_workingFace.WorkStyle == rbtn38.Text)
            {
                rbtn38.Checked = true;
            }
            else
            {
                rbtn46.Checked = true;
            }
            //班次
            cboWorkTime.Text = _workingFace.WorkTime;
        }

        private string[] SplitString(string p)
        {
            var sArray = new string[10];

            if (!string.IsNullOrEmpty(p))
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

            wfChoose = new SelectWorkingFaceDlg();
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
                btnChooseWF.Text = wfChoose.SelectedWorkingFace.WorkingFaceName;
                //实体赋值
                _workingFace = wfChoose.SelectedWorkingFace;
            }
        }

        /// <summary>
        ///     主运顺槽按钮
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnChooseZY_Click(object sender, EventArgs e)
        {
            if (_workingFace == null)
            {
                Alert.alert("请先选择工作面");
                return;
            }

            var tunnelChoose = new SelectTunnelDlg(_workingFace);
            //巷道选择完毕
            if (DialogResult.OK != tunnelChoose.ShowDialog()) return;
            if (_tunnelZy != null)
            {
                var ent = Tunnel.Find(_tunnelZy.TunnelId);
                ent.TunnelType = TunnelTypeEnum.OTHER;
                _tunnelSet.Add(ent);
            }

            //巷道选择按钮Text改变
            btnChooseZY.Text = tunnelChoose.SelectedTunnel.TunnelName;
            //实体赋值
            _tunnelZy = Tunnel.Find(tunnelChoose.SelectedTunnel.TunnelId);
            if (_tunnelZy == null) return;
            _tunnelZy.TunnelType = TunnelTypeEnum.STOPING_ZY;
            _tunnelSet.Add(_tunnelZy);
        }

        /// <summary>
        ///     辅运顺槽
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnChooseFY_Click(object sender, EventArgs e)
        {
            if (_workingFace == null)
            {
                Alert.alert("请先选择工作面");
                return;
            }

            //巷道选择窗体
            var tunnelChoose = new SelectTunnelDlg(_workingFace);

            //巷道选择完毕
            if (DialogResult.OK != tunnelChoose.ShowDialog()) return;
            if (_tunnelFy != null)
            {
                var ent = Tunnel.Find(_tunnelFy.TunnelId);
                ent.TunnelType = TunnelTypeEnum.OTHER;
                _tunnelSet.Add(ent);
            }

            //巷道选择按钮Text改变
            btnChooseFY.Text = tunnelChoose.SelectedTunnel.TunnelName;
            //实体赋值

            _tunnelFy = Tunnel.Find(tunnelChoose.SelectedTunnel.TunnelId);
            if (_tunnelFy == null) return;
            _tunnelFy.TunnelType = TunnelTypeEnum.STOPING_FY;
            _tunnelSet.Add(_tunnelFy);
        }

        /// <summary>
        ///     开切眼
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnChooseQY_Click(object sender, EventArgs e)
        {
            if (_workingFace == null)
            {
                Alert.alert("请先选择工作面");
                return;
            }

            //巷道选择窗体
            var tunnelChoose = new SelectTunnelDlg(_workingFace);

            //巷道选择完毕
            if (DialogResult.OK == tunnelChoose.ShowDialog())
            {
                if (_tunnelQy != null)
                {
                    var ent = Tunnel.Find(_tunnelQy.TunnelId);
                    ent.TunnelType = TunnelTypeEnum.OTHER;
                    _tunnelSet.Add(ent);
                }

                //巷道选择按钮Text改变
                btnChooseQY.Text = tunnelChoose.SelectedTunnel.TunnelName;
                //实体赋值
                _tunnelQy = Tunnel.Find(tunnelChoose.SelectedTunnel.TunnelId);
                if (_tunnelQy != null)
                {
                    _tunnelQy.TunnelType = TunnelTypeEnum.STOPING_QY;
                    _tunnelSet.Add(_tunnelQy);
                }
            }
        }

        /// <summary>
        ///     回采巷道实体赋值
        /// </summary>
        private void bindTunnelHCEntity()
        {
            if (_workingFace == null) return;

            //队别
            _workingFace.Team = Team.Find(cboTeamName.SelectedValue);
            //开工日期
            _workingFace.StartDate = dtpStartDate.Value;
            //是否停工
            if (rbtnHCY.Checked)
            {
                _workingFace.IsFinish = 1;
            }
            if (rbtnHCN.Checked)
            {
                _workingFace.IsFinish = 0;
            }
            //停工日期
            if (rbtnHCY.Checked)
            {
                _workingFace.StopDate = dtpStopDate.Value;
            }
            //工作制式
            if (rbtn38.Checked)
            {
                _workingFace.WorkStyle = rbtn38.Text;
            }
            if (rbtn46.Checked)
            {
                _workingFace.WorkStyle = rbtn46.Text;
            }
            //班次
            _workingFace.WorkTime = cboWorkTime.Text;
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


                _workingFace.Save();
                foreach (var i in _tunnelSet)
                {
                    i.Save();
                }

                //添加
                if (Text == Const_GM.TUNNEL_HC_ADD)
                {
                    //添加回采进尺图上显示信息
                    AddHcjc(_tunnelZy.TunnelId, _tunnelFy.TunnelId, _tunnelQy.TunnelId, _tunnelZy.TunnelWid,
                        _tunnelFy.TunnelWid, _tunnelQy.TunnelWid);
                }

                //修改
                if (Text == Const_GM.TUNNEL_HC_CHANGE)
                {
                    //修改回采进尺图上显示信息，更新工作面信息表
                    UpdateHcjc(_tunnelZy.TunnelId, _tunnelFy.TunnelId, _tunnelQy.TunnelId, _tunnelFy.TunnelWid,
                        _tunnelFy.TunnelWid, _tunnelQy.TunnelWid);
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
            var initialHc = 0.0;
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
                var dzxlist = Global.cons.DrawHDHC(hd1.ToString(), hd2.ToString(),
                    qy.ToString(), initialHc, zywid, fywid, qywid, 1, Global.searchlen, dics, true, null, out pos);

                //工作面信息提交
                if (pos == null)
                    return;

                _workingFace.SetCoordinate(pos.X, pos.Y, 0.0);
                _workingFace.Save();

                //添加地质构造信息到数据库表中
                if (dzxlist.Count > 0)
                {
                    GeologySpaceBll.DeleteGeologySpaceEntityInfos(_workingFace.WorkingFaceId); //删除工作面ID对应的地质构造信息
                    foreach (var key in dzxlist.Keys)
                    {
                        var geoinfos = dzxlist[key];
                        var geo_type = key;
                        for (var i = 0; i < geoinfos.Count; i++)
                        {
                            var tmp = geoinfos[i];

                            var geologyspaceEntity = new GeologySpace();
                            geologyspaceEntity.WorkingFace = _workingFace;
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
            var initialHc = 0.0;

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
                var dzxlist = Global.cons.DrawHDHC(hd1.ToString(), hd2.ToString(),
                    qy.ToString(), initialHc, zywid, fywid, qywid, 1, Global.searchlen, dics, false, null, out pos);

                if (null == pos)
                {
                    Log.Debug("[回采面关联]: pos is null.");
                    return;
                }

                //工作面信息提交
                _workingFace.SetCoordinate(pos.X, pos.Y, 0.0);
                _workingFace.Save();

                //更新地质构造表
                if (dzxlist.Count > 0)
                {
                    GeologySpaceBll.DeleteGeologySpaceEntityInfos(_workingFace.WorkingFaceId); //删除对应工作面ID的地质构造信息
                    foreach (var key in dzxlist.Keys)
                    {
                        var geoinfos = dzxlist[key];
                        var geo_type = key;
                        for (var i = 0; i < geoinfos.Count; i++)
                        {
                            var tmp = geoinfos[i];

                            var geologyspaceEntity = new GeologySpace();
                            geologyspaceEntity.WorkingFace = _workingFace;
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
            SetWorkTimeName();
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
            SetWorkTimeName();
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
            if (_tunnelZy == null)
            {
                Alert.alert(Const.MSG_PLEASE_CHOOSE + Const_GM.MAIN_TUNNEL + Const.SIGN_EXCLAMATION_MARK);
                return false;
            }
            //辅运顺槽选择
            if (_tunnelFy == null)
            {
                Alert.alert(Const.MSG_PLEASE_CHOOSE + Const_GM.SECOND_TUNNEL + Const.SIGN_EXCLAMATION_MARK);
                return false;
            }
            //开切眼选择
            if (_tunnelQy == null)
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
            tunnelChoose = new SelectTunnelDlg();
            if (DialogResult.OK == tunnelChoose.ShowDialog())
            {
                //添加信息到listBox
                var tunnel = tunnelChoose.SelectedTunnel;
                if (tunnel != null)
                {
                    listBox_Browse.Items.Add(tunnel);
                    tunnel.TunnelType = TunnelTypeEnum.STOPING_OTHER;
                    tunnel.WorkingFace = _workingFace;
                    _tunnelSet.Add(tunnel);
                }
            }
        }

        private void listBox_Browse_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode != Keys.Delete) return;
            var tunnel = listBox_Browse.SelectedItem as Tunnel;
            if (tunnel != null)
            {
                tunnel.TunnelType = TunnelTypeEnum.OTHER;
                _tunnelSet.Add(tunnel);
            }

            listBox_Browse.Items.Remove(listBox_Browse.SelectedItem);
        }

        #region ******变量声明******;

        // 主运
        // 辅运
        private readonly List<Tunnel> _otherTunnelList = new List<Tunnel>();
        private readonly HashSet<Tunnel> _tunnelSet = new HashSet<Tunnel>();
        private Tunnel _tunnelFy;
        // 切眼
        private Tunnel _tunnelQy;
        private Tunnel _tunnelZy;

        private WorkingFace _workingFace;

        #endregion ******变量声明******
    }
}