using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.Geodatabase;
using GIS;
using GIS.Common;
using LibCommon;
using LibCommonControl;
using LibCommonForm;
using LibEntity;

namespace sys3
{
    public partial class TunnelInfoManagement : BaseForm
    {

        /// <summary>
        ///     构造方法
        /// </summary>
        public TunnelInfoManagement()
        {
            InitializeComponent();
            FormDefaultPropertiesSetter.SetManagementFormDefaultProperties(this, Const_GM.TUNNEL_INFO_MANAGEMENT);
        }

        private void RefreshData()
        {
            gcTunnel.DataSource = Tunnel.FindAll();
        }

        /// <summary>
        ///     初始化
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TunnelInfoManagement_Load(object sender, EventArgs e)
        {
            RefreshData();
            btnNoWire.BackColor = Const.NO_WIRE_TUNNEL_COLOR;
            btnWired.BackColor = Const.WIRED_TUNNEL_COLOR;
            btnTunnelJJ.BackColor = Const.JJ_TUNNEL_COLOR;
            btnTunnelHC.BackColor = Const.HC_TUNNEL_COLOR;
        }

        /// <summary>
        ///     添加按钮响应
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tsBtnAdd_Click(object sender, EventArgs e)
        {
            var d = new TunnelInfoEntering();

            if (DialogResult.OK == d.ShowDialog())
            {
                RefreshData();
            }
        }

        /// <summary>
        ///     修改按钮响应
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tsBtnModify_Click(object sender, EventArgs e)
        {
            var d = new TunnelInfoEntering((Tunnel)gridView1.GetFocusedRow());
            if (DialogResult.OK == d.ShowDialog())
            {
                RefreshData();
            }
        }

        /// <summary>
        ///     删除按钮响应
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tsBtnDel_Click(object sender, EventArgs e)
        {
            if (Alert.confirm(Const_GM.TUNNEL_INFO_MSG_DEL))
            {
                //掘进ID
                var tunnelEntity = (Tunnel)gridView1.GetFocusedRow();
                //巷道类型为掘进或回采巷道
                if (Wire.FindOneByTunnelId(tunnelEntity.TunnelId) != null)
                {
                    var wire = Wire.FindOneByTunnelId(tunnelEntity.TunnelId);
                    Wire.DeleteAll(WirePoint.FindAllByWireId(wire.WireId).Select(u => u.WirePointId));
                    //不删除时将导线重新绑定到其他巷道，默认为巷道ID=0
                }
                //删除巷道对应掘进日报
                DayReportJj.DeleteByWorkingFaceId(tunnelEntity.WorkingFace.WorkingFaceId);
                //删除巷道对应回采日报
                DayReportHc.DeleteByWorkingFaceId(tunnelEntity.WorkingFace.WorkingFaceId);
                //删除巷道
                tunnelEntity.Delete();
                RefreshData();
            }
        }

        /// <summary>
        ///     退出按钮事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tsBtnExit_Click(object sender, EventArgs e)
        {
            Close();
        }
        /// <summary>
        ///     导出按钮事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tsBtnExport_Click(object sender, EventArgs e)
        {
            if (saveFileDialog1.ShowDialog() == DialogResult.OK)
            {
                gcTunnel.ExportToXls(saveFileDialog1.FileName);
            }
        }

        /// <summary>
        ///     打印按钮事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tsBtnPrint_Click(object sender, EventArgs e)
        {
            DevUtil.DevPrint(gcTunnel, "巷道信息报表");
        }

        /// <summary>
        ///     刷新按钮事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tsBtnRefresh_Click(object sender, EventArgs e)
        {
            RefreshData();
        }

        private void btnMap_Click(object sender, EventArgs e)
        {
            // 获取已选择明细行的索引
            var pLayer = DataEditCommon.GetLayerByName(DataEditCommon.g_pMap, LayerNames.LAYER_ALIAS_MR_TUNNEL);
            if (pLayer == null)
            {
                MessageBox.Show(@"未发现巷道全图层！");
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
                    str = "HdId='" + bid + "'";
                //else
                //    str += " or HdId='" + bid + "'";
            }
            //}
            List<IFeature> list = MyMapHelp.FindFeatureListByWhereClause(pFeatureLayer, str);
            if (list.Count > 0)
            {
                MyMapHelp.Jump(MyMapHelp.GetGeoFromFeature(list));
                DataEditCommon.g_pMap.ClearSelection();
                foreach (var t in list)
                {
                    DataEditCommon.g_pMap.SelectFeature(pLayer, t);
                }
                WindowState = FormWindowState.Normal;
                Location = DataEditCommon.g_axTocControl.Location;
                Width = DataEditCommon.g_axTocControl.Width;
                Height = DataEditCommon.g_axTocControl.Height;
                DataEditCommon.g_pMyMapCtrl.ActiveView.PartialRefresh(esriViewDrawPhase.esriViewGeoSelection, null,
                    DataEditCommon.g_pAxMapControl.Extent);
            }
            else
            {
                Alert.alert("图元丢失");
            }
        }

        private void gridView1_CustomColumnDisplayText(object sender, DevExpress.XtraGrid.Views.Base.CustomColumnDisplayTextEventArgs e)
        {
            if (e.Column.FieldName == "TunnelType")
            {
                switch (e.DisplayText)
                {
                    case "OTHER":
                        e.DisplayText = "其他";
                        break;
                    case "TUNNELLING":
                        e.DisplayText = "掘进巷道";
                        break;
                    case "STOPING_OTHER":
                        e.DisplayText = "回采面其他关联巷道";
                        break;
                    case "STOPING_QY":
                        e.DisplayText = "切眼";
                        break;
                    case "STOPING_FY":
                        e.DisplayText = "辅运顺槽";
                        break;
                    case "STOPING_ZY":
                        e.DisplayText = "主运顺槽";
                        break;
                }
            }
        }
    }
}