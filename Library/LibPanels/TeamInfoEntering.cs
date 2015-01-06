// ******************************************************************
// 概  述：队别信息添加修改
// 作  者：宋英杰
// 创建日期：2014/3/11
// 版本号：V1.0
// 版本信息：
// V1.0 新建
// ******************************************************************

using System;
using System.Data;
using System.Windows.Forms;
using LibBusiness;
using LibCommon;
using LibEntity;

namespace LibPanels
{
    public partial class TeamInfoEntering : Form
    {
        //队别实体
        TeamInfo teamInfoEntity = new TeamInfo();

        /// <summary>
        /// 构造方法
        /// </summary>
        public TeamInfoEntering()
        {
            InitializeComponent();
            //窗体属性设置
            LibCommon.FormDefaultPropertiesSetter.SetEnteringFormDefaultProperties(this, Const_MS.TEAM_INFO_ADD);
        }

        /// <summary>
        /// 构造方法
        /// </summary>
        /// <param name="teamInfoEntity">队别信息实体</param>
        public TeamInfoEntering(TeamInfo teamInfoEntity)
        {
            this.teamInfoEntity = teamInfoEntity;

            InitializeComponent();

            //绑定修改初始化信息
            bindName();

            //窗体属性设置
            LibCommon.FormDefaultPropertiesSetter.SetEnteringFormDefaultProperties(this, Const_MS.TEAM_INFO_CHANGE);
        }

        //绑定姓名信息
        private void bindName()
        {
            /**队别名称**/
            txtTeamName.Text = teamInfoEntity.TeamName;
            /**队长名称**/
            txtTeamLeader.Text = teamInfoEntity.TeamLeader;
            /**队员名称**/
            if (teamInfoEntity.TeamMember != "")
            {
                string[] teamMember = teamInfoEntity.TeamMember.Split(Const_MS.TEAM_INFO_MEMBER_BREAK_SIGN);

                //队员名称添加到List中
                for (int i = 0; i < teamMember.Length; i++)
                {
                    lstTeamMate.Items.Add(teamMember[i]);
                }
            }
        }

        /// <summary>
        /// 提交添加队别信息
        /// </summary>
        private void submitTeamInfo()
        {
            //队别名称
            teamInfoEntity.TeamName = txtTeamName.Text;
            //队长姓名
            teamInfoEntity.TeamLeader = txtTeamLeader.Text;
            //队员姓名
            for (int i = 0; i < lstTeamMate.Items.Count; i++)
            {
                teamInfoEntity.TeamMember += lstTeamMate.Items[i].ToString() + Const_MS.TEAM_INFO_MEMBER_BREAK_SIGN;
            }
            //删除队员姓名是后一个(,)
            teamInfoEntity.TeamMember = teamInfoEntity.TeamMember.Remove(teamInfoEntity.TeamMember.Length - 1);

            teamInfoEntity.Save();

            Alert.alert(Const_MS.MSG_UPDATE_FAILURE);

        }

        /// <summary>
        /// 提交修改队别信息
        /// </summary>
        private void changeTeamInfo()
        {
            //队别名称
            teamInfoEntity.TeamLeader = txtTeamLeader.Text;
            //队长姓名
            teamInfoEntity.TeamName = txtTeamName.Text;
            //队员姓名
            string teamMember = "";
            for (int i = 0; i < lstTeamMate.Items.Count; i++)
            {
                teamMember += lstTeamMate.Items[i].ToString() + Const_MS.TEAM_INFO_MEMBER_BREAK_SIGN;
            }
            //删除队员姓名最后一个(,)
            teamMember = teamMember.Remove(teamMember.Length - 1);
            teamInfoEntity.TeamMember = teamMember;
            //修改操作
            TeamBll.updateTeamInfo(teamInfoEntity);
        }

        /// <summary>
        /// 提交按钮响应
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnSubmit_Click(object sender, EventArgs e)
        {
            // 验证
            if (!this.check())
            {
                DialogResult = DialogResult.None;
                return;
            }
            DialogResult = DialogResult.OK;

            //添加
            if (this.Text == Const_MS.TEAM_INFO_ADD)
            {
                submitTeamInfo();
            }
            //修改
            if (this.Text == Const_MS.TEAM_INFO_CHANGE)
            {
                changeTeamInfo();
            }
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
            //设置按钮可操作性
            setButtonEnable();
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
                    this.lstTeamMate.Items.Remove(lstTeamMate.SelectedItem);
                    i--;
                }
            }
            //设置按钮可操作性
            setButtonEnable();
        }

        //验证
        private bool check()
        {
            //队别名称非空
            if (!Check.checkSpecialCharacters(txtTeamName, Const_MS.TEAM_NAME))
            {
                return false;
            }
            //队别名称非空
            if (!Check.isEmpty(txtTeamName, Const_MS.TEAM_NAME))
            {
                return false;
            }
            //队别名称不可重复
            //添加时
            if (this.Text == Const_MS.TEAM_INFO_ADD)
            {
                DataSet ds = new DataSet();
                ds = TeamBll.selectTeamInfoByTeamName(txtTeamName.Text);
                if (ds.Tables[0].Rows.Count > 0)
                {
                    Alert.alert(Const_MS.TEAM_INFO_MSG_TEAM_NAME_EXIST);
                    return false;
                }
            }
            //修改时
            else
            {
                if (this.txtTeamName.Text != teamInfoEntity.TeamName)
                {
                    DataSet ds = new DataSet();
                    ds = TeamBll.selectTeamInfoByTeamName(txtTeamName.Text);
                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        Alert.alert(Const_MS.TEAM_INFO_MSG_TEAM_NAME_EXIST);
                        return false;
                    }
                }
            }
            //队长姓名特殊字符
            if (!Check.checkSpecialCharacters(txtTeamLeader, Const_MS.TEAM_LEADER + Const_MS.NAME))
            {
                return false;
            }
            //队长姓名非空
            if (!Check.isEmpty(txtTeamLeader, Const_MS.TEAM_LEADER + Const_MS.NAME))
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
            this.Close();
        }

        /// <summary>
        /// 返回队别名称
        /// </summary>
        /// <returns></returns>
        public string returnTeamName()
        {
            return txtTeamName.Text;
        }

        /// <summary>
        /// 全选
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnSelectAll_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < lstTeamMate.Items.Count; i++)
            {
                lstTeamMate.SetSelected(i, true);
            }
        }

        /// <summary>
        /// 反选
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnInverse_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < lstTeamMate.Items.Count; i++)
            {
                if (lstTeamMate.GetSelected(i))
                {
                    lstTeamMate.SetSelected(i, false);
                }
                else
                {
                    lstTeamMate.SetSelected(i, true);
                }
            }
        }

        /// <summary>
        /// 初始化
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TeamInfo_Load(object sender, EventArgs e)
        {
            //设置按钮可操作性
            setButtonEnable();
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
            if (lstTeamMate.SelectedItems.Count > 0)
            {
                btnDel.Enabled = true;
            }
            //未选择
            else
            {
                btnDel.Enabled = false;
            }
        }

        /// <summary>
        /// 设置按钮可操作性
        /// </summary>
        private void setButtonEnable()
        {
            if (lstTeamMate.SelectedItems.Count > 0)
            {
                btnDel.Enabled = true;
            }
            else
            {
                btnDel.Enabled = false;
            }
            if (lstTeamMate.Items.Count > 0)
            {
                btnSelectAll.Enabled = true;
                btnInverse.Enabled = true;
            }
            else
            {
                btnSelectAll.Enabled = false;
                btnInverse.Enabled = false;
            }
        }
    }
}
