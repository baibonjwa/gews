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
    public static class BigFaultagePointDbConstNames
    {
        // 表名
        public const string TABLE_NAME = "T_BIG_FAULTAGE_POINT"; // 大断层表

        public const string ID = "ID";

        // 断层编号
        public const string BIG_FAULTAGE_ID = "BIG_FAULTAGE_ID";

        public const string UPORDOWN = "UPORDOWN";

        public const string COORDINATE_X = "COORDINATE_X";


        public const string COORDINATE_Y = "COORDINATE_Y";


        public const string COORDINATE_Z = "COORDINATE_Z";

        public const string BINDINGID = "BINDINGID";


        // 揭露点
        //public const string EXPOSE_POINTS = "EXPOSE_POINTS"; // 存放断层名（小），一个大断层是由多个小断层组成的。

        // BID
        //public const string BID = "BID";												
    }
}
