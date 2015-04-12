using System;
using System.Collections;
using System.Data;
using System.Windows.Forms;
using ESRI.ArcGIS.Carto;
using FarPoint.Win;
using FarPoint.Win.Spread;
using FarPoint.Win.Spread.CellType;
using GIS;
using GIS.Common;
using GIS.HdProc;
using LibBusiness;
using LibCommon;
using LibEntity;

namespace _3.GeologyMeasure
{
    public partial class TunnelHChuanManagement : Form
    {
        private int _checkCount; //选择行数
        private DataSet _ds = new DataSet();
        //****************变量声明***************
        private int _iRecordCount;
        private int _rowsCount; //数据行数
        private int _tmpRowIndex;
        private int tmpInt;
        private readonly int _BIDIndex = 18;
        //需要过滤的列索引
        private readonly int[] _filterColunmIdxs;
        /** 保存所有用户选中的行的索引 **/
        private readonly Hashtable _htSelIdxs = new Hashtable();
        private readonly int _rowDetailStartIndex = 4;
        private readonly TunnelHChuan tunnelHChuanEntity = new TunnelHChuan();
        //****************************************

        /// <summary>
        ///     构造方法
        /// </summary>
        public TunnelHChuanManagement()
        {
            InitializeComponent();

            //分页委托
            dataPager1.FrmChild_EventHandler += FrmParent_EventHandler;

            //窗体属性设置
            FormDefaultPropertiesSetter.SetManagementFormDefaultProperties(this, Const_GM.TUNNEL_HCHUAN_MANAGEMENT);

            //Farpoint属性设置
            FarpointDefaultPropertiesSetter.SetFpDefaultProperties(fpDayReportHChuan,
                Const_GM.TUNNEL_HCHUAN_FARPOINT_TITLE, _rowDetailStartIndex);

            _filterColunmIdxs = new[]
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
            FarpointDefaultPropertiesSetter.SetFpFilterHideProperties(fpDayReportHChuan, _filterColunmIdxs);
        }

        /// <summary>
        ///     初始化
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TunnelHChuanManagement_Load(object sender, EventArgs e)
        {
            bindFpTunnelHChuanInfo();
        }

        /// <summary>
        ///     分页委托
        /// </summary>
        /// <param name="sender"></param>
        private void FrmParent_EventHandler(object sender)
        {
            bindFpTunnelHChuanInfo();
        }

        /// <summary>
        ///     farpoint中checkbox选中对全选反选的影响
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void fpStopLineInfo_ButtonClicked(object sender, EditorNotifyEventArgs e)
        {
            if (e.EditingControl is FpCheckBox)
            {
                var fpChk = (FpCheckBox) e.EditingControl;
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
                            chkSelAll.Checked = true;
                        }
                    }

                    _checkCount++;
                }
                else
                {
                    // 移除索引号
                    _htSelIdxs.Remove(e.Row);

                    // 全选/全不选checkbox设为未选中
                    chkSelAll.Checked = false;

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
        ///     设置按钮可操作性
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
        ///     为变量StopLineEntity赋值
        /// </summary>
        private void setTunnelHCEntityValue()
        {
            var searchCount = _rowsCount;
            var rowDetailStartIndex = 4;
            for (var i = 0; i < _rowsCount; i++)
            {
                if (fpDayReportHChuan.Sheets[0].Cells[rowDetailStartIndex + i, 0].Value != null &&
                    (bool) fpDayReportHChuan.Sheets[0].Cells[rowDetailStartIndex + i, 0].Value)
                {
                    _tmpRowIndex = rowDetailStartIndex + i;
                    var index = 14;
                    var tmp = 0;
                    //主键
                    tunnelHChuanEntity.Id = Convert.ToInt32(_ds.Tables[0].Rows[i][TunnelHChuanDbConstNames.ID]);
                    var HChuanName = _ds.Tables[0].Rows[i][TunnelHChuanDbConstNames.NAME_HCHUAN];
                    tunnelHChuanEntity.NameHChuan = HChuanName == null ? "" : HChuanName.ToString();
                    tunnelHChuanEntity.Width =
                        Convert.ToDouble(_ds.Tables[0].Rows[i][TunnelHChuanDbConstNames.TUNNEL_WIDTH]);
                    //关联巷道1
                    if (int.TryParse(_ds.Tables[0].Rows[i][TunnelHChuanDbConstNames.TUNNEL_ID1].ToString(), out tmp))
                    {
                        tunnelHChuanEntity.TunnelId1 = tmp;
                        tmp = 0;
                    }
                    //关联巷道2
                    if (int.TryParse(_ds.Tables[0].Rows[i][TunnelHChuanDbConstNames.TUNNEL_ID2].ToString(), out tmp))
                    {
                        tunnelHChuanEntity.TunnelId2 = tmp;
                        tmp = 0;
                    }
                    //x1
                    tunnelHChuanEntity.X1 =
                        Convert.ToDouble(_ds.Tables[0].Rows[i][TunnelHChuanDbConstNames.TUNNEL_X1].ToString());
                    //y1
                    tunnelHChuanEntity.Y1 =
                        Convert.ToDouble(_ds.Tables[0].Rows[i][TunnelHChuanDbConstNames.TUNNEL_Y1].ToString());
                    //z1
                    tunnelHChuanEntity.Z1 =
                        Convert.ToDouble(_ds.Tables[0].Rows[i][TunnelHChuanDbConstNames.TUNNEL_Z1].ToString());
                    //x2
                    tunnelHChuanEntity.X2 =
                        Convert.ToDouble(_ds.Tables[0].Rows[i][TunnelHChuanDbConstNames.TUNNEL_X2].ToString());
                    //y2
                    tunnelHChuanEntity.Y2 =
                        Convert.ToDouble(_ds.Tables[0].Rows[i][TunnelHChuanDbConstNames.TUNNEL_Y2].ToString());
                    //z2
                    tunnelHChuanEntity.Z2 =
                        Convert.ToDouble(_ds.Tables[0].Rows[i][TunnelHChuanDbConstNames.TUNNEL_Z2].ToString());
                    //azimuth
                    tunnelHChuanEntity.Azimuth =
                        Convert.ToDouble(_ds.Tables[0].Rows[i][TunnelHChuanDbConstNames.TUNNEL_AZIMUTH].ToString());

                    //队别
                    if (int.TryParse(_ds.Tables[0].Rows[i][TunnelHChuanDbConstNames.TEAM_NAME_ID].ToString(), out tmp))
                    {
                        tunnelHChuanEntity.Team.TeamId = tmp;
                        tmp = 0;
                    }
                    DateTime _dateTime;
                    if (DateTime.TryParse(_ds.Tables[0].Rows[i][TunnelHChuanDbConstNames.START_DATE].ToString(),
                        out _dateTime))
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
                        tunnelHChuanEntity.StopDate =
                            Convert.ToDateTime(_ds.Tables[0].Rows[i][TunnelHChuanDbConstNames.STOP_DATE].ToString());
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
        }

        private void tsBtnAdd_Click(object sender, EventArgs e)
        {
            var tunnelHCForm = new TunnelHChuanEntering(this);
            tunnelHCForm.Show(this);
        }

        public void refreshAdd()
        {
            bindFpTunnelHChuanInfo();
            dataPager1.btnLastPage_Click(null, null);
            FarPointOperate.farPointFocusSetAdd(fpDayReportHChuan, _rowDetailStartIndex, _rowsCount);
        }

        /// <summary>
        ///     farpoint数据绑定
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

            var iStartIndex = dataPager1.getStartIndex();
            var iEndIndex = dataPager1.getEndIndex();

            _ds = TunnelHChuanBLL.selectTunnelHChuan(iStartIndex, iEndIndex);

            _rowsCount = _ds.Tables[0].Rows.Count;

            //重绘Farpoint
            FarPointOperate.farPointReAdd(fpDayReportHChuan, _rowDetailStartIndex, _rowsCount);

            if (_rowsCount > 0)
            {
                var ckbxcell = new CheckBoxCellType();
                //取消三选
                ckbxcell.ThreeState = false;

                for (var i = 0; i < _ds.Tables[0].Rows.Count; i++)
                {
                    var index = 0;
                    fpDayReportHChuan.Sheets[0].Cells[_rowDetailStartIndex + i, index].CellType = ckbxcell;
                    //主运顺槽
                    var tunnelID1 = Convert.ToInt32(_ds.Tables[0].Rows[i][TunnelHChuanDbConstNames.TUNNEL_ID1]);
                    //辅运顺槽
                    var tunnelID2 = Convert.ToInt32(_ds.Tables[0].Rows[i][TunnelHChuanDbConstNames.TUNNEL_ID2]);

                    var tmpTunnelID = (tunnelID1 == 0 ? (tunnelID2 == 0 ? 0 : tunnelID2) : tunnelID1);

                    //矿井名称
                    fpDayReportHChuan.Sheets[0].Cells[_rowDetailStartIndex + i, ++index].Text =
                        _ds.Tables[0].Rows[i][TunnelHChuanDbConstNames.NAME_HCHUAN].ToString();
                    ;
                    //关联巷道1
                    fpDayReportHChuan.Sheets[0].Cells[_rowDetailStartIndex + i, ++index].Text =
                        _ds.Tables[0].Rows[i][TunnelHChuanDbConstNames.TUNNEL_ID1].ToString();
                    ;
                    //关联巷道2
                    fpDayReportHChuan.Sheets[0].Cells[_rowDetailStartIndex + i, ++index].Text =
                        _ds.Tables[0].Rows[i][TunnelHChuanDbConstNames.TUNNEL_ID2].ToString();
                    ;

                    //x1
                    fpDayReportHChuan.Sheets[0].Cells[_rowDetailStartIndex + i, ++index].Text =
                        _ds.Tables[0].Rows[i][TunnelHChuanDbConstNames.TUNNEL_X1].ToString();
                    //y1
                    fpDayReportHChuan.Sheets[0].Cells[_rowDetailStartIndex + i, ++index].Text =
                        _ds.Tables[0].Rows[i][TunnelHChuanDbConstNames.TUNNEL_Y1].ToString();
                    //z1
                    fpDayReportHChuan.Sheets[0].Cells[_rowDetailStartIndex + i, ++index].Text =
                        _ds.Tables[0].Rows[i][TunnelHChuanDbConstNames.TUNNEL_Z1].ToString();
                    //x2
                    fpDayReportHChuan.Sheets[0].Cells[_rowDetailStartIndex + i, ++index].Text =
                        _ds.Tables[0].Rows[i][TunnelHChuanDbConstNames.TUNNEL_X2].ToString();
                    //y2
                    fpDayReportHChuan.Sheets[0].Cells[_rowDetailStartIndex + i, ++index].Text =
                        _ds.Tables[0].Rows[i][TunnelHChuanDbConstNames.TUNNEL_Y2].ToString();
                    //z2
                    fpDayReportHChuan.Sheets[0].Cells[_rowDetailStartIndex + i, ++index].Text =
                        _ds.Tables[0].Rows[i][TunnelHChuanDbConstNames.TUNNEL_Z2].ToString();
                    //azimuth
                    fpDayReportHChuan.Sheets[0].Cells[_rowDetailStartIndex + i, ++index].Text =
                        _ds.Tables[0].Rows[i][TunnelHChuanDbConstNames.TUNNEL_AZIMUTH].ToString();
                    //队别
                    if (int.TryParse(_ds.Tables[0].Rows[i][TunnelHChuanDbConstNames.TEAM_NAME_ID].ToString(), out tmpInt))
                    {
                        fpDayReportHChuan.Sheets[0].Cells[_rowDetailStartIndex + i, ++index].Text =
                            Team.Find(tmpInt).TeamName;
                        tmpInt = 0;
                    }
                    //开工日期
                    fpDayReportHChuan.Sheets[0].Cells[_rowDetailStartIndex + i, ++index].Text =
                        _ds.Tables[0].Rows[i][TunnelHChuanDbConstNames.START_DATE].ToString()
                            .Substring(0,
                                _ds.Tables[0].Rows[i][TunnelHChuanDbConstNames.START_DATE].ToString().IndexOf(' '));
                    //是否回采完毕
                    fpDayReportHChuan.Sheets[0].Cells[_rowDetailStartIndex + i, ++index].Text =
                        Convert.ToInt32(_ds.Tables[0].Rows[i][TunnelHChuanDbConstNames.IS_FINISH].ToString()) == 1
                            ? Const.MSG_YES
                            : Const.MSG_NO;
                    //停工日期
                    if (Convert.ToInt32(_ds.Tables[0].Rows[i][TunnelHChuanDbConstNames.IS_FINISH].ToString()) == 1)
                    {
                        fpDayReportHChuan.Sheets[0].Cells[_rowDetailStartIndex + i, ++index].Text =
                            _ds.Tables[0].Rows[i][TunnelHChuanDbConstNames.STOP_DATE].ToString()
                                .Substring(0,
                                    _ds.Tables[0].Rows[i][TunnelHChuanDbConstNames.STOP_DATE].ToString().IndexOf(' '));
                    }
                    else
                    {
                        ++index;
                    }

                    //工作制式
                    fpDayReportHChuan.Sheets[0].Cells[_rowDetailStartIndex + i, ++index].Text =
                        _ds.Tables[0].Rows[i][TunnelHChuanDbConstNames.WORK_STYLE].ToString();
                    //班次
                    fpDayReportHChuan.Sheets[0].Cells[_rowDetailStartIndex + i, ++index].Text =
                        _ds.Tables[0].Rows[i][TunnelHChuanDbConstNames.WORK_TIME].ToString();
                    //状态
                    fpDayReportHChuan.Sheets[0].Cells[_rowDetailStartIndex + i, ++index].Text =
                        _ds.Tables[0].Rows[i][TunnelHChuanDbConstNames.TUNNEL_STATE].ToString();
                    //bid
                    fpDayReportHChuan.Sheets[0].Cells[_rowDetailStartIndex + i, ++index].Text =
                        _ds.Tables[0].Rows[i][TunnelHChuanDbConstNames.ID].ToString();
                    fpDayReportHChuan.Sheets[0].Columns[_BIDIndex].Visible = false;
                }
            }
            //设置按钮可操作性
            setButtenEnable();
        }

        /// <summary>
        ///     删除按钮响应
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tsBtnDel_Click(object sender, EventArgs e)
        {
            if (Alert.confirm(Const.DEL_CONFIRM_MSG))
            {
                var searchCount = _rowsCount;
                var bResult = false;
                for (var i = 0; i < _rowsCount; i++)
                {
                    _tmpRowIndex = fpDayReportHChuan.Sheets[0].ActiveRowIndex;
                    //遍历“选择”是否选中
                    if (fpDayReportHChuan.Sheets[0].Cells[_rowDetailStartIndex + i, 0].Value != null &&
                        (bool) fpDayReportHChuan.Sheets[0].Cells[_rowDetailStartIndex + i, 0].Value)
                    {
                        //主键
                        tunnelHChuanEntity.Id = Convert.ToInt32(_ds.Tables[0].Rows[i][TunnelHChuanDbConstNames.ID]);
                        var tmp = 0;
                        //主运顺槽
                        if (int.TryParse(_ds.Tables[0].Rows[i][TunnelHChuanDbConstNames.TUNNEL_ID1].ToString(), out tmp))
                        {
                            tunnelHChuanEntity.TunnelId1 = tmp;
                            tmp = 0;
                        }
                        //辅运顺槽
                        if (int.TryParse(_ds.Tables[0].Rows[i][TunnelHChuanDbConstNames.TUNNEL_ID2].ToString(), out tmp))
                        {
                            tunnelHChuanEntity.TunnelId2 = tmp;
                            tmp = 0;
                        }

                        //重设巷道类型
                        var tunnel1 = Tunnel.Find(tunnelHChuanEntity.TunnelId1);
                        tunnel1.TunnelType = TunnelTypeEnum.OTHER;
                        tunnel1.Save();
                        var tunnel2 = Tunnel.Find(tunnelHChuanEntity.TunnelId2);
                        tunnel2.TunnelType = TunnelTypeEnum.OTHER;
                        tunnel2.Save();

                        //删除回采巷道信息
                        bResult = TunnelHChuanBLL.deleteTunnelHChuan(tunnelHChuanEntity);
                    }
                }
                if (bResult)
                {
                    //TODO:删除后事件
                    //将图层中对应的信息删除
                    DelHChuanjc(tunnelHChuanEntity.TunnelId1, tunnelHChuanEntity.TunnelId2);
                    //删除工作面中对应的回采信息
                    /////Mark
                }
                bindFpTunnelHChuanInfo();
                FarPointOperate.farPointFocusSetDel(fpDayReportHChuan, _tmpRowIndex);
            }
        }

        /// <summary>
        ///     回采删除信息
        /// </summary>
        /// <param name="hd1"></param>
        /// <param name="hd2"></param>
        /// <param name="qy"></param>
        private void DelHChuanjc(int hd1, int hd2)
        {
            // 获取已选择明细行的索引
            var iSelIdxsArr = GetSelIdxs();
            var bid = "";
            //ILayer pLayer = GIS.Common.DataEditCommon.GetLayerByName(GIS.Common.DataEditCommon.g_pMap, GIS.LayerNames.DEFALUT_HENGCHUAN);
            //if (pLayer == null)
            //{
            //    MessageBox.Show("未发现横川图层！");
            //    return;
            //}
            //IFeatureLayer pFeatureLayer = (IFeatureLayer)pLayer;
            var str = "";
            for (var i = 0; i < iSelIdxsArr.Length; i++)
            {
                bid = fpDayReportHChuan.Sheets[0].Cells[iSelIdxsArr[i], _BIDIndex].Text.Trim();
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
            DataEditCommon.g_pMyMapCtrl.ActiveView.PartialRefresh(esriViewDrawPhase.esriViewBackground, null, null);
            //}
        }

        /// <summary>
        ///     全选反选
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void chkSelAll_Click(object sender, EventArgs e)
        {
            //farpoint有数据时
            if (_rowsCount > 0)
            {
                //遍历数据
                for (var i = 0; i < _rowsCount; i++)
                {
                    //checkbox选中
                    if (chkSelAll.Checked)
                    {
                        if (!_htSelIdxs.Contains(_rowDetailStartIndex + i))
                        {
                            _htSelIdxs.Add(_rowDetailStartIndex + i, true);
                        }
                        fpDayReportHChuan.Sheets[0].Cells[_rowDetailStartIndex + i, 0].Value =
                            ((CheckBox) sender).Checked;
                        _checkCount = _ds.Tables[0].Rows.Count;
                    }
                    //checkbox未选中
                    else
                    {
                        _htSelIdxs.Remove(_rowDetailStartIndex + i);
                        fpDayReportHChuan.Sheets[0].Cells[_rowDetailStartIndex + i, 0].Value =
                            ((CheckBox) sender).Checked;
                        _checkCount = 0;
                    }
                }
            }
            //设置按钮可操作性
            setButtenEnable();
        }

        /// <summary>
        ///     刷新
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tsBtnRefresh_Click(object sender, EventArgs e)
        {
            //绑定数据
            bindFpTunnelHChuanInfo();
        }

        private void tsBtnExit_Click(object sender, EventArgs e)
        {
            //关闭窗体
            Close();
        }

        private void tsBtnModify_Click(object sender, EventArgs e)
        {
            setTunnelHCEntityValue();

            var tunnelHChuanForm = new TunnelHChuanEntering(tunnelHChuanEntity, this);
            tunnelHChuanForm.Show(this);
        }

        public void refreshUpdate()
        {
            bindFpTunnelHChuanInfo();
            FarPointOperate.farPointFocusSetChange(fpDayReportHChuan, _tmpRowIndex);
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            //关闭窗体
            Close();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            //关闭窗体
            Close();
        }

        /// <summary>
        ///     获取farpoint中选中的所有行（必须实装）
        /// </summary>
        /// <returns>注意，返回值可能是null，null则代表一个也没选中</returns>
        private int[] GetSelIdxs()
        {
            if (_htSelIdxs.Count == 0)
            {
                return null;
            }
            var retArr = new int[_htSelIdxs.Count];
            _htSelIdxs.Keys.CopyTo(retArr, 0);
            return retArr;
        }

        /// <summary>
        ///     图显按钮事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnMap_Click(object sender, EventArgs e)
        {
            // 获取已选择明细行的索引
            var iSelIdxsArr = GetSelIdxs();
            if (iSelIdxsArr == null)
            {
                MessageBox.Show("未选中数据行！");
                return;
            }
            var bid = "";
            //ILayer pLayer = GIS.Common.DataEditCommon.GetLayerByName(GIS.Common.DataEditCommon.g_pMap, GIS.LayerNames.DEFALUT_HENGCHUAN);
            //if (pLayer == null)
            //{
            //    MessageBox.Show("未发现横川图层！");
            //    return;
            //}
            //IFeatureLayer pFeatureLayer = (IFeatureLayer)pLayer;
            var str = "";
            for (var i = 0; i < iSelIdxsArr.Length; i++)
            {
                bid = fpDayReportHChuan.Sheets[0].Cells[iSelIdxsArr[i], _BIDIndex].Text.Trim();
                if (bid != "")
                {
                    if (i == 0)
                        str = "bid='" + bid + "'";
                    else
                        str += " or bid='" + bid + "'";
                }
            }
            var list = MyMapHelp.FindFeatureListByWhereClause(Global.hdfdfulllyr, str);
            if (list.Count > 0)
            {
                MyMapHelp.Jump(MyMapHelp.GetGeoFromFeature(list));
                DataEditCommon.g_pMap.ClearSelection();
                for (var i = 0; i < list.Count; i++)
                {
                    DataEditCommon.g_pMap.SelectFeature(Global.hdfdfulllyr, list[i]);
                }
                WindowState = FormWindowState.Normal;
                Location = DataEditCommon.g_axTocControl.Location;
                Width = DataEditCommon.g_axTocControl.Width;
                Height = DataEditCommon.g_axTocControl.Height;
                DataEditCommon.g_pMyMapCtrl.ActiveView.PartialRefresh(esriViewDrawPhase.esriViewGeoSelection, null,
                    DataEditCommon.g_pAxMapControl.Extent);
            }
            else
            {
                Alert.alert("图元丢失");
            }
        }
    }
}