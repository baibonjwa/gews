// ******************************************************************
// 概  述：导线点实体
// 作  者：宋英杰  
// 创建日期：2013/11/29
// 版本号：1.0
// ******************************************************************

using Castle.ActiveRecord;

namespace LibEntity
{
    [ActiveRecord("T_WIRE_POINT")]
    public class WirePointInfo : ActiveRecordBase<WirePointInfo>
    {
        private WireInfo _wireInfo;

        /// <summary>
        ///     构造方法
        /// </summary>
        public WirePointInfo()
        {
        }

        /// <summary>
        ///     构造方法
        /// </summary>
        /// <param name="src">导线点实体</param>
        public WirePointInfo(WirePointInfo src)
        {
            WirePointId = src.WirePointId;
            CoordinateX = src.CoordinateX;
            CoordinateY = src.CoordinateY;
            CoordinateZ = src.CoordinateZ;
            LeftDis = src.LeftDis;
            RightDis = src.RightDis;
            _wireInfo = src._wireInfo;
        }

        /// <summary>
        ///     主键
        /// </summary>
        [PrimaryKey(PrimaryKeyType.Identity, "ID")]
        public int Id { get; set; }

        public string WirePointId { get; set; }

        /// <summary>
        ///     距底板距离
        /// </summary>
        [Property("DISTANCE_FROM_BOTTOM")]
        public double BottomDis { get; set; }

        /// <summary>
        ///     距顶板距离
        /// </summary>
        [Property("DISTANCE_FROM_TOP")]
        public double TopDis { get; set; }

        /// <summary>
        ///     绑定导线编号
        /// </summary>
        [BelongsTo("WIRE_INFO_ID")]
        public WireInfo WireInfo
        {
            get { return _wireInfo; }
            set { _wireInfo = value; }
        }

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
        ///     距左帮距离
        /// </summary>
        [Property("DISTANCE_FROM_THE_LEFT")]
        public double LeftDis { get; set; }

        /// <summary>
        ///     距右帮距离
        /// </summary>
        [Property("DISTANCE_FROM_THE_RIGHT")]
        public double RightDis { get; set; }

        /// <summary>
        ///     绑定ID
        /// </summary>
        [Property("BINDINGID")]
        public string BindingId { get; set; }
    }
}