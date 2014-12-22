using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using ESRI.ArcGIS.Geodatabase;
using LibCommon;
using LibEntity;

namespace GIS.SpecialGraphic
{
    public partial class FrmManageZhzzt : Form
    {
        FrmZhzzt frmzhzzt;
        public FrmManageZhzzt(FrmZhzzt frm)
        {
            InitializeComponent();
            frmzhzzt = frm;
        }

        private void FrmManageZhzzt_Load(object sender, EventArgs e)
        {
            dgrdvZhzzt.AutoGenerateColumns = false;
            binddata();
        }
        private void binddata()
        {
            IQueryFilter pFilter = new QueryFilterClass();
            pFilter.WhereClause = "strtype ='1'";
            IFeatureCursor pCursor = frmzhzzt.pFeatureClass.Search(pFilter, false);
            IFeature pFeature = pCursor.NextFeature();
            List<Histogram> list = new List<Histogram>();
            while (pFeature != null)
            {
                Histogram his = new Histogram();
                string id = pFeature.get_Value(pFeature.Fields.FindField("BID")).ToString();
                string blc = pFeature.get_Value(pFeature.Fields.FindField("bilici")).ToString();
                string textstr = pFeature.get_Value(pFeature.Fields.FindField("textstr")).ToString();
                his.BLC = Convert.ToDouble(blc);
                his.ID = id;
                his.HistogramEntName = textstr;
                list.Add(his);
                pFeature = pCursor.NextFeature();
            }
            dgrdvZhzzt.DataSource = list;
        }
        //全选/全不选
        private void checkBoxAll_CheckedChanged(object sender, EventArgs e)
        {
            for (int i = 0; i < dgrdvZhzzt.Rows.Count; i++)
            {
                dgrdvZhzzt.Rows[i].Cells[0].Value = checkBoxAll.Checked;
            }
        }
        //删除选中项
        private void button1_Click(object sender, EventArgs e)
        {
            string bid = "";
            for (int i = 0; i < dgrdvZhzzt.Rows.Count; i++)
            {
                if (dgrdvZhzzt.Rows[i].Cells[0].Value.ToString().ToLower() == "true")
                {
                    if (bid == "")
                        bid = "BID='" + dgrdvZhzzt.Rows[i].Cells[2].Value.ToString() + "'";
                    else
                        bid += " or BID='" + dgrdvZhzzt.Rows[i].Cells[2].Value.ToString() + "'";
                }
            }
            if (bid != "")
            {
                if (GIS.Common.DataEditCommon.DeleteFeatureByWhereClause(frmzhzzt.pFeatureClass, bid))
                    binddata();
            }
        }

        private void dgrdvZhzzt_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == 4 && Alert.confirm(Const.DEL_CONFIRM_MSG))
            {
                //删除
                if (GIS.Common.DataEditCommon.DeleteFeatureByWhereClause(frmzhzzt.pFeatureClass, "BID='" + dgrdvZhzzt.Rows[e.RowIndex].Cells[2].Value.ToString() + "'"))
                    binddata();
            }
            if (e.ColumnIndex == 5)
            {
                //打开
                string bid = dgrdvZhzzt.Rows[e.RowIndex].Cells[2].Value.ToString();
                string bilichi = dgrdvZhzzt.Rows[e.RowIndex].Cells[3].Value.ToString();
                List<ESRI.ArcGIS.Geometry.IGeometry> listgeo = new List<ESRI.ArcGIS.Geometry.IGeometry>();
                IQueryFilter pFilter = new QueryFilterClass();
                pFilter.WhereClause = "BID='" + bid + "'";
                IFeatureCursor pCursor = frmzhzzt.pFeatureClass.Search(pFilter, false);
                IFeature pFeature = pCursor.NextFeature();
                while (pFeature != null)
                {
                    listgeo.Add(pFeature.Shape);
                    pFeature = pCursor.NextFeature();
                }
                frmzhzzt.pGeometry = MyMapHelp.GetGeoFromGeos(listgeo);
                frmzhzzt.BID = bid;
                frmzhzzt.blc = Convert.ToDouble(bilichi);

                DialogResult = DialogResult.OK;
                this.Close();
            }
            if (e.ColumnIndex == 6)
            {
                //修改
                this.Close();
                string bid = dgrdvZhzzt.Rows[e.RowIndex].Cells[2].Value.ToString();
                FrmNewZHZZT frm = new FrmNewZHZZT(frmzhzzt, bid);
                frm.Show(frmzhzzt);
            }
            if (e.ColumnIndex == 0)
            {
                if (dgrdvZhzzt.Rows[e.RowIndex].Cells[e.ColumnIndex].Value.ToString().ToLower() == "true")
                    dgrdvZhzzt.Rows[e.RowIndex].Cells[e.ColumnIndex].Value = false;
                else
                    dgrdvZhzzt.Rows[e.RowIndex].Cells[e.ColumnIndex].Value = true;
            }
        }

        private void dgrdvZhzzt_RowsAdded(object sender, DataGridViewRowsAddedEventArgs e)
        {
            if (dgrdvZhzzt.Rows[e.RowIndex].Cells[0].Value == null)
                dgrdvZhzzt.Rows[e.RowIndex].Cells[0].Value = false;
        }

    }
}
