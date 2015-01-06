using Castle.ActiveRecord;

namespace LibEntity
{
    [ActiveRecord("T_DEPT_INFO_MANAGEMENT")]
    public class Department : ActiveRecordBase<Department>
    {
        /// <summary>
        ///     获取、设置ID
        /// </summary>
        [PrimaryKey(PrimaryKeyType.Identity, "DEPT_Id")]
        public string Id { get; set; }

        /// <summary>
        ///     获取、设置部门名称
        /// </summary>
        [Property("DEPT_NAME")]
        public string Name { get; set; }

        /// <summary>
        ///     获取、设置电话
        /// </summary>
        [Property("DEPT_TEL")]
        public string Tel { get; set; }

        /// <summary>
        ///     获取、设置邮箱
        /// </summary>
        [Property("DEPT_EMAIL")]
        public string Email { get; set; }

        /// <summary>
        ///     获取设置用户人数
        /// </summary>
        [Property("DEPT_STAFF_COUNT")]
        public string Staff { get; set; }

        /// <summary>
        ///     获取、设置备注
        /// </summary>
        [Property("DEPT_REMARKS")]
        public string Remark { get; set; }


    }
}