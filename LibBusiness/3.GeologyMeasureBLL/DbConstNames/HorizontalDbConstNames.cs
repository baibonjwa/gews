// ******************************************************************
// 概  述：水平数据库常量名
// 作  者：伍鑫
// 创建日期：2014/02/25
// 版本号：V1.0
// 版本信息：
// V1.0 新建
// ******************************************************************
namespace LibBusiness
{
    public static class HorizontalDbConstNames
    {
        // 表名
        public const string TABLE_NAME = "T_HORIZONTAL_INFO"; // 水平信息表

        // 水平编号
        public const string HORIZONTAL_ID = "HORIZONTAL_ID"; // 主键

        // 水平名称
        public const string HORIZONTAL_NAME = "HORIZONTAL_NAME";

        // 矿井编号
        public const string MINE_ID = "MINE_ID"; // 外键
    }
}
