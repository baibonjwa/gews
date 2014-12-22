// ******************************************************************
// 概  述：用户登录信息实体
// 作  者：秦凯
// 创建日期：2014/03/10
// 版本号：V1.0
// 版本信息:
// V1.0 新建
// ******************************************************************

using Castle.ActiveRecord;

namespace LibEntity
{
    [ActiveRecord("T_USER_INFO_LOGIN_MANAGEMENT")]
    public class UserLogin : ActiveRecordBase<UserLogin>
    {
        /// <summary>
        ///     默认构造函数
        /// </summary>
        public UserLogin()
        {
            Remarks = "";
            GroupName = "";
            Permission = "";
            PassWord = "";
            LoginName = "";
            Id = "";
            NaverLogin = false;
            SavePassWord = false;
        }

        /// <summary>
        ///     分配默认用户权限的构造函数
        /// </summary>
        /// <param name="permission"></param>
        public UserLogin(string permission)
        {
            Remarks = "";
            GroupName = "";
            PassWord = "";
            LoginName = "";
            Id = "";
            NaverLogin = false;
            SavePassWord = false;
            Permission = permission;
        }

        /// <summary>
        ///     ID
        /// </summary>
        [PrimaryKey(PrimaryKeyType.Identity, "USER_ID")]
        public string Id { get; set; }

        /// <summary>
        ///     登录名
        /// </summary>
        [Property("USER_LOGIN_NAME")]
        public string LoginName { get; set; }

        /// <summary>
        ///     密码
        /// </summary>
        [Property("USER_PASSWORD")]
        public string PassWord { get; set; }

        /// <summary>
        ///     权限
        /// </summary>
        [Property("USER_PERMISSION")]
        public string Permission { get; set; }

        /// <summary>
        ///     用户组名称
        /// </summary>
        [Property("USER_GROUP_NAME")]
        public string GroupName { get; set; }

        /// <summary>
        ///     记住密码
        /// </summary>
        [Property("USER_SAVE_PASSWORD")]
        public bool SavePassWord { get; set; }

        /// <summary>
        ///     尚未登录
        /// </summary>
        [Property("USER_REMARKS")]
        public bool NaverLogin { get; set; }

        /// <summary>
        ///     备注
        /// </summary>
        [Property("USER_REMARKS")]
        public string Remarks { get; set; }
    }
}