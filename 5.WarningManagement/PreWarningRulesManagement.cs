using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using LibBusiness;
using LibCommon;
using LibSocket;

namespace _5.WarningManagement
{
    public partial class PreWarningRulesManagement : Form
    {
        PreWarningRulesPanel _preWarningRulesPanel = new PreWarningRulesPanel();
        public PreWarningRulesManagement()
        {
            InitializeComponent();
        }
        void InitFormProperty()
        {
            _preWarningRulesPanel.MdiParent = this;
            _panel.Controls.Add(_preWarningRulesPanel);
            _preWarningRulesPanel.WindowState = FormWindowState.Maximized;
            _preWarningRulesPanel.Dock = DockStyle.Fill;
            _preWarningRulesPanel.Show();
            _preWarningRulesPanel.Activate();
        }

        private void PreWarningRulesManagement_Load(object sender, EventArgs e)
        {
            //该函数需要在Load当中调用，在构造函数当中调用显示会出问题。
            InitFormProperty();
        }



        private void btnExit_Click(object sender, EventArgs e)
        {
            //关闭窗体
            this.Close();
        }

        private void btnImport_Click(object sender, EventArgs e)
        {
            OpenFileDialog d = new OpenFileDialog();
            d.CheckFileExists = true;
            d.Filter = "Microsoft Excel 工作表.xlsx|*.xlsx";
            d.Multiselect = false;
            if (DialogResult.OK == d.ShowDialog())
            {
                string path = d.FileName;
                if (PreWarningExcelBLL.ImportExcelRules2Db(path))
                {
                    //清空数据库

                    //清空Farpoint
                    _preWarningRulesPanel.ClearFarpointCells();
                    _preWarningRulesPanel.FillFarpointCellsWithAllRules();
                    MessageBox.Show("导入预警规则成功！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    //TODO:ResetTunnelRulesMsg msg = new ResetTunnelRulesMsg(Const.INVALID_ID, this.selectTunnelSimple1.ITunnelId,
                    //UsualForecastDbConstNames.TABLE_NAME, OPERATION_TYPE.ADD, DateTime.Now);
                    //this.MainForm.SendMsg2Server(msg);
                }
            }
        }

        /// <summary>
        /// 导出
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void toolStripButton4_Click(object sender, EventArgs e)
        {
            //saveFileDialog1.ShowDialog();
            string fileName = saveFileDialog1.FileName;
            bool bResult = _preWarningRulesPanel.returnExportInfo(fileName);
            {
                //addColumn();
                if (bResult)
                {
                    Alert.alert("导出成功");
                }
                else
                {
                    Alert.alert("导出失败");
                }
            }
        }

        private void btnPrint_Click(object sender, EventArgs e)
        {
            _preWarningRulesPanel.PrintRules();
        }
    }
}
