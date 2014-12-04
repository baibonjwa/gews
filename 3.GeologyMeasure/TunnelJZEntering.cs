using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using LibEntity;
using LibBusiness;
using LibCommon;
using LibCommonForm;
using GIS.HdProc;
using ESRI.ArcGIS.Geometry;
using LibCommonControl;

namespace _3.GeologyMeasure
{
    /// <summary>
    /// 掘进巷道矫正
    /// </summary>
    public partial class TunnelJZEntering : BaseForm
    {
        /**********变量声明***********/
        DataGridViewCell[] dgvc = new DataGridViewCell[8];
        string[] dr = new string[8];
        int doing = 0;
        int _tmpRowIndex = -1;
        int _itemCount = 0;
        Tunnel _tunnelEntity = new Tunnel();
        WireInfo wireInfoEntity = new WireInfo();
        WirePointInfo[] wpiEntity;
        int[] _arr = new int[5];
        DataSet _dsWirePoint = new DataSet();
        int _tunnelID;
        double _tmpDouble = 0;

        /*****************************/
        public TunnelJZEntering()
        {
            InitializeComponent();
            //日期
            dtpDate.Value = DateTime.Now.Date;
            //
            //设置窗体格式
            LibCommon.FormDefaultPropertiesSetter.SetEnteringFormDefaultProperties(this, "掘进巷道校正");
            ////自定义控件巷道过滤
            //this.selectTunnelUserControl1.SetFilterOn(TunnelTypeEnum.TUNNELLING);
            //this.selectTunnelUserControl1.init(this.MainForm);
            //自定义控件初始化
            this.selectTunnelUserControl1.setCurSelectedID(_arr);
            LibEntity.TunnelDefaultSelect tunnelDefaultSelectEntity = LibBusiness.TunnelDefaultSelect.selectDefaultTunnel(WireInfoDbConstNames.TABLE_NAME);
            if (tunnelDefaultSelectEntity != null)
            {
                _arr = new int[5];
                _arr[0] = tunnelDefaultSelectEntity.MineID;
                _arr[1] = tunnelDefaultSelectEntity.HorizontalID;
                _arr[2] = tunnelDefaultSelectEntity.MiningAreaID;
                _arr[3] = tunnelDefaultSelectEntity.WorkingFaceID;
                this.selectTunnelUserControl1.setCurSelectedID(_arr);
            }
            else
            {
                this.selectTunnelUserControl1.loadMineName();
            }
            // 注册委托事件
            this.selectTunnelUserControl1.TunnelNameChanged +=
                InheritTunnelNameChanged;
        }

        /// <summary>
        /// 委托事件
        /// </summary>
        /// <param name="sender"></param>
        private void InheritTunnelNameChanged(object sender, LibCommonForm.TunnelEventArgs e)
        {
            bindDistanceFromWirePoint();
        }

        private void bindDistanceFromWirePoint()
        {
            if (this.Text == "掘进巷道校正")
            {
                _tunnelEntity.TunnelID = selectTunnelUserControl1.ITunnelId;
            }
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            //去掉无用空行
            for (int i = 0; i < dgrdvWire.RowCount - 1; i++)
            {
                if (this.dgrdvWire.Rows[i].Cells[0].Value == null &&
                    this.dgrdvWire.Rows[i].Cells[1].Value == null &&
                    this.dgrdvWire.Rows[i].Cells[2].Value == null &&
                    this.dgrdvWire.Rows[i].Cells[3].Value == null &&
                    this.dgrdvWire.Rows[i].Cells[4].Value == null &&
                    this.dgrdvWire.Rows[i].Cells[5].Value == null &&
                    this.dgrdvWire.Rows[i].Cells[6].Value == null &&
                    this.dgrdvWire.Rows[i].Cells[7].Value == null)
                {
                    this.dgrdvWire.Rows.RemoveAt(i);
                }
            }
            //验证
            if (!check())
            {
                DialogResult = DialogResult.None;
                return;
            }
            List<IPoint> coordinates = new List<IPoint>();
            for (int i = 0; i < this.dgrdvWire.Rows.Count - 1; i++)
            {
                double x = Convert.ToDouble(this.dgrdvWire.Rows[i].Cells[1].Value);
                double y = Convert.ToDouble(this.dgrdvWire.Rows[i].Cells[2].Value);
                double z = Convert.ToDouble(this.dgrdvWire.Rows[i].Cells[3].Value);
                IPoint pnt = new PointClass();
                pnt.X = x;
                pnt.Y = y;
                pnt.Z = z;
                coordinates.Add(pnt);
            }
            DataSet dst = LibBusiness.TunnelInfoBLL.selectOneTunnelInfoByTunnelID(_tunnelEntity.TunnelID);

            Global.cons.DrawJJJZ(_tunnelEntity.TunnelID.ToString(), coordinates, Convert.ToDouble(dst.Tables[0].Rows[0][TunnelInfoDbConstNames.TUNNEL_WID]), 0, 0, Global.searchlen, Global.sxjl, 1);
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        /// <summary>
        /// 验证
        /// </summary>
        /// <returns></returns>
        private bool check()
        {
            // 判断导线点编号是否入力
            if (this.dgrdvWire.Rows.Count - 1 == 0)
            {
                Alert.alert(Const.MSG_PLEASE_TYPE_IN + Const_GM.WIRE_POINT_ID + Const.SIGN_EXCLAMATION_MARK);
                return false;
            }
            //dgrdvWire内部判断
            for (int i = 0; i < this.dgrdvWire.RowCount; i++)
            {
                // 最后一行为空行时，跳出循环
                if (i == dgrdvWire.RowCount - 1)
                {
                    break;
                }
                DataGridViewTextBoxCell cell = dgrdvWire.Rows[i].Cells[0] as DataGridViewTextBoxCell;
                // 判断导线点编号是否入力
                if (cell.Value == null)
                {
                    cell.Style.BackColor = Const.ERROR_FIELD_COLOR;
                    Alert.alert(Const_GM.WIRE_POINT_ID + Const.MSG_NOT_NULL + Const.SIGN_EXCLAMATION_MARK);
                    return false;
                }
                else
                {
                    cell.Style.BackColor = Const.NO_ERROR_FIELD_COLOR;
                }
                //判断导线点编号是否存在
                if (this.Text == Const_GM.WIRE_INFO_ADD)
                {
                    //导线点是否存在
                    if (new WireInfoBLL().isWirePointExist(dgrdvWire.Rows[i].Cells[0].Value.ToString(), wireInfoEntity.WireInfoID))
                    {
                        cell.Style.BackColor = Const.ERROR_FIELD_COLOR;
                        Alert.alert(Const_GM.WIRE_POINT_ID + Const.MSG_ALREADY_HAVE + Const.SIGN_EXCLAMATION_MARK);
                        return false;
                    }
                    else
                    {
                        cell.Style.BackColor = Const.NO_ERROR_FIELD_COLOR;
                    }
                }
                //判断导线点编号是否有输入重复
                for (int j = 0; j < i; j++)
                {
                    if (dgrdvWire[0, j].Value.ToString() == dgrdvWire[0, i].Value.ToString())
                    {
                        cell.Style.BackColor = Const.ERROR_FIELD_COLOR;
                        dgrdvWire[0, j].Style.BackColor = Const.ERROR_FIELD_COLOR;
                        Alert.alert(Const_GM.WIRE_POINT_ID + Const.MSG_DOUBLE_EXISTS + Const.SIGN_EXCLAMATION_MARK);
                        return false;
                    }
                    else
                    {
                        cell.Style.BackColor = Const.NO_ERROR_FIELD_COLOR;
                        dgrdvWire[0, j].Style.BackColor = Const.NO_ERROR_FIELD_COLOR;
                    }
                }

                //判断坐标X是否入力
                cell = dgrdvWire.Rows[i].Cells[1] as DataGridViewTextBoxCell;
                if (cell.Value == null)
                {
                    cell.Style.BackColor = Const.ERROR_FIELD_COLOR;
                    Alert.alert(Const.rowNotNull(i, Const_GM.X));
                    return false;
                }
                else
                {
                    cell.Style.BackColor = Const.NO_ERROR_FIELD_COLOR;
                }

                // 判断坐标X是否为数字
                if (!Validator.IsNumeric(cell.Value.ToString()))
                {
                    cell.Style.BackColor = Const.ERROR_FIELD_COLOR;
                    Alert.alert(Const.rowMustBeNumber(i, Const_GM.X));
                    return false;
                }
                else
                {
                    cell.Style.BackColor = Const.NO_ERROR_FIELD_COLOR;
                }
                //判断坐标Y是否入力
                cell = dgrdvWire.Rows[i].Cells[2] as DataGridViewTextBoxCell;
                if (cell.Value == null)
                {
                    cell.Style.BackColor = Const.ERROR_FIELD_COLOR;
                    Alert.alert(Const.rowNotNull(i, Const_GM.Y));
                    return false;
                }
                else
                {
                    cell.Style.BackColor = Const.NO_ERROR_FIELD_COLOR;
                }

                // 判断坐标Y是否为数字
                if (!Validator.IsNumeric(cell.Value.ToString()))
                {
                    cell.Style.BackColor = Const.ERROR_FIELD_COLOR;
                    Alert.alert(Const.rowMustBeNumber(i, Const_GM.Y));
                    return false;
                }
                else
                {
                    cell.Style.BackColor = Const.NO_ERROR_FIELD_COLOR;
                }
                //判断坐标Z是否入力
                cell = dgrdvWire.Rows[i].Cells[3] as DataGridViewTextBoxCell;
                if (cell.Value == null)
                {
                    cell.Style.BackColor = Const.ERROR_FIELD_COLOR;
                    Alert.alert(Const.rowNotNull(i, Const_GM.Z));
                    return false;
                }
                else
                {
                    cell.Style.BackColor = Const.NO_ERROR_FIELD_COLOR;
                }

                // 判断坐标Z是否为数字
                if (!Validator.IsNumeric(cell.Value.ToString()))
                {
                    cell.Style.BackColor = Const.ERROR_FIELD_COLOR;
                    Alert.alert(Const.rowMustBeNumber(i, Const_GM.Z));
                    return false;
                }
                else
                {
                    cell.Style.BackColor = Const.NO_ERROR_FIELD_COLOR;
                }
                //判断距左帮距离是否入力
                cell = dgrdvWire.Rows[i].Cells[4] as DataGridViewTextBoxCell;
                if (cell.Value == null)
                {
                    cell.Style.BackColor = Const.ERROR_FIELD_COLOR;
                    Alert.alert(Const.rowNotNull(i, Const_GM.DISTANCE_TO_LEFT));
                    return false;
                }
                else
                {
                    cell.Style.BackColor = Const.NO_ERROR_FIELD_COLOR;
                }

                // 判断距左帮距离是否为数字
                if (!Validator.IsNumeric(cell.Value.ToString()))
                {
                    cell.Style.BackColor = Const.ERROR_FIELD_COLOR;
                    Alert.alert(Const.rowMustBeNumber(i, Const_GM.DISTANCE_TO_LEFT));
                    return false;
                }
                else
                {
                    cell.Style.BackColor = Const.NO_ERROR_FIELD_COLOR;
                }
                //判断距右帮距离是否入力
                cell = dgrdvWire.Rows[i].Cells[5] as DataGridViewTextBoxCell;
                if (cell.Value == null)
                {
                    cell.Style.BackColor = Const.ERROR_FIELD_COLOR;
                    Alert.alert(Const.rowNotNull(i, Const_GM.DISTANCE_TO_RIGHT));
                    return false;
                }
                else
                {
                    cell.Style.BackColor = Const.NO_ERROR_FIELD_COLOR;
                }

                // 判断距右帮距离是否为数字
                if (!Validator.IsNumeric(cell.Value.ToString()))
                {
                    cell.Style.BackColor = Const.ERROR_FIELD_COLOR;
                    Alert.alert(Const.rowMustBeNumber(i, Const_GM.DISTANCE_TO_RIGHT));
                    return false;
                }
                else
                {
                    cell.Style.BackColor = Const.NO_ERROR_FIELD_COLOR;
                }
                cell = dgrdvWire.Rows[i].Cells[6] as DataGridViewTextBoxCell;
                // 判断距顶板距离是否为数字
                if (cell.Value != null && !Validator.IsNumeric(cell.Value.ToString()))
                {
                    cell.Style.BackColor = Const.ERROR_FIELD_COLOR;
                    Alert.alert(Const.rowMustBeNumber(i, Const_GM.DISTANCE_TO_TOP));
                    return false;
                }
                else
                {
                    cell.Style.BackColor = Const.NO_ERROR_FIELD_COLOR;
                }
                cell = dgrdvWire.Rows[i].Cells[7] as DataGridViewTextBoxCell;
                // 判断距底板距离是否为数字
                if (cell.Value != null && !Validator.IsNumeric(cell.Value.ToString()))
                {
                    cell.Style.BackColor = Const.ERROR_FIELD_COLOR;
                    Alert.alert(Const.rowMustBeNumber(i, Const_GM.DISTANCE_TO_BOTTOM));
                    return false;
                }
                else
                {
                    cell.Style.BackColor = Const.NO_ERROR_FIELD_COLOR;
                }
            }
            //验证成功
            return true;
        }

        private void selectTunnelUserControl1_Load(object sender, EventArgs e)
        {
            selectTunnelUserControl1.SetFilterOn(TunnelTypeEnum.TUNNELLING, TunnelTypeEnum.OTHER);
        }
    }
}
