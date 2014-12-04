// ******************************************************************
// 概  述：瓦斯压力实体
// 作  者：伍鑫
// 创建日期：2013/12/07
// 版本号：V1.0
// 版本信息：
// V1.0 新建
// ******************************************************************

using System;

namespace LibEntity
{
    public class GasPressure
    {
        /** 编号 **/

        private DateTime measureDateTime;

        /// <summary>
        ///     编号
        /// </summary>
        public int PrimaryKey { get; set; }

        /** 坐标X **/

        /// <summary>
        ///     坐标X
        /// </summary>
        public double CoordinateX { get; set; }

        /** 坐标Y **/

        /// <summary>
        ///     坐标Y
        /// </summary>
        public double CoordinateY { get; set; }

        /** 坐标Z **/

        /// <summary>
        ///     坐标Z
        /// </summary>
        public double CoordinateZ { get; set; }

        /** 埋深 **/

        /// <summary>
        ///     埋深
        /// </summary>
        public double Depth { get; set; }

        /** 瓦斯压力值 **/

        /// <summary>
        ///     瓦斯压力值
        /// </summary>
        public double GasPressureValue { get; set; }

        /** 测定时间 **/

        /// <summary>
        ///     测定时间
        /// </summary>
        public DateTime MeasureDateTime
        {
            get { return measureDateTime; }
            set { measureDateTime = value; }
        }

        // 巷道编号

        /// <summary>
        ///     巷道编号
        /// </summary>
        public int TunnelID { get; set; }

        // 煤层编号

        /// <summary>
        ///     煤层编号
        /// </summary>
        public int CoalSeamsId { get; set; }

        /** BID **/

        /// <summary>
        ///     BID
        /// </summary>
        public string BindingId { get; set; }
    }
}