using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace _1.GasEmission
{
    public partial class MoniroSettings : Form
    {
        MainFormGe mainWin;
        public MoniroSettings(MainFormGe win)
        {
            InitializeComponent();
            mainWin = win;
            tbRedThreshold.Text = win.RedDataThreshold.ToString();
            tbYellowThreshold.Text = win.YellowDataThreshold.ToString();
            tbDataCountPerFrame.Text = win.DataCountPerFrame.ToString();
            tbBadDataThreshold.Text = win.BadDataThreshold.ToString();
            _cmbFrequency.Text = win.UpdateFrequency.ToString();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            FileProperties fp = new FileProperties(mainWin.ConfigFileName);
            fp.set("redthreshold", tbRedThreshold.Text);
            fp.set("yellowthreshold", tbYellowThreshold.Text);
            fp.set("countperframe", tbDataCountPerFrame.Text);
            fp.set("baddatathreshold", tbBadDataThreshold.Text);
            fp.set("updatefrequency", _cmbFrequency.SelectedItem);
            fp.Save();

            int iValue;
            int.TryParse(tbDataCountPerFrame.Text, out iValue);
            mainWin.DataCountPerFrame = iValue;
            int.TryParse(_cmbFrequency.Text, out iValue);
            mainWin.UpdateFrequency = iValue;

            double dValue;
            double.TryParse(tbYellowThreshold.Text, out dValue);
            mainWin.YellowDataThreshold = dValue;

            double.TryParse(tbRedThreshold.Text, out dValue);
            mainWin.RedDataThreshold = dValue;

            double.TryParse(tbBadDataThreshold.Text,out dValue);
            mainWin.BadDataThreshold = dValue;

            this.Close();
        }
    }
}
