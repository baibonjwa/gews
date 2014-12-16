using System;
using System.Data;
using System.Linq;
using System.Windows.Forms;
using FarPoint.Win;
using FarPoint.Win.Spread;
using FarPoint.Win.Spread.CellType;
using LibBusiness;
using LibCommon;
using LibCommonControl;
using LibEntity;

namespace LibPanels
{
    public partial class GasInfoManagement : BaseForm
    {
        //***********************************
        private readonly int[] _filterColunmIdxs;
        private readonly GasData gdEntity = new GasData();
        private int _iRecordCount;
        private int _tmpRowIndex;
        private int checkCount; //选择行数
        private LibEntity.MineData mdEntity = new LibEntity.MineData();
        private int rowDetailStartIndex = 4;
        private int rowsCount; //数据行数
        private double tmpDouble;
        private DateTime tmpDtp = DateTime.Now;
        private int tmpInt;
        private GasData[] gasDatas;
        public static GasData gasEntity = new GasData();
        //需要过滤的列索引
        //***********************************

        /// <summary>
        ///     构造方法
        /// </summary>
        public GasInfoManagement(MainFrm mainFrm)
        {
            MainForm = mainFrm;

            InitializeComponent();

            dataPager1.FrmChild_EventHandler += FrmParent_EventHandler;

            //窗体属性设置
            FormDefaultPropertiesSetter.SetManagementFormDefaultProperties(this, Const_OP.GAS_DATA_MANAGEMENT);

            //Farpoint属性设置
            FarpointDefaultPropertiesSetter.SetFpDefaultProperties(fpGasData, Const_OP.GAS_DATA_FARPOINT,
                rowDetailStartIndex);

            _filterColunmIdxs = new[]
            {
                1,
                13,
                14,
                15
            };
            //禁用选择颜色相关控件
            farpointFilter1.EnableChooseColorCtrls(false);
            //设置自动隐藏过滤条件
            FarpointDefaultPropertiesSetter.SetFpFilterHideProperties(fpGasData, _filterColunmIdxs);

            _queryConditions.Show = bindFpGasDataWithCondition;
        }

        /// <summary>
        ///     初始化
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MineDataManagement_Load(object sender, EventArgs e)
        {
            bindFpGasDataWithCondition();
        }

        /// <summary>
        ///     委托事件
        /// </summary>
        /// <param name="sender"></param>
        private void FrmParent_EventHandler(object sender)
        {
            bindFpGasDataWithCondition();
        }

        private void bindFpGasDataWithCondition()
        {
            FarPointOperate.farPointClear(fpGasData, rowDetailStartIndex, rowsCount);
            checkCount = 0;
            chkSelAll.Checked = false;
            // ※分页必须
            _iRecordCount = GasData.GetRecordCount();

            // ※分页必须
            dataPager1.PageControlInit(_iRecordCount);
            int iStartIndex = dataPager1.getStartIndex();
            int iEndIndex = dataPager1.getEndIndex();
            gasDatas = GasData.SlicedFindByCondition(iStartIndex, iEndIndex, _queryConditions.TunnelId,
                Convert.ToDateTime(_queryConditions.DefaultStartTime),
                Convert.ToDateTime(_queryConditions.DefaultEndTime));

            rowsCount = gasDatas.Length;

            FarPointOperate.farPointReAdd(fpGasData, rowDetailStartIndex, rowsCount);
            if (rowsCount > 0)
            {
                var ckbxcell = new CheckBoxCellType { ThreeState = false };
                for (int i = 0; i < gasDatas.Length; i++)
                {
                    int index = 0;
                    fpGasData.Sheets[0].Cells[rowDetailStartIndex + i, index].CellType = ckbxcell;
                    //巷道名称
                    fpGasData.Sheets[0].Cells[rowDetailStartIndex + i, ++index].Text =
                        BasicInfoManager.getInstance().getTunnelByID(_queryConditions.TunnelId).TunnelName;
                    if (LibPanels.checkCoordinate(gasDatas[i].CoordinateX, gasDatas[i].CoordinateY,
                        gasDatas[i].CoordinateZ))
                    {
                        //坐标X
                        fpGasData.Sheets[0].Cells[rowDetailStartIndex + i, ++index].Text =
                            gasDatas[i].CoordinateX.ToString();
                        //坐标Y
                        fpGasData.Sheets[0].Cells[rowDetailStartIndex + i, ++index].Text =
                            gasDatas[i].CoordinateY.ToString();
                        //坐标Z
                        fpGasData.Sheets[0].Cells[rowDetailStartIndex + i, ++index].Text =
                            gasDatas[i].CoordinateZ.ToString();
                    }
                    else
                    {
                        index = index + 3;
                    }
                    //时间
                    fpGasData.Sheets[0].Cells[rowDetailStartIndex + i, ++index].Text =
                        gasDatas[i].Datetime.ToString("yyyy-MM-dd hh:mm:ss");

                    //瓦斯探头断电次数
                    fpGasData.Sheets[0].Cells[rowDetailStartIndex + i, ++index].Text =
                        gasDatas[i].PowerFailure.ToString();
                    //煤样类型

                    //吸钻预兆次数
                    fpGasData.Sheets[0].Cells[rowDetailStartIndex + i, ++index].Text =
                        gasDatas[i].DrillTimes.ToString();
                    //瓦斯忽大忽小预兆次数
                    fpGasData.Sheets[0].Cells[rowDetailStartIndex + i, ++index].Text =
                        gasDatas[i].GasTimes.ToString();
                    //气温下降预兆次数
                    fpGasData.Sheets[0].Cells[rowDetailStartIndex + i, ++index].Text =
                        gasDatas[i].TempDownTimes.ToString();
                    //煤炮频繁预兆次数
                    fpGasData.Sheets[0].Cells[rowDetailStartIndex + i, ++index].Text =
                        gasDatas[i].CoalBangTimes.ToString();
                    //喷孔次数
                    fpGasData.Sheets[0].Cells[rowDetailStartIndex + i, ++index].Text =
                        gasDatas[i].CraterTimes.ToString();
                    //顶钻次数
                    fpGasData.Sheets[0].Cells[rowDetailStartIndex + i, ++index].Text =
                        gasDatas[i].StoperTimes.ToString();
                    //顶钻次数
                    fpGasData.Sheets[0].Cells[rowDetailStartIndex + i, ++index].Text =
                        gasDatas[i].GasThickness.ToString();
                    //工作制式
                    fpGasData.Sheets[0].Cells[rowDetailStartIndex + i, ++index].Text =
                        gasDatas[i].WorkStyle;
                    //班次
                    fpGasData.Sheets[0].Cells[rowDetailStartIndex + i, ++index].Text =
                        gasDatas[i].WorkTime;
                    //填报人
                    fpGasData.Sheets[0].Cells[rowDetailStartIndex + i, ++index].Text =
                        gasDatas[i].Submitter;
                }
            }
            setButtenEnable();
        }

        /// <summary>
        ///     farpoint中checkbox选中对全选反选的影响
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void fpGasEmissionData_ButtonClicked(object sender, EditorNotifyEventArgs e)
        {
            if (e.EditingControl is FpCheckBox)
            {
                var fpChk = (FpCheckBox)e.EditingControl;
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
        ///     设置按钮可操作性
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
        ///     全选反选
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
                        fpGasData.Sheets[0].Cells[rowDetailStartIndex + i, 0].Value = ((CheckBox)sender).Checked;
                        checkCount = gasDatas.Length;
                    }
                    //checkbox未选中
                    else
                    {
                        fpGasData.Sheets[0].Cells[rowDetailStartIndex + i, 0].Value = ((CheckBox)sender).Checked;
                        checkCount = 0;
                    }
                }
            }
            setButtenEnable();
        }

        /// <summary>
        ///     添加按钮事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tsBtnAdd_Click(object sender, EventArgs e)
        {
            //MineData m = new MineData();
            var m = new MineDataSimple(MainForm);
            m.Text = new LibPanels(MineDataPanelName.GasData).panelFormName;
            if (DialogResult.OK == m.ShowDialog())
            {
                bindFpGasDataWithCondition();
                dataPager1.btnLastPage_Click(sender, e);
                //添加后重置焦点
                FarPointOperate.farPointFocusSetAdd(fpGasData, rowDetailStartIndex, rowsCount);
            }
        }

        /// <summary>
        ///     修改按钮事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tsBtnModify_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < rowsCount; i++)
            {
                if (fpGasData.Sheets[0].Cells[rowDetailStartIndex + i, 0].Value != null &&
                    (bool)fpGasData.Sheets[0].Cells[rowDetailStartIndex + i, 0].Value == true)
                {
                    gasEntity = gasDatas[i];
                }
            }
            var m = new MineData(gasEntity, MainForm);
            _tmpRowIndex = fpGasData.ActiveSheet.ActiveRowIndex;

            m.Text = new LibPanels(MineDataPanelName.GasData_Change).panelFormName;
            if (DialogResult.OK == m.ShowDialog())
            {
                bindFpGasDataWithCondition();
                //修改后重置焦点
                FarPointOperate.farPointFocusSetChange(fpGasData, _tmpRowIndex);
            }
        }

        /// <summary>
        ///     删除按钮事件
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
                    if (fpGasData.Sheets[0].Cells[rowDetailStartIndex + i, 0].Value != null &&
                        (bool)fpGasData.Sheets[0].Cells[rowDetailStartIndex + i, 0].Value)
                    {
                        _tmpRowIndex = fpGasData.ActiveSheet.ActiveRowIndex;
                        gasDatas[i].Delete();
                        bResult = true;
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
        ///     退出按钮事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tsBtnExit_Click(object sender, EventArgs e)
        {
            Close();
        }

        /// <summary>
        ///     刷新按钮事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tsBtnRefresh_Click(object sender, EventArgs e)
        {
            bindFpGasDataWithCondition();
        }

        /// <summary>
        ///     导出按钮事件
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

        private void btnOK_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void dataPager1_Load(object sender, EventArgs e)
        {
        }

        #region Farpoint自动过滤功能

        private void farpointFilter1_OnCheckFilterChanged(object sender, EventArgs arg)
        {
            var chk = (CheckBox)sender;
            //当Checkbox选中时，筛选过程中则将不符合条件的数据隐藏
            if (chk.Checked)
            {
                //禁用选择颜色相关控件
                farpointFilter1.EnableChooseColorCtrls(false);
                //设置自动隐藏过滤条件
                FarpointDefaultPropertiesSetter.SetFpFilterHideProperties(fpGasData, _filterColunmIdxs);
            }
            else //未选中时，根据用户自定义的颜色进行分类显示
            {
                //启用选择颜色相关控件
                farpointFilter1.EnableChooseColorCtrls(true);
                //设置自定义过滤条件
                FarpointDefaultPropertiesSetter.SetFpCustomFilterProperties(fpGasData,
                    farpointFilter1.GetSelectedFitColor(), farpointFilter1.GetSelectedNotFitColor(), _filterColunmIdxs);
            }
        }

        private void farpointFilter1_OnClickClearFilterBtn(object sender, EventArgs arg)
        {
            //清空过滤条件
            fpGasData.ActiveSheet.RowFilter.ResetFilter();
        }

        private void farpointFilter1_OnClickFitColorBtnOK(object sender, EventArgs arg)
        {
            //根据新的颜色值设置自动隐藏过滤条件
            FarpointDefaultPropertiesSetter.SetFpCustomFilterProperties(fpGasData, farpointFilter1.GetSelectedFitColor(),
                farpointFilter1.GetSelectedNotFitColor(), _filterColunmIdxs);
        }

        private void farpointFilter1_OnClickNotFitColorBtnOK(object sender, EventArgs arg)
        {
            //根据新的颜色值设置自动隐藏过滤条件
            FarpointDefaultPropertiesSetter.SetFpCustomFilterProperties(fpGasData, farpointFilter1.GetSelectedFitColor(),
                farpointFilter1.GetSelectedNotFitColor(), _filterColunmIdxs);
        }

        #endregion
    }
}