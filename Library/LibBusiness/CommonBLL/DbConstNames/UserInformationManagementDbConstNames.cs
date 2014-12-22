// ******************************************************************
// 概  述：用户信息管理表
// 作  者：秦凯
// 创建日期：2014/03/06
// 版本号：V1.0
// 版本信息:
// V1.0 新建
// ******************************************************************
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LibBusiness
{
    class UserInformationManagementDbConstNames
    {
        //用户信息管理表名
        public const string TABLE_NAME = "T_USER_INFO_MANAGEMENT";

        //用户信息Id
        public const string USER_ID = "USER_ID";
        
        //用户登陆名
        public const string USER_LOGIN_NAME = "USER_LOGIN_NAME";
        
        //用户密码
        public const string USER_PASSWORD = "USER_PASSWORD";

        //用户所属用户组
        public const string USER_UNDER_GROUP = "USER_UNDER_GROUP";

        //用户所属部门
        public const string USER_UNDER_DEPT = "USER_UNDER_DEPT";

        //用户姓名
        public const string USER_NAME = "USER_NAME";

        //用户邮箱
        public const string USER_EMAIL = "USER_EMAIL";

        //用户电话
        public const string USER_TEL = "USER_TEL";

        //用户手机
        public const string USER_PHONE = "USER_PHONE";
        
        //备注
        public const string USER_REMARKS = "USER_REMARKS";

        //用户权限
        public const string USER_PERMISSION = "USER_PERMISSION";

    }
}
