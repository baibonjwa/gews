using System;
using System.Windows.Forms;

namespace UnderTerminal
{
    public partial class SysMgr : Form
    {
        public SysMgr()
        {
            InitializeComponent();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            if (tbUserName.Text == "admin" && tbPassword.Text == "Jcbsc8860")
            {
                Application.Exit();
            }
            else
            {
                MessageBox.Show("用户名或密码错误!");
            }
        }
    }
}