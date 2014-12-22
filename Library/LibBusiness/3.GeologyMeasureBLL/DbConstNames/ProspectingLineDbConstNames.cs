// ******************************************************************
// 概  述：勘探线数据库常量名
// 作  者：伍鑫
// 创建日期：2014/03/05
// 版本号：V1.0
// 版本信息：
// V1.0 新建
// ******************************************************************
namespace LibBusiness
{
    public static class ProspectingLineDbConstNames
    {
        // 表名
        public const string TABLE_NAME = "T_PROSPECTING_LINE_INFO"; // 勘探线信息表

        // 勘探线编号
        public const string PROSPECTING_LINE_ID = "PROSPECTING_LINE_ID";

        // 勘探线名称
        public const string PROSPECTING_LINE_NAME = "PROSPECTING_LINE_NAME";

        // 勘探钻孔
        public const string PROSPECTING_BOREHOLE = "PROSPECTING_BOREHOLE"; // 存放组成这条勘探线的勘探钻孔的编号

        // BID
        public const string BID = "BID";												
    }
}
