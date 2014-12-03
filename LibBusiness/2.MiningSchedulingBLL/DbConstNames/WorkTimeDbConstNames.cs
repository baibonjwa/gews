// ******************************************************************
// 概  述：采掘班次数据库常量名
// 作  者：梁俊丽
// 创建日期：2014/01/17
// 版本号：V1.0
// 版本信息：
// V1.0 新建
// ******************************************************************
namespace LibBusiness
{
    public static class WorkTimeDbConstNames
    {
        //表名
        public const string TABLE_NAME = "T_WORK_TIME";//采掘班次表

        //班次编号
        public const string WORK_TIME_ID = "WORK_TIME_ID";//主键，内部识别ID

        //班次分组编号
        public const string WORK_TIME_GROUP_ID = "WORK_TIME_GROUP_ID";//1为三八制，2为四六制

        //班次名称
        public const string WORK_TIME_NAME = "WORK_TIME_NAME";

        //起始时间
        public const string WORK_TIME_FROM = "WORK_TIME_FROM";

        //终止时间
        public const string WORK_TIME_TO = "WORK_TIME_TO";

        //默认班次表表名
        public const string DEFAULT_WORK_TIME_TABLE_NAME = "T_WORK_TIME_DEFAULT";

        //默认班次ID
        public const string DEFAULT_WORK_TIME_GROUP_ID = "DEFAULT_WORK_TIME_GROUP_ID";
    }
}
