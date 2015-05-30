using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using ESRI.ArcGIS.Geometry;
using ESRI.ArcGIS.Geodatabase;
using GIS.Common;
using ESRI.ArcGIS.Controls;

namespace GIS.SpecialGraphic
{
    public partial class FrmZhzzt : Form
    {
        public FrmZhzzt()
        {
            InitializeComponent();
        }
        public IGeometry pGeometry;
        public string BID;
        public double blc = 50;
        public IFeatureClass pFeatureClass;
        public ESRI.ArcGIS.Carto.ILayer pLayer;
        private void tStripBtnNew_Click(object sender, EventArgs e)
        {
            FrmNewZHZZT frm = new FrmNewZHZZT(this);
            if (frm.ShowDialog(this) == DialogResult.OK)
            {
                MyMapHelp.Show_IsVisiable(axMapControlZZT.Map, pLayer, "BID='" + BID + "'");
                pGeometry = setgeo(pGeometry);
                axMapControlZZT.Extent = pGeometry.Envelope;
                axMapControlZZT.MapScale = blc;
                axMapControlZZT.ActiveView.Refresh();
            }
            else
            {
                MyMapHelp.Show_IsVisiable(axMapControlZZT.Map, pLayer, "BID='-1'");
            }
        }

        private void tStripBtnOpen_Click(object sender, EventArgs e)
        {
            FrmManageZhzzt frm = new FrmManageZhzzt(this);
            if (frm.ShowDialog(this) == DialogResult.OK)
            {
                MyMapHelp.Show_IsVisiable(axMapControlZZT.Map, pLayer, "BID='" + BID + "'");
                pGeometry = setgeo(pGeometry);
                axMapControlZZT.Extent = pGeometry.Envelope;
                axMapControlZZT.MapScale = blc;
                axMapControlZZT.ActiveView.Refresh();
            }
            else
            {
                MyMapHelp.Show_IsVisiable(axMapControlZZT.Map, pLayer, "BID='-1'");
            }
        }
        public void refresh()
        {
            MyMapHelp.Show_IsVisiable(axMapControlZZT.Map, pLayer, "BID='" + BID + "'");
            pGeometry = setgeo(pGeometry);
            axMapControlZZT.Extent = pGeometry.Envelope;
            axMapControlZZT.MapScale = blc;
            axMapControlZZT.ActiveView.Refresh();
        }
        private void tStripBtnSave_Click(object sender, EventArgs e)
        {
            MyMapHelp.ExportPicPdf((IMapControl3)axMapControlZZT.Object);
        }

        private void tStripBtnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void FrmZhzzt_Load(object sender, EventArgs e)
        {
            axMapControlZZT.LoadMxFile(Application.StartupPath+@"\综合柱状图.mxd");
            pLayer = DataEditCommon.GetLayerByName(axMapControlZZT.Map, LayerNames.LAYER_ALIAS_MR_Zhuzhuang);
            ESRI.ArcGIS.Carto.IFeatureLayer pFeatureLayer = (ESRI.ArcGIS.Carto.IFeatureLayer)pLayer;
            pFeatureClass = pFeatureLayer.FeatureClass;
            if (pFeatureLayer == null)
            {
                MessageBox.Show("柱状图图层缺失！");
                this.Close();
                return;
            }
            if (pFeatureClass == null)
            {
                MessageBox.Show("柱状图图层缺失！");
                this.Close();
                return;
            }
            if (BID == "" || pGeometry==null)
                return;
            List<IGeometry> listgeo = new List<IGeometry>();
            IQueryFilter pFilter = new QueryFilterClass();
            pFilter.WhereClause = "BID='" + BID + "'";
            IFeatureCursor pCursor = pFeatureClass.Search(pFilter, false);
            IFeature pFeature = pCursor.NextFeature();
            while (pFeature != null)
            {
                listgeo.Add(pFeature.Shape);
                pFeature = pCursor.NextFeature();
            }
            pGeometry = setgeo(MyMapHelp.GetGeoFromGeos(listgeo));
            axMapControlZZT.Extent = pGeometry.Envelope;
            axMapControlZZT.MapScale = blc;
            axMapControlZZT.ActiveView.Refresh();
        }

        private void axMapControlZZT_SizeChanged(object sender, EventArgs e)
        {
            if (pGeometry == null) return;
            axMapControlZZT.Extent = pGeometry.Envelope;
            axMapControlZZT.MapScale = blc;
            axMapControlZZT.ActiveView.Refresh();
        }
        private IGeometry setgeo(IGeometry pgeo)
        {
            ITopologicalOperator pTopo = (ITopologicalOperator)pgeo;
            return pTopo.Buffer(0.5);
        }
    }
}
