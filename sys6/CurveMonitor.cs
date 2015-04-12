using System;
using System.Drawing;
using System.Windows.Forms;
using LibCommon;
using LibEntity;
using TeeChartWrapper;

namespace UnderTerminal
{
    public partial class CurveMonitor : Form
    {
        private static int t2DataCount = 1;
        private DateTime _LastTime;
        //private double _WarnValue = Const.WARN_VALUE;
        private DateTime _LastTimeMN; // M/N 的数据更新时间
        private DateTime _LastTimeT2; // T2的数据更新时间
        // 获取点击开始按钮时候的系统时间
        private DateTime _StartTime;
        private string currentProbeId = string.Empty;
        /** 显示最大数据数 **/
        private int DEFAULT_DATA_SHOW_COUNT = 20;
        private int MAX_SECONDS_INTERVAL = 6*60*60; // 6 个小时
        private Random rnd = new Random();
        private double t2DeltaSumValue;

        /// ** 探头数据更新频率(单位：秒) **/
        //private int _xInterval = 5;
        private int tunnelId = -1;

        private string tunnelName = string.Empty;
        private readonly string _T2Id = string.Empty;
        private readonly UnderMessageWindow mainWin;
        private readonly double redDataThreshold = 10;
        private readonly double yellowDataThreshold = 0.75;

        public CurveMonitor(int tunnelId, string tunnelName, UnderMessageWindow mainW)
        {
            InitializeComponent();
            this.tunnelId = tunnelId;
            mainWin = mainW;
            Text += "-" + tunnelName;

            // 清空datagridview
            _dgvData.Rows.Clear();
            // 清空曲线1的fastline
            tChartM.Series[0].Clear();
            // 清空曲线2的fastline
            tChartT2.Series[0].Clear();
            // 清空曲线3的fastline
            tChartN.Series[0].Clear();

            // 根据巷道编号获取探头信息
            Object[] probes = Probe.FindAllByTunnelId(this.tunnelId);

            cbxSensors.Items.AddRange(probes);

            cbxSensors.DisplayMember = "ProbeName";
            cbxSensors.ValueMember = "ProbeId";

            cbxSensors.SelectedIndex = -1;

            // 获取T2传感器的Id
            _T2Id = Probe.GetT2Id(tunnelId);
        }

        public int TunnelId
        {
            get { return tunnelId; }
            set { tunnelId = value; }
        }

        public string TunnelName
        {
            get { return tunnelName; }
            set { tunnelName = value; }
        }

        //public static string getT2Id(int tunnelId)
        //{
        //    StringBuilder sqlStr = new StringBuilder();
        //    sqlStr.Append("SELECT [PROBE_ID] FROM " + ProbeManageDbConstNames.TABLE_NAME);
        //    sqlStr.Append(" WHERE ");
        //    sqlStr.Append("TUNNEL_ID = " + tunnelId);
        //    sqlStr.Append(" AND [PROBE_NAME]='T2'");

        //    ManageDataBase db = new ManageDataBase(DATABASE_TYPE.GasEmissionDB);
        //    DataSet ds = db.ReturnDS(sqlStr.ToString());
        //    if (ds.Tables[0].Rows.Count <= 0)
        //        return string.Empty;
        //    else
        //    {
        //        return ds.Tables[0].Rows[0][0].ToString();
        //    }
        //}

        #region 实时监控

        /// <summary>
        ///     获取指定探头的最新实时数据
        /// </summary>
        /// <param name="iProbeId"></param>
        /// <returns></returns>
        /// <summary>
        ///     开始按钮Click事件
        ///     原始数据实时监控
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void _btnStart_Click(object sender, EventArgs e)
        {
            // 没有选择巷道 
            if (tunnelId == Const.INVALID_ID)
            {
                Alert.alert(Const_GE.TUNNEL_NAME_MUST_INPUT);
                return;
            }

            // 没有选择探头
            if (cbxSensors.SelectedIndex < 0)
            {
                Alert.alert(Const_GE.PROBE_MUST_CHOOSE);
                return;
            }

            if (_btnStart.Text == "开始")
            {
                //this.timer2.Enabled = true;

                // 设置TeeChart1(监控系统原始数据M)
                TeeChartUtil.resetTeeChart(tChartM);
                // 设置TeeChart2(T2瓦斯浓度平均增加值Q)
                TeeChartUtil.resetTeeChart(tChartT2);
                // 设置TeeChart3(同一工序条件下瓦斯浓度变化值N)
                TeeChartUtil.resetTeeChart(tChartN);

                // 获取点击开始按钮时候的系统时间
                _StartTime = DateTime.Now;

                _btnStart.Text = "停止";

                timer1.Interval = 15000;
                DEFAULT_DATA_SHOW_COUNT = 120;

                timer1.Enabled = true;
            }
            else
            {
                _btnStart.Text = "开始";
                timer1.Enabled = false;
            }

            // 清空datagridview
            _dgvData.Rows.Clear();
        }

        #endregion

        private void btnExit_Click(object sender, EventArgs e)
        {
            var result = MessageBox.Show(@"确认退出监控系统", "井下终端录入系统", MessageBoxButtons.OKCancel,
                MessageBoxIcon.Question);
            if (result == DialogResult.OK)
            {
                timer1.Enabled = false;
                mainWin.Cm = null;
                Close();
            }
        }

        /// <summary>
        ///     设置曲线的Marks显示与否
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void _ckbSetMarks_Click(object sender, EventArgs e)
        {
            if (_ckbSetMarks.Checked)
            {
                // Mark显示
                tChartM.Series[0].Marks.Visible = true;
                tChartT2.Series[0].Marks.Visible = true;
                tChartN.Series[0].Marks.Visible = true;
            }
            else
            {
                // Mark隐藏
                tChartM.Series[0].Marks.Visible = false;
                tChartT2.Series[0].Marks.Visible = false;
                tChartN.Series[0].Marks.Visible = false;
            }
        }

        private void btnSwitch_Click(object sender, EventArgs e)
        {
            Hide();
        }

        private void cbxSensors_SelectedIndexChanged(object sender, EventArgs e)
        {
            var cbx = sender as ComboBox;
            if (null != cbx.SelectedValue)
                currentProbeId = cbx.SelectedValue.ToString();
        }

        #region 监控系统原始数据M

        /// <summary>
        ///     计时器
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void timer1_Tick(object sender, EventArgs e)
        {
            updateMNData(); // Update raw data curve.
            updateT2Data(); // Update T2 curve.
        }

        /// <summary>
        ///     获取指定探头的最新2行实时数据
        /// </summary>
        /// <param name="iProbeId"></param>
        /// <returns></returns>
        //public static DataSet getLatest2RowsData(string iProbeId)
        //{
        //    StringBuilder sqlStr = new StringBuilder();
        //    sqlStr.Append("SELECT TOP 2 * FROM " + GasConcentrationProbeDataDbConstNames.TABLE_NAME);
        //    sqlStr.Append(" WHERE " + GasConcentrationProbeDataDbConstNames.PROBE_ID + " = " + iProbeId);
        //    sqlStr.Append(" ORDER BY " + GasConcentrationProbeDataDbConstNames.PROBE_DATA_ID + " DESC");

        //    ManageDataBase db = new ManageDataBase(DATABASE_TYPE.GasEmissionDB);
        //    DataSet ds = db.ReturnDS(sqlStr.ToString());
        //    return ds;
        //}
        private void updateT2Data()
        {
            if (_T2Id == string.Empty)
            {
                tChartT2.Header.Text = "该巷道没有设置T2传感器.";
                return;
            }

            var datas = GasConcentrationProbeData.FindNewRealData(_T2Id, 2);
            var time = datas[0].RecordTime;
            var value0 = datas[0].ProbeValue;
            var value1 = datas[1].ProbeValue;
            // 判断是否是最新数据
            if (time != _LastTimeT2)
            {
                _LastTimeT2 = time;

                var value = value1 - value0;
                t2DeltaSumValue += value;
                value = t2DeltaSumValue/++t2DataCount;
                TeeChartUtil.addSingleData2TeeChart(tChartT2, DEFAULT_DATA_SHOW_COUNT, time, value);
            }
        }

        // 更新M_N数据
        // 同一工序下，瓦斯浓度变化值N
        private void updateMNData()
        {
            var datas = GasConcentrationProbeData.FindNewRealData(currentProbeId, 2);
            var time = datas[0].RecordTime;
            var time1 = datas[1].RecordTime;
            var value = datas[0].ProbeValue;
            var value1 = datas[1].ProbeValue;

            var valueN = value - value1;

            // 判断是否是最新数据
            if (time != _LastTimeMN)
            {
                _LastTimeMN = time;

                // 往DGV中填充数据
                _dgvData.Rows.Add(value + "%", time);
                if (_dgvData.Rows.Count > 0)
                {
                    // 定位滚动条
                    _dgvData.FirstDisplayedScrollingRowIndex = _dgvData.Rows.Count - 1;

                    // 瓦斯浓度超过安全范围 
                    if (value >= yellowDataThreshold && value <= redDataThreshold)
                    {
                        _dgvData.Rows[_dgvData.Rows.Count - 1].DefaultCellStyle.BackColor = Color.Yellow;
                    }
                    else if (value > redDataThreshold)
                    {
                        _dgvData.Rows[_dgvData.Rows.Count - 1].DefaultCellStyle.BackColor = Color.Red;
                    }
                }

                TeeChartUtil.addSingleData2TeeChart(tChartM, DEFAULT_DATA_SHOW_COUNT, time, value);
                TeeChartUtil.addSingleData2TeeChart(tChartN, DEFAULT_DATA_SHOW_COUNT, time, valueN);
            }
        }

        #endregion
    }
}