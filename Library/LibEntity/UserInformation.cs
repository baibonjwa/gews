// ******************************************************************
// 概  述：用户信息实体
// 作  者：秦凯 
// 创建日期：2014/03/07
// 版本号：V1.0
// 版本信息:
// V1.0 新建
// ******************************************************************

using Castle.ActiveRecord;

namespace LibEntity
{
    [ActiveRecord("T_USER_INFO_MANAGEMENT")]
    public class UserInformation
    {
        [PrimaryKey(PrimaryKeyType.Identity, "USER_ID")]
        public string Id { get; set; }

        [Property("USER_LOGIN_NAME")]
        public string Name { get; set; }

        [Property("USER_TEL")]
        public string Tel { get; set; }

        [Property("USER_EMAIL")]
        public string Email { get; set; }

        [Property("USER_PHONE")]
        public string Phone { get; set; }

        [Property("USER_LOGIN_NAME")]
        public string LoginName { get; set; }

        [Property("USER_REMARKS")]
        public string Remark { get; set; }

        [Property("USER_PASSWORD")]
        public string PassWord { get; set; }

        [Property("USER_UNDER_GROUP")]
        public string Group { get; set; }

        [Property("USER_UNDER_DEPT")]
        public string Department { get; set; }

        [Property("USER_PERMISSION")]
        public string Permission { get; set; }

    }
}