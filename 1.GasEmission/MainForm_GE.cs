// ******************************************************************
// 概  述：系统一主界面
// 作  者：伍鑫
// 创建日期：
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
using LibCommonForm;
using LibCommon;
using LibSocket;
using DevExpress.Skins;
using DevExpress.LookAndFeel;
using DevExpress.XtraEditors;
using LibBusiness;
using LibDatabase;
using LibAbout;
using TeeChartWrapper;
using System.Threading;
using System.Net.Sockets;
using System.Net;

namespace _1.GasEmission
{
    public partial class MainForm_GE : MainFrm
    {
        ///** 探头数据更新频率(单位：秒) **/
        //private int _xInterval = 5;

        /** 显示最大数据数 **/
        private int dataCountPerFrame;
        private int updateFrequency; // 10s
        private double redDataThreshold;
        private double yellowDataThreshold;

        private int currentTunnelId = -1;
        private string currentProbeId = string.Empty;
        private string _T2Id = string.Empty;

        double t2DeltaSumValue = 0;
        private static int t2DataCount = 1;
        private System.Timers.Timer checkTimer = new System.Timers.Timer();
        private DateTime _LastTimeT2; // T2的数据更新时间
        private DateTime _LastTimeMN; // M/N 的数据更新时间
        private DateTime lastUpdate;
        private Socket udpServerSocket;
        private byte[] buffer = new byte[1024];
        private EndPoint ep = new IPEndPoint(IPAddress.Any, 9876);
        private string configFileName = "sys.properties";

        private bool online;

        public bool OnLine
        {
            get { return this.online; }
            set { this.online = value; }
        }

        delegate void Loading();
        Loading loading;

        // 配置文件
        public string ConfigFileName
        {
            get { return this.configFileName; }
        }

        // 曲线更新频率
        public int UpdateFrequency
        {
            get { return this.updateFrequency; }
            set
            {
                this.updateFrequency = value;
                this.timer1.Interval = value * 1000;
            }
        }

        // 曲线每一帧显示的数据个数
        public int DataCountPerFrame
        {
            get { return this.dataCountPerFrame; }
            set
            {
                this.dataCountPerFrame = value;
            }
        }

        public double RedDataThreshold
        {
            get { return this.redDataThreshold; }
            set { this.redDataThreshold = value; }
        }

        public double YellowDataThreshold
        {
            get { return this.yellowDataThreshold; }
            set { this.yellowDataThreshold = value; }
        }

        public double BadDataThreshold { get; set; }

        //private double _WarnValue = Const.WARN_VALUE;

        Random rnd = new Random();

        // 获取点击开始按钮时候的系统时间
        private DateTime _StartTime;

        MonitoringDataAnalysis monitoringDataAnalysisForm = null;

        private bool _EnableDeleteAndModifyBtn = true;
        public bool EnableDeleteAndModifyBtn
        {
            set { _EnableDeleteAndModifyBtn = value; }
            get { return _EnableDeleteAndModifyBtn; }
        }

        public MainForm_GE()
        {
            InitializeComponent();
            base.doInitilization();
            monitoringDataAnalysisForm = new MonitoringDataAnalysis(this);

            loading = new Loading(BarLoading);

            FileProperties fp = new FileProperties(configFileName);

            int iValue;
            double dValue;
            int.TryParse(fp.get("countperframe"), out iValue);
            this.dataCountPerFrame = iValue;
            int.TryParse(fp.get("updatefrequency"), out iValue);
            this.updateFrequency = iValue;
            double.TryParse(fp.get("redthreshold"), out dValue);
            this.redDataThreshold = dValue;
            double.TryParse(fp.get("yellowthreshold"), out dValue);
            this.yellowDataThreshold = dValue;
            double.TryParse(fp.get("baddatathreshold"), out dValue);
            this.BadDataThreshold = dValue;

            //初始化客户端Socket
            //InitClientSocket();

            //udpServerSocket = new Socket(AddressFamily.InterNetwork,
            //  SocketType.Dgram, ProtocolType.Udp);
            //udpServerSocket.Bind(ep);
            //udpServerSocket.BeginReceiveFrom(buffer, 0, 1024, SocketFlags.None, ref ep, new AsyncCallback(ReceiveData), udpServerSocket);

            //stateMonitor1.
            //checkTimer.Interval = 4000;
            //checkTimer.AutoReset = true;
            //checkTimer.Elapsed += new System.Timers.ElapsedEventHandler(checkTimer_Elapsed);
            //checkTimer.Start();
            stateMonitor1.doInitialization(this);
            stateMonitor1.Start();
            stateMonitor1.Type = StateMonitor.LocationType.BottomLeft;
            stateMonitor1.X = 2;
            stateMonitor1.Y = 0;

            // 注册事件（巷道选择自定义控件必须实装代码）
            this.selectTunnelSimple1.TunnelNameChanged += new LibCommonForm.SelectTunnelSimple.TunnelNameChangedEventHandler(TunnelNameChanged);
            //// 设置日期控件格式
            this._dateTimeStart.Text = DateTime.Now.ToString("yyyy-MM-dd") + " 00:00:00";
            this._dateTimeEnd.Text = DateTime.Now.ToString("yyyy-MM-dd") + " 23:59:59";
            //this.tChart1.GetEnvironment().SetMouseWheelScroll(FALSE);
            // 加载探头类型信息
            loadProbeTypeInfo();


            //// 增加滚轮事件
            //this.tChartM.MouseWheel += new System.Windows.Forms.MouseEventHandler(TeeChart_MouseWheel);
            //this.tChartN.MouseWheel += new System.Windows.Forms.MouseEventHandler(TeeChart_MouseWheel);
            //this.tChartT2.MouseWheel += new System.Windows.Forms.MouseEventHandler(TeeChart_MouseWheel);
        }

        private void MainForm_GE_Load(object sender, EventArgs e)
        {

            //monitoringDataAnalysisForm.MdiParent = this;
            //this.panel1.Controls.Add(monitoringDataAnalysisForm);
            //monitoringDataAnalysisForm.WindowState = FormWindowState.Maximized;
            //monitoringDataAnalysisForm.Dock = DockStyle.Fill;
            //monitoringDataAnalysisForm.Show();
            //monitoringDataAnalysisForm.Activate();

            //浮动工具条中文设置

            int height = panel1.ClientRectangle.Height - 66;
            tChartT2.Height = Convert.ToInt32(Math.Round(height * 0.3));
            tChartN.Height = Convert.ToInt32(Math.Round(height * 0.3));
            //tChartN.Location = new Point(tChartN.Location.X, tChartN.Location.Y + 10);
            tChartM.Height = height - tChartT2.ClientRectangle.Height - tChartN.ClientRectangle.Height;
            //tChartM.Anchor = AnchorStyles.Bottom;

            DXSeting.floatToolsLoadSet();
        }

        #region ////////////传感器设置
        private void mniSensorManagement_Click(object sender, EventArgs e)
        {
            ProbeInfoManagement probeInfoManagementForm = new ProbeInfoManagement(this);
            probeInfoManagementForm.Show();
        }
        #endregion

        #region ////////////系统设置

        /// <summary>
        /// 密码修改
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void mniMMXG_Click(object sender, EventArgs e)
        {
            PasswordUpdate passwordUpdateForm = new PasswordUpdate();
            passwordUpdateForm.ShowDialog();
        }
        #endregion

        private void MainForm_GE_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (DialogResult.Yes == MessageBox.Show("您确定要退出系统吗?", "系统提示", MessageBoxButtons.YesNo, MessageBoxIcon.Warning))
            {
                e.Cancel = false;
            }
            else
            {
                e.Cancel = true;
            }
        }

        private void MainForm_GE_FormClosed(object sender, FormClosedEventArgs e)
        {
            Application.Exit();
        }
        //监控数据分析
        private void mniMonitoring_Click(object sender, EventArgs e)
        {

        }

        //关于
        private void mniAbout_Click(object sender, EventArgs e)
        {

        }
        //帮助文件
        private void mniHelpFile_Click(object sender, EventArgs e)
        {

        }

        //传感器管理
        private void mniSensorManagement_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            ProbeInfoManagement ProbeInfoManagementForm = new ProbeInfoManagement(this);
            ProbeInfoManagementForm.Show();
        }

        /// <summary>
        /// 数据库设置
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void mniDatabaseSet_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            DatabaseManagement databaseManagementForm = new DatabaseManagement(LibDatabase.DATABASE_TYPE.GasEmissionDB);
            databaseManagementForm.ShowDialog();
        }

        //人员信息管理
        private void mniUserInfoMana_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            UserInformationDetailsManagementFather uidmf = new UserInformationDetailsManagementFather();
            uidmf.ShowDialog();
        }

        //部门信息管理
        private void mniDepartment_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            DepartmentInformation di = new DepartmentInformation();
            di.ShowDialog();
        }

        //用户信息管理
        private void mniUserLoginInfoMana_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            UserLoginInformationManagement ulim = new UserLoginInformationManagement();
            ulim.ShowDialog();
        }

        //用户组信息管理
        private void mniUserGroupInfoMana_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            UserGroupInformationManagement ugm = new UserGroupInformationManagement();
            ugm.ShowDialog();
        }

        //系统设置浮动工具条
        private void mniSystemSet_DockChanged(object sender, EventArgs e)
        {
            //mniSystemSet.Text = null;
        }

        //数据库设置浮动工具条
        private void mniDatabaseSetFloat_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            this.mniDatabaseSet_ItemClick(null, null);
        }

        //部门信息管理浮动工具条
        private void mniDepartmentFloat_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            this.mniDepartment_ItemClick(null, null);
        }

        //用户信息管理浮动工具条
        private void mniUserLoginInfoManaFloat_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            this.mniUserLoginInfoMana_ItemClick(null, null);
        }

        //用户组信息管理
        private void mniUserGroupInfoManaFloat_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            this.mniUserGroupInfoMana_ItemClick(null, null);
        }

        // 传感器数据管理
        private void mniSensorDataManage_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            GasConcentrationProbeDataManamement gasConcentrationProbeDataManamementForm = new GasConcentrationProbeDataManamement(this);
            gasConcentrationProbeDataManamementForm.Show();
        }

        //人员信息管理浮动工具条
        private void mniUserInfoManaFloat_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            this.mniUserInfoMana_ItemClick(null, null);
        }

        private void mniHelpFile_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            string strHelpFilePath = System.Windows.Forms.Application.StartupPath + Const_GE.System1_Help_File;
            try
            {
                System.Diagnostics.Process.Start(strHelpFilePath);
            }
            catch (Exception)
            {
                Alert.alert("帮助文件不存在或已损坏");
            }

        }


        private void _DXbtAbout_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            Const.strPicturepath = System.Windows.Forms.Application.StartupPath + Const_GE.Picture_Name;
            LibAbout.About libabout = new About(ProductName, ProductVersion);
            libabout.ShowDialog();
        }

        #region 实时监控

        /// <summary>
        /// 巷道选择事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TunnelNameChanged(object sender, TunnelEventArgs e)
        {
            this._lstProbeName.DataSource = null;
            this.currentTunnelId = this.selectTunnelSimple1.ITunnelId;
            this._T2Id = getT2Id(this.currentTunnelId);
        }

        /// <summary>
        /// 加载探头类型信息
        /// </summary>
        private void loadProbeTypeInfo()
        {
            DataSet ds = ProbeTypeBLL.selectAllProbeTypeInfo();
            if (ds.Tables[0].Rows.Count > 0)
            {
                this._lstProbeStyle.DataSource = ds.Tables[0];
                this._lstProbeStyle.DisplayMember = ProbeTypeDbConstNames.PROBE_TYPE_NAME;
                this._lstProbeStyle.ValueMember = ProbeTypeDbConstNames.PROBE_TYPE_ID;

                this._lstProbeStyle.SelectedIndex = -1;
            }
        }

        /// <summary>
        /// 开始实时监控
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void startRealTimeCurveMonitoring()
        {
            // 检查是否选择了巷道和传感器
            if (!Check())
                return;

            // reset Tee Chart

            TeeChartUtil.resetTeeChart(this.tChartM); // tChart1, 监控系统原始数据M
            TeeChartUtil.resetTeeChart(this.tChartT2); // tChart2, T2瓦斯浓度平均增加值Q
            TeeChartUtil.resetTeeChart(this.tChartN); // tChart3, 同一工序条件下瓦斯浓度变化值N

            // 开始时间
            _StartTime = DateTime.Now;

            // 清空datagridview
            this._dgvData.Rows.Clear();

            // 获取指定探头的旧数据 ----------用来填充曲线。
            DataSet _DsData = this.getOldDataByProbeId(this.currentProbeId);
            addDataSet2TeeChart(this.tChartM, _DsData, "M");
            addDataSet2TeeChart(this.tChartN, _DsData, "N");
            if (!String.IsNullOrEmpty(this._T2Id))
            {
                DataSet ds = this.getOldDataByProbeId(this._T2Id);
                addDataSet2TeeChart(this.tChartT2, ds, "T2");
            }

            if (this._dgvData.Rows.Count > 0)
            {
                // 定位滚动条
                this._dgvData.FirstDisplayedScrollingRowIndex = this._dgvData.Rows.Count - 1;
            }
            // 获取旧数据 ---------- End

            this.timer1.Enabled = true; // 启动定时器
        }

        /// <summary>
        /// 开始实时监控
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void stopRealTimeCurveMonitoring()
        {
            this.timer1.Enabled = false;
        }

        #endregion

        /// <summary>
        /// 计时器
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void timer1_Tick(object sender, EventArgs e)
        {
            updateMNData();  // Update raw data curve.
            updateT2Data(); // Update T2 curve.
        }

        private void updateT2Data()
        {
            if (_T2Id == string.Empty)
                return;
            DataSet ds = getLatest2RowsData(_T2Id);
            DateTime time = Convert.ToDateTime(ds.Tables[0].Rows[0][GasConcentrationProbeDataDbConstNames.RECORD_TIME].ToString());
            double value0 = Convert.ToDouble(ds.Tables[0].Rows[0][GasConcentrationProbeDataDbConstNames.PROBE_VALUE].ToString());
            double value1 = Convert.ToDouble(ds.Tables[0].Rows[1][GasConcentrationProbeDataDbConstNames.PROBE_VALUE].ToString());

            // 判断是否是最新数据
            if (time != this._LastTimeT2)
            {
                this._LastTimeT2 = time;

                double value = value1 - value0;
                t2DeltaSumValue += value;
                value = t2DeltaSumValue / ++t2DataCount;
                TeeChartUtil.addSingleData2TeeChart(tChartT2, this.dataCountPerFrame, time, value);
            }
        }

        // 更新M_N数据
        // 同一工序下，瓦斯浓度变化值N
        private void updateMNData()
        {
            DataSet ds = getLatest2RowsData(this.currentProbeId);
            DateTime time = Convert.ToDateTime(ds.Tables[0].Rows[0][GasConcentrationProbeDataDbConstNames.RECORD_TIME].ToString());
            DateTime time1 = Convert.ToDateTime(ds.Tables[0].Rows[1][GasConcentrationProbeDataDbConstNames.RECORD_TIME].ToString());
            double value = Convert.ToDouble(ds.Tables[0].Rows[0][GasConcentrationProbeDataDbConstNames.PROBE_VALUE].ToString());
            double value1 = Convert.ToDouble(ds.Tables[0].Rows[1][GasConcentrationProbeDataDbConstNames.PROBE_VALUE].ToString());

            double valueN = value - value1;

            // 判断是否是最新数据
            if (time != this._LastTimeMN && _LastTimeMN != DateTime.MinValue)
            {
                this._LastTimeMN = time;

                // 往DGV中填充数据
                this._dgvData.Rows.Add(value + "%", time);
                if (this._dgvData.Rows.Count > 0)
                {
                    // 定位滚动条
                    this._dgvData.FirstDisplayedScrollingRowIndex = this._dgvData.Rows.Count - 1;

                    // 瓦斯浓度超过安全范围 
                    if (value >= this.yellowDataThreshold && value <= this.redDataThreshold)
                    {
                        this._dgvData.Rows[this._dgvData.Rows.Count - 1].DefaultCellStyle.BackColor = Color.Yellow;
                    }
                    else if (value > this.redDataThreshold)
                    {
                        this._dgvData.Rows[this._dgvData.Rows.Count - 1].DefaultCellStyle.BackColor = Color.Red;
                    }
                }

                TeeChartUtil.addSingleData2TeeChart(tChartM, this.dataCountPerFrame, time, value);
                TeeChartUtil.addSingleData2TeeChart(tChartN, this.dataCountPerFrame, time, valueN);
            }
            else
            {
                this._LastTimeMN = time;
            }
        }

        private bool addDataSet2TeeChart(Steema.TeeChart.TChart tChart, DataSet ds, string type)
        {
            bool bReturn = false;

            if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                int sqlCnt = ds.Tables[0].Rows.Count;
                // 禁止自动生成列(※位置不可变)
                this._dgvData.AutoGenerateColumns = false;
                tChart.Series[0].Clear();
                // 重绘
                tChart.AutoRepaint = false;

                //// 重新设置X轴的最大值和最小值
                //tChart.Series[0].GetHorizAxis.SetMinMax
                //(
                //    Convert.ToDateTime(ds.Tables[0].Rows[sqlCnt - 1][GasConcentrationProbeDataDbConstNames.RECORD_TIME]).ToOADate(),
                //    Convert.ToDateTime(ds.Tables[0].Rows[0][GasConcentrationProbeDataDbConstNames.RECORD_TIME]).ToOADate()
                //);

                // 重新设置X轴的最大值和最小值
                // 如果数据量非常大的时候，一屏的数据将会非常密集，影响观察效果，因此需要设置合适的时间轴范围。
                DateTime startTime = Convert.ToDateTime(ds.Tables[0].Rows[0][GasConcentrationProbeDataDbConstNames.RECORD_TIME]);
                DateTime endTime = Convert.ToDateTime(ds.Tables[0].Rows[sqlCnt - 1][GasConcentrationProbeDataDbConstNames.RECORD_TIME]);

                TimeSpan ts1 = new TimeSpan(startTime.Ticks);
                TimeSpan ts2 = new TimeSpan(endTime.Ticks);
                TimeSpan ts = ts1.Subtract(ts2).Duration();

                // 如果时间跨度大于2个小时，则需要调整时间轴
                if (ts.Hours > 2)
                {
                    DateTime tmpTime = endTime.AddSeconds(-7200); // 7200seconds = 2 hours.
                    tChart.Series[0].GetHorizAxis.SetMinMax
                    (
                        tmpTime.ToOADate(),
                        endTime.ToOADate()
                    );
                }
                else
                {
                    tChart.Series[0].GetHorizAxis.SetMinMax
                                      (
                                          startTime.ToOADate(),
                                          endTime.ToOADate()
                                      );
                }

                //// 设置Y轴的最小值和最大值
                //tChart.Series[0].GetVertAxis.SetMinMax(0, 10);

                //// 设置Y轴间距
                tChart.Axes.Left.Increment = 0.1;

                addDataToTeeChart(tChart, ds, type);

                // 重绘
                tChart.AutoRepaint = true;
                this.Invoke(new MethodInvoker(tChart.Refresh));

                bReturn = true;
            }

            if (bReturn == false && type == "T2")
            {
                tChart.Header.Text = "该巷道没有T2传感器，或者T2没有数据。";
            }

            return bReturn;
        }

        private void addDataToTeeChart(Steema.TeeChart.TChart tChart, DataSet ds, string type)
        {
            if (type == "M")
            {
                addDataToTeeChartM(tChart, ds);
            }
            else if (type == "N")
            {
                addDataToTeeChartN(tChart, ds);
            }
            else if (type == "T2")
            {
                addDataToTeeChartT2(tChart, ds);
            }
        }

        /// <summary>
        /// 将DataSet中的数据添加到TeeChart中。
        /// 主要用于添加涌出量原始数据M
        /// </summary>
        /// <param name="tChart">TeeChart图表</param>
        /// <param name="ds">数据集</param>
        private void addDataToTeeChartM(Steema.TeeChart.TChart tChart, DataSet ds)
        {
            int sqlCnt = ds.Tables[0].Rows.Count;

            double maxVertValue = 0;
            double minVertValue = 0;

            for (int i = sqlCnt - 1; i >= 0; i--)
            {

                double value = Convert.ToDouble(ds.Tables[0].Rows[i][GasConcentrationProbeDataDbConstNames.PROBE_VALUE]);
                DateTime time = Convert.ToDateTime(ds.Tables[0].Rows[i][GasConcentrationProbeDataDbConstNames.RECORD_TIME]);

                if (value > maxVertValue)
                {
                    maxVertValue = value;
                }

                if (value < minVertValue)
                {
                    minVertValue = value;
                }
                this.Invoke(new MethodInvoker(delegate
                {
                    tChart.Series[0].Add(time, value);


                    // 往DGV中填充数据
                    this._dgvData.Rows.Add(value + "%", time);

                    // 瓦斯浓度超过安全范围 
                    if (value >= this.YellowDataThreshold && value <= RedDataThreshold)
                    {
                        this._dgvData.Rows[this._dgvData.Rows.Count - 1].DefaultCellStyle.BackColor = Color.Yellow;
                    }
                    else if (value > this.RedDataThreshold)
                    {
                        this._dgvData.Rows[this._dgvData.Rows.Count - 1].DefaultCellStyle.BackColor = Color.Red;
                    }
                }));
            }

            if (maxVertValue < 1)
            {
                tChart.Series[0].GetVertAxis.SetMinMax(0, 1);
            }
            else
            {
                tChart.Series[0].GetVertAxis.SetMinMax(minVertValue - minVertValue * 0.1, maxVertValue + maxVertValue * 0.1);
            }
        }

        /// <summary>
        /// 添加同一工序条件下瓦斯浓度变化值N
        /// </summary>
        /// <param name="tChart"></param>
        /// <param name="ds"></param>
        private void addDataToTeeChartN(Steema.TeeChart.TChart tChart, DataSet ds)
        {
            int sqlCnt = ds.Tables[0].Rows.Count;
            double value = 0;
            DateTime time = new DateTime();

            double maxVertValue = 0;
            double minVertValue = 0;

            for (int i = 0; i < sqlCnt; i++)
            {
                if ((i + 1) != ds.Tables[0].Rows.Count)
                {
                    value = Convert.ToDouble(
                        ds.Tables[0].Rows[i + 1][GasConcentrationProbeDataDbConstNames.PROBE_VALUE])
                        -
                        Convert.ToDouble(ds.Tables[0].Rows[i][GasConcentrationProbeDataDbConstNames.PROBE_VALUE]
                        );
                    time = Convert.ToDateTime(ds.Tables[0].Rows[i + 1][GasConcentrationProbeDataDbConstNames.RECORD_TIME]);

                    if (value > maxVertValue)
                    {
                        maxVertValue = value;
                    }

                    if (value < minVertValue)
                    {
                        minVertValue = value;
                    }

                    tChart.Series[0].Add(time, value);
                }

            }

            tChart.Series[0].GetVertAxis.SetMinMax(minVertValue - 1, maxVertValue + 1);

        }

        /// <summary>
        /// 添加T2瓦斯浓度平均增加值
        /// </summary>
        /// <param name="tChart"></param>
        /// <param name="ds"></param>
        private void addDataToTeeChartT2(Steema.TeeChart.TChart tChart, DataSet ds)
        {
            int sqlCnt = ds.Tables[0].Rows.Count;
            double value = 0;
            DateTime time = new DateTime();
            double sumValue = 0;

            double maxVertValue = 0;
            double minVertValue = 0;

            for (int i = 0; i < sqlCnt; i++)
            {
                if ((i + 1) != ds.Tables[0].Rows.Count)
                {
                    sumValue = sumValue +
                       (Convert.ToDouble(ds.Tables[0].Rows[i + 1][GasConcentrationProbeDataDbConstNames.PROBE_VALUE])
                       - Convert.ToDouble(ds.Tables[0].Rows[i][GasConcentrationProbeDataDbConstNames.PROBE_VALUE]));

                    value = sumValue / (i + 1);
                    time = Convert.ToDateTime(ds.Tables[0].Rows[i + 1][GasConcentrationProbeDataDbConstNames.RECORD_TIME]);

                    if (value > maxVertValue)
                    {
                        maxVertValue = value;
                    }

                    if (value < minVertValue)
                    {
                        minVertValue = value;
                    }

                    tChart.Series[0].Add(time, value);
                }
            }
            tChart.Series[0].GetVertAxis.SetMinMax(minVertValue - 1, maxVertValue + 1);
        }

        /// <summary>
        /// 开始实时数据监控
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void _rbtnRealtime_Click(object sender, EventArgs e)
        {
            startRealTimeCurveMonitoring();

            _dateTimeStart.Enabled = false;
            _dateTimeEnd.Enabled = false;
            _btnBeforeDay.Enabled = false;
            _btnNow.Enabled = false;
            _btnAfterDay.Enabled = false;
            _btnQuery.Enabled = false;
        }

        /// <summary>
        /// 开始历史数据查询
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void _rbtnHistory_Click(object sender, EventArgs e)
        {
            // 选择巷道设为可用
            this.selectTunnelSimple1.Enabled = true;
            // 探头类型设为可用
            this._lstProbeStyle.Enabled = true;
            // 探头名称设为可用
            this._lstProbeName.Enabled = true;

            // 停止实时数据监控
            stopRealTimeCurveMonitoring();

            // 清空datagridview
            this._dgvData.Rows.Clear();
            // 清空曲线1的fastline
            this.tChartM.Series[0].Clear();
            // 清空曲线2的fastline
            this.tChartT2.Series[0].Clear();
            // 清空曲线3的fastline
            this.tChartN.Series[0].Clear();

            _dateTimeStart.Enabled = true;
            _dateTimeEnd.Enabled = true;
            _btnBeforeDay.Enabled = true;
            _btnNow.Enabled = true;
            _btnAfterDay.Enabled = true;
            _btnQuery.Enabled = true;
        }

        /// <summary>
        /// 执行历史数据查询
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void _btnQuery_Click(object sender, EventArgs e)
        {
            stopRealTimeCurveMonitoring();

            // 检查是否选择了巷道和传感器
            if (!Check())
                return;
            // 清空datagridview
            this._dgvData.Rows.Clear();

            Thread t2 = new Thread(new ThreadStart(LoadAllHistoryData));
            t2.Start();


            //t.Abort();
        }

        private void LoadAllHistoryData()
        {
            String probeName = "";
            String dateTimeStart = "";
            String datetimeEnd = "";
            _lstProbeName.MouseUp -= _lstProbeName_SelectedIndexChanged;

            this.Invoke(new MethodInvoker(delegate
            {
                _lblLoading.Visible = true;
                _btnQuery.Text = "查询中...";
                _btnQuery.Enabled = false;
                probeName = this._lstProbeName.SelectedValue.ToString();
                dateTimeStart = this._dateTimeStart.Text;
                datetimeEnd = this._dateTimeEnd.Text;
                _rbtnRealtime.Enabled = false;
                _rbtnHistory.Enabled = false;
            }));
            DataSet ds = this.getHistoryData(
                            probeName,
                            dateTimeStart,
                            datetimeEnd
                            );

            // load监控系统原始数据M历史数据
            loadHistoryDataM(this.tChartM, ds);
            // load同一工序条件下瓦斯浓度变化值N历史数据
            loadHistoryDataN(this.tChartN, ds);
            // loadT2瓦斯浓度平均增加值Q历史数据
            loadHistoryDataT2(this.tChartT2);

            this.Invoke(new MethodInvoker(delegate
            {
                _lblLoading.Visible = false;
                _btnQuery.Text = "查询";
                _btnQuery.Enabled = true;
                _rbtnRealtime.Enabled = true;
                _rbtnHistory.Enabled = true;
            }));
            _lstProbeName.MouseUp += _lstProbeName_SelectedIndexChanged;
        }



        /// <summary>
        /// load瓦斯浓度历史数据--监控系统原始数据M
        /// </summary>
        private void loadHistoryDataM(Steema.TeeChart.TChart tChart, DataSet ds)
        {
            int sqlCnt = ds.Tables[0].Rows.Count;

            if (sqlCnt > 0)
            {
                tChart.Header.Text = "监控系统原始数据M";
                addDataSet2TeeChart(tChartM, ds, "M");
            }
            else
            {
                //Alert.alert("没有瓦斯浓度原始数据M！");
                tChart.Header.Text = "没有瓦斯浓度原始数据M！";
            }
        }

        /// <summary>
        /// loadT2瓦斯浓度平均增加值Q历史数据
        /// </summary>
        private void loadHistoryDataT2(Steema.TeeChart.TChart tChart)
        {
            DataSet ds = this.getHistoryData(
              this._T2Id,
               this._dateTimeStart.Text,
               this._dateTimeEnd.Text
               );
            int sqlCnt = 0;
            if (ds.Tables.Count > 0)
            {
                sqlCnt = ds.Tables[0].Rows.Count;
            }
            if (sqlCnt > 0)
            {
                tChart.Header.Text = "T2瓦斯浓度平均增加值Q";
                addDataSet2TeeChart(tChart, ds, "T2");
            }
            else
            {
                //Alert.alert("没有T2瓦斯浓度数据！");
                tChart.Header.Text = "没有T2瓦斯浓度数据！";
            }

        }

        /// <summary>
        /// 同一工序条件下瓦斯浓度变化值N
        /// </summary>
        private void loadHistoryDataN(Steema.TeeChart.TChart tChart, DataSet ds)
        {
            int sqlCnt = ds.Tables[0].Rows.Count;

            if (sqlCnt > 0)
            {
                tChart.Header.Text = "同一工序条件下瓦斯浓度变化值N";
                addDataSet2TeeChart(tChart, ds, "N");
            }
            else
            {
                //Alert.alert("没有瓦斯浓度数据N！");
                tChart.Header.Text = "没有瓦斯浓度数据N！";
            }
        }

        /// <summary>
        /// 设置曲线1的Marks显示与否
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void _ckbSetMarks1_Click(object sender, EventArgs e)
        {
            if (this._ckbSetMarks1.Checked == true)
            {
                // Mark显示
                //this.fastLine1.Marks.Visible = true;
                this.tChartM.Series[0].Marks.Visible = true;
            }
            else
            {
                // Mark隐藏
                //this.fastLine1.Marks.Visible = false;
                this.tChartM.Series[0].Marks.Visible = false;
            }
        }

        #region T2瓦斯浓度平均增加值Q


        /// <summary>
        /// 设置曲线2的Marks显示与否
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void _ckbSetMarks2_Click(object sender, EventArgs e)
        {
            if (this._ckbSetMarks2.Checked == true)
            {
                // Mark显示
                this.tChartT2.Series[0].Marks.Visible = true;
            }
            else
            {
                // Mark隐藏
                this.tChartT2.Series[0].Marks.Visible = false;
            }
        }
        #endregion

        #region 同一工序条件下瓦斯浓度变化值N

        /// <summary>
        /// 设置曲线3的Marks显示与否
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void _ckbSetMarks3_Click(object sender, EventArgs e)
        {
            if (this._ckbSetMarks3.Checked == true)
            {
                // Mark显示
                this.tChartN.Series[0].Marks.Visible = true;
            }
            else
            {
                // Mark隐藏
                this.tChartN.Series[0].Marks.Visible = false;
            }
        }

        #endregion

        ///// <summary>
        ///// 模拟实时往数据库传输数据/测试时使用，功能完成后要删除
        ///// </summary>
        ///// <param name="sender"></param>
        ///// <param name="e"></param>
        //private void timer2_Tick(object sender, EventArgs e)
        //{
        //    DateTime time = DateTime.Now;
        //    double value = rnd.Next(200);
        //    if (Math.Abs(value) > 1.0e+4) value = 0.0;

        //    GasConcentrationProbeDataEntity gasConcentrationProbeDataEntity = new GasConcentrationProbeDataEntity();

        //    // 探头编号
        //    gasConcentrationProbeDataEntity.ProbeId = "001";
        //    // 数值
        //    gasConcentrationProbeDataEntity.ProbeValue = value / 100;
        //    // 时间
        //    gasConcentrationProbeDataEntity.RecordTime = time;
        //    // 类型(矿监控系统读取)
        //    gasConcentrationProbeDataEntity.RecordType = Const_GE.RECORDTYPE_COMPUTER;

        //    bool result = GasConcentrationProbeDataBLL.insertGasConcentrationProbeData(gasConcentrationProbeDataEntity);

        //    // 添加失败的场合
        //    if (!result)
        //    {
        //        // TODO： 暂定
        //    }

        //}

        /// <summary>
        /// 后一天
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void _btnAfterDay_Click(object sender, EventArgs e)
        {
            // 清空datagridview
            this._dgvData.Rows.Clear();
            // 清空曲线1的fastline
            this.tChartM.Series[0].Clear();
            // 清空曲线2的fastline
            this.tChartT2.Series[0].Clear();
            // 清空曲线3的fastline
            this.tChartN.Series[0].Clear();

            this.timer1.Enabled = false;
            // 历史数据分析设为选中
            this._rbtnHistory.Checked = true;

            if (this.Check())
            {
                DateTime dtStart = Convert.ToDateTime(this._dateTimeStart.Text).AddDays(1);
                DateTime dtEnd = Convert.ToDateTime(this._dateTimeEnd.Text).AddDays(1);

                this._dateTimeStart.Text = dtStart.ToString();
                this._dateTimeEnd.Text = dtEnd.ToString();

                //this._dateTimeStart.Format = DateTimePickerFormat.Custom;
                //this._dateTimeStart.CustomFormat = Const.DATE_FORMART_YYYY_MM_DD;
                //this._dateTimeStart.Text = DateTime.Now.ToString("yyyy-MM-dd") + " 00:00:00";
                //this._dateTimeEnd.Text = DateTime.Now.ToString("yyyy-MM-dd") + " 23:59:59";
                //this._dateTimeEnd.Format = DateTimePickerFormat.Custom;
                //this._dateTimeEnd.CustomFormat = Const.DATE_FORMART_YYYY_MM_DD;
                _btnQuery_Click(sender, e);
            }
        }

        /// <summary>
        /// 前一天
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void _btnBeforeDay_Click(object sender, EventArgs e)
        {
            // 清空datagridview
            this._dgvData.Rows.Clear();
            // 清空曲线1的fastline
            this.tChartM.Series[0].Clear();
            // 清空曲线2的fastline
            this.tChartT2.Series[0].Clear();
            // 清空曲线3的fastline
            this.tChartN.Series[0].Clear();

            this.timer1.Enabled = false;
            // 历史数据分析设为选中
            this._rbtnHistory.Checked = true;

            if (this.Check())
            {
                DateTime dtStart = Convert.ToDateTime(this._dateTimeStart.Text).AddDays(-1);
                DateTime dtEnd = Convert.ToDateTime(this._dateTimeEnd.Text).AddDays(-1);

                this._dateTimeStart.Text = dtStart.ToString();
                this._dateTimeEnd.Text = dtEnd.ToString();

                _btnQuery_Click(sender, e);
            }
        }

        /// <summary>
        /// 当前时间
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void _btnNow_Click(object sender, EventArgs e)
        {
            // 清空datagridview
            this._dgvData.Rows.Clear();
            // 清空曲线1的fastline
            this.tChartM.Series[0].Clear();
            // 清空曲线2的fastline
            this.tChartT2.Series[0].Clear();
            // 清空曲线3的fastline
            this.tChartN.Series[0].Clear();

            this.timer1.Enabled = false;
            // 历史数据分析设为选中
            this._rbtnHistory.Checked = true;

            if (this.Check())
            {
                this._dateTimeStart.Text = DateTime.Now.ToString("yyyy-MM-dd") + " 00:00:00";
                this._dateTimeEnd.Text = DateTime.Now.ToString("yyyy-MM-dd") + " 23:59:59";

                _btnQuery_Click(sender, e);
            }
        }

        /// <summary>
        /// 检验
        /// </summary>
        private bool Check()
        {
            // 没有选择巷道 
            if (currentTunnelId <= Const.INVALID_ID)
            {
                Alert.alert(Const_GE.TUNNEL_NAME_MUST_INPUT);
                return false;
            }

            // 没有选择探头
            if (this._lstProbeName.SelectedItems.Count == 0)
            {
                Alert.alert(Const_GE.PROBE_MUST_CHOOSE);
                return false;
            }

            return true;
        }

        /// 根据探头编号和开始结束时间，获取特定探头和特定时间段内的【瓦斯浓度探头数据】
        /// </summary>
        /// <param name="strProbeId">探头编号</param>
        /// <param name="dtStartTime">开始时间</param>
        /// <returns>特定探头和特定时间段内的【瓦斯浓度探头数据】</returns>
        public static DataSet selectAllGasDataByProbeIdAndStartTime(string strProbeId, DateTime dtStartTime)
        {
            StringBuilder sqlStr = new StringBuilder();
            sqlStr.Append("SELECT * FROM " + GasConcentrationProbeDataDbConstNames.TABLE_NAME);
            sqlStr.Append(" WHERE ");
            sqlStr.Append(GasConcentrationProbeDataDbConstNames.PROBE_ID + " ='" + strProbeId + "'");
            sqlStr.Append(" AND ");
            sqlStr.Append(GasConcentrationProbeDataDbConstNames.RECORD_TIME + " >= '" + dtStartTime + "'");

            ManageDataBase db = new ManageDataBase(DATABASE_TYPE.GasEmissionDB);
            DataSet ds = db.ReturnDS(sqlStr.ToString());
            return ds;
        }

        // Database Operation
        /// <summary>
        /// 获取指定探头的最新实时数据
        /// </summary>
        /// <param name="iProbeId"></param>
        /// <returns></returns>
        public static string getT2Id(int tunnelId)
        {
            StringBuilder sqlStr = new StringBuilder();
            sqlStr.Append("SELECT [PROBE_ID] FROM " + ProbeManageDbConstNames.TABLE_NAME);
            sqlStr.Append(" WHERE ");
            sqlStr.Append("TUNNEL_ID = " + tunnelId);
            sqlStr.Append(" AND [PROBE_NAME]='T2'");

            ManageDataBase db = new ManageDataBase(DATABASE_TYPE.GasEmissionDB);
            DataSet ds = db.ReturnDS(sqlStr.ToString());
            if (ds.Tables[0].Rows.Count <= 0)
                return string.Empty;
            else
            {
                return ds.Tables[0].Rows[0][0].ToString();
            }
        }

        /// <summary>
        /// 获取指定探头的最新2行实时数据
        /// </summary>
        /// <param name="iProbeId"></param>
        /// <returns></returns>
        public static DataSet getLatest2RowsData(string iProbeId)
        {
            StringBuilder sqlStr = new StringBuilder();
            sqlStr.Append("SELECT TOP 2 * FROM " + GasConcentrationProbeDataDbConstNames.TABLE_NAME);
            sqlStr.Append(" WHERE " + GasConcentrationProbeDataDbConstNames.PROBE_ID + " = " + iProbeId);
            sqlStr.Append(" ORDER BY " + GasConcentrationProbeDataDbConstNames.PROBE_DATA_ID + " DESC");

            ManageDataBase db = new ManageDataBase(DATABASE_TYPE.GasEmissionDB);
            DataSet ds = db.ReturnDS(sqlStr.ToString());
            return ds;
        }

        /// <summary>
        /// 获取瓦斯浓度数据
        /// @param probeId - 探头ID
        /// @param startTime - 开始时间
        /// @param endTime - 结束时间
        /// </summary>
        /// <returns></returns>
        private DataSet getHistoryData(string probeId, string startTime, string endTime)
        {
            StringBuilder sqlStr = new StringBuilder();
            sqlStr.Append("SELECT ");
            sqlStr.Append("* ");
            sqlStr.Append("FROM ");
            sqlStr.Append(GasConcentrationProbeDataDbConstNames.TABLE_NAME + " ");
            sqlStr.Append("WHERE ");
            sqlStr.Append(GasConcentrationProbeDataDbConstNames.PROBE_ID + " = " + probeId + " ");
            sqlStr.Append("AND ");
            sqlStr.Append(GasConcentrationProbeDataDbConstNames.RECORD_TIME + " >= '" + startTime + "' ");
            sqlStr.Append("AND ");
            sqlStr.Append(GasConcentrationProbeDataDbConstNames.RECORD_TIME + " <= '" + endTime + "' ");
            sqlStr.Append("ORDER BY RECORD_TIME ");

            ManageDataBase db = new ManageDataBase(DATABASE_TYPE.GasEmissionDB);
            DataSet ds = db.ReturnDS(sqlStr.ToString());

            return ds;
        }

        /// <summary>
        /// 获取指定探头过去一段时间的若干数据
        /// </summary>
        /// <param name="probeId">探头Id</param>
        /// <returns></returns>
        private DataSet getOldDataByProbeId(string probeId)
        {
            StringBuilder sqlStr = new StringBuilder();
            sqlStr.Append("SELECT ");
            sqlStr.Append("TOP " + (this.dataCountPerFrame - 1) + "* ");
            sqlStr.Append("FROM ");
            sqlStr.Append(GasConcentrationProbeDataDbConstNames.TABLE_NAME + " ");
            sqlStr.Append("WHERE ");
            sqlStr.Append(GasConcentrationProbeDataDbConstNames.PROBE_ID + " = " + probeId + " ");
            sqlStr.Append("AND ");
            //sqlStr.Append(GasConcentrationProbeDataDbConstNames.RECORD_TIME + " >= '" + this._dateTimeStart.Text + "' ");
            //sqlStr.Append("AND ");
            sqlStr.Append(GasConcentrationProbeDataDbConstNames.RECORD_TIME + " <= '" + DateTime.Now.ToString() + "' ");
            sqlStr.Append("ORDER BY RECORD_TIME DESC");

            ManageDataBase db = new ManageDataBase(DATABASE_TYPE.GasEmissionDB);
            DataSet ds = db.ReturnDS(sqlStr.ToString());

            return ds;
        }

        // 监控系统参数设置
        private void bbiMonitorSetting_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            MoniroSettings ms = new MoniroSettings(this);
            ms.ShowDialog();
        }

        // 退出
        private void bbiExit_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            Application.Exit();
        }

        // 坏数据剔除
        private void bbiBadDataEdit_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            BadDataDelete bdd = new BadDataDelete(this);
            bdd.ShowDialog();
        }

        private void StartBarLoading()
        {
            while (true)
            {
                Thread.Sleep(1000);   //线程1休眠100毫秒
                _lblLoading.Invoke(loading);
            }

        }

        private void BarLoading()
        {
            switch (_lblLoading.Text)
            {
                case "数据加载中": _lblLoading.Text = "数据加载中."; break;
                case "数据加载中.": _lblLoading.Text = "数据加载中.."; break;
                case "数据加载中..": _lblLoading.Text = "数据加载中..."; break;
                case "数据加载中...": _lblLoading.Text = "数据加载中"; break;
            }
            //while (true)
            //{
            //    _lblLoading.Text = "数据加载中";
            //    Thread.Sleep(1000);
            //    _lblLoading.Text = "数据加载中.";
            //    Thread.Sleep(1000);
            //    _lblLoading.Text = "数据加载中..";
            //    Thread.Sleep(1000);
            //    _lblLoading.Text = "数据加载中...";
            //    Thread.Sleep(1000);
            //}
        }

        //void ReceiveData(IAsyncResult iar)
        //{
        //    // Create temporary remote end Point
        //    IPEndPoint sender = new IPEndPoint(IPAddress.Any, 0);
        //    EndPoint tempRemoteEP = (EndPoint)sender;

        //    // Get the Socket
        //    Socket remote = (Socket)iar.AsyncState;

        //    // Call EndReceiveFrom to get the received Data
        //    int recv = remote.EndReceiveFrom(iar, ref tempRemoteEP);

        //    // Get the Data from the buffer to a string
        //    string stringData = Encoding.ASCII.GetString(buffer, 0, recv);
        //    Console.WriteLine(stringData);

        //    // update Timestamp
        //    lastUpdate = DateTime.Now.ToUniversalTime();

        //    // Restart receiving
        //    if (!this.IsDisposed)
        //    {
        //        udpServerSocket.BeginReceiveFrom(buffer, 0, 1024, SocketFlags.None, ref ep, new AsyncCallback(ReceiveData), udpServerSocket);
        //    }
        //}



        //private void checkTimer_Elapsed(object sender, System.Timers.ElapsedEventArgs e)
        //{
        //    // Calculate the Timespan since the Last Update from the Client.
        //    TimeSpan timeSinceLastHeartbeat = DateTime.Now.ToUniversalTime() - lastUpdate;

        //    // Set Lable Text depending of the Timespan
        //    if (timeSinceLastHeartbeat > TimeSpan.FromSeconds(4))
        //    {

        //        //UpdateInfo("离线", Color.Red);
        //        this.online = false;

        //        // Clear data.
        //        //this.tunnelIdWarning = -1;
        //        //this.tunnelNameWarning = string.Empty;
        //        if (_lblStatus.InvokeRequired)
        //        {
        //            this.Invoke(new MethodInvoker(delegate
        //            {
        //                _lblStatus.Text = "离线（单击连接）";
        //                _lblStatus.ForeColor = Color.Red;
        //            }));
        //        }
        //    }
        //    else
        //    {
        //        //this.Invoke(new MethodInvoker(delegate
        //        //{
        //        if (_lblStatus.InvokeRequired)
        //        {
        //            this.Invoke(new MethodInvoker(delegate
        //            {
        //                _lblStatus.Text = "联机";
        //            }));
        //        }
        //        //UpdateInfo("联机", Color.Green);
        //        this.online = true;
        //    }
        //}

        private void _lstProbeName_SelectedIndexChanged(object sender, EventArgs e)
        {
            this._rbtnRealtime.Checked = true;

            currentProbeId = this._lstProbeName.SelectedValue == null ? "-1" : this._lstProbeName.SelectedValue.ToString();


            _dateTimeStart.Enabled = false;
            _dateTimeEnd.Enabled = false;
            _btnBeforeDay.Enabled = false;
            _btnNow.Enabled = false;
            _btnAfterDay.Enabled = false;
            _btnQuery.Enabled = false;


            // 开始实时数据监控
            startRealTimeCurveMonitoring();
        }

        private void _lstProbeStyle_SelectedIndexChanged(object sender, EventArgs e)
        {
            this._lstProbeName.DataSource = null;

            // 没有选择巷道
            if (currentTunnelId <= Const.INVALID_ID)
            {
                Alert.alert(Const_GE.TUNNEL_NAME_MUST_INPUT);
            }
            else
            {
                // 根据巷道编号和探头类型编号获取探头信息
                DataSet ds = ProbeManageBLL.selectProbeManageInfoByTunnelIDAndProbeType(
                    currentTunnelId,
                    Convert.ToInt32(this._lstProbeStyle.SelectedValue));

                if (ds.Tables[0].Rows.Count > 0)
                {
                    this._lstProbeName.DataSource = ds.Tables[0];
                    this._lstProbeName.DisplayMember = ProbeManageDbConstNames.PROBE_NAME;
                    this._lstProbeName.ValueMember = ProbeManageDbConstNames.PROBE_ID;

                    this._lstProbeName.SelectedIndex = -1;
                }
            }
        }

        private void _rbtnRealtime_Click_1(object sender, EventArgs e)
        {

            // 停止实时数据监控

            // 清空datagridview
            this._dgvData.Rows.Clear();
            // 清空曲线1的fastline
            this.tChartM.Series[0].Clear();
            // 清空曲线2的fastline
            this.tChartT2.Series[0].Clear();
            // 清空曲线3的fastline
            this.tChartN.Series[0].Clear();

            _dateTimeStart.Enabled = false;
            _dateTimeEnd.Enabled = false;
            _btnBeforeDay.Enabled = false;
            _btnNow.Enabled = false;
            _btnAfterDay.Enabled = false;
            _btnQuery.Enabled = false;

            startRealTimeCurveMonitoring();
        }


        ///// <summary>
        ///// TeeChart鼠标滚轮事件
        ///// 当e.Delta > 0时鼠标滚轮是向上滚动，e.Delta < 0时鼠标滚轮向下滚动。
        ///// </summary>
        ///// <param name="sender"></param>
        ///// <param name="e"></param>
        //public void TeeChart_MouseWheel(object sender, MouseEventArgs e)
        //{
        //    Steema.TeeChart.TChart tChart = sender as Steema.TeeChart.TChart;
        //    double minHorizValue = tChart.Series[0].GetHorizAxis.MinXValue;
        //    double maxHorizValue = tChart.Series[0].GetHorizAxis.MaxXValue;

        //    DateTime startTime = DateTime.FromOADate(minHorizValue);
        //    DateTime endTime = DateTime.FromOADate(maxHorizValue);

        //    if (e.Delta > 0) //鼠标滚轮是向上滚动
        //    {
        //        // 滚轮向上放大ZoomIn
        //        startTime.AddSeconds(-1200);
        //        endTime.AddSeconds(1200);
        //    }
        //    else if (e.Delta < 0) // 鼠标滚轮向下滚动
        //    {
        //        // 滚轮向下缩小ZoomOut
        //        startTime.AddSeconds(1200);
        //        endTime.AddSeconds(-1200);
        //    }

        //    tChart.Series[0].GetHorizAxis.SetMinMax
        //    (
        //        startTime.ToOADate(),
        //        endTime.ToOADate()
        //    );
        //}

        //// 鼠标滚轮事件要在空间获得焦点时才能实现，所以最好添加如下代码：
        //private void tChartM_MouseEnter(object sender, EventArgs e)
        //{
        //    this.tChartM.Focus();
        //}
    }
}
