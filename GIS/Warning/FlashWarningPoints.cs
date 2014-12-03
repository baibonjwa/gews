using System;
using System.Drawing;
using System.Runtime.InteropServices;
using ESRI.ArcGIS.ADF.BaseClasses;
using ESRI.ArcGIS.ADF.CATIDs;
using System.Data;
using ESRI.ArcGIS.Controls;
using System.Timers;
using ESRI.ArcGIS.Display;
using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.Geometry;
using ESRI.ArcGIS.Geodatabase;
using System.Windows.Forms;
using System.Collections.Generic;
using LibEntity;
using LibDatabase;

namespace GIS.Warning
{
    /// <summary>
    /// Summary description for FlashWarningPoints.
    /// </summary>
    [Guid("92a160ec-af14-4f2a-ab02-889af47bd178")]
    [ClassInterface(ClassInterfaceType.None)]
    [ProgId("GIS.Warning.FlashWarningPoints")]
    public sealed class FlashWarningPoints : BaseCommand
    {
        private IHookHelper m_hookHelper = null;
        private System.Timers.Timer m_updateTimer = null;           //数据库查询计时器
        private System.Windows.Forms.Timer m_flashTimer = null;     //点闪烁计时器
        private int m_nFlashInterval = 6000;                        //点闪烁间隔时间(毫秒)

        private DataTable m_table = null;
        private const int m_nNumOfItems = 10;
        private IMapControl3 m_pMapControl;

        private ESRI.ArcGIS.Display.ISymbol _defaultFlashSymbol;    //默认闪烁符号

        private IPointCollection m_pFlashPoints = null;
        private int _count = 0;
        IGraphicsContainer graphCon;
        /// <summary>
        /// 预警结果字段
        /// </summary>
        private const string m_WR = "WARNINGRES";

        public FlashWarningPoints()
        {
            base.m_category = "warning"; //localizable text
            base.m_caption = "预警点闪烁";  //localizable text
            base.m_message = "预警点闪烁";  //localizable text 
            base.m_toolTip = "预警点闪烁";  //localizable text 
            base.m_name = "FlashWarningPoints";   //unique id, non-localizable (e.g. "MyCategory_MyCommand")

            try
            {
                base.m_bitmap = Properties.Resources.AddSpecialPoint;// FlashWarningPoint;
            }
            catch (Exception ex)
            {
                System.Diagnostics.Trace.WriteLine(ex.Message, "Invalid Bitmap");
            }
        }

        /// <summary>
        /// 设置或获取要素闪烁的状态
        /// </summary>
        public bool FlashStatus
        {
            set
            {
                if (m_flashTimer != null)
                    m_flashTimer.Enabled = value;

                if (m_updateTimer != null)
                    m_updateTimer.Enabled = m_flashTimer.Enabled;
                if (m_flashTimer.Enabled == false)
                {
                    graphCon.DeleteAllElements();
                    m_hookHelper.ActiveView.PartialRefresh(esriViewDrawPhase.esriViewGraphics, null, null);
                }
            }
            get
            {
                if (m_flashTimer != null)
                    return m_flashTimer.Enabled;
                else
                    return false;
            }
        }

        #region Overridden Class Methods

        /// <summary>
        /// Occurs when this command is created
        /// </summary>
        /// <param name="hook">Instance of the application</param>
        public override void OnCreate(object hook)
        {
            try
            {
                m_hookHelper = new HookHelperClass();
                m_hookHelper.Hook = hook;
                graphCon = (IGraphicsContainer)m_hookHelper.ActiveView;
                if (m_hookHelper.Hook is IToolbarControl)
                {
                    IToolbarControl toolbarControl = m_hookHelper.Hook as IToolbarControl;
                    m_pMapControl = toolbarControl.Buddy as IMapControl3;
                }
                else if (m_hookHelper.Hook is IMapControl3)
                {
                    m_pMapControl = m_hookHelper.Hook as IMapControl3;
                }
            }
            catch
            {
                m_hookHelper = null;
            }
        }

        /// <summary>
        /// Occurs when this command is clicked
        /// </summary>
        public override void OnClick()
        {
            //if (m_flashTimer != null)
            //{
            //    m_flashTimer.Enabled = !m_flashTimer.Enabled;

            //    if (m_updateTimer != null)
            //        m_updateTimer.Enabled = m_flashTimer.Enabled;
            //}
        }

        #endregion

        #region 实时读取预警结果表 1.读取预警点信息，生成预警点图层并添加到MapControl及数据库中；

        //点闪烁计时器的间隔时间
        public int FlashInterver
        {
            set
            {
                m_nFlashInterval = value;
                if (m_flashTimer != null)
                    m_flashTimer.Interval = m_nFlashInterval;
            }
            get
            {
                return m_nFlashInterval;
            }
        }

        public void UpdateEarlyWarningPoint()
        {
            m_table = new DataTable("rows");
            m_table.Columns.Add("OID", typeof(int));	//0
            m_table.Columns.Add("X", typeof(double));   //1
            m_table.Columns.Add("Y", typeof(double));   //2
            m_table.Columns.Add("TYPE", typeof(int));   //3

            //set the ID column to be AutoIncremented
            m_table.Columns[0].AutoIncrement = true;

            //set the update timer for the layer
            m_updateTimer = new System.Timers.Timer(60000);
            //m_updateTimer.Enabled = true;//false
            m_updateTimer.Elapsed += OnLayerUpdateEvent;

            m_flashTimer = new System.Windows.Forms.Timer();
            m_flashTimer.Interval = m_nFlashInterval;
            m_flashTimer.Tick += flashTimer_Tick;

            InitPointLayer();

            WaringBig(18);
            //green(12);
            graphCon.AddElements(pElementCollectiona, 0);
            //graphCon.AddElements(PElementCollectionc, 0);
            m_hookHelper.ActiveView.PartialRefresh(esriViewDrawPhase.esriViewGraphics, null, null);
            Application.DoEvents();
        }

        /// <summary>
        /// timer elapsed event handler, used to update the layer
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        /// <remarks>This layer has synthetic data, and therefore need the timer in order
        /// to update the layers' items.  </remarks>
        private void OnLayerUpdateEvent(object sender, ElapsedEventArgs e)
        {
            InitPointLayer();
            //FlashGeometries();
        }

        //初始化预警点图层
        List<PreWarningResultQueryEnt> _ents2 = new List<PreWarningResultQueryEnt>();
        // 巷道ID，工作面信息
        Dictionary<int, WarningPosition> warningPositionList = new Dictionary<int, WarningPosition>();
        double epsilon = 0.001;
        internal class WarningPosition
        {
            private int tunnelId;
            public int TunnelId
            {
                get
                {
                    return tunnelId;
                }

                set
                {
                    tunnelId = value;
                }
            }

            private int level;
            public int Level
            {
                get
                {
                    return level;
                }

                set
                {
                    level = value;
                }
            }

            private double x;
            public double X
            {
                get { return x; }
                set
                {
                    x = value;
                }
            }


            private double y;

            public double Y
            {
                get
                {
                    return y;
                }

                set { y = value; }
            }
        }

        public void updateWarningPosition()
        {
            warningPositionList.Clear();

            string strSql = "SELECT A.TUNNEL_ID, B.WORKINGFACE_ID, B.COORDINATE_X, B.COORDINATE_Y, B.COORDINATE_Z FROM T_TUNNEL_INFO AS A, T_WORKINGFACE_INFO AS B WHERE A.WORKINGFACE_ID = B.WORKINGFACE_ID";

            ManageDataBase database = new ManageDataBase(DATABASE_TYPE.WarningManagementDB);
            database.Open();
            DataTable dt = database.ReturnDSNotOpenAndClose(strSql.ToString()).Tables[0];

            DataRowCollection drCollection = dt.Rows;

            if (dt != null)
            {
                int n = dt.Rows.Count;
                for (int i = 0; i < n; i++)
                {
                    DataRow dr = drCollection[i];
                    WarningPosition info = new WarningPosition();
                    info.TunnelId = Convert.ToInt32(dr["TUNNEL_ID"].ToString());

                    info.X = dr["COORDINATE_X"] == DBNull.Value ? 0.0 : Convert.ToDouble(dr["COORDINATE_X"]);
                    info.Y = dr["COORDINATE_Y"] == DBNull.Value ? 0.0 : Convert.ToDouble(dr["COORDINATE_Y"]); ;
                    warningPositionList.Add(info.TunnelId, info);
                }
            }
        }

        private void InitPointLayer()
        {
            //try
            //{
            //1.获取预警点数据
            m_table.Clear();
            updateWarningPosition();

            // 所有红色和黄色预警
            _ents2 = LibBusiness.PreWarningLastedResultQueryBLL.QueryHoldWarningResult();
            List<IPoint> list = new List<IPoint>();
            IPoint pt;
            if (_ents2.Count > 0)
            {

                for (int i = 0; i < _ents2.Count; i++)
                {

                    int type1 = _ents2[i].OutBrustWarningResult.WarningResult;
                    int type2 = _ents2[i].OverLimitWarningResult.WarningResult;
                    //0是红色，1是黄色
                    //type = 0;
                    double X = warningPositionList[_ents2[i].TunnelID].X;
                    double Y = warningPositionList[_ents2[i].TunnelID].Y;

                    var difference = Math.Abs(X - 0.0);
                    var isEqual = difference < epsilon;
                    if (isEqual == true)
                        continue;
                    pt = new PointClass();
                    m_table.Rows.Add(0, X, Y, type1 < type2 ? type1 : type2);
                    pt.X = X;
                    pt.Y = Y;
                    list.Add(pt);
                }
                if (list.Count < 1) return;
                IGeometry pgeo = MyMapHelp.GetGeoFromPoint(list);
                if (pgeo == null)
                    return;
                if (pgeo.Envelope.Height == 0 && pgeo.Envelope.Width == 0)
                {
                    ITopologicalOperator pTopo = (ITopologicalOperator)pgeo;
                    pgeo = pTopo.Buffer(50);
                }
                MyMapHelp.Jump(pgeo);
            }

            //IDisplayTransformation displayTransformation = null;
            //displayTransformation = m_pMapControl.ActiveView.ScreenDisplay.DisplayTransformation;
            //IntializeLayerData(displayTransformation);

            //}
            //catch (Exception ex)
            //{
            //    System.Diagnostics.Trace.WriteLine(ex.Message);
            //}
        }

        #region 先通过获取随机值作为预警点数据
        /// <summary>
        /// Initialize the synthetic data of the layer
        /// </summary>
        private void IntializeLayerData(IDisplayTransformation displayTransformation)
        {
            try
            {
                IEnvelope extent = displayTransformation.FittedBounds;

                Random rnd = new Random();

                //generate the items
                for (int i = 0; i < m_nNumOfItems; i++)
                {
                    //create new record
                    DataRow r = NewItem();
                    //set the item's coordinate
                    r[1] = extent.XMin + rnd.NextDouble() * (extent.XMax - extent.XMin);
                    r[2] = extent.YMin + rnd.NextDouble() * (extent.YMax - extent.YMin);

                    //add a type ID in order to define the symbol for the item
                    switch (i % 3)
                    {
                        case 0:
                            r[3] = 0;
                            break;
                        case 1:
                            r[3] = 1;
                            break;
                        case 2:
                            r[3] = 2;
                            break;
                    }

                    //add the new item record to the table
                    AddItem(r);
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Trace.WriteLine(ex.Message);
            }
        }

        #region public methods
        public void Connect()
        {
            m_updateTimer.Enabled = true;
        }

        public void Disconnect()
        {
            m_updateTimer.Enabled = false;
        }

        public DataRow NewItem()
        {
            if (m_table == null)
                return null;
            else
                return m_table.NewRow();
        }

        public void AddItem(DataRow row)
        {
            if (row == null)
                return;
            else
                m_table.Rows.Add(row);
        }
        #endregion

        #endregion
        #endregion

        #region 2.按照风险等级闪烁：读取预警点信息→根据风险等级分类组合→闪烁不同类别闪烁点
        private void flashTimer_Tick(object sender, EventArgs e)
        {
            FlashGeometries();
            //System.Threading.Thread td = new System.Threading.Thread(FlashWaringPoint);
            //td.Start();
            if (m_flashTimer.Enabled == false)
            {
                graphCon.DeleteAllElements();
                m_hookHelper.ActiveView.PartialRefresh(esriViewDrawPhase.esriViewGraphics, null, null);
            }
        }

        //预警点局部放大后闪烁图形 
        public void FlashWaringPoint()
        {
            IRgbColor markcolor = new RgbColorClass();

            markcolor.Red = 255;
            markcolor.Blue = 0;
            markcolor.Green = 0;


            ESRI.ArcGIS.Display.ISimpleMarkerSymbol simpleMarkerSymbol = new ESRI.ArcGIS.Display.SimpleMarkerSymbolClass();
            simpleMarkerSymbol.Style = ESRI.ArcGIS.Display.esriSimpleMarkerStyle.esriSMSCircle;
            simpleMarkerSymbol.Size = 12;
            simpleMarkerSymbol.Color = markcolor;
            ISymbol pSymbol = (ISymbol)simpleMarkerSymbol;

            IPoint pt;
            List<IPoint> list = new List<IPoint>();
            for (int i = 0; i < m_table.Rows.Count; i++)
            {
                pt = new PointClass();
                pt.X = Convert.ToDouble(m_table.Rows[i]["x"].ToString());
                pt.Y = Convert.ToDouble(m_table.Rows[i]["y"].ToString());
                list.Add(pt);
            }
            IGeometry geo = MyMapHelp.GetGeoFromPoint(list);
            if (geo == null) return;
            GIS.Common.DataEditCommon.g_pAxMapControl.FlashShape(geo, 3, 1000, pSymbol);
        }
        public void FlashGeometries()
        {
            WaringBig(18);
            WaringSmall(6);
            //green(12);
            for (int a = 0; a < 3; a++)
            {
                graphCon.DeleteAllElements();
                //graphCon.AddElements(PElementCollectionc, 0);
                graphCon.AddElements(PElementCollectionb, 0);
                m_hookHelper.ActiveView.PartialRefresh(esriViewDrawPhase.esriViewGraphics, null, null);
                Application.DoEvents();
                System.Threading.Thread.Sleep(200);
                graphCon.AddElements(pElementCollectiona, 0);
                m_hookHelper.ActiveView.PartialRefresh(esriViewDrawPhase.esriViewGraphics, null, null);
                Application.DoEvents();
                System.Threading.Thread.Sleep(200);
            }
        }
        #endregion
        #region 点闪烁样式
        IElementCollection pElementCollectiona;
        private IElementCollection PElementCollectionb;
        private IElementCollection PElementCollectionc;
        private void WaringBig(double size)
        {
            pElementCollectiona = new ElementCollectionClass();
            IMarkerSymbol m_pMarkerSymbol;
            IRgbColor markcolor;
            IPoint pt;
            for (int i = 0; i < m_table.Rows.Count; i++)
            {
                markcolor = new RgbColorClass();
                pt = new PointClass();
                pt.X = Convert.ToDouble(m_table.Rows[i]["x"].ToString());
                pt.Y = Convert.ToDouble(m_table.Rows[i]["y"].ToString());
                string type = m_table.Rows[i]["type"].ToString();
                if (type == "0")
                {
                    markcolor.Red = 255;
                    markcolor.Blue = 0;
                    markcolor.Green = 0;
                }
                else if (type == "1")
                {
                    markcolor.Red = 255;
                    markcolor.Blue = 0;
                    markcolor.Green = 170;
                }
                else
                {
                    continue;
                }

                ESRI.ArcGIS.Display.ISimpleMarkerSymbol simpleMarkerSymbol = new ESRI.ArcGIS.Display.SimpleMarkerSymbolClass();
                simpleMarkerSymbol.Style = ESRI.ArcGIS.Display.esriSimpleMarkerStyle.esriSMSCircle;
                simpleMarkerSymbol.Size = size;

                simpleMarkerSymbol.Color = markcolor;

                IElement pMarkerElement = new MarkerElementClass();
                pMarkerElement.Geometry = pt;
                IMarkerElement pPointElement = (IMarkerElement)pMarkerElement;

                ISymbol pSymbol = (ISymbol)simpleMarkerSymbol;
                pPointElement.Symbol = (IMarkerSymbol)pSymbol;
                pElementCollectiona.Add(pMarkerElement);

            }
        }
        private void WaringSmall(double size)
        {
            PElementCollectionb = new ElementCollectionClass();
            IMarkerSymbol m_pMarkerSymbol;
            IRgbColor markcolor;
            IPoint pt;
            for (int i = 0; i < m_table.Rows.Count; i++)
            {
                markcolor = new RgbColorClass();
                pt = new PointClass();
                pt.X = Convert.ToDouble(m_table.Rows[i]["x"].ToString());
                pt.Y = Convert.ToDouble(m_table.Rows[i]["y"].ToString());
                string type = m_table.Rows[i]["type"].ToString();
                if (type == "0")
                {
                    markcolor.Red = 255;
                    markcolor.Blue = 0;
                    markcolor.Green = 0;
                }
                else if (type == "1")
                {
                    markcolor.Red = 255;
                    markcolor.Blue = 0;
                    markcolor.Green = 170;
                }
                else
                {
                    continue;
                }

                ESRI.ArcGIS.Display.ISimpleMarkerSymbol simpleMarkerSymbol = new ESRI.ArcGIS.Display.SimpleMarkerSymbolClass();
                simpleMarkerSymbol.Style = ESRI.ArcGIS.Display.esriSimpleMarkerStyle.esriSMSCircle;
                simpleMarkerSymbol.Size = size;

                simpleMarkerSymbol.Color = markcolor;

                IElement pMarkerElement = new MarkerElementClass();
                pMarkerElement.Geometry = pt;
                IMarkerElement pPointElement = (IMarkerElement)pMarkerElement;

                ISymbol pSymbol = (ISymbol)simpleMarkerSymbol;
                pPointElement.Symbol = (IMarkerSymbol)pSymbol;
                PElementCollectionb.Add(pMarkerElement, 0);
            }
        }
        private void green(double size)
        {
            PElementCollectionc = new ElementCollectionClass();
            IMarkerSymbol m_pMarkerSymbol;
            IRgbColor markcolor;
            IPoint pt;
            for (int i = 0; i < m_table.Rows.Count; i++)
            {
                markcolor = new RgbColorClass();
                pt = new PointClass();
                pt.X = Convert.ToDouble(m_table.Rows[i]["x"].ToString());
                pt.Y = Convert.ToDouble(m_table.Rows[i]["y"].ToString());
                string type = m_table.Rows[i]["type"].ToString();
                if (type == "0" || type == "1")
                {
                    continue;
                }
                else
                {
                    markcolor.Red = 76;
                    markcolor.Blue = 0;
                    markcolor.Green = 230;
                }

                ESRI.ArcGIS.Display.ISimpleMarkerSymbol simpleMarkerSymbol = new ESRI.ArcGIS.Display.SimpleMarkerSymbolClass();
                simpleMarkerSymbol.Style = ESRI.ArcGIS.Display.esriSimpleMarkerStyle.esriSMSCircle;
                simpleMarkerSymbol.Size = size;

                simpleMarkerSymbol.Color = markcolor;
                IElement pMarkerElement = new MarkerElementClass();
                pMarkerElement.Geometry = pt;
                IMarkerElement pPointElement = (IMarkerElement)pMarkerElement;

                ISymbol pSymbol = (ISymbol)simpleMarkerSymbol;
                pPointElement.Symbol = (IMarkerSymbol)pSymbol;
                PElementCollectionc.Add(pMarkerElement);
            }
        }
        #endregion
    }
}
