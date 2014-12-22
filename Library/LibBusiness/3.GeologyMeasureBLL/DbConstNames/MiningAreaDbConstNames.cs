// ******************************************************************
// 概  述：采区数据库常量名
// 作  者：伍鑫
// 创建日期：2014/02/25
// 版本号：V1.0
// 版本信息：
// V1.0 新建
// ******************************************************************
namespace LibBusiness
{
    public static class MiningAreaDbConstNames
    {
        // 表名
        public const string TABLE_NAME = "T_MININGAREA_INFO"; // 采区信息表

        // 采区编号
        public const string MININGAREA_ID = "MININGAREA_ID"; // 主键

        // 采区名称
        public const string MININGAREA_NAME = "MININGAREA_NAME";

        // 水平编号
        public const string HORIZONTAL_ID = "HORIZONTAL_ID"; // 外键
    }
}
