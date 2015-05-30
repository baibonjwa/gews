using System;
using System.Windows.Forms;
using LibCommon;

namespace _5.WarningManagement
{
    public partial class PreWarningResultManagement : Form
    {
        public PreWarningResultManagement()
        {
            InitializeComponent();
        }

        private void btnPrint_Click(object sender, EventArgs e)
        {
            FilePrint.CommonPrint(fpRules, 0);
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            //关闭窗体
            Close();
        }
    }
}