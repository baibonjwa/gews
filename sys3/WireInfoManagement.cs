using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Windows.Forms;
using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.Geodatabase;
using FarPoint.Win.Spread;
using GIS;
using GIS.Common;
using GIS.HdProc;
using LibBusiness;
using LibCommon;
using LibCommonControl;
using LibEntity;

namespace sys3
{
    public partial class WireInfoManagement : Form
    {
        //****************变量声明***************

        // 列名称
        private readonly DataSet _ds = new DataSet();
        private readonly int[] _filterColunmIdxs = null;
        private readonly Cells cells = null;
        private int _BIDIndex = 22;
        private int _delRows = 0;
        private int _iRecordCount = 0;
        private int _rowDetailStartIndex = 2;
        private int _rowsCount = 0; //数据行数
        private int[] _wirePointPrimaryKey;
        private Tunnel tunnelEntity;
        private Wire wireEntity = new Wire();
        private WirePoint wirePointInfoEntity = new WirePoint();
        //****************************************

        /// <summary>
        ///     构造方法
        /// </summary>
        public WireInfoManagement()
        {
            InitializeComponent();

            //设置窗体属性
            FormDefaultPropertiesSetter.SetManagementFormDefaultProperties(this, Const_GM.WIRE_INFO_MANAGEMENT);
        }


        private void RefreshData()
        {
            var wires = Wire.FindAll();
            gcWireInfo.DataSource = wires;
        }

        /// <summary>
        ///     初始化
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void WireInfoManagement_Load(object sender, EventArgs e)
        {
            RefreshData();
        }

        /// <summary>
        ///     添加按钮响应
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tsBtnAdd_Click(object sender, EventArgs e)
        {
            var wireInfoForm = new WireInfoEntering();
            if (DialogResult.OK == wireInfoForm.ShowDialog())
            {
                RefreshData();
            }
        }

        /// <summary>
        ///     修改按钮响应
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tsBtnModify_Click(object sender, EventArgs e)
        {
            //实体赋值
            setWireInfoEntityValue();

            int tunnelID = Wire.Find(wireEntity.WireId).Tunnel.TunnelId;
            tunnelEntity = Tunnel.Find(tunnelID);

            //导线修改界面
            var wireInfoForm = new WireInfoEntering(wireEntity);
            if (DialogResult.OK == wireInfoForm.ShowDialog())
            {
                RefreshData();
            }
        }

        /// <summary>
        ///     导线实体赋值
        /// </summary>
        public void setWireInfoEntityValue()
        {
            for (int i = 0; i < _wirePointPrimaryKey.Length; i++)
            {
                if (cells[_rowDetailStartIndex + i, 0].Value != null &&
                    (bool)cells[_rowDetailStartIndex + i, 0].Value)
                {
                    //导线点ID
                    wirePointInfoEntity.WirePointId = _wirePointPrimaryKey[i];

                    //导线点实体
                    wirePointInfoEntity = WirePoint.Find(wirePointInfoEntity.WirePointId);

                    //矿井编号
                    wireEntity = wirePointInfoEntity.Wire;

                    //巷道实体
                    wireEntity = Wire.Find(wireEntity.WireId);
                }
            }
        }

        /// <summary>
        ///     删除按钮响应
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tsBtnDel_Click(object sender, EventArgs e)
        {
            bool bResult = false;

            //是否删除导线点
            if (Alert.confirm(Const.DEL_CONFIRM_MSG))
            {
                for (int i = 0; i < _wirePointPrimaryKey.Length; i++)
                {
                    if (cells[_rowDetailStartIndex + i, 0].Value != null &&
                        (bool)cells[_rowDetailStartIndex + i, 0].Value)
                    {
                        //导线点ID
                        wirePointInfoEntity.WirePointId = _wirePointPrimaryKey[i];

                        //导线点实体
                        wirePointInfoEntity = WirePoint.Find(wirePointInfoEntity.WirePointId);

                        //矿井编号
                        wireEntity = wirePointInfoEntity.Wire;

                        //导线实体
                        wireEntity = Wire.Find(wireEntity.WireId);
                        wireEntity.Delete();

                        //20140430 lyf
                        //同时删除导线点、巷道图元
                        DialogResult dlgResult = MessageBox.Show("是否删除对应图元？", "", MessageBoxButtons.YesNo);
                        if (dlgResult == DialogResult.Yes)
                        {
                            //DeleteWirePtByBID(wirePointInfoEntity);
                            DelHdByHdId(tunnelEntity.TunnelId.ToString());

                            //wireEntity.Tunnel
                        }
                    }
                }
                RefreshData();
            }
        }

        /// <summary>
        ///     根据巷道ID，删除巷道图元
        /// </summary>
        /// <param name="HdId">巷道Id</param>
        private void DelHdByHdId(string HdId)
        {
            //清除巷道信息
            string sql = "\"" + GIS_Const.FIELD_HDID + "\"='" + HdId + "'";
            //string sql = "\"" + GIS_Const.FIELD_HDID + "\"<>'" + HdId + "'";
            Global.commonclss.DelFeatures(Global.pntlyr, sql);
            Global.commonclss.DelFeatures(Global.centerlyr, sql);
            Global.commonclss.DelFeatures(Global.centerfdlyr, sql);
            Global.commonclss.DelFeatures(Global.hdfdfulllyr, sql);
            Global.commonclss.DelFeatures(Global.hdfdlyr, sql);
            Global.commonclss.DelFeatures(Global.pntlinlyr, sql);
            //删除峒室信息
            Global.commonclss.DelFeatures(Global.dslyr, sql);
        }

        /// <summary>
        ///     刷新按钮响应
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tsBtnRefresh_Click(object sender, EventArgs e)
        {
        }

        /// <summary>
        ///     退出按钮响应
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tsBtnExit_Click(object sender, EventArgs e)
        {
            Close();
        }


        /// <summary>
        ///     导出按钮
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tsBtnExport_Click(object sender, EventArgs e)
        {
            if (saveFileDialog1.ShowDialog() == DialogResult.OK)
            {
                gcWireInfo.ExportToXls(saveFileDialog1.FileName);
            }
        }

        /// <summary>
        ///     打印按钮
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tsBtnPrint_Click(object sender, EventArgs e)
        {
            DevUtil.DevPrint(gcWireInfo, "巷道导线点信息报表");
        }

        /// <summary>
        ///     图显按钮事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnMap_Click(object sender, EventArgs e)
        {
            // 获取已选择明细行的索引
            ILayer pLayer = DataEditCommon.GetLayerByName(DataEditCommon.g_pMap, LayerNames.DEFALUT_WIRE_PT);
            if (pLayer == null)
            {
                MessageBox.Show("未发现导线点图层！");
                return;
            }
            var pFeatureLayer = (IFeatureLayer)pLayer;
            string str = "";
            string bid = ((Wire)gridView1.GetFocusedRow()).Tunnel.BindingId;
            if (bid != "")
            {
                if (true)
                    str = "bid='" + bid + "'";
                //else
                //    str += " or bid='" + bid + "'";
            }
            List<IFeature> list = MyMapHelp.FindFeatureListByWhereClause(pFeatureLayer, str);
            if (list.Count > 0)
            {
                MyMapHelp.Jump(MyMapHelp.GetGeoFromFeature(list));
                DataEditCommon.g_pMap.ClearSelection();
                for (int i = 0; i < list.Count; i++)
                {
                    DataEditCommon.g_pMap.SelectFeature(pLayer, list[i]);
                }
                WindowState = FormWindowState.Normal;
                Location = DataEditCommon.g_axTocControl.Location;
                Width = DataEditCommon.g_axTocControl.Width;
                Height = DataEditCommon.g_axTocControl.Height;
                DataEditCommon.g_pMyMapCtrl.ActiveView.PartialRefresh(esriViewDrawPhase.esriViewGeoSelection, null,
                    DataEditCommon.g_pAxMapControl.Extent);
            }
            else
            {
                Alert.alert("图元丢失");
            }
        }

        #region 删导线点、巷道图元

        /// <summary>
        ///     根据导线点绑定ID删除导线点图元
        /// </summary>
        /// <param name="sfpFaultageBIDArray">要删除导线点的绑定ID</param>
        private void DeleteWirePtByBID(WirePoint wirePointInfoEntity)
        {
            if (wirePointInfoEntity.BindingId == "") return;

            //1.获得当前编辑图层
            var drawspecial = new DrawSpecialCommon();
            string sLayerAliasName = LayerNames.DEFALUT_WIRE_PT; //“默认_导线点”图层
            IFeatureLayer featureLayer = drawspecial.GetFeatureLayerByName(sLayerAliasName);
            if (featureLayer == null)
            {
                MessageBox.Show("未找到" + sLayerAliasName + "图层,无法删除导线点图元。");
                return;
            }

            //2.删除导线点图元
            DataEditCommon.DeleteFeatureByBId(featureLayer, wirePointInfoEntity.BindingId);
        }


        /// <summary>
        ///     根据巷道ID删除巷道图元
        /// </summary>
        /// <param name="sfpFaultageBIDArray">要删除巷道的绑定ID</param>
        private void DeleteWirePtByBID(Wire wireEntity)
        {
            if (wireEntity.Tunnel.ToString() == "") return;

            //1.获得当前编辑图层
            var drawspecial = new DrawSpecialCommon();
            string sLayerAliasName = LayerNames.DEFALUT_TUNNEL; //“默认_巷道”图层
            IFeatureLayer featureLayer = drawspecial.GetFeatureLayerByName(sLayerAliasName);
            if (featureLayer == null)
            {
                MessageBox.Show("未找到" + sLayerAliasName + "图层,无法删除巷道图元。");
                return;
            }

            //2.删除巷道图元
            DataEditCommon.DeleteFeatureByBId(featureLayer, wireEntity.Tunnel.ToString());
        }

        #endregion
    }
}