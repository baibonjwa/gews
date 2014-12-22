// ******************************************************************
// 概  述：用户信息录入
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

namespace LibCommonForm
{
    public partial class UserInformationInput : Form
    {
        //ManageDataBase database = new ManageDataBase(DATABASE_TYPE.WarningManagementDB);
        public UserInformationInput()
        {
            InitializeComponent();
            LibCommon.FormDefaultPropertiesSetter.SetEnteringFormDefaultProperties(this, LibCommon.LibFormTitles.USER_INFO_MANMAGEMENT_ADD);
        }

        private void UserInformationInput_Load(object sender, EventArgs e)
        {
            _cboPromission.SelectedIndex = 0;
            GetDepartmentName();
            GetGroupName();
        }
        private void GetDepartmentName()
        {
            //string sql = "select distinct DEPT_NAME from T_DEPT_INFO_MANAGEMENT";
            //if (database.ReturnDS(sql).Tables[0]!=null)
            //{
            //    int n = database.ReturnDS(sql).Tables[0].Rows.Count;
            //    for (int i = 0; i < n;i++ )
            //    {
            //        _cboDepartment.Items.Add(database.ReturnDS(sql).Tables[0].Rows[i][0].ToString());
            //    }
            //    _cboDepartment.SelectedIndex = 0;
            //}            
            string[] deptNames=UserInformationManagementBLL.sqlGetDepartmentName();
              foreach(string str in deptNames)
              {
                  _cboDepartment.Items.Add(str);
              }
              if (_cboDepartment.Items.Count>0)
            {
                _cboDepartment.SelectedIndex = 0;
            }
        }

        private void GetGroupName()
        {
            //string sql = "select distinct USER_GROUP_NAME from T_USER_GROUP_INFO_MANAGEMENT";
            //if (database.ReturnDS(sql).Tables[0] != null)
            //{
            //    int n = database.ReturnDS(sql).Tables[0].Rows.Count;
            //    for (int i = 0; i < n; i++)
            //    {
            //        _cboGroup.Items.Add(database.ReturnDS(sql).Tables[0].Rows[i][0].ToString());
            //    }
            //    _cboGroup.SelectedIndex = 0;
            //}
            string[] groupName = UserInformationManagementBLL.sqlGetUserGroupName();
            foreach (string str in groupName)
            {
                _cboGroup.Items.Add(str);
            }
            if (_cboGroup.Items.Count > 0)
            {
                _cboGroup.SelectedIndex = 0;
            }
        }

        private void buttonOK_Click(object sender, EventArgs e)
        {
            string name = _txtLoginName.Text.ToString().Trim();
            //DataTable dt = database.ReturnDS("select * from T_USER_INFO_MANAGEMENT where USER_LOGIN_NAME = '" + name + "'").Tables[0];
            if (name == "" || UserInformationManagementBLL.FindTheSameLoginName(name))
            {
                MessageBox.Show(@"登陆名不能为空且不能重复，请重新输入！", "提示：", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                _txtLoginName.Focus();
                return;
            }
            //string sql = "insert into T_USER_INFO_MANAGEMENT values ('" + _txtLoginName.Text.ToString().Trim() + "','"
            //    + _txtPassWord.Text.ToString().Trim() + "','"
            //    + _cboGroup.Text.ToString().Trim() + "','"
            //    + _cboDepartment.Text.ToString().Trim() + "','"
            //    + _txtName.Text.ToString().Trim() + "','"
            //    + _txtEmail.Text.ToString().Trim() + "','"
            //    + _txtTel.Text.ToString().Trim() + "','"
            //    + _txtPhoneNumber.Text.ToString().Trim() + "','"
            //    + _rtxtRemark.Text.ToString().Trim() + "','"
            //    + _cboPromission.Text.ToString().Trim() + "')";
            //database.OperateDB(sql);
            UserInformation ent = new UserInformation();
            ent.LoginName = _txtLoginName.Text.ToString().Trim();
            ent.PassWord = _txtPassWord.Text.ToString().Trim();
            ent.Group = _cboGroup.Text.ToString().Trim();
            ent.Department = _cboDepartment.Text.ToString().Trim();
            ent.Name = _txtName.Text.ToString().Trim();
            ent.Email = _txtEmail.Text.ToString().Trim();
            ent.Tel = _txtTel.Text.ToString().Trim();
            ent.Phone = _txtPhoneNumber.Text.ToString().Trim();
            ent.Remark = _rtxtRemark.Text.ToString().Trim();
            ent.Permission = _cboPromission.Text.ToString().Trim();
            //添加用户信息实体到数据库中
            UserInformationManagementBLL.InsertRecordIntoTableUserInformation(ent);
            //判断是否初次登陆
            if (LibCommon.Const.FIRST_TIME_LOGIN)
            {//记录新注册的用户帐号密码
                LibCommon.Const.FIRST_LOGIN_NAME = ent.LoginName;
                LibCommon.Const.FIRST_LOGIN_PASSWORD = ent.PassWord;
                if (ent.Permission=="普通用户")
                {
                    LibCommon.Const.FIRST_LOGIN_PERMISSION = LibCommon.Permission.普通用户.ToString();
                }
                if (ent.Permission == "管理员")
                {
                    LibCommon.Const.FIRST_LOGIN_PERMISSION = LibCommon.Permission.管理员.ToString();
                }
                this.Close();
                return;
            }

            if (MessageBox.Show(@"添加新用户成功，是否继续添加？", "提示：", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                _txtLoginName.Text = "";
                _txtLoginName.Focus();
                _txtPassWord.Text = "";
                _txtName.Text = "";
                _txtEmail.Text = "";
                _txtTel.Text = "";
                _txtPhoneNumber.Text = "";
                _rtxtRemark.Text = "";
            }
            else
            {
                this.Close();
            }            
        }

        private void buttonCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
