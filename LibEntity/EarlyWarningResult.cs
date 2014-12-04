using System.Collections.Generic;

namespace LibEntity
{
    /// <summary>
    ///     预警结果
    /// </summary>
    public class EarlyWarningResult
    {
        /// <summary>
        ///     工作面名称
        /// </summary>
        public string WarkingFaceName { get; set; }

        /// <summary>
        ///     预警ID--对应预警结果表中的ID（主键）
        /// </summary>
        public List<string> WarningIDList { get; set; }

        /// <summary>
        ///     巷道ID
        /// </summary>
        public string TunnelID { get; set; }

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