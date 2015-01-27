using System;
using System.Collections.Generic;
using System.Windows.Forms;
using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.Geodatabase;
using GIS.Common;
using LibCommon;
using LibEntity;

namespace sys3
{
    public partial class BoreholeInfoManagement : Form
    {
        /// <summary>
        /// 构造方法
        /// </summary>
        public BoreholeInfoManagement()
        {
            InitializeComponent();
            // 设置窗体默认属性
            FormDefaultPropertiesSetter.SetManagementFormDefaultProperties(this, Const_GM.MANAGE_BOREHOLE_INFO);
        }


        private void RefreshData()
        {
            gcBorehole.DataSource = Borehole.FindAll();
        }
        /// <summary>
        /// 添加
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnAdd_Click(object sender, EventArgs e)
        {
            var m = new BoreholeInfoEntering();
            if (DialogResult.OK == m.ShowDialog())
            {
                RefreshData();
            }
        }

        /// <summary>
        /// 修改
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnUpdate_Click(object sender, EventArgs e)
        {
            var m = new BoreholeInfoEntering(((Borehole)bandedGridView1.GetFocusedRow()).BoreholeId);
            if (DialogResult.OK == m.ShowDialog())
            {
                RefreshData();
            }

        }

        /// <summary>
        /// 删除按钮
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (Alert.confirm(Const_GM.DEL_CONFIRM_MSG_BOREHOLE))
            {
                // 钻孔数据删除
                var borehole = (Borehole)bandedGridView1.GetFocusedRow();
                borehole.Delete();
                //20140428 lyf 根据钻孔绑定ID删除图元
                DeleteZuanKongByBid(new[] { borehole.BindingId });
                RefreshData();
            }
        }

        /// <summary>
        /// 根据钻孔绑定ID删除钻孔图元
        /// </summary>
        /// <param name="sBoreholeBidArray">要删除钻孔的绑定ID</param>
        private static void DeleteZuanKongByBid(ICollection<string> sBoreholeBidArray)
        {
            if (sBoreholeBidArray.Count == 0) return;

            //1.获得当前编辑图层
            var drawspecial = new DrawSpecialCommon();
            const string sLayerAliasName = GIS.LayerNames.DEFALUT_BOREHOLE; //“默认_钻孔”图层
            var featureLayer = drawspecial.GetFeatureLayerByName(sLayerAliasName);
            if (featureLayer == null)
            {
                MessageBox.Show(@"未找到" + sLayerAliasName + @"图层,无法删除钻孔图元。");
                return;
            }

            //2.删除钻孔图元
            foreach (var sBoreholeBid in sBoreholeBidArray)
            {
                DataEditCommon.DeleteFeatureByBId(featureLayer, sBoreholeBid);
            }
        }

        /// <summary>
        /// 退出
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnExit_Click(object sender, EventArgs e)
        {
            // 关闭窗口
            Close();
        }


        /// <summary>
        /// 导出
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tsBtnExport_Click(object sender, EventArgs e)
        {
            if (saveFileDialog1.ShowDialog() == DialogResult.OK)
            {
                gcBorehole.ExportToXls(saveFileDialog1.FileName);
            }
        }

        /// <summary>
        /// 打印
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tsBtnPrint_Click(object sender, EventArgs e)
        {
            DevUtil.DevPrint(gcBorehole, "钻孔信息报表");
        }

        /// <summary>
        /// 刷新
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnRefresh_Click(object sender, EventArgs e)
        {
            RefreshData();
        }


        /// <summary>
        /// 图显按钮事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnMap_Click(object sender, EventArgs e)
        {
            // 获取已选择明细行的索引

            ILayer pLayer = DataEditCommon.GetLayerByName(DataEditCommon.g_pMap, GIS.LayerNames.DEFALUT_BOREHOLE);
            if (pLayer == null)
            {
                MessageBox.Show(@"未发现钻孔图层！");
                return;
            }
            var pFeatureLayer = (IFeatureLayer)pLayer;
            string str = "";
            string bid = ((Borehole)bandedGridView1.GetFocusedRow()).BindingId;
            if (bid != "")
            {
                if (true)
                    str = "bid='" + bid + "'";
                //else
                //    str += " or bid='" + bid + "'";
            }
            List<IFeature> list = GIS.MyMapHelp.FindFeatureListByWhereClause(pFeatureLayer, str);
            if (list.Count > 0)
            {
                GIS.MyMapHelp.Jump(GIS.MyMapHelp.GetGeoFromFeature(list));
                DataEditCommon.g_pMap.ClearSelection();
                foreach (var t in list)
                {
                    DataEditCommon.g_pMap.SelectFeature(pLayer, t);
                }
                WindowState = FormWindowState.Normal;
                Location = DataEditCommon.g_axTocControl.Location;
                Width = DataEditCommon.g_axTocControl.Width;
                Height = DataEditCommon.g_axTocControl.Height;
                DataEditCommon.g_pMyMapCtrl.ActiveView.PartialRefresh(esriViewDrawPhase.esriViewGeoSelection, null, DataEditCommon.g_pAxMapControl.Extent);
            }
            else
            {
                Alert.alert("图元丢失");
            }
        }

        private void BoreholeInfoManagement_Load(object sender, EventArgs e)
        {
            RefreshData();
        }
    }
}
