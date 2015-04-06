using System;
using System.Windows.Forms;
using LibBusiness;
using LibBusiness.CommonBLL;
using LibCommon;
using LibCommonControl;
using LibCommonForm;
using LibEntity;

namespace sys3
{
    public partial class TunnelJjEntering : Form
    {
        /************变量定义*****************/
        // 掘进面
        WorkingFace jjWorkFaceEntity;
        // 巷道
        Tunnel tunnelEntity;
        /*************************************/

        /// <summary>
        /// 构造方法
        /// </summary>
        public TunnelJjEntering()
        {
            InitializeComponent();

            //默认选择未掘进完毕
            rbtnJJN.Checked = true;

            //默认工作制式选择
            if (WorkingTimeDefault.FindFirst().DefaultWorkTimeGroupId == Const_MS.WORK_GROUP_ID_38)
            {
                rbtn38.Checked = true;
            }
            else
            {
                rbtn46.Checked = true;
            }

            // 设置班次名称
            setWorkTimeName();

            //设置窗体属性
            FormDefaultPropertiesSetter.SetEnteringFormDefaultProperties(this, Const_GM.TUNNEL_JJ_ADD);
        }

        /// <summary>
        /// 构造方法
        /// </summary>
        /// <param name="tunnelEntity"></param>
        public TunnelJjEntering(Tunnel tunnelEntity)
        {

            this.tunnelEntity = tunnelEntity;

            InitializeComponent();

            //绑定修改信息

            //窗体属性设置
            FormDefaultPropertiesSetter.SetEnteringFormDefaultProperties(this, Const_GM.TUNNEL_JJ_CHANGE);
        }

        /// <summary>
        /// 初始化
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TunnelJJHC_Load(object sender, EventArgs e)
        {
            DataBindUtil.LoadTeam(cboTeamName);

            if (this.Text == Const_GM.TUNNEL_JJ_CHANGE)
            {
                bindInfo();
            }

            if (rbtnJJN.Checked)
            {
                dtpStopDate.Enabled = false;
            }

            cboTeamName.DropDownStyle = ComboBoxStyle.DropDownList;

            cboWorkTime.DropDownStyle = ComboBoxStyle.DropDownList;
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

            if (!string.IsNullOrEmpty(strWorkTimeName))
            {
                cboWorkTime.Text = strWorkTimeName;
            }
        }

        /// <summary>
        /// 绑定已有信息
        /// </summary>
        private void bindInfo()
        {
            //掘进面选择按钮文字
            btnChooseWorkFace.Text = jjWorkFaceEntity.WorkingFaceName;
            btnChooseTunnel.Text = tunnelEntity.TunnelName;

            //队别
            cboTeamName.Text = jjWorkFaceEntity.Team.TeamName;

            //开工日期
            dtpStartDate.Value = DateTimeUtil.validateDTPDateTime((System.DateTime)this.jjWorkFaceEntity.StartDate);

            //是否掘进完毕
            if (jjWorkFaceEntity.IsFinish == Const.FINISHED)
            {
                rbtnJJY.Checked = true;
            }
            else
            {
                rbtnJJN.Checked = true;
            }
            //停工日期
            if (jjWorkFaceEntity.IsFinish == Const.FINISHED)
            {
                dtpStopDate.Value = (System.DateTime)jjWorkFaceEntity.StopDate;
            }

            //工作制式
            if (jjWorkFaceEntity.WorkStyle == rbtn38.Text)
            {
                rbtn38.Checked = true;
            }
            else
            {
                rbtn46.Checked = true;
            }
            //班次
            cboWorkTime.Text = jjWorkFaceEntity.WorkTime;
        }

        /// <summary>
        /// 掘进面实体赋值
        /// </summary>
        private void bindTunnelJJEntity()
        {
            if (null == jjWorkFaceEntity || null == tunnelEntity)
            {
                Alert.alert("未选择工作面和巷道");
                return;
            }

            jjWorkFaceEntity.Team = Team.Find(cboTeamName.SelectedValue);
            jjWorkFaceEntity.StartDate = dtpStartDate.Value;
            if (rbtnJJY.Checked)
            {
                jjWorkFaceEntity.IsFinish = Const.FINISHED;
            }
            if (rbtnJJN.Checked)
            {
                jjWorkFaceEntity.IsFinish = Const.NOT_FINISHED;
            }
            if (rbtnJJY.Checked == true)
            {
                jjWorkFaceEntity.StopDate = dtpStopDate.Value;
            }
            if (rbtn38.Checked)
            {
                jjWorkFaceEntity.WorkStyle = rbtn38.Text;
            }
            if (rbtn46.Checked)
            {
                jjWorkFaceEntity.WorkStyle = rbtn46.Text;
            }
            jjWorkFaceEntity.WorkTime = cboWorkTime.Text;
        }

        /// <summary>
        /// 掘进面选择
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnChooseWorkFace_Click(object sender, EventArgs e)
        {
            //工作面选择窗体
            SelectWorkingFaceDlg selectWF = new SelectWorkingFaceDlg();

            //巷道选择完毕
            if (DialogResult.OK == selectWF.ShowDialog())
            {
                //巷道选择按钮Text改变
                btnChooseWorkFace.Text = selectWF.SelectedWorkingFace.WorkingFaceName;
                //实体赋值
                jjWorkFaceEntity = selectWF.SelectedWorkingFace;
            }
        }

        /// <summary>
        /// 巷道选择
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnChooseTunnel_Click(object sender, EventArgs e)
        {
            //巷道选择窗体
            var tunnelChoose = new SelectTunnelDlg();
            //巷道选择完毕
            if (DialogResult.OK != tunnelChoose.ShowDialog()) return;
            //巷道选择按钮Text改变
            btnChooseTunnel.Text = tunnelChoose.SelectedTunnel.TunnelName;
            //实体赋值
            tunnelEntity = tunnelChoose.SelectedTunnel;
        }

        /// <summary>
        /// 提交按钮事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnSubmit_Click(object sender, EventArgs e)
        {
            //验证
            if (!check())
            {
                DialogResult = DialogResult.None;
                return;
            }
            DialogResult = DialogResult.OK;

            //实体赋值
            bindTunnelJJEntity();

            //设置巷道为掘进巷道
            tunnelEntity.TunnelType = TunnelTypeEnum.TUNNELLING;
            jjWorkFaceEntity.Save();
            tunnelEntity.Save();
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

        /// <summary>
        /// 工作制式选择
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void rbtn46_CheckedChanged(object sender, EventArgs e)
        {
            DataBindUtil.LoadWorkTime(cboWorkTime,
                rbtn38.Checked ? Const_MS.WORK_GROUP_ID_38 : Const_MS.WORK_GROUP_ID_46);
            // 设置班次名称
            setWorkTimeName();
        }

        /// <summary>
        /// 验证
        /// </summary>
        /// <returns></returns>
        private bool check()
        {
            //添加特殊验证
            if (this.Text == Const_GM.TUNNEL_JJ_ADD)
            {
                ////是否已为掘进巷道
                //if (TunnelJJBLL.selectTunnelJJ(tunnelEntity))
                //{
                //    Alert.alert(Const_GM.TUNNEL_JJ_MSG_TUNNEL_IS_ALREADY_JJ);
                //    return false;
                //}

            }

            //巷道是否选择
            if (tunnelEntity.TunnelId == 0)
            {
                Alert.alert(Const.MSG_PLEASE_CHOOSE + Const_GM.TUNNEL + Const.SIGN_EXCLAMATION_MARK);
                return false;
            }

            //是否已为回采巷道
            if (tunnelEntity.WorkingFace.WorkingfaceTypeEnum == WorkingfaceTypeEnum.HC)
            {
                Alert.alert(Const_GM.TUNNEL_JJ_MSG_TUNNEL_IS_HC);
                return false;
            }

            //队别非空
            if (!Check.isEmpty(cboTeamName, Const_MS.TEAM_NAME))
            {
                return false;
            }
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

        private void rbtnJJY_CheckedChanged(object sender, EventArgs e)
        {
            if (rbtnJJY.Checked)
            {
                dtpStopDate.Enabled = true;
            }
            else
            {
                dtpStopDate.Enabled = false;
            }
        }
    }
}
