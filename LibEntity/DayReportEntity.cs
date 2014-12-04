using System;

namespace LibEntity
{
    /// <summary>
    ///     回采和掘进日报的基类
    /// </summary>
    public class DayReportEntity
    {
        // 记录ID

        private DateTime dateTime;

        /// <summary>
        ///     记录ID
        /// </summary>
        public int ID { get; set; }

        // 队别名称

        /// <summary>
        ///     队别名称
        /// </summary>
        public int TeamNameID { get; set; }

        // 班次

        /// <summary>
        ///     班次
        /// </summary>
        public string WorkTime { get; set; }

        // 工作制式

        /// <summary>
        ///     工作制式
        /// </summary>
        public string WorkTimeStyle { get; set; }

        // 工作内容

        /// <summary>
        ///     工作内容
        /// </summary>
        public string WorkInfo { get; set; }

        // 进尺

        /// <summary>
        ///     进尺
        /// </summary>
        public double JinChi { get; set; }

        // 工作面编号

        /// <summary>
        ///     工作面编号
        /// </summary>
        public int WorkingFaceID { get; set; }

        // 日期

        /// <summary>
        ///     日期
        /// </summary>
        public DateTime DateTime
        {
            get { return dateTime; }
            set { dateTime = value; }
        }

        // 填报人

        /// <summary>
        ///     填报人
        /// </summary>
        public string Submitter { get; set; }

        // 备注

        /// <summary>
        ///     备注
        /// </summary>
        public string Other { get; set; }

        // BID

        /// <summary>
        ///     BID
        /// </summary>
        public string BindingID { get; set; }
    }
}