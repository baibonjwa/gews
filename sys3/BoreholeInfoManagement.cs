// ******************************************************************
// 概  述：勘探钻孔数据管理界面
// 作  者：伍鑫
// 创建日期：2013/12/05
// 版本号：V1.0
// 版本信息：
// V1.0 新建
// ******************************************************************

using System;
using System.Collections;
using System.Collections.Generic;
using System.Windows.Forms;
using ESRI.ArcGIS.Carto;
using GIS.Common;
using LibBusiness;
using LibCommon;
using _3.GeologyMeasure;

namespace sys3
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
            LibCommon.FarpointDefaultPropertiesSetter.SetFpDefaultProperties(this.fpBoreholeInfo,
                Const_GM.MANAGE_BOREHOLE_INFO, _iRowDetailStartIndex);
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
            BoreholeInfoEntering boreholeInfoEnteringForm = new BoreholeInfoEntering(iboreholeId, this);
            boreholeInfoEnteringForm.Show(this);

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

                        }
                    }
                }
                else
                {
                    // 移除索引号
                    _htSelIdxs.Remove(e.Row);
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
