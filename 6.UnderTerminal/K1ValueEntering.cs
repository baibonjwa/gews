// ******************************************************************
// 概  述：K1值信息添加修改
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
using LibCommonForm;
using LibEntity;
using LibBusiness;
using LibCommon;
using LibSocket;

namespace UnderTerminal
{
    public partial class K1ValueEntering : Form
    {
        #region ******各种变量定义******
        UnderMessageWindow mainWin;
        K1Value _k1ValueEntity = new K1Value();
        K1Value[] k1Entitys;
        Tunnel tunnelEntity = new Tunnel();
        //巷道控件用数组
        int[] arr = new int[5];
        /// <summary>
        /// K1Value分组数
        /// </summary>
        int groupCount = 0;
        /// <summary>
        /// 数据行数
        /// </summary>
        int rowCount = 0;
        DateTimePicker dtp = new DateTimePicker();
        int pointX = -1;
        int pointY = -1;
        double tmpDouble = 0;
        int nowDoing = 0;
        int tunnelId = -1;
        #endregion

        public String X { get; set; }
        public String Y { get; set; }
        public String Z { get; set; }

        /// <summary>
        /// 添加
        /// <param name="tunnelId">巷道id</param>
        /// </summary>
        public K1ValueEntering(int tunnelId, string tunnelName, UnderMessageWindow win)
        {
            InitializeComponent();
            this.tunnelId = tunnelId;
            this.Text = tunnelName;
            this.mainWin = win;
            Tunnel entTunnel = BasicInfoManager.getInstance().getTunnelByID(tunnelId);

            X = entTunnel.WorkingFace.Coordinate.X.ToString();
            Y = entTunnel.WorkingFace.Coordinate.Y.ToString();
            Z = entTunnel.WorkingFace.Coordinate.Z.ToString();

            tbCoordinateX.Text = X;
            tbCoordinateY.Text = Y;
            tbCoordinateZ.Text = Z;

            //dgrdvK1Value.Columns[0].
            //txtCoordinateY.Text = entWorkingFace.CoordinateY.ToString();
            //txtCoordinateZ.Text = entWorkingFace.CoordinateZ.ToString();
        }

        /// <summary>
        /// 初始化
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void K1Value_Load(object sender, EventArgs e)
        {
            //设置Datagridview样式
            setDataGridViewStyle();

            dtp.Visible = false;
            dtp.Format = DateTimePickerFormat.Custom;
            dtp.CustomFormat = Const.DATE_FORMART_H_MM_SS;
            dtp.TextChanged += dtp_TextChanged;
            dgrdvK1Value.Controls.Add(dtp);
        }

        #region ******控制datagridview样式******
        /// <summary>
        /// 设置Datagridview样式
        /// </summary>
        private void setDataGridViewStyle()
        {
            dgrdvK1Value.AutoGenerateColumns = true;
            dgrdvK1Value.AllowUserToAddRows = true;
        }
        #endregion

        /// <summary>
        /// 提交按钮事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnSubmit_Click(object sender, EventArgs e)
        {
            //验证
            if (!check())
            {
                this.DialogResult = DialogResult.None;
                return;
            }
            this.DialogResult = DialogResult.OK;

            tunnelEntity.TunnelID = this.tunnelId;
            tunnelEntity = BasicInfoManager.getInstance().getTunnelByID(tunnelId);
            //添加
            //if (this.Text == Const_OP.K1_VALUE_ADD)
            //{
            insertK1Value();
            //}
        }

        /// <summary>
        /// 添加K1Value值
        /// </summary>
        private void insertK1Value()
        {
            rowCount = dgrdvK1Value.Rows.Count - 1;
            groupCount = K1ValueBLL.selectValueK1GroupCount();
            bool bResult = false;
            for (int i = 0; i < rowCount; i++)
            {

                _k1ValueEntity.K1ValueID = groupCount + 1;

                if (!String.IsNullOrEmpty(tbCoordinateX.Text))
                    _k1ValueEntity.CoordinateX = Convert.ToDouble(tbCoordinateX.Text);

                if (!String.IsNullOrEmpty(tbCoordinateY.Text))
                    _k1ValueEntity.CoordinateY = Convert.ToDouble(tbCoordinateY.Text);

                if (!String.IsNullOrEmpty(tbCoordinateZ.Text))
                    _k1ValueEntity.CoordinateZ = Convert.ToDouble(tbCoordinateZ.Text);

                if (dgrdvK1Value[0, i].Value != null)
                {
                    if (double.TryParse(dgrdvK1Value[0, i].Value.ToString(), out tmpDouble))
                    {
                        _k1ValueEntity.ValueK1Dry = tmpDouble;
                        tmpDouble = 0;
                    }
                }
                //_k1ValueEntity.ValueK1Dry = Convert.ToDouble(dgrdvK1Value[0, i].Value);

                if (dgrdvK1Value[1, i].Value != null)
                {
                    if (double.TryParse(dgrdvK1Value[1, i].Value.ToString(), out tmpDouble))
                    {
                        _k1ValueEntity.ValueK1Wet = tmpDouble;
                        tmpDouble = 0;
                    }
                }

                //_k1ValueEntity.ValueK1Wet = Convert.ToDouble(dgrdvK1Value[1, i].Value);


                if (dgrdvK1Value[2, i].Value != null)
                {
                    if (double.TryParse(dgrdvK1Value[2, i].Value.ToString(), out tmpDouble))
                    {
                        _k1ValueEntity.Sg = tmpDouble;
                        tmpDouble = 0;
                    }
                }

                if (dgrdvK1Value[3, i].Value != null)
                {
                    if (double.TryParse(dgrdvK1Value[3, i].Value.ToString(), out tmpDouble))
                    {
                        _k1ValueEntity.Sv = tmpDouble;
                        tmpDouble = 0;
                    }
                }

                if (dgrdvK1Value[4, i].Value != null)
                {
                    if (double.TryParse(dgrdvK1Value[4, i].Value.ToString(), out tmpDouble))
                    {
                        _k1ValueEntity.Q = tmpDouble;
                        tmpDouble = 0;
                    }
                }

                if (dgrdvK1Value[5, i].Value != null)
                {
                    if (double.TryParse(dgrdvK1Value[5, i].Value.ToString(), out tmpDouble))
                    {
                        _k1ValueEntity.BoreholeDeep = tmpDouble;
                        tmpDouble = 0;
                    }
                }

                //记录时间
                _k1ValueEntity.Time = dgrdvK1Value[6, i].Value != null ? Convert.ToDateTime(dgrdvK1Value[6, i].Value) : DateTime.Now;
                //录入时间
                _k1ValueEntity.TypeInTime = dgrdvK1Value[7, i].Value != null ? Convert.ToDateTime(dgrdvK1Value[7, i].Value) : DateTime.Now;
                //巷道ID
                _k1ValueEntity.TunnelID = tunnelEntity.TunnelID;
                //添加
                bResult = K1ValueBLL.insertValueK1(_k1ValueEntity);
                if (bResult)
                {
                    UpdateWarningDataMsg msg = new UpdateWarningDataMsg(this.mainWin.workingfaceId, this.tunnelId, K1ValueDbConstNames.TABLE_NAME, OPERATION_TYPE.ADD, DateTime.Now);
                    mainWin.SendMsg2Server(msg);
                    Alert.noteMsg("提交成功!");
                }
                else
                {
                    return;
                }
            }
        }

        /// <summary>
        /// 取消按钮事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnCancel_Click(object sender, EventArgs e)
        {
            //关闭窗口
            this.Close();
        }

        /// <summary>
        /// 自动填充
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void dgrdvK1Value_CellEnter(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex != dgrdvK1Value.NewRowIndex)
            {
                //日期自动填充
                if (e.ColumnIndex == 6 || e.ColumnIndex == 7)
                {
                    //datetimepicker控件位置大小
                    Rectangle rect = dgrdvK1Value.GetCellDisplayRectangle(e.ColumnIndex, e.RowIndex, false);
                    dtp.Visible = true;
                    dtp.Top = rect.Top;
                    dtp.Left = rect.Left;
                    dtp.Height = rect.Height;
                    dtp.Width = rect.Width;
                    //datetimepicker赋值
                    if (dgrdvK1Value[e.ColumnIndex, e.RowIndex].Value != null)
                    {
                        dtp.Value = Convert.ToDateTime(dgrdvK1Value[e.ColumnIndex, e.RowIndex].Value.ToString());
                    }
                    //默认填充系统时间
                    else
                    {
                        dtp.Value = DateTime.Now;
                        dgrdvK1Value[e.ColumnIndex, e.RowIndex].Value = dtp.Text;
                    }
                }
            }
        }

        /// <summary>
        /// 日期赋值
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void dtp_TextChanged(object sender, EventArgs e)
        {
            dgrdvK1Value.CurrentCell.Value = dtp.Text;
            dgrdvK1Value.CancelEdit();
        }

        /// <summary>
        /// 控制日期控件隐藏显示
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void dgrdvK1Value_CellLeave(object sender, DataGridViewCellEventArgs e)
        {
            dtp.Visible = false;
        }

        /// <summary>
        /// 控制日期控件隐藏显示
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void dgrdvK1Value_CellBeginEdit(object sender, DataGridViewCellCancelEventArgs e)
        {
            //dgrdvK1Value[0, e.RowIndex].Value = X;
            //dgrdvK1Value[1, e.RowIndex].Value = Y;
            //dgrdvK1Value[2, e.RowIndex].Value = Z;


            ////拾取点自动填充
            //if (e.ColumnIndex != 8 && dgrdvK1Value[0, e.RowIndex].Value == null && dgrdvK1Value[1, e.RowIndex].Value == null && dgrdvK1Value[2, e.RowIndex].Value == null && e.ColumnIndex > 2)
            //{
            //    if (dgrdvK1Value.Rows.Count < 8 && dgrdvK1Value.Rows.Count > 1)
            //    {
            //        if (e.RowIndex == 0)
            //        {
            //            if (dgrdvK1Value[0, e.RowIndex].Value == null && dgrdvK1Value[1, e.RowIndex].Value == null && dgrdvK1Value[2, e.RowIndex].Value == null)
            //            {
            //                dgrdvK1Value[0, e.RowIndex].Value = dgrdvK1Value[0, e.RowIndex + 1].Value;
            //                dgrdvK1Value[1, e.RowIndex].Value = dgrdvK1Value[1, e.RowIndex + 1].Value;
            //                dgrdvK1Value[2, e.RowIndex].Value = dgrdvK1Value[2, e.RowIndex + 1].Value;
            //            }
            //        }
            //        else
            //        {
            //            dgrdvK1Value[0, e.RowIndex].Value = dgrdvK1Value[0, e.RowIndex - 1].Value;
            //            dgrdvK1Value[1, e.RowIndex].Value = dgrdvK1Value[1, e.RowIndex - 1].Value;
            //            dgrdvK1Value[2, e.RowIndex].Value = dgrdvK1Value[2, e.RowIndex - 1].Value;
            //        }
            //    }
            //}
            //dtp.Visible = false;
        }

        /// <summary>
        /// 删除单元格
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnDeleteCell_Click(object sender, EventArgs e)
        {
            dgrdvK1Value[pointX, pointY].Value = null;
            dtp.Visible = false;
        }

        /// <summary>
        /// 获取鼠标点击行列
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void dgrdvK1Value_CellMouseDown(object sender, DataGridViewCellMouseEventArgs e)
        {
            //获取点击单元格行列号
            pointX = e.ColumnIndex;
            pointY = e.RowIndex;
        }

        /// <summary>
        /// 删除行
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnDeleteRow_Click(object sender, EventArgs e)
        {
            dgrdvK1Value.Rows.RemoveAt(pointY);
            dgrdvK1Value.Rows.Add();
        }

        /// <summary>
        /// 验证
        /// </summary>
        /// <returns></returns>
        private bool check()
        {
            //巷道是否选择
            if (this.tunnelId <= 0)
            {
                Alert.alert(Const.MSG_PLEASE_CHOOSE + Const_GM.TUNNEL + Const.SIGN_EXCLAMATION_MARK);
                return false;
            }

            if (dgrdvK1Value.NewRowIndex == 0)
            {
                Alert.alert(Const_OP.K1_VALUE_MSG_ADD_MORE_THAN_ONE);
                return false;
            }
            for (int i = 0; i < this.dgrdvK1Value.RowCount - 1; i++)
            {
                DataGridViewTextBoxCell cell = dgrdvK1Value.Rows[i].Cells[3] as DataGridViewTextBoxCell;

                // 孔深
                cell = dgrdvK1Value.Rows[i].Cells[5] as DataGridViewTextBoxCell;
                if (cell.Value == null)
                {
                    cell.Style.BackColor = Const.ERROR_FIELD_COLOR;
                    Alert.alert(Const.rowNotNull(i, Const_OP.K1_VALUE_BOREHOLE_DEEP));
                    return false;
                }
                else
                {
                    cell.Style.BackColor = Const.NO_ERROR_FIELD_COLOR;
                }
                if (!Validator.IsNumeric(cell.Value.ToString()))
                {
                    Alert.alert(Const.rowMustBeNumber(i, Const_OP.K1_VALUE_BOREHOLE_DEEP));
                    cell.Style.BackColor = Const.ERROR_FIELD_COLOR;
                    return false;
                }
                else
                {
                    cell.Style.BackColor = Const.NO_ERROR_FIELD_COLOR;
                }
            }
            return true;
        }

        private void dgrdvK1Value_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            // 判断列索引是不是删除按钮
            if (e.ColumnIndex == 8)
            {
                //// 最后一行为空行时，跳出循环
                // 最后一行删除按钮设为不可
                if (dgrdvK1Value.RowCount - 1 != this.dgrdvK1Value.CurrentRow.Index)
                {
                    this.dgrdvK1Value.Rows.Remove(this.dgrdvK1Value.CurrentRow);
                    dgrdvK1Value.AllowUserToAddRows = true;
                }
            }
        }

        private void dgrdvK1Value_RowsAdded(object sender, DataGridViewRowsAddedEventArgs e)
        {
            if (dgrdvK1Value.Rows.Count > 7)
            {
                dgrdvK1Value.Rows[dgrdvK1Value.NewRowIndex].ReadOnly = true;
            }
            else
            {
                dgrdvK1Value.Rows[dgrdvK1Value.NewRowIndex].ReadOnly = false;
            }
        }

        private void dgrdvK1Value_RowEnter(object sender, DataGridViewCellEventArgs e)
        {
            if (dgrdvK1Value.Rows.Count < 8)
            {
                dgrdvK1Value.Rows[dgrdvK1Value.NewRowIndex].ReadOnly = false;
            }
        }
    }
}
