using System.Collections.Generic;
using Castle.ActiveRecord;

namespace LibEntity
{
    public class EarlyWarningResult
    {
        /// <summary>
        ///     工作面名称
        /// </summary>
        public string WarkingFaceName { get; set; }

        /// <summary>
        ///     预警ID--对应预警结果表中的ID（主键）
        /// </summary>
        public List<string> WarningIdList { get; set; }

        /// <summary>
        ///     巷道ID
        /// </summary>
        public string TunnelId { get; set; }

        /// <summary>
        ///     日期
        /// </summary>
        public string DateTime { get; set; }

        /// <summary>
        ///     班次
        /// </summary>
        public string DateShift { get; set; }
    }
}