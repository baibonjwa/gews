using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using LibBusiness;
using LibEntity;
using System.IO;

namespace UnderTerminal
{
    public partial class LoginPopup : Form
    {
        private UserLoginInformationEnt[] ents = null;
        
        public LoginPopup(string title)
        {
            InitializeComponent();
            ents = LoginFormBLL.GetUserLoginInformations();
            this.DialogResult = DialogResult.None;
            this.Text = title;
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        /// <summary>
        /// 登录成功
        /// </summary>
        /// <param name="userName">用户名</param>
        /// <param name="password">密码</param>
        /// <returns></returns>
        private bool LoginSuccess(string userName, string password)
        {
            //定义记录登录成功与否的值
            bool isLogin = false;
            int n = ents.Length;
            for (int i = 0; i < n; i++)
            {
                //验证帐号密码是否正确
                if (ents[i].LoginName == userName && ents[i].PassWord == password)
                {
                    CurrentUserEnt.CurLoginUserInfo = ents[i];
                    //记录最后一次登录用户
                    StreamWriter sw = new StreamWriter(Application.StartupPath + "\\DefaultUser.txt", false);
                    sw.WriteLine(userName);
                    sw.Close();

                    //记住密码,登录成功，修改用户“尚未登录”为False；根据是否记住密码设定相应的值
                    LoginFormBLL.RememberPassword(ents[i].LoginName, false);
                    isLogin = true;
                    break;
                }
            }
            return isLogin;
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            if (LoginSuccess(tbUserName.Text, tbPassword.Text))
            {
                this.DialogResult = DialogResult.OK;
                this.Close();
            }
            else
            {
                MessageBox.Show("用户名或密码错误!");
            }
        }
    }
}
