// ******************************************************************
// 概  述：S值信息管理
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
    public partial class SValueManagement : BaseForm
    {
        /********************************/
        private int _iRecordCount = 0;
        int rowsCount = 0;      //数据行数
        int checkCount = 0;     //选择行数
        int rowDetailStartIndex = 3;
        int _tmpRowIndex = 0;
        //需要过滤的列索引
        private int[] _filterColunmIdxs = null;
        SValue sValueEntity = new SValue();
        Tunnel tunnelEntity = new Tunnel();
        DataSet ds = new DataSet();
        DataSet dsAll = new DataSet();
        int[] arr = new int[5];
        /********************************/

        /// <summary>
        /// 构造方法
        /// </summary>
        public SValueManagement(MainFrm mainFrm)
        {
            InitializeComponent();
            this.MainForm = mainFrm;
            dataPager1.FrmChild_EventHandler += FrmParent_EventHandler;
            LibCommon.FormDefaultPropertiesSetter.SetManagementFormDefaultProperties(this, Const_OP.S_VALUE_MANAGEMENT);
            LibCommon.FarpointDefaultPropertiesSetter.SetFpDefaultProperties(fpSValue, LibCommon.Const_OP.S_VALUE_FARPOINT_TITLE, rowDetailStartIndex);
            _filterColunmIdxs = new int[]
            {
                8,
            };
            //禁用选择颜色相关控件
            farpointFilter1.EnableChooseColorCtrls(false);
            //设置自动隐藏过滤条件
            FarpointDefaultPropertiesSetter.SetFpFilterHideProperties(this.fpSValue, _filterColunmIdxs);
        }

        /// <summary>
        /// 初始化
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SValueManagement_Load(object sender, EventArgs e)
        {
            bindfpSValue();
        }

        /// <summary>
        /// 委托方法
        /// </summary>
        /// <param name="sender"></param>
        private void FrmParent_EventHandler(object sender)
        {
            bindfpSValue();
        }

        #region ******菜单按钮******
        /// <summary>
        /// 添加按钮事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnAdd_Click(object sender, EventArgs e)
        {
            SValueEntering sValue = new SValueEntering(this.MainForm);
            if (DialogResult.OK == sValue.ShowDialog())
            {
                bindfpSValue();
                //跳转到最后一页
                this.dataPager1.btnLastPage_Click(sender, e);
                FarPointOperate.farPointFocusSetAdd(fpSValue, rowDetailStartIndex, rowsCount);
            }
        }

        /// <summary>
        /// 修改按钮事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnUpdate_Click(object sender, EventArgs e)
        {
            setSValueEntity();
            _tmpRowIndex = fpSValue.ActiveSheet.ActiveRowIndex;
            SValueEntering sValue = new SValueEntering(arr, sValueEntity.ID, this.MainForm);
            if (DialogResult.OK == sValue.ShowDialog())
            {
                bindfpSValue();
                FarPointOperate.farPointFocusSetChange(fpSValue, _tmpRowIndex);
            }
        }

        /// <summary>
        /// 删除按钮事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (Alert.confirm(Const.DEL_CONFIRM_MSG))
            {
                bool bResult = false;
                for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                {
                    if (fpSValue.Sheets[0].Cells[rowDetailStartIndex + i, 0].Value != null && (bool)fpSValue.Sheets[0].Cells[rowDetailStartIndex + i, 0].Value == true)
                    {
                        int tmpInt = 0;
                        //S值ID
                        if (int.TryParse(ds.Tables[0].Rows[i][SValueDbConstNames.ID].ToString(), out tmpInt))
                        {
                            sValueEntity.ID = tmpInt;
                            tmpInt = 0;
                            bResult = SValueBLL.deleteValueS(sValueEntity);
                            
                        }
                    }
                }
                if (bResult)
                {
                    //TODO:删除成功
                    bindfpSValue();
                    FarPointOperate.farPointFocusSetDel(fpSValue, _tmpRowIndex);
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
            bindfpSValue();
        }

        /// <summary>
        /// 退出按钮事件
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
        /// 绑定数据
        /// </summary>
        private void bindfpSValue()
        {
            //清空Farpoint
            FarPointOperate.farPointClear(fpSValue, rowDetailStartIndex, rowsCount);

            chkSelAll.Checked = false;
            rowsCount = 0;
            checkCount = 0;

            _iRecordCount = SValueBLL.selectValueS().Tables[0].Rows.Count;
            dataPager1.PageControlInit(_iRecordCount);
            int iStartIndex = dataPager1.getStartIndex();
            int iEndIndex = dataPager1.getEndIndex();
            ds = SValueBLL.selectValueSEntity(iStartIndex, iEndIndex);
            rowsCount = ds.Tables[0].Rows.Count;
            //重绘Farpoint
            FarPointOperate.farPointReAdd(fpSValue, rowDetailStartIndex, rowsCount);

            if (rowsCount > 0)
            {
                FarPoint.Win.Spread.CellType.CheckBoxCellType ckbxcell = new FarPoint.Win.Spread.CellType.CheckBoxCellType();
                ckbxcell.ThreeState = false;
                for (int i = 0; i < rowsCount; i++)
                {
                    int index = 0;
                    //tunnelEntity = TunnelInfoBLL.selectTunnelInfoByTunnelID(Convert.ToInt32(ds.Tables[0].Rows[i][SValueDbConstNames.TUNNEL_ID]));
                    sValueEntity = SValueBLL.selectValueSByID(Convert.ToInt32(ds.Tables[0].Rows[i][SValueDbConstNames.ID]));
                    this.fpSValue.Sheets[0].Cells[rowDetailStartIndex + i, index].CellType = ckbxcell;
                    //坐标X
                    this.fpSValue.Sheets[0].Cells[rowDetailStartIndex + i, ++index].Text = sValueEntity.CoordinateX.ToString();
                    //坐标Y
                    this.fpSValue.Sheets[0].Cells[rowDetailStartIndex + i, ++index].Text = sValueEntity.CoordinateY.ToString();
                    //坐标Z
                    this.fpSValue.Sheets[0].Cells[rowDetailStartIndex + i, ++index].Text = sValueEntity.CoordinateZ.ToString();
                    //Sg
                    this.fpSValue.Sheets[0].Cells[rowDetailStartIndex + i, ++index].Text = sValueEntity.ValueSg.ToString();
                    //Sv
                    this.fpSValue.Sheets[0].Cells[rowDetailStartIndex + i, ++index].Text = sValueEntity.ValueSv.ToString();
                    //q
                    this.fpSValue.Sheets[0].Cells[rowDetailStartIndex + i, ++index].Text = sValueEntity.Valueq.ToString();
                    //孔深
                    this.fpSValue.Sheets[0].Cells[rowDetailStartIndex + i, ++index].Text = sValueEntity.BoreholeDeep.ToString();
                    //巷道名称
                    this.fpSValue.Sheets[0].Cells[rowDetailStartIndex + i, ++index].Text = tunnelEntity.TunnelName;
                    //记录时间
                    this.fpSValue.Sheets[0].Cells[rowDetailStartIndex + i, ++index].Text = sValueEntity.Time.ToString();
                    //录入时间
                    this.fpSValue.Sheets[0].Cells[rowDetailStartIndex + i, ++index].Text = sValueEntity.TypeInTime.ToString();
                }
            }
            setButtenEnable();
        }

        /// <summary>
        /// 颜色交替
        /// </summary>
        /// <param name="color"></param>
        /// <returns></returns>
        private static Color changeColor(Color color)
        {
            if (color == Color.White)
            {
                return Color.Silver;
            }
            return Color.White;
        }

        /// <summary>
        /// S值实体赋值
        /// </summary>
        private void setSValueEntity()
        {
            for (int i = 0; i < rowsCount; i++)
            {
                if (fpSValue.Sheets[0].Cells[rowDetailStartIndex + i, 0].Value != null && (bool)fpSValue.Sheets[0].Cells[rowDetailStartIndex + i, 0].Value == true)
                {
                    int tmpInt = 0;
                    double tmpDouble = 0;
                    DateTime tmpDt = DateTime.Now;
                    //矿井编号
                    if (int.TryParse(ds.Tables[0].Rows[i][SValueDbConstNames.TUNNEL_ID].ToString(), out tmpInt))
                    {
                        sValueEntity.TunnelID = tmpInt;
                        tmpInt = 0;
                    }
                    //S值ID
                    if (int.TryParse(ds.Tables[0].Rows[i][SValueDbConstNames.ID].ToString(), out tmpInt))
                    {
                        sValueEntity.ID = tmpInt;
                        tmpInt = 0;
                    }
                    //S值分组ID
                    if (int.TryParse(ds.Tables[0].Rows[i][SValueDbConstNames.VALUE_S_ID].ToString(), out tmpInt))
                    {
                        sValueEntity.SValueID = tmpInt;
                        tmpInt = 0;
                    }
                    //坐标X
                    if (double.TryParse(ds.Tables[0].Rows[i][SValueDbConstNames.COORDINATE_X].ToString(), out tmpDouble))
                    {
                        sValueEntity.CoordinateX = tmpDouble;
                        tmpDouble = 0;
                    }
                    //坐标Y
                    if (double.TryParse(ds.Tables[0].Rows[i][SValueDbConstNames.COORDINATE_Y].ToString(), out tmpDouble))
                    {
                        sValueEntity.CoordinateY = tmpDouble;
                        tmpDouble = 0;
                    }
                    //坐标Z
                    if (double.TryParse(ds.Tables[0].Rows[i][SValueDbConstNames.COORDINATE_Z].ToString(), out tmpDouble))
                    {
                        sValueEntity.CoordinateZ = tmpDouble;
                        tmpDouble = 0;
                    }
                    //Sg
                    if (double.TryParse(ds.Tables[0].Rows[i][SValueDbConstNames.VALUE_SG].ToString(), out tmpDouble))
                    {
                        sValueEntity.ValueSg = tmpDouble;
                        tmpDouble = 0;
                    }
                    //Sv
                    if (double.TryParse(ds.Tables[0].Rows[i][SValueDbConstNames.VALUE_SV].ToString(), out tmpDouble))
                    {
                        sValueEntity.ValueSv = tmpDouble;
                        tmpDouble = 0;
                    }
                    //q
                    if (double.TryParse(ds.Tables[0].Rows[i][SValueDbConstNames.VALUE_Q].ToString(), out tmpDouble))
                    {
                        sValueEntity.Valueq = tmpDouble;
                        tmpDouble = 0;
                    }
                    //孔深
                    if (double.TryParse(ds.Tables[0].Rows[i][SValueDbConstNames.BOREHOLE_DEEP].ToString(), out tmpDouble))
                    {
                        sValueEntity.BoreholeDeep = tmpDouble;
                        tmpDouble = 0;
                    }
                    //记录时间
                    if (DateTime.TryParse(ds.Tables[0].Rows[i][SValueDbConstNames.TIME].ToString(), out tmpDt))
                    {
                        sValueEntity.Time = tmpDt;
                        tmpDt = DateTime.Now;
                    }
                    //录入时间
                    if (DateTime.TryParse(ds.Tables[0].Rows[i][SValueDbConstNames.TYPE_IN_TIME].ToString(), out tmpDt))
                    {
                        sValueEntity.TypeInTime = tmpDt;
                        tmpDt = DateTime.Now;
                    }
                    //巷道实体
                    //tunnelEntity = TunnelInfoBLL.selectTunnelInfoByTunnelID(sValueEntity.Tunnel);
                    arr[0] = tunnelEntity.WorkingFace.MiningArea.Horizontal.Mine.MineId;
                    arr[1] = tunnelEntity.WorkingFace.MiningArea.Horizontal.HorizontalId;
                    arr[2] = tunnelEntity.WorkingFace.MiningArea.MiningAreaId;
                    arr[3] = tunnelEntity.WorkingFace.WorkingFaceID;
                    arr[4] = tunnelEntity.TunnelId;
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
        /// Farpoint操作对全选反选的影响
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void fpSValue_ButtonClicked(object sender, FarPoint.Win.Spread.EditorNotifyEventArgs e)
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
                        this.fpSValue.Sheets[0].Cells[rowDetailStartIndex + i, 0].Value = ((CheckBox)sender).Checked;
                        checkCount = ds.Tables[0].Rows.Count;
                    }
                    //checkbox未选中
                    else
                    {
                        fpSValue.Sheets[0].Cells[rowDetailStartIndex + i, 0].Value = ((CheckBox)sender).Checked;
                        checkCount = 0;
                    }
                }
            }
            setButtenEnable();
        }

        /// <summary>
        /// 取消按钮事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnCancel_Click(object sender, EventArgs e)
        {
            //关闭窗体
            this.Close();
        }

        /// <summary>
        /// 确定按钮事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnOK_Click(object sender, EventArgs e)
        {
            //关闭窗体
            this.Close();
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
                FarpointDefaultPropertiesSetter.SetFpFilterHideProperties(this.fpSValue, _filterColunmIdxs);

            }
            else//未选中时，根据用户自定义的颜色进行分类显示
            {
                //启用选择颜色相关控件
                farpointFilter1.EnableChooseColorCtrls(true);
                //设置自定义过滤条件
                FarpointDefaultPropertiesSetter.SetFpCustomFilterProperties(this.fpSValue, farpointFilter1.GetSelectedFitColor(), farpointFilter1.GetSelectedNotFitColor(), _filterColunmIdxs);
            }
        }

        private void farpointFilter1_OnClickClearFilterBtn(object sender, EventArgs arg)
        {
            //清空过滤条件
            this.fpSValue.ActiveSheet.RowFilter.ResetFilter();
        }

        private void farpointFilter1_OnClickFitColorBtnOK(object sender, EventArgs arg)
        {
            //根据新的颜色值设置自动隐藏过滤条件
            FarpointDefaultPropertiesSetter.SetFpCustomFilterProperties(this.fpSValue, farpointFilter1.GetSelectedFitColor(), farpointFilter1.GetSelectedNotFitColor(), _filterColunmIdxs);
        }

        private void farpointFilter1_OnClickNotFitColorBtnOK(object sender, EventArgs arg)
        {
            //根据新的颜色值设置自动隐藏过滤条件
            FarpointDefaultPropertiesSetter.SetFpCustomFilterProperties(this.fpSValue, farpointFilter1.GetSelectedFitColor(), farpointFilter1.GetSelectedNotFitColor(), _filterColunmIdxs);
        }

        private void toolStripButton7_Click(object sender, EventArgs e)
        {

        }

        private void toolStripButton4_Click(object sender, EventArgs e)
        {

        }
    }
}
