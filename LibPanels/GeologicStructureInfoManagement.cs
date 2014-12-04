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
        DataSet _ds = new DataSet();
        LibEntity.MineData mdEntity = new LibEntity.MineData();
        GeologicStructure geologicStructureEntity = new GeologicStructure();
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
        /// farpoint数据绑定
        /// </summary>
        //private void bindFpGeologicStructure()
        //{
        //    FarPointOperate.farPointClear(fpGeologicStructure, rowDetailStartIndex, rowsCount);
        //    checkCount = 0;
        //    chkSelAll.Checked = false;
        //    // ※分页必须
        //    _iRecordCount = GeologicStructureBLL.selectGeologicStructure().Tables[0].Rows.Count;

        //    // ※分页必须
        //    dataPager1.PageControlInit(_iRecordCount);
        //    int iStartIndex = dataPager1.getStartIndex();
        //    int iEndIndex = dataPager1.getEndIndex();
        //    _ds = GeologicStructureBLL.selectGeologicStructure(iStartIndex, iEndIndex);
        //    rowsCount = _ds.Tables[0].Rows.Count;
        //    FarPointOperate.farPointReAdd(fpGeologicStructure, rowDetailStartIndex, rowsCount);
        //    if (rowsCount > 0)
        //    {
        //        FarPoint.Win.Spread.CellType.CheckBoxCellType ckbxcell = new FarPoint.Win.Spread.CellType.CheckBoxCellType();
        //        ckbxcell.ThreeState = false;
        //        for (int i = 0; i < _ds.Tables[0].Rows.Count; i++)
        //        {
        //            int index = 0;
        //            this.fpGeologicStructure.Sheets[0].Cells[rowDetailStartIndex + i, index].CellType = ckbxcell;
        //            //巷道名称
        //            this.fpGeologicStructure.Sheets[0].Cells[rowDetailStartIndex + i, ++index].Text = TunnelInfoBLL.selectTunnelInfoByTunnelID(Convert.ToInt32(_ds.Tables[0].Rows[i][GeologicStructureDbConstNames.TUNNEL_ID])).TunnelName;
        //            if (LibPanels.checkCoordinate(_ds.Tables[0].Rows[i][GeologicStructureDbConstNames.X].ToString(), _ds.Tables[0].Rows[i][GeologicStructureDbConstNames.Y].ToString(), _ds.Tables[0].Rows[i][GeologicStructureDbConstNames.Z].ToString()))
        //            {
        //                //坐标X
        //                this.fpGeologicStructure.Sheets[0].Cells[rowDetailStartIndex + i, ++index].Text = _ds.Tables[0].Rows[i][GeologicStructureDbConstNames.X].ToString();
        //                //坐标Y
        //                this.fpGeologicStructure.Sheets[0].Cells[rowDetailStartIndex + i, ++index].Text = _ds.Tables[0].Rows[i][GeologicStructureDbConstNames.Y].ToString();
        //                //坐标Z
        //                this.fpGeologicStructure.Sheets[0].Cells[rowDetailStartIndex + i, ++index].Text = _ds.Tables[0].Rows[i][GeologicStructureDbConstNames.Z].ToString();
        //            }
        //            else
        //            {
        //                index = index + 3;
        //            }
        //            //时间
        //            this.fpGeologicStructure.Sheets[0].Cells[rowDetailStartIndex + i, ++index].Text = _ds.Tables[0].Rows[i][GeologicStructureDbConstNames.DATETIME].ToString().Substring(0, _ds.Tables[0].Rows[i][GeologicStructureDbConstNames.DATETIME].ToString().IndexOf(' '));
        //            //无计划揭露构造
        //            this.fpGeologicStructure.Sheets[0].Cells[rowDetailStartIndex + i, ++index].Text = LibPanels.DataChangeYesNo(_ds.Tables[0].Rows[i][GeologicStructureDbConstNames.NO_PLAN_STRUCTURE].ToString());
        //            //过构造时措施无效
        //            this.fpGeologicStructure.Sheets[0].Cells[rowDetailStartIndex + i, ++index].Text = LibPanels.DataChangeYesNo(_ds.Tables[0].Rows[i][GeologicStructureDbConstNames.PASSED_STRUCTURE_RULE_INVALID].ToString());
        //            //黄色预警措施无效
        //            this.fpGeologicStructure.Sheets[0].Cells[rowDetailStartIndex + i, ++index].Text = LibPanels.DataChangeYesNo(_ds.Tables[0].Rows[i][GeologicStructureDbConstNames.YELLOW_RULE_INVALID].ToString());
        //            //顶板破碎
        //            this.fpGeologicStructure.Sheets[0].Cells[rowDetailStartIndex + i, ++index].Text = LibPanels.DataChangeYesNo(_ds.Tables[0].Rows[i][GeologicStructureDbConstNames.ROOF_BROKEN].ToString());
        //            //煤层松软
        //            this.fpGeologicStructure.Sheets[0].Cells[rowDetailStartIndex + i, ++index].Text = LibPanels.DataChangeYesNo(_ds.Tables[0].Rows[i][GeologicStructureDbConstNames.COAL_SEAM_SOFT].ToString());
        //            //工作面煤层处于分叉、合层状态
        //            this.fpGeologicStructure.Sheets[0].Cells[rowDetailStartIndex + i, ++index].Text = LibPanels.DataChangeYesNo(_ds.Tables[0].Rows[i][GeologicStructureDbConstNames.COAL_SEAM_BRANCH].ToString());
        //            //顶板条件发生变化
        //            this.fpGeologicStructure.Sheets[0].Cells[rowDetailStartIndex + i, ++index].Text = LibPanels.DataChangeYesNo(_ds.Tables[0].Rows[i][GeologicStructureDbConstNames.ROOF_CHANGE].ToString());
        //            //工作面夹矸突然变薄或消失
        //            this.fpGeologicStructure.Sheets[0].Cells[rowDetailStartIndex + i, ++index].Text = LibPanels.DataChangeYesNo(_ds.Tables[0].Rows[i][GeologicStructureDbConstNames.GANGUE_DISAPPEAR].ToString());
        //            //夹矸位置急剧变化
        //            this.fpGeologicStructure.Sheets[0].Cells[rowDetailStartIndex + i, ++index].Text = LibPanels.DataChangeYesNo(_ds.Tables[0].Rows[i][GeologicStructureDbConstNames.GANGUE_LOCATION_CHANGE].ToString());
        //            //工作制式
        //            this.fpGeologicStructure.Sheets[0].Cells[rowDetailStartIndex + i, ++index].Text = _ds.Tables[0].Rows[i][GeologicStructureDbConstNames.WORK_STYLE].ToString();
        //            //班次
        //            this.fpGeologicStructure.Sheets[0].Cells[rowDetailStartIndex + i, ++index].Text = _ds.Tables[0].Rows[i][GeologicStructureDbConstNames.WORK_TIME].ToString();
        //            //填报人
        //            this.fpGeologicStructure.Sheets[0].Cells[rowDetailStartIndex + i, ++index].Text = _ds.Tables[0].Rows[i][GeologicStructureDbConstNames.SUBMITTER].ToString();
        //        }
        //    }
        //    setButtenEnable();
        //}


        /// <summary>
        /// farpoint数据绑定带条件
        /// </summary>
        private void bindFpGeologicStructureWithCondition()
        {
            FarPointOperate.farPointClear(fpGeologicStructure, rowDetailStartIndex, rowsCount);
            checkCount = 0;
            chkSelAll.Checked = false;
            // ※分页必须
            _iRecordCount = GeologicStructureBLL.selectGeologicStructureWithCondition(_queryConditions.TunnelId, _queryConditions.DefaultStartTime, _queryConditions.DefaultEndTime).Tables[0].Rows.Count;

            // ※分页必须
            dataPager1.PageControlInit(_iRecordCount);
            int iStartIndex = dataPager1.getStartIndex();
            int iEndIndex = dataPager1.getEndIndex();
            _ds = GeologicStructureBLL.selectGeologicStructureWithCondition(iStartIndex, iEndIndex, _queryConditions.TunnelId, _queryConditions.DefaultStartTime, _queryConditions.DefaultEndTime);
            rowsCount = _ds.Tables[0].Rows.Count;
            FarPointOperate.farPointReAdd(fpGeologicStructure, rowDetailStartIndex, rowsCount);
            if (rowsCount > 0)
            {
                FarPoint.Win.Spread.CellType.CheckBoxCellType ckbxcell = new FarPoint.Win.Spread.CellType.CheckBoxCellType();
                ckbxcell.ThreeState = false;
                for (int i = 0; i < _ds.Tables[0].Rows.Count; i++)
                {
                    int index = 0;
                    this.fpGeologicStructure.Sheets[0].Cells[rowDetailStartIndex + i, index].CellType = ckbxcell;
                    //巷道名称
                    this.fpGeologicStructure.Sheets[0].Cells[rowDetailStartIndex + i, ++index].Text =
                        BasicInfoManager.getInstance().getTunnelByID(_queryConditions.TunnelId).TunnelName;
                    // TunnelInfoBLL.selectTunnelInfoByTunnelID(Convert.ToInt32(_ds.Tables[0].Rows[i][GeologicStructureDbConstNames.TUNNEL_ID])).TunnelName;
                    if (LibPanels.checkCoordinate(_ds.Tables[0].Rows[i][GeologicStructureDbConstNames.X].ToString(), _ds.Tables[0].Rows[i][GeologicStructureDbConstNames.Y].ToString(), _ds.Tables[0].Rows[i][GeologicStructureDbConstNames.Z].ToString()))
                    {
                        //坐标X
                        this.fpGeologicStructure.Sheets[0].Cells[rowDetailStartIndex + i, ++index].Text = _ds.Tables[0].Rows[i][GeologicStructureDbConstNames.X].ToString();
                        //坐标Y
                        this.fpGeologicStructure.Sheets[0].Cells[rowDetailStartIndex + i, ++index].Text = _ds.Tables[0].Rows[i][GeologicStructureDbConstNames.Y].ToString();
                        //坐标Z
                        this.fpGeologicStructure.Sheets[0].Cells[rowDetailStartIndex + i, ++index].Text = _ds.Tables[0].Rows[i][GeologicStructureDbConstNames.Z].ToString();
                    }
                    else
                    {
                        index = index + 3;
                    }
                    //时间
                    this.fpGeologicStructure.Sheets[0].Cells[rowDetailStartIndex + i, ++index].Text = _ds.Tables[0].Rows[i][GeologicStructureDbConstNames.DATETIME].ToString().Substring(0, _ds.Tables[0].Rows[i][GeologicStructureDbConstNames.DATETIME].ToString().IndexOf(' '));
                    //无计划揭露构造
                    this.fpGeologicStructure.Sheets[0].Cells[rowDetailStartIndex + i, ++index].Text = LibPanels.DataChangeYesNo(_ds.Tables[0].Rows[i][GeologicStructureDbConstNames.NO_PLAN_STRUCTURE].ToString());
                    //过构造时措施无效
                    this.fpGeologicStructure.Sheets[0].Cells[rowDetailStartIndex + i, ++index].Text = LibPanels.DataChangeYesNo(_ds.Tables[0].Rows[i][GeologicStructureDbConstNames.PASSED_STRUCTURE_RULE_INVALID].ToString());
                    //黄色预警措施无效
                    this.fpGeologicStructure.Sheets[0].Cells[rowDetailStartIndex + i, ++index].Text = LibPanels.DataChangeYesNo(_ds.Tables[0].Rows[i][GeologicStructureDbConstNames.YELLOW_RULE_INVALID].ToString());
                    //顶板破碎
                    this.fpGeologicStructure.Sheets[0].Cells[rowDetailStartIndex + i, ++index].Text = LibPanels.DataChangeYesNo(_ds.Tables[0].Rows[i][GeologicStructureDbConstNames.ROOF_BROKEN].ToString());
                    //煤层松软
                    this.fpGeologicStructure.Sheets[0].Cells[rowDetailStartIndex + i, ++index].Text = LibPanels.DataChangeYesNo(_ds.Tables[0].Rows[i][GeologicStructureDbConstNames.COAL_SEAM_SOFT].ToString());
                    //工作面煤层处于分叉、合层状态
                    this.fpGeologicStructure.Sheets[0].Cells[rowDetailStartIndex + i, ++index].Text = LibPanels.DataChangeYesNo(_ds.Tables[0].Rows[i][GeologicStructureDbConstNames.COAL_SEAM_BRANCH].ToString());
                    //顶板条件发生变化
                    this.fpGeologicStructure.Sheets[0].Cells[rowDetailStartIndex + i, ++index].Text = LibPanels.DataChangeYesNo(_ds.Tables[0].Rows[i][GeologicStructureDbConstNames.ROOF_CHANGE].ToString());
                    //工作面夹矸突然变薄或消失
                    this.fpGeologicStructure.Sheets[0].Cells[rowDetailStartIndex + i, ++index].Text = LibPanels.DataChangeYesNo(_ds.Tables[0].Rows[i][GeologicStructureDbConstNames.GANGUE_DISAPPEAR].ToString());
                    //夹矸位置急剧变化
                    this.fpGeologicStructure.Sheets[0].Cells[rowDetailStartIndex + i, ++index].Text = LibPanels.DataChangeYesNo(_ds.Tables[0].Rows[i][GeologicStructureDbConstNames.GANGUE_LOCATION_CHANGE].ToString());
                    //工作制式
                    this.fpGeologicStructure.Sheets[0].Cells[rowDetailStartIndex + i, ++index].Text = _ds.Tables[0].Rows[i][GeologicStructureDbConstNames.WORK_STYLE].ToString();
                    //班次
                    this.fpGeologicStructure.Sheets[0].Cells[rowDetailStartIndex + i, ++index].Text = _ds.Tables[0].Rows[i][GeologicStructureDbConstNames.WORK_TIME].ToString();
                    //填报人
                    this.fpGeologicStructure.Sheets[0].Cells[rowDetailStartIndex + i, ++index].Text = _ds.Tables[0].Rows[i][GeologicStructureDbConstNames.SUBMITTER].ToString();
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

        private void setMineDataEntityValue()
        {
            for (int i = 0; i < rowsCount; i++)
            {
                if (fpGeologicStructure.Sheets[0].Cells[rowDetailStartIndex + i, 0].Value != null && (bool)fpGeologicStructure.Sheets[0].Cells[rowDetailStartIndex + i, 0].Value == true)
                {
                    //主键
                    if (int.TryParse(_ds.Tables[0].Rows[i][GasDataDbConstNames.ID].ToString(), out tmpInt))
                    {
                        geologicStructureEntity.Id = tmpInt;
                        tmpInt = 0;
                    }
                    //绑定巷道ID
                    if (int.TryParse(_ds.Tables[0].Rows[i][GasDataDbConstNames.TUNNEL_ID].ToString(), out tmpInt))
                    {
                        geologicStructureEntity.Tunnel.TunnelID = tmpInt;
                        tmpInt = 0;
                    }
                    //坐标X
                    if (double.TryParse(_ds.Tables[0].Rows[i][GasDataDbConstNames.X].ToString(), out tmpDouble))
                    {
                        geologicStructureEntity.CoordinateX = tmpDouble;
                        tmpDouble = 0;
                    }
                    //坐标Y
                    if (double.TryParse(_ds.Tables[0].Rows[i][GasDataDbConstNames.Y].ToString(), out tmpDouble))
                    {
                        geologicStructureEntity.CoordinateY = tmpDouble;
                        tmpDouble = 0;
                    }
                    //坐标Z
                    if (double.TryParse(_ds.Tables[0].Rows[i][GasDataDbConstNames.Z].ToString(), out tmpDouble))
                    {
                        geologicStructureEntity.CoordinateZ = tmpDouble;
                        tmpDouble = 0;
                    }
                    //工作制式
                    geologicStructureEntity.WorkStyle = _ds.Tables[0].Rows[i][GasDataDbConstNames.WORK_STYLE].ToString();
                    //班次
                    geologicStructureEntity.WorkTime = _ds.Tables[0].Rows[i][GasDataDbConstNames.WORK_TIME].ToString();
                    //队别名称
                    geologicStructureEntity.TeamName = _ds.Tables[0].Rows[i][GasDataDbConstNames.TEAM_NAME].ToString();
                    //填报人
                    geologicStructureEntity.Submitter = _ds.Tables[0].Rows[i][GasDataDbConstNames.SUBMITTER].ToString();
                    //提交日期
                    if (DateTime.TryParse(_ds.Tables[0].Rows[i][GasDataDbConstNames.DATETIME].ToString(), out tmpDtp))
                    {
                        geologicStructureEntity.Datetime = tmpDtp;
                        tmpDtp = DateTime.Now;
                    }
                    //无计划揭露构造
                    if (int.TryParse(_ds.Tables[0].Rows[i][GeologicStructureDbConstNames.NO_PLAN_STRUCTURE].ToString(), out tmpInt))
                    {
                        geologicStructureEntity.NoPlanStructure = tmpInt;
                        tmpInt = 0;
                    }
                    //过构造时措施无效
                    if (int.TryParse(_ds.Tables[0].Rows[i][GeologicStructureDbConstNames.PASSED_STRUCTURE_RULE_INVALID].ToString(), out tmpInt))
                    {
                        geologicStructureEntity.PassedStructureRuleInvalid = tmpInt;
                        tmpInt = 0;
                    }
                    //黄色预警措施无效
                    if (int.TryParse(_ds.Tables[0].Rows[i][GeologicStructureDbConstNames.YELLOW_RULE_INVALID].ToString(), out tmpInt))
                    {
                        geologicStructureEntity.YellowRuleInvalid = tmpInt;
                        tmpInt = 0;
                    }
                    //顶板破碎
                    if (int.TryParse(_ds.Tables[0].Rows[i][GeologicStructureDbConstNames.ROOF_BROKEN].ToString(), out tmpInt))
                    {
                        geologicStructureEntity.RoofBroken = tmpInt;
                        tmpInt = 0;
                    }
                    //煤层松软
                    if (int.TryParse(_ds.Tables[0].Rows[i][GeologicStructureDbConstNames.COAL_SEAM_SOFT].ToString(), out tmpInt))
                    {
                        geologicStructureEntity.CoalSeamSoft = tmpInt;
                        tmpInt = 0;
                    }
                    //工作面煤层处于分叉、合层状态
                    if (int.TryParse(_ds.Tables[0].Rows[i][GeologicStructureDbConstNames.COAL_SEAM_BRANCH].ToString(), out tmpInt))
                    {
                        geologicStructureEntity.CoalSeamBranch = tmpInt;
                        tmpInt = 0;
                    }
                    //顶板条件发生变化
                    if (int.TryParse(_ds.Tables[0].Rows[i][GeologicStructureDbConstNames.ROOF_CHANGE].ToString(), out tmpInt))
                    {
                        geologicStructureEntity.RoofChange = tmpInt;
                        tmpInt = 0;
                    }
                    //工作面夹矸突然变薄或消失
                    if (int.TryParse(_ds.Tables[0].Rows[i][GeologicStructureDbConstNames.GANGUE_DISAPPEAR].ToString(), out tmpInt))
                    {
                        geologicStructureEntity.GangueDisappear = tmpInt;
                        tmpInt = 0;
                    }
                    //夹矸位置急剧变化
                    if (int.TryParse(_ds.Tables[0].Rows[i][GeologicStructureDbConstNames.GANGUE_LOCATION_CHANGE].ToString(), out tmpInt))
                    {
                        geologicStructureEntity.GangueLocationChange = tmpInt;
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
                        this.fpGeologicStructure.Sheets[0].Cells[rowDetailStartIndex + i, 0].Value = ((CheckBox)sender).Checked;
                        checkCount = _ds.Tables[0].Rows.Count;
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
            setMineDataEntityValue();
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
                        int id = (int)_ds.Tables[0].Rows[i][GeologicStructureDbConstNames.ID];
                        //删除
                        bResult = GeologicStructureBLL.deleteGeologicStructure(id);
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
