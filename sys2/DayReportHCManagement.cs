using System;
using System.Collections.Generic;
using System.Globalization;
using System.Windows.Forms;
using Castle.ActiveRecord;
using ESRI.ArcGIS.Geometry;
using GIS.HdProc;
using LibBusiness;
using LibCommon;
using LibEntity;
using LibEntity.Domain;
using LibSocket;

namespace sys2
{
    /// <summary>
    /// 回采进尺管理
    /// </summary>
    public partial class DayReportHcManagement : Form
    {
        /// <summary>
        /// 构造方法
        /// </summary>
        public DayReportHcManagement()
        {
            InitializeComponent();

            //窗体属性设置
            FormDefaultPropertiesSetter.SetManagementFormDefaultProperties(this, Const_MS.DAY_REPORT_HC_MANAGEMENT);
        }


        private void RefreshData()
        {
            gcDayReportHc.DataSource = DayReportHc.FindAll();
        }

        /// <summary>
        /// 初始化
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DayReportHCManagement_Load(object sender, EventArgs e)
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
            var m = new DayReportHcEntering();
            if (DialogResult.OK == m.ShowDialog())
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
            var dayReportHc = (DayReportHc)gridView1.GetFocusedRow();

            var m = new DayReportHcEntering(dayReportHc);
            if (DialogResult.OK == m.ShowDialog())
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
            using (new SessionScope())
            {
                var entity = (DayReportHc)gridView1.GetFocusedRow();
                var workingFace = WorkingFace.Find(entity.WorkingFace.WorkingFaceId);
                // 掘进工作面，只有一条巷道
                var workingFaceHc = WorkingFaceHc.FindByWorkingFace(workingFace);
                if (workingFaceHc != null)
                {
                    DelHcjc(workingFaceHc.TunnelZy.TunnelId, workingFaceHc.TunnelFy.TunnelId, workingFaceHc.TunnelQy.TunnelId, entity.BindingId,
                        workingFace,
                        workingFaceHc.TunnelZy.TunnelWid, workingFaceHc.TunnelFy.TunnelWid);
                    entity.Delete();
                    RefreshData();
                    // 向server端发送更新预警数据
                    var msg = new UpdateWarningDataMsg(entity.WorkingFace.WorkingFaceId,
                        Const.INVALID_ID,
                        DayReportHc.TableName, OPERATION_TYPE.DELETE, DateTime.Now);
                    SocketUtil.SendMsg2Server(msg);
                }
                else
                {
                    Alert.alert("该工作面没有关联主运、辅运、切眼巷道");
                }
            }
        }

        /// <summary>
        /// 删除GIS回采进尺图形信息
        /// </summary>
        /// <param name="hd1">主运顺槽id</param>
        /// <param name="hd2">辅运顺槽id</param>
        /// <param name="qy">切眼id</param>
        /// <param name="bid">回采进尺的BindingID</param>
        /// <param name="wfEntity">回采面实体</param>
        /// <param name="zywid"></param>
        /// <param name="fywid"></param>
        private void DelHcjc(int hd1, int hd2, int qy, string bid, WorkingFace wfEntity, double zywid, double fywid)
        {
            //删除对应的回采进尺图形和数据表中的记录信息
            Dictionary<string, IPoint> results = Global.cons.DelHCCD(hd1.ToString(CultureInfo.InvariantCulture), hd2.ToString(CultureInfo.InvariantCulture), qy.ToString(CultureInfo.InvariantCulture), bid, zywid, fywid, Global.searchlen);
            if (results == null)
                return;

            //更新当前回采进尺后的回采进尺记录表信息
            int count = results.Keys.Count;
            int index = 0;
            IPoint posnew = null;
            foreach (string key in results.Keys)
            {
                double x = results[key].X;
                double y = results[key].Y;
                double z = results[key].Z;
                wfEntity.SetCoordinate(x, y, z);
                wfEntity.Save();

                index += 1;
                if (index == count - 1)
                {
                    posnew = new PointClass { X = x, Y = y, Z = z };
                }
            }
            //更新回采进尺表，将isdel设置0
            var entity = DayReportHc.FindByBid(bid);
            entity.IsDel = 0;
            entity.SaveAndFlush();


            //更新地质构造表中的信息
            if (posnew == null)
                return;
            var hdIds = new List<int> { hd1, hd2, qy };
            Dictionary<string, List<GeoStruct>> dzxlist = Global.commonclss.GetStructsInfos(posnew, hdIds);
            if (dzxlist.Count > 0)
            {
                GeologySpaceBll.DeleteGeologySpaceEntityInfos(wfEntity.WorkingFaceId);//删除工作面ID对应的地质构造信息
                foreach (string key in dzxlist.Keys)
                {
                    List<GeoStruct> geoinfos = dzxlist[key];
                    foreach (GeoStruct tmp in geoinfos)
                    {
                        var geologyspaceEntity = new GeologySpace
                        {
                            WorkingFace = wfEntity,
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
                gcDayReportHc.ExportToXls(saveFileDialog1.FileName);
            }
        }

        /// <summary>
        /// 打印按钮事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tsBtnPrint_Click(object sender, EventArgs e)
        {
            DevUtil.DevPrint(gcDayReportHc, "回采进尺信息报表");
        }
    }
}
