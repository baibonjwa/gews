// ******************************************************************
// 概  述：探头管理数据录入
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
using LibEntity;
using LibSocket;

namespace _5.WarningManagement
{
    public partial class ProbeInfoEntering : BaseForm
    {
        /** 主键  **/
        /** 业务逻辑类型：添加、修改  **/
        private readonly string _bllType = "add";
        private readonly string _iPK;
        /** 存放矿井名称，水平，采区，工作面，巷道编号的数组  **/
        private int[] _intArr = new int[5];

        private int oldTunnelId;

        /// <summary>
        ///     构造方法
        /// </summary>
        public ProbeInfoEntering(MainFrm mainFrm)
        {
            MainForm = mainFrm;

            InitializeComponent();

            // 设置窗体默认属性
            FormDefaultPropertiesSetter.SetEnteringFormDefaultProperties(this, Const_GE.INSERT_PROBE_INFO);

            // 加载探头类型信息
            loadProbeTypeInfo();

            // 调用选择巷道控件时需要调用的方法
            selectTunnelUserControl1.init(mainFrm);
            selectTunnelUserControl1.loadMineName();
        }

        /// <summary>
        ///     带参数的构造方法
        /// </summary>
        /// <param name="strPrimaryKey">主键</param>
        public ProbeInfoEntering(string strPrimaryKey, MainFrm mainFrm)
        {
            MainForm = mainFrm;

            InitializeComponent();

            _iPK = strPrimaryKey;

            // 设置窗体默认属性
            FormDefaultPropertiesSetter.SetEnteringFormDefaultProperties(this, Const_GE.UPDATE_PROBE_INFO);

            // 设置业务类型
            _bllType = "update";

            // 加载探头类型信息
            loadProbeTypeInfo();

            // 设置探头信息
            setProbeInfo();

            // 调用选择巷道控件时需要调用的方法
            selectTunnelUserControl1.setCurSelectedID(_intArr);
        }

        /// <summary>
        ///     加载探头类型信息
        /// </summary>
        private void loadProbeTypeInfo()
        {
            ProbeType[] probeTypes = ProbeType.FindAll();
            if (probeTypes.Length > 0)
            {
                cboProbeType.DataSource = probeTypes;
                cboProbeType.DisplayMember = "ProbeTypeName";
                cboProbeType.ValueMember = "ProbeTypeId";

                cboProbeType.SelectedIndex = -1;
            }
        }

        /// <summary>
        ///     验证画面录入数据
        /// </summary>
        /// <returns>验证结果：true 通过验证, false未通过验证</returns>
        private bool check()
        {
            // 判断探头编号是否录入
            if (!Check.isEmpty(txtProbeId, Const_GE.PROBE_ID))
            {
                return false;
            }

            // 判断探头编号是否包含特殊字符判断
            if (!Check.checkSpecialCharacters(txtProbeId, Const_GE.PROBE_ID))
            {
                return false;
            }

            // 只有在添加窗口才做探头编号是否存在的判断
            if (_bllType == "add")
            {
                // 判断探头编号是否存在
                if (!Check.isExist(txtProbeId, Const_GE.PROBE_ID,
                    Probe.Exists(txtProbeId.Text.Trim())))
                {
                    return false;
                }
            }

            // 判断探头名称是否录入
            if (!Check.isEmpty(txtProbeName, Const_GE.PROBE_NAME))
            {
                return false;
            }

            // 判断探头名称是否包含特殊字符判断
            if (!Check.checkSpecialCharacters(txtProbeName, Const_GE.PROBE_NAME))
            {
                return false;
            }

            // 判断探头类型是否选择
            if (!Check.isEmpty(cboProbeType, Const_GE.PROBE_TYPE))
            {
                return false;
            }

            // 判断所属是否选择
            if (selectTunnelUserControl1.ITunnelId == Const.INVALID_ID)
            {
                Alert.alert(Const_GE.TUNNEL_NAME_MUST_INPUT);
                return false;
            }

            // *********************根据侯哥修改意见2014.03.11 探头坐标X改为非必须录入******************
            //// 判断坐标X是否录入
            //if (!Check.isEmpty(this.txtProbeLocationX, Const_GE.PROBE_LOCATION_X))
            //{
            //    return false;
            //}
            // **************************************************************************************

            // 判断坐标X是否为数字
            if (!Check.IsNumeric(txtProbeLocationX, Const_GE.PROBE_LOCATION_X))
            {
                return false;
            }

            // *********************根据侯哥修改意见2014.03.11 探头坐标Y改为非必须录入******************
            //// 判断坐标Y是否录入
            //if (!Check.isEmpty(this.txtProbeLocationY, Const_GE.PROBE_LOCATION_Y))
            //{
            //    return false;
            //}
            // **************************************************************************************

            // 判断坐标Y是否为数字
            if (!Check.IsNumeric(txtProbeLocationY, Const_GE.PROBE_LOCATION_Y))
            {
                return false;
            }

            // *********************根据侯哥修改意见2014.02.25 探头坐标Z改为非必须录入*********************
            //// 判断坐标Z是否录入
            //if (!Check.isEmpty(this.txtProbeLocationZ, Const_GE.PROBE_LOCATION_Z))
            //{
            //    return false;
            //}
            // **************************************************************************************

            // 判断坐标Z是否为数字
            if (!Check.IsNumeric(txtProbeLocationZ, Const_GE.PROBE_LOCATION_Z))
            {
                return false;
            }

            // 验证通过
            return true;
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

            // 创建探头管理实体
            var probeEntity = new Probe();

            // 探头编号
            probeEntity.ProbeId = txtProbeId.Text.Trim();

            // 探头名称
            probeEntity.ProbeName = txtProbeName.Text.Trim();

            // 探头类型编号
            int iProbeTypeId = 0;
            if (int.TryParse(Convert.ToString(cboProbeType.SelectedValue), out iProbeTypeId))
            {
                probeEntity.ProbeType.ProbeTypeId = iProbeTypeId;
            }

            // 巷道编号
            probeEntity.Tunnel.TunnelId = selectTunnelUserControl1.ITunnelId;

            // 探头位置坐标X
            double dProbeLocationX = 0;
            if (double.TryParse(txtProbeLocationX.Text.Trim(), out dProbeLocationX))
            {
                probeEntity.ProbeLocationX = dProbeLocationX;
            }

            // 探头位置坐标Y
            double dProbeLocationY = 0;
            if (double.TryParse(txtProbeLocationY.Text.Trim(), out dProbeLocationY))
            {
                probeEntity.ProbeLocationY = dProbeLocationY;
            }

            // 探头位置坐标Z
            double dProbeLocationZ = 0;
            if (double.TryParse(txtProbeLocationZ.Text.Trim(), out dProbeLocationZ))
            {
                probeEntity.ProbeLocationZ = dProbeLocationZ;
            }

            // 探头描述
            probeEntity.ProbeDescription = txtProbeDescription.Text.Trim();
            OPERATION_TYPE opType;

            bool bResult = false;
            if (_bllType == "add")
            {
                // 探头管理信息登录
                probeEntity.SaveAndFlush();
                bResult = true;
                opType = OPERATION_TYPE.ADD;
            }
            else
            {
                // 主键
                probeEntity.ProbeId = _iPK;
                // 探头管理信息修改
                probeEntity.SaveAndFlush();
                bResult = true;
                opType = OPERATION_TYPE.UPDATE;
            }

            // 添加/修改成功的场合
            if (bResult)
            {
                //通知服务器探头数据已更新
                WorkingFace workingfaceEnt =
                    BasicInfoManager.getInstance().getTunnelByID(probeEntity.Tunnel.TunnelId).WorkingFace;

                if (oldTunnelId != 0)
                {
                    var msgUpdate = new UpdateWarningDataMsg(workingfaceEnt.WorkingFaceID, oldTunnelId,
                        Probe.TableName, opType, DateTime.Now);
                    MainForm.SendMsg2Server(msgUpdate);
                }

                var msg = new UpdateWarningDataMsg(workingfaceEnt.WorkingFaceID,
                    probeEntity.Tunnel.TunnelId,
                    Probe.TableName, opType, DateTime.Now);
                MainForm.SendMsg2Server(msg);
            }

            // *********************根据小杨总修改意见2014.03.15  成功失败MSG都不要*********************
            //
            //if (bResult)
            //{
            //    Alert.alert(Const.SUCCESS_MSG);
            //    this.Close();
            //}
            //else
            //{
            //    Alert.alert(Const.FAILURE_MSG);
            //}
            // **************************************************************************************
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
        private void setProbeInfo()
        {
            // 探头信息
            Probe probe = Probe.Find(_iPK);
            if (probe != null)
            {
                // 探头编号
                txtProbeId.Text = probe.ProbeId;
                // 探头编号不可修改
                txtProbeId.Enabled = false;

                // 探头名称
                txtProbeName.Text = probe.ProbeName;

                // 探头类型
                cboProbeType.SelectedValue = probe.ProbeType != null ? probe.ProbeType.ProbeTypeId : 0;

                // X坐标
                txtProbeLocationX.Text = probe.ProbeLocationX.ToString();

                // Y坐标
                txtProbeLocationY.Text = probe.ProbeLocationY.ToString();

                // Z坐标
                txtProbeLocationZ.Text = probe.ProbeLocationZ.ToString();

                // 探头描述
                txtProbeDescription.Text = probe.ProbeDescription;

                // 所在巷道绑定
                if (probe.Tunnel != null)
                {
                    var intArr = new int[5];
                    intArr[0] = probe.Tunnel.WorkingFace.MiningArea.Horizontal.Mine.MineId;
                    intArr[1] = probe.Tunnel.WorkingFace.MiningArea.Horizontal.HorizontalId;
                    intArr[2] = probe.Tunnel.WorkingFace.MiningArea.MiningAreaId;
                    intArr[3] = probe.Tunnel.WorkingFace.WorkingFaceID;
                    intArr[4] = probe.Tunnel.TunnelId;
                    _intArr = intArr;
                }
            }
        }
    }
}