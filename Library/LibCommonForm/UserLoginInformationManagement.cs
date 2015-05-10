// ******************************************************************
// 概  述：用户登录信息管理
// 作  者：秦凯
// 创建日期：2014/03/07
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
using LibEntity;
using LibBusiness;
using LibCommon;

namespace LibCommonForm
{
    public partial class UserLoginInformationManagement : Form
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        public UserLoginInformationManagement()
        {
            InitializeComponent();
            //设置窗体和farpoint格式
            FormDefaultPropertiesSetter.SetManagementFormDefaultProperties(this, LibFormTitles.USER_LOGIN_INFO_MANMAGEMENT);

        }

        /// <summary>
        /// 窗体登陆时触发，清除_userSel中的值，加载数据，设置按钮是否启用
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void UserLoginInformationManagement_Load(object sender, EventArgs e)
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
            UserLoginInformationInput ulii = new UserLoginInformationInput(null);
            ulii.ShowDialog();
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
        /// 刷新后，重新load数据
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
    }
}
