using System;
using System.Windows.Forms;
using LibCommonControl;
using LibCommon;
using LibEntity;

namespace LibPanels
{
    public partial class ManagementInfoManagement : BaseForm
    {
        public ManagementInfoManagement()
        {
            InitializeComponent();
        }

        private void RefreshData()
        {
            gcManagement.DataSource = Management.FindAll();
        }

        /// <summary>
        /// 添加按钮事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tsBtnAdd_Click(object sender, EventArgs e)
        {
            //MineData m = new MineData();
            var m = new MineDataSimple(MainForm) { Text = new LibPanels(MineDataPanelName.Management).panelFormName };
            if (DialogResult.OK == m.ShowDialog())
            {
                RefreshData();
            }
        }

        /// <summary>
        ///  修改按钮事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tsBtnModify_Click(object sender, EventArgs e)
        {
            var m = new MineData(bandedGridView1.GetFocusedRow(), MainForm)
            {
                Text = new LibPanels(MineDataPanelName.Management_Change).panelFormName
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
            var management = (Management)bandedGridView1.GetFocusedRow();
            management.Delete();
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
                gcManagement.ExportToXls(saveFileDialog1.FileName);
            }
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
        /// 退出按钮事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tsBtnExit_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void tsBtnPrint_Click(object sender, EventArgs e)
        {
            DevUtil.DevPrint(gcManagement, "管理预警信息报表");
        }

        private void ManagementInfoManagement_Load(object sender, EventArgs e)
        {
            RefreshData();
        }

        private void bandedGridView1_CustomColumnDisplayText(object sender,
            DevExpress.XtraGrid.Views.Base.CustomColumnDisplayTextEventArgs e)
        {
            if (e.Column.FieldName == "IsGasErrorNotReport" || e.Column.FieldName == "IsWfNotReport" ||
                e.Column.FieldName == "IsStrgasNotDoWell" || e.Column.FieldName == "IsRwmanagementNotDoWell" ||
                e.Column.FieldName == "IsVfBrokenByPeople" || e.Column.FieldName == "IsElementPlaceNotGood" ||
                e.Column.FieldName == "IsReporterFalseData" || e.Column.FieldName == "IsDrillWrongBuild" ||
                e.Column.FieldName == "IsOpNotDoWell" || e.Column.FieldName == "IsOpErrorNotReport" ||
                e.Column.FieldName == "IsPartWindSwitchError" || e.Column.FieldName == "IsSafeCtrlUninstall" ||
                e.Column.FieldName == "IsCtrlStop" || e.Column.FieldName == "IsGasNotDowWell" ||
                e.Column.FieldName == "IsMineNoChecker" || e.Column.FieldName == "IsDrillNotDoWell")
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
