// ******************************************************************
// 概  述：井下数据瓦斯信息管理
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
using LibCommonControl;

namespace LibPanels
{
    public partial class GasInfoManagement : BaseForm
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
        MineDataEntity mdEntity = new MineDataEntity();
        GasDataEntity gdEntity = new GasDataEntity();
        //***********************************

        /// <summary>
        /// 构造方法
        /// </summary>
        public GasInfoManagement(MainFrm mainFrm)
        {
            this.MainForm = mainFrm;

            InitializeComponent();

            dataPager1.FrmChild_EventHandler += FrmParent_EventHandler;

            //窗体属性设置
            LibCommon.FormDefaultPropertiesSetter.SetManagementFormDefaultProperties(this, Const_OP.GAS_DATA_MANAGEMENT);

            //Farpoint属性设置
            LibCommon.FarpointDefaultPropertiesSetter.SetFpDefaultProperties(this.fpGasData, LibCommon.Const_OP.GAS_DATA_FARPOINT, rowDetailStartIndex);

            _filterColunmIdxs = new int[]
            {
                1,
                13,
                14,
                15,
            };
            //禁用选择颜色相关控件
            farpointFilter1.EnableChooseColorCtrls(false);
            //设置自动隐藏过滤条件
            FarpointDefaultPropertiesSetter.SetFpFilterHideProperties(this.fpGasData, _filterColunmIdxs);

            _queryConditions.Show = bindFpGasDataWithCondition;
        }

        /// <summary>
        /// 初始化
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MineDataManagement_Load(object sender, EventArgs e)
        {
            this.bindFpGasDataWithCondition();
        }

        /// <summary>
        /// 委托事件
        /// </summary>
        /// <param name="sender"></param>
        private void FrmParent_EventHandler(object sender)
        {
            bindFpGasDataWithCondition();
        }

        /// <summary>
        /// farpoint数据绑定
        /// </summary>
        //private void bindFpGasData()
        //{
        //    FarPointOperate.farPointClear(fpGasData, rowDetailStartIndex, rowsCount);
        //    checkCount = 0;
        //    chkSelAll.Checked = false;
        //    // ※分页必须
        //    _iRecordCount = GasDataBLL.selectGasData().Tables[0].Rows.Count;

        //    // ※分页必须
        //    dataPager1.PageControlInit(_iRecordCount);
        //    int iStartIndex = dataPager1.getStartIndex();
        //    int iEndIndex = dataPager1.getEndIndex();
        //    ds = GasDataBLL.selectGasData(iStartIndex, iEndIndex);
        //    rowsCount = ds.Tables[0].Rows.Count;
        //    FarPointOperate.farPointReAdd(fpGasData, rowDetailStartIndex, rowsCount);
        //    if (rowsCount > 0)
        //    {
        //        FarPoint.Win.Spread.CellType.CheckBoxCellType ckbxcell = new FarPoint.Win.Spread.CellType.CheckBoxCellType();
        //        ckbxcell.ThreeState = false;
        //        for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
        //        {
        //            int index = 0;
        //            this.fpGasData.Sheets[0].Cells[rowDetailStartIndex + i, index].CellType = ckbxcell;
        //            //巷道名称
        //            this.fpGasData.Sheets[0].Cells[rowDetailStartIndex + i, ++index].Text = TunnelInfoBLL.selectTunnelInfoByTunnelID(Convert.ToInt32(ds.Tables[0].Rows[i][GasDataDbConstNames.TUNNEL_ID])).TunnelName;
        //            if (LibPanels.checkCoordinate(ds.Tables[0].Rows[i][GasDataDbConstNames.X].ToString(), ds.Tables[0].Rows[i][GasDataDbConstNames.Y].ToString(), ds.Tables[0].Rows[i][GasDataDbConstNames.Z].ToString()))
        //            {
        //                //坐标X
        //                this.fpGasData.Sheets[0].Cells[rowDetailStartIndex + i, ++index].Text = ds.Tables[0].Rows[i][GasDataDbConstNames.X].ToString();
        //                //坐标Y
        //                this.fpGasData.Sheets[0].Cells[rowDetailStartIndex + i, ++index].Text = ds.Tables[0].Rows[i][GasDataDbConstNames.Y].ToString();
        //                //坐标Z
        //                this.fpGasData.Sheets[0].Cells[rowDetailStartIndex + i, ++index].Text = ds.Tables[0].Rows[i][GasDataDbConstNames.Z].ToString();
        //            }
        //            else
        //            {
        //                index = index + 3;
        //            }
        //            //时间
        //            this.fpGasData.Sheets[0].Cells[rowDetailStartIndex + i, ++index].Text = ds.Tables[0].Rows[i][GasDataDbConstNames.DATETIME].ToString().Substring(0, ds.Tables[0].Rows[i][GasDataDbConstNames.DATETIME].ToString().IndexOf(' '));
        //            //瓦斯探头断电次数
        //            this.fpGasData.Sheets[0].Cells[rowDetailStartIndex + i, ++index].Text = ds.Tables[0].Rows[i][GasDataDbConstNames.POWER_FALIURE].ToString();
        //            //煤样类型

        //            //吸钻预兆次数
        //            this.fpGasData.Sheets[0].Cells[rowDetailStartIndex + i, ++index].Text = ds.Tables[0].Rows[i][GasDataDbConstNames.DRILL_TIMES].ToString();
        //            //瓦斯忽大忽小预兆次数
        //            this.fpGasData.Sheets[0].Cells[rowDetailStartIndex + i, ++index].Text = ds.Tables[0].Rows[i][GasDataDbConstNames.GAS_TIMES].ToString();
        //            //气温下降预兆次数
        //            this.fpGasData.Sheets[0].Cells[rowDetailStartIndex + i, ++index].Text = ds.Tables[0].Rows[i][GasDataDbConstNames.TEMP_DOWN_TIMES].ToString();
        //            //煤炮频繁预兆次数
        //            this.fpGasData.Sheets[0].Cells[rowDetailStartIndex + i, ++index].Text = ds.Tables[0].Rows[i][GasDataDbConstNames.COAL_BANG_TIMES].ToString();
        //            //喷孔次数
        //            this.fpGasData.Sheets[0].Cells[rowDetailStartIndex + i, ++index].Text = ds.Tables[0].Rows[i][GasDataDbConstNames.CRATER_TIMES].ToString();
        //            //顶钻次数
        //            this.fpGasData.Sheets[0].Cells[rowDetailStartIndex + i, ++index].Text = ds.Tables[0].Rows[i][GasDataDbConstNames.STOPER_TIMES].ToString();
        //            //顶钻次数
        //            this.fpGasData.Sheets[0].Cells[rowDetailStartIndex + i, ++index].Text = ds.Tables[0].Rows[i][GasDataDbConstNames.GAS_THICKNESS].ToString();
        //            //工作制式
        //            this.fpGasData.Sheets[0].Cells[rowDetailStartIndex + i, ++index].Text = ds.Tables[0].Rows[i][GasDataDbConstNames.WORK_STYLE].ToString();
        //            //班次
        //            this.fpGasData.Sheets[0].Cells[rowDetailStartIndex + i, ++index].Text = ds.Tables[0].Rows[i][GasDataDbConstNames.WORK_TIME].ToString();
        //            //填报人
        //            this.fpGasData.Sheets[0].Cells[rowDetailStartIndex + i, ++index].Text = ds.Tables[0].Rows[i][GasDataDbConstNames.SUBMITTER].ToString();
        //        }
        //    }
        //    setButtenEnable();
        //}

        /// <summary>
        ///  farpoint带条件数据绑定
        /// </summary>
        private void bindFpGasDataWithCondition()
        {
            FarPointOperate.farPointClear(fpGasData, rowDetailStartIndex, rowsCount);
            checkCount = 0;
            chkSelAll.Checked = false;
            // ※分页必须
            _iRecordCount = GasDataBLL.selectGasDataWithCondition(_queryConditions.TunnelId, _queryConditions.DefaultStartTime, _queryConditions.DefaultEndTime).Tables[0].Rows.Count;

            // ※分页必须
            dataPager1.PageControlInit(_iRecordCount);
            int iStartIndex = dataPager1.getStartIndex();
            int iEndIndex = dataPager1.getEndIndex();
            ds = GasDataBLL.selectGasDataWithCondition(iStartIndex, iEndIndex, _queryConditions.TunnelId, _queryConditions.DefaultStartTime, _queryConditions.DefaultEndTime);
            rowsCount = ds.Tables[0].Rows.Count;
            FarPointOperate.farPointReAdd(fpGasData, rowDetailStartIndex, rowsCount);
            if (rowsCount > 0)
            {
                FarPoint.Win.Spread.CellType.CheckBoxCellType ckbxcell = new FarPoint.Win.Spread.CellType.CheckBoxCellType();
                ckbxcell.ThreeState = false;
                for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                {
                    int index = 0;
                    this.fpGasData.Sheets[0].Cells[rowDetailStartIndex + i, index].CellType = ckbxcell;
                    //巷道名称
                    this.fpGasData.Sheets[0].Cells[rowDetailStartIndex + i, ++index].Text =
                        BasicInfoManager.getInstance().getTunnelByID(_queryConditions.TunnelId).TunnelName;
                    if (LibPanels.checkCoordinate(ds.Tables[0].Rows[i][GasDataDbConstNames.X].ToString(), ds.Tables[0].Rows[i][GasDataDbConstNames.Y].ToString(), ds.Tables[0].Rows[i][GasDataDbConstNames.Z].ToString()))
                    {
                        //坐标X
                        this.fpGasData.Sheets[0].Cells[rowDetailStartIndex + i, ++index].Text = ds.Tables[0].Rows[i][GasDataDbConstNames.X].ToString();
                        //坐标Y
                        this.fpGasData.Sheets[0].Cells[rowDetailStartIndex + i, ++index].Text = ds.Tables[0].Rows[i][GasDataDbConstNames.Y].ToString();
                        //坐标Z
                        this.fpGasData.Sheets[0].Cells[rowDetailStartIndex + i, ++index].Text = ds.Tables[0].Rows[i][GasDataDbConstNames.Z].ToString();
                    }
                    else
                    {
                        index = index + 3;
                    }
                    //时间
                    this.fpGasData.Sheets[0].Cells[rowDetailStartIndex + i, ++index].Text = ds.Tables[0].Rows[i][GasDataDbConstNames.DATETIME].ToString().Substring(0, ds.Tables[0].Rows[i][GasDataDbConstNames.DATETIME].ToString().IndexOf(' '));
                    //瓦斯探头断电次数
                    this.fpGasData.Sheets[0].Cells[rowDetailStartIndex + i, ++index].Text = ds.Tables[0].Rows[i][GasDataDbConstNames.POWER_FALIURE].ToString();
                    //煤样类型

                    //吸钻预兆次数
                    this.fpGasData.Sheets[0].Cells[rowDetailStartIndex + i, ++index].Text = ds.Tables[0].Rows[i][GasDataDbConstNames.DRILL_TIMES].ToString();
                    //瓦斯忽大忽小预兆次数
                    this.fpGasData.Sheets[0].Cells[rowDetailStartIndex + i, ++index].Text = ds.Tables[0].Rows[i][GasDataDbConstNames.GAS_TIMES].ToString();
                    //气温下降预兆次数
                    this.fpGasData.Sheets[0].Cells[rowDetailStartIndex + i, ++index].Text = ds.Tables[0].Rows[i][GasDataDbConstNames.TEMP_DOWN_TIMES].ToString();
                    //煤炮频繁预兆次数
                    this.fpGasData.Sheets[0].Cells[rowDetailStartIndex + i, ++index].Text = ds.Tables[0].Rows[i][GasDataDbConstNames.COAL_BANG_TIMES].ToString();
                    //喷孔次数
                    this.fpGasData.Sheets[0].Cells[rowDetailStartIndex + i, ++index].Text = ds.Tables[0].Rows[i][GasDataDbConstNames.CRATER_TIMES].ToString();
                    //顶钻次数
                    this.fpGasData.Sheets[0].Cells[rowDetailStartIndex + i, ++index].Text = ds.Tables[0].Rows[i][GasDataDbConstNames.STOPER_TIMES].ToString();
                    //顶钻次数
                    this.fpGasData.Sheets[0].Cells[rowDetailStartIndex + i, ++index].Text = ds.Tables[0].Rows[i][GasDataDbConstNames.GAS_THICKNESS].ToString();
                    //工作制式
                    this.fpGasData.Sheets[0].Cells[rowDetailStartIndex + i, ++index].Text = ds.Tables[0].Rows[i][GasDataDbConstNames.WORK_STYLE].ToString();
                    //班次
                    this.fpGasData.Sheets[0].Cells[rowDetailStartIndex + i, ++index].Text = ds.Tables[0].Rows[i][GasDataDbConstNames.WORK_TIME].ToString();
                    //填报人
                    this.fpGasData.Sheets[0].Cells[rowDetailStartIndex + i, ++index].Text = ds.Tables[0].Rows[i][GasDataDbConstNames.SUBMITTER].ToString();
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
        /// 瓦斯实体赋值
        /// </summary>
        private void setMineDataEntityValue()
        {
            for (int i = 0; i < rowsCount; i++)
            {
                if (fpGasData.Sheets[0].Cells[rowDetailStartIndex + i, 0].Value != null && (bool)fpGasData.Sheets[0].Cells[rowDetailStartIndex + i, 0].Value == true)
                {
                    //主键
                    if (int.TryParse(ds.Tables[0].Rows[i][GasDataDbConstNames.ID].ToString(), out tmpInt))
                    {
                        gdEntity.Id = tmpInt;
                        tmpInt = 0;
                    }
                    //绑定巷道ID
                    if (int.TryParse(ds.Tables[0].Rows[i][GasDataDbConstNames.TUNNEL_ID].ToString(), out tmpInt))
                    {
                        gdEntity.TunnelID = tmpInt;
                        tmpInt = 0;
                    }
                    //坐标X
                    if (double.TryParse(ds.Tables[0].Rows[i][GasDataDbConstNames.X].ToString(), out tmpDouble))
                    {
                        gdEntity.CoordinateX = tmpDouble;
                        tmpDouble = 0;
                    }
                    //坐标Y
                    if (double.TryParse(ds.Tables[0].Rows[i][GasDataDbConstNames.Y].ToString(), out tmpDouble))
                    {
                        gdEntity.CoordinateY = tmpDouble;
                        tmpDouble = 0;
                    }
                    //坐标Z
                    if (double.TryParse(ds.Tables[0].Rows[i][GasDataDbConstNames.Z].ToString(), out tmpDouble))
                    {
                        gdEntity.CoordinateZ = tmpDouble;
                        tmpDouble = 0;
                    }
                    //工作制式
                    gdEntity.WorkStyle = ds.Tables[0].Rows[i][GasDataDbConstNames.WORK_STYLE].ToString();
                    //班次
                    gdEntity.WorkTime = ds.Tables[0].Rows[i][GasDataDbConstNames.WORK_TIME].ToString();
                    //队别名称
                    gdEntity.TeamName = ds.Tables[0].Rows[i][GasDataDbConstNames.TEAM_NAME].ToString();
                    //填报人
                    gdEntity.Submitter = ds.Tables[0].Rows[i][GasDataDbConstNames.SUBMITTER].ToString();
                    //提交日期
                    if (DateTime.TryParse(ds.Tables[0].Rows[i][GasDataDbConstNames.DATETIME].ToString(), out tmpDtp))
                    {
                        gdEntity.Datetime = tmpDtp;
                        tmpDtp = DateTime.Now;
                    }
                    //瓦斯探头断电次数
                    if (double.TryParse(ds.Tables[0].Rows[i][GasDataDbConstNames.POWER_FALIURE].ToString(), out tmpDouble))
                    {
                        gdEntity.PowerFailure = tmpDouble;
                        tmpDouble = 0;
                    }
                    //吸钻预兆次数
                    if (double.TryParse(ds.Tables[0].Rows[i][GasDataDbConstNames.DRILL_TIMES].ToString(), out tmpDouble))
                    {
                        gdEntity.DrillTimes = tmpDouble;
                        tmpDouble = 0;
                    }
                    //瓦斯忽大忽小预兆次数
                    if (double.TryParse(ds.Tables[0].Rows[i][GasDataDbConstNames.GAS_TIMES].ToString(), out tmpDouble))
                    {
                        gdEntity.GasTimes = tmpDouble;
                        tmpDouble = 0;
                    }
                    //气温下降预兆次数
                    if (double.TryParse(ds.Tables[0].Rows[i][GasDataDbConstNames.TEMP_DOWN_TIMES].ToString(), out tmpDouble))
                    {
                        gdEntity.TempDownTimes = tmpDouble;
                        tmpDouble = 0;
                    }
                    //煤炮频繁预兆次数
                    if (double.TryParse(ds.Tables[0].Rows[i][GasDataDbConstNames.COAL_BANG_TIMES].ToString(), out tmpDouble))
                    {
                        gdEntity.CoalBangTimes = tmpDouble;
                        tmpDouble = 0;
                    }
                    //喷孔次数
                    if (double.TryParse(ds.Tables[0].Rows[i][GasDataDbConstNames.CRATER_TIMES].ToString(), out tmpDouble))
                    {
                        gdEntity.CraterTimes = tmpDouble;
                        tmpDouble = 0;
                    }
                    //顶钻次数
                    if (double.TryParse(ds.Tables[0].Rows[i][GasDataDbConstNames.STOPER_TIMES].ToString(), out tmpDouble))
                    {
                        gdEntity.StoperTimes = tmpDouble;
                        tmpDouble = 0;
                    }
                    //瓦斯浓度
                    if (double.TryParse(ds.Tables[0].Rows[i][GasDataDbConstNames.GAS_THICKNESS].ToString(), out tmpDouble))
                    {
                        gdEntity.GasThickness = tmpDouble;
                        tmpDouble = 0;
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
                        this.fpGasData.Sheets[0].Cells[rowDetailStartIndex + i, 0].Value = ((CheckBox)sender).Checked;
                        checkCount = ds.Tables[0].Rows.Count;
                    }
                    //checkbox未选中
                    else
                    {
                        this.fpGasData.Sheets[0].Cells[rowDetailStartIndex + i, 0].Value = ((CheckBox)sender).Checked;
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
            m.Text = new LibPanels(MineDataPanelName.GasData).panelFormName;
            if (DialogResult.OK == m.ShowDialog())
            {
                bindFpGasDataWithCondition();
                this.dataPager1.btnLastPage_Click(sender, e);
                //添加后重置焦点
                FarPointOperate.farPointFocusSetAdd(fpGasData, rowDetailStartIndex, rowsCount);
            }
        }

        /// <summary>
        /// 修改按钮事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tsBtnModify_Click(object sender, EventArgs e)
        {
            //实体赋值
            setMineDataEntityValue();
            _tmpRowIndex = fpGasData.ActiveSheet.ActiveRowIndex;
            MineData m = new MineData(gdEntity, this.MainForm);
            m.Text = new LibPanels(MineDataPanelName.GasData_Change).panelFormName;
            if (DialogResult.OK == m.ShowDialog())
            {
                bindFpGasDataWithCondition();
                //修改后重置焦点
                FarPointOperate.farPointFocusSetChange(fpGasData, _tmpRowIndex);
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
                    //被选中的
                    if (fpGasData.Sheets[0].Cells[rowDetailStartIndex + i, 0].Value != null && (bool)fpGasData.Sheets[0].Cells[rowDetailStartIndex + i, 0].Value == true)
                    {
                        _tmpRowIndex = fpGasData.ActiveSheet.ActiveRowIndex;
                        int id = (int)ds.Tables[0].Rows[i][GasDataDbConstNames.ID];
                        //删除
                        bResult = GasDataBLL.deleteGasDataInfo(id);
                    }
                }
                if (bResult)
                {
                    //TODO:删除成功
                }
                bindFpGasDataWithCondition();
                //删除后重置焦点
                FarPointOperate.farPointFocusSetDel(fpGasData, _tmpRowIndex);
            }
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
        /// 刷新按钮事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tsBtnRefresh_Click(object sender, EventArgs e)
        {
            bindFpGasDataWithCondition();
        }

        /// <summary>
        /// 导出按钮事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tsBtnExport_Click(object sender, EventArgs e)
        {
            if (FileExport.fileExport(fpGasData, true))
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
                FarpointDefaultPropertiesSetter.SetFpFilterHideProperties(this.fpGasData, _filterColunmIdxs);

            }
            else//未选中时，根据用户自定义的颜色进行分类显示
            {
                //启用选择颜色相关控件
                farpointFilter1.EnableChooseColorCtrls(true);
                //设置自定义过滤条件
                FarpointDefaultPropertiesSetter.SetFpCustomFilterProperties(this.fpGasData, farpointFilter1.GetSelectedFitColor(), farpointFilter1.GetSelectedNotFitColor(), _filterColunmIdxs);
            }
        }

        private void farpointFilter1_OnClickClearFilterBtn(object sender, EventArgs arg)
        {
            //清空过滤条件
            this.fpGasData.ActiveSheet.RowFilter.ResetFilter();
        }

        private void farpointFilter1_OnClickFitColorBtnOK(object sender, EventArgs arg)
        {
            //根据新的颜色值设置自动隐藏过滤条件
            FarpointDefaultPropertiesSetter.SetFpCustomFilterProperties(this.fpGasData, farpointFilter1.GetSelectedFitColor(), farpointFilter1.GetSelectedNotFitColor(), _filterColunmIdxs);
        }

        private void farpointFilter1_OnClickNotFitColorBtnOK(object sender, EventArgs arg)
        {
            //根据新的颜色值设置自动隐藏过滤条件
            FarpointDefaultPropertiesSetter.SetFpCustomFilterProperties(this.fpGasData, farpointFilter1.GetSelectedFitColor(), farpointFilter1.GetSelectedNotFitColor(), _filterColunmIdxs);
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

        private void dataPager1_Load(object sender, EventArgs e)
        {

        }
    }
}
