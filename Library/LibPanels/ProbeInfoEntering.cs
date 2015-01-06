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

            // 调用选择巷道控件时需要调用的方法
            //this.selectTunnelSimple1.loadMineName();
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

            // 调用选择巷道控件时需要调用的方法
            //this.selectTunnelUserControl1.setCurSelectedID(_intArr);
        }


        private void selectTunnelSimple1_Load(object sender, EventArgs e)
        {
            // 加载探头类型信息
            loadProbeTypeInfo();

            // 设置探头信息
            if (_bllType == "update")
                setProbeInfo();
        }

        /// <summary>
        ///     加载探头类型信息
        /// </summary>
        private void loadProbeTypeInfo()
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
            if (!Check.isEmpty(cmbProbeName, Const_GE.PROBE_NAME))
            {
                return false;
            }

            // 判断探头名称是否包含特殊字符判断
            if (!Check.checkSpecialCharacters(cmbProbeName, Const_GE.PROBE_NAME))
            {
                return false;
            }

            // TODO:探头名称是否重新不做判断,下面代码暂不删除。
            //// 数据录入
            //if (this._bllType == "add")
            //{
            //    // 判断探头名称是否存在
            //    if (!Check.isExist(this.txtProbeName, Const_GE.PROBE_NAME,
            //        ProbeManageBLL.isProbeNameExist(this.txtProbeName.Text.Trim())))
            //    {
            //        return false;
            //    }
            //}
            //// 数据修改
            //else
            //{
            //    /* 修改的时候，首先要获取UI输入的探头名称到DB中去检索，
            //    如果检索件数 > 0 并且该探头ID还不是传过来的主键，那么视为输入了已存在的探头名称 */
            //    DataSet ds = ProbeManageBLL.selectProbeManageInfoByProbeName(this.txtProbeName.Text.Trim());
            //    if (ds.Tables[0].Rows.Count > 0 && !ds.Tables[0].Rows[0][ProbeManageDbConstNames.PROBE_ID].ToString().Equals(_iPK.ToString()))
            //    {
            //        this.txtProbeName.BackColor = Const.ERROR_FIELD_COLOR;
            //        Alert.alert(Const_GE.PROBE_NAME_EXIST_MSG); // 探头名称已存在，请重新录入！
            //        this.txtProbeName.Focus();
            //        return false;
            //    }

            //}

            // 判断探头类型是否选择
            if (!Check.isEmpty(cboProbeType, Const_GE.PROBE_TYPE))
            {
                return false;
            }

            // 是否自动位移如果选择是的话
            if (rbtnYes.Checked)
            {
                //// 判断距迎头距离是否录入
                //if (this.txtM.Text.Trim() == "")
                //{
                //    Alert.alert(Const_GE.FAR_FROM_FRONTAL_MUST_INPUT);
                //    return false;
                //}

                // 判断距迎头距离是否录入
                if (!Check.isEmpty(txtM, Const_GE.FAR_FROM_FRONTAL))
                {
                    return false;
                }

                // 判断距迎头距离是否为数字
                if (!Check.IsNumeric(txtM, Const_GE.FAR_FROM_FRONTAL))
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

            // 探头类型编号
            int iProbeTypeId = 0;
            int.TryParse(Convert.ToString(cboProbeType.SelectedValue), out iProbeTypeId);
            Probe probe = Probe.FindFirstByProbeTypeIdAndProbeNameAndTunnelId(iProbeTypeId,
                cmbProbeName.SelectedItem.ToString(),
                selectTunnelSimple1.ITunnelId);
            if (probe != null)
            {
                if (Alert.confirm("该巷道下已经存在" + cboProbeType.Text + "探头，确认提交将重新设定" + cboProbeType.Text + "探头。"))
                {
                    // 重置探头类型
                    probe.ProbeType = null;
                    probe.SaveAndFlush();
                    return true;
                }
                return false;
            }

            // 验证通过
            return true;
        }

        //public static DataSet doesProbeExist(int iProbeTypeId, string sProbeName, int iTunnelId)
        //{
        //    StringBuilder sqlStr = new StringBuilder();
        //    sqlStr.Append("SELECT * FROM " + ProbeManageDbConstNames.TABLE_NAME);
        //    sqlStr.Append(" WHERE " + ProbeManageDbConstNames.PROBE_TYPE_ID + " = " + iProbeTypeId);
        //    sqlStr.Append(" AND ");
        //    sqlStr.Append(ProbeManageDbConstNames.TUNNEL_ID + " = " + iTunnelId);
        //    sqlStr.Append(" AND ");
        //    sqlStr.Append(ProbeManageDbConstNames.PROBE_NAME + " = '" + sProbeName + "'");

        //    ManageDataBase db = new ManageDataBase(DATABASE_TYPE.GasEmissionDB);
        //    DataSet ds = db.ReturnDS(sqlStr.ToString());

        //    return ds;
        //}
        /// <summary>
        ///     获取指定【巷道】下，指定【探头类型】的探头信息
        /// </summary>
        /// <param name="iProbeTypeId">【探头类型】</param>
        /// <param name="sProbeName">【探头名称】</param>
        /// <param name="iTunnelId">【巷道】</param>
        /// <returns>探头信息</returns>
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
            probeEntity.ProbeName = cmbProbeName.Text.Trim();

            // 探头类型编号
            int iProbeTypeId = 0;
            if (int.TryParse(Convert.ToString(cboProbeType.SelectedValue), out iProbeTypeId))
            {
                probeEntity.ProbeType.ProbeTypeId = iProbeTypeId;
            }

            // 2014/5/29 add by wuxin Start
            // 探头类型名
            probeEntity.ProbeTypeDisplayName = cboProbeType.Text;
            // 2014/5/29 add by wuxin End

            // 是否自动位移如果选择是的话
            if (rbtnYes.Checked)
            {
                // 是否自动位移
                probeEntity.IsMove = 1;
                // 距迎头距离
                double dFarFromFrontal = 0;
                double.TryParse(txtM.Text.Trim(), out dFarFromFrontal);
                probeEntity.FarFromFrontal = dFarFromFrontal;
            }
            else
            {
                // 是否自动位移
                probeEntity.IsMove = 0;
            }

            // 巷道编号
            probeEntity.Tunnel.TunnelId = selectTunnelSimple1.ITunnelId;

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
                // TODO:具体实装代码待定。
                //通知服务器探头数据已更新
                // Added by jhou, 2014/3/24
                WorkingFace workingfaceEnt =
                    BasicInfoManager.getInstance().getTunnelByID(probeEntity.Tunnel.TunnelId).WorkingFace;

                if (oldTunnelId != 0)
                {
                    var msgUpdate = new UpdateWarningDataMsg(workingfaceEnt.WorkingFaceID,
                        probeEntity.Tunnel.TunnelId,
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
                cmbProbeName.Text = probe.ProbeName;

                // 探头类型
                cboProbeType.SelectedValue = probe.ProbeType != null ? probe.ProbeType.ProbeTypeId : 0;

                // 是否自动位移
                if (probe.IsMove == 1)
                {
                    rbtnYes.Checked = true;

                    // 距迎头距离
                    txtM.Text = probe.FarFromFrontal.ToString();
                    txtM.Enabled = true;
                }
                else
                {
                    rbtnNo.Checked = true;
                }

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
                    //if(
                    var ts = new TunnelSimple(probe.Tunnel.TunnelId, probe.Tunnel.TunnelName);
                    selectTunnelSimple1.SelectTunnelItemWithoutHistory(ts);

                    //if (tunnelEntity != null)
                    //{
                    //    int[] intArr = new int[5];
                    //    intArr[0] = tunnelEntity.WorkingFace.MiningArea.Horizontal.Mine.MineId;// tunnelEntity.MineID;
                    //    intArr[1] = tunnelEntity.WorkingFace.MiningArea.Horizontal.HorizontalId; //tunnelEntity.HorizontalID;
                    //    intArr[2] = tunnelEntity.WorkingFace.MiningArea.MiningAreaId;
                    //    intArr[3] = tunnelEntity.WorkingFace.WorkingFaceID; //tunnelEntity.WorkingFaceID;
                    //    intArr[4] = tunnelEntity.Tunnel;
                    //    _intArr = intArr;
                    //}
                }
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