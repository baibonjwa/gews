// ******************************************************************
// 概  述：用户登录信息录入
// 作  者：秦凯
// 创建日期：2014/03/10
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
using LibBusiness;
using LibEntity;
using LibCommon;

namespace LibCommonForm
{
    public partial class UserLoginInformationInput : Form
    {
        //定义字符串，表达添加或修改
        string _strIsAddOrModify = "add";
        //需要修改的实体
        UserLoginInformationEnt _needModifyEnt = new UserLoginInformationEnt();

        /// <summary>
        /// 构造函数
        /// </summary>
        public UserLoginInformationInput(UserLoginInformationEnt ent,bool isAddNewUser)
        {
            InitializeComponent();

            //旧登录名
            _txtLoginName.Text = ent.LoginName;
            //旧密码
            _txtPassWord.Text = ent.PassWord;

            //Defult
            _strIsAddOrModify = "add";
            //设置添加窗体格式
            LibCommon.FormDefaultPropertiesSetter.SetEnteringFormDefaultProperties(this, LibCommon.LibFormTitles.USER_LOGIN_INFO_ADD);

            //添加用户组名称
            SetcboGroupNameValue();
            //添加用户权限类型
            SetcboPermissionValue();
        }

        /// <summary>
        /// Defult
        /// </summary>
        public UserLoginInformationInput()
        {
            InitializeComponent();            
        }

        /// <summary>
        /// 构造函数,传入实体,用以区别添加/修改
        /// </summary>
        /// <param name="ent"></param>
        public UserLoginInformationInput(UserLoginInformationEnt ent)
        {
            //传出需要修改的实体
            _needModifyEnt = ent;
            InitializeComponent();
            //添加用户组名称
            SetcboGroupNameValue();
            //添加用户权限类型
            SetcboPermissionValue();

            //通过传入实体的值，判断是添加、修改
            if (ent != null)
            {            
                //旧登录名
                _txtLoginName.Text = ent.LoginName;
                //旧密码
                _txtPassWord.Text = ent.PassWord;
                //旧权限
                if (_cboPermission.Items.Count!=0)
                {
                    _cboPermission.SelectedIndex = GetPermissionIndex(ent.Permission);
                }
                //旧用户组
                if (_cboGroup.Items .Count!= 0)
                {
                    _cboGroup.SelectedIndex = GetGroupIndex(ent.GroupName);
                }
                //旧备注
                _rtxtRemark.Text = ent.Remarks;

                //标志字符
                _strIsAddOrModify = "modify";
                //设置修改窗体格式
                LibCommon.FormDefaultPropertiesSetter.SetEnteringFormDefaultProperties(this, LibCommon.LibFormTitles.USER_LOGIN_INFO_MOD);                
            }
            else
            {
                _strIsAddOrModify = "add";
                //设置添加窗体格式
                LibCommon.FormDefaultPropertiesSetter.SetEnteringFormDefaultProperties(this, LibCommon.LibFormTitles.USER_LOGIN_INFO_ADD);
            }
        }

        /// <summary>
        /// 确定
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonOK_Click(object sender, EventArgs e)
        {
            //登录名
            string loginname = _txtLoginName.Text;
            //密码
            string password = _txtPassWord.Text;
            //所属用户组
            string groupname = _cboGroup.Text;
            //权限
            string permission = _cboPermission.Text;
            //备注
            string remarks=_rtxtRemark.Text;

            //用户登录名不能包含特殊字符且不能为空
            //查空、特殊字符
            if (LibCommon.Validator.checkSpecialCharacters(_txtLoginName.Text.ToString()) || LibCommon.Validator.IsEmpty(_txtLoginName.Text.ToString()))
            {
                Alert.alert(LibCommon.Const.LOGIN_NAME_IS_WRONG, LibCommon.Const.NOTES, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                _txtLoginName.Focus();
                return;
            }

            //添加时查重
            if (_strIsAddOrModify == "add")
            {
                UserLoginInformationEnt entAdd = LoginFormBLL.GetUserLoginInformationByLoginname(_txtLoginName.Text.ToString().Trim());
                if (entAdd != null)
                {
                    if (entAdd.LoginName == _txtLoginName.Text.ToString())
                    {
                        Alert.alert(LibCommon.Const.LOGIN_NAME_EXIST, LibCommon.Const.NOTES, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        _txtLoginName.Focus();
                        return;
                    }
                }
            }
            //修改
            else if (_strIsAddOrModify == "modify")
            {
                UserLoginInformationEnt entModify = LoginFormBLL.GetUserLoginInformationByIDAndLoginName(_needModifyEnt.ID, _txtLoginName.Text.ToString().Trim());
                if (entModify != null)
                {                
                    Alert.alert(LibCommon.Const.LOGIN_NAME_EXIST, LibCommon.Const.NOTES, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    _txtLoginName.Focus();
                    return;
                }
            }

            //密码不能包含特殊字符且不能为空
            if (LibCommon.Validator.checkSpecialCharacters(password) || LibCommon.Validator.IsEmptyOrBlank(password))
            {
                Alert.alert(LibCommon.Const.PASS_WORD_IS_WRONG, LibCommon.Const.NOTES, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                _txtPassWord.Focus();
                return;
            }

            //确认密码不能为空且必须与密码值相同
            if (_txtConfirmPassword.Text != _txtPassWord.Text)
            {
                Alert.alert(LibCommon.Const.CONFIRM_PASSWORD_IS_WRONG, LibCommon.Const.NOTES, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                _txtConfirmPassword.Focus();
                return;
            }

            //权限选择不能为空
            if (_cboPermission.SelectedItem == null)
            {
                Alert.alert(LibCommon.Const.PERMISSION_IS_WRONG, LibCommon.Const.NOTES, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                _cboPermission.Focus();
                return;
            }

            //定义用户实体
            UserLoginInformationEnt ent = new UserLoginInformationEnt();
            //登录名
            ent.LoginName = loginname;
            //密码
            ent.PassWord = password;
            //权限
            ent.Permission = permission;
            //所属用户组
            ent.GroupName = groupname;
            //备注
            ent.Remarks = remarks;
            //尚未登录系统，在插入新值时，默认为True
            ent.NaverLogin = true;
            //记住密码，在插入新值时，默认为False
            ent.SavePassWord = false;

            //添加
            if (_strIsAddOrModify=="add")
            {
                //数据库插值
                LoginFormBLL.InsertUserLoginInfoIntoTable(ent);
                UserGroupInformationManagementBLL.UpdateUserCountFromUserGroup(ent.GroupName);
            }
            //修改
            if (_strIsAddOrModify == "modify")
            {
                //数据库更新，ent代表修改的实体，参数2代表LoginName
                LoginFormBLL.UpdateUserLoginInfomation(ent, UserLoginInformationManagement._userSel[0]);
                UserGroupInformationManagementBLL.UpdateUserCountFromUserGroup(ent.GroupName);
            }

            //初次登录系统时，需要添加新用户。记录初次登录的用户名与密码，并直接输入到LoginForm中
            if (LibCommon.Const.FIRST_TIME_LOGIN)
            {
                LibCommon.Const.FIRST_LOGIN_NAME = ent.LoginName;
                LibCommon.Const.FIRST_LOGIN_PASSWORD = ent.PassWord;
                LibCommon.Const.FIRST_LOGIN_PERMISSION = ent.Permission;
                this.Close(); 
                return;
            }
            this.Close();
        }
                      
        /// <summary>
        /// 添加所有用户组名称
        /// </summary>
        private void SetcboGroupNameValue()
        {       
            //从数据库中获得所有组名称
            string[] groupName = LoginFormBLL.GetUserGroupName();
            foreach (string str in groupName)
            {
                _cboGroup.Items.Add(str);
            }
            if (_cboGroup.Items.Count > 0)
            {
                _cboGroup.SelectedIndex = 0;
            }
        }

        /// <summary>
        /// 添加所有权限名称
        /// </summary>
        private void SetcboPermissionValue()
        {
            _cboPermission.Items.Add(LibCommon.Permission.管理员.ToString());
            _cboPermission.Items.Add(LibCommon.Permission.普通用户.ToString());

            //设置默认选择值为普通用户
            if (_cboPermission.Items.Count > 0)
            {
                _cboPermission.SelectedIndex = 1;
            }
        }

        /// <summary>
        /// 获取权限在权限组中的索引，用于“修改”命令中，旧值的导入
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        private int GetPermissionIndex(string value)
        {
            int returnNumber = 0;
            foreach (object str in _cboPermission.Items)
            {
                if (str.ToString() == value)
                {
                    return returnNumber;
                }
                returnNumber++;
            }
            //若不存在该权限时,返回0
            if (_cboPermission.Items.Count == returnNumber)
            {
                return -1;
            }
            return returnNumber;
        }

        /// <summary>
        /// 获取用户在用户组中的索引，用于“修改”命令中，旧值的导入
        /// </summary>
        /// <param name="value">用户组名称</param>
        /// <returns>用户组名称在combox中的索引</returns>
        private int GetGroupIndex(string value)
        {
            int returnNumber = 0;
            foreach (object str in _cboGroup.Items)
            {
                if (str.ToString() == value)
                {
                    return returnNumber;
                }
                returnNumber++;
            }
            //若不存在该用户组时,返回0
            if (_cboGroup.Items.Count == returnNumber)
            {
                return -1;
            }
            return returnNumber;
        }             

        /// <summary>
        /// 窗体登陆事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void UserLoginInformationInput_Load(object sender, EventArgs e)
        {

        }      
    }
}
