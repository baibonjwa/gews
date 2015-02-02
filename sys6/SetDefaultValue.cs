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
            this.bindWorkTimeFirstTime();
            if (WorkTimeBLL.getDefaultWorkTime() == Const_MS.WORK_TIME_38)
            {
                rbtn38.Checked = true;
            }
            else
            {
                rbtn46.Checked = true;
            }
            cboWorkTime.Text = workTime(rbtn38.Checked ? Const_MS.WORK_TIME_38 : Const_MS.WORK_TIME_46);

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

        private void rbtn38_CheckedChanged(object sender, EventArgs e)
        {
            if (rbtn38.Checked)
            {

                cboWorkTime.Text = "";
                cboWorkTime.Items.Clear();
                DataSet dsWorkTime = WorkTimeBLL.returnWorkTime(rbtn38.Text);
                for (int i = 0; i < dsWorkTime.Tables[0].Rows.Count; i++)
                {
                    cboWorkTime.Items.Add(dsWorkTime.Tables[0].Rows[i][WorkTimeDbConstNames.WORK_TIME_NAME].ToString());
                }
            }
            else
            {
                cboWorkTime.Text = "";
                cboWorkTime.Items.Clear();
                DataSet dsWorkTime = WorkTimeBLL.returnWorkTime(rbtn46.Text);
                for (int i = 0; i < dsWorkTime.Tables[0].Rows.Count; i++)
                {
                    cboWorkTime.Items.Add(dsWorkTime.Tables[0].Rows[i][WorkTimeDbConstNames.WORK_TIME_NAME].ToString());
                }
            }
        }

        private void cboTeamName_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.bindTeamMember();
        }

        //绑定填报人
        private void bindTeamMember()
        {
            cboSubmitter.Items.Clear();
            cboSubmitter.Text = "";
            DataSet ds = TeamBll.selectTeamInfoByTeamName(cboTeamName.Text);
            string teamLeader = ds.Tables[0].Rows[0][TeamDbConstNames.TEAM_LEADER].ToString();
            string[] teamMember = ds.Tables[0].Rows[0][TeamDbConstNames.TEAM_MEMBER].ToString().Split(',');
            cboSubmitter.Items.Add(teamLeader);
            for (int i = 0; i < teamMember.Length; i++)
            {
                cboSubmitter.Items.Add(teamMember[i]);
            }
        }


        /// <summary>
        /// 返回班次名
        /// </summary>
        /// <param name="workStyle">工作制式名</param>
        /// <returns>班次名</returns>
        public static string workTime(string workStyle)
        {
            DataSet ds = WorkTimeBLL.returnWorkTime(workStyle);
            int hour = DateTime.Now.Hour;
            string workTime = "";
            for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
            {
                if (hour > Convert.ToInt32(ds.Tables[0].Rows[i][WorkTimeDbConstNames.WORK_TIME_FROM].ToString().Remove(2)) && hour <= Convert.ToInt32(ds.Tables[0].Rows[i][WorkTimeDbConstNames.WORK_TIME_TO].ToString().Remove(2)))
                {
                    workTime = ds.Tables[0].Rows[i][WorkTimeDbConstNames.WORK_TIME_NAME].ToString();
                }
            }
            return workTime;
        }

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
