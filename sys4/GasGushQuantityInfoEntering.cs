// ******************************************************************
// 概  述：瓦斯涌出量点数据录入
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
using LibEntity;
using LibBusiness;
using LibCommon;
using ESRI.ArcGIS.Geometry;
using GIS.Common;
using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.Geodatabase;
using GIS.SpecialGraphic;
using LibCommonForm;
using LibCommonControl;

namespace _4.OutburstPrevention
{
    public partial class GasGushQuantityInfoEntering : BaseForm
    {
        /** 主键  **/
        private int _iPK;
        /** 业务逻辑类型：添加、修改  **/
        private string _bllType = "add";
        /** 存放矿井，水平，采区，工作面，巷道编号的数组  **/
        private int[] _intArr = new int[5];

        /// <summary>
        /// 构造方法
        /// </summary>
        public GasGushQuantityInfoEntering(MainFrm mainFrm)
        {
            InitializeComponent();

            this.MainForm = mainFrm;

            // 设置窗体默认属性
            FormDefaultPropertiesSetter.SetEnteringFormDefaultProperties(this, Const_OP.INSERT_GASGUSHQUANTITY_INFO);


            //this.selectTunnelUserControl1.init(mainFrm);
            // 调用选择巷道控件时需要调用的方法
            //this.selectTunnelUserControl1.loadMineName();
        }

        /// <summary>
        /// 带参数的构造方法
        /// </summary>
        /// <param name="strPrimaryKey">主键</param>
        public GasGushQuantityInfoEntering(string strPrimaryKey, MainFrm mainFrm)
        {
            InitializeComponent();

            this.MainForm = mainFrm;

            // 主键
            int iPK = 0;
            if (int.TryParse(strPrimaryKey, out iPK))
            {
                this._iPK = iPK;

                // 设置窗体默认属性
                FormDefaultPropertiesSetter.SetEnteringFormDefaultProperties(this, Const_OP.UPDATE_GASGUSHQUANTITY_INFO);

                // 设置业务类型
                this._bllType = "update";



                //this.selectTunnelUserControl1.init(mainFrm);
                // 调用选择巷道控件时需要调用的方法
                //this.selectTunnelUserControl1.setCurSelectedID(_intArr);

            }
        }


        /// <summary>
        /// 20140311 lyf 加载窗体时传入拾取点的坐标值
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void GasGushQuantityInfoEntering_Load(object sender, EventArgs e)
        {
            if (this._bllType == "update")
            {
                // 设置日期控件格式
                this.dtpStopeDate.Format = DateTimePickerFormat.Custom;
                this.dtpStopeDate.CustomFormat = Const.DATE_FORMART_YYYY_MM;

                // 绑定煤层名称信息
                loadCoalSeamsInfo();

                // 设置瓦斯涌出量信息
                this.setGasGushQuantityInfo();
            }
            else
            {
                // 设置日期控件格式
                this.dtpStopeDate.Format = DateTimePickerFormat.Custom;
                this.dtpStopeDate.CustomFormat = Const.DATE_FORMART_YYYY_MM;

                // 绑定煤层名称信息
                loadCoalSeamsInfo();
            }

            if (m_point != null)
            {
                this.txtCoordinateX.Text = Math.Round(m_point.X, 3).ToString();
                this.txtCoordinateY.Text = Math.Round(m_point.Y, 3).ToString();
                this.txtCoordinateZ.Text = Math.Round(m_point.Z, 3).ToString();
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

            // 创建瓦斯涌出量点实体
            GasGushQuantity gasGushQuantityEntity = new GasGushQuantity();
            // 坐标X
            double dCoordinateX = 0;
            if (double.TryParse(this.txtCoordinateX.Text.Trim(), out dCoordinateX))
            {
                gasGushQuantityEntity.CoordinateX = dCoordinateX;
            }
            // 坐标Y
            double dCoordinateY = 0;
            if (double.TryParse(this.txtCoordinateY.Text.Trim(), out dCoordinateY))
            {
                gasGushQuantityEntity.CoordinateY = dCoordinateY;
            }
            // 坐标Z
            double dCoordinateZ = 0;
            if (double.TryParse(this.txtCoordinateZ.Text.Trim(), out dCoordinateZ))
            {
                gasGushQuantityEntity.CoordinateZ = dCoordinateZ;
            }
            // 绝对瓦斯涌出量
            double dAbsoluteGasGushQuantity = 0;
            if (double.TryParse(this.txtAbsoluteGasGushQuantity.Text.Trim(), out dAbsoluteGasGushQuantity))
            {
                gasGushQuantityEntity.AbsoluteGasGushQuantity = dAbsoluteGasGushQuantity;
            }
            // 相对瓦斯涌出量
            double dRelativeGasGushQuantity = 0;
            if (double.TryParse(this.txtRelativeGasGushQuantity.Text.Trim(), out dRelativeGasGushQuantity))
            {
                gasGushQuantityEntity.RelativeGasGushQuantity = dRelativeGasGushQuantity;
            }
            // 工作面日产量
            double dWorkingFaceDayOutput = 0;
            if (double.TryParse(this.txtWorkingFaceDayOutput.Text.Trim(), out dWorkingFaceDayOutput))
            {
                gasGushQuantityEntity.WorkingFaceDayOutput = dWorkingFaceDayOutput;
            }
            // 回采年月
            gasGushQuantityEntity.StopeDate = this.dtpStopeDate.Text;
            // 巷道编号
            gasGushQuantityEntity.Tunnel.TunnelId = this.selectTunnelSimple1.ITunnelId;
            // 煤层编号
            int iCoalSeamsId = 0;
            string mc = gasGushQuantityEntity.CoalSeams.ToString();//修改时用到改前的信息删除feature
            if (int.TryParse(Convert.ToString(this.cboCoalSeams.SelectedValue), out iCoalSeamsId))
            {
                gasGushQuantityEntity.CoalSeams.CoalSeamsId = iCoalSeamsId;
            }

            bool bResult = false;
            if (this._bllType == "add")
            {
                // BID
                gasGushQuantityEntity.BindingId = IDGenerator.NewBindingID();

                // 瓦斯涌出量数据登录
                if (GasGushQuantityBLL.insertGasGushQuantityInfo(gasGushQuantityEntity))
                    DrawGasGushQuantityPt(gasGushQuantityEntity);
            }
            else
            {
                // 主键
                gasGushQuantityEntity.PrimaryKey = this._iPK;
                // 瓦斯涌出量数据修改
                if (GasGushQuantityBLL.updateGasGushQuantityInfo(gasGushQuantityEntity))
                {
                    DelGasGushQuantityPt(gasGushQuantityEntity.BindingId, mc);
                    DrawGasGushQuantityPt(gasGushQuantityEntity);
                }
            }

            // 添加/修改成功的场合
            //if (bResult)
            //{
            //    // 20140311 lyf
            //    string sCoalseamNO = Convert.ToString(gasGushQuantityEntity.CoalSeams);//煤层号
            //    DrawGasGushQuantityPt(sCoalseamNO);//绘制瓦斯压力点图元
            //}
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

            // 判断坐标Z是否录入
            if (!Check.isEmpty(this.txtCoordinateZ, Const_OP.STOPE_WORKING_FACE_GAS_GUSH_QUANTITY_COORDINATE_Z))
            {
                return false;
            }

            // 判断坐标Z是否为数字
            if (!Check.IsNumeric(this.txtCoordinateZ, Const_OP.STOPE_WORKING_FACE_GAS_GUSH_QUANTITY_COORDINATE_Z))
            {
                return false;
            }

            // 判断绝对瓦斯涌出量是否录入
            if (!Check.isEmpty(this.txtAbsoluteGasGushQuantity, Const_OP.ABSOLUTE_GAS_GUSH_QUANTITY))
            {
                return false;
            }

            // 判断绝对瓦斯涌出量是否为数字
            if (!Check.IsNumeric(this.txtAbsoluteGasGushQuantity, Const_OP.ABSOLUTE_GAS_GUSH_QUANTITY))
            {
                return false;
            }

            // 判断相对瓦斯涌出量是否录入
            if (!Check.isEmpty(this.txtRelativeGasGushQuantity, Const_OP.RELATIVE_GAS_GUSH_QUANTITY))
            {
                return false;
            }

            // 判断相对瓦斯涌出量是否为数字
            if (!Check.IsNumeric(this.txtRelativeGasGushQuantity, Const_OP.RELATIVE_GAS_GUSH_QUANTITY))
            {
                return false;
            }

            // 判断工作面日产量是否录入
            if (!Check.isEmpty(this.txtWorkingFaceDayOutput, Const_OP.WORKING_FACE_DAY_OUTPUT))
            {
                return false;
            }

            // 判断工作面日产量是否为数字
            if (!Check.IsNumeric(this.txtWorkingFaceDayOutput, Const_OP.WORKING_FACE_DAY_OUTPUT))
            {
                return false;
            }

            // 验证通过
            return true;
        }

        /// <summary>
        /// 设置瓦斯涌出量信息
        /// </summary>
        private void setGasGushQuantityInfo()
        {
            // 通过主键获取瓦斯涌出量信息
            DataSet ds = GasGushQuantityBLL.selectGasGushQuantityInfoByPK(this._iPK);

            if (ds.Tables[0].Rows.Count > 0)
            {
                // 坐标X
                this.txtCoordinateX.Text = ds.Tables[0].Rows[0][GasGushQuantityDbConstNames.X].ToString();
                // 坐标Y
                this.txtCoordinateY.Text = ds.Tables[0].Rows[0][GasGushQuantityDbConstNames.Y].ToString();
                // 坐标Z
                this.txtCoordinateZ.Text = ds.Tables[0].Rows[0][GasGushQuantityDbConstNames.Z].ToString();
                // 绝对瓦斯涌出量
                this.txtAbsoluteGasGushQuantity.Text = ds.Tables[0].Rows[0][GasGushQuantityDbConstNames.ABSOLUTE_GAS_GUSH_QUANTITY].ToString();
                // 相对瓦斯涌出量
                this.txtRelativeGasGushQuantity.Text = ds.Tables[0].Rows[0][GasGushQuantityDbConstNames.RELATIVE_GAS_GUSH_QUANTITY].ToString();
                // 工作面日产量
                this.txtWorkingFaceDayOutput.Text = ds.Tables[0].Rows[0][GasGushQuantityDbConstNames.WORKING_FACE_DAY_OUTPUT].ToString();
                // 回采年月
                this.dtpStopeDate.Text = ds.Tables[0].Rows[0][GasGushQuantityDbConstNames.STOPE_DATE].ToString();

                // 所在煤层绑定
                int iCoalSeamsId = 0;
                if (int.TryParse(ds.Tables[0].Rows[0][GasGushQuantityDbConstNames.COAL_SEAMS_ID].ToString(), out iCoalSeamsId))
                {
                    this.cboCoalSeams.SelectedValue = iCoalSeamsId;
                }

                // 所在巷道绑定
                int iTunnelID = 0;
                if (int.TryParse(ds.Tables[0].Rows[0][GasGushQuantityDbConstNames.TUNNEL_ID].ToString(), out iTunnelID))
                {
                    Tunnel tunnelEntity = Tunnel.Find(iTunnelID);// TunnelInfoBLL.selectTunnelInfoByTunnelID(iTunnelID);
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


        #region 绘制瓦斯涌出量点图元

        public const string STOPE_WORKING_FACE_GAS_GUSH_QUANTITY_PT = "瓦斯涌出量点";
        /// <summary>
        /// 20140311 lyf 拾取的瓦斯涌出量点
        /// </summary>
        private IPoint m_point = null;
        public IPoint GasGushQuantityPoint
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
        /// 20140801SDE中添加瓦斯涌出量点
        /// </summary>
        private void DrawGasGushQuantityPt(GasGushQuantity gasGushQuantityEntity)
        {
            double dCoordinateX = Convert.ToDouble(this.txtCoordinateX.Text.ToString());
            double dCoordinateY = Convert.ToDouble(this.txtCoordinateY.Text.ToString());
            double dCoordinateZ = Convert.ToDouble(this.txtCoordinateZ.Text.ToString());
            IPoint pt = new PointClass();
            pt.X = dCoordinateX;
            pt.Y = dCoordinateY;
            pt.Z = dCoordinateZ;
            ILayer pLayer = DataEditCommon.GetLayerByName(DataEditCommon.g_pMap, GIS.LayerNames.LAYER_ALIAS_MR_HCGZMWSYCLD);
            if (pLayer == null)
            {
                MessageBox.Show("未找到瓦斯涌出量点图层,无法绘制瓦斯涌出量点图元。");
                return;
            }
            IFeatureLayer pFeatureLayer = (IFeatureLayer)pLayer;
            IGeometry geometry = pt;
            List<ziduan> list = new List<ziduan>();
            list.Add(new ziduan("bid", gasGushQuantityEntity.BindingId));
            list.Add(new ziduan("mc", gasGushQuantityEntity.CoalSeams.ToString()));
            list.Add(new ziduan("addtime", DateTime.Now.ToString()));
            string hcny = gasGushQuantityEntity.StopeDate;
            string ydwsycl = gasGushQuantityEntity.AbsoluteGasGushQuantity.ToString();
            string xdwsycl = gasGushQuantityEntity.RelativeGasGushQuantity.ToString();
            string gzmrcl = gasGushQuantityEntity.WorkingFaceDayOutput.ToString();
            if (DataEditCommon.strLen(ydwsycl) < DataEditCommon.strLen(xdwsycl))
            {
                int count = DataEditCommon.strLen(xdwsycl) - DataEditCommon.strLen(ydwsycl);
                for (int i = 0; i < count; i++)
                {
                    ydwsycl += " ";
                }
            }
            else if (DataEditCommon.strLen(ydwsycl) > DataEditCommon.strLen(xdwsycl))
            {
                int count = DataEditCommon.strLen(ydwsycl) - DataEditCommon.strLen(xdwsycl);
                for (int i = 0; i < count; i++)
                {
                    xdwsycl += " ";
                }
            }
            if (DataEditCommon.strLen(gzmrcl) < DataEditCommon.strLen(hcny))
            {
                int count = DataEditCommon.strLen(hcny) - DataEditCommon.strLen(gzmrcl);
                for (int i = 0; i < count; i++)
                {
                    gzmrcl = " " + gzmrcl;
                }
            }
            else if (DataEditCommon.strLen(gzmrcl) > DataEditCommon.strLen(hcny))
            {
                int count = DataEditCommon.strLen(gzmrcl) - DataEditCommon.strLen(hcny);
                for (int i = 0; i < count; i++)
                {
                    hcny += " ";
                }
            }
            list.Add(new ziduan("hcny", hcny));
            list.Add(new ziduan("jdwsycl", ydwsycl));
            list.Add(new ziduan("xdwsycl", xdwsycl));
            list.Add(new ziduan("gzmrcl", gzmrcl));

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
            ILayer pLayer = DataEditCommon.GetLayerByName(DataEditCommon.g_pMap, GIS.LayerNames.LAYER_ALIAS_MR_HCGZMWSYCLD);
            IFeatureLayer pFeatureLayer = (IFeatureLayer)pLayer;
            DataEditCommon.DeleteFeatureByWhereClause(pFeatureLayer, "bid='" + bid + "' and mc='" + mc + "'");
        }
        /// <summary>
        /// 20140311 lyf 绘制瓦斯涌出量点图元
        /// </summary>
        //private void DrawGasGushQuantityPt(string coalseamNO)
        //{
        //    DrawSpecialCommon drawspecial = new DrawSpecialCommon();
        //    ////获得当前编辑图层
        //    //IFeatureLayer featureLayer = (IFeatureLayer)DataEditCommon.g_pLayer;

        //    ///1.获得对应瓦斯涌出量点图层
        //    string sLayerAliasName = coalseamNO + "号煤层-" + STOPE_WORKING_FACE_GAS_GUSH_QUANTITY_PT;            
        //    IFeatureLayer featureLayer = drawspecial.GetFeatureLayerByName(sLayerAliasName);

        //    if (featureLayer == null)
        //    {
        //        //如果对应图层不存在，要自动创建图层
        //        IWorkspace workspace = DataEditCommon.g_pCurrentWorkSpace;
        //        string layerName = "STOPE_WORKING_FACE_GAS_GUSH_QUANTITY_PT" + "_NO" + coalseamNO;
        //        //若MapControl不存在该图层，但数据库中存在该图层，则先删除之，再重新生成
        //        IDataset dataset = drawspecial.GetDatasetByName(workspace, layerName);
        //        if (dataset != null) dataset.Delete();
        //        //自动创建图层
        //        IMap map = DataEditCommon.g_pMap;                
        //        featureLayer = drawspecial.CreateFeatureLayer(map, workspace, layerName, sLayerAliasName);
        //        if (featureLayer == null)
        //        {
        //            MessageBox.Show("未成功创建" + sLayerAliasName + "图层,无法绘制瓦斯涌出量点图元。");
        //            return;
        //        }
        //    }

        //    ///2.绘制瓦斯涌出量点   
        //    double dCoordinateX = Convert.ToDouble(this.txtCoordinateX.Text.ToString());
        //    double dCoordinateY = Convert.ToDouble(this.txtCoordinateY.Text.ToString());
        //    double dCoordinateZ = Convert.ToDouble(this.txtCoordinateZ.Text.ToString());
        //    IPoint pt = new PointClass();
        //    pt.X = dCoordinateX;
        //    pt.Y = dCoordinateY;
        //    pt.Z = dCoordinateZ;         

        //    DrawWSYCLD pDrawWSYLD = new DrawWSYCLD(this.txtAbsoluteGasGushQuantity.Text.ToString(),
        //                                            this.txtRelativeGasGushQuantity.Text.ToString(),
        //                                            this.txtWorkingFaceDayOutput.Text.ToString(),
        //                                            this.dtpStopeDate.Text.ToString());

        //    IFeature feature = featureLayer.FeatureClass.CreateFeature();

        //    IGeometry geometry = pt;
        //    DrawCommon.HandleZMValue(feature, geometry);//几何图形Z值处理
        //    feature.Shape = pt;
        //    feature.Store();

        //    string strValue = feature.get_Value(feature.Fields.FindField("OBJECTID")).ToString();
        //    DataEditCommon.SpecialPointRenderer(featureLayer, "OBJECTID", strValue, pDrawWSYLD.m_Bitmap);

        //    ///3.显示瓦斯涌出量点图层
        //    if (featureLayer.Visible == false)
        //        featureLayer.Visible = true;

        //    DataEditCommon.g_pMyMapCtrl.ActiveView.Refresh();
        //}

        #endregion 绘制瓦斯涌出量点图元

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

    }
}
