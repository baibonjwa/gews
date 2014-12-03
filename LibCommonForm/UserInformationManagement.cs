// ******************************************************************
// 概   述：用户信息管理
// 作  者：秦  凯
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
using LibXPorperty;
using FarPoint.Win.Spread.CellType;
using System.Collections;
using LibBusiness;
using LibEntity;
using LibCommon;

namespace LibCommonForm
{
    
    public partial class UserInformationManagement : Form
    {
        //ManageDataBase database = new ManageDataBase(DATABASE_TYPE.WarningManagementDB);
        List<string> _userSel = new List<string>();
        List<string> _strGroupName = new List<string>();
        List<string> _strDepartmentName = new List<string>();

        private UserParameter _srcEnt = null;
        private XProps _props = new XProps();
        XProp _userLoginName = new XProp();
        XProp _userPassWord = new XProp();
        XProp _userGroup = new XProp();
        XProp _userDepartment = new XProp();
        XProp _userName = new XProp();
        XProp _userEmail = new XProp();
        XProp _userTel = new XProp();
        XProp _userPhone = new XProp();
        XProp _userPermission = new XProp();
        XProp _remarks = new XProp();
        List<XProp> _paramProp = new List<XProp>();

        const string CATEGORY_LOGINNAME_TITLE = "1.登陆名";
        const string CATEGORY_PASSWORD_TITLE = "2.用户密码";
        const string CATEGORY_GROUP_TYPE_TITLE = "2.所属用户组";
        const string CATEGORY_DEPARTMENT_TYPE_TITLE = "3.所属部门";
        const string CATEGORY_NAME_TYPE_TITLE = "4.姓名";
        const string CATEGORY_EMAIL_TYPE_TITLE = "5.电子邮箱";
        const string CATEGORY_TEL_TYPE_TITLE = "6.电话号码";
        const string CATEGORY_PHONE_TYPE_TITLE = "7.手机号码";
        const string CATEGORY_PERMISSION_TYPE_TITLE = "8.用户权限";
        const string CATEGORY_REMARKS_TITLE = "9.备注";//MAKR FIELD       

        class UserParameter
        {
            private string _loginname;
            private string _password;
            private string _group;
            private string _department;
            private string _name;
            private string _phone;
            private string _tel;
            private string _email;
            private string _permisson;
            private string _remark;
            private string _Id;

            public string Name
            {
                get { return _name; }
                set { _name = value; }
            }
            public string Tel
            {
                get { return _tel; }
                set { _tel = value; }
            }
            public string Email
            {
                get { return _email; }
                set { _email = value; }
            }
            public string Phone
            {
                get { return _phone; }
                set { _phone = value; }
            }
            public string LoginName
            {
                get { return _loginname; }
                set { _loginname = value; }
            }
            public string Remark
            {
                get { return _remark; }
                set { _remark = value; }
            }   
            public string PassWord
            {
                get { return _password; }
                set { _password = value; }
            }   
            public string Group
            {
                get { return _group; }
                set { _group = value; }
            }   
            public string Department
            {
                get { return _department; }
                set { _department = value; }
            }   
            public string Permission
            {
                get { return _permisson; }
                set { _permisson = value; }
            }
            public string ID
            {
                get { return _Id; }
                set { _Id = value; }
            }
            public UserParameter(string name)
            {
                _name = name;
            }
            public UserParameter(string loginname,string password,string group,string department,string name, string email, string tel, string phone, string remark, string permission)
            {
                
                _loginname = loginname;
                _group = group;
                _department = department;
                _name = name;
                _tel = tel;
                _email = email;
                _phone = phone;
                _remark = remark;
                _permisson = permission;
            }
            public UserParameter()
            {

            }
            public UserParameter DeepClone()
            {
                UserParameter cpyEnt = new UserParameter();
                cpyEnt.ID = ID; ;
                cpyEnt.Tel = Tel;
                cpyEnt.Email = Email;
                cpyEnt.Phone = Phone;
                cpyEnt.Remark = Remark;
                cpyEnt.LoginName = LoginName; ;
                cpyEnt.PassWord = PassWord;
                cpyEnt.Group = Group;
                cpyEnt.Department = Department;
                cpyEnt.Permission = Permission;
                return cpyEnt;
            }
        }

        void InitXPorps()
        {
            #region 基本信息
            //登陆名
            _userLoginName.Category = CATEGORY_LOGINNAME_TITLE;
            _userLoginName.Name = "登陆名";
            _userLoginName.Description = "用户唯一身份标识，登陆名称不能重复";
            _userLoginName.ReadOnly = true;
            _userLoginName.ProType = typeof(string);
            _userLoginName.Visible = true;
            _userLoginName.Converter = null;
            _props.Add(_userLoginName);

            //用户密码
            _userPassWord.Category = CATEGORY_LOGINNAME_TITLE;
            _userPassWord.Name = "用户密码";
            _userPassWord.Description = "用户密码若无，则无需添加";
            _userPassWord.ReadOnly = true;
            _userPassWord.ProType = typeof(string);
            _userPassWord.Visible = true;
            _userPassWord.Converter = null;
            _props.Add(_userPassWord);

            //所属用户组
            _userGroup.Category = CATEGORY_GROUP_TYPE_TITLE;
            _userGroup.Name = "所属用户组";
            _userGroup.Description = "所属用户组若无，则无需添加";
            _userGroup.ReadOnly = true;
            _userGroup.ProType = typeof(string);
            _userGroup.Visible = true;
            _userGroup.Converter = null;
            _props.Add(_userGroup);

            //所属部门
            _userDepartment.Category = CATEGORY_DEPARTMENT_TYPE_TITLE;
            _userDepartment.Name = "所属部门";
            _userDepartment.Description = "所属部门组若无，则无需添加";
            _userDepartment.ReadOnly = true;
            _userDepartment.ProType = typeof(string);
            _userDepartment.Visible = true;
            _userDepartment.Converter = null;
            _props.Add(_userDepartment);

            //姓名
            _userName.Category = CATEGORY_NAME_TYPE_TITLE;
            _userName.Name = "用户姓名";
            _userName.Description = "所属部门组若无，则无需添加";
            _userName.ReadOnly = true;
            _userName.ProType = typeof(string);
            _userName.Visible = true;
            _userName.Converter = null;
            _props.Add(_userName);

            //电子邮箱
            _userEmail.Category = CATEGORY_EMAIL_TYPE_TITLE;
            _userEmail.Name = "电子邮箱";
            _userEmail.Description = "电子邮箱若无，则无需添加";
            _userEmail.ReadOnly = true;
            _userEmail.ProType = typeof(string);
            _userEmail.Visible = true;
            _userEmail.Converter = null;
            _props.Add(_userEmail);

            //电话号码
            _userTel.Category = CATEGORY_TEL_TYPE_TITLE;
            _userTel.Name = "电话号码";
            _userTel.Description = "电话号码若无，则无需添加";
            _userTel.ReadOnly = true;
            _userTel.ProType = typeof(string);
            _userTel.Visible = true;
            _userTel.Converter = null;
            _props.Add(_userTel); 
            
            //手机号码            
            _userPhone.Category = CATEGORY_PHONE_TYPE_TITLE;
            _userPhone.Name = "手机号码";
            _userPhone.Description = "手机号码若无，则无需添加";
            _userPhone.ReadOnly = true;
            _userPhone.ProType = typeof(string);
            _userPhone.Visible = true;
            _userPhone.Converter = null;
            _props.Add(_userPhone);

            //权限
            _userPermission.Category = CATEGORY_PERMISSION_TYPE_TITLE;
            _userPermission.Name = "权限";
            _userPermission.Description = "权限若无，则无需添加";
            _userPermission.ReadOnly = true;
            _userPermission.ProType = typeof(string);
            _userPermission.Visible = true;
            _userPermission.Converter = null;
            _props.Add(_userPermission);  
                       
            //备注
            _remarks.Category = CATEGORY_REMARKS_TITLE;
            _remarks.Name = "备注";
            _remarks.Description = "备注若无，则无需添加";
            _remarks.ReadOnly = true;
            _remarks.ProType = typeof(string);
            _remarks.Visible = true;
            _remarks.Converter = null;
            _props.Add(_remarks); 
            #endregion
        }

        public UserInformationManagement()
        {
            InitializeComponent();
            LibCommon.FarpointDefaultPropertiesSetter.SetFpDefaultProperties(fpUserInfo, LibCommon.LibFormTitles.USER_INFO_MANMAGEMENT, 2);
            LibCommon.FormDefaultPropertiesSetter.SetManagementFormDefaultProperties(this, LibCommon.LibFormTitles.USER_INFO_MANMAGEMENT);
        }

        private void tsBtnAdd_Click(object sender, EventArgs e)
        {
            UserInformationInput ui = new UserInformationInput();
            ui.ShowDialog();
            //GetDepartmentInfo();
            GetUsertInfo();
        }

        private void GetUsertInfo()
        {
            //string sql = "select USER_LOGIN_NAME,USER_PASSWORD,USER_UNDER_GROUP,USER_UNDER_DEPT,USER_NAME,USER_EMAIL,USER_TEL,USER_PHONE,USER_REMARKS,USER_PERMISSION from T_USER_INFO_MANAGEMENT";
            //DataTable dt = database.ReturnDS(sql).Tables[0];
            DataTable dt = UserInformationManagementBLL.sqlGetUserInformation();
            if (dt != null)
            {
                int n = dt.Rows.Count;
                int k = dt.Columns.Count;
                for (int i = 0; i < n; i++)
                {
                    for (int j = 0; j < k; j++)
                    {
                        this.fpUserInfo.Sheets[0].Cells[i + 2, j + 1].CellType = new FarPoint.Win.Spread.CellType.TextCellType();
                        this.fpUserInfo.Sheets[0].Cells[i + 2, j + 1].Locked = true;
                        this.fpUserInfo.Sheets[0].Cells[i + 2, j + 1].Text = LibEncryptDecrypt.DWEncryptDecryptClass.DecryptString(dt.Rows[i][j].ToString());
                        this.fpUserInfo.Sheets[0].Cells[i + 2, j + 1].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Left;
                        this.fpUserInfo.Sheets[0].Cells[i + 2, j + 1].VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
                    }
                    this.fpUserInfo.Sheets[0].Cells[i + 2, 0].CellType = new FarPoint.Win.Spread.CellType.CheckBoxCellType();
                    this.fpUserInfo.Sheets[0].Cells[i + 2, 0].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
                    this.fpUserInfo.Sheets[0].Cells[i + 2, 0].VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
                }
            }
        }

        private void UserInformationManagement_Load(object sender, EventArgs e)
        {
            GetUsertInfo();
            InitXPorps();
            _userSel.Clear();
            tsBtnModify.Enabled = false;
            tsBtnDel.Enabled = false;
            //this.删除ToolStripMenuItem.Enabled = false;
            GetDepartmentName();
            GetGroupName();
        }

        private void fpUserInfo_ButtonClicked(object sender, FarPoint.Win.Spread.EditorNotifyEventArgs e)
        {
            if (this.fpUserInfo.ActiveSheet.Cells[e.Row, 1] != null)
            {
                // 判断点击的空间类型是否是.FpCheckBox)
                if (e.EditingControl is FarPoint.Win.FpCheckBox)
                {
                    FarPoint.Win.FpCheckBox fpChk = (FarPoint.Win.FpCheckBox)e.EditingControl;
                    // 判断是否被选中
                    if (fpChk.Checked)
                    {
                        if (!_userSel.Contains(this.fpUserInfo.ActiveSheet.Cells[e.Row, 1].Value.ToString()))
                        {
                            _userSel.Add(this.fpUserInfo.ActiveSheet.Cells[e.Row, 1].Value.ToString());
                        }
                    }
                    else
                    {
                        // 移除索引号
                        _userSel.Remove(this.fpUserInfo.ActiveSheet.Cells[e.Row, 1].Value.ToString());
                        // 全选/全不选checkbox设为未选中
                        this.chkSelAll.CheckedChanged -= new System.EventHandler(this.chkSelAll_CheckedChanged);
                        this.chkSelAll.Checked = false;
                        this.chkSelAll.CheckedChanged += new System.EventHandler(this.chkSelAll_CheckedChanged);
                    }
                    // 如果保存索引号件数是1，则修改按钮设为可用，否则设为不可用
                    this.tsBtnModify.Enabled = (_userSel.Count == 1) ? true : false;
                    // 删除按钮
                    this.tsBtnDel.Enabled = (_userSel.Count >= 1) ? true : false;
                    //this.删除ToolStripMenuItem.Enabled = (_userSel.Count >= 1) ? true : false;
                }
            }
        }

        private int GetTableRecordCount(string tableName)
        {
            //int n = 0;
            //string sqlCount = "select count(*) from "+tableName;
            //string sqlCount = UserInformationManagementBLL.sqlGetRecordCountFromTable(tableName);
            //DataTable dt = database.ReturnDS(sqlCount).Tables[0];
            //if (dt != null)
            //{
            //    string str = dt.Rows[0][0].ToString();
            //    int j = 0;
            //    if (int.TryParse(str, out j))
            //    {
            //        n = j;
            //    }
            //}
            return UserInformationManagementBLL.sqlGetRecordCountFromTable(tableName);
        }

        private void chkSelAll_CheckedChanged(object sender, EventArgs e)
        {
            _userSel.Clear();
            int n = GetTableRecordCount("T_USER_INFO_MANAGEMENT");
            if (chkSelAll.Checked)
            {
                for (int i = 0; i < n; i++)
                {
                    this.fpUserInfo.Sheets[0].Cells[2 + i, 0].Value = ((CheckBox)sender).Checked;
                    _userSel.Add(this.fpUserInfo.Sheets[0].Cells[2 + i, 0].Value.ToString());
                }
            }
            else
            {
                for (int i = 0; i < n; i++)
                {
                    this.fpUserInfo.Sheets[0].Cells[2 + i, 0].Value = false;
                }
            }
            this.tsBtnDel.Enabled = (_userSel.Count >= 1) ? true : false;
            //this.删除ToolStripMenuItem.Enabled = (_userSel.Count >= 1) ? true : false;
        }

        private void fpUserInfo_SelectionChanged(object sender, FarPoint.Win.Spread.SelectionChangedEventArgs e)
        {
            int n = this.fpUserInfo.ActiveSheet.ActiveCell.Row.Index;
            if (this.fpUserInfo.ActiveSheet.Cells[n, 1].Value == null)
            {
                _propInfo.Enabled = false;
            }
            else
            {
                _propInfo.Enabled = true;
            }

            if (n < 2)
            {
                return;
            }

            string loginname = "";
            string password = "";
            string group = "";
            string department = "";
            string name = "";
            string tel = "";
            string email = "";
            string phone = "";
            string permission = "";
            string remark = "";

            if (this.fpUserInfo.ActiveSheet.Cells[n, 1].Value != null)
            {
                loginname = this.fpUserInfo.ActiveSheet.Cells[n, 1].Value.ToString();
            }
            if (this.fpUserInfo.ActiveSheet.Cells[n, 2].Value != null)
            {
                password = this.fpUserInfo.ActiveSheet.Cells[n, 2].Value.ToString();
            }
            if (this.fpUserInfo.ActiveSheet.Cells[n, 3].Value != null)
            {
                group = this.fpUserInfo.ActiveSheet.Cells[n, 3].Value.ToString();
            }
            if (this.fpUserInfo.ActiveSheet.Cells[n, 4].Value != null)
            {
                department = this.fpUserInfo.ActiveSheet.Cells[n, 4].Value.ToString();
            }
            if (this.fpUserInfo.ActiveSheet.Cells[n, 5].Value != null)
            {
                name = this.fpUserInfo.ActiveSheet.Cells[n, 5].Value.ToString();
            }
            if (this.fpUserInfo.ActiveSheet.Cells[n, 6].Value != null)
            {
                email = this.fpUserInfo.ActiveSheet.Cells[n, 6].Value.ToString();
            }
            if (this.fpUserInfo.ActiveSheet.Cells[n, 7].Value != null)
            {
                tel = this.fpUserInfo.ActiveSheet.Cells[n, 7].Value.ToString();
            }
            if (this.fpUserInfo.ActiveSheet.Cells[n, 8].Value != null)
            {
                phone = this.fpUserInfo.ActiveSheet.Cells[n, 8].Value.ToString();
            }
            if (this.fpUserInfo.ActiveSheet.Cells[n, 9].Value != null)
            {
                remark = this.fpUserInfo.ActiveSheet.Cells[n, 9].Value.ToString();
            }
            if (this.fpUserInfo.ActiveSheet.Cells[n, 10].Value != null)
            {
                permission = this.fpUserInfo.ActiveSheet.Cells[n, 10].Value.ToString();
            }
            
            UserParameter d = new UserParameter(loginname, password,group,department,name,email,tel,phone,remark,permission);
            SetPropertyGridEnt(d);
        }

        private void GetDepartmentName()
        {
            _strDepartmentName.Clear();
            //string sql = "select distinct DEPT_NAME from T_DEPT_INFO_MANAGEMENT";
            //if (database.ReturnDS(sql).Tables[0] != null)
            //{
            //    int n = database.ReturnDS(sql).Tables[0].Rows.Count;
            //    for (int i = 0; i < n; i++)
            //    {
            //        _strDepartmentName.Add(database.ReturnDS(sql).Tables[0].Rows[i][0].ToString());
            //    }
            //}
            string[] deptNames = UserInformationManagementBLL.sqlGetDepartmentName();
            foreach (string name in deptNames)
            {
                _strDepartmentName.Add(name);
            }
        }

        private void GetGroupName()
        {
            //_strGroupName.Clear();
            //string sql = "select distinct USER_GROUP_NAME from T_USER_GROUP_INFO_MANAGEMENT";
            //if (database.ReturnDS(sql).Tables[0] != null)
            //{
            //    int n = database.ReturnDS(sql).Tables[0].Rows.Count;
            //    for (int i = 0; i < n; i++)
            //    {
            //        _strGroupName.Add(database.ReturnDS(sql).Tables[0].Rows[i][0].ToString());
            //    }
            //}
            string[] groupNames = UserInformationManagementBLL.sqlGetUserGroupName();
            foreach (string name in groupNames)
            {
                _strGroupName.Add(name);
            }
        }

        private int GetGroupIndex(string value)
        {
            int returnNumber = 0;
            foreach ( string str in _strGroupName)
            {
                if (str==value)
                {
                    return returnNumber;                    
                }
                returnNumber++;
            }
            return returnNumber;
        }

        private int GetDepartmentIndex(string value)
        {
            int returnNumber = 0;
            foreach (string str in _strDepartmentName)
            {
                if (str == value)
                {
                    return returnNumber;
                }
                returnNumber++;
            }
            return returnNumber;
        }

        private void SetPropertyGridEnt(UserParameter ent)
        {
            if (ent == null)
            {
                return;
            }
            _srcEnt = ent.DeepClone();
            _userLoginName.ReadOnly = false;
            _userPassWord.ReadOnly = false;
            _userGroup.ReadOnly = false;
            _userDepartment.ReadOnly = false;
            _remarks.ReadOnly = false;
            _userName.ReadOnly = false;
            _userEmail.ReadOnly = false;
            _userTel.ReadOnly = false;
            _userPhone.ReadOnly = false;
            _userPermission.ReadOnly = false;

            _userLoginName.Value = ent.LoginName;
            _userPassWord.Value = ent.PassWord;

            _userGroup.ProType=typeof(MyComboItemConvert);
            _userGroup.Converter = new MyComboItemConvert(_strGroupName);
            _userGroup.Value= GetGroupIndex(ent.Group);
            //_userGroup. = ent.Group;

            _userDepartment.ProType = typeof(MyComboItemConvert);
            _userDepartment.Converter = new MyComboItemConvert(_strDepartmentName);
            _userDepartment.Value = GetDepartmentIndex(ent.Department);       

            _userName.Value = ent.Name;
            _userEmail.Value = ent.Email;
            _userTel.Value = ent.Tel;
            _userPhone.Value = ent.Phone;
            _userPermission.ProType = typeof(LibCommon.Permission);

            _userPermission.Value = LibCommon.Permission.普通用户;
            if (ent.Permission=="管理员")
            {
                _userPermission.Value = LibCommon.Permission.管理员;
            }
           
            _remarks.Value = ent.Remark;
            int preCnt = _paramProp.Count;
            for (int i = 0; i < preCnt; i++)
            {
                _props.Remove(_paramProp[i]);
            }
            _paramProp.Clear();
            _propInfo.SelectedObject = _props;
        }
        
        private void _propInfo_PropertyValueChanged(object s, PropertyValueChangedEventArgs e)
        {
            int n = this.fpUserInfo.ActiveSheet.ActiveCell.Row.Index;
            string changeValue = "";
            if (e.ChangedItem.Value != null)
            {
                string ss = e.ChangedItem.Label.ToString();
                int columnIndex = this.fpUserInfo.ActiveSheet.Columns[ss].Index;
                changeValue = e.ChangedItem.Value.ToString();
                this.fpUserInfo.ActiveSheet.Cells[n, columnIndex].Text = changeValue;

                if (ss == "所属用户组")
                {
                    this.fpUserInfo.ActiveSheet.Cells[n, columnIndex].Text = _strGroupName[Convert.ToInt32(changeValue)].ToString();
                }
                if (ss == "所属部门")
                {
                    this.fpUserInfo.ActiveSheet.Cells[n, columnIndex].Text = _strDepartmentName[Convert.ToInt32(changeValue)].ToString();
                }
                string loginname = "";
                string password = "";
                string group = "";
                string department = "";
                string name = "";
                string tel = "";
                string email = "";
                string phone = "";
                string permission = "";
                string remark = "";
                if (this.fpUserInfo.ActiveSheet.Cells[n, 1].Value != null)
                {
                    loginname = this.fpUserInfo.ActiveSheet.Cells[n, 1].Value.ToString();
                }
                if (this.fpUserInfo.ActiveSheet.Cells[n, 2].Value != null)
                {
                    password = this.fpUserInfo.ActiveSheet.Cells[n, 2].Value.ToString();
                }
                if (this.fpUserInfo.ActiveSheet.Cells[n, 3].Value != null)
                {
                    group = this.fpUserInfo.ActiveSheet.Cells[n, 3].Value.ToString();
                }
                if (this.fpUserInfo.ActiveSheet.Cells[n, 4].Value != null)
                {
                    department = this.fpUserInfo.ActiveSheet.Cells[n, 4].Value.ToString();
                }
                if (this.fpUserInfo.ActiveSheet.Cells[n, 5].Value != null)
                {
                    name = this.fpUserInfo.ActiveSheet.Cells[n, 5].Value.ToString();
                }
                if (this.fpUserInfo.ActiveSheet.Cells[n, 6].Value != null)
                {
                    email = this.fpUserInfo.ActiveSheet.Cells[n, 6].Value.ToString();
                }
                if (this.fpUserInfo.ActiveSheet.Cells[n, 7].Value != null)
                {
                    tel = this.fpUserInfo.ActiveSheet.Cells[n, 7].Value.ToString();
                }
                if (this.fpUserInfo.ActiveSheet.Cells[n, 8].Value != null)
                {
                    phone = this.fpUserInfo.ActiveSheet.Cells[n, 8].Value.ToString();
                }
                if (this.fpUserInfo.ActiveSheet.Cells[n, 9].Value != null)
                {
                    remark = this.fpUserInfo.ActiveSheet.Cells[n, 9].Value.ToString();
                }
                if (this.fpUserInfo.ActiveSheet.Cells[n, 10].Value != null)
                {
                    permission = this.fpUserInfo.ActiveSheet.Cells[n, 10].Value.ToString().Trim();
                }

                string oldName = loginname;
                if (ss == "登陆名")
                {
                    oldName = e.OldValue.ToString();
                }

                UserInformationManagementEntity userInfo = new UserInformationManagementEntity();
                userInfo.LoginName = loginname;
                userInfo.PassWord = password;
                userInfo.Group = group;
                userInfo.Department = department;
                userInfo.Name = name;
                userInfo.Email = email;
                userInfo.Tel = tel;
                userInfo.Phone = phone;
                userInfo.Remark = remark;
                userInfo.Permission = permission;

                //string sqlUpdate = "update T_USER_INFO_MANAGEMENT set USER_LOGIN_NAME = '" + loginname +"',"
                //    + "USER_PASSWORD = '" + password + "',"
                //    + "USER_UNDER_GROUP = '" + group + "',"
                //    + "USER_UNDER_DEPT = '" + department + "',"
                //    + "USER_NAME = '" + name + "',"
                //    + "USER_EMAIL = '" + email + "',"
                //    + "USER_TEL = '" + tel + "',"
                //    + "USER_PHONE = '" + phone + "',"
                //    + "USER_REMARKS = '" + remark + "',"
                //    + "USER_PERMISSION = '" + permission + "'"
                //    + " where USER_LOGIN_NAME = '"+oldName+"'";
                //database.OperateDB(sqlUpdate);数据库业务逻辑摘出前

                //更新数据库
                UserInformationManagementBLL.UpdateUserInfomationDatabase(userInfo, oldName);
            }
        }

        private void tsBtnExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void tsBtnRefresh_Click(object sender, EventArgs e)
        {
            int n = GetTableRecordCount("T_USER_INFO_MANAGEMENT");
            for (int i = n + 1; i > 1; i--)
            {
                fpUserInfo.Sheets[0].RemoveRows(i, 1);
            }
            GetUsertInfo();
            for (int i = 0; i < n; i++)
            {
                fpUserInfo.Sheets[0].AddRows(n + 2 + i, 1);
            }
            chkSelAll.Checked = false;
        }

        private void tsBtnDel_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("确认要删除用户信息吗？", "提示：", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                int n = GetTableRecordCount("T_USER_INFO_MANAGEMENT");
                for (int i = n - 1; i >= 0; i--)
                {
                    if (this.fpUserInfo.ActiveSheet.Cells[2 + i, 0].CellType is CheckBoxCellType)
                    {
                        if (this.fpUserInfo.ActiveSheet.Cells[2 + i, 0].Value != null)
                        {
                            if ((bool)this.fpUserInfo.ActiveSheet.Cells[2 + i, 0].Value)
                            {
                                this.fpUserInfo.ActiveSheet.Rows.Remove(2 + i, 1);
                            }
                        }
                    }
                }
                foreach (string str in _userSel)
                {
                    //string sqlDelete = "delete from T_USER_INFO_MANAGEMENT where USER_LOGIN_NAME ='" + str +"'";
                    //database.OperateDB(sqlDelete);
                    UserInformationManagementBLL.DeleteUserInformationByLoginName(str);
                }
                fpUserInfo.ActiveSheet.RowCount = 500;
                fpUserInfo.Refresh();
            }
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void tsBtnModify_Click(object sender, EventArgs e)
        {

        }

        private void tsBtnPrint_Click(object sender, EventArgs e)
        {
            FilePrint.CommonPrint(fpUserInfo, 0);
        }

        private void tsBtnExport_Click(object sender, EventArgs e)
        {
            if (FileExport.fileExport(fpUserInfo, true))
            {
                Alert.alert(Const.EXPORT_SUCCESS_MSG);
            }            
        }
    }

    //重写下拉菜单中的项，使之与属性页的项关联
    public abstract class ComboBoxItemTypeConvert : TypeConverter
    {
        public Hashtable myhash = null;
        public ComboBoxItemTypeConvert()
        {
            myhash = new Hashtable();
            GetConvertHash();
        }
        public abstract void GetConvertHash();

        //是否支持选择列表的编辑
        public override bool GetStandardValuesSupported(ITypeDescriptorContext context)
        {
            return true;
        }
        //重写combobox的选择列表
        public override StandardValuesCollection GetStandardValues(ITypeDescriptorContext context)
        {
            int[] ids = new int[myhash.Values.Count];
            int i = 0;
            foreach (DictionaryEntry myDE in myhash)
            {
                ids[i++] = (int)(myDE.Key);
            }
            return new StandardValuesCollection(ids);
        }
        //判断转换器是否可以工作
        public override bool CanConvertFrom(ITypeDescriptorContext context, Type sourceType)
        {
            if (sourceType == typeof(string))
            {
                return true;
            }
            return base.CanConvertFrom(context, sourceType);
        }
        //重写转换器，将选项列表（即下拉菜单）中的值转换到该类型的值
        public override object ConvertFrom(ITypeDescriptorContext context, System.Globalization.CultureInfo culture, object obj)
        {
            if (obj is string)
            {
                foreach (DictionaryEntry myDE in myhash)
                {
                    if (myDE.Value.Equals((obj.ToString())))
                        return myDE.Key;
                }
            }
            return base.ConvertFrom(context, culture, obj);
        }
        public override bool CanConvertTo(ITypeDescriptorContext context, Type destinationType)
        {
            if (destinationType == typeof(string))
            {
                return true;
            }
            return base.CanConvertTo(context, destinationType);
        }
        //重写转换器将该类型的值转换到选择列表中
        public override object ConvertTo(ITypeDescriptorContext context, System.Globalization.CultureInfo culture, object obj, Type destinationType)
        {
            if (destinationType == typeof(string))
            {
                foreach (DictionaryEntry myDE in myhash)
                {
                    if (myDE.Key.Equals(obj))
                        return myDE.Value.ToString();
                }
                return "";
            }
            return base.ConvertTo(context, culture, obj, destinationType);
        }
        public override bool GetStandardValuesExclusive(ITypeDescriptorContext context)
        {
            return false;
        }
    }
    //重写下拉菜单，在这里实现定义下拉菜单内的项
    public class MyComboItemConvert : ComboBoxItemTypeConvert
    {
        private Hashtable hash;
        public override void GetConvertHash()
        {
            try
            {
                myhash = hash;
            }
            catch
            {
                throw new NotImplementedException();
            }
        }
        public MyComboItemConvert(List<string> values)
        {
            hash = new Hashtable();
            for (int i = 0; i < values.Count; i++)
            {
                hash.Add(i, values[i]);
            }
            GetConvertHash();
            value = 0;
        }
        public int value { get; set; }
        public MyComboItemConvert(string str, int s)
        {
            hash = new Hashtable();
            string[] stest = str.Split(',');
            for (int i = 0; i < stest.Length; i++)
            {
                hash.Add(i, stest[i]);
            }
            GetConvertHash();
            value = s;
        }
    }
}
