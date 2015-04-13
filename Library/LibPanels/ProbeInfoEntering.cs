using System;
using System.Globalization;
using System.Windows.Forms;
using LibBusiness;
using LibCommon;
using LibEntity;
using LibSocket;

namespace LibPanels
{
    public partial class ProbeInfoEntering : Form
    {
        private Probe Probe { get; set; }
        /// <summary>
        ///     构造方法
        /// </summary>
        public ProbeInfoEntering()
        {
            InitializeComponent();

            // 设置窗体默认属性
            FormDefaultPropertiesSetter.SetEnteringFormDefaultProperties(this, Const_GE.INSERT_PROBE_INFO);

            // 调用选择巷道控件时需要调用的方法
            //this.selectTunnelSimple1.loadMineName();
        }

        /// <summary>
        ///     带参数的构造方法
        /// </summary>
        /// <param name="probe"></param>
        public ProbeInfoEntering(Probe probe)
        {
            InitializeComponent();
            Probe = probe;
            FormDefaultPropertiesSetter.SetEnteringFormDefaultProperties(this, Const_GE.UPDATE_PROBE_INFO);
        }


        private void selectTunnelSimple1_Load(object sender, EventArgs e)
        {
            DataBindUtil.LoadProbeType(cboProbeType);
            if (Probe == null) return;
            // 探头编号
            txtProbeId.Text = Probe.ProbeId;
            // 探头编号不可修改
            txtProbeId.Enabled = false;

            // 探头名称
            cmbProbeName.Text = Probe.ProbeName;

            // 探头类型
            cboProbeType.SelectedValue = Probe.ProbeType != null ? Probe.ProbeType.ProbeTypeId : 0;

            // 是否自动位移
            if (Probe.IsMove == 1)
            {
                rbtnYes.Checked = true;

                // 距迎头距离
                txtM.Text = Probe.FarFromFrontal.ToString(CultureInfo.InvariantCulture);
                txtM.Enabled = true;
            }
            else
            {
                rbtnNo.Checked = true;
            }

            // 探头描述
            txtProbeDescription.Text = Probe.ProbeDescription;

            // 所在巷道绑定
            if (Probe.Tunnel != null)
            {
                selectTunnelSimple1.SetTunnel(Probe.Tunnel);
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

            // 判断探头编号是否存在
            //if (!LibCommon.Check.isExist(txtProbeId, Const_GE.PROBE_ID,
            //    Probe.Exists(txtProbeId.Text.Trim())))
            //{
            //    return false;
            //}


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
            if (selectTunnelSimple1.SelectedTunnel == null)
            {
                Alert.alert(Const_GE.TUNNEL_NAME_MUST_INPUT);
                return false;
            }

            // 探头类型编号
            int iProbeTypeId;
            int.TryParse(Convert.ToString(cboProbeType.SelectedValue), out iProbeTypeId);
            if (selectTunnelSimple1.SelectedTunnel != null)
            {
                var probeExist = Probe.FindFirstByProbeTypeIdAndProbeNameAndTunnelId(iProbeTypeId,
                    cmbProbeName.SelectedItem.ToString(),
                    selectTunnelSimple1.SelectedTunnel.TunnelId);
                if (probeExist == null) return true;
            }
            if (!Alert.confirm("该巷道下已经存在" + cboProbeType.Text + "探头，确认提交将重新设定" + cboProbeType.Text + "探头。"))
                return false;
            // 重置探头类型
            Probe.ProbeType = null;
            Probe.SaveAndFlush();
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

            Probe.ProbeName = cmbProbeName.Text.Trim();
            Probe.ProbeType = ProbeType.Find(cboProbeType.SelectedValue);


            if (rbtnYes.Checked)
            {
                // 是否自动位移
                Probe.IsMove = 1;
                // 距迎头距离
                double dFarFromFrontal;
                double.TryParse(txtM.Text.Trim(), out dFarFromFrontal);
                Probe.FarFromFrontal = dFarFromFrontal;
            }
            else
            {
                // 是否自动位移
                Probe.IsMove = 0;
            }

            // 巷道编号
            Probe.Tunnel = selectTunnelSimple1.SelectedTunnel;

            // 探头描述
            Probe.ProbeDescription = txtProbeDescription.Text.Trim();
            Probe.Save();

            var msg = new UpdateWarningDataMsg(Probe.Tunnel.WorkingFace.WorkingFaceId,
               Probe.Tunnel.TunnelId,
                Probe.TableName, OPERATION_TYPE.UPDATE, DateTime.Now);
            SocketUtil.SendMsg2Server(msg);

        }

        /// <summary>
        ///     取消
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnCancel_Click(object sender, EventArgs e)
        {
            // 关闭窗口

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