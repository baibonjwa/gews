using System;
using System.Collections.Generic;
using System.Data;
using System.Windows.Forms;
using ESRI.ArcGIS.Geometry;
using GIS.HdProc;
using LibCommon;
using LibEntity;

namespace _3.GeologyMeasure
{
    /// <summary>
    ///     掘进巷道矫正
    /// </summary>
    public partial class TunnelJZEntering : Form
    {
        private int[] _arr = new int[5];
        private DataSet _dsWirePoint = new DataSet();
        private int _itemCount = 0;
        private double _tmpDouble = 0;
        private int _tmpRowIndex = -1;
        private int _tunnelID;
        /**********变量声明***********/
        private DataGridViewCell[] dgvc = new DataGridViewCell[8];
        private int doing = 0;
        private string[] dr = new string[8];
        private WirePoint[] wpiEntity;
        private readonly Tunnel _tunnelEntity = new Tunnel();
        private readonly Wire wireEntity = new Wire();
        /*****************************/

        public TunnelJZEntering()
        {
            InitializeComponent();
            //日期
            dtpDate.Value = DateTime.Now.Date;
            //
            //设置窗体格式
            FormDefaultPropertiesSetter.SetEnteringFormDefaultProperties(this, "掘进巷道校正");
            ////自定义控件巷道过滤
            //this.selectTunnelUserControl1.SetFilterOn(TunnelTypeEnum.TUNNELLING);
            //this.selectTunnelUserControl1.init(this.MainForm);
            //自定义控件初始化

            // 注册委托事件
            //this.selectTunnelUserControl1.TunnelNameChanged +=
            //    InheritTunnelNameChanged;
        }

        /// <summary>
        ///     委托事件
        /// </summary>
        /// <param name="sender"></param>
        //private void InheritTunnelNameChanged(object sender, LibCommonForm.TunnelEventArgs e)
        //{
        //    bindDistanceFromWirePoint();
        //}
        private void bindDistanceFromWirePoint()
        {
            if (Text == "掘进巷道校正")
            {
                _tunnelEntity.TunnelId = selectTunnelUserControl1.SelectedTunnel.TunnelId;
            }
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            //去掉无用空行
            for (var i = 0; i < dgrdvWire.RowCount - 1; i++)
            {
                if (dgrdvWire.Rows[i].Cells[0].Value == null &&
                    dgrdvWire.Rows[i].Cells[1].Value == null &&
                    dgrdvWire.Rows[i].Cells[2].Value == null &&
                    dgrdvWire.Rows[i].Cells[3].Value == null &&
                    dgrdvWire.Rows[i].Cells[4].Value == null &&
                    dgrdvWire.Rows[i].Cells[5].Value == null &&
                    dgrdvWire.Rows[i].Cells[6].Value == null &&
                    dgrdvWire.Rows[i].Cells[7].Value == null)
                {
                    dgrdvWire.Rows.RemoveAt(i);
                }
            }
            //验证
            if (!check())
            {
                DialogResult = DialogResult.None;
                return;
            }
            var coordinates = new List<IPoint>();
            for (var i = 0; i < dgrdvWire.Rows.Count - 1; i++)
            {
                var x = Convert.ToDouble(dgrdvWire.Rows[i].Cells[1].Value);
                var y = Convert.ToDouble(dgrdvWire.Rows[i].Cells[2].Value);
                var z = Convert.ToDouble(dgrdvWire.Rows[i].Cells[3].Value);
                IPoint pnt = new PointClass();
                pnt.X = x;
                pnt.Y = y;
                pnt.Z = z;
                coordinates.Add(pnt);
            }
            var tunnel = Tunnel.Find(_tunnelEntity.TunnelId);

            Global.cons.DrawJJJZ(_tunnelEntity.TunnelId.ToString(), coordinates, Convert.ToDouble(tunnel.TunnelWid), 0,
                0, Global.searchlen, Global.sxjl, 1);
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            Close();
        }

        /// <summary>
        ///     验证
        /// </summary>
        /// <returns></returns>
        private bool check()
        {
            // 判断导线点编号是否入力
            if (dgrdvWire.Rows.Count - 1 == 0)
            {
                Alert.alert(Const.MSG_PLEASE_TYPE_IN + Const_GM.WIRE_POINT_ID + Const.SIGN_EXCLAMATION_MARK);
                return false;
            }
            //dgrdvWire内部判断
            for (var i = 0; i < dgrdvWire.RowCount; i++)
            {
                // 最后一行为空行时，跳出循环
                if (i == dgrdvWire.RowCount - 1)
                {
                    break;
                }
                var cell = dgrdvWire.Rows[i].Cells[0] as DataGridViewTextBoxCell;
                // 判断导线点编号是否入力
                if (cell.Value == null)
                {
                    cell.Style.BackColor = Const.ERROR_FIELD_COLOR;
                    Alert.alert(Const_GM.WIRE_POINT_ID + Const.MSG_NOT_NULL + Const.SIGN_EXCLAMATION_MARK);
                    return false;
                }
                cell.Style.BackColor = Const.NO_ERROR_FIELD_COLOR;
                //判断导线点编号是否存在
                if (Text == Const_GM.WIRE_INFO_ADD)
                {
                    //导线点是否存在
                    if (WirePoint.ExistsByWirePointIdInWireInfo(wireEntity.WireId,
                        dgrdvWire.Rows[i].Cells[0].Value.ToString()))
                    {
                        cell.Style.BackColor = Const.ERROR_FIELD_COLOR;
                        Alert.alert(Const_GM.WIRE_POINT_ID + Const.MSG_ALREADY_HAVE + Const.SIGN_EXCLAMATION_MARK);
                        return false;
                    }
                    cell.Style.BackColor = Const.NO_ERROR_FIELD_COLOR;
                }
                //判断导线点编号是否有输入重复
                for (var j = 0; j < i; j++)
                {
                    if (dgrdvWire[0, j].Value.ToString() == dgrdvWire[0, i].Value.ToString())
                    {
                        cell.Style.BackColor = Const.ERROR_FIELD_COLOR;
                        dgrdvWire[0, j].Style.BackColor = Const.ERROR_FIELD_COLOR;
                        Alert.alert(Const_GM.WIRE_POINT_ID + Const.MSG_DOUBLE_EXISTS + Const.SIGN_EXCLAMATION_MARK);
                        return false;
                    }
                    cell.Style.BackColor = Const.NO_ERROR_FIELD_COLOR;
                    dgrdvWire[0, j].Style.BackColor = Const.NO_ERROR_FIELD_COLOR;
                }

                //判断坐标X是否入力
                cell = dgrdvWire.Rows[i].Cells[1] as DataGridViewTextBoxCell;
                if (cell.Value == null)
                {
                    cell.Style.BackColor = Const.ERROR_FIELD_COLOR;
                    Alert.alert(Const.rowNotNull(i, Const_GM.X));
                    return false;
                }
                cell.Style.BackColor = Const.NO_ERROR_FIELD_COLOR;

                // 判断坐标X是否为数字
                if (!Validator.IsNumeric(cell.Value.ToString()))
                {
                    cell.Style.BackColor = Const.ERROR_FIELD_COLOR;
                    Alert.alert(Const.rowMustBeNumber(i, Const_GM.X));
                    return false;
                }
                cell.Style.BackColor = Const.NO_ERROR_FIELD_COLOR;
                //判断坐标Y是否入力
                cell = dgrdvWire.Rows[i].Cells[2] as DataGridViewTextBoxCell;
                if (cell.Value == null)
                {
                    cell.Style.BackColor = Const.ERROR_FIELD_COLOR;
                    Alert.alert(Const.rowNotNull(i, Const_GM.Y));
                    return false;
                }
                cell.Style.BackColor = Const.NO_ERROR_FIELD_COLOR;

                // 判断坐标Y是否为数字
                if (!Validator.IsNumeric(cell.Value.ToString()))
                {
                    cell.Style.BackColor = Const.ERROR_FIELD_COLOR;
                    Alert.alert(Const.rowMustBeNumber(i, Const_GM.Y));
                    return false;
                }
                cell.Style.BackColor = Const.NO_ERROR_FIELD_COLOR;
                //判断坐标Z是否入力
                cell = dgrdvWire.Rows[i].Cells[3] as DataGridViewTextBoxCell;
                if (cell.Value == null)
                {
                    cell.Style.BackColor = Const.ERROR_FIELD_COLOR;
                    Alert.alert(Const.rowNotNull(i, Const_GM.Z));
                    return false;
                }
                cell.Style.BackColor = Const.NO_ERROR_FIELD_COLOR;

                // 判断坐标Z是否为数字
                if (!Validator.IsNumeric(cell.Value.ToString()))
                {
                    cell.Style.BackColor = Const.ERROR_FIELD_COLOR;
                    Alert.alert(Const.rowMustBeNumber(i, Const_GM.Z));
                    return false;
                }
                cell.Style.BackColor = Const.NO_ERROR_FIELD_COLOR;
                //判断距左帮距离是否入力
                cell = dgrdvWire.Rows[i].Cells[4] as DataGridViewTextBoxCell;
                if (cell.Value == null)
                {
                    cell.Style.BackColor = Const.ERROR_FIELD_COLOR;
                    Alert.alert(Const.rowNotNull(i, Const_GM.DISTANCE_TO_LEFT));
                    return false;
                }
                cell.Style.BackColor = Const.NO_ERROR_FIELD_COLOR;

                // 判断距左帮距离是否为数字
                if (!Validator.IsNumeric(cell.Value.ToString()))
                {
                    cell.Style.BackColor = Const.ERROR_FIELD_COLOR;
                    Alert.alert(Const.rowMustBeNumber(i, Const_GM.DISTANCE_TO_LEFT));
                    return false;
                }
                cell.Style.BackColor = Const.NO_ERROR_FIELD_COLOR;
                //判断距右帮距离是否入力
                cell = dgrdvWire.Rows[i].Cells[5] as DataGridViewTextBoxCell;
                if (cell.Value == null)
                {
                    cell.Style.BackColor = Const.ERROR_FIELD_COLOR;
                    Alert.alert(Const.rowNotNull(i, Const_GM.DISTANCE_TO_RIGHT));
                    return false;
                }
                cell.Style.BackColor = Const.NO_ERROR_FIELD_COLOR;

                // 判断距右帮距离是否为数字
                if (!Validator.IsNumeric(cell.Value.ToString()))
                {
                    cell.Style.BackColor = Const.ERROR_FIELD_COLOR;
                    Alert.alert(Const.rowMustBeNumber(i, Const_GM.DISTANCE_TO_RIGHT));
                    return false;
                }
                cell.Style.BackColor = Const.NO_ERROR_FIELD_COLOR;
                cell = dgrdvWire.Rows[i].Cells[6] as DataGridViewTextBoxCell;
                // 判断距顶板距离是否为数字
                if (cell.Value != null && !Validator.IsNumeric(cell.Value.ToString()))
                {
                    cell.Style.BackColor = Const.ERROR_FIELD_COLOR;
                    Alert.alert(Const.rowMustBeNumber(i, Const_GM.DISTANCE_TO_TOP));
                    return false;
                }
                cell.Style.BackColor = Const.NO_ERROR_FIELD_COLOR;
                cell = dgrdvWire.Rows[i].Cells[7] as DataGridViewTextBoxCell;
                // 判断距底板距离是否为数字
                if (cell.Value != null && !Validator.IsNumeric(cell.Value.ToString()))
                {
                    cell.Style.BackColor = Const.ERROR_FIELD_COLOR;
                    Alert.alert(Const.rowMustBeNumber(i, Const_GM.DISTANCE_TO_BOTTOM));
                    return false;
                }
                cell.Style.BackColor = Const.NO_ERROR_FIELD_COLOR;
            }
            //验证成功
            return true;
        }

        private void selectTunnelUserControl1_Load(object sender, EventArgs e)
        {
        }
    }
}