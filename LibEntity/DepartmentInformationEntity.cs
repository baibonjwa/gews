// ******************************************************************
// 概  述：部门信息实体
// 作  者：秦凯
// 创建日期：2014/03/07
// 版本号：V1.0
// 版本信息:
// V1.0 新建
// ******************************************************************

namespace LibEntity
{
    public class DepartmentInformationEntity
    {
        /// <summary>
        ///     获取、设置部门名称
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        ///     获取、设置电话
        /// </summary>
        public string Tel { get; set; }

        /// <summary>
        ///     获取、设置邮箱
        /// </summary>
        public string Email { get; set; }

        /// <summary>
        ///     获取设置用户人数
        /// </summary>
        public string Staff { get; set; }

        /// <summary>
        ///     获取、设置备注
        /// </summary>
        public string Remark { get; set; }

        /// <summary>
        ///     获取、设置ID
        /// </summary>
        public string ID { get; set; }
    }
}