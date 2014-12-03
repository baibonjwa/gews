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
    public partial class SelectTunnelDlg : BaseForm
    {
        public int tunnelId;
        public string tunnelName;

        /// <summary>
        /// 构造方法
        /// </summary>
        public SelectTunnelDlg(MainFrm mainFrm)
        {
            InitializeComponent();

            this.MainForm = mainFrm;

            // 设置窗体默认属性
            FormDefaultPropertiesSetter.SetEnteringFormDefaultProperties(this, Const_GE.CHOOSE_TUNNEL);

            // 调用选择巷道控件时需要调用的方法
            this.selectTunnelUserControl1.loadMineName();
        }

        public SelectTunnelDlg(MainFrm mainFrm, params TunnelTypeEnum[] types)
        {
            InitializeComponent();

            this.MainForm = mainFrm;

            // 设置窗体默认属性
            FormDefaultPropertiesSetter.SetEnteringFormDefaultProperties(this, Const_GE.CHOOSE_TUNNEL);

            this.selectTunnelUserControl1.SetFilterOn(types);
            // 调用选择巷道控件时需要调用的方法
            this.selectTunnelUserControl1.loadMineName();
        }

        /// <summary>
        /// 构造方法
        /// </summary>
        /// <param name="tEntity">巷道实体</param>
        public SelectTunnelDlg(TunnelEntity tEntity, MainFrm mainFrm)
        {
            this.MainForm = mainFrm;
            InitializeComponent();

            //窗体属性设置
            LibCommon.FormDefaultPropertiesSetter.SetEnteringFormDefaultProperties(this, Const_GM.TUNNEL_CHOOSE);
            this.selectTunnelUserControl1.SetFilterOn(TunnelFilter.TunnelFilterRules.IS_WIRE_INFO_BIND);
            //自定义控件
            this.selectTunnelUserControl1.setCurSelectedID(tEntity);
        }

        /// <summary>
        /// 构造方法
        /// </summary>
        /// <param name="tEntity">巷道实体</param>
        public SelectTunnelDlg(int[] intArr, MainFrm mainFrm)
        {
            this.MainForm = mainFrm;
            InitializeComponent();

            //窗体属性设置
            LibCommon.FormDefaultPropertiesSetter.SetEnteringFormDefaultProperties(this, Const_GM.TUNNEL_CHOOSE);
            this.selectTunnelUserControl1.SetFilterOn(TunnelFilter.TunnelFilterRules.IS_WIRE_INFO_BIND);
            //自定义控件
            this.selectTunnelUserControl1.setCurSelectedID(intArr);
        }

        /// <summary>
        /// 构造方法
        /// </summary>
        /// <param name="tEntity">巷道实体</param>
        public SelectTunnelDlg(int[] intArr, MainFrm mainFrm, int filterType)
        {
            this.MainForm = mainFrm;
            InitializeComponent();

            //窗体属性设置
            LibCommon.FormDefaultPropertiesSetter.SetEnteringFormDefaultProperties(this, Const_GM.TUNNEL_CHOOSE);
            this.selectTunnelUserControl1.SetFilterOn(TunnelFilter.TunnelFilterRules.IS_WIRE_INFO_BIND);
            //自定义控件
            this.selectTunnelUserControl1.SetFilterOn(filterType);
            this.selectTunnelUserControl1.setCurSelectedID(intArr);
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
