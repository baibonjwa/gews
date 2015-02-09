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
using Steema.TeeChart.Functions;

namespace UnderTerminal
{
    public partial class SetDefaultValue : Form
    {
        private UnderMessageWindow superForm;
        public SetDefaultValue(UnderMessageWindow form)
        {
            InitializeComponent();
            superForm = form;
            addInfo();
        }


        /// <summary>
        /// 添加时初始化
        /// </summary>
        private void addInfo()
        {
            this.bindTeamInfo();
            DataBindUtil.LoadWorkTime(cboWorkTime,
      rbtn38.Checked ? Const_MS.WORK_GROUP_ID_38 : Const_MS.WORK_GROUP_ID_46);
            if (WorkTimeBLL.getDefaultWorkTime() == Const_MS.WORK_TIME_38)
            {
                rbtn38.Checked = true;
            }
            else
            {
                rbtn46.Checked = true;
            }
            cboWorkTime.Text = DataBindUtil.JudgeWorkTimeNow(rbtn38.Checked ? Const_MS.WORK_TIME_38 : Const_MS.WORK_TIME_46);

        }

        /// <summary>
        /// 绑定队别名称
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

        private void rbtn38_CheckedChanged(object sender, EventArgs e)
        {
            cboWorkTime.Text = "";
            cboWorkTime.Items.Clear();
            DataBindUtil.LoadWorkTime(cboWorkTime,
                rbtn38.Checked ? Const_MS.WORK_GROUP_ID_38 : Const_MS.WORK_GROUP_ID_46);
        }

        private void cboTeamName_SelectedIndexChanged(object sender, EventArgs e)
        {
            DataBindUtil.LoadTeamMemberByTeamName(cboSubmitter, cboTeamName.Text);
        }


        /// <summary>
        /// 返回班次名
        /// </summary>
        /// <param name="workStyle">工作制式名</param>
        /// <returns>班次名</returns>

        private void btnOK_Click(object sender, EventArgs e)
        {
            superForm.DefaultWorkTime = cboWorkTime.SelectedItem != null ? cboWorkTime.SelectedItem.ToString() : "";
            superForm.DefaultWorkStyle = rbtn38.Checked ? rbtn38.Text : rbtn46.Text;
            superForm.DefaultTeamName = cboTeamName.SelectedItem != null ? cboTeamName.SelectedItem.ToString() : "";
            superForm.DefaultSubmitter = cboSubmitter.SelectedItem != null ? cboSubmitter.SelectedItem.ToString() : "";
            superForm.RefreshDefaultValue();

            this.Close();
        }
    }
}
