// ******************************************************************
// 概  述：揭露断层实体
// 作  者：伍鑫
// 创建日期：2013/11/27
// 版本号：V1.0
// 版本信息：
// V1.0 新建
// ******************************************************************

using Castle.ActiveRecord;

namespace LibEntity
{
    [ActiveRecord("T_FAULTAGE")]
    public class Faultage : ActiveRecordBase<Faultage>
    {
        /** 断层编号 **/

        /// <summary>
        ///     断层编号
        /// </summary>
        [PrimaryKey(PrimaryKeyType.Identity, "FAULTAGE_ID")]
        public int FaultageId { get; set; }

        /** 断层名称 **/

        /// <summary>
        ///     断层名称
        /// </summary>
        [Property("FAULTAGE_NAME")]
        public string FaultageName { get; set; }

        /** 落差 **/

        /// <summary>
        ///     落差
        /// </summary>
        [Property("GAP")]
        public string Gap { get; set; }

        /** 倾角 **/

        /// <summary>
        ///     倾角
        /// </summary>
        [Property("ANGLE")]
        public double Angle { get; set; }

        /** 类型 **/

        /// <summary>
        ///     类型
        /// </summary>
        [Property("TYPE")]
        public string Type { get; set; }

        /** 走向 **/

        /// <summary>
        ///     走向
        /// </summary>
        [Property("TREND")]
        public string Trend { get; set; }

        /** 断距 **/

        /// <summary>
        ///     断距
        /// </summary>
        [Property("SEPARATION")]
        public string Separation { get; set; }

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

        /** BID **/

        /// <summary>
        ///     BID
        /// </summary>
        [Property("BID")]
        public string BindingId { get; set; }

    }
}