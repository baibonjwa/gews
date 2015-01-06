using System;
using System.Windows.Forms;

namespace sys2
{
    public partial class DayReport : Form
    {
        public DayReport()
        {
            InitializeComponent();
        }

        private void btnCancle_Click(object sender, EventArgs e)
        {
            //关闭窗体
            Close();
        }
    }
}
