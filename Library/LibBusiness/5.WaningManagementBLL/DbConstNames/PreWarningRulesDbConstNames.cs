// ******************************************************************
// 概  述：预警规则数据库常量名
// 作  者：梁俊丽
// 创建日期：2014/01/17
// 版本号：V1.0
// 版本信息：
// V1.0 新建
// ******************************************************************
namespace LibBusiness
{
    public static class PreWarningRulesDbConstNames
    {
        //表名
        public const string TABLE_NAME = "T_PRE_WARNING_RULES";//预警规则表

        //规则ID
        public const string RULE_ID = "RULE_ID";//主键，内部识别ID

        //规则编码
        public const string RULE_CODE = "RULE_CODE";//编码，唯一，不能重复

        //规则类别
        public const string RULE_TYPE = 
            "RULE_TYPE";//瓦斯、煤层赋存、地质构造、通风、采掘影响、防突措施、日常预测、管理因素、其他

        //预警类型
        public const string WARNING_TYPE = "WARNING_TYPE";//趋势预警、状态预警

        //预警级别
        public const string WARNING_LEVEL = "WARNING_LEVEL";//红色预警、黄色预警

        //适用位置
        public const string SUITABLE_LOCATION = 
            "SUITABLE_LOCATION";//掘进工作面、回采工作面、掘进和回采工作面、整个矿井

        //规则描述
        public const string RULE_DESCRIPTION = 
            "RULE_DESCRIPTION";//规则描述当中参数名使用[]标识，参数值使

        //指标类型
        public const string INDICATOR_TYPE = "INDICATOR_TYPE";//用()标识

        //比较符
        public const string OPERATOR = "OPERATOR";//＞,≥,＜,≤,＝,
            ≠；多个比较符用逗号分隔，比较符个数与参数个数是一一对应的

        //修改日期
        public const string MODIFY_DATE = "MODIFY_DATE";//修改日期，精确到时分秒

        //备注
        public const string REMARKS = "REMARKS";
        //※规则描述举例：当距离构造[L](20)米以内，启动黄色预警。注:[]、()符号均为英文半角

        //绑定的数据库表名称
        public const string BINDING_TABLE_NAME = "BINDING_TABLE_NAME";

        //绑定的数据库表字段个名称
        public const string BINDING_COLUMN_NAME = "BINDING_COLUMN_NAME";

        //字段使用方式
        public const string USE_TYPE = "USE_TYPE";

        //组合规则绑定的单一规则编码
        public const string BINDING_SINGLERULES = "BINDING_SINGLERULES";
    }
}
