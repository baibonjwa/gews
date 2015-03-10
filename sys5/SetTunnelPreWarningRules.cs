// ******************************************************************
// 概  述：设置巷道预警规则
// 作  者：伍鑫
// 创建日期：2013/12/28
// 版本号：1.0
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
using LibCommonControl;
using LibCommonForm;
using LibSocket;

namespace _5.WarningManagement
{
    public partial class SetTunnelPreWarningRules : Form
    {
        SocketHelper mainForm;

        // 创建预警规则管理界面
        PreWarningRulesPanel _preWarningRulesPanel = new PreWarningRulesPanel();

        private RULE_TYPE_WARNING_TYPE_FILTER _warningTypeFilter = RULE_TYPE_WARNING_TYPE_FILTER.ALL;

        /// <summary>
        /// 根据过滤条件显示预警规则
        /// </summary>
        /// <param name="ruleTypeFilter"></param>
        public SetTunnelPreWarningRules(RULE_TYPE_WARNING_TYPE_FILTER warningTypeFilter)
        {
            InitializeComponent();
            _warningTypeFilter = warningTypeFilter;
            _warningTypeFilter = RULE_TYPE_WARNING_TYPE_FILTER.ALL;

            this.Text = Const_WM.TUNNEL_RULES_SETTING;
        }

        /// <summary>
        /// 根据过滤条件显示预警规则
        /// </summary>
        /// <param name="ruleTypeFilter"></param>
        public SetTunnelPreWarningRules(RULE_TYPE_WARNING_TYPE_FILTER warningTypeFilter, SocketHelper mainFrm)
        {
            this.mainForm = mainFrm;

            InitializeComponent();
            _warningTypeFilter = warningTypeFilter;
            _warningTypeFilter = RULE_TYPE_WARNING_TYPE_FILTER.ALL;

            this.Text = Const_WM.TUNNEL_RULES_SETTING;
        }

        /// <summary>
        /// load事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SetTunnelPreWarningRules_Load(object sender, EventArgs e)
        {
            lblMessage.Text = "";

            // 设置显示位置
            _preWarningRulesPanel.MdiParent = this;
            this._panelRules.Controls.Add(_preWarningRulesPanel);
            _preWarningRulesPanel.WindowState = FormWindowState.Maximized;
            //规则显示前先设置加载规则内容
            _preWarningRulesPanel.SetInitWarningTypeFilter(_warningTypeFilter);
            _preWarningRulesPanel.Show();
            _preWarningRulesPanel.Activate();

            //_selectTunnel.loadMineName();

            //注册事件 
            selectTunnelSimple1.cbxTunnel.SelectedIndexChanged += TunnelNameChanged;
        }

        private void TunnelNameChanged(object sender, EventArgs e)
        {
            lblMessage.Text = Const_WM.MSG03;
            if (selectTunnelSimple1.SelectedTunnel != null)
            {
                _preWarningRulesPanel.SetTunnelSelectedRuleIdsAndUpdateParams(selectTunnelSimple1.SelectedTunnel.TunnelId);
            }
            else
            {
                _preWarningRulesPanel.ClearSelectedRules();
            }
        }

        /// <summary>
        /// 应用
        /// 注意：在预警规则管理界面更改预警参数值时，不会将预警参数值应用至绑定的巷道。
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnApply_Click(object sender, EventArgs e)
        {
            if (this.selectTunnelSimple1.SelectedTunnel != null)
            {
                _preWarningRulesPanel.ApplyParamsValues();
                if (!_preWarningRulesPanel.UpdateTunnelBindingRuleCodeAndParamsInfo(this.selectTunnelSimple1.SelectedTunnel.TunnelId))
                {
                    MessageBox.Show(@"更新巷道绑定的规则参数失败！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
                else
                {
                    Alert.noteMsg("应用成功!");
                    // TODO, 需要修改日期
                    //通知服务器预警数据已更新
                    var msg = new ResetTunnelRulesMsg(Const.INVALID_ID, this.selectTunnelSimple1.SelectedTunnel.TunnelId,
                        TunnelInfoDbConstNames.TABLE_NAME, DateTime.Now);
                    SocketUtil.SendMsg2Server(msg);
                }
            }
        }

        /// <summary>
        /// 全选所有规则
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnSelAll_Click(object sender, EventArgs e)
        {
            _preWarningRulesPanel.SelectAllRuleCodes(true);
        }

        /// <summary>
        /// 取消全选所有规则
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnCancelSelAll_Click(object sender, EventArgs e)
        {
            _preWarningRulesPanel.SelectAllRuleCodes(false);
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            //关闭窗体
            this.Close();
        }
    }
}
