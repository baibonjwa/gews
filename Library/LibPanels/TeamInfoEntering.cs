using System;
using System.Windows.Forms;
using LibCommon;
using LibEntity;

namespace LibPanels
{
    public partial class TeamInfoEntering : Form
    {
        private Team Team { get; set; }

        public TeamInfoEntering()
        {
            InitializeComponent();
            //窗体属性设置
            FormDefaultPropertiesSetter.SetEnteringFormDefaultProperties(this, Const_MS.TEAM_INFO_ADD);
        }

        /// <summary>
        /// 构造方法
        /// </summary>
        /// <param name="team">队别信息实体</param>
        public TeamInfoEntering(Team team)
        {
            InitializeComponent();
            Team = team;
            txtTeamName.Text = team.TeamName;
            txtTeamLeader.Text = team.TeamLeader;
            if (team.TeamMember != "")
            {
                string[] teamMember = team.TeamMember.Split(Const_MS.TEAM_INFO_MEMBER_BREAK_SIGN);
                foreach (string t in teamMember)
                {
                    lstTeamMate.Items.Add(t);
                }
            }
            FormDefaultPropertiesSetter.SetEnteringFormDefaultProperties(this, Const_MS.TEAM_INFO_CHANGE);
        }

        /// <summary>
        /// 提交按钮响应
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnSubmit_Click(object sender, EventArgs e)
        {
            // 验证
            if (!Check())
            {
                DialogResult = DialogResult.None;
                return;
            }
            var team = Team;
            if (team == null)
            {
                team = new Team { TeamName = txtTeamName.Text, TeamLeader = txtTeamLeader.Text };
                foreach (var t in lstTeamMate.Items)
                {
                    team.TeamMember += t + ",";
                }
                //删除队员姓名是后一个(,)
                team.TeamMember = team.TeamMember.Remove(team.TeamMember.Length - 1);

            }
            else
            {
                team.TeamName = txtTeamName.Text;
                team.TeamLeader = txtTeamLeader.Text;
                team.TeamMember = "";
                foreach (var t in lstTeamMate.Items)
                {
                    team.TeamMember += t + ",";
                }
                team.TeamMember = team.TeamMember.Remove(team.TeamMember.Length - 1);
            }
            team.Save();
            Alert.alert("提交成功");
            DialogResult = DialogResult.OK;
        }

        /// <summary>
        /// 添加按钮响应
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnAdd_Click(object sender, EventArgs e)
        {
            //请输入姓名窗体
            InputName d = new InputName();
            if (DialogResult.OK == d.ShowDialog())
            {
                //姓名
                string name = d.returnName();
                if (name != Const.DATA_NULL)
                {
                    lstTeamMate.Items.Add(name);
                }
            }
        }
        /// <summary>
        /// 删除按钮响应
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnDel_Click(object sender, EventArgs e)
        {
            //删除姓名列表中的姓名
            for (int i = 0; i < lstTeamMate.Items.Count; i++)
            {
                if (lstTeamMate.GetSelected(i))
                {
                    lstTeamMate.Items.Remove(lstTeamMate.SelectedItem);
                    i--;
                }
            }
        }

        //验证
        private bool Check()
        {
            //队别名称非空
            if (!LibCommon.Check.checkSpecialCharacters(txtTeamName, Const_MS.TEAM_NAME))
            {
                return false;
            }
            //队别名称非空
            if (!LibCommon.Check.isEmpty(txtTeamName, Const_MS.TEAM_NAME))
            {
                return false;
            }
            //队长姓名特殊字符
            if (!LibCommon.Check.checkSpecialCharacters(txtTeamLeader, Const_MS.TEAM_LEADER + Const_MS.NAME))
            {
                return false;
            }
            //队长姓名非空
            if (!LibCommon.Check.isEmpty(txtTeamLeader, Const_MS.TEAM_LEADER + Const_MS.NAME))
            {
                return false;
            }
            //队员数量
            if (lstTeamMate.Items.Count == 0)
            {
                Alert.alert(Const_MS.TEAM_INFO_MSG_NEED_AT_LEAST_ONE);
                return false;
            }
            return true;
        }

        /// <summary>
        /// 取消按钮响应
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnCancel_Click(object sender, EventArgs e)
        {
            //窗体关闭
            Close();
        }

        /// <summary>
        /// 队员列表选择改变事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void lstTeamMate_SelectedValueChanged(object sender, EventArgs e)
        {
            //队员列表选择项改变时
            //选择数>0
            btnDel.Enabled = lstTeamMate.SelectedItems.Count > 0;
        }
    }
}
