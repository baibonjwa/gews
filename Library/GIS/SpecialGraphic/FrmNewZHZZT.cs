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
using ESRI.ArcGIS.Geometry;
using ESRI.ArcGIS.Carto;
using GIS.Common;
using ESRI.ArcGIS.Geodatabase;
using LibDatabase;
using LibEntity;

namespace GIS.SpecialGraphic
{
    public partial class FrmNewZHZZT : Form
    {
        FrmZhzzt frmZhzzt = null;
        bool isadd = true;
        public FrmNewZHZZT(FrmZhzzt mFrmZhzzt)
        {
            InitializeComponent();
            setcombox();
            //设置窗体属性
            LibCommon.FormDefaultPropertiesSetter.SetEnteringFormDefaultProperties(this, "新建柱状图");
            frmZhzzt = mFrmZhzzt;
        }
        string bid;
        public FrmNewZHZZT(FrmZhzzt mFrmZhzzt, string BID)
        {
            InitializeComponent();
            setcombox();
            //设置窗体属性
            LibCommon.FormDefaultPropertiesSetter.SetEnteringFormDefaultProperties(this, "修改柱状图");
            frmZhzzt = mFrmZhzzt;
            bid = BID;
            isadd = false;

            IQueryFilter pFilter = new QueryFilterClass();
            pFilter.WhereClause = "BID='" + BID + "' and strtype='1'";
            IFeatureCursor pCursor = mFrmZhzzt.pFeatureClass.Search(pFilter, false);
            IFeature pFeature = pCursor.NextFeature();
            if (pFeature != null)
            {
                string textstr = pFeature.get_Value(pFeature.Fields.FindField("textstr")).ToString();
                string bilici = pFeature.get_Value(pFeature.Fields.FindField("bilici")).ToString();
                txtname.Text = textstr;
                txtBlc.Text = bilici;
            }
            pFilter = new QueryFilterClass();
            pFilter.WhereClause = "BID='" + BID + "' and xuhaoR>0";
            int count = mFrmZhzzt.pFeatureClass.FeatureCount(pFilter);
            count = count / 6;
            if (count % 6 != 0)
                count += 1;
            if (count == 0)
                return;
            dgrdvZhzzt.RowCount = count;
            for (int i = 0; i < count; i++)
            {
                pCursor = MyMapHelp.FeatureSorting(mFrmZhzzt.pFeatureClass, "BID='" + BID + "' and xuhaoR=" + (i + 1) + "", "order by xuhaoC");
                pFeature = pCursor.NextFeature();
                while (pFeature != null)
                {
                    string textstr = "";
                    if (pFeature.get_Value(pFeature.Fields.FindField("textstr")) != null)
                        textstr = pFeature.get_Value(pFeature.Fields.FindField("textstr")).ToString();
                    string xuhaoC = pFeature.get_Value(pFeature.Fields.FindField("xuhaoC")).ToString();
                    if (xuhaoC.Equals("2"))
                    {
                        dgrdvZhzzt[0, i].Value = textstr;
                    }
                    if (xuhaoC.Equals("3"))
                    {
                        dgrdvZhzzt[1, i].Value = textstr;
                    }
                    if (xuhaoC.Equals("5"))
                    {
                        string zztype = pFeature.get_Value(pFeature.Fields.FindField("zztype")).ToString();
                        dgrdvZhzzt[2, i].Value = ZZCodeToStr(zztype);
                    }
                    if (xuhaoC.Equals("6"))
                    {
                        dgrdvZhzzt[3, i].Value = textstr;
                    }
                    pFeature = pCursor.NextFeature();
                }
            }
        }
        //绑定datagrid里的combox
        private void setcombox()
        {
            DropDownZZ.Items.Clear();
            DropDownZZ.Items.Add("煤");
            DropDownZZ.Items.Add("碳化灰岩");
            DropDownZZ.Items.Add("粉砂质泥岩");
            DropDownZZ.Items.Add("泥岩");
            DropDownZZ.Items.Add("泥质粉砂岩");
            //煤,碳化灰岩,粉砂质泥岩,泥岩,泥质粉砂岩
        }
        private string ZZCodeToStr(string code)
        {
            string str = "";
            switch (code)
            {
                case "1":
                    str = "煤";
                    break;
                case "2":
                    str = "碳化灰岩";
                    break;
                case "3":
                    str = "粉砂质泥岩";
                    break;
                case "4":
                    str = "泥岩";
                    break;
                case "5":
                    str = "泥质粉砂岩";
                    break;
            }
            return str;
        }
        private string ZZStrToCode(string str)
        {
            string code = "";
            switch (str)
            {
                case "煤":
                    code = "1";
                    break;
                case "碳化灰岩":
                    code = "2";
                    break;
                case "粉砂质泥岩":
                    code = "3";
                    break;
                case "泥岩":
                    code = "4";
                    break;
                case "泥质粉砂岩":
                    code = "5";
                    break;
            }
            return code;
        }
        /// <summary>
        /// 验证
        /// </summary>
        /// <returns></returns>
        private bool check()
        {
            //名称非空
            if (!Check.isEmpty(txtname, "柱状图名称"))
            {
                return false;
            }
            //名称特殊字符
            if (!Check.checkSpecialCharacters(txtname, "柱状图名称"))
            {
                return false;
            }
            //名称重复
            IQueryFilter pFilter = new QueryFilterClass();
            if (isadd == false)
                pFilter.WhereClause = "BID!='" + bid + "' and textstr='" + txtname.Text.Trim() + "' and strtype='1'";
            else
                pFilter.WhereClause = "textstr='" + txtname.Text.Trim() + "' and strtype='1'";
            int count = frmZhzzt.pFeatureClass.FeatureCount(pFilter);
            if (count > 0)
            {
                Alert.alert("相同名称的综合柱状图已经存在！");
                return false;
            }
            //比例尺非空
            if (!Check.isEmpty(txtBlc, "比例尺"))
            {
                return false;
            }
            //比例尺数字
            if (!Check.IsNumeric(txtBlc, "比例尺"))
            {
                return false;
            }
            if (dgrdvZhzzt.RowCount < 2)
            {
                Alert.alert("柱状图属性不能空！");
                return false;
            }
            //datagridview内部
            for (int i = 0; i < dgrdvZhzzt.RowCount - 1; i++)
            {
                //煤岩名称
                DataGridViewTextBoxCell cell = dgrdvZhzzt.Rows[i].Cells[0] as DataGridViewTextBoxCell;
                //非空
                if (cell.Value == null)
                {
                    Alert.alert("煤岩名称" + Const.MSG_NOT_NULL + Const.SIGN_EXCLAMATION_MARK);
                    return false;
                }

                //厚度
                cell = dgrdvZhzzt.Rows[i].Cells[1] as DataGridViewTextBoxCell;
                //非空
                if (cell.Value == null)
                {
                    Alert.alert("厚度" + Const.MSG_NOT_NULL + Const.SIGN_EXCLAMATION_MARK);
                    return false;
                }
                //数字
                if (!Validator.IsNumeric(cell.Value.ToString()))
                {
                    Alert.alert("厚度" + Const.MSG_MUST_NUMBER + Const.SIGN_EXCLAMATION_MARK);
                    return false;
                }

                //柱状非空
                if (dgrdvZhzzt.Rows[i].Cells[2].Value == null)
                {
                    Alert.alert("柱状" + Const.MSG_NOT_NULL + Const.SIGN_EXCLAMATION_MARK);
                    return false;
                }
            }
            //成功
            return true;
        }
        #region 保存
        private void btnSubmit_Click(object sender, EventArgs e)
        {
            //1煤2碳化灰岩3粉砂质泥岩4泥岩5泥质粉砂岩
            IWorkspaceEdit workspaceEdit = null;
            try
            {
                labelSC.Visible = true;
                Application.DoEvents();
                //去除无用空行
                for (int i = 0; i < dgrdvZhzzt.RowCount - 1; i++)
                {
                    if (this.dgrdvZhzzt.Rows[i].Cells[0].Value == null &&
                        this.dgrdvZhzzt.Rows[i].Cells[1].Value == null &&
                        this.dgrdvZhzzt.Rows[i].Cells[2].Value == null)
                    {
                        this.dgrdvZhzzt.Rows.RemoveAt(i);
                    }
                }
                //验证
                if (!check())
                {
                    this.DialogResult = DialogResult.None;
                    return;
                }
                this.DialogResult = DialogResult.OK;
                if (isadd)
                {
                    bid = IDGenerator.NewBindingID();
                }
                //实体赋值
                Histogram historam = new Histogram();
                historam.HistogramEntName = txtname.Text.Trim();
                historam.BLC = Convert.ToDouble(txtBlc.Text.Trim());
                historam.ID = bid;
                historam.listMY = new List<Historgramlist>();
                double height = 0;
                for (int i = 0; i < dgrdvZhzzt.RowCount - 1; i++)
                {
                    Historgramlist hisl = new Historgramlist();
                    hisl.BID = bid;
                    hisl.Index = (i + 1);
                    //煤岩名称
                    DataGridViewTextBoxCell cell = dgrdvZhzzt.Rows[i].Cells[0] as DataGridViewTextBoxCell;
                    hisl.MYName = cell.Value.ToString();
                    cell = dgrdvZhzzt.Rows[i].Cells[1] as DataGridViewTextBoxCell;
                    hisl.Height = Convert.ToDouble(cell.Value.ToString());
                    height += hisl.Height;
                    hisl.SHeight = height;
                    hisl.ZZType = ZZStrToCode(dgrdvZhzzt.Rows[i].Cells[2].Value.ToString());
                    cell = dgrdvZhzzt.Rows[i].Cells[3] as DataGridViewTextBoxCell;
                    if (cell.Value != null)
                    {
                        hisl.Describe = cell.Value.ToString();
                    }
                    historam.listMY.Add(hisl);
                }
                if (!isadd)
                {
                    DataEditCommon.DeleteFeatureByWhereClause(frmZhzzt.pFeatureClass, "BID='" + bid + "'");
                }

                progressBar1.Maximum = historam.listMY.Count * 6 + 9;
                progressBar1.Value = 0;


                IFeatureClass pFeatureClass = frmZhzzt.pFeatureClass;
                progressBar1.Value += 1;
                Application.DoEvents();
                IDataset dataset = (IDataset)pFeatureClass;
                IWorkspace workspace = dataset.Workspace;
                workspaceEdit = workspace as IWorkspaceEdit;

                workspaceEdit.StartEditing(true);
                workspaceEdit.StartEditOperation();


                ISegmentCollection pSegmentCollection = new PolygonClass();

                double xbasic = 100;
                double xmin = xbasic, ymin = 99.5, xmax = 110, ymax = 100;

                List<IGeometry> listgeo = new List<IGeometry>();
                IEnvelope pEnvelope = new EnvelopeClass();
                pEnvelope.PutCoords(xmin, ymin, xmax, ymax);
                List<ziduan> listzd = new List<ziduan>();
                listzd.Add(new ziduan("textstr", historam.HistogramEntName));
                listzd.Add(new ziduan("zztype", "6"));
                listzd.Add(new ziduan("strtype", "1"));
                listzd.Add(new ziduan("bilici", historam.BLC.ToString()));
                listzd.Add(new ziduan("BID", historam.ID));
                pSegmentCollection.SetRectangle(pEnvelope);
                DataEditCommon.CreateFeatureNoEditor(pFeatureClass, (IGeometry)pSegmentCollection, listzd);
                listgeo.Add(pSegmentCollection as IGeometry);
                progressBar1.Value += 1;
                Application.DoEvents();

                pEnvelope = new EnvelopeClass();
                ymax = ymin;
                ymin -= 0.3;
                pEnvelope.PutCoords(xmin, ymin, xmax, ymax);
                listzd = new List<ziduan>();
                listzd.Add(new ziduan("textstr", "比例 1:" + historam.BLC.ToString() + "  单位：米"));
                listzd.Add(new ziduan("zztype", "6"));
                listzd.Add(new ziduan("strtype", "2"));
                listzd.Add(new ziduan("bilici", historam.BLC.ToString()));
                listzd.Add(new ziduan("BID", historam.ID));
                pSegmentCollection = new PolygonClass();
                pSegmentCollection.SetRectangle(pEnvelope);
                DataEditCommon.CreateFeatureNoEditor(pFeatureClass, (IGeometry)pSegmentCollection, listzd);
                listgeo.Add(pSegmentCollection as IGeometry);
                progressBar1.Value += 1;
                Application.DoEvents();

                pEnvelope = new EnvelopeClass();
                ymax = ymin;
                ymin -= 0.3; xmax = xmin + 0.6;
                pEnvelope.PutCoords(xmin, ymin, xmax, ymax);
                listzd = new List<ziduan>();
                listzd.Add(new ziduan("textstr", "序号"));
                listzd.Add(new ziduan("zztype", "0"));
                listzd.Add(new ziduan("strtype", "0"));
                listzd.Add(new ziduan("BID", historam.ID));
                pSegmentCollection = new PolygonClass();
                pSegmentCollection.SetRectangle(pEnvelope);
                DataEditCommon.CreateFeatureNoEditor(pFeatureClass, (IGeometry)pSegmentCollection, listzd);
                listgeo.Add(pSegmentCollection as IGeometry);
                progressBar1.Value += 1;
                Application.DoEvents();

                pEnvelope = new EnvelopeClass();
                xmin = xmax; xmax += 1.8;
                pEnvelope.PutCoords(xmin, ymin, xmax, ymax);
                listzd = new List<ziduan>();
                listzd.Add(new ziduan("textstr", "煤 岩 名 称"));
                listzd.Add(new ziduan("zztype", "0"));
                listzd.Add(new ziduan("strtype", "0"));
                listzd.Add(new ziduan("BID", historam.ID));
                pSegmentCollection = new PolygonClass();
                pSegmentCollection.SetRectangle(pEnvelope);
                DataEditCommon.CreateFeatureNoEditor(pFeatureClass, (IGeometry)pSegmentCollection, listzd);
                listgeo.Add(pSegmentCollection as IGeometry);
                progressBar1.Value += 1;
                Application.DoEvents();

                pEnvelope = new EnvelopeClass();
                xmin = xmax; xmax += 0.8;
                pEnvelope.PutCoords(xmin, ymin, xmax, ymax);
                listzd = new List<ziduan>();
                listzd.Add(new ziduan("textstr", "真厚"));
                listzd.Add(new ziduan("zztype", "0"));
                listzd.Add(new ziduan("strtype", "0"));
                listzd.Add(new ziduan("BID", historam.ID));
                pSegmentCollection = new PolygonClass();
                pSegmentCollection.SetRectangle(pEnvelope);
                DataEditCommon.CreateFeatureNoEditor(pFeatureClass, (IGeometry)pSegmentCollection, listzd);
                listgeo.Add(pSegmentCollection as IGeometry);
                progressBar1.Value += 1;
                Application.DoEvents();

                pEnvelope = new EnvelopeClass();
                xmin = xmax; xmax += 1;
                pEnvelope.PutCoords(xmin, ymin, xmax, ymax);
                listzd = new List<ziduan>();
                listzd.Add(new ziduan("textstr", "累厚"));
                listzd.Add(new ziduan("zztype", "0"));
                listzd.Add(new ziduan("strtype", "0"));
                listzd.Add(new ziduan("BID", historam.ID));
                pSegmentCollection = new PolygonClass();
                pSegmentCollection.SetRectangle(pEnvelope);
                DataEditCommon.CreateFeatureNoEditor(pFeatureClass, (IGeometry)pSegmentCollection, listzd);
                listgeo.Add(pSegmentCollection as IGeometry);
                progressBar1.Value += 1;
                Application.DoEvents();

                pEnvelope = new EnvelopeClass();
                xmin = xmax; xmax += 1.2;
                pEnvelope.PutCoords(xmin, ymin, xmax, ymax);
                listzd = new List<ziduan>();
                listzd.Add(new ziduan("textstr", "柱  状"));
                listzd.Add(new ziduan("zztype", "0"));
                listzd.Add(new ziduan("strtype", "0"));
                listzd.Add(new ziduan("BID", historam.ID));
                pSegmentCollection = new PolygonClass();
                pSegmentCollection.SetRectangle(pEnvelope);
                DataEditCommon.CreateFeatureNoEditor(pFeatureClass, (IGeometry)pSegmentCollection, listzd);
                listgeo.Add(pSegmentCollection as IGeometry);
                progressBar1.Value += 1;
                Application.DoEvents();

                pEnvelope = new EnvelopeClass();
                xmin = xmax; xmax += 4.6;
                pEnvelope.PutCoords(xmin, ymin, xmax, ymax);
                listzd = new List<ziduan>();
                listzd.Add(new ziduan("textstr", "岩 性 描 述"));
                listzd.Add(new ziduan("zztype", "0"));
                listzd.Add(new ziduan("strtype", "0"));
                listzd.Add(new ziduan("BID", historam.ID));
                pSegmentCollection = new PolygonClass();
                pSegmentCollection.SetRectangle(pEnvelope);
                DataEditCommon.CreateFeatureNoEditor(pFeatureClass, (IGeometry)pSegmentCollection, listzd);
                listgeo.Add(pSegmentCollection as IGeometry);
                progressBar1.Value += 1;
                Application.DoEvents();


                List<Historgramlist> list = historam.ListMY;
                for (int i = 0; i < list.Count; i++)
                {
                    xmin = xbasic;

                    double m_height = list[i].Height;
                    if (m_height < 0.3)
                        m_height = 0.3;
                    pEnvelope = new EnvelopeClass();
                    ymax = ymin;
                    ymin -= m_height; xmax = xmin + 0.6;
                    pEnvelope.PutCoords(xmin, ymin, xmax, ymax);
                    listzd = new List<ziduan>();
                    listzd.Add(new ziduan("textstr", list[i].Index.ToString()));
                    listzd.Add(new ziduan("xuhaoR", list[i].Index.ToString()));
                    listzd.Add(new ziduan("xuhaoC", "1"));
                    listzd.Add(new ziduan("zztype", "0"));
                    listzd.Add(new ziduan("strtype", "0"));
                    listzd.Add(new ziduan("BID", historam.ID));
                    pSegmentCollection = new PolygonClass();
                    pSegmentCollection.SetRectangle(pEnvelope);
                    DataEditCommon.CreateFeatureNoEditor(pFeatureClass, (IGeometry)pSegmentCollection, listzd);
                    listgeo.Add(pSegmentCollection as IGeometry);
                    progressBar1.Value += 1;
                    Application.DoEvents();

                    pEnvelope = new EnvelopeClass();
                    xmin = xmax; xmax += 1.8;
                    pEnvelope.PutCoords(xmin, ymin, xmax, ymax);
                    listzd = new List<ziduan>();
                    listzd.Add(new ziduan("textstr", list[i].MYName));
                    listzd.Add(new ziduan("zztype", "0"));
                    listzd.Add(new ziduan("xuhaoR", list[i].Index.ToString()));
                    listzd.Add(new ziduan("xuhaoC", "2"));
                    listzd.Add(new ziduan("strtype", "0"));
                    listzd.Add(new ziduan("BID", historam.ID));
                    pSegmentCollection = new PolygonClass();
                    pSegmentCollection.SetRectangle(pEnvelope);
                    DataEditCommon.CreateFeatureNoEditor(pFeatureClass, (IGeometry)pSegmentCollection, listzd);
                    listgeo.Add(pSegmentCollection as IGeometry);
                    progressBar1.Value += 1;
                    Application.DoEvents();

                    pEnvelope = new EnvelopeClass();
                    xmin = xmax; xmax += 0.8;
                    pEnvelope.PutCoords(xmin, ymin, xmax, ymax);
                    listzd = new List<ziduan>();
                    listzd.Add(new ziduan("textstr", list[i].Height.ToString()));
                    listzd.Add(new ziduan("zztype", "0"));
                    listzd.Add(new ziduan("xuhaoR", list[i].Index.ToString()));
                    listzd.Add(new ziduan("xuhaoC", "3"));
                    listzd.Add(new ziduan("strtype", "0"));
                    listzd.Add(new ziduan("BID", historam.ID));
                    pSegmentCollection = new PolygonClass();
                    pSegmentCollection.SetRectangle(pEnvelope);
                    DataEditCommon.CreateFeatureNoEditor(pFeatureClass, (IGeometry)pSegmentCollection, listzd);
                    listgeo.Add(pSegmentCollection as IGeometry);
                    progressBar1.Value += 1;
                    Application.DoEvents();

                    pEnvelope = new EnvelopeClass();
                    xmin = xmax; xmax += 1;
                    pEnvelope.PutCoords(xmin, ymin, xmax, ymax);
                    listzd = new List<ziduan>();
                    listzd.Add(new ziduan("textstr", Math.Round(list[i].SHeight, 1).ToString()));
                    listzd.Add(new ziduan("zztype", "0"));
                    listzd.Add(new ziduan("xuhaoR", list[i].Index.ToString()));
                    listzd.Add(new ziduan("xuhaoC", "4"));
                    listzd.Add(new ziduan("strtype", "0"));
                    listzd.Add(new ziduan("BID", historam.ID));
                    pSegmentCollection = new PolygonClass();
                    pSegmentCollection.SetRectangle(pEnvelope);
                    DataEditCommon.CreateFeatureNoEditor(pFeatureClass, (IGeometry)pSegmentCollection, listzd);
                    listgeo.Add(pSegmentCollection as IGeometry);
                    progressBar1.Value += 1;
                    Application.DoEvents();

                    pEnvelope = new EnvelopeClass();
                    xmin = xmax; xmax += 1.2;
                    pEnvelope.PutCoords(xmin, ymin, xmax, ymax);
                    listzd = new List<ziduan>();
                    listzd.Add(new ziduan("textstr", ""));
                    listzd.Add(new ziduan("zztype", list[i].ZZType));
                    listzd.Add(new ziduan("xuhaoR", list[i].Index.ToString()));
                    listzd.Add(new ziduan("xuhaoC", "5"));
                    listzd.Add(new ziduan("strtype", "0"));
                    listzd.Add(new ziduan("BID", historam.ID));
                    pSegmentCollection = new PolygonClass();
                    pSegmentCollection.SetRectangle(pEnvelope);
                    DataEditCommon.CreateFeatureNoEditor(pFeatureClass, (IGeometry)pSegmentCollection, listzd);
                    listgeo.Add(pSegmentCollection as IGeometry);
                    progressBar1.Value += 1;
                    Application.DoEvents();

                    pEnvelope = new EnvelopeClass();
                    xmin = xmax; xmax += 4.6;
                    pEnvelope.PutCoords(xmin, ymin, xmax, ymax);
                    listzd = new List<ziduan>();
                    string strms = list[i].Describe;
                    string miaoshu = "";
                    if (strms != null && strms.Length > 0)
                    {
                        ASCIIEncoding ascii = new ASCIIEncoding();
                        int temLen = 0;
                        byte[] s = ascii.GetBytes(strms);

                        for (int j = 0; j < s.Length; j++)
                        {
                            if ((int)s[j] == 63)
                            {
                                temLen += 2;
                            }
                            else
                            {
                                temLen += 1;
                            }
                            miaoshu += strms[j];
                            if (temLen % 38 == 0 || (temLen + 1) % 38 == 0)
                            {
                                if (miaoshu.LastIndexOf('|') != miaoshu.Length - 1)
                                    miaoshu += "|";
                            }
                        }
                    }

                    listzd.Add(new ziduan("textstr", miaoshu));
                    listzd.Add(new ziduan("zztype", "0"));
                    listzd.Add(new ziduan("xuhaoR", list[i].Index.ToString()));
                    listzd.Add(new ziduan("xuhaoC", "6"));
                    listzd.Add(new ziduan("strtype", "3"));
                    listzd.Add(new ziduan("BID", historam.ID));
                    pSegmentCollection = new PolygonClass();
                    pSegmentCollection.SetRectangle(pEnvelope);
                    DataEditCommon.CreateFeatureNoEditor(pFeatureClass, (IGeometry)pSegmentCollection, listzd);
                    listgeo.Add(pSegmentCollection as IGeometry);
                    progressBar1.Value += 1;
                    Application.DoEvents();
                }
                workspaceEdit.StopEditOperation();
                workspaceEdit.StopEditing(true);

                frmZhzzt.pGeometry = MyMapHelp.GetGeoFromGeos(listgeo);
                frmZhzzt.BID = historam.ID;
                frmZhzzt.blc = historam.BLC;

                DialogResult = DialogResult.OK;
                this.Close();
                if (!isadd)
                    frmZhzzt.refresh();
            }
            catch (Exception ex)
            {
                labelSC.Visible = false;
                MessageBox.Show(ex.Message);
                if (workspaceEdit != null)
                {
                    workspaceEdit.AbortEditOperation();
                    workspaceEdit.StopEditing(false);
                }
            }
        }
        #endregion
        #region datagrid事件
        private void dgrdvZhzzt_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex != dgrdvZhzzt.Rows.Count - 1 && e.ColumnIndex == 4 && Alert.confirm(Const.DEL_CONFIRM_MSG))
            {
                if (e.ColumnIndex == 4)
                {
                    dgrdvZhzzt.Rows.RemoveAt(e.RowIndex);
                }
            }
        }
        /// <summary>
        /// 显示行号
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void dgrdvZhzzt_RowPostPaint(object sender, DataGridViewRowPostPaintEventArgs e)
        {
            System.Drawing.Rectangle rectangle = new System.Drawing.Rectangle(e.RowBounds.Location.X,
                e.RowBounds.Location.Y, dgrdvZhzzt.RowHeadersWidth - 4, e.RowBounds.Height);

            TextRenderer.DrawText(e.Graphics, (e.RowIndex + 1).ToString(),
                dgrdvZhzzt.RowHeadersDefaultCellStyle.Font, rectangle,
                dgrdvZhzzt.RowHeadersDefaultCellStyle.ForeColor,
                TextFormatFlags.VerticalCenter | TextFormatFlags.Right);
        }

        private void dgrdvZhzzt_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            //if (e.ColumnIndex == 0)
            //{
            //    // 岩性
            //    DataGridViewComboBoxCell cell0 = this.dgrdvZhzzt.Rows[this.dgrdvZhzzt.CurrentRow.Index].Cells[0] as DataGridViewComboBoxCell;
            //    // 煤层名称
            //    DataGridViewTextBoxCell cell3 = dgrdvZhzzt.Rows[this.dgrdvZhzzt.CurrentRow.Index].Cells[3] as DataGridViewTextBoxCell;

            //    DataSet ds = LithologyBLL.selectCoalInfo();
            //    if (ds.Tables[0].Rows.Count > 0)
            //    {
            //        // 当岩性名称选择为“煤”时，煤层名称可编辑，否则煤层名称设置为不可编辑，并清空
            //        if (Convert.ToString(cell0.Value) == ds.Tables[0].Rows[0][LithologyDbConstNames.LITHOLOGY_NAME].ToString())
            //        {
            //            cell3.ReadOnly = false;
            //        }
            //        else
            //        {
            //            cell3.Value = "";
            //            cell3.ReadOnly = true;
            //        }
            //    }
            //}
        }

        private void dgrdvZhzzt_RowValidated(object sender, DataGridViewCellEventArgs e)
        {
            //for (int i = 0; i < this.dgrdvZhzzt.RowCount - 1; i++)
            //{
            //    // 岩性
            //    DataGridViewComboBoxCell cell0 = this.dgrdvZhzzt.Rows[i].Cells[0] as DataGridViewComboBoxCell;
            //    // 煤层名称
            //    DataGridViewTextBoxCell cell3 = dgrdvZhzzt.Rows[i].Cells[3] as DataGridViewTextBoxCell;

            //    DataSet ds = LithologyBLL.selectCoalInfo();
            //    if (ds.Tables[0].Rows.Count > 0)
            //    {
            //        // 当岩性名称选择为“煤”时，煤层名称可编辑，否则煤层名称设置为不可编辑，并清空
            //        if (Convert.ToString(cell0.Value) == ds.Tables[0].Rows[0][LithologyDbConstNames.LITHOLOGY_NAME].ToString())
            //        {
            //            cell3.ReadOnly = false;
            //        }
            //        else
            //        {
            //            cell3.Value = "";
            //            cell3.ReadOnly = true;
            //        }
            //    }
            //}
        }
        //右键菜单
        private void dgrdvZhzzt_CellMouseDown(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                if (e.RowIndex >= 0)
                {
                    //若行已是选中状态就不再进行设置
                    if (this.dgrdvZhzzt.Rows[e.RowIndex].Selected == false)
                    {
                        this.dgrdvZhzzt.ClearSelection();
                        this.dgrdvZhzzt.Rows[e.RowIndex].Selected = true;
                        this.dgrdvZhzzt.CurrentCell = this.dgrdvZhzzt.Rows[e.RowIndex].Cells[0];
                    }
                }
            }
        }
        #endregion
        #region 菜单事件
        /// <summary>
        /// 上移
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void 上移ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int iNowIndex = this.dgrdvZhzzt.CurrentRow.Index;

            if (iNowIndex == 0)
            {
                Alert.alert("无法上移");
                return;
            }

            object[] objArrRowData = new object[4];

            int index = -1;
            int n = -1;
            objArrRowData[++n] = this.dgrdvZhzzt.Rows[iNowIndex].Cells[++index].Value;
            objArrRowData[++n] = this.dgrdvZhzzt.Rows[iNowIndex].Cells[++index].Value;
            objArrRowData[++n] = this.dgrdvZhzzt.Rows[iNowIndex].Cells[++index].Value;
            objArrRowData[++n] = this.dgrdvZhzzt.Rows[iNowIndex].Cells[++index].Value;

            index = -1;
            this.dgrdvZhzzt.Rows[iNowIndex].Cells[++index].Value = this.dgrdvZhzzt.Rows[iNowIndex - 1].Cells[index].Value;
            this.dgrdvZhzzt.Rows[iNowIndex].Cells[++index].Value = this.dgrdvZhzzt.Rows[iNowIndex - 1].Cells[index].Value;
            this.dgrdvZhzzt.Rows[iNowIndex].Cells[++index].Value = this.dgrdvZhzzt.Rows[iNowIndex - 1].Cells[index].Value;
            this.dgrdvZhzzt.Rows[iNowIndex].Cells[++index].Value = this.dgrdvZhzzt.Rows[iNowIndex - 1].Cells[index].Value;

            index = -1;
            n = -1;
            this.dgrdvZhzzt.Rows[iNowIndex - 1].Cells[++index].Value = objArrRowData[++n];
            this.dgrdvZhzzt.Rows[iNowIndex - 1].Cells[++index].Value = objArrRowData[++n];
            this.dgrdvZhzzt.Rows[iNowIndex - 1].Cells[++index].Value = objArrRowData[++n];
            this.dgrdvZhzzt.Rows[iNowIndex - 1].Cells[++index].Value = objArrRowData[++n];

            this.dgrdvZhzzt.CurrentCell = this.dgrdvZhzzt.Rows[iNowIndex - 1].Cells[0];//设定当前行
            this.dgrdvZhzzt.Rows[iNowIndex - 1].Selected = true;

        }

        /// <summary>
        /// 下移
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void 下移ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int iNowIndex = this.dgrdvZhzzt.CurrentRow.Index;

            if (iNowIndex == this.dgrdvZhzzt.Rows.Count - 2 || iNowIndex == this.dgrdvZhzzt.Rows.Count - 1)
            {
                Alert.alert("无法下移");
                return;
            }

            object[] objArrRowData = new object[4];

            int index = -1;
            int n = -1;
            objArrRowData[++n] = this.dgrdvZhzzt.Rows[iNowIndex].Cells[++index].Value;
            objArrRowData[++n] = this.dgrdvZhzzt.Rows[iNowIndex].Cells[++index].Value;
            objArrRowData[++n] = this.dgrdvZhzzt.Rows[iNowIndex].Cells[++index].Value;
            objArrRowData[++n] = this.dgrdvZhzzt.Rows[iNowIndex].Cells[++index].Value;

            index = -1;
            this.dgrdvZhzzt.Rows[iNowIndex].Cells[++index].Value = this.dgrdvZhzzt.Rows[iNowIndex + 1].Cells[index].Value;
            this.dgrdvZhzzt.Rows[iNowIndex].Cells[++index].Value = this.dgrdvZhzzt.Rows[iNowIndex + 1].Cells[index].Value;
            this.dgrdvZhzzt.Rows[iNowIndex].Cells[++index].Value = this.dgrdvZhzzt.Rows[iNowIndex + 1].Cells[index].Value;
            this.dgrdvZhzzt.Rows[iNowIndex].Cells[++index].Value = this.dgrdvZhzzt.Rows[iNowIndex + 1].Cells[index].Value;

            index = -1;
            n = -1;
            this.dgrdvZhzzt.Rows[iNowIndex + 1].Cells[++index].Value = objArrRowData[++n];
            this.dgrdvZhzzt.Rows[iNowIndex + 1].Cells[++index].Value = objArrRowData[++n];
            this.dgrdvZhzzt.Rows[iNowIndex + 1].Cells[++index].Value = objArrRowData[++n];
            this.dgrdvZhzzt.Rows[iNowIndex + 1].Cells[++index].Value = objArrRowData[++n];

            this.dgrdvZhzzt.CurrentCell = this.dgrdvZhzzt.Rows[iNowIndex + 1].Cells[0];//设定当前行
            this.dgrdvZhzzt.Rows[iNowIndex + 1].Selected = true;
        }

        object[] _objArrRowData = new object[4];
        /// <summary>
        /// 插入
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void 插入ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int iNowIndex = this.dgrdvZhzzt.CurrentRow.Index;

            DataGridViewRow newRow = new DataGridViewRow();//新建行
            this.dgrdvZhzzt.Rows.Insert(iNowIndex, newRow);//当前行的上面插入新行
        }
        /// <summary>
        /// 复制
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void 复制ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int iNowIndex = this.dgrdvZhzzt.CurrentRow.Index;

            object[] objArrRowData = new object[4];

            int index = -1;
            int n = -1;
            objArrRowData[++n] = this.dgrdvZhzzt.Rows[iNowIndex].Cells[++index].Value;
            objArrRowData[++n] = this.dgrdvZhzzt.Rows[iNowIndex].Cells[++index].Value;
            objArrRowData[++n] = this.dgrdvZhzzt.Rows[iNowIndex].Cells[++index].Value;
            objArrRowData[++n] = this.dgrdvZhzzt.Rows[iNowIndex].Cells[++index].Value;

            _objArrRowData = objArrRowData;

            //MessageBox.Show("复制成功！");

            this.contextMenuStrip1.Items["粘贴ToolStripMenuItem"].Visible = true;
        }

        /// <summary>
        /// 粘贴
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void 粘贴ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int iNowIndex = this.dgrdvZhzzt.CurrentRow.Index;

            if (iNowIndex == this.dgrdvZhzzt.Rows.Count - 1)
            {
                this.dgrdvZhzzt.Rows.Add();
            }

            int index = -1;
            int n = -1;
            this.dgrdvZhzzt.Rows[iNowIndex].Cells[++index].Value = _objArrRowData[++n];
            this.dgrdvZhzzt.Rows[iNowIndex].Cells[++index].Value = _objArrRowData[++n];
            this.dgrdvZhzzt.Rows[iNowIndex].Cells[++index].Value = _objArrRowData[++n];
            this.dgrdvZhzzt.Rows[iNowIndex].Cells[++index].Value = _objArrRowData[++n];

            this.contextMenuStrip1.Items["粘贴ToolStripMenuItem"].Visible = false;
        }

        /// <summary>
        /// 剪切
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void 剪切ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int iNowIndex = this.dgrdvZhzzt.CurrentRow.Index;

            object[] objArrRowData = new object[4];

            int index = -1;
            int n = -1;
            objArrRowData[++n] = this.dgrdvZhzzt.Rows[iNowIndex].Cells[++index].Value;
            objArrRowData[++n] = this.dgrdvZhzzt.Rows[iNowIndex].Cells[++index].Value;
            objArrRowData[++n] = this.dgrdvZhzzt.Rows[iNowIndex].Cells[++index].Value;
            objArrRowData[++n] = this.dgrdvZhzzt.Rows[iNowIndex].Cells[++index].Value;

            _objArrRowData = objArrRowData;

            this.dgrdvZhzzt.Rows.Remove(this.dgrdvZhzzt.CurrentRow);

            //MessageBox.Show("剪切成功！");

            this.contextMenuStrip1.Items["粘贴ToolStripMenuItem"].Visible = true;
        }
        #endregion

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
