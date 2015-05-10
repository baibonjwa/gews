// ******************************************************************
// 概  述：人员详细信息管理
// 作  者：秦凯
// 创建日期：2014/03/16
// 版本号：V1.0
// 版本信息:
// V1.0 新建
// ******************************************************************
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace LibCommonForm
{
    public partial class UserInformationDetailsManagementFather : Form
    {
        //人员信息子窗体
        UserInformationDetailsManagement uidm = new UserInformationDetailsManagement();

        /// <summary>
        /// 构造函数
        /// </summary>
        public UserInformationDetailsManagementFather()
        {
            InitializeComponent();
            //设置子窗体格式
            LibCommon.FormDefaultPropertiesSetter.SetManagementFormDefaultProperties(this, LibCommon.LibFormTitles.USER_INFO_DETAILS_MANMAGEMENT);
        }

        /// <summary>
        /// 窗体登录事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void UserInformationDetailsManagementFather_Load(object sender, EventArgs e)
        {

            //设置窗体能否接收子窗体
            this.IsMdiContainer = true;
            //设置窗体的子窗体
            uidm.MdiParent = this;
            //添加窗体
            panelFather.Controls.Add(uidm);
            //设置显示格式
            uidm.WindowState = FormWindowState.Maximized;
            //uidm.Dock = DockStyle.Fill;
            uidm.Anchor =( AnchorStyles.Left | AnchorStyles.Right| AnchorStyles.Top| AnchorStyles.Bottom);
            uidm.Show();
            uidm.Activate();
        }

        /// <summary>
        /// 捕捉按Enter事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void UserInformationDetailsManagementFather_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar==13)
            {
                this.Close();
            }             
        }

        /// <summary>
        /// 关闭窗体的事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void uidm_OnButtonClickHandle(object sender, EventArgs e)
        {
            //关闭窗体
            this.Close();
        }
    }
}
