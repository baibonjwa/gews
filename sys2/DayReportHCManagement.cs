using System;
using System.Collections.Generic;
using System.Windows.Forms;
using ESRI.ArcGIS.Geometry;
using GIS.HdProc;
using LibBusiness;
using LibCommon;
using LibCommonControl;
using LibEntity;

namespace sys2
{
    /// <summary>
    /// 回采进尺管理
    /// </summary>
    public partial class DayReportHcManagement : BaseForm
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

        /// <summary>
        /// 初始化
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DayReportHCManagement_Load(object sender, EventArgs e)
        {

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
            bool result = Alert.confirm(Const.DEL_CONFIRM_MSG);

            if (result == true)
            {
                bool bResult = true;

                //获取当前farpoint选中焦点
                //    _tmpRowIndex = fpDayReportHC.Sheets[0].ActiveRowIndex;
                //    for (int i = 0; i < _rowsCount; i++)
                //    {
                //        //选择为null时，该选择框没有被选择过,与未选中同样效果
                //        //选择框被选择
                //        if (cells[_rowDetailStartIndex + i, 0].Value != null &&
                //            (bool)cells[_rowDetailStartIndex + i, 0].Value == true)
                //        {
                //            DayReportHc entity = new DayReportHc();
                //            //获取掘进ID
                //            entity.Id = (int)dr[DayReportHCDbConstNames.ID];
                //            entity.WorkingFace.WorkingFaceID = Convert.ToInt32(dr[DayReportHCDbConstNames.WORKINGFACE_ID]);
                //            entity.BindingId = dr[DayReportHCDbConstNames.BINDINGID].ToString();

                //            // 回采面对象
                //            WorkingFace hjEntity = BasicInfoManager.getInstance().getWorkingFaceById(entity.WorkingFace.WorkingFaceID);
                //            if (hjEntity != null)
                //                hjEntity.tunnelSet = BasicInfoManager.getInstance().getTunnelSetByDataSet(TunnelInfoBLL.selectTunnelByWorkingFaceId(hjEntity.WorkingFaceID));
                //            Dictionary<TunnelTypeEnum, Tunnel> tDict = TunnelUtils.getTunnelDict(hjEntity);

                //            if (tDict.Count > 0)
                //            {
                //                Tunnel tunnelZY = tDict[TunnelTypeEnum.STOPING_ZY];
                //                Tunnel tunnelFY = tDict[TunnelTypeEnum.STOPING_FY];
                //                Tunnel tunnelQY = tDict[TunnelTypeEnum.STOPING_QY];
                //                // 删除GIS图形上的回采进尺
                //                DelHcjc(tunnelZY.TunnelId, tunnelFY.TunnelId, tunnelQY.TunnelId, entity.BindingId, hjEntity, tunnelZY.TunnelWid, tunnelFY.TunnelWid, tunnelQY.TunnelWid);
                //            }

                //            // 从数据库中删除对应的进尺信息
                //            entity.DeleteAndFlush();

                //            BasicInfoManager.getInstance().refreshWorkingFaceInfo(hjEntity);

                //            #region 通知服务器预警数据已更新
                //            UpdateWarningDataMsg msg = new UpdateWarningDataMsg(entity.WorkingFace.WorkingFaceID,
                //                Const.INVALID_ID,
                //                DayReportHCDbConstNames.TABLE_NAME, OPERATION_TYPE.DELETE, DateTime.Now);
                //            this.MainForm.SendMsg2Server(msg);
                //            #endregion
                //        }
                //    }
                //    //删除成功
                //    if (bResult)
                //    {
                //        //绑定数据
                //        bindDayReportHC();
                //        //删除后重设Farpoint焦点
                //        FarPointOperate.farPointFocusSetDel(fpDayReportHC, _tmpRowIndex);
                //    }
                //    //删除失败
                //    else
                //    {
                //        Alert.alert(Const_MS.MSG_DELETE_FAILURE);
                //    }
                //}
            }
            return;
        }

        /// <summary>
        /// 删除GIS回采进尺图形信息
        /// </summary>
        /// <param name="hd1">主运顺槽id</param>
        /// <param name="hd2">辅运顺槽id</param>
        /// <param name="qy">切眼id</param>
        /// <param name="bid">回采进尺的BindingID</param>
        /// <param name="wfEntity">回采面实体</param>
        private void DelHcjc(int hd1, int hd2, int qy, string bid, WorkingFace wfEntity, double zywid, double fywid, double qywid)
        {
            //删除对应的回采进尺图形和数据表中的记录信息
            Dictionary<string, IPoint> results = Global.cons.DelHCCD(hd1.ToString(), hd2.ToString(), qy.ToString(), bid, zywid, fywid, Global.searchlen);
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
                wfEntity.Coordinate = new Coordinate(x, y, z);
                wfEntity.Save();

                index += 1;
                if (index == count - 1)
                {
                    posnew = new PointClass();
                    posnew.X = x;
                    posnew.Y = y;
                    posnew.Z = z;
                }
            }
            //更新回采进尺表，将isdel设置0
            DayReportHc entity = new DayReportHc();
            entity = DayReportHc.FindByBid(bid);
            entity.IsDel = 0;
            entity.SaveAndFlush();


            //更新地质构造表中的信息
            if (posnew == null)
                return;
            List<int> hd_ids = new List<int>();
            hd_ids.Add(hd1);
            hd_ids.Add(hd2);
            hd_ids.Add(qy);
            Dictionary<string, List<GeoStruct>> dzxlist = Global.commonclss.GetStructsInfos(posnew, hd_ids);
            if (dzxlist.Count > 0)
            {
                GeologySpaceBll.DeleteGeologySpaceEntityInfos(wfEntity.WorkingFaceId);//删除工作面ID对应的地质构造信息
                foreach (string key in dzxlist.Keys)
                {
                    List<GeoStruct> geoinfos = dzxlist[key];
                    string geo_type = key;
                    for (int j = 0; j < geoinfos.Count; j++)
                    {
                        GeoStruct tmp = geoinfos[j];

                        GeologySpace geologyspaceEntity = new GeologySpace();
                        geologyspaceEntity.WorkingFace = wfEntity;
                        geologyspaceEntity.TectonicType = Convert.ToInt32(key);
                        geologyspaceEntity.TectonicId = tmp.geoinfos[GIS.GIS_Const.FIELD_BID].ToString();
                        geologyspaceEntity.Distance = tmp.dist;
                        geologyspaceEntity.OnDateTime = DateTime.Now.ToShortDateString();

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
            this.Close();
        }

        /// <summary>
        /// 刷新按钮事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tsBtnRefresh_Click(object sender, EventArgs e)
        {

        }


        /// <summary>
        /// 导出按钮事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tsBtnExport_Click(object sender, EventArgs e)
        {

        }

        /// <summary>
        /// 打印按钮事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tsBtnPrint_Click(object sender, EventArgs e)
        {

        }
    }
}
