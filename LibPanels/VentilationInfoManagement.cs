// ******************************************************************
// 概  述：井下数据通风信息管理
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

namespace LibPanels
{
    public partial class VentilationInfoManagement : BaseForm
    {
        //***********************************
        private int _iRecordCount = 0;
        int rowsCount = 0;      //数据行数
        int checkCount = 0;     //选择行数
        int rowDetailStartIndex = 4;
        //需要过滤的列索引
        private int[] _filterColunmIdxs = null;
        public static LibEntity.MineData mdEntity = new LibEntity.MineData();
        public static VentilationInfo viEntity = new VentilationInfo();
        private VentilationInfo[] ventilationInfos;
        Tunnel tunnelEntity = new Tunnel();

        //***********************************

        public VentilationInfoManagement(MainFrm mainFrm)
        {
            MainForm = mainFrm;

            InitializeComponent();

            dataPager1.FrmChild_EventHandler += FrmParent_EventHandler;

            //窗体属性设置
            LibCommon.FormDefaultPropertiesSetter.SetManagementFormDefaultProperties(this, Const_GE.VENTILATION_MANAGEMENT);

            //Farpoint属性设置
            LibCommon.FarpointDefaultPropertiesSetter.SetFpDefaultProperties(this.fpVentilation, LibCommon.Const_GE.VENTILATION_FARPOINT, rowDetailStartIndex);

            _filterColunmIdxs = new int[]
            {
                1,
                11,
                12,
                13,
            };
            //禁用选择颜色相关控件
            farpointFilter1.EnableChooseColorCtrls(false);
            //设置自动隐藏过滤条件
            FarpointDefaultPropertiesSetter.SetFpFilterHideProperties(this.fpVentilation, _filterColunmIdxs);

            _queryConditions.Show = bindFpGasEmissionDataWithCondition;
        }

        /// <summary>
        /// 初始化
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MineDataManagement_Load(object sender, EventArgs e)
        {
            this.fpVentilation.Sheets[0].Rows.Default.Resizable = false;
            this.fpVentilation.Sheets[0].Columns[0].Resizable = false;
            this.bindFpGasEmissionDataWithCondition();
        }

        /// <summary>
        /// 委托事件
        /// </summary>
        /// <param name="sender"></param>
        private void FrmParent_EventHandler(object sender)
        {
            bindFpGasEmissionDataWithCondition();
        }

        private void bindFpGasEmissionDataWithCondition()
        {
            FarPointOperate.farPointClear(fpVentilation, rowDetailStartIndex, rowsCount);
            checkCount = 0;
            chkSelAll.Checked = false;
            // ※分页必须
            _iRecordCount = VentilationInfo.GetRecordCount();

            // ※分页必须
            dataPager1.PageControlInit(_iRecordCount);
            int iStartIndex = dataPager1.getStartIndex();
            int iEndIndex = dataPager1.getEndIndex();
            ventilationInfos = VentilationInfo.SlicedFindByCondition(iStartIndex, iEndIndex, _queryConditions.TunnelId, Convert.ToDateTime(_queryConditions.DefaultStartTime), Convert.ToDateTime(_queryConditions.DefaultEndTime));
            rowsCount = ventilationInfos.Length;
            FarPointOperate.farPointReAdd(fpVentilation, rowDetailStartIndex, rowsCount);

            if (rowsCount > 0)
            {
                FarPoint.Win.Spread.CellType.CheckBoxCellType ckbxcell = new FarPoint.Win.Spread.CellType.CheckBoxCellType();
                ckbxcell.ThreeState = false;
                for (int i = 0; i < ventilationInfos.Length; i++)
                {
                    int index = 0;
                    fpVentilation.Sheets[0].Cells[rowDetailStartIndex + i, index].CellType = ckbxcell;
                    //巷道名称

                    fpVentilation.Sheets[0].Cells[rowDetailStartIndex + i, ++index].Text =
                     BasicInfoManager.getInstance().getTunnelByID(_queryConditions.TunnelId).TunnelName;
                    if (LibPanels.checkCoordinate(ventilationInfos[i].CoordinateX, ventilationInfos[i].CoordinateY, ventilationInfos[i].CoordinateZ))
                    {

                        //坐标X
                        fpVentilation.Sheets[0].Cells[rowDetailStartIndex + i, ++index].Text =
                            ventilationInfos[i].CoordinateX.ToString();
                        //坐标Y
                        fpVentilation.Sheets[0].Cells[rowDetailStartIndex + i, ++index].Text =
                            ventilationInfos[i].CoordinateY.ToString();
                        //坐标Z
                        fpVentilation.Sheets[0].Cells[rowDetailStartIndex + i, ++index].Text =
                            ventilationInfos[i].CoordinateZ.ToString();
                    }
                    else
                    {
                        index = index + 3;
                    }
                    //时间
                    fpVentilation.Sheets[0].Cells[rowDetailStartIndex + i, ++index].Text =
                        ventilationInfos[i].Datetime.ToString("yyyy-MM-dd hh:mm:ss");
                    //是否有停风区域
                    fpVentilation.Sheets[0].Cells[rowDetailStartIndex + i, ++index].Text =
                        LibPanels.DataChangeYesNo(ventilationInfos[i].IsNoWindArea);
                    //是否有微风区域
                    fpVentilation.Sheets[0].Cells[rowDetailStartIndex + i, ++index].Text =
                        LibPanels.DataChangeYesNo(ventilationInfos[i].IsLightWindArea);
                    //是否有风流反向区域
                    fpVentilation.Sheets[0].Cells[rowDetailStartIndex + i, ++index].Text =
                        LibPanels.DataChangeYesNo(ventilationInfos[i].IsReturnWindArea);
                    //是否通风断面小于设计断面的2/3
                    fpVentilation.Sheets[0].Cells[rowDetailStartIndex + i, ++index].Text =
                        LibPanels.DataChangeYesNo(ventilationInfos[i].IsSmall);
                    //是否工作面风量低于计划风量，风速与《煤矿安全规程》规定不符
                    fpVentilation.Sheets[0].Cells[rowDetailStartIndex + i, ++index].Text =
                        LibPanels.DataChangeYesNo(ventilationInfos[i].IsFollowRule);
                    //是否通风断面小于设计断面的2/3
                    fpVentilation.Sheets[0].Cells[rowDetailStartIndex + i, ++index].Text =
                        ventilationInfos[i].FaultageArea.ToString();
                    //是否工作面风量低于计划风量，风速与《煤矿安全规程》规定不符
                    fpVentilation.Sheets[0].Cells[rowDetailStartIndex + i, ++index].Text =
                        ventilationInfos[i].AirFlow.ToString();
                    //工作制式
                    fpVentilation.Sheets[0].Cells[rowDetailStartIndex + i, ++index].Text =
                        ventilationInfos[i].WorkStyle;
                    //班次
                    fpVentilation.Sheets[0].Cells[rowDetailStartIndex + i, ++index].Text =
                        ventilationInfos[i].WorkTime;
                    //填报人
                    fpVentilation.Sheets[0].Cells[rowDetailStartIndex + i, ++index].Text =
                        ventilationInfos[i].Submitter;
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
                        fpVentilation.Sheets[0].Cells[rowDetailStartIndex + i, 0].Value = ((CheckBox)sender).Checked;
                        checkCount = ventilationInfos.Length;
                    }
                    //checkbox未选中
                    else
                    {
                        fpVentilation.Sheets[0].Cells[rowDetailStartIndex + i, 0].Value = ((CheckBox)sender).Checked;
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
            m.Text = new LibPanels(MineDataPanelName.Ventilation).panelFormName;
            if (DialogResult.OK == m.ShowDialog())
            {
                bindFpGasEmissionDataWithCondition();
                this.dataPager1.btnLastPage_Click(sender, e);
            }
        }

        /// <summary>
        /// 修改按钮事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tsBtnModify_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < rowsCount; i++)
            {
                if (fpVentilation.Sheets[0].Cells[rowDetailStartIndex + i, 0].Value != null &&
                    (bool)fpVentilation.Sheets[0].Cells[rowDetailStartIndex + i, 0].Value == true)
                {
                    viEntity = ventilationInfos[i];
                }
            }
            MineData m = new MineData(viEntity, this.MainForm);

            m.Text = new LibPanels(MineDataPanelName.Ventilation_Change).panelFormName;
            if (DialogResult.OK == m.ShowDialog())
            {
                bindFpGasEmissionDataWithCondition();
            }
        }

        /// <summary>
        /// 删除按钮事件
        /// </summary>d
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tsBtnDel_Click(object sender, EventArgs e)
        {
            if (Alert.confirm(Const.DEL_CONFIRM_MSG))
            {
                bool bResult = false;
                for (int i = 0; i < rowsCount; i++)
                {
                    if (fpVentilation.Sheets[0].Cells[rowDetailStartIndex + i, 0].Value != null && (bool)fpVentilation.Sheets[0].Cells[rowDetailStartIndex + i, 0].Value == true)
                    {
                        ventilationInfos[i].Delete();
                        bResult = true;
                    }
                }
                if (bResult)
                {
                    bindFpGasEmissionDataWithCondition();
                }
            }
        }

        /// <summary>
        /// 刷新按钮事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tsBtnRefresh_Click(object sender, EventArgs e)
        {
            bindFpGasEmissionDataWithCondition();
        }

        /// <summary>
        /// 退出按钮事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tsBtnExit_Click(object sender, EventArgs e)
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
            if (FileExport.fileExport(fpVentilation, true))
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
                FarpointDefaultPropertiesSetter.SetFpFilterHideProperties(this.fpVentilation, _filterColunmIdxs);

            }
            else//未选中时，根据用户自定义的颜色进行分类显示
            {
                //启用选择颜色相关控件
                farpointFilter1.EnableChooseColorCtrls(true);
                //设置自定义过滤条件
                FarpointDefaultPropertiesSetter.SetFpCustomFilterProperties(this.fpVentilation, farpointFilter1.GetSelectedFitColor(), farpointFilter1.GetSelectedNotFitColor(), _filterColunmIdxs);
            }
        }

        private void farpointFilter1_OnClickClearFilterBtn(object sender, EventArgs arg)
        {
            //清空过滤条件
            this.fpVentilation.ActiveSheet.RowFilter.ResetFilter();
        }

        private void farpointFilter1_OnClickFitColorBtnOK(object sender, EventArgs arg)
        {
            //根据新的颜色值设置自动隐藏过滤条件
            FarpointDefaultPropertiesSetter.SetFpCustomFilterProperties(this.fpVentilation, farpointFilter1.GetSelectedFitColor(), farpointFilter1.GetSelectedNotFitColor(), _filterColunmIdxs);
        }

        private void farpointFilter1_OnClickNotFitColorBtnOK(object sender, EventArgs arg)
        {
            //根据新的颜色值设置自动隐藏过滤条件
            FarpointDefaultPropertiesSetter.SetFpCustomFilterProperties(this.fpVentilation, farpointFilter1.GetSelectedFitColor(), farpointFilter1.GetSelectedNotFitColor(), _filterColunmIdxs);
        }
        #endregion

        private void btnOK_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
