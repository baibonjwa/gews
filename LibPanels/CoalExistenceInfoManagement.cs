// ******************************************************************
// 概  述：井下数据煤层赋存信息管理
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
    public partial class CoalExistenceInfoManagement : BaseForm
    {
        //***********************************
        private int _iRecordCount = 0;
        int rowsCount = 0;      //数据行数
        int checkCount = 0;     //选择行数
        int rowDetailStartIndex = 4;
        int _tmpRowIndex = 0;
        //需要过滤的列索引
        private int[] _filterColunmIdxs = null;
        //DataSet ds = new DataSet();
        private CoalExistence[] coalExistences;
        public static Tunnel tunnelEntity = new Tunnel();
        public static CoalExistence ceEntity = new CoalExistence();

        //***********************************

        /// <summary>
        /// 构造方法
        /// </summary>
        public CoalExistenceInfoManagement(MainFrm mainFrm)
        {
            this.MainForm = mainFrm;

            InitializeComponent();

            dataPager1.FrmChild_EventHandler += FrmParent_EventHandler;

            //窗体属性设置
            LibCommon.FormDefaultPropertiesSetter.SetManagementFormDefaultProperties(this, Const_GE.COAL_EXISTENCE_MANAGEMENT);

            //Farpoint属性设置
            LibCommon.FarpointDefaultPropertiesSetter.SetFpDefaultProperties(this.fpCoalExistence, LibCommon.Const_GE.COAL_EXISTENCE_FARPOINT, rowDetailStartIndex);

            _filterColunmIdxs = new int[]
            {
                1,
                14,
                15,
                16,
            };
            //禁用选择颜色相关控件
            farpointFilter1.EnableChooseColorCtrls(false);
            //设置自动隐藏过滤条件
            FarpointDefaultPropertiesSetter.SetFpFilterHideProperties(this.fpCoalExistence, _filterColunmIdxs);

            _queryConditions.Show = bindFpGasEmissionDataWithCondition;
        }

        /// <summary>
        /// 初始化
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MineDataManagement_Load(object sender, EventArgs e)
        {
            bindFpGasEmissionDataWithCondition();
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
            FarPointOperate.farPointClear(fpCoalExistence, rowDetailStartIndex, rowsCount);
            checkCount = 0;
            chkSelAll.Checked = false;


            // ※分页必须
            _iRecordCount = CoalExistence.GetRecordCount();
            // ※分页必须
            dataPager1.PageControlInit(_iRecordCount);
            int iStartIndex = dataPager1.getStartIndex();
            int iEndIndex = dataPager1.getEndIndex();

            coalExistences = CoalExistence.SlicedFindByCondition(iStartIndex, iEndIndex, _queryConditions.TunnelId, Convert.ToDateTime(_queryConditions.DefaultStartTime),
   Convert.ToDateTime(_queryConditions.DefaultEndTime));

            rowsCount = coalExistences.Length;
            FarPointOperate.farPointReAdd(fpCoalExistence, rowDetailStartIndex, rowsCount);
            if (rowsCount > 0)
            {
                FarPoint.Win.Spread.CellType.CheckBoxCellType ckbxcell = new FarPoint.Win.Spread.CellType.CheckBoxCellType();
                ckbxcell.ThreeState = false;
                for (int i = 0; i < coalExistences.Length; i++)
                {
                    int index = 0;
                    this.fpCoalExistence.Sheets[0].Cells[rowDetailStartIndex + i, index].CellType = ckbxcell;

                    fpCoalExistence.Sheets[0].Cells[rowDetailStartIndex + i, ++index].Text =
                        coalExistences[i].Tunnel.TunnelName;

                    if (LibPanels.checkCoordinate(coalExistences[i].CoordinateX, coalExistences[i].CoordinateY, coalExistences[i].CoordinateZ))
                    {
                        //坐标X
                        fpCoalExistence.Sheets[0].Cells[rowDetailStartIndex + i, ++index].Text =
                            coalExistences[i].CoordinateX.ToString();
                        //坐标Y
                        this.fpCoalExistence.Sheets[0].Cells[rowDetailStartIndex + i, ++index].Text =
                            coalExistences[i].CoordinateY.ToString();
                        //坐标Z
                        this.fpCoalExistence.Sheets[0].Cells[rowDetailStartIndex + i, ++index].Text =
                            coalExistences[i].CoordinateZ.ToString();
                    }
                    else
                    {
                        index = index + 3;
                    }
                    //时间
                    fpCoalExistence.Sheets[0].Cells[rowDetailStartIndex + i, ++index].Text =
                        coalExistences[i].Datetime.ToString();
                    //是否层理紊乱
                    fpCoalExistence.Sheets[0].Cells[rowDetailStartIndex + i, ++index].Text =
                        LibPanels.DataChangeYesNo(coalExistences[i].IsLevelDisorder);
                    //煤厚变化
                    fpCoalExistence.Sheets[0].Cells[rowDetailStartIndex + i, ++index].Text =
                        coalExistences[i].CoalThickChange.ToString();
                    //软分层（构造煤）厚度
                    fpCoalExistence.Sheets[0].Cells[rowDetailStartIndex + i, ++index].Text =
                        coalExistences[i].TectonicCoalThick.ToString();
                    //是否软分层（构造煤）层位是否发生变化
                    fpCoalExistence.Sheets[0].Cells[rowDetailStartIndex + i, ++index].Text =
                        LibPanels.DataChangeYesNo(coalExistences[i].IsLevelChange);
                    //煤体破坏类型
                    fpCoalExistence.Sheets[0].Cells[rowDetailStartIndex + i, ++index].Text =
                        coalExistences[i].CoalDistoryLevel;
                    //是否煤层走向、倾角突然急剧变化
                    fpCoalExistence.Sheets[0].Cells[rowDetailStartIndex + i, ++index].Text =
                        LibPanels.DataChangeYesNo(coalExistences[i].IsTowardsChange);
                    //工作制式
                    fpCoalExistence.Sheets[0].Cells[rowDetailStartIndex + i, ++index].Text =
                        coalExistences[i].WorkStyle;
                    //班次
                    fpCoalExistence.Sheets[0].Cells[rowDetailStartIndex + i, ++index].Text =
                        coalExistences[i].WorkTime;
                    //填报人
                    fpCoalExistence.Sheets[0].Cells[rowDetailStartIndex + i, ++index].Text =
                        coalExistences[i].Submitter;
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
                        this.fpCoalExistence.Sheets[0].Cells[rowDetailStartIndex + i, 0].Value = ((CheckBox)sender).Checked;
                        checkCount = coalExistences.Length;
                    }
                    //checkbox未选中
                    else
                    {
                        this.fpCoalExistence.Sheets[0].Cells[rowDetailStartIndex + i, 0].Value = ((CheckBox)sender).Checked;
                        checkCount = 0;
                    }
                }
            }
            setButtenEnable();
        }

        /// <summary>
        /// 煤层赋存实体赋值
        /// </summary>
        //private void setMineDataEntityValue()
        //{
        //    for (int i = 0; i < rowsCount; i++)
        //    {
        //        if (fpCoalExistence.Sheets[0].Cells[rowDetailStartIndex + i, 0].Value != null && (bool)fpCoalExistence.Sheets[0].Cells[rowDetailStartIndex + i, 0].Value == true)
        //        {
        //            ceEntity.Id = Convert.ToInt32(ds.Tables[0].Rows[i][CoalExistenceDbConstNames.ID]);
        //            ceEntity.Tunnel.TunnelId = Convert.ToInt32(ds.Tables[0].Rows[i][CoalExistenceDbConstNames.TUNNEL_ID]);
        //            ceEntity.CoordinateX = Convert.ToDouble(ds.Tables[0].Rows[i][CoalExistenceDbConstNames.COORDINATE_X]);
        //            ceEntity.CoordinateY = Convert.ToDouble(ds.Tables[0].Rows[i][CoalExistenceDbConstNames.COORDINATE_Y]);
        //            ceEntity.CoordinateZ = Convert.ToDouble(ds.Tables[0].Rows[i][CoalExistenceDbConstNames.COORDINATE_Z]);
        //            ceEntity.WorkStyle = ds.Tables[0].Rows[i][GasDataDbConstNames.WORK_STYLE].ToString();
        //            ceEntity.WorkTime = ds.Tables[0].Rows[i][GasDataDbConstNames.WORK_TIME].ToString();
        //            ceEntity.TeamName = ds.Tables[0].Rows[i][GasDataDbConstNames.TEAM_NAME].ToString();
        //            ceEntity.Submitter = ds.Tables[0].Rows[i][GasDataDbConstNames.SUBMITTER].ToString();
        //            ceEntity.Datetime = Convert.ToDateTime(ds.Tables[0].Rows[i][GasDataDbConstNames.DATETIME]);
        //            ceEntity.IsLevelDisorder = Convert.ToInt32(ds.Tables[0].Rows[i][CoalExistenceDbConstNames.IS_LEVEL_DISORDER]);
        //            ceEntity.CoalThickChange = Convert.ToDouble(ds.Tables[0].Rows[i][CoalExistenceDbConstNames.COAL_THICK_CHANGE]);
        //            ceEntity.TectonicCoalThick = Convert.ToDouble(ds.Tables[0].Rows[i][CoalExistenceDbConstNames.TECTONIC_COAL_THICK]);
        //            ceEntity.IsLevelChange = Convert.ToInt32(ds.Tables[0].Rows[i][CoalExistenceDbConstNames.IS_LEVEL_CHANGE]);
        //            ceEntity.CoalDistoryLevel = ds.Tables[0].Rows[i][CoalExistenceDbConstNames.COAL_DISTORY_LEVEL].ToString();
        //            ceEntity.IsTowardsChange = Convert.ToInt32(ds.Tables[0].Rows[i][CoalExistenceDbConstNames.IS_TOWARDS_CHANGE].ToString());
        //        }
        //    }
        //}

        /// <summary>
        ///  添加按钮事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tsBtnAdd_Click(object sender, EventArgs e)
        {
            //MineData m = new MineData();
            MineDataSimple m = new MineDataSimple(this.MainForm);
            m.Text = new LibPanels(MineDataPanelName.CoalExistence).panelFormName;
            if (DialogResult.OK == m.ShowDialog())
            {
                bindFpGasEmissionDataWithCondition();
                this.dataPager1.btnLastPage_Click(sender, e);
                FarPointOperate.farPointFocusSetAdd(fpCoalExistence, rowDetailStartIndex, rowsCount);
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
                if (fpCoalExistence.Sheets[0].Cells[rowDetailStartIndex + i, 0].Value != null &&
                    (bool)fpCoalExistence.Sheets[0].Cells[rowDetailStartIndex + i, 0].Value == true)
                {
                    ceEntity = coalExistences[i];
                }
            }
            MineData m = new MineData(ceEntity, this.MainForm);
            _tmpRowIndex = fpCoalExistence.ActiveSheet.ActiveRowIndex;
            m.Text = new LibPanels(MineDataPanelName.CoalExistence_Change).panelFormName;
            if (DialogResult.OK == m.ShowDialog())
            {
                bindFpGasEmissionDataWithCondition();
                FarPointOperate.farPointFocusSetChange(fpCoalExistence, _tmpRowIndex);
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
                    if (fpCoalExistence.Sheets[0].Cells[rowDetailStartIndex + i, 0].Value != null && (bool)fpCoalExistence.Sheets[0].Cells[rowDetailStartIndex + i, 0].Value == true)
                    {
                        coalExistences[i].Delete();
                        bResult = true;
                    }
                }
                if (bResult)
                {
                    bindFpGasEmissionDataWithCondition();
                    FarPointOperate.farPointFocusSetDel(fpCoalExistence, _tmpRowIndex);
                }
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

        /******************用于Farpoint删除选择列，暂时保留******************
        private void removeColumn()
        {
            int searchCount = rowsCount+2;
            if (searchCount > 0)
            {
                for (int i = 0; i < searchCount; i++)
                {
                    if (rowDetailStartIndex == 3)
                    {
                        continue;
                    }
                    this.fpCoalExistence.Sheets[0].Cells[rowDetailStartIndex + i, 0].CellType = new FarPoint.Win.Spread.CellType.TextCellType();
                    this.fpCoalExistence.Sheets[0].Cells[rowDetailStartIndex + i, 0].ResetText();
                    this.fpCoalExistence.Sheets[0].Cells[rowDetailStartIndex + i, 0].Text = this.fpCoalExistence.Sheets[0].Cells[rowDetailStartIndex + i, 1].Text;
                    this.fpCoalExistence.Sheets[0].Cells[rowDetailStartIndex + i, 1].ResetText();
                }
                fpCoalExistence.Sheets[0].Columns[0].Width = 115;
                fpCoalExistence.Sheets[0].Columns[1].Remove();
            }
        }

        private void addColumn()
        {
            fpCoalExistence.Sheets[0].Columns[1].Add();
            int searchCount = rowsCount+2;
            if (searchCount > 0)
            {
                int rowDetailStartIndex = 2;
                for (int i = 0; i < searchCount; i++)
                {
                    if (rowDetailStartIndex == 3)
                    {
                        continue;
                    }
                    this.fpCoalExistence.Sheets[0].Cells[rowDetailStartIndex + i, 1].Text = this.fpCoalExistence.Sheets[0].Cells[rowDetailStartIndex + i, 0].Text;
                    this.fpCoalExistence.Sheets[0].Cells[rowDetailStartIndex + i, 0].ResetText();
                    if (rowDetailStartIndex == 2)
                    {
                        this.fpCoalExistence.Sheets[0].Cells[rowDetailStartIndex + i, 0].Text = "选择";
                        continue;
                    }
                    this.fpCoalExistence.Sheets[0].Cells[rowDetailStartIndex + i, 0].CellType = new FarPoint.Win.Spread.CellType.CheckBoxCellType();
                }
                this.fpCoalExistence.Sheets[0].AddSpanCell(2, 1, 2, 1);
                this.fpCoalExistence.Sheets[0].Cells[2, 1].Border = this.fpCoalExistence.Sheets[0].Cells[2, 0].Border;
                this.fpCoalExistence.Sheets[0].Columns[1].Width = 115;
                this.fpCoalExistence.Sheets[0].Columns[0].Width = 36;
                bindFpGasEmissionData();
            }
        }
        **********************************************************/

        /// <summary>
        /// 导出按钮事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tsBtnExport_Click(object sender, EventArgs e)
        {
            if (FileExport.fileExport(fpCoalExistence, true))
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
                FarpointDefaultPropertiesSetter.SetFpFilterHideProperties(this.fpCoalExistence, _filterColunmIdxs);

            }
            else//未选中时，根据用户自定义的颜色进行分类显示
            {
                //启用选择颜色相关控件
                farpointFilter1.EnableChooseColorCtrls(true);
                //设置自定义过滤条件
                FarpointDefaultPropertiesSetter.SetFpCustomFilterProperties(this.fpCoalExistence, farpointFilter1.GetSelectedFitColor(), farpointFilter1.GetSelectedNotFitColor(), _filterColunmIdxs);
            }
        }

        private void farpointFilter1_OnClickClearFilterBtn(object sender, EventArgs arg)
        {
            //清空过滤条件
            this.fpCoalExistence.ActiveSheet.RowFilter.ResetFilter();
        }

        private void farpointFilter1_OnClickFitColorBtnOK(object sender, EventArgs arg)
        {
            //根据新的颜色值设置自动隐藏过滤条件
            FarpointDefaultPropertiesSetter.SetFpCustomFilterProperties(this.fpCoalExistence, farpointFilter1.GetSelectedFitColor(), farpointFilter1.GetSelectedNotFitColor(), _filterColunmIdxs);
        }

        private void farpointFilter1_OnClickNotFitColorBtnOK(object sender, EventArgs arg)
        {
            //根据新的颜色值设置自动隐藏过滤条件
            FarpointDefaultPropertiesSetter.SetFpCustomFilterProperties(this.fpCoalExistence, farpointFilter1.GetSelectedFitColor(), farpointFilter1.GetSelectedNotFitColor(), _filterColunmIdxs);
        }
        #endregion

        private void tsBtnExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void tsBtnExit_Click_1(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
