using System;
using System.Windows.Forms;
using LibCommon;

namespace LibCommonForm
{
    public partial class UserGroupInformationManagement : Form
    {


        /// <summary>
        /// 默认构造函数
        /// </summary>
        public UserGroupInformationManagement()
        {
            InitializeComponent();

            //设置窗体和farpoint格式
            FormDefaultPropertiesSetter.SetManagementFormDefaultProperties(this, LibFormTitles.USER_GROUP_INFO_MANMAGEMENT);
        }

        /// <summary>
        /// 添加
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tsBtnAdd_Click(object sender, EventArgs e)
        {
            UserGroupInformationInput ugi = new UserGroupInformationInput();
            ugi.ShowDialog();
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
        /// 删除
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tsBtnDel_Click(object sender, EventArgs e)
        {

        }

        /// <summary>
        /// 刷新
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
            Close();
        }

        /// <summary>
        /// 窗体登陆
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void UserGroupInformationManagement_Load(object sender, EventArgs e)
        {

        }
    }
}
