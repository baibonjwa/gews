// ******************************************************************
// 概  述：用户信息实体
// 作  者：秦凯 
// 创建日期：2014/03/07
// 版本号：V1.0
// 版本信息:
// V1.0 新建
// ******************************************************************
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LibEntity
{
    public class UserInformationManagementEntity
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
    }
}
