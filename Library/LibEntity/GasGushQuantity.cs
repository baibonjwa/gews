using System;
using Castle.ActiveRecord;

namespace LibEntity
{
    [ActiveRecord("T_GAS_GUSH_QUANTITY")]
    public class GasGushQuantity : ActiveRecordBase<GasGushQuantity>
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

        /** 绝对瓦斯涌出量 **/

        /// <summary>
        ///     绝对瓦斯涌出量
        /// </summary>
        [Property("ABSOLUTE_GAS_GUSH_QUANTITY")]
        public double AbsoluteGasGushQuantity { get; set; }

        /** 相对瓦斯涌出量 **/

        /// <summary>
        ///     相对瓦斯涌出量
        /// </summary>
        [Property("RELATIVE_GAS_GUSH_QUANTITY")]
        public double RelativeGasGushQuantity { get; set; }

        /** 工作面日产量 **/

        /// <summary>
        ///     工作面日产量
        /// </summary>
        [Property("WORKING_FACE_DAY_OUTPUT")]
        public double WorkingFaceDayOutput { get; set; }

        /** 回采年月 **/

        /// <summary>
        ///     回采年月
        /// </summary>
        [Property("STOPE_DATE")]
        public DateTime StopeDate { get; set; }

        // 巷道编号

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