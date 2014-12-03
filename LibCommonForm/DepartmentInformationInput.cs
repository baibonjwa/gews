// ******************************************************************
// 概  述：部门信息录入
// 作  者：秦凯
// 创建日期：2014/03/06
// 版本号：V1.0
// 版本信息:
// V1.0 新建
// ******************************************************************
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using LibDatabase;
using LibBusiness;
using LibEntity;
using LibCommon;

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
            LibCommon.FormDefaultPropertiesSetter.SetEnteringFormDefaultProperties(this, LibCommon.LibFormTitles.DEPARTMENT_MANMAGEMENT_ADD);

            //设置最大长度
            _txtName.MaxLength = 50;
            _txtTel.MaxLength = 50;
            _txtEmail.MaxLength = 50;
        }

        /// <summary>
        /// 确定
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonOK_Click(object sender, EventArgs e)
        {
            string name = _txtName.Text.ToString().Trim();
            string tel = _txtTel.Text.ToString().Trim();
            string email = _txtEmail.Text.ToString().Trim();
            string staff = _txtStaffCount.Text.ToString().Trim();

            //查询数据库中是否已存在该部门名称
            if (LibCommon.Validator.checkSpecialCharacters(name) ||
                LibCommon.Validator.IsEmpty(name) ||
                DepartmentInformationManagementBLL.FindDeptInformationByDeptName(name))
            {
                //部门名称不能为空,且不能重复
                Alert.alert(LibCommon.Const.DEPT_NAME_IS_WRONG, LibCommon.Const.NOTES, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                //设置焦点
                _txtName.Focus();
                return;
            }

            //验证电话格式
            if (!LibCommon.Validator.checkIsIsTelePhone(tel)&&tel!="")
            {
                Alert.alert(LibCommon.Const.DEPT_TEL_IS_WRONG, LibCommon.Const.NOTES, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                _txtTel.Focus();
                return;
            }

            //邮箱格式
            if (!LibCommon.Validator.checkIsEmailAddress(email)&&email!="")
            {
                Alert.alert(LibCommon.Const.DEPT_EMAIL_IS_WRONG, LibCommon.Const.NOTES, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                _txtEmail.Focus();
                return;
            }
           
            //定义默认的部门人数
            int staffNumber = 0;

            //部门人数可以为空,但不能为非数字
            if (!int.TryParse(_txtStaffCount.Text.ToString().Trim(), out staffNumber) && staff != "")
            {
                //提示用户部门人数不为数字,并设置焦点
                Alert.alert(LibCommon.Const.DEPT_STAFF_COUNT_IS_WRONG, LibCommon.Const.NOTES, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                _txtStaffCount.Focus();
                return;
            }

            //定义部门信息实体
            DepartmentInformationEntity ent = new DepartmentInformationEntity();
            //部门名称
            ent.Name = _txtName.Text.ToString().Trim();
            //部门邮箱
            ent.Email = _txtEmail.Text.ToString().Trim();
            //部门电话
            ent.Tel = _txtTel.Text.ToString().Trim();
            //部门人数
            ent.Staff = staffNumber.ToString();
            //备注
            ent.Remark = _rtxtRemarks.Text.ToString().Trim();

            //添加数据到数据库
            DepartmentInformationManagementBLL.InsertDeptInfoIntoTable(ent);

            this.Close();
        }

        /// <summary>
        /// 退出
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
