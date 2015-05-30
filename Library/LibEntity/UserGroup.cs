// ******************************************************************
// 概  述：用户组信息实体
// 作  者：秦凯
// 创建日期：2014/03/07
// 版本号：V1.0
// 版本信息:
// V1.0 新建
// ******************************************************************

using Castle.ActiveRecord;

namespace LibEntity
{
    [ActiveRecord("T_USER_GROUP_INFO_MANAGEMENT")]
    public class UserGroup : ActiveRecordBase<UserGroup>
    {
        /// <summary>
        ///     获取、设置用户组名称
        /// </summary>
        [Property("USER_GROUP_NAME")]
        public string GroupName { get; set; }

        /// <summary>
        ///     获取、设置用户人数
        /// </summary>
        [Property("USER_GROUP_STAFF_COUNT")]
        public string UserCount { get; set; }

        /// <summary>
        ///     获取、设置备注
        /// </summary>
        [Property("USER_GROUP_REMARKS")]
        public string Remark { get; set; }

        /// <summary>
        ///     获取、设置用户组ID
        /// </summary>
        [PrimaryKey(PrimaryKeyType.Identity, "USER_GROUP_Id")]
        public string Id { get; set; }

        //用户组权限
        /// <summary>
        ///     获取、设置用户组ID
        /// </summary>
        [Property("USER_GROUP_PERMISSION")]
        public string Permission { get; set; }
    }
}