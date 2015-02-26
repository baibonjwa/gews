using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using System.Windows.Forms;
using FarPoint.Win;
using FarPoint.Win.Spread;
using LibCommon;
using LibEntity;

namespace sys1
{
    public partial class BadDataDelete : Form
    {
        /** 明细部开始index位置 **/
        private const int _iRowDetailStartIndex = 4;
        /** 保存所有用户选中的行的索引 **/
        /** 处理标志位 **/
        public static int _iDisposeFlag = Const.DISPOSE_FLAG_ZERO;
        /** 探头编号 **/
        public static string _probeId;
        /** 需要过滤的列索引 **/
        private readonly int[] _filterColunmIdxs;
        private readonly Hashtable _htSelIdxs = new Hashtable();
        private readonly MainFormGe mainWin;
        private int _iRowCount;
        /** 主键1Index **/
        private int _primaryKey1Index = 1;
        /** 主键2Index **/
        private int _primaryKey2Index = 2;
        private Thread t;
        private ThreadStart ts;

        public BadDataDelete()
        {
            InitializeComponent();
            //为了使用上个窗体的坏数据点阈值变量
            //分配用户权限
            if (CurrentUser.CurLoginUserInfo.Permission != Permission.管理员.ToString())
            {
                //btnAdd.Visible = false;
                //btnUpdate.Visible = false;
                //btnDelete.Visible = false;
                selectTunnelUserControl1.SetButtonEnable(false);
            }

            // 设置窗体默认属性
            FormDefaultPropertiesSetter.SetManagementFormDefaultProperties(this,
                Const_GE.BADDATE_GAS_CONCENTRATION_PROBE_DATA);

            // 设置Farpoint默认属性
            FarpointDefaultPropertiesSetter.SetFpDefaultProperties(fpGasConcentrationProbeDataInfo,
                Const_GE.MANAGE_GAS_CONCENTRATION_PROBE_DATA, _iRowDetailStartIndex);

            // 设置日期控件格式
            _dateTimeStart.Format = DateTimePickerFormat.Custom;
            _dateTimeStart.CustomFormat = Const.DATE_FORMART_YYYY_MM_DD;
            _dateTimeEnd.Format = DateTimePickerFormat.Custom;
            _dateTimeEnd.CustomFormat = Const.DATE_FORMART_YYYY_MM_DD;

            // 设置日期默认值（当天的0点到24点）
            string todayStart = DateTime.Now.ToString("yyyy/MM/dd") + " " + "00:00:00";
            string todayEnd = DateTime.Now.ToString("yyyy/MM/dd") + " " + "23:59:59";
            _dateTimeStart.Value = Convert.ToDateTime(todayStart);
            _dateTimeEnd.Value = Convert.ToDateTime(todayEnd);

            // 调用选择巷道控件时需要调用的方法
            selectTunnelUserControl1.LoadData();

            // 注册委托事件
            //selectTunnelUserControl1.TunnelNameChanged +=
            //    InheritTunnelNameChanged;

            // 调用委托方法 （必须实装）
            //dataPager1.FrmChild_EventHandler += new DataPager.FrmChild_DelegateHandler(FrmParent_EventHandler);

            // 设置farpoint默认行数
            fpGasConcentrationProbeDataInfo.Sheets[0].Rows.Count = _iRowDetailStartIndex;

            #region Farpoint自动过滤功能

            //初始化需要过滤功能的列
            _filterColunmIdxs = new[]
            {
                2,
                5,
                6,
                7,
                12,
                13,
                14,
                15,
                16
            };
            //禁用选择颜色相关控件
            farpointFilter1.EnableChooseColorCtrls(false);
            //设置自动隐藏过滤条件
            FarpointDefaultPropertiesSetter.SetFpFilterHideProperties(fpGasConcentrationProbeDataInfo, _filterColunmIdxs);

            #endregion
        }

        /// <summary>
        ///     委托事件
        /// </summary>
        /// <param name="sender"></param>
        //private void InheritTunnelNameChanged(object sender, TunnelEventArgs e)
        //{
        //    _lstProbeStyle.DataSource = null;
        //    _lstProbeName.DataSource = null;

        //    // 加载探头类型信息
        //    loadProbeTypeInfo();
        //}

        /// <summary>
        ///     加载探头类型信息
        /// </summary>
        private void loadProbeTypeInfo()
        {
            ProbeType[] probeTypes = ProbeType.FindAll();
            if (probeTypes.Length > 0)
            {
                _lstProbeStyle.DataSource = probeTypes;
                _lstProbeStyle.DisplayMember = "ProbeTypeName";
                _lstProbeStyle.ValueMember = "ProbeTypeId";

                _lstProbeStyle.SelectedIndex = -1;
            }
        }

        /// <summary>
        ///     探头类型选择事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void _lstProbeStyle_MouseUp(object sender, MouseEventArgs e)
        {
            _lstProbeName.DataSource = null;

            // 没有选择巷道
            if (selectTunnelUserControl1.SelectedTunnel == null)
            {
                Alert.alert(Const_GE.TUNNEL_NAME_MUST_INPUT);
            }
            else
            {
                // 根据巷道编号和探头类型编号获取探头信息
                Probe[] probes = Probe.FindAllByTunnelIdAndProbeTypeId(selectTunnelUserControl1.SelectedTunnel.TunnelId,
                  Convert.ToInt32(this._lstProbeStyle.SelectedValue));

                for (int i = 0; i < probes.Length; i++)
                {
                    _lstProbeName.Items.Add(probes);
                }
                _lstProbeName.DisplayMember = "ProbeName";
                _lstProbeName.ValueMember = "ProbeId";

                _lstProbeName.SelectedIndex = -1;
            }
        }

        /// <summary>
        ///     探头名称选择事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void _lstProbeName_MouseUp(object sender, MouseEventArgs e)
        {
            // 没有选择巷道
            if (selectTunnelUserControl1.SelectedTunnel == null)
            {
                Alert.alert(Const_GE.TUNNEL_NAME_MUST_INPUT);
            }
            // 设置探头编号全部变量
            _probeId = Convert.ToString(_lstProbeName.SelectedValue);
        }

        /// <summary>
        ///     查询
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void _btnQuery_Click(object sender, EventArgs e)
        {
            // 隐藏无数据提示信息

            // 验证
            if (!check())
            {
                DialogResult = DialogResult.None;
                return;
            }
            DialogResult = DialogResult.None;

            // 加载瓦斯浓度探头数据信息
            //ts = loadGasConcentrationProbeDataInfo;
            //t = new Thread(ts);
            //t.Start();

            // 启动.t.Start();
            //loadGasConcentrationProbeDataInfo();
        }

        /// <summary>
        ///     验证画面入力数据
        /// </summary>
        /// <returns>验证结果：true 通过验证, false未通过验证</returns>
        private bool check()
        {
            // 判断选择巷道是否选择
            if (selectTunnelUserControl1.SelectedTunnel == null)
            {
                Alert.alert(Const_GE.TUNNEL_NAME_MUST_INPUT);
                return false;
            }

            // 判断传感器是否选择
            if (_lstProbeName.SelectedItems.Count == 0)
            {
                Alert.alert(Const_GE.PROBE_MUST_CHOOSE);
                return false;
            }

            // 验证通过
            return true;
        }

        /// <summary>
        ///     调用委托方法 （必须实装）
        /// </summary>
        /// <param name="sender"></param>
        private void FrmParent_EventHandler(object sender)
        {
            // 分页控件与Farpoint过滤绑定问题
            FarpointFilterSetter.ClearFpFilter(fpGasConcentrationProbeDataInfo);

            // 加载瓦斯浓度探头数据信息
            // loadGasConcentrationProbeDataInfo();
        }

        /// <summary>
        ///     加载瓦斯浓度探头数据信息
        /// </summary>
        //private void loadGasConcentrationProbeDataInfo()
        //{
        //    // 修改按钮设为不可用（必须实装）
        //    //this.btnUpdate.Enabled = false;
        //    // 删除按钮设为不可用（必须实装）
        //    //this.btnDelete.Enabled = false;
        //    // 全选/全不选checkbox设为未选中（必须实装）
        //    DataSet ds = null;
        //    int iSelCnt = 0;
        //    DateTime dtTimeStart = _dateTimeStart.Value;
        //    DateTime dtTimeEnd = _dateTimeEnd.Value;
        //    Invoke(new MethodInvoker(delegate
        //    {
        //        _chkSelAll.Checked = false;

        //        _lblTips.Text = "提示：计算坏数据中";
        //        _btnOK.Enabled = false;
        //        _btnQuery.Enabled = false;
        //        _btnStopCalculate.Enabled = true;
        //        // 清空HashTabl（必须实装）
        //        _htSelIdxs.Clear();

        //        // 删除farpoint明细部（必须实装）
        //        // 解决修改、删除某条数据后，重新load的时候，选择列checkbox不恢复成默认（不选择）的BUG
        //        // 解决删除全部数据后，再添加一行，报错的BUG
        //        if (fpGasConcentrationProbeDataInfo.Sheets[0].Rows.Count != _iRowDetailStartIndex)
        //        {
        //            fpGasConcentrationProbeDataInfo.Sheets[0].Rows.Remove(_iRowDetailStartIndex, _iRowCount);
        //        }
        //        else
        //        {
        //            _iRowCount = 0;
        //        }


        //        // 根据探头编号和开始结束时间，获取特定探头和特定时间段内的【瓦斯浓度探头数据】（必须实装）
        //        // int iRecordCount = GasConcentrationProbeDataBLL.selectAllGasConcentrationProbeDataByProbeIdAndTime(_probeId, dtTimeStart, dtTimeEnd).Tables[0].Rows.Count;

        //        //if (iRecordCount > 0)
        //        //{
        //        //    this._gbPage.Enabled = true;
        //        //}
        //        //else
        //        //{
        //        //    this._gbPage.Enabled = false;
        //        //}

        //        // 调用分页控件初始化方法（必须实装）
        //        //dataPager1.PageControlInit(iRecordCount);

        //        // 获取要检索数据的开始位置和结束位置 （必须实装）
        //        //int iStartIndex = dataPager1.getStartIndex();
        //        //int iEndIndex = dataPager1.getEndIndex();

        //        //// 获取开始位置和结束位置之间的数据（必须实装）
        //        //// 说明：如果画面当前显示的件数是10，那么init时开始位置为1，结束位置为10，点击下一页后，开始位置变为11，结束位置变为20
        //        //
        //        pbBar.Value = 0;
        //    }));
        //    ds = GasConcentrationProbeDataBLL.selectGasConcentrationProbeDataForPageByProbeIdAndTimeAndBad(_probeId,
        //        dtTimeStart, dtTimeEnd, mainWin.BadDataThreshold);

        //    // 当前检索件数（必须实装）
        //    iSelCnt = ds.Tables[0].Rows.Count;
        //    // 重新设定farpoint显示行数 （必须实装）
        //    Invoke(new MethodInvoker(delegate
        //    {
        //        fpGasConcentrationProbeDataInfo.Sheets[0].Rows.Count = _iRowDetailStartIndex + iSelCnt;
        //        pbBar.Maximum = ds.Tables[0].Rows.Count;
        //    }));
        //    // 检索件数 > 0 的场合
        //    if (iSelCnt > 0)
        //    {
        //        // 设置处理标识位
        //        _iDisposeFlag = Const.DISPOSE_FLAG_ONE;

        //        // 当前检索件数（必须实装）
        //        _iRowCount = 0;

        //        // 循环结果集
        //        for (int i = 0; i < iSelCnt; i++)
        //        {
        //            _iRowCount++;
        //            int index = 0;
        //            // 选择
        //            Invoke(new MethodInvoker(delegate
        //            {
        //                var objCheckCell = new CheckBoxCellType();
        //                fpGasConcentrationProbeDataInfo.Sheets[0].Cells[_iRowDetailStartIndex + i, index].CellType =
        //                    objCheckCell;

        //                // 探头数据编号
        //                fpGasConcentrationProbeDataInfo.Sheets[0].Cells[_iRowDetailStartIndex + i, ++index].Text =
        //                    ds.Tables[0].Rows[i][GasConcentrationProbeDataDbConstNames.PROBE_DATA_ID].ToString();
        //                // 探头编号
        //                fpGasConcentrationProbeDataInfo.Sheets[0].Cells[_iRowDetailStartIndex + i, ++index].Text =
        //                    ds.Tables[0].Rows[i][GasConcentrationProbeDataDbConstNames.PROBE_ID].ToString();
        //                // 探头数值
        //                fpGasConcentrationProbeDataInfo.Sheets[0].Cells[_iRowDetailStartIndex + i, ++index].Text =
        //                    ds.Tables[0].Rows[i][GasConcentrationProbeDataDbConstNames.PROBE_VALUE].ToString();
        //                // 探头前一数值
        //                //this.fpGasConcentrationProbeDataInfo.Sheets[0].Cells[_iRowDetailStartIndex + i, ++index].Text =
        //                //    ds.Tables[0].Rows[i]["PreviouData"].ToString();
        //                // 探头差值
        //                fpGasConcentrationProbeDataInfo.Sheets[0].Cells[_iRowDetailStartIndex + i, ++index].Text =
        //                    ds.Tables[0].Rows[i]["Difference"].ToString();
        //                // 记录时间
        //                fpGasConcentrationProbeDataInfo.Sheets[0].Cells[_iRowDetailStartIndex + i, ++index].Text =
        //                    ds.Tables[0].Rows[i][GasConcentrationProbeDataDbConstNames.RECORD_TIME].ToString();
        //                // 记录类型
        //                fpGasConcentrationProbeDataInfo.Sheets[0].Cells[_iRowDetailStartIndex + i, ++index].Text =
        //                    ds.Tables[0].Rows[i][GasConcentrationProbeDataDbConstNames.RECORD_TYPE].ToString();

        //                // 探头编号
        //                string strProbeId =
        //                    ds.Tables[0].Rows[i][GasConcentrationProbeDataDbConstNames.PROBE_ID].ToString();
        //                // 探头信息取得
        //                // 循环中调用数据库，效率很低
        //                //DataSet dsProbe = ProbeManageBLL.selectProbeManageInfoByProbeId(strProbeId);

        //                //if (dsProbe.Tables[0].Rows.Count > 0)
        //                //{
        //                // 探头名称
        //                fpGasConcentrationProbeDataInfo.Sheets[0].Cells[_iRowDetailStartIndex + i, ++index].Text =
        //                    ds.Tables[0].Rows[0][ProbeManageDbConstNames.PROBE_NAME].ToString();

        //                // 探头类型
        //                //TODO:此处可优化
        //                fpGasConcentrationProbeDataInfo.Sheets[0].Cells[_iRowDetailStartIndex + i, ++index].Text = "";
        //                int iProbeTypeId = 0;
        //                if (int.TryParse(ds.Tables[0].Rows[0][ProbeManageDbConstNames.PROBE_TYPE_ID].ToString(),
        //                    out iProbeTypeId))
        //                {
        //                    //DataSet dsProbeType = ProbeTypeBLL.selectProbeTypeInfoByProbeTypeId(iProbeTypeId);

        //                    //if (dsProbeType.Tables[0].Rows.Count > 0)
        //                    //{
        //                    fpGasConcentrationProbeDataInfo.Sheets[0].Cells[_iRowDetailStartIndex + i, index].Text =
        //                        ds.Tables[0].Rows[0][ProbeTypeDbConstNames.PROBE_TYPE_NAME].ToString();
        //                    //}
        //                }

        //                // 探头位置坐标X
        //                string strProbeLocationX =
        //                    ds.Tables[0].Rows[0][ProbeManageDbConstNames.PROBE_LOCATION_X].ToString();
        //                fpGasConcentrationProbeDataInfo.Sheets[0].Cells[_iRowDetailStartIndex + i, ++index].Text =
        //                    (strProbeLocationX == Const.DOUBLE_DEFAULT_VALUE ? "" : strProbeLocationX);

        //                // 探头位置坐标Y
        //                string strProbeLocationY =
        //                    ds.Tables[0].Rows[0][ProbeManageDbConstNames.PROBE_LOCATION_Y].ToString();
        //                fpGasConcentrationProbeDataInfo.Sheets[0].Cells[_iRowDetailStartIndex + i, ++index].Text =
        //                    (strProbeLocationY == Const.DOUBLE_DEFAULT_VALUE ? "" : strProbeLocationY);

        //                // 探头位置坐标Z
        //                string strProbeLocationZ =
        //                    ds.Tables[0].Rows[0][ProbeManageDbConstNames.PROBE_LOCATION_Z].ToString();
        //                fpGasConcentrationProbeDataInfo.Sheets[0].Cells[_iRowDetailStartIndex + i, ++index].Text =
        //                    (strProbeLocationZ == Const.DOUBLE_DEFAULT_VALUE ? "" : strProbeLocationZ);

        //                // 探头描述
        //                fpGasConcentrationProbeDataInfo.Sheets[0].Cells[_iRowDetailStartIndex + i, ++index].Text =
        //                    ds.Tables[0].Rows[0][ProbeManageDbConstNames.PROBE_DESCRIPTION].ToString();

        //                // 巷道信息
        //                // 矿井名称
        //                fpGasConcentrationProbeDataInfo.Sheets[0].Cells[_iRowDetailStartIndex + i, ++index].Text = "";
        //                // 水平
        //                fpGasConcentrationProbeDataInfo.Sheets[0].Cells[_iRowDetailStartIndex + i, ++index].Text = "";
        //                // 采区
        //                fpGasConcentrationProbeDataInfo.Sheets[0].Cells[_iRowDetailStartIndex + i, ++index].Text = "";
        //                // 工作面
        //                fpGasConcentrationProbeDataInfo.Sheets[0].Cells[_iRowDetailStartIndex + i, ++index].Text = "";
        //                // 巷道名称
        //                fpGasConcentrationProbeDataInfo.Sheets[0].Cells[_iRowDetailStartIndex + i, ++index].Text = "";

        //                int iTunnelID = 0;
        //                //TODO:此处可优化
        //                if (int.TryParse(ds.Tables[0].Rows[0][ProbeManageDbConstNames.TUNNEL_ID].ToString(),
        //                    out iTunnelID))
        //                {
        //                    //TunnelEntity tunnelEntity = TunnelInfoBLL.selectTunnelInfoByTunnelID(iTunnelID);
        //                    //if (tunnelEntity != null)
        //                    //{
        //                    // 矿井名称
        //                    fpGasConcentrationProbeDataInfo.Sheets[0].Cells[_iRowDetailStartIndex + i, index - 4].Text =
        //                        ds.Tables[0].Rows[i][MineDbConstNames.MINE_NAME].ToString();
        //                    // 水平
        //                    fpGasConcentrationProbeDataInfo.Sheets[0].Cells[_iRowDetailStartIndex + i, index - 3].Text =
        //                        ds.Tables[0].Rows[i][HorizontalDbConstNames.HORIZONTAL_NAME].ToString();
        //                    // 采区
        //                    fpGasConcentrationProbeDataInfo.Sheets[0].Cells[_iRowDetailStartIndex + i, index - 2].Text =
        //                        ds.Tables[0].Rows[i][MiningAreaDbConstNames.MININGAREA_NAME].ToString();
        //                    // 工作面
        //                    fpGasConcentrationProbeDataInfo.Sheets[0].Cells[_iRowDetailStartIndex + i, index - 1].Text =
        //                        ds.Tables[0].Rows[i][WorkingFaceDbConstNames.WORKINGFACE_NAME].ToString();
        //                    // 巷道名称
        //                    fpGasConcentrationProbeDataInfo.Sheets[0].Cells[_iRowDetailStartIndex + i, index].Text =
        //                        ds.Tables[0].Rows[i][TunnelInfoDbConstNames.TUNNEL_NAME].ToString();
        //                    //}
        //                }
        //                //}
        //            }));
        //            Invoke(new MethodInvoker(delegate { pbBar.Value++; }));
        //        }
        //        Invoke(new MethodInvoker(delegate
        //        {
        //            _lblTips.Text = "提示：计算完成";
        //            _btnQuery.Enabled = true;
        //            _btnStopCalculate.Enabled = false;
        //            _btnOK.Enabled = true;
        //        }));
        //    }
        //    else
        //    {
        //        // 显示无数据提示信息
        //        Invoke(new MethodInvoker(delegate
        //        {
        //            _lblTips.Text = "提示：无坏点数据";
        //            _btnQuery.Enabled = true;
        //            _btnStopCalculate.Enabled = false;
        //            _btnOK.Enabled = true;
        //        }));
        //    }
        //}


        /// <summary>
        ///     添加
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnAdd_Click(object sender, EventArgs e)
        {
            var gasConcentrationProbeDataEntering = new GasConcentrationProbeDataEntering();
            if (DialogResult.OK == gasConcentrationProbeDataEntering.ShowDialog())
            {
                if (_iDisposeFlag == Const.DISPOSE_FLAG_ONE)
                {
                    // 加载瓦斯浓度探头数据信息
                    //loadGasConcentrationProbeDataInfo();
                    // 跳转到尾页（必须实装）
                    //this.dataPager1.btnLastPage_Click(sender, e);

                    // 设置farpoint焦点（必须实装）
                    fpGasConcentrationProbeDataInfo.Sheets[0].SetActiveCell(
                        fpGasConcentrationProbeDataInfo.Sheets[0].Rows.Count, 0);
                }
            }
        }

        /// <summary>
        ///     修改（必须实装）
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnUpdate_Click(object sender, EventArgs e)
        {
            // 获取已选择明细行的索引
            int[] iSelIdxsArr = GetSelIdxs();
            // 获取编号（主键）
            string strPrimaryKey = fpGasConcentrationProbeDataInfo.Sheets[0].Cells[iSelIdxsArr[0], 1].Text;

            var gasConcentrationProbeDataEntering = new GasConcentrationProbeDataEntering(strPrimaryKey);
            if (DialogResult.OK == gasConcentrationProbeDataEntering.ShowDialog())
            {
                // 加载瓦斯浓度探头数据信息
                //loadGasConcentrationProbeDataInfo();

                // 设置farpoint焦点（必须实装）
                fpGasConcentrationProbeDataInfo.Sheets[0].SetActiveCell(iSelIdxsArr[0], 0);
                fpGasConcentrationProbeDataInfo.Sheets[0].Cells[iSelIdxsArr[0], 0].Value = true;

                // *****************************************************

                // 保存索引号
                if (!_htSelIdxs.Contains(iSelIdxsArr[0]))
                {
                    _htSelIdxs.Add(iSelIdxsArr[0], true);
                }
                // 修改按钮设为不可用（必须实装）
                //this.btnUpdate.Enabled = true;
                // 删除按钮设为不可用（必须实装）
                //this.btnDelete.Enabled = true;
                // 全选/全不选checkbox设为未选中（必须实装）
                _chkSelAll.Checked = false;

                // *****************************************************
            }
        }

        /// <summary>
        ///     删除（必须实装）
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (Alert.confirm(Const_GE.DEL_CONFIRM_MSG))
            {
                // 获取已选择明细行的索引
                int[] iSelIdxsArr = GetSelIdxs();

                var pkIdxArrList = new List<string[]>();

                for (int i = 0; i < iSelIdxsArr.Length; i++)
                {
                    // 获取主键
                    string iPk1 =
                        fpGasConcentrationProbeDataInfo.Sheets[0].Cells[iSelIdxsArr[i], _primaryKey1Index].Text;
                    string iPk2 =
                        fpGasConcentrationProbeDataInfo.Sheets[0].Cells[iSelIdxsArr[i], _primaryKey2Index].Text;

                    var strArr = new string[2];
                    strArr[0] = iPk1;
                    strArr[1] = iPk2;

                    pkIdxArrList.Add(strArr);
                }

                GasConcentrationProbeData.DeleteAll(pkIdxArrList);
                // 加载瓦斯浓度探头数据信息
                //loadGasConcentrationProbeDataInfo();

                // 设置farpoint焦点（必须实装）
                fpGasConcentrationProbeDataInfo.Sheets[0].SetActiveCell(iSelIdxsArr[0], 0);
            }
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
        ///     farpoint的ButtonClicked事件（必须实装）
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void fpGasConcentrationProbeDataInfo_ButtonClicked(object sender, EditorNotifyEventArgs e)
        {
            // 判断点击的空间类型是否是.FpCheckBox)
            if (e.EditingControl is FpCheckBox)
            {
                var fpChk = (FpCheckBox)e.EditingControl;
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
                            _chkSelAll.Checked = true;
                        }
                    }
                }
                else
                {
                    // 移除索引号
                    _htSelIdxs.Remove(e.Row);

                    // 全选/全不选checkbox设为未选中
                    _chkSelAll.Checked = false;
                }

                // 如果保存索引号的Hashtable中保存的索引件数是1，则修改按钮设为可用，否则设为不可用
                //this.btnUpdate.Enabled = (_htSelIdxs.Count == 1) ? true : false;
                // 删除按钮
                //this.btnDelete.Enabled = (_htSelIdxs.Count >= 1) ? true : false;
            }
        }

        /// <summary>
        ///     全选/全不选checkbox的click事件（必须实装）
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void _chkSelAll_Click(object sender, EventArgs e)
        {
            // 全不选的情况下
            if (_htSelIdxs.Count == _iRowCount)
            {
                // 循环明细
                for (int i = 0; i < _iRowCount; i++)
                {
                    // 将所有明细的checkbox设为未选中
                    fpGasConcentrationProbeDataInfo.Sheets[0].Cells[_iRowDetailStartIndex + i, 0].Value =
                        ((CheckBox)sender).Checked;
                    // 将存有选中项目的数组清空
                    _htSelIdxs.Remove(_iRowDetailStartIndex + i);
                }
                // 删除按钮设为不可用
                //this.btnDelete.Enabled = false;
            }
            // 全选的情况下
            else
            {
                // 循环明细
                for (int i = 0; i < _iRowCount; i++)
                {
                    // 将所有明细设为全选中
                    fpGasConcentrationProbeDataInfo.Sheets[0].Cells[_iRowDetailStartIndex + i, 0].Value =
                        ((CheckBox)sender).Checked;
                    // 将选中明细的索引添加到数组中，如果已经存在不要二次添加
                    if (!_htSelIdxs.Contains(_iRowDetailStartIndex + i))
                    {
                        _htSelIdxs.Add(_iRowDetailStartIndex + i, true);
                    }
                }
                // 删除按钮设为可用
                //this.btnDelete.Enabled = true;
            }

            // 如果保存索引号的Hashtable中保存的索引件数是1，则修改按钮设为可用，否则设为不可用
            //this.btnUpdate.Enabled = (_htSelIdxs.Count == 1) ? true : false;
            // 删除按钮
            //this.btnDelete.Enabled = (_htSelIdxs.Count >= 1) ? true : false;
        }

        /// <summary>
        ///     farpointFilter1的OnCheckFilterChanged方法
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="arg"></param>
        private void farpointFilter1_OnCheckFilterChanged(object sender, EventArgs arg)
        {
            var chk = (CheckBox)sender;
            //当Checkbox选中时，筛选过程中则将不符合条件的数据隐藏
            if (chk.Checked)
            {
                //禁用选择颜色相关控件
                farpointFilter1.EnableChooseColorCtrls(false);
                //设置自动隐藏过滤条件
                FarpointDefaultPropertiesSetter.SetFpFilterHideProperties(fpGasConcentrationProbeDataInfo,
                    _filterColunmIdxs);
            }
            //未选中时，根据用户自定义的颜色进行分类显示
            else
            {
                //启用选择颜色相关控件
                farpointFilter1.EnableChooseColorCtrls(true);
                //设置自定义过滤条件
                FarpointDefaultPropertiesSetter.SetFpCustomFilterProperties(fpGasConcentrationProbeDataInfo,
                    farpointFilter1.GetSelectedFitColor(), farpointFilter1.GetSelectedNotFitColor(), _filterColunmIdxs);
            }
        }

        /// <summary>
        ///     清空过滤条件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="arg"></param>
        private void farpointFilter1_OnClickClearFilterBtn(object sender, EventArgs arg)
        {
            //清空过滤条件
            fpGasConcentrationProbeDataInfo.ActiveSheet.RowFilter.ResetFilter();
        }

        /// <summary>
        ///     根据新的颜色值设置自动隐藏过滤条件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="arg"></param>
        private void farpointFilter1_OnClickFitColorBtnOK(object sender, EventArgs arg)
        {
            //根据新的颜色值设置自动隐藏过滤条件
            FarpointDefaultPropertiesSetter.SetFpCustomFilterProperties(fpGasConcentrationProbeDataInfo,
                farpointFilter1.GetSelectedFitColor(), farpointFilter1.GetSelectedNotFitColor(), _filterColunmIdxs);
        }

        /// <summary>
        ///     根据新的颜色值设置自动隐藏过滤条件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="arg"></param>
        private void farpointFilter1_OnClickNotFitColorBtnOK(object sender, EventArgs arg)
        {
            //根据新的颜色值设置自动隐藏过滤条件
            FarpointDefaultPropertiesSetter.SetFpCustomFilterProperties(fpGasConcentrationProbeDataInfo,
                farpointFilter1.GetSelectedFitColor(), farpointFilter1.GetSelectedNotFitColor(), _filterColunmIdxs);
        }

        /// <summary>
        ///     确定
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void _btnOK_Click(object sender, EventArgs e)
        {
            // 窗口关闭
            if (Alert.confirm(Const_GE.DEL_CONFIRM_MSG))
            {
                // 获取已选择明细行的索引
                int[] iSelIdxsArr = GetSelIdxs();

                var pkIdxArrList = new List<string[]>();
                if (iSelIdxsArr == null)
                {
                    Alert.alert("请选择要剔除的数据");
                }
                else
                {
                    for (int i = 0; i < iSelIdxsArr.Length; i++)
                    {
                        // 获取主键
                        string iPk1 =
                            fpGasConcentrationProbeDataInfo.Sheets[0].Cells[iSelIdxsArr[i], _primaryKey1Index].Text;
                        string iPk2 =
                            fpGasConcentrationProbeDataInfo.Sheets[0].Cells[iSelIdxsArr[i], _primaryKey2Index].Text;

                        var strArr = new string[2];
                        strArr[0] = iPk1;
                        strArr[1] = iPk2;

                        pkIdxArrList.Add(strArr);
                    }

                    // 删除成功的场合
                    GasConcentrationProbeData.DeleteAll(pkIdxArrList);
                    //ts = loadGasConcentrationProbeDataInfo;
                    t = new Thread(ts);
                    t.Start();

                    // 设置farpoint焦点（必须实装）
                    fpGasConcentrationProbeDataInfo.Sheets[0].SetActiveCell(iSelIdxsArr[0], 0);
                }
            }
        }

        /// <summary>
        ///     取消
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void _btnCancel_Click(object sender, EventArgs e)
        {
            // 窗口关闭
            Close();
        }

        /// <summary>
        ///     退出
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnExit_Click(object sender, EventArgs e)
        {
            // 关闭窗口
            Close();
        }

        /// <summary>
        ///     导出
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tsBtnExport_Click(object sender, EventArgs e)
        {
            if (FileExport.fileExport(fpGasConcentrationProbeDataInfo, true))
            {
                Alert.alert(Const.EXPORT_SUCCESS_MSG);
            }
        }

        /// <summary>
        ///     打印
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tsBtnPrint_Click(object sender, EventArgs e)
        {
            FilePrint.CommonPrint(fpGasConcentrationProbeDataInfo, 0);
        }

        /// <summary>
        ///     刷新
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnRefresh_Click(object sender, EventArgs e)
        {
            // 加载瓦斯浓度探头数据信息
            //loadGasConcentrationProbeDataInfo();
        }

        private void _btnStopCalculate_Click(object sender, EventArgs e)
        {
            t.Abort();
            _btnQuery.Enabled = true;
            _btnStopCalculate.Enabled = false;
            _btnOK.Enabled = true;
            _lblTips.Text = "提示：计算已停止";
        }
    }
}