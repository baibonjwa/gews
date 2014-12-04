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
        DataSet ds = new DataSet();
        public static LibEntity.MineData mdEntity = new LibEntity.MineData();
        public static VentilationInfo viEntity = new VentilationInfo();
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

        /// <summary>
        /// farpoint数据绑定
        /// </summary>
        //private void bindFpGasEmissionData()
        //{
        //    FarPointOperate.farPointClear(fpVentilation, rowDetailStartIndex, rowsCount);
        //    checkCount = 0;
        //    chkSelAll.Checked = false;
        //    // ※分页必须
        //    _iRecordCount = VentilationBLL.selectVentilationInfo().Tables[0].Rows.Count;

        //    // ※分页必须
        //    dataPager1.PageControlInit(_iRecordCount);
        //    int iStartIndex = dataPager1.getStartIndex();
        //    int iEndIndex = dataPager1.getEndIndex();
        //    ds = VentilationBLL.selectVentilationInfo(iStartIndex, iEndIndex);
        //    rowsCount = ds.Tables[0].Rows.Count;
        //    FarPointOperate.farPointReAdd(fpVentilation, rowDetailStartIndex, rowsCount);

        //    if (rowsCount > 0)
        //    {
        //        FarPoint.Win.Spread.CellType.CheckBoxCellType ckbxcell = new FarPoint.Win.Spread.CellType.CheckBoxCellType();
        //        ckbxcell.ThreeState = false;
        //        for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
        //        {
        //            int index = 0;
        //            this.fpVentilation.Sheets[0].Cells[rowDetailStartIndex + i, index].CellType = ckbxcell;
        //            //巷道名称
        //            if (TunnelInfoBLL.selectTunnelInfoByTunnelID(Convert.ToInt32(ds.Tables[0].Rows[i][VentilationDbConstNames.TUNNEL_ID])) != null)
        //                this.fpVentilation.Sheets[0].Cells[rowDetailStartIndex + i, ++index].Text = TunnelInfoBLL.selectTunnelInfoByTunnelID(Convert.ToInt32(ds.Tables[0].Rows[i][VentilationDbConstNames.TUNNEL_ID])).TunnelName;
        //            if (LibPanels.checkCoordinate(ds.Tables[0].Rows[i][VentilationDbConstNames.COORDINATE_X].ToString(), ds.Tables[0].Rows[i][VentilationDbConstNames.COORDINATE_Y].ToString(), ds.Tables[0].Rows[i][VentilationDbConstNames.COORDINATE_Z].ToString()))
        //            {
        //                //坐标X
        //                this.fpVentilation.Sheets[0].Cells[rowDetailStartIndex + i, ++index].Text = ds.Tables[0].Rows[i][VentilationDbConstNames.COORDINATE_X].ToString();
        //                //坐标Y
        //                this.fpVentilation.Sheets[0].Cells[rowDetailStartIndex + i, ++index].Text = ds.Tables[0].Rows[i][VentilationDbConstNames.COORDINATE_Y].ToString();
        //                //坐标Z
        //                this.fpVentilation.Sheets[0].Cells[rowDetailStartIndex + i, ++index].Text = ds.Tables[0].Rows[i][VentilationDbConstNames.COORDINATE_Z].ToString();
        //            }
        //            else
        //            {
        //                index = index + 3;
        //            }
        //            //时间
        //            this.fpVentilation.Sheets[0].Cells[rowDetailStartIndex + i, ++index].Text = ds.Tables[0].Rows[i][VentilationDbConstNames.DATETIME].ToString().Substring(0, ds.Tables[0].Rows[i][VentilationDbConstNames.DATETIME].ToString().IndexOf(' '));
        //            //是否有停风区域
        //            this.fpVentilation.Sheets[0].Cells[rowDetailStartIndex + i, ++index].Text = LibPanels.DataChangeYesNo(ds.Tables[0].Rows[i][VentilationDbConstNames.IS_NO_WIND_AREA].ToString());
        //            //是否有微风区域
        //            this.fpVentilation.Sheets[0].Cells[rowDetailStartIndex + i, ++index].Text = LibPanels.DataChangeYesNo(ds.Tables[0].Rows[i][VentilationDbConstNames.IS_LIGHT_WIND_AREA].ToString());
        //            //是否有风流反向区域
        //            this.fpVentilation.Sheets[0].Cells[rowDetailStartIndex + i, ++index].Text = LibPanels.DataChangeYesNo(ds.Tables[0].Rows[i][VentilationDbConstNames.IS_RETURN_WIND_AREA].ToString());
        //            //是否通风断面小于设计断面的2/3
        //            this.fpVentilation.Sheets[0].Cells[rowDetailStartIndex + i, ++index].Text = LibPanels.DataChangeYesNo(ds.Tables[0].Rows[i][VentilationDbConstNames.IS_SMALL].ToString());
        //            //是否工作面风量低于计划风量，风速与《煤矿安全规程》规定不符
        //            this.fpVentilation.Sheets[0].Cells[rowDetailStartIndex + i, ++index].Text = LibPanels.DataChangeYesNo(ds.Tables[0].Rows[i][VentilationDbConstNames.IS_FOLLOW_RULE].ToString());
        //            //是否通风断面小于设计断面的2/3
        //            this.fpVentilation.Sheets[0].Cells[rowDetailStartIndex + i, ++index].Text = ds.Tables[0].Rows[i][VentilationDbConstNames.FAULTAGE_AREA].ToString();
        //            //是否工作面风量低于计划风量，风速与《煤矿安全规程》规定不符
        //            this.fpVentilation.Sheets[0].Cells[rowDetailStartIndex + i, ++index].Text = ds.Tables[0].Rows[i][VentilationDbConstNames.AIR_FLOW].ToString();
        //            //工作制式
        //            this.fpVentilation.Sheets[0].Cells[rowDetailStartIndex + i, ++index].Text = ds.Tables[0].Rows[i][VentilationDbConstNames.WORK_STYLE].ToString();
        //            //班次
        //            this.fpVentilation.Sheets[0].Cells[rowDetailStartIndex + i, ++index].Text = ds.Tables[0].Rows[i][VentilationDbConstNames.WORK_TIME].ToString();
        //            //填报人
        //            this.fpVentilation.Sheets[0].Cells[rowDetailStartIndex + i, ++index].Text = ds.Tables[0].Rows[i][VentilationDbConstNames.SUBMITTER].ToString();
        //        }
        //    }
        //    setButtenEnable();
        //}

        private void bindFpGasEmissionDataWithCondition()
        {
            FarPointOperate.farPointClear(fpVentilation, rowDetailStartIndex, rowsCount);
            checkCount = 0;
            chkSelAll.Checked = false;
            // ※分页必须
            _iRecordCount = VentilationBLL.selectVentilationInfoWithCondition(_queryConditions.TunnelId, _queryConditions.DefaultStartTime, _queryConditions.DefaultEndTime).Tables[0].Rows.Count;

            // ※分页必须
            dataPager1.PageControlInit(_iRecordCount);
            int iStartIndex = dataPager1.getStartIndex();
            int iEndIndex = dataPager1.getEndIndex();
            ds = VentilationBLL.selectVentilationInfoWithCondition(iStartIndex, iEndIndex, _queryConditions.TunnelId, _queryConditions.DefaultStartTime, _queryConditions.DefaultEndTime);
            rowsCount = ds.Tables[0].Rows.Count;
            FarPointOperate.farPointReAdd(fpVentilation, rowDetailStartIndex, rowsCount);

            if (rowsCount > 0)
            {
                FarPoint.Win.Spread.CellType.CheckBoxCellType ckbxcell = new FarPoint.Win.Spread.CellType.CheckBoxCellType();
                ckbxcell.ThreeState = false;
                for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                {
                    int index = 0;
                    this.fpVentilation.Sheets[0].Cells[rowDetailStartIndex + i, index].CellType = ckbxcell;
                    //巷道名称

                    this.fpVentilation.Sheets[0].Cells[rowDetailStartIndex + i, ++index].Text =
                     BasicInfoManager.getInstance().getTunnelByID(_queryConditions.TunnelId).TunnelName;
                    if (LibPanels.checkCoordinate(ds.Tables[0].Rows[i][VentilationDbConstNames.COORDINATE_X].ToString(), ds.Tables[0].Rows[i][VentilationDbConstNames.COORDINATE_Y].ToString(), ds.Tables[0].Rows[i][VentilationDbConstNames.COORDINATE_Z].ToString()))
                    {
                        //坐标X
                        this.fpVentilation.Sheets[0].Cells[rowDetailStartIndex + i, ++index].Text = ds.Tables[0].Rows[i][VentilationDbConstNames.COORDINATE_X].ToString();
                        //坐标Y
                        this.fpVentilation.Sheets[0].Cells[rowDetailStartIndex + i, ++index].Text = ds.Tables[0].Rows[i][VentilationDbConstNames.COORDINATE_Y].ToString();
                        //坐标Z
                        this.fpVentilation.Sheets[0].Cells[rowDetailStartIndex + i, ++index].Text = ds.Tables[0].Rows[i][VentilationDbConstNames.COORDINATE_Z].ToString();
                    }
                    else
                    {
                        index = index + 3;
                    }
                    //时间
                    this.fpVentilation.Sheets[0].Cells[rowDetailStartIndex + i, ++index].Text = ds.Tables[0].Rows[i][VentilationDbConstNames.DATETIME].ToString().Substring(0, ds.Tables[0].Rows[i][VentilationDbConstNames.DATETIME].ToString().IndexOf(' '));
                    //是否有停风区域
                    this.fpVentilation.Sheets[0].Cells[rowDetailStartIndex + i, ++index].Text = LibPanels.DataChangeYesNo(ds.Tables[0].Rows[i][VentilationDbConstNames.IS_NO_WIND_AREA].ToString());
                    //是否有微风区域
                    this.fpVentilation.Sheets[0].Cells[rowDetailStartIndex + i, ++index].Text = LibPanels.DataChangeYesNo(ds.Tables[0].Rows[i][VentilationDbConstNames.IS_LIGHT_WIND_AREA].ToString());
                    //是否有风流反向区域
                    this.fpVentilation.Sheets[0].Cells[rowDetailStartIndex + i, ++index].Text = LibPanels.DataChangeYesNo(ds.Tables[0].Rows[i][VentilationDbConstNames.IS_RETURN_WIND_AREA].ToString());
                    //是否通风断面小于设计断面的2/3
                    this.fpVentilation.Sheets[0].Cells[rowDetailStartIndex + i, ++index].Text = LibPanels.DataChangeYesNo(ds.Tables[0].Rows[i][VentilationDbConstNames.IS_SMALL].ToString());
                    //是否工作面风量低于计划风量，风速与《煤矿安全规程》规定不符
                    this.fpVentilation.Sheets[0].Cells[rowDetailStartIndex + i, ++index].Text = LibPanels.DataChangeYesNo(ds.Tables[0].Rows[i][VentilationDbConstNames.IS_FOLLOW_RULE].ToString());
                    //是否通风断面小于设计断面的2/3
                    this.fpVentilation.Sheets[0].Cells[rowDetailStartIndex + i, ++index].Text = ds.Tables[0].Rows[i][VentilationDbConstNames.FAULTAGE_AREA].ToString();
                    //是否工作面风量低于计划风量，风速与《煤矿安全规程》规定不符
                    this.fpVentilation.Sheets[0].Cells[rowDetailStartIndex + i, ++index].Text = ds.Tables[0].Rows[i][VentilationDbConstNames.AIR_FLOW].ToString();
                    //工作制式
                    this.fpVentilation.Sheets[0].Cells[rowDetailStartIndex + i, ++index].Text = ds.Tables[0].Rows[i][VentilationDbConstNames.WORK_STYLE].ToString();
                    //班次
                    this.fpVentilation.Sheets[0].Cells[rowDetailStartIndex + i, ++index].Text = ds.Tables[0].Rows[i][VentilationDbConstNames.WORK_TIME].ToString();
                    //填报人
                    this.fpVentilation.Sheets[0].Cells[rowDetailStartIndex + i, ++index].Text = ds.Tables[0].Rows[i][VentilationDbConstNames.SUBMITTER].ToString();
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
                        checkCount = ds.Tables[0].Rows.Count;
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
        /// 实体赋值
        /// </summary>
        private void setMineDataEntityValue()
        {
            int searchCount = rowsCount;
            for (int i = 0; i < rowsCount; i++)
            {
                if (fpVentilation.Sheets[0].Cells[rowDetailStartIndex + i, 0].Text == "True")
                {
                    viEntity.Id = Convert.ToInt32(ds.Tables[0].Rows[i][VentilationDbConstNames.ID]);
                    viEntity = VentilationBLL.selectVentilationInfo(viEntity.Id);
                }
            }
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
            setMineDataEntityValue();
            MineData m = new MineData(viEntity, this.MainForm);

            m.Text = new LibPanels(MineDataPanelName.Ventilation_Change).panelFormName;
            if (DialogResult.OK == m.ShowDialog())
            {
                bindFpGasEmissionDataWithCondition();
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
                    if (fpVentilation.Sheets[0].Cells[rowDetailStartIndex + i, 0].Value != null && (bool)fpVentilation.Sheets[0].Cells[rowDetailStartIndex + i, 0].Value == true)
                    {
                        int id = (int)ds.Tables[0].Rows[i][VentilationDbConstNames.ID];
                        bResult = VentilationBLL.deleteVentilationInfo(id);
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
