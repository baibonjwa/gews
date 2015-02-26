// ******************************************************************
// 概  述：瓦斯压力点数据管理
// 作  者：伍鑫
// 创建日期：2013/12/09
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
using System.Collections;
using LibBusiness;
using LibCommon;
using LibCommonControl;
using GIS;
using ESRI.ArcGIS.Controls;
using LibEntity;
using ESRI.ArcGIS.Carto;
using GIS.Common;
using ESRI.ArcGIS.Geometry;
using LibCommonForm;

namespace _4.OutburstPrevention
{
    public partial class GasPressureInfoManagement : Form
    {
        /** 明细部开始index位置 **/
        private const int _iRowDetailStartIndex = 4;
        /** 保存所有用户选中的行的索引 **/
        private Hashtable _htSelIdxs = new Hashtable();
        /** 检索件数**/
        private int _iRowCount = 0;
        /** 主键index **/
        private int _primaryKeyIndex = 13;
        /** 绑定ID index **/
        private int _BIDIndex = 14;
        /** 需要过滤的列索引 **/
        private int[] _filterColunmIdxs = null;

        /// <summary>
        /// 构造方法
        /// </summary>
        public GasPressureInfoManagement()
        {
            InitializeComponent();

            // 设置窗体默认属性
            LibCommon.FormDefaultPropertiesSetter.SetManagementFormDefaultProperties(this, Const_OP.MANAGE_GASPRESSURE_INFO);

            // 设置Farpoint默认属性
            LibCommon.FarpointDefaultPropertiesSetter.SetFpDefaultProperties(this.fpGasPressureInfo, Const_OP.MANAGE_GASPRESSURE_INFO, _iRowDetailStartIndex);

            // 调用委托方法 （必须实装）
            dataPager1.FrmChild_EventHandler += FrmParent_EventHandler;

            // 加载瓦斯压力数据
            loadGasPressureInfo();

            #region Farpoint自动过滤功能
            //初始化需要过滤功能的列
            _filterColunmIdxs = new int[]
            {
                7,
                8,
                9,
                10,
                11,
                12,
            };
            //禁用选择颜色相关控件
            farpointFilter1.EnableChooseColorCtrls(false);
            //设置自动隐藏过滤条件
            FarpointDefaultPropertiesSetter.SetFpFilterHideProperties(this.fpGasPressureInfo, _filterColunmIdxs);
            #endregion
        }

        /// <summary>
        /// 调用委托方法 （必须实装）
        /// </summary>
        /// <param name="sender"></param>
        private void FrmParent_EventHandler(object sender)
        {
            // 分页控件与Farpoint过滤绑定问题
            FarpointFilterSetter.ClearFpFilter(this.fpGasPressureInfo);
            // 加载瓦斯压力数据
            loadGasPressureInfo();
        }

        /// <summary>
        /// 加载瓦斯压力数据
        /// </summary>
        private void loadGasPressureInfo()
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
            if (this.fpGasPressureInfo.Sheets[0].Rows.Count != _iRowDetailStartIndex)
            {
                this.fpGasPressureInfo.Sheets[0].Rows.Remove(_iRowDetailStartIndex, _iRowCount);
            }
            else
            {
                _iRowCount = 0;
            }

            // 获取全部数据件数（必须实装）
            int iRecordCount = GasPressureBLL.selectAllGasPressureInfo().Tables[0].Rows.Count;

            // 调用分页控件初始化方法（必须实装）
            dataPager1.PageControlInit(iRecordCount);

            // 获取要检索数据的开始位置和结束位置 （必须实装）
            int iStartIndex = dataPager1.getStartIndex();
            int iEndIndex = dataPager1.getEndIndex();

            // 获取开始位置和结束位置之间的数据（必须实装）
            // 说明：如果画面当前显示的件数是10，那么init时开始位置为1，结束位置为10，点击下一页后，开始位置变为11，结束位置变为20
            DataSet ds = GasPressureBLL.selectGasPressureInfoForPage(iStartIndex, iEndIndex);

            // 当前检索件数（必须实装）
            int iSelCnt = ds.Tables[0].Rows.Count;

            // 重新设定farpoint显示行数 （必须实装）
            this.fpGasPressureInfo.Sheets[0].Rows.Count = _iRowDetailStartIndex + iSelCnt;

            FarPoint.Win.Spread.Cells cells = this.fpGasPressureInfo.Sheets[0].Cells;

            // 检索件数 > 0 的场合
            if (iSelCnt > 0)
            {
                // 循环结果集
                for (int i = 0; i < iSelCnt; i++)
                {
                    // 当前检索件数（必须实装）
                    this._iRowCount = iSelCnt;

                    int index = 0;
                    // 选择
                    FarPoint.Win.Spread.CellType.CheckBoxCellType objCheckCell = new FarPoint.Win.Spread.CellType.CheckBoxCellType();
                    cells[_iRowDetailStartIndex + i, index].CellType = objCheckCell;
                    // 坐标X
                    cells[_iRowDetailStartIndex + i, ++index].Text = ds.Tables[0].Rows[i][GasPressureDbConstNames.X].ToString();
                    // 坐标Y
                    cells[_iRowDetailStartIndex + i, ++index].Text = ds.Tables[0].Rows[i][GasPressureDbConstNames.Y].ToString();
                    // 测点标高
                    cells[_iRowDetailStartIndex + i, ++index].Text = ds.Tables[0].Rows[i][GasPressureDbConstNames.Z].ToString();
                    // 埋深
                    cells[_iRowDetailStartIndex + i, ++index].Text = ds.Tables[0].Rows[i][GasPressureDbConstNames.DEPTH].ToString();
                    // 瓦斯压力值
                    cells[_iRowDetailStartIndex + i, ++index].Text = ds.Tables[0].Rows[i][GasPressureDbConstNames.GAS_PRESSURE_VALUE].ToString();
                    // 测定时间
                    cells[_iRowDetailStartIndex + i, ++index].Text = ds.Tables[0].Rows[i][GasPressureDbConstNames.MEASURE_DATE_TIME].ToString();

                    // 巷道信息
                    // 矿井名称
                    cells[_iRowDetailStartIndex + i, ++index].Text = "";
                    // 水平
                    cells[_iRowDetailStartIndex + i, ++index].Text = "";
                    // 采区
                    cells[_iRowDetailStartIndex + i, ++index].Text = "";
                    // 工作面
                    cells[_iRowDetailStartIndex + i, ++index].Text = "";
                    // 巷道名称
                    cells[_iRowDetailStartIndex + i, ++index].Text = "";

                    int iTunnelID = 0;
                    if (int.TryParse(ds.Tables[0].Rows[i][GasPressureDbConstNames.TUNNEL_ID].ToString(), out iTunnelID))
                    {
                        Tunnel tunnelEntity = Tunnel.Find(iTunnelID);//TunnelInfoBLL.selectTunnelInfoByTunnelID(iTunnelID);
                        if (tunnelEntity != null)
                        {
                            // 矿井名称
                            cells[_iRowDetailStartIndex + i, index - 4].Text = tunnelEntity.WorkingFace.MiningArea.Horizontal.Mine.MineName;
                            // 水平
                            cells[_iRowDetailStartIndex + i, index - 3].Text = tunnelEntity.WorkingFace.MiningArea.Horizontal.HorizontalName;
                            // 采区
                            cells[_iRowDetailStartIndex + i, index - 2].Text = tunnelEntity.WorkingFace.MiningArea.MiningAreaName;
                            // 工作面
                            cells[_iRowDetailStartIndex + i, index - 1].Text = tunnelEntity.WorkingFace.WorkingFaceName;
                            // 巷道名称
                            cells[_iRowDetailStartIndex + i, index].Text = tunnelEntity.TunnelName;
                        }
                    }

                    // 煤层信息
                    cells[_iRowDetailStartIndex + i, ++index].Text = "";
                    int iCoalSeamsId = 0;
                    if (int.TryParse(ds.Tables[0].Rows[i][GasPressureDbConstNames.COAL_SEAMS_ID].ToString(), out iCoalSeamsId))
                    {
                        cells[_iRowDetailStartIndex + i, index].Text = CoalSeams.Find(iCoalSeamsId).CoalSeamsName;
                    }

                    // 主键（隐藏列）
                    cells[_iRowDetailStartIndex + i, ++index].Text = ds.Tables[0].Rows[i][GasPressureDbConstNames.ID].ToString();
                    this.fpGasPressureInfo.Sheets[0].Columns[index].Visible = false;
                    // 绑定ID（隐藏列）
                    cells[_iRowDetailStartIndex + i, ++index].Text = ds.Tables[0].Rows[i][GasContentDbConstNames.BID].ToString();
                    this.fpGasPressureInfo.Sheets[0].Columns[index].Visible = false;
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
            GasPressureInfoEntering gasPressureInfoEnteringForm = new GasPressureInfoEntering();
            if (DialogResult.OK == gasPressureInfoEnteringForm.ShowDialog())
            {
                // 加载瓦斯压力数据
                loadGasPressureInfo();
                // 跳转到尾页（必须实装）
                this.dataPager1.btnLastPage_Click(sender, e);

                // 设置farpoint焦点（必须实装）
                this.fpGasPressureInfo.Sheets[0].SetActiveCell(this.fpGasPressureInfo.Sheets[0].Rows.Count, 0);
            }
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
            string strPrimaryKey = this.fpGasPressureInfo.Sheets[0].Cells[iSelIdxsArr[0], _primaryKeyIndex].Text;

            GasPressureInfoEntering gasPressureInfoEnteringForm = new GasPressureInfoEntering(strPrimaryKey);
            if (DialogResult.OK == gasPressureInfoEnteringForm.ShowDialog())
            {
                // 加载瓦斯压力数据
                loadGasPressureInfo();

                // 设置farpoint焦点（必须实装）
                this.fpGasPressureInfo.Sheets[0].SetActiveCell(iSelIdxsArr[0], 0);
            }
        }

        /// <summary>
        /// 删除按钮（必须实装）
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (Alert.confirm(Const_OP.DEL_CONFIRM_MSG_GASPRESSURE))
            {
                // 获取已选择明细行的索引
                int[] iSelIdxsArr = GetSelIdxs();

                // 存放主键的数组
                int[] iPkIdxsArr = new int[iSelIdxsArr.Length];
                string[] bidArr = new string[iSelIdxsArr.Length];
                for (int i = 0; i < iSelIdxsArr.Length; i++)
                {
                    // 获取主键
                    string iPk = this.fpGasPressureInfo.Sheets[0].Cells[iSelIdxsArr[i], _primaryKeyIndex].Text;
                    iPkIdxsArr[i] = Convert.ToInt32(iPk);
                    bidArr[i] = this.fpGasPressureInfo.Sheets[0].Cells[iSelIdxsArr[i], _BIDIndex].Text;
                }

                // 瓦斯压力数据删除
                bool bResult = GasPressureBLL.deleteGasPressureInfo(iPkIdxsArr);

                // 删除成功的场合
                if (bResult)
                {
                    DelGasGushQuantityPt(bidArr);
                    // 加载瓦斯压力数据
                    loadGasPressureInfo();

                    // 设置farpoint焦点（必须实装）
                    this.fpGasPressureInfo.Sheets[0].SetActiveCell(iSelIdxsArr[0], 0);
                }
            }
        }
        /// <summary>
        /// 删除瓦斯信息
        /// </summary>
        /// <param name="bid">绑定ID</param>
        /// <param name="mc">煤层</param>
        private void DelGasGushQuantityPt(string[] bid)
        {
            ILayer pLayer = DataEditCommon.GetLayerByName(DataEditCommon.g_pMap, GIS.LayerNames.LAYER_ALIAS_MR_WSYLD);
            IFeatureLayer pFeatureLayer = (IFeatureLayer)pLayer;
            string strsql = "";
            for (int i = 0; i < bid.Length; i++)
            {
                if (i == 0)
                    strsql = "bid='" + bid[i] + "'";
                else
                    strsql += " or bid='" + bid[i] + "' ";
            }
            DataEditCommon.DeleteFeatureByWhereClause(pFeatureLayer, strsql);
        }
        /// <summary>
        /// 获取farpoint中选中的所有行（必须实装）
        /// </summary>
        /// <returns>注意，返回值可能是null，null则代表一个也没选中</returns>
        private int[] GetSelIdxs()
        {
            if (_htSelIdxs.Count == 0)
            {
                return null;
            }
            int[] retArr = new int[_htSelIdxs.Count];
            _htSelIdxs.Keys.CopyTo(retArr, 0);
            return retArr;
        }

        /// <summary>
        /// farpoint的ButtonClicked事件（必须实装）
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void fpGasPressureInfo_ButtonClicked(object sender, FarPoint.Win.Spread.EditorNotifyEventArgs e)
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
        /// 全选/全不选checkbox的click事件
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
                    this.fpGasPressureInfo.Sheets[0].Cells[_iRowDetailStartIndex + i, 0].Value = ((CheckBox)sender).Checked;
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
                    this.fpGasPressureInfo.Sheets[0].Cells[_iRowDetailStartIndex + i, 0].Value = ((CheckBox)sender).Checked;
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
                FarpointDefaultPropertiesSetter.SetFpFilterHideProperties(this.fpGasPressureInfo, _filterColunmIdxs);

            }
            //未选中时，根据用户自定义的颜色进行分类显示
            else
            {
                //启用选择颜色相关控件
                farpointFilter1.EnableChooseColorCtrls(true);
                //设置自定义过滤条件
                FarpointDefaultPropertiesSetter.SetFpCustomFilterProperties(this.fpGasPressureInfo, farpointFilter1.GetSelectedFitColor(), farpointFilter1.GetSelectedNotFitColor(), _filterColunmIdxs);
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
            this.fpGasPressureInfo.ActiveSheet.RowFilter.ResetFilter();
        }

        /// <summary>
        /// 根据新的颜色值设置自动隐藏过滤条件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="arg"></param>
        private void farpointFilter1_OnClickFitColorBtnOK(object sender, EventArgs arg)
        {
            //根据新的颜色值设置自动隐藏过滤条件
            FarpointDefaultPropertiesSetter.SetFpCustomFilterProperties(this.fpGasPressureInfo, farpointFilter1.GetSelectedFitColor(), farpointFilter1.GetSelectedNotFitColor(), _filterColunmIdxs);
        }

        /// <summary>
        /// 根据新的颜色值设置自动隐藏过滤条件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="arg"></param>
        private void farpointFilter1_OnClickNotFitColorBtnOK(object sender, EventArgs arg)
        {
            //根据新的颜色值设置自动隐藏过滤条件
            FarpointDefaultPropertiesSetter.SetFpCustomFilterProperties(this.fpGasPressureInfo, farpointFilter1.GetSelectedFitColor(), farpointFilter1.GetSelectedNotFitColor(), _filterColunmIdxs);
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
        /// 打印
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tsBtnPrint_Click(object sender, EventArgs e)
        {
            FilePrint.CommonPrint(this.fpGasPressureInfo, 0);
        }

        /// <summary>
        /// 导出
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tsBtnExport_Click(object sender, EventArgs e)
        {
            if (FileExport.fileExport(fpGasPressureInfo, true))
            {
                Alert.alert(Const.EXPORT_SUCCESS_MSG);
            }
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
        /// 刷新
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnRefresh_Click(object sender, EventArgs e)
        {
            // 加载瓦斯压力数据
            loadGasPressureInfo();
        }

        private void btnMap_Click(object sender, EventArgs e)
        {
            // 获取已选择明细行的索引
            int[] iSelIdxsArr = GetSelIdxs();
            if (iSelIdxsArr == null)
            {
                MessageBox.Show("未选中数据行！");
                return;
            }
            IPoint pt;
            List<IPoint> list = new List<IPoint>();
            for (int i = 0; i < iSelIdxsArr.Length; i++)
            {
                try
                {
                    pt = new PointClass();
                    pt.X = Convert.ToDouble(this.fpGasPressureInfo.Sheets[0].Cells[iSelIdxsArr[i], 1].Text);
                    pt.Y = Convert.ToDouble(this.fpGasPressureInfo.Sheets[0].Cells[iSelIdxsArr[i], 2].Text);
                    list.Add(pt);
                }
                catch { }
            }
            GIS.MyMapHelp.Jump(GIS.MyMapHelp.GetGeoFromPoint(list));
        }
    }
}
