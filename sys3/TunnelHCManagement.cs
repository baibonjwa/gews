// ******************************************************************
// 概  述：回采巷道信息管理
// 作  者：宋英杰
// 创建日期：2014/3/11
// 版本号：V1.0
// 版本信息：
// V1.0 新建
// ******************************************************************

using System;
using System.Collections.Generic;
using System.Data;
using System.Windows.Forms;
using ESRI.ArcGIS.Geodatabase;
using ESRI.ArcGIS.Geometry;
using GIS.HdProc;
using LibBusiness;
using LibCommon;
using LibCommonControl;
using LibEntity;
using LibEntity.Domain;

namespace sys3
{
    public partial class TunnelHcManagement : Form
    {
        /// <summary>
        /// 构造方法
        /// </summary>
        public TunnelHcManagement()
        {
            InitializeComponent();

            //窗体属性设置
            FormDefaultPropertiesSetter.SetManagementFormDefaultProperties(this, Const_GM.TUNNEL_HC_MANAGEMENT);
        }

        private void RefreshData()
        {
            gcTunnelHc.DataSource = WorkingFaceHc.FindAll();
        }


        /// <summary>
        /// 初始化
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TunnelHCManagement_Load(object sender, EventArgs e)
        {
            RefreshData();
        }

        /// <summary>
        /// 添加按钮响应
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tsBtnAdd_Click(object sender, EventArgs e)
        {
            //TunnelHCEntering tunnelHCForm = new TunnelHCEntering();
            var tunnelHcForm = new TunnelHcEntering();

            if (DialogResult.OK == tunnelHcForm.ShowDialog())
            {
                RefreshData();
            }
        }

        /// <summary>
        /// 修改按钮响应
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tsBtnModify_Click(object sender, EventArgs e)
        {
            var tunnelHcForm = new TunnelHcEntering(((WorkingFaceHc)gridView1.GetFocusedRow()).WorkingFace);
            if (DialogResult.OK == tunnelHcForm.ShowDialog())
            {
                RefreshData();
            }
        }

        /// <summary>
        /// 删除按钮响应
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tsBtnDel_Click(object sender, EventArgs e)
        {
            if (!Alert.confirm(Const.DEL_CONFIRM_MSG)) return;
            var tunnelHcEntity = (WorkingFaceHc)gridView1.GetFocusedRow();
            tunnelHcEntity.Delete();
            DelHcjc(tunnelHcEntity.TunnelZy.TunnelId, tunnelHcEntity.TunnelFy.TunnelId);
            RefreshData();
        }

        /// <summary>
        /// 回采删除信息
        /// </summary>
        /// <param name="hd1"></param>
        /// <param name="hd2"></param>
        private void DelHcjc(int hd1, int hd2)
        {
            var hdids = new Dictionary<string, string>();
            hdids["HdId"] = hd1 + "_" + hd2;
            var selcjqs = Global.commonclss.SearchFeaturesByGeoAndText(Global.hcqlyr, hdids);
            if (selcjqs.Count > 0)
            {
                foreach (Tuple<IFeature, IGeometry, Dictionary<string, string>> t in selcjqs)
                {
                    IFeature fea = t.Item1;
                    Global.commonclss.DelFeature(Global.hcqlyr, fea);
                }
            }
        }

        /// <summary>
        /// 退出
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tsBtnExit_Click(object sender, EventArgs e)
        {
            //关闭窗体
            Close();
        }

        /// <summary>
        /// 刷新
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tsBtnRefresh_Click(object sender, EventArgs e)
        {
            RefreshData();
        }

        /// <summary>
        /// 导出
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tsBtnExport_Click(object sender, EventArgs e)
        {
            if (saveFileDialog1.ShowDialog() == DialogResult.OK)
            {
                gcTunnelHc.ExportToXls(saveFileDialog1.FileName);
            }
        }

        /// <summary>
        /// 打印
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tsBtnPrint_Click(object sender, EventArgs e)
        {
            DevUtil.DevPrint(gcTunnelHc, "回采面信息报表");
        }
    }
}
