// ******************************************************************
// 概  述：传感器数据录入
// 作  者：伍鑫
// 创建日期：2013/12/01
// 版本号：V1.0
// 版本信息：
// V1.0 新建
// ******************************************************************

using System;
using System.Windows.Forms;
using LibBusiness;
using LibCommon;
using LibCommonControl;
using LibCommonForm;
using LibEntity;
using LibSocket;
using _1.GasEmission;

namespace sys1
{
    public partial class GasConcentrationProbeDataEntering : BaseForm
    {
        /** 主键  **/
        /** 业务逻辑类型：添加、修改  **/
        private readonly string _bllType = "add";
        private readonly string _iPK;

        /// <summary>
        ///     构造方法
        /// </summary>
        public GasConcentrationProbeDataEntering(SocketHelper mainFrm)
        {
            MainForm = mainFrm;

            InitializeComponent();

            //分配权限
            if (CurrentUser.CurLoginUserInfo.Permission != Permission.管理员.ToString())
            {
                selectTunnelUserControl1.SetButtonEnable(false);
            }

            // 设置窗体默认属性
            FormDefaultPropertiesSetter.SetEnteringFormDefaultProperties(this,
                Const_GE.INSERT_GAS_CONCENTRATION_PROBE_DATA);

            // 设置日期控件格式
            dtpRecordTime.Format = DateTimePickerFormat.Custom;
            dtpRecordTime.CustomFormat = Const.DATE_FORMART_YYYY_MM_DD;

            // 调用选择巷道控件时需要调用的方法
            selectTunnelUserControl1.LoadData();

            // 注册委托事件
            //selectTunnelUserControl1.TunnelNameChanged +=
            //    InheritTunnelNameChanged;
        }

        /// <summary>
        ///     带参数的构造方法
        /// </summary>
        /// <param name="intArr"></param>
        public GasConcentrationProbeDataEntering()
        {

            InitializeComponent();

            // 设置窗体默认属性
            FormDefaultPropertiesSetter.SetEnteringFormDefaultProperties(this,
                Const_GE.INSERT_GAS_CONCENTRATION_PROBE_DATA);

            // 设置日期控件格式
            dtpRecordTime.Format = DateTimePickerFormat.Custom;
            dtpRecordTime.CustomFormat = Const.DATE_FORMART_YYYY_MM_DD;
        }

        /// <summary>
        ///     带参数的构造方法
        /// </summary>
        /// <param name="intArr"></param>
        public GasConcentrationProbeDataEntering(string strPrimaryKey)
        {
            InitializeComponent();

            _iPK = strPrimaryKey;

            // 设置业务类型
            _bllType = "update";

            // 设置窗体默认属性
            FormDefaultPropertiesSetter.SetEnteringFormDefaultProperties(this,
                Const_GE.UPDATE_GAS_CONCENTRATION_PROBE_DATA);

            // 设置日期控件格式
            dtpRecordTime.Format = DateTimePickerFormat.Custom;
            dtpRecordTime.CustomFormat = Const.DATE_FORMART_YYYY_MM_DD;

            // 调用选择巷道控件时需要调用的方法
            //selectTunnelUserControl1.setCurSelectedID(intArr);

            // 注册委托事件
            //selectTunnelUserControl1.TunnelNameChanged +=
            //    InheritTunnelNameChanged;

            // 设置瓦斯浓度探头数据
            setGasConcentrationProbeData(strPrimaryKey);
        }

        /// <summary>
        ///     设置瓦斯浓度探头数据
        /// </summary>
        public void setGasConcentrationProbeData(string strPrimaryKey)
        {
            if (!Validator.IsEmpty(strPrimaryKey))
            {
                GasConcentrationProbeData data = GasConcentrationProbeData.TryFind(strPrimaryKey);
                // 数值
                if (data != null)
                {
                    txtProbeValue.Text =
                        data.ProbeValue.ToString();
                    // 时间
                    dtpRecordTime.Text =
                        data.RecordTime.ToString("yyyy-MMMM-dd hh:mm:ss");
                }
            }
        }

        /// <summary>
        ///     委托事件
        /// </summary>
        /// <param name="sender"></param>
        //private void InheritTunnelNameChanged(object sender, TunnelEventArgs e)
        //{
        //    _lstProbeStyle.DataSource = null;
        //    _lstProbeName.DataSource = null;

        //    // 加载探头类型信息
        //    loadProbeTypeInfo();
        //}

        /// <summary>
        ///     加载探头类型信息
        /// </summary>
        private void loadProbeTypeInfo()
        {
            ProbeType[] probeTypes = ProbeType.FindAll();
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
            if (selectTunnelUserControl1.SelectedTunnel == null)
            {
                Alert.alert(Const_GE.TUNNEL_NAME_MUST_INPUT);
            }
            else
            {
                // 根据巷道编号和探头类型编号获取探头信息
                Probe[] probes = Probe.FindAllByTunnelIdAndProbeTypeId(selectTunnelUserControl1.SelectedTunnel.TunnelId,
                    Convert.ToInt32(this._lstProbeStyle.SelectedValue));

                for (int i = 0; i < probes.Length; i++)
                {
                    _lstProbeName.Items.Add(probes);
                }
                _lstProbeName.DisplayMember = "ProbeName";
                _lstProbeName.ValueMember = "ProbeId";

                _lstProbeName.SelectedIndex = -1;
            }
        }

        /// <summary>
        ///     加载探头名称
        /// </summary>
        /// <param name="iTunnelId"></param>
        private void loadProbeName(int iTunnelId)
        {
            _lstProbeName.DataSource = null;

            // 根据巷道编号和探头类型编号获取探头信息
            Probe[] probes = Probe.FindAllByTunnelIdAndProbeTypeId(iTunnelId,
                Convert.ToInt32(this._lstProbeStyle.SelectedValue));

            for (int i = 0; i < probes.Length; i++)
            {
                _lstProbeName.Items.Add(probes);
            }
            _lstProbeName.DisplayMember = "ProbeName";
            _lstProbeName.ValueMember = "ProbeId";

            _lstProbeName.SelectedIndex = -1;
        }

        /// <summary>
        ///     探头名称选择事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void _lstProbeName_MouseUp(object sender, MouseEventArgs e)
        {
            // 没有选择巷道
            if (selectTunnelUserControl1.SelectedTunnel == null)
            {
                Alert.alert(Const_GE.TUNNEL_NAME_MUST_INPUT);
            }
        }

        /// <summary>
        ///     提交
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnSubmit_Click(object sender, EventArgs e)
        {
            // 验证
            if (!check())
            {
                DialogResult = DialogResult.None;
                return;
            }
            DialogResult = DialogResult.OK;

            // 创建一个瓦斯浓度探头数据实体
            var gasConcentrationProbeDataEntity = new GasConcentrationProbeData();
            // 探头编号
            gasConcentrationProbeDataEntity.Probe.ProbeId = Convert.ToString(_lstProbeName.SelectedValue);
            // 探头数值
            double dProbeValue = 0;
            if (double.TryParse(txtProbeValue.Text.Trim(), out dProbeValue))
            {
                gasConcentrationProbeDataEntity.ProbeValue = dProbeValue;
            }
            // 记录时间
            gasConcentrationProbeDataEntity.RecordTime = dtpRecordTime.Value;
            // 记录类型
            gasConcentrationProbeDataEntity.RecordType = Const_GE.RECORDTYPE_PEOPLE;

            OPERATION_TYPE opType;
            bool bResult = false;
            if (_bllType == "add")
            {
                // 瓦斯浓度探头数据登录
                gasConcentrationProbeDataEntity.CreateAndFlush();
                bResult = true;
                opType = OPERATION_TYPE.ADD;

                if (bResult)
                {
                    #region 通知服务器预警数据已更新

                    WorkingFace workingfaceEnt = selectTunnelUserControl1.SelectedTunnel.WorkingFace;
                    var msg = new UpdateWarningDataMsg(workingfaceEnt.WorkingFaceId, selectTunnelUserControl1.SelectedTunnel.TunnelId,
                        DayReportHc.TableName, opType, gasConcentrationProbeDataEntity.RecordTime);
                    MainForm.SendMsg2Server(msg);

                    #endregion
                }
            }
            else
            {
                // 主键
                gasConcentrationProbeDataEntity.ProbeDataId = Convert.ToInt32(_iPK);
                // 探头管理信息修改
                gasConcentrationProbeDataEntity.SaveAndFlush();
                opType = OPERATION_TYPE.UPDATE;

                #region 通知服务器预警数据已更新

                WorkingFace workingfaceEnt = selectTunnelUserControl1.SelectedTunnel.WorkingFace;
                var msg = new UpdateWarningDataMsg(workingfaceEnt.WorkingFaceId, selectTunnelUserControl1.SelectedTunnel.TunnelId,
                    DayReportHc.TableName, opType, gasConcentrationProbeDataEntity.RecordTime);
                MainForm.SendMsg2Server(msg);

                #endregion
            }


            // 添加\修改成功的场合
            if (bResult)
            {
                // 如何探头编号发生改变则不更新管理画面的farpoint
                if (GasConcentrationProbeDataManamement._probeId != Convert.ToString(_lstProbeName.SelectedValue))
                {
                    GasConcentrationProbeDataManamement._iDisposeFlag = Const.DISPOSE_FLAG_ZERO;
                }
            }
        }

        /// <summary>
        ///     取消
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnCancel_Click(object sender, EventArgs e)
        {
            // 窗口关闭
            Close();
        }

        /// <summary>
        ///     验证画面入力数据
        /// </summary>
        /// <returns>验证结果：true 通过验证, false未通过验证</returns>
        private bool check()
        {
            // 判断选择巷道是否选择
            if (selectTunnelUserControl1.SelectedTunnel == null)
            {
                Alert.alert(Const_GE.TUNNEL_NAME_MUST_INPUT);
                return false;
            }

            // 判断传感器是否选择
            if (_lstProbeName.SelectedItems.Count == 0)
            {
                Alert.alert(Const_GE.PROBE_MUST_CHOOSE);
                return false;
            }

            // 判断传感器数值是否录入
            if (!Check.isEmpty(txtProbeValue, Const_GE.PROBE_VALUE))
            {
                return false;
            }

            // 判断传感器数值是否为数字
            if (!Check.IsNumeric(txtProbeValue, Const_GE.PROBE_VALUE))
            {
                return false;
            }

            // 验证通过
            return true;
        }
    }
}