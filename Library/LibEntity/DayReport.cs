using System;
using Castle.ActiveRecord;

namespace LibEntity
{
    /// <summary>
    ///     回采和掘进日报的基类
    /// </summary>
    public class DayReport : ActiveRecordBase
    {
        // 记录ID

        private DateTime _dateTime;

        /// <summary>
        ///     记录ID
        /// </summary>
        [PrimaryKey(PrimaryKeyType.Identity, "OBJECTID")]
        public int Id { get; set; }

        // 队别名称

        /// <summary>
        ///     队别名称
        /// </summary>
        [BelongsTo("TEAM_NAME_ID")]
        public Team Team { get; set; }

        // 班次

        /// <summary>
        ///     班次
        /// </summary>
        [Property("WORK_TIME")]
        public string WorkTime { get; set; }

        // 工作制式

        /// <summary>
        ///     工作制式
        /// </summary>
        [Property("WORK_TIME_SYTLE")]
        public string WorkTimeStyle { get; set; }

        // 工作内容

        /// <summary>
        ///     工作内容
        /// </summary>
        [Property("WORK_INFO")]
        public string WorkInfo { get; set; }

        // 进尺

        /// <summary>
        ///     进尺
        /// </summary>
        [Property("JIN_CHI")]
        public double JinChi { get; set; }

        // 工作面编号

        /// <summary>
        ///     工作面编号
        /// </summary>
        [BelongsTo("WORKINGFACE_ID")]
        public WorkingFace WorkingFace { get; set; }

        // 日期

        /// <summary>
        ///     日期
        /// </summary>
        [Property("DATETIME")]
        public DateTime DateTime
        {
            get { return _dateTime; }
            set { _dateTime = value; }
        }

        // 填报人

        /// <summary>
        ///     填报人
        /// </summary>
        [Property("SUBMITTER")]
        public string Submitter { get; set; }

        // 备注

        /// <summary>
        ///     备注
        /// </summary>
        [Property("OTHER")]
        public string Other { get; set; }

        // BID

        /// <summary>
        ///     BID
        /// </summary>
        [Property("BINDINGID")]
        public string BindingId { get; set; }
    }
}