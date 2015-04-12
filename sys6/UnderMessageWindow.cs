using System;
using System.Diagnostics;
using System.IO;
using System.Windows.Forms;
using LibCommon;
using LibPanels;

namespace UnderTerminal
{
    public partial class UnderMessageWindow : Form
    {
        public CurveMonitor Cm; // 监控曲线

        public UnderMessageWindow()
        {
            //base.doInitilization();

            InitializeComponent();

            //注册更新预警结果事件
            //_clientSocket.OnMsgUpdateWarningResult += UpdateWarningResultUi;
        }

        public String DefaultWorkStyle { get; set; }
        public String DefaultWorkTime { get; set; }
        public String DefaultTeamName { get; set; }
        public String DefaultSubmitter { get; set; }
        public bool OnLine { get; set; }

        private void btnUnderManage_Click(object sender, EventArgs e)
        {
            if (selectTunnelSimple1.SelectedTunnel == null)
            {
                Alert.alert("请选择巷道");
                return;
            }

            var m = new MineDataSimple(selectTunnelSimple1.SelectedTunnel)
            {
                Text = new LibPanels.LibPanels(MineDataPanelName.Management).panelFormName
            };
            m.ShowDialog();
        }

        private void btnUnderCoal_Click(object sender, EventArgs e)
        {
            if (selectTunnelSimple1.SelectedTunnel == null)
            {
                Alert.alert("请选择巷道");
                return;
            }

            var m = new MineDataSimple(selectTunnelSimple1.SelectedTunnel)
            {
                Text = new LibPanels.LibPanels(MineDataPanelName.CoalExistence).panelFormName
            };
            m.ShowDialog();
        }

        private void btnUnderGas_Click(object sender, EventArgs e)
        {
            if (selectTunnelSimple1.SelectedTunnel == null)
            {
                Alert.alert("请选择巷道");
                return;
            }

            var m = new MineDataSimple(selectTunnelSimple1.SelectedTunnel)
            {
                Text = new LibPanels.LibPanels(MineDataPanelName.GasData).panelFormName
            };
            m.ShowDialog();
        }

        private void btnUnderVen_Click(object sender, EventArgs e)
        {
            if (selectTunnelSimple1.SelectedTunnel == null)
            {
                Alert.alert("请选择巷道");
                return;
            }

            var m = new MineDataSimple(selectTunnelSimple1.SelectedTunnel)
            {
                Text = new LibPanels.LibPanels(MineDataPanelName.Ventilation).panelFormName
            };
            m.ShowDialog();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            if (selectTunnelSimple1.SelectedTunnel == null)
            {
                Alert.alert("请选择巷道");
                return;
            }

            var k = new K1ValueEntering();
            k.ShowDialog();
        }

        private void btnGeo_Click(object sender, EventArgs e)
        {
            if (selectTunnelSimple1.SelectedTunnel == null)
            {
                Alert.alert("请选择巷道");
                return;
            }

            var m = new MineDataSimple(selectTunnelSimple1.SelectedTunnel)
            {
                Text = new LibPanels.LibPanels(MineDataPanelName.GeologicStructure).panelFormName
            };
            m.ShowDialog();
        }

        /// <summary>
        ///     预警结果更新响应函数,参数无用
        /// </summary>
        //private void UpdateWarningResultUi(UpdateWarningResultMessage data)
        //{
        //    if (InvokeRequired)
        //    {
        //        ShowDelegate sd = UpdateWarningResultUi;
        //        lblWarning.Invoke(sd, data);
        //    }
        //    else
        //    {
        //        lblWarning.Visible = true;

        //        // 2 绿色
        //        if (data.WarningLevel == "2")
        //        {
        //            lblWarning.Text = tunnelNameWarning + "--绿色";
        //            lblWarning.BackColor = Color.Green;
        //            timer1.Stop();
        //        }
        //        else if (data.WarningLevel == "0")
        //        {
        //            //string type = string.Empty;
        //            //if (data.WarningType == "OVER_LIMIT")
        //            //    type = "超限";
        //            //else if (data.WarningType == "OUTBURST")
        //            //    type = "突出";
        //            // 0 代表“红色预警”
        //            lblWarning.Text = tunnelNameWarning + "--红色预警";
        //            lblWarning.BackColor = Color.Red;
        //            lblWarning.Tag = data.WarningReason;
        //            //lblWarning.Image=
        //            timer1.Start();
        //        }
        //        else if (data.WarningLevel == "1")
        //        {
        //            //string type = string.Empty;
        //            //if (data.WarningType == "OVER_LIMIT")
        //            //    type = "超限";
        //            //else if (data.WarningType == "OUTBURST")
        //            //    type = "突出";
        //            // 1 代表“黄色预警”
        //            lblWarning.Text = tunnelNameWarning + "--黄色预警";
        //            lblWarning.BackColor = Color.Yellow;
        //            lblWarning.Tag = data.WarningReason;
        //            timer1.Start();
        //        }
        //    }
        //}

        // 打开一通三防报表
        private void btnOpenReport_Click(object sender, EventArgs e)
        {
            const string reportFile = "C:\\tmp\\report.xls";
            if (!File.Exists(reportFile))
            {
                Alert.alert(reportFile + "不存在。");
                return;
            }

            Process.Start(reportFile);
        }

        // 系统1监控曲线
        private void btnMonitorCurve_Click(object sender, EventArgs e)
        {
            if (selectTunnelSimple1.SelectedTunnel == null)
            {
                Alert.alert("请选择巷道");
                return;
            }

            if (null == Cm)
            {
                Cm = new CurveMonitor(selectTunnelSimple1.SelectedTunnel.TunnelId, selectTunnelSimple1.SelectedTunnel.TunnelName, this);
                Cm.ShowDialog();
            }
            else
            {
                Cm.TunnelId = selectTunnelSimple1.SelectedTunnel.TunnelId;
                Cm.TunnelName = selectTunnelSimple1.SelectedTunnel.TunnelName;
                Cm.Visible = true;
            }
        }

        private void timer1_Tick(object sender, EventArgs e)
        {

        }

        // 关机
        private void btnCloseSys_Click(object sender, EventArgs e)
        {
            // DialogResult result = MessageBox.Show("确认关机", "井下终端录入系统", MessageBoxButtons.OKCancel, MessageBoxIcon.Question);
            var popup = new LoginPopup("确认关机");
            if (popup.ShowDialog() == DialogResult.OK)
            {
                SysHelper.DoExitWin(SysHelper.EWX_SHUTDOWN);
            }
        }

        // 重启系统
        private void btnRestartSys_Click(object sender, EventArgs e)
        {
            //DialogResult result = MessageBox.Show("确认重启", "井下终端录入系统", MessageBoxButtons.OKCancel, MessageBoxIcon.Question);
            var popup = new LoginPopup("确认重启");
            if (popup.ShowDialog() == DialogResult.OK)
            {
                SysHelper.Reboot();
            }
        }

        // 维护本程序
        private void btnManagement_Click(object sender, EventArgs e)
        {
            var mgr = new SysMgr();
            mgr.ShowDialog();
        }

        private void btnSetDefaultValue_Click(object sender, EventArgs e)
        {
            var form = new SetDefaultValue(this);
            form.Show();
        }

        public void RefreshDefaultValue()
        {
            lbWorkStyle.Text = DefaultWorkStyle;
            lbTeamName.Text = DefaultTeamName;
            lbSubmitter.Text = DefaultSubmitter;
            lbWorkTime.Text = DefaultWorkTime;
        }
    }
}