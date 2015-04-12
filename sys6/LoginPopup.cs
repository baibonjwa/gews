using System;
using System.IO;
using System.Windows.Forms;
using LibEntity;

namespace UnderTerminal
{
    public partial class LoginPopup : Form
    {
        private readonly UserLogin[] ents;

        public LoginPopup(string title)
        {
            InitializeComponent();
            ents = UserLogin.FindAll();
            DialogResult = DialogResult.None;
            Text = title;
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            Close();
        }

        /// <summary>
        ///     登录成功
        /// </summary>
        /// <param name="userName">用户名</param>
        /// <param name="password">密码</param>
        /// <returns></returns>
        private bool LoginSuccess(string userName, string password)
        {
            //定义记录登录成功与否的值
            var isLogin = false;
            var n = ents.Length;
            for (var i = 0; i < n; i++)
            {
                //验证帐号密码是否正确
                if (ents[i].LoginName == userName && ents[i].PassWord == password)
                {
                    CurrentUser.CurLoginUserInfo = ents[i];
                    //记录最后一次登录用户
                    var sw = new StreamWriter(Application.StartupPath + "\\DefaultUser.txt", false);
                    sw.WriteLine(userName);
                    sw.Close();

                    //记住密码,登录成功，修改用户“尚未登录”为False；根据是否记住密码设定相应的值
                    var userLogin = UserLogin.FindOneByLoginName(userName);
                    userLogin.IsSavePassWord = 0;
                    userLogin.Save();
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
                DialogResult = DialogResult.OK;
                Close();
            }
            else
            {
                MessageBox.Show("用户名或密码错误!");
            }
        }
    }
}