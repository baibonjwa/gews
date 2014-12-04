// ******************************************************************
// 概  述：瓦斯含量数据录入
// 作  者：伍鑫
// 创建日期：2013/12/08
// 版本号：1.0
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
using LibBusiness;
using LibCommon;
using LibEntity;
using GIS.Common;
using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.Geometry;
using ESRI.ArcGIS.Geodatabase;
using GIS.SpecialGraphic;
using LibCommonForm;
using LibCommonControl;

namespace _4.OutburstPrevention
{
    public partial class GasContentInfoEntering : BaseForm
    {
        /** 主键  **/
        private int _iPK;
        /** 业务逻辑类型：添加/修改  **/
        private string _bllType = "add";
        /** 存放矿井，水平，采区，工作面，巷道编号的数组  **/
        private int[] _intArr = new int[5];

        /// <summary>
        /// 构造方法
        /// </summary>
        public GasContentInfoEntering(MainFrm mainFrm)
        {
            InitializeComponent();

            this.MainForm = mainFrm;

            // 设置窗体默认属性
            FormDefaultPropertiesSetter.SetEnteringFormDefaultProperties(this, Const_OP.INSERT_GASCONTENT_INFO);

            // 设置日期控件格式


            //this.selectTunnelSimple1.init(mainFrm);
            // 调用选择巷道控件时需要调用的方法
            //this.selectTunnelUserControl1.loadMineName();
        }

        /// <summary>
        /// 带参数的构造方法
        /// </summary>
        /// <param name="strPrimaryKey">主键</param>
        public GasContentInfoEntering(string strPrimaryKey, MainFrm mainFrm)
        {
            InitializeComponent();

            this.MainForm = mainFrm;

            // 主键
            int iPK = 0;
            if (int.TryParse(strPrimaryKey, out iPK))
            {
                this._iPK = iPK;

                // 设置窗体默认属性
                FormDefaultPropertiesSetter.SetEnteringFormDefaultProperties(this, Const_OP.UPDATE_GASCONTENT_INFO);

                // 设置业务类型
                this._bllType = "update";

                // 设置日期控件格式

            }
        }

        /// <summary>
        /// 20140311 lyf 加载窗体时传入拾取点的坐标值
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void GasContentInfoEntering_Load(object sender, EventArgs e)
        {
            if (this._bllType == "update")
            {
                this.dtpMeasureDateTime.Format = DateTimePickerFormat.Custom;
                this.dtpMeasureDateTime.CustomFormat = Const.DATE_FORMART_YYYY_MM_DD;

                // 绑定煤层名称信息
                loadCoalSeamsInfo();

                // 设置瓦斯含量信息
                this.setGasContentInfo();

                //this.selectTunnelUserControl1.init(mainFrm);
                // 调用选择巷道控件时需要调用的方法
                //this.selectTunnelUserControl1.setCurSelectedID(_intArr);
            }
            else
            {
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
        /// 提交
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

            // 创建一个瓦斯含量点实体
            GasContent gasContentEntity = new GasContent();
            // 坐标X
            double dCoordinateX = 0;
            if (double.TryParse(this.txtCoordinateX.Text.Trim(), out dCoordinateX))
            {
                gasContentEntity.CoordinateX = dCoordinateX;
            }
            // 坐标Y
            double dCoordinateY = 0;
            if (double.TryParse(this.txtCoordinateY.Text.Trim(), out dCoordinateY))
            {
                gasContentEntity.CoordinateY = dCoordinateY;
            }
            // 坐标Z
            double dCoordinateZ = 0;
            if (double.TryParse(this.txtCoordinateZ.Text.Trim(), out dCoordinateZ))
            {
                gasContentEntity.CoordinateZ = dCoordinateZ;
            }
            // 埋深
            double dDepth = 0;
            if (double.TryParse(this.txtDepth.Text.Trim(), out dDepth))
            {
                gasContentEntity.Depth = dDepth;
            }
            // 瓦斯含量值
            double dGasContentValue = 0;
            if (double.TryParse(this.txtGasContentValue.Text.Trim(), out dGasContentValue))
            {
                gasContentEntity.GasContentValue = dGasContentValue;
            }
            // 测定时间
            gasContentEntity.MeasureDateTime = this.dtpMeasureDateTime.Value;
            // 巷道编号
            gasContentEntity.TunnelID = this.selectTunnelSimple1.ITunnelId;
            // 煤层编号
            int iCoalSeamsId = 0;
            string mc = gasContentEntity.CoalSeamsId.ToString();//修改时用到改前的信息删除feature
            if (int.TryParse(Convert.ToString(this.cboCoalSeams.SelectedValue), out iCoalSeamsId))
            {
                gasContentEntity.CoalSeamsId = iCoalSeamsId;
            }

            if (this._bllType == "add")
            {
                // BID
                gasContentEntity.BindingId = IDGenerator.NewBindingID();
                // 瓦斯含量信息登录
                if (GasContentBLL.insertGasContentInfo(gasContentEntity))
                {
                    DrawGasGushQuantityPt(gasContentEntity);
                }
            }
            else
            {
                // 主键
                gasContentEntity.PrimaryKey = this._iPK;
                // 瓦斯含量数据修改
                if (GasContentBLL.updateGasContentInfo(gasContentEntity))
                {
                    DelGasGushQuantityPt(gasContentEntity.BindingId, mc);
                    DrawGasGushQuantityPt(gasContentEntity);
                }
            }
        }

        /// <summary>
        /// 取消
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

            // 判断瓦斯含量值是否录入
            if (!Check.isEmpty(this.txtGasContentValue, Const_OP.GAS_CONTENT_VALUE))
            {
                return false;
            }

            // 判断瓦斯含量值是否为数字
            if (!Check.IsNumeric(this.txtGasContentValue, Const_OP.GAS_CONTENT_VALUE))
            {
                return false;
            }

            // 验证通过
            return true;
        }

        /// <summary>
        /// 设置瓦斯含量信息
        /// </summary>
        private void setGasContentInfo()
        {
            // 根据主键获取瓦斯含量信息
            DataSet ds = GasContentBLL.selectGasContentInfoByPK(this._iPK);

            // 检索件数 > 0时
            if (ds.Tables[0].Rows.Count > 0)
            {
                // 坐标X
                this.txtCoordinateX.Text = ds.Tables[0].Rows[0][GasContentDbConstNames.X].ToString();
                // 坐标Y
                this.txtCoordinateY.Text = ds.Tables[0].Rows[0][GasContentDbConstNames.Y].ToString();
                // 坐标Z
                this.txtCoordinateZ.Text = ds.Tables[0].Rows[0][GasContentDbConstNames.Z].ToString();
                // 埋深
                this.txtDepth.Text = ds.Tables[0].Rows[0][GasContentDbConstNames.DEPTH].ToString();
                // 瓦斯含量值
                this.txtGasContentValue.Text = ds.Tables[0].Rows[0][GasContentDbConstNames.GAS_CONTENT_VALUE].ToString();
                // 测定时间
                this.dtpMeasureDateTime.Text = ds.Tables[0].Rows[0][GasContentDbConstNames.MEASURE_DATE_TIME].ToString();

                // 所在煤层绑定
                int iCoalSeamsId = 0;
                if (int.TryParse(ds.Tables[0].Rows[0][GasContentDbConstNames.COAL_SEAMS_ID].ToString(), out iCoalSeamsId))
                {
                    this.cboCoalSeams.SelectedValue = iCoalSeamsId;
                }

                // 所在巷道绑定
                int iTunnelID = 0;
                if (int.TryParse(ds.Tables[0].Rows[0][GasContentDbConstNames.TUNNEL_ID].ToString(), out iTunnelID))
                {
                    Tunnel tunnelEntity = BasicInfoManager.getInstance().getTunnelByID(iTunnelID);// TunnelInfoBLL.selectTunnelInfoByTunnelID(iTunnelID);
                    TunnelSimple ts = new TunnelSimple(tunnelEntity.TunnelID, tunnelEntity.TunnelName);
                    selectTunnelSimple1.SelectTunnelItemWithoutHistory(ts);
                    //if (tunnelEntity != null)
                    //{
                    //    int[] intArr = new int[5];
                    //    intArr[0] = tunnelEntity.WorkingFace.MiningArea.Horizontal.Mine.MineId;
                    //    intArr[1] = tunnelEntity.WorkingFace.MiningArea.Horizontal.HorizontalId;
                    //    intArr[2] = tunnelEntity.WorkingFace.MiningArea.MiningAreaId;
                    //    intArr[3] = tunnelEntity.WorkingFace.WorkingFaceID;
                    //    intArr[4] = tunnelEntity.TunnelID;
                    //    _intArr = intArr;
                    //}
                }
            }
        }


        #region 绘制瓦斯含量点图元

        public const string GAS_CONTENT_PT = "瓦斯含量点";
        /// <summary>
        /// 20140311 lyf 拾取的瓦斯含量点
        /// </summary>
        private IPoint m_point = null;
        public IPoint GasContentPoint
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
        /// 20140311 lyf 绘制瓦斯含量点图元
        /// </summary>
        private void DrawGasContentPt(string coalseamNO)
        {
            DrawSpecialCommon drawspecial = new DrawSpecialCommon();
            ////获得当前编辑图层
            //IFeatureLayer featureLayer = (IFeatureLayer)DataEditCommon.g_pLayer;

            ///1.获得对应瓦斯含量点图层
            string sLayerAliasName = coalseamNO + "号煤层-" + GAS_CONTENT_PT;
            IFeatureLayer featureLayer = drawspecial.GetFeatureLayerByName(sLayerAliasName);

            if (featureLayer == null)
            {
                //如果对应图层不存在，要自动创建图层                
                IWorkspace workspace = DataEditCommon.g_pCurrentWorkSpace;
                string layerName = "GAS_CONTENT_PT" + "_NO" + coalseamNO;
                //若MapControl不存在该图层，但数据库中存在该图层，则先删除之，再重新生成
                IDataset dataset = drawspecial.GetDatasetByName(workspace, "GasEarlyWarningGIS.SDE." + layerName);
                if (dataset != null) dataset.Delete();
                //自动创建图层
                IMap map = DataEditCommon.g_pMap;
                featureLayer = drawspecial.CreateFeatureLayer(map, workspace, layerName, sLayerAliasName);
                if (featureLayer == null)
                {
                    MessageBox.Show("未成功创建" + sLayerAliasName + "图层,无法绘制瓦斯含量点图元。");
                    return;
                }
            }

            ///2.绘制瓦斯含量点   
            double dCoordinateX = Convert.ToDouble(this.txtCoordinateX.Text.ToString());
            double dCoordinateY = Convert.ToDouble(this.txtCoordinateY.Text.ToString());
            double dCoordinateZ = Convert.ToDouble(this.txtCoordinateZ.Text.ToString());
            IPoint pt = new PointClass();
            pt.X = dCoordinateX;
            pt.Y = dCoordinateY;
            pt.Z = dCoordinateZ;
            DrawWSHLD pDrawWSYLD = new DrawWSHLD("W", this.txtGasContentValue.Text.ToString(),
                this.txtCoordinateZ.Text.ToString(), this.txtDepth.Text.ToString());
            IFeature feature = featureLayer.FeatureClass.CreateFeature();

            IGeometry geometry = pt;
            DrawCommon.HandleZMValue(feature, geometry);//几何图形Z值处理
            feature.Shape = pt;
            feature.Store();

            string strValue = feature.get_Value(feature.Fields.FindField("OBJECTID")).ToString();
            DataEditCommon.SpecialPointRenderer(featureLayer, "OBJECTID", strValue, pDrawWSYLD.m_Bitmap);

            ///3.显示瓦斯含量点图层
            if (featureLayer.Visible == false)
                featureLayer.Visible = true;

            DataEditCommon.g_pMyMapCtrl.ActiveView.Refresh();
        }

        #endregion 绘制瓦斯含量点图元

        /// <summary>
        /// 煤层信息添加管理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnAddCoalSeams_Click(object sender, EventArgs e)
        {
            CommonManagement commonManagementForm = new CommonManagement(5, 0, this.MainForm);

            if (DialogResult.OK == commonManagementForm.ShowDialog())
            {
                // 绑定煤层名称信息
                loadCoalSeamsInfo();
            }
        }


        /// <summary>
        /// 20140801SDE中添加瓦斯含量点
        /// </summary>
        private void DrawGasGushQuantityPt(GasContent gasGushQuantityEntity)
        {
            double dCoordinateX = Convert.ToDouble(this.txtCoordinateX.Text.ToString());
            double dCoordinateY = Convert.ToDouble(this.txtCoordinateY.Text.ToString());
            double dCoordinateZ = Convert.ToDouble(this.txtCoordinateZ.Text.ToString());
            IPoint pt = new PointClass();
            pt.X = dCoordinateX;
            pt.Y = dCoordinateY;
            pt.Z = dCoordinateZ;
            ILayer pLayer = DataEditCommon.GetLayerByName(DataEditCommon.g_pMap, GIS.LayerNames.LAYER_ALIAS_MR_WSHLD);
            if (pLayer == null)
            {
                MessageBox.Show("未找到瓦斯含量点图层,无法绘制瓦斯含量点图元。");
                return;
            }
            IFeatureLayer pFeatureLayer = (IFeatureLayer)pLayer;
            IGeometry geometry = pt;
            List<ziduan> list = new List<ziduan>();
            list.Add(new ziduan("bid", gasGushQuantityEntity.BindingId));
            list.Add(new ziduan("mc", gasGushQuantityEntity.CoalSeamsId.ToString()));
            list.Add(new ziduan("addtime", DateTime.Now.ToString()));
            string wshl = gasGushQuantityEntity.GasContentValue.ToString(); // 瓦斯含量
            string cdbg = gasGushQuantityEntity.CoordinateZ.ToString();
            string ms = gasGushQuantityEntity.Depth.ToString(); // 埋深
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
            ILayer pLayer = DataEditCommon.GetLayerByName(DataEditCommon.g_pMap, GIS.LayerNames.LAYER_ALIAS_MR_WSHLD);
            IFeatureLayer pFeatureLayer = (IFeatureLayer)pLayer;
            DataEditCommon.DeleteFeatureByWhereClause(pFeatureLayer, "bid='" + bid + "' and mc='" + mc + "'");
        }
    }
}
