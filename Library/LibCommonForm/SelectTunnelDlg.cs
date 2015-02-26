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
using LibCommonControl;
using LibBusiness;
using LibCommon;
using LibEntity;

namespace LibCommonForm
{
    public partial class SelectTunnelDlg : Form
    {
        public int tunnelId;
        public string tunnelName;

        /// <summary>
        /// 构造方法
        /// </summary>
        public SelectTunnelDlg()
        {
            InitializeComponent();

            // 设置窗体默认属性
            FormDefaultPropertiesSetter.SetEnteringFormDefaultProperties(this, Const_GE.CHOOSE_TUNNEL);
        }

        public SelectTunnelDlg(params TunnelTypeEnum[] types)
        {
            InitializeComponent();
            // 设置窗体默认属性
            FormDefaultPropertiesSetter.SetEnteringFormDefaultProperties(this, Const_GE.CHOOSE_TUNNEL);
        }

        /// <summary>
        /// 构造方法
        /// </summary>
        /// <param name="tEntity">巷道实体</param>
        public SelectTunnelDlg(Tunnel tEntity)
        {
            InitializeComponent();

            //窗体属性设置
            LibCommon.FormDefaultPropertiesSetter.SetEnteringFormDefaultProperties(this, Const_GM.TUNNEL_CHOOSE);
        }

        /// <summary>
        /// 构造方法
        /// </summary>
        /// <param name="tEntity">巷道实体</param>
        public SelectTunnelDlg(int[] intArr)
        {
            InitializeComponent();

            //窗体属性设置
            LibCommon.FormDefaultPropertiesSetter.SetEnteringFormDefaultProperties(this, Const_GM.TUNNEL_CHOOSE);

        }

        /// <summary>
        /// 构造方法
        /// </summary>
        /// <param name="tEntity">巷道实体</param>
        public SelectTunnelDlg(int[] intArr, int filterType)
        {
            InitializeComponent();

            //窗体属性设置
            LibCommon.FormDefaultPropertiesSetter.SetEnteringFormDefaultProperties(this, Const_GM.TUNNEL_CHOOSE);
        }

        /// <summary>
        /// 提交
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnSubmit_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.OK;
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
