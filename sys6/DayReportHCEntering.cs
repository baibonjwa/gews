// ******************************************************************
// 概  述：回采日报添加修改
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
using LibCommonForm;
using LibEntity;
using LibCommon;
using LibSocket;

namespace UnderTerminal
{
    public partial class DayReportHCEntering : Form
    {
        #region ******变量声明******
        /**巷道实体**/
        Tunnel _tunnelEntity = new Tunnel();
        UnderMessageWindow mainWin;
        /**回采日报实体**/
        DayReportHc _dayReportHCEntity = new DayReportHc();
        /**巷道关联矿井等信息ID集合**/
        int[] _arr;
        int tunnelId = -1;
        private int workingfaceId = -1;
        #endregion ******变量声明******


        const int C_WORK_TIME = 0;     // 班次
        const int C_WORK_CONTENT = 1;     // 工作内容
        const int C_WORK_PROGRESS = 2;     // 进尺
        const int C_COMMENTS = 3;     // 备注


        /// <summary>
        /// 构造方法
        /// </summary>
        public DayReportHCEntering(int tunnelId, string tunnelName, UnderMessageWindow win)
        {
            InitializeComponent();
            this.tunnelId = tunnelId;
            this.Text += "-" + tunnelName;
            this.mainWin = win;
            addInfo();
            workingfaceId = BasicInfoManager.getInstance().getTunnelByID(tunnelId).WorkingFace.WorkingFaceId;
            //自定义控件初始化
            LibEntity.TunnelDefaultSelect tunnelDefaultSelectEntity = LibBusiness.TunnelDefaultSelect.selectDefaultTunnel(DayReportHc.TableName);

        }

        /// <summary>
        /// 委托事件
        /// </summary>
        /// <param name="sender"></param>
        private void InheritTunnelNameChanged(object sender, TunnelEventArgs e)
        {
            for (int i = 0; i < dgrdvDayReportHC.RowCount; i++)
            {
                if (dgrdvDayReportHC[2, i].Value != null)
                {
                    DataGridViewCellEventArgs dgvce = new DataGridViewCellEventArgs(2, i);
                    dgrdvDayReportHC_CellEndEdit(sender, dgvce);
                }
            }
        }

        /// <summary>
        /// 添加时加载初始化设置
        /// </summary>
        private void addInfo()
        {
            dgrdvDayReportHC[1, 0].Value = "回采";
            //日期
            dtpDate.Value = DateTime.Now.Date;
            //绑定队别名称
            this.bindTeamInfo();
            //初始化班次
            this.bindWorkTimeFirstTime();
            //设置为默认工作制式
            if (WorkTimeBLL.getDefaultWorkTime() == Const_MS.WORK_TIME_38)
            {
                rbtn38.Checked = true;
            }
            else
            {
                rbtn46.Checked = true;
            }
            //设置班次为当前时间对应的班次
            dgrdvDayReportHC[0, 0].Value = Utils.returnSysWorkTime(rbtn38.Checked ? Const_MS.WORK_TIME_38 : Const_MS.WORK_TIME_46);
        }

        /// <summary>
        /// 修改时加载初始化设置
        /// </summary>
        private void changeInfo()
        {
            //绑定默认信息
            addInfo();
            //绑定修改数据
            bindInfo();
        }

        /// <summary>
        /// datagridview绑定信息
        /// </summary>
        private void bindInfo()
        {
            //巷道ID
            _tunnelEntity.TunnelId = _arr[4];

            //巷道实体
            _tunnelEntity = BasicInfoManager.getInstance().getTunnelByID(tunnelId);

            //日期
            dtpDate.Value = _dayReportHCEntity.DateTime;

            //工作制式
            if (_dayReportHCEntity.WorkTimeStyle == Const_MS.WORK_TIME_38)
            {
                rbtn38.Checked = true; ;
            }
            if (_dayReportHCEntity.WorkTimeStyle == Const_MS.WORK_TIME_46)
            {
                rbtn46.Checked = true;
            }

            //队别
            cboTeamName.SelectedValue = _dayReportHCEntity.Team;

            //绑定队别成员
            DataBindUtil.LoadTeamMemberByTeamName(cboSubmitter, cboTeamName.Text);

            //填报人
            cboSubmitter.Text = _dayReportHCEntity.Submitter;
            dgrdvDayReportHC.Rows.Add();
            dgrdvDayReportHC[0, 0].Value = _dayReportHCEntity.WorkTime;
            dgrdvDayReportHC[1, 0].Value = _dayReportHCEntity.WorkInfo;
            dgrdvDayReportHC[2, 0].Value = _dayReportHCEntity.JinChi;
            //TODO
            //dgrdvDayReportHC[3, 0].Value = _dayReportHCEntity.OpenOffCutDistance;
            dgrdvDayReportHC[4, 0].Value = _dayReportHCEntity.Remarks;
        }

        /// <summary>
        /// 绑定队别名称
        /// </summary>
        private void bindTeamInfo()
        {
            cboTeamName.Items.Clear();
            Team[] team = Team.FindAll();
            foreach (Team t in team)
            {
                cboTeamName.Items.Add(t.TeamName);
            }
        }


        /// <summary>
        /// 自动填充距切眼距离
        /// </summary>
        /// <param name="lastOpenOffCutDistance">上次距切眼距离</param>
        /// <param name="HCJC">回采进尺</param>
        /// <returns>本次距切眼距离</returns>
        private double autoOpenOffCutDistance(double lastOpenOffCutDistance, double HCJC)
        {
            return HCJC + lastOpenOffCutDistance;
        }

        /// <summary>
        /// 队别选择事件（根据队别绑定队员）
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cboTeamName_SelectedIndexChanged(object sender, EventArgs e)
        {
            //this.bindTeamMember();
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
        /// 提交按钮事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnSubmit_Click(object sender, EventArgs e)
        {
            // 验证
            if (!this.check())
            {
                DialogResult = DialogResult.None;
                return;
            }
            DialogResult = DialogResult.OK;
            _tunnelEntity.TunnelId =
                workingfaceId;
            _tunnelEntity = BasicInfoManager.getInstance().getTunnelByID(tunnelId);
            //if (this.Text == Const_MS.DAY_REPORT_HC_ADD)
            //{
            //TunnelDefaultSelect.InsertDefaultTunnel(DayReportHCDbConstNames.TABLE_NAME, tunnelId);
            insertDayReportHCInfo();
            //}
            //if (this.Text == Const_MS.DAY_REPORT_HC_CHANGE)
            //{
            //    DayReportHCEntity oldDayReportHCEntity = _dayReportHCEntity;      //修改前实体
            //    TunnelDefaultSelect.UpdateDefaultTunnel(DayReportHCDbConstNames.TABLE_NAME, selectTunnelSimple1.ITunnelId);
            //    updateDayReportHCInfo();
            //}

            #region 通知服务器预警数据已更新
            //外层函数已执行通知服务器操作
            //UpdateWarningDataMsg msg = new UpdateWarningDataMsg(_tunnelEntity.Tunnel, DayReportHCDbConstNames.TABLE_NAME, OPERATION_TYPE.ADD, dtpDate.Value);
            //mainWin.SendMsg2Server(msg);
            #endregion
        }

        /// <summary>
        /// 添加回采日报
        /// </summary>
        private void insertDayReportHCInfo()
        {
            List<DayReportHc> dayReportHCEntityList = new List<DayReportHc>();
            for (int i = 0; i < this.dgrdvDayReportHC.RowCount; i++)
            {
                DayReportHc dayReportHCEntity = new DayReportHc();
                // 最后一行为空行时，跳出循环
                if (i == this.dgrdvDayReportHC.RowCount - 1)
                {
                    break;
                }

                /**回采日报实体赋值**/
                //队别名称
                //TODO:
                dayReportHCEntity.Team = null;
                //绑定巷道编号
                //TODO:
                dayReportHCEntity.WorkingFace = null;
                //日期
                dayReportHCEntity.DateTime = dtpDate.Value;
                //填报人
                dayReportHCEntity.Submitter = cboSubmitter.Text;
                //工作制式
                if (rbtn38.Checked)
                {
                    dayReportHCEntity.WorkTimeStyle = rbtn38.Text;
                }
                if (rbtn46.Checked)
                {
                    dayReportHCEntity.WorkTimeStyle = rbtn46.Text;
                }
                //班次
                if (this.dgrdvDayReportHC.Rows[i].Cells[C_WORK_TIME].Value != null)
                {
                    dayReportHCEntity.WorkTime = this.dgrdvDayReportHC.Rows[i].Cells[C_WORK_TIME].Value.ToString();
                }
                //工作内容
                if (this.dgrdvDayReportHC.Rows[i].Cells[C_WORK_CONTENT].Value != null)
                {
                    dayReportHCEntity.WorkInfo = this.dgrdvDayReportHC.Rows[i].Cells[C_WORK_CONTENT].Value.ToString();
                }
                //回采进尺
                if (this.dgrdvDayReportHC.Rows[i].Cells[C_WORK_PROGRESS].Value != null)
                {
                    dayReportHCEntity.JinChi = Convert.ToDouble(this.dgrdvDayReportHC.Rows[i].Cells[C_WORK_PROGRESS].Value);
                }
                //距切眼距离
                //if (this.dgrdvDayReportHC.Rows[i].Cells[3].Value != null)
                //{
                //    //TODO
                //    //dayReportHCEntity.OpenOffCutDistance = Convert.ToDouble(this.dgrdvDayReportHC.Rows[i].Cells[3].Value);
                //}
                //备注
                if (this.dgrdvDayReportHC.Rows[i].Cells[C_COMMENTS].Value != null)
                {
                    dayReportHCEntity.Remarks = this.dgrdvDayReportHC.Rows[i].Cells[C_COMMENTS].Value.ToString();
                }
                //BID
                dayReportHCEntity.BindingId = IDGenerator.NewBindingID();

                //添加到dayReportHCEntityList中
                dayReportHCEntityList.Add(dayReportHCEntity);
            }

            bool bResult = false;

            //循环添加
            foreach (DayReportHc dayReportHCEntity in dayReportHCEntityList)
            {
                //添加回采进尺日报
                dayReportHCEntity.SaveAndFlush();
                bResult = true;
                if (bResult)
                {
                    var msg = new UpdateWarningDataMsg(this.mainWin.workingfaceId, tunnelId, DayReportHc.TableName, OPERATION_TYPE.ADD, dtpDate.Value);
                    mainWin.SendMsg2Server(msg);
                }
            }

            //添加失败
            if (!bResult)
            {
                Alert.alert(Const_MS.MSG_UPDATE_FAILURE);
                return;
            }
        }

        private void bindWorkTimeFirstTime()
        {
            DataSet dsWorkTime;
            //获取三八制班次
            if (rbtn38.Checked)
            {
                dsWorkTime = WorkTimeBLL.returnWorkTime(rbtn38.Text);
            }
            //获取四六制班次
            else
            {
                dsWorkTime = WorkTimeBLL.returnWorkTime(rbtn46.Text);
            }
            //向combobox里插入数据
            for (int i = 0; i < dsWorkTime.Tables[0].Rows.Count; i++)
            {
                cboWorkTime.Items.Add(dsWorkTime.Tables[0].Rows[i][WorkTimeDbConstNames.WORK_TIME_NAME].ToString());
            }
        }

        /// <summary>
        /// 三八制选择事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void rbtn38_CheckedChanged(object sender, EventArgs e)
        {
            //选择三八制
            if (rbtn38.Checked)
            {
                cboWorkTime.Items.Clear();
                //清空班次下拉框中选项

                DataSet dsWorkTime = WorkTimeBLL.returnWorkTime(rbtn38.Text);
                for (int j = 0; j < dsWorkTime.Tables[0].Rows.Count; j++)
                {
                    cboWorkTime.Items.Add(dsWorkTime.Tables[0].Rows[j][WorkTimeDbConstNames.WORK_TIME_NAME].ToString());
                }
                for (int i = 0; i < dgrdvDayReportHC.RowCount; i++)
                {
                    //清空班次
                    dgrdvDayReportHC[0, i].Value = "";

                    if (i == 0)
                    {
                        dgrdvDayReportHC[0, 0].Value = Utils.returnSysWorkTime(rbtn38.Text);
                    }
                    else
                    {
                        dgrdvDayReportHC[0, i].Value = null;
                    }
                }
            }
            //选择四六制
            else
            {
                cboWorkTime.Items.Clear();
                DataSet dsWorkTime = WorkTimeBLL.returnWorkTime(rbtn46.Text);
                for (int j = 0; j < dsWorkTime.Tables[0].Rows.Count; j++)
                {
                    cboWorkTime.Items.Add(dsWorkTime.Tables[0].Rows[j][WorkTimeDbConstNames.WORK_TIME_NAME].ToString());
                }
                for (int i = 0; i < dgrdvDayReportHC.RowCount; i++)
                {
                    dgrdvDayReportHC[0, i].Value = "";
                    if (i == 0)
                    {
                        dgrdvDayReportHC[0, 0].Value = Utils.returnSysWorkTime(rbtn46.Text);
                    }
                    else
                    {
                        dgrdvDayReportHC[0, i].Value = null;
                    }
                }
            }
        }

        /// <summary>
        /// 验证
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
            if (Validator.IsEmpty(this.cboTeamName.Text))
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
            if (this.dgrdvDayReportHC.Rows.Count - 1 == 0)
            {
                //添加时判断为未录入进尺
                if (this.Text == Const_MS.DAY_REPORT_HC_ADD)
                {
                    Alert.alert(Const.MSG_PLEASE_TYPE_IN + Const_MS.JC + Const.SIGN_EXCLAMATION_MARK);
                    return false;
                }
                //修改时
                else
                {
                    bool bResult = false;
                    //为空返回false，不数据时跳出循环
                    for (int i = 0; i < dgrdvDayReportHC.ColumnCount; i++)
                    {
                        if (dgrdvDayReportHC[i, 0].Value == null)
                        {
                            bResult = false;
                        }
                        else
                        {
                            bResult = true;
                            break;
                        }
                    }
                    if (!bResult)
                    {
                        Alert.alert(Const.MSG_PLEASE_TYPE_IN + Const_MS.JC + Const.SIGN_EXCLAMATION_MARK);
                        return bResult;
                    }
                }
            }
            for (int i = 0; i < this.dgrdvDayReportHC.RowCount; i++)
            {
                // 最后一行为空行时，跳出循环
                if (i == dgrdvDayReportHC.RowCount - 1)
                {
                    break;
                }
                DataGridViewTextBoxCell cell = dgrdvDayReportHC.Rows[i].Cells[C_WORK_PROGRESS] as DataGridViewTextBoxCell;
                //进尺为空
                if (cell.Value == null)
                {
                    cell.Style.BackColor = Const.ERROR_FIELD_COLOR;
                    Alert.alert(Const_MS.JC + Const.MSG_NOT_NULL + Const_MS.SIGN_EXCLAMATION_MARK);
                    return false;
                }
                else
                {
                    cell.Style.BackColor = Const.NO_ERROR_FIELD_COLOR;
                }
                //进尺不为数字
                if (!Validator.IsNumeric(cell.Value.ToString()))
                {
                    cell.Style.BackColor = Const.ERROR_FIELD_COLOR;
                    Alert.alert(Const_MS.JC + Const.MSG_MUST_NUMBER + Const_MS.SIGN_EXCLAMATION_MARK);
                    return false;
                }
                else
                {
                    cell.Style.BackColor = Const.NO_ERROR_FIELD_COLOR;
                }
                //cell = dgrdvDayReportHC.Rows[i].Cells[3] as DataGridViewTextBoxCell;
                //距切眼距离不能为空
                //if (cell.Value == null)
                //{
                //    cell.Style.BackColor = Const.ERROR_FIELD_COLOR;
                //    Alert.alert(Const_MS.OFC + Const.MSG_NOT_NULL + Const_MS.SIGN_EXCLAMATION_MARK);
                //    return false;
                //}
                //else
                //{
                //    cell.Style.BackColor = Const.NO_ERROR_FIELD_COLOR;
                //}
                //距切眼距离应为数字
                //if (!Validator.IsNumeric(cell.Value.ToString()))
                //{
                //    cell.Style.BackColor = Const.ERROR_FIELD_COLOR;
                //    Alert.alert(Const_MS.OFC + Const.MSG_MUST_NUMBER + Const_MS.SIGN_EXCLAMATION_MARK);
                //    return false;
                //}
                //else
                //{
                //    cell.Style.BackColor = Const.NO_ERROR_FIELD_COLOR;
                //}
                cell = dgrdvDayReportHC.Rows[i].Cells[C_COMMENTS] as DataGridViewTextBoxCell;
                //备注不能含特殊字符
                if (cell.Value != null)
                {
                    if (Validator.checkSpecialCharacters(cell.Value.ToString()))
                    {
                        cell.Style.BackColor = Const.ERROR_FIELD_COLOR;
                        Alert.alert(Const_MS.OTHER + Const.MSG_SP_CHAR + Const_MS.SIGN_EXCLAMATION_MARK);
                        return false;
                    }
                    else
                    {
                        cell.Style.BackColor = Const.NO_ERROR_FIELD_COLOR;
                    }
                }
            }
            //验证成功
            return true;
        }

        /// <summary>
        /// 进尺输入计算距切眼距离
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void dgrdvDayReportHC_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            //第一行时
            if (e.RowIndex == 0)
            {
                //进尺单元格
                if (e.ColumnIndex == 2)
                {
                    //非空
                    if (dgrdvDayReportHC[e.ColumnIndex, e.RowIndex].Value != null)
                    {
                        //验证输入是否为数字
                        if (Validator.IsNumeric(dgrdvDayReportHC[e.ColumnIndex, e.RowIndex].Value.ToString()))
                        {
                            //添加时计算方式
                            if (this.Text == Const_MS.DAY_REPORT_HC_ADD)
                            {
                                if (tunnelId >= 0)
                                {
                                    dgrdvDayReportHC[e.ColumnIndex + 1, e.RowIndex].Value = autoOpenOffCutDistance(0, Convert.ToDouble(dgrdvDayReportHC[e.ColumnIndex, e.RowIndex].Value));
                                }
                            }
                            //修改时计算方式
                            else
                            {
                                //TODO
                                //dgrdvDayReportHC[e.ColumnIndex + 1, e.RowIndex].Value = Convert.ToDouble(dgrdvDayReportHC[e.ColumnIndex, e.RowIndex].Value) - _dayReportHCEntity.JinChi + _dayReportHCEntity.OpenOffCutDistance;
                            }
                        }
                        //不是数字时
                        else
                        {
                            Alert.alert(Const_MS.JC + Const.MSG_MUST_NUMBER + Const_MS.SIGN_EXCLAMATION_MARK);
                            //错误处理
                            //添加时处理方式
                            if (this.Text == Const_MS.DAY_REPORT_HC_ADD)
                            {
                                dgrdvDayReportHC[e.ColumnIndex, e.RowIndex].Value = null;
                                dgrdvDayReportHC[e.ColumnIndex + 1, e.RowIndex].Value = null;
                            }
                            //修改时处理方式
                            else
                            {
                                dgrdvDayReportHC[e.ColumnIndex, e.RowIndex].Value = _dayReportHCEntity.JinChi;
                                //TODO
                                //dgrdvDayReportHC[e.ColumnIndex + 1, e.RowIndex].Value = _dayReportHCEntity.OpenOffCutDistance;
                            }
                        }
                    }
                }
                //距切眼距离单元格
                if (e.ColumnIndex == 3)
                {
                    //为空
                    if (dgrdvDayReportHC[e.ColumnIndex, e.RowIndex].Value == null || dgrdvDayReportHC[e.ColumnIndex, e.RowIndex].Value.ToString().Trim() == "")
                    {
                        Alert.alert(Const_MS.OFC + Const.MSG_NOT_NULL);
                        //添加时计算方式
                        if (this.Text == Const_MS.DAY_REPORT_HC_ADD)
                        {
                            if (tunnelId >= 0)
                            {
                                dgrdvDayReportHC[e.ColumnIndex, e.RowIndex].Value = autoOpenOffCutDistance(0, Convert.ToDouble(dgrdvDayReportHC[e.ColumnIndex - 1, e.RowIndex].Value));
                            }
                        }
                        //修改时计算方式
                        else
                        {
                            //TODO
                            //dgrdvDayReportHC[e.ColumnIndex, e.RowIndex].Value = Convert.ToDouble(dgrdvDayReportHC[e.ColumnIndex - 1, e.RowIndex].Value) - _dayReportHCEntity.JinChi + _dayReportHCEntity.OpenOffCutDistance;
                        }

                    }
                }
            }
            //非第一行时
            else
            {
                //进尺单元格
                if (e.ColumnIndex == 2)
                {
                    //非空
                    if (dgrdvDayReportHC[e.ColumnIndex, e.RowIndex].Value != null)
                    {
                        //验证输入是否为数字
                        if (Validator.IsNumeric(dgrdvDayReportHC[e.ColumnIndex, e.RowIndex].Value.ToString()))
                        {
                            //添加时计算方式
                            if (this.Text == Const_MS.DAY_REPORT_HC_ADD)
                            {
                                dgrdvDayReportHC[e.ColumnIndex + 1, e.RowIndex].Value = autoOpenOffCutDistance(Convert.ToDouble(dgrdvDayReportHC[e.ColumnIndex + 1, e.RowIndex - 1].Value), Convert.ToDouble(dgrdvDayReportHC[e.ColumnIndex, e.RowIndex].Value));
                            }
                        }
                        //不是数字时
                        else
                        {
                            Alert.alert(Const_MS.JC + Const.MSG_MUST_NUMBER + Const_MS.SIGN_EXCLAMATION_MARK);
                            //错误处理
                            //添加时处理方式
                            if (this.Text == Const_MS.DAY_REPORT_HC_ADD)
                            {
                                dgrdvDayReportHC[e.ColumnIndex, e.RowIndex].Value = null;
                                dgrdvDayReportHC[e.ColumnIndex + 1, e.RowIndex].Value = null;
                            }
                        }
                    }
                }
                //距切眼距离单元格
                if (e.ColumnIndex == 3)
                {
                    //为空
                    if (dgrdvDayReportHC[e.ColumnIndex, e.RowIndex].Value == null || dgrdvDayReportHC[e.ColumnIndex, e.RowIndex].Value.ToString().Trim() == "")
                    {
                        Alert.alert(Const_MS.OFC + Const.MSG_NOT_NULL);
                        //添加时计算方式
                        if (this.Text == Const_MS.DAY_REPORT_HC_ADD)
                        {
                            if (tunnelId >= 0)
                            {
                                dgrdvDayReportHC[e.ColumnIndex, e.RowIndex].Value = autoOpenOffCutDistance(0, Convert.ToDouble(dgrdvDayReportHC[e.ColumnIndex - 1, e.RowIndex].Value));
                            }
                        }
                        //修改时计算方式
                        else
                        {
                            //TODO
                            //dgrdvDayReportHC[e.ColumnIndex, e.RowIndex].Value = Convert.ToDouble(dgrdvDayReportHC[e.ColumnIndex - 1, e.RowIndex].Value) - _dayReportHCEntity.JinChi + _dayReportHCEntity.OpenOffCutDistance;
                        }

                    }
                }
            }

        }


        private void cboTeamName_TextChanged(object sender, EventArgs e)
        {
            for (int i = 0; i < cboTeamName.Items.Count; i++)
            {
                if (cboTeamName.Text == cboTeamName.GetItemText(cboTeamName.Items[i]))
                {
                    DataBindUtil.LoadTeamMemberByTeamName(cboSubmitter, cboTeamName.Text);
                }
                else
                {
                    cboSubmitter.Items.Clear();
                    cboSubmitter.Text = "";
                }
            }
        }

        private void dgrdvDayReportHC_RowEnter(object sender, DataGridViewCellEventArgs e)
        {
            dgrdvDayReportHC[1, e.RowIndex].Value = "回采";
        }

        private void dgrdvDayReportHC_RowLeave(object sender, DataGridViewCellEventArgs e)
        {
            if (dgrdvDayReportHC[0, e.RowIndex].Value == null && dgrdvDayReportHC[2, e.RowIndex].Value == null && dgrdvDayReportHC[3, e.RowIndex].Value == null && dgrdvDayReportHC[4, e.RowIndex].Value == null)
            {
                dgrdvDayReportHC[1, e.RowIndex].Value = null;
            }
        }

        private void dgrdvDayReportHC_CellLeave(object sender, DataGridViewCellEventArgs e)
        {
            dgrdvDayReportHC.EndEdit();
        }
    }
}
