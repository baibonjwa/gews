using System;
using System.Windows.Forms;
using LibCommon;
using LibEntity;

namespace LibPanels
{
    public partial class TeamManagement : Form
    {

        /// <summary>
        /// 构造方法
        /// </summary>
        public TeamManagement()
        {
            InitializeComponent();
            //窗体属性设置
            FormDefaultPropertiesSetter.SetManagementFormDefaultProperties(this, Const_MS.TEAM_INFO_MANAGEMENT);
        }

        private void RefreshData()
        {
            gcTeam.DataSource = Team.FindAll();
        }


        /// <summary>
        /// 初始化
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TeamManagement_Load(object sender, EventArgs e)
        {
            RefreshData();
        }


        /// <summary>
        /// 添加按钮响应
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tsBtnAdd_Click(object sender, EventArgs e)
        {
            var d = new TeamInfoEntering();
            if (DialogResult.OK == d.ShowDialog())
            {
                RefreshData();
            }

        }

        /// <summary>
        /// 修改按钮响应
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tsBtnModify_Click(object sender, EventArgs e)
        {
            var teamEntity = (Team)gridView1.GetFocusedRow();
            var teamInfoForm = new TeamInfoEntering(teamEntity);
            if (DialogResult.OK == teamInfoForm.ShowDialog())
            {
                RefreshData();
            }
        }

        /// <summary>
        /// 删除按钮响应
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tsBtnDel_Click(object sender, EventArgs e)
        {
            //确定
            if (!Alert.confirm(Const.DEL_CONFIRM_MSG)) return;
            var teamEntity = (Team)gridView1.GetFocusedRow();
            teamEntity.Delete();
            RefreshData();
        }

        /// <summary>
        /// 刷新按钮响应
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tsBtnRefresh_Click(object sender, EventArgs e)
        {
            RefreshData();
        }

        /// <summary>
        /// 退出按钮响应
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tsBtnExit_Click(object sender, EventArgs e)
        {
            Close();
        }

        /// <summary>
        /// 导出按钮事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tsBtnExport_Click(object sender, EventArgs e)
        {
            if (saveFileDialog1.ShowDialog() == DialogResult.OK)
            {
                gcTeam.ExportToXls(saveFileDialog1.FileName);
            }
        }

        /// <summary>
        /// 打印
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tsBtnPrint_Click(object sender, EventArgs e)
        {
            DevUtil.DevPrint(gcTeam, "队别信息报表");
        }
    }
}
