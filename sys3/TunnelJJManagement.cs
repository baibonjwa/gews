// ******************************************************************
// 概  述：掘进巷道信息管理
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
using LibCommonControl;
using LibCommonForm;

namespace _3.GeologyMeasure
{
    public partial class TunnelJJManagement : BaseForm
    {
        /****************变量声明***************/
        private int _iRecordCount = 0;
        int rowsCount = 0;      //数据行数
        int checkCount = 0;     //选择行数
        int rowDetailStartIndex = 4;//表头冻结行数
        int _tmpRowIndex = 0;
        //需要过滤的列索引
        private int[] _filterColunmIdxs = null;
        public static WorkingFace jjWorkFaceEntity = null;
        public static Tunnel tunnelEntity = null;
        // 分页时用
        DataSet ds = new DataSet();
        // 数据库中全部数据
        DataSet dsAll = new DataSet();
        int tmpInt = 0;

        FarPoint.Win.Spread.Cells cells = null;

        //各列索引
        const int COLUMN_INDEX_CHOOSE_BUTTON = 0;      // 选择按钮
        const int COLUMN_INDEX_MINE_NAME = 1;      // 矿井名称
        const int COLUMN_INDEX_HORIZONTAL_NAME = 2;      // 水平名称
        const int COLUMN_INDEX_MINING_AREA_NAME = 3;      // 采区名称
        const int COLUMN_INDEX_WORKING_FACE_NAME = 4;      // 工作面名称
        const int COLUMN_INDEX_TUNNEL_NAME = 5;      // 巷道名称
        const int COLUMN_INDEX_TEAM_NAME = 6;      // 对别名称
        const int COLUMN_INDEX_START_DATE = 7;      // 开始时间
        const int COLUMN_INDEX_IS_FINISHED = 8;      // 是否结束
        const int COLUMN_INDEX_STOP_DATE = 9;      // 结束名称
        const int COLUMN_INDEX_WORK_TIME = 10;     // 班次
        const int COLUMN_INDEX_TUNNEL_ID = 11;     // 巷道id
        const int COLUMN_INDEX_WORKING_FACE_ID = 12;     // id
        private int _BIDIndex = 13;
        /** 保存所有用户选中的行的索引 **/
        private Hashtable _htSelIdxs = new Hashtable();

        /****************************************/

        /// <summary>
        /// 构造方法
        /// </summary>
        public TunnelJJManagement(MainFrm mainFrm)
        {
            this.MainForm = mainFrm;
            InitializeComponent();

            dataPager1.FrmChild_EventHandler += FrmParent_EventHandler;
            LibCommon.FormDefaultPropertiesSetter.SetManagementFormDefaultProperties(this, Const_GM.TUNNEL_JJ_MANAGEMENT);
            LibCommon.FarpointDefaultPropertiesSetter.SetFpDefaultProperties(fpTunnelJJInfo,
                LibCommon.Const_GM.TUNNEL_JJ_FARPOINT_TITLE,
                rowDetailStartIndex
                );
            _filterColunmIdxs = new int[]
            {
                1,
                2,
                3,
                4,
                6,
                8,
                10,
            };
            //禁用选择颜色相关控件
            farpointFilter1.EnableChooseColorCtrls(false);
            //设置自动隐藏过滤条件
            FarpointDefaultPropertiesSetter.SetFpFilterHideProperties(this.fpTunnelJJInfo, _filterColunmIdxs);

            cells = this.fpTunnelJJInfo.Sheets[0].Cells;

            bindFpTunnelJJInfo();
        }

        /// <summary>
        /// 初始化
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TunnelInfoManagement_Load(object sender, EventArgs e)
        {
            this.bindFpTunnelJJInfo();
        }

        /// <summary>
        /// 委托事件
        /// </summary>
        /// <param name="sender"></param>
        private void FrmParent_EventHandler(object sender)
        {
            bindFpTunnelJJInfo();
        }

        /// <summary>
        /// farpoint数据绑定
        /// </summary>
        private void bindFpTunnelJJInfo()
        {
            // 清空HashTabl（必须实装）
            _htSelIdxs.Clear();

            FarPointOperate.farPointClear(fpTunnelJJInfo, rowDetailStartIndex, rowsCount);
            chkSelAll.Checked = false;
            checkCount = 0;
            // ※分页必须
            _iRecordCount = TunnelInfoBLL.selectTunnelJJ().Tables[0].Rows.Count;
            // ※分页必须
            dataPager1.PageControlInit(_iRecordCount);
            int iStartIndex = dataPager1.getStartIndex();
            int iEndIndex = dataPager1.getEndIndex();

            ds = TunnelInfoBLL.selectTunnelJJ(iStartIndex, iEndIndex);

            if (ds.Tables.Count == 0)
            {
                Log.Info("[Jue Jin]--没有掘进巷道。");
                return;
            }

            List<Tunnel> tunnelList = BasicInfoManager.getInstance().getTunnelListByDataSet(ds);

            rowsCount = ds.Tables[0].Rows.Count;
            FarPointOperate.farPointReAdd(fpTunnelJJInfo, rowDetailStartIndex, rowsCount);
            if (rowsCount > 0)
            {
                FarPoint.Win.Spread.CellType.CheckBoxCellType ckbxcell = new FarPoint.Win.Spread.CellType.CheckBoxCellType();
                ckbxcell.ThreeState = false;
                int i = 0;
                foreach (Tunnel entity in tunnelList)
                {
                    cells[rowDetailStartIndex + i, COLUMN_INDEX_CHOOSE_BUTTON].CellType = ckbxcell;
                    //矿井名称
                    cells[rowDetailStartIndex + i, COLUMN_INDEX_MINE_NAME].Text = entity.WorkingFace.MiningArea.Horizontal.Mine.MineName;
                    //水平
                    cells[rowDetailStartIndex + i, COLUMN_INDEX_HORIZONTAL_NAME].Text = entity.WorkingFace.MiningArea.Horizontal.HorizontalName;
                    //采区
                    cells[rowDetailStartIndex + i, COLUMN_INDEX_MINING_AREA_NAME].Text = entity.WorkingFace.MiningArea.MiningAreaName;
                    //工作面
                    cells[rowDetailStartIndex + i, COLUMN_INDEX_WORKING_FACE_NAME].Text = entity.WorkingFace.WorkingFaceName;
                    //巷道名称
                    cells[rowDetailStartIndex + i, COLUMN_INDEX_TUNNEL_NAME].Text = entity.TunnelName;
                    //队别
                    cells[rowDetailStartIndex + i, COLUMN_INDEX_TEAM_NAME].Text = BasicInfoManager.getInstance().getTeamNameById(entity.WorkingFace.TeamNameId);
                    //开工日期
                    cells[rowDetailStartIndex + i, COLUMN_INDEX_START_DATE].Text = String.Format("{0:yyyy-MM-dd}", entity.WorkingFace.StartDate);
                    //是否掘进完毕
                    cells[rowDetailStartIndex + i, COLUMN_INDEX_IS_FINISHED].Text = entity.WorkingFace.IsFinish == Const.FINISHED ? Const.MSG_YES : Const.MSG_NO; ;
                    //停工日期
                    if (entity.WorkingFace.IsFinish == Const.FINISHED)
                    {
                        cells[rowDetailStartIndex + i, COLUMN_INDEX_STOP_DATE].Text = String.Format("{0:yyyy-MM-dd}", entity.WorkingFace.StopDate); ;
                    }

                    //班次
                    cells[rowDetailStartIndex + i, COLUMN_INDEX_WORK_TIME].Text = entity.WorkingFace.WorkTime;

                    // 隐藏列，
                    cells[rowDetailStartIndex + i, COLUMN_INDEX_TUNNEL_ID].Text = entity.TunnelId.ToString();
                    cells[rowDetailStartIndex + i, COLUMN_INDEX_WORKING_FACE_ID].Text = entity.WorkingFace.WorkingFaceId.ToString();

                    cells[rowDetailStartIndex + i, _BIDIndex].Text = entity.BindingID;

                    i++;
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

                    // 全选/全不选checkbox设为未选中
                    this.chkSelAll.Checked = false;

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

        ///// <summary>
        ///// 为变量jjWorkFaceEntity赋值
        ///// </summary>
        private void setTunnelJJEntityValue()
        {
            int searchCount = rowsCount;
            for (int i = 0; i < rowsCount; i++)
            {
                if (cells[rowDetailStartIndex + i, 0].Text == "True")
                {
                    //
                    jjWorkFaceEntity = BasicInfoManager.getInstance().getWorkingFaceById(
                        Convert.ToInt32(cells[rowDetailStartIndex + i, COLUMN_INDEX_WORKING_FACE_ID].Text)
                        );
                    tunnelEntity = BasicInfoManager.getInstance().getTunnelByID(
                        Convert.ToInt32(cells[rowDetailStartIndex + i, COLUMN_INDEX_TUNNEL_ID].Text)
                        );
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
            TunnelJJEntering d = new TunnelJJEntering(this.MainForm);
            if (DialogResult.OK == d.ShowDialog())
            {
                bindFpTunnelJJInfo();
                this.dataPager1.btnLastPage_Click(sender, e);
                FarPointOperate.farPointFocusSetAdd(fpTunnelJJInfo, rowDetailStartIndex, rowsCount);
            }
        }

        /// <summary>
        /// 修改按钮响应
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tsBtnModify_Click(object sender, EventArgs e)
        {
            setTunnelJJEntityValue();
            TunnelJJEntering d = new TunnelJJEntering(jjWorkFaceEntity, tunnelEntity, this.MainForm);
            if (DialogResult.OK == d.ShowDialog())
            {
                bindFpTunnelJJInfo();
                FarPointOperate.farPointFocusSetChange(fpTunnelJJInfo, _tmpRowIndex);
            }
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
                int searchCount = rowsCount;
                int rowDetailStartIndex = 4;
                bool bResult = false;
                int delCount = 0;
                for (int i = 0; i < rowsCount; i++)
                {
                    //遍历“选择”是否选中
                    if (cells[rowDetailStartIndex + i, 0].Value != null &&
                        (bool)cells[rowDetailStartIndex + i, 0].Value == true)
                    {
                        int tunnelId = Convert.ToInt32(cells[rowDetailStartIndex + i, COLUMN_INDEX_TUNNEL_ID].Text);
                        //掘进ID
                        var tunnel = Tunnel.Find(tunnelId);
                        tunnel.TunnelType = TunnelTypeEnum.OTHER;
                        tunnel.Save();
                        delCount++;
                    }
                }
                //删除成功
                if (bResult)
                {
                    //TODO
                }
                bindFpTunnelJJInfo();
                FarPointOperate.farPointFocusSetDel(fpTunnelJJInfo, _tmpRowIndex);
            }
            else
            {
                return;
            }
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            this.Close();
        }

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
                        cells[rowDetailStartIndex + i, 0].Value = ((CheckBox)sender).Checked;
                        checkCount = ds.Tables[0].Rows.Count;
                    }
                    //checkbox未选中
                    else
                    {
                        _htSelIdxs.Remove(rowDetailStartIndex + i);
                        cells[rowDetailStartIndex + i, 0].Value = ((CheckBox)sender).Checked;
                        checkCount = 0;
                    }
                }
            }
            setButtenEnable();
        }

        private void tsBtnRefresh_Click(object sender, EventArgs e)
        {
            bindFpTunnelJJInfo();
        }

        private void tsBtnExport_Click(object sender, EventArgs e)
        {
            if (FileExport.fileExport(fpTunnelJJInfo, true))
            {
                Alert.alert(Const.EXPORT_SUCCESS_MSG);
            }
        }

        private void tsBtnPrint_Click(object sender, EventArgs e)
        {
            FilePrint.CommonPrint(fpTunnelJJInfo, 0);
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            // 关闭窗口
            this.Close();
        }

        #region Farpoint自动过滤功能
        private void farpointFilter1_OnCheckFilterChanged(object sender, EventArgs arg)
        {
            CheckBox chk = (CheckBox)sender;
            //当Checkbox选中时，筛选过程中则将不符合条件的数据隐藏
            if (chk.Checked == true)
            {
                //禁用选择颜色相关控件
                farpointFilter1.EnableChooseColorCtrls(false);
                //设置自动隐藏过滤条件
                FarpointDefaultPropertiesSetter.SetFpFilterHideProperties(this.fpTunnelJJInfo, _filterColunmIdxs);

            }
            else//未选中时，根据用户自定义的颜色进行分类显示
            {
                //启用选择颜色相关控件
                farpointFilter1.EnableChooseColorCtrls(true);
                //设置自定义过滤条件
                FarpointDefaultPropertiesSetter.SetFpCustomFilterProperties(this.fpTunnelJJInfo, farpointFilter1.GetSelectedFitColor(), farpointFilter1.GetSelectedNotFitColor(), _filterColunmIdxs);
            }
        }

        private void farpointFilter1_OnClickClearFilterBtn(object sender, EventArgs arg)
        {
            //清空过滤条件
            this.fpTunnelJJInfo.ActiveSheet.RowFilter.ResetFilter();
        }

        private void farpointFilter1_OnClickFitColorBtnOK(object sender, EventArgs arg)
        {
            //根据新的颜色值设置自动隐藏过滤条件
            FarpointDefaultPropertiesSetter.SetFpCustomFilterProperties(this.fpTunnelJJInfo, farpointFilter1.GetSelectedFitColor(), farpointFilter1.GetSelectedNotFitColor(), _filterColunmIdxs);
        }

        private void farpointFilter1_OnClickNotFitColorBtnOK(object sender, EventArgs arg)
        {
            //根据新的颜色值设置自动隐藏过滤条件
            FarpointDefaultPropertiesSetter.SetFpCustomFilterProperties(this.fpTunnelJJInfo, farpointFilter1.GetSelectedFitColor(), farpointFilter1.GetSelectedNotFitColor(), _filterColunmIdxs);
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
            ILayer pLayer = GIS.Common.DataEditCommon.GetLayerByName(GIS.Common.DataEditCommon.g_pMap, GIS.LayerNames.LAYER_ALIAS_MR_STOPING_AREA);
            if (pLayer == null)
            {
                MessageBox.Show("未发现采掘区图层！");
                return;
            }
            IFeatureLayer pFeatureLayer = (IFeatureLayer)pLayer;
            string str = "";
            for (int i = 0; i < iSelIdxsArr.Length; i++)
            {
                bid = this.fpTunnelJJInfo.Sheets[0].Cells[iSelIdxsArr[i], _BIDIndex].Text.Trim();
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
