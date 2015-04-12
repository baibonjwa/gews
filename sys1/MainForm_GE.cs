using System;
using System.Diagnostics;
using System.Drawing;
using System.Globalization;
using System.Threading;
using System.Windows.Forms;
using AutoUpdaterDotNET;
using DevExpress.XtraBars;
using LibAbout;
using LibBusiness;
using LibCommon;
using LibCommonForm;
using LibDatabase;
using LibEntity;
using LibPanels;
using LibSocket;
using Steema.TeeChart;
using TeeChartWrapper;

namespace sys1
{
    public partial class MainFormGe
    {
        private string _currentProbeId = string.Empty;
        private DateTime _lastTimeMn; // M/N 的数据更新时间
        private DateTime _lastTimeT2; // T2的数据更新时间
        private int _updateFrequency; // 10s
        private readonly string _t2Id = string.Empty;

        public MainFormGe(BarButtonItem mniAbout)
        {
            EnableDeleteAndModifyBtn = true;
            this.mniAbout = mniAbout;
            InitializeComponent();
            SocketUtil.DoInitilization();

            var fp = new FileProperties(configFileName);

            int iValue;
            double dValue;
            int.TryParse(fp.Get("countperframe"), out iValue);
            DataCountPerFrame = iValue;
            int.TryParse(fp.Get("updatefrequency"), out iValue);
            _updateFrequency = iValue;
            double.TryParse(fp.Get("redthreshold"), out dValue);
            RedDataThreshold = dValue;
            double.TryParse(fp.Get("yellowthreshold"), out dValue);
            YellowDataThreshold = dValue;
            double.TryParse(fp.Get("baddatathreshold"), out dValue);
            BadDataThreshold = dValue;

            dateTimeStart.Text = DateTime.Now.ToString("yyyy-MM-dd") + @" 00:00:00";
            dateTimeEnd.Text = DateTime.Now.ToString("yyyy-MM-dd") + @" 23:59:59";
            DataBindUtil.LoadProbeType(lstProbeType);
        }

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
                timer1.Interval = value*1000;
            }
        }

        // 曲线每一帧显示的数据个数
        public int DataCountPerFrame { get; set; }
        public double RedDataThreshold { get; set; }
        public double YellowDataThreshold { get; set; }
        public double BadDataThreshold { get; set; }
        public bool EnableDeleteAndModifyBtn { get; set; }

        private void MainForm_GE_Load(object sender, EventArgs e)
        {
            AutoUpdater.Start("http://bltmld.vicp.cc:8090/sys1/update.xml");
            DXSeting.floatToolsLoadSet();
        }

        private void MainForm_GE_FormClosed(object sender, FormClosedEventArgs e)
        {
            Application.Exit();
        }

        //传感器管理
        private void mniSensorManagement_ItemClick(object sender, ItemClickEventArgs e)
        {
            var probeInfoManagementForm = new ProbeInfoManagement();
            probeInfoManagementForm.Show();
        }

        /// <summary>
        ///     数据库设置
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
            var gasConcentrationProbeDataManamementForm = new GasConcentrationProbeDataManamement();
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

        /// <summary>
        ///     开始实时监控
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
            dgvData.Rows.Clear();

            // 获取指定探头的旧数据 ----------用来填充曲线。
            var dsData = GasConcentrationProbeData.FindHistaryData(_currentProbeId);
            AddDataSet2TeeChart(tChartM, dsData, "M");
            AddDataSet2TeeChart(tChartN, dsData, "N");
            if (!String.IsNullOrEmpty(_t2Id))
            {
                var ds = GasConcentrationProbeData.FindHistaryData(_t2Id);
                AddDataSet2TeeChart(tChartT2, ds, "T2");
            }

            if (dgvData.Rows.Count > 0)
            {
                // 定位滚动条
                dgvData.FirstDisplayedScrollingRowIndex = dgvData.Rows.Count - 1;
            }
            // 获取旧数据 ---------- End

            timer1.Enabled = true; // 启动定时器
        }

        /// <summary>
        ///     开始实时监控
        /// </summary>
        private void StopRealTimeCurveMonitoring()
        {
            timer1.Enabled = false;
        }

        /// <summary>
        ///     计时器
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void timer1_Tick(object sender, EventArgs e)
        {
            UpdateMnData(); // Update raw data curve.
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

            var value = value1 - value0;
            TeeChartUtil.addSingleData2TeeChart(tChartT2, DataCountPerFrame, time, value);
        }

        // 更新M_N数据
        // 同一工序下，瓦斯浓度变化值N
        private void UpdateMnData()
        {
            var datas = GasConcentrationProbeData.FindNewRealData(_currentProbeId, 2);
            var time = datas[0].RecordTime;
            var value = datas[0].ProbeValue;
            var value1 = datas[1].ProbeValue;

            var valueN = value - value1;

            // 判断是否是最新数据
            if (time != _lastTimeMn && _lastTimeMn != DateTime.MinValue)
            {
                _lastTimeMn = time;

                // 往DGV中填充数据
                dgvData.Rows.Add(value + "%", time);
                if (dgvData.Rows.Count > 0)
                {
                    // 定位滚动条
                    dgvData.FirstDisplayedScrollingRowIndex = dgvData.Rows.Count - 1;

                    // 瓦斯浓度超过安全范围 
                    if (value >= YellowDataThreshold && value <= RedDataThreshold)
                    {
                        dgvData.Rows[dgvData.Rows.Count - 1].DefaultCellStyle.BackColor = Color.Yellow;
                    }
                    else if (value > RedDataThreshold)
                    {
                        dgvData.Rows[dgvData.Rows.Count - 1].DefaultCellStyle.BackColor = Color.Red;
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
            var bReturn = false;

            if (datas.Length > 0)
            {
                var sqlCnt = datas.Length;
                // 禁止自动生成列(※位置不可变)
                dgvData.AutoGenerateColumns = false;
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
                var startTime = datas[0].RecordTime;
                var endTime = datas[sqlCnt - 1].RecordTime;

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
        ///     将DataSet中的数据添加到TeeChart中。
        ///     主要用于添加涌出量原始数据M
        /// </summary>
        /// <param name="tChart">TeeChart图表</param>
        /// <param name="datas"></param>
        private void AddDataToTeeChartM(TChart tChart, GasConcentrationProbeData[] datas)
        {
            var sqlCnt = datas.Length;

            double maxVertValue = 0;
            double minVertValue = 0;

            for (var i = sqlCnt - 1; i >= 0; i--)
            {
                var value = datas[i].ProbeValue;
                var time = datas[i].RecordTime;

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
                    dgvData.Rows.Add(value + "%", time);

                    // 瓦斯浓度超过安全范围 
                    if (value >= YellowDataThreshold && value <= RedDataThreshold)
                    {
                        dgvData.Rows[dgvData.Rows.Count - 1].DefaultCellStyle.BackColor = Color.Yellow;
                    }
                    else if (value > RedDataThreshold)
                    {
                        dgvData.Rows[dgvData.Rows.Count - 1].DefaultCellStyle.BackColor = Color.Red;
                    }
                }));
            }

            if (maxVertValue < 1)
            {
                tChart.Series[0].GetVertAxis.SetMinMax(0, 1);
            }
            else
            {
                tChart.Series[0].GetVertAxis.SetMinMax(minVertValue - minVertValue*0.1, maxVertValue + maxVertValue*0.1);
            }
        }

        /// <summary>
        ///     添加同一工序条件下瓦斯浓度变化值N
        /// </summary>
        /// <param name="tChart"></param>
        /// <param name="datas"></param>
        private void addDataToTeeChartN(TChart tChart, GasConcentrationProbeData[] datas)
        {
            var sqlCnt = datas.Length;

            double maxVertValue = 0;
            double minVertValue = 0;

            for (var i = 0; i < sqlCnt; i++)
            {
                if ((i + 1) != datas.Length)
                {
                    var value = datas[i + 1].ProbeValue - datas[i].ProbeValue;
                    var time = datas[i + 1].RecordTime;

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
        ///     执行历史数据查询
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
            dgvData.Rows.Clear();

            var t2 = new Thread(LoadAllHistoryData);
            t2.Start();


            //t.Abort();
        }

        private void LoadAllHistoryData()
        {
            var probeName = "";
            var startTime = new DateTime();
            var endTime = new DateTime();

            Invoke(new MethodInvoker(delegate
            {
                lblLoading.Visible = true;
                btnQuery.Text = @"查询中...";
                btnQuery.Enabled = false;
                probeName = lstProbeName.SelectedValue.ToString();
                startTime = Convert.ToDateTime(dateTimeStart.Text);
                endTime = Convert.ToDateTime(dateTimeEnd.Text);
                rbtnRealtime.Enabled = false;
                rbtnHistory.Enabled = false;
            }));
            var datas = GasConcentrationProbeData.FindHistaryData(probeName, startTime, endTime);

            // load监控系统原始数据M历史数据
            LoadHistoryDataM(tChartM, datas);
            // load同一工序条件下瓦斯浓度变化值N历史数据
            LoadHistoryDataN(tChartN, datas);
            // loadT2瓦斯浓度平均增加值Q历史数据
            LoadHistoryDataT2(tChartT2);

            Invoke(new MethodInvoker(delegate
            {
                lblLoading.Visible = false;
                btnQuery.Text = @"查询";
                btnQuery.Enabled = true;
                rbtnRealtime.Enabled = true;
                rbtnHistory.Enabled = true;
            }));
        }

        /// <summary>
        ///     load瓦斯浓度历史数据--监控系统原始数据M
        /// </summary>
        private void LoadHistoryDataM(TChart tChart, GasConcentrationProbeData[] datas)
        {
            var sqlCnt = datas.Length;

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
        ///     loadT2瓦斯浓度平均增加值Q历史数据
        /// </summary>
        private void LoadHistoryDataT2(TChart tChart)
        {
            var datas = GasConcentrationProbeData.FindHistaryData(
                _t2Id,
                Convert.ToDateTime(dateTimeStart.Text),
                Convert.ToDateTime(dateTimeEnd.Text)
                );
            var sqlCnt = 0;
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
        ///     同一工序条件下瓦斯浓度变化值N
        /// </summary>
        private void LoadHistoryDataN(TChart tChart, GasConcentrationProbeData[] datas)
        {
            var sqlCnt = datas.Length;

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
        ///     设置曲线1的Marks显示与否
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void _ckbSetMarks1_Click(object sender, EventArgs e)
        {
            tChartM.Series[0].Marks.Visible = _ckbSetMarks1.Checked;
        }

        #region T2瓦斯浓度平均增加值Q

        /// <summary>
        ///     设置曲线2的Marks显示与否
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
        ///     设置曲线3的Marks显示与否
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
        ///     后一天
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void _btnAfterDay_Click(object sender, EventArgs e)
        {
            // 清空datagridview
            dgvData.Rows.Clear();
            // 清空曲线1的fastline
            tChartM.Series[0].Clear();
            // 清空曲线2的fastline
            tChartT2.Series[0].Clear();
            // 清空曲线3的fastline
            tChartN.Series[0].Clear();

            timer1.Enabled = false;
            // 历史数据分析设为选中
            rbtnHistory.Checked = true;

            if (Check())
            {
                var dtStart = Convert.ToDateTime(dateTimeStart.Text).AddDays(1);
                var dtEnd = Convert.ToDateTime(dateTimeEnd.Text).AddDays(1);

                dateTimeStart.Text = dtStart.ToString(CultureInfo.InvariantCulture);
                dateTimeEnd.Text = dtEnd.ToString(CultureInfo.InvariantCulture);

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
        ///     前一天
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void _btnBeforeDay_Click(object sender, EventArgs e)
        {
            // 清空datagridview
            dgvData.Rows.Clear();
            // 清空曲线1的fastline
            tChartM.Series[0].Clear();
            // 清空曲线2的fastline
            tChartT2.Series[0].Clear();
            // 清空曲线3的fastline
            tChartN.Series[0].Clear();

            timer1.Enabled = false;
            // 历史数据分析设为选中
            rbtnHistory.Checked = true;

            if (Check())
            {
                var dtStart = Convert.ToDateTime(dateTimeStart.Text).AddDays(-1);
                var dtEnd = Convert.ToDateTime(dateTimeEnd.Text).AddDays(-1);

                dateTimeStart.Text = dtStart.ToString(CultureInfo.InvariantCulture);
                dateTimeEnd.Text = dtEnd.ToString(CultureInfo.InvariantCulture);

                _btnQuery_Click(sender, e);
            }
        }

        /// <summary>
        ///     当前时间
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void _btnNow_Click(object sender, EventArgs e)
        {
            // 清空datagridview
            dgvData.Rows.Clear();
            // 清空曲线1的fastline
            tChartM.Series[0].Clear();
            // 清空曲线2的fastline
            tChartT2.Series[0].Clear();
            // 清空曲线3的fastline
            tChartN.Series[0].Clear();

            timer1.Enabled = false;
            // 历史数据分析设为选中
            rbtnHistory.Checked = true;

            if (Check())
            {
                dateTimeStart.Text = DateTime.Now.ToString("yyyy-MM-dd") + @" 00:00:00";
                dateTimeEnd.Text = DateTime.Now.ToString("yyyy-MM-dd") + @" 23:59:59";

                _btnQuery_Click(sender, e);
            }
        }

        /// <summary>
        ///     检验
        /// </summary>
        private bool Check()
        {
            // 没有选择巷道 
            if (selectTunnelSimple1.SelectedTunnel == null)
            {
                Alert.alert(Const_GE.TUNNEL_NAME_MUST_INPUT);
                return false;
            }

            // 没有选择探头
            if (lstProbeName.SelectedItems.Count == 0)
            {
                Alert.alert(Const_GE.PROBE_MUST_CHOOSE);
                return false;
            }

            return true;
        }

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
            var bdd = new BadDataDelete();
            bdd.ShowDialog();
        }

        /// <summary>
        ///     添加T2瓦斯浓度平均增加值
        /// </summary>
        /// <param name="tChart"></param>
        /// <param name="datas"></param>
        private void addDataToTeeChartT2(TChart tChart, GasConcentrationProbeData[] datas)
        {
            var sqlCnt = datas.Length;
            double sumValue = 0;

            double maxVertValue = 0;
            double minVertValue = 0;

            for (var i = 0; i < sqlCnt; i++)
            {
                if ((i + 1) != datas.Length)
                {
                    sumValue = sumValue + datas[i + 1].ProbeValue - datas[i].ProbeValue;
                    var value = sumValue/(i + 1);
                    var time = datas[i + 1].RecordTime;

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

        private void barButtonItem1_ItemClick(object sender, ItemClickEventArgs e)
        {
            AutoUpdater.CheckAtOnce = true;
            AutoUpdater.Start("http://bltmld.vicp.cc:8090/sys1/update.xml");
        }

        private void rbtnRealtime_CheckedChanged(object sender, EventArgs e)
        {
            if (!rbtnRealtime.Checked) return;
            dateTimeStart.Enabled = false;
            dateTimeEnd.Enabled = false;
            btnBeforeDay.Enabled = false;
            btnNow.Enabled = false;
            btnAfterDay.Enabled = false;
            btnQuery.Enabled = false;
            StartRealTimeCurveMonitoring();
        }

        private void rbtnHistory_CheckedChanged(object sender, EventArgs e)
        {
            if (!rbtnHistory.Checked) return;
            selectTunnelSimple1.Enabled = true;
            lstProbeType.Enabled = true;
            lstProbeName.Enabled = true;
            StopRealTimeCurveMonitoring();
            dgvData.Rows.Clear();
            tChartM.Series[0].Clear();
            tChartT2.Series[0].Clear();
            tChartN.Series[0].Clear();

            dateTimeStart.Enabled = true;
            dateTimeEnd.Enabled = true;
            btnBeforeDay.Enabled = true;
            btnNow.Enabled = true;
            btnAfterDay.Enabled = true;
            btnQuery.Enabled = true;
        }

        private void lstProbeType_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (selectTunnelSimple1.SelectedTunnel == null) return;
            lstProbeName.DataSource =
                Probe.FindAllByTunnelIdAndProbeTypeId(selectTunnelSimple1.SelectedTunnel.TunnelId,
                    Convert.ToInt32(lstProbeType.SelectedValue));
            lstProbeName.DisplayMember = "ProbeName";
            lstProbeName.ValueMember = "ProbeId";
        }

        private void lstProbeName_SelectedIndexChanged(object sender, EventArgs e)
        {
            rbtnRealtime.Checked = true;

            _currentProbeId = lstProbeName.SelectedValue == null ? "-1" : lstProbeName.SelectedValue.ToString();


            dateTimeStart.Enabled = false;
            dateTimeEnd.Enabled = false;
            btnBeforeDay.Enabled = false;
            btnNow.Enabled = false;
            btnAfterDay.Enabled = false;
            btnQuery.Enabled = false;


            // 开始实时数据监控
            StartRealTimeCurveMonitoring();
        }
    }
}