// ******************************************************************
// 概  述：井下数据瓦斯信息管理
// 作  者：宋英杰
// 创建日期：2014/3/11
// 版本号：V1.0
// 版本信息：
// V1.0 新建
// ******************************************************************
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using LibCommonControl;
using LibBusiness;
using LibCommon;
using LibEntity;
using LibCommonForm;

namespace LibPanels
{
    public partial class GeologicStructureInfoManagement : Form
    {

        /// <summary>
        /// 构造方法
        /// </summary>
        public GeologicStructureInfoManagement()
        {
            InitializeComponent();
        }

        private void RefreshData()
        {
            gcGeologicStructure.DataSource = GeologicStructure.FindAll();
        }


        /// <summary>
        /// 添加按钮事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tsBtnAdd_Click(object sender, EventArgs e)
        {
            //MineData m = new MineData();
            var m = new MineDataSimple()
            {
                Text = new LibPanels(MineDataPanelName.GeologicStructure).panelFormName
            };
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
            if (bandedGridView1.GetFocusedRow() == null)
            {
                Alert.alert("请选择要修改的信息");
                return;
            }
            var m = new MineDataSimple((GeologicStructure)bandedGridView1.GetFocusedRow())
            {
                Text = new LibPanels(MineDataPanelName.GeologicStructure_Change).panelFormName
            };
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
            if (!Alert.confirm("确认删除数据吗？")) return;
            var geologicStructure = (GeologicStructure)bandedGridView1.GetFocusedRow();
            geologicStructure.Delete();
            RefreshData();
        }

        /// <summary>
        /// 退出按钮事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tsBtnExit_Click(object sender, EventArgs e)
        {
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
                gcGeologicStructure.ExportToXls(saveFileDialog1.FileName);
            }
        }

        private void GeologicStructureInfoManagement_Load(object sender, EventArgs e)
        {
            RefreshData();
        }

        private void tsBtnPrint_Click(object sender, EventArgs e)
        {
            DevUtil.DevPrint(gcGeologicStructure, "地质构造预警信息报表");
        }

        private void bandedGridView1_CustomColumnDisplayText(object sender, DevExpress.XtraGrid.Views.Base.CustomColumnDisplayTextEventArgs e)
        {
            if (e.Column.FieldName == "NoPlanStructure" || e.Column.FieldName == "PassedStructureRuleInvalid" ||
                e.Column.FieldName == "YellowRuleInvalid" || e.Column.FieldName == "RoofBroken" ||
                e.Column.FieldName == "CoalSeamSoft" || e.Column.FieldName == "CoalSeamBranch" ||
                e.Column.FieldName == "RoofChange" || e.Column.FieldName == "GangueDisappear" ||
                e.Column.FieldName == "GangueLocationChange")
            {
                switch (e.DisplayText)
                {
                    case "0":
                        e.DisplayText = "否";
                        break;
                    case "1":
                        e.DisplayText = "是";
                        break;
                }
            }
        }
    }
}
