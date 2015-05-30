using System;
using System.Windows.Forms;
using LibCommon;
using LibEntity;

namespace LibPanels
{
    public partial class K1ValueManagement : Form
    {
        /// <summary>
        /// 构造方法
        /// </summary>
        public K1ValueManagement()
        {
            InitializeComponent();

            FormDefaultPropertiesSetter.SetManagementFormDefaultProperties(this, Const_OP.K1_VALUE_MANAGEMENT);
        }

        private void RefreshData()
        {
            gcK1AndSValue.DataSource = K1Value.FindAll();
        }

        /// <summary>
        /// 初始化
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void K1ValueManagement_Load(object sender, EventArgs e)
        {
            RefreshData();
        }

        /// <summary>
        /// 添加按钮事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnAdd_Click(object sender, EventArgs e)
        {
            var k1Value = new K1ValueEntering();
            //添加成功
            if (DialogResult.OK == k1Value.ShowDialog())
            {
                RefreshData();
            }
        }

        /// <summary>
        /// 修改按钮事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnUpdate_Click(object sender, EventArgs e)
        {
            if (gridView1.GetFocusedRow() == null)
            {
                Alert.alert("请选择要修改的信息");
                return;
            }
            K1ValueEntering k1Value = new K1ValueEntering((K1Value)gridView1.GetFocusedRow());
            if (DialogResult.OK == k1Value.ShowDialog())
            {
                RefreshData();
            }
        }

        /// <summary>
        /// 删除按钮事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (!Alert.confirm("确认删除数据吗？")) return;
            var k1Value = (K1Value)gridView1.GetFocusedRow();
            k1Value.Delete();
            RefreshData();
        }

        /// <summary>
        /// 刷新
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnRefresh_Click(object sender, EventArgs e)
        {
            RefreshData();
        }

        /// <summary>
        /// 退出
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnExit_Click(object sender, EventArgs e)
        {
            Close();
        }

        /// <summary>
        /// 打印按钮事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tsBtnPrint_Click(object sender, EventArgs e)
        {
            DevUtil.DevPrint(gcK1AndSValue, "K1/S值信息报表");
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
                gcK1AndSValue.ExportToXls(saveFileDialog1.FileName);
            }
        }

    }
}
