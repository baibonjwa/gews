using System;
using System.Windows.Forms;
using LibBusiness;
using LibCommon;
using LibEntity;

namespace UnderTerminal
{
    public partial class SetDefaultValue : Form
    {
        private readonly UnderMessageWindow _superForm;

        public SetDefaultValue(UnderMessageWindow form)
        {
            InitializeComponent();
            _superForm = form;
            DataBindUtil.LoadTeam(cboTeamName);
            DataBindUtil.LoadWorkTime(cboWorkTime,
                rbtn38.Checked ? Const_MS.WORK_GROUP_ID_38 : Const_MS.WORK_GROUP_ID_46);
            if (WorkingTimeDefault.FindFirst().DefaultWorkTimeGroupId == Const_MS.WORK_GROUP_ID_38)
            {
                rbtn38.Checked = true;
            }
            else
            {
                rbtn46.Checked = true;
            }
            cboWorkTime.Text =
                DataBindUtil.JudgeWorkTimeNow(rbtn38.Checked ? Const_MS.WORK_TIME_38 : Const_MS.WORK_TIME_46);
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
        ///     返回班次名
        /// </summary>
        /// <returns>班次名</returns>
        private void btnOK_Click(object sender, EventArgs e)
        {
            _superForm.DefaultWorkTime = cboWorkTime.SelectedItem != null ? ((WorkingTime)cboWorkTime.SelectedItem).WorkTimeName : "";
            _superForm.DefaultWorkStyle = rbtn38.Checked ? rbtn38.Text : rbtn46.Text;
            _superForm.DefaultTeamName = cboTeamName.SelectedItem != null ? ((Team)cboTeamName.SelectedItem).TeamName : "";
            _superForm.DefaultSubmitter = cboSubmitter.Text;
            _superForm.Team = (Team)cboTeamName.SelectedItem;
            _superForm.Submitter = cboSubmitter.Text;
            _superForm.RefreshDefaultValue();
            Close();
        }
    }
}