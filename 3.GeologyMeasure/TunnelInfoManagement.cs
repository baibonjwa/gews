// ******************************************************************
// 概  述：巷道信息管理
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
using ESRI.ArcGIS.Carto;
using LibEntity;
using LibBusiness;
using LibCommon;
using Excel = Microsoft.Office.Interop.Excel;
using LibCommonControl;
using LibCommonForm;
using System.Data.SqlClient;

namespace _3.GeologyMeasure
{
    public partial class TunnelInfoManagement : BaseForm
    {
        //****************变量声明***************
        private int _iRecordCount = 0;
        int rowsCount = 0;      //数据行数
        int checkCount = 0;     //选择行数
        DataSet ds = new DataSet();
        int rowDetailStartIndex = 4;
        int activeRow = 0;
        //需要过滤的列索引
        private int[] _filterColunmIdxs = null;
        private bool bFirst = true;

        private int _BIDIndex = 16;
        /** 保存所有用户选中的行的索引 **/
        private Hashtable _htSelIdxs = new Hashtable();
        Tunnel tunnelEntity = new Tunnel();

        FarPoint.Win.Spread.Cells cells;
        //****************************************

        /// <summary>
        /// 构造方法
        /// </summary>
        public TunnelInfoManagement(MainFrm mainFrm)
        {
            this.MainForm = mainFrm;
            InitializeComponent();

            LibCommon.FormDefaultPropertiesSetter.SetManagementFormDefaultProperties(this, Const_GM.TUNNEL_INFO_MANAGEMENT);

            LibCommon.FarpointDefaultPropertiesSetter.SetFpDefaultProperties(fpTunnelInfo, LibCommon.Const_GM.TUNNEL_INFO_FARPOINT_TITLE, rowDetailStartIndex);

            dataPager1.FrmChild_EventHandler += FrmParent_EventHandler;

            _filterColunmIdxs = new int[]
            {
                1,
                2,
                3,
                4,
                5,
                8,
                9,
                10,
                11,
                12,
                13,
                14,
                15
            };
            //禁用选择颜色相关控件
            farpointFilter1.EnableChooseColorCtrls(false);
            //设置自动隐藏过滤条件
            FarpointDefaultPropertiesSetter.SetFpFilterHideProperties(this.fpTunnelInfo, _filterColunmIdxs);




        }

        /// <summary>
        /// 初始化
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TunnelInfoManagement_Load(object sender, EventArgs e)
        {
            btnNoWire.BackColor = Const.NO_WIRE_TUNNEL_COLOR;
            btnWired.BackColor = Const.WIRED_TUNNEL_COLOR;
            btnTunnelJJ.BackColor = Const.JJ_TUNNEL_COLOR;
            btnTunnelHC.BackColor = Const.HC_TUNNEL_COLOR;
            this.bindFpTunnelInfo();
        }

        /// <summary>
        /// 委托方法
        /// </summary>
        /// <param name="sender"></param>
        private void FrmParent_EventHandler(object sender)
        {
            if (!bFirst)
            {
                bindFpTunnelInfo();
            }
            else
            {
                bFirst = false;
            }
        }

        /// <summary>
        /// farpoint数据绑定
        /// </summary>
        private void bindFpTunnelInfo()
        {

            // 清空HashTabl（必须实装）
            _htSelIdxs.Clear();

            FarPointOperate.farPointClear(fpTunnelInfo, rowDetailStartIndex, rowsCount);
            checkCount = 0;
            chkSelAll.Checked = false;

            // ※分页必须
            _iRecordCount = TunnelInfoBLL.selectTunnelInfo().Tables[0].Rows.Count;

            // ※分页必须
            dataPager1.PageControlInit(_iRecordCount);
            int iStartIndex = dataPager1.getStartIndex();
            int iEndIndex = dataPager1.getEndIndex();

            // 获取巷道信息
            ds = TunnelInfoBLL.selectTunnelInfo(iStartIndex, iEndIndex);
            rowsCount = ds.Tables[0].Rows.Count;
            FarPointOperate.farPointReAdd(fpTunnelInfo, rowDetailStartIndex, rowsCount);

            int columnCount = fpTunnelInfo_Sheet1.ColumnCount - 9;

            List<Tunnel> tunnelList = BasicInfoManager.getInstance().getTunnelListByDataSet(ds);

            FarPoint.Win.Spread.CellType.CheckBoxCellType ckbxcell = new FarPoint.Win.Spread.CellType.CheckBoxCellType();

            // Get the spread sheet cells.
            cells = this.fpTunnelInfo.Sheets[0].Cells;
            fpTunnelInfo.Sheets[0].Columns[_BIDIndex].Visible = false;

            ckbxcell.ThreeState = false;
            int i = 0; // Processed row count.
            foreach (Tunnel entity in tunnelList)
            {
                int index = 0; // column index.

                cells[rowDetailStartIndex + i, index].CellType = ckbxcell;
                //矿井名称
                cells[rowDetailStartIndex + i, ++index].Text = entity.WorkingFace.MiningArea.Horizontal.Mine.MineName;
                //水平
                cells[rowDetailStartIndex + i, ++index].Text = entity.WorkingFace.MiningArea.Horizontal.HorizontalName;
                //采区
                cells[rowDetailStartIndex + i, ++index].Text = entity.WorkingFace.MiningArea.MiningAreaName;
                //工作面
                cells[rowDetailStartIndex + i, ++index].Text = entity.WorkingFace.WorkingFaceName;
                //巷道名称
                cells[rowDetailStartIndex + i, ++index].Text = entity.TunnelName;
                //设计长度
                cells[rowDetailStartIndex + i, ++index].Text = entity.TunnelDesignLength.ToString();
                //设计面积
                cells[rowDetailStartIndex + i, ++index].Text = entity.TunnelDesignArea.ToString();
                //支护方式
                cells[rowDetailStartIndex + i, ++index].Text = entity.TunnelSupportPattern;
                //围岩类型
                cells[rowDetailStartIndex + i, ++index].Text = BasicInfoManager.getInstance().getLithologyNameById(entity.TunnelLithologyID);
                //断面类型
                cells[rowDetailStartIndex + i, ++index].Text = entity.TunnelSectionType;
                //断面参数
                cells[rowDetailStartIndex + i, ++index].Text = entity.TunnelParam;
                //煤巷岩巷
                cells[rowDetailStartIndex + i, ++index].Text = entity.CoalOrStone;
                //绑定煤层
                cells[rowDetailStartIndex + i, ++index].Text =
                    entity.CoalLayerID == 0 ? "" : BasicInfoManager.getInstance().getCoalSeamById(entity.CoalLayerID).CoalSeamsName;

                ++index;
                if (WireInfoBLL.selectAllWireInfo(tunnelEntity).Tables[0].Rows.Count > 0)
                {
                    //绑定导线名称
                    cells[rowDetailStartIndex + i, index].Text =
                        WireInfoBLL.selectAllWireInfo(tunnelEntity).Tables[0].Rows[0][WireInfoDbConstNames.WIRE_NAME].ToString();

                }
                //未绑定导线巷道背景色设置
                else
                {
                    FarPointOperate.farPointRowColorChange(fpTunnelInfo, i, rowDetailStartIndex, columnCount, Const.NO_WIRE_TUNNEL_COLOR);
                }

                //巷道类型
                cells[rowDetailStartIndex + i, ++index].Text = ceChange(entity.TunnelType);
                //控制掘进回采巷道行背景色
                //掘进巷道
                if (cells[rowDetailStartIndex + i, index].Text == Const_GM.TUNNEL_TYPE_TUNNELING_CHN)
                {
                    FarPointOperate.farPointRowColorChange(fpTunnelInfo, i, rowDetailStartIndex, columnCount, Const.JJ_TUNNEL_COLOR);
                }
                //回采巷道
                if (cells[rowDetailStartIndex + i, index].Text == Const_GM.TUNNEL_TYPE_STOPING_CHN)
                {
                    FarPointOperate.farPointRowColorChange(fpTunnelInfo, i, rowDetailStartIndex, columnCount, Const.HC_TUNNEL_COLOR);
                }

                //BID

                //cells[rowDetailStartIndex + i, ++index].Text = entity.BindingID;

                cells[rowDetailStartIndex + i, ++index].Text = entity.TunnelId.ToString();

                i++;
            }

            setButtenEnable();
        }

        /// <summary>
        /// farpoint中checkbox选中对全选反选的影响
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void fpTunnelInfo_ButtonClicked(object sender, FarPoint.Win.Spread.EditorNotifyEventArgs e)
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
                        if (_htSelIdxs.Count == checkCount)
                        {
                            // 全选/全不选checkbox设为选中
                            this.chkSelAll.Checked = true;
                        }
                    }

                    checkCount++;
                }
                else
                {
                    // 移除索引号
                    _htSelIdxs.Remove(e.Row);

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
        /// 为变量tunnelEntity赋值
        /// </summary>
        private void setTunnelEntityValue()
        {
            int searchCount = rowsCount;
            int rowDetailStartIndex = 4;
            for (int i = 0; i < rowsCount; i++)
            {

                if (cells[rowDetailStartIndex + i, 0].Value != null &&
                    (bool)cells[rowDetailStartIndex + i, 0].Value == true)
                {
                    //巷道编号
                    tunnelEntity.TunnelId = (int)ds.Tables[0].Rows[i][TunnelInfoDbConstNames.ID];
                    //巷道实体
                    tunnelEntity = BasicInfoManager.getInstance().getTunnelByID(tunnelEntity.TunnelId);

                    activeRow = rowDetailStartIndex + i;
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
            LibCommonForm.TunnelInfoEntering d = new LibCommonForm.TunnelInfoEntering(this.MainForm);

            if (DialogResult.OK == d.ShowDialog())
            {
                //绑定信息
                bindFpTunnelInfo();
                //跳转到尾页
                this.dataPager1.btnLastPage_Click(sender, e);
                //获取最新行号
                activeRow = rowsCount + rowDetailStartIndex;
                // 设置farpoint焦点
                FarPointOperate.farPointFocusSetAdd(fpTunnelInfo, rowDetailStartIndex, rowsCount);
            }
        }

        /// <summary>
        /// 修改按钮响应
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tsBtnModify_Click(object sender, EventArgs e)
        {
            //实体赋值
            setTunnelEntityValue();
            int[] arr = new int[4] {
                tunnelEntity.WorkingFace.MiningArea.Horizontal.Mine.MineId,
                tunnelEntity.WorkingFace.MiningArea.Horizontal.HorizontalId,
                tunnelEntity.WorkingFace.MiningArea.MiningAreaId,
                tunnelEntity.WorkingFace.WorkingFaceID
            };

            LibCommonForm.TunnelInfoEntering d = new LibCommonForm.TunnelInfoEntering(tunnelEntity.TunnelId, arr, this.MainForm);
            if (DialogResult.OK == d.ShowDialog())
            {
                //绑定巷道信息
                bindFpTunnelInfo();
                //修改后焦点设置
                this.fpTunnelInfo.Sheets[0].SetActiveCell(activeRow, 0);
            }
        }

        /// <summary>
        /// 删除按钮响应
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tsBtnDel_Click(object sender, EventArgs e)
        {
            if (Alert.confirm(Const_GM.TUNNEL_INFO_MSG_DEL))
            {
                bool bResult = false;
                for (int i = 0; i < rowsCount; i++)
                {
                    if (cells[rowDetailStartIndex + i, 0].Value != null &&
                        (bool)cells[rowDetailStartIndex + i, 0].Value == true)
                    {
                        //掘进ID
                        tunnelEntity.TunnelId = (int)ds.Tables[0].Rows[i][TunnelInfoDbConstNames.ID];
                        //巷道类型为掘进或回采巷道
                        if (TunnelInfoBLL.isTunnelJJ(tunnelEntity) || TunnelInfoBLL.isTunnelHC(tunnelEntity))
                        {
                            TunnelInfoBLL.deleteJJHCTunnelInfo(tunnelEntity);
                        }
                        if (WireInfoBLL.selectAllWireInfo(tunnelEntity).Tables[0].Rows.Count > 0)
                        {
                            TunnelInfoBLL.deleteWireInfoBindingTunnelID(tunnelEntity);
                            //是否删除关联导线
                            if (Alert.confirm(Const_GM.TUNNEL_INFO_MSG_DEL_WIRE))
                            {
                                TunnelInfoBLL.deleteWireInfoBindingTunnelID(tunnelEntity);
                            }

                            //不删除时将导线重新绑定到其他巷道，默认为巷道ID=0
                            else
                            {
                                //WireInfoBLL.changeWireInfoTunnelIDAsDefaultValue(tunnelEntity, 0);
                            }
                        }
                        //删除巷道对应掘进日报
                        TunnelInfoBLL.deleteDayReportJJBindingTunnelID(tunnelEntity);
                        //删除巷道对应回采日报
                        TunnelInfoBLL.deleteDayReportHCBindingTunnelID(tunnelEntity);
                        //删除巷道
                        bResult = TunnelInfoBLL.deleteTunnelInfo(tunnelEntity);
                    }
                }
                if (bResult)
                {
                    //绑定信息
                    bindFpTunnelInfo();
                    //删除后焦点设置
                    FarPointOperate.farPointFocusSetDel(fpTunnelInfo, rowDetailStartIndex);
                }
            }
        }

        /// <summary>
        /// 确定按钮事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnOK_Click(object sender, EventArgs e)
        {
            this.Close();
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
                        if (!_htSelIdxs.Contains(rowDetailStartIndex + i))
                        {
                            _htSelIdxs.Add(rowDetailStartIndex + i, true);
                        }
                        this.fpTunnelInfo.Sheets[0].Cells[rowDetailStartIndex + i, 0].Value = ((CheckBox)sender).Checked;
                        checkCount = ds.Tables[0].Rows.Count;
                    }
                    //checkbox未选中
                    else
                    {
                        _htSelIdxs.Remove(rowDetailStartIndex + i);
                        this.fpTunnelInfo.Sheets[0].Cells[rowDetailStartIndex + i, 0].Value = ((CheckBox)sender).Checked;
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
            // 关闭窗口
            this.Close();
        }

        /// <summary>
        /// 导出按钮事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tsBtnExport_Click(object sender, EventArgs e)
        {
            if (FileExport.fileExport(fpTunnelInfo, true))
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
            FilePrint.CommonPrint(fpTunnelInfo, 0);
        }

        /// <summary>
        /// 刷新按钮事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tsBtnRefresh_Click(object sender, EventArgs e)
        {
            bindFpTunnelInfo();
        }

        /// <summary>
        /// 巷道类型中英转化
        /// </summary>
        /// <param name="tunnelType"></param>
        /// <returns></returns>
        public string ceChange(TunnelTypeEnum tunnelType)
        {
            //回采面
            if (tunnelType == TunnelTypeEnum.STOPING_FY ||
                tunnelType == TunnelTypeEnum.STOPING_QY ||
                tunnelType == TunnelTypeEnum.STOPING_ZY ||
                tunnelType == TunnelTypeEnum.STOPING_OTHER)
            {
                return Const_GM.TUNNEL_TYPE_STOPING_CHN;
            }

            //掘进巷道中->英
            if (tunnelType == TunnelTypeEnum.TUNNELLING)
            {
                return Const_GM.TUNNEL_TYPE_TUNNELING_CHN;
            }

            //回采巷道英->中
            if (tunnelType == TunnelTypeEnum.OTHER)
            {
                return Const_GM.TUNNEL_TYPE_OTHER_CHN;
            }

            //无结果返回“”
            return "";
        }

        #region Farpoint自动过滤功能
        private void farpointFilter1_OnCheckFilterChanged(object sender, EventArgs arg)
        {
            CheckBox chk = (CheckBox)sender;
            //当Checkbox选中时，筛选过程中则将不符合条件的数据隐藏

            MessageBox.Show("你好");
            if (chk.Checked == true)
            {
                //禁用选择颜色相关控件
                farpointFilter1.EnableChooseColorCtrls(false);
                //设置自动隐藏过滤条件
                FarpointDefaultPropertiesSetter.SetFpFilterHideProperties(this.fpTunnelInfo, _filterColunmIdxs);

            }
            else//未选中时，根据用户自定义的颜色进行分类显示
            {
                //启用选择颜色相关控件
                farpointFilter1.EnableChooseColorCtrls(true);
                //设置自定义过滤条件
                FarpointDefaultPropertiesSetter.SetFpCustomFilterProperties(this.fpTunnelInfo, farpointFilter1.GetSelectedFitColor(), farpointFilter1.GetSelectedNotFitColor(), _filterColunmIdxs);
            }
        }

        private void farpointFilter1_OnClickClearFilterBtn(object sender, EventArgs arg)
        {
            //清空过滤条件
            this.fpTunnelInfo.ActiveSheet.RowFilter.ResetFilter();
        }

        private void farpointFilter1_OnClickFitColorBtnOK(object sender, EventArgs arg)
        {
            //根据新的颜色值设置自动隐藏过滤条件
            FarpointDefaultPropertiesSetter.SetFpCustomFilterProperties(this.fpTunnelInfo, farpointFilter1.GetSelectedFitColor(), farpointFilter1.GetSelectedNotFitColor(), _filterColunmIdxs);
        }

        private void farpointFilter1_OnClickNotFitColorBtnOK(object sender, EventArgs arg)
        {
            //根据新的颜色值设置自动隐藏过滤条件
            FarpointDefaultPropertiesSetter.SetFpCustomFilterProperties(this.fpTunnelInfo, farpointFilter1.GetSelectedFitColor(), farpointFilter1.GetSelectedNotFitColor(), _filterColunmIdxs);
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
            ILayer pLayer = GIS.Common.DataEditCommon.GetLayerByName(GIS.Common.DataEditCommon.g_pMap, GIS.LayerNames.LAYER_ALIAS_MR_TUNNEL);
            if (pLayer == null)
            {
                MessageBox.Show("未发现巷道全图层！");
                return;
            }
            IFeatureLayer pFeatureLayer = (IFeatureLayer)pLayer;
            string str = "";
            for (int i = 0; i < iSelIdxsArr.Length; i++)
            {
                bid = this.fpTunnelInfo.Sheets[0].Cells[iSelIdxsArr[i], _BIDIndex].Text.Trim();
                if (bid != "")
                {
                    if (i == 0)
                        str = "HdId='" + bid + "'";
                    else
                        str += " or HdId='" + bid + "'";
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
