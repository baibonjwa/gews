// ******************************************************************
// 概  述：井筒数据管理
// 作  者：伍鑫
// 创建日期：2014/03/27
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
using LibCommon;
using System.Collections;
using LibCommonControl;
using GIS.Common;
using ESRI.ArcGIS.Carto;

namespace _3.GeologyMeasure
{
    public partial class PitshaftInfoManagement : Form
    {
        /** 明细部开始index位置 **/
        private const int _iRowDetailStartIndex = 3;
        /** 保存所有用户选中的行的索引 **/
        private Hashtable _htSelIdxs = new Hashtable();
        /** 检索件数**/
        private int _iRowCount = 0;
        /** 主键index **/
        private int _primaryKeyIndex = 10;
        /** 需要过滤的列索引 **/
        private int[] _filterColunmIdxs = null;
        /** BIDindex **/
        private int _BIDIndex = 11;

        public PitshaftInfoManagement()
        {
            InitializeComponent();

            // 设置窗体默认属性
            LibCommon.FormDefaultPropertiesSetter.SetManagementFormDefaultProperties(this, Const_GM.MANAGE_PITSHAFT_INFO);

            // 设置Farpoint默认属性
            LibCommon.FarpointDefaultPropertiesSetter.SetFpDefaultProperties(this.fpPitshaftInfo, Const_GM.MANAGE_PITSHAFT_INFO, _iRowDetailStartIndex);

            // 调用委托方法 （必须实装）
            dataPager1.FrmChild_EventHandler += FrmParent_EventHandler;

            // 加载井筒信息
            loadPitshaftInfo();

            #region Farpoint自动过滤功能
            //初始化需要过滤功能的列
            _filterColunmIdxs = new int[]
            {
                2,
            };
            //禁用选择颜色相关控件
            farpointFilter1.EnableChooseColorCtrls(false);
            //设置自动隐藏过滤条件
            FarpointDefaultPropertiesSetter.SetFpFilterHideProperties(this.fpPitshaftInfo, _filterColunmIdxs);
            #endregion
        }

        /// <summary>
        /// 调用委托方法 （必须实装）
        /// </summary>
        /// <param name="sender"></param>
        private void FrmParent_EventHandler(object sender)
        {
            // 分页控件与Farpoint过滤绑定问题
            FarpointFilterSetter.ClearFpFilter(this.fpPitshaftInfo);

            // 加载井筒信息
            loadPitshaftInfo();
        }

        /// <summary>
        /// 加载井筒信息
        /// </summary>
        private void loadPitshaftInfo()
        {
            // 修改按钮设为不可用（必须实装）
            this.btnUpdate.Enabled = false;
            // 删除按钮设为不可用（必须实装）
            this.btnDelete.Enabled = false;
            // 全选/全不选checkbox设为未选中（必须实装）
            this.chkSelAll.Checked = false;

            // 清空HashTabl（必须实装）
            _htSelIdxs.Clear();

            // 删除farpoint明细部（必须实装）
            // 解决修改、删除某条数据后，重新load的时候，选择列checkbox不恢复成默认（不选择）的BUG
            // 解决删除全部数据后，再添加一行，报错的BUG
            if (this.fpPitshaftInfo.Sheets[0].Rows.Count != _iRowDetailStartIndex)
            {
                this.fpPitshaftInfo.Sheets[0].Rows.Remove(_iRowDetailStartIndex, _iRowCount);
            }
            else
            {
                _iRowCount = 0;
            }

            // 获取全部数据件数（必须实装）
            int iRecordCount = PitshaftBLL.selectAllPitshaftInfo().Tables[0].Rows.Count;

            // 调用分页控件初始化方法（必须实装）
            dataPager1.PageControlInit(iRecordCount);

            // 获取要检索数据的开始位置和结束位置 （必须实装）
            int iStartIndex = dataPager1.getStartIndex();
            int iEndIndex = dataPager1.getEndIndex();

            // 获取开始位置和结束位置之间的数据（必须实装）
            // 说明：如果画面当前显示的件数是10，那么init时开始位置为1，结束位置为10，点击下一页后，开始位置变为11，结束位置变为20
            DataSet ds = PitshaftBLL.selectPitshaftInfoForPage(iStartIndex, iEndIndex);

            // 当前检索件数（必须实装）
            int iSelCnt = ds.Tables[0].Rows.Count;

            // 重新设定farpoint显示行数 （必须实装）
            this.fpPitshaftInfo.Sheets[0].Rows.Count = _iRowDetailStartIndex + iSelCnt;

            // 检索件数 > 0 的场合
            if (iSelCnt > 0)
            {
                // 当前检索件数（必须实装）
                this._iRowCount = iSelCnt;

                // 循环结果集
                for (int i = 0; i < iSelCnt; i++)
                {
                    int index = 0;
                    // 选择
                    FarPoint.Win.Spread.CellType.CheckBoxCellType objCheckCell = new FarPoint.Win.Spread.CellType.CheckBoxCellType();
                    this.fpPitshaftInfo.Sheets[0].Cells[_iRowDetailStartIndex + i, index].CellType = objCheckCell;
                    // 井筒名称
                    this.fpPitshaftInfo.Sheets[0].Cells[_iRowDetailStartIndex + i, ++index].Text = ds.Tables[0].Rows[i][PitshaftDbConstNames.PITSHAFT_NAME].ToString();
                    
                    // 井筒类型
                    this.fpPitshaftInfo.Sheets[0].Cells[_iRowDetailStartIndex + i, ++index].Text = "";
                    int iPitshaftTypeId = 0;
                    if (int.TryParse(ds.Tables[0].Rows[i][PitshaftDbConstNames.PITSHAFT_TYPE_ID].ToString(), out iPitshaftTypeId))
                    {

                        DataSet dsPitshaftType = PitshaftTypeBLL.selectPitshaftTypeByPitshaftTypeId(iPitshaftTypeId);
                        if (dsPitshaftType.Tables[0].Rows.Count > 0)
                        {
                            this.fpPitshaftInfo.Sheets[0].Cells[_iRowDetailStartIndex + i, index].Text = dsPitshaftType.Tables[0].Rows[0][PitshaftTypeDbConstNames.PITSHAFT_TYPE_NAME].ToString();
                        }
                    }

                    // 井口标高
                    this.fpPitshaftInfo.Sheets[0].Cells[_iRowDetailStartIndex + i, ++index].Text = ds.Tables[0].Rows[i][PitshaftDbConstNames.WELLHEAD_ELEVATION].ToString();
                    // 井底标高
                    this.fpPitshaftInfo.Sheets[0].Cells[_iRowDetailStartIndex + i, ++index].Text = ds.Tables[0].Rows[i][PitshaftDbConstNames.WELLBOTTOM_ELEVATION].ToString();
                    // 井筒坐标X
                    this.fpPitshaftInfo.Sheets[0].Cells[_iRowDetailStartIndex + i, ++index].Text = ds.Tables[0].Rows[i][PitshaftDbConstNames.PITSHAFT_COORDINATE_X].ToString();
                    // 井筒坐标Y
                    this.fpPitshaftInfo.Sheets[0].Cells[_iRowDetailStartIndex + i, ++index].Text = ds.Tables[0].Rows[i][PitshaftDbConstNames.PITSHAFT_COORDINATE_Y].ToString();
                    // 图形X坐标
                    this.fpPitshaftInfo.Sheets[0].Cells[_iRowDetailStartIndex + i, ++index].Text = ds.Tables[0].Rows[i][PitshaftDbConstNames.FIGURE_COORDINATE_X].ToString();
                    // 图形Y坐标
                    this.fpPitshaftInfo.Sheets[0].Cells[_iRowDetailStartIndex + i, ++index].Text = ds.Tables[0].Rows[i][PitshaftDbConstNames.FIGURE_COORDINATE_Y].ToString();
                    // 图形Z坐标
                    string strCoordinate_Z = ds.Tables[0].Rows[i][PitshaftDbConstNames.FIGURE_COORDINATE_Z].ToString();
                    this.fpPitshaftInfo.Sheets[0].Cells[_iRowDetailStartIndex + i, ++index].Text = (strCoordinate_Z == Const.DOUBLE_DEFAULT_VALUE ? "" : strCoordinate_Z);

                    // 井筒编号
                    this.fpPitshaftInfo.Sheets[0].Cells[_iRowDetailStartIndex + i, ++index].Text = ds.Tables[0].Rows[i][PitshaftDbConstNames.PITSHAFT_ID].ToString();
                    this.fpPitshaftInfo.Sheets[0].Columns[index].Visible = false;

                    // BID
                    this.fpPitshaftInfo.Sheets[0].Cells[_iRowDetailStartIndex + i, ++index].Text = ds.Tables[0].Rows[i][PitshaftDbConstNames.BID].ToString();
                    this.fpPitshaftInfo.Sheets[0].Columns[index].Visible = false;
                }
            }
        }

        /// <summary>
        /// 添加（必须实装）
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnAdd_Click(object sender, EventArgs e)
        {
            PitshaftInfoEntering pitshaftInfoEnteringForm = new PitshaftInfoEntering(this);
            pitshaftInfoEnteringForm.Show(this);
        }
        public void refreshAdd()
        {
            // 加载井筒信息
            loadPitshaftInfo();
            // 跳转到尾页（必须实装）
            this.dataPager1.btnLastPage_Click(null, null);

            // 设置farpoint焦点（必须实装）
            this.fpPitshaftInfo.Sheets[0].SetActiveCell(this.fpPitshaftInfo.Sheets[0].Rows.Count, 0);
        }
        /// <summary>
        /// 修改（必须实装）
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnUpdate_Click(object sender, EventArgs e)
        {
            // 获取已选择明细行的索引
            int[] iSelIdxsArr = GetSelIdxs();
            // 获取编号（主键）
            string strPrimaryKey = this.fpPitshaftInfo.Sheets[0].Cells[iSelIdxsArr[0], _primaryKeyIndex].Text;

            PitshaftInfoEntering pitshaftInfoEnteringForm = new PitshaftInfoEntering(strPrimaryKey, Const_GM.UPDATE_PITSHAFT_INFO,this);
            pitshaftInfoEnteringForm.Show(this);
        }
        public void refreshUpdate()
        {
            // 加载井筒信息
            loadPitshaftInfo();

            // 设置farpoint焦点（必须实装）
            //this.fpPitshaftInfo.Sheets[0].SetActiveCell(iSelIdxsArr[0], 0);
            FarPointOperate.farPointFocusSetChange(this.fpPitshaftInfo, _primaryKeyIndex);
        }
        /// <summary>
        /// 删除按钮（必须实装）
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (Alert.confirm(Const_GM.DEL_CONFIRM_MSG_PITSHAFT))
            {
                // 获取已选择明细行的索引
                int[] iSelIdxsArr = GetSelIdxs();

                // 存放主键的数组
                int[] iPkIdxsArr = new int[iSelIdxsArr.Length];

                //20140428 lyf 存放井筒绑定ID
                string[] sPitshaftBIDArray = new string[iSelIdxsArr.Length];

                for (int i = 0; i < iSelIdxsArr.Length; i++)
                {
                    // 获取主键
                    string iPk = this.fpPitshaftInfo.Sheets[0].Cells[iSelIdxsArr[i], _primaryKeyIndex].Text;
                    iPkIdxsArr[i] = Convert.ToInt32(iPk);

                    ///20140428 lyf 
                    ///获取要删除井筒的绑定ID                    
                    string sPitshaftBID = "";
                    sPitshaftBID = PitshaftBLL.selectPitshaftInfoBIDByPitshaftPK(Convert.ToInt32(iPk));
                    if (sPitshaftBID != "")
                    {
                        sPitshaftBIDArray[i] = sPitshaftBID;
                    }

                }

                // 断层数据删除
                bool bResult = PitshaftBLL.deletePitshaftInfo(iPkIdxsArr);

                //20140428 lyf 根据井筒绑定ID删除图元
                DeleteJintTongByBID(sPitshaftBIDArray);

                // 删除成功的场合
                if (bResult)
                {
                    // 加载井筒信息
                    loadPitshaftInfo();

                    // 设置farpoint焦点（必须实装）
                    this.fpPitshaftInfo.Sheets[0].SetActiveCell(iSelIdxsArr[0], 0);
                }
            }
        }

        #region 删除井筒图元

        /// <summary>
        /// 根据井筒绑定ID删除井筒图元
        /// </summary>
        /// <param name="sPitshaftBIDArray">要删除井筒的绑定ID</param>
        private void DeleteJintTongByBID(string[] sPitshaftBIDArray)
        {
            if (sPitshaftBIDArray.Length == 0) return;

            //1.获得当前编辑图层
            DrawSpecialCommon drawspecial = new DrawSpecialCommon();
            string sLayerAliasName = GIS.LayerNames.DEFALUT_JINGTONG;//“默认_井筒”图层
            IFeatureLayer featureLayer = drawspecial.GetFeatureLayerByName(sLayerAliasName);
            if (featureLayer == null)
            {
                MessageBox.Show("未找到" + sLayerAliasName + "图层,无法删除井筒图元。");
                return;
            }

            //2.删除井筒图元
            string sPitshaftBID = "";
            for (int i = 0; i < sPitshaftBIDArray.Length; i++)
            {
                sPitshaftBID = sPitshaftBIDArray[i];

                DataEditCommon.DeleteFeatureByBId(featureLayer, sPitshaftBID);
            }
        }
        #endregion

        /// <summary>
        /// 获取farpoint中选中的所有行（必须实装）
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
        /// farpoint的ButtonClicked事件（必须实装）
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void fpPitshaftInfo_ButtonClicked(object sender, FarPoint.Win.Spread.EditorNotifyEventArgs e)
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
                        if (_htSelIdxs.Count == _iRowCount)
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
        /// 全选/全不选checkbox的click事件（必须实装）
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void chkSelAll_Click(object sender, EventArgs e)
        {
            // 全不选的情况下
            if (_htSelIdxs.Count == _iRowCount)
            {
                // 循环明细
                for (int i = 0; i < _iRowCount; i++)
                {
                    // 将所有明细的checkbox设为未选中
                    this.fpPitshaftInfo.Sheets[0].Cells[_iRowDetailStartIndex + i, 0].Value = ((CheckBox)sender).Checked;
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
                for (int i = 0; i < _iRowCount; i++)
                {
                    // 将所有明细设为全选中
                    this.fpPitshaftInfo.Sheets[0].Cells[_iRowDetailStartIndex + i, 0].Value = ((CheckBox)sender).Checked;
                    // 将选中明细的索引添加到数组中，如果已经存在不要二次添加
                    if (!_htSelIdxs.Contains(_iRowDetailStartIndex + i))
                    {
                        _htSelIdxs.Add(_iRowDetailStartIndex + i, true);
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
        /// farpointFilter1的OnCheckFilterChanged方法
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="arg"></param>
        private void farpointFilter1_OnCheckFilterChanged(object sender, EventArgs arg)
        {
            CheckBox chk = (CheckBox)sender;
            //当Checkbox选中时，筛选过程中则将不符合条件的数据隐藏
            if (chk.Checked == true)
            {
                //禁用选择颜色相关控件
                farpointFilter1.EnableChooseColorCtrls(false);
                //设置自动隐藏过滤条件
                FarpointDefaultPropertiesSetter.SetFpFilterHideProperties(this.fpPitshaftInfo, _filterColunmIdxs);

            }
            //未选中时，根据用户自定义的颜色进行分类显示
            else
            {
                //启用选择颜色相关控件
                farpointFilter1.EnableChooseColorCtrls(true);
                //设置自定义过滤条件
                FarpointDefaultPropertiesSetter.SetFpCustomFilterProperties(this.fpPitshaftInfo, farpointFilter1.GetSelectedFitColor(), farpointFilter1.GetSelectedNotFitColor(), _filterColunmIdxs);
            }
        }

        /// <summary>
        /// 清空过滤条件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="arg"></param>
        private void farpointFilter1_OnClickClearFilterBtn(object sender, EventArgs arg)
        {
            //清空过滤条件
            this.fpPitshaftInfo.ActiveSheet.RowFilter.ResetFilter();
        }

        /// <summary>
        /// 根据新的颜色值设置自动隐藏过滤条件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="arg"></param>
        private void farpointFilter1_OnClickFitColorBtnOK(object sender, EventArgs arg)
        {
            //根据新的颜色值设置自动隐藏过滤条件
            FarpointDefaultPropertiesSetter.SetFpCustomFilterProperties(this.fpPitshaftInfo, farpointFilter1.GetSelectedFitColor(), farpointFilter1.GetSelectedNotFitColor(), _filterColunmIdxs);
        }

        /// <summary>
        /// 根据新的颜色值设置自动隐藏过滤条件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="arg"></param>
        private void farpointFilter1_OnClickNotFitColorBtnOK(object sender, EventArgs arg)
        {
            //根据新的颜色值设置自动隐藏过滤条件
            FarpointDefaultPropertiesSetter.SetFpCustomFilterProperties(this.fpPitshaftInfo, farpointFilter1.GetSelectedFitColor(), farpointFilter1.GetSelectedNotFitColor(), _filterColunmIdxs);
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
        /// 导出
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tsBtnExport_Click(object sender, EventArgs e)
        {
            if (FileExport.fileExport(fpPitshaftInfo, true))
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
            FilePrint.CommonPrint(this.fpPitshaftInfo, 0);
        }

        /// <summary>
        /// 刷新
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnRefresh_Click(object sender, EventArgs e)
        {
            // 加载井筒信息
            loadPitshaftInfo();
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
            ILayer pLayer = GIS.Common.DataEditCommon.GetLayerByName(GIS.Common.DataEditCommon.g_pMap, GIS.LayerNames.DEFALUT_JINGTONG);
            if (pLayer == null)
            {
                MessageBox.Show("未发现井筒图层！");
                return;
            }
            IFeatureLayer pFeatureLayer = (IFeatureLayer)pLayer;
            string str = "";
            for (int i = 0; i < iSelIdxsArr.Length; i++)
            {
                bid = this.fpPitshaftInfo.Sheets[0].Cells[iSelIdxsArr[i], _BIDIndex].Text.Trim();
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
