using System;
using System.Windows.Forms;
using LibCommon;
using LibEntity;

namespace LibPanels
{
    public partial class VentilationInfoManagement : Form
    {
        public VentilationInfoManagement()
        {
            InitializeComponent();
        }

        private void RefreshData()
        {
            gcVentilation.DataSource = Ventilation.FindAll();
        }

        /// <summary>
        /// 初始化
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MineDataManagement_Load(object sender, EventArgs e)
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
            //MineData m = new MineData();
            var m = new MineDataSimple
            {
                Text = new LibPanels(MineDataPanelName.Ventilation).panelFormName
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
            var m = new MineDataSimple(bandedGridView1.GetFocusedRow())
            {
                Text = new LibPanels(MineDataPanelName.Ventilation_Change).panelFormName
            };

            if (DialogResult.OK == m.ShowDialog())
            {
                RefreshData();
            }
        }

        /// <summary>
        /// 删除按钮事件
        /// </summary>d
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tsBtnDel_Click(object sender, EventArgs e)
        {
            if (!Alert.confirm("确认删除数据吗？")) return;
            var ventilationInfo = (Ventilation)bandedGridView1.GetFocusedRow();
            ventilationInfo.Delete();
            RefreshData();
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

        /// <summary>
        /// 导出按钮事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tsBtnExport_Click(object sender, EventArgs e)
        {
            if (saveFileDialog1.ShowDialog() == DialogResult.OK)
            {
                gcVentilation.ExportToXls(saveFileDialog1.FileName);
            }
        }

        private void tsBtnPrint_Click(object sender, EventArgs e)
        {
            DevUtil.DevPrint(gcVentilation, "通风预警信息报表");
        }
    }
}
