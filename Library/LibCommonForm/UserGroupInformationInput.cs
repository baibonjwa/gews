using System;
using System.Windows.Forms;
using LibCommon;

namespace LibCommonForm
{
    public partial class UserGroupInformationInput : Form
    {

        /// <summary>
        /// 构造函数
        /// </summary>
        public UserGroupInformationInput()
        {
            InitializeComponent();
            FormDefaultPropertiesSetter.SetEnteringFormDefaultProperties(this, LibFormTitles.USER_GROUP_INFO_MANMAGEMENT_ADD);
        }

        /// <summary>
        /// 确定
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonOK_Click(object sender, EventArgs e)
        {

            Close();
        }

        /// <summary>
        /// 取消
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonCancel_Click(object sender, EventArgs e)
        {
            Close();
        }

        /// <summary>
        /// 窗体load
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void UserGroupInformationInput_Load(object sender, EventArgs e)
        {

        }

        /// <summary>
        /// 选择的用户信息改变,用以检测是否已选择用户,设置按钮启用与否
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void _dgrvdUserInfo_SelectionChanged(object sender, EventArgs e)
        {
            _btnSelAdding.Enabled = _dgrvdUserInfo.SelectedRows.Count > 0;
        }

        /// <summary>
        /// 添加用户到已选择列表中
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void _btnSelAdding_Click(object sender, EventArgs e)
        {

        }

        /// <summary>
        /// 移除
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void _btnSelReduce_Click(object sender, EventArgs e)
        {

        }

        /// <summary>
        /// 选择的用户信息改变,用以检测是否已选择用户,设置按钮启用与否
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void _lstSelUserName_SelectedIndexChanged(object sender, EventArgs e)
        {
            _btnSelReduce.Enabled = _lstSelUserName.SelectedItems.Count > 0;
        }
    }
}
