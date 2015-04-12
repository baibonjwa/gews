// ******************************************************************
// 概  述：监控数据分析
// 作  者：伍鑫
// 创建日期：2013/12/22
// 版本号：V1.0
// 版本信息：
// V1.0 新建
// ******************************************************************

using System;
using System.Drawing;
using System.Windows.Forms;
using LibCommon;
using LibDatabase;
using LibEntity;
using Steema.TeeChart;

namespace UnderTerminal
{
    public partial class MonitoringDataAnalysis : Form
    {
        /** 显示最大数据数 **/
        private const int DEFAULT_DATA_SHOW_COUNT = 110;
        /** 探头数据更新频率(单位：秒) **/
        private int _xInterval = 5;
        private readonly double _WarnValue = Const.WARN_VALUE;
        private readonly Random rnd = new Random();

        /// <summary>
        ///     构造方法
        /// </summary>
        public MonitoringDataAnalysis()
        {
            InitializeComponent();

            // 调用选择巷道控件时需要调用的方法
            selectTunnelUserControl1.loadMineName();

            // 注册委托事件
            selectTunnelUserControl1.TunnelNameChanged += InheritTunnelNameChanged;
        }

        /// <summary>
        ///     委托事件
        /// </summary>
        /// <param name="sender"></param>
        private void InheritTunnelNameChanged(object sender, TunnelEventArgs e)
        {
            _lstProbeStyle.DataSource = null;
            _lstProbeName.DataSource = null;

            // 加载探头类型信息
            loadProbeTypeInfo();
        }

        /// <summary>
        ///     加载探头类型信息
        /// </summary>
        private void loadProbeTypeInfo()
        {
            var probeTypes = ProbeType.FindAll();
            if (probeTypes.Length > 0)
            {
                _lstProbeStyle.DataSource = probeTypes;
                _lstProbeStyle.DisplayMember = "ProbeTypeName";
                _lstProbeStyle.ValueMember = "ProbeTypeId";

                _lstProbeStyle.SelectedIndex = -1;
            }
        }

        /// <summary>
        ///     探头类型选择事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void _lstProbeStyle_MouseUp(object sender, MouseEventArgs e)
        {
            _lstProbeName.DataSource = null;

            // 没有选择巷道
            if (selectTunnelUserControl1.ITunnelId == Const.INVALID_ID)
            {
                Alert.alert(Const_GE.TUNNEL_NAME_MUST_INPUT);
            }
            else
            {
                // 根据巷道编号和探头类型编号获取探头信息
                var probes = Probe.FindAllByTunnelIdAndProbeTypeId(selectTunnelUserControl1.ITunnelId,
                    Convert.ToInt32(_lstProbeStyle.SelectedValue));

                for (var i = 0; i < probes.Length; i++)
                {
                    _lstProbeName.Items.Add(probes);
                }
                _lstProbeName.DisplayMember = "ProbeName";
                _lstProbeName.ValueMember = "ProbeId";

                _lstProbeName.SelectedIndex = -1;
            }
        }

        /// <summary>
        ///     探头名称选择事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void _lstProbeName_MouseUp(object sender, MouseEventArgs e)
        {
            // 没有选择巷道
            if (selectTunnelUserControl1.ITunnelId == Const.INVALID_ID)
            {
                Alert.alert(Const_GE.TUNNEL_NAME_MUST_INPUT);
            }
        }

        /// <summary>
        ///     实时数据监控
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void _rbtnRealtime_Click(object sender, EventArgs e)
        {
            /** 历史数据分析 **/
            // 历史数据分析按钮
            _rbtnHistory.Checked = false;
            _rbtnHistory.Enabled = true;

            // 开始时间
            _dateTimeStart.Enabled = false;
            // 结束时间
            _dateTimeEnd.Enabled = false;
            // 查询
            _btnQuery.Enabled = false;

            /** 实时数据监控 **/
            // 数据传输频率
            _txtSpeed.Enabled = true;
            // 开始按钮
            _btnStart.Enabled = true;
        }

        /// <summary>
        ///     历史数据分析
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void _rbtnHistory_Click(object sender, EventArgs e)
        {
            // 实时数据监控按钮
            _rbtnRealtime.Checked = false;
            _rbtnRealtime.Enabled = true;

            // 开始时间
            _dateTimeStart.Enabled = true;
            // 结束时间
            _dateTimeEnd.Enabled = true;
            // 查询
            _btnQuery.Enabled = true;

            /** 实时数据监控 **/
            // 数据传输频率
            _txtSpeed.Enabled = false;
            // 开始按钮
            _btnStart.Enabled = false;

            _btnStart.Text = "开始";
            timer1.Enabled = false;

            // 清空datagridview
            _dgvData.Rows.Clear();
            // 清空fastline
            fastLine1.Clear();
        }

        /// <summary>
        ///     计时器
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void timer1_Tick(object sender, EventArgs e)
        {
            AnimateSeries(tChart1);
        }

        private void AnimateSeries(TChart chart)
        {
            double newY;
            DateTime newX;
            // 重绘
            chart.AutoRepaint = false;

            /// <summary>
            /// 绘画坐标点超过50个时将实时更新X时间坐标
            /// </summary>
            while (fastLine1.Count > DEFAULT_DATA_SHOW_COUNT)
            {
                // 删除第一个点
                fastLine1.Delete(0);
                // 重新设置X轴的最大值和最小值
                fastLine1.GetHorizAxis.SetMinMax(DateTime.Now.AddSeconds(-110), DateTime.Now.AddSeconds(10));
            }

            newX = DateTime.Now;
            newY = rnd.Next(500);
            if (Math.Abs(newY) > 1.0e+4) newY = 0.0;
            fastLine1.Add(newX, newY/100);

            // 往DGV中填充数据
            _dgvData.Rows.Add(newY/100 + "%", DateTime.Now);
            // 预警值
            var dWarnValue = _WarnValue;

            // 当某点的Y坐标超过某一值时
            if (newY/100 > dWarnValue)
            {
                _dgvData.Rows[_dgvData.Rows.Count - 1].DefaultCellStyle.BackColor = Color.Red;
            }
            // 定位滚动条
            _dgvData.FirstDisplayedScrollingRowIndex = _dgvData.Rows.Count - 1;

            // 重绘
            chart.AutoRepaint = true;
            chart.Refresh();
        }

        /// <summary>
        ///     load数据
        /// </summary>
        private void loadData()
        {
            // TeeChart初期化
            teechartInit();

            // 重绘
            tChart1.AutoRepaint = false;

            var sqlStr = "SELECT * FROM T_GAS_CONCENTRATION_PROBE_DATA WHERE RECORD_TIME > '" +
                         _dateTimeStart.Text + "' AND RECORD_TIME < '" + _dateTimeEnd.Text + "' ORDER BY RECORD_TIME";
            var db = new ManageDataBase(DATABASE_TYPE.GasEmissionDB);
            var ds = db.ReturnDS(sqlStr);

            // 禁止自动生成列(※位置不可变)
            _dgvData.AutoGenerateColumns = false;

            var sqlCnt = ds.Tables[0].Rows.Count;

            if (sqlCnt > 0)
            {
                // 重新设置X轴的最大值和最小值
                fastLine1.GetHorizAxis.SetMinMax(Convert.ToDateTime(ds.Tables[0].Rows[0]["RECORD_TIME"]).ToOADate(),
                    Convert.ToDateTime(ds.Tables[0].Rows[0]["RECORD_TIME"]).AddSeconds(120).ToOADate());

                for (var i = 0; i < sqlCnt; i++)
                {
                    var value = Convert.ToDouble(ds.Tables[0].Rows[i]["PROBE_VALUE"]);
                    var time = Convert.ToDateTime(ds.Tables[0].Rows[i]["RECORD_TIME"]);

                    //fastLine1.Add(time, value);
                    fastLine1.Add(time, value);

                    // 往DGV中填充数据
                    _dgvData.Rows.Add(value + "%", time);
                    // 预警值
                    var dWarnValue = _WarnValue;

                    // 当某点的Y坐标超过某一值时
                    if (value > dWarnValue)
                    {
                        _dgvData.Rows[_dgvData.Rows.Count - 1].DefaultCellStyle.BackColor = Color.Red;
                    }
                }
            }

            // 重绘
            tChart1.AutoRepaint = true;
            tChart1.Refresh();
        }

        /// <summary>
        ///     TeeChart初期化
        /// </summary>
        private void teechartInit()
        {
            // 初始化
            tChart1.Series[0].Clear();

            //fastLine1.Add(DateTime.Now.ToOADate(), 2.5);

            // 设置Y轴的最小值和最大值
            fastLine1.GetVertAxis.SetMinMax(0, 6);
            // 设置Y轴间距
            tChart1.Axes.Left.Increment = 0.25;
        }

        /// <summary>
        ///     设置Marks显示与否
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void _ckbSetMarks_Click(object sender, EventArgs e)
        {
            if (_ckbSetMarks.Checked)
            {
                // Mark显示
                fastLine1.Marks.Visible = true;
            }
            else
            {
                // Mark隐藏
                fastLine1.Marks.Visible = false;
            }
        }

        /// <summary>
        ///     开始按钮Click事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void _btnStart_Click(object sender, EventArgs e)
        {
            // 没有选择巷道 
            if (selectTunnelUserControl1.ITunnelId == Const.INVALID_ID)
            {
                Alert.alert(Const_GE.TUNNEL_NAME_MUST_INPUT);
                return;
            }

            // 没有选择探头
            if (_lstProbeName.SelectedItems.Count == 0)
            {
                Alert.alert(Const_GE.PROBE_MUST_CHOOSE);
                return;
            }

            if (selectTunnelUserControl1.ITunnelId != Const.INVALID_ID
                && _lstProbeName.SelectedItems.Count > 0)
            {
                if (_btnStart.Text == "开始")
                {
                    var iInterva = 0;

                    if (_txtSpeed.Text != "")
                    {
                        iInterva = Convert.ToInt32(_txtSpeed.Text)*1000;
                    }
                    else
                    {
                        iInterva = 5000;
                    }
                    _xInterval = Convert.ToInt32(_txtSpeed.Text);

                    timer1.Interval = iInterva;

                    // TeeChart初期化
                    teechartInit();

                    // 设置X轴最小值和最大值
                    fastLine1.GetHorizAxis.SetMinMax(DateTime.Now, DateTime.Now.AddSeconds(120));

                    _btnStart.Text = "停止";
                    timer1.Enabled = true;

                    // 清空datagridview
                    _dgvData.Rows.Clear();
                }
                else
                {
                    _btnStart.Text = "开始";
                    timer1.Enabled = false;
                }
            }
        }

        /// <summary>
        ///     查询
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void _btnQuery_Click(object sender, EventArgs e)
        {
            // 没有选择巷道 
            if (selectTunnelUserControl1.ITunnelId == Const.INVALID_ID)
            {
                Alert.alert(Const_GE.TUNNEL_NAME_MUST_INPUT);
                return;
            }

            // 没有选择探头
            if (_lstProbeName.SelectedItems.Count == 0)
            {
                Alert.alert(Const_GE.PROBE_MUST_CHOOSE);
                return;
            }

            if (selectTunnelUserControl1.ITunnelId != Const.INVALID_ID
                && _lstProbeName.SelectedItems.Count > 0)
            {
                // 清空datagridview
                _dgvData.Rows.Clear();

                loadData();
            }
        }
    }
}