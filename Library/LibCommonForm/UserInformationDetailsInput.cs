using System;
using System.Windows.Forms;
using LibEntity;

namespace LibCommonForm
{
    public partial class UserInformationDetailsInput : Form
    {
        /// <summary>
        /// 传入实体，用于实例化时判断为添加、修改。若实体为空，则认为是添加。
        /// </summary>
        /// <param name="ent">用户详细信息实体</param>
        public UserInformationDetailsInput(UserInformationDetails ent)
        {
            InitializeComponent();
        }

        /// <summary>
        /// 点击确定按钮出发的事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonOK_Click(object sender, EventArgs e)
        {

        }

    }
}
