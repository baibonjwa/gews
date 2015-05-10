using System;
using System.Windows.Forms;
using LibCommon;

namespace LibCommonForm
{
    public partial class UserInformationDetailsManagement : Form
    {

        public UserInformationDetailsManagement()
        {
            InitializeComponent();
            //设置窗体、farpoint格式
            FormDefaultPropertiesSetter.SetMdiChildrenManagementFormDefaultProperties(this, LibFormTitles.USER_INFO_DETAILS_MANMAGEMENT);

        }

        /// <summary>
        /// 打印
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tsBtnPrint_Click(object sender, EventArgs e)
        {
        }

        /// <summary>
        /// 导出
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tsBtnExport_Click(object sender, EventArgs e)
        {

        }

        /// <summary>
        /// 添加
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tsBtnAdd_Click(object sender, EventArgs e)
        {
            //添加,实例化方法中，null则认为是添加
            UserInformationDetailsInput uidi = new UserInformationDetailsInput(null);
            uidi.ShowDialog();
            //重新获取用户信息到显示界面

        }

        /// <summary>
        /// 修改
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tsBtnModify_Click(object sender, EventArgs e)
        {

        }

        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tsBtnDel_Click(object sender, EventArgs e)
        {

        }

        /// <summary>
        /// 刷新。从数据库中重新读取数据
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tsBtnRefresh_Click(object sender, EventArgs e)
        {
        }

        /// <summary>
        /// 退出
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tsBtnExit_Click(object sender, EventArgs e)
        {

        }

        /// <summary>
        /// 窗体登陆
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void UserInformationDetailsManagement_Load(object sender, EventArgs e)
        {

        }
    }
}
