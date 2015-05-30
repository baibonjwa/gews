using System;
using System.Windows.Forms;
using LibCommon;

namespace LibCommonForm
{

    public partial class UserInformationManagement : Form
    {
        public UserInformationManagement()
        {
            InitializeComponent();
            FormDefaultPropertiesSetter.SetManagementFormDefaultProperties(this, LibFormTitles.USER_INFO_MANMAGEMENT);
        }

        private void tsBtnAdd_Click(object sender, EventArgs e)
        {
            UserInformationInput ui = new UserInformationInput();
            ui.ShowDialog();
        }

        private void UserInformationManagement_Load(object sender, EventArgs e)
        {

        }

        private void tsBtnExit_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void tsBtnRefresh_Click(object sender, EventArgs e)
        {

        }

        private void tsBtnDel_Click(object sender, EventArgs e)
        {

        }

        private void tsBtnModify_Click(object sender, EventArgs e)
        {

        }

        private void tsBtnPrint_Click(object sender, EventArgs e)
        {

        }

        private void tsBtnExport_Click(object sender, EventArgs e)
        {

        }
    }
}
