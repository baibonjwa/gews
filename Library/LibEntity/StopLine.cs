// ******************************************************************
// 概  述：停采线数据实体
// 作  者：宋英杰
// 日  期：2014/3/12
// 版本号：V1.0
// 版本信息：
// V1.0 新建
// ******************************************************************

using Castle.ActiveRecord;

namespace LibEntity
{
    public class StopLine : ActiveRecordBase<StopLine>
    {
        /// <summary>
        ///     设置或获取主键
        /// </summary>
        [PrimaryKey(PrimaryKeyType.Identity, "OBJECTID")]
        public int Id { get; set; }

        /// <summary>
        ///     设置或获取停采线名称
        /// </summary>
        [Property("STOP_LINE_NAME")]
        public string StopLineName { get; set; }

        /// <summary>
        ///     设置或获取起点坐标X
        /// </summary>
        [Property("S_COORDINATE_X")]
        public double SCoordinateX { get; set; }

        /// <summary>
        ///     设置或获取起点坐标Y
        /// </summary>
        [Property("S_COORDINATE_Y")]
        public double SCoordinateY { get; set; }

        /// <summary>
        ///     设置或获取起点坐标Z
        /// </summary>
        [Property("S_COORDINATE_Z")]
        public double SCoordinateZ { get; set; }

        /// <summary>
        ///     设置或获取终点坐标X
        /// </summary>
        [Property("F_COORDINATE_X")]
        public double FCoordinateX { get; set; }

        /// <summary>
        ///     设置或获取终点坐标Y
        /// </summary>
        [Property("F_COORDINATE_Y")]
        public double FCoordinateY { get; set; }

        /// <summary>
        ///     设置或获取终点坐标Z
        /// </summary>
        [Property("F_COORDINATE_Z")]
        public double FCoordinateZ { get; set; }

        /// <summary>
        ///     BID
        /// </summary>
        [Property("BINDINGID")]
        public string BindingId { get; set; }
    }
}