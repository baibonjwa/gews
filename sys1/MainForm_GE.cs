// ******************************************************************
// 概  述：系统一主界面
// 作  者：伍鑫
// 创建日期：
// 版本号：V1.0
// 版本信息：
// V1.0 新建
// ******************************************************************
using System;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Globalization;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraBars;
using LibCommonControl;
using LibCommonForm;
using LibCommon;
using LibBusiness;
using LibDatabase;
using LibAbout;
using LibEntity;
using Steema.TeeChart;
using TeeChartWrapper;
using System.Threading;
using DepartmentInformation = LibCommonForm.DepartmentInformation;

namespace _1.GasEmission
{
    public partial class MainFormGe
    {
        private int _updateFrequency; // 10s

        private int _currentTunnelId = -1;
        private string _currentProbeId = string.Empty;
        private string _t2Id = string.Empty;

        private DateTime _lastTimeT2; // T2的数据更新时间
        private DateTime _lastTimeMn; // M/N 的数据更新时间

        public bool OnLine { get; set; }
        // 配置文件
        public string ConfigFileName
        {
            get { return configFileName; }
        }

        // 曲线更新频率
        public int UpdateFrequency
        {
            get { return _updateFrequency; }
            set
            {
                _updateFrequency = value;
                timer1.Interval = value * 1000;
            }
        }

        // 曲线每一帧显示的数据个数
        public int DataCountPerFrame { get; set; }

        public double RedDataThreshold { get; set; }

        public double YellowDataThreshold { get; set; }

        public double BadDataThreshold { get; set; }

        public bool EnableDeleteAndModifyBtn { get; set; }

        public MainFormGe(BarButtonItem mniAbout)
        {
            EnableDeleteAndModifyBtn = true;
            this.mniAbout = mniAbout;
            InitializeComponent();
            doInitilization();


            var fp = new FileProperties(configFileName);

            int iValue;
            double dValue;
            int.TryParse(fp.get("countperframe"), out iValue);
            DataCountPerFrame = iValue;
            int.TryParse(fp.get("updatefrequency"), out iValue);
            _updateFrequency = iValue;
            double.TryParse(fp.get("redthreshold"), out dValue);
            RedDataThreshold = dValue;
            double.TryParse(fp.get("yellowthreshold"), out dValue);
            YellowDataThreshold = dValue;
            double.TryParse(fp.get("baddatathreshold"), out dValue);
            BadDataThreshold = dValue;

            //初始化客户端Socket
            //InitClientSocket();

            //udpServerSocket = new Socket(AddressFamily.InterNetwork,
            //  SocketType.Dgram, ProtocolType.Udp);
            //udpServerSocket.Bind(ep);//udpServerSocket.BeginReceiveFrom(buffer, 0, 1024, SocketFlags.None, ref ep, new AsyncCallback(ReceiveData), udpServerSocket);

            //stateMonitor1.
            //checkTimer.Interval = 4000;//checkTimer.AutoReset = true;
            //checkTimer.Elapsed += new System.Timers.ElapsedEventHandler(checkTimer_Elapsed);
            //checkTimer.Start();

            // 注册事件（巷道选择自定义控件必须实装代码）
            selectTunnelSimple1.TunnelNameChanged += TunnelNameChanged;
            //// 设置日期控件格式
            _dateTimeStart.Text = DateTime.Now.ToString("yyyy-MM-dd") + @" 00:00:00";
            _dateTimeEnd.Text = DateTime.Now.ToString("yyyy-MM-dd") + @" 23:59:59";
            //this.tChart1.GetEnvironment().SetMouseWheelScroll(FALSE);
            // 加载探头类型信息
            LoadProbeTypeInfo();


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

        private void MainForm_GE_FormClosing(object sender, FormClosingEventArgs e)
        {
            e.Cancel = DialogResult.Yes != MessageBox.Show(@"您确定要退出系统吗?", @"系统提示", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
        }

        private void MainForm_GE_FormClosed(object sender, FormClosedEventArgs e)
        {
            Application.Exit();
        }

        //传感器管理
        private void mniSensorManagement_ItemClick(object sender, ItemClickEventArgs e)
        {
            var probeInfoManagementForm = new ProbeInfoManagement(this);
            probeInfoManagementForm.Show();
        }

        /// <summary>
        /// 数据库设置
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void mniDatabaseSet_ItemClick(object sender, ItemClickEventArgs e)
        {
            var databaseManagementForm = new DatabaseManagement(DATABASE_TYPE.GasEmissionDB);
            databaseManagementForm.ShowDialog();
        }

        //人员信息管理
        private void mniUserInfoMana_ItemClick(object sender, ItemClickEventArgs e)
        {
            var uidmf = new UserInformationDetailsManagementFather();
            uidmf.ShowDialog();
        }

        //部门信息管理
        private void mniDepartment_ItemClick(object sender, ItemClickEventArgs e)
        {
            var di = new DepartmentInformation();
            di.ShowDialog();
        }

        //用户信息管理
        private void mniUserLoginInfoMana_ItemClick(object sender, ItemClickEventArgs e)
        {
            var ulim = new UserLoginInformationManagement();
            ulim.ShowDialog();
        }

        //用户组信息管理
        private void mniUserGroupInfoMana_ItemClick(object sender, ItemClickEventArgs e)
        {
            var ugm = new UserGroupInformationManagement();
            ugm.ShowDialog();
        }

        //系统设置浮动工具条
        private void mniSystemSet_DockChanged(object sender, EventArgs e)
        {
            //mniSystemSet.Text = null;
        }

        //数据库设置浮动工具条
        private void mniDatabaseSetFloat_ItemClick(object sender, ItemClickEventArgs e)
        {
            mniDatabaseSet_ItemClick(null, null);
        }

        //部门信息管理浮动工具条
        private void mniDepartmentFloat_ItemClick(object sender, ItemClickEventArgs e)
        {
            mniDepartment_ItemClick(null, null);
        }

        //用户信息管理浮动工具条
        private void mniUserLoginInfoManaFloat_ItemClick(object sender, ItemClickEventArgs e)
        {
            mniUserLoginInfoMana_ItemClick(null, null);
        }

        //用户组信息管理
        private void mniUserGroupInfoManaFloat_ItemClick(object sender, ItemClickEventArgs e)
        {
            mniUserGroupInfoMana_ItemClick(null, null);
        }

        // 传感器数据管理
        private void mniSensorDataManage_ItemClick(object sender, ItemClickEventArgs e)
        {
            var gasConcentrationProbeDataManamementForm = new GasConcentrationProbeDataManamement(this);
            gasConcentrationProbeDataManamementForm.Show();
        }

        //人员信息管理浮动工具条
        private void mniUserInfoManaFloat_ItemClick(object sender, ItemClickEventArgs e)
        {
            mniUserInfoMana_ItemClick(null, null);
        }

        private void mniHelpFile_ItemClick(object sender, ItemClickEventArgs e)
        {
            var strHelpFilePath = Application.StartupPath + Const_GE.System1_Help_File;
            try
            {
                Process.Start(strHelpFilePath);
            }
            catch (Exception)
            {
                Alert.alert("帮助文件不存在或已损坏");
            }

        }


        private void _DXbtAbout_ItemClick(object sender, ItemClickEventArgs e)
        {
            Const.strPicturepath = Application.StartupPath + Const_GE.Picture_Name;
            var libabout = new About(ProductName, ProductVersion);
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
            _lstProbeName.DataSource = null;
            _currentTunnelId = selectTunnelSimple1.ITunnelId;
            _t2Id = GetT2Id(_currentTunnelId);
        }

        /// <summary>
        /// 加载探头类型信息
        /// </summary>
        private void LoadProbeTypeInfo()
        {
            var ds = ProbeTypeBLL.selectAllProbeTypeInfo();
            if (ds.Tables[0].Rows.Count <= 0) return;
            _lstProbeStyle.DataSource = ds.Tables[0];
            _lstProbeStyle.DisplayMember = ProbeTypeDbConstNames.PROBE_TYPE_NAME;
            _lstProbeStyle.ValueMember = ProbeTypeDbConstNames.PROBE_TYPE_ID;
            _lstProbeStyle.SelectedIndex = -1;
        }

        /// <summary>
        /// 开始实时监控
        /// </summary>
        private void StartRealTimeCurveMonitoring()
        {
            // 检查是否选择了巷道和传感器
            if (!Check())
                return;

            // reset Tee Chart

            TeeChartUtil.resetTeeChart(tChartM); // tChart1, 监控系统原始数据M
            TeeChartUtil.resetTeeChart(tChartT2); // tChart2, T2瓦斯浓度平均增加值Q
            TeeChartUtil.resetTeeChart(tChartN); // tChart3, 同一工序条件下瓦斯浓度变化值N

            // 清空datagridview
            _dgvData.Rows.Clear();

            // 获取指定探头的旧数据 ----------用来填充曲线。
            var dsData = GasConcentrationProbeData.FindHistaryData(_currentProbeId);
            AddDataSet2TeeChart(tChartM, dsData, "M");
            AddDataSet2TeeChart(tChartN, dsData, "N");
            if (!String.IsNullOrEmpty(_t2Id))
            {
                var ds = GasConcentrationProbeData.FindHistaryData(_t2Id);
                AddDataSet2TeeChart(tChartT2, ds, "T2");
            }

            if (_dgvData.Rows.Count > 0)
            {
                // 定位滚动条
                _dgvData.FirstDisplayedScrollingRowIndex = _dgvData.Rows.Count - 1;
            }
            // 获取旧数据 ---------- End

            timer1.Enabled = true; // 启动定时器
        }

        /// <summary>
        /// 开始实时监控
        /// </summary>
        private void StopRealTimeCurveMonitoring()
        {
            timer1.Enabled = false;
        }

        #endregion

        /// <summary>
        /// 计时器
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void timer1_Tick(object sender, EventArgs e)
        {
            UpdateMnData();  // Update raw data curve.
            UpdateT2Data(); // Update T2 curve.
        }

        private void UpdateT2Data()
        {
            if (_t2Id == string.Empty)
                return;
            var datas = GasConcentrationProbeData.FindNewRealData(_t2Id, 2);
            var time = datas[0].RecordTime;
            var value0 = datas[0].ProbeValue;
            var value1 = datas[1].ProbeValue;

            // 判断是否是最新数据
            if (time == _lastTimeT2) return;
            _lastTimeT2 = time;

            double value = value1 - value0;
            TeeChartUtil.addSingleData2TeeChart(tChartT2, DataCountPerFrame, time, value);
        }

        // 更新M_N数据
        // 同一工序下，瓦斯浓度变化值N
        private void UpdateMnData()
        {
            var datas = GasConcentrationProbeData.FindNewRealData(_currentProbeId, 2);
            DateTime time = datas[0].RecordTime;
            double value = datas[0].ProbeValue;
            double value1 = datas[1].ProbeValue;

            double valueN = value - value1;

            // 判断是否是最新数据
            if (time != _lastTimeMn && _lastTimeMn != DateTime.MinValue)
            {
                _lastTimeMn = time;

                // 往DGV中填充数据
                _dgvData.Rows.Add(value + "%", time);
                if (_dgvData.Rows.Count > 0)
                {
                    // 定位滚动条
                    _dgvData.FirstDisplayedScrollingRowIndex = _dgvData.Rows.Count - 1;

                    // 瓦斯浓度超过安全范围 
                    if (value >= YellowDataThreshold && value <= RedDataThreshold)
                    {
                        _dgvData.Rows[_dgvData.Rows.Count - 1].DefaultCellStyle.BackColor = Color.Yellow;
                    }
                    else if (value > RedDataThreshold)
                    {
                        _dgvData.Rows[_dgvData.Rows.Count - 1].DefaultCellStyle.BackColor = Color.Red;
                    }
                }

                TeeChartUtil.addSingleData2TeeChart(tChartM, DataCountPerFrame, time, value);
                TeeChartUtil.addSingleData2TeeChart(tChartN, DataCountPerFrame, time, valueN);
            }
            else
            {
                _lastTimeMn = time;
            }
        }

        private void AddDataSet2TeeChart(TChart tChart, GasConcentrationProbeData[] datas, string type)
        {
            bool bReturn = false;

            if (datas.Length > 0)
            {
                int sqlCnt = datas.Length;
                // 禁止自动生成列(※位置不可变)
                _dgvData.AutoGenerateColumns = false;
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
                DateTime startTime = datas[0].RecordTime;
                DateTime endTime = datas[sqlCnt - 1].RecordTime;

                var ts1 = new TimeSpan(startTime.Ticks);
                var ts2 = new TimeSpan(endTime.Ticks);
                var ts = ts1.Subtract(ts2).Duration();

                // 如果时间跨度大于2个小时，则需要调整时间轴
                if (ts.Hours > 2)
                {
                    var tmpTime = endTime.AddSeconds(-7200); // 7200seconds = 2 hours.
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

                AddDataToTeeChart(tChart, datas, type);

                // 重绘
                tChart.AutoRepaint = true;
                Invoke(new MethodInvoker(tChart.Refresh));

                bReturn = true;
            }

            if (bReturn == false && type == "T2")
            {
                tChart.Header.Text = "该巷道没有T2传感器，或者T2没有数据。";
            }
        }

        private void AddDataToTeeChart(TChart tChart, GasConcentrationProbeData[] datas, string type)
        {
            if (type == "M")
            {
                AddDataToTeeChartM(tChart, datas);
            }
            else if (type == "N")
            {
                addDataToTeeChartN(tChart, datas);
            }
            else if (type == "T2")
            {
                addDataToTeeChartT2(tChart, datas);
            }
        }

        /// <summary>
        /// 将DataSet中的数据添加到TeeChart中。
        /// 主要用于添加涌出量原始数据M
        /// </summary>
        /// <param name="tChart">TeeChart图表</param>
        /// <param name="ds">数据集</param>
        private void AddDataToTeeChartM(TChart tChart, GasConcentrationProbeData[] datas)
        {
            int sqlCnt = datas.Length;

            double maxVertValue = 0;
            double minVertValue = 0;

            for (int i = sqlCnt - 1; i >= 0; i--)
            {

                double value = datas[i].ProbeValue;
                DateTime time = datas[i].RecordTime;

                if (value > maxVertValue)
                {
                    maxVertValue = value;
                }

                if (value < minVertValue)
                {
                    minVertValue = value;
                }
                Invoke(new MethodInvoker(delegate
                {
                    tChart.Series[0].Add(time, value);


                    // 往DGV中填充数据
                    _dgvData.Rows.Add(value + "%", time);

                    // 瓦斯浓度超过安全范围 
                    if (value >= YellowDataThreshold && value <= RedDataThreshold)
                    {
                        _dgvData.Rows[_dgvData.Rows.Count - 1].DefaultCellStyle.BackColor = Color.Yellow;
                    }
                    else if (value > RedDataThreshold)
                    {
                        _dgvData.Rows[_dgvData.Rows.Count - 1].DefaultCellStyle.BackColor = Color.Red;
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
        private void addDataToTeeChartN(TChart tChart, GasConcentrationProbeData[] datas)
        {
            int sqlCnt = datas.Length;

            double maxVertValue = 0;
            double minVertValue = 0;

            for (int i = 0; i < sqlCnt; i++)
            {
                if ((i + 1) != datas.Length)
                {
                    double value = datas[i + 1].ProbeValue - datas[i].ProbeValue;
                    DateTime time = datas[i + 1].RecordTime;

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
        /// 开始历史数据查询
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void _rbtnHistory_Click(object sender, EventArgs e)
        {
            // 选择巷道设为可用
            selectTunnelSimple1.Enabled = true;
            // 探头类型设为可用
            _lstProbeStyle.Enabled = true;
            // 探头名称设为可用
            _lstProbeName.Enabled = true;

            // 停止实时数据监控
            StopRealTimeCurveMonitoring();

            // 清空datagridview
            _dgvData.Rows.Clear();
            // 清空曲线1的fastline
            tChartM.Series[0].Clear();
            // 清空曲线2的fastline
            tChartT2.Series[0].Clear();
            // 清空曲线3的fastline
            tChartN.Series[0].Clear();

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
            StopRealTimeCurveMonitoring();

            // 检查是否选择了巷道和传感器
            if (!Check())
                return;
            // 清空datagridview
            _dgvData.Rows.Clear();

            var t2 = new Thread(LoadAllHistoryData);
            t2.Start();


            //t.Abort();
        }

        private void LoadAllHistoryData()
        {
            String probeName = "";
            DateTime dateTimeStart = new DateTime();
            DateTime datetimeEnd = new DateTime();
            _lstProbeName.MouseUp -= _lstProbeName_SelectedIndexChanged;

            Invoke(new MethodInvoker(delegate
            {
                _lblLoading.Visible = true;
                _btnQuery.Text = @"查询中...";
                _btnQuery.Enabled = false;
                probeName = _lstProbeName.SelectedValue.ToString();
                dateTimeStart = Convert.ToDateTime(_dateTimeStart.Text);
                datetimeEnd = Convert.ToDateTime(_dateTimeEnd.Text);
                _rbtnRealtime.Enabled = false;
                _rbtnHistory.Enabled = false;
            }));
            GasConcentrationProbeData[] datas = GasConcentrationProbeData.FindHistaryData(probeName, dateTimeStart, datetimeEnd);

            // load监控系统原始数据M历史数据
            LoadHistoryDataM(tChartM, datas);
            // load同一工序条件下瓦斯浓度变化值N历史数据
            LoadHistoryDataN(tChartN, datas);
            // loadT2瓦斯浓度平均增加值Q历史数据
            LoadHistoryDataT2(tChartT2);

            Invoke(new MethodInvoker(delegate
            {
                _lblLoading.Visible = false;
                _btnQuery.Text = @"查询";
                _btnQuery.Enabled = true;
                _rbtnRealtime.Enabled = true;
                _rbtnHistory.Enabled = true;
            }));
            _lstProbeName.MouseUp += _lstProbeName_SelectedIndexChanged;
        }



        /// <summary>
        /// load瓦斯浓度历史数据--监控系统原始数据M
        /// </summary>
        private void LoadHistoryDataM(TChart tChart, GasConcentrationProbeData[] datas)
        {
            int sqlCnt = datas.Length;

            if (sqlCnt > 0)
            {
                tChart.Header.Text = "监控系统原始数据M";
                AddDataSet2TeeChart(tChartM, datas, "M");
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
        private void LoadHistoryDataT2(TChart tChart)
        {
            GasConcentrationProbeData[] datas = GasConcentrationProbeData.FindHistaryData(
                _t2Id,
                Convert.ToDateTime(_dateTimeStart.Text),
                Convert.ToDateTime(_dateTimeEnd.Text)
                );
            int sqlCnt = 0;
            if (datas.Length > 0)
            {
                sqlCnt = datas.Length;
            }
            if (sqlCnt > 0)
            {
                tChart.Header.Text = "T2瓦斯浓度平均增加值Q";
                AddDataSet2TeeChart(tChart, datas, "T2");
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
        private void LoadHistoryDataN(TChart tChart, GasConcentrationProbeData[] datas)
        {
            int sqlCnt = datas.Length;

            if (sqlCnt > 0)
            {
                tChart.Header.Text = "同一工序条件下瓦斯浓度变化值N";
                AddDataSet2TeeChart(tChart, datas, "N");
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
            tChartM.Series[0].Marks.Visible = _ckbSetMarks1.Checked;
        }

        #region T2瓦斯浓度平均增加值Q


        /// <summary>
        /// 设置曲线2的Marks显示与否
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void _ckbSetMarks2_Click(object sender, EventArgs e)
        {
            tChartT2.Series[0].Marks.Visible = _ckbSetMarks2.Checked;
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
            tChartN.Series[0].Marks.Visible = _ckbSetMarks3.Checked;
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
        //    gasConcentrationProbeDataEntity.Probe = "001";
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
            _dgvData.Rows.Clear();
            // 清空曲线1的fastline
            tChartM.Series[0].Clear();
            // 清空曲线2的fastline
            tChartT2.Series[0].Clear();
            // 清空曲线3的fastline
            tChartN.Series[0].Clear();

            timer1.Enabled = false;
            // 历史数据分析设为选中
            _rbtnHistory.Checked = true;

            if (Check())
            {
                DateTime dtStart = Convert.ToDateTime(_dateTimeStart.Text).AddDays(1);
                DateTime dtEnd = Convert.ToDateTime(_dateTimeEnd.Text).AddDays(1);

                _dateTimeStart.Text = dtStart.ToString(CultureInfo.InvariantCulture);
                _dateTimeEnd.Text = dtEnd.ToString(CultureInfo.InvariantCulture);

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
            _dgvData.Rows.Clear();
            // 清空曲线1的fastline
            tChartM.Series[0].Clear();
            // 清空曲线2的fastline
            tChartT2.Series[0].Clear();
            // 清空曲线3的fastline
            tChartN.Series[0].Clear();

            timer1.Enabled = false;
            // 历史数据分析设为选中
            _rbtnHistory.Checked = true;

            if (Check())
            {
                DateTime dtStart = Convert.ToDateTime(_dateTimeStart.Text).AddDays(-1);
                DateTime dtEnd = Convert.ToDateTime(_dateTimeEnd.Text).AddDays(-1);

                _dateTimeStart.Text = dtStart.ToString(CultureInfo.InvariantCulture);
                _dateTimeEnd.Text = dtEnd.ToString(CultureInfo.InvariantCulture);

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
            _dgvData.Rows.Clear();
            // 清空曲线1的fastline
            tChartM.Series[0].Clear();
            // 清空曲线2的fastline
            tChartT2.Series[0].Clear();
            // 清空曲线3的fastline
            tChartN.Series[0].Clear();

            timer1.Enabled = false;
            // 历史数据分析设为选中
            _rbtnHistory.Checked = true;

            if (Check())
            {
                _dateTimeStart.Text = DateTime.Now.ToString("yyyy-MM-dd") + @" 00:00:00";
                _dateTimeEnd.Text = DateTime.Now.ToString("yyyy-MM-dd") + @" 23:59:59";

                _btnQuery_Click(sender, e);
            }
        }

        /// <summary>
        /// 检验
        /// </summary>
        private bool Check()
        {
            // 没有选择巷道 
            if (_currentTunnelId <= Const.INVALID_ID)
            {
                Alert.alert(Const_GE.TUNNEL_NAME_MUST_INPUT);
                return false;
            }

            // 没有选择探头
            if (_lstProbeName.SelectedItems.Count == 0)
            {
                Alert.alert(Const_GE.PROBE_MUST_CHOOSE);
                return false;
            }

            return true;
        }

        /// 根据探头编号和开始结束时间，获取特定探头和特定时间段内的【瓦斯浓度探头数据】
        /// 
        /// <param name="strProbeId">探头编号</param>
        /// <param name="dtStartTime">开始时间</param>
        /// <returns>特定探头和特定时间段内的【瓦斯浓度探头数据】</returns>
        //public static DataSet SelectAllGasDataByProbeIdAndStartTime(string strProbeId, DateTime dtStartTime)
        //{
        //    var sqlStr = new StringBuilder();
        //    sqlStr.Append("SELECT * FROM " + GasConcentrationProbeDataDbConstNames.TABLE_NAME);
        //    sqlStr.Append(" WHERE ");
        //    sqlStr.Append(GasConcentrationProbeDataDbConstNames.PROBE_ID + " ='" + strProbeId + "'");
        //    sqlStr.Append(" AND ");
        //    sqlStr.Append(GasConcentrationProbeDataDbConstNames.RECORD_TIME + " >= '" + dtStartTime + "'");

        //    var db = new ManageDataBase(DATABASE_TYPE.GasEmissionDB);
        //    var ds = db.ReturnDS(sqlStr.ToString());
        //    return ds;
        //}

        // Database Operation
        /// <summary>
        /// 获取指定探头的最新实时数据
        /// </summary>
        /// <returns></returns>
        public static string GetT2Id(int tunnelId)
        {
            var sqlStr = new StringBuilder();
            sqlStr.Append("SELECT [PROBE_ID] FROM " + ProbeManageDbConstNames.TABLE_NAME);
            sqlStr.Append(" WHERE ");
            sqlStr.Append("TUNNEL_ID = " + tunnelId);
            sqlStr.Append(" AND [PROBE_NAME]='T2'");

            var db = new ManageDataBase(DATABASE_TYPE.GasEmissionDB);
            var ds = db.ReturnDS(sqlStr.ToString());
            return ds.Tables[0].Rows.Count <= 0 ? string.Empty : ds.Tables[0].Rows[0][0].ToString();
        }

        /// <summary>
        /// 获取指定探头的最新2行实时数据
        /// </summary>
        /// <param name="iProbeId"></param>
        /// <returns></returns>
        //public static DataSet GetLatest2RowsData(string iProbeId)
        //{
        //    var sqlStr = new StringBuilder();
        //    sqlStr.Append("SELECT TOP 2 * FROM " + GasConcentrationProbeDataDbConstNames.TABLE_NAME);
        //    sqlStr.Append(" WHERE " + GasConcentrationProbeDataDbConstNames.PROBE_ID + " = " + iProbeId);
        //    sqlStr.Append(" ORDER BY " + GasConcentrationProbeDataDbConstNames.PROBE_DATA_ID + " DESC");

        //    var db = new ManageDataBase(DATABASE_TYPE.GasEmissionDB);
        //    var ds = db.ReturnDS(sqlStr.ToString());
        //    return ds;
        //}

        /// <summary>
        /// 获取瓦斯浓度数据
        /// @param probeId - 探头ID
        /// @param startTime - 开始时间
        /// @param endTime - 结束时间
        /// </summary>
        /// <returns></returns>
        //private DataSet getHistoryData(string probeId, string startTime, string endTime)
        //{
        //    var ds = new DataSet();
        //    if (String.IsNullOrEmpty(probeId)) return ds;
        //    var sqlStr = new StringBuilder();
        //    sqlStr.Append("SELECT ");
        //    sqlStr.Append("* ");
        //    sqlStr.Append("FROM ");
        //    sqlStr.Append(GasConcentrationProbeDataDbConstNames.TABLE_NAME + " ");
        //    sqlStr.Append("WHERE ");
        //    sqlStr.Append(GasConcentrationProbeDataDbConstNames.PROBE_ID + " = " + probeId + " ");
        //    sqlStr.Append("AND ");
        //    sqlStr.Append(GasConcentrationProbeDataDbConstNames.RECORD_TIME + " >= '" + startTime + "' ");
        //    sqlStr.Append("AND ");
        //    sqlStr.Append(GasConcentrationProbeDataDbConstNames.RECORD_TIME + " <= '" + endTime + "' ");
        //    sqlStr.Append("ORDER BY RECORD_TIME ");

        //    var db = new ManageDataBase(DATABASE_TYPE.GasEmissionDB);
        //    ds = db.ReturnDS(sqlStr.ToString());
        //    return ds;
        //}

        /// <summary>
        /// 获取指定探头过去一段时间的若干数据
        /// </summary>
        /// <param name="probeId">探头Id</param>
        /// <returns></returns>
        //private DataSet GetOldDataByProbeId(string probeId)
        //{
        //    var sqlStr = new StringBuilder();
        //    sqlStr.Append("SELECT ");
        //    sqlStr.Append("TOP " + (DataCountPerFrame - 1) + "* ");
        //    sqlStr.Append("FROM ");
        //    sqlStr.Append(GasConcentrationProbeDataDbConstNames.TABLE_NAME + " ");
        //    sqlStr.Append("WHERE ");
        //    sqlStr.Append(GasConcentrationProbeDataDbConstNames.PROBE_ID + " = " + probeId + " ");
        //    sqlStr.Append("AND ");
        //    //sqlStr.Append(GasConcentrationProbeDataDbConstNames.RECORD_TIME + " >= '" + this._dateTimeStart.Text + "' ");
        //    //sqlStr.Append("AND ");
        //    sqlStr.Append(GasConcentrationProbeDataDbConstNames.RECORD_TIME + " <= '" + DateTime.Now.ToString(CultureInfo.InvariantCulture) + "' ");
        //    sqlStr.Append("ORDER BY RECORD_TIME DESC");

        //    var db = new ManageDataBase(DATABASE_TYPE.GasEmissionDB);
        //    var ds = db.ReturnDS(sqlStr.ToString());

        //    return ds;
        //}

        // 监控系统参数设置
        private void bbiMonitorSetting_ItemClick(object sender, ItemClickEventArgs e)
        {
            var ms = new MoniroSettings(this);
            ms.ShowDialog();
        }

        // 退出
        private void bbiExit_ItemClick(object sender, ItemClickEventArgs e)
        {
            Application.Exit();
        }

        // 坏数据剔除
        private void bbiBadDataEdit_ItemClick(object sender, ItemClickEventArgs e)
        {
            var bdd = new BadDataDelete(this);
            bdd.ShowDialog();
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
            _rbtnRealtime.Checked = true;

            _currentProbeId = _lstProbeName.SelectedValue == null ? "-1" : _lstProbeName.SelectedValue.ToString();


            _dateTimeStart.Enabled = false;
            _dateTimeEnd.Enabled = false;
            _btnBeforeDay.Enabled = false;
            _btnNow.Enabled = false;
            _btnAfterDay.Enabled = false;
            _btnQuery.Enabled = false;


            // 开始实时数据监控
            StartRealTimeCurveMonitoring();
        }

        private void _lstProbeStyle_SelectedIndexChanged(object sender, EventArgs e)
        {
            _lstProbeName.DataSource = null;

            // 没有选择巷道
            if (_currentTunnelId <= Const.INVALID_ID)
            {
                Alert.alert(Const_GE.TUNNEL_NAME_MUST_INPUT);
            }
            else
            {
                // 根据巷道编号和探头类型编号获取探头信息
                DataSet ds = ProbeManageBLL.selectProbeManageInfoByTunnelIDAndProbeType(
                    _currentTunnelId,
                    Convert.ToInt32(_lstProbeStyle.SelectedValue));

                if (ds.Tables[0].Rows.Count > 0)
                {
                    _lstProbeName.DataSource = ds.Tables[0];
                    _lstProbeName.DisplayMember = ProbeManageDbConstNames.PROBE_NAME;
                    _lstProbeName.ValueMember = ProbeManageDbConstNames.PROBE_ID;

                    _lstProbeName.SelectedIndex = -1;
                }
            }
        }

        private void _rbtnRealtime_Click_1(object sender, EventArgs e)
        {

            // 停止实时数据监控

            // 清空datagridview
            _dgvData.Rows.Clear();
            // 清空曲线1的fastline
            tChartM.Series[0].Clear();
            // 清空曲线2的fastline
            tChartT2.Series[0].Clear();
            // 清空曲线3的fastline
            tChartN.Series[0].Clear();

            _dateTimeStart.Enabled = false;
            _dateTimeEnd.Enabled = false;
            _btnBeforeDay.Enabled = false;
            _btnNow.Enabled = false;
            _btnAfterDay.Enabled = false;
            _btnQuery.Enabled = false;

            StartRealTimeCurveMonitoring();
        }

        /// <summary>
        /// 添加T2瓦斯浓度平均增加值
        /// </summary>
        /// <param name="tChart"></param>
        /// <param name="ds"></param>
        private void addDataToTeeChartT2(TChart tChart, GasConcentrationProbeData[] datas)
        {
            int sqlCnt = datas.Length;
            double value = 0;
            DateTime time = new DateTime();
            double sumValue = 0;

            double maxVertValue = 0;
            double minVertValue = 0;

            for (int i = 0; i < sqlCnt; i++)
            {
                if ((i + 1) != datas.Length)
                {
                    sumValue = sumValue + datas[i + 1].ProbeValue - datas[i].ProbeValue;
                    value = sumValue / (i + 1);
                    time = datas[i + 1].RecordTime;

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
    }
}
