using System;
using Castle.ActiveRecord;
using NHibernate.Criterion;

namespace LibEntity
{
    /// <summary>
    ///     工作时间类
    /// </summary>
    [ActiveRecord("T_WORK_TIME")]
    public class WorkingTime : ActiveRecordBase<WorkingTime>
    {
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
        public DateTime WorkTimeFrom { get; set; }

        /// <summary>
        ///     获取设置工作终止时间
        /// </summary>
        [Property("WORK_TIME_TO")]
        public DateTime WorkTimeTo { get; set; }

        public static WorkingTime[] FindAllBy38Times()
        {
            var criterion = new ICriterion[]
            {
                Restrictions.Eq("WorkTimeGroupId", 1)
            };
            return FindAll(criterion);
        }

        public static WorkingTime[] FindAllBy46Times()
        {
            var criterion = new ICriterion[]
            {
                Restrictions.Eq("WorkTimeGroupId", 2)
            };
            return FindAll(criterion);
        }

    }
}