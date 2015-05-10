using System;
using System.Windows.Forms;
using LibCommon;

namespace LibCommonForm
{
    public partial class UserInformationInput : Form
    {
        public UserInformationInput()
        {
            InitializeComponent();
            FormDefaultPropertiesSetter.SetEnteringFormDefaultProperties(this, LibFormTitles.USER_INFO_MANMAGEMENT_ADD);
        }

        private void UserInformationInput_Load(object sender, EventArgs e)
        {

        }

        private void buttonOK_Click(object sender, EventArgs e)
        {

        }

        private void buttonCancel_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
