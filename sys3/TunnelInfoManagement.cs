using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Windows.Forms;
using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.Geodatabase;
using FarPoint.Win;
using FarPoint.Win.Spread;
using FarPoint.Win.Spread.CellType;
using GIS;
using GIS.Common;
using LibBusiness;
using LibCommon;
using LibCommonControl;
using LibCommonForm;
using LibEntity;

namespace sys3
{
    public partial class TunnelInfoManagement : BaseForm
    {
        //****************变量声明***************
        private readonly int[] _filterColunmIdxs;
        private readonly Hashtable _htSelIdxs = new Hashtable();
        private int _BIDIndex = 16;
        private int _iRecordCount;
        private int activeRow;
        private bool bFirst = true;
        private Cells cells;
        private int checkCount; //选择行数
        private DataSet ds = new DataSet();
        private int rowDetailStartIndex = 4;
        private int rowsCount; //数据行数
        private Tunnel tunnelEntity = new Tunnel();

        //****************************************

        /// <summary>
        ///     构造方法
        /// </summary>
        public TunnelInfoManagement(MainFrm mainFrm)
        {
            MainForm = mainFrm;
            InitializeComponent();

            FormDefaultPropertiesSetter.SetManagementFormDefaultProperties(this, Const_GM.TUNNEL_INFO_MANAGEMENT);

        }

        /// <summary>
        ///     初始化
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TunnelInfoManagement_Load(object sender, EventArgs e)
        {
            btnNoWire.BackColor = Const.NO_WIRE_TUNNEL_COLOR;
            btnWired.BackColor = Const.WIRED_TUNNEL_COLOR;
            btnTunnelJJ.BackColor = Const.JJ_TUNNEL_COLOR;
            btnTunnelHC.BackColor = Const.HC_TUNNEL_COLOR;
        }

        /// <summary>
        ///     为变量tunnelEntity赋值
        /// </summary>
        private void setTunnelEntityValue()
        {
            int searchCount = rowsCount;
            int rowDetailStartIndex = 4;
            for (int i = 0; i < rowsCount; i++)
            {
                if (cells[rowDetailStartIndex + i, 0].Value != null &&
                    (bool)cells[rowDetailStartIndex + i, 0].Value)
                {
                    //巷道编号
                    tunnelEntity.TunnelId = (int)ds.Tables[0].Rows[i][TunnelInfoDbConstNames.ID];
                    //巷道实体
                    tunnelEntity = BasicInfoManager.getInstance().getTunnelByID(tunnelEntity.TunnelId);

                    activeRow = rowDetailStartIndex + i;
                }
            }
        }

        /// <summary>
        ///     添加按钮响应
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tsBtnAdd_Click(object sender, EventArgs e)
        {
            var d = new TunnelInfoEntering(MainForm);

            if (DialogResult.OK == d.ShowDialog())
            {

            }
        }

        /// <summary>
        ///     修改按钮响应
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tsBtnModify_Click(object sender, EventArgs e)
        {


            var d = new TunnelInfoEntering(tunnelEntity.TunnelId);
            if (DialogResult.OK == d.ShowDialog())
            {

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
                bool bResult = false;
                for (int i = 0; i < rowsCount; i++)
                {
                    if (cells[rowDetailStartIndex + i, 0].Value != null &&
                        (bool)cells[rowDetailStartIndex + i, 0].Value)
                    {
                        //掘进ID
                        tunnelEntity.TunnelId = (int)ds.Tables[0].Rows[i][TunnelInfoDbConstNames.ID];
                        //巷道类型为掘进或回采巷道
                        if (tunnelEntity.WorkingFace.WorkingfaceTypeEnum == WorkingfaceTypeEnum.JJ || TunnelInfoBLL.isTunnelHC(tunnelEntity))
                        {
                            TunnelInfoBLL.deleteJJHCTunnelInfo(tunnelEntity);
                        }
                        if (Wire.FindOneByTunnelId(tunnelEntity.TunnelId) != null)
                        {
                            TunnelInfoBLL.deleteWireInfoBindingTunnelID(tunnelEntity);
                            //是否删除关联导线
                            if (Alert.confirm(Const_GM.TUNNEL_INFO_MSG_DEL_WIRE))
                            {
                                TunnelInfoBLL.deleteWireInfoBindingTunnelID(tunnelEntity);
                            }

                            //不删除时将导线重新绑定到其他巷道，默认为巷道ID=0
                        }
                        //删除巷道对应掘进日报
                        TunnelInfoBLL.deleteDayReportJJBindingTunnelID(tunnelEntity);
                        //删除巷道对应回采日报
                        TunnelInfoBLL.deleteDayReportHCBindingTunnelID(tunnelEntity);
                        //删除巷道
                        tunnelEntity.Delete();
                    }
                }
                //绑定信息
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

        }

        /// <summary>
        ///     打印按钮事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tsBtnPrint_Click(object sender, EventArgs e)
        {

        }

        /// <summary>
        ///     刷新按钮事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tsBtnRefresh_Click(object sender, EventArgs e)
        {

        }

        private void btnMap_Click(object sender, EventArgs e)
        {
            // 获取已选择明细行的索引
            string bid = "";
            ILayer pLayer = DataEditCommon.GetLayerByName(DataEditCommon.g_pMap, LayerNames.LAYER_ALIAS_MR_TUNNEL);
            if (pLayer == null)
            {
                MessageBox.Show("未发现巷道全图层！");
                return;
            }
            var pFeatureLayer = (IFeatureLayer)pLayer;
            string str = "";
            //for (int i = 0; i < iSelIdxsArr.Length; i++)
            //{
            bid = ((Tunnel)gridView1.GetFocusedRow()).BindingID;
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
                for (int i = 0; i < list.Count; i++)
                {
                    DataEditCommon.g_pMap.SelectFeature(pLayer, list[i]);
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
    }
}