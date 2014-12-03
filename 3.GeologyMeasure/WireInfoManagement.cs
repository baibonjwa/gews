// ******************************************************************
// 概  述：导线信息管理
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
using LibCommon;
using LibEntity;
using LibCommonControl;
using LibCommonForm;
using Microsoft.Office.Interop.Excel;
using System.Collections;
using ESRI.ArcGIS.Carto;
using GIS;
using GIS.Common;

namespace _3.GeologyMeasure
{
    public partial class WireInfoManagement : BaseForm
    {
        //****************变量声明***************
        private int _iRecordCount = 0;
        int _rowsCount = 0;      //数据行数
        int _delRows = 0;
        int _checkCount = 0;     //选择行数
        int _rowDetailStartIndex = 2;
        Color rowBackColor = Color.White;
        TunnelEntity tunnelEntity = null;
        WireInfoEntity wireInfoEntity = new WireInfoEntity();
        WirePointInfoEntity wirePointInfoEntity = new WirePointInfoEntity();
        DataSet _ds = new DataSet();
        DataSet _dsWirePoint = new DataSet();
        int[] _wirePointPrimaryKey;
        //需要过滤的列索引
        private int[] _filterColunmIdxs = null;

        FarPoint.Win.Spread.Cells cells = null;
        private int _BIDIndex = 22;
        /** 保存所有用户选中的行的索引 **/
        private Hashtable _htSelIdxs = new Hashtable();

        // 列名称
        const int COLUMN_INDEX_CHOOSE_BUTTON = 0;      // 选择按钮
        const int COLUMN_INDEX_MINE_NAME = 1;      // 矿井名称
        const int COLUMN_INDEX_HORIZONTAL_NAME = 2;      // 水平名称
        const int COLUMN_INDEX_MINING_AREA_NAME = 3;      // 采区名称
        const int COLUMN_INDEX_WORKING_FACE_NAME = 4;      // 工作面名称
        const int COLUMN_INDEX_TUNNEL_NAME = 5;      // 巷道名称
        //****************************************

        /// <summary>
        /// 构造方法
        /// </summary>
        public WireInfoManagement(MainFrm mainFrm)
        {
            this.MainForm = mainFrm;
            InitializeComponent();

            //分页控件
            dataPager1.FrmChild_EventHandler += new DataPager.FrmChild_DelegateHandler(FrmParent_EventHandler);

            //设置窗体属性
            LibCommon.FormDefaultPropertiesSetter.SetManagementFormDefaultProperties(this, Const_GM.WIRE_INFO_MANAGEMENT);

            //设置Farpoint属性
            LibCommon.FarpointDefaultPropertiesSetter.SetFpDefaultProperties(fpWire, LibCommon.Const_GM.WIRE_INFO_FARPOINT_TITLE, _rowDetailStartIndex);

            _filterColunmIdxs = new int[]
            {
                1, 2, 3, 4, 5, 5, 17, 19, 21,
            };
            //禁用选择颜色相关控件
            farpointFilter1.EnableChooseColorCtrls(false);
            //设置自动隐藏过滤条件
            FarpointDefaultPropertiesSetter.SetFpFilterHideProperties(this.fpWire, _filterColunmIdxs);

            cells = this.fpWire.Sheets[0].Cells;
        }

        /// <summary>
        /// 初始化
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void WireInfoManagement_Load(object sender, EventArgs e)
        {
            bindFpWire();
        }

        /// <summary>
        /// 委托事件
        /// </summary>
        /// <param name="sender"></param>
        private void FrmParent_EventHandler(object sender)
        {
            bindFpWire();
        }

        /// <summary>
        /// 绑定farpoint数据
        /// </summary>
        private void bindFpWire()
        {
            //清空Farpoint
            FarPointOperate.farPointClear(fpWire, _rowDetailStartIndex, _rowsCount);

            // 清空HashTabl（必须实装）
            _htSelIdxs.Clear();

            _rowsCount = 0;
            _checkCount = 0;
            rowBackColor = Color.White;

            chkSelAll.Checked = false;
            // ※分页必须
            _iRecordCount = WireInfoBLL.selectAllWireInfo().Tables[0].Rows.Count;

            // ※分页必须
            dataPager1.PageControlInit(_iRecordCount);

            int iStartIndex = dataPager1.getStartIndex();
            int iEndIndex = dataPager1.getEndIndex();

            //分页用信息
            _ds = WireInfoBLL.selectAllWireInfo(iStartIndex, iEndIndex);

            //分页用数据行数
            _rowsCount = _ds.Tables[0].Rows.Count;

            //重绘Farpoint
            FarPointOperate.farPointReAdd(fpWire, _rowDetailStartIndex, _rowsCount);

            if (_rowsCount > 0)
            {
                FarPoint.Win.Spread.CellType.CheckBoxCellType ckbxcell = new FarPoint.Win.Spread.CellType.CheckBoxCellType();
                ckbxcell.ThreeState = false;

                /**合并单元格用行列号**/
                int addSpanRowIndex = _rowDetailStartIndex;
                int addSpanRowCounts = 0;

                for (int i = 0; i < _rowsCount; i++)
                {
                    DataRow drInfo = _ds.Tables[0].Rows[i];
                    int tunnelID = Convert.ToInt32(drInfo[WireInfoDbConstNames.TUNNEL_ID]);

                    //行编号累加
                    addSpanRowIndex += addSpanRowCounts;
                    //列编号清空
                    addSpanRowCounts = 0;

                    //导线编号
                    int wireInfoID = Convert.ToInt32(drInfo[WireInfoDbConstNames.ID].ToString());
                    //巷道实体
                    tunnelEntity = BasicInfoManager.getInstance().getTunnelByID(tunnelID);

                    // 获取该导线的导线点信息
                    _dsWirePoint = WirePointBLL.selectAllWirePointInfo(wireInfoID);
                    for (int j = 0; j < _dsWirePoint.Tables[0].Rows.Count; j++)
                    {
                        fpWire.ActiveSheet.RowCount++;
                        DataRow drPoint = _dsWirePoint.Tables[0].Rows[j];

                        // 选择按钮
                        cells[addSpanRowIndex + j, COLUMN_INDEX_CHOOSE_BUTTON].CellType = ckbxcell;
                        //矿井名称
                        cells[addSpanRowIndex + j, COLUMN_INDEX_MINE_NAME].Text = tunnelEntity.WorkingFace.MiningArea.Horizontal.Mine.MineName;
                        //水平
                        cells[addSpanRowIndex + j, COLUMN_INDEX_HORIZONTAL_NAME].Text = tunnelEntity.WorkingFace.MiningArea.Horizontal.HorizontalName;
                        //采区
                        cells[addSpanRowIndex + j, COLUMN_INDEX_MINING_AREA_NAME].Text = tunnelEntity.WorkingFace.MiningArea.MiningAreaName;
                        //工作面
                        cells[addSpanRowIndex + j, COLUMN_INDEX_WORKING_FACE_NAME].Text = tunnelEntity.WorkingFace.WorkingFaceName;
                        //巷道名称
                        cells[addSpanRowIndex + j, COLUMN_INDEX_TUNNEL_NAME].Text = tunnelEntity.TunnelName;


                        //导线名称
                        cells[addSpanRowIndex + j, 6].Text = drInfo[WireInfoDbConstNames.WIRE_NAME].ToString();

                        //导线点编号
                        cells[addSpanRowIndex + j, 7].Text = drPoint[WirePointDbConstNames.WIRE_POINT_NAME].ToString();
                        //坐标X
                        cells[addSpanRowIndex + j, 8].Text = drPoint[WirePointDbConstNames.COORDINATE_X].ToString();
                        //坐标Y
                        cells[addSpanRowIndex + j, 9].Text = drPoint[WirePointDbConstNames.COORDINATE_Y].ToString();
                        //坐标Z
                        cells[addSpanRowIndex + j, 10].Text = drPoint[WirePointDbConstNames.COORDINATE_Z].ToString();
                        //距左帮距离
                        cells[addSpanRowIndex + j, 11].Text = drPoint[WirePointDbConstNames.DISTANCE_FROM_THE_LEFT].ToString();
                        //距右帮距离
                        cells[addSpanRowIndex + j, 12].Text = drPoint[WirePointDbConstNames.DISTANCE_FROM_THE_RIGHT].ToString();
                        //距顶板距离
                        cells[addSpanRowIndex + j, 13].Text = drPoint[WirePointDbConstNames.DISTANCE_FROM_TOP].ToString();
                        //距底板距离
                        cells[addSpanRowIndex + j, 14].Text = drPoint[WirePointDbConstNames.DISTANCE_FROM_BOTTOM].ToString();

                        //导线级别
                        cells[addSpanRowIndex + j, 15].Text = drInfo[WireInfoDbConstNames.WIRE_LEVEL].ToString();
                        //测量日期
                        cells[addSpanRowIndex + j, 16].Text = drInfo[WireInfoDbConstNames.MEASURE_DATE].ToString().Substring(0, _ds.Tables[0].Rows[i][WireInfoDbConstNames.MEASURE_DATE].ToString().IndexOf(' '));
                        //观测者
                        cells[addSpanRowIndex + j, 17].Text = drInfo[WireInfoDbConstNames.VOBSERVER].ToString();
                        //计算者
                        cells[addSpanRowIndex + j, 18].Text = drInfo[WireInfoDbConstNames.COUNT_DATE].ToString().Substring(0, _ds.Tables[0].Rows[i][WireInfoDbConstNames.COUNT_DATE].ToString().IndexOf(' '));
                        //计算日期
                        cells[addSpanRowIndex + j, 19].Text = drInfo[WireInfoDbConstNames.COUNTER].ToString();
                        //校核者
                        cells[addSpanRowIndex + j, 20].Text = drInfo[WireInfoDbConstNames.CHECK_DATE].ToString().Substring(0, _ds.Tables[0].Rows[i][WireInfoDbConstNames.CHECK_DATE].ToString().IndexOf(' '));
                        //校核日期
                        cells[addSpanRowIndex + j, 21].Text = drInfo[WireInfoDbConstNames.CHECKER].ToString();
                        //bid
                        cells[addSpanRowIndex + j, 22].Text = drPoint[WirePointDbConstNames.BINDINGID].ToString();

                        addSpanRowCounts++;
                        FarPointOperate.farPointRowBackColorChange(fpWire, addSpanRowIndex + j, 0, 21, rowBackColor);
                    }
                    rowBackColor = FarPointOperate.changeColor(rowBackColor);
                    this.fpWire.ActiveSheet.RowCount--;

                    if (addSpanRowCounts > 0)
                    {
                        //合并单元格
                        this.fpWire.ActiveSheet.AddSpanCell(addSpanRowIndex, 0, addSpanRowCounts, 1);
                        this.fpWire.ActiveSheet.AddSpanCell(addSpanRowIndex, 1, addSpanRowCounts, 1);
                        this.fpWire.ActiveSheet.AddSpanCell(addSpanRowIndex, 2, addSpanRowCounts, 1);
                        this.fpWire.ActiveSheet.AddSpanCell(addSpanRowIndex, 3, addSpanRowCounts, 1);
                        this.fpWire.ActiveSheet.AddSpanCell(addSpanRowIndex, 4, addSpanRowCounts, 1);
                        this.fpWire.ActiveSheet.AddSpanCell(addSpanRowIndex, 5, addSpanRowCounts, 1);
                        this.fpWire.ActiveSheet.AddSpanCell(addSpanRowIndex, 6, addSpanRowCounts, 1);
                        this.fpWire.ActiveSheet.AddSpanCell(addSpanRowIndex, 15, addSpanRowCounts, 1);
                        this.fpWire.ActiveSheet.AddSpanCell(addSpanRowIndex, 16, addSpanRowCounts, 1);
                        this.fpWire.ActiveSheet.AddSpanCell(addSpanRowIndex, 17, addSpanRowCounts, 1);
                        this.fpWire.ActiveSheet.AddSpanCell(addSpanRowIndex, 18, addSpanRowCounts, 1);
                        this.fpWire.ActiveSheet.AddSpanCell(addSpanRowIndex, 19, addSpanRowCounts, 1);
                        this.fpWire.ActiveSheet.AddSpanCell(addSpanRowIndex, 20, addSpanRowCounts, 1);
                        this.fpWire.ActiveSheet.AddSpanCell(addSpanRowIndex, 21, addSpanRowCounts, 1);
                    }
                }

                _rowsCount = _rowsCount - _delRows;
                fpWire.ActiveSheet.RowCount = fpWire.ActiveSheet.RowCount - _delRows;
                //导线点主键
                _wirePointPrimaryKey = new int[this.fpWire.ActiveSheet.RowCount - _rowDetailStartIndex];
                int index = 0;
                for (int i = 0; i < _rowsCount; i++)
                {
                    _dsWirePoint = WirePointBLL.selectAllWirePointInfo(Convert.ToInt32(_ds.Tables[0].Rows[i][WireInfoDbConstNames.ID].ToString()));
                    for (int j = 0; j < _dsWirePoint.Tables[0].Rows.Count; j++)
                    {
                        _wirePointPrimaryKey[index++] = Convert.ToInt32(_dsWirePoint.Tables[0].Rows[j][WirePointDbConstNames.ID]);
                    }
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
        private void fpWire_ButtonClicked(object sender, FarPoint.Win.Spread.EditorNotifyEventArgs e)
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
        /// 添加按钮响应
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tsBtnAdd_Click(object sender, EventArgs e)
        {
            WireInfoEntering wireInfoForm = new WireInfoEntering(this.MainForm);
            if (DialogResult.OK == wireInfoForm.ShowDialog())
            {
                bindFpWire();
                this.dataPager1.btnLastPage_Click(sender, e);
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
            setWireInfoEntityValue();

            int tunnelID = WireInfoBLL.selectTunnelIDByWireInfoID(wireInfoEntity.WireInfoID);
            tunnelEntity = BasicInfoManager.getInstance().getTunnelByID(tunnelID);

            //自定义用控件用矿井信息编号
            int[] arr = new int[5] 
            { 
                tunnelEntity.WorkingFace.MiningArea.Horizontal.Mine.MineId, 
                tunnelEntity.WorkingFace.MiningArea.Horizontal.HorizontalId, 
                tunnelEntity.WorkingFace.MiningArea.MiningAreaId, 
                tunnelEntity.WorkingFace.WorkingFaceID, 
                tunnelEntity.TunnelID,
            };

            //导线修改界面
            WireInfoEntering wireInfoForm = new WireInfoEntering(arr, wireInfoEntity, this.MainForm);
            if (DialogResult.OK == wireInfoForm.ShowDialog())
            {
                bindFpWire();
            }
        }

        /// <summary>
        /// 导线实体赋值
        /// </summary>
        public void setWireInfoEntityValue()
        {
            for (int i = 0; i < _wirePointPrimaryKey.Length; i++)
            {
                if (cells[_rowDetailStartIndex + i, 0].Value != null &&
                    (bool)cells[_rowDetailStartIndex + i, 0].Value == true)
                {
                    //导线点ID
                    wirePointInfoEntity.ID = _wirePointPrimaryKey[i];

                    //导线点实体
                    wirePointInfoEntity = WirePointBLL.selectWirePointInfoByWirePointId(wirePointInfoEntity.ID);

                    //矿井编号
                    wireInfoEntity.WireInfoID = wirePointInfoEntity.WireInfoID;

                    //巷道实体
                    wireInfoEntity = WireInfoBLL.selectAllWireInfo(wireInfoEntity.WireInfoID);
                }
            }
        }

        /// <summary>
        /// 删除按钮响应
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tsBtnDel_Click(object sender, EventArgs e)
        {
            bool bResult = false;

            //是否删除导线点
            if (Alert.confirm(Const.DEL_CONFIRM_MSG))
            {
                for (int i = 0; i < _wirePointPrimaryKey.Length; i++)
                {
                    if (cells[_rowDetailStartIndex + i, 0].Value != null && (bool)cells[_rowDetailStartIndex + i, 0].Value == true)
                    {
                        //导线点ID
                        wirePointInfoEntity.ID = _wirePointPrimaryKey[i];

                        //导线点实体
                        wirePointInfoEntity = WirePointBLL.selectWirePointInfoByWirePointId(wirePointInfoEntity.ID);

                        //矿井编号
                        wireInfoEntity.WireInfoID = wirePointInfoEntity.WireInfoID;

                        //导线实体
                        wireInfoEntity = WireInfoBLL.selectAllWireInfo(wireInfoEntity.WireInfoID);
                        DataSet ds = WirePointBLL.selectAllWirePointInfo(wireInfoEntity.WireInfoID);
                        if (ds.Tables[0].Rows.Count > 0)
                        {
                            bResult = WirePointBLL.deleteWirePointInfo(wireInfoEntity);
                        }
                        bResult = WireInfoBLL.deleteWireInfo(wireInfoEntity);

                        //20140430 lyf
                        //同时删除导线点、巷道图元
                        DialogResult dlgResult = MessageBox.Show("是否删除对应图元？", "", MessageBoxButtons.YesNo);
                        if (dlgResult == DialogResult.Yes)
                        {
                            //DeleteWirePtByBID(wirePointInfoEntity);
                            DelHdByHdId(tunnelEntity.TunnelID.ToString());

                            //wireInfoEntity.TunnelID
                        }
                    }
                }

                // 全部删除成功
                if (bResult)
                {
                    //TODO:全部删除后事件
                }

                bindFpWire();
            }
        }

        /// <summary>
        /// 根据巷道ID，删除巷道图元
        /// </summary>
        /// <param name="HdId">巷道Id</param>
        private void DelHdByHdId(string HdId)
        {
            //清除巷道信息
            string sql = "\"" + GIS_Const.FIELD_HDID + "\"='" + HdId + "'";
            //string sql = "\"" + GIS_Const.FIELD_HDID + "\"<>'" + HdId + "'";
            GIS.HdProc.Global.commonclss.DelFeatures(GIS.HdProc.Global.pntlyr, sql);
            GIS.HdProc.Global.commonclss.DelFeatures(GIS.HdProc.Global.centerlyr, sql);
            GIS.HdProc.Global.commonclss.DelFeatures(GIS.HdProc.Global.centerfdlyr, sql);
            GIS.HdProc.Global.commonclss.DelFeatures(GIS.HdProc.Global.hdfdfulllyr, sql);
            GIS.HdProc.Global.commonclss.DelFeatures(GIS.HdProc.Global.hdfdlyr, sql);
            GIS.HdProc.Global.commonclss.DelFeatures(GIS.HdProc.Global.pntlinlyr, sql);
            //删除峒室信息
            GIS.HdProc.Global.commonclss.DelFeatures(GIS.HdProc.Global.dslyr, sql);
        }

        #region 删导线点、巷道图元

        /// <summary>
        /// 根据导线点绑定ID删除导线点图元
        /// </summary>
        /// <param name="sfpFaultageBIDArray">要删除导线点的绑定ID</param>
        private void DeleteWirePtByBID(WirePointInfoEntity wirePointInfoEntity)
        {
            if (wirePointInfoEntity.BindingID == "") return;

            //1.获得当前编辑图层
            DrawSpecialCommon drawspecial = new DrawSpecialCommon();
            string sLayerAliasName = GIS.LayerNames.DEFALUT_WIRE_PT;//“默认_导线点”图层
            IFeatureLayer featureLayer = drawspecial.GetFeatureLayerByName(sLayerAliasName);
            if (featureLayer == null)
            {
                MessageBox.Show("未找到" + sLayerAliasName + "图层,无法删除导线点图元。");
                return;
            }

            //2.删除导线点图元
            DataEditCommon.DeleteFeatureByBId(featureLayer, wirePointInfoEntity.BindingID);
        }


        /// <summary>
        /// 根据巷道ID删除巷道图元
        /// </summary>
        /// <param name="sfpFaultageBIDArray">要删除巷道的绑定ID</param>
        private void DeleteWirePtByBID(WireInfoEntity wireInfoEntity)
        {
            if (wireInfoEntity.TunnelID.ToString() == "") return;

            //1.获得当前编辑图层
            DrawSpecialCommon drawspecial = new DrawSpecialCommon();
            string sLayerAliasName = GIS.LayerNames.DEFALUT_TUNNEL;//“默认_巷道”图层
            IFeatureLayer featureLayer = drawspecial.GetFeatureLayerByName(sLayerAliasName);
            if (featureLayer == null)
            {
                MessageBox.Show("未找到" + sLayerAliasName + "图层,无法删除巷道图元。");
                return;
            }

            //2.删除巷道图元
            DataEditCommon.DeleteFeatureByBId(featureLayer, wireInfoEntity.TunnelID.ToString());
        }

        #endregion

        /// <summary>
        /// 刷新按钮响应
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tsBtnRefresh_Click(object sender, EventArgs e)
        {
            bindFpWire();
        }

        /// <summary>
        /// 退出按钮响应
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
            if (_rowsCount > 0)
            {
                //遍历数据
                for (int i = _rowDetailStartIndex; i < fpWire.ActiveSheet.Rows.Count; i++)
                {
                    if (cells[i, 1].Value != cells[i - 1, 1].Value)
                    {
                        cells[i, 0].Value = ((System.Windows.Forms.CheckBox)sender).Checked;
                    }
                    //checkbox选中
                    if (chkSelAll.Checked)
                    {
                        if (!_htSelIdxs.Contains(_rowDetailStartIndex + i))
                        {
                            _htSelIdxs.Add(_rowDetailStartIndex + i, true);
                        }
                        _checkCount = _ds.Tables[0].Rows.Count;
                    }
                    //checkbox未选中
                    else
                    {
                        _htSelIdxs.Remove(_rowDetailStartIndex + i);
                        _checkCount = 0;
                    }

                    // 将存有选中项目的数组清空

                }
            }
            //设置按钮可操作性
            setButtenEnable();
        }

        /// <summary>
        /// 确定按钮
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnOK_Click(object sender, EventArgs e)
        {
            // 关闭窗口
            this.Close();
        }

        /// <summary>
        /// 取消按钮
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnCancel_Click(object sender, EventArgs e)
        {
            // 关闭窗口
            this.Close();
        }

        /// <summary>
        /// 导出按钮
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tsBtnExport_Click(object sender, EventArgs e)
        {
            if (FileExport.fileExport(fpWire, true))
            {
                /**导出成功提示暂时保留**/
                Alert.alert(Const.EXPORT_SUCCESS_MSG);
            }
        }

        /// <summary>
        /// 打印按钮
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tsBtnPrint_Click(object sender, EventArgs e)
        {
            //打印
            FilePrint.CommonPrint(fpWire, 0);
        }

        #region Farpoint自动过滤功能
        private void farpointFilter1_OnCheckFilterChanged(object sender, EventArgs arg)
        {
            System.Windows.Forms.CheckBox chk = (System.Windows.Forms.CheckBox)sender;
            //当Checkbox选中时，筛选过程中则将不符合条件的数据隐藏
            if (chk.Checked == true)
            {
                //禁用选择颜色相关控件
                farpointFilter1.EnableChooseColorCtrls(false);
                //设置自动隐藏过滤条件
                FarpointDefaultPropertiesSetter.SetFpFilterHideProperties(this.fpWire, _filterColunmIdxs);

            }
            else//未选中时，根据用户自定义的颜色进行分类显示
            {
                //启用选择颜色相关控件
                farpointFilter1.EnableChooseColorCtrls(true);
                //设置自定义过滤条件
                FarpointDefaultPropertiesSetter.SetFpCustomFilterProperties(this.fpWire, farpointFilter1.GetSelectedFitColor(), farpointFilter1.GetSelectedNotFitColor(), _filterColunmIdxs);
            }
        }

        private void farpointFilter1_OnClickClearFilterBtn(object sender, EventArgs arg)
        {
            //清空过滤条件
            this.fpWire.ActiveSheet.RowFilter.ResetFilter();
        }

        private void farpointFilter1_OnClickFitColorBtnOK(object sender, EventArgs arg)
        {
            //根据新的颜色值设置自动隐藏过滤条件
            FarpointDefaultPropertiesSetter.SetFpCustomFilterProperties(this.fpWire, farpointFilter1.GetSelectedFitColor(), farpointFilter1.GetSelectedNotFitColor(), _filterColunmIdxs);
        }

        private void farpointFilter1_OnClickNotFitColorBtnOK(object sender, EventArgs arg)
        {
            //根据新的颜色值设置自动隐藏过滤条件
            FarpointDefaultPropertiesSetter.SetFpCustomFilterProperties(this.fpWire, farpointFilter1.GetSelectedFitColor(), farpointFilter1.GetSelectedNotFitColor(), _filterColunmIdxs);
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
            ILayer pLayer = GIS.Common.DataEditCommon.GetLayerByName(GIS.Common.DataEditCommon.g_pMap, GIS.LayerNames.DEFALUT_WIRE_PT);
            if (pLayer == null)
            {
                MessageBox.Show("未发现导线点图层！");
                return;
            }
            IFeatureLayer pFeatureLayer = (IFeatureLayer)pLayer;
            string str = "";
            for (int i = 0; i < iSelIdxsArr.Length; i++)
            {
                bid = this.fpWire.Sheets[0].Cells[iSelIdxsArr[i], _BIDIndex].Text.Trim();
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
