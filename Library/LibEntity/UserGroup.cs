// ******************************************************************
// 概  述：用户组信息实体
// 作  者：秦凯
// 创建日期：2014/03/07
// 版本号：V1.0
// 版本信息:
// V1.0 新建
// ******************************************************************

namespace LibEntity
{
    public class UserGroup
    {
        //用户组名称

        /// <summary>
        ///     获取、设置用户组名称
        /// </summary>
        public string GroupName { get; set; }

        //用户人数

        /// <summary>
        ///     获取、设置用户人数
        /// </summary>
        public string UserCount { get; set; }

        //备注

        /// <summary>
        ///     获取、设置备注
        /// </summary>
        public string Remark { get; set; }

        //用户组ID

        /// <summary>
        ///     获取、设置用户组ID
        /// </summary>
        public string ID { get; set; }

        //用户组权限
        /// <summary>
        ///     获取、设置用户组ID
        /// </summary>
        public string Permission { get; set; }
    }
}