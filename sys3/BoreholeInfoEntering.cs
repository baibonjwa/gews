using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Text;
using System.Windows.Forms;
using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.Geodatabase;
using ESRI.ArcGIS.Geometry;
using GIS;
using GIS.Common;
using LibBusiness;
using LibCommon;
using LibEntity;

namespace sys3
{
    public partial class BoreholeInfoEntering : Form
    {
        /** 钻孔编号 **/
        /** 业务逻辑类型：添加、修改  **/
        private readonly string _bllType = "add";
        private readonly int _boreholeId;
        private string BID = "";
        private object[] _objArrRowData = new object[7];

        /// <summary>
        ///     构造方法
        /// </summary>
        public BoreholeInfoEntering()
        {
            InitializeComponent();

            // 设置窗体默认属性
            FormDefaultPropertiesSetter.SetEnteringFormDefaultProperties(this, Const_GM.INSERT_BOREHOLE_INFO);

            // 加载岩性信息
            loadLithologyInfo();
        }

        public BoreholeInfoEntering(IPoint pt)
        {
            InitializeComponent();

            // 设置窗体默认属性
            FormDefaultPropertiesSetter.SetEnteringFormDefaultProperties(this, Const_GM.INSERT_BOREHOLE_INFO);
            txtCoordinateX.Text = pt.X.ToString();
            txtCoordinateY.Text = pt.Y.ToString();
            if (pt.Z.ToString().Equals("非数字"))
                txtCoordinateZ.Text = "0";
            else
                txtCoordinateZ.Text = pt.Z.ToString();
            // 加载岩性信息
            loadLithologyInfo();
        }

        /// <summary>
        ///     带参数的构造方法
        /// </summary>
        /// <param name="boreholeId">钻孔编号</param>
        public BoreholeInfoEntering(int boreholeId)
        {
            InitializeComponent();

            // 设置窗体默认属性
            FormDefaultPropertiesSetter.SetEnteringFormDefaultProperties(this, Const_GM.UPDATE_BOREHOLE_INFO);

            // 设置业务类型
            _bllType = "update";

            // 钻孔编号
            _boreholeId = boreholeId;

            // 设置钻孔信息
            setBoreholeInfo();
        }

        /// <summary>
        ///     加载岩性信息
        /// </summary>
        private void loadLithologyInfo()
        {
            // 获取岩性信息
            Lithology[] lithologys = Lithology.FindAll();
            if (lithologys.Length > 0)
            {
                foreach (Lithology t in lithologys)
                {
                    LITHOLOGY.Items.Add(t.LithologyName);
                }
            }
        }

        /// <summary>
        ///     提  交
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnSubmit_Click(object sender, EventArgs e)
        {
            // 验证
            if (!check())
            {
                DialogResult = DialogResult.None;
                return;
            }
            DialogResult = DialogResult.OK;

            // 创建钻孔实体
            var breholeEntity = new Borehole();

            // 钻孔编号
            // 添加的时候自动採番，修改的场合使用上层传过来的钻孔编号

            breholeEntity.BoreholeId = _boreholeId;

            // 孔号
            breholeEntity.BoreholeNumber = txtBoreholeNumber.Text.Trim();
            // 地面标高
            double dGroundElevation = 0;
            if (double.TryParse(Convert.ToString(txtGroundElevation.Text.Trim()), out dGroundElevation))
            {
                breholeEntity.GroundElevation = dGroundElevation;
            }
            // X坐标
            double dCoordinateX = 0;
            if (double.TryParse(Convert.ToString(txtCoordinateX.Text.Trim()), out dCoordinateX))
            {
                breholeEntity.CoordinateX = dCoordinateX;
            }
            // Y坐标
            double dCoordinateY = 0;
            if (double.TryParse(Convert.ToString(txtCoordinateY.Text.Trim()), out dCoordinateY))
            {
                breholeEntity.CoordinateY = dCoordinateY;
            }
            // Z坐标
            double dCoordinateZ = 0;
            if (double.TryParse(Convert.ToString(txtCoordinateZ.Text.Trim()), out dCoordinateZ))
            {
                breholeEntity.CoordinateZ = dCoordinateZ;
            }
            // 煤层结构
            breholeEntity.CoalSeamsTexture = string.Empty;

            var boreholeLithologyEntityList = new List<BoreholeLithology>();
            for (int i = 0; i < gvCoalSeamsTexture.RowCount; i++)
            {
                // 最后一行为空行时，跳出循环
                if (i == gvCoalSeamsTexture.RowCount - 1)
                {
                    break;
                }
                // 创建钻孔岩性实体
                var boreholeLithologyEntity = new BoreholeLithology();
                // 钻孔编号
                boreholeLithologyEntity.Borehole.BoreholeId = _boreholeId;
                // 岩性编号
                var cell0 = gvCoalSeamsTexture.Rows[i].Cells[0] as DataGridViewComboBoxCell;
                if (cell0 != null && cell0.Value != null)
                {
                    boreholeLithologyEntity.Lithology = Lithology.FindOneByLithologyName(cell0.Value.ToString());
                }
                // 底板标高
                double dFloorElevation = 0;
                if (double.TryParse(Convert.ToString(gvCoalSeamsTexture.Rows[i].Cells[1].Value),
                    out dFloorElevation))
                {
                    boreholeLithologyEntity.FloorElevation = dFloorElevation;
                }
                // 厚度
                double dThickness = 0;
                if (double.TryParse(Convert.ToString(gvCoalSeamsTexture.Rows[i].Cells[2].Value), out dThickness))
                {
                    boreholeLithologyEntity.Thickness = dThickness;
                }
                // 煤层名称
                if (!Validator.IsEmpty(Convert.ToString(gvCoalSeamsTexture.Rows[i].Cells[3].Value)))
                {
                    boreholeLithologyEntity.CoalSeamsName =
                        gvCoalSeamsTexture.Rows[i].Cells[3].Value.ToString().Trim();
                }
                // 坐标X
                double dCoordinateX1 = 0;
                if (double.TryParse(Convert.ToString(gvCoalSeamsTexture.Rows[i].Cells[4].Value), out dCoordinateX1))
                {
                    boreholeLithologyEntity.CoordinateX = dCoordinateX1;
                }
                // 坐标Y
                double dCoordinateY1 = 0;
                if (double.TryParse(Convert.ToString(gvCoalSeamsTexture.Rows[i].Cells[5].Value), out dCoordinateY1))
                {
                    boreholeLithologyEntity.CoordinateY = dCoordinateY1;
                }
                // 坐标Z
                double dCoordinateZ1 = 0;
                if (double.TryParse(Convert.ToString(gvCoalSeamsTexture.Rows[i].Cells[6].Value), out dCoordinateZ1))
                {
                    boreholeLithologyEntity.CoordinateZ = dCoordinateZ1;
                }

                boreholeLithologyEntityList.Add(boreholeLithologyEntity);
            }

            // 执行结果
            bool bResult = false;
            // 只有当添加新钻孔信息的时候才去判断孔号是否重复
            if (_bllType == "add")
            {
                breholeEntity.BindingId = IDGenerator.NewBindingID();

                // 钻孔信息登录
                breholeEntity.Save();

                // 钻孔岩性信息登录
                if (bResult)
                {
                    foreach (BoreholeLithology boreholeLithologyEntity in boreholeLithologyEntityList)
                    {
                        boreholeLithologyEntity.Save();
                    }
                }
            }
            else
            {
                //获取钻孔BID，为后面绘制钻孔赋值所用
                //string sBID = "";
                //BoreholeBLL.selectBoreholeBIDByBoreholeNum(breholeEntity.BoreholeNumber, out sBID);
                breholeEntity.BindingId = BID;

                breholeEntity.Save();

                // 钻孔岩性信息删除

                BoreholeLithology.DeleteAllByBoreholeId(breholeEntity.BoreholeId);

                // 钻孔岩性信息登录
                foreach (var boreholeLithologyEntity in boreholeLithologyEntityList)
                {
                    boreholeLithologyEntity.Save();
                }
            }


            // 添加/修改成功的场合
            if (bResult)
            {
                // TODO:

                if (breholeEntity.BindingId == null || breholeEntity.BindingId == "") return; //若BID值为空，则不绘制钻孔
                if (_bllType == "add")
                {
                    DialogResult dlgResult = MessageBox.Show("是：见煤钻孔，否：未见煤钻孔，取消：不绘制钻孔", "绘制钻孔",
                        MessageBoxButtons.YesNoCancel);

                    if (dlgResult == DialogResult.Yes)
                    {
                        DrawZuanKong(breholeEntity, boreholeLithologyEntityList[0]);
                    }
                    else if (dlgResult == DialogResult.No)
                    {
                        DrawZuanKong(breholeEntity);
                    }
                    else if (dlgResult == DialogResult.Cancel)
                    {
                        return;
                    }
                }
                else
                {
                    //1.获得当前编辑图层
                    var drawspecial = new DrawSpecialCommon();
                    string sLayerAliasName = LayerNames.DEFALUT_BOREHOLE; //“默认_钻孔”图层
                    IFeatureLayer featureLayer = drawspecial.GetFeatureLayerByName(sLayerAliasName);
                    if (featureLayer == null)
                    {
                        MessageBox.Show("未找到" + sLayerAliasName + "图层,无法删钻孔图元。");
                        return;
                    }

                    bool bIsDeleteOldFeature = DataEditCommon.DeleteFeatureByBId(featureLayer, breholeEntity.BindingId);

                    if (bIsDeleteOldFeature)
                    {
                        DialogResult dlgResult = MessageBox.Show("是：见煤钻孔，否：未见煤钻孔，取消：不绘制钻孔",
                            "绘制钻孔", MessageBoxButtons.YesNoCancel);

                        if (dlgResult == DialogResult.Yes)
                        {
                            DrawZuanKong(breholeEntity, boreholeLithologyEntityList[0]);
                        }
                        else if (dlgResult == DialogResult.No)
                        {
                            DrawZuanKong(breholeEntity);
                        }
                        else if (dlgResult == DialogResult.Cancel)
                        {
                            return;
                        }
                    }
                    else
                    {
                        return;
                    }
                }
                Close();
            }
        }

        /// <summary>
        ///     取  消
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnCancel_Click(object sender, EventArgs e)
        {
            // 关闭窗体
            Close();
        }

        /// <summary>
        ///     验证画面入力数据
        /// </summary>
        /// <returns>验证结果：true 通过验证, false未通过验证</returns>
        private bool check()
        {
            // 判断孔号是否录入
            if (!Check.isEmpty(txtBoreholeNumber, Const_GM.BOREHOLE_NUMBER))
            {
                return false;
            }

            // 判断地面标高是否录入
            if (!Check.isEmpty(txtGroundElevation, Const_GM.GROUND_ELEVATION))
            {
                return false;
            }

            // 判断地面标高是否为数字
            if (!Check.IsNumeric(txtGroundElevation, Const_GM.GROUND_ELEVATION))
            {
                return false;
            }

            // 判断坐标X是否录入
            if (!Check.isEmpty(txtCoordinateX, Const_GM.COORDINATE_X))
            {
                return false;
            }

            // 判断坐标X是否为数字
            if (!Check.IsNumeric(txtCoordinateX, Const_GM.COORDINATE_X))
            {
                return false;
            }

            // 判断坐标Y是否录入
            if (!Check.isEmpty(txtCoordinateY, Const_GM.COORDINATE_Y))
            {
                return false;
            }

            // 判断坐标Y是否为数字
            if (!Check.IsNumeric(txtCoordinateY, Const_GM.COORDINATE_Y))
            {
                return false;
            }

            // 判断坐标Z是否录入
            if (!Check.isEmpty(txtCoordinateZ, Const_GM.COORDINATE_Z))
            {
                return false;
            }

            // 判断坐标Z是否为数字
            if (!Check.IsNumeric(txtCoordinateZ, Const_GM.COORDINATE_Z))
            {
                return false;
            }

            // 判断岩性是否入力
            if (gvCoalSeamsTexture.Rows.Count - 1 == 0)
            {
                Alert.alert(Const_GM.LITHOLOGY_MUST_INPUT); // 请录入煤层的岩性！
                return false;
            }

            // 临时存储煤层名称
            var arrCoalSeamsName = new List<String>();

            // 判断底板标高、厚度是否入力，以及入力的是否为数字
            for (int i = 0; i < gvCoalSeamsTexture.RowCount; i++)
            {
                // 最后一行为空行时，跳出循环
                if (i == gvCoalSeamsTexture.RowCount - 1)
                {
                    break;
                }

                // 岩性
                var cell0 = gvCoalSeamsTexture.Rows[i].Cells[0] as DataGridViewComboBoxCell;
                // 判断岩性是否选择
                if (cell0.Value == null)
                {
                    Alert.alert("第" + (i + 1) + "行，请选择岩性！");
                    return false;
                }

                // 底板标高
                var cell1 = gvCoalSeamsTexture.Rows[i].Cells[1] as DataGridViewTextBoxCell;
                // 判断底板标高是否入力
                if (cell1.Value == null)
                {
                    cell1.Style.BackColor = Const.ERROR_FIELD_COLOR;
                    Alert.alert("第" + (i + 1) + "行，底板标高不能为空！");
                    return false;
                }
                cell1.Style.BackColor = Const.NO_ERROR_FIELD_COLOR;

                // 判断底板标高是否为数字
                if (!Validator.IsNumeric(cell1.Value.ToString()))
                {
                    cell1.Style.BackColor = Const.ERROR_FIELD_COLOR;
                    Alert.alert("第" + (i + 1) + "行，底板标高应为数字！");
                    return false;
                }
                cell1.Style.BackColor = Const.NO_ERROR_FIELD_COLOR;

                // 厚度
                var cell2 = gvCoalSeamsTexture.Rows[i].Cells[2] as DataGridViewTextBoxCell;
                // 判断厚度是否入力
                if (cell2.Value == null)
                {
                    cell2.Style.BackColor = Const.ERROR_FIELD_COLOR;
                    Alert.alert("第" + (i + 1) + "行，厚度不能为空！");
                    return false;
                }
                cell2.Style.BackColor = Const.NO_ERROR_FIELD_COLOR;
                // 判断厚度是否为数字
                if (!Validator.IsNumeric(cell2.Value.ToString()))
                {
                    cell2.Style.BackColor = Const.ERROR_FIELD_COLOR;
                    Alert.alert("第" + (i + 1) + "行，厚度应为数字！");
                    return false;
                }
                cell2.Style.BackColor = Const.NO_ERROR_FIELD_COLOR;

                // 当岩性选择为煤层时
                Lithology lithology = Lithology.FindOneByCoal();
                if (Convert.ToString(cell0.Value) == lithology.LithologyName)
                {
                    // 煤层名称
                    var cell3 = gvCoalSeamsTexture.Rows[i].Cells[3] as DataGridViewTextBoxCell;
                    // 判断煤层名称是否入力
                    if (cell3.Value == null || cell3.Value == "")
                    {
                        // TODO:煤层名称改为非必须录入
                        //cell3.Style.BackColor = Const.ERROR_FIELD_COLOR;
                        //Alert.alert("第" + (i + 1) + "行，煤层名称不能为空！");
                        //return false;
                    }
                    else
                    {
                        cell3.Style.BackColor = Const.NO_ERROR_FIELD_COLOR;

                        // 特殊符号判断
                        //if (Validator.checkSpecialCharacters(cell3.Value.ToString()))
                        //{
                        //    cell3.Style.BackColor = Const.ERROR_FIELD_COLOR;
                        //    Alert.alert("第" + (i + 1) + "行，煤层结构名称包含特殊符号！");
                        //    return false;
                        //}
                        //else
                        //{
                        if (cell3.Value.ToString().Trim() != "")
                        {
                            cell3.Style.BackColor = Const.NO_ERROR_FIELD_COLOR;

                            // 煤层名称不能重复
                            if (!arrCoalSeamsName.Contains(cell3.Value.ToString().Trim()))
                            {
                                arrCoalSeamsName.Add(cell3.Value.ToString().Trim());
                                cell3.Style.BackColor = Const.NO_ERROR_FIELD_COLOR;
                            }
                            else
                            {
                                cell3.Style.BackColor = Const.ERROR_FIELD_COLOR;
                                Alert.alert("第" + (i + 1) + "行，煤层名称重复！（同一钻孔不能有相同的煤层名称）");
                                return false;
                            }
                            //}
                        }
                    }

                    // TODO:钻孔岩性的坐标改成非必须录入
                    //// 坐标X
                    //DataGridViewTextBoxCell cell4 = gvCoalSeamsTexture.Rows[i].Cells[4] as DataGridViewTextBoxCell;
                    //// 判断坐标X是否入力
                    //if (cell4.Value == null)
                    //{
                    //    cell4.Style.BackColor = Const.ERROR_FIELD_COLOR;
                    //    Alert.alert("第" + (i + 1) + "行，坐标X不能为空！");
                    //    return false;
                    //}
                    //else
                    //{
                    //    cell4.Style.BackColor = Const.NO_ERROR_FIELD_COLOR;
                    //}

                    //// 坐标Y
                    //DataGridViewTextBoxCell cell5 = gvCoalSeamsTexture.Rows[i].Cells[5] as DataGridViewTextBoxCell;
                    //// 判断坐标Y是否入力
                    //if (cell5.Value == null)
                    //{
                    //    cell5.Style.BackColor = Const.ERROR_FIELD_COLOR;
                    //    Alert.alert("第" + (i + 1) + "行，坐标Y不能为空！");
                    //    return false;
                    //}
                    //else
                    //{
                    //    cell5.Style.BackColor = Const.NO_ERROR_FIELD_COLOR;
                    //}

                    //// 坐标Z
                    //DataGridViewTextBoxCell cell6 = gvCoalSeamsTexture.Rows[i].Cells[6] as DataGridViewTextBoxCell;
                    //// 判断坐标Z是否入力
                    //if (cell6.Value == null)
                    //{
                    //    cell6.Style.BackColor = Const.ERROR_FIELD_COLOR;
                    //    Alert.alert("第" + (i + 1) + "行，坐标Z不能为空！");
                    //    return false;
                    //}
                    //else
                    //{
                    //    cell6.Style.BackColor = Const.NO_ERROR_FIELD_COLOR;
                    //}
                }

                var cell40 = gvCoalSeamsTexture.Rows[i].Cells[4] as DataGridViewTextBoxCell;
                // 判断坐标X是否为数字
                if (!Validator.IsNumeric(Convert.ToString(cell40.Value)))
                {
                    cell40.Style.BackColor = Const.ERROR_FIELD_COLOR;
                    Alert.alert("第" + (i + 1) + "行，坐标X应为数字！");
                    return false;
                }
                cell40.Style.BackColor = Const.NO_ERROR_FIELD_COLOR;

                var cell50 = gvCoalSeamsTexture.Rows[i].Cells[5] as DataGridViewTextBoxCell;
                // 判断坐标Y是否为数字
                if (!Validator.IsNumeric(Convert.ToString(cell50.Value)))
                {
                    cell50.Style.BackColor = Const.ERROR_FIELD_COLOR;
                    Alert.alert("第" + (i + 1) + "行，坐标Y应为数字！");
                    return false;
                }
                cell50.Style.BackColor = Const.NO_ERROR_FIELD_COLOR;

                var cell60 = gvCoalSeamsTexture.Rows[i].Cells[6] as DataGridViewTextBoxCell;
                // 判断坐标Z是否为数字
                if (!Validator.IsNumeric(Convert.ToString(cell60.Value)))
                {
                    cell60.Style.BackColor = Const.ERROR_FIELD_COLOR;
                    Alert.alert("第" + (i + 1) + "行，坐标Z应为数字！");
                    return false;
                }
                cell60.Style.BackColor = Const.NO_ERROR_FIELD_COLOR;
            }

            // 验证通过
            return true;
        }

        /// <summary>
        ///     设置钻孔信息
        /// </summary>
        private void setBoreholeInfo()
        {
            // 获取钻孔信息
            Borehole brehole = Borehole.Find(_boreholeId);

            // 获取钻孔岩性信息（煤层结构）
            var boreholeLithologies = BoreholeLithology.FindAllByBoreholeId(_boreholeId);

            if (brehole != null)
            {
                //BID
                BID = brehole.BindingId;
                // 孔号
                txtBoreholeNumber.Text = brehole.BoreholeNumber;
                // 地面标高
                txtGroundElevation.Text = brehole.GroundElevation.ToString(CultureInfo.InvariantCulture);
                // X坐标
                txtCoordinateX.Text = brehole.CoordinateX.ToString(CultureInfo.InvariantCulture);
                // Y坐标
                txtCoordinateY.Text = brehole.CoordinateY.ToString(CultureInfo.InvariantCulture);
                // Z坐标
                txtCoordinateZ.Text = brehole.CoordinateZ.ToString(CultureInfo.InvariantCulture);

                // 获取岩性信息
                Lithology[] lithologys = Lithology.FindAll();
                if (lithologys.Length > 0)
                {

                    foreach (Lithology t in lithologys)
                    {
                        LITHOLOGY.Items.Add(t.LithologyName);
                    }
                }

                // 明细
                gvCoalSeamsTexture.RowCount = boreholeLithologies.Length + 1;
                for (int i = 0; i < boreholeLithologies.Length; i++)
                {
                    // 岩性名称
                    int iLithologyId = boreholeLithologies[i].Lithology.LithologyId;

                    Lithology lithology = Lithology.Find(iLithologyId);

                    gvCoalSeamsTexture[0, i].Value = lithology.LithologyName;
                    // 底板标高
                    gvCoalSeamsTexture[1, i].Value = boreholeLithologies[i].FloorElevation;
                    // 厚度
                    gvCoalSeamsTexture[2, i].Value = boreholeLithologies[i].Thickness;
                    // 煤层名称
                    gvCoalSeamsTexture[3, i].Value = boreholeLithologies[i].CoalSeamsName;

                    // 坐标X
                    gvCoalSeamsTexture[4, i].Value = boreholeLithologies[i].CoordinateX.ToString(CultureInfo.InvariantCulture);

                    // 坐标Y
                    gvCoalSeamsTexture[5, i].Value = boreholeLithologies[i].CoordinateY.ToString(CultureInfo.InvariantCulture);

                    // 坐标Z
                    gvCoalSeamsTexture[6, i].Value = boreholeLithologies[i].CoordinateX.ToString(CultureInfo.InvariantCulture);

                }
            }
        }

        // **************************************************************************
        /// <summary>
        ///     岩性选择事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void gvCoalSeamsTexture_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == 0)
            {
                // 岩性
                var cell0 =
                    gvCoalSeamsTexture.Rows[gvCoalSeamsTexture.CurrentRow.Index].Cells[0] as
                        DataGridViewComboBoxCell;
                // 煤层名称
                var cell3 =
                    gvCoalSeamsTexture.Rows[gvCoalSeamsTexture.CurrentRow.Index].Cells[3] as
                        DataGridViewTextBoxCell;

                Lithology lithology = Lithology.FindOneByCoal();

                // 当岩性名称选择为“煤”时，煤层名称可编辑，否则煤层名称设置为不可编辑，并清空
                if (Convert.ToString(cell0.Value) ==
                  lithology.LithologyName)
                {
                    cell3.ReadOnly = false;
                }
                else
                {
                    cell3.Value = "";
                    cell3.ReadOnly = true;
                }
            }
        }

        /// <summary>
        ///     修改岩性时，当前记录为煤时，煤层名称设为可编辑，否则设为不可编辑
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void gvCoalSeamsTexture_RowValidated(object sender, DataGridViewCellEventArgs e)
        {
            for (int i = 0; i < gvCoalSeamsTexture.RowCount - 1; i++)
            {
                // 岩性
                var cell0 = gvCoalSeamsTexture.Rows[i].Cells[0] as DataGridViewComboBoxCell;
                // 煤层名称
                var cell3 = gvCoalSeamsTexture.Rows[i].Cells[3] as DataGridViewTextBoxCell;


                Lithology lithology = Lithology.FindOneByCoal();
                // 当岩性名称选择为“煤”时，煤层名称可编辑，否则煤层名称设置为不可编辑，并清空
                if (Convert.ToString(cell0.Value) ==
                    lithology.LithologyName)
                {
                    cell3.ReadOnly = false;
                }
                else
                {
                    cell3.Value = "";
                    cell3.ReadOnly = true;
                }
            }
        }

        /// <summary>
        ///     行删除
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void gvCoalSeamsTexture_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            // 判断列索引是不是删除按钮
            if (e.ColumnIndex == 7)
            {
                //// 最后一行为空行时，跳出循环
                // 最后一行删除按钮设为不可
                if (gvCoalSeamsTexture.RowCount - 1 != gvCoalSeamsTexture.CurrentRow.Index)
                {
                    if (Alert.confirm(Const.DEL_CONFIRM_MSG))
                    {
                        gvCoalSeamsTexture.Rows.Remove(gvCoalSeamsTexture.CurrentRow);
                    }
                }
            }
        }

        /// <summary>
        ///     右键菜单
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void gvCoalSeamsTexture_CellMouseDown(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                if (e.RowIndex >= 0)
                {
                    //若行已是选中状态就不再进行设置
                    if (gvCoalSeamsTexture.Rows[e.RowIndex].Selected == false)
                    {
                        gvCoalSeamsTexture.ClearSelection();
                        gvCoalSeamsTexture.Rows[e.RowIndex].Selected = true;
                        gvCoalSeamsTexture.CurrentCell = gvCoalSeamsTexture.Rows[e.RowIndex].Cells[0];
                    }
                }
            }
        }

        /// <summary>
        ///     上移
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void 上移ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int iNowIndex = gvCoalSeamsTexture.CurrentRow.Index;

            if (iNowIndex == 0)
            {
                Alert.alert("无法上移");
                return;
            }

            var objArrRowData = new object[7];

            int index = -1;
            int n = -1;
            objArrRowData[++n] = gvCoalSeamsTexture.Rows[iNowIndex].Cells[++index].Value;
            objArrRowData[++n] = gvCoalSeamsTexture.Rows[iNowIndex].Cells[++index].Value;
            objArrRowData[++n] = gvCoalSeamsTexture.Rows[iNowIndex].Cells[++index].Value;
            objArrRowData[++n] = gvCoalSeamsTexture.Rows[iNowIndex].Cells[++index].Value;
            objArrRowData[++n] = gvCoalSeamsTexture.Rows[iNowIndex].Cells[++index].Value;
            objArrRowData[++n] = gvCoalSeamsTexture.Rows[iNowIndex].Cells[++index].Value;
            objArrRowData[++n] = gvCoalSeamsTexture.Rows[iNowIndex].Cells[++index].Value;

            index = -1;
            gvCoalSeamsTexture.Rows[iNowIndex].Cells[++index].Value =
                gvCoalSeamsTexture.Rows[iNowIndex - 1].Cells[index].Value;
            gvCoalSeamsTexture.Rows[iNowIndex].Cells[++index].Value =
                gvCoalSeamsTexture.Rows[iNowIndex - 1].Cells[index].Value;
            gvCoalSeamsTexture.Rows[iNowIndex].Cells[++index].Value =
                gvCoalSeamsTexture.Rows[iNowIndex - 1].Cells[index].Value;
            gvCoalSeamsTexture.Rows[iNowIndex].Cells[++index].Value =
                gvCoalSeamsTexture.Rows[iNowIndex - 1].Cells[index].Value;
            gvCoalSeamsTexture.Rows[iNowIndex].Cells[++index].Value =
                gvCoalSeamsTexture.Rows[iNowIndex - 1].Cells[index].Value;
            gvCoalSeamsTexture.Rows[iNowIndex].Cells[++index].Value =
                gvCoalSeamsTexture.Rows[iNowIndex - 1].Cells[index].Value;
            gvCoalSeamsTexture.Rows[iNowIndex].Cells[++index].Value =
                gvCoalSeamsTexture.Rows[iNowIndex - 1].Cells[index].Value;

            index = -1;
            n = -1;
            gvCoalSeamsTexture.Rows[iNowIndex - 1].Cells[++index].Value = objArrRowData[++n];
            gvCoalSeamsTexture.Rows[iNowIndex - 1].Cells[++index].Value = objArrRowData[++n];
            gvCoalSeamsTexture.Rows[iNowIndex - 1].Cells[++index].Value = objArrRowData[++n];
            gvCoalSeamsTexture.Rows[iNowIndex - 1].Cells[++index].Value = objArrRowData[++n];
            gvCoalSeamsTexture.Rows[iNowIndex - 1].Cells[++index].Value = objArrRowData[++n];
            gvCoalSeamsTexture.Rows[iNowIndex - 1].Cells[++index].Value = objArrRowData[++n];
            gvCoalSeamsTexture.Rows[iNowIndex - 1].Cells[++index].Value = objArrRowData[++n];

            gvCoalSeamsTexture.CurrentCell = gvCoalSeamsTexture.Rows[iNowIndex - 1].Cells[0]; //设定当前行
            gvCoalSeamsTexture.Rows[iNowIndex - 1].Selected = true;
        }

        /// <summary>
        ///     下移
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void 下移ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int iNowIndex = gvCoalSeamsTexture.CurrentRow.Index;

            if (iNowIndex == gvCoalSeamsTexture.Rows.Count - 2 ||
                iNowIndex == gvCoalSeamsTexture.Rows.Count - 1)
            {
                Alert.alert("无法下移");
                return;
            }

            var objArrRowData = new object[7];

            int index = -1;
            int n = -1;
            objArrRowData[++n] = gvCoalSeamsTexture.Rows[iNowIndex].Cells[++index].Value;
            objArrRowData[++n] = gvCoalSeamsTexture.Rows[iNowIndex].Cells[++index].Value;
            objArrRowData[++n] = gvCoalSeamsTexture.Rows[iNowIndex].Cells[++index].Value;
            objArrRowData[++n] = gvCoalSeamsTexture.Rows[iNowIndex].Cells[++index].Value;
            objArrRowData[++n] = gvCoalSeamsTexture.Rows[iNowIndex].Cells[++index].Value;
            objArrRowData[++n] = gvCoalSeamsTexture.Rows[iNowIndex].Cells[++index].Value;
            objArrRowData[++n] = gvCoalSeamsTexture.Rows[iNowIndex].Cells[++index].Value;

            index = -1;
            gvCoalSeamsTexture.Rows[iNowIndex].Cells[++index].Value =
                gvCoalSeamsTexture.Rows[iNowIndex + 1].Cells[index].Value;
            gvCoalSeamsTexture.Rows[iNowIndex].Cells[++index].Value =
                gvCoalSeamsTexture.Rows[iNowIndex + 1].Cells[index].Value;
            gvCoalSeamsTexture.Rows[iNowIndex].Cells[++index].Value =
                gvCoalSeamsTexture.Rows[iNowIndex + 1].Cells[index].Value;
            gvCoalSeamsTexture.Rows[iNowIndex].Cells[++index].Value =
                gvCoalSeamsTexture.Rows[iNowIndex + 1].Cells[index].Value;
            gvCoalSeamsTexture.Rows[iNowIndex].Cells[++index].Value =
                gvCoalSeamsTexture.Rows[iNowIndex + 1].Cells[index].Value;
            gvCoalSeamsTexture.Rows[iNowIndex].Cells[++index].Value =
                gvCoalSeamsTexture.Rows[iNowIndex + 1].Cells[index].Value;
            gvCoalSeamsTexture.Rows[iNowIndex].Cells[++index].Value =
                gvCoalSeamsTexture.Rows[iNowIndex + 1].Cells[index].Value;

            index = -1;
            n = -1;
            gvCoalSeamsTexture.Rows[iNowIndex + 1].Cells[++index].Value = objArrRowData[++n];
            gvCoalSeamsTexture.Rows[iNowIndex + 1].Cells[++index].Value = objArrRowData[++n];
            gvCoalSeamsTexture.Rows[iNowIndex + 1].Cells[++index].Value = objArrRowData[++n];
            gvCoalSeamsTexture.Rows[iNowIndex + 1].Cells[++index].Value = objArrRowData[++n];
            gvCoalSeamsTexture.Rows[iNowIndex + 1].Cells[++index].Value = objArrRowData[++n];
            gvCoalSeamsTexture.Rows[iNowIndex + 1].Cells[++index].Value = objArrRowData[++n];
            gvCoalSeamsTexture.Rows[iNowIndex + 1].Cells[++index].Value = objArrRowData[++n];

            gvCoalSeamsTexture.CurrentCell = gvCoalSeamsTexture.Rows[iNowIndex + 1].Cells[0]; //设定当前行
            gvCoalSeamsTexture.Rows[iNowIndex + 1].Selected = true;
        }

        /// <summary>
        ///     复制
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void 复制ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int iNowIndex = gvCoalSeamsTexture.CurrentRow.Index;

            var objArrRowData = new object[7];

            int index = -1;
            int n = -1;
            objArrRowData[++n] = gvCoalSeamsTexture.Rows[iNowIndex].Cells[++index].Value;
            objArrRowData[++n] = gvCoalSeamsTexture.Rows[iNowIndex].Cells[++index].Value;
            objArrRowData[++n] = gvCoalSeamsTexture.Rows[iNowIndex].Cells[++index].Value;
            objArrRowData[++n] = gvCoalSeamsTexture.Rows[iNowIndex].Cells[++index].Value;
            objArrRowData[++n] = gvCoalSeamsTexture.Rows[iNowIndex].Cells[++index].Value;
            objArrRowData[++n] = gvCoalSeamsTexture.Rows[iNowIndex].Cells[++index].Value;
            objArrRowData[++n] = gvCoalSeamsTexture.Rows[iNowIndex].Cells[++index].Value;

            _objArrRowData = objArrRowData;

            //MessageBox.Show("复制成功！");

            contextMenuStrip1.Items["粘贴ToolStripMenuItem"].Visible = true;
        }

        /// <summary>
        ///     粘贴
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void 粘贴ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int iNowIndex = gvCoalSeamsTexture.CurrentRow.Index;

            if (iNowIndex == gvCoalSeamsTexture.Rows.Count - 1)
            {
                gvCoalSeamsTexture.Rows.Add();
            }

            int index = -1;
            int n = -1;
            gvCoalSeamsTexture.Rows[iNowIndex].Cells[++index].Value = _objArrRowData[++n];
            gvCoalSeamsTexture.Rows[iNowIndex].Cells[++index].Value = _objArrRowData[++n];
            gvCoalSeamsTexture.Rows[iNowIndex].Cells[++index].Value = _objArrRowData[++n];
            gvCoalSeamsTexture.Rows[iNowIndex].Cells[++index].Value = _objArrRowData[++n];
            gvCoalSeamsTexture.Rows[iNowIndex].Cells[++index].Value = _objArrRowData[++n];
            gvCoalSeamsTexture.Rows[iNowIndex].Cells[++index].Value = _objArrRowData[++n];
            gvCoalSeamsTexture.Rows[iNowIndex].Cells[++index].Value = _objArrRowData[++n];

            contextMenuStrip1.Items["粘贴ToolStripMenuItem"].Visible = false;
        }

        /// <summary>
        ///     剪切
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void 剪切ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int iNowIndex = gvCoalSeamsTexture.CurrentRow.Index;

            var objArrRowData = new object[7];

            int index = -1;
            int n = -1;
            objArrRowData[++n] = gvCoalSeamsTexture.Rows[iNowIndex].Cells[++index].Value;
            objArrRowData[++n] = gvCoalSeamsTexture.Rows[iNowIndex].Cells[++index].Value;
            objArrRowData[++n] = gvCoalSeamsTexture.Rows[iNowIndex].Cells[++index].Value;
            objArrRowData[++n] = gvCoalSeamsTexture.Rows[iNowIndex].Cells[++index].Value;
            objArrRowData[++n] = gvCoalSeamsTexture.Rows[iNowIndex].Cells[++index].Value;
            objArrRowData[++n] = gvCoalSeamsTexture.Rows[iNowIndex].Cells[++index].Value;
            objArrRowData[++n] = gvCoalSeamsTexture.Rows[iNowIndex].Cells[++index].Value;

            _objArrRowData = objArrRowData;

            gvCoalSeamsTexture.Rows.Remove(gvCoalSeamsTexture.CurrentRow);

            //MessageBox.Show("剪切成功！");

            contextMenuStrip1.Items["粘贴ToolStripMenuItem"].Visible = true;
        }

        /// <summary>
        ///     插入
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void 插入ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int iNowIndex = gvCoalSeamsTexture.CurrentRow.Index;

            var newRow = new DataGridViewRow(); //新建行
            gvCoalSeamsTexture.Rows.Insert(iNowIndex, newRow); //当前行的上面插入新行
        }

        /// <summary>
        ///     添加数据顺序编号
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void gvCoalSeamsTexture_RowPostPaint(object sender, DataGridViewRowPostPaintEventArgs e)
        {
            var rectangle = new Rectangle(e.RowBounds.Location.X,
                e.RowBounds.Location.Y, gvCoalSeamsTexture.RowHeadersWidth - 4, e.RowBounds.Height);

            TextRenderer.DrawText(e.Graphics, (e.RowIndex + 1).ToString(),
                gvCoalSeamsTexture.RowHeadersDefaultCellStyle.Font, rectangle,
                gvCoalSeamsTexture.RowHeadersDefaultCellStyle.ForeColor,
                TextFormatFlags.VerticalCenter | TextFormatFlags.Right);
        }

        private void btnQD_Click(object sender, EventArgs e)
        {
            DataEditCommon.PickUpPoint(txtCoordinateX, txtCoordinateY);
        }

        private void btnReadMultTxt_Click(object sender, EventArgs e)
        {
            var ofd = new OpenFileDialog();
            ofd.InitialDirectory = @"C:\";
            ofd.RestoreDirectory = true;
            ofd.Filter = "文本文件(*.txt)|*.txt|所有文件(*.*)|*.*";
            ofd.Multiselect = true;
            //ofd.ShowDialog();
            if (ofd.ShowDialog() == DialogResult.OK)
            {
                for (int i = 0; i < ofd.FileNames.Length; i++)
                {
                    string aa = ofd.FileNames[i];
                    Encoding encoder = TxtFileEncoding.GetEncoding(aa, Encoding.GetEncoding("GB2312"));
                    var sr = new StreamReader(@aa, encoder);
                    string duqu;
                    while ((duqu = sr.ReadLine()) != null)
                    {
                        String[] str = duqu.Split('|');
                        //txtBoreholeNumber.Text = str[0];
                        //txtCoordinateX.Text = str[1].Split(',')[0];
                        //txtCoordinateY.Text = str[1].Split(',')[1];
                        //txtCoordinateZ.Text = "0";
                        //gvCoalSeamsTexture.Rows.Add("煤层", str[4], str[2], "3#", str[1].Split(',')[0],
                        //    str[1].Split(',')[1],
                        //    "0");
                        //txtGroundElevation.Text = str[3];

                        var breholeEntity = new Borehole();

                        // 孔号
                        breholeEntity.BoreholeNumber = str[0];
                        // 地面标高

                        breholeEntity.GroundElevation = Convert.ToDouble(str[3]);
                        // X坐标
                        breholeEntity.CoordinateX = Convert.ToDouble(str[1].Split(',')[0]);

                        breholeEntity.CoordinateY = Convert.ToDouble(str[1].Split(',')[1]);

                        breholeEntity.CoordinateZ = 0;

                        breholeEntity.CoalSeamsTexture = String.Empty;

                        // 创建钻孔岩性实体
                        var boreholeLithologyEntity = new BoreholeLithology();
                        // 钻孔编号
                        boreholeLithologyEntity.Borehole.BoreholeId = breholeEntity.BoreholeId;
                        // 岩性编号
                        boreholeLithologyEntity.Lithology = Lithology.FindOneByCoal();
                        // 底板标高

                        boreholeLithologyEntity.FloorElevation = Convert.ToDouble(str[4]);
                        // 厚度

                        boreholeLithologyEntity.Thickness = Convert.ToDouble(str[2]);

                        // 煤层名称

                        boreholeLithologyEntity.CoalSeamsName = "3#";


                        boreholeLithologyEntity.CoordinateX = Convert.ToDouble(str[1].Split(',')[0]);

                        boreholeLithologyEntity.CoordinateY = Convert.ToDouble(str[1].Split(',')[1]);

                        boreholeLithologyEntity.CoordinateZ = 0;

                        // 执行结果
                        bool bResult = false;
                        // 只有当添加新钻孔信息的时候才去判断孔号是否重复

                        breholeEntity.BindingId = IDGenerator.NewBindingID();

                        // 钻孔信息登录
                        breholeEntity.Save();
                        // 钻孔岩性信息登录

                        boreholeLithologyEntity.Save();

                        // 添加/修改成功的场合

                        if (breholeEntity.BindingId == null || breholeEntity.BindingId == "")
                            return; //若BID值为空，则不绘制钻孔

                        DrawZuanKong(breholeEntity, boreholeLithologyEntity);

                    }
                }
            }
        }

        private void btnReadTxt_Click(object sender, EventArgs e)
        {
            var ofd = new OpenFileDialog();
            ofd.InitialDirectory = @"C:\";
            ofd.RestoreDirectory = true;
            ofd.Filter = "文本文件(*.txt)|*.txt|所有文件(*.*)|*.*";
            ofd.Multiselect = true;
            //ofd.ShowDialog();
            if (ofd.ShowDialog() == DialogResult.OK)
            {
                for (int i = 0; i < ofd.FileNames.Length; i++)
                {
                    string aa = ofd.FileNames[i];
                    Encoding encoder = TxtFileEncoding.GetEncoding(aa, Encoding.GetEncoding("GB2312"));
                    var sr = new StreamReader(@aa, encoder);
                    string duqu;
                    while ((duqu = sr.ReadLine()) != null)
                    {
                        String[] str = duqu.Split('|');
                        txtBoreholeNumber.Text = str[0];
                        txtCoordinateX.Text = str[1].Split(',')[0];
                        txtCoordinateY.Text = str[1].Split(',')[1];
                        txtCoordinateZ.Text = "0";
                        gvCoalSeamsTexture.Rows.Add("煤层", str[4], str[2], "3#", str[1].Split(',')[0],
                            str[1].Split(',')[1],
                            "0");
                        txtGroundElevation.Text = str[3];
                    }
                }
            }
        }

        #region 绘制钻孔

        /// <summary>
        ///     见煤钻孔
        /// </summary>
        /// <param name="breholeEntity">钻孔实体</param>
        /// <param name="boreholeLithologyEntity">钻孔岩性实体</param>
        private void DrawZuanKong(Borehole breholeEntity, BoreholeLithology boreholeLithologyEntity)
        {
            ////1.获得当前编辑图层
            //DrawSpecialCommon drawspecial = new DrawSpecialCommon();
            //string sLayerAliasName = LibCommon.LibLayerNames.DEFALUT_BOREHOLE;//“默认_钻孔”图层
            //IFeatureLayer featureLayer = drawspecial.GetFeatureLayerByName(sLayerAliasName);
            //if (featureLayer == null)
            //{
            //    MessageBox.Show("未找到" + sLayerAliasName + "图层,无法绘制钻孔图元。");
            //    return;
            //}

            ////2.绘制图元
            //IPoint pt = new PointClass();
            //pt.X = breholeEntity.CoordinateX;
            //pt.Y = breholeEntity.CoordinateY;
            //pt.Z = breholeEntity.CoordinateZ;

            ////标注内容
            //string strH = breholeEntity.GroundElevation.ToString();//地面标高
            //string strName = breholeEntity.BoreholeNumber.ToString();//孔号（名称）
            //string strDBBG = boreholeLithologyEntity.FloorElevation.ToString();//底板标高
            //string strMCHD = boreholeLithologyEntity.Thickness.ToString();//煤层厚度

            //GIS.SpecialGraphic.DrawZK1 drawZK1 = new GIS.SpecialGraphic.DrawZK1(strName, strH, strDBBG, strMCHD);
            //DataEditCommon.g_CurWorkspaceEdit.StartEditing(false);
            //DataEditCommon.g_CurWorkspaceEdit.StartEditOperation();
            //IFeature feature = featureLayer.FeatureClass.CreateFeature();

            //IGeometry geometry = pt;
            //DataEditCommon.ZMValue(feature, geometry);   //几何图形Z值处理
            ////drawspecial.ZMValue(feature, geometry);//几何图形Z值处理
            //feature.Shape = pt;//要素形状
            ////要素ID字段赋值（对应属性表中BindingID）
            //int iFieldID = feature.Fields.FindField("BID");
            //feature.Value[iFieldID] =breholeEntity.BindingId.ToString();
            //feature.Store();//存储要素
            //DataEditCommon.g_CurWorkspaceEdit.StopEditOperation();
            //DataEditCommon.g_CurWorkspaceEdit.StopEditing(true);
            //string strValue = feature.get_Value(feature.Fields.FindField("OBJECTID")).ToString();
            //DataEditCommon.SpecialPointRenderer(featureLayer, "OBJECTID", strValue, drawZK1.m_Bitmap);

            /////3.显示钻孔图层
            //if (featureLayer.Visible == false)
            //    featureLayer.Visible = true;

            //IEnvelope envelop = feature.Shape.Envelope;
            //DataEditCommon.g_pMyMapCtrl.ActiveView.Extent = envelop;
            //DataEditCommon.g_pMyMapCtrl.ActiveView.PartialRefresh(esriViewDrawPhase.esriViewBackground, null, null);
            //DataEditCommon.g_pMyMapCtrl.ActiveView.Refresh();

            IPoint pt = new PointClass();
            pt.X = breholeEntity.CoordinateX;
            pt.Y = breholeEntity.CoordinateY;
            pt.Z = breholeEntity.CoordinateZ;
            if (pt.Z == double.NaN)
                pt.Z = 0;
            ILayer pLayer = DataEditCommon.GetLayerByName(DataEditCommon.g_pMap, LayerNames.DEFALUT_BOREHOLE);
            if (pLayer == null)
            {
                MessageBox.Show("未找到钻孔图层,无法绘制钻孔图元。");
                return;
            }
            var pFeatureLayer = (IFeatureLayer)pLayer;
            IGeometry geometry = pt;
            var list = new List<ziduan>();
            list.Add(new ziduan("bid", breholeEntity.BindingId));
            list.Add(new ziduan("BOREHOLE_NUMBER", breholeEntity.BoreholeNumber));
            list.Add(new ziduan("addtime", DateTime.Now.ToString()));
            list.Add(new ziduan("GROUND_ELEVATION", breholeEntity.GroundElevation.ToString()));
            list.Add(new ziduan("FLOOR_ELEVATION", boreholeLithologyEntity.FloorElevation.ToString()));
            list.Add(new ziduan("THICKNESS", boreholeLithologyEntity.Thickness.ToString()));
            list.Add(new ziduan("type", "2"));

            IFeature pfeature = DataEditCommon.CreateNewFeature(pFeatureLayer, geometry, list);
            if (pfeature != null)
            {
                MyMapHelp.Jump(pt);
                DataEditCommon.g_pMyMapCtrl.ActiveView.PartialRefresh(
                    esriViewDrawPhase.esriViewGeography | esriViewDrawPhase.esriViewForeground, null, null);
            }
        }

        /// <summary>
        ///     未见煤钻孔
        /// </summary>
        /// <param name="breholeEntity">钻孔实体</param>
        private void DrawZuanKong(Borehole breholeEntity)
        {
            ////1.获得当前编辑图层
            //DrawSpecialCommon drawspecial = new DrawSpecialCommon();
            //string sLayerAliasName = LibCommon.LibLayerNames.DEFALUT_BOREHOLE;//“默认_钻孔”图层
            //IFeatureLayer featureLayer = drawspecial.GetFeatureLayerByName(sLayerAliasName);
            //if (featureLayer == null)
            //{
            //    MessageBox.Show("未找到" + sLayerAliasName + "图层,无法绘制钻孔图元。");
            //    return;
            //}

            ////2.绘制图元
            //IPoint pt = new PointClass();
            //pt.X = breholeEntity.CoordinateX;
            //pt.Y = breholeEntity.CoordinateY;
            //pt.Z = breholeEntity.CoordinateZ;
            //if (pt.Z == double.NaN)
            //    pt.Z = 0;
            //GIS.SpecialGraphic.DrawZK2 drawZK2 = null;
            ////标注内容
            //string strH =breholeEntity.GroundElevation.ToString();//地面标高
            //string strName = breholeEntity.BoreholeNumber.ToString();//孔号（名称）

            //IFeature feature = featureLayer.FeatureClass.CreateFeature();
            //IGeometry geometry = pt;
            //DataEditCommon.ZMValue(feature, geometry);   //几何图形Z值处理
            ////drawspecial.ZMValue(feature, geometry);    //几何图形Z值处理
            //feature.Shape = pt;//要素形状
            ////要素ID字段赋值（对应属性表中BindingID）
            //int iFieldID = feature.Fields.FindField("ID");
            //feature.Value[iFieldID] = breholeEntity.BindingId.ToString();
            //feature.Store();//存储要素

            //string strValue = feature.get_Value(feature.Fields.FindField("OBJECTID")).ToString();
            //DataEditCommon.SpecialPointRenderer(featureLayer, "OBJECTID", strValue, drawZK2.m_Bitmap);

            /////3.显示钻孔图层
            //if (featureLayer.Visible == false)
            //    featureLayer.Visible = true;

            //IEnvelope envelop = feature.Shape.Envelope;
            //DataEditCommon.g_pMyMapCtrl.ActiveView.Extent = envelop;
            //DataEditCommon.g_pMyMapCtrl.ActiveView.PartialRefresh(esriViewDrawPhase.esriViewBackground, null, null);
            //DataEditCommon.g_pMyMapCtrl.ActiveView.Refresh();

            IPoint pt = new PointClass();
            pt.X = breholeEntity.CoordinateX;
            pt.Y = breholeEntity.CoordinateY;
            pt.Z = breholeEntity.CoordinateZ;
            if (pt.Z == double.NaN)
                pt.Z = 0;
            ILayer pLayer = DataEditCommon.GetLayerByName(DataEditCommon.g_pMap, LayerNames.DEFALUT_BOREHOLE);
            if (pLayer == null)
            {
                MessageBox.Show("未找到钻孔图层,无法绘制钻孔图元。");
                return;
            }
            var pFeatureLayer = (IFeatureLayer)pLayer;
            IGeometry geometry = pt;
            var list = new List<ziduan>();
            list.Add(new ziduan(GIS_Const.FIELD_BID, breholeEntity.BindingId));
            list.Add(new ziduan(GIS_Const.FIELD_BOREHOLE_NUMBER, breholeEntity.BoreholeNumber));
            list.Add(new ziduan(GIS_Const.FIELD_ADD_TIME, DateTime.Now.ToString()));
            list.Add(new ziduan(GIS_Const.FIELD_GROUND_ELEVATION, breholeEntity.GroundElevation.ToString()));
            list.Add(new ziduan(GIS_Const.FIELD_GROUND_FLOOR_ELEVATION, ""));
            list.Add(new ziduan(GIS_Const.FIELD_THICKNESS, ""));
            list.Add(new ziduan(GIS_Const.FIELD_TYPE, "1"));

            IFeature pfeature = DataEditCommon.CreateNewFeature(pFeatureLayer, geometry, list);
            if (pfeature != null)
            {
                MyMapHelp.Jump(pt);
                DataEditCommon.g_pMyMapCtrl.ActiveView.PartialRefresh(
                    esriViewDrawPhase.esriViewGeography | esriViewDrawPhase.esriViewForeground, null, null);
            }
        }

        #endregion
    }

    // **************************************************************************
}