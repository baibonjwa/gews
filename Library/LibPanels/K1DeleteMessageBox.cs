using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace _4.OutburstPrevention
{
    public partial class K1DeleteMessageBox : Form
    {
        public K1DeleteMessageBox()
        {
            InitializeComponent();
        }

        private void btnDeleteAll_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnDeleteSelected_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {

        }
    }
}
