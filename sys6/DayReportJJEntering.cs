using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Windows.Forms;
using LibBusiness;
using LibBusiness.CommonBLL;
using LibCommon;
using LibEntity;
using LibSocket;

namespace UnderTerminal
{
    public partial class DayReportJJEntering : Form
    {
        #region ******变量声明******

        private readonly int[] _arr;
        private readonly UnderMessageWindow mainWin;
        private readonly int tunnelId = -1;
        /**巷道实体**/
        /**回采日报实体**/
        private DayReportJj _dayReportJJEntity = new DayReportJj();
        private Tunnel _tunnelEntity = new Tunnel();
        /**巷道关联矿井等信息ID集合**/
        private WirePoint[] wirePoints;

        #endregion ******变量声明******

        #region

        //各列索引
        private const int C_WORK_TIME = 0; // 班次
        private const int C_WORK_CONTENT = 1; // 工作内容
        private const int C_WORK_PROGRESS = 2; // 进尺
        private const int C_COMMENTS = 3; // 备注

        #endregion

        /// <summary>
        ///     构造方法
        /// </summary>
        public DayReportJJEntering(int tunnelId, string tunnelName, UnderMessageWindow win)
        {
            InitializeComponent();

            this.tunnelId = tunnelId;
            Text = tunnelName;
            mainWin = win;

            addInfo();
        }

        /// <summary>
        ///     构造方法
        /// </summary>
        /// <param name="array">巷道编号数组</param>
        /// <param name="dayReportHCEntity">回采进尺日报实体</param>
        public DayReportJJEntering(int[] array, DayReportJj dayReportJJEntity)
        {
            _arr = array;
            _dayReportJJEntity = dayReportJJEntity;
            _tunnelEntity.TunnelId = array[4];
            InitializeComponent();
            //修改初始化
            changeInfo();
            //设置窗体格式
            FormDefaultPropertiesSetter.SetEnteringFormDefaultProperties(this, Const_MS.DAY_REPORT_JJ_CHANGE);

            dgrdvDayReportJJ.AllowUserToAddRows = false;
        }

        /// <summary>
        ///     委托事件
        /// </summary>
        /// <param name="sender"></param>
        private void InheritTunnelNameChanged(object sender, TunnelEventArgs e)
        {
            //bindDistanceFromWirePoint();
        }

        //private void bindDistanceFromWirePoint()
        //{
        //    if (this.Text == Const_MS.DAY_REPORT_JJ_ADD)
        //    {
        //        _tunnelEntity.Tunnel = this.tunnelId;
        //    }
        //    DataSet dsWireInfo = WireInfoBLL.selectAllWireInfo(_tunnelEntity);
        //    if (dsWireInfo.Tables[0].Rows.Count > 0)
        //    {
        //        dsWirePoint = WirePointBLL.selectAllWirePointInfo(Convert.ToInt32(dsWireInfo.Tables[0].Rows[0][WireInfoDbConstNames.ID].ToString()));
        //        cboConsultWirePoint.DataSource = dsWirePoint.Tables[0];
        //        cboConsultWirePoint.DisplayMember = WirePointDbConstNames.WIRE_POINT_NAME;
        //        cboConsultWirePoint.ValueMember = WirePointDbConstNames.ID;
        //    }
        //    else
        //    {
        //        for (int i = 0; i < dgrdvDayReportJJ.Rows.Count; i++)
        //        {
        //            dgrdvDayReportJJ.Rows[i].Cells[4].Value = null;
        //        }
        //        cboConsultWirePoint.DataSource = null;
        //    }
        //}

        /// <summary>
        ///     添加时加载初始化设置
        /// </summary>
        private void addInfo()
        {
            //日期
            dtpDate.Value = DateTime.Now.Date;
            //绑定队别名称
            bindTeamInfo();
            //初始化班次
            //this.bindWorkTimeFirstTime();
            //设置为默认工作制式
            if (WorkingTimeDefault.FindFirst().DefaultWorkTimeGroupId == Const_MS.WORK_GROUP_ID_38)
            {
                rbtn38.Checked = true;
            }
            else
            {
                rbtn46.Checked = true;
            }

            setWorkTimeName();
            //设置班次为当前时间对应的班次
            dgrdvDayReportJJ[0, 0].Value =
                DataBindUtil.JudgeWorkTimeNow(rbtn38.Checked ? Const_MS.WORK_TIME_38 : Const_MS.WORK_TIME_46);

            dgrdvDayReportJJ[1, 0].Value = Const_MS.JJ;
        }

        /// <summary>
        ///     修改时加载初始化设置
        /// </summary>
        private void changeInfo()
        {
            //绑定默认信息
            addInfo();
            //绑定修改数据
            bindInfo();
        }

        /// <summary>
        ///     datagridview绑定信息
        /// </summary>
        private void bindInfo()
        {
            //巷道ID
            _tunnelEntity.TunnelId = _arr[4];

            //巷道实体
            _tunnelEntity = BasicInfoManager.getInstance().getTunnelByID(tunnelId);

            //日期
            dtpDate.Value = _dayReportJJEntity.DateTime;

            //工作制式
            if (_dayReportJJEntity.WorkTimeStyle == Const_MS.WORK_TIME_38)
            {
                rbtn38.Checked = true;
                ;
            }
            if (_dayReportJJEntity.WorkTimeStyle == Const_MS.WORK_TIME_46)
            {
                rbtn46.Checked = true;
            }

            //队别
            cboTeamName.Text = Team.Find(_dayReportJJEntity.Team.TeamId).TeamName;

            //绑定队别成员
            DataBindUtil.LoadTeamMemberByTeamName(cboSubmitter, cboTeamName.Text);

            //bindDistanceFromWirePoint();


            //填报人
            cboSubmitter.Text = _dayReportJJEntity.Submitter;
            dgrdvDayReportJJ.Rows.Add();
            dgrdvDayReportJJ[0, 0].Value = _dayReportJJEntity.WorkTime;
            dgrdvDayReportJJ[1, 0].Value = _dayReportJJEntity.WorkInfo;
            dgrdvDayReportJJ[2, 0].Value = _dayReportJJEntity.JinChi;
            dgrdvDayReportJJ[3, 0].Value = _dayReportJJEntity.DistanceFromWirepoint;
            dgrdvDayReportJJ[4, 0].Value = _dayReportJJEntity.ConsultWirepoint;
            dgrdvDayReportJJ[5, 0].Value = _dayReportJJEntity.Remarks;
        }

        /// <summary>
        ///     绑定队别名称
        /// </summary>
        private void bindTeamInfo()
        {
            cboTeamName.Items.Clear();
            Team[] teams = Team.FindAll();
            foreach (Team t in teams)
            {
                cboTeamName.Items.Add(t.TeamName);
            }
        }


        /// <summary>
        ///     队别选择事件（根据队别绑定队员）
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cboTeamName_SelectedIndexChanged(object sender, EventArgs e)
        {
            //this.bindTeamMember();
        }

        /// <summary>
        ///     取消按钮事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnCancel_Click(object sender, EventArgs e)
        {
            //关闭窗体
            Close();
        }

        /// <summary>
        ///     提交按钮事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnSubmit_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show(@"确认提交", "井下终端录入系统", MessageBoxButtons.OKCancel,
                MessageBoxIcon.Question);
            if (result != DialogResult.OK)
            {
                return;
            }

            _tunnelEntity.TunnelId = tunnelId;
            _tunnelEntity = BasicInfoManager.getInstance().getTunnelByID(tunnelId);
            // 验证
            if (!check())
            {
                DialogResult = DialogResult.None;
                return;
            }
            DialogResult = DialogResult.OK;

            //if (this.Text == Const_MS.DAY_REPORT_JJ_ADD)
            //{
            //TunnelDefaultSelect.InsertDefaultTunnel(DayReportJJDbConstNames.TABLE_NAME, this.tunnelId);
            insertDayReportJJInfo();
            //}
            //if (this.Text == Const_MS.DAY_REPORT_JJ_CHANGE)
            //{
            //    DayReportJJEntity oldDayReportJJEntity = _dayReportJJEntity;   //修改之前的实体
            //    TunnelDefaultSelect.UpdateDefaultTunnel(DayReportJJDbConstNames.TABLE_NAME, this.tunnelId);
            //    updateDayReportJJInfo();
            //}
        }

        /// <summary>
        ///     添加回采日报
        /// </summary>
        private void insertDayReportJJInfo()
        {
            var dayReportJJEntityList = new List<DayReportJj>();
            for (int i = 0; i < dgrdvDayReportJJ.RowCount; i++)
            {
                var _dayReportJJEntity = new DayReportJj();
                // 最后一行为空行时，跳出循环
                if (i == dgrdvDayReportJJ.RowCount - 1)
                {
                    break;
                }

                /**回采日报实体赋值**/
                //队别名称
                _dayReportJJEntity.Team.TeamId = Convert.ToInt32(cboTeamName.SelectedValue);
                //绑定巷道编号
                _dayReportJJEntity.WorkingFace.WorkingFaceId =
                    BasicInfoManager.getInstance().getTunnelByID(_tunnelEntity.TunnelId).WorkingFace.WorkingFaceId;
                //日期
                _dayReportJJEntity.DateTime = dtpDate.Value;
                //填报人
                _dayReportJJEntity.Submitter = cboSubmitter.Text;
                //工作制式
                if (rbtn38.Checked)
                {
                    _dayReportJJEntity.WorkTimeStyle = rbtn38.Text;
                }
                if (rbtn46.Checked)
                {
                    _dayReportJJEntity.WorkTimeStyle = rbtn46.Text;
                }
                //班次
                if (dgrdvDayReportJJ.Rows[i].Cells[C_WORK_TIME].Value != null)
                {
                    _dayReportJJEntity.WorkTime = dgrdvDayReportJJ.Rows[i].Cells[C_WORK_TIME].Value.ToString();
                }
                //工作内容
                if (dgrdvDayReportJJ.Rows[i].Cells[C_WORK_CONTENT].Value != null)
                {
                    _dayReportJJEntity.WorkInfo = dgrdvDayReportJJ.Rows[i].Cells[C_WORK_CONTENT].Value.ToString();
                }
                //掘进进尺
                if (dgrdvDayReportJJ.Rows[i].Cells[C_WORK_PROGRESS].Value != null)
                {
                    _dayReportJJEntity.JinChi = Convert.ToDouble(dgrdvDayReportJJ.Rows[i].Cells[C_WORK_PROGRESS].Value);
                }
                //备注
                if (dgrdvDayReportJJ.Rows[i].Cells[C_COMMENTS].Value != null)
                {
                    _dayReportJJEntity.Remarks = dgrdvDayReportJJ.Rows[i].Cells[C_COMMENTS].Value.ToString();
                }
                //BID
                _dayReportJJEntity.BindingId = IDGenerator.NewBindingID();

                //添加到dayReportHCEntityList中
                dayReportJJEntityList.Add(_dayReportJJEntity);
            }
            bool bResult = false;
            //循环添加
            foreach (DayReportJj dayReportJJEntity in dayReportJJEntityList)
            {
                //添加回采进尺日报
                dayReportJJEntity.SaveAndFlush();

                var msg = new UpdateWarningDataMsg(mainWin.workingfaceId, tunnelId, DayReportJj.TableName,
                    OPERATION_TYPE.ADD, DateTime.Now);
                mainWin.SendMsg2Server(msg);
            }
        }

        /// <summary>
        ///     修改回采日报信息
        /// </summary>
        private void updateDayReportJJInfo()
        {
            //绑定巷道编号
            _dayReportJJEntity.WorkingFace.WorkingFaceId = tunnelId;
            //队别名称
            _dayReportJJEntity.Team.TeamId = Convert.ToInt32(cboTeamName.SelectedValue);
            //日期
            _dayReportJJEntity.DateTime = dtpDate.Value;
            //填报人
            _dayReportJJEntity.Submitter = cboSubmitter.Text;
            //工作制式
            if (rbtn38.Checked)
            {
                _dayReportJJEntity.WorkTimeStyle = rbtn38.Text;
            }
            if (rbtn46.Checked)
            {
                _dayReportJJEntity.WorkTimeStyle = rbtn46.Text;
            }
            //班次
            if (dgrdvDayReportJJ.Rows[0].Cells[0].Value != null)
            {
                _dayReportJJEntity.WorkTime = dgrdvDayReportJJ.Rows[0].Cells[0].Value.ToString();
            }
            //工作内容
            if (dgrdvDayReportJJ.Rows[0].Cells[1].Value != null)
            {
                _dayReportJJEntity.WorkInfo = dgrdvDayReportJJ.Rows[0].Cells[1].Value.ToString();
            }
            //掘进进尺
            if (dgrdvDayReportJJ.Rows[0].Cells[2].Value != null)
            {
                _dayReportJJEntity.JinChi = Convert.ToDouble(dgrdvDayReportJJ.Rows[0].Cells[2].Value);
            }
            //备注
            if (dgrdvDayReportJJ.Rows[0].Cells[5].Value != null)
            {
                _dayReportJJEntity.Remarks = dgrdvDayReportJJ.Rows[0].Cells[5].Value.ToString();
            }

            //备注
            if (dgrdvDayReportJJ.Rows[0].Cells[3].Value != null)
            {
                _dayReportJJEntity.DistanceFromWirepoint =
                    Convert.ToDouble(dgrdvDayReportJJ.Rows[0].Cells[3].Value.ToString());
            }

            //备注
            if (dgrdvDayReportJJ.Rows[0].Cells[4].Value != null)
            {
                _dayReportJJEntity.ConsultWirepoint = Convert.ToInt32(dgrdvDayReportJJ.Rows[0].Cells[4].Value.ToString());
            }

            bool bResult = false;
            //提交修改
            _dayReportJJEntity.SaveAndFlush();

            //修改成功
            var msg = new UpdateWarningDataMsg(mainWin.workingfaceId, tunnelId, DayReportJj.TableName,
                OPERATION_TYPE.UPDATE, dtpDate.Value);
            mainWin.SendMsg2Server(msg);
        }

        /// <summary>
        ///     设置班次名称
        /// </summary>
        private void setWorkTimeName()
        {
            string strWorkTimeName = "";
            string sysDateTime = DateTime.Now.ToLongTimeString();
            if (rbtn38.Checked)
            {
                strWorkTimeName = MineDataSimpleBLL.selectWorkTimeNameByWorkTimeGroupIdAndSysTime(1, sysDateTime);
            }
            else
            {
                strWorkTimeName = MineDataSimpleBLL.selectWorkTimeNameByWorkTimeGroupIdAndSysTime(2, sysDateTime);
            }

            if (strWorkTimeName != null && strWorkTimeName != "")
            {
                dgrdvDayReportJJ[C_WORK_TIME, 0].Value = strWorkTimeName;
            }
        }

        /// <summary>
        ///     三八制选择事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void rbtn38_CheckedChanged(object sender, EventArgs e)
        {
            DataBindUtil.LoadWorkTime(cboWorkTime,
                rbtn38.Checked ? Const_MS.WORK_GROUP_ID_38 : Const_MS.WORK_GROUP_ID_46);
        }

        /// <summary>
        ///     验证
        /// </summary>
        /// <returns></returns>
        private bool check()
        {
            //巷道是否选择
            if (tunnelId <= 0)
            {
                Alert.alert(Const.MSG_PLEASE_CHOOSE + Const_MS.TUNNEL + Const.SIGN_EXCLAMATION_MARK);
                return false;
            }
            //队别为空
            if (Validator.IsEmpty(cboTeamName.Text))
            {
                Alert.alert(Const.MSG_PLEASE_TYPE_IN + Const_MS.TEAM_NAME + Const.SIGN_EXCLAMATION_MARK);
                return false;
            }
            //队别名称是否有特殊字符
            if (Validator.checkSpecialCharacters(cboTeamName.Text))
            {
                Alert.alert(Const_MS.TEAM_NAME + Const.MSG_SP_CHAR + Const.SIGN_EXCLAMATION_MARK);
                return false;
            }
            //填报人是否有特殊字符
            if (Validator.checkSpecialCharacters(cboSubmitter.Text))
            {
                Alert.alert(Const_MS.SUBMITTER + Const.MSG_SP_CHAR + Const.SIGN_EXCLAMATION_MARK);
                return false;
            }
            //datagridview验证
            //只有一条数据时
            if (dgrdvDayReportJJ.Rows.Count - 1 == 0)
            {
                //添加时判断为未录入进尺
                if (Text == Const_MS.DAY_REPORT_JJ_ADD)
                {
                    Alert.alert(Const.MSG_PLEASE_TYPE_IN + Const_MS.JC + Const.SIGN_EXCLAMATION_MARK);
                    return false;
                }
                //修改时
                //else
                //{
                //    bool bResult = false;
                //    //为空返回false，不数据时跳出循环
                //    for (int i = 0; i < dgrdvDayReportJJ.ColumnCount; i++)
                //    {
                //        if (dgrdvDayReportJJ[i, 0].Value == null)
                //        {
                //            bResult = false;
                //        }
                //        else
                //        {
                //            bResult = true;
                //            break;
                //        }
                //    }
                //    if (!bResult)
                //    {
                //        Alert.alert(Const.MSG_PLEASE_TYPE_IN + Const_MS.JC);
                //        return bResult;
                //    }
                //}
            }
            for (int i = 0; i < dgrdvDayReportJJ.RowCount; i++)
            {
                // 最后一行为空行时，跳出循环
                if (i == dgrdvDayReportJJ.RowCount - 1)
                {
                    break;
                }
                var cell = dgrdvDayReportJJ.Rows[i].Cells[C_WORK_PROGRESS] as DataGridViewTextBoxCell;
                //进尺为空
                if (cell.Value == null)
                {
                    if (Const_MS.JJ == dgrdvDayReportJJ[1, i].Value.ToString())
                    {
                        cell.Style.BackColor = Const.ERROR_FIELD_COLOR;
                        Alert.alert(Const_MS.JC + Const.MSG_NOT_NULL + Const.SIGN_EXCLAMATION_MARK);
                        return false;
                    }
                    cell.Style.BackColor = Const.NO_ERROR_FIELD_COLOR;
                }
                else
                {
                    cell.Style.BackColor = Const.NO_ERROR_FIELD_COLOR;
                }
                //进尺不为数字
                if (cell.Value != null && !Validator.IsNumeric(cell.Value.ToString()))
                {
                    cell.Style.BackColor = Const.ERROR_FIELD_COLOR;
                    Alert.alert(Const_MS.JC + Const.MSG_MUST_NUMBER + Const.SIGN_EXCLAMATION_MARK);
                    return false;
                }
                cell.Style.BackColor = Const.NO_ERROR_FIELD_COLOR;
                cell = dgrdvDayReportJJ.Rows[i].Cells[C_COMMENTS] as DataGridViewTextBoxCell;
                //备注不能含特殊字符
                if (cell.Value != null)
                {
                    if (Validator.checkSpecialCharacters(cell.Value.ToString()))
                    {
                        cell.Style.BackColor = Const.ERROR_FIELD_COLOR;
                        Alert.alert(Const_MS.OTHER + Const.MSG_SP_CHAR + Const.SIGN_EXCLAMATION_MARK);
                        return false;
                    }
                    cell.Style.BackColor = Const.NO_ERROR_FIELD_COLOR;
                }
            }
            //验证成功
            return true;
        }

        private void cboTeamName_TextChanged(object sender, EventArgs e)
        {
            for (int i = 0; i < cboTeamName.Items.Count; i++)
            {
                if (cboTeamName.Text == cboTeamName.GetItemText(cboTeamName.Items[i]))
                {
                    DataBindUtil.LoadTeamMemberByTeamName(cboSubmitter, cboTeamName.Text);
                }
                cboSubmitter.Items.Clear();
                cboSubmitter.Text = "";
            }
        }



        /// <summary>
        ///     自动计算距参考导线点距离
        /// </summary>
        /// <param name="lastDistanceFromWirepoint">上次距参考导线点距离</param>
        /// <param name="jjjc">掘进进尺</param>
        /// <param name="wirePointID">参考导线点编号</param>
        /// <returns>距参考导线点距离</returns>
        //private double autoDistanceFromWirepoint(double lastDistanceFromWirepoint, double jjjc, int wirePointID)
        //{
        //    double distance = 0;
        //    double distanceFromWirepoint = 0;
        //    var wirePointInfoEntityNew = new WirePoint();
        //    var wirePointInfoEntityOld = new WirePoint();
        //    wirePointInfoEntityNew.WirePointName = Convert.ToInt32(dgrdvDayReportJJ.CurrentRow.Cells[4].Value);
        //    wirePointInfoEntityNew = WirePointBLL.selectWirePointInfoByWirePointId(wirePointInfoEntityNew.WirePointName);
        //    wirePointInfoEntityOld.WirePointName = wirePointID;
        //    wirePointInfoEntityOld = WirePointBLL.selectWirePointInfoByWirePointId(wirePointInfoEntityOld.WirePointName);
        //    if (wirePointID != 0)
        //    {
        //        distance =
        //            Math.Sqrt(Math.Pow((wirePointInfoEntityNew.CoordinateX - wirePointInfoEntityOld.CoordinateX), 2) +
        //                      Math.Pow((wirePointInfoEntityNew.CoordinateY - wirePointInfoEntityOld.CoordinateY), 2));
        //        if (wirePointInfoEntityNew.WirePointName < wirePointInfoEntityOld.WirePointName)
        //        {
        //            distanceFromWirepoint = jjjc + lastDistanceFromWirepoint + distance;
        //        }
        //        else
        //        {
        //            distanceFromWirepoint = jjjc + lastDistanceFromWirepoint - distance;
        //        }
        //        //if (distanceFromWirepoint < 0)
        //        //{
        //        //    distanceFromWirepoint = -distanceFromWirepoint;
        //        //}
        //    }
        //    else
        //    {
        //        DataSet ds = DayReportJJBLL.selectDayReportJJInfo(tunnelId);
        //        int minWirepoint = 0;
        //        if (ds.Tables[0].Rows.Count > 0)
        //        {
        //            if (int.TryParse(ds.Tables[0].Rows[0][DayReportJJDbConstNames.ID].ToString(), out minWirepoint))
        //            {
        //                wirePointInfoEntityOld.WirePointName = minWirepoint;
        //                wirePointInfoEntityOld = WirePointBLL.selectWirePointInfoByWirePointId(wirePointInfoEntityOld.WirePointName);
        //            }
        //            distance =
        //                Math.Sqrt(
        //                    Math.Pow((wirePointInfoEntityNew.CoordinateX - wirePointInfoEntityOld.CoordinateX), 2) +
        //                    Math.Pow((wirePointInfoEntityNew.CoordinateY - wirePointInfoEntityOld.CoordinateY), 2));
        //        }
        //        else
        //        {
        //            distance = 0;
        //        }

        //        distanceFromWirepoint = jjjc + lastDistanceFromWirepoint - distance;
        //    }
        //    return distanceFromWirepoint;
        //}

        /// <summary>
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        //private void dgrdvDayReportJJ_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        //{
        //    //第一行时
        //    if (e.RowIndex == 0)
        //    {
        //        //进尺单元格
        //        if (e.ColumnIndex == 2)
        //        {
        //            //非空
        //            if (dgrdvDayReportJJ[e.ColumnIndex, e.RowIndex].Value != null)
        //            {
        //                //验证输入是否为数字
        //                if (Validator.IsNumeric(dgrdvDayReportJJ[e.ColumnIndex, e.RowIndex].Value.ToString()))
        //                {
        //                    //添加时计算方式
        //                    if (Text == Const_MS.DAY_REPORT_JJ_ADD)
        //                    {
        //                        if (tunnelId > 0)
        //                        {
        //                            _dayReportJJEntity = DayReportJJBLL.returnMaxRowDistanceFromWirepoint(tunnelId);
        //                            dgrdvDayReportJJ[e.ColumnIndex + 1, e.RowIndex].Value =
        //                                autoDistanceFromWirepoint(_dayReportJJEntity.DistanceFromWirepoint,
        //                                    Convert.ToDouble(dgrdvDayReportJJ[e.ColumnIndex, e.RowIndex].Value),
        //                                    _dayReportJJEntity.ConsultWirepoint);
        //                        }
        //                    }
        //                    //修改时计算方式
        //                    else
        //                    {
        //                        dgrdvDayReportJJ[e.ColumnIndex + 1, e.RowIndex].Value =
        //                            Convert.ToDouble(dgrdvDayReportJJ[e.ColumnIndex, e.RowIndex].Value) -
        //                            _dayReportJJEntity.JinChi + _dayReportJJEntity.DistanceFromWirepoint;
        //                    }
        //                }
        //                //不是数字时
        //                else
        //                {
        //                    Alert.alert(Const_MS.JC + Const.MSG_MUST_NUMBER + Const_MS.SIGN_EXCLAMATION_MARK);
        //                    //错误处理
        //                    //添加时处理方式
        //                    if (Text == Const_MS.DAY_REPORT_JJ_ADD)
        //                    {
        //                        dgrdvDayReportJJ[e.ColumnIndex, e.RowIndex].Value = null;
        //                        dgrdvDayReportJJ[e.ColumnIndex + 1, e.RowIndex].Value = null;
        //                    }
        //                    //修改时处理方式
        //                    else
        //                    {
        //                        dgrdvDayReportJJ[e.ColumnIndex, e.RowIndex].Value = _dayReportJJEntity.JinChi;
        //                        dgrdvDayReportJJ[e.ColumnIndex + 1, e.RowIndex].Value =
        //                            _dayReportJJEntity.DistanceFromWirepoint;
        //                    }
        //                }
        //            }
        //        }
        //        //距切眼距离单元格
        //        if (e.ColumnIndex == 3)
        //        {
        //            //为空
        //            if (dgrdvDayReportJJ[e.ColumnIndex, e.RowIndex].Value == null ||
        //                dgrdvDayReportJJ[e.ColumnIndex, e.RowIndex].Value.ToString().Trim() == "")
        //            {
        //                Alert.alert(Const_MS.OFC + Const.MSG_NOT_NULL);
        //                //添加时计算方式
        //                if (Text == Const_MS.DAY_REPORT_JJ_ADD)
        //                {
        //                    if (tunnelId > 0)
        //                    {
        //                        dgrdvDayReportJJ[e.ColumnIndex, e.RowIndex].Value =
        //                            autoDistanceFromWirepoint(_dayReportJJEntity.DistanceFromWirepoint,
        //                                Convert.ToDouble(dgrdvDayReportJJ[e.ColumnIndex - 1, e.RowIndex].Value),
        //                                _dayReportJJEntity.ConsultWirepoint);
        //                    }
        //                }
        //                //修改时计算方式
        //                else
        //                {
        //                    dgrdvDayReportJJ[e.ColumnIndex, e.RowIndex].Value =
        //                        Convert.ToDouble(dgrdvDayReportJJ[e.ColumnIndex - 1, e.RowIndex].Value) -
        //                        _dayReportJJEntity.JinChi + _dayReportJJEntity.DistanceFromWirepoint;
        //                }
        //            }
        //        }
        //    }
        //    //非第一行时
        //    else
        //    {
        //        //进尺单元格
        //        if (e.ColumnIndex == 2)
        //        {
        //            //非空
        //            if (dgrdvDayReportJJ[e.ColumnIndex, e.RowIndex].Value != null)
        //            {
        //                //验证输入是否为数字
        //                if (Validator.IsNumeric(dgrdvDayReportJJ[e.ColumnIndex, e.RowIndex].Value.ToString()))
        //                {
        //                    //添加时计算方式
        //                    if (Text == Const_MS.DAY_REPORT_JJ_ADD)
        //                    {
        //                        dgrdvDayReportJJ[e.ColumnIndex + 1, e.RowIndex].Value =
        //                            autoDistanceFromWirepoint(
        //                                Convert.ToDouble(dgrdvDayReportJJ[e.ColumnIndex + 1, e.RowIndex - 1].Value),
        //                                Convert.ToDouble(dgrdvDayReportJJ[e.ColumnIndex, e.RowIndex].Value),
        //                                Convert.ToInt32(dgrdvDayReportJJ[4, dgrdvDayReportJJ.CurrentRow.Index - 1].Value));
        //                    }
        //                }
        //                //不是数字时
        //                else
        //                {
        //                    Alert.alert(Const_MS.JC + Const.MSG_MUST_NUMBER + Const_MS.SIGN_EXCLAMATION_MARK);
        //                    //错误处理
        //                    //添加时处理方式
        //                    if (Text == Const_MS.DAY_REPORT_HC_ADD)
        //                    {
        //                        dgrdvDayReportJJ[e.ColumnIndex, e.RowIndex].Value = null;
        //                        dgrdvDayReportJJ[e.ColumnIndex + 1, e.RowIndex].Value = null;
        //                    }
        //                }
        //            }
        //        }
        //        //距参考导线点距离
        //        if (e.ColumnIndex == 3)
        //        {
        //            //为空
        //            if (dgrdvDayReportJJ[e.ColumnIndex, e.RowIndex].Value == null ||
        //                dgrdvDayReportJJ[e.ColumnIndex, e.RowIndex].Value.ToString().Trim() == "")
        //            {
        //                Alert.alert(Const_MS.DFW + Const.MSG_NOT_NULL);
        //                //添加时计算方式
        //                if (Text == Const_MS.DAY_REPORT_JJ_ADD)
        //                {
        //                    if (tunnelId > 0)
        //                    {
        //                        dgrdvDayReportJJ[e.ColumnIndex, e.RowIndex].Value =
        //                            autoDistanceFromWirepoint(_dayReportJJEntity.DistanceFromWirepoint,
        //                                Convert.ToDouble(dgrdvDayReportJJ[e.ColumnIndex - 1, e.RowIndex].Value),
        //                                _dayReportJJEntity.ConsultWirepoint);
        //                    }
        //                }
        //                //修改时计算方式
        //                else
        //                {
        //                    dgrdvDayReportJJ[e.ColumnIndex, e.RowIndex].Value =
        //                        Convert.ToDouble(dgrdvDayReportJJ[e.ColumnIndex - 1, e.RowIndex].Value) -
        //                        _dayReportJJEntity.JinChi + _dayReportJJEntity.DistanceFromWirepoint;
        //                }
        //            }
        //        }
        //    }
        //}

        //private void dgrdvDayReportJJ_RowEnter(object sender, DataGridViewCellEventArgs e)
        //{
        //    dgrdvDayReportJJ[1, e.RowIndex].Value = "掘进";
        //    if (e.RowIndex > 0 && dgrdvDayReportJJ[e.ColumnIndex, e.RowIndex].Value == null)
        //    {
        //        dgrdvDayReportJJ[4, e.RowIndex].Value = dgrdvDayReportJJ[4, e.RowIndex - 1].Value;
        //    }
        //}

        //private void dgrdvDayReportJJ_RowLeave(object sender, DataGridViewCellEventArgs e)
        //{
        //    if (dgrdvDayReportJJ[0, e.RowIndex].Value == null && dgrdvDayReportJJ[2, e.RowIndex].Value == null &&
        //        dgrdvDayReportJJ[3, e.RowIndex].Value == null && dgrdvDayReportJJ[4, e.RowIndex].Value == null &&
        //        dgrdvDayReportJJ[5, e.RowIndex].Value == null)
        //    {
        //        dgrdvDayReportJJ[1, e.RowIndex].Value = null;
        //    }
        //}

        /// <summary>
        ///     获取参考导线点的下一个导线点
        /// </summary>
        /// <param name="refPointID">参考导线点ID</param>
        /// <returns>下一个点的ID</returns>
        //private int getNextWirePointID(int refPointID)
        //{
        //    try
        //    {
        //        var wireInfo = Wire.FindOneByTunnelId(_tunnelEntity.TunnelId);
        //        if (wireInfo != null)
        //        {
        //            //获取导线上的所有导线点信息
        //            wirePoints = WirePoint.FindAllByWireId(wireInfo.WireId);
        //            if (wirePoints.Length < 2)
        //                return -1;

        //            int wirePointID = -1;
        //            for (int i = 0; i < wirePoints.Length; i++)
        //            {
        //                wirePointID = wirePoints[i].WirePointName;
        //                if (wirePointID == refPointID + 1) //参考导线点的下一个点
        //                    return wirePointID;
        //            }
        //        }
        //        return -1;
        //    }
        //    catch (Exception ex)
        //    {
        //        Trace.WriteLine("获取参考导线点下一个点ID出错:" + ex.Message);
        //        return -1;
        //    }
        //}

        //private void dgrdvDayReportJJ_CellLeave(object sender, DataGridViewCellEventArgs e)
        //{
        //    dgrdvDayReportJJ.EndEdit();
        //}
    }
}