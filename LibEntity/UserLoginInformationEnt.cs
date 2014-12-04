// ******************************************************************
// 概  述：用户登录信息实体
// 作  者：秦凯
// 创建日期：2014/03/10
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
    public class UserLoginInformationEnt
    {
        //ID
        private string _id = "";
        /// <summary>
        /// ID
        /// </summary>
        public string ID
        {
            get { return _id; }
            set { _id = value; }
        }
        //登录名
        private string _userLoginName = "";
        /// <summary>
        /// 登录名
        /// </summary>
        public string LoginName
        {
            get { return _userLoginName; }
            set { _userLoginName = value; }
        }
        //密码
        private string _userPassword = "";
        /// <summary>
        /// 密码
        /// </summary>
        public string PassWord
        {
            get { return _userPassword; }
            set { _userPassword = value; }
        }
        //权限
        private string _userPermission = "";
        /// <summary>
        /// 权限
        /// </summary>
        public string Permission
        {
            get { return _userPermission; }
            set { _userPermission = value; }
        }
        //用户组名称
        private string _userGroupName = "";
        /// <summary>
        /// 用户组名称
        /// </summary>
        public string GroupName
        {
            get { return _userGroupName; }
            set { _userGroupName = value; }
        }
        //记住密码
        private bool _userSavePassword = false;
        /// <summary>
        /// 记住密码
        /// </summary>
        public bool SavePassWord
        {
            get { return _userSavePassword; }
            set { _userSavePassword = value; }
        }
        //尚未登录
        private bool _userNaverLogin = false;
        /// <summary>
        /// 尚未登录
        /// </summary>
        public bool NaverLogin
        {
            get { return _userNaverLogin; }
            set { _userNaverLogin = value; }
        }
        //备注
        private string _userRemarks = "";
        /// <summary>
        /// 备注
        /// </summary>
        public string Remarks
        {
            get { return _userRemarks; }
            set { _userRemarks = value; }
        }

        /// <summary>
        /// 默认构造函数
        /// </summary>
        public UserLoginInformationEnt()
        {

        }

        /// <summary>
        /// 分配默认用户权限的构造函数
        /// </summary>
        /// <param name="permission"></param>
        public UserLoginInformationEnt(string permission)
        {
            _userPermission = permission;
        }
    }
}
