// ******************************************************************
// 概  述：钻孔岩性实体
// 作  者：伍鑫
// 创建日期：2013/11/26
// 版本号：1.0
// ******************************************************************

namespace LibEntity
{
    public class BoreholeLithologyEntity
    {
        /** 钻孔编号 **/

        /// <summary>
        ///     钻孔编号
        /// </summary>
        public int BoreholeId { get; set; }

        /** 岩性编号 **/

        /// <summary>
        ///     岩性编号
        /// </summary>
        public int LithologyId { get; set; }

        /** 底板标高 **/

        /// <summary>
        ///     底板标高
        /// </summary>
        public double FloorElevation { get; set; }

        /** 厚度 **/

        /// <summary>
        ///     厚度
        /// </summary>
        public double Thickness { get; set; }

        /** 煤层名称 **/

        /// <summary>
        ///     煤层名称
        /// </summary>
        public string CoalSeamsName { get; set; }

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
    }
}