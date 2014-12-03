// ******************************************************************
// 概  述：人员详细信息声明常量
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
    public class UserInformationDetailsManagementDbConstNames
    {
        //表名
        public const string TABLE_NAME = "T_USER_INFO_DETAILS_MANAGEMENT";
        //ID
        public const string ID = "USER_ID";
        //用户姓名
        public const string USER_NAME = "USER_NAME";
        //手机号码
        public const string USER_PHONENUMBER = "USER_PHONENUMBER";
        //电话号码
        public const string USER_TELEPHONE = "USER_TELEPHONE";
        //用户邮箱
        public const string USER_EMAIL = "USER_EMAIL";
        //所属部门
        public const string USER_DEPARTMENT = "USER_DEPARTMENT";
        //用户职位
        public const string USER_POSITION = "USER_POSITION";
        //备注
        public const string USER_REMARKS = "USER_REMARKS";
        //是否通知预警
        public const string USER_ISINFORM = "USER_ISINFORM";
    }
}
