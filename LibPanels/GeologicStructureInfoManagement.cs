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
using System.Globalization;
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
    public partial class GeologicStructureInfoManagement : BaseForm
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
        LibEntity.MineData mdEntity = new LibEntity.MineData();
        GeologicStructure geologicStructureEntity = new GeologicStructure();
        private GeologicStructure[] geologicStructures;
        //***********************************

        /// <summary>
        /// 构造方法
        /// </summary>
        public GeologicStructureInfoManagement(MainFrm mainFrm)
        {
            this.MainForm = mainFrm;

            InitializeComponent();

            dataPager1.FrmChild_EventHandler += FrmParent_EventHandler;

            //窗体属性设置
            LibCommon.FormDefaultPropertiesSetter.SetManagementFormDefaultProperties(this, Const_OP.GEOLOGIC_STRUCTURE_MANAGEMENT);

            //Farpoint属性设置
            LibCommon.FarpointDefaultPropertiesSetter.SetFpDefaultProperties(this.fpGeologicStructure, LibCommon.Const_OP.GEOLOGIC_STRUCTURE_FARPOINT, rowDetailStartIndex);

            _filterColunmIdxs = new int[]
            {
                1,
                15,
                16,
                17,
            };
            //禁用选择颜色相关控件
            farpointFilter1.EnableChooseColorCtrls(false);
            //设置自动隐藏过滤条件
            FarpointDefaultPropertiesSetter.SetFpFilterHideProperties(this.fpGeologicStructure, _filterColunmIdxs);

            _queryConditions.Show = bindFpGeologicStructureWithCondition;
        }

        /// <summary>
        /// 初始化
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MineDataManagement_Load(object sender, EventArgs e)
        {
            this.fpGeologicStructure.Sheets[0].Rows.Default.Resizable = false;
            this.fpGeologicStructure.Sheets[0].Columns[0].Resizable = false;
            this.bindFpGeologicStructureWithCondition();
        }

        /// <summary>
        /// 委托方法
        /// </summary>
        /// <param name="sender"></param>
        private void FrmParent_EventHandler(object sender)
        {
            bindFpGeologicStructureWithCondition();
        }

        /// <summary>
        /// farpoint数据绑定带条件
        /// </summary>
        private void bindFpGeologicStructureWithCondition()
        {
            FarPointOperate.farPointClear(fpGeologicStructure, rowDetailStartIndex, rowsCount);
            checkCount = 0;
            chkSelAll.Checked = false;
            // ※分页必须
            _iRecordCount = GeologicStructure.GetRecordCount();

            // ※分页必须
            dataPager1.PageControlInit(_iRecordCount);
            int iStartIndex = dataPager1.getStartIndex();
            int iEndIndex = dataPager1.getEndIndex();
            geologicStructures = GeologicStructure.SlicedFindByCondition(iStartIndex, iEndIndex, _queryConditions.TunnelId, Convert.ToDateTime(_queryConditions.DefaultStartTime), Convert.ToDateTime(_queryConditions.DefaultEndTime));
            rowsCount = geologicStructures.Length; ;

            FarPointOperate.farPointReAdd(fpGeologicStructure, rowDetailStartIndex, rowsCount);
            if (rowsCount > 0)
            {
                FarPoint.Win.Spread.CellType.CheckBoxCellType ckbxcell = new FarPoint.Win.Spread.CellType.CheckBoxCellType();
                ckbxcell.ThreeState = false;
                for (int i = 0; i < geologicStructures.Length; i++)
                {
                    int index = 0;
                    this.fpGeologicStructure.Sheets[0].Cells[rowDetailStartIndex + i, index].CellType = ckbxcell;
                    //巷道名称
                    this.fpGeologicStructure.Sheets[0].Cells[rowDetailStartIndex + i, ++index].Text =
                        geologicStructures[i].Tunnel.TunnelName;
                    // TunnelInfoBLL.selectTunnelInfoByTunnelID(Convert.ToInt32(_ds.Tables[0].Rows[i][GeologicStructureDbConstNames.TUNNEL_ID])).TunnelName;
                    if (LibPanels.checkCoordinate(geologicStructures[i].CoordinateX, geologicStructures[i].CoordinateY, geologicStructures[i].CoordinateZ))
                    {
                        //坐标X
                        fpGeologicStructure.Sheets[0].Cells[rowDetailStartIndex + i, ++index].Text =
                            geologicStructures[i].CoordinateX.ToString();
                        //坐标Y
                        fpGeologicStructure.Sheets[0].Cells[rowDetailStartIndex + i, ++index].Text =
                            geologicStructures[i].CoordinateY.ToString();
                        //坐标Z
                        fpGeologicStructure.Sheets[0].Cells[rowDetailStartIndex + i, ++index].Text =
                            geologicStructures[i].CoordinateZ.ToString();
                    }
                    else
                    {
                        index = index + 3;
                    }
                    //时间
                    fpGeologicStructure.Sheets[0].Cells[rowDetailStartIndex + i, ++index].Text =
                        geologicStructures[i].Datetime.ToString("yyyy-MM-dd hh:mm:ss");
                    //无计划揭露构造
                    fpGeologicStructure.Sheets[0].Cells[rowDetailStartIndex + i, ++index].Text =
                        geologicStructures[i].NoPlanStructure.ToString();
                    //过构造时措施无效
                    fpGeologicStructure.Sheets[0].Cells[rowDetailStartIndex + i, ++index].Text =
                        geologicStructures[i].PassedStructureRuleInvalid.ToString();
                    //黄色预警措施无效
                    fpGeologicStructure.Sheets[0].Cells[rowDetailStartIndex + i, ++index].Text =
                        geologicStructures[i].YellowRuleInvalid.ToString();
                    //顶板破碎
                    fpGeologicStructure.Sheets[0].Cells[rowDetailStartIndex + i, ++index].Text =
                        geologicStructures[i].RoofBroken.ToString();
                    //煤层松软
                    fpGeologicStructure.Sheets[0].Cells[rowDetailStartIndex + i, ++index].Text =
                        geologicStructures[i].CoalSeamSoft.ToString();
                    //工作面煤层处于分叉、合层状态
                    fpGeologicStructure.Sheets[0].Cells[rowDetailStartIndex + i, ++index].Text =
                        geologicStructures[i].CoalSeamBranch.ToString();
                    //顶板条件发生变化
                    fpGeologicStructure.Sheets[0].Cells[rowDetailStartIndex + i, ++index].Text =
                        geologicStructures[i].RoofChange.ToString();
                    //工作面夹矸突然变薄或消失
                    fpGeologicStructure.Sheets[0].Cells[rowDetailStartIndex + i, ++index].Text =
                        geologicStructures[i].GangueDisappear.ToString();
                    //夹矸位置急剧变化
                    fpGeologicStructure.Sheets[0].Cells[rowDetailStartIndex + i, ++index].Text =
                        geologicStructures[i].GangueLocationChange.ToString();
                    //工作制式
                    fpGeologicStructure.Sheets[0].Cells[rowDetailStartIndex + i, ++index].Text =
                        geologicStructures[i].WorkStyle;
                    //班次
                    fpGeologicStructure.Sheets[0].Cells[rowDetailStartIndex + i, ++index].Text =
                        geologicStructures[i].WorkTime;
                    //填报人
                    fpGeologicStructure.Sheets[0].Cells[rowDetailStartIndex + i, ++index].Text =
                        geologicStructures[i].Submitter;
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
                        this.fpGeologicStructure.Sheets[0].Cells[rowDetailStartIndex + i, 0].Value = ((CheckBox)sender).Checked;
                        checkCount = geologicStructures.Length;
                    }
                    //checkbox未选中
                    else
                    {
                        this.fpGeologicStructure.Sheets[0].Cells[rowDetailStartIndex + i, 0].Value = ((CheckBox)sender).Checked;
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
            m.Text = new LibPanels(MineDataPanelName.GeologicStructure).panelFormName;
            if (DialogResult.OK == m.ShowDialog())
            {
                //重新绑定数据
                bindFpGeologicStructureWithCondition();
                //跳转到最后一页
                this.dataPager1.btnLastPage_Click(sender, e);
                //添加后焦点设置
                FarPointOperate.farPointFocusSetAdd(fpGeologicStructure, rowDetailStartIndex, rowsCount);
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
            for (int i = 0; i < rowsCount; i++)
            {
                if (fpGeologicStructure.Sheets[0].Cells[rowDetailStartIndex + i, 0].Value != null &&
                    (bool)fpGeologicStructure.Sheets[0].Cells[rowDetailStartIndex + i, 0].Value == true)
                {
                    geologicStructureEntity = geologicStructures[i];
                }
            }
            //记录修改行号
            _tmpRowIndex = fpGeologicStructure.ActiveSheet.ActiveRowIndex;
            MineData m = new MineData(geologicStructureEntity, this.MainForm);
            m.Text = new LibPanels(MineDataPanelName.GeologicStructure_Change).panelFormName;
            if (DialogResult.OK == m.ShowDialog())
            {
                //TODO:修改成功
            }
            //重新绑定数据
            bindFpGeologicStructureWithCondition();
            //修改后焦点设置
            FarPointOperate.farPointFocusSetChange(fpGeologicStructure, _tmpRowIndex);
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
                    if (fpGeologicStructure.Sheets[0].Cells[rowDetailStartIndex + i, 0].Value != null && (bool)fpGeologicStructure.Sheets[0].Cells[rowDetailStartIndex + i, 0].Value == true)
                    {
                        _tmpRowIndex = fpGeologicStructure.ActiveSheet.ActiveRowIndex;
                        geologicStructures[i].Delete();
                        bResult = true;
                    }
                }
                //删除成功
                if (bResult)
                {
                    //重新绑定数据
                    bindFpGeologicStructureWithCondition();
                    //删除后焦点设置
                    FarPointOperate.farPointFocusSetDel(fpGeologicStructure, _tmpRowIndex);
                }
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
            bindFpGeologicStructureWithCondition();
        }

        /// <summary>
        /// 导出按钮事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tsBtnExport_Click(object sender, EventArgs e)
        {
            if (FileExport.fileExport(fpGeologicStructure, true))
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
                FarpointDefaultPropertiesSetter.SetFpFilterHideProperties(this.fpGeologicStructure, _filterColunmIdxs);

            }
            else//未选中时，根据用户自定义的颜色进行分类显示
            {
                //启用选择颜色相关控件
                farpointFilter1.EnableChooseColorCtrls(true);
                //设置自定义过滤条件
                FarpointDefaultPropertiesSetter.SetFpCustomFilterProperties(this.fpGeologicStructure, farpointFilter1.GetSelectedFitColor(), farpointFilter1.GetSelectedNotFitColor(), _filterColunmIdxs);
            }
        }

        private void farpointFilter1_OnClickClearFilterBtn(object sender, EventArgs arg)
        {
            //清空过滤条件
            this.fpGeologicStructure.ActiveSheet.RowFilter.ResetFilter();
        }

        private void farpointFilter1_OnClickFitColorBtnOK(object sender, EventArgs arg)
        {
            //根据新的颜色值设置自动隐藏过滤条件
            FarpointDefaultPropertiesSetter.SetFpCustomFilterProperties(this.fpGeologicStructure, farpointFilter1.GetSelectedFitColor(), farpointFilter1.GetSelectedNotFitColor(), _filterColunmIdxs);
        }

        private void farpointFilter1_OnClickNotFitColorBtnOK(object sender, EventArgs arg)
        {
            //根据新的颜色值设置自动隐藏过滤条件
            FarpointDefaultPropertiesSetter.SetFpCustomFilterProperties(this.fpGeologicStructure, farpointFilter1.GetSelectedFitColor(), farpointFilter1.GetSelectedNotFitColor(), _filterColunmIdxs);
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
