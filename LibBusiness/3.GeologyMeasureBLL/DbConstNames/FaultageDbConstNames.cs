// ******************************************************************
// 概  述：揭露断层数据库常量名
// 作  者：梁俊丽
// 创建日期：2014/01/17
// 版本号：V1.0
// 版本信息：
// V1.0 新建
// ******************************************************************
namespace LibBusiness
{
    public static class FaultageDbConstNames
    {
        // 表名
        public const string TABLE_NAME = "T_FAULTAGE"; // 断层表

        // 断层编号
        public const string FAULTAGE_ID = "FAULTAGE_ID";

        // 断层名称
        public const string FAULTAGE_NAME = "FAULTAGE_NAME";

        // 落差
        public const string GAP = "GAP"; // 单位：m

        // 倾角
        public const string ANGLE = "ANGLE"; // 单位：°

        // 类型
        public const string TYPE = "TYPE"; // 正断层、逆断层

        // 走向
        public const string TREND = "TREND";

        // 断距
        public const string SEPARATION = "SEPARATION";

        // 坐标X
        public const string X = "COORDINATE_X";

        // 坐标Y
        public const string Y = "COORDINATE_Y";

        // 坐标Z
        public const string Z = "COORDINATE_Z";

        // BID
        public const string BID = "BID";

        //※大断层数据实为小断层数据的集合，加上手工调整绘制而成。详见大断层。									
    }
}
