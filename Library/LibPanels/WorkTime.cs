using System;
using System.Drawing;
using System.Windows.Forms;
using LibBusiness;
using LibCommon;
using LibEntity;
using LibSocket;

namespace LibPanels
{
    public partial class WorkTime : Form
    {
        #region ******变量声明******

        private WorkingTime[] _workingTimes38;
        private WorkingTime[] _workingTimes46;
        private readonly DateTimePicker _dtp = new DateTimePicker();
        #endregion

        /// <summary>
        /// 构造方法
        /// </summary>
        public WorkTime()
        {
            InitializeComponent();
            //修改窗体格式
            FormDefaultPropertiesSetter.SetEnteringFormDefaultProperties(this, Const_MS.WORK_TIME_MANAGEMENT);
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
            _dtp.TextChanged += dtp_TextChanged;
            dgrdvWorkTime.Controls.Add(_dtp);

            if (WorkingTimeDefault.FindFirst().DefaultWorkTimeGroupId == Const_MS.WORK_GROUP_ID_38)
            {
                rbtn38.Checked = true;
            }
            else
            {
                rbtn46.Checked = true;
            }
            _workingTimes38 = WorkingTime.FindAllBy38Times();
            _workingTimes46 = WorkingTime.FindAllBy46Times();
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
                Bind38();
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
                Bind46();
            }
        }

        /// <summary>
        /// 绑定三八制
        /// </summary>
        private void Bind38()
        {
            //设置datagridview数据行数
            dgrdvWorkTime.RowCount = _workingTimes38.Length;
            //插入数据
            for (int i = 0; i < _workingTimes38.Length; i++)
            {
                dgrdvWorkTime[0, i].Value = _workingTimes38[i].WorkTimeName;
                dgrdvWorkTime[1, i].Value = _workingTimes38[i].WorkTimeFrom;
                dgrdvWorkTime[2, i].Value = _workingTimes38[i].WorkTimeTo;
            }
        }

        /// <summary>
        /// 绑定四六制
        /// </summary>
        private void Bind46()
        {
            //设置datagridview行数
            dgrdvWorkTime.RowCount = _workingTimes46.Length;

            for (var i = 0; i < _workingTimes46.Length; i++)
            {
                dgrdvWorkTime[0, i].Value = _workingTimes46[i].WorkTimeName;
                dgrdvWorkTime[1, i].Value = _workingTimes46[i].WorkTimeFrom;
                dgrdvWorkTime[2, i].Value = _workingTimes46[i].WorkTimeTo;
            }
        }


        /// <summary>
        /// 更新表数据
        /// </summary>
        private void UpdateWorkTimeTable()
        {
            //选择三八制
            if (rbtn38.Checked)
            {
                //绑定三八制数据
                for (int i = 0; i < _workingTimes38.Length; i++)
                {
                    _workingTimes38[i].WorkTimeGroupId = Const_MS.WORK_GROUP_ID_38;
                    _workingTimes38[i].WorkTimeName = dgrdvWorkTime[0, i].Value.ToString();
                    _workingTimes38[i].WorkTimeFrom = Convert.ToDateTime(dgrdvWorkTime[1, i].Value);
                    _workingTimes38[i].WorkTimeTo = Convert.ToDateTime(dgrdvWorkTime[2, i].Value);
                    _workingTimes38[i].Save();
                }
            }
            else if (rbtn46.Checked)
            {
                //绑定四六制数据
                for (int i = 0; i < _workingTimes46.Length; i++)
                {
                    _workingTimes46[i].WorkTimeGroupId = Const_MS.WORK_GROUP_ID_46;
                    _workingTimes46[i].WorkTimeName = dgrdvWorkTime[0, i].Value.ToString();
                    _workingTimes46[i].WorkTimeFrom = Convert.ToDateTime(dgrdvWorkTime[1, i].Value);
                    _workingTimes46[i].WorkTimeTo = Convert.ToDateTime(dgrdvWorkTime[2, i].Value);
                    _workingTimes38[i].Save();
                }
            }
        }

        /// <summary>
        /// 提交
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnSubmit_Click(object sender, EventArgs e)
        {
            if (!Check()) return;
            //提交修改
            UpdateWorkTimeTable();
            // 通知server端，班次信息已经修改
            var msg = new UpdateWarningDataMsg(Const.INVALID_ID, Const.INVALID_ID,
              WorkingTime.TableName, OPERATION_TYPE.UPDATE, DateTime.Now)
            {
                CommandId = COMMAND_ID.UPDATE_WORK_TIME
            };
            SocketUtil.SendMsg2Server(msg);
            Close();
        }

        /// <summary>
        /// 取消
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnCancle_Click(object sender, EventArgs e)
        {
            //关闭窗体
            Close();
        }

        /// <summary>
        /// 设置为默认
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnSetAsDefault_Click(object sender, EventArgs e)
        {
            //设置默认工作制式
            var workingTime = new WorkingTimeDefault
            {
                DefaultWorkTimeGroupId = rbtn38.Checked ? Const_MS.WORK_GROUP_ID_38 : Const_MS.WORK_GROUP_ID_46
            };
            workingTime.Save();
            Alert.alert(Const_MS.WORK_TIME_MSG_CHANGE_DEFAULT_WORK_TIME_SUCCESS + (rbtn38.Checked ? Const_MS.WORK_TIME_38 : Const_MS.WORK_TIME_46));
        }

        /// <summary>
        /// 验证
        /// </summary>
        /// <returns></returns>
        private bool Check()
        {
            for (int i = 0; i < dgrdvWorkTime.RowCount; i++)
            {
                var cell = dgrdvWorkTime.Rows[i].Cells[0] as DataGridViewTextBoxCell;
                //判断班次名非空
                if (cell != null && Convert.ToString(cell.Value).Trim() == Const.DATA_NULL)
                {
                    cell.Style.BackColor = Const.ERROR_FIELD_COLOR;
                    Alert.alert(Const_MS.WORK_TIME_NAME + Const.MSG_NOT_NULL);
                    return false;
                }
                if (cell != null)
                {
                    cell.Style.BackColor = Const.NO_ERROR_FIELD_COLOR;
                    // 判断班次名特殊字符
                    if (Validator.checkSpecialCharacters(cell.Value.ToString()))
                    {
                        Alert.alert(Const_MS.WORK_TIME_NAME + Const.MSG_SP_CHAR);
                        cell.Style.BackColor = Const.ERROR_FIELD_COLOR;
                        return false;
                    }
                    cell.Style.BackColor = Const.NO_ERROR_FIELD_COLOR;
                }
                cell = dgrdvWorkTime.Rows[i].Cells[1] as DataGridViewTextBoxCell;
                //判断开始时间非空
                if (cell != null && cell.Value == null)
                {
                    cell.Style.BackColor = Const.ERROR_FIELD_COLOR;
                    Alert.alert(Const_MS.WORK_TIME_FROM + Const.MSG_NOT_NULL);
                    return false;
                }
                if (cell != null) cell.Style.BackColor = Const.NO_ERROR_FIELD_COLOR;
                cell = dgrdvWorkTime.Rows[i].Cells[2] as DataGridViewTextBoxCell;
                //判断结束时间非空
                if (cell != null && cell.Value == null)
                {
                    cell.Style.BackColor = Const.ERROR_FIELD_COLOR;
                    Alert.alert(Const_MS.WORK_TIME_TO + Const.MSG_NOT_NULL);
                    return false;
                }
                if (cell != null) cell.Style.BackColor = Const.NO_ERROR_FIELD_COLOR;
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
