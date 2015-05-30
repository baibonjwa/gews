// ******************************************************************
// 概  述：登录窗体声明常量
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

namespace LibBusiness
{
    public class LoginFormDbConstNames
    {
        //表名
        public const string TABLE_NAME = "T_USER_INFO_LOGIN_MANAGEMENT";
        //ID
        public const string USER_ID = "USER_ID";
        //登录名
        public const string USER_LOGIN_NAME = "USER_LOGIN_NAME";
        //密码
        public const string USER_PASSWORD = "USER_PASSWORD";
        //权限
        public const string USER_PERMISSION = "USER_PERMISSION";
        //组名
        public const string USER_GROUP_NAME = "USER_GROUP_NAME";
        //保存密码
        public const string USER_SAVE_PASSWORD = "USER_SAVE_PASSWORD";
        //尚未登录
        public const string USER_NAVER_LOGIN = "USER_NAVER_LOGIN";
        //备注
        public const string USER_REMARKS = "USER_REMARKS";
    }
}
