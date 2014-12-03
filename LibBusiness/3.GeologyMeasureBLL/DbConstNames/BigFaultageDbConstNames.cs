// ******************************************************************
// 概  述：大断层数据库常量名
// 作  者：梁俊丽
// 创建日期：2014/01/17
// 版本号：V1.0
// 版本信息：
// V1.0 新建
// ******************************************************************
namespace LibBusiness
{
    public static partial class BigFaultageDbConstNames
    {
        // 表名
        public const string TABLE_NAME = "T_BIG_FAULTAGE"; // 大断层表

        // 断层编号
        public const string FAULTAGE_ID = "FAULTAGE_ID";

        // 断层名称
        public const string FAULTAGE_NAME = "FAULTAGE_NAME";

        // 类型
        public const string TYPE = "TYPE"; // 正断层、逆断层

        //断距
        public const string GAP = "GAP";

        //角度
        public const string ANGLE = "ANGLE";

        //走向
        public const string TREND = "TREND";

        // 揭露点
        //public const string EXPOSE_POINTS = "EXPOSE_POINTS"; // 存放断层名（小），一个大断层是由多个小断层组成的。

        //BID
        public const string BID = "BID";
    }
}
