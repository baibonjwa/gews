// ******************************************************************
// 概  述：勘探钻孔数据管理界面
// 作  者：伍鑫
// 创建日期：2013/12/05
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
using System.Collections;
using LibEntity;
using LibCommon;
using LibCommonControl;
using GIS.Common;
using ESRI.ArcGIS.Carto;

namespace _3.GeologyMeasure
{
    public partial class BoreholeInfoManagement : Form
    {
        /** 明细部开始index位置 **/
        private const int _iRowDetailStartIndex = 4;
        /** 保存所有用户选中的行的索引 **/
        private Hashtable _htSelIdxs = new Hashtable();
        /** 钻孔RowCount **/
        private int _iBoreholeRowCount = 0;
        /** 钻孔岩性RowCount **/
        private int _iBoreholeLithologyRowCount = 0;
        /** farpoint最大显示行数 **/
        private int _farpointMaxCnt = 500;
        private int _BIDIndex = 13;

        /// <summary>
        /// 构造方法
        /// </summary>
        public BoreholeInfoManagement()
        {
            InitializeComponent();

            // 设置窗体默认属性
            LibCommon.FormDefaultPropertiesSetter.SetManagementFormDefaultProperties(this, Const_GM.MANAGE_BOREHOLE_INFO);

            // 设置Farpoint默认属性
            LibCommon.FarpointDefaultPropertiesSetter.SetFpDefaultProperties(this.fpBoreholeInfo, Const_GM.MANAGE_BOREHOLE_INFO, _iRowDetailStartIndex);

            // 调用委托方法 （必须实装）
            dataPager1.FrmChild_EventHandler += FrmParent_EventHandler;

            // 加载钻孔信息
            loadBoreholeInfo();
        }

        /// <summary>
        /// 调用委托方法 （必须实装）
        /// </summary>
        /// <param name="sender"></param>
        private void FrmParent_EventHandler(object sender)
        {
            // 加载钻孔信息
            loadBoreholeInfo();
        }

        /// <summary>
        /// 加载钻孔信息
        /// </summary>
        private void loadBoreholeInfo()
        {
            // 修改按钮设为不可用
            this.btnUpdate.Enabled = false;
            // 删除按钮设为不可用
            this.btnDelete.Enabled = false;
            // 全选/全不选checkbox设为未选中
            this.chkSelAll.Checked = false;

            // 清空HashTabl
            _htSelIdxs.Clear();

            // 设定farpoint初始化显示行数
            this.fpBoreholeInfo.Sheets[0].Rows.Count = _farpointMaxCnt;

            // 删除farpoint明细部
            // 解决修改、删除某条数据后，重新load的时候，选择列checkbox不恢复成默认（不选择）的BUG
            // 解决删除全部数据后，再添加一行，报错的BUG
            if (this.fpBoreholeInfo.Sheets[0].Rows.Count != _iRowDetailStartIndex)
            {
                this.fpBoreholeInfo.Sheets[0].Rows.Remove(_iRowDetailStartIndex, _iBoreholeLithologyRowCount);
            }
            else
            {
                _iBoreholeLithologyRowCount = 0;
            }

            // 获取全部数据件数
            int iRecordCount = BoreholeBLL.selectAllBoreholeInfo().Tables[0].Rows.Count;

            // 调用分页控件初始化方法
            dataPager1.PageControlInit(iRecordCount);

            // 获取要检索数据的开始位置和结束位置
            int iStartIndex = dataPager1.getStartIndex();
            int iEndIndex = dataPager1.getEndIndex();

            // 临时变量，存储明细部开始index位置
            int iTmpIdx = _iRowDetailStartIndex;

            // 获取开始位置和结束位置之间的数据
            // 说明：如果画面当前显示的件数是10，那么init时开始位置为1，结束位置为10，点击下一页后，开始位置变为11，结束位置变为20
            DataSet dsBorehole = BoreholeBLL.selectBoreholeInfoForPage(iStartIndex, iEndIndex);

            // 当前检索件数
            int iCountBorehole = dsBorehole.Tables[0].Rows.Count;

            //this.fpBoreholeInfo.Sheets[0].Rows.Count = 500;

            // 检索件数 > 0 的场合
            if (iCountBorehole > 0)
            {
                // 钻孔RowCount统计件数
                this._iBoreholeRowCount = iCountBorehole;
                // 钻孔岩性RowCount清零
                _iBoreholeLithologyRowCount = 0;

                // 循环结果集
                for (int i = 0; i < iCountBorehole; i++)
                {
                    // 钻孔编号
                    int iBoreholeId = Convert.ToInt32(dsBorehole.Tables[0].Rows[i][BoreholeDBConstNames.BOREHOLE_ID].ToString());

                    // 取得钻孔岩性信息
                    DataSet dsBoreholeLithology = BoreholeLithologyBLL.selectBoreholeLithologyInfoByBoreholeId(iBoreholeId);
                    // 钻孔岩性信息取得件数
                    int iCountBoreholeLithology = dsBoreholeLithology.Tables[0].Rows.Count;

                    // 钻孔岩性信息取得件数 > 0 的场合
                    if (iCountBoreholeLithology > 0)
                    {
                        // 钻孔岩性RowCount统计件数
                        _iBoreholeLithologyRowCount = _iBoreholeLithologyRowCount + iCountBoreholeLithology;

                        // ******************************合并单元格*********************************************
                        // 选择列
                        this.fpBoreholeInfo.ActiveSheet.AddSpanCell(iTmpIdx + i, 0, iCountBoreholeLithology, 1);
                        // 孔号列
                        this.fpBoreholeInfo.ActiveSheet.AddSpanCell(iTmpIdx + i, 1, iCountBoreholeLithology, 1);
                        // 地面标高列
                        this.fpBoreholeInfo.ActiveSheet.AddSpanCell(iTmpIdx + i, 2, iCountBoreholeLithology, 1);
                        // X坐标列
                        this.fpBoreholeInfo.ActiveSheet.AddSpanCell(iTmpIdx + i, 3, iCountBoreholeLithology, 1);
                        // Y坐标列
                        this.fpBoreholeInfo.ActiveSheet.AddSpanCell(iTmpIdx + i, 4, iCountBoreholeLithology, 1);
                        // Z坐标列
                        this.fpBoreholeInfo.ActiveSheet.AddSpanCell(iTmpIdx + i, 5, iCountBoreholeLithology, 1);
                        // ******************************合并单元格*********************************************
                    }

                    // 选择
                    FarPoint.Win.Spread.CellType.CheckBoxCellType objCheckCell = new FarPoint.Win.Spread.CellType.CheckBoxCellType();
                    this.fpBoreholeInfo.Sheets[0].Cells[iTmpIdx + i, 0].CellType = objCheckCell;

                    // 孔号
                    this.fpBoreholeInfo.Sheets[0].Cells[iTmpIdx + i, 1].Text = dsBorehole.Tables[0].Rows[i][BoreholeDBConstNames.BOREHOLE_NUMBER].ToString();

                    // 地面标高
                    this.fpBoreholeInfo.Sheets[0].Cells[iTmpIdx + i, 2].Text = dsBorehole.Tables[0].Rows[i][BoreholeDBConstNames.GROUND_ELEVATION].ToString();
                    // X坐标
                    this.fpBoreholeInfo.Sheets[0].Cells[iTmpIdx + i, 3].Text = dsBorehole.Tables[0].Rows[i][BoreholeDBConstNames.COORDINATE_X].ToString();
                    // Y坐标
                    this.fpBoreholeInfo.Sheets[0].Cells[iTmpIdx + i, 4].Text = dsBorehole.Tables[0].Rows[i][BoreholeDBConstNames.COORDINATE_Y].ToString();
                    // Z坐标
                    this.fpBoreholeInfo.Sheets[0].Cells[iTmpIdx + i, 5].Text = dsBorehole.Tables[0].Rows[i][BoreholeDBConstNames.COORDINATE_Z].ToString();

                    if (i % 2 == 0)
                    {
                        this.fpBoreholeInfo.Sheets[0].Cells[iTmpIdx + i, 0].BackColor = Const.DOUBLE_LINE;
                        this.fpBoreholeInfo.Sheets[0].Cells[iTmpIdx + i, 1].BackColor = Const.DOUBLE_LINE;
                        this.fpBoreholeInfo.Sheets[0].Cells[iTmpIdx + i, 2].BackColor = Const.DOUBLE_LINE;
                        this.fpBoreholeInfo.Sheets[0].Cells[iTmpIdx + i, 3].BackColor = Const.DOUBLE_LINE;
                        this.fpBoreholeInfo.Sheets[0].Cells[iTmpIdx + i, 4].BackColor = Const.DOUBLE_LINE;
                        this.fpBoreholeInfo.Sheets[0].Cells[iTmpIdx + i, 5].BackColor = Const.DOUBLE_LINE;
                    }
                    else
                    {
                        this.fpBoreholeInfo.Sheets[0].Cells[iTmpIdx + i, 0].BackColor = Const.SINGLE_LINE;
                        this.fpBoreholeInfo.Sheets[0].Cells[iTmpIdx + i, 1].BackColor = Const.SINGLE_LINE;
                        this.fpBoreholeInfo.Sheets[0].Cells[iTmpIdx + i, 2].BackColor = Const.SINGLE_LINE;
                        this.fpBoreholeInfo.Sheets[0].Cells[iTmpIdx + i, 3].BackColor = Const.SINGLE_LINE;
                        this.fpBoreholeInfo.Sheets[0].Cells[iTmpIdx + i, 4].BackColor = Const.SINGLE_LINE;
                        this.fpBoreholeInfo.Sheets[0].Cells[iTmpIdx + i, 5].BackColor = Const.SINGLE_LINE;
                    }

                    // 煤层结构
                    for (int n = 0; n < iCountBoreholeLithology; n++)
                    {
                        // 岩性
                        this.fpBoreholeInfo.Sheets[0].Cells[iTmpIdx + i + n, 6].Text = dsBoreholeLithology.Tables[0].Rows[n][LithologyDbConstNames.LITHOLOGY_NAME].ToString();
                        // 底板高
                        this.fpBoreholeInfo.Sheets[0].Cells[iTmpIdx + i + n, 7].Text = dsBoreholeLithology.Tables[0].Rows[n][BoreholeLithologyDbConstNames.FLOOR_ELEVATION].ToString();
                        // 厚度
                        this.fpBoreholeInfo.Sheets[0].Cells[iTmpIdx + i + n, 8].Text = dsBoreholeLithology.Tables[0].Rows[n][BoreholeLithologyDbConstNames.THICKNESS].ToString();
                        // 煤层名称
                        this.fpBoreholeInfo.Sheets[0].Cells[iTmpIdx + i + n, 9].Text = dsBoreholeLithology.Tables[0].Rows[n][BoreholeLithologyDbConstNames.COAL_SEAMS_NAME].ToString();

                        // 坐标X
                        string strCoordinate_X = dsBoreholeLithology.Tables[0].Rows[n][BoreholeLithologyDbConstNames.COORDINATE_X].ToString();
                        this.fpBoreholeInfo.Sheets[0].Cells[iTmpIdx + i + n, 10].Text = (strCoordinate_X == Const.DOUBLE_DEFAULT_VALUE ? "" : strCoordinate_X);

                        // 坐标Y
                        string strCoordinate_Y = dsBoreholeLithology.Tables[0].Rows[n][BoreholeLithologyDbConstNames.COORDINATE_Y].ToString();
                        this.fpBoreholeInfo.Sheets[0].Cells[iTmpIdx + i + n, 11].Text = (strCoordinate_Y == Const.DOUBLE_DEFAULT_VALUE ? "" : strCoordinate_Y);

                        // 坐标Z
                        string strCoordinate_Z = dsBoreholeLithology.Tables[0].Rows[n][BoreholeLithologyDbConstNames.COORDINATE_Z].ToString();
                        this.fpBoreholeInfo.Sheets[0].Cells[iTmpIdx + i + n, 12].Text = (strCoordinate_Z == Const.DOUBLE_DEFAULT_VALUE ? "" : strCoordinate_Z);

                        if (i % 2 == 0)
                        {
                            this.fpBoreholeInfo.Sheets[0].Cells[iTmpIdx + i + n, 6].BackColor = Const.DOUBLE_LINE;
                            this.fpBoreholeInfo.Sheets[0].Cells[iTmpIdx + i + n, 7].BackColor = Const.DOUBLE_LINE;
                            this.fpBoreholeInfo.Sheets[0].Cells[iTmpIdx + i + n, 8].BackColor = Const.DOUBLE_LINE;
                            this.fpBoreholeInfo.Sheets[0].Cells[iTmpIdx + i + n, 9].BackColor = Const.DOUBLE_LINE;
                            this.fpBoreholeInfo.Sheets[0].Cells[iTmpIdx + i + n, 10].BackColor = Const.DOUBLE_LINE;
                            this.fpBoreholeInfo.Sheets[0].Cells[iTmpIdx + i + n, 11].BackColor = Const.DOUBLE_LINE;
                            this.fpBoreholeInfo.Sheets[0].Cells[iTmpIdx + i + n, 12].BackColor = Const.DOUBLE_LINE;
                        }
                        else
                        {
                            this.fpBoreholeInfo.Sheets[0].Cells[iTmpIdx + i + n, 6].BackColor = Const.SINGLE_LINE;
                            this.fpBoreholeInfo.Sheets[0].Cells[iTmpIdx + i + n, 7].BackColor = Const.SINGLE_LINE;
                            this.fpBoreholeInfo.Sheets[0].Cells[iTmpIdx + i + n, 8].BackColor = Const.SINGLE_LINE;
                            this.fpBoreholeInfo.Sheets[0].Cells[iTmpIdx + i + n, 9].BackColor = Const.SINGLE_LINE;
                            this.fpBoreholeInfo.Sheets[0].Cells[iTmpIdx + i + n, 10].BackColor = Const.SINGLE_LINE;
                            this.fpBoreholeInfo.Sheets[0].Cells[iTmpIdx + i + n, 11].BackColor = Const.SINGLE_LINE;
                            this.fpBoreholeInfo.Sheets[0].Cells[iTmpIdx + i + n, 12].BackColor = Const.SINGLE_LINE;
                        }

                    }

                    // bid
                    this.fpBoreholeInfo.Sheets[0].Cells[iTmpIdx + i, 13].Text = dsBorehole.Tables[0].Rows[i][BoreholeDBConstNames.BID].ToString();
                    fpBoreholeInfo.Sheets[0].Columns[13].Visible = false;
                    // 保存count数
                    iTmpIdx = iTmpIdx + iCountBoreholeLithology - 1;
                }

                // 重新设定farpoint显示行数
                this.fpBoreholeInfo.Sheets[0].Rows.Count = _iRowDetailStartIndex + _iBoreholeLithologyRowCount;

            }
            else
            {
                this.fpBoreholeInfo.Sheets[0].Rows.Count = _iRowDetailStartIndex;
            }


        }

        /// <summary>
        /// 添加
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnAdd_Click(object sender, EventArgs e)
        {
            BoreholeInfoEntering boreholeInfoEnteringForm = new BoreholeInfoEntering(this);
            boreholeInfoEnteringForm.Show(this);
        }
        public void refreshAdd()
        { 
            // 加载钻孔信息
            loadBoreholeInfo();
            // 跳转到尾页
            this.dataPager1.btnLastPage_Click(null, null);

            // 设置farpoint焦点（必须实装）
            this.fpBoreholeInfo.Sheets[0].SetActiveCell(this.fpBoreholeInfo.Sheets[0].Rows.Count, 0);
        }
        /// <summary>
        /// 修改
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnUpdate_Click(object sender, EventArgs e)
        {
            // 获取已选择明细行的索引
            int[] iSelIdxsArr = GetSelIdxs();
            //孔号(非主键但唯一)
            string strBoreholeNumber = fpBoreholeInfo.Sheets[0].Cells[iSelIdxsArr[0], 1].Text;
            int iboreholeId = -1;
            if (!BoreholeBLL.selectBoreholeIdByBoreholeNum(strBoreholeNumber, out iboreholeId))
            {
                Alert.alert(Const_GM.CAN_NOT_GET_BOREHOLE_ID); // 无法根据孔号获取钻孔ID！
                return;
            }
            selidxs = iSelIdxsArr[0];
            BoreholeInfoEntering boreholeInfoEnteringForm = new BoreholeInfoEntering(iboreholeId,this);
            boreholeInfoEnteringForm.Show(this);

        }
        int selidxs = -1;
        public void refreshUpdate()
        { 
            // 加载钻孔信息
            loadBoreholeInfo();
            // 设置farpoint焦点（必须实装）
            this.fpBoreholeInfo.Sheets[0].SetActiveCell(selidxs, 0);
        }

        /// <summary>
        /// 删除按钮
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (Alert.confirm(Const_GM.DEL_CONFIRM_MSG_BOREHOLE))
            {
                // 获取已选择明细行的索引
                int[] iSelIdxsArr = GetSelIdxs();

                // 存放主键的数组
                int[] iPkIdxsArr = new int[iSelIdxsArr.Length];

                //20140428 lyf 存放钻孔绑定ID
                string[] sBoreholeBIDArray = new string[iSelIdxsArr.Length];

                for (int i = 0; i < iSelIdxsArr.Length; i++)
                {
                    // 获取主键
                    string strBoreholeNumber = fpBoreholeInfo.Sheets[0].Cells[iSelIdxsArr[i], 1].Text;
                    if (strBoreholeNumber != "")
                    {
                        int iboreholeId = -1;
                        if (!BoreholeBLL.selectBoreholeIdByBoreholeNum(strBoreholeNumber, out iboreholeId))
                        {
                            Alert.alert(Const_GM.CAN_NOT_GET_BOREHOLE_ID); // 无法根据孔号获取钻孔ID！
                        }
                        iPkIdxsArr[i] = Convert.ToInt32(iboreholeId);

                        //20140428 lyf 获取要删除钻孔的绑定ID
                        string sBoreholeBID = "";
                        if (!BoreholeBLL.selectBoreholeBIDByBoreholeNum(strBoreholeNumber, out sBoreholeBID))
                        {
                            Alert.alert(Const_GM.CAN_NOT_GET_BOREHOLE_BID); // 无法根据孔号获取钻孔绑定ID！
                        }
                        sBoreholeBIDArray[i] = sBoreholeBID;
                    }

                }

                // 钻孔数据删除
                bool bResult = BoreholeBLL.deleteBoreholeInfo(iPkIdxsArr);

                //20140428 lyf 根据钻孔绑定ID删除图元
                DeleteZuanKongByBID(sBoreholeBIDArray);

                // 删除成功的场合
                if (bResult)
                {
                    // 加载钻孔信息
                    loadBoreholeInfo();

                    // 设置farpoint焦点（必须实装）
                    this.fpBoreholeInfo.Sheets[0].SetActiveCell(iSelIdxsArr[0], 0);
                }
            }
        }

        #region 删除钻孔图元

        /// <summary>
        /// 根据钻孔绑定ID删除钻孔图元
        /// </summary>
        /// <param name="sBoreholeBIDArray">要删除钻孔的绑定ID</param>
        private void DeleteZuanKongByBID(string[] sBoreholeBIDArray)
        {
            if (sBoreholeBIDArray.Length == 0) return;

            //1.获得当前编辑图层
            DrawSpecialCommon drawspecial = new DrawSpecialCommon();
            string sLayerAliasName = GIS.LayerNames.DEFALUT_BOREHOLE;//“默认_钻孔”图层
            IFeatureLayer featureLayer = drawspecial.GetFeatureLayerByName(sLayerAliasName);
            if (featureLayer == null)
            {
                MessageBox.Show("未找到" + sLayerAliasName + "图层,无法删除钻孔图元。");
                return;
            }

            //2.删除钻孔图元
            string sBoreholeBID = "";
            for (int i = 0; i < sBoreholeBIDArray.Length; i++)
            {
                sBoreholeBID = sBoreholeBIDArray[i];

                DataEditCommon.DeleteFeatureByBId(featureLayer, sBoreholeBID);
            }
        }
        #endregion

        /// <summary>
        /// 获取farpoint中选中的所有行
        /// </summary>
        /// <returns>注意，返回值可能是null，null则代表一个也没选中</returns>
        private int[] GetSelIdxs()
        {
            if (this._htSelIdxs.Count == 0)
            {
                return null;
            }
            int[] retArr = new int[this._htSelIdxs.Count];
            this._htSelIdxs.Keys.CopyTo(retArr, 0);
            return retArr;
        }

        /// <summary>
        /// farpoint的ButtonClicked事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void fpBoreholeInfo_ButtonClicked(object sender, FarPoint.Win.Spread.EditorNotifyEventArgs e)
        {
            // 判断点击的空间类型是否是.FpCheckBox)
            if (e.EditingControl is FarPoint.Win.FpCheckBox)
            {
                FarPoint.Win.FpCheckBox fpChk = (FarPoint.Win.FpCheckBox)e.EditingControl;
                // 判断是否被选中
                if (fpChk.Checked)
                {
                    // 保存索引号
                    if (!_htSelIdxs.Contains(e.Row))
                    {
                        _htSelIdxs.Add(e.Row, true);

                        // 点击每条记录知道全部选中的情况下，全选/全不选checkbox设为选中
                        if (_htSelIdxs.Count == _iBoreholeRowCount)
                        {
                            // 全选/全不选checkbox设为选中
                            this.chkSelAll.Checked = true;
                        }
                    }
                }
                else
                {
                    // 移除索引号
                    _htSelIdxs.Remove(e.Row);

                    // 全选/全不选checkbox设为未选中
                    this.chkSelAll.Checked = false;
                }

                // 如果保存索引号的Hashtable中保存的索引件数是1，则修改按钮设为可用，否则设为不可用
                this.btnUpdate.Enabled = (_htSelIdxs.Count == 1) ? true : false;
                // 删除按钮
                this.btnDelete.Enabled = (_htSelIdxs.Count >= 1) ? true : false;
            }
        }

        /// <summary>
        /// 全选/全不选checkbox的click事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void chkSelAll_Click(object sender, EventArgs e)
        {
            // 全不选的情况下
            if (_htSelIdxs.Count == _iBoreholeRowCount)
            {
                // 循环明细
                for (int i = 0; i < _iBoreholeLithologyRowCount; i++)
                {
                    // 将所有明细的checkbox设为未选中
                    this.fpBoreholeInfo.Sheets[0].Cells[_iRowDetailStartIndex + i, 0].Value = ((CheckBox)sender).Checked;
                    // 将存有选中项目的数组清空
                    _htSelIdxs.Remove(_iRowDetailStartIndex + i);
                }
                // 删除按钮设为不可用
                this.btnDelete.Enabled = false;
            }
            // 全选的情况下
            else
            {
                // 循环明细
                for (int i = 0; i < _iBoreholeLithologyRowCount; i++)
                {
                    // 将所有明细设为全选中
                    this.fpBoreholeInfo.Sheets[0].Cells[_iRowDetailStartIndex + i, 0].Value = ((CheckBox)sender).Checked;

                    if (this.fpBoreholeInfo.Sheets[0].Cells[_iRowDetailStartIndex + i, 0].CellType is
                        FarPoint.Win.Spread.CellType.CheckBoxCellType)
                    {
                        // 将选中明细的索引添加到数组中，如果已经存在不要二次添加
                        if (!_htSelIdxs.Contains(_iRowDetailStartIndex + i))
                        {
                            _htSelIdxs.Add(_iRowDetailStartIndex + i, true);
                        }
                    }
                }
                // 删除按钮设为可用
                this.btnDelete.Enabled = true;
            }

            // 如果保存索引号的Hashtable中保存的索引件数是1，则修改按钮设为可用，否则设为不可用
            this.btnUpdate.Enabled = (_htSelIdxs.Count == 1) ? true : false;
            // 删除按钮
            this.btnDelete.Enabled = (_htSelIdxs.Count >= 1) ? true : false;
        }

        /// <summary>
        /// 退出
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnExit_Click(object sender, EventArgs e)
        {
            // 关闭窗口
            this.Close();
        }

        /// <summary>
        /// 取消
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnCancel_Click(object sender, EventArgs e)
        {
            // 关闭窗口
            this.Close();
        }

        /// <summary>
        /// 确定
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnOK_Click(object sender, EventArgs e)
        {
            // 关闭窗口
            this.Close();
        }

        /// <summary>
        /// 导出
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tsBtnExport_Click(object sender, EventArgs e)
        {
            if (FileExport.fileExport(fpBoreholeInfo, true))
            {
                Alert.alert(Const.EXPORT_SUCCESS_MSG);
            }
        }

        /// <summary>
        /// 打印
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tsBtnPrint_Click(object sender, EventArgs e)
        {
            FilePrint.CommonPrint(this.fpBoreholeInfo, 0);
        }

        /// <summary>
        /// 刷新
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnRefresh_Click(object sender, EventArgs e)
        {
            // 加载钻孔信息
            loadBoreholeInfo();
        }


        /// <summary>
        /// 图显按钮事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnMap_Click(object sender, EventArgs e)
        {
            // 获取已选择明细行的索引
            int[] iSelIdxsArr = GetSelIdxs();
            if (iSelIdxsArr == null)
            {
                MessageBox.Show("未选中数据行！");
                return;
            }
            string bid = "";
            ILayer pLayer = GIS.Common.DataEditCommon.GetLayerByName(GIS.Common.DataEditCommon.g_pMap, GIS.LayerNames.DEFALUT_BOREHOLE);
            if (pLayer == null)
            {
                MessageBox.Show("未发现钻孔图层！");
                return;
            }
            IFeatureLayer pFeatureLayer = (IFeatureLayer)pLayer;
            string str = "";
            for (int i = 0; i < iSelIdxsArr.Length; i++)
            {
                bid = this.fpBoreholeInfo.Sheets[0].Cells[iSelIdxsArr[i], _BIDIndex].Text.Trim();
                if (bid != "")
                {
                    if (i == 0)
                        str = "bid='" + bid + "'";
                    else
                        str += " or bid='" + bid + "'";
                }
            }
            List<ESRI.ArcGIS.Geodatabase.IFeature> list = GIS.MyMapHelp.FindFeatureListByWhereClause(pFeatureLayer, str);
            if (list.Count > 0)
            {
                GIS.MyMapHelp.Jump(GIS.MyMapHelp.GetGeoFromFeature(list));
                GIS.Common.DataEditCommon.g_pMap.ClearSelection();
                for (int i = 0; i < list.Count; i++)
                {
                    GIS.Common.DataEditCommon.g_pMap.SelectFeature(pLayer, list[i]);
                }
                this.WindowState = FormWindowState.Normal;
                this.Location = GIS.Common.DataEditCommon.g_axTocControl.Location;
                this.Width = GIS.Common.DataEditCommon.g_axTocControl.Width;
                this.Height = GIS.Common.DataEditCommon.g_axTocControl.Height;
                GIS.Common.DataEditCommon.g_pMyMapCtrl.ActiveView.PartialRefresh(esriViewDrawPhase.esriViewGeoSelection, null, GIS.Common.DataEditCommon.g_pAxMapControl.Extent);
            }
            else
            {
                Alert.alert("图元丢失");
            }
        }
    }
}
