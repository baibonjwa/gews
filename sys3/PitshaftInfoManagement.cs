using System;
using System.Collections.Generic;
using System.Windows.Forms;
using ESRI.ArcGIS.Carto;
using GIS.Common;
using LibCommon;
using LibEntity;

namespace sys3
{
    public partial class PitshaftInfoManagement : Form
    {
        public PitshaftInfoManagement()
        {
            InitializeComponent();

            // 设置窗体默认属性
            FormDefaultPropertiesSetter.SetManagementFormDefaultProperties(this, Const_GM.MANAGE_PITSHAFT_INFO);

        }


        private void RefreshData()
        {
            gcPitshaft.DataSource = Pitshaft.FindAll();
        }


        /// <summary>
        /// 添加（必须实装）
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnAdd_Click(object sender, EventArgs e)
        {
            var m = new PitshaftInfoEntering();
            if (DialogResult.OK == m.ShowDialog())
            {
                RefreshData();
            }
        }

        /// <summary>
        /// 修改（必须实装）
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnUpdate_Click(object sender, EventArgs e)
        {
            var m = new PitshaftInfoEntering(((Pitshaft)gridView1.GetFocusedRow()).PitshaftId.ToString(), Const_GM.UPDATE_PITSHAFT_INFO);
            if (DialogResult.OK == m.ShowDialog())
            {
                RefreshData();
            }
        }

        /// <summary>
        /// 删除按钮（必须实装）
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (Alert.confirm(Const_GM.DEL_CONFIRM_MSG_PITSHAFT))
            {
                var pitshaft = (Pitshaft)gridView1.GetFocusedRow();
                DeleteJintTongByBID(new[] { pitshaft.BindingId });
                pitshaft.Delete();
                RefreshData();
            }
        }

        #region 删除井筒图元

        /// <summary>
        /// 根据井筒绑定ID删除井筒图元
        /// </summary>
        /// <param name="sPitshaftBIDArray">要删除井筒的绑定ID</param>
        private void DeleteJintTongByBID(string[] sPitshaftBIDArray)
        {
            if (sPitshaftBIDArray.Length == 0) return;

            //1.获得当前编辑图层
            DrawSpecialCommon drawspecial = new DrawSpecialCommon();
            string sLayerAliasName = GIS.LayerNames.DEFALUT_JINGTONG;//“默认_井筒”图层
            IFeatureLayer featureLayer = drawspecial.GetFeatureLayerByName(sLayerAliasName);
            if (featureLayer == null)
            {
                MessageBox.Show("未找到" + sLayerAliasName + "图层,无法删除井筒图元。");
                return;
            }

            //2.删除井筒图元
            string sPitshaftBID = "";
            for (int i = 0; i < sPitshaftBIDArray.Length; i++)
            {
                sPitshaftBID = sPitshaftBIDArray[i];

                DataEditCommon.DeleteFeatureByBId(featureLayer, sPitshaftBID);
            }
        }
        #endregion

        /// <summary>
        /// 退出
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnExit_Click(object sender, EventArgs e)
        {
            // 关闭窗口
            this.Close();
        }

        /// <summary>
        /// 导出
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tsBtnExport_Click(object sender, EventArgs e)
        {

        }

        /// <summary>
        /// 打印
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tsBtnPrint_Click(object sender, EventArgs e)
        {

        }

        /// <summary>
        /// 刷新
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnRefresh_Click(object sender, EventArgs e)
        {

        }


        /// <summary>
        /// 图显按钮事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnMap_Click(object sender, EventArgs e)
        {
            string bid = ((Pitshaft)gridView1.GetFocusedRow()).BindingId;
            ILayer pLayer = GIS.Common.DataEditCommon.GetLayerByName(GIS.Common.DataEditCommon.g_pMap, GIS.LayerNames.DEFALUT_JINGTONG);
            if (pLayer == null)
            {
                MessageBox.Show("未发现井筒图层！");
                return;
            }
            IFeatureLayer pFeatureLayer = (IFeatureLayer)pLayer;
            string str = "";
            //for (int i = 0; i < iSelIdxsArr.Length; i++)
            //{
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
                GIS.Common.DataEditCommon.g_pMap.ClearSelection();
                for (int i = 0; i < list.Count; i++)
                {
                    GIS.Common.DataEditCommon.g_pMap.SelectFeature(pLayer, list[i]);
                }
                this.WindowState = FormWindowState.Normal;
                this.Location = GIS.Common.DataEditCommon.g_axTocControl.Location;
                this.Width = GIS.Common.DataEditCommon.g_axTocControl.Width;
                this.Height = GIS.Common.DataEditCommon.g_axTocControl.Height;
                GIS.Common.DataEditCommon.g_pMyMapCtrl.ActiveView.PartialRefresh(esriViewDrawPhase.esriViewGeoSelection, null, GIS.Common.DataEditCommon.g_pAxMapControl.Extent);
            }
            else
            {
                Alert.alert("图元丢失");
            }
        }
    }
}
