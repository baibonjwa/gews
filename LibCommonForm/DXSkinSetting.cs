using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using LibCommon;

namespace LibCommonForm
{
    public partial class DXSkinSetting : Form
    {
        public DXSkinSetting()
        {
            InitializeComponent();
        }

        private void DXSkinSetting_Load(object sender, EventArgs e)
        {
            string[] skinNames = DXSkineNames.GetDXSkinNames();
            int cnt = skinNames.Length;
            for (int i = 0; i < cnt; i++)
            {
                cboSkinNames.Items.Add(skinNames[i]);
            }
        }

        private void cboSkinNames_SelectedIndexChanged(object sender, EventArgs e)
        {
            DXSeting.SetFormSkin(cboSkinNames.Text);
        }
    }
}
