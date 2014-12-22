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
using LibGeometry;
using LibCommon;
using DevExpress.Skins;
using DevExpress.LookAndFeel;
using DevExpress.UserSkins;
using DevExpress.XtraEditors;
using LibBusiness;
using LibDatabase;
using LibEntity;
using TeeChartWrapper;

namespace UnderTerminal
{
    public partial class CurveMonitor : Form
    {
        ///** 探头数据更新频率(单位：秒) **/
        //private int _xInterval = 5;

        private int tunnelId = -1;
        private string tunnelName = string.Empty;
        private string currentProbeId = string.Empty;
        private string _T2Id = string.Empty;

        private double redDataThreshold = 10;
        private double yellowDataThreshold = 0.75;

        UnderMessageWindow mainWin;

        /** 显示最大数据数 **/
        private int DEFAULT_DATA_SHOW_COUNT = 20;
        private int MAX_SECONDS_INTERVAL = 6 * 60 * 60; // 6 个小时

        double t2DeltaSumValue = 0;
        private static int t2DataCount = 1;

        private DateTime _LastTime;

        //private double _WarnValue = Const.WARN_VALUE;
        private DateTime _LastTimeT2; // T2的数据更新时间
        private DateTime _LastTimeMN; // M/N 的数据更新时间

        Random rnd = new Random();

        // 获取点击开始按钮时候的系统时间
        private DateTime _StartTime;

        MonitoringDataAnalysis monitoringDataAnalysisForm = new MonitoringDataAnalysis();

        public CurveMonitor(int tunnelId, string tunnelName, UnderMessageWindow mainW)
        {
            InitializeComponent();
            this.tunnelId = tunnelId;
            this.mainWin = mainW;
            this.Text += "-" + tunnelName;

            // 清空datagridview
            this._dgvData.Rows.Clear();
            // 清空曲线1的fastline
            this.tChartM.Series[0].Clear();
            // 清空曲线2的fastline
            this.tChartT2.Series[0].Clear();
            // 清空曲线3的fastline
            this.tChartN.Series[0].Clear();

            // 根据巷道编号获取探头信息
            DataSet ds = ProbeManageBLL.selectProbeManageInfoByTunnelID(this.tunnelId);

            if (ds.Tables[0].Rows.Count > 0)
            {
                this.cbxSensors.DataSource = ds.Tables[0];
                this.cbxSensors.DisplayMember = ProbeManageDbConstNames.PROBE_NAME;
                this.cbxSensors.ValueMember = ProbeManageDbConstNames.PROBE_ID;

                this.cbxSensors.SelectedIndex = -1;
            }

            // 获取T2传感器的Id
            this._T2Id = getT2Id(tunnelId);
        }

        public int TunnelId
        {
            get { return this.tunnelId; }
            set { this.tunnelId = value;}
        }

        public string TunnelName
        {
            get { return this.tunnelName; }
            set { this.tunnelName = value; }
        }

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

        #region 实时监控

        /// <summary>
        /// 开始按钮Click事件
        /// 原始数据实时监控
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void _btnStart_Click(object sender, EventArgs e)
        {
            // 没有选择巷道 
            if (this.tunnelId == Const.INVALID_ID)
            {
                Alert.alert(Const_GE.TUNNEL_NAME_MUST_INPUT);
                return;
            }

            // 没有选择探头
            if (this.cbxSensors.SelectedIndex < 0)
            {
                Alert.alert(Const_GE.PROBE_MUST_CHOOSE);
                return;
            }

            if (this._btnStart.Text == "开始")
            {
                //this.timer2.Enabled = true;

                // 设置TeeChart1(监控系统原始数据M)
                TeeChartUtil.resetTeeChart(this.tChartM);
                // 设置TeeChart2(T2瓦斯浓度平均增加值Q)
                TeeChartUtil.resetTeeChart(this.tChartT2);
                // 设置TeeChart3(同一工序条件下瓦斯浓度变化值N)
                TeeChartUtil.resetTeeChart(this.tChartN);

                // 获取点击开始按钮时候的系统时间
                _StartTime = DateTime.Now;

                this._btnStart.Text = "停止";

                this.timer1.Interval = 15000;
                DEFAULT_DATA_SHOW_COUNT = 120;

                this.timer1.Enabled = true;
            }
            else
            {
                this._btnStart.Text = "开始";
                this.timer1.Enabled = false;
            }

            // 清空datagridview
            this._dgvData.Rows.Clear();
        }

        #endregion

        #region 监控系统原始数据M



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

        private void updateT2Data()
        {
            if (_T2Id == string.Empty)
            {
                tChartT2.Header.Text = "该巷道没有设置T2传感器.";
                return;
            }

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
                TeeChartUtil.addSingleData2TeeChart(tChartT2, DEFAULT_DATA_SHOW_COUNT, time, value);
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
            if (time != this._LastTimeMN)
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

                TeeChartUtil.addSingleData2TeeChart(tChartM, DEFAULT_DATA_SHOW_COUNT, time, value);
                TeeChartUtil.addSingleData2TeeChart(tChartN, DEFAULT_DATA_SHOW_COUNT, time, valueN);
            }
        }

        #endregion


        /// <summary>
        /// 获取瓦斯浓度数据
        /// </summary>
        /// <returns></returns>
        private DataSet getData()
        {
            StringBuilder sqlStr = new StringBuilder();
            sqlStr.Append("SELECT ");
            sqlStr.Append("* ");
            sqlStr.Append("FROM ");
            sqlStr.Append(GasConcentrationProbeDataDbConstNames.TABLE_NAME + " ");
            sqlStr.Append("WHERE ");
            sqlStr.Append(GasConcentrationProbeDataDbConstNames.PROBE_ID + " = " + this.cbxSensors.SelectedValue + " ");
            //sqlStr.Append("AND ");
            //sqlStr.Append(GasConcentrationProbeDataDbConstNames.RECORD_TIME + " >= '" + this._dateTimeStart.Text + "' ");
            //sqlStr.Append("AND ");
            //sqlStr.Append(GasConcentrationProbeDataDbConstNames.RECORD_TIME + " <= '" + this._dateTimeEnd.Text + "' ");
            sqlStr.Append("ORDER BY RECORD_TIME ");

            ManageDataBase db = new ManageDataBase(DATABASE_TYPE.GasEmissionDB);
            DataSet ds = db.ReturnDS(sqlStr.ToString());

            return ds;
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show(@"确认退出监控系统", "井下终端录入系统", MessageBoxButtons.OKCancel, MessageBoxIcon.Question);
            if (result == DialogResult.OK)
            {
                this.timer1.Enabled = false;
                mainWin.cm = null;
                this.Close();
            }
        }

        /// <summary>
        /// 设置曲线的Marks显示与否
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void _ckbSetMarks_Click(object sender, EventArgs e)
        {
            if (this._ckbSetMarks.Checked == true)
            {
                // Mark显示
                this.tChartM.Series[0].Marks.Visible = true;
                this.tChartT2.Series[0].Marks.Visible = true;
                this.tChartN.Series[0].Marks.Visible = true;
            }
            else
            {
                // Mark隐藏
                this.tChartM.Series[0].Marks.Visible = false;
                this.tChartT2.Series[0].Marks.Visible = false;
                this.tChartN.Series[0].Marks.Visible = false;
            }
        }

        private void btnSwitch_Click(object sender, EventArgs e)
        {
            this.Hide();
        }

        private void cbxSensors_SelectedIndexChanged(object sender, EventArgs e)
        {
            System.Windows.Forms.ComboBox cbx = sender as System.Windows.Forms.ComboBox;
            if (null != cbx.SelectedValue)
                this.currentProbeId = cbx.SelectedValue.ToString();
        }
    }
}
