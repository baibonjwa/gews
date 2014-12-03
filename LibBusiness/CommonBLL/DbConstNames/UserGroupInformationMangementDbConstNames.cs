// ******************************************************************
// 概  述：用户组信息管理表
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
    public class UserGroupInformationMangementDbConstNames
    {
        //表名
        public const string TABLE_NAME = "T_USER_GROUP_INFO_MANAGEMENT";

        //用户组信息Id
        public const string USER_GROUP_Id = "USER_GROUP_Id";

        //用户组名
        public const string USER_GROUP_NAME = "USER_GROUP_NAME";

        //用户组人数
        public const string USER_GROUP_STAFF_COUNT = "USER_GROUP_STAFF_COUNT";

        //备注
        public const string USER_GROUP_REMARKS = "USER_GROUP_REMARKS";
    }
}
