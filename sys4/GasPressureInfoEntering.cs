// ******************************************************************
// 概  述：瓦斯压力点数据录入
// 作  者：伍鑫
// 创建日期：2013/12/07
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
using LibCommon;
using LibBusiness;
using LibEntity;
using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.Geometry;
using ESRI.ArcGIS.Geodatabase;
using GIS.Common;
using GIS.SpecialGraphic;
using LibCommonControl;
using LibCommonForm;

namespace _4.OutburstPrevention
{
    public partial class GasPressureInfoEntering : BaseForm
    {
        /** 主键 **/
        private int _iPK;
        /** 业务逻辑类型：添加/修改 **/
        private string _bllType = "add";
        /** 存放矿井，水平，采区，工作面，巷道编号的数组 **/
        private int[] _intArr = new int[5];

        /// <summary>
        /// 构造方法
        /// </summary>
        public GasPressureInfoEntering(SocketHelper mainFrm)
        {
            InitializeComponent();

            this.MainForm = mainFrm;

            // 设置窗体默认属性
            FormDefaultPropertiesSetter.SetEnteringFormDefaultProperties(this, Const_OP.INSERT_GASPRESSURE_INFO);

            //this.selectTunnelUserControl1.init(mainFrm);
            // 调用选择巷道控件时需要调用的方法
            //this.selectTunnelUserControl1.loadMineName();
        }

        /// <summary>
        /// 带参数的构造方法
        /// </summary>
        /// <param name="strPrimaryKey">主键</param>
        public GasPressureInfoEntering(string strPrimaryKey, SocketHelper mainFrm)
        {
            InitializeComponent();

            this.MainForm = mainFrm;

            // 主键
            int iPK = 0;
            if (int.TryParse(strPrimaryKey, out iPK))
            {
                this._iPK = iPK;

                // 设置窗体默认属性
                FormDefaultPropertiesSetter.SetEnteringFormDefaultProperties(this, Const_OP.UPDATE_GASPRESSURE_INFO);

                // 设置业务类型
                this._bllType = "update";



                // 调用选择巷道控件时需要调用的方法
                //this.selectTunnelUserControl1.setCurSelectedID(_intArr);
            }
        }

        /// <summary>
        /// 20140308 lyf 加载窗体时传入拾取点的坐标值
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void GasPressureInfoEntering_Load(object sender, EventArgs e)
        {
            if (this._bllType == "update")
            {
                // 设置日期控件格式
                this.dtpMeasureDateTime.Format = DateTimePickerFormat.Custom;
                this.dtpMeasureDateTime.CustomFormat = Const.DATE_FORMART_YYYY_MM_DD;

                // 绑定煤层名称信息
                loadCoalSeamsInfo();

                // 设置瓦斯压力信息
                this.setGasPressureInfo();
            }
            else
            {
                // 设置日期控件格式
                this.dtpMeasureDateTime.Format = DateTimePickerFormat.Custom;
                this.dtpMeasureDateTime.CustomFormat = Const.DATE_FORMART_YYYY_MM_DD;

                // 绑定煤层名称信息
                loadCoalSeamsInfo();
            }

            if (m_point != null)
            {
                this.txtCoordinateX.Text = Math.Round(m_point.X, 3).ToString();
                this.txtCoordinateY.Text = Math.Round(m_point.Y, 3).ToString();
            }
        }

        /// <summary>
        /// 绑定煤层名称信息
        /// </summary>
        private void loadCoalSeamsInfo()
        {
            DataSet DS = CoalSeamsBLL.selectAllCoalSeamsInfo();
            this.cboCoalSeams.DataSource = DS.Tables[0];
            this.cboCoalSeams.DisplayMember = CoalSeamsDbConstNames.COAL_SEAMS_NAME;
            this.cboCoalSeams.ValueMember = CoalSeamsDbConstNames.COAL_SEAMS_ID;
            this.cboCoalSeams.SelectedIndex = -1;
        }

        /// <summary>
        /// 提  交
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnSubmit_Click(object sender, EventArgs e)
        {
            // 验证
            if (!this.check())
            {
                this.DialogResult = DialogResult.None;
                return;
            }
            this.DialogResult = DialogResult.OK;

            // 创建一个瓦斯压力点实体
            GasPressure gasPressureEntity = new GasPressure();
            // 坐标X
            double dCoordinateX = 0;
            if (double.TryParse(this.txtCoordinateX.Text.Trim(), out dCoordinateX))
            {
                gasPressureEntity.CoordinateX = dCoordinateX;
            }
            // 坐标Y
            double dCoordinateY = 0;
            if (double.TryParse(this.txtCoordinateY.Text.Trim(), out dCoordinateY))
            {
                gasPressureEntity.CoordinateY = dCoordinateY;
            }
            // 测点标高
            double dCoordinateZ = 0;
            if (double.TryParse(this.txtCoordinateZ.Text.Trim(), out dCoordinateZ))
            {
                gasPressureEntity.CoordinateZ = dCoordinateZ;
            }
            // 埋深
            double dDepth = 0;
            if (double.TryParse(this.txtDepth.Text.Trim(), out dDepth))
            {
                gasPressureEntity.Depth = dDepth;
            }
            // 瓦斯压力值
            double dGasPressureValue = 0;
            if (double.TryParse(this.txtGasPressureValue.Text.Trim(), out dGasPressureValue))
            {
                gasPressureEntity.GasPressureValue = dGasPressureValue;
            }
            // 测定时间
            gasPressureEntity.MeasureDateTime = this.dtpMeasureDateTime.Value;
            // 巷道编号
            gasPressureEntity.Tunnel.TunnelId = this.selectTunnelSimple1.ITunnelId;
            // 煤层编号
            int iCoalSeamsId = 0;
            string mc = gasPressureEntity.CoalSeams.ToString();//修改时用到改前的信息删除feature
            if (int.TryParse(Convert.ToString(this.cboCoalSeams.SelectedValue), out iCoalSeamsId))
            {
                gasPressureEntity.CoalSeams.CoalSeamsId = iCoalSeamsId;
            }

            // SQL执行结果
            bool bResult = false;

            // 添加瓦斯压力点
            if (this._bllType == "add")
            {
                // BID
                gasPressureEntity.BindingId = IDGenerator.NewBindingID();

                // 瓦斯压力数据登录
                if (GasPressureBLL.insertGasPressureInfo(gasPressureEntity))
                {
                    DrawGasGushQuantityPt(gasPressureEntity);
                }

            }
            // 修改瓦斯压力点
            else
            {
                // 主键
                gasPressureEntity.PrimaryKey = this._iPK;
                // 瓦斯压力数据修改
                if (GasPressureBLL.updateGasPressureInfo(gasPressureEntity))
                {
                    DelGasGushQuantityPt(gasPressureEntity.BindingId, mc);
                    DrawGasGushQuantityPt(gasPressureEntity);
                }
            }

            // 添加/修改成功的场合
            //if (bResult)
            //{
            //    // 20140309 lyf
            //    string sCoalseamNO = Convert.ToString(gasPressureEntity.CoalSeams);//煤层号
            //    DrawGasPressurePt(sCoalseamNO);//绘制瓦斯压力点图元
            //}
        }

        /// <summary>
        /// 取  消
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnCancel_Click(object sender, EventArgs e)
        {
            // 关闭窗口
            this.Close();
        }

        /// <summary>
        /// 验证画面入力数据
        /// </summary>
        /// <returns>验证结果：true 通过验证, false未通过验证</returns>
        private bool check()
        {
            // 判断是否选择所属巷道
            if (this.selectTunnelSimple1.ITunnelId == Const.INVALID_ID)
            {
                Alert.alert(Const_OP.TUNNEL_NAME_MUST_INPUT);
                return false;
            }

            // 判断所在煤层是否选择
            if (this.cboCoalSeams.SelectedValue == null)
            {
                Alert.alert(Const.COALSEAMS_MUST_SELECT);
                return false;
            }

            // 判断坐标X是否录入
            if (!Check.isEmpty(this.txtCoordinateX, Const_OP.COORDINATE_X))
            {
                return false;
            }

            // 判断坐标X是否为数字
            if (!Check.IsNumeric(this.txtCoordinateX, Const_OP.COORDINATE_X))
            {
                return false;
            }

            // 判断坐标Y是否录入
            if (!Check.isEmpty(this.txtCoordinateY, Const_OP.COORDINATE_Y))
            {
                return false;
            }

            // 判断坐标Y是否为数字
            if (!Check.IsNumeric(this.txtCoordinateY, Const_OP.COORDINATE_Y))
            {
                return false;
            }

            // 判断测点标高是否录入
            if (!Check.isEmpty(this.txtCoordinateZ, Const_OP.GAS_PRESSURE_COORDINATE_Z))
            {
                return false;
            }

            // 判断测点标高是否为数字
            if (!Check.IsNumeric(this.txtCoordinateZ, Const_OP.GAS_PRESSURE_COORDINATE_Z))
            {
                return false;
            }

            // 判断埋深是否录入
            if (!Check.isEmpty(this.txtDepth, Const_OP.DEPTH))
            {
                return false;
            }

            // 判断埋深是否为数字
            if (!Check.IsNumeric(this.txtDepth, Const_OP.DEPTH))
            {
                return false;
            }

            // 判断瓦斯压力值是否录入
            if (!Check.isEmpty(this.txtGasPressureValue, Const_OP.GAS_PRESSURE_VALUE))
            {
                return false;
            }

            // 判断瓦斯压力值是否为数字
            if (!Check.IsNumeric(this.txtGasPressureValue, Const_OP.GAS_PRESSURE_VALUE))
            {
                return false;
            }

            // 验证通过
            return true;
        }

        /// <summary>
        /// 设置瓦斯压力信息
        /// </summary>
        private void setGasPressureInfo()
        {
            // 通过主键获取选择的瓦斯压力信息
            DataSet ds = GasPressureBLL.selectGasPressureInfoByPK(this._iPK);

            // 检索件数 > 0时
            if (ds.Tables[0].Rows.Count > 0)
            {
                // 坐标X
                this.txtCoordinateX.Text = ds.Tables[0].Rows[0][GasPressureDbConstNames.X].ToString();
                // 坐标Y
                this.txtCoordinateY.Text = ds.Tables[0].Rows[0][GasPressureDbConstNames.Y].ToString();
                // 坐标Z
                this.txtCoordinateZ.Text = ds.Tables[0].Rows[0][GasPressureDbConstNames.Z].ToString();
                // 埋深
                this.txtDepth.Text = ds.Tables[0].Rows[0][GasPressureDbConstNames.DEPTH].ToString();
                // 瓦斯压力值
                this.txtGasPressureValue.Text = ds.Tables[0].Rows[0][GasPressureDbConstNames.GAS_PRESSURE_VALUE].ToString();
                // 测定时间
                this.dtpMeasureDateTime.Text = ds.Tables[0].Rows[0][GasPressureDbConstNames.MEASURE_DATE_TIME].ToString();

                // 所在煤层绑定
                int iCoalSeamsId = 0;
                if (int.TryParse(ds.Tables[0].Rows[0][GasPressureDbConstNames.COAL_SEAMS_ID].ToString(), out iCoalSeamsId))
                {
                    this.cboCoalSeams.SelectedValue = iCoalSeamsId;
                }

                // 所在巷道绑定
                int iTunnelID = 0;
                if (int.TryParse(ds.Tables[0].Rows[0][GasPressureDbConstNames.TUNNEL_ID].ToString(), out iTunnelID))
                {
                    Tunnel tunnelEntity = Tunnel.Find(iTunnelID); // TunnelInfoBLL.selectTunnelInfoByTunnelID(iTunnelID);
                    TunnelSimple ts = new TunnelSimple(tunnelEntity.TunnelId, tunnelEntity.TunnelName);
                    selectTunnelSimple1.SelectTunnelItemWithoutHistory(ts);

                    //if (tunnelEntity != null)
                    //{
                    //    int[] intArr = new int[5];
                    //    intArr[0] = tunnelEntity.WorkingFace.MiningArea.Horizontal.Mine.MineId;
                    //    intArr[1] = tunnelEntity.WorkingFace.MiningArea.Horizontal.HorizontalId;
                    //    intArr[2] = tunnelEntity.WorkingFace.MiningArea.MiningAreaId;
                    //    intArr[3] = tunnelEntity.WorkingFace.WorkingFaceID;
                    //    intArr[4] = tunnelEntity.Tunnel;
                    //    _intArr = intArr;
                    //}
                }
            }
        }

        #region 绘制瓦斯压力点图元

        public const string GAS_PRESSURE_PT = "瓦斯压力点";
        /// <summary>
        /// 20140308 lyf 拾取的瓦斯压力点
        /// </summary>
        private IPoint m_point = null;
        public IPoint GasPressurePoint
        {
            get
            {
                return m_point;
            }

            set
            {
                m_point = value;
            }
        }



        /// <summary>
        /// 20140309 lyf 绘制瓦斯压力点图元
        /// </summary>
        private void DrawGasPressurePt(string coalseamNO)
        {
            DrawSpecialCommon drawspecial = new DrawSpecialCommon();
            ////获得当前编辑图层
            //IFeatureLayer featureLayer = (IFeatureLayer)DataEditCommon.g_pLayer;

            ///1.获得对应瓦斯压力点图层
            string sLayerAliasName = coalseamNO + "号煤层-" + GAS_PRESSURE_PT;
            //string sLayerAliasName = GAS_PRESSURE_PT;
            IFeatureLayer featureLayer = drawspecial.GetFeatureLayerByName(sLayerAliasName);

            if (featureLayer == null)
            {
                //如果对应图层不存在，要自动创建图层
                //IFeatureLayer existFeaLayer = drawspecial.GetFeatureLayerByName(GAS_PRESSURE_PT);
                IWorkspace workspace = DataEditCommon.g_pCurrentWorkSpace;
                string layerName = "GAS_PRESSURE_PT" + "_NO" + coalseamNO;
                //若MapControl不存在该图层，但数据库中存在该图层，则先删除之，再重新生成
                IDataset dataset = drawspecial.GetDatasetByName(workspace, layerName);
                if (dataset != null) dataset.Delete();
                //自动创建图层
                IMap map = DataEditCommon.g_pMap;
                //drawspecial.CreateFeatureLayerByExistLayer(map, existFeaLayer, workspace, layerName, sLayerAliasName);
                featureLayer = drawspecial.CreateFeatureLayer(map, workspace, layerName, sLayerAliasName);
                if (featureLayer == null)
                {
                    MessageBox.Show("未成功创建" + sLayerAliasName + "图层,无法绘制瓦斯压力点图元，请联系管理员。");
                    return;
                }
            }

            ///2.绘制瓦斯压力点   
            double dCoordinateX = Convert.ToDouble(this.txtCoordinateX.Text.ToString());
            double dCoordinateY = Convert.ToDouble(this.txtCoordinateY.Text.ToString());
            double dCoordinateZ = Convert.ToDouble(this.txtCoordinateZ.Text.ToString());
            IPoint pt = new PointClass();
            pt.X = dCoordinateX;
            pt.Y = dCoordinateY;
            pt.Z = dCoordinateZ;
            DrawWSYLD pDrawWSYLD = new DrawWSYLD("P", this.txtGasPressureValue.Text.ToString(),
                this.txtCoordinateZ.Text.ToString(), this.txtDepth.Text.ToString());
            IFeature feature = featureLayer.FeatureClass.CreateFeature();

            IGeometry geometry = pt;
            DrawCommon.HandleZMValue(feature, geometry);//几何图形Z值处理
            feature.Shape = pt;
            feature.Store();

            string strValue = feature.get_Value(feature.Fields.FindField("OBJECTID")).ToString();
            DataEditCommon.SpecialPointRenderer(featureLayer, "OBJECTID", strValue, pDrawWSYLD.m_Bitmap);

            ///3.显示瓦斯压力点图层
            if (featureLayer.Visible == false)
                featureLayer.Visible = true;

            DataEditCommon.g_pMyMapCtrl.ActiveView.Refresh();
        }

        #endregion 绘制瓦斯压力点图元

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
                loadCoalSeamsInfo();
            }
        }


        /// <summary>
        /// 20140801SDE中添加瓦斯压力点
        /// </summary>
        private void DrawGasGushQuantityPt(GasPressure gasGushQuantityEntity)
        {
            double dCoordinateX = Convert.ToDouble(this.txtCoordinateX.Text.ToString());
            double dCoordinateY = Convert.ToDouble(this.txtCoordinateY.Text.ToString());
            double dCoordinateZ = Convert.ToDouble(this.txtCoordinateZ.Text.ToString());
            IPoint pt = new PointClass();
            pt.X = dCoordinateX;
            pt.Y = dCoordinateY;
            pt.Z = dCoordinateZ;
            ILayer pLayer = DataEditCommon.GetLayerByName(DataEditCommon.g_pMap, GIS.LayerNames.LAYER_ALIAS_MR_WSYLD);
            if (pLayer == null)
            {
                MessageBox.Show("未找到瓦斯压力点图层,无法绘制瓦斯压力点图元。");
                return;
            }
            IFeatureLayer pFeatureLayer = (IFeatureLayer)pLayer;
            IGeometry geometry = pt;
            List<ziduan> list = new List<ziduan>();
            list.Add(new ziduan("bid", gasGushQuantityEntity.BindingId));
            list.Add(new ziduan("mc", gasGushQuantityEntity.CoalSeams.ToString()));
            list.Add(new ziduan("addtime", DateTime.Now.ToString()));
            string wsyl = gasGushQuantityEntity.GasPressureValue.ToString();
            string cdbg = gasGushQuantityEntity.CoordinateZ.ToString();
            string ms = gasGushQuantityEntity.Depth.ToString();
            if (DataEditCommon.strLen(cdbg) < DataEditCommon.strLen(ms))
            {
                int count = DataEditCommon.strLen(ms) - DataEditCommon.strLen(cdbg);
                for (int i = 0; i < count; i++)
                {
                    cdbg = " " + cdbg; // // 测点标高
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

            list.Add(new ziduan("wsyl", wsyl));
            list.Add(new ziduan("cdbg", cdbg));
            list.Add(new ziduan("ms", ms));

            IFeature pfeature = DataEditCommon.CreateNewFeature(pFeatureLayer, geometry, list);
            if (pfeature != null)
            {
                GIS.MyMapHelp.Jump(pt);
                DataEditCommon.g_pMyMapCtrl.ActiveView.PartialRefresh(esriViewDrawPhase.esriViewGeography | esriViewDrawPhase.esriViewForeground, null, null);
            }
        }
        /// <summary>
        /// 删除瓦斯信息
        /// </summary>
        /// <param name="bid">绑定ID</param>
        /// <param name="mc">煤层</param>
        private void DelGasGushQuantityPt(string bid, string mc)
        {
            ILayer pLayer = DataEditCommon.GetLayerByName(DataEditCommon.g_pMap, GIS.LayerNames.LAYER_ALIAS_MR_WSYLD);
            IFeatureLayer pFeatureLayer = (IFeatureLayer)pLayer;
            DataEditCommon.DeleteFeatureByWhereClause(pFeatureLayer, "bid='" + bid + "' and mc='" + mc + "'");
        }
    }
}
