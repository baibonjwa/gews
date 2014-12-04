// ******************************************************************
// 概  述：停采线数据实体
// 作  者：宋英杰
// 日  期：2014/3/12
// 版本号：V1.0
// 版本信息：
// V1.0 新建
// ******************************************************************

namespace LibEntity
{
    public class StopLineEntity
    {
        // 主键

        /// <summary>
        ///     设置或获取主键
        /// </summary>
        public int ID { get; set; }

        // 停采线名称

        /// <summary>
        ///     设置或获取停采线名称
        /// </summary>
        public string StopLineName { get; set; }

        // 起点坐标X

        /// <summary>
        ///     设置或获取起点坐标X
        /// </summary>
        public double SCoordinateX { get; set; }

        // 起点坐标Y

        /// <summary>
        ///     设置或获取起点坐标Y
        /// </summary>
        public double SCoordinateY { get; set; }

        // 起点坐标Z

        /// <summary>
        ///     设置或获取起点坐标Z
        /// </summary>
        public double SCoordinateZ { get; set; }

        // 终点坐标X

        /// <summary>
        ///     设置或获取终点坐标X
        /// </summary>
        public double FCoordinateX { get; set; }

        // 终点坐标Y

        /// <summary>
        ///     设置或获取终点坐标Y
        /// </summary>
        public double FCoordinateY { get; set; }

        // 终点坐标Z

        /// <summary>
        ///     设置或获取终点坐标Z
        /// </summary>
        public double FCoordinateZ { get; set; }

        // BID

        /// <summary>
        ///     BID
        /// </summary>
        public string BindingID { get; set; }
    }
}