using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using LibBusiness;
using LibEntity;
using LibCommon;
using LibCommonControl;
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
            this.Close();

        }

    }
}
