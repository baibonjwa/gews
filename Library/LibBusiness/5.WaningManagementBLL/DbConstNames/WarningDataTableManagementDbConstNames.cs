// ******************************************************************
// 概  述：预警数据表管理表
// 作  者：杨小颖
// 创建日期：2014/01/17
// 版本号：V1.0
// 版本信息：
// V1.0 新建
// ******************************************************************
namespace LibBusiness
{
    public static class WarningDataTableManagementDbConstNames
    {
        //表名(预警数据表管理表名!)
        public const string TABLE_NAME = 
            "T_PRE_WARNING_DATA_TABLE_MANAGEMENT";
        //预警数据表名
        public const string DATA_TABLE_NAME = "DATA_TABLE_NAME";
        //预警数据字段与规则编码对应关系表名
        public const string BINDING_TABLE_NAME = "BINDING_TABLE_NAME";
        //是否需要考虑约束条件
        public const string NEED_CONSTRAINS = 
            "NEED_CONSTRAINS";//如：断层、陷落柱等构造不需要考虑时间及巷道ID等约束条件。代码中转换为bool
    }
}
