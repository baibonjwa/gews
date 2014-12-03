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
using GIS.Common;
using LibCommon;
using LibCommonControl;
using LibEntity;
using LibBusiness;
using GIS.HdProc;

namespace _3.GeologyMeasure
{
    public partial class TunnelHChuanManagement : BaseForm
    {
        //****************变量声明***************
        private int _iRecordCount = 0;
        int _rowsCount = 0;      //数据行数
        int _checkCount = 0;     //选择行数
        int _rowDetailStartIndex = 4;
        int _tmpRowIndex = 0;
        //需要过滤的列索引
        private int[] _filterColunmIdxs = null;
        TunnelHChuanEntity tunnelHChuanEntity = new TunnelHChuanEntity();
        DataSet _ds = new DataSet();
        int tmpInt = 0;
        private int _BIDIndex = 18;
        /** 保存所有用户选中的行的索引 **/
        private Hashtable _htSelIdxs = new Hashtable();
        //****************************************

        /// <summary>
        /// 构造方法
        /// </summary>
        public TunnelHChuanManagement(MainFrm mainFrm)
        {
            this.MainForm = mainFrm;
            InitializeComponent();

            //分页委托
            dataPager1.FrmChild_EventHandler += FrmParent_EventHandler;

            //窗体属性设置
            LibCommon.FormDefaultPropertiesSetter.SetManagementFormDefaultProperties(this, Const_GM.TUNNEL_HCHUAN_MANAGEMENT);

            //Farpoint属性设置
            LibCommon.FarpointDefaultPropertiesSetter.SetFpDefaultProperties(fpDayReportHChuan, LibCommon.Const_GM.TUNNEL_HCHUAN_FARPOINT_TITLE, _rowDetailStartIndex);

            _filterColunmIdxs = new int[]
            {
                1,
                2,
                3,
                4,
                5,
                6,
                7,
                8,
                9,
                11,
                12,
                13
            };
            //禁用选择颜色相关控件
            farpointFilter1.EnableChooseColorCtrls(false);
            //设置自动隐藏过滤条件
            FarpointDefaultPropertiesSetter.SetFpFilterHideProperties(this.fpDayReportHChuan, _filterColunmIdxs);
        }

        /// <summary>
        /// 初始化
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TunnelHChuanManagement_Load(object sender, EventArgs e)
        {
            this.bindFpTunnelHChuanInfo();
        }

        /// <summary>
        /// 分页委托
        /// </summary>
        /// <param name="sender"></param>
        private void FrmParent_EventHandler(object sender)
        {
            bindFpTunnelHChuanInfo();
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
        /// 为变量StopLineEntity赋值
        /// </summary>
        private void setTunnelHCEntityValue()
        {
            int searchCount = _rowsCount;
            int rowDetailStartIndex = 4;
            for (int i = 0; i < _rowsCount; i++)
            {

                if (fpDayReportHChuan.Sheets[0].Cells[rowDetailStartIndex + i, 0].Value != null && (bool)fpDayReportHChuan.Sheets[0].Cells[rowDetailStartIndex + i, 0].Value == true)
                {
                    _tmpRowIndex = rowDetailStartIndex + i;
                    int index = 14;
                    int tmp = 0;
                    //主键
                    tunnelHChuanEntity.ID = Convert.ToInt32(_ds.Tables[0].Rows[i][TunnelHChuanDbConstNames.ID]);
                    var HChuanName = _ds.Tables[0].Rows[i][TunnelHChuanDbConstNames.NAME_HCHUAN];
                    tunnelHChuanEntity.NameHChuan = HChuanName == null ? "" : HChuanName.ToString();
                    tunnelHChuanEntity.Width = Convert.ToDouble(_ds.Tables[0].Rows[i][TunnelHChuanDbConstNames.TUNNEL_WIDTH]);
                    //关联巷道1
                    if (int.TryParse(_ds.Tables[0].Rows[i][TunnelHChuanDbConstNames.TUNNEL_ID1].ToString(), out tmp))
                    {
                        tunnelHChuanEntity.TunnelID1 = tmp;
                        tmp = 0;
                    }
                    //关联巷道2
                    if (int.TryParse(_ds.Tables[0].Rows[i][TunnelHChuanDbConstNames.TUNNEL_ID2].ToString(), out tmp))
                    {
                        tunnelHChuanEntity.TunnelID2 = tmp;
                        tmp = 0;
                    }
                    //x1
                    tunnelHChuanEntity.X_1 = Convert.ToDouble(_ds.Tables[0].Rows[i][TunnelHChuanDbConstNames.TUNNEL_X1].ToString());
                    //y1
                    tunnelHChuanEntity.Y_1 = Convert.ToDouble(_ds.Tables[0].Rows[i][TunnelHChuanDbConstNames.TUNNEL_Y1].ToString());
                    //z1
                    tunnelHChuanEntity.Z_1 = Convert.ToDouble(_ds.Tables[0].Rows[i][TunnelHChuanDbConstNames.TUNNEL_Z1].ToString());
                    //x2
                    tunnelHChuanEntity.X_2 = Convert.ToDouble(_ds.Tables[0].Rows[i][TunnelHChuanDbConstNames.TUNNEL_X2].ToString());
                    //y2
                    tunnelHChuanEntity.Y_2 = Convert.ToDouble(_ds.Tables[0].Rows[i][TunnelHChuanDbConstNames.TUNNEL_Y2].ToString());
                    //z2
                    tunnelHChuanEntity.Z_2 = Convert.ToDouble(_ds.Tables[0].Rows[i][TunnelHChuanDbConstNames.TUNNEL_Z2].ToString());
                    //azimuth
                    tunnelHChuanEntity.Azimuth = Convert.ToDouble(_ds.Tables[0].Rows[i][TunnelHChuanDbConstNames.TUNNEL_AZIMUTH].ToString());

                    //队别
                    if (int.TryParse(_ds.Tables[0].Rows[i][TunnelHChuanDbConstNames.TEAM_NAME_ID].ToString(), out tmp))
                    {
                        tunnelHChuanEntity.TeamNameID = tmp;
                        tmp = 0;
                    }
                    DateTime _dateTime;
                    if (DateTime.TryParse(_ds.Tables[0].Rows[i][TunnelHChuanDbConstNames.START_DATE].ToString(), out _dateTime))
                    {
                        //开工日期
                        tunnelHChuanEntity.StartDate = _dateTime;
                    }

                    int _finish;

                    if (int.TryParse(_ds.Tables[0].Rows[i][TunnelHChuanDbConstNames.IS_FINISH].ToString(), out _finish))
                    {
                        //是否回采完毕
                        tunnelHChuanEntity.IsFinish = _finish;
                    }

                    ++index;
                    //停工日期
                    if (_finish > 0)
                    {
                        tunnelHChuanEntity.StopDate = Convert.ToDateTime(_ds.Tables[0].Rows[i][TunnelHChuanDbConstNames.STOP_DATE].ToString());
                    }
                    //工作制式
                    tunnelHChuanEntity.WorkStyle = _ds.Tables[0].Rows[i][TunnelHChuanDbConstNames.WORK_STYLE].ToString();
                    //班次
                    tunnelHChuanEntity.WorkTime = _ds.Tables[0].Rows[i][TunnelHChuanDbConstNames.WORK_TIME].ToString();
                    //状态
                    tunnelHChuanEntity.State = _ds.Tables[0].Rows[i][TunnelHChuanDbConstNames.TUNNEL_STATE].ToString();
                }
            }
        }
        private void tsBtnPrint_Click(object sender, EventArgs e)
        {
            //打印
            FilePrint.CommonPrint(fpDayReportHChuan, 0);
        }

        private void tsBtnExport_Click(object sender, EventArgs e)
        {
            if (FileExport.fileExport(fpDayReportHChuan, true))
            {
                Alert.alert(Const.EXPORT_SUCCESS_MSG);
            }
            return;
        }

        private void tsBtnAdd_Click(object sender, EventArgs e)
        {
            TunnelHChuanEntering tunnelHCForm = new TunnelHChuanEntering(this.MainForm,this);
            tunnelHCForm.Show(this);
        }
        public void refreshAdd()
        {
            bindFpTunnelHChuanInfo();
            this.dataPager1.btnLastPage_Click(null, null);
            FarPointOperate.farPointFocusSetAdd(fpDayReportHChuan, _rowDetailStartIndex, _rowsCount);
        }
        /// <summary>
        /// farpoint数据绑定
        /// </summary>
        private void bindFpTunnelHChuanInfo()
        {
            // 清空HashTabl（必须实装）
            _htSelIdxs.Clear();

            //清空Farpoint
            FarPointOperate.farPointClear(fpDayReportHChuan, _rowDetailStartIndex, _rowsCount);

            _checkCount = 0;

            chkSelAll.Checked = false;
            // ※分页必须
            _iRecordCount = TunnelHChuanBLL.selectTunnelHChuan().Tables[0].Rows.Count;

            // ※分页必须
            dataPager1.PageControlInit(_iRecordCount);

            int iStartIndex = dataPager1.getStartIndex();
            int iEndIndex = dataPager1.getEndIndex();

            _ds = TunnelHChuanBLL.selectTunnelHChuan(iStartIndex, iEndIndex);

            _rowsCount = _ds.Tables[0].Rows.Count;

            //重绘Farpoint
            FarPointOperate.farPointReAdd(fpDayReportHChuan, _rowDetailStartIndex, _rowsCount);

            if (_rowsCount > 0)
            {
                FarPoint.Win.Spread.CellType.CheckBoxCellType ckbxcell = new FarPoint.Win.Spread.CellType.CheckBoxCellType();
                //取消三选
                ckbxcell.ThreeState = false;

                for (int i = 0; i < _ds.Tables[0].Rows.Count; i++)
                {
                    int index = 0;
                    this.fpDayReportHChuan.Sheets[0].Cells[_rowDetailStartIndex + i, index].CellType = ckbxcell;
                    //主运顺槽
                    int tunnelID1 = Convert.ToInt32(_ds.Tables[0].Rows[i][TunnelHChuanDbConstNames.TUNNEL_ID1]);
                    //辅运顺槽
                    int tunnelID2 = Convert.ToInt32(_ds.Tables[0].Rows[i][TunnelHChuanDbConstNames.TUNNEL_ID2]);

                    int tmpTunnelID = (tunnelID1 == 0 ? (tunnelID2 == 0 ? 0 : tunnelID2) : tunnelID1);

                    //矿井名称
                    this.fpDayReportHChuan.Sheets[0].Cells[_rowDetailStartIndex + i, ++index].Text = _ds.Tables[0].Rows[i][TunnelHChuanDbConstNames.NAME_HCHUAN].ToString(); ;
                    //关联巷道1
                    this.fpDayReportHChuan.Sheets[0].Cells[_rowDetailStartIndex + i, ++index].Text = _ds.Tables[0].Rows[i][TunnelHChuanDbConstNames.TUNNEL_ID1].ToString(); ;
                    //关联巷道2
                    this.fpDayReportHChuan.Sheets[0].Cells[_rowDetailStartIndex + i, ++index].Text = _ds.Tables[0].Rows[i][TunnelHChuanDbConstNames.TUNNEL_ID2].ToString(); ;

                    //x1
                    this.fpDayReportHChuan.Sheets[0].Cells[_rowDetailStartIndex + i, ++index].Text = _ds.Tables[0].Rows[i][TunnelHChuanDbConstNames.TUNNEL_X1].ToString();
                    //y1
                    this.fpDayReportHChuan.Sheets[0].Cells[_rowDetailStartIndex + i, ++index].Text = _ds.Tables[0].Rows[i][TunnelHChuanDbConstNames.TUNNEL_Y1].ToString();
                    //z1
                    this.fpDayReportHChuan.Sheets[0].Cells[_rowDetailStartIndex + i, ++index].Text = _ds.Tables[0].Rows[i][TunnelHChuanDbConstNames.TUNNEL_Z1].ToString();
                    //x2
                    this.fpDayReportHChuan.Sheets[0].Cells[_rowDetailStartIndex + i, ++index].Text = _ds.Tables[0].Rows[i][TunnelHChuanDbConstNames.TUNNEL_X2].ToString();
                    //y2
                    this.fpDayReportHChuan.Sheets[0].Cells[_rowDetailStartIndex + i, ++index].Text = _ds.Tables[0].Rows[i][TunnelHChuanDbConstNames.TUNNEL_Y2].ToString();
                    //z2
                    this.fpDayReportHChuan.Sheets[0].Cells[_rowDetailStartIndex + i, ++index].Text = _ds.Tables[0].Rows[i][TunnelHChuanDbConstNames.TUNNEL_Z2].ToString();
                    //azimuth
                    this.fpDayReportHChuan.Sheets[0].Cells[_rowDetailStartIndex + i, ++index].Text = _ds.Tables[0].Rows[i][TunnelHChuanDbConstNames.TUNNEL_AZIMUTH].ToString();
                    //队别
                    if (int.TryParse(_ds.Tables[0].Rows[i][TunnelHChuanDbConstNames.TEAM_NAME_ID].ToString(), out tmpInt))
                    {
                        this.fpDayReportHChuan.Sheets[0].Cells[_rowDetailStartIndex + i, ++index].Text = TeamBLL.selectTeamInfoByID(tmpInt).TeamName;
                        tmpInt = 0;
                    }
                    //开工日期
                    this.fpDayReportHChuan.Sheets[0].Cells[_rowDetailStartIndex + i, ++index].Text = _ds.Tables[0].Rows[i][TunnelHChuanDbConstNames.START_DATE].ToString().Substring(0, _ds.Tables[0].Rows[i][TunnelHChuanDbConstNames.START_DATE].ToString().IndexOf(' '));
                    //是否回采完毕
                    this.fpDayReportHChuan.Sheets[0].Cells[_rowDetailStartIndex + i, ++index].Text = Convert.ToInt32(_ds.Tables[0].Rows[i][TunnelHChuanDbConstNames.IS_FINISH].ToString()) == 1 ? Const.MSG_YES : Const.MSG_NO;
                    //停工日期
                    if (Convert.ToInt32(_ds.Tables[0].Rows[i][TunnelHChuanDbConstNames.IS_FINISH].ToString()) == 1)
                    {
                        this.fpDayReportHChuan.Sheets[0].Cells[_rowDetailStartIndex + i, ++index].Text = _ds.Tables[0].Rows[i][TunnelHChuanDbConstNames.STOP_DATE].ToString().Substring(0, _ds.Tables[0].Rows[i][TunnelHChuanDbConstNames.STOP_DATE].ToString().IndexOf(' '));
                    }
                    else
                    {
                        ++index;
                    }

                    //工作制式
                    this.fpDayReportHChuan.Sheets[0].Cells[_rowDetailStartIndex + i, ++index].Text = _ds.Tables[0].Rows[i][TunnelHChuanDbConstNames.WORK_STYLE].ToString();
                    //班次
                    this.fpDayReportHChuan.Sheets[0].Cells[_rowDetailStartIndex + i, ++index].Text = _ds.Tables[0].Rows[i][TunnelHChuanDbConstNames.WORK_TIME].ToString();
                    //状态
                    this.fpDayReportHChuan.Sheets[0].Cells[_rowDetailStartIndex + i, ++index].Text = _ds.Tables[0].Rows[i][TunnelHChuanDbConstNames.TUNNEL_STATE].ToString();
                    //bid
                    this.fpDayReportHChuan.Sheets[0].Cells[_rowDetailStartIndex + i, ++index].Text = _ds.Tables[0].Rows[i][TunnelHChuanDbConstNames.ID].ToString();
                    fpDayReportHChuan.Sheets[0].Columns[_BIDIndex].Visible = false;
                }
            }
            //设置按钮可操作性
            setButtenEnable();
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
                int searchCount = _rowsCount;
                bool bResult = false;
                for (int i = 0; i < _rowsCount; i++)
                {
                    _tmpRowIndex = this.fpDayReportHChuan.Sheets[0].ActiveRowIndex;
                    //遍历“选择”是否选中
                    if (fpDayReportHChuan.Sheets[0].Cells[_rowDetailStartIndex + i, 0].Value != null && (bool)fpDayReportHChuan.Sheets[0].Cells[_rowDetailStartIndex + i, 0].Value == true)
                    {
                        //主键
                        tunnelHChuanEntity.ID = Convert.ToInt32(_ds.Tables[0].Rows[i][TunnelHChuanDbConstNames.ID]);
                        int tmp = 0;
                        //主运顺槽
                        if (int.TryParse(_ds.Tables[0].Rows[i][TunnelHChuanDbConstNames.TUNNEL_ID1].ToString(), out tmp))
                        {
                            tunnelHChuanEntity.TunnelID1 = tmp;
                            tmp = 0;
                        }
                        //辅运顺槽
                        if (int.TryParse(_ds.Tables[0].Rows[i][TunnelHChuanDbConstNames.TUNNEL_ID2].ToString(), out tmp))
                        {
                            tunnelHChuanEntity.TunnelID2 = tmp;
                            tmp = 0;
                        }

                        //重设巷道类型
                        TunnelInfoBLL.clearTunnelType(tunnelHChuanEntity.TunnelID1);
                        TunnelInfoBLL.clearTunnelType(tunnelHChuanEntity.TunnelID2);

                        //删除回采巷道信息
                        bResult = TunnelHChuanBLL.deleteTunnelHChuan(tunnelHChuanEntity);
                    }
                }
                if (bResult)
                {
                    //TODO:删除后事件
                    //将图层中对应的信息删除
                    DelHChuanjc(tunnelHChuanEntity.TunnelID1, tunnelHChuanEntity.TunnelID2);
                    //删除工作面中对应的回采信息
                    /////Mark

                }
                bindFpTunnelHChuanInfo();
                FarPointOperate.farPointFocusSetDel(this.fpDayReportHChuan, _tmpRowIndex);
            }
            else
            {
                return;
            }
        }

        /// <summary>
        /// 回采删除信息
        /// </summary>
        /// <param name="hd1"></param>
        /// <param name="hd2"></param>
        /// <param name="qy"></param>
        private void DelHChuanjc(int hd1, int hd2)
        {
            // 获取已选择明细行的索引
            int[] iSelIdxsArr = GetSelIdxs();
            string bid = "";
            //ILayer pLayer = GIS.Common.DataEditCommon.GetLayerByName(GIS.Common.DataEditCommon.g_pMap, GIS.LayerNames.DEFALUT_HENGCHUAN);
            //if (pLayer == null)
            //{
            //    MessageBox.Show("未发现横川图层！");
            //    return;
            //}
            //IFeatureLayer pFeatureLayer = (IFeatureLayer)pLayer;
            string str = "";
            for (int i = 0; i < iSelIdxsArr.Length; i++)
            {
                bid = this.fpDayReportHChuan.Sheets[0].Cells[iSelIdxsArr[i], _BIDIndex].Text.Trim();
                if (bid != "")
                {
                    if (i == 0)
                        str = "bid='" + bid + "'";
                    else
                        str += " or bid='" + bid + "'";
                }
            }
            DataEditCommon.DeleteFeatureByWhereClause(Global.hdfdfulllyr, str);
            DataEditCommon.DeleteFeatureByWhereClause(Global.hdfdlyr, str);
            //if (DataEditCommon.DeleteFeatureByWhereClause(pFeatureLayer, str))
            //{
                DataEditCommon.g_pMyMapCtrl.ActiveView.PartialRefresh(esriViewDrawPhase.esriViewBackground,null,null);
            //}
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
                for (int i = 0; i < _rowsCount; i++)
                {
                    //checkbox选中
                    if (chkSelAll.Checked)
                    {
                        if (!_htSelIdxs.Contains(_rowDetailStartIndex + i))
                        {
                            _htSelIdxs.Add(_rowDetailStartIndex + i, true);
                        }
                        this.fpDayReportHChuan.Sheets[0].Cells[_rowDetailStartIndex + i, 0].Value = ((CheckBox)sender).Checked;
                        _checkCount = _ds.Tables[0].Rows.Count;
                    }
                    //checkbox未选中
                    else
                    {
                        _htSelIdxs.Remove(_rowDetailStartIndex + i);
                        this.fpDayReportHChuan.Sheets[0].Cells[_rowDetailStartIndex + i, 0].Value = ((CheckBox)sender).Checked;
                        _checkCount = 0;
                    }
                }
            }
            //设置按钮可操作性
            setButtenEnable();
        }
        /// <summary>
        /// 刷新
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>       
        private void tsBtnRefresh_Click(object sender, EventArgs e)
        {
            //绑定数据
            this.bindFpTunnelHChuanInfo();
        }

        private void tsBtnExit_Click(object sender, EventArgs e)
        {
            //关闭窗体
            this.Close();
        }

        private void tsBtnModify_Click(object sender, EventArgs e)
        {
            setTunnelHCEntityValue();

            TunnelHChuanEntering tunnelHChuanForm = new TunnelHChuanEntering(tunnelHChuanEntity, this.MainForm,this);
            tunnelHChuanForm.Show(this);
        }
        public void refreshUpdate()
        {
            bindFpTunnelHChuanInfo();
            FarPointOperate.farPointFocusSetChange(this.fpDayReportHChuan, _tmpRowIndex);
        }
        private void btnOK_Click(object sender, EventArgs e)
        {
            //关闭窗体
            this.Close();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            //关闭窗体
            this.Close();
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
            //ILayer pLayer = GIS.Common.DataEditCommon.GetLayerByName(GIS.Common.DataEditCommon.g_pMap, GIS.LayerNames.DEFALUT_HENGCHUAN);
            //if (pLayer == null)
            //{
            //    MessageBox.Show("未发现横川图层！");
            //    return;
            //}
            //IFeatureLayer pFeatureLayer = (IFeatureLayer)pLayer;
            string str = "";
            for (int i = 0; i < iSelIdxsArr.Length; i++)
            {
                bid = this.fpDayReportHChuan.Sheets[0].Cells[iSelIdxsArr[i], _BIDIndex].Text.Trim();
                if (bid != "")
                {
                    if (i == 0)
                        str = "bid='" + bid + "'";
                    else
                        str += " or bid='" + bid + "'";
                }
            }
            List<ESRI.ArcGIS.Geodatabase.IFeature> list = GIS.MyMapHelp.FindFeatureListByWhereClause(Global.hdfdfulllyr, str);
            if (list.Count > 0)
            {
                GIS.MyMapHelp.Jump(GIS.MyMapHelp.GetGeoFromFeature(list));
                GIS.Common.DataEditCommon.g_pMap.ClearSelection();
                for (int i = 0; i < list.Count; i++)
                {
                    GIS.Common.DataEditCommon.g_pMap.SelectFeature(Global.hdfdfulllyr, list[i]);
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
