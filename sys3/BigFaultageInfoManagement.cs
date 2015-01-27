using System;
using System.Collections.Generic;
using System.Globalization;
using System.Windows.Forms;
using ESRI.ArcGIS.Carto;
using GIS.HdProc;
using LibCommon;
using LibEntity;

namespace sys3
{
    public partial class BigFaultageInfoManagement : Form
    {

        // 构造方法
        public BigFaultageInfoManagement()
        {
            InitializeComponent();
        }

        private void RefreshData()
        {
            var bigFaultages = BigFaultage.FindAll();
            gcBigFaultage.DataSource = bigFaultages;
        }

        /// <summary>
        /// 添加（必须实装）
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnAdd_Click(object sender, EventArgs e)
        {
            var m = new BigFaultageInfoEntering();

            if (DialogResult.OK == m.ShowDialog())
            {
                RefreshData();
            }
        }

        /// <summary>
        /// 修改（必须实装）
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnUpdate_Click(object sender, EventArgs e)
        {
            var bigFaultageInfoEntering = new BigFaultageInfoEntering(((BigFaultage)gridView1.GetFocusedRow()).FaultageId.ToString(CultureInfo.InvariantCulture));
            if (DialogResult.OK == bigFaultageInfoEntering.ShowDialog())
            {
                RefreshData();
            }
        }

        /// <summary>
        /// 删除按钮（必须实装）
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (!Alert.confirm("确认删除数据吗？")) return;
            var bigFaultage = (BigFaultage)gridView1.GetFocusedRow();
            var delIds = new[] { bigFaultage.BindingId };
            Global.tdclass.DelTdLyr(delIds);
            bigFaultage.Delete();
            RefreshData();
        }


        /// <summary>
        /// 退出
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnExit_Click(object sender, EventArgs e)
        {
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
                gcBigFaultage.ExportToXls(saveFileDialog1.FileName);
            }
        }

        /// <summary>
        /// 打印
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tsBtnPrint_Click(object sender, EventArgs e)
        {
            DevUtil.DevPrint(gcBigFaultage, "推断断层信息报表");
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
            int[] iSelIdxsArr = { ((BigFaultage)gridView1.GetFocusedRow()).FaultageId };

            ILayer pLayer = GIS.Common.DataEditCommon.GetLayerByName(GIS.Common.DataEditCommon.g_pMap, GIS.LayerNames.DEFALUT_INFERRED_FAULTAGE);
            if (pLayer == null)
            {
                MessageBox.Show(@"未发现推断断层图层！");
                return;
            }
            IFeatureLayer pFeatureLayer = (IFeatureLayer)pLayer;
            string str = "";
            for (int i = 0; i < iSelIdxsArr.Length; i++)
            {
                string bid = ((BigFaultage)gridView1.GetFocusedRow()).BindingId;
                if (bid == "") continue;
                if (i == 0)
                    str = "bid='" + bid + "'";
                else
                    str += " or bid='" + bid + "'";
            }
            List<ESRI.ArcGIS.Geodatabase.IFeature> list = GIS.MyMapHelp.FindFeatureListByWhereClause(pFeatureLayer, str);
            if (list.Count > 0)
            {
                GIS.MyMapHelp.Jump(GIS.MyMapHelp.GetGeoFromFeature(list));
                GIS.Common.DataEditCommon.g_pMap.ClearSelection();
                for (int i = 0; i < list.Count; i++)
                {
                    GIS.Common.DataEditCommon.g_pMap.SelectFeature(pLayer, list[i]);
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

        private void BigFaultageInfoManagement_Load(object sender, EventArgs e)
        {
            RefreshData();
        }
    }
}
