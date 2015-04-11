// ******************************************************************
// 概  述：瓦斯压力实体
// 作  者：伍鑫
// 创建日期：2013/12/07
// 版本号：V1.0
// 版本信息：
// V1.0 新建
// ******************************************************************

using System;
using Castle.ActiveRecord;

namespace LibEntity
{
    [ActiveRecord("T_GAS_PRESSURE")]
    public class GasPressure : ActiveRecordBase<GasPressure>
    {
        /// <summary>
        ///     编号
        /// </summary>
        [PrimaryKey(PrimaryKeyType.Native, "PRIMARY_KEY")]
        public int PrimaryKey { get; set; }

        /// <summary>
        ///     坐标X
        /// </summary>
        [Property("COORDINATE_X")]
        public double CoordinateX { get; set; }

        /// <summary>
        ///     坐标Y
        /// </summary>
        [Property("COORDINATE_Y")]
        public double CoordinateY { get; set; }

        /// <summary>
        ///     坐标Z
        /// </summary>
        [Property("COORDINATE_Z")]
        public double CoordinateZ { get; set; }

        /// <summary>
        ///     埋深
        /// </summary>
        [Property("DEPTH")]
        public double Depth { get; set; }

        /// <summary>
        ///     瓦斯压力值
        /// </summary>
        [Property("GAS_PRESSURE_VALUE")]
        public double GasPressureValue { get; set; }

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

        /// <summary>
        ///     煤层编号
        /// </summary>
        [BelongsTo("COAL_SEAMS_ID")]
        public CoalSeams CoalSeams { get; set; }

        /// <summary>
        ///     BID
        /// </summary>
        [Property("BID")]
        public string BindingId { get; set; }
    }
}