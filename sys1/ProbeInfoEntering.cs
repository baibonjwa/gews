// ******************************************************************
// 概  述：探头管理数据录入
// 作  者：伍鑫
// 创建日期：2013/12/01
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
using LibCommonForm;
using LibEntity;
using LibBusiness;
using LibCommon;
using LibSocket;
using LibDatabase;
using LibCommonControl;

namespace _1.GasEmission
{
    public partial class ProbeInfoEntering : BaseForm
    {
        /** 主键  **/
        private string _iPK;
        /** 业务逻辑类型：添加、修改  **/
        private string _bllType = "add";
        /** 存放矿井名称，水平，采区，工作面，巷道编号的数组  **/
        private int[] _intArr = new int[5];

        private int oldTunnelId;

        /// <summary>
        /// 构造方法
        /// </summary>
        public ProbeInfoEntering(MainFrm mainFrm)
        {
            this.MainForm = mainFrm;
            InitializeComponent();

            // 设置窗体默认属性
            FormDefaultPropertiesSetter.SetEnteringFormDefaultProperties(this, Const_GE.INSERT_PROBE_INFO);

            // 调用选择巷道控件时需要调用的方法
            //this.selectTunnelSimple1.loadMineName();
        }

        /// <summary>
        /// 带参数的构造方法
        /// </summary>
        /// <param name="strPrimaryKey">主键</param>
        public ProbeInfoEntering(string strPrimaryKey, MainFrm mainFrm)
        {
            this.MainForm = mainFrm;
            InitializeComponent();

            this._iPK = strPrimaryKey;

            // 设置窗体默认属性
            FormDefaultPropertiesSetter.SetEnteringFormDefaultProperties(this, Const_GE.UPDATE_PROBE_INFO);

            // 设置业务类型
            this._bllType = "update";

            // 调用选择巷道控件时需要调用的方法
            //this.selectTunnelUserControl1.setCurSelectedID(_intArr);
        }


        private void selectTunnelSimple1_Load(object sender, EventArgs e)
        {
            // 加载探头类型信息
            loadProbeTypeInfo();

            // 设置探头信息
            if (_bllType == "update")
                this.setProbeInfo();
        }

        /// <summary>
        /// 加载探头类型信息
        /// </summary>
        private void loadProbeTypeInfo()
        {
            DataSet ds = ProbeTypeBLL.selectAllProbeTypeInfo();
            if (ds.Tables[0].Rows.Count > 0)
            {
                this.cboProbeType.DataSource = ds.Tables[0];
                this.cboProbeType.DisplayMember = ProbeTypeDbConstNames.PROBE_TYPE_NAME;
                this.cboProbeType.ValueMember = ProbeTypeDbConstNames.PROBE_TYPE_ID;

                this.cboProbeType.SelectedIndex = -1;
            }
        }

        /// <summary>
        /// 验证画面录入数据
        /// </summary>
        /// <returns>验证结果：true 通过验证, false未通过验证</returns>
        private bool check()
        {
            // 判断探头编号是否录入
            if (!Check.isEmpty(this.txtProbeId, Const_GE.PROBE_ID))
            {
                return false;
            }

            // 判断探头编号是否包含特殊字符判断
            if (!Check.checkSpecialCharacters(this.txtProbeId, Const_GE.PROBE_ID))
            {
                return false;
            }

            // 只有在添加窗口才做探头编号是否存在的判断
            if (this._bllType == "add")
            {
                // 判断探头编号是否存在
                if (!Check.isExist(this.txtProbeId, Const_GE.PROBE_ID,
                    ProbeManageBLL.isProbeIdExist(this.txtProbeId.Text.Trim())))
                {
                    return false;
                }
            }

            // 判断探头名称是否录入
            if (!Check.isEmpty(this.cmbProbeName, Const_GE.PROBE_NAME))
            {
                return false;
            }

            // 判断探头名称是否包含特殊字符判断
            if (!Check.checkSpecialCharacters(this.cmbProbeName, Const_GE.PROBE_NAME))
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
            if (!Check.isEmpty(this.cboProbeType, Const_GE.PROBE_TYPE))
            {
                return false;
            }

            // 是否自动位移如果选择是的话
            if (this.rbtnYes.Checked == true)
            {
                //// 判断距迎头距离是否录入
                //if (this.txtM.Text.Trim() == "")
                //{
                //    Alert.alert(Const_GE.FAR_FROM_FRONTAL_MUST_INPUT);
                //    return false;
                //}

                // 判断距迎头距离是否录入
                if (!Check.isEmpty(this.txtM, Const_GE.FAR_FROM_FRONTAL))
                {
                    return false;
                }

                // 判断距迎头距离是否为数字
                if (!Check.IsNumeric(this.txtM, Const_GE.FAR_FROM_FRONTAL))
                {
                    return false;
                }
            }

            // 判断所属是否选择
            if (this.selectTunnelSimple1.ITunnelId == Const.INVALID_ID)
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
            if (!Check.IsNumeric(this.txtProbeLocationX, Const_GE.PROBE_LOCATION_X))
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
            if (!Check.IsNumeric(this.txtProbeLocationY, Const_GE.PROBE_LOCATION_Y))
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
            if (!Check.IsNumeric(this.txtProbeLocationZ, Const_GE.PROBE_LOCATION_Z))
            {
                return false;
            }

            // 探头类型编号
            int iProbeTypeId = 0;
            int.TryParse(Convert.ToString(this.cboProbeType.SelectedValue), out iProbeTypeId);
            DataSet ds = doesProbeExist(iProbeTypeId,
                this.cmbProbeName.SelectedItem.ToString(),
                this.selectTunnelSimple1.ITunnelId);
            if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {

                if (Alert.confirm("该巷道下已经存在" + this.cboProbeType.Text + "探头，确认提交将重新设定" + this.cboProbeType.Text + "探头。"))
                {
                    // 重置探头类型
                    ProbeManageBLL.resetProbeTypeInOneTunnel(ds.Tables[0].Rows[0][ProbeManageDbConstNames.PROBE_ID].ToString());

                    return true;
                }
                else
                {
                    return false;
                }

            }

            // 验证通过
            return true;
        }

        /// <summary>
        /// 获取指定【巷道】下，指定【探头类型】的探头信息
        /// </summary>
        /// <param name="iProbeTypeId">【探头类型】</param>
        /// <param name="sProbeName">【探头名称】</param>
        /// <param name="iTunnelId">【巷道】</param>
        /// <returns>探头信息</returns>
        public static DataSet doesProbeExist(int iProbeTypeId, string sProbeName, int iTunnelId)
        {
            StringBuilder sqlStr = new StringBuilder();
            sqlStr.Append("SELECT * FROM " + ProbeManageDbConstNames.TABLE_NAME);
            sqlStr.Append(" WHERE " + ProbeManageDbConstNames.PROBE_TYPE_ID + " = " + iProbeTypeId);
            sqlStr.Append(" AND ");
            sqlStr.Append(ProbeManageDbConstNames.TUNNEL_ID + " = " + iTunnelId);
            sqlStr.Append(" AND ");
            sqlStr.Append(ProbeManageDbConstNames.PROBE_NAME + " = '" + sProbeName + "'");

            ManageDataBase db = new ManageDataBase(DATABASE_TYPE.GasEmissionDB);
            DataSet ds = db.ReturnDS(sqlStr.ToString());

            return ds;
        }

        /// <summary>
        /// 提交
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnSubmit_Click(object sender, EventArgs e)
        {
            // 验证
            if (!this.check())
            {
                DialogResult = DialogResult.None;
                return;
            }
            DialogResult = DialogResult.OK;

            // 创建探头管理实体
            Probe probeEntity = new Probe();

            // 探头编号
            probeEntity.ProbeId = this.txtProbeId.Text.Trim();

            // 探头名称
            probeEntity.ProbeName = this.cmbProbeName.Text.Trim();

            // 探头类型编号
            int iProbeTypeId = 0;
            if (int.TryParse(Convert.ToString(this.cboProbeType.SelectedValue), out iProbeTypeId))
            {
                probeEntity.ProbeType.ProbeTypeId = iProbeTypeId;
            }

            // 2014/5/29 add by wuxin Start
            // 探头类型名
            probeEntity.ProbeTypeDisplayName = this.cboProbeType.Text;
            // 2014/5/29 add by wuxin End

            // 是否自动位移如果选择是的话
            if (this.rbtnYes.Checked == true)
            {
                // 是否自动位移
                probeEntity.IsMove = 1;
                // 距迎头距离
                double dFarFromFrontal = 0;
                double.TryParse(this.txtM.Text.Trim(), out dFarFromFrontal);
                probeEntity.FarFromFrontal = dFarFromFrontal;
            }
            else
            {   // 是否自动位移
                probeEntity.IsMove = 0;
            }

            // 巷道编号
            probeEntity.Tunnel.TunnelId = this.selectTunnelSimple1.ITunnelId;

            // 探头位置坐标X
            double dProbeLocationX = 0;
            if (double.TryParse(this.txtProbeLocationX.Text.Trim(), out dProbeLocationX))
            {
                probeEntity.ProbeLocationX = dProbeLocationX;
            }

            // 探头位置坐标Y
            double dProbeLocationY = 0;
            if (double.TryParse(this.txtProbeLocationY.Text.Trim(), out dProbeLocationY))
            {
                probeEntity.ProbeLocationY = dProbeLocationY;
            }

            // 探头位置坐标Z
            double dProbeLocationZ = 0;
            if (double.TryParse(this.txtProbeLocationZ.Text.Trim(), out dProbeLocationZ))
            {
                probeEntity.ProbeLocationZ = dProbeLocationZ;
            }

            // 探头描述
            probeEntity.ProbeDescription = this.txtProbeDescription.Text.Trim();
            OPERATION_TYPE opType;

            bool bResult = false;
            if (this._bllType == "add")
            {
                // 探头管理信息登录
                bResult = ProbeManageBLL.insertProbeManageInfo(probeEntity);
                opType = OPERATION_TYPE.ADD;
            }
            else
            {
                // 主键
                probeEntity.ProbeId = this._iPK;
                // 探头管理信息修改
                bResult = ProbeManageBLL.updateProbeManageInfo(probeEntity);
                opType = OPERATION_TYPE.UPDATE;
            }

            // 添加/修改成功的场合
            if (bResult)
            {
                // TODO:具体实装代码待定。
                //通知服务器探头数据已更新
                // Added by jhou, 2014/3/24
                var workingfaceEnt =
                    BasicInfoManager.getInstance().getTunnelByID(probeEntity.Tunnel.TunnelId).WorkingFace;

                if (oldTunnelId != 0)
                {
                    UpdateWarningDataMsg msgUpdate = new UpdateWarningDataMsg(workingfaceEnt.WorkingFaceID, probeEntity.Tunnel.TunnelId,
                        ProbeManageDbConstNames.TABLE_NAME, opType, DateTime.Now);
                    this.MainForm.SendMsg2Server(msgUpdate);
                }

                UpdateWarningDataMsg msg = new UpdateWarningDataMsg(workingfaceEnt.WorkingFaceID, probeEntity.Tunnel.TunnelId,
                    ProbeManageDbConstNames.TABLE_NAME, opType, DateTime.Now);
                this.MainForm.SendMsg2Server(msg);
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
        /// 取消
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnCancel_Click(object sender, EventArgs e)
        {
            // 关闭窗口
            this.Close();
        }

        /// <summary>
        /// 设置探头信息
        /// </summary>
        private void setProbeInfo()
        {
            // 探头信息
            DataSet dsProbeInfo = ProbeManageBLL.selectProbeManageInfoByProbeId(this._iPK);
            if (dsProbeInfo.Tables[0].Rows.Count > 0)
            {
                // 探头编号
                this.txtProbeId.Text = dsProbeInfo.Tables[0].Rows[0][ProbeManageDbConstNames.PROBE_ID].ToString();
                // 探头编号不可修改
                this.txtProbeId.Enabled = false;

                // 探头名称
                this.cmbProbeName.Text = dsProbeInfo.Tables[0].Rows[0][ProbeManageDbConstNames.PROBE_NAME].ToString();

                // 探头类型
                int iProbeTypeId = 0;
                if (int.TryParse(dsProbeInfo.Tables[0].Rows[0][ProbeManageDbConstNames.PROBE_TYPE_ID].ToString(), out iProbeTypeId))
                {
                    this.cboProbeType.SelectedValue = iProbeTypeId;
                }

                // 是否自动位移
                if (dsProbeInfo.Tables[0].Rows[0][ProbeManageDbConstNames.IS_MOVE].ToString() == "1")
                {
                    this.rbtnYes.Checked = true;

                    // 距迎头距离
                    this.txtM.Text = dsProbeInfo.Tables[0].Rows[0][ProbeManageDbConstNames.FAR_FROM_FRONTAL].ToString();
                    this.txtM.Enabled = true;
                }
                else
                {
                    this.rbtnNo.Checked = true;
                }

                // X坐标
                string strProbeLocationX = dsProbeInfo.Tables[0].Rows[0][ProbeManageDbConstNames.PROBE_LOCATION_X].ToString();
                this.txtProbeLocationX.Text = (strProbeLocationX == Const.DOUBLE_DEFAULT_VALUE ? "" : strProbeLocationX);

                // Y坐标
                string strProbeLocationY = dsProbeInfo.Tables[0].Rows[0][ProbeManageDbConstNames.PROBE_LOCATION_Y].ToString();
                this.txtProbeLocationY.Text = (strProbeLocationY == Const.DOUBLE_DEFAULT_VALUE ? "" : strProbeLocationY);

                // Z坐标
                string strProbeLocationZ = dsProbeInfo.Tables[0].Rows[0][ProbeManageDbConstNames.PROBE_LOCATION_Z].ToString();
                this.txtProbeLocationZ.Text = (strProbeLocationZ == Const.DOUBLE_DEFAULT_VALUE ? "" : strProbeLocationZ);

                // 探头描述
                this.txtProbeDescription.Text = dsProbeInfo.Tables[0].Rows[0][ProbeManageDbConstNames.PROBE_DESCRIPTION].ToString();

                // 所在巷道绑定
                int iTunnelID = 0;
                string id = dsProbeInfo.Tables[0].Rows[0][ProbeManageDbConstNames.TUNNEL_ID].ToString();
                if (int.TryParse(dsProbeInfo.Tables[0].Rows[0][ProbeManageDbConstNames.TUNNEL_ID].ToString(), out iTunnelID))
                {
                    Tunnel tunnelEntity = BasicInfoManager.getInstance().getTunnelByID(iTunnelID);
                    if (tunnelEntity != null)
                    {
                        oldTunnelId = tunnelEntity.TunnelId;
                        //if(
                        TunnelSimple ts = new TunnelSimple(tunnelEntity.TunnelId, tunnelEntity.TunnelName);
                        selectTunnelSimple1.SelectTunnelItemWithoutHistory(ts);
                    }
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
        /// 是否自动位移Click事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void rbtnYes_Click(object sender, EventArgs e)
        {
            if (this.rbtnYes.Checked == true)
            {
                // 距迎头距离设为可用
                this.txtM.Enabled = true;
            }
        }

        /// <summary>
        /// 是否自动位移Click事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void rbtnNo_Click(object sender, EventArgs e)
        {
            if (this.rbtnNo.Checked == true)
            {
                // 距迎头距离设为不可用
                this.txtM.Enabled = false;
            }
        }

    }
}
