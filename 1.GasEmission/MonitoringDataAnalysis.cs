// ******************************************************************
// 概  述：监控数据分析
// 作  者：伍鑫
// 创建日期：2013/12/22
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
using LibDatabase;
using System.Data.SqlClient;
using LibBusiness;
using LibEntity;
using LibCommon;
using LibCommonControl;
using LibCommonForm;

namespace _1.GasEmission
{
    public partial class MonitoringDataAnalysis : BaseForm
    {
        /** 探头数据更新频率(单位：秒) **/
        private int _xInterval = 5;

        /** 显示最大数据数 **/
        private const int DEFAULT_DATA_SHOW_COUNT = 110;

        private double _WarnValue = Const.WARN_VALUE;

        Random rnd = new Random();

        /// <summary>
        /// 构造方法
        /// </summary>
        public MonitoringDataAnalysis(MainFrm mainFrm)
        {
            this.MainForm = mainFrm;
            InitializeComponent();

            // 调用选择巷道控件时需要调用的方法
            this.selectTunnelUserControl1.loadMineName();

            // 注册委托事件
            this.selectTunnelUserControl1.TunnelNameChanged += InheritTunnelNameChanged;
        }

        /// <summary>
        /// 委托事件
        /// </summary>
        /// <param name="sender"></param>
        private void InheritTunnelNameChanged(object sender, TunnelEventArgs e)
        {
            this._lstProbeStyle.DataSource = null;
            this._lstProbeName.DataSource = null;

            // 加载探头类型信息
            loadProbeTypeInfo();
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
        /// 探头类型选择事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void _lstProbeStyle_MouseUp(object sender, MouseEventArgs e)
        {
            this._lstProbeName.DataSource = null;

            // 没有选择巷道
            if (this.selectTunnelUserControl1.ITunnelId == Const.INVALID_ID)
            {
                Alert.alert(Const_GE.TUNNEL_NAME_MUST_INPUT);
            }
            else
            {
                // 根据巷道编号和探头类型编号获取探头信息
                DataSet ds = ProbeManageBLL.selectProbeManageInfoByTunnelIDAndProbeType(this.selectTunnelUserControl1.ITunnelId,
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

        /// <summary>
        /// 探头名称选择事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void _lstProbeName_MouseUp(object sender, MouseEventArgs e)
        {
            // 没有选择巷道
            if (this.selectTunnelUserControl1.ITunnelId == Const.INVALID_ID)
            {
                Alert.alert(Const_GE.TUNNEL_NAME_MUST_INPUT);
                return;
            }
            else
            {
                // TODO:方法待实装
            }
        }

        /// <summary>
        /// 实时数据监控
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void _rbtnRealtime_Click(object sender, EventArgs e)
        {
            /** 历史数据分析 **/
            // 历史数据分析按钮
            this._rbtnHistory.Checked = false;
            this._rbtnHistory.Enabled = true;

            // 开始时间
            this._dateTimeStart.Enabled = false;
            // 结束时间
            this._dateTimeEnd.Enabled = false;
            // 查询
            this._btnQuery.Enabled = false;

            /** 实时数据监控 **/
            // 数据传输频率
            _txtSpeed.Enabled = true;
            // 开始按钮
            _btnStart.Enabled = true;
        }

        /// <summary>
        /// 历史数据分析
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void _rbtnHistory_Click(object sender, EventArgs e)
        {
            // 实时数据监控按钮
            this._rbtnRealtime.Checked = false;
            this._rbtnRealtime.Enabled = true;

            // 开始时间
            this._dateTimeStart.Enabled = true;
            // 结束时间
            this._dateTimeEnd.Enabled = true;
            // 查询
            this._btnQuery.Enabled = true;

            /** 实时数据监控 **/
            // 数据传输频率
            _txtSpeed.Enabled = false;
            // 开始按钮
            _btnStart.Enabled = false;

            this._btnStart.Text = "开始";
            this.timer1.Enabled = false;

            // 清空datagridview
            this._dgvData.Rows.Clear();
            // 清空fastline
            this.fastLine1.Clear();
        }

        /// <summary>
        /// 计时器
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void timer1_Tick(object sender, EventArgs e)
        {
            AnimateSeries(this.tChart1);
        }

        private void AnimateSeries(Steema.TeeChart.TChart chart)
        {
            double newY;
            DateTime newX;
            // 重绘
            chart.AutoRepaint = false;

            /// <summary>
            /// 绘画坐标点超过50个时将实时更新X时间坐标
            /// </summary>
            while (this.fastLine1.Count > DEFAULT_DATA_SHOW_COUNT)
            {
                // 删除第一个点
                this.fastLine1.Delete(0);
                // 重新设置X轴的最大值和最小值
                fastLine1.GetHorizAxis.SetMinMax(DateTime.Now.AddSeconds(-110), DateTime.Now.AddSeconds(10));
            }

            newX = DateTime.Now;
            newY = rnd.Next(500);
            if (Math.Abs(newY) > 1.0e+4) newY = 0.0;
            fastLine1.Add(newX, newY / 100);

            // 往DGV中填充数据
            this._dgvData.Rows.Add(newY / 100 + "%", DateTime.Now);
            // 预警值
            double dWarnValue = _WarnValue;

            // 当某点的Y坐标超过某一值时
            if (newY / 100 > dWarnValue)
            {
                this._dgvData.Rows[this._dgvData.Rows.Count - 1].DefaultCellStyle.BackColor = Color.Red;
            }
            // 定位滚动条
            this._dgvData.FirstDisplayedScrollingRowIndex = this._dgvData.Rows.Count - 1;

            // 重绘
            chart.AutoRepaint = true;
            chart.Refresh();
        }

        /// <summary>
        /// load数据
        /// </summary>
        private void loadData()
        {
            // TeeChart初期化
            teechartInit();

            // 重绘
            this.tChart1.AutoRepaint = false;

            string sqlStr = "SELECT * FROM T_GAS_CONCENTRATION_PROBE_DATA WHERE RECORD_TIME > '" +
            this._dateTimeStart.Text + "' AND RECORD_TIME < '" + this._dateTimeEnd.Text + "' ORDER BY RECORD_TIME";
            ManageDataBase db = new ManageDataBase(DATABASE_TYPE.GasEmissionDB);
            DataSet ds = db.ReturnDS(sqlStr);

            // 禁止自动生成列(※位置不可变)
            this._dgvData.AutoGenerateColumns = false;

            int sqlCnt = ds.Tables[0].Rows.Count;

            if (sqlCnt > 0)
            {

                // 重新设置X轴的最大值和最小值
                fastLine1.GetHorizAxis.SetMinMax(Convert.ToDateTime(ds.Tables[0].Rows[0]["RECORD_TIME"]).ToOADate(),
                    Convert.ToDateTime(ds.Tables[0].Rows[0]["RECORD_TIME"]).AddSeconds(120).ToOADate());

                for (int i = 0; i < sqlCnt; i++)
                {
                    double value = Convert.ToDouble(ds.Tables[0].Rows[i]["PROBE_VALUE"]);
                    DateTime time = Convert.ToDateTime(ds.Tables[0].Rows[i]["RECORD_TIME"]);

                    //fastLine1.Add(time, value);
                    fastLine1.Add(time, value);

                    // 往DGV中填充数据
                    this._dgvData.Rows.Add(value + "%", time);
                    // 预警值
                    double dWarnValue = _WarnValue;

                    // 当某点的Y坐标超过某一值时
                    if (value > dWarnValue)
                    {
                        this._dgvData.Rows[this._dgvData.Rows.Count - 1].DefaultCellStyle.BackColor = Color.Red;
                    }
                }

            }

            // 重绘
            this.tChart1.AutoRepaint = true;
            this.tChart1.Refresh();

        }

        /// <summary>
        /// TeeChart初期化
        /// </summary>
        private void teechartInit()
        {
            // 初始化
            this.tChart1.Series[0].Clear();

            //fastLine1.Add(DateTime.Now.ToOADate(), 2.5);

            // 设置Y轴的最小值和最大值
            fastLine1.GetVertAxis.SetMinMax(0, 6);
            // 设置Y轴间距
            this.tChart1.Axes.Left.Increment = 0.25;
        }

        /// <summary>
        /// 设置Marks显示与否
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void _ckbSetMarks_Click(object sender, EventArgs e)
        {
            if (this._ckbSetMarks.Checked == true)
            {
                // Mark显示
                this.fastLine1.Marks.Visible = true;
            }
            else
            {
                // Mark隐藏
                this.fastLine1.Marks.Visible = false;
            }
        }

        /// <summary>
        /// 开始按钮Click事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void _btnStart_Click(object sender, EventArgs e)
        {
            // 没有选择巷道 
            if (this.selectTunnelUserControl1.ITunnelId == Const.INVALID_ID)
            {
                Alert.alert(Const_GE.TUNNEL_NAME_MUST_INPUT);
                return;
            }

            // 没有选择探头
            if (this._lstProbeName.SelectedItems.Count == 0)
            {
                Alert.alert(Const_GE.PROBE_MUST_CHOOSE);
                return;
            }

            if (this.selectTunnelUserControl1.ITunnelId != Const.INVALID_ID
                && this._lstProbeName.SelectedItems.Count > 0)
            {

                if (this._btnStart.Text == "开始")
                {
                    int iInterva = 0;

                    if (this._txtSpeed.Text != "")
                    {
                        iInterva = Convert.ToInt32(this._txtSpeed.Text) * 1000;
                    }
                    else
                    {
                        iInterva = 5000;
                    }
                    _xInterval = Convert.ToInt32(this._txtSpeed.Text);

                    this.timer1.Interval = iInterva;

                    // TeeChart初期化
                    teechartInit();

                    // 设置X轴最小值和最大值
                    fastLine1.GetHorizAxis.SetMinMax(DateTime.Now, DateTime.Now.AddSeconds(120));

                    this._btnStart.Text = "停止";
                    this.timer1.Enabled = true;

                    // 清空datagridview
                    this._dgvData.Rows.Clear();
                }
                else
                {
                    this._btnStart.Text = "开始";
                    this.timer1.Enabled = false;
                }
            }
        }

        /// <summary>
        /// 查询
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void _btnQuery_Click(object sender, EventArgs e)
        {
            // 没有选择巷道 
            if (this.selectTunnelUserControl1.ITunnelId == Const.INVALID_ID)
            {
                Alert.alert(Const_GE.TUNNEL_NAME_MUST_INPUT);
                return;
            }

            // 没有选择探头
            if (this._lstProbeName.SelectedItems.Count == 0)
            {
                Alert.alert(Const_GE.PROBE_MUST_CHOOSE);
                return;
            }

            if (this.selectTunnelUserControl1.ITunnelId != Const.INVALID_ID
                && this._lstProbeName.SelectedItems.Count > 0)
            {
                // 清空datagridview
                this._dgvData.Rows.Clear();

                loadData();
            }

        }
    }
}
