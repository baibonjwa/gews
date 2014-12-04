// ******************************************************************
// 概  述：揭露断层实体
// 作  者：伍鑫
// 创建日期：2013/11/27
// 版本号：V1.0
// 版本信息：
// V1.0 新建
// ******************************************************************

namespace LibEntity
{
    public class FaultageEntity
    {
        /** 断层编号 **/

        /// <summary>
        ///     断层编号
        /// </summary>
        public int FaultageId { get; set; }

        /** 断层名称 **/

        /// <summary>
        ///     断层名称
        /// </summary>
        public string FaultageName { get; set; }

        /** 落差 **/

        /// <summary>
        ///     落差
        /// </summary>
        public string Gap { get; set; }

        /** 倾角 **/

        /// <summary>
        ///     倾角
        /// </summary>
        public double Angle { get; set; }

        /** 类型 **/

        /// <summary>
        ///     类型
        /// </summary>
        public string Type { get; set; }

        /** 走向 **/

        /// <summary>
        ///     走向
        /// </summary>
        public string Trend { get; set; }

        /** 断距 **/

        /// <summary>
        ///     断距
        /// </summary>
        public string Separation { get; set; }

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

        /** BID **/

        /// <summary>
        ///     BID
        /// </summary>
        public string BindingId { get; set; }
    }
}