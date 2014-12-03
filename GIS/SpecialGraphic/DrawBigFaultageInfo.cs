using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ESRI.ArcGIS.Geometry;
using System.Windows.Forms;
using ESRI.ArcGIS.Display;
using ESRI.ArcGIS.Carto;
using GIS.Common;
using ESRI.ArcGIS.Geodatabase;
using LibEntity;

namespace GIS
{
    /// <summary>
    /// 绘制推断断层
    /// </summary>
    public class DrawBigFaultageInfo
    {
        /// <summary>
        /// 绘制推断断层
        /// </summary>
        /// <param name="faultage"></param>
        /// <param name="faultagePointList"></param>
        /// <returns></returns>
        public static bool DrawTddc(String title, List<BigFaultagePointEntity> faultagePointList, String bId)
        {
            List<IPoint> listptS = new List<IPoint>();
            List<IPoint> listptX = new List<IPoint>();

            foreach (var i in faultagePointList)
            {
                if (i.UpOrDown == "上盘")
                {
                    IPoint point = new PointClass();
                    point.X = i.CoordinateX;
                    point.Y = i.CoordinateY;
                    point.Z = i.CoordinateZ;

                    listptS.Add(point);
                }
                else if (i.UpOrDown == "下盘")
                {
                    IPoint point = new PointClass();
                    point.X = i.CoordinateX;
                    point.Y = i.CoordinateY;
                    point.Z = i.CoordinateZ;

                    listptX.Add(point);
                }
            }

            return DrawTDDC(title, bId, listptS, listptX);
        }


        /// <summary>
        /// 根据文本文件绘制推断断层
        /// </summary>
        /// <param name="filename">文本路径</param>
        /// <returns></returns>
        public static bool DrawTddcByFile(string filename, string BID)
        {
            try
            {
                string title = "";
                string[] strs = System.IO.File.ReadAllLines(filename, Encoding.Default);
                List<IPoint> listptS = new List<IPoint>();
                List<IPoint> listptX = new List<IPoint>();
                IPoint ptS = new PointClass();
                IPoint ptX = new PointClass();
                string strx, stry, strz;
                double x, y, z;
                string type = "";
                for (int i = 0; i < strs.Length; i++)
                {
                    if (i == 0)
                    {
                        title = strs[0];
                    }
                    if (strs[i] == "上盘")
                    {
                        type = "上盘";
                        continue;
                    }
                    if (strs[i] == "下盘")
                    {
                        type = "下盘";
                        continue;
                    }
                    if (strs[i].Equals(""))
                    {
                        continue;
                    }
                    if (type == "上盘")
                    {
                        ptS = new PointClass();
                        strx = strs[i].Split(',')[0];
                        stry = strs[i].Split(',')[1];
                        strz = strs[i].Split(',')[2];
                        if (double.TryParse(strx, out x))
                        {
                            ptS.X = x;
                        }
                        else
                        {
                            MessageBox.Show("第" + (i + 1).ToString() + "行非法X坐标！");
                            return false;
                        }
                        if (double.TryParse(stry, out y))
                        {
                            ptS.Y = y;
                        }
                        else
                        {
                            MessageBox.Show("第" + (i + 1).ToString() + "行非法Y坐标！");
                            return false;
                        }
                        if (double.TryParse(strz, out z))
                        {
                            ptS.Z = z;
                        }
                        else
                        {
                            MessageBox.Show("第" + (i + 1).ToString() + "行非法Z坐标！");
                            return false;
                        }
                        listptS.Add(ptS);
                    }
                    if (type == "下盘")
                    {
                        ptX = new PointClass();
                        strx = strs[i].Split(',')[0];
                        stry = strs[i].Split(',')[1];
                        strz = strs[i].Split(',')[2];
                        if (double.TryParse(strx, out x))
                        {
                            ptX.X = x;
                        }
                        else
                        {
                            MessageBox.Show("第" + (i + 1).ToString() + "行非法X坐标！");
                            return false;
                        }
                        if (double.TryParse(stry, out y))
                        {
                            ptX.Y = y;
                        }
                        else
                        {
                            MessageBox.Show("第" + (i + 1).ToString() + "行非法Y坐标！");
                            return false;
                        }
                        if (double.TryParse(strz, out z))
                        {
                            ptX.Z = z;
                        }
                        else
                        {
                            MessageBox.Show("第" + (i + 1).ToString() + "行非法Z坐标！");
                            return false;
                        }
                        listptX.Add(ptX);
                    }
                }
                if (listptS.Count < 1)
                {
                    MessageBox.Show("上盘坐标读取失败！");
                    return false;
                }
                if (listptX.Count < 1)
                {
                    MessageBox.Show("下盘坐标读取失败！");
                    return false;
                }
                return DrawTDDC(title, BID, listptS, listptX);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return false;
            }
        }
        /// <summary>
        /// 绘制推断断层
        /// </summary>
        /// <param name="title">文字</param>
        /// <param name="listptS">上盘坐标集合</param>
        /// <param name="listptX">下盘坐标集合</param>
        /// <returns></returns>
        public static bool DrawTDDC(string title, string BID, List<IPoint> listptS, List<IPoint> listptX)
        {
            try
            {
                ITopologicalOperator pTopo;
                string sLayerAliasName = GIS.LayerNames.DEFALUT_INFERRED_FAULTAGE;//“默认_推断断层”图层
                ILayer pLayer = GIS.Common.DataEditCommon.GetLayerByName(GIS.Common.DataEditCommon.g_pMap, sLayerAliasName);
                IFeatureLayer pFeatureLayer = pLayer as IFeatureLayer;
                if (pFeatureLayer == null)
                {
                    MessageBox.Show("推断断层图层缺失！");
                    return false;
                }

                INewBezierCurveFeedback pNewBezierCurveFeedback = new NewBezierCurveFeedbackClass();
                pNewBezierCurveFeedback.Display = GIS.Common.DataEditCommon.g_pAxMapControl.ActiveView.ScreenDisplay;
                for (int i = 0; i < listptS.Count; i++)
                {
                    if (i == 0)
                        pNewBezierCurveFeedback.Start(listptS[0]);
                    else
                        pNewBezierCurveFeedback.AddPoint(listptS[i]);
                }
                IGeometry pGeometry;
                pGeometry = pNewBezierCurveFeedback.Stop();
                IPolyline polyline = new PolylineClass();
                polyline = (IPolyline)pGeometry;
                pTopo = pGeometry as ITopologicalOperator;
                List<ziduan> ziduan = new List<GIS.Common.ziduan>();
                ziduan.Add(new GIS.Common.ziduan("str", title));
                ziduan.Add(new GIS.Common.ziduan("type", "1"));
                ziduan.Add(new GIS.Common.ziduan("BID", BID));
                IFeature pFeature = DataEditCommon.CreateNewFeature(pFeatureLayer, polyline, ziduan);
                DataEditCommon.g_pMap.SelectFeature(pFeatureLayer, pFeature);


                pNewBezierCurveFeedback = new NewBezierCurveFeedbackClass();
                pNewBezierCurveFeedback.Display = GIS.Common.DataEditCommon.g_pAxMapControl.ActiveView.ScreenDisplay;
                for (int i = 0; i < listptX.Count; i++)
                {
                    if (i == 0)
                        pNewBezierCurveFeedback.Start(listptX[0]);
                    else
                        pNewBezierCurveFeedback.AddPoint(listptX[i]);
                }
                IGeometry xGeometry = pNewBezierCurveFeedback.Stop();
                polyline = (IPolyline)xGeometry;
                ziduan = new List<GIS.Common.ziduan>();
                ziduan.Add(new GIS.Common.ziduan("str", ""));
                ziduan.Add(new GIS.Common.ziduan("type", "2"));
                ziduan.Add(new GIS.Common.ziduan("BID", BID));
                pFeature = DataEditCommon.CreateNewFeature(pFeatureLayer, polyline, ziduan);
                pTopo.Union(xGeometry);
                DataEditCommon.g_pMap.SelectFeature(pFeatureLayer, pFeature);

                GIS.MyMapHelp.Jump((IGeometry)pTopo);
                DataEditCommon.g_pAxMapControl.ActiveView.PartialRefresh(esriViewDrawPhase.esriViewGeoSelection | esriViewDrawPhase.esriViewBackground, null, null);
                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return false;
            }
        }

        public static bool DelTDDC(string BID)
        {
            try
            {
                string sLayerAliasName = GIS.LayerNames.DEFALUT_INFERRED_FAULTAGE;
                ILayer pLayer = GIS.Common.DataEditCommon.GetLayerByName(GIS.Common.DataEditCommon.g_pMap,
                    sLayerAliasName);
                IFeatureLayer pFeatureLayer = pLayer as IFeatureLayer;
                if (pFeatureLayer == null)
                {
                    MessageBox.Show("推断断层图层确实！");
                    return false;
                }
                return GIS.Common.DataEditCommon.DeleteFeatureByBId(pFeatureLayer, BID);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                throw;
            }
            
        }
    }

}
