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
using System.Globalization;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using LibEntity;
using LibBusiness;
using LibCommon;
using ESRI.ArcGIS.Geometry;
using GIS.Common;
using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.Geodatabase;
using LibCommonForm;
using LibCommonControl;
using LibSocket;

namespace _4.OutburstPrevention
{
    public partial class K1ValueEntering : Form
    {
        #region ******各种变量定义******
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
        #endregion

        /// <summary>
        /// 添加
        /// </summary>
        public K1ValueEntering()
        {
            InitializeComponent();

            //设置窗体属性
            LibCommon.FormDefaultPropertiesSetter.SetEnteringFormDefaultProperties(this, Const_OP.K1_VALUE_ADD);
            //this.selectTunnelUserControl1.init(mainFrm);
            //this.selectTunnelUserControl1.TunnelNameChanged += BindCoordinate;
            //自定义控件设置
            //this.selectTunnelUserControl1.loadMineName();


        }

        //private void BindCoordinate(object sender, TunnelEventArgs e)
        //{
        //    Tunnel tunnel = Tunnel.Find(selectTunnelSimple1.ITunnelId);
        //    tbCoordinateX.Text = tunnel.WorkingFace.Coordinate.X.ToString(CultureInfo.InvariantCulture);
        //    tbCoordinateY.Text = tunnel.WorkingFace.Coordinate.Y.ToString(CultureInfo.InvariantCulture);
        //    tbCoordinateZ.Text = tunnel.WorkingFace.Coordinate.Z.ToString(CultureInfo.InvariantCulture);
        //}

        /// <summary>
        /// 修改
        /// </summary>
        /// <param name="array"></param>
        public K1ValueEntering(int[] array, int id)
        {
            arr = array;
            _k1ValueEntity.Id = id;

            InitializeComponent();

            //窗体属性设置
            LibCommon.FormDefaultPropertiesSetter.SetEnteringFormDefaultProperties(this, Const_OP.K1_VALUE_CHANGE);
            //this.selectTunnelUserControl1.init(mainFrm);
            //自定义控件设置
            //this.selectTunnelUserControl1.setCurSelectedID(arr); 
        }

        /// <summary>
        /// 初始化
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void K1Value_Load(object sender, EventArgs e)
        {
            //this.selectTunnelSimple1.TunnelNameChanged += BindCoordinate;

            //设置Datagridview样式
            setDataGridViewStyle();

            if (this.Text == Const_OP.K1_VALUE_CHANGE)
            {
                change();
            }
            dtp.Visible = false;
            dtp.Format = DateTimePickerFormat.Custom;
            dtp.CustomFormat = Const.DATE_FORMART_H_MM_SS;
            dtp.TextChanged += dtp_TextChanged;
            dgrdvK1Value.Controls.Add(dtp);
        }

        /// <summary>
        /// 修改时绑定datagridview值
        /// </summary>
        private void change()
        {
            //实体赋值
            _k1ValueEntity = K1ValueBLL.selectValueK1ByID(_k1ValueEntity.Id);
            //K1值实体组
            k1Entitys = K1ValueBLL.selectValueK1ByK1ValueID(_k1ValueEntity.K1ValueId);

            for (int i = 0; i < k1Entitys.Length; i++)
            {

                //干煤K1值
                dgrdvK1Value[0, i].Value = k1Entitys[i].ValueK1Dry;
                //湿煤K1值
                dgrdvK1Value[1, i].Value = k1Entitys[i].ValueK1Wet;
                //Sg
                dgrdvK1Value[2, i].Value = k1Entitys[i].Sg;
                //Sv
                dgrdvK1Value[3, i].Value = k1Entitys[i].Sv;
                //q
                dgrdvK1Value[4, i].Value = k1Entitys[i].Q;
                //孔深
                dgrdvK1Value[5, i].Value = k1Entitys[i].BoreholeDeep;
                //记录时间
                dgrdvK1Value[6, i].Value = k1Entitys[i].Time;
                //录入时间
                dgrdvK1Value[7, i].Value = k1Entitys[i].TypeInTime;

                if (dgrdvK1Value.Rows[dgrdvK1Value.Rows.Count - 1].IsNewRow)
                {
                    dgrdvK1Value.Rows.Add();
                    dgrdvK1Value[0, dgrdvK1Value.Rows.Count - 2].Value = dgrdvK1Value[0, dgrdvK1Value.Rows.Count - 1].Value;
                    dgrdvK1Value[1, dgrdvK1Value.Rows.Count - 2].Value = dgrdvK1Value[1, dgrdvK1Value.Rows.Count - 1].Value;
                    dgrdvK1Value[2, dgrdvK1Value.Rows.Count - 2].Value = dgrdvK1Value[2, dgrdvK1Value.Rows.Count - 1].Value;
                    dgrdvK1Value[3, dgrdvK1Value.Rows.Count - 2].Value = dgrdvK1Value[3, dgrdvK1Value.Rows.Count - 1].Value;
                    dgrdvK1Value[4, dgrdvK1Value.Rows.Count - 2].Value = dgrdvK1Value[4, dgrdvK1Value.Rows.Count - 1].Value;
                    dgrdvK1Value[5, dgrdvK1Value.Rows.Count - 2].Value = dgrdvK1Value[5, dgrdvK1Value.Rows.Count - 1].Value;
                    dgrdvK1Value[6, dgrdvK1Value.Rows.Count - 2].Value = dgrdvK1Value[6, dgrdvK1Value.Rows.Count - 1].Value;
                    dgrdvK1Value[7, dgrdvK1Value.Rows.Count - 2].Value = dgrdvK1Value[7, dgrdvK1Value.Rows.Count - 1].Value;
                    dgrdvK1Value[0, dgrdvK1Value.Rows.Count - 1].Value = null;
                    dgrdvK1Value[1, dgrdvK1Value.Rows.Count - 1].Value = null;
                    dgrdvK1Value[2, dgrdvK1Value.Rows.Count - 1].Value = null;
                    dgrdvK1Value[3, dgrdvK1Value.Rows.Count - 1].Value = null;
                    dgrdvK1Value[4, dgrdvK1Value.Rows.Count - 1].Value = null;
                    dgrdvK1Value[5, dgrdvK1Value.Rows.Count - 1].Value = null;
                    dgrdvK1Value[6, dgrdvK1Value.Rows.Count - 1].Value = null;
                    dgrdvK1Value[7, dgrdvK1Value.Rows.Count - 1].Value = null;
                }
                //修改列为选中状态
                if (k1Entitys[i].Id == _k1ValueEntity.Id)
                {
                    dgrdvK1Value.Rows[i].Selected = true;
                }
                Tunnel tunnelEntity = k1Entitys[i].Tunnel; // TunnelInfoBLL.selectTunnelInfoByTunnelID(iTunnelID);
                selectTunnelSimple1.SetTunnel(tunnelEntity);

            }
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

            //tunnelEntity.Tunnel = selectTunnelSimple1.ITunnelId;
            tunnelEntity = selectTunnelSimple1.SelectedTunnel;
            //TunnelInfoBLL.selectTunnelInfoByTunnelID(tunnelEntity.Tunnel);
            //添加
            if (this.Text == Const_OP.K1_VALUE_ADD)
            {
                insertK1Value();
            }
            //修改
            if (this.Text == Const_OP.K1_VALUE_CHANGE)
            {
                updateK1Value();
            }
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
                //K1分组ID
                _k1ValueEntity.K1ValueId = groupCount + 1;
                //拾取点X
                if (double.TryParse(tbCoordinateX.Text, out tmpDouble))
                {
                    _k1ValueEntity.CoordinateX = tmpDouble;
                    tmpDouble = 0;
                }
                //拾取点Y
                if (double.TryParse(tbCoordinateY.Text, out tmpDouble))
                {
                    _k1ValueEntity.CoordinateY = tmpDouble;
                    tmpDouble = 0;
                }
                //拾取点Z
                if (double.TryParse(tbCoordinateZ.Text, out tmpDouble))
                {
                    _k1ValueEntity.CoordinateZ = tmpDouble;
                    tmpDouble = 0;
                }
                //干煤K1值
                if (dgrdvK1Value[0, i].Value == null || double.TryParse(dgrdvK1Value[0, i].Value.ToString(), out tmpDouble))
                {
                    _k1ValueEntity.ValueK1Dry = tmpDouble;
                    tmpDouble = 0;
                }
                //湿煤K1值
                if (dgrdvK1Value[1, i].Value == null || double.TryParse(dgrdvK1Value[1, i].Value.ToString(), out tmpDouble))
                {
                    _k1ValueEntity.ValueK1Wet = tmpDouble;
                    tmpDouble = 0;
                }
                //Sg
                if (dgrdvK1Value[2, i].Value == null || double.TryParse(dgrdvK1Value[2, i].Value.ToString(), out tmpDouble))
                {
                    _k1ValueEntity.Sg = tmpDouble;
                    tmpDouble = 0;
                }
                //Sv
                if (dgrdvK1Value[3, i].Value == null || double.TryParse(dgrdvK1Value[3, i].Value.ToString(), out tmpDouble))
                {
                    _k1ValueEntity.Sv = tmpDouble;
                    tmpDouble = 0;
                }
                //q
                if (dgrdvK1Value[4, i].Value == null || double.TryParse(dgrdvK1Value[4, i].Value.ToString(), out tmpDouble))
                {
                    _k1ValueEntity.Q = tmpDouble;
                    tmpDouble = 0;
                }
                _k1ValueEntity.BoreholeDeep = dgrdvK1Value[5, i].Value == null
                    ? 0.0
                    : Convert.ToDouble(dgrdvK1Value[5, i].Value);
                //记录时间
                _k1ValueEntity.Time = dgrdvK1Value[6, i].Value != null ? Convert.ToDateTime(dgrdvK1Value[6, i].Value) : DateTime.Now;
                //录入时间
                _k1ValueEntity.TypeInTime = dgrdvK1Value[7, i].Value != null ? Convert.ToDateTime(dgrdvK1Value[7, i].Value) : DateTime.Now;
                //巷道ID
                _k1ValueEntity.Tunnel = tunnelEntity;
                //添加
                bResult = K1ValueBLL.insertValueK1(_k1ValueEntity);
                if (bResult)
                {
                    //TODO:添加成功
                    UpdateWarningDataMsg msg = new UpdateWarningDataMsg(tunnelEntity.WorkingFace.WorkingFaceId, tunnelEntity.TunnelId, K1ValueDbConstNames.TABLE_NAME, OPERATION_TYPE.ADD, DateTime.Now);
                    SocketUtil.SendMsg2Server(msg);
                    DrawGasGushQuantityPt();
                }
                else
                {
                    return;
                }
            }
        }

        /// <summary>
        /// 修改K1Value值
        /// </summary>
        private void updateK1Value()
        {
            //数据行数
            rowCount = dgrdvK1Value.Rows.Count - 1;
            //分组数
            groupCount = K1ValueBLL.selectValueK1GroupCount();

            bool bResult = false;

            for (int i = 0; i < rowCount; i++)
            {
                //K1分组ID
                _k1ValueEntity.K1ValueId = groupCount + 1;
                //拾取点X
                if (double.TryParse(tbCoordinateX.Text, out tmpDouble))
                {
                    _k1ValueEntity.CoordinateX = tmpDouble;
                    tmpDouble = 0;
                }
                //拾取点Y
                if (double.TryParse(tbCoordinateY.Text, out tmpDouble))
                {
                    _k1ValueEntity.CoordinateY = tmpDouble;
                    tmpDouble = 0;
                }
                //拾取点Z
                if (double.TryParse(tbCoordinateZ.Text, out tmpDouble))
                {
                    _k1ValueEntity.CoordinateZ = tmpDouble;
                    tmpDouble = 0;
                }
                //干煤K1值
                if (dgrdvK1Value[0, i].Value == null || double.TryParse(dgrdvK1Value[0, i].Value.ToString(), out tmpDouble))
                {
                    _k1ValueEntity.ValueK1Dry = tmpDouble;
                    tmpDouble = 0;
                }
                //湿煤K1值
                if (dgrdvK1Value[1, i].Value == null || double.TryParse(dgrdvK1Value[1, i].Value.ToString(), out tmpDouble))
                {
                    _k1ValueEntity.ValueK1Wet = tmpDouble;
                    tmpDouble = 0;
                }
                //Sg
                if (dgrdvK1Value[2, i].Value == null || double.TryParse(dgrdvK1Value[2, i].Value.ToString(), out tmpDouble))
                {
                    _k1ValueEntity.Sg = tmpDouble;
                    tmpDouble = 0;
                }
                //Sv
                if (dgrdvK1Value[3, i].Value == null || double.TryParse(dgrdvK1Value[3, i].Value.ToString(), out tmpDouble))
                {
                    _k1ValueEntity.Sv = tmpDouble;
                    tmpDouble = 0;
                }
                //q
                if (dgrdvK1Value[4, i].Value == null || double.TryParse(dgrdvK1Value[4, i].Value.ToString(), out tmpDouble))
                {
                    _k1ValueEntity.Q = tmpDouble;
                    tmpDouble = 0;
                }
                _k1ValueEntity.BoreholeDeep = dgrdvK1Value[5, i].Value == null
                    ? 0.0
                    : Convert.ToDouble(dgrdvK1Value[5, i].Value);
                //记录时间
                _k1ValueEntity.Time = dgrdvK1Value[6, i].Value != null ? Convert.ToDateTime(dgrdvK1Value[6, i].Value) : DateTime.Now;
                //录入时间
                _k1ValueEntity.TypeInTime = dgrdvK1Value[7, i].Value != null ? Convert.ToDateTime(dgrdvK1Value[7, i].Value) : DateTime.Now;
                //巷道ID
                _k1ValueEntity.Tunnel.TunnelId = tunnelEntity.TunnelId;
                //添加

                if (i < k1Entitys.Length)
                {
                    //K1Value编号
                    _k1ValueEntity.Id = k1Entitys[i].Id;
                    //修改
                    bResult = K1ValueBLL.updateValueK1(_k1ValueEntity);
                    if (bResult)
                    {
                        //TODO:修改成功
                        UpdateWarningDataMsg msg = new UpdateWarningDataMsg(tunnelEntity.WorkingFace.WorkingFaceId, tunnelEntity.TunnelId, K1ValueDbConstNames.TABLE_NAME, OPERATION_TYPE.ADD, DateTime.Now);
                        SocketUtil.SendMsg2Server(msg);
                        DelGasGushQuantityPt("", "");
                        DrawGasGushQuantityPt();
                    }
                }
                //新添加部分
                else
                {
                    //添加
                    bResult = K1ValueBLL.insertValueK1(_k1ValueEntity);
                    if (bResult)
                    {
                        //TODO:添加成功
                        DrawGasGushQuantityPt();
                    }
                }
                if (!bResult)
                {
                    return;
                }
            }
            //数据少于原数据条数
            if (rowCount < k1Entitys.Length)
            {
                for (int i = rowCount; i < k1Entitys.Length; i++)
                {
                    //K1Value编号
                    _k1ValueEntity.Id = k1Entitys[i].Id;
                    //删除
                    bResult = K1ValueBLL.deleteK1Value(_k1ValueEntity, 0);
                    if (bResult)
                    {
                        //TODO:删除成功
                        DelGasGushQuantityPt("", "");
                    }
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
            //拾取点自动填充
            if (e.ColumnIndex != 8 && dgrdvK1Value[0, e.RowIndex].Value == null && dgrdvK1Value[1, e.RowIndex].Value == null && dgrdvK1Value[2, e.RowIndex].Value == null && e.ColumnIndex > 2)
            {
                if (dgrdvK1Value.Rows.Count < 8 && dgrdvK1Value.Rows.Count > 1)
                {
                    if (e.RowIndex == 0)
                    {
                        if (dgrdvK1Value[0, e.RowIndex].Value == null && dgrdvK1Value[1, e.RowIndex].Value == null && dgrdvK1Value[2, e.RowIndex].Value == null)
                        {
                            dgrdvK1Value[0, e.RowIndex].Value = dgrdvK1Value[0, e.RowIndex + 1].Value;
                            dgrdvK1Value[1, e.RowIndex].Value = dgrdvK1Value[1, e.RowIndex + 1].Value;
                            dgrdvK1Value[2, e.RowIndex].Value = dgrdvK1Value[2, e.RowIndex + 1].Value;
                        }
                    }
                    else
                    {
                        dgrdvK1Value[0, e.RowIndex].Value = dgrdvK1Value[0, e.RowIndex - 1].Value;
                        dgrdvK1Value[1, e.RowIndex].Value = dgrdvK1Value[1, e.RowIndex - 1].Value;
                        dgrdvK1Value[2, e.RowIndex].Value = dgrdvK1Value[2, e.RowIndex - 1].Value;
                    }
                }
            }
            dtp.Visible = false;
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
            for (int i = 0; i < this.dgrdvK1Value.RowCount - 1; i++)
            {
                DataGridViewTextBoxCell cell = dgrdvK1Value.Rows[i].Cells[0] as DataGridViewTextBoxCell;
                //if (cell.Value == null)
                //{

                //    Alert.alert(Const.rowNotNull(i, Const_OP.K1_VALUE_COORDINATE_X));
                //    return false;
                //}
                //else
                //{
                //    cell.Style.BackColor = Const.NO_ERROR_FIELD_COLOR;
                //}
                //if (!Validator.IsNumeric(cell.Value.ToString()))
                //{
                //    Alert.alert(Const.rowMustBeNumber(i, Const_OP.K1_VALUE_COORDINATE_X));
                //    cell.Style.BackColor = Const.ERROR_FIELD_COLOR;
                //    return false;
                //}
                //else
                //{
                //    cell.Style.BackColor = Const.NO_ERROR_FIELD_COLOR;
                //}
                //cell = dgrdvK1Value.Rows[i].Cells[1] as DataGridViewTextBoxCell;
                //if (cell.Value == null)
                //{
                //    cell.Style.BackColor = Const.ERROR_FIELD_COLOR;
                //    Alert.alert(Const.rowNotNull(i, Const_OP.K1_VALUE_COORDINATE_Y));
                //    return false;
                //}
                //else
                //{
                //    cell.Style.BackColor = Const.NO_ERROR_FIELD_COLOR;
                //}
                //if (!Validator.IsNumeric(cell.Value.ToString()))
                //{
                //    Alert.alert(Const.rowMustBeNumber(i, Const_OP.K1_VALUE_COORDINATE_Y));
                //    cell.Style.BackColor = Const.ERROR_FIELD_COLOR;
                //    return false;
                //}
                //else
                //{
                //    cell.Style.BackColor = Const.NO_ERROR_FIELD_COLOR;
                //}
                //cell = dgrdvK1Value.Rows[i].Cells[2] as DataGridViewTextBoxCell;
                //if (cell.Value == null)
                //{
                //    cell.Style.BackColor = Const.ERROR_FIELD_COLOR;
                //    Alert.alert(Const.rowNotNull(i, Const_OP.K1_VALUE_COORDINATE_Z));
                //    return false;
                //}
                //else
                //{
                //    cell.Style.BackColor = Const.NO_ERROR_FIELD_COLOR;
                //}
                //if (!Validator.IsNumeric(cell.Value.ToString()))
                //{
                //    Alert.alert(Const.rowMustBeNumber(i, Const_OP.K1_VALUE_COORDINATE_Z));
                //    cell.Style.BackColor = Const.ERROR_FIELD_COLOR;
                //    return false;
                //}
                //else
                //{
                //    cell.Style.BackColor = Const.NO_ERROR_FIELD_COLOR;
                //}
                //cell = dgrdvK1Value.Rows[i].Cells[3] as DataGridViewTextBoxCell;
                //if (cell.Value == null)
                //{
                //    cell.Style.BackColor = Const.ERROR_FIELD_COLOR;
                //    Alert.alert(Const.rowNotNull(i, Const_OP.K1_VALUE_DRY));
                //    return false;
                //}
                //else
                //{
                //    cell.Style.BackColor = Const.NO_ERROR_FIELD_COLOR;
                //}
                //if (!Validator.IsNumeric(cell.Value.ToString()))
                //{
                //    Alert.alert(Const.rowNotNull(i, Const_OP.K1_VALUE_DRY));
                //    cell.Style.BackColor = Const.ERROR_FIELD_COLOR;
                //    return false;
                //}
                //else
                //{
                //    cell.Style.BackColor = Const.NO_ERROR_FIELD_COLOR;
                //}
                //cell = dgrdvK1Value.Rows[i].Cells[4] as DataGridViewTextBoxCell;
                //if (cell.Value == null)
                //{
                //    cell.Style.BackColor = Const.ERROR_FIELD_COLOR;
                //    Alert.alert(Const.rowNotNull(i, Const_OP.K1_VALUE_WET));
                //    return false;
                //}
                //else
                //{
                //    cell.Style.BackColor = Const.NO_ERROR_FIELD_COLOR;
                //}
                //if (!Validator.IsNumeric(cell.Value.ToString()))
                //{
                //    Alert.alert(Const.rowMustBeNumber(i, Const_OP.K1_VALUE_WET));
                //    cell.Style.BackColor = Const.ERROR_FIELD_COLOR;
                //    return false;
                //}
                //else
                //{
                //    cell.Style.BackColor = Const.NO_ERROR_FIELD_COLOR;
                //}
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


        /// <summary>
        /// 20140801SDE中添加
        /// </summary>
        private void DrawGasGushQuantityPt()
        {
            //IPoint pt = new PointClass();
            //pt.X = _k1ValueEntity.CoordinateX;
            //pt.Y = _k1ValueEntity.CoordinateY;
            //pt.Z = _k1ValueEntity.CoordinateZ;
            //ILayer pLayer = DataEditCommon.GetLayerByName(DataEditCommon.g_pMap, GIS.LayerNames.MR_K1_LAYER_NAME);
            //IFeatureLayer pFeatureLayer = (IFeatureLayer)pLayer;
            //IGeometry geometry = pt;
            //List<ziduan> list = new List<ziduan>();
            //list.Add(new ziduan("bid", _k1ValueEntity.K1ValueID.ToString()));
            ////list.Add(new ziduan("mc", gasGushQuantityEntity.CoalSeams.ToString()));
            ////list.Add(new ziduan("addtime", DateTime.Now.ToString()));


            //IFeature pfeature = DataEditCommon.CreateNewFeature(pFeatureLayer, geometry, list);
            //if (pfeature != null)
            //{
            //    DataEditCommon.g_pMyMapCtrl.ActiveView.PartialRefresh(esriViewDrawPhase.esriViewGeography | esriViewDrawPhase.esriViewForeground, null, null);
            //}
        }
        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="bid">绑定ID</param>
        /// <param name="mc">煤层</param>
        private void DelGasGushQuantityPt(string bid, string mc)
        {
            //ILayer pLayer = DataEditCommon.GetLayerByName(DataEditCommon.g_pMap, GIS.LayerNames.MR_K1_LAYER_NAME);
            //IFeatureLayer pFeatureLayer = (IFeatureLayer)pLayer;
            //DataEditCommon.DeleteFeatureByWhereClause(pFeatureLayer, "bid='" + bid + "' and mc='" + mc + "'");
        }
    }
}
