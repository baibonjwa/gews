using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Runtime.InteropServices;
using LibDatabase;
using LibCommon;

namespace LibCommonForm
{    
    public partial class DatabaseManagement : Form
    {
        /**   **/
        private LibDatabase.ManageDataBase db;

        /// <summary>
        /// 构造方法
        /// </summary>
        //public DatabaseManagement()
        //{
        //    InitializeComponent();
        //}

        /// <summary>
        /// 带参数的构造方法
        /// </summary>
        public DatabaseManagement(LibDatabase.DATABASE_TYPE type)
        {
            InitializeComponent();
            db = new ManageDataBase(type);
            // 服务器名称或IP
            this.txtServerNameOrIP.Text = db.DataSource.ToString();
            // 数据库名
            this.txtDataBaseName.Text = db.DataBase.ToString();
            // 登录名
            this.txtLoginName.Text = db.strID.ToString();
            // 登录密码
            this.txtPassword.Text = db.strPW.ToString();

        }

        /// <summary>
        /// 测试连接
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnConn_Click(object sender, EventArgs e)
        {
            // 验证
            if (this.check())
            {
                db.DataSource = txtServerNameOrIP.Text;
                db.DataBase = txtDataBaseName.Text;
                db.strID = txtLoginName.Text;
                db.strPW = txtPassword.Text;

                if (db.TestLink())
                {
                    Alert.alert("测试连接成功！");
                }
                else
                {
                    Alert.alert("测试连接失败！");
                }
            }
        }

        /// <summary>
        /// 重置
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnReset_Click(object sender, EventArgs e)
        {
            // 服务器名称或IP
            this.txtServerNameOrIP.Text = db.DataSource.ToString();
            // 数据库名
            this.txtDataBaseName.Text = db.DataBase.ToString();
            // 登录名
            this.txtLoginName.Text = db.strID.ToString();
            // 登录密码
            this.txtPassword.Text = db.strPW.ToString();

        }

        /// <summary>
        /// 保存
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnSave_Click(object sender, EventArgs e)
        {
            // 验证
            if (!this.check())
            {
                this.DialogResult = DialogResult.None;
                return;
            }
            this.DialogResult = DialogResult.OK;

            db.DataSource = txtServerNameOrIP.Text;
            db.DataBase = txtDataBaseName.Text;
            db.strID = txtLoginName.Text;
            db.strPW = txtPassword.Text;

            // 设置配置文件数据
            db.SetConnectString();

            Alert.alert("数据库配置文件已变更！");

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
            // 服务器名称或IP
            if (!Check.isEmpty(this.txtServerNameOrIP, "服务器名称或IP"))
            {
                return false;
            }

            // 数据库名
            if (!Check.isEmpty(this.txtDataBaseName, "数据库名"))
            {
                return false;
            }

            // 登录名
            if (!Check.isEmpty(this.txtLoginName, "登录名"))
            {
                return false;
            }

            // 登录密码
            if (!Check.isEmpty(this.txtPassword, "登录密码"))
            {
                return false;
            }

            // 验证通过
            return true;
        }
    }
}
