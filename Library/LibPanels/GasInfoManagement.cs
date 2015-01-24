using System;
using System.Windows.Forms;
using LibCommon;
using LibCommonControl;
using LibEntity;

namespace LibPanels
{
    public partial class GasInfoManagement : BaseForm
    {

        /// <summary>
        ///     构造方法
        /// </summary>
        public GasInfoManagement(MainFrm mainFrm)
        {
            MainForm = mainFrm;
            InitializeComponent();
        }

        private void RefreshData()
        {
            gcGasData.DataSource = GasData.FindAll();
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
        ///     刷新按钮事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tsBtnRefresh_Click(object sender, EventArgs e)
        {
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
                gcGasData.ExportToXls(saveFileDialog1.FileName);
            }
        }

        private void tsBtnAdd_Click(object sender, EventArgs e)
        {
            //MineData m = new MineData();
            var m = new MineDataSimple(MainForm)
            {
                Text = new LibPanels(MineDataPanelName.GasData).panelFormName
            };
            if (DialogResult.OK == m.ShowDialog())
            {
                RefreshData();
            }
        }

        private void tsBtnModify_Click(object sender, EventArgs e)
        {
            var m = new MineData(MainForm) { Text = new LibPanels(MineDataPanelName.GasData_Change).panelFormName };

            if (DialogResult.OK == m.ShowDialog())
            {
                RefreshData();
            }
        }

        private void tsBtnDel_Click(object sender, EventArgs e)
        {
            if (!Alert.confirm("确认删除数据吗？")) return;
            var gasData = (GasData)bandedGridView1.GetFocusedRow();
            gasData.Delete();
            RefreshData();
        }

        private void tsBtnPrint_Click(object sender, EventArgs e)
        {
            DevUtil.DevPrint(gcGasData, "瓦斯预警信息报表");
        }

        private void GasInfoManagement_Load(object sender, EventArgs e)
        {
            RefreshData();
        }
    }
}