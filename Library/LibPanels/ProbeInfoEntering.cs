using System;
using System.Globalization;
using System.Windows.Forms;
using LibBusiness;
using LibCommon;
using LibCommonControl;
using LibCommonForm;
using LibEntity;
using LibSocket;

namespace LibPanels
{
    public partial class ProbeInfoEntering : BaseForm
    {
        /** 主键  **/
        /** 业务逻辑类型：添加、修改  **/
        private readonly string _bllType = "add";
        private readonly string _iPk;

        private readonly int _oldTunnelId;

        private readonly Probe _probe;

        /// <summary>
        ///     构造方法
        /// </summary>
        public ProbeInfoEntering(MainFrm mainFrm)
        {
            MainForm = mainFrm;
            InitializeComponent();

            // 设置窗体默认属性
            FormDefaultPropertiesSetter.SetEnteringFormDefaultProperties(this, Const_GE.INSERT_PROBE_INFO);

            // 调用选择巷道控件时需要调用的方法
            //this.selectTunnelSimple1.loadMineName();
        }

        /// <summary>
        ///     带参数的构造方法
        /// </summary>
        /// <param name="strPrimaryKey">主键</param>
        /// <param name="mainFrm"></param>
        public ProbeInfoEntering(string strPrimaryKey, MainFrm mainFrm)
        {
            MainForm = mainFrm;
            InitializeComponent();
            _oldTunnelId = Convert.ToInt32(strPrimaryKey);
            _iPk = strPrimaryKey;

            // 设置窗体默认属性
            FormDefaultPropertiesSetter.SetEnteringFormDefaultProperties(this, Const_GE.UPDATE_PROBE_INFO);

            // 设置业务类型
            _bllType = "update";

            _probe = Probe.Find(strPrimaryKey);
            // 调用选择巷道控件时需要调用的方法
            //this.selectTunnelUserControl1.setCurSelectedID(_intArr);
        }


        private void selectTunnelSimple1_Load(object sender, EventArgs e)
        {
            // 加载探头类型信息
            LoadProbeTypeInfo();

            // 设置探头信息
            if (_bllType == "update")
                SetProbeInfo();
        }

        /// <summary>
        ///     加载探头类型信息
        /// </summary>
        private void LoadProbeTypeInfo()
        {
            ProbeType[] preoTypes = ProbeType.FindAll();
            if (preoTypes.Length > 0)
            {
                cboProbeType.DataSource = preoTypes;
                cboProbeType.DisplayMember = "ProbeTypeName";
                cboProbeType.ValueMember = "ProbeTypeId";

                cboProbeType.SelectedIndex = -1;
            }
        }

        /// <summary>
        ///     验证画面录入数据
        /// </summary>
        /// <returns>验证结果：true 通过验证, false未通过验证</returns>
        private bool Check()
        {
            // 判断探头编号是否录入
            if (!LibCommon.Check.isEmpty(txtProbeId, Const_GE.PROBE_ID))
            {
                return false;
            }

            // 只有在添加窗口才做探头编号是否存在的判断
            if (_bllType == "add")
            {
                // 判断探头编号是否存在
                if (!LibCommon.Check.isExist(txtProbeId, Const_GE.PROBE_ID,
                    Probe.Exists(txtProbeId.Text.Trim())))
                {
                    return false;
                }
            }

            // 判断探头名称是否录入
            if (!LibCommon.Check.isEmpty(cmbProbeName, Const_GE.PROBE_NAME))
            {
                return false;
            }

            // 判断探头类型是否选择
            if (!LibCommon.Check.isEmpty(cboProbeType, Const_GE.PROBE_TYPE))
            {
                return false;
            }

            // 是否自动位移如果选择是的话
            if (rbtnYes.Checked)
            {

                // 判断距迎头距离是否录入
                if (!LibCommon.Check.isEmpty(txtM, Const_GE.FAR_FROM_FRONTAL))
                {
                    return false;
                }

                // 判断距迎头距离是否为数字
                if (!LibCommon.Check.IsNumeric(txtM, Const_GE.FAR_FROM_FRONTAL))
                {
                    return false;
                }
            }

            // 判断所属是否选择
            if (selectTunnelSimple1.ITunnelId == Const.INVALID_ID)
            {
                Alert.alert(Const_GE.TUNNEL_NAME_MUST_INPUT);
                return false;
            }

            // 探头类型编号
            int iProbeTypeId;
            int.TryParse(Convert.ToString(cboProbeType.SelectedValue), out iProbeTypeId);
            var probeExist = Probe.FindFirstByProbeTypeIdAndProbeNameAndTunnelId(iProbeTypeId,
                cmbProbeName.SelectedItem.ToString(),
                selectTunnelSimple1.ITunnelId);
            if (probeExist == null) return true;
            if (!Alert.confirm("该巷道下已经存在" + cboProbeType.Text + "探头，确认提交将重新设定" + cboProbeType.Text + "探头。"))
                return false;
            // 重置探头类型
            _probe.ProbeType = null;
            _probe.SaveAndFlush();
            return true;

            // 验证通过
        }

        private void btnSubmit_Click(object sender, EventArgs e)
        {
            // 验证
            if (!Check())
            {
                DialogResult = DialogResult.None;
                return;
            }
            DialogResult = DialogResult.OK;

            // 创建探头管理实体

            _probe.ProbeName = cmbProbeName.Text.Trim();
            _probe.ProbeType = ProbeType.Find(cboProbeType.SelectedValue);


            if (rbtnYes.Checked)
            {
                // 是否自动位移
                _probe.IsMove = 1;
                // 距迎头距离
                double dFarFromFrontal;
                double.TryParse(txtM.Text.Trim(), out dFarFromFrontal);
                _probe.FarFromFrontal = dFarFromFrontal;
            }
            else
            {
                // 是否自动位移
                _probe.IsMove = 0;
            }

            // 巷道编号
            _probe.Tunnel = Tunnel.Find(selectTunnelSimple1.ITunnelId);

            // 探头描述
            _probe.ProbeDescription = txtProbeDescription.Text.Trim();
            OPERATION_TYPE opType;

            if (_bllType == "add")
            {
                // 探头管理信息登录
                _probe.Save();
                opType = OPERATION_TYPE.ADD;
            }
            else
            {
                // 主键
                _probe.ProbeId = _iPk;
                // 探头管理信息修改
                _probe.Save();
                opType = OPERATION_TYPE.UPDATE;
            }
            //通知服务器探头数据已更新
            // Added by jhou, 2014/3/24
            WorkingFace workingfaceEnt =
                BasicInfoManager.getInstance().getTunnelByID(_probe.Tunnel.TunnelId).WorkingFace;

            if (_oldTunnelId != 0)
            {
                var msgUpdate = new UpdateWarningDataMsg(workingfaceEnt.WorkingFaceId,
                    _probe.Tunnel.TunnelId,
                    Probe.TableName, opType, DateTime.Now);
                MainForm.SendMsg2Server(msgUpdate);
            }

            var msg = new UpdateWarningDataMsg(workingfaceEnt.WorkingFaceId,
                _probe.Tunnel.TunnelId,
                Probe.TableName, opType, DateTime.Now);
            MainForm.SendMsg2Server(msg);
        }

        /// <summary>
        ///     取消
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnCancel_Click(object sender, EventArgs e)
        {
            // 关闭窗口
            Close();
        }

        /// <summary>
        ///     设置探头信息
        /// </summary>
        private void SetProbeInfo()
        {
            // 探头信息
            if (_probe == null) return;
            // 探头编号
            txtProbeId.Text = _probe.ProbeId;
            // 探头编号不可修改
            txtProbeId.Enabled = false;

            // 探头名称
            cmbProbeName.Text = _probe.ProbeName;

            // 探头类型
            cboProbeType.SelectedValue = _probe.ProbeType != null ? _probe.ProbeType.ProbeTypeId : 0;

            // 是否自动位移
            if (_probe.IsMove == 1)
            {
                rbtnYes.Checked = true;

                // 距迎头距离
                txtM.Text = _probe.FarFromFrontal.ToString(CultureInfo.InvariantCulture);
                txtM.Enabled = true;
            }
            else
            {
                rbtnNo.Checked = true;
            }

            // 探头描述
            txtProbeDescription.Text = _probe.ProbeDescription;

            // 所在巷道绑定
            if (_probe.Tunnel != null)
            {
                //if(
                var ts = new TunnelSimple(_probe.Tunnel.TunnelId, _probe.Tunnel.TunnelName);
                selectTunnelSimple1.SelectTunnelItemWithoutHistory(ts);
            }
        }

        /// <summary>
        ///     是否自动位移Click事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void rbtnYes_Click(object sender, EventArgs e)
        {
            if (rbtnYes.Checked)
            {
                // 距迎头距离设为可用
                txtM.Enabled = true;
            }
        }

        /// <summary>
        ///     是否自动位移Click事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void rbtnNo_Click(object sender, EventArgs e)
        {
            if (rbtnNo.Checked)
            {
                // 距迎头距离设为不可用
                txtM.Enabled = false;
            }
        }
    }
}