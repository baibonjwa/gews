// ******************************************************************
// 概  述：预警数据表绑定表通用字段名。井下数据表绑定表的字段名必须与下列字段名一致！
// 作  者：杨小颖
// 创建日期：2014/01/17
// 版本号：V1.0
// 版本信息：
// V1.0 新建
// ******************************************************************
namespace LibBusiness
{
    public static class WarningDataBindingTableCommonDbConstNames
    {
        //预警数据字段名称
        public const string COLUMN_NAME = 
            "COLUMN_NAME";//预警计算时会根据字段名查找对应的数据。一个字段（预警数据）可能会对应多条规则编码（如：红色预警规则编码和黄色预警规则编码同时对应一个字段），该种情况只需在填写数据时增加一条新的记录即可。
        //预警数据字段对应的规则编码
        public const string BINDING_WARNING_RULES = 
            "BINDING_WARNING_RULES";//字段绑定的规则编码，注意：如若一个字段对应多条规则编码，添加一条新的记录即可，不要使用逗号分隔规则编码！
        //字段使用方式
        public const string COLUMN_USE_MANNER = 
            "COLUMN_USE_MANNER";//字段使用方式，目前包含：直接使用、计算最近距离(详见预警规则计算流程.vsd)。当字段使用方式为计算最近距离时，当前表中COLUMN_NAME在预警数据表中随便选一个即可。字段使用方式为 
            //非直接使用 的 一种字段使用方式只能对应一条记录
    }
}
