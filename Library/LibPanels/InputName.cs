// ******************************************************************
// 概  述：队别成员姓名添加修改
// 作  者：宋英杰
// 创建日期：2014/3/11
// 版本号：V1.0
// 版本信息：
// V1.0 新建
// ******************************************************************

using System;
using System.Windows.Forms;
using LibCommon;

namespace LibPanels
{
    public partial class InputName : Form
    {
        /// <summary>
        /// 构造方法
        /// </summary>
        public InputName()
        {
            InitializeComponent();
            LibCommon.FormDefaultPropertiesSetter.SetEnteringFormDefaultProperties(this, Const_MS.INPUT_NAME);
        }

        /// <summary>
        /// 初始化
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void InputName_Load(object sender, EventArgs e)
        {
            //设置焦点
            this.txtTeamMemberName.Focus();
        }

        /// <summary>
        /// 确定按钮事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnOK_Click(object sender, EventArgs e)
        {
            //验证
            if (!check())
            {
                this.DialogResult = DialogResult.None;
                return;
            }
            this.DialogResult = DialogResult.OK;
        }

        /// <summary>
        /// 取消按钮事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnCancel_Click(object sender, EventArgs e)
        {
            //关闭窗体
            this.Close();
        }

        /// <summary>
        /// 返回队员姓名
        /// </summary>
        /// <returns>队员姓名</returns>
        public string returnName()
        {
            string a = txtTeamMemberName.Text.Trim();
            return a;
        }

        /// <summary>
        /// 验证
        /// </summary>
        /// <returns></returns>
        private bool check()
        {
            //队员姓名特殊字符验证
            if (!Check.checkSpecialCharacters(txtTeamMemberName, Const_MS.TEAM_MEMBER + Const_MS.NAME))
            {
                return false;
            }
            //队员姓名非空验证
            if (!Check.isEmpty(txtTeamMemberName, Const_MS.TEAM_MEMBER + Const_MS.NAME))
            {
                return false;
            }
            return true;
        }
    }
}
