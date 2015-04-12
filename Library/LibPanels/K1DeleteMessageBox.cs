using System;
using System.Windows.Forms;

namespace LibPanels
{
    public partial class K1DeleteMessageBox : Form
    {
        public K1DeleteMessageBox()
        {
            InitializeComponent();
        }

        private void btnDeleteAll_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void btnDeleteSelected_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
