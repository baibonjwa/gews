// ******************************************************************
// 概  述：导线点实体
// 作  者：宋英杰  
// 创建日期：2013/11/29
// 版本号：1.0
// ******************************************************************

namespace LibEntity
{
    public class WirePointInfoEntity
    {
        //导线点编号
        //坐标X
        //坐标Y
        //坐标Z
        //距左帮距离
        //距右帮距离
        //绑定导线编号
        private int wireInfoID;

        /// <summary>
        ///     构造方法
        /// </summary>
        public WirePointInfoEntity()
        {
        }

        /// <summary>
        ///     构造方法
        /// </summary>
        /// <param name="src">导线点实体</param>
        public WirePointInfoEntity(WirePointInfoEntity src)
        {
            WirePointID = src.WirePointID;
            CoordinateX = src.CoordinateX;
            CoordinateY = src.CoordinateY;
            CoordinateZ = src.CoordinateZ;
            LeftDis = src.LeftDis;
            RightDis = src.RightDis;
            wireInfoID = src.wireInfoID;
        }

        //巷道ID
        //距顶板距离
        //距底板距离

        /// <summary>
        ///     距底板距离
        /// </summary>
        public double BottomDis { get; set; }

        /// <summary>
        ///     距顶板距离
        /// </summary>
        public double TopDis { get; set; }

        /// <summary>
        ///     巷道ID
        /// </summary>
        public int TunnelID { get; set; }

        /// <summary>
        ///     主键
        /// </summary>
        public int ID { get; set; }

        /// <summary>
        ///     绑定导线编号
        /// </summary>
        public int WireInfoID
        {
            get { return wireInfoID; }
            set { wireInfoID = value; }
        }

        //BID

        /// <summary>
        ///     导线点编号
        /// </summary>
        public string WirePointID { get; set; }

        /// <summary>
        ///     坐标X
        /// </summary>
        public double CoordinateX { get; set; }

        /// <summary>
        ///     坐标Y
        /// </summary>
        public double CoordinateY { get; set; }

        /// <summary>
        ///     坐标Z
        /// </summary>
        public double CoordinateZ { get; set; }

        /// <summary>
        ///     距左帮距离
        /// </summary>
        public double LeftDis { get; set; }

        /// <summary>
        ///     距右帮距离
        /// </summary>
        public double RightDis { get; set; }

        /// <summary>
        ///     绑定ID
        /// </summary>
        public string BindingID { get; set; }
    }
}