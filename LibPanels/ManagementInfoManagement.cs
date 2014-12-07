// ******************************************************************
// 概  述：井下数据管理信息管理
// 作  者：宋英杰
// 创建日期：2014/3/11
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
using LibCommonControl;
using LibBusiness;
using LibCommon;
using LibEntity;
using LibCommonForm;

namespace LibPanels
{
    public partial class ManagementInfoManagement : BaseForm
    {
        //***********************************
        private int _iRecordCount = 0;
        int rowsCount = 0;      //数据行数
        int checkCount = 0;     //选择行数
        int rowDetailStartIndex = 4;
        int _tmpRowIndex = 0;
        int tmpInt = 0;
        double tmpDouble = 0;
        DateTime tmpDtp = DateTime.Now;
        //需要过滤的列索引
        private int[] _filterColunmIdxs = null;
        DataSet ds = new DataSet();
        public static LibEntity.MineData mdEntity = new LibEntity.MineData();
        public static Management mEntity = new Management();
        public static Tunnel te = new Tunnel();
        //***********************************

        /// <summary>
        /// 构造方法
        /// </summary>
        public ManagementInfoManagement(MainFrm mainFrm)
        {
            InitializeComponent();

            this.MainForm = mainFrm;

            dataPager1.FrmChild_EventHandler += FrmParent_EventHandler;

            //窗体属性设置
            LibCommon.FormDefaultPropertiesSetter.SetManagementFormDefaultProperties(this, Const_OP.MANAGEMENT_MANAGEMENT);

            //Farpoint属性设置
            LibCommon.FarpointDefaultPropertiesSetter.SetFpDefaultProperties(this.fpManagement, LibCommon.Const_OP.MANAGEMENT_FARPOINT, rowDetailStartIndex);
            _filterColunmIdxs = new int[]
            {
                1,
                22,
                23,
                24,
            };
            //禁用选择颜色相关控件
            farpointFilter1.EnableChooseColorCtrls(false);
            //设置自动隐藏过滤条件
            FarpointDefaultPropertiesSetter.SetFpFilterHideProperties(this.fpManagement, _filterColunmIdxs);

            _queryConditions.Show = bindFpManagementWithCondition;
        }

        /// <summary>
        /// 初始化
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MineDataManagement_Load(object sender, EventArgs e)
        {
            this.fpManagement.Sheets[0].Rows.Default.Resizable = false;
            this.fpManagement.Sheets[0].Columns[0].Resizable = false;
            this.bindFpManagementWithCondition();
        }

        /// <summary>
        /// 委托事件
        /// </summary>
        /// <param name="sender"></param>
        private void FrmParent_EventHandler(object sender)
        {
            bindFpManagementWithCondition();
        }

        /// <summary>
        /// farpoint数据绑定
        /// </summary>
        private void bindFpManagementWithCondition()
        {
            FarPointOperate.farPointClear(fpManagement, rowDetailStartIndex, rowsCount);
            checkCount = 0;
            chkSelAll.Checked = false;
            // ※分页必须
            _iRecordCount = ManagementBLL.selectManagementWithCondition(_queryConditions.TunnelId, _queryConditions.DefaultStartTime, _queryConditions.DefaultEndTime).Tables[0].Rows.Count;

            // ※分页必须
            dataPager1.PageControlInit(_iRecordCount);
            int iStartIndex = dataPager1.getStartIndex();
            int iEndIndex = dataPager1.getEndIndex();
            ds = ManagementBLL.selectManagementWithCondition(iStartIndex, iEndIndex, _queryConditions.TunnelId, _queryConditions.DefaultStartTime, _queryConditions.DefaultEndTime);
            rowsCount = ds.Tables[0].Rows.Count;
            FarPointOperate.farPointReAdd(fpManagement, rowDetailStartIndex, rowsCount);
            if (rowsCount > 0)
            {
                FarPoint.Win.Spread.CellType.CheckBoxCellType ckbxcell = new FarPoint.Win.Spread.CellType.CheckBoxCellType();
                ckbxcell.ThreeState = false;
                for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                {
                    int index = 0;
                    this.fpManagement.Sheets[0].Cells[rowDetailStartIndex + i, index].CellType = ckbxcell;
                    //巷道名称
                    this.fpManagement.Sheets[0].Cells[rowDetailStartIndex + i, ++index].Text =
                        BasicInfoManager.getInstance().getTunnelByID(_queryConditions.TunnelId).TunnelName;// TunnelInfoBLL.selectTunnelInfoByTunnelID(Convert.ToInt32(ds.Tables[0].Rows[i]["TUNNEL_ID"])).TunnelName;
                    if (LibPanels.checkCoordinate(ds.Tables[0].Rows[i][ManagementDbConstNames.X].ToString(), ds.Tables[0].Rows[i][ManagementDbConstNames.Y].ToString(), ds.Tables[0].Rows[i][ManagementDbConstNames.Z].ToString()))
                    {
                        //坐标X
                        this.fpManagement.Sheets[0].Cells[rowDetailStartIndex + i, ++index].Text = ds.Tables[0].Rows[i][ManagementDbConstNames.X].ToString();
                        //坐标Y
                        this.fpManagement.Sheets[0].Cells[rowDetailStartIndex + i, ++index].Text = ds.Tables[0].Rows[i][ManagementDbConstNames.Y].ToString();
                        //坐标Z
                        this.fpManagement.Sheets[0].Cells[rowDetailStartIndex + i, ++index].Text = ds.Tables[0].Rows[i][ManagementDbConstNames.Z].ToString();
                    }
                    else
                    {
                        index = index + 3;
                    }
                    //时间
                    this.fpManagement.Sheets[0].Cells[rowDetailStartIndex + i, ++index].Text = ds.Tables[0].Rows[i][ManagementDbConstNames.DATETIME].ToString().Substring(0, ds.Tables[0].Rows[i][ManagementDbConstNames.DATETIME].ToString().IndexOf(' '));
                    //是否存在瓦斯异常不汇报
                    this.fpManagement.Sheets[0].Cells[rowDetailStartIndex + i, ++index].Text = LibPanels.DataChangeYesNo(ds.Tables[0].Rows[i][ManagementDbConstNames.IS_GAS_ERROR_NOT_REPORT].ToString());
                    //是否存在工作面出现地质构造不汇报
                    this.fpManagement.Sheets[0].Cells[rowDetailStartIndex + i, ++index].Text = LibPanels.DataChangeYesNo(ds.Tables[0].Rows[i][ManagementDbConstNames.IS_WF_NOT_REPORT].ToString());
                    //是否存在强化瓦斯措施执行不到位
                    this.fpManagement.Sheets[0].Cells[rowDetailStartIndex + i, ++index].Text = LibPanels.DataChangeYesNo(ds.Tables[0].Rows[i][ManagementDbConstNames.IS_STRGAS_NOT_DO_WELL].ToString());
                    //是否存在进回风巷隅角、尾巷管理不到位
                    this.fpManagement.Sheets[0].Cells[rowDetailStartIndex + i, ++index].Text = LibPanels.DataChangeYesNo(ds.Tables[0].Rows[i][ManagementDbConstNames.IS_RWMANAGEMENT_NOT_DO_WELL].ToString());
                    //是否存在通风设施人为损坏
                    this.fpManagement.Sheets[0].Cells[rowDetailStartIndex + i, ++index].Text = LibPanels.DataChangeYesNo(ds.Tables[0].Rows[i][ManagementDbConstNames.IS_VF_BROKEN_BY_PEOPLE].ToString());
                    //是否存在甲烷传感器位置不当、误差大、调校超过规定
                    this.fpManagement.Sheets[0].Cells[rowDetailStartIndex + i, ++index].Text = LibPanels.DataChangeYesNo(ds.Tables[0].Rows[i][ManagementDbConstNames.IS_ELEMENT_PLACE_NOT_GOOD].ToString());
                    //是否存在瓦检员空漏假检
                    this.fpManagement.Sheets[0].Cells[rowDetailStartIndex + i, ++index].Text = LibPanels.DataChangeYesNo(ds.Tables[0].Rows[i][ManagementDbConstNames.IS_REPORTER_FALSE_DATA].ToString());
                    //钻孔未按设计施工次数
                    this.fpManagement.Sheets[0].Cells[rowDetailStartIndex + i, ++index].Text = LibPanels.DataChangeYesNo(ds.Tables[0].Rows[i][ManagementDbConstNames.IS_DRILL_WRONG_BUILD].ToString());
                    //钻孔施工不到位次数
                    this.fpManagement.Sheets[0].Cells[rowDetailStartIndex + i, ++index].Text = LibPanels.DataChangeYesNo(ds.Tables[0].Rows[i][ManagementDbConstNames.IS_DRILL_NOT_DO_WELL].ToString());
                    //防突措施执行不到位次数
                    this.fpManagement.Sheets[0].Cells[rowDetailStartIndex + i, ++index].Text = LibPanels.DataChangeYesNo(ds.Tables[0].Rows[i][ManagementDbConstNames.IS_OP_NOT_DO_WELL].ToString());
                    //防突异常情况未汇报次数
                    this.fpManagement.Sheets[0].Cells[rowDetailStartIndex + i, ++index].Text = LibPanels.DataChangeYesNo(ds.Tables[0].Rows[i][ManagementDbConstNames.IS_OP_ERROR_NOT_REPORT].ToString());
                    //是否存在局部通风机单回路供电或不能正常切换
                    this.fpManagement.Sheets[0].Cells[rowDetailStartIndex + i, ++index].Text = LibPanels.DataChangeYesNo(ds.Tables[0].Rows[i][ManagementDbConstNames.IS_PART_WIND_SWITCH_ERROR].ToString());
                    //是否存在安全监测监控系统未及时安装
                    this.fpManagement.Sheets[0].Cells[rowDetailStartIndex + i, ++index].Text = LibPanels.DataChangeYesNo(ds.Tables[0].Rows[i][ManagementDbConstNames.IS_SAFE_CTRL_UNINSTALL].ToString());
                    //是否存在监测监控停运
                    this.fpManagement.Sheets[0].Cells[rowDetailStartIndex + i, ++index].Text = LibPanels.DataChangeYesNo(ds.Tables[0].Rows[i][ManagementDbConstNames.IS_CTRL_STOP].ToString());
                    //是否存在不执行瓦斯治理措施、破坏通风设施
                    this.fpManagement.Sheets[0].Cells[rowDetailStartIndex + i, ++index].Text = LibPanels.DataChangeYesNo(ds.Tables[0].Rows[i][ManagementDbConstNames.IS_GAS_NOT_DO_WELL].ToString());
                    //是否高、突矿井工作面无专职瓦斯检查员
                    this.fpManagement.Sheets[0].Cells[rowDetailStartIndex + i, ++index].Text = LibPanels.DataChangeYesNo(ds.Tables[0].Rows[i][ManagementDbConstNames.IS_MINE_NO_CHECKER].ToString());
                    //工作制式
                    this.fpManagement.Sheets[0].Cells[rowDetailStartIndex + i, ++index].Text = ds.Tables[0].Rows[i][ManagementDbConstNames.WORK_STYLE].ToString();
                    //班次
                    this.fpManagement.Sheets[0].Cells[rowDetailStartIndex + i, ++index].Text = ds.Tables[0].Rows[i][ManagementDbConstNames.WORK_TIME].ToString();
                    //填报人
                    this.fpManagement.Sheets[0].Cells[rowDetailStartIndex + i, ++index].Text = ds.Tables[0].Rows[i][ManagementDbConstNames.SUBMITTER].ToString();
                }
            }
            setButtenEnable();
        }

        /// <summary>
        /// farpoint中checkbox选中对全选反选的影响
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void fpGasEmissionData_ButtonClicked(object sender, FarPoint.Win.Spread.EditorNotifyEventArgs e)
        {
            if (e.EditingControl is FarPoint.Win.FpCheckBox)
            {
                FarPoint.Win.FpCheckBox fpChk = (FarPoint.Win.FpCheckBox)e.EditingControl;
                if (fpChk.Checked)
                {
                    checkCount++;
                }
                else
                {
                    checkCount--;
                }
            }
            if (checkCount == rowsCount)
            {
                chkSelAll.Checked = true;
            }
            else
            {
                chkSelAll.Checked = false;
            }
            setButtenEnable();
        }

        /// <summary>
        /// 设置按钮可操作性
        /// </summary>
        private void setButtenEnable()
        {
            if (checkCount == 1)
            {
                tsBtnModify.Enabled = true;
            }
            else
            {
                tsBtnModify.Enabled = false;
            }
            if (checkCount > 0)
            {
                tsBtnDel.Enabled = true;
            }
            else
            {
                tsBtnDel.Enabled = false;
            }
        }

        /// <summary>
        /// 管理实体赋值
        /// </summary>
        private void setMineDataEntityValue()
        {
            for (int i = 0; i < rowsCount; i++)
            {
                if (fpManagement.Sheets[0].Cells[rowDetailStartIndex + i, 0].Value != null && (bool)fpManagement.Sheets[0].Cells[rowDetailStartIndex + i, 0].Value == true)
                {
                    //ID
                    if (int.TryParse(ds.Tables[0].Rows[i][ManagementDbConstNames.ID].ToString(), out tmpInt))
                    {
                        mEntity.Id = tmpInt;
                        tmpInt = 0;
                    }
                    //绑定巷道ID
                    if (int.TryParse(ds.Tables[0].Rows[i][ManagementDbConstNames.TUNNEL_ID].ToString(), out tmpInt))
                    {
                        mEntity.Tunnel.TunnelId = tmpInt;
                        tmpInt = 0;
                    }
                    //坐标X
                    if (double.TryParse(ds.Tables[0].Rows[i][ManagementDbConstNames.X].ToString(), out tmpDouble))
                    {
                        mEntity.CoordinateX = tmpDouble;
                        tmpDouble = 0;
                    }
                    //坐标Y
                    if (double.TryParse(ds.Tables[0].Rows[i][ManagementDbConstNames.Y].ToString(), out tmpDouble))
                    {
                        mEntity.CoordinateY = tmpDouble;
                        tmpDouble = 0;
                    }
                    //坐标Z
                    if (double.TryParse(ds.Tables[0].Rows[i][ManagementDbConstNames.Z].ToString(), out tmpDouble))
                    {
                        mEntity.CoordinateZ = tmpDouble;
                        tmpDouble = 0;
                    }
                    //工作制式
                    mEntity.WorkStyle = ds.Tables[0].Rows[i][ManagementDbConstNames.WORK_STYLE].ToString();
                    //班次
                    mEntity.WorkTime = ds.Tables[0].Rows[i][ManagementDbConstNames.WORK_TIME].ToString();
                    //队别名称
                    mEntity.TeamName = ds.Tables[0].Rows[i][ManagementDbConstNames.TEAM_NAME].ToString();
                    //填报人
                    mEntity.Submitter = ds.Tables[0].Rows[i][ManagementDbConstNames.SUBMITTER].ToString();
                    //提交日期
                    if (DateTime.TryParse(ds.Tables[0].Rows[i][ManagementDbConstNames.DATETIME].ToString(), out tmpDtp))
                    {
                        mEntity.Datetime = tmpDtp;
                        tmpDtp = DateTime.Now;
                    }
                    //是否存在瓦斯异常不汇报
                    if (int.TryParse(ds.Tables[0].Rows[i][ManagementDbConstNames.IS_GAS_ERROR_NOT_REPORT].ToString(), out tmpInt))
                    {
                        mEntity.IsGasErrorNotReport = tmpInt;
                        tmpInt = 0;
                    }
                    //是否存在工作面出现地质构造不汇报
                    if (int.TryParse(ds.Tables[0].Rows[i][ManagementDbConstNames.IS_WF_NOT_REPORT].ToString(), out tmpInt))
                    {
                        mEntity.IsWFNotReport = tmpInt;
                        tmpInt = 0;
                    }
                    //是否存在强化瓦斯措施执行不到位
                    if (int.TryParse(ds.Tables[0].Rows[i][ManagementDbConstNames.IS_STRGAS_NOT_DO_WELL].ToString(), out tmpInt))
                    {
                        mEntity.IsStrgasNotDoWell = tmpInt;
                        tmpInt = 0;
                    }
                    //是否存在进回风巷隅角、尾巷管理不到位
                    if (int.TryParse(ds.Tables[0].Rows[i][ManagementDbConstNames.IS_RWMANAGEMENT_NOT_DO_WELL].ToString(), out tmpInt))
                    {
                        mEntity.IsRwmanagementNotDoWell = tmpInt;
                        tmpInt = 0;
                    }
                    //是否存在通风设施人为损坏
                    if (int.TryParse(ds.Tables[0].Rows[i][ManagementDbConstNames.IS_VF_BROKEN_BY_PEOPLE].ToString(), out tmpInt))
                    {
                        mEntity.IsVFBrokenByPeople = tmpInt;
                        tmpInt = 0;
                    }
                    //是否存在甲烷传感器位置不当、误差大、调校超过规定
                    if (int.TryParse(ds.Tables[0].Rows[i][ManagementDbConstNames.IS_ELEMENT_PLACE_NOT_GOOD].ToString(), out tmpInt))
                    {
                        mEntity.IsElementPlaceNotGood = tmpInt;
                        tmpInt = 0;
                    }
                    //是否存在瓦检员空漏假检
                    if (int.TryParse(ds.Tables[0].Rows[i][ManagementDbConstNames.IS_REPORTER_FALSE_DATA].ToString(), out tmpInt))
                    {
                        mEntity.IsReporterFalseData = tmpInt;
                        tmpInt = 0;
                    }
                    //钻孔未按设计施工次数
                    if (int.TryParse(ds.Tables[0].Rows[i][ManagementDbConstNames.IS_DRILL_WRONG_BUILD].ToString(), out tmpInt))
                    {
                        mEntity.IsDrillWrongBuild = tmpInt;
                        tmpInt = 0;
                    }
                    //钻孔施工不到位次数
                    if (int.TryParse(ds.Tables[0].Rows[i][ManagementDbConstNames.IS_DRILL_NOT_DO_WELL].ToString(), out tmpInt))
                    {
                        mEntity.IsDrillNotDoWell = tmpInt;
                        tmpInt = 0;
                    }
                    //防突措施执行不到位次数
                    if (int.TryParse(ds.Tables[0].Rows[i][ManagementDbConstNames.IS_OP_NOT_DO_WELL].ToString(), out tmpInt))
                    {
                        mEntity.IsOPNotDoWell = tmpInt;
                        tmpInt = 0;
                    }
                    //防突异常情况未汇报次数
                    if (int.TryParse(ds.Tables[0].Rows[i][ManagementDbConstNames.IS_OP_ERROR_NOT_REPORT].ToString(), out tmpInt))
                    {
                        mEntity.IsOPErrorNotReport = tmpInt;
                        tmpInt = 0;
                    }
                    //是否存在局部通风机单回路供电或不能正常切换
                    if (int.TryParse(ds.Tables[0].Rows[i][ManagementDbConstNames.IS_PART_WIND_SWITCH_ERROR].ToString(), out tmpInt))
                    {
                        mEntity.IsPartWindSwitchError = tmpInt;
                        tmpInt = 0;
                    }
                    //是否存在安全监测监控系统未及时安装
                    if (int.TryParse(ds.Tables[0].Rows[i][ManagementDbConstNames.IS_SAFE_CTRL_UNINSTALL].ToString(), out tmpInt))
                    {
                        mEntity.IsSafeCtrlUninstall = tmpInt;
                        tmpInt = 0;
                    }
                    //是否存在监测监控停运
                    if (int.TryParse(ds.Tables[0].Rows[i][ManagementDbConstNames.IS_CTRL_STOP].ToString(), out tmpInt))
                    {
                        mEntity.IsCtrlStop = tmpInt;
                        tmpInt = 0;
                    }
                    //是否存在不执行瓦斯治理措施、破坏通风设施
                    if (int.TryParse(ds.Tables[0].Rows[i][ManagementDbConstNames.IS_GAS_NOT_DO_WELL].ToString(), out tmpInt))
                    {
                        mEntity.IsGasNotDowWell = tmpInt;
                        tmpInt = 0;
                    }
                    //是否高、突矿井工作面无专职瓦斯检查员
                    if (int.TryParse(ds.Tables[0].Rows[i][ManagementDbConstNames.IS_MINE_NO_CHECKER].ToString(), out tmpInt))
                    {
                        mEntity.IsMineNoChecker = tmpInt;
                        tmpInt = 0;
                    }
                }
            }
        }

        /// <summary>
        /// 全选反选
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void chkSelAll_Click(object sender, EventArgs e)
        {
            //farpoint有数据时
            if (rowsCount > 0)
            {
                //遍历数据
                for (int i = 0; i < rowsCount; i++)
                {
                    //checkbox选中
                    if (chkSelAll.Checked)
                    {
                        this.fpManagement.Sheets[0].Cells[rowDetailStartIndex + i, 0].Value = ((CheckBox)sender).Checked;
                        checkCount = ds.Tables[0].Rows.Count;
                    }
                    //checkbox未选中
                    else
                    {
                        this.fpManagement.Sheets[0].Cells[rowDetailStartIndex + i, 0].Value = ((CheckBox)sender).Checked;
                        checkCount = 0;
                    }
                }
            }
            setButtenEnable();
        }

        /// <summary>
        /// 添加按钮事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tsBtnAdd_Click(object sender, EventArgs e)
        {
            //MineData m = new MineData();
            MineDataSimple m = new MineDataSimple(this.MainForm);
            m.Text = new LibPanels(MineDataPanelName.Management).panelFormName;
            if (DialogResult.OK == m.ShowDialog())
            {
                //重新绑定
                bindFpManagementWithCondition();
                this.dataPager1.btnLastPage_Click(sender, e);
                FarPointOperate.farPointFocusSetAdd(fpManagement, rowDetailStartIndex, rowsCount);
            }
        }

        /// <summary>
        ///  修改按钮事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tsBtnModify_Click(object sender, EventArgs e)
        {
            setMineDataEntityValue();
            MineData m = new MineData(mEntity, this.MainForm);

            m.Text = new LibPanels(MineDataPanelName.Management_Change).panelFormName;
            if (DialogResult.OK == m.ShowDialog())
            {
                _tmpRowIndex = fpManagement.ActiveSheet.ActiveRowIndex;
                bindFpManagementWithCondition();
                FarPointOperate.farPointFocusSetChange(fpManagement, _tmpRowIndex);
            }
        }

        /// <summary>
        /// 删除按钮事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tsBtnDel_Click(object sender, EventArgs e)
        {
            if (Alert.confirm(Const.DEL_CONFIRM_MSG))
            {
                bool bResult = false;
                for (int i = 0; i < rowsCount; i++)
                {
                    if (fpManagement.Sheets[0].Cells[rowDetailStartIndex + i, 0].Value != null && (bool)fpManagement.Sheets[0].Cells[rowDetailStartIndex + i, 0].Value == true)
                    {
                        int id = (int)ds.Tables[0].Rows[i][ManagementDbConstNames.ID];
                        bResult = ManagementBLL.deleteManagementInfo(id);
                    }
                }
                if (bResult)
                {
                    //TODO:删除成功
                }
                bindFpManagementWithCondition();
            }
        }

        /// <summary>
        /// 确定按钮事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnOK_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        /// <summary>
        /// 取消按钮事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        /// <summary>
        /// 导出按钮事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tsBtnExport_Click(object sender, EventArgs e)
        {
            if (FileExport.fileExport(fpManagement, true))
            {
                Alert.alert(Const.EXPORT_SUCCESS_MSG);
            }
        }

        #region Farpoint自动过滤功能
        private void farpointFilter1_OnCheckFilterChanged(object sender, EventArgs arg)
        {
            CheckBox chk = (CheckBox)sender;
            //当Checkbox选中时，筛选过程中则将不符合条件的数据隐藏
            if (chk.Checked == true)
            {
                //禁用选择颜色相关控件
                farpointFilter1.EnableChooseColorCtrls(false);
                //设置自动隐藏过滤条件
                FarpointDefaultPropertiesSetter.SetFpFilterHideProperties(this.fpManagement, _filterColunmIdxs);

            }
            else//未选中时，根据用户自定义的颜色进行分类显示
            {
                //启用选择颜色相关控件
                farpointFilter1.EnableChooseColorCtrls(true);
                //设置自定义过滤条件
                FarpointDefaultPropertiesSetter.SetFpCustomFilterProperties(this.fpManagement, farpointFilter1.GetSelectedFitColor(), farpointFilter1.GetSelectedNotFitColor(), _filterColunmIdxs);
            }
        }

        private void farpointFilter1_OnClickClearFilterBtn(object sender, EventArgs arg)
        {
            //清空过滤条件
            this.fpManagement.ActiveSheet.RowFilter.ResetFilter();
        }

        private void farpointFilter1_OnClickFitColorBtnOK(object sender, EventArgs arg)
        {
            //根据新的颜色值设置自动隐藏过滤条件
            FarpointDefaultPropertiesSetter.SetFpCustomFilterProperties(this.fpManagement, farpointFilter1.GetSelectedFitColor(), farpointFilter1.GetSelectedNotFitColor(), _filterColunmIdxs);
        }

        private void farpointFilter1_OnClickNotFitColorBtnOK(object sender, EventArgs arg)
        {
            //根据新的颜色值设置自动隐藏过滤条件
            FarpointDefaultPropertiesSetter.SetFpCustomFilterProperties(this.fpManagement, farpointFilter1.GetSelectedFitColor(), farpointFilter1.GetSelectedNotFitColor(), _filterColunmIdxs);
        }
        #endregion

        /// <summary>
        /// 刷新按钮事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tsBtnRefresh_Click(object sender, EventArgs e)
        {
            bindFpManagementWithCondition();
        }

        /// <summary>
        /// 退出按钮事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tsBtnExit_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
