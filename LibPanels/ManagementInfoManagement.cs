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
        public static LibEntity.MineData mdEntity = new LibEntity.MineData();
        public static Management mEntity = new Management();
        public static Tunnel te = new Tunnel();
        private Management[] managements;
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
            _iRecordCount = Management.GetRecordCount();

            // ※分页必须
            dataPager1.PageControlInit(_iRecordCount);
            int iStartIndex = dataPager1.getStartIndex();
            int iEndIndex = dataPager1.getEndIndex();
            managements = Management.SlicedFindByCondition(iStartIndex, iEndIndex, _queryConditions.TunnelId,
                Convert.ToDateTime(_queryConditions.DefaultStartTime),
                Convert.ToDateTime(_queryConditions.DefaultEndTime));
            rowsCount = managements.Length;
            FarPointOperate.farPointReAdd(fpManagement, rowDetailStartIndex, rowsCount);
            if (rowsCount > 0)
            {
                FarPoint.Win.Spread.CellType.CheckBoxCellType ckbxcell =
                    new FarPoint.Win.Spread.CellType.CheckBoxCellType();
                ckbxcell.ThreeState = false;
                for (int i = 0; i < managements.Length; i++)
                {
                    int index = 0;
                    this.fpManagement.Sheets[0].Cells[rowDetailStartIndex + i, index].CellType = ckbxcell;
                    //巷道名称
                    this.fpManagement.Sheets[0].Cells[rowDetailStartIndex + i, ++index].Text =
                        BasicInfoManager.getInstance().getTunnelByID(_queryConditions.TunnelId).TunnelName;
                    // TunnelInfoBLL.selectTunnelInfoByTunnelID(Convert.ToInt32(ds.Tables[0].Rows[i]["TUNNEL_ID"])).TunnelName;
                    if (LibPanels.checkCoordinate(managements[i].CoordinateX, managements[i].CoordinateY,
                        managements[i].CoordinateZ))
                    {
                        //坐标X
                        fpManagement.Sheets[0].Cells[rowDetailStartIndex + i, ++index].Text =
                            managements[i].CoordinateX.ToString();
                        //坐标Y
                        fpManagement.Sheets[0].Cells[rowDetailStartIndex + i, ++index].Text =
                            managements[i].CoordinateY.ToString();
                        //坐标Z
                        fpManagement.Sheets[0].Cells[rowDetailStartIndex + i, ++index].Text =
                            managements[i].CoordinateZ.ToString();
                    }
                    else
                    {
                        index = index + 3;
                    }
                    //时间
                    fpManagement.Sheets[0].Cells[rowDetailStartIndex + i, ++index].Text =
                        managements[i].Datetime.ToString("yyyy-MM-dd hh:mm:ss");
                    //是否存在瓦斯异常不汇报
                    fpManagement.Sheets[0].Cells[rowDetailStartIndex + i, ++index].Text =
                        LibPanels.DataChangeYesNo(managements[i].IsGasErrorNotReport);
                    //是否存在工作面出现地质构造不汇报
                    fpManagement.Sheets[0].Cells[rowDetailStartIndex + i, ++index].Text =
                        LibPanels.DataChangeYesNo(managements[i].IsWFNotReport);
                    //是否存在强化瓦斯措施执行不到位
                    fpManagement.Sheets[0].Cells[rowDetailStartIndex + i, ++index].Text =
                        LibPanels.DataChangeYesNo(managements[i].IsStrgasNotDoWell);
                    //是否存在进回风巷隅角、尾巷管理不到位
                    fpManagement.Sheets[0].Cells[rowDetailStartIndex + i, ++index].Text =
                        LibPanels.DataChangeYesNo(managements[i].IsRwmanagementNotDoWell);
                    //是否存在通风设施人为损坏
                    fpManagement.Sheets[0].Cells[rowDetailStartIndex + i, ++index].Text =
                        LibPanels.DataChangeYesNo(managements[i].IsVFBrokenByPeople);
                    //是否存在甲烷传感器位置不当、误差大、调校超过规定
                    fpManagement.Sheets[0].Cells[rowDetailStartIndex + i, ++index].Text =
                        LibPanels.DataChangeYesNo(managements[i].IsElementPlaceNotGood);
                    //是否存在瓦检员空漏假检
                    fpManagement.Sheets[0].Cells[rowDetailStartIndex + i, ++index].Text =
                        LibPanels.DataChangeYesNo(managements[i].IsReporterFalseData);
                    //钻孔未按设计施工次数
                    fpManagement.Sheets[0].Cells[rowDetailStartIndex + i, ++index].Text =
                        LibPanels.DataChangeYesNo(managements[i].IsDrillWrongBuild);
                    //钻孔施工不到位次数
                    fpManagement.Sheets[0].Cells[rowDetailStartIndex + i, ++index].Text =
                        LibPanels.DataChangeYesNo(managements[i].IsDrillNotDoWell);
                    //防突措施执行不到位次数
                    fpManagement.Sheets[0].Cells[rowDetailStartIndex + i, ++index].Text =
                        LibPanels.DataChangeYesNo(managements[i].IsOPNotDoWell);
                    //防突异常情况未汇报次数
                    fpManagement.Sheets[0].Cells[rowDetailStartIndex + i, ++index].Text =
                        LibPanels.DataChangeYesNo(managements[i].IsOPErrorNotReport);
                    //是否存在局部通风机单回路供电或不能正常切换
                    fpManagement.Sheets[0].Cells[rowDetailStartIndex + i, ++index].Text =
                        LibPanels.DataChangeYesNo(managements[i].IsPartWindSwitchError);
                    //是否存在安全监测监控系统未及时安装
                    fpManagement.Sheets[0].Cells[rowDetailStartIndex + i, ++index].Text =
                        LibPanels.DataChangeYesNo(managements[i].IsSafeCtrlUninstall);
                    //是否存在监测监控停运
                    fpManagement.Sheets[0].Cells[rowDetailStartIndex + i, ++index].Text =
                        LibPanels.DataChangeYesNo(managements[i].IsCtrlStop);
                    //是否存在不执行瓦斯治理措施、破坏通风设施
                    fpManagement.Sheets[0].Cells[rowDetailStartIndex + i, ++index].Text =
                        LibPanels.DataChangeYesNo(managements[i].IsGasNotDowWell);
                    //是否高、突矿井工作面无专职瓦斯检查员
                    fpManagement.Sheets[0].Cells[rowDetailStartIndex + i, ++index].Text =
                        LibPanels.DataChangeYesNo(managements[i].IsMineNoChecker);
                    //工作制式
                    fpManagement.Sheets[0].Cells[rowDetailStartIndex + i, ++index].Text =
                        managements[i].WorkStyle;
                    //班次
                    fpManagement.Sheets[0].Cells[rowDetailStartIndex + i, ++index].Text =
                        managements[i].WorkTime;
                    //填报人
                    fpManagement.Sheets[0].Cells[rowDetailStartIndex + i, ++index].Text =
                        managements[i].Submitter;
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
                        checkCount = managements.Length;
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
            for (int i = 0; i < rowsCount; i++)
            {
                if (fpManagement.Sheets[0].Cells[rowDetailStartIndex + i, 0].Value != null &&
                    (bool)fpManagement.Sheets[0].Cells[rowDetailStartIndex + i, 0].Value == true)
                {
                    mEntity = managements[i];
                }
            }
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
                        managements[i].DeleteAndFlush();
                        bResult = true;
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
