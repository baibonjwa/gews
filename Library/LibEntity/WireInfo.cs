// ******************************************************************
// 概  述：导线实体
// 作  者：宋英杰  
// 创建日期：2013/11/2
// 版本号：1.0
// ******************************************************************

using System;
using Castle.ActiveRecord;

namespace LibEntity
{
    [ActiveRecord("T_WIRE_INFO")]
    public class WireInfo
    {

        /// <summary>
        ///     导线编号
        /// </summary>
        [PrimaryKey(PrimaryKeyType.Identity, "OBJECTID")]
        public int WireInfoId { get; set; }
        /// <summary>
        ///     校核日期
        /// </summary>
        [Property("CHECK_DATE")]
        public DateTime CheckDate { get; set; }

        /// <summary>
        ///     校核者
        /// </summary>
        [Property("CHECKER")]
        public string Checker { get; set; }

        /// <summary>
        ///     计算日期
        /// </summary>
        [Property("COUNT_DATE")]
        public DateTime CountDate { get; set; }

        /// <summary>
        ///     计算者
        /// </summary>
        [Property("COUNTER")]
        public string Counter { get; set; }

        /// <summary>
        ///     巷道编号
        /// </summary>
        [BelongsTo("TUNNEL_ID")]
        public Tunnel Tunnel { get; set; }

        /// <summary>
        ///     导线名称
        /// </summary>
        [Property("WIRE_NAME")]
        public string WireName { get; set; }

        /// <summary>
        ///     导线级别
        /// </summary>
        [Property("WIRE_LEVEL")]
        public string WireLevel { get; set; }

        /// <summary>
        ///     测试日期
        /// </summary>
        [Property("MEASURE_DATE")]
        public DateTime MeasureDate { get; set; }

        /// <summary>
        ///     观测者
        /// </summary>
        [Property("VOBSERVER")]
        public string Vobserver { get; set; }
    }
}