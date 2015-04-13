using System;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using LibBusiness;
using LibCommon;
using LibEntity;
using LibSocket;

namespace LibPanels
{
    public partial class K1ValueEntering : Form
    {

        private K1Value K1Value { get; set; }

        /// <summary>
        /// 添加
        /// </summary>
        public K1ValueEntering()
        {
            InitializeComponent();
            FormDefaultPropertiesSetter.SetEnteringFormDefaultProperties(this, Const_OP.K1_VALUE_ADD);
        }

        public K1ValueEntering(Tunnel tunnel)
        {
            InitializeComponent();
            selectTunnelSimple1.SetTunnel(tunnel);
        }

        /// <summary>
        /// 修改
        /// </summary>
        /// <param name="k1Value"></param>
        public K1ValueEntering(K1Value k1Value)
        {
            InitializeComponent();
            K1Value = k1Value;

            FormDefaultPropertiesSetter.SetEnteringFormDefaultProperties(this, Const_OP.K1_VALUE_CHANGE);
            var k1ValueList = K1Value.FindAllByK1ValueId(k1Value.K1ValueId);
            selectTunnelSimple1.SetTunnel(K1Value.Tunnel);
            foreach (var k1 in k1ValueList)
            {
                dgrdvK1Value.Rows.Add(k1.ValueK1Dry, k1.ValueK1Wet, k1.Sg, k1.Sv, k1.Q, k1.BoreholeDeep, k1.Time);
            }
        }

        /// <summary>
        /// 初始化
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void K1Value_Load(object sender, EventArgs e)
        {

        }

        private void dtp_TextChange(object sender, EventArgs e)
        {
            dgrdvK1Value.Rows[dgrdvK1Value.CurrentCell.RowIndex].Cells[6].Value = ((DateTimePicker)sender).Text;
        }
        /// <summary>
        /// 提交按钮事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnSubmit_Click(object sender, EventArgs e)
        {
            if (!Check()) return;
            if (K1Value != null)
            {
                var k1ValueList = K1Value.FindAllByK1ValueId(K1Value.K1ValueId);
                int i = 0;
                foreach (var k1 in k1ValueList)
                {
                    if (dgrdvK1Value.Rows[i].Cells[5].Value == null)
                    {
                        k1.Delete();
                        continue;
                    }
                    k1.ValueK1Dry = Convert.ToDouble(dgrdvK1Value.Rows[i].Cells[0].Value);
                    k1.ValueK1Wet = Convert.ToDouble(dgrdvK1Value.Rows[i].Cells[1].Value);
                    k1.Sg = Convert.ToDouble(dgrdvK1Value.Rows[i].Cells[2].Value);
                    k1.Sv = Convert.ToDouble(dgrdvK1Value.Rows[i].Cells[3].Value);
                    k1.Q = Convert.ToDouble(dgrdvK1Value.Rows[i].Cells[4].Value);
                    k1.BoreholeDeep = Convert.ToDouble(dgrdvK1Value.Rows[i].Cells[5].Value);
                    k1.Time = Convert.ToDateTime(dgrdvK1Value.Rows[i].Cells[6].Value);
                    k1.TypeInTime = DateTime.Now;
                    k1.Tunnel = selectTunnelSimple1.SelectedTunnel;
                    k1.CoordinateX = selectTunnelSimple1.SelectedTunnel.WorkingFace.CoordinateX;
                    k1.CoordinateY = selectTunnelSimple1.SelectedTunnel.WorkingFace.CoordinateY;
                    k1.CoordinateZ = selectTunnelSimple1.SelectedTunnel.WorkingFace.CoordinateZ;
                    k1.Save();
                    i++;
                }
            }
            else
            {
                var groupId = K1Value.GetMaxGroupId();
                var k1List = (from DataGridViewRow row in dgrdvK1Value.Rows
                              where row.Cells[5] != null && row.Cells[5].Value != null
                              select new K1Value
                              {
                                  K1ValueId = groupId + 1,
                                  ValueK1Dry = Convert.ToDouble(row.Cells[0].Value),
                                  ValueK1Wet = Convert.ToDouble(row.Cells[1].Value),
                                  Sg = Convert.ToDouble(row.Cells[2].Value),
                                  Sv = Convert.ToDouble(row.Cells[3].Value),
                                  Q = Convert.ToDouble(row.Cells[4].Value),
                                  BoreholeDeep = Convert.ToDouble(row.Cells[5].Value),
                                  Time = Convert.ToDateTime(row.Cells[6].Value),
                                  TypeInTime = DateTime.Now,
                                  Tunnel = selectTunnelSimple1.SelectedTunnel,
                                  CoordinateX = selectTunnelSimple1.SelectedTunnel.WorkingFace.CoordinateX,
                                  CoordinateY = selectTunnelSimple1.SelectedTunnel.WorkingFace.CoordinateY,
                                  CoordinateZ = selectTunnelSimple1.SelectedTunnel.WorkingFace.CoordinateZ
                              }).ToList();

                foreach (var k1Value in k1List)
                {
                    k1Value.Save();
                }
            }
            //TODO:添加成功
            UpdateWarningDataMsg msg = new UpdateWarningDataMsg(selectTunnelSimple1.SelectedTunnel.WorkingFace.WorkingFaceId, selectTunnelSimple1.SelectedTunnel.TunnelId, K1ValueDbConstNames.TABLE_NAME, OPERATION_TYPE.ADD, DateTime.Now);
            SocketUtil.SendMsg2Server(msg);
        }

        /// <summary>
        /// 取消按钮事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnCancel_Click(object sender, EventArgs e)
        {
            Close();
        }

        /// <summary>
        /// 获取鼠标点击行列
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void dgrdvK1Value_CellMouseDown(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (e.ColumnIndex == 6)
            {
                //datetimepicker控件位置大小
                var dtp = new DateTimePicker();
                dgrdvK1Value.Controls.Add(dtp); //把时间控件加入DataGridView
                dtp.Format = DateTimePickerFormat.Custom; //设置日期格式为2010-08-05
                dtp.CustomFormat = @"yyyy-MM-dd";
                dtp.TextChanged += dtp_TextChange; //为时间控件加入事件dtp_TextChange
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

        /// <summary>
        /// 删除行
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnDeleteRow_Click(object sender, EventArgs e)
        {
            dgrdvK1Value.Rows.Add();
        }

        /// <summary>
        /// 验证
        /// </summary>
        /// <returns></returns>
        private bool Check()
        {
            //巷道是否选择
            if (selectTunnelSimple1.SelectedTunnel == null)
            {
                Alert.alert(Const.MSG_PLEASE_CHOOSE + Const_GM.TUNNEL + Const.SIGN_EXCLAMATION_MARK);
                return false;
            }
            if (dgrdvK1Value.NewRowIndex == 0)
            {
                Alert.alert(Const_OP.K1_VALUE_MSG_ADD_MORE_THAN_ONE);
                return false;
            }
            for (int i = 0; i < dgrdvK1Value.RowCount - 1; i++)
            {
                var cell = dgrdvK1Value.Rows[i].Cells[5] as DataGridViewTextBoxCell;
                if (cell != null && cell.Value == null)
                {
                    cell.Style.BackColor = Const.ERROR_FIELD_COLOR;
                    Alert.alert(Const.rowNotNull(i, Const_OP.K1_VALUE_BOREHOLE_DEEP));
                    return false;
                }
                if (cell == null) continue;
                cell.Style.BackColor = Const.NO_ERROR_FIELD_COLOR;
                if (!Validator.IsNumeric(cell.Value.ToString()))
                {
                    Alert.alert(Const.rowMustBeNumber(i, Const_OP.K1_VALUE_BOREHOLE_DEEP));
                    cell.Style.BackColor = Const.ERROR_FIELD_COLOR;
                    return false;
                }
                cell.Style.BackColor = Const.NO_ERROR_FIELD_COLOR;
            }
            return true;
        }

        private void dgrdvK1Value_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            // 判断列索引是不是删除按钮
            if (e.ColumnIndex != 7) return;
            //// 最后一行为空行时，跳出循环
            // 最后一行删除按钮设为不可
            var dataGridViewRow = dgrdvK1Value.CurrentRow;
            if (dataGridViewRow == null || dgrdvK1Value.RowCount - 1 == dataGridViewRow.Index) return;
            if (dgrdvK1Value.CurrentRow != null) dgrdvK1Value.Rows.Remove(dgrdvK1Value.CurrentRow);
            dgrdvK1Value.AllowUserToAddRows = true;
        }

        private void dgrdvK1Value_RowsAdded(object sender, DataGridViewRowsAddedEventArgs e)
        {
            dgrdvK1Value.Rows[dgrdvK1Value.NewRowIndex].ReadOnly = dgrdvK1Value.Rows.Count > 7;
        }

        private void dgrdvK1Value_RowEnter(object sender, DataGridViewCellEventArgs e)
        {
            if (dgrdvK1Value.Rows.Count < 7)
            {
                dgrdvK1Value.Rows[dgrdvK1Value.NewRowIndex].ReadOnly = false;
            }
        }
    }
}
