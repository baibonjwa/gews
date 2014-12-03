// ******************************************************************
// 概  述：选择巷道
// 作  者：jhou
// 创建日期：2014/04/16
// 版本号：V1.0
// 版本信息：
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

using LibBusiness;
using LibCommon;

namespace UnderTerminal
{
    public partial class SelectTunnelDlg : Form
    {
        /** 存放矿井名称，水平，采区，工作面，巷道编号的数组  **/
        private int[] _intArr = new int[5];

        public int tunnelId;
        public string tunnelName;

        /// <summary>
        /// 构造方法
        /// </summary>
        public SelectTunnelDlg()
        {
            InitializeComponent();

            // 调用选择巷道控件时需要调用的方法
            this.selectTunnelUserControl1.loadMineName();
        }

        /// <summary>
        /// 带参数的构造方法
        /// </summary>
        /// <param name="strPrimaryKey">主键</param>
        public SelectTunnelDlg(string strPrimaryKey)
        {
            InitializeComponent();

            // 调用选择巷道控件时需要调用的方法
            this.selectTunnelUserControl1.setCurSelectedID(_intArr);
        }

        /// <summary>
        /// 提交
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnSubmit_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.OK;

            // 巷道编号
            tunnelId = this.selectTunnelUserControl1.ITunnelId;
            tunnelName = this.selectTunnelUserControl1.STunnelName;

            //MessageBox.Show("tunnelId=" + tunnelId + ", tunnelName=" + tunnelName);
        }

        /// <summary>
        /// Cancel the option
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.None;
            // 关闭窗口
            this.Close();
        }
    }
}
