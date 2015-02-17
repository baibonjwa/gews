using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using LibCommonForm;
using LibEntity;
using LibBusiness;
using LibCommon;
using LibSocket;
using System.Net;
using System.Net.Sockets;
using LibCommonControl;
using LibConfig;

namespace UnderTerminal
{
    public partial class UnderMessageWindow : Form
    {
        private int tunnelId = -1;
        public int workingfaceId = -1;
        private string tunnelName = String.Empty;
        private int tunnelIdWarning = -1;
        private string tunnelNameWarning = string.Empty;
        public CurveMonitor cm; // 监控曲线
        public static ClientSocket _clientSocket = null;

        public String DefaultWorkStyle { get; set; }
        public String DefaultWorkTime { get; set; }
        public String DefaultTeamName { get; set; }
        public String DefaultSubmitter { get; set; }

        public UnderMessageWindow()
        {
            //base.doInitilization();

            InitializeComponent();

            // 注册委托事件
            this.selectTunnelSimple1.TunnelNameChanged +=
                InheritTunnelNameChanged;

            lblWarning.Text = string.Empty;

            InitClientSocket();

            //--------The following code is for heartbeat.-----------------------------------------------------------
            udpServerSocket = new Socket(AddressFamily.InterNetwork,
                SocketType.Dgram, ProtocolType.Udp);
            udpServerSocket.Bind(ep);
            udpServerSocket.BeginReceiveFrom(buffer, 0, 1024, SocketFlags.None, ref ep, ReceiveData, udpServerSocket);

            checkTimer.Interval = 4000;
            checkTimer.AutoReset = true;
            checkTimer.Elapsed += checkTimer_Elapsed;
            checkTimer.Start();
            //--------end heart beat code.-----------------------------------------------------------------------------------------------

            //注册更新预警结果事件
            _clientSocket.OnMsgUpdateWarningResult += UpdateWarningResultUI;
        }

        /// <summary>
        /// 委托事件
        /// </summary>
        /// <param name="sender"></param>
        private void InheritTunnelNameChanged(object sender, TunnelEventArgs e)
        {
            this.tunnelId = this.selectTunnelSimple1.ITunnelId;
            this.tunnelName = this.selectTunnelSimple1.ITunnelName;
            workingfaceId = Tunnel.Find(tunnelId).WorkingFace.WorkingFaceId;
        }

        private void btnUnderManage_Click(object sender, EventArgs e)
        {
            if (this.tunnelId <= 0)
            {
                MessageBox.Show("请选择巷道!");
                return;
            }

            MineDataSimple m = new MineDataSimple(this.tunnelId, this.tunnelName, this);
            m.Text = new LibPanels(MineDataPanelName.Management).panelFormName;
            m.ShowDialog();
        }

        private void btnUnderCoal_Click(object sender, EventArgs e)
        {
            if (this.tunnelId <= 0)
            {
                MessageBox.Show("请选择巷道!");
                return;
            }

            MineDataSimple m = new MineDataSimple(this.tunnelId, this.tunnelName, this);
            m.Text = new LibPanels(MineDataPanelName.CoalExistence).panelFormName;
            string ThePath = System.Windows.Forms.Application.StartupPath;
            m.ShowDialog();
        }

        private void btnUnderGas_Click(object sender, EventArgs e)
        {
            if (this.tunnelId <= 0)
            {
                MessageBox.Show("请选择巷道!");
                return;
            }

            MineDataSimple m = new MineDataSimple(this.tunnelId, this.tunnelName, this);
            m.Text = new LibPanels(MineDataPanelName.GasData).panelFormName;
            m.ShowDialog();
        }

        private void btnUnderVen_Click(object sender, EventArgs e)
        {
            if (this.tunnelId <= 0)
            {
                MessageBox.Show("请选择巷道!");
                return;
            }

            MineDataSimple m = new MineDataSimple(this.tunnelId, this.tunnelName, this);
            m.Text = new LibPanels(MineDataPanelName.Ventilation).panelFormName;
            m.ShowDialog();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            if (this.tunnelId <= 0)
            {
                MessageBox.Show("请选择巷道!");
                return;
            }

            K1ValueEntering k = new K1ValueEntering(this.tunnelId, this.tunnelName, this);
            k.ShowDialog();
        }

        // 弹出管理界面
        private void button1_Click(object sender, EventArgs e)
        {
            if (this.tunnelId <= 0)
            {
                MessageBox.Show("请选择巷道!");
                return;
            }

            MineDataSimple m = new MineDataSimple(this.tunnelId, this.tunnelName, this);
            m.Text = new LibPanels(MineDataPanelName.Management).panelFormName;
            m.ShowDialog();
        }

        private void btnGeo_Click(object sender, EventArgs e)
        {
            if (this.tunnelId <= 0)
            {
                MessageBox.Show("请选择巷道!");
                return;
            }

            MineDataSimple m = new MineDataSimple(this.tunnelId, this.tunnelName, this);
            m.Text = new LibPanels(MineDataPanelName.GeologicStructure).panelFormName;
            string ThePath = System.Windows.Forms.Application.StartupPath;
            m.ShowDialog();
        }

        private void btnDayReportJJ_Click(object sender, EventArgs e)
        {
            if (this.tunnelId <= 0)
            {
                MessageBox.Show("请选择巷道!");
                return;
            }

            DayReportJJEntering dayReportJJEntering = new DayReportJJEntering(this.tunnelId, this.tunnelName, this);
            dayReportJJEntering.ShowDialog();
        }

        private void btnDayReportHC_Click(object sender, EventArgs e)
        {
            if (this.tunnelId <= 0)
            {
                MessageBox.Show("请选择巷道!");
                return;
            }

            DayReportHCEntering dayReportHCEntering = new DayReportHCEntering(this.tunnelId, this.tunnelName, this);
            dayReportHCEntering.ShowDialog();
        }

        private delegate void ShowDelegate(UpdateWarningResultMessage data);

        /// <summary>
        /// 预警结果更新响应函数,参数无用
        /// </summary>
        /// <param name="data"></param>
        private void UpdateWarningResultUI(UpdateWarningResultMessage data)
        {
            if (this.InvokeRequired)
            {
                ShowDelegate sd = UpdateWarningResultUI;
                lblWarning.Invoke(sd, data);
            }
            else
            {
                lblWarning.Visible = true;

                // 2 绿色
                if (data.WarningLevel == "2")
                {
                    lblWarning.Text = this.tunnelNameWarning + "--绿色";
                    lblWarning.BackColor = Color.Green;
                    timer1.Stop();
                }
                else if (data.WarningLevel == "0")
                {
                    //string type = string.Empty;
                    //if (data.WarningType == "OVER_LIMIT")
                    //    type = "超限";
                    //else if (data.WarningType == "OUTBURST")
                    //    type = "突出";
                    // 0 代表“红色预警”
                    lblWarning.Text = this.tunnelNameWarning + "--红色预警";
                    lblWarning.BackColor = Color.Red;
                    lblWarning.Tag = data.WarningReason;
                    //lblWarning.Image=
                    timer1.Start();
                }
                else if (data.WarningLevel == "1")
                {
                    //string type = string.Empty;
                    //if (data.WarningType == "OVER_LIMIT")
                    //    type = "超限";
                    //else if (data.WarningType == "OUTBURST")
                    //    type = "突出";
                    // 1 代表“黄色预警”
                    lblWarning.Text = this.tunnelNameWarning + "--黄色预警";
                    lblWarning.BackColor = Color.Yellow;
                    lblWarning.Tag = data.WarningReason;
                    timer1.Start();
                }
            }
        }

        // 打开一通三防报表
        private void btnOpenReport_Click(object sender, EventArgs e)
        {
            /*
            OpenFileDialog fileDialog = new OpenFileDialog();
            fileDialog.InitialDirectory = "C:\\tmp";
            fileDialog.Multiselect = false;
            fileDialog.Title = "请选择一通三防报表";
            fileDialog.Filter = "Excel文件(*.xls)|*.xls";
            fileDialog.RestoreDirectory = true;
            fileDialog.FilterIndex = 1;
            if (fileDialog.ShowDialog() == DialogResult.OK)
            {
                string file = fileDialog.FileName;
                System.Diagnostics.Process.Start(file);
            }
            */
            string reportFile = "C:\\tmp\\report.xls";
            if (!System.IO.File.Exists(reportFile))
            {
                MessageBox.Show(reportFile + "不存在。");
                return;
            }

            System.Diagnostics.Process.Start(reportFile);
        }

        // 系统1监控曲线
        private void btnMonitorCurve_Click(object sender, EventArgs e)
        {
            if (this.tunnelId <= 0)
            {
                MessageBox.Show("请选择巷道!");
                return;
            }

            if (null == cm)
            {
                cm = new CurveMonitor(this.tunnelId, this.tunnelName, this);
                cm.ShowDialog();
            }
            else
            {
                cm.TunnelId = this.tunnelId;
                cm.TunnelName = this.tunnelName;
                cm.Visible = true;
            }
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            lblWarning.Visible = !lblWarning.Visible;
        }

        private void btnChooseWarningTunnel_Click(object sender, EventArgs e)
        {
            SelectTunnelDlg dlg = new SelectTunnelDlg();
            if (DialogResult.OK == dlg.ShowDialog())
            {
                this.tunnelIdWarning = dlg.tunnelId;
                this.tunnelNameWarning = dlg.tunnelName;
                lblWarning.Text = dlg.tunnelName;

                // Register warning notification from server.
                registerWarningNotification();
            }
        }

        private void registerWarningNotification()
        {
            SocketMessage msg = new SocketMessage(COMMAND_ID.REGISTER_WARNING_RESULT_NOTIFICATION_SINGLE, DateTime.Now);
            msg.TunnelId = this.tunnelIdWarning;
            SendMsg2Server(msg);
        }

        // 关机
        private void btnCloseSys_Click(object sender, EventArgs e)
        {
            // DialogResult result = MessageBox.Show("确认关机", "井下终端录入系统", MessageBoxButtons.OKCancel, MessageBoxIcon.Question);
            LoginPopup popup = new LoginPopup("确认关机");
            if (popup.ShowDialog() == DialogResult.OK)
            {
                SysHelper.DoExitWin(SysHelper.EWX_SHUTDOWN);
            }
        }

        // 重启系统
        private void btnRestartSys_Click(object sender, EventArgs e)
        {
            //DialogResult result = MessageBox.Show("确认重启", "井下终端录入系统", MessageBoxButtons.OKCancel, MessageBoxIcon.Question);
            LoginPopup popup = new LoginPopup("确认重启");
            if (popup.ShowDialog() == DialogResult.OK)
            {
                SysHelper.Reboot();
            }
        }

        // 维护本程序
        private void btnManagement_Click(object sender, EventArgs e)
        {
            SysMgr mgr = new SysMgr();
            mgr.ShowDialog();
        }

        // server network connection status
        private void btnServerStatus_Click(object sender, EventArgs e)
        {
            Button btn = sender as Button;
            if (btn.Text == "离线")
            {
                // 重新连接server
                InitClientSocket();
            }
        }

        private bool online;

        public bool OnLine
        {
            get { return this.online; }
            set { this.online = value; }
        }

        private DateTime lastUpdate;
        private Socket udpServerSocket;
        private EndPoint ep = new IPEndPoint(IPAddress.Any, 9876);
        private System.Timers.Timer checkTimer = new System.Timers.Timer();
        private byte[] buffer = new byte[1024];

        private delegate void MyDelegate(String text, Color color);

        private void UpdateInfo(String text, Color color)
        {
            if (btnServerStatus.InvokeRequired)
            {
                MyDelegate sd = UpdateInfo;
                btnServerStatus.Invoke(sd, text, color);
            }
            else
            {
                btnServerStatus.Text = text;
                btnServerStatus.BackColor = color;
            }
        }

        /// <summary>
        /// Timer event handler. Checks if the last Heartbeat was more than 3 seconds
        /// ago and sets the lable to the Alarm message.
        /// </summary>
        /// <param name="sender">Sender of the Event; not used.</param>
        /// <param name="e">Parameter for the Event; not used.</param>
        private void checkTimer_Elapsed(object sender, System.Timers.ElapsedEventArgs e)
        {
            // Calculate the Timespan since the Last Update from the Client.
            TimeSpan timeSinceLastHeartbeat = DateTime.Now.ToUniversalTime() - lastUpdate;

            // Set Lable Text depending of the Timespan
            if (timeSinceLastHeartbeat > TimeSpan.FromSeconds(4))
            {

                UpdateInfo("离线", Color.Red);
                this.online = false;

                // Clear data.
                this.tunnelIdWarning = -1;
                this.tunnelNameWarning = string.Empty;
                this.Invoke(new MethodInvoker(delegate
                {
                    lblWarning.Text = string.Empty;
                }));
            }
            else
            {
                UpdateInfo("联机", Color.Green);
                this.online = true;
            }
        }

        /// <summary>
        /// Callback function for the BeginReceiveFrom Function.
        /// Receives the data from the buffer, sets the lastUpdate Variable
        /// and starts a new BeginReceiveFrom.
        /// </summary>
        /// <param name="iar">The result of the asynchronous operation.</param>
        void ReceiveData(IAsyncResult iar)
        {
            // Create temporary remote end Point
            IPEndPoint sender = new IPEndPoint(IPAddress.Any, 0);
            EndPoint tempRemoteEP = (EndPoint)sender;

            // Get the Socket
            Socket remote = (Socket)iar.AsyncState;

            // Call EndReceiveFrom to get the received Data
            int recv = remote.EndReceiveFrom(iar, ref tempRemoteEP);

            // Get the Data from the buffer to a string
            string stringData = Encoding.ASCII.GetString(buffer, 0, recv);
            Console.WriteLine(stringData);

            // update Timestamp
            lastUpdate = DateTime.Now.ToUniversalTime();

            // Restart receiving
            if (!this.IsDisposed)
            {
                udpServerSocket.BeginReceiveFrom(buffer, 0, 1024, SocketFlags.None, ref ep, ReceiveData, udpServerSocket);
            }
        }

        public void InitClientSocket()
        {
            string serverIp = ConfigManager.Instance.getValueByKey(ConfigConst.CONFIG_SERVER_IP);
            int port = int.Parse(ConfigManager.Instance.getValueByKey(ConfigConst.CONFIG_PORT));

            //初始化客户端Socket，连接服务器
            string errorMsg = SocketHelper.InitClientSocket(serverIp, port, out _clientSocket);
            if (errorMsg != "")
            {
                Alert.alert(Const.CONNECT_SOCKET_ERROR, Const.NOTES, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                Log.Error(errorMsg);
            }
            else
            {
                //连接服务器成功
                Log.Info(Const.LOG_MSG_CONNECT_SUCCEED);
            }
        }

        public void SendMsg2Server(SocketMessage msg)
        {
            ClientSocket cs = GetClientSocketInstance();
            if (cs != null)
            {
                string errMsg = cs.SendSocketMsg2Server(msg);
                if (errMsg != "")
                {
                    Log.Error(Const.SEND_MSG_FAILED + Const.CONNECT_ARROW + msg.ToString());
                }
            }
            else
            {
                Log.Info(Const.CLIENT_SOCKET_IS_NULL + Const.CONNECT_ARROW);
            }
        }

        public ClientSocket GetClientSocketInstance()
        {
            if (_clientSocket == null)
            {
                InitClientSocket();
            }
            return _clientSocket;
        }

        private void btnSetDefaultValue_Click(object sender, EventArgs e)
        {
            SetDefaultValue form = new SetDefaultValue(this);
            form.Show();
        }

        public void RefreshDefaultValue()
        {
            lbWorkStyle.Text = DefaultWorkStyle;
            lbTeamName.Text = DefaultTeamName;
            lbSubmitter.Text = DefaultSubmitter;
            lbWorkTime.Text = DefaultWorkTime;
        }

        //public void doInitilization()
        //{
        //    // Initialize configuration manager.
        //    ConfigManager cfgMgr = ConfigManager.Instance;
        //    string msg = cfgMgr.init(Application.StartupPath);

        //    if (msg != string.Empty)
        //    {
        //        MessageBox.Show(msg);
        //        Application.Exit();
        //    }

        //    //初始化客户端Socket
        //    InitClientSocket();
        //}
    }
}
