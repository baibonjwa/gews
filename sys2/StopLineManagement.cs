using System;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using ESRI.ArcGIS.Carto;
using GIS;
using GIS.Common;
using GIS.SpecialGraphic;
using LibCommon;
using LibEntity;

namespace sys2
{
    public partial class StopLineManagement : XtraForm
    {
        /// <summary>
        ///     构造方法
        /// </summary>
        public StopLineManagement()
        {
            InitializeComponent();

            //窗体属性设置
            FormDefaultPropertiesSetter.SetManagementFormDefaultProperties(this, Const_MS.STOP_LINE_MANAGEMENT);
        }

        private void RefreshData()
        {
            gcStopLine.DataSource = StopLine.FindAll();
        }

        /// <summary>
        ///     初始化
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TunnelInfoManagement_Load(object sender, EventArgs e)
        {
            RefreshData();
        }

        /// <summary>
        ///     添加按钮事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tsBtnAdd_Click(object sender, EventArgs e)
        {
            var d = new StopLineEntering();
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
            var d = new StopLineEntering((StopLine) gridView1.GetFocusedRow());
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
            if (!Alert.confirm(Const.DEL_CONFIRM_MSG)) return;
            var featureLayer = GetStopLineFeatureLayer();
            //删除操作
            var stopLine = (StopLine) gridView1.GetFocusedRow();
            if (featureLayer != null)
                DrawStopLine.DeleteLineFeature(featureLayer, stopLine.BindingId); //删除对应的停采线要素
            stopLine.Delete();
            RefreshData();
        }

        /// <summary>
        ///     获取停采线图层
        /// </summary>
        /// <returns>矢量图层</returns>
        private IFeatureLayer GetStopLineFeatureLayer()
        {
            //找到图层
            const string layerName = LayerNames.STOP_LINE; //“停采线”图层
            var drawSpecialCom = new DrawSpecialCommon();
            var featureLayer = drawSpecialCom.GetFeatureLayerByName(layerName);
            return featureLayer;
        }

        /// <summary>
        ///     退出按钮事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tsBtnExit_Click(object sender, EventArgs e)
        {
            //关闭窗体
            Close();
        }

        /// <summary>
        ///     刷新按钮事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tsBtnRefresh_Click(object sender, EventArgs e)
        {
            //绑定数据
            RefreshData();
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
                gcStopLine.ExportToXls(saveFileDialog1.FileName);
            }
        }

        /// <summary>
        ///     打印按钮事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tsBtnPrint_Click(object sender, EventArgs e)
        {
            DevUtil.DevPrint(gcStopLine, "停采线信息报表");
        }

        /// <summary>
        ///     图显按钮事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnMap_Click(object sender, EventArgs e)
        {
            var pLayer = DataEditCommon.GetLayerByName(DataEditCommon.g_pMap, LayerNames.STOP_LINE);
            if (pLayer == null)
            {
                MessageBox.Show(@"未发现停采线图层！");
                return;
            }
            var pFeatureLayer = (IFeatureLayer) pLayer;
            var str = "";

            var bid = ((StopLine) gridView1.GetFocusedRow()).BindingId;
            if (bid != "")
            {
                if (true)
                    str = "bid='" + bid + "'";
                //else
                //    str += " or bid='" + bid + "'";
            }
            var list = MyMapHelp.FindFeatureListByWhereClause(pFeatureLayer, str);
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
    }
}