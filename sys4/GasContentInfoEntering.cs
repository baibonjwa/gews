using System;
using System.Collections.Generic;
using System.Globalization;
using System.Windows.Forms;
using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.Geodatabase;
using ESRI.ArcGIS.Geometry;
using GIS;
using GIS.Common;
using LibBusiness;
using LibCommon;
using LibCommonForm;
using LibEntity;

namespace sys4
{
    public partial class GasContentInfoEntering : Form
    {
        private GasContent GasContent { get; set; }

        /// <summary>
        /// 构造方法
        /// </summary>
        public GasContentInfoEntering()
        {
            InitializeComponent();
            FormDefaultPropertiesSetter.SetEnteringFormDefaultProperties(this, Const_OP.INSERT_GASCONTENT_INFO);
        }

        /// <summary>
        /// 带参数的构造方法
        /// </summary>
        public GasContentInfoEntering(GasContent gasContent)
        {
            InitializeComponent();
            GasContent = gasContent;
            FormDefaultPropertiesSetter.SetEnteringFormDefaultProperties(this, Const_OP.UPDATE_GASCONTENT_INFO);
        }

        /// <summary>
        /// 20140311 lyf 加载窗体时传入拾取点的坐标值
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void GasContentInfoEntering_Load(object sender, EventArgs e)
        {
            dtpMeasureDateTime.Format = DateTimePickerFormat.Custom;
            dtpMeasureDateTime.CustomFormat = Const.DATE_FORMART_YYYY_MM_DD;
            DataBindUtil.LoadCoalSeamsName(cboCoalSeams);
            if (GasContent != null)
            {
                txtCoordinateX.Text = GasContent.CoordinateX.ToString(CultureInfo.InvariantCulture);
                txtCoordinateY.Text = GasContent.CoordinateY.ToString(CultureInfo.InvariantCulture);
                txtCoordinateZ.Text = GasContent.CoordinateZ.ToString(CultureInfo.InvariantCulture);
                txtDepth.Text = GasContent.Depth.ToString(CultureInfo.InvariantCulture);
                txtGasContentValue.Text = GasContent.GasContentValue.ToString(CultureInfo.InvariantCulture);
                dtpMeasureDateTime.Value = GasContent.MeasureDateTime;
                cboCoalSeams.SelectedValue = GasContent.CoalSeams;
                selectTunnelSimple1.SetTunnel(GasContent.Tunnel);
            }
        }


        /// <summary>
        /// 提交
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnSubmit_Click(object sender, EventArgs e)
        {
            // 验证
            if (!Check())
            {
                DialogResult = DialogResult.None;
                return;
            }
            DialogResult = DialogResult.OK;

            // 创建一个瓦斯含量点实体
            if (GasContent == null)
            {
                var gasContent = new GasContent
                {
                    CoordinateX = Convert.ToDouble(txtCoordinateX.Text),
                    CoordinateY = Convert.ToDouble(txtCoordinateY.Text),
                    CoordinateZ = 0.0,
                    Depth = Convert.ToDouble(txtDepth.Text),
                    GasContentValue = Convert.ToDouble(txtGasContentValue.Text),
                    MeasureDateTime = dtpMeasureDateTime.Value,
                    Tunnel = selectTunnelSimple1.SelectedTunnel,
                    CoalSeams = (CoalSeams)cboCoalSeams.SelectedItem,
                    BindingId = IDGenerator.NewBindingID()
                };
                // 坐标X
                gasContent.Save();
                DrawGasGushQuantityPt(gasContent);
            }
            else
            {
                GasContent.CoordinateX = Convert.ToDouble(txtCoordinateX.Text);
                GasContent.CoordinateY = Convert.ToDouble(txtCoordinateY.Text);
                GasContent.CoordinateZ = 0.0;
                GasContent.Depth = Convert.ToDouble(txtDepth.Text);
                GasContent.GasContentValue = Convert.ToDouble(txtGasContentValue.Text);
                GasContent.MeasureDateTime = dtpMeasureDateTime.Value;
                GasContent.Tunnel = selectTunnelSimple1.SelectedTunnel;
                GasContent.CoalSeams = (CoalSeams)cboCoalSeams.SelectedValue;
                GasContent.Save();
                DelGasGushQuantityPt(GasContent.BindingId, GasContent.CoalSeams.CoalSeamsName);
                DrawGasGushQuantityPt(GasContent);
            }
        }

        /// <summary>
        /// 取消
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnCancel_Click(object sender, EventArgs e)
        {
            Close();
        }

        /// <summary>
        /// 验证画面入力数据
        /// </summary>
        /// <returns>验证结果：true 通过验证, false未通过验证</returns>
        private bool Check()
        {
            // 判断是否选择所属巷道
            //if (selectTunnelSimple1.SelectedTunnel == null)
            //{
            //    Alert.alert(Const_OP.TUNNEL_NAME_MUST_INPUT);
            //    return false;
            //}

            // 判断所在煤层是否选择
            if (cboCoalSeams.SelectedValue == null)
            {
                Alert.alert(Const.COALSEAMS_MUST_SELECT);
                return false;
            }

            // 判断坐标X是否录入
            if (!LibCommon.Check.isEmpty(txtCoordinateX, Const_OP.COORDINATE_X))
            {
                return false;
            }

            // 判断坐标X是否为数字
            if (!LibCommon.Check.IsNumeric(txtCoordinateX, Const_OP.COORDINATE_X))
            {
                return false;
            }

            // 判断坐标Y是否录入
            if (!LibCommon.Check.isEmpty(txtCoordinateY, Const_OP.COORDINATE_Y))
            {
                return false;
            }

            // 判断坐标Y是否为数字
            if (!LibCommon.Check.IsNumeric(txtCoordinateY, Const_OP.COORDINATE_Y))
            {
                return false;
            }

            // 判断测点标高是否录入
            if (!LibCommon.Check.isEmpty(txtCoordinateZ, Const_OP.GAS_PRESSURE_COORDINATE_Z))
            {
                return false;
            }

            // 判断测点标高是否为数字
            if (!LibCommon.Check.IsNumeric(txtCoordinateZ, Const_OP.GAS_PRESSURE_COORDINATE_Z))
            {
                return false;
            }

            // 判断埋深是否录入
            if (!LibCommon.Check.isEmpty(txtDepth, Const_OP.DEPTH))
            {
                return false;
            }

            // 判断埋深是否为数字
            if (!LibCommon.Check.IsNumeric(txtDepth, Const_OP.DEPTH))
            {
                return false;
            }

            // 判断瓦斯含量值是否录入
            if (!LibCommon.Check.isEmpty(txtGasContentValue, Const_OP.GAS_CONTENT_VALUE))
            {
                return false;
            }

            // 判断瓦斯含量值是否为数字
            if (!LibCommon.Check.IsNumeric(txtGasContentValue, Const_OP.GAS_CONTENT_VALUE))
            {
                return false;
            }

            // 验证通过
            return true;
        }


        #region 绘制瓦斯含量点图元

        public const string GasContentPt = "瓦斯含量点";
        public IPoint GasContentPoint { get; set; }

        /// <summary>
        /// 20140311 lyf 绘制瓦斯含量点图元
        /// </summary>
        //private void DrawGasContentPt(string coalseamNO)
        //{
        //    DrawSpecialCommon drawspecial = new DrawSpecialCommon();
        //    ////获得当前编辑图层
        //    //IFeatureLayer featureLayer = (IFeatureLayer)DataEditCommon.g_pLayer;

        //    //1.获得对应瓦斯含量点图层
        //    string sLayerAliasName = coalseamNO + "号煤层-" + GasContentPt;
        //    IFeatureLayer featureLayer = drawspecial.GetFeatureLayerByName(sLayerAliasName);

        //    if (featureLayer == null)
        //    {
        //        //如果对应图层不存在，要自动创建图层                
        //        IWorkspace workspace = DataEditCommon.g_pCurrentWorkSpace;
        //        string layerName = "GAS_CONTENT_PT" + "_NO" + coalseamNO;
        //        //若MapControl不存在该图层，但数据库中存在该图层，则先删除之，再重新生成
        //        IDataset dataset = drawspecial.GetDatasetByName(workspace, "GasEarlyWarningGIS.SDE." + layerName);
        //        if (dataset != null) dataset.Delete();
        //        //自动创建图层
        //        IMap map = DataEditCommon.g_pMap;
        //        featureLayer = drawspecial.CreateFeatureLayer(map, workspace, layerName, sLayerAliasName);
        //        if (featureLayer == null)
        //        {
        //            MessageBox.Show(@"未成功创建" + sLayerAliasName + @"图层,无法绘制瓦斯含量点图元。");
        //            return;
        //        }
        //    }

        //    double dCoordinateX = Convert.ToDouble(txtCoordinateX.Text);
        //    double dCoordinateY = Convert.ToDouble(txtCoordinateY.Text);
        //    double dCoordinateZ = Convert.ToDouble(txtCoordinateZ.Text);
        //    IPoint pt = new PointClass();
        //    pt.X = dCoordinateX;
        //    pt.Y = dCoordinateY;
        //    pt.Z = dCoordinateZ;
        //    DrawWSHLD pDrawWsyld = new DrawWSHLD("W", txtGasContentValue.Text, txtCoordinateZ.Text, txtDepth.Text);
        //    IFeature feature = featureLayer.FeatureClass.CreateFeature();

        //    IGeometry geometry = pt;
        //    DrawCommon.HandleZMValue(feature, geometry);//几何图形Z值处理
        //    feature.Shape = pt;
        //    feature.Store();

        //    string strValue = feature.Value[feature.Fields.FindField("OBJECTID")].ToString();
        //    DataEditCommon.SpecialPointRenderer(featureLayer, "OBJECTID", strValue, pDrawWsyld.m_Bitmap);

        //    ///3.显示瓦斯含量点图层
        //    if (featureLayer.Visible == false)
        //        featureLayer.Visible = true;

        //    DataEditCommon.g_pMyMapCtrl.ActiveView.Refresh();
        //}

        #endregion 绘制瓦斯含量点图元

        /// <summary>
        /// 煤层信息添加管理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnAddCoalSeams_Click(object sender, EventArgs e)
        {
            CommonManagement commonManagementForm = new CommonManagement(5, 0);

            if (DialogResult.OK == commonManagementForm.ShowDialog())
            {
                // 绑定煤层名称信息
                DataBindUtil.LoadCoalSeamsName(cboCoalSeams);
            }
        }


        /// <summary>
        /// 20140801SDE中添加瓦斯含量点
        /// </summary>
        private void DrawGasGushQuantityPt(GasContent gasGushQuantityEntity)
        {
            double dCoordinateX = Convert.ToDouble(txtCoordinateX.Text);
            double dCoordinateY = Convert.ToDouble(txtCoordinateY.Text);
            double dCoordinateZ = Convert.ToDouble(txtCoordinateZ.Text);
            IPoint pt = new PointClass();
            pt.X = dCoordinateX;
            pt.Y = dCoordinateY;
            pt.Z = dCoordinateZ;
            ILayer pLayer = DataEditCommon.GetLayerByName(DataEditCommon.g_pMap, LayerNames.LAYER_ALIAS_MR_WSHLD);
            if (pLayer == null)
            {
                MessageBox.Show(@"未找到瓦斯含量点图层,无法绘制瓦斯含量点图元。");
                return;
            }
            IFeatureLayer pFeatureLayer = (IFeatureLayer)pLayer;
            IGeometry geometry = pt;
            List<ziduan> list = new List<ziduan>
            {
                new ziduan("bid", gasGushQuantityEntity.BindingId),
                new ziduan("mc", gasGushQuantityEntity.CoalSeams.ToString()),
                new ziduan("addtime", DateTime.Now.ToString(CultureInfo.InvariantCulture))
            };
            string wshl = gasGushQuantityEntity.GasContentValue.ToString(CultureInfo.InvariantCulture); // 瓦斯含量
            string cdbg = gasGushQuantityEntity.CoordinateZ.ToString(CultureInfo.InvariantCulture);
            string ms = gasGushQuantityEntity.Depth.ToString(CultureInfo.InvariantCulture); // 埋深
            if (DataEditCommon.strLen(cdbg) < DataEditCommon.strLen(ms))
            {
                int count = DataEditCommon.strLen(ms) - DataEditCommon.strLen(cdbg);
                for (int i = 0; i < count; i++)
                {
                    cdbg = " " + cdbg;
                }
            }
            else if (DataEditCommon.strLen(cdbg) > DataEditCommon.strLen(ms))
            {
                int count = DataEditCommon.strLen(cdbg) - DataEditCommon.strLen(ms);
                for (int i = 0; i < count; i++)
                {
                    ms += " ";
                }
            }

            list.Add(new ziduan("wshl", wshl));
            list.Add(new ziduan("cdbg", cdbg));
            list.Add(new ziduan("ms", ms));

            IFeature pfeature = DataEditCommon.CreateNewFeature(pFeatureLayer, geometry, list);
            if (pfeature != null)
            {
                MyMapHelp.Jump(pt);
                DataEditCommon.g_pMyMapCtrl.ActiveView.PartialRefresh((esriViewDrawPhase)34, null, null);
            }
        }

        /// <summary>
        /// 删除瓦斯信息
        /// </summary>
        /// <param name="bid">绑定ID</param>
        /// <param name="mc">煤层</param>
        private void DelGasGushQuantityPt(string bid, string mc)
        {
            ILayer pLayer = DataEditCommon.GetLayerByName(DataEditCommon.g_pMap, LayerNames.LAYER_ALIAS_MR_WSHLD);
            IFeatureLayer pFeatureLayer = (IFeatureLayer)pLayer;
            DataEditCommon.DeleteFeatureByWhereClause(pFeatureLayer, "bid='" + bid + "' and mc='" + mc + "'");
        }
    }
}
