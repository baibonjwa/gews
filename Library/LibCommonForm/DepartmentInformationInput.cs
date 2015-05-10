using System;
using System.Windows.Forms;
using LibCommon;
using LibEntity;

namespace LibCommonForm
{
    public partial class DepartmentInformationInput : Form
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        public DepartmentInformationInput()
        {
            InitializeComponent();

            //设置窗体格式
            FormDefaultPropertiesSetter.SetEnteringFormDefaultProperties(this, LibFormTitles.DEPARTMENT_MANMAGEMENT_ADD);

            //设置最大长度
            _txtName.MaxLength = 50;
            _txtTel.MaxLength = 50;
            _txtEmail.MaxLength = 50;
        }

        public DepartmentInformationInput(Department department)
        {

        }

        /// <summary>
        /// 确定
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonOK_Click(object sender, EventArgs e)
        {
          
        }

        /// <summary>
        /// 退出
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonCancel_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
