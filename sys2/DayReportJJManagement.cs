using System;
using System.Collections.Generic;
using System.Globalization;
using System.Windows.Forms;
using ESRI.ArcGIS.Carto;
using GIS.HdProc;
using LibBusiness;
using LibCommon;
using LibEntity;
using LibSocket;

namespace sys2
{
    public partial class DayReportJjManagement : Form
    {
        /// <summary>
        /// 构造方法
        /// </summary>
        public DayReportJjManagement()
        {
            InitializeComponent();

            //窗体属性设置
            FormDefaultPropertiesSetter.SetManagementFormDefaultProperties(this, Const_MS.DAY_REPORT_JJ_MANAGEMENT);
        }

        private void RefreshData()
        {
            gcDayReportJj.DataSource = DayReportJj.FindAll();
        }

        /// <summary>
        /// 初始化
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DayReportJJManagement_Load(object sender, EventArgs e)
        {
            RefreshData();
        }

        /// <summary>
        /// 添加按钮事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tsBtnAdd_Click(object sender, EventArgs e)
        {
            var dayReportJjForm = new DayReportJJEntering();
            if (DialogResult.OK == dayReportJjForm.ShowDialog())
            {
                RefreshData();
            }
        }

        /// <summary>
        /// 修改按钮事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tsBtnModify_Click(object sender, EventArgs e)
        {
            var dayReportJjForm = new DayReportJJEntering((DayReportJj)gridView1.GetFocusedRow());
            if (DialogResult.OK == dayReportJjForm.ShowDialog())
            {
                RefreshData();
            }
        }

        /// <summary>
        /// 删除按钮事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tsBtnDel_Click(object sender, EventArgs e)
        {
            //确认删除
            if (!Alert.confirm(Const.DEL_CONFIRM_MSG)) return;
            var entity = (DayReportJj)gridView1.GetFocusedRow();
            // 掘进工作面，只有一条巷道
            var tunnel = Tunnel.FindFirstByWorkingFaceId(entity.WorkingFace.WorkingFaceId);
            //删除操作
            entity.Delete();
            DelJJCD(tunnel.TunnelId.ToString(CultureInfo.InvariantCulture), entity.BindingId, entity.WorkingFace.WorkingFaceId);
            // 向server端发送更新预警数据
            var msg = new UpdateWarningDataMsg(entity.WorkingFace.WorkingFaceId,
                Const.INVALID_ID,
                DayReportJj.TableName, OPERATION_TYPE.DELETE, DateTime.Now);
            SocketUtil.SendMsg2Server(msg);
        }

        /// <summary>
        /// 删除掘进进尺日报对应的地图信息
        /// </summary>
        /// <param name="hdid">巷道ID</param>
        /// <param name="bid">绑定ID</param>
        /// <param name="workingfaceid"></param>
        private void DelJJCD(string hdid, string bid, int workingfaceid)
        {
            Global.cons.DelJJCD(hdid, bid);
            //计算地质构造距离
            //var sql = "\"" + GIS.GIS_Const.FIELD_HDID + "\"='" + hdid + "'";
            var dics = new Dictionary<string, string> { { GIS.GIS_Const.FIELD_HDID, hdid } };
            List<Tuple<ESRI.ArcGIS.Geodatabase.IFeature, ESRI.ArcGIS.Geometry.IGeometry, Dictionary<string, string>>> objs = Global.commonclss.SearchFeaturesByGeoAndText(Global.hdfdlyr, dics);
            if (objs.Count > 0)
            {
                var poly0 = objs[0].Item2 as ESRI.ArcGIS.Geometry.IPointCollection;
                //var poly1 = objs[1].Item2 as ESRI.ArcGIS.Geometry.IPointCollection;
                ESRI.ArcGIS.Geometry.IPoint pline = new ESRI.ArcGIS.Geometry.PointClass();
                if (poly0 != null && poly0.Point[0].X - poly0.Point[1].X > 0)//向右掘进
                {
                    pline.X = (poly0.Point[0].X + poly0.Point[3].X) / 2;
                    pline.Y = (poly0.Point[0].Y + poly0.Point[3].Y) / 2;
                }
                else//向左掘进
                {
                    if (poly0 != null)
                    {
                        pline.X = (poly0.Point[1].X + poly0.Point[2].X) / 2;
                        pline.Y = (poly0.Point[1].Y + poly0.Point[2].Y) / 2;
                    }
                }
                //查询地质构造信息
                var hdids = new List<int> { Convert.ToInt32(hdid) };
                Dictionary<string, List<GeoStruct>> dzxlist = Global.commonclss.GetStructsInfosNew(pline, hdids);
                GeologySpaceBll.DeleteGeologySpaceEntityInfos(workingfaceid);//删除工作面ID对应的地质构造信息

                foreach (string key in dzxlist.Keys)
                {
                    List<GeoStruct> geoinfos = dzxlist[key];
                    //string geoType = key;
                    foreach (var tmp in geoinfos)
                    {
                        var geologyspaceEntity = new GeologySpace
                        {
                            WorkingFace = WorkingFace.Find(workingfaceid),
                            TectonicType = Convert.ToInt32(key),
                            TectonicId = tmp.geoinfos[GIS.GIS_Const.FIELD_BID],
                            Distance = tmp.dist,
                            OnDateTime = DateTime.Now.ToShortDateString()
                        };

                        geologyspaceEntity.Save();
                    }
                }

            }
        }

        /// <summary>
        /// 退出按钮事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tsBtnExit_Click(object sender, EventArgs e)
        {
            //关闭窗体
            Close();
        }

        /// <summary>
        /// 刷新按钮事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tsBtnRefresh_Click(object sender, EventArgs e)
        {
            RefreshData();
        }


        /// <summary>
        /// 导出按钮事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tsBtnExport_Click(object sender, EventArgs e)
        {
            if (saveFileDialog1.ShowDialog() == DialogResult.OK)
            {
                gcDayReportJj.ExportToXls(saveFileDialog1.FileName);
            }
        }

        /// <summary>
        /// 打印按钮事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tsBtnPrint_Click(object sender, EventArgs e)
        {
            DevUtil.DevPrint(gcDayReportJj, "掘进进尺信息报表");
        }



        /// <summary>
        /// 图显按钮事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnMap_Click(object sender, EventArgs e)
        {
            ILayer pLayer = GIS.Common.DataEditCommon.GetLayerByName(GIS.Common.DataEditCommon.g_pMap, GIS.LayerNames.LAYER_ALIAS_MR_TUNNEL_FD);
            if (pLayer == null)
            {
                MessageBox.Show(@"未发现掘进进尺图层！");
                return;
            }
            var pFeatureLayer = (IFeatureLayer)pLayer;
            string str = "";
            //for (int i = 0; i < iSelIdxsArr.Length; i++)
            //{
            string bid = ((DayReportJj)gridView1.GetFocusedRow()).BindingId;
            if (bid != "")
            {
                if (true)
                    str = "bid='" + bid + "'";
                //else
                //    str += " or bid='" + bid + "'";
            }
            //}
            List<ESRI.ArcGIS.Geodatabase.IFeature> list = GIS.MyMapHelp.FindFeatureListByWhereClause(pFeatureLayer, str);
            if (list.Count > 0)
            {
                GIS.MyMapHelp.Jump(GIS.MyMapHelp.GetGeoFromFeature(list));
            }
            else
            {
                Alert.alert("图元丢失");
            }
        }
    }
}
