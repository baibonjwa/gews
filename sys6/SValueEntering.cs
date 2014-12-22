// ******************************************************************
// 概  述：S值信息添加修改
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
    public partial class SValueEntering : Form
    {
        /*******************************/
        UnderMessageWindow mainWin;
        SValue _sValueEntity = new SValue();
        SValue[] sEntitys;
        Tunnel tunnelEntity = new Tunnel();
        //巷道控件用数组
        int[] arr = new int[5];
        /** K1Value分组数 **/
        int groupCount = 0;
        /** 数据行数 **/
        int rowCount = 0;
        DateTimePicker dtp = new DateTimePicker();
        int pointX = -1;
        int pointY = -1;
        double tmpDouble = 0;
        int tunnelId;
        /*******************************/

        public String X { get; set; }
        public String Y { get; set; }
        public String Z { get; set; }

        /// <summary>
        /// 构造方法
        /// <param name="tunnelId">巷道id</param>
        /// </summary>
        public SValueEntering(int tunnelId, string tunnelName, UnderMessageWindow win)
        {
            InitializeComponent();
            this.tunnelId = tunnelId;
            this.Text = tunnelName;
            this.mainWin = win;

            Tunnel entTunnel = BasicInfoManager.getInstance().getTunnelByID(tunnelId);
            //WorkingFaceEntity entWorkingFace = WorkingFaceBLL.selectWorkingFaceInfoByID(entTunnel.WorkingFace.WorkingFaceID);

            X = entTunnel.WorkingFace.Coordinate.X.ToString();
            Y = entTunnel.WorkingFace.Coordinate.Y.ToString();
            Z = entTunnel.WorkingFace.Coordinate.Z.ToString();
        }

        /// <summary>
        /// 初始化
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SValue_Load(object sender, EventArgs e)
        {
            //设置Datagridview样式
            setDataGridViewStyle();

            dtp.Visible = false;
            dtp.Format = DateTimePickerFormat.Custom;
            dtp.CustomFormat = Const.DATE_FORMART_H_MM_SS;
            dtp.TextChanged += dtp_TextChanged;
            dgrdvSValue.Controls.Add(dtp);
        }

        #region ******控制datagridview样式******
        /// <summary>
        /// 设置DataGridView默认样式
        /// </summary>
        private void setDataGridViewStyle()
        {

        }

        /// <summary>
        /// 自动填充坐标
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void dgrdvSValue_RowEnter(object sender, DataGridViewCellEventArgs e)
        {
            if (dgrdvSValue.Rows.Count < 8)
            {
                dgrdvSValue.Rows[dgrdvSValue.NewRowIndex].ReadOnly = false;
            }
        }
        #endregion

        /// <summary>
        /// 绑定修改数据
        /// </summary>
        private void change()
        {
            //S值实体
            _sValueEntity = SValueBLL.selectValueSByID(_sValueEntity.ID);
            //S值实体组
            sEntitys = SValueBLL.selectValueSBySValueID(_sValueEntity.SValueID);

            //datagridview赋值
            for (int i = 0; i < sEntitys.Length; i++)
            {
                //拾取点X
                dgrdvSValue[0, i].Value = sEntitys[i].CoordinateX;
                //拾取点Y
                dgrdvSValue[1, i].Value = sEntitys[i].CoordinateY;
                //拾取点Z
                dgrdvSValue[2, i].Value = sEntitys[i].CoordinateZ;
                //Sg
                if (sEntitys[i].ValueSg != 0)
                    dgrdvSValue[3, i].Value = sEntitys[i].ValueSg;
                //Sv
                if (sEntitys[i].ValueSv != 0)
                    dgrdvSValue[4, i].Value = sEntitys[i].ValueSv;
                //q
                if (sEntitys[i].Valueq != 0)
                    dgrdvSValue[5, i].Value = sEntitys[i].Valueq;
                //孔深
                dgrdvSValue[6, i].Value = sEntitys[i].BoreholeDeep;
                //记录时间
                dgrdvSValue[7, i].Value = sEntitys[i].Time;
                //录入时间
                dgrdvSValue[8, i].Value = sEntitys[i].TypeInTime;
                if (dgrdvSValue.Rows[dgrdvSValue.Rows.Count - 1].IsNewRow)
                {
                    dgrdvSValue.Rows.Add();
                    dgrdvSValue[0, dgrdvSValue.Rows.Count - 2].Value = dgrdvSValue[0, dgrdvSValue.Rows.Count - 1].Value;
                    dgrdvSValue[1, dgrdvSValue.Rows.Count - 2].Value = dgrdvSValue[1, dgrdvSValue.Rows.Count - 1].Value;
                    dgrdvSValue[2, dgrdvSValue.Rows.Count - 2].Value = dgrdvSValue[2, dgrdvSValue.Rows.Count - 1].Value;
                    dgrdvSValue[3, dgrdvSValue.Rows.Count - 2].Value = dgrdvSValue[3, dgrdvSValue.Rows.Count - 1].Value;
                    dgrdvSValue[4, dgrdvSValue.Rows.Count - 2].Value = dgrdvSValue[4, dgrdvSValue.Rows.Count - 1].Value;
                    dgrdvSValue[5, dgrdvSValue.Rows.Count - 2].Value = dgrdvSValue[5, dgrdvSValue.Rows.Count - 1].Value;
                    dgrdvSValue[6, dgrdvSValue.Rows.Count - 2].Value = dgrdvSValue[6, dgrdvSValue.Rows.Count - 1].Value;
                    dgrdvSValue[7, dgrdvSValue.Rows.Count - 2].Value = dgrdvSValue[7, dgrdvSValue.Rows.Count - 1].Value;
                    dgrdvSValue[8, dgrdvSValue.Rows.Count - 2].Value = dgrdvSValue[8, dgrdvSValue.Rows.Count - 1].Value;
                    dgrdvSValue[0, dgrdvSValue.Rows.Count - 1].Value = null;
                    dgrdvSValue[1, dgrdvSValue.Rows.Count - 1].Value = null;
                    dgrdvSValue[2, dgrdvSValue.Rows.Count - 1].Value = null;
                    dgrdvSValue[3, dgrdvSValue.Rows.Count - 1].Value = null;
                    dgrdvSValue[4, dgrdvSValue.Rows.Count - 1].Value = null;
                    dgrdvSValue[5, dgrdvSValue.Rows.Count - 1].Value = null;
                    dgrdvSValue[6, dgrdvSValue.Rows.Count - 1].Value = null;
                    dgrdvSValue[7, dgrdvSValue.Rows.Count - 1].Value = null;
                    dgrdvSValue[8, dgrdvSValue.Rows.Count - 1].Value = null;
                }
                //修改列为选中状态
                if (sEntitys[i].ID == _sValueEntity.ID)
                {
                    dgrdvSValue.Rows[i].Selected = true;
                }
            }
        }

        /// <summary>
        /// 提交按钮事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnSubmit_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show(@"确认提交", "井下终端录入系统", MessageBoxButtons.OKCancel, MessageBoxIcon.Question);
            if (result != DialogResult.OK)
            {
                return;
            }

            //验证
            if (!check())
            {
                this.DialogResult = DialogResult.None;
                return;
            }
            this.DialogResult = DialogResult.OK;
            tunnelEntity.TunnelId = this.tunnelId;
            tunnelEntity = BasicInfoManager.getInstance().getTunnelByID(tunnelId);
            //添加
            //if (this.Text == Const_OP.S_VALUE_ADD)
            //{
            insertSValue();
            //}
            //修改
            //if (this.Text == Const_OP.S_VALUE_CHANGE)
            //{
            //    updateSValue();
            //}
        }

        /// <summary>
        /// 添加
        /// </summary>
        private void insertSValue()
        {
            rowCount = dgrdvSValue.Rows.Count - 1;
            groupCount = SValueBLL.selectMaxGroupID();
            bool bResult = false;
            for (int i = 0; i < rowCount; i++)
            {
                _sValueEntity.SValueID = 0;
                _sValueEntity.CoordinateX = 0;
                _sValueEntity.CoordinateY = 0;
                _sValueEntity.CoordinateZ = 0;
                _sValueEntity.ValueSg = 0;
                _sValueEntity.ValueSv = 0;
                _sValueEntity.Valueq = 0;
                _sValueEntity.BoreholeDeep = 0;
                _sValueEntity.Time = DateTime.Now;
                _sValueEntity.TypeInTime = DateTime.Now;
                //S值分组ID
                _sValueEntity.SValueID = groupCount + 1;
                //拾取点X
                if (double.TryParse(dgrdvSValue[0, i].Value.ToString(), out tmpDouble))
                {
                    _sValueEntity.CoordinateX = tmpDouble;
                    tmpDouble = 0;
                }
                //拾取点Y
                if (double.TryParse(dgrdvSValue[1, i].Value.ToString(), out tmpDouble))
                {
                    _sValueEntity.CoordinateY = tmpDouble;
                    tmpDouble = 0;
                }
                //拾取点Z
                if (double.TryParse(dgrdvSValue[2, i].Value.ToString(), out tmpDouble))
                {
                    _sValueEntity.CoordinateZ = Convert.ToDouble(dgrdvSValue[2, i].Value);
                }
                //Sg
                if (dgrdvSValue[3, i].Value != null)
                {
                    if (double.TryParse(dgrdvSValue[3, i].Value.ToString(), out tmpDouble))
                    {
                        _sValueEntity.ValueSg = tmpDouble;
                        tmpDouble = 0;
                    }
                }
                //Sv
                if (dgrdvSValue[4, i].Value != null)
                {
                    if (double.TryParse(dgrdvSValue[4, i].Value.ToString(), out tmpDouble))
                    {
                        _sValueEntity.ValueSv = tmpDouble;
                        tmpDouble = 0;
                    }
                }
                //q
                if (dgrdvSValue[5, i].Value != null)
                {
                    if (double.TryParse(dgrdvSValue[5, i].Value.ToString(), out tmpDouble))
                    {
                        _sValueEntity.Valueq = tmpDouble;
                        tmpDouble = 0;
                    }
                }
                //孔深
                if (double.TryParse(dgrdvSValue[6, i].Value.ToString(), out tmpDouble))
                {
                    _sValueEntity.BoreholeDeep = tmpDouble;
                    tmpDouble = 0;
                }
                //记录时间
                _sValueEntity.Time = dgrdvSValue[7, i].Value != null ? Convert.ToDateTime(dgrdvSValue[7, i].Value) : DateTime.Now;

                //录入时间
                _sValueEntity.TypeInTime = dgrdvSValue[8, i].Value != null ? Convert.ToDateTime(dgrdvSValue[7, i].Value) : DateTime.Now;
                //巷道ID
                _sValueEntity.TunnelID = tunnelEntity.TunnelId;
                //添加
                bResult = SValueBLL.insertValueS(_sValueEntity);
                if (bResult)
                {
                    Alert.noteMsg("提交成功!");
                }
            }
        }

        /// <summary>
        /// 修改
        /// </summary>
        private void updateSValue()
        {
            rowCount = dgrdvSValue.Rows.Count - 1;
            groupCount = K1ValueBLL.selectValueK1GroupCount();
            bool bResult = false;

            for (int i = 0; i < rowCount; i++)
            {
                //拾取点X
                _sValueEntity.CoordinateX = Convert.ToDouble(dgrdvSValue[0, i].Value);
                //拾取点Y
                _sValueEntity.CoordinateY = Convert.ToDouble(dgrdvSValue[1, i].Value);
                //拾取点Z
                _sValueEntity.CoordinateZ = Convert.ToDouble(dgrdvSValue[2, i].Value);
                //Sg
                _sValueEntity.ValueSg = Convert.ToDouble(dgrdvSValue[3, i].Value);
                //Sv
                _sValueEntity.ValueSv = Convert.ToDouble(dgrdvSValue[4, i].Value);
                //q
                _sValueEntity.Valueq = Convert.ToDouble(dgrdvSValue[5, i].Value);
                //孔深
                _sValueEntity.BoreholeDeep = Convert.ToDouble(dgrdvSValue[6, i].Value);
                //记录时间
                _sValueEntity.Time = Convert.ToDateTime(dgrdvSValue[7, i].Value);
                //录入时间
                _sValueEntity.TypeInTime = Convert.ToDateTime(dgrdvSValue[8, i].Value);
                //巷道ID
                _sValueEntity.TunnelID = tunnelEntity.TunnelId;
                //修改
                if (i < sEntitys.Length)
                {
                    _sValueEntity.ID = sEntitys[i].ID;
                    bResult = SValueBLL.updateValueS(_sValueEntity);
                    if (bResult)
                    {
                        //TODO:修改成功
                    }
                }
                //超出数据条数部分做添加处理
                else
                {
                    bResult = SValueBLL.insertValueS(_sValueEntity);
                    if (bResult)
                    {
                        UpdateWarningDataMsg msg = new UpdateWarningDataMsg(this.mainWin.workingfaceId, this.tunnelId, SValueDbConstNames.TABLE_NAME, OPERATION_TYPE.ADD, DateTime.Now);
                        mainWin.SendMsg2Server(msg);
                    }
                }
            }
            //少于数据条数部分做删除处理
            if (rowCount < sEntitys.Length)
            {
                for (int i = rowCount; i < sEntitys.Length; i++)
                {
                    _sValueEntity.ID = sEntitys[i].ID;
                    bResult = SValueBLL.deleteValueS(_sValueEntity);
                    if (bResult)
                    {
                        //TODO:删除成功
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
            dgrdvSValue.CurrentCell.Value = dtp.Text;
            dgrdvSValue.CancelEdit();
        }

        /// <summary>
        /// 日期填充
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void dgrdvSValue_CellEnter(object sender, DataGridViewCellEventArgs e)
        {
            //Datetimepicker设置
            if (e.ColumnIndex != 9)
            {
                if (e.RowIndex < 7)
                {
                    if (e.ColumnIndex == 7 || e.ColumnIndex == 8)
                    {
                        //位置大小
                        Rectangle rect = dgrdvSValue.GetCellDisplayRectangle(e.ColumnIndex, e.RowIndex, false);
                        dtp.Visible = true;
                        dtp.Top = rect.Top;
                        dtp.Left = rect.Left;
                        dtp.Height = rect.Height;
                        dtp.Width = rect.Width;
                        //Datetimepicker赋值
                        if (dgrdvSValue[e.ColumnIndex, e.RowIndex].Value != null)
                        {
                            dtp.Value = Convert.ToDateTime(dgrdvSValue[e.ColumnIndex, e.RowIndex].Value.ToString());
                        }
                        else
                        {
                            dtp.Value = DateTime.Now;
                            dgrdvSValue[e.ColumnIndex, e.RowIndex].Value = dtp.Text;
                        }
                    }
                }
            }
        }

        /// <summary>
        /// 控制日期控件隐藏显示
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void dgrdvSValue_CellBeginEdit(object sender, DataGridViewCellCancelEventArgs e)
        {
            dgrdvSValue[0, e.RowIndex].Value = X;
            dgrdvSValue[1, e.RowIndex].Value = Y;
            dgrdvSValue[2, e.RowIndex].Value = Z;
            //拾取点自动填充
            //if (e.ColumnIndex != 9 && dgrdvSValue[0, e.RowIndex].Value == null && dgrdvSValue[1, e.RowIndex].Value == null && dgrdvSValue[2, e.RowIndex].Value == null && e.ColumnIndex > 2)
            //{
            //    if (dgrdvSValue.Rows.Count < 8 && dgrdvSValue.Rows.Count > 1)
            //    {
            //        if (e.RowIndex == 0)
            //        {
            //            if (dgrdvSValue[0, e.RowIndex].Value == null && dgrdvSValue[1, e.RowIndex].Value == null && dgrdvSValue[2, e.RowIndex].Value == null)
            //            {
            //                dgrdvSValue[0, e.RowIndex].Value = dgrdvSValue[0, e.RowIndex + 1].Value;
            //                dgrdvSValue[1, e.RowIndex].Value = dgrdvSValue[1, e.RowIndex + 1].Value;
            //                dgrdvSValue[2, e.RowIndex].Value = dgrdvSValue[2, e.RowIndex + 1].Value;
            //            }
            //        }
            //        else
            //        {
            //            dgrdvSValue[0, e.RowIndex].Value = dgrdvSValue[0, e.RowIndex - 1].Value;
            //            dgrdvSValue[1, e.RowIndex].Value = dgrdvSValue[1, e.RowIndex - 1].Value;
            //            dgrdvSValue[2, e.RowIndex].Value = dgrdvSValue[2, e.RowIndex - 1].Value;
            //        }
            //    }
            //}
            //dtp.Visible = false;
        }

        /// <summary>
        /// 控制日期控件隐藏显示
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void dgrdvSValue_CellLeave(object sender, DataGridViewCellEventArgs e)
        {
            dtp.Visible = false;
        }

        /// <summary>
        /// 获取鼠标点击行列
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void dgrdvSValue_CellMouseDown(object sender, DataGridViewCellMouseEventArgs e)
        {
            pointX = e.ColumnIndex;
            pointY = e.RowIndex;
        }

        /// <summary>
        /// 删除单元格
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnDeleteCell_Click(object sender, EventArgs e)
        {
            dgrdvSValue[pointX, pointY].Value = null;
            dtp.Visible = false;
        }

        /// <summary>
        /// 删除行
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnDeleteRow_Click(object sender, EventArgs e)
        {
            dgrdvSValue.Rows.RemoveAt(pointY);
            dgrdvSValue.Rows.Add();
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
            if (dgrdvSValue.NewRowIndex == 0)
            {
                Alert.alert(Const_OP.S_VALUE_MSG_ADD_MORE_THAN_ONE);
                return false;
            }
            for (int i = 0; i < this.dgrdvSValue.RowCount - 1; i++)
            {
                //Sg
                DataGridViewTextBoxCell cell = dgrdvSValue.Rows[i].Cells[3] as DataGridViewTextBoxCell;

                if (dgrdvSValue.Rows[i].Cells[3].Value == null && dgrdvSValue.Rows[i].Cells[4].Value == null && dgrdvSValue.Rows[i].Cells[5].Value == null)
                {
                    Alert.alert("Sg,Sv,q值必须录入其中之一！");
                    dgrdvSValue.Rows[i].Cells[3].Style.BackColor = Const.ERROR_FIELD_COLOR;
                    dgrdvSValue.Rows[i].Cells[3].Style.BackColor = Const.ERROR_FIELD_COLOR;
                    dgrdvSValue.Rows[i].Cells[3].Style.BackColor = Const.ERROR_FIELD_COLOR;
                    return false;
                }
                else
                {
                    dgrdvSValue.Rows[i].Cells[3].Style.BackColor = Const.NO_ERROR_FIELD_COLOR;
                    dgrdvSValue.Rows[i].Cells[3].Style.BackColor = Const.NO_ERROR_FIELD_COLOR;
                    dgrdvSValue.Rows[i].Cells[3].Style.BackColor = Const.NO_ERROR_FIELD_COLOR;
                }

                if (cell.Value != null && !Validator.IsNumeric(cell.Value.ToString()))
                {
                    Alert.alert(Const.rowMustBeNumber(i, Const_OP.S_VALUE_SG));
                    cell.Style.BackColor = Const.ERROR_FIELD_COLOR;
                    return false;
                }
                else
                {
                    cell.Style.BackColor = Const.NO_ERROR_FIELD_COLOR;
                }
                //Sv
                cell = dgrdvSValue.Rows[i].Cells[4] as DataGridViewTextBoxCell;
                if (cell.Value != null && !Validator.IsNumeric(cell.Value.ToString()))
                {
                    Alert.alert(Const.rowMustBeNumber(i, Const_OP.S_VALUE_SV));
                    cell.Style.BackColor = Const.ERROR_FIELD_COLOR;
                    return false;
                }
                else
                {
                    cell.Style.BackColor = Const.NO_ERROR_FIELD_COLOR;
                }
                //q
                cell = dgrdvSValue.Rows[i].Cells[5] as DataGridViewTextBoxCell;
                if (cell.Value != null && !Validator.IsNumeric(cell.Value.ToString()))
                {
                    Alert.alert(Const.rowMustBeNumber(i, Const_OP.S_VALUE_Q));
                    cell.Style.BackColor = Const.ERROR_FIELD_COLOR;
                    return false;
                }
                else
                {
                    cell.Style.BackColor = Const.NO_ERROR_FIELD_COLOR;
                }
                cell = dgrdvSValue.Rows[i].Cells[6] as DataGridViewTextBoxCell;
                if (cell.Value == null)
                {
                    cell.Style.BackColor = Const.ERROR_FIELD_COLOR;
                    Alert.alert(Const.rowNotNull(i, Const_OP.S_VALUE_BOREHOLE_DEEP));
                    return false;
                }
                else
                {
                    cell.Style.BackColor = Const.NO_ERROR_FIELD_COLOR;
                }
                if (!Validator.IsNumeric(cell.Value.ToString()))
                {
                    Alert.alert(Const.rowMustBeNumber(i, Const_OP.S_VALUE_BOREHOLE_DEEP));
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

        private void dgrdvSValue_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            // 判断列索引是不是删除按钮
            if (e.ColumnIndex == 9)
            {
                //// 最后一行为空行时，跳出循环
                // 最后一行删除按钮设为不可
                if (dgrdvSValue.RowCount - 1 != this.dgrdvSValue.CurrentRow.Index)
                {
                    this.dgrdvSValue.Rows.Remove(this.dgrdvSValue.CurrentRow);
                    dgrdvSValue.AllowUserToAddRows = true;
                }
            }
        }

        private void dgrdvSValue_RowsAdded(object sender, DataGridViewRowsAddedEventArgs e)
        {
            if (dgrdvSValue.Rows.Count > 7)
            {
                dgrdvSValue.Rows[dgrdvSValue.NewRowIndex].ReadOnly = true;
            }
            else
            {
                dgrdvSValue.Rows[dgrdvSValue.NewRowIndex].ReadOnly = false;
            }
        }
    }
}
