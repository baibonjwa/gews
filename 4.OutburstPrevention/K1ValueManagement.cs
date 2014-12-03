// ******************************************************************
// 概  述：K1值信息管理
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
using LibBusiness;
using LibEntity;
using LibCommon;
using LibCommonControl;

namespace _4.OutburstPrevention
{
    public partial class K1ValueManagement : BaseForm
    {
        /***********变量声明*************/
        private int _iRecordCount = 0;
        int rowsCount = 0;      //数据行数
        int checkCount = 0;     //选择行数
        int rowDetailStartIndex = 3;
        int _tmpRowIndex = 0;
        Color rowBackColor = Color.White;
        //需要过滤的列索引
        private int[] _filterColunmIdxs = null;
        K1ValueEntity k1ValueEntity = new K1ValueEntity();
        TunnelEntity tunnelEntity = new TunnelEntity();
        DataSet ds = new DataSet();
        DataSet dsAll = new DataSet();
        int[] arr = new int[5];
        /********************************/

        /// <summary>
        /// 构造方法
        /// </summary>
        public K1ValueManagement(MainFrm mainFrm)
        {
            InitializeComponent();

            this.MainForm = mainFrm;

            dataPager1.FrmChild_EventHandler += FrmParent_EventHandler;

            LibCommon.FormDefaultPropertiesSetter.SetManagementFormDefaultProperties(this, Const_OP.K1_VALUE_MANAGEMENT);

            LibCommon.FarpointDefaultPropertiesSetter.SetFpDefaultProperties(fpK1Value, LibCommon.Const_OP.K1_VALUE_FARPOINT_TITLE, rowDetailStartIndex);

            //this._dateTimeStart.Text = DateTime.Now.ToString("yyyy-MM-dd") + " 00:00:00";
            //this._dateTimeEnd.Text = DateTime.Now.ToString("yyyy-MM-dd") + " 23:59:59";
            _queryConditions.Show = bindfpK1Value;

            _filterColunmIdxs = new int[]
            {
                7,
            };
            //禁用选择颜色相关控件
            farpointFilter1.EnableChooseColorCtrls(false);
            //设置自动隐藏过滤条件
            FarpointDefaultPropertiesSetter.SetFpFilterHideProperties(this.fpK1Value, _filterColunmIdxs);
        }

        /// <summary>
        /// 初始化
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void K1ValueManagement_Load(object sender, EventArgs e)
        {
            bindfpK1Value();
        }

        /// <summary>
        /// 委托事件
        /// </summary>
        /// <param name="sender"></param>
        private void FrmParent_EventHandler(object sender)
        {
            bindfpK1Value();
        }

        #region ******菜单按钮******
        /// <summary>
        /// 添加按钮事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnAdd_Click(object sender, EventArgs e)
        {
            K1ValueEntering k1value = new K1ValueEntering(this.MainForm);
            //添加成功
            if (DialogResult.OK == k1value.ShowDialog())
            {
                bindfpK1Value();
                //跳转到最后一页
                this.dataPager1.btnLastPage_Click(sender, e);

                FarPointOperate.farPointFocusSetAdd(fpK1Value, rowDetailStartIndex, rowsCount);
            }
        }

        /// <summary>
        /// 修改按钮事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnUpdate_Click(object sender, EventArgs e)
        {
            //K1值实体赋值
            setK1ValueEntity();
            //记录当前选定单元格
            _tmpRowIndex = fpK1Value.ActiveSheet.ActiveRowIndex;
            K1ValueEntering k1value = new K1ValueEntering(arr, k1ValueEntity.ID, this.MainForm);
            //添加成功
            if (DialogResult.OK == k1value.ShowDialog())
            {
                bindfpK1Value();
                FarPointOperate.farPointFocusSetChange(fpK1Value, _tmpRowIndex);
            }
        }

        /// <summary>
        /// 删除按钮事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnDelete_Click(object sender, EventArgs e)
        {
            int[] tmpID = new int[rowsCount];
            bool bResult = false;
            int delStyle = 0;
            K1DeleteMessageBox k1DeleteMessageBox = new K1DeleteMessageBox();
            DialogResult dr = k1DeleteMessageBox.ShowDialog();
            if (DialogResult.Yes == dr)
            {
                delStyle = 1;
            }
            else if (DialogResult.No == dr)
            {
                delStyle = 0;
            }
            else
            {
                return;
            }
            for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
            {
                if (fpK1Value.Sheets[0].Cells[i + rowDetailStartIndex, 0].Value != null && (bool)fpK1Value.Sheets[0].Cells[i + rowDetailStartIndex, 0].Value == true)
                {
                    int tmpInt = 0;
                    bool deletedID = false;
                    //矿井编号
                    if (int.TryParse(ds.Tables[0].Rows[i][K1ValueDbConstNames.TUNNEL_ID].ToString(), out tmpInt))
                    {
                        k1ValueEntity.TunnelID = tmpInt;
                        tmpInt = 0;
                    }
                    //当干煤最大与湿煤最大为一条数据时，只做一次删除操作
                    if (int.TryParse(ds.Tables[0].Rows[i][K1ValueDbConstNames.ID].ToString(), out tmpInt))
                    {
                        k1ValueEntity.ID = tmpInt;
                        //主键对应数据是否已被删除
                        for (int j = 0; j < tmpID.Length; j++)
                        {
                            if (k1ValueEntity.ID == tmpID[j])
                            {
                                deletedID = true;
                                break;
                            }
                        }
                        //已删除直接跳过
                        if (deletedID)
                        {
                            continue;
                        }
                        tmpID[i] = tmpInt;
                        tmpInt = 0;
                    }

                    if (int.TryParse(ds.Tables[0].Rows[i][K1ValueDbConstNames.VALUE_K1_ID].ToString(), out tmpInt))
                    {
                        k1ValueEntity.K1ValueID = tmpInt;
                        tmpInt = 0;
                    }
                    //删除
                    bResult = K1ValueBLL.deleteK1Value(k1ValueEntity, delStyle);
                }
            }
            if (bResult)
            {
                //TODO:删除成功
            }
            bindfpK1Value();
        }

        /// <summary>
        /// 刷新
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnRefresh_Click(object sender, EventArgs e)
        {
            bindfpK1Value();
        }

        /// <summary>
        /// 退出
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnExit_Click(object sender, EventArgs e)
        {
            //关闭窗体
            this.Close();
        }
        #endregion

        /// <summary>
        /// 绑定Farpoint数据
        /// </summary>
        private void bindfpK1Value()
        {
            //Farpoint清空
            FarPointOperate.farPointClear(fpK1Value, rowDetailStartIndex, rowsCount);

            checkCount = 0;

            chkSelAll.Checked = false;

            _iRecordCount = K1ValueBLL.selectValueK1Entity2(_queryConditions.TunnelId, _queryConditions.DefaultStartTime, _queryConditions.DefaultEndTime).Tables[0].Rows.Count;
            dataPager1.PageControlInit(_iRecordCount);
            int iStartIndex = dataPager1.getStartIndex();
            int iEndIndex = dataPager1.getEndIndex();
            ds = K1ValueBLL.selectValueK1Entity2(iStartIndex, iEndIndex, _queryConditions.TunnelId, _queryConditions.DefaultStartTime, _queryConditions.DefaultEndTime);
            rowsCount = ds.Tables[0].Rows.Count;

            FarPointOperate.farPointReAdd(fpK1Value, rowDetailStartIndex, rowsCount);
            if (rowsCount > 0)
            {
                FarPoint.Win.Spread.CellType.CheckBoxCellType ckbxcell = new FarPoint.Win.Spread.CellType.CheckBoxCellType();
                ckbxcell.ThreeState = false;
                for (int i = 0; i < rowsCount; i++)
                {
                    int index = 0;
                    tunnelEntity =
                        BasicInfoManager.getInstance()
                            .getTunnelByID(Convert.ToInt32(ds.Tables[0].Rows[i][K1ValueDbConstNames.TUNNEL_ID]));// TunnelInfoBLL.selectTunnelInfoByTunnelID(Convert.ToInt32(ds.Tables[0].Rows[i][K1ValueDbConstNames.TUNNEL_ID]));
                    k1ValueEntity = K1ValueBLL.selectValueK1ByID(Convert.ToInt32(ds.Tables[0].Rows[i][K1ValueDbConstNames.ID]));
                    this.fpK1Value.Sheets[0].Cells[rowDetailStartIndex + i, index].CellType = ckbxcell;
                    //坐标X
                    this.fpK1Value.Sheets[0].Cells[rowDetailStartIndex + i, ++index].Text = k1ValueEntity.CoordinateX.ToString();
                    //坐标Y
                    this.fpK1Value.Sheets[0].Cells[rowDetailStartIndex + i, ++index].Text = k1ValueEntity.CoordinateY.ToString();
                    //坐标Z
                    this.fpK1Value.Sheets[0].Cells[rowDetailStartIndex + i, ++index].Text = k1ValueEntity.CoordinateZ.ToString();
                    //干煤K1值
                    this.fpK1Value.Sheets[0].Cells[rowDetailStartIndex + i, ++index].Text = k1ValueEntity.ValueK1Dry.ToString();
                    //湿煤K1值
                    this.fpK1Value.Sheets[0].Cells[rowDetailStartIndex + i, ++index].Text = k1ValueEntity.ValueK1Wet.ToString();
                    //Sg值
                    this.fpK1Value.Sheets[0].Cells[rowDetailStartIndex + i, ++index].Text = k1ValueEntity.Sg.ToString();
                    //Sv值
                    this.fpK1Value.Sheets[0].Cells[rowDetailStartIndex + i, ++index].Text = k1ValueEntity.Sv.ToString();
                    //Q
                    this.fpK1Value.Sheets[0].Cells[rowDetailStartIndex + i, ++index].Text = k1ValueEntity.Q.ToString();
                    //孔深
                    this.fpK1Value.Sheets[0].Cells[rowDetailStartIndex + i, ++index].Text = k1ValueEntity.BoreholeDeep.ToString();
                    //巷道名称
                    this.fpK1Value.Sheets[0].Cells[rowDetailStartIndex + i, ++index].Text = tunnelEntity.TunnelName;
                    //记录时间
                    this.fpK1Value.Sheets[0].Cells[rowDetailStartIndex + i, ++index].Text = k1ValueEntity.Time.ToString();
                    //录入时间
                    this.fpK1Value.Sheets[0].Cells[rowDetailStartIndex + i, ++index].Text = k1ValueEntity.TypeInTime.ToString();
                    if (i > 0 && ds.Tables[0].Rows[i][K1ValueDbConstNames.VALUE_K1_ID].ToString() != ds.Tables[0].Rows[i - 1][K1ValueDbConstNames.VALUE_K1_ID].ToString())
                    {
                        rowBackColor = FarPointOperate.changeColor(rowBackColor);
                    }
                    FarPointOperate.farPointRowBackColorChange(fpK1Value, rowDetailStartIndex + i, 0, 9, rowBackColor);
                }
            }
            setButtenEnable();
        }

        /// <summary>
        /// K1实体赋值
        /// </summary>
        private void setK1ValueEntity()
        {
            for (int i = 0; i < rowsCount; i++)
            {
                if (fpK1Value.Sheets[0].Cells[rowDetailStartIndex + i, 0].Value != null && (bool)fpK1Value.Sheets[0].Cells[rowDetailStartIndex + i, 0].Value == true)
                {
                    int tmpInt = 0;
                    double tmpDouble = 0;
                    DateTime tmpDt = DateTime.Now;
                    //矿井编号
                    if (int.TryParse(ds.Tables[0].Rows[i][K1ValueDbConstNames.TUNNEL_ID].ToString(), out tmpInt))
                    {
                        k1ValueEntity.TunnelID = tmpInt;
                        tmpInt = 0;
                    }
                    //ID
                    if (int.TryParse(ds.Tables[0].Rows[i][K1ValueDbConstNames.ID].ToString(), out tmpInt))
                    {
                        k1ValueEntity.ID = tmpInt;
                        tmpInt = 0;
                    }
                    //K1分组ID
                    if (int.TryParse(ds.Tables[0].Rows[i][K1ValueDbConstNames.VALUE_K1_ID].ToString(), out tmpInt))
                    {
                        k1ValueEntity.K1ValueID = tmpInt;
                        tmpInt = 0;
                    }
                    //坐标X
                    if (double.TryParse(ds.Tables[0].Rows[i][K1ValueDbConstNames.COORDINATE_X].ToString(), out tmpDouble))
                    {
                        k1ValueEntity.CoordinateX = tmpDouble;
                        tmpDouble = 0;
                    }
                    //坐标Y
                    if (double.TryParse(ds.Tables[0].Rows[i][K1ValueDbConstNames.COORDINATE_Y].ToString(), out tmpDouble))
                    {
                        k1ValueEntity.CoordinateY = tmpDouble;
                        tmpDouble = 0;
                    }
                    //坐标Z
                    if (double.TryParse(ds.Tables[0].Rows[i][K1ValueDbConstNames.COORDINATE_Z].ToString(), out tmpDouble))
                    {
                        k1ValueEntity.CoordinateZ = tmpDouble;
                        tmpDouble = 0;
                    }
                    //干煤K1值
                    if (double.TryParse(ds.Tables[0].Rows[i][K1ValueDbConstNames.VALUE_K1_DRY].ToString(), out tmpDouble))
                    {
                        k1ValueEntity.ValueK1Dry = tmpDouble;
                        tmpDouble = 0;
                    }
                    //湿煤K1值
                    if (double.TryParse(ds.Tables[0].Rows[i][K1ValueDbConstNames.VALUE_K1_WET].ToString(), out tmpDouble))
                    {
                        k1ValueEntity.ValueK1Wet = tmpDouble;
                        tmpDouble = 0;
                    }
                    //孔深
                    if (double.TryParse(ds.Tables[0].Rows[i][K1ValueDbConstNames.BOREHOLE_DEEP].ToString(), out tmpDouble))
                    {
                        k1ValueEntity.BoreholeDeep = tmpDouble;
                        tmpDouble = 0;
                    }
                    //记录时间
                    if (DateTime.TryParse(ds.Tables[0].Rows[i][K1ValueDbConstNames.TIME].ToString(), out tmpDt))
                    {
                        k1ValueEntity.Time = tmpDt;
                        tmpDt = DateTime.Now;
                    }
                    //录入时间
                    if (DateTime.TryParse(ds.Tables[0].Rows[i][K1ValueDbConstNames.TYPE_IN_TIME].ToString(), out tmpDt))
                    {
                        k1ValueEntity.TypeInTime = tmpDt;
                        tmpDt = DateTime.Now;
                    }
                    //巷道实体
                    //tunnelEntity = TunnelInfoBLL.selectTunnelInfoByTunnelID(k1ValueEntity.TunnelID);
                    arr[0] = tunnelEntity.WorkingFace.MiningArea.Horizontal.Mine.MineId;
                    arr[1] = tunnelEntity.WorkingFace.MiningArea.Horizontal.HorizontalId;
                    arr[2] = tunnelEntity.WorkingFace.MiningArea.MiningAreaId;
                    arr[3] = tunnelEntity.WorkingFace.WorkingFaceID;
                    arr[4] = tunnelEntity.TunnelID;
                }
            }
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
        /// Farpoint对全选反选影响
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void fpK1Value_ButtonClicked(object sender, FarPoint.Win.Spread.EditorNotifyEventArgs e)
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
        /// 打印按钮事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tsBtnPrint_Click(object sender, EventArgs e)
        {
            FilePrint.CommonPrint(fpK1Value, 0);
        }

        /// <summary>
        /// 取消按钮事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnCancel_Click(object sender, EventArgs e)
        {
            //关闭窗口
            this.Close();
        }

        /// <summary>
        /// 确定按钮事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnOK_Click(object sender, EventArgs e)
        {
            //关闭窗口
            this.Close();

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
                        this.fpK1Value.Sheets[0].Cells[rowDetailStartIndex + i, 0].Value = ((CheckBox)sender).Checked;
                        checkCount = ds.Tables[0].Rows.Count;
                    }
                    //checkbox未选中
                    else
                    {
                        fpK1Value.Sheets[0].Cells[rowDetailStartIndex + i, 0].Value = ((CheckBox)sender).Checked;
                        checkCount = 0;
                    }
                }
            }
            setButtenEnable();
        }

        /// <summary>
        /// 导出按钮事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tsBtnExport_Click(object sender, EventArgs e)
        {
            if (FileExport.fileExport(fpK1Value, true))
            {
                Alert.alert(Const.EXPORT_SUCCESS_MSG);
            }
        }

        private void farpointFilter1_OnCheckFilterChanged(object sender, EventArgs arg)
        {
            CheckBox chk = (CheckBox)sender;
            //当Checkbox选中时，筛选过程中则将不符合条件的数据隐藏
            if (chk.Checked == true)
            {
                //禁用选择颜色相关控件
                farpointFilter1.EnableChooseColorCtrls(false);
                //设置自动隐藏过滤条件
                FarpointDefaultPropertiesSetter.SetFpFilterHideProperties(this.fpK1Value, _filterColunmIdxs);

            }
            else//未选中时，根据用户自定义的颜色进行分类显示
            {
                //启用选择颜色相关控件
                farpointFilter1.EnableChooseColorCtrls(true);
                //设置自定义过滤条件
                FarpointDefaultPropertiesSetter.SetFpCustomFilterProperties(this.fpK1Value, farpointFilter1.GetSelectedFitColor(), farpointFilter1.GetSelectedNotFitColor(), _filterColunmIdxs);
            }
        }

        private void farpointFilter1_OnClickClearFilterBtn(object sender, EventArgs arg)
        {
            //清空过滤条件
            this.fpK1Value.ActiveSheet.RowFilter.ResetFilter();
        }

        private void farpointFilter1_OnClickFitColorBtnOK(object sender, EventArgs arg)
        {
            //根据新的颜色值设置自动隐藏过滤条件
            FarpointDefaultPropertiesSetter.SetFpCustomFilterProperties(this.fpK1Value, farpointFilter1.GetSelectedFitColor(), farpointFilter1.GetSelectedNotFitColor(), _filterColunmIdxs);
        }

        private void farpointFilter1_OnClickNotFitColorBtnOK(object sender, EventArgs arg)
        {
            //根据新的颜色值设置自动隐藏过滤条件
            FarpointDefaultPropertiesSetter.SetFpCustomFilterProperties(this.fpK1Value, farpointFilter1.GetSelectedFitColor(), farpointFilter1.GetSelectedNotFitColor(), _filterColunmIdxs);
        }

        private void _btnSearch_Click(object sender, EventArgs e)
        {
            bindfpK1Value();
        }

    }
}
