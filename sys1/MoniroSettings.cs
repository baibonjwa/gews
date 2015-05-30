using System;
using System.Windows.Forms;

namespace sys1
{
    public partial class MoniroSettings : Form
    {
        private readonly MainFormGe mainWin;

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
            Close();
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            var fp = new FileProperties(mainWin.ConfigFileName);
            fp.Set("redthreshold", tbRedThreshold.Text);
            fp.Set("yellowthreshold", tbYellowThreshold.Text);
            fp.Set("countperframe", tbDataCountPerFrame.Text);
            fp.Set("baddatathreshold", tbBadDataThreshold.Text);
            fp.Set("updatefrequency", _cmbFrequency.SelectedItem);
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

            double.TryParse(tbBadDataThreshold.Text, out dValue);
            mainWin.BadDataThreshold = dValue;

            Close();
        }
    }
}