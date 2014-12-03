using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using LibCommon;
using LibConfig;
using LibSocket;
using System.Net;
using System.Net.Sockets;

namespace LibCommonControl
{
    public partial class StateMonitor : UserControl
    {
        private Socket udpServerSocket;
        private System.Timers.Timer checkTimer = new System.Timers.Timer();
        private DateTime lastUpdate;
        private byte[] buffer = new byte[1024];
        private EndPoint ep = new IPEndPoint(IPAddress.Any, 9876);

        public MainFrm mainForm;
        public enum LocationType
        {
            UpperLeft,
            UpperRight,
            BottomLeft,
            BottomRight
        }

        public LocationType Type { get; set; }
        public int X { get; set; }
        public int Y { get; set; }

        public StateMonitor()
        {
            InitializeComponent();
        }

        /// <summary>
        /// 本函数需要在主窗体初始化后立即调用
        /// </summary>
        /// <param name="mainFrm">主窗体</param>
        public void doInitialization(MainFrm mainFrm)
        {
            this.mainForm = mainFrm;
            mainFrm.Load += Relocation;
            mainFrm.SizeChanged += Relocation;
        }

        private void checkTimer_Elapsed(object sender, System.Timers.ElapsedEventArgs e)
        {
            // Calculate the Timespan since the Last Update from the Client.
            TimeSpan timeSinceLastHeartbeat = DateTime.Now.ToUniversalTime() - lastUpdate;

            // Set Lable Text depending of the Timespan
            if (timeSinceLastHeartbeat > TimeSpan.FromSeconds(4))
            {

                //UpdateInfo("离线", Color.Red);
                //this.online = false;

                // Clear data.
                //this.tunnelIdWarning = -1;
                //this.tunnelNameWarning = string.Empty;
                if (_lbState.InvokeRequired)
                {
                    this.Invoke(new MethodInvoker(delegate
                    {
                        _lbState.Text = "离线（单击连接）";
                        _lbState.ForeColor = Color.Red;
                    }));
                }
            }
            else
            {
                //this.Invoke(new MethodInvoker(delegate
                //{
                if (_lbState.InvokeRequired)
                {
                    this.Invoke(new MethodInvoker(delegate
                    {
                        _lbState.Text = "联机";
                    }));
                }
                //UpdateInfo("联机", Color.Green);
                //this.online = true;
            }
        }


        private void ReceiveData(IAsyncResult iar)
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
                udpServerSocket.BeginReceiveFrom(buffer, 0, 1024, SocketFlags.None, ref ep, new AsyncCallback(ReceiveData), udpServerSocket);
            }
        }

        private void _lbState_Click(object sender, EventArgs e)
        {
            if (_lbState.Text.Contains("离线"))
            {
                // 重新连接server
                mainForm.InitClientSocket();
            }
        }

        public void Start()
        {
            //初始化客户端Socket

            udpServerSocket = new Socket(AddressFamily.InterNetwork,
              SocketType.Dgram, ProtocolType.Udp);
            udpServerSocket.Bind(ep);
            udpServerSocket.BeginReceiveFrom(buffer, 0, 1024, SocketFlags.None, ref ep, new AsyncCallback(ReceiveData), udpServerSocket);

            checkTimer.Interval = 4000;
            checkTimer.AutoReset = true;
            checkTimer.Elapsed += new System.Timers.ElapsedEventHandler(checkTimer_Elapsed);
            checkTimer.Start();
        }

        public void Relocation(object sender, EventArgs e)
        {
            switch (Type)
            {
                case LocationType.UpperLeft:
                    this.Location = new Point(X, Y);
                    break;
                case LocationType.UpperRight:
                    this.Location = new Point(mainForm.ClientRectangle.Width - X - this.Width, Y);
                    break;
                case LocationType.BottomLeft:
                    this.Location = new Point(X, mainForm.ClientRectangle.Height - Y - this.Height);
                    break;
                case LocationType.BottomRight:
                    this.Location = new Point(mainForm.ClientRectangle.Width - X - this.Width, mainForm.ClientRectangle.Height - Y - this.Height);
                    break;
                default:
                    this.Location = new Point(X, Y);
                    break;
            }
        }
    }
}
