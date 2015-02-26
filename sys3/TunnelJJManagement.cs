using System;
using System.Collections.Generic;
using System.Windows.Forms;
using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.Geodatabase;
using LibCommon;
using LibCommonControl;
using LibEntity;

namespace sys3
{
    public partial class TunnelJjManagement : Form
    {
        /// <summary>
        /// 构造方法
        /// </summary>
        public TunnelJjManagement()
        {
            InitializeComponent();
        }

        private void RefreshData()
        {
            gcTunnelJJ.DataSource = Tunnel.FindAllByTunnelType(TunnelTypeEnum.TUNNELLING);
        }

        /// <summary>
        /// 初始化
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TunnelInfoManagement_Load(object sender, EventArgs e)
        {
            RefreshData();
        }

        /// <summary>
        /// 添加按钮响应
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tsBtnAdd_Click(object sender, EventArgs e)
        {
            var d = new TunnelJjEntering();
            if (DialogResult.OK == d.ShowDialog())
            {
                RefreshData();
            }
        }

        /// <summary>
        /// 修改按钮响应
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tsBtnModify_Click(object sender, EventArgs e)
        {
            var d = new TunnelJjEntering((Tunnel)gridView1.GetFocusedRow());
            if (DialogResult.OK == d.ShowDialog())
            {
                RefreshData();
            }
        }

        /// <summary>
        /// 删除按钮响应
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tsBtnDel_Click(object sender, EventArgs e)
        {
            if (!Alert.confirm(Const.DEL_CONFIRM_MSG)) return;
            //掘进ID
            var tunnel = (Tunnel)gridView1.GetFocusedRow();
            tunnel.TunnelType = TunnelTypeEnum.OTHER;
            tunnel.Save();
        }

        private void tsBtnExit_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void tsBtnRefresh_Click(object sender, EventArgs e)
        {
            RefreshData();
        }

        private void tsBtnExport_Click(object sender, EventArgs e)
        {
            if (saveFileDialog1.ShowDialog() == DialogResult.OK)
            {
                gcTunnelJJ.ExportToXls(saveFileDialog1.FileName);
            }
        }

        private void tsBtnPrint_Click(object sender, EventArgs e)
        {
            DevUtil.DevPrint(gcTunnelJJ, "掘进面信息报表");
        }

        /// <summary>
        /// 图显按钮事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnMap_Click(object sender, EventArgs e)
        {
            ILayer pLayer = GIS.Common.DataEditCommon.GetLayerByName(GIS.Common.DataEditCommon.g_pMap, GIS.LayerNames.LAYER_ALIAS_MR_STOPING_AREA);
            if (pLayer == null)
            {
                MessageBox.Show(@"未发现采掘区图层！");
                return;
            }
            var pFeatureLayer = (IFeatureLayer)pLayer;
            string str = "";
            //for (int i = 0; i < iSelIdxsArr.Length; i++)
            //{
            string bid = ((Tunnel)gridView1.GetFocusedRow()).BindingId;
            if (bid != "")
            {
                if (true)
                    str = "bid='" + bid + "'";
                //else
                //    str += " or bid='" + bid + "'";
            }
            //}
            List<IFeature> list = GIS.MyMapHelp.FindFeatureListByWhereClause(pFeatureLayer, str);
            if (list.Count > 0)
            {
                GIS.MyMapHelp.Jump(GIS.MyMapHelp.GetGeoFromFeature(list));
                GIS.Common.DataEditCommon.g_pMap.ClearSelection();
                foreach (IFeature t in list)
                {
                    GIS.Common.DataEditCommon.g_pMap.SelectFeature(pLayer, t);
                }
                WindowState = FormWindowState.Normal;
                Location = GIS.Common.DataEditCommon.g_axTocControl.Location;
                Width = GIS.Common.DataEditCommon.g_axTocControl.Width;
                Height = GIS.Common.DataEditCommon.g_axTocControl.Height;
                GIS.Common.DataEditCommon.g_pMyMapCtrl.ActiveView.PartialRefresh(esriViewDrawPhase.esriViewGeoSelection, null, GIS.Common.DataEditCommon.g_pAxMapControl.Extent);
            }
            else
            {
                Alert.alert("图元丢失");
            }
        }
    }
}
