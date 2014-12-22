// ******************************************************************
// 概  述：陷落柱数据管理
// 作  者：宋英杰
// 创建日期：2014/3/11
// 版本号：V1.0
// 版本信息：
// V1.0 新建
// ******************************************************************
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using LibEntity;
using LibBusiness;
using LibCommon;
using LibCommonControl;
using GIS.Common;
using ESRI.ArcGIS.Carto;

namespace _3.GeologyMeasure
{
    public partial class CollapsePillarsManagement : MainFrm
    {
        #region ******变量声明******
        /**分页用**/
        private int _iRecordCount = 0;
        /**数据行数**/
        int _rowsCount = 0;
        /**Farpoint行数**/
        int _farpointRowsCount = 0;
        /**选择行数**/
        int _checkCount = 0;
        /**表头冻结行数**/
        int _rowDetailStartIndex = 4;
        /**修改行号（修改时重新设置焦点用）**/
        int _tmpRowIndex = 0;
        /**陷落柱实体**/
        CollapsePillarsEnt collapsePillarsEnt = new CollapsePillarsEnt();
        /**接陷落柱数据**/
        DataSet _ds = new DataSet();
        /**接关键点数据**/
        DataSet _dsCollapsePillarsPoint = new DataSet();
        /**关键点主键**/
        int[] _collapsePillarsPointPrimaryKey;
        /** 保存所有用户选中的行的索引 **/
        private Hashtable _htSelIdxs = new Hashtable();
        /** BIDindex **/
        private int _BIDIndex = 6;
        #endregion ******变量声明******

        /// <summary>
        /// 构造方法
        /// </summary>
        public CollapsePillarsManagement()
        {
            InitializeComponent();

            _htSelIdxs.Clear();

            //调用委托
            dataPager1.FrmChild_EventHandler += FrmParent_EventHandler;

            //设置窗体属性
            LibCommon.FormDefaultPropertiesSetter.SetManagementFormDefaultProperties(this, Const_GM.COLLAPSEPILLARE_MANAGEMENT);

            //设置Farpoint属性
            LibCommon.FarpointDefaultPropertiesSetter.SetFpDefaultProperties(fpCollapsePillarsInfo, LibCommon.Const_GM.COLLAPSEPILLARE_FARPOINT_TITLE, _rowDetailStartIndex);

        }

        /// <summary>
        /// 初始化
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CollapsePillarsManagement_Load(object sender, EventArgs e)
        {
            //绑定数据
            this.bindFpCollapsePillars();
        }

        /// <summary>
        /// 委托事件
        /// </summary>
        /// <param name="sender"></param>
        private void FrmParent_EventHandler(object sender)
        {
            _htSelIdxs.Clear();

            //绑定数据
            bindFpCollapsePillars();
        }

        /// <summary>
        /// 绑定Farpoint数据
        /// </summary>
        private void bindFpCollapsePillars()
        {
            //清空Farpoint
            FarPointOperate.farPointClear(fpCollapsePillarsInfo, _rowDetailStartIndex, _collapsePillarsPointPrimaryKey == null ? 0 : _collapsePillarsPointPrimaryKey.Length);

            //选择记数清空
            _checkCount = 0;

            //全选取消
            chkSelAll.Checked = false;

            // ※分页必须
            _iRecordCount = CollapsePillarsBLL.selectCollapsePillars().Tables[0].Rows.Count;

            // ※分页必须
            dataPager1.PageControlInit(_iRecordCount);

            int iStartIndex = dataPager1.getStartIndex();
            int iEndIndex = dataPager1.getEndIndex();

            //分页
            _ds = CollapsePillarsBLL.selectCollapsePillars(iStartIndex, iEndIndex);
            _rowsCount = _ds.Tables[0].Rows.Count;
            FarPointOperate.farPointReAdd(fpCollapsePillarsInfo, _rowDetailStartIndex, _rowsCount);

            if (_rowsCount > 0)
            {
                FarPoint.Win.Spread.CellType.CheckBoxCellType ckbxcell = new FarPoint.Win.Spread.CellType.CheckBoxCellType();
                ckbxcell.ThreeState = false;
                int addSpanRowIndex = _rowDetailStartIndex;
                int addSpanRowCounts = 0;
                for (int i = 0; i < _ds.Tables[0].Rows.Count; i++)
                {
                    //记录合并单元格的行编号
                    addSpanRowIndex = addSpanRowIndex + addSpanRowCounts;
                    //合并单元格的行数
                    addSpanRowCounts = 0;
                    if (!Validator.IsEmptyOrBlank(_ds.Tables[0].Rows[i][CollapsePillarsInfoDbConstNames.ID].ToString()))
                    {
                        _dsCollapsePillarsPoint = CollapsePillarsBLL.selectCollapsePillarsPoint(Convert.ToInt32(_ds.Tables[0].Rows[i][CollapsePillarsInfoDbConstNames.ID].ToString()));
                    }
                    for (int j = 0; j < _dsCollapsePillarsPoint.Tables[0].Rows.Count; j++)
                    {
                        //控制Farpoint行数
                        fpCollapsePillarsInfo.ActiveSheet.RowCount++;
                        int index = 0;
                        this.fpCollapsePillarsInfo.Sheets[0].Cells[addSpanRowIndex + j, index].CellType = ckbxcell;

                        //陷落柱名称
                        this.fpCollapsePillarsInfo.Sheets[0].Cells[addSpanRowIndex + j, ++index].Text = Convert.ToString(_ds.Tables[0].Rows[i][CollapsePillarsInfoDbConstNames.COLLAPSE_PILLARS]);
                        //关键点坐标X
                        this.fpCollapsePillarsInfo.Sheets[0].Cells[addSpanRowIndex + j, ++index].Text = Convert.ToString(_dsCollapsePillarsPoint.Tables[0].Rows[j][CollapsePillarsPointDbConstNames.COORDINATE_X]);
                        //关键点坐标Y
                        this.fpCollapsePillarsInfo.Sheets[0].Cells[addSpanRowIndex + j, ++index].Text = Convert.ToString(_dsCollapsePillarsPoint.Tables[0].Rows[j][CollapsePillarsPointDbConstNames.COORDINATE_Y]);
                        //关键点坐标Z
                        this.fpCollapsePillarsInfo.Sheets[0].Cells[addSpanRowIndex + j, ++index].Text = Convert.ToString(_dsCollapsePillarsPoint.Tables[0].Rows[j][CollapsePillarsPointDbConstNames.COORDINATE_Z]);
                        //描述
                        this.fpCollapsePillarsInfo.Sheets[0].Cells[addSpanRowIndex + j, ++index].Text = Convert.ToString(_ds.Tables[0].Rows[i][CollapsePillarsInfoDbConstNames.DISCRIBE]);

                        // ID
                        this.fpCollapsePillarsInfo.Sheets[0].Cells[addSpanRowIndex + j, ++index].Text = Convert.ToString(_ds.Tables[0].Rows[i][CollapsePillarsInfoDbConstNames.ID]);
                        this.fpCollapsePillarsInfo.Sheets[0].Columns[index].Visible = false;

                        //记录合并单元格的行数
                        addSpanRowCounts++;
                    }
                    //控制Farpoint行数
                    this.fpCollapsePillarsInfo.ActiveSheet.RowCount--;
                    if (addSpanRowCounts > 0)
                    {
                        //合并单元格
                        this.fpCollapsePillarsInfo.ActiveSheet.AddSpanCell(addSpanRowIndex, 0, addSpanRowCounts, 1);
                        this.fpCollapsePillarsInfo.ActiveSheet.AddSpanCell(addSpanRowIndex, 1, addSpanRowCounts, 1);
                        this.fpCollapsePillarsInfo.ActiveSheet.AddSpanCell(addSpanRowIndex, 5, addSpanRowCounts, 5);
                    }

                }
                //记录关键点主键

                _collapsePillarsPointPrimaryKey = new int[this.fpCollapsePillarsInfo.ActiveSheet.RowCount - _rowDetailStartIndex];
                int pointIndex = 0;
                for (int i = 0; i < _rowsCount; i++)
                {
                    _dsCollapsePillarsPoint = CollapsePillarsBLL.selectCollapsePillarsPoint(Convert.ToInt32(_ds.Tables[0].Rows[i][CollapsePillarsInfoDbConstNames.ID].ToString()));
                    for (int j = 0; j < _dsCollapsePillarsPoint.Tables[0].Rows.Count; j++)
                    {
                        _collapsePillarsPointPrimaryKey[pointIndex++] = Convert.ToInt32(_dsCollapsePillarsPoint.Tables[0].Rows[j][CollapsePillarsInfoDbConstNames.ID]);
                    }
                }
            }
            if (_rowsCount == 0)
            {
                _collapsePillarsPointPrimaryKey = null;
            }
            setButtenEnable();
        }

        /// <summary>
        /// farpoint对全选反选的影响
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void fpCollapsePillarsInfo_ButtonClicked(object sender, FarPoint.Win.Spread.EditorNotifyEventArgs e)
        {
            if (e.EditingControl is FarPoint.Win.FpCheckBox)
            {
                FarPoint.Win.FpCheckBox fpChk = (FarPoint.Win.FpCheckBox)e.EditingControl;
                if (fpChk.Checked)
                {
                    // 保存索引号
                    if (!_htSelIdxs.Contains(e.Row))
                    {
                        _htSelIdxs.Add(e.Row, true);

                        // 点击每条记录知道全部选中的情况下，全选/全不选checkbox设为选中
                        if (_htSelIdxs.Count == _checkCount)
                        {
                            // 全选/全不选checkbox设为选中
                            this.chkSelAll.Checked = true;
                        }
                    }

                    _checkCount++;
                }
                else
                {
                    // 移除索引号
                    _htSelIdxs.Remove(e.Row);

                    _checkCount--;
                }
            }
            if (_checkCount == _rowsCount)
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
            if (_rowsCount > 0)
            {
                //遍历数据
                for (int i = _rowDetailStartIndex; i < fpCollapsePillarsInfo.ActiveSheet.Rows.Count; i++)
                {
                    if (fpCollapsePillarsInfo.Sheets[0].Cells[i, 1].Value != fpCollapsePillarsInfo.Sheets[0].Cells[i - 1, 1].Value)
                    {
                        fpCollapsePillarsInfo.Sheets[0].Cells[i, 0].Value = ((CheckBox)sender).Checked;
                        // 将存有选中项目的数组清空
                        _htSelIdxs.Remove(_rowDetailStartIndex + i);
                    }
                    //checkbox选中
                    if (chkSelAll.Checked)
                    {
                        _checkCount = _ds.Tables[0].Rows.Count;
                    }
                    //checkbox未选中
                    else
                    {
                        _checkCount = 0;
                    }
                }
            }
            else
            {
                // 循环明细
                for (int i = 0; i < fpCollapsePillarsInfo.ActiveSheet.Rows.Count; i++)
                {
                    // 将所有明细设为全选中
                    this.fpCollapsePillarsInfo.Sheets[0].Cells[i + i, 0].Value = ((CheckBox)sender).Checked;
                    // 将选中明细的索引添加到数组中，如果已经存在不要二次添加
                    if (!_htSelIdxs.Contains(i + i))
                    {
                        _htSelIdxs.Add(i + i, true);
                    }
                }
            }
            setButtenEnable();
        }

        /// <summary>
        /// 设置按钮可操作性
        /// </summary>
        private void setButtenEnable()
        {
            if (_checkCount == 1)
            {
                tsBtnModify.Enabled = true;
            }
            else
            {
                tsBtnModify.Enabled = false;
            }
            if (_checkCount > 0)
            {
                tsBtnDel.Enabled = true;
            }
            else
            {
                tsBtnDel.Enabled = false;
            }
        }

        /// <summary>
        /// 为CollapsePillarsEnt赋值
        /// </summary>
        private void setCollapsePillarsEntValue()
        {
            for (int i = 0; i < _collapsePillarsPointPrimaryKey.Length; i++)
            {
                if (fpCollapsePillarsInfo.Sheets[0].Cells[_rowDetailStartIndex + i, 0].Value != null && (bool)fpCollapsePillarsInfo.Sheets[0].Cells[_rowDetailStartIndex + i, 0].Value == true)
                {
                    _tmpRowIndex = _rowDetailStartIndex + i;
                    int index = 0;
                    //主键
                    collapsePillarsEnt.Id = Convert.ToInt32(CollapsePillarsBLL.selectCollapsePillarsPointByPointID(_collapsePillarsPointPrimaryKey[i]).Tables[0].Rows[0][CollapsePillarsPointDbConstNames.COLLAPSE_PILLARS_ID].ToString());
                    //陷落柱名称
                    collapsePillarsEnt.CollapsePillarsName = this.fpCollapsePillarsInfo.Sheets[0].Cells[_rowDetailStartIndex + i, ++index].Text;
                    //关键点坐标X
                    collapsePillarsEnt.CoordinateX = Convert.ToDouble(this.fpCollapsePillarsInfo.Sheets[0].Cells[_rowDetailStartIndex + i, ++index].Text);
                    //关键点坐标Y
                    collapsePillarsEnt.CoordinateY = Convert.ToDouble(this.fpCollapsePillarsInfo.Sheets[0].Cells[_rowDetailStartIndex + i, ++index].Text);
                    //关键点坐标Z
                    collapsePillarsEnt.CoordinateZ = Convert.ToDouble(this.fpCollapsePillarsInfo.Sheets[0].Cells[_rowDetailStartIndex + i, ++index].Text);
                    //描述
                    collapsePillarsEnt.Discribe = this.fpCollapsePillarsInfo.Sheets[0].Cells[_rowDetailStartIndex + i, ++index].Text;
                    //BID
                    collapsePillarsEnt.BindingID = CollapsePillarsBLL.selectCollapsePillarsPointByPointID(_collapsePillarsPointPrimaryKey[i]).Tables[0].Rows[0][CollapsePillarsPointDbConstNames.BINDINGID].ToString();
                }
            }
        }

        /// <summary>
        /// 添加按钮响应
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tsBtnAdd_Click(object sender, EventArgs e)
        {
            _htSelIdxs.Clear();
            GIS.CollapsePillarsEntering c = new GIS.CollapsePillarsEntering();
            if (DialogResult.OK == c.ShowDialog())
            {
                bindFpCollapsePillars();
                this.dataPager1.btnLastPage_Click(sender, e);
                FarPointOperate.farPointFocusSetAdd(fpCollapsePillarsInfo, _rowDetailStartIndex, _collapsePillarsPointPrimaryKey.Length);
            }
        }
        /// <summary>
        /// 修改按钮响应
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tsBtnModify_Click(object sender, EventArgs e)
        {
            setCollapsePillarsEntValue();
            GIS.CollapsePillarsEntering c = new GIS.CollapsePillarsEntering(collapsePillarsEnt);
            if (DialogResult.OK == c.ShowDialog())
            {
                _htSelIdxs.Clear();
                bindFpCollapsePillars();
                FarPointOperate.farPointFocusSetChange(fpCollapsePillarsInfo, _tmpRowIndex);
            }
        }
        /// <summary>
        /// 删除按钮响应
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tsBtnDel_Click(object sender, EventArgs e)
        {
            if (Alert.confirm(Const.DEL_CONFIRM_MSG))
            {
                bool bResult = false;
                //当前选定单元格行号
                _tmpRowIndex = fpCollapsePillarsInfo.Sheets[0].ActiveRowIndex;

                //20140511 lyf
                string sCollapseID = "";

                for (int i = 0; i < _collapsePillarsPointPrimaryKey.Length; i++)
                {
                    if (fpCollapsePillarsInfo.Sheets[0].Cells[_rowDetailStartIndex + i, 0].Value != null && (bool)fpCollapsePillarsInfo.Sheets[0].Cells[_rowDetailStartIndex + i, 0].Value == true)
                    {
                        //陷落柱ID
                        collapsePillarsEnt.Id = Convert.ToInt32(CollapsePillarsBLL.selectCollapsePillarsPointByPointID(_collapsePillarsPointPrimaryKey[i]).Tables[0].Rows[0][CollapsePillarsPointDbConstNames.COLLAPSE_PILLARS_ID].ToString());
                        //关键点ID
                        collapsePillarsEnt.PointId = _collapsePillarsPointPrimaryKey[i];
                        //删除
                        bResult = CollapsePillarsBLL.deleteCollapsePillars(collapsePillarsEnt);

                        //20140511 lyf
                        sCollapseID = collapsePillarsEnt.Id.ToString();
                        if (bResult && sCollapseID != "")
                        {
                            DeleteyXLZ(sCollapseID);
                        }
                    }
                }
                //绑定数据
                bindFpCollapsePillars();
                //删除后重设焦点
                FarPointOperate.farPointFocusSetDel(fpCollapsePillarsInfo, _tmpRowIndex);

                //20140511 lyf

                _htSelIdxs.Clear();
            }
        }

        #region 删除陷落柱图元

        /// <summary>
        /// 删除陷落柱图元
        /// </summary>
        /// <param name="sCollapseID"></param>
        private void DeleteyXLZ(string sCollapseID)
        {
            //1.获得当前编辑图层
            DrawSpecialCommon drawspecial = new DrawSpecialCommon();
            string sLayerAliasName = GIS.LayerNames.LAYER_ALIAS_MR_XianLuoZhu1;//“默认_陷落柱_1”图层
            IFeatureLayer featureLayer = drawspecial.GetFeatureLayerByName(sLayerAliasName);
            if (featureLayer == null)
            {
                MessageBox.Show("未找到" + sLayerAliasName + "图层,无法删除陷落柱图元。");
                return;
            }

            //2.删除原来图元，重新绘制新图元
            bool bIsDeleteOldFeature = DataEditCommon.DeleteFeatureByBId(featureLayer, sCollapseID);
        }

        #endregion

        /// <summary>
        /// 刷新按钮响应
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tsBtnRefresh_Click(object sender, EventArgs e)
        {
            _htSelIdxs.Clear();
            bindFpCollapsePillars();
        }

        /// <summary>
        /// 退出按钮响应
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tsBtnExit_Click(object sender, EventArgs e)
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
        /// 导出按钮事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tsBtnExport_Click(object sender, EventArgs e)
        {
            if (FileExport.fileExport(fpCollapsePillarsInfo, true))
            {
                Alert.alert(Const.EXPORT_SUCCESS_MSG);
            }
        }

        /// <summary>
        /// 打印按钮事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tsBtnPrint_Click(object sender, EventArgs e)
        {
            FilePrint.CommonPrint(fpCollapsePillarsInfo, 0);
        }

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
            ILayer pLayer = GIS.Common.DataEditCommon.GetLayerByName(GIS.Common.DataEditCommon.g_pMap, GIS.LayerNames.LAYER_ALIAS_MR_XianLuoZhu1);
            if (pLayer == null)
            {
                MessageBox.Show("未发现陷落柱图层！");
                return;
            }
            IFeatureLayer pFeatureLayer = (IFeatureLayer)pLayer;
            string str = "";
            for (int i = 0; i < iSelIdxsArr.Length; i++)
            {
                bid = this.fpCollapsePillarsInfo.Sheets[0].Cells[iSelIdxsArr[i], _BIDIndex].Text.Trim();
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
