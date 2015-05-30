using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using System.IO;
using LibCommon;

namespace LibCommonForm
{
    public partial class PasswordUpdate : Form
    {
        //声明api
        [DllImport("kernel32")]
        private static extern bool GetPrivateProfileString(string lpApplicationName, string lpKeyName, string lpDefault, StringBuilder lpReturnedString, int nSize, string lpFileName);
        [DllImport("kernel32")]
        private static extern bool WritePrivateProfileString(string lpAppName, string lpKeyName, string lpString, string lpFileName);

        private const string _DATABASE_CONFIG_FILE_NAME = "UserInfo.ini";
        public StringBuilder _UID = new StringBuilder(256);
        public StringBuilder _PWD = new StringBuilder(256);

        public string _TITLE = "UserInfo";
        public string _CONFIG_PATH;

        // 解密后的UID
        private string _strDSUID;
        // 解密后的PWD
        private string _strDSPWD;

        // 加密后的UID
        private string _strESUID;
        // 加密后的PWD
        private string _strESPWD;

        /// <summary>
        /// 构造方法
        /// </summary>
        public PasswordUpdate()
        {
            InitializeComponent();

            try
            {
                _CONFIG_PATH = Application.StartupPath + "\\" + _DATABASE_CONFIG_FILE_NAME;
                if (!File.Exists(_CONFIG_PATH))
                {
                    MessageBox.Show("无法找到数据库配置文件:" + _DATABASE_CONFIG_FILE_NAME);
                } else {
                    GetPrivateProfileString(_TITLE, "UID", "NULL", _UID, _UID.Capacity, _CONFIG_PATH);
                    GetPrivateProfileString(_TITLE, "PWD", "NULL", _PWD, _PWD.Capacity, _CONFIG_PATH);
                    // 解密
                    _strDSUID = LibEncryptDecrypt.DWEncryptDecryptClass.DecryptString(_UID.ToString());
                    _strDSPWD = LibEncryptDecrypt.DWEncryptDecryptClass.DecryptString(_PWD.ToString());
                    this.txtUserName.Text = _strDSUID;
                    //this.txtUserName.Text = _UID.ToString();
                }
            }
            catch (System.Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        /// <summary>
        /// 修改
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnUpdate_Click(object sender, EventArgs e)
        {
            if (!File.Exists(_CONFIG_PATH))
            {
                MessageBox.Show("无法找到数据库配置文件:" + _DATABASE_CONFIG_FILE_NAME);
                return;
            }

            // 验证
            if (!this.check())
            {
                this.DialogResult = DialogResult.None;
                return;
            }
            this.DialogResult = DialogResult.OK;

            _UID =  new StringBuilder(this.txtUserName.Text.Trim());
            _PWD = new StringBuilder(this.txtNewPassword1.Text.Trim());

            try
            {
                // 加密
                _strESUID = LibEncryptDecrypt.DWEncryptDecryptClass.EncryptString(_UID.ToString());
                _strESPWD = LibEncryptDecrypt.DWEncryptDecryptClass.EncryptString(_PWD.ToString());

                WritePrivateProfileString(_TITLE, "UID", _strESUID, _CONFIG_PATH);
                WritePrivateProfileString(_TITLE, "PWD", _strESPWD, _CONFIG_PATH);
            }
            catch (System.Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

            Alert.alert("密码修改成功！");

        }

        /// <summary>
        /// 取消
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnCancel_Click(object sender, EventArgs e)
        {
            // 关闭窗口
            this.Close();
        }

        /// <summary>
        /// 验证画面录入数据
        /// </summary>
        /// <returns>验证结果：true 通过验证, false未通过验证</returns>
        private bool check()
        {
            this.errorProvider1.Clear();
            this.errorProvider2.Clear();
            this.errorProvider3.Clear();
            this.errorProvider4.Clear();

            bool result = true;
            //// 用户名
            //if (this.txtUserName.Text.Trim() == "")
            //{
            //    this.errorProvider1.SetError(this.txtUserName, "不能为空！");
            //    result = false;
            //}

            //// 旧密码
            //if (this.txtOldPassword.Text.Trim() == "")
            //{
            //    this.errorProvider2.SetError(this.txtOldPassword, "不能为空！");
            //    result = false;
            //}

            //// 新密码1
            //if (this.txtNewPassword1.Text.Trim() == "")
            //{
            //    this.errorProvider3.SetError(this.txtNewPassword1, "不能为空！");
            //    result = false;
            //}

            //// 新密码2
            //if (this.txtNewPassword2.Text.Trim() == "")
            //{
            //    this.errorProvider4.SetError(this.txtNewPassword2, "不能为空！");
            //    result = false;
            //}

            // 旧密码验证
            if (this.txtOldPassword.Text.Trim() != _strDSPWD)
            {
                this.errorProvider2.SetError(this.txtOldPassword, "当前密码输入错误！");
                result = false;
            }

            // 两次输入密码不一致
            if (this.txtNewPassword1.Text.Trim() != this.txtNewPassword2.Text.Trim())
            {
                this.errorProvider4.SetError(this.txtNewPassword2, "两次输入的密码不一致！");
                result = false;
            }

            // 验证通过
            return result;
        }
    }
}
