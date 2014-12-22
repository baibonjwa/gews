using Castle.ActiveRecord;

namespace LibEntity
{
    /// <summary>
    ///     工作时间类
    /// </summary>
    [ActiveRecord("T_WORK_TIME")]
    public class WorkingTime : ActiveRecordBase<WorkingTime>
    {
        public WorkingTime()
        {
            WorkTimeTo = "";
            WorkTimeFrom = "";
            WorkTimeName = "";
        }

        /// <summary>
        ///     获取设置工作制Id
        /// </summary>
        [PrimaryKey(PrimaryKeyType.Identity, "WORK_TIME_ID")]
        public string Id { get; set; }

        /// <summary>
        ///     获取设置工作制制式类别
        /// </summary>
        [Property("WORK_TIME_GROUP_ID")]
        public string WorkTimeGroupId { get; set; }

        /// <summary>
        ///     获取设置工作制名称
        /// </summary>
        [Property("WORK_TIME_NAME")]
        public string WorkTimeName { get; set; }

        /// <summary>
        ///     获取设置工作起始时间
        /// </summary>
        [Property("WORK_TIME_FROM")]
        public string WorkTimeFrom { get; set; }

        /// <summary>
        ///     获取设置工作终止时间
        /// </summary>
        [Property("WORK_TIME_TO")]
        public string WorkTimeTo { get; set; }
    }
}