// ******************************************************************
// 概  述：掘进面信息添加修改
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
using LibCommon;
using LibEntity;
using LibBusiness;
//using _2.MiningScheduling;
using LibBusiness.CommonBLL;
using LibCommonControl;
using LibCommonForm;

namespace _3.GeologyMeasure
{
    public partial class TunnelJJEntering : BaseForm
    {
        /************变量定义*****************/
        // 掘进面
        WorkingFace jjWorkFaceEntity = null;
        // 巷道
        Tunnel tunnelEntity = null;
        /*************************************/

        /// <summary>
        /// 构造方法
        /// </summary>
        public TunnelJJEntering(MainFrm mainFrm)
        {
            this.MainForm = mainFrm;
            InitializeComponent();

            //默认选择未掘进完毕
            rbtnJJN.Checked = true;

            //默认工作制式选择
            if (WorkTimeBLL.getDefaultWorkTime() == Const_MS.WORK_TIME_38)
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
            LibCommon.FormDefaultPropertiesSetter.SetEnteringFormDefaultProperties(this, Const_GM.TUNNEL_JJ_ADD);
        }

        /// <summary>
        /// 构造方法
        /// </summary>
        /// <param name="tunnelJJEntity">掘进巷道实体</param>
        public TunnelJJEntering(WorkingFace wfEntity, Tunnel tunnelEntity, MainFrm mainFrm)
        {
            this.MainForm = mainFrm;

            this.tunnelEntity = tunnelEntity;
            this.jjWorkFaceEntity = wfEntity;

            InitializeComponent();

            //绑定修改信息

            //窗体属性设置
            LibCommon.FormDefaultPropertiesSetter.SetEnteringFormDefaultProperties(this, Const_GM.TUNNEL_JJ_CHANGE);
        }

        /// <summary>
        /// 初始化
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TunnelJJHC_Load(object sender, EventArgs e)
        {
            //绑定队别名称
            bindTeamInfo();

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

            if (strWorkTimeName != null && strWorkTimeName != "")
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
            cboTeamName.Text = BasicInfoManager.getInstance().getTeamNameById(jjWorkFaceEntity.TeamNameID);

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

            jjWorkFaceEntity.TeamNameID = Convert.ToInt32(cboTeamName.SelectedValue);
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
            SelectWorkingFaceDlg selectWF = new SelectWorkingFaceDlg(WorkingfaceTypeEnum.JJ, WorkingfaceTypeEnum.OTHER);

            //巷道选择完毕
            if (DialogResult.OK == selectWF.ShowDialog())
            {
                //巷道选择按钮Text改变
                btnChooseWorkFace.Text = selectWF.workFaceName;
                //实体赋值
                jjWorkFaceEntity = BasicInfoManager.getInstance().getWorkingFaceById(selectWF.workFaceId);
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
            SelectTunnelDlg tunnelChoose;

            tunnelChoose = new SelectTunnelDlg(this.MainForm, TunnelTypeEnum.TUNNELLING, TunnelTypeEnum.OTHER);

            //巷道选择完毕
            if (DialogResult.OK == tunnelChoose.ShowDialog())
            {
                //巷道选择按钮Text改变
                btnChooseTunnel.Text = tunnelChoose.tunnelName;
                //实体赋值
                tunnelEntity = BasicInfoManager.getInstance().getTunnelByID(tunnelChoose.tunnelId);
            }
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
            TunnelInfoBLL.setTunnelAsJJ(jjWorkFaceEntity, tunnelEntity);
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
            if (tunnelEntity.TunnelID == 0)
            {
                Alert.alert(Const.MSG_PLEASE_CHOOSE + Const_GM.TUNNEL + Const.SIGN_EXCLAMATION_MARK);
                return false;
            }

            //是否已为回采巷道
            if (TunnelInfoBLL.isTunnelHC(tunnelEntity))
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
