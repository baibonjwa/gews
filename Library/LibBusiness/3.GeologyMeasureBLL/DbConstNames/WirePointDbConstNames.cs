// ******************************************************************
// 概  述：导线点数据库常量名
// 作  者：梁俊丽
// 创建日期：2014/01/17
// 版本号：V1.0
// 版本信息：
// V1.0 新建
// ******************************************************************
namespace LibBusiness
{
    public static class WirePointDbConstNames
    {
        //表名
        public const string TABLE_NAME = "T_WIRE_POINT";//导线点表

        // 主键
        public const string ID = "ID";

        //导线点编号
        public const string WIRE_POINT_NAME = "WIRE_POINT_NAME";

        //坐标X
        public const string COORDINATE_X = "COORDINATE_X";//一条导线由多个导线点组成。

        //坐标Y
        public const string COORDINATE_Y = "COORDINATE_Y";

        //坐标Z
        public const string COORDINATE_Z = "COORDINATE_Z";//格式：YYYYMMDD

        //距左帮距离
        public const string DISTANCE_FROM_THE_LEFT = "DISTANCE_FROM_THE_LEFT";

        //距右帮距离
        public const string DISTANCE_FROM_THE_RIGHT = "DISTANCE_FROM_THE_RIGHT";

        //距顶板距离
        public const string DISTANCE_FROM_TOP = "DISTANCE_FROM_TOP";

        //距底板距离
        public const string DISTANCE_FROM_BOTTOM = "DISTANCE_FROM_BOTTOM";

        //BID
        public const string BINDINGID = "BINDINGID";

        //导线名称
        public const string WIRE_INFO_ID = "WIRE_INFO_ID";

        //巷道ID
        public const string TUNNEL_ID = "TUNNEL_ID";
    }
}
