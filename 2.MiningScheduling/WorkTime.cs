// ******************************************************************
// 概  述：工作制式添加修改
// 作  者：宋英杰
// 创建日期：2013/12/5
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
using LibPanels;
using LibSocket;
using LibCommonForm;
using LibCommonControl;

namespace _2.MiningScheduling
{
    public partial class WorkTime : BaseForm
    {
        #region ******变量声明******
        DataSet _ds38 = new DataSet();
        DataSet _ds46 = new DataSet();
        DateTimePicker _dtp = new DateTimePicker();
        #endregion

        /// <summary>
        /// 构造方法
        /// </summary>
        public WorkTime(MainFrm mainFrm)
        {
            InitializeComponent();

            this.MainForm = mainFrm;

            //修改窗体格式
            LibCommon.FormDefaultPropertiesSetter.SetEnteringFormDefaultProperties(this, Const_MS.WORK_TIME_MANAGEMENT);
        }

        /// <summary>
        /// 初始化
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void WorkTime_Load(object sender, EventArgs e)
        {
            //日期控件隐藏
            _dtp.Visible = false;

            //日期格式
            _dtp.Format = DateTimePickerFormat.Custom;
            _dtp.CustomFormat = Const_MS.WORK_TIME_FORMAT;
            //改变datetimepicker样式
            _dtp.ShowUpDown = true;

            //日期改变事件
            _dtp.TextChanged += new EventHandler(dtp_TextChanged);
            dgrdvWorkTime.Controls.Add(_dtp);

            if (WorkTimeBLL.getDefaultWorkTime() == Const_MS.WORK_TIME_38)
            {
                rbtn38.Checked = true;
            }
            else
            {
                rbtn46.Checked = true;
            }
        }

        /// <summary>
        /// 日期选择事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void dtp_TextChanged(object sender, EventArgs e)
        {
            //datagridview当前单元格内容替换
            dgrdvWorkTime.CurrentCell.Value = _dtp.Text;
        }

        /// <summary>
        /// 选择三八制
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void rbtn38_CheckedChanged(object sender, EventArgs e)
        {
            if (rbtn38.Checked)
            {
                //绑定三八制
                this.bind38();
            }
        }

        /// <summary>
        /// 选择四六制
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void rbtn46_CheckedChanged(object sender, EventArgs e)
        {
            if (rbtn46.Checked)
            {
                //绑定四六制
                this.bind46();
            }
        }

        /// <summary>
        /// 绑定三八制
        /// </summary>
        private void bind38()
        {
            //检查数据库中是否有38制的数据
            bool bResult = WorkTimeBLL.isWorkTime38Exist();
            //复制表结构与数据
            _ds38 = WorkTimeBLL.returnWorkTime38DS().Copy();
            //存在
            if (bResult)
            {
                //设置datagridview数据行数
                dgrdvWorkTime.RowCount = _ds38.Tables[0].Rows.Count;
                //插入数据
                for (int i = 0; i < _ds38.Tables[0].Rows.Count; i++)
                {
                    dgrdvWorkTime[0, i].Value = _ds38.Tables[0].Rows[i][WorkTimeDbConstNames.WORK_TIME_NAME].ToString();
                    dgrdvWorkTime[1, i].Value = _ds38.Tables[0].Rows[i][WorkTimeDbConstNames.WORK_TIME_FROM].ToString();
                    dgrdvWorkTime[2, i].Value = _ds38.Tables[0].Rows[i][WorkTimeDbConstNames.WORK_TIME_TO].ToString();
                }
            }
            //不存在
            else
            {
                //绑定默认数据
                this.bindDefault38();
            }
        }

        /// <summary>
        /// 绑定默认三八制
        /// </summary>
        private void bindDefault38()
        {
            dgrdvWorkTime.ColumnCount = 3;
            dgrdvWorkTime.RowCount = 3;
            dgrdvWorkTime[0, 0].Value = Const_MS.DEFAULT_TIME_MORNING;
            dgrdvWorkTime[1, 0].Value = Const_MS.DEFAULT_TIME_MORNING_FROM;
            dgrdvWorkTime[2, 0].Value = Const_MS.DEFAULT_TIME_MORNING_TO;
            dgrdvWorkTime[0, 1].Value = Const_MS.DEFAULT_TIME_NOON;
            dgrdvWorkTime[1, 1].Value = Const_MS.DEFAULT_TIME_NOON_FROM;
            dgrdvWorkTime[2, 1].Value = Const_MS.DEFAULT_TIME_NOON_TO;
            dgrdvWorkTime[0, 2].Value = Const_MS.DEFAULT_TIME_EVENING;
            dgrdvWorkTime[1, 2].Value = Const_MS.DEFAULT_TIME_EVENING_FROM;
            dgrdvWorkTime[2, 2].Value = Const_MS.DEFAULT_TIME_EVENING_TO;
        }

        /// <summary>
        /// 绑定四六制
        /// </summary>
        private void bind46()
        {
            //检查数据库中是否有46制数据
            bool bResult = WorkTimeBLL.isWorkTime46Exist();
            //复制表结构与数据
            _ds46 = WorkTimeBLL.returnWorkTime46DS().Copy();
            //存在
            if (bResult)
            {
                //设置datagridview行数
                dgrdvWorkTime.RowCount = _ds46.Tables[0].Rows.Count;
                //插入数据
                for (int i = 0; i < _ds46.Tables[0].Rows.Count; i++)
                {
                    dgrdvWorkTime[0, i].Value = _ds46.Tables[0].Rows[i][WorkTimeDbConstNames.WORK_TIME_NAME].ToString();
                    dgrdvWorkTime[1, i].Value = _ds46.Tables[0].Rows[i][WorkTimeDbConstNames.WORK_TIME_FROM].ToString();
                    dgrdvWorkTime[2, i].Value = _ds46.Tables[0].Rows[i][WorkTimeDbConstNames.WORK_TIME_TO].ToString();
                }
            }
            //不存在
            else
            {
                //绑定默认数据
                this.bindDefault46();
            }
        }

        /// <summary>
        /// 绑定默认四六制
        /// </summary>
        private void bindDefault46()
        {
            dgrdvWorkTime.ColumnCount = 3;
            dgrdvWorkTime.RowCount = 4;
            dgrdvWorkTime[0, 0].Value = Const_MS.DEFAULT_TIME_0;
            dgrdvWorkTime[1, 0].Value = Const_MS.DEFAULT_TIME_0_FROM;
            dgrdvWorkTime[2, 0].Value = Const_MS.DEFAULT_TIME_0_TO;
            dgrdvWorkTime[0, 1].Value = Const_MS.DEFAULT_TIME_6;
            dgrdvWorkTime[1, 1].Value = Const_MS.DEFAULT_TIME_6_FROM;
            dgrdvWorkTime[2, 1].Value = Const_MS.DEFAULT_TIME_6_TO;
            dgrdvWorkTime[0, 2].Value = Const_MS.DEFAULT_TIME_12;
            dgrdvWorkTime[1, 2].Value = Const_MS.DEFAULT_TIME_12_FROM;
            dgrdvWorkTime[2, 2].Value = Const_MS.DEFAULT_TIME_12_TO;
            dgrdvWorkTime[0, 3].Value = Const_MS.DEFAULT_TIME_18;
            dgrdvWorkTime[1, 3].Value = Const_MS.DEFAULT_TIME_18_FROM;
            dgrdvWorkTime[2, 3].Value = Const_MS.DEFAULT_TIME_18_TO;
        }

        /// <summary>
        /// 更新表数据
        /// </summary>
        private bool updateWorkTimeTable()
        {
            DataSet ds = new DataSet();
            //克隆表结构
            ds.Tables.Add(WorkTimeBLL.returnWorkTime38DS().Tables[0].Copy());

            ds.Tables[0].Rows.Clear();
            //选择三八制
            if (rbtn38.Checked)
            {
                //表中有三八制数据
                if (WorkTimeBLL.isWorkTime38Exist())
                {
                    //绑定三八制数据
                    for (int i = 0; i < dgrdvWorkTime.RowCount; i++)
                    {
                        ds.Tables[0].Rows.Add();
                        ds.Tables[0].Rows[i][WorkTimeDbConstNames.WORK_TIME_ID] = _ds38.Tables[0].Rows[i][WorkTimeDbConstNames.WORK_TIME_ID];
                        ds.Tables[0].Rows[i][WorkTimeDbConstNames.WORK_TIME_GROUP_ID] = Const_MS.WORK_GROUP_ID_38;
                        ds.Tables[0].Rows[i][WorkTimeDbConstNames.WORK_TIME_NAME] = dgrdvWorkTime[0, i].Value;
                        ds.Tables[0].Rows[i][WorkTimeDbConstNames.WORK_TIME_FROM] = Convert.ToDateTime(dgrdvWorkTime[1, i].Value).TimeOfDay;
                        ds.Tables[0].Rows[i][WorkTimeDbConstNames.WORK_TIME_TO] = Convert.ToDateTime(dgrdvWorkTime[2, i].Value).TimeOfDay;
                    }
                }
                //表中无三八制数据
                else
                {
                    //绑定默认三八制数据
                    for (int i = 0; i < dgrdvWorkTime.RowCount; i++)
                    {
                        ds.Tables[0].Rows.Add();
                        ds.Tables[0].Rows[i][WorkTimeDbConstNames.WORK_TIME_GROUP_ID] = Const_MS.WORK_GROUP_ID_38;
                        ds.Tables[0].Rows[i][WorkTimeDbConstNames.WORK_TIME_NAME] = dgrdvWorkTime[0, i].Value;
                        ds.Tables[0].Rows[i][WorkTimeDbConstNames.WORK_TIME_FROM] = Convert.ToDateTime(dgrdvWorkTime[1, i].Value).TimeOfDay;
                        ds.Tables[0].Rows[i][WorkTimeDbConstNames.WORK_TIME_TO] = Convert.ToDateTime(dgrdvWorkTime[2, i].Value).TimeOfDay;
                    }
                }
            }
            //选择四六制
            if (rbtn46.Checked)
            {
                //表中有四六制数据
                if (WorkTimeBLL.isWorkTime46Exist())
                {
                    //绑定四六制数据
                    for (int i = 0; i < dgrdvWorkTime.RowCount; i++)
                    {
                        ds.Tables[0].Rows.Add();
                        ds.Tables[0].Rows[i][WorkTimeDbConstNames.WORK_TIME_ID] = _ds46.Tables[0].Rows[i][WorkTimeDbConstNames.WORK_TIME_ID];
                        ds.Tables[0].Rows[i][WorkTimeDbConstNames.WORK_TIME_GROUP_ID] = Const_MS.WORK_GROUP_ID_46;
                        ds.Tables[0].Rows[i][WorkTimeDbConstNames.WORK_TIME_NAME] = dgrdvWorkTime[0, i].Value;
                        ds.Tables[0].Rows[i][WorkTimeDbConstNames.WORK_TIME_FROM] = Convert.ToDateTime(dgrdvWorkTime[1, i].Value).TimeOfDay;
                        ds.Tables[0].Rows[i][WorkTimeDbConstNames.WORK_TIME_TO] = Convert.ToDateTime(dgrdvWorkTime[2, i].Value).TimeOfDay;
                    }
                }
                else
                //表中无四六制数据
                {
                    for (int i = 0; i < dgrdvWorkTime.RowCount; i++)
                    {
                        //绑定默认四六制数据
                        ds.Tables[0].Rows.Add();
                        ds.Tables[0].Rows[i][WorkTimeDbConstNames.WORK_TIME_GROUP_ID] = Const_MS.WORK_GROUP_ID_46;
                        ds.Tables[0].Rows[i][WorkTimeDbConstNames.WORK_TIME_NAME] = dgrdvWorkTime[0, i].Value;
                        ds.Tables[0].Rows[i][WorkTimeDbConstNames.WORK_TIME_FROM] = Convert.ToDateTime(dgrdvWorkTime[1, i].Value).TimeOfDay;
                        ds.Tables[0].Rows[i][WorkTimeDbConstNames.WORK_TIME_TO] = Convert.ToDateTime(dgrdvWorkTime[2, i].Value).TimeOfDay;
                    }
                }
            }
            bool bResult = WorkTimeBLL.insertWorkTime(ds);
            return bResult;
        }

        /// <summary>
        /// 提交
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnSubmit_Click(object sender, EventArgs e)
        {
            if (check())
            {
                //提交修改
                bool bResult = updateWorkTimeTable();
                if (bResult)
                {
                    this.Close();

                    // 通知server端，班次信息已经修改
                    UpdateWarningDataMsg msg = new UpdateWarningDataMsg(Const.INVALID_ID, Const.INVALID_ID,
                        WorkTimeDbConstNames.TABLE_NAME, OPERATION_TYPE.UPDATE, DateTime.Now);
                    msg.CommandId = COMMAND_ID.UPDATE_WORK_TIME;
                    this.MainForm.SendMsg2Server(msg);
                }
            }
        }

        /// <summary>
        /// 取消
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnCancle_Click(object sender, EventArgs e)
        {
            //关闭窗体
            this.Close();
        }

        /// <summary>
        /// 设置为默认
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnSetAsDefault_Click(object sender, EventArgs e)
        {
            //设置默认工作制式
            if (WorkTimeBLL.setDefaultWorkTime(rbtn38.Checked ? Const_MS.WORK_TIME_38 : Const_MS.WORK_TIME_46))
            {
                Alert.alert(Const_MS.WORK_TIME_MSG_CHANGE_DEFAULT_WORK_TIME_SUCCESS + (rbtn38.Checked ? Const_MS.WORK_TIME_38 : Const_MS.WORK_TIME_46));
            }
        }

        /// <summary>
        /// 验证
        /// </summary>
        /// <returns></returns>
        private bool check()
        {
            for (int i = 0; i < this.dgrdvWorkTime.RowCount; i++)
            {
                DataGridViewTextBoxCell cell = dgrdvWorkTime.Rows[i].Cells[0] as DataGridViewTextBoxCell;
                //判断班次名非空
                if (Convert.ToString(cell.Value).Trim() == Const.DATA_NULL)
                {
                    cell.Style.BackColor = Const.ERROR_FIELD_COLOR;
                    Alert.alert(Const_MS.WORK_TIME_NAME + Const.MSG_NOT_NULL);
                    return false;
                }
                else
                {
                    cell.Style.BackColor = Const.NO_ERROR_FIELD_COLOR;
                }
                // 判断班次名特殊字符
                if (Validator.checkSpecialCharacters(cell.Value.ToString()))
                {
                    Alert.alert(Const_MS.WORK_TIME_NAME + Const.MSG_SP_CHAR);
                    cell.Style.BackColor = Const.ERROR_FIELD_COLOR;
                    return false;
                }
                else
                {
                    cell.Style.BackColor = Const.NO_ERROR_FIELD_COLOR;
                }
                cell = dgrdvWorkTime.Rows[i].Cells[1] as DataGridViewTextBoxCell;
                //判断开始时间非空
                if (cell.Value == null)
                {
                    cell.Style.BackColor = Const.ERROR_FIELD_COLOR;
                    Alert.alert(Const_MS.WORK_TIME_FROM + Const.MSG_NOT_NULL);
                    return false;
                }
                else
                {
                    cell.Style.BackColor = Const.NO_ERROR_FIELD_COLOR;
                }
                cell = dgrdvWorkTime.Rows[i].Cells[2] as DataGridViewTextBoxCell;
                //判断结束时间非空
                if (cell.Value == null)
                {
                    cell.Style.BackColor = Const.ERROR_FIELD_COLOR;
                    Alert.alert(Const_MS.WORK_TIME_TO + Const.MSG_NOT_NULL);
                    return false;
                }
                else
                {
                    cell.Style.BackColor = Const.NO_ERROR_FIELD_COLOR;
                }
            }
            return true;
        }

        /// <summary>
        /// datagridview单元格进入事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void dgrdvWorkTime_CellEnter(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == 1 || e.ColumnIndex == 2)
            {
                //获取当前单元格区域
                Rectangle rect = dgrdvWorkTime.GetCellDisplayRectangle(e.ColumnIndex, e.RowIndex, false);
                _dtp.Visible = true;
                _dtp.Top = rect.Top;
                _dtp.Left = rect.Left;
                _dtp.Height = rect.Height;
                _dtp.Width = rect.Width;

                //datetimepicker获取焦点
                _dtp.Focus();

                //datetimepicker显示值设置
                if (dgrdvWorkTime[e.ColumnIndex, e.RowIndex].Value != null)
                {
                    _dtp.Value = Convert.ToDateTime(dgrdvWorkTime[e.ColumnIndex, e.RowIndex].Value.ToString());
                }
                else
                {
                    _dtp.Value = DateTime.Now;
                    dgrdvWorkTime[e.ColumnIndex, e.RowIndex].Value = _dtp.Text;
                }
            }
        }

        /// <summary>
        /// datagridview单元格离开事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void dgrdvWorkTime_CellLeave(object sender, DataGridViewCellEventArgs e)
        {
            //隐藏datetimepicker
            _dtp.Visible = false;
        }

        /// <summary>
        /// 返回当前时间对应的班次名
        /// </summary>
        /// <param name="workStyle">工作制式名</param>
        /// <returns>班次名</returns>
        public static string returnSysWorkTime(string workStyle)
        {
            //获取班次
            DataSet ds = WorkTimeBLL.returnWorkTime(workStyle);
            //小时
            int hour = DateTime.Now.Hour;
            string workTime = "";
            for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
            {
                //对比小时
                if (hour > Convert.ToInt32(ds.Tables[0].Rows[i][WorkTimeDbConstNames.WORK_TIME_FROM].ToString().Remove(2)) && hour <= Convert.ToInt32(ds.Tables[0].Rows[i][WorkTimeDbConstNames.WORK_TIME_TO].ToString().Remove(2)))
                {
                    //获取当前时间对应班次
                    workTime = ds.Tables[0].Rows[i][WorkTimeDbConstNames.WORK_TIME_NAME].ToString();
                }
            }
            return workTime;
        }

        /// <summary>
        /// 恢复初始设置
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnReset_Click(object sender, EventArgs e)
        {
            //TODO:恢复初始设置
            //确认
            //if (Alert.confirm(Const_MS.WORK_TIME_MSG_IS_TRUNCATE))
            //{
            //    //清空班次表与默认班次表
            //    WorkTimeBLL.truncateWorkTime();

            //    rbtn38.Checked = true;

            //    updateWorkTimeTable();

            //    rbtn46.Checked = true;

            //    updateWorkTimeTable();

            //    //选择46制
            //    rbtn46.Checked = true;

            //    //设置默认工作制式
            //    WorkTimeBLL.setDefaultWorkTime(Const_MS.WORK_TIME_46);
            //}
        }
    }
}
