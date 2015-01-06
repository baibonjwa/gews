// ******************************************************************
// 概  述：停采线数据管理
// 作  者：宋英杰
// 日  期：2014/3/11
// 版本号：V1.0
// 版本信息：
// V1.0 新建
// ******************************************************************

using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using ESRI.ArcGIS.Carto;
using LibBusiness;
using LibCommon;
using LibEntity;

namespace sys2
{
    public partial class StopLineManagement : XtraForm
    {
        #region ******变量声明******
        /**分页用数据行数**/
        private int _iRecordCount = 0;
        /**当前页数据行数**/
        int _rowsCount = 0;
        /**选择行数**/
        int _checkCount = 0;
        /**表头冻结行数**/
        int _rowDetailStartIndex = 3;
        /**修改行号（修改时重新设置焦点用）**/
        int _tmpRowIndex = 0;
        /**停采线实体**/
        StopLine stopLineEntity = new StopLine();
        /**接分页查询数据**/
        DataSet _ds = new DataSet();
        //BID列序号
        int _bidindex = 8;
        /** 保存所有用户选中的行的索引 **/
        private Hashtable _htSelIdxs = new Hashtable();
        #endregion

        #region ******初始化******
        /// <summary>
        /// 构造方法
        /// </summary>
        public StopLineManagement()
        {
            InitializeComponent();

            //分页初始化
            dataPager1.FrmChild_EventHandler += FrmParent_EventHandler;

            //窗体属性设置
            LibCommon.FormDefaultPropertiesSetter.SetManagementFormDefaultProperties(this, Const_MS.STOP_LINE_MANAGEMENT);

            //Farpoint属性设置
            LibCommon.FarpointDefaultPropertiesSetter.SetFpDefaultProperties(fpStopLineInfo, LibCommon.Const_MS.STOP_LINE_FARPOINT_TITLE, _rowDetailStartIndex);
        }

        /// <summary>
        /// 初始化
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TunnelInfoManagement_Load(object sender, EventArgs e)
        {
            //绑定数据
            this.bindFpStopLineInfo();
        }

        /// <summary>
        /// 调用委托方法
        /// </summary>
        /// <param name="sender"></param>
        private void FrmParent_EventHandler(object sender)
        {
            //绑定数据
            bindFpStopLineInfo();
        }
        #endregion

        #region ******farpoint操作******
        /// <summary>
        /// farpoint数据绑定
        /// </summary>
        private void bindFpStopLineInfo()
        {
            //farpoint清空（必须）
            FarPointOperate.farPointClear(fpStopLineInfo, _rowDetailStartIndex, _rowsCount);

            // 清空HashTabl（必须实装）
            _htSelIdxs.Clear();

            //清空选择记数（必须)
            _checkCount = 0;

            //全选checkbox取消选择（必须）
            chkSelAll.Checked = false;

            // ※分页必须
            _iRecordCount = StopLineBLL.selectStopLineInfo().Tables[0].Rows.Count;

            // ※分页必须
            dataPager1.PageControlInit(_iRecordCount);

            int iStartIndex = dataPager1.getStartIndex();
            int iEndIndex = dataPager1.getEndIndex();

            //分布查询数据
            _ds = StopLineBLL.selectStopLineInfo(iStartIndex, iEndIndex);

            //数据行数
            _rowsCount = _ds.Tables[0].Rows.Count;

            //farpoint重新绘制（必须）
            FarPointOperate.farPointReAdd(fpStopLineInfo, _rowDetailStartIndex, _rowsCount);

            //查询到数据时
            if (_rowsCount > 0)
            {
                FarPoint.Win.Spread.CellType.CheckBoxCellType ckbxcell = new FarPoint.Win.Spread.CellType.CheckBoxCellType();
                //取消checkbox三选
                ckbxcell.ThreeState = false;

                //绑定farpoint数据
                for (int i = 0; i < _ds.Tables[0].Rows.Count; i++)
                {
                    int index = 0;

                    //选择
                    this.fpStopLineInfo.Sheets[0].Cells[_rowDetailStartIndex + i, index].CellType = ckbxcell;

                    //停采线名称
                    this.fpStopLineInfo.Sheets[0].Cells[_rowDetailStartIndex + i, ++index].Text = _ds.Tables[0].Rows[i][StopLineDbConstNames.STOP_LINE_NAME].ToString();
                    //起点坐标X
                    this.fpStopLineInfo.Sheets[0].Cells[_rowDetailStartIndex + i, ++index].Text = _ds.Tables[0].Rows[i][StopLineDbConstNames.S_COORDINATE_X].ToString();
                    //起点坐标Y
                    this.fpStopLineInfo.Sheets[0].Cells[_rowDetailStartIndex + i, ++index].Text = _ds.Tables[0].Rows[i][StopLineDbConstNames.S_COORDINATE_Y].ToString();
                    //起点坐标Z
                    this.fpStopLineInfo.Sheets[0].Cells[_rowDetailStartIndex + i, ++index].Text = _ds.Tables[0].Rows[i][StopLineDbConstNames.S_COORDINATE_Z].ToString();
                    //终点坐标X
                    this.fpStopLineInfo.Sheets[0].Cells[_rowDetailStartIndex + i, ++index].Text = _ds.Tables[0].Rows[i][StopLineDbConstNames.F_COORDINATE_X].ToString();
                    //终点坐标Y
                    this.fpStopLineInfo.Sheets[0].Cells[_rowDetailStartIndex + i, ++index].Text = _ds.Tables[0].Rows[i][StopLineDbConstNames.F_COORDINATE_Y].ToString();
                    //终点坐标Z
                    this.fpStopLineInfo.Sheets[0].Cells[_rowDetailStartIndex + i, ++index].Text = _ds.Tables[0].Rows[i][StopLineDbConstNames.F_COORDINATE_Z].ToString();
                    //BID
                    this.fpStopLineInfo.Sheets[0].Cells[_rowDetailStartIndex + i, 8].Text = _ds.Tables[0].Rows[i][StopLineDbConstNames.BINDINGID].ToString();
                    this.fpStopLineInfo.Sheets[0].Cells[_rowDetailStartIndex + i, 8].Column.Visible = false;
                }
            }
            //设置按钮可操作性
            setButtenEnable();
        }

        /// <summary>
        /// farpoint中checkbox选中对全选反选的影响
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void fpStopLineInfo_ButtonClicked(object sender, FarPoint.Win.Spread.EditorNotifyEventArgs e)
        {
            if (e.EditingControl is FarPoint.Win.FpCheckBox)
            {
                FarPoint.Win.FpCheckBox fpChk = (FarPoint.Win.FpCheckBox)e.EditingControl;
                //记录选中行数
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

                    // 全选/全不选checkbox设为未选中
                    this.chkSelAll.Checked = false;

                    _checkCount--;
                }
            }
            //控制全选框的状态
            if (_checkCount == _rowsCount)
            {
                chkSelAll.Checked = true;
            }
            else
            {
                chkSelAll.Checked = false;
            }
            //设置按钮可操作性
            setButtenEnable();
        }
        #endregion

        #region ******实体赋值******
        /// <summary>
        /// 为变量StopLineEntity赋值
        /// </summary>
        private void setStopLineEntityValue()
        {
            for (int i = 0; i < _rowsCount; i++)
            {
                if (fpStopLineInfo.Sheets[0].Cells[_rowDetailStartIndex + i, 0].Value != null && (bool)fpStopLineInfo.Sheets[0].Cells[_rowDetailStartIndex + i, 0].Value == true)
                {
                    _tmpRowIndex = _rowDetailStartIndex + i;

                    int index = 0;
                    //主键
                    stopLineEntity.Id = (int)_ds.Tables[0].Rows[i][StopLineDbConstNames.ID];
                    //停采线名称
                    stopLineEntity.StopLineName = this.fpStopLineInfo.Sheets[0].Cells[_rowDetailStartIndex + i, ++index].Text;
                    //起点坐标X
                    stopLineEntity.SCoordinateX = Convert.ToDouble(this.fpStopLineInfo.Sheets[0].Cells[_rowDetailStartIndex + i, ++index].Text);
                    //起点坐标Y
                    stopLineEntity.SCoordinateY = Convert.ToDouble(this.fpStopLineInfo.Sheets[0].Cells[_rowDetailStartIndex + i, ++index].Text);
                    //起点坐标Z
                    stopLineEntity.SCoordinateZ = Convert.ToDouble(this.fpStopLineInfo.Sheets[0].Cells[_rowDetailStartIndex + i, ++index].Text);
                    //终点坐标X
                    stopLineEntity.FCoordinateX = Convert.ToDouble(this.fpStopLineInfo.Sheets[0].Cells[_rowDetailStartIndex + i, ++index].Text);
                    //终点坐标Y
                    stopLineEntity.FCoordinateY = Convert.ToDouble(this.fpStopLineInfo.Sheets[0].Cells[_rowDetailStartIndex + i, ++index].Text);
                    //终点坐标Z
                    stopLineEntity.FCoordinateZ = Convert.ToDouble(this.fpStopLineInfo.Sheets[0].Cells[_rowDetailStartIndex + i, ++index].Text);
                    stopLineEntity.BindingId = this.fpStopLineInfo.Sheets[0].Cells[_rowDetailStartIndex + i, 8].Text;
                }
            }
        }
        #endregion

        #region ******按钮点击事件******
        /// <summary>
        /// 添加按钮事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tsBtnAdd_Click(object sender, EventArgs e)
        {
            StopLineEntering d = new StopLineEntering(this);
            d.Show(this);
        }
        public void refreshAdd()
        { //绑定数据
            bindFpStopLineInfo();
            //跳转到最后一页
            this.dataPager1.btnLastPage_Click(null, null);
            //添加后重设farpoint焦点
            FarPointOperate.farPointFocusSetAdd(fpStopLineInfo, _rowDetailStartIndex, _rowsCount);
        }
        /// <summary>
        /// 修改按钮响应
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tsBtnModify_Click(object sender, EventArgs e)
        {
            //停采线实体赋值
            setStopLineEntityValue();
            StopLineEntering stopLineForm = new StopLineEntering(stopLineEntity, this);
            stopLineForm.Show(this);
        }
        public void refreshUpdate()
        { //绑定数据
            bindFpStopLineInfo();
            //添加后重设farpoint焦点
            FarPointOperate.farPointFocusSetAdd(fpStopLineInfo, _rowDetailStartIndex, _rowsCount);
        }
        /// <summary>
        /// 删除按钮响应
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tsBtnDel_Click(object sender, EventArgs e)
        {
            bool result = Alert.confirm(Const.DEL_CONFIRM_MSG);

            if (result == true)
            {
                bool bResult = false;

                IFeatureLayer featureLayer = GetStopLineFeatureLayer();

                //获取当前farpoint选中焦点
                _tmpRowIndex = fpStopLineInfo.Sheets[0].ActiveRowIndex;

                for (int i = 0; i < _rowsCount; i++)
                {
                    //选择为null时，该选择框没有被选择过,与未选中同样效果
                    if (fpStopLineInfo.Sheets[0].Cells[_rowDetailStartIndex + i, 0].Value != null)
                    {
                        //选择框被选择
                        if ((bool)fpStopLineInfo.Sheets[0].Cells[_rowDetailStartIndex + i, 0].Value == true)
                        {
                            //获取掘进ID
                            stopLineEntity.Id = (int)_ds.Tables[0].Rows[i][StopLineDbConstNames.ID];
                            //删除操作
                            stopLineEntity.Delete();

                            if (featureLayer != null)
                                GIS.SpecialGraphic.DrawStopLine.DeleteLineFeature(featureLayer, stopLineEntity.BindingId); //删除对应的停采线要素
                        }
                    }
                }
                if (bResult)
                {
                    //TODO:删除成功后操作
                }

                //绑定信息
                bindFpStopLineInfo();
                FarPointOperate.farPointFocusSetDel(fpStopLineInfo, _tmpRowIndex);
                /******************跟据杨小颖意见，删除操作后提示信息***************/
                ////删除成功
                //if (bResult)
                //{
                //    //删除后重设Farpoint焦点
                //    FarPointOperate.farPointFocusSetDel(fpStopLineInfo, _tmpRowIndex);
                //}
                ////删除失败
                //else
                //{
                //    Alert.alert(Const_MS.MSG_DELETE_FAILURE);
                //}
                /********************************************************************/
            }
            return;
        }

        /// <summary>
        /// 获取停采线图层
        /// </summary>
        /// <returns>矢量图层</returns>
        private IFeatureLayer GetStopLineFeatureLayer()
        {
            //找到图层
            IMap map = GIS.Common.DataEditCommon.g_pMap;
            string layerName = GIS.LayerNames.STOP_LINE; //“停采线”图层
            GIS.Common.DrawSpecialCommon drawSpecialCom = new GIS.Common.DrawSpecialCommon();
            IFeatureLayer featureLayer = drawSpecialCom.GetFeatureLayerByName(layerName);

            if (featureLayer == null)
            {
                //MessageBox.Show("没有找到" + layerName + "图层，将不能绘制停采线。", "提示", MessageBoxButtons.OK);
                return null;
            }

            return featureLayer;
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
        /// 退出按钮事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tsBtnExit_Click(object sender, EventArgs e)
        {
            //关闭窗体
            this.Close();
        }

        /// <summary>
        /// 刷新按钮事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tsBtnRefresh_Click(object sender, EventArgs e)
        {
            //绑定数据
            bindFpStopLineInfo();
        }

        /// <summary>
        /// 导出按钮事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tsBtnExport_Click(object sender, EventArgs e)
        {
            if (FileExport.fileExport(fpStopLineInfo, true))
            {
                Alert.alert(Const.EXPORT_SUCCESS_MSG);
            }
            return;
        }

        /// <summary>
        /// 打印按钮事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tsBtnPrint_Click(object sender, EventArgs e)
        {
            //打印
            FilePrint.CommonPrint(fpStopLineInfo, 0);
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
        #endregion

        #region ******设置按钮可操作性******
        /// <summary>
        /// 设置按钮可操作性
        /// </summary>
        private void setButtenEnable()
        {
            //修改只在选中一条时可用
            if (_checkCount == 1)
            {
                tsBtnModify.Enabled = true;
            }
            else
            {
                tsBtnModify.Enabled = false;
            }
            //删除在选中条数大于0时可用
            if (_checkCount > 0)
            {
                tsBtnDel.Enabled = true;
            }
            else
            {
                tsBtnDel.Enabled = false;
            }
        }
        #endregion

        #region ******选择框事件******
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
                for (int i = 0; i < _rowsCount; i++)
                {
                    //checkbox选中
                    if (chkSelAll.Checked)
                    {
                        if (!_htSelIdxs.Contains(_rowDetailStartIndex + i))
                        {
                            _htSelIdxs.Add(_rowDetailStartIndex + i, true);
                        }
                        fpStopLineInfo.Sheets[0].Cells[_rowDetailStartIndex + i, 0].Value = ((CheckBox)sender).Checked;
                        _checkCount = _ds.Tables[0].Rows.Count;
                    }
                    //checkbox未选中
                    else
                    {
                        _htSelIdxs.Remove(_rowDetailStartIndex + i);
                        fpStopLineInfo.Sheets[0].Cells[_rowDetailStartIndex + i, 0].Value = ((CheckBox)sender).Checked;
                        _checkCount = 0;
                    }
                }
            }
            //设置按钮可操作性
            setButtenEnable();
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
            ILayer pLayer = GIS.Common.DataEditCommon.GetLayerByName(GIS.Common.DataEditCommon.g_pMap, GIS.LayerNames.STOP_LINE);
            if (pLayer == null)
            {
                MessageBox.Show("未发现停采线图层！");
                return;
            }
            IFeatureLayer pFeatureLayer = (IFeatureLayer)pLayer;
            string str = "";
            for (int i = 0; i < iSelIdxsArr.Length; i++)
            {
                bid = this.fpStopLineInfo.Sheets[0].Cells[iSelIdxsArr[i], _bidindex].Text.Trim();
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
