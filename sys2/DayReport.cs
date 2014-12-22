using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace _2.MiningScheduling
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
            this.Close();
        }
    }
}
