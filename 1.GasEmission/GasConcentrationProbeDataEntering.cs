// ******************************************************************
// 概  述：传感器数据录入
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
using LibBusiness;
using LibCommon;
using LibEntity;
using LibCommonControl;
using LibCommonForm;
using LibSocket;

namespace _1.GasEmission
{
    public partial class GasConcentrationProbeDataEntering : BaseForm
    {
        /** 主键  **/
        private string _iPK;
        /** 业务逻辑类型：添加、修改  **/
        private string _bllType = "add";

        /// <summary>
        /// 构造方法
        /// </summary>
        public GasConcentrationProbeDataEntering(MainFrm mainFrm)
        {
            this.MainForm = mainFrm;

            InitializeComponent();

            //分配权限
            if (CurrentUserEnt._curLoginUserInfo.Permission != Permission.管理员.ToString())
            {
                selectTunnelUserControl1.SetButtonEnable(false);
            }

            // 设置窗体默认属性
            FormDefaultPropertiesSetter.SetEnteringFormDefaultProperties(this, Const_GE.INSERT_GAS_CONCENTRATION_PROBE_DATA);

            // 设置日期控件格式
            this.dtpRecordTime.Format = DateTimePickerFormat.Custom;
            this.dtpRecordTime.CustomFormat = Const.DATE_FORMART_YYYY_MM_DD;

            // 调用选择巷道控件时需要调用的方法
            this.selectTunnelUserControl1.loadMineName();

            // 注册委托事件
            this.selectTunnelUserControl1.TunnelNameChanged +=
                InheritTunnelNameChanged;
        }

        /// <summary>
        /// 带参数的构造方法
        /// </summary>
        /// <param name="intArr"></param>
        public GasConcentrationProbeDataEntering(object[] objArr, MainFrm mainFrm)
        {
            this.MainForm = mainFrm;

            InitializeComponent();

            // 设置窗体默认属性
            FormDefaultPropertiesSetter.SetEnteringFormDefaultProperties(this, Const_GE.INSERT_GAS_CONCENTRATION_PROBE_DATA);

            // 设置日期控件格式
            this.dtpRecordTime.Format = DateTimePickerFormat.Custom;
            this.dtpRecordTime.CustomFormat = Const.DATE_FORMART_YYYY_MM_DD;

            // 提取数组前五位，用来绑定巷道信息，后两位是用来绑定探头类型及探头名称信息
            int[] intArr = new int[5];
            // 矿井编号
            int iMineId = 0;
            if (int.TryParse(Convert.ToString(objArr[0]), out iMineId))
            {
                intArr[0] = iMineId;
            }
            // 水平编号
            int iHorizontal = 0;
            if (int.TryParse(Convert.ToString(objArr[1]), out iHorizontal))
            {
                intArr[1] = iHorizontal;
            }
            // 采区编号
            int iMiningArea = 0;
            if (int.TryParse(Convert.ToString(objArr[2]), out iMiningArea))
            {
                intArr[2] = iMiningArea;
            }
            // 工作面编号
            int iWorkingFace = 0;
            if (int.TryParse(Convert.ToString(objArr[3]), out iWorkingFace))
            {
                intArr[3] = iWorkingFace;
            }
            // 巷道编号
            int iTunnel = 0;
            if (int.TryParse(Convert.ToString(objArr[4]), out iTunnel))
            {
                intArr[4] = iTunnel;
            }

            // 巷道编号不等于0的场合（即巷道已选择）
            if (iTunnel != 0)
            {
                // 加载探头类型
                loadProbeTypeInfo();

                // 探头类型编号不为
                if (objArr[5] != null)
                {
                    int iProbeTypeId = 0;
                    if (int.TryParse(Convert.ToString(objArr[5]), out iProbeTypeId))
                    {
                        this._lstProbeStyle.SelectedValue = iProbeTypeId;
                    }
                }

                // 探头编号
                if (objArr[6] != null)
                {
                    // 加载探头名称
                    loadProbeName(iTunnel);
                    this._lstProbeName.SelectedValue = Convert.ToString(objArr[6]);
                }
            }

            // 调用选择巷道控件时需要调用的方法
            this.selectTunnelUserControl1.setCurSelectedID(intArr);

            // 注册委托事件
            this.selectTunnelUserControl1.TunnelNameChanged +=
                InheritTunnelNameChanged;

        }

        /// <summary>
        /// 带参数的构造方法
        /// </summary>
        /// <param name="intArr"></param>
        public GasConcentrationProbeDataEntering(string strPrimaryKey, object[] objArr, MainFrm mainFrm)
        {
            this.MainForm = mainFrm;
            InitializeComponent();

            this._iPK = strPrimaryKey;

            // 设置业务类型
            this._bllType = "update";

            // 设置窗体默认属性
            FormDefaultPropertiesSetter.SetEnteringFormDefaultProperties(this, Const_GE.UPDATE_GAS_CONCENTRATION_PROBE_DATA);

            // 设置日期控件格式
            this.dtpRecordTime.Format = DateTimePickerFormat.Custom;
            this.dtpRecordTime.CustomFormat = Const.DATE_FORMART_YYYY_MM_DD;

            // 提取数组前五位，用来绑定巷道信息，后两位是用来绑定探头类型及探头名称信息
            int[] intArr = new int[5];
            // 矿井编号
            int iMineId = 0;
            if (int.TryParse(Convert.ToString(objArr[0]), out iMineId))
            {
                intArr[0] = iMineId;
            }
            // 水平编号
            int iHorizontal = 0;
            if (int.TryParse(Convert.ToString(objArr[1]), out iHorizontal))
            {
                intArr[1] = iHorizontal;
            }
            // 采区编号
            int iMiningArea = 0;
            if (int.TryParse(Convert.ToString(objArr[2]), out iMiningArea))
            {
                intArr[2] = iMiningArea;
            }
            // 工作面编号
            int iWorkingFace = 0;
            if (int.TryParse(Convert.ToString(objArr[3]), out iWorkingFace))
            {
                intArr[3] = iWorkingFace;
            }
            // 巷道编号
            int iTunnel = 0;
            if (int.TryParse(Convert.ToString(objArr[4]), out iTunnel))
            {
                intArr[4] = iTunnel;
            }

            // 巷道编号不等于0的场合（即巷道已选择）
            if (iTunnel != 0)
            {
                // 加载探头类型
                loadProbeTypeInfo();

                // 探头类型编号不为
                if (objArr[5] != null)
                {
                    int iProbeTypeId = 0;
                    if (int.TryParse(Convert.ToString(objArr[5]), out iProbeTypeId))
                    {
                        this._lstProbeStyle.SelectedValue = iProbeTypeId;
                    }
                }

                // 探头编号
                if (objArr[6] != null)
                {
                    // 加载探头名称
                    loadProbeName(iTunnel);
                    this._lstProbeName.SelectedValue = Convert.ToString(objArr[6]);
                }
            }

            // 调用选择巷道控件时需要调用的方法
            this.selectTunnelUserControl1.setCurSelectedID(intArr);

            // 注册委托事件
            this.selectTunnelUserControl1.TunnelNameChanged +=
                InheritTunnelNameChanged;

            // 设置瓦斯浓度探头数据
            setGasConcentrationProbeData(strPrimaryKey);
        }

        /// <summary>
        /// 设置瓦斯浓度探头数据
        /// </summary>
        public void setGasConcentrationProbeData(string strPrimaryKey)
        {
            if (!Validator.IsEmpty(strPrimaryKey))
            {
                DataSet ds = GasConcentrationProbeDataBLL.selectProbeDataByProbeDataId(Convert.ToInt32(strPrimaryKey));

                if (ds.Tables[0].Rows.Count > 0)
                {
                    // 数值
                    this.txtProbeValue.Text = ds.Tables[0].Rows[0][GasConcentrationProbeDataDbConstNames.PROBE_VALUE].ToString();
                    // 时间
                    this.dtpRecordTime.Text = ds.Tables[0].Rows[0][GasConcentrationProbeDataDbConstNames.RECORD_TIME].ToString();
                }
            }

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
        /// 加载探头名称
        /// </summary>
        /// <param name="iTunnelId"></param>
        private void loadProbeName(int iTunnelId)
        {
            this._lstProbeName.DataSource = null;

            // 根据巷道编号和探头类型编号获取探头信息
            DataSet ds = ProbeManageBLL.selectProbeManageInfoByTunnelIDAndProbeType(iTunnelId, Convert.ToInt32(this._lstProbeStyle.SelectedValue));

            if (ds.Tables[0].Rows.Count > 0)
            {
                this._lstProbeName.DataSource = ds.Tables[0];
                this._lstProbeName.DisplayMember = ProbeManageDbConstNames.PROBE_NAME;
                this._lstProbeName.ValueMember = ProbeManageDbConstNames.PROBE_ID;

                this._lstProbeName.SelectedIndex = -1;
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
        /// 提交
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnSubmit_Click(object sender, EventArgs e)
        {
            // 验证
            if (!this.check())
            {
                this.DialogResult = DialogResult.None;
                return;
            }
            this.DialogResult = DialogResult.OK;

            // 创建一个瓦斯浓度探头数据实体
            GasConcentrationProbeDataEntity gasConcentrationProbeDataEntity = new GasConcentrationProbeDataEntity();
            // 探头编号
            gasConcentrationProbeDataEntity.ProbeId = Convert.ToString(this._lstProbeName.SelectedValue);
            // 探头数值
            double dProbeValue = 0;
            if (double.TryParse(this.txtProbeValue.Text.Trim(), out dProbeValue))
            {
                gasConcentrationProbeDataEntity.ProbeValue = dProbeValue;
            }
            // 记录时间
            gasConcentrationProbeDataEntity.RecordTime = this.dtpRecordTime.Value;
            // 记录类型
            gasConcentrationProbeDataEntity.RecordType = Const_GE.RECORDTYPE_PEOPLE;

            OPERATION_TYPE opType;
            bool bResult = false;
            if (this._bllType == "add")
            {
                // 瓦斯浓度探头数据登录
                bResult = GasConcentrationProbeDataBLL.insertGasConcentrationProbeData(gasConcentrationProbeDataEntity);
                opType = OPERATION_TYPE.ADD;

                if (bResult)
                {
                    #region 通知服务器预警数据已更新
                    var workingfaceEnt =
                  BasicInfoManager.getInstance().getTunnelByID(selectTunnelUserControl1.ITunnelId).WorkingFace;
                    UpdateWarningDataMsg msg = new UpdateWarningDataMsg(workingfaceEnt.WorkingFaceID, selectTunnelUserControl1.ITunnelId, DayReportHCDbConstNames.TABLE_NAME, opType, gasConcentrationProbeDataEntity.RecordTime);
                    this.MainForm.SendMsg2Server(msg);
                    #endregion
                }
            }
            else
            {
                // 主键
                gasConcentrationProbeDataEntity.ProbeDataId = Convert.ToInt32(this._iPK);
                // 探头管理信息修改
                bResult = GasConcentrationProbeDataBLL.updateGasConcentrationProbeData(gasConcentrationProbeDataEntity);
                opType = OPERATION_TYPE.UPDATE;

                if (bResult)
                {
                    #region 通知服务器预警数据已更新
                    var workingfaceEnt =
                  BasicInfoManager.getInstance().getTunnelByID(selectTunnelUserControl1.ITunnelId).WorkingFace;
                    UpdateWarningDataMsg msg = new UpdateWarningDataMsg(workingfaceEnt.WorkingFaceID, selectTunnelUserControl1.ITunnelId, DayReportHCDbConstNames.TABLE_NAME, opType, gasConcentrationProbeDataEntity.RecordTime);
                    this.MainForm.SendMsg2Server(msg);
                    #endregion
                }
            }


            // 添加\修改成功的场合
            if (bResult)
            {
                // 如何探头编号发生改变则不更新管理画面的farpoint
                if (GasConcentrationProbeDataManamement._probeId != Convert.ToString(this._lstProbeName.SelectedValue))
                {
                    GasConcentrationProbeDataManamement._iDisposeFlag = Const.DISPOSE_FLAG_ZERO;
                }
            }
        }

        /// <summary>
        /// 取消
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnCancel_Click(object sender, EventArgs e)
        {
            // 窗口关闭
            this.Close();
        }

        /// <summary>
        /// 验证画面入力数据
        /// </summary>
        /// <returns>验证结果：true 通过验证, false未通过验证</returns>
        private bool check()
        {
            // 判断选择巷道是否选择
            if (this.selectTunnelUserControl1.ITunnelId == Const.INVALID_ID)
            {
                Alert.alert(Const_GE.TUNNEL_NAME_MUST_INPUT);
                return false;
            }

            // 判断传感器是否选择
            if (this._lstProbeName.SelectedItems.Count == 0)
            {
                Alert.alert(Const_GE.PROBE_MUST_CHOOSE);
                return false;
            }

            // 判断传感器数值是否录入
            if (!Check.isEmpty(this.txtProbeValue, Const_GE.PROBE_VALUE))
            {
                return false;
            }

            // 判断传感器数值是否为数字
            if (!Check.IsNumeric(this.txtProbeValue, Const_GE.PROBE_VALUE))
            {
                return false;
            }

            // 验证通过
            return true;
        }
    }
}
