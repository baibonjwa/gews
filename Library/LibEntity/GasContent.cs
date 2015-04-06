// ******************************************************************
// 概  述：瓦斯含量实体
// 作  者：伍鑫
// 创建日期：2013/12/08
// 版本号：V1.0
// 版本信息：
// V1.0 新建
// ******************************************************************

using System;
using Castle.ActiveRecord;

namespace LibEntity
{
    [ActiveRecord("T_GAS_CONTENT")]
    public class GasContent : ActiveRecordBase<GasContent>
    {
        /** 编号 **/

        /// <summary>
        ///     编号
        /// </summary>
        [PrimaryKey(PrimaryKeyType.Native, "PRIMARY_KEY")]
        public int PrimaryKey { get; set; }

        /** 坐标X **/

        /// <summary>
        ///     坐标X
        /// </summary>
        [Property("COORDINATE_X")]
        public double CoordinateX { get; set; }

        /** 坐标Y **/

        /// <summary>
        ///     坐标Y
        /// </summary>
        [Property("COORDINATE_Y")]
        public double CoordinateY { get; set; }

        /** 坐标Z **/

        /// <summary>
        ///     坐标Z
        /// </summary>
        [Property("COORDINATE_Z")]
        public double CoordinateZ { get; set; }

        /** 埋深 **/

        /// <summary>
        ///     埋深
        /// </summary>
        [Property("DEPTH")]
        public double Depth { get; set; }

        /** 瓦斯含量值 **/

        /// <summary>
        ///     瓦斯含量值
        /// </summary>
        [Property("GAS_CONTENT_VALUE")]
        public double GasContentValue { get; set; }

        /** 测定时间 **/

        /// <summary>
        ///     测定时间
        /// </summary>
        [Property("MEASURE_DATE_TIME")]
        public DateTime MeasureDateTime { get; set; }

        /// <summary>
        ///     巷道编号
        /// </summary>
        [BelongsTo("TUNNEL_ID")]
        public Tunnel Tunnel { get; set; }

        // 煤层编号

        /// <summary>
        ///     煤层编号
        /// </summary>
        [BelongsTo("COAL_SEAMS_ID")]
        public CoalSeams CoalSeams { get; set; }

        /** BID **/

        /// <summary>
        ///     BID
        /// </summary>
        [Property("BID")]
        public string BindingId { get; set; }
    }
}