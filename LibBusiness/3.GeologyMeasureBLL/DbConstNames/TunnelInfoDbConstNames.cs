// ******************************************************************
// 概  述：巷道信息数据库常量名
// 作  者：梁俊丽
// 创建日期：2014/01/17
// 版本号：V1.0
// 版本信息：
// V1.0 新建
// ******************************************************************
namespace LibBusiness
{
    public static class TunnelInfoDbConstNames
    {
        //表名
        public const string TABLE_NAME = "T_TUNNEL_INFO"; //巷道信息表

        // 巷道编号
        public const string ID = "TUNNEL_ID";

        //巷道名称
        public const string TUNNEL_NAME = "TUNNEL_NAME";

        //支护方式
        public const string SUPPORT_PATTERN = "SUPPORT_PATTERN";

        //围岩类型ID
        public const string LITHOLOGY_ID = "LITHOLOGY_ID";

        // 断面类型
        public const string SECTION_TYPE = "SECTION_TYPE";

        // 断面参数
        public const string PARAM = "PARAM";

        // 设计长度
        public const string DESIGNLENGTH = "DESIGNLENGTH";

        // 设计面积
        public const string DESIGNAREA = "DESIGNAREA";

        //巷道类型
        public const string TUNNEL_TYPE = "TUNNEL_TYPE";

        //煤巷岩巷
        public const string COAL_OR_STONE = "COAL_OR_STONE";

        //煤层
        public const string COAL_LAYER_ID = "COAL_LAYER_ID";

        //BID
        public const string BINDINGID = "BINDINGID";

        // 规则编码
        public const string RULE_IDS = "RULE_IDS"; //规则编码可为多个，使用分号分隔

        // 预警规则参数
        public const string PRE_WARNING_PARAMS = "PRE_WARNING_PARAMS";
            //预警参数与规则编码对应，可为0，也可为多个，且每个规则编码可对应多个参数，不同规则编码对应的预警规则参数用分号分隔，相同规则编码对应的规则参数用逗号分隔。参数名用[]区分。如:[P](10);[MIN](1),[MAX](5);									

        //工作面编号
        public const string WORKINGFACE_ID = "WORKINGFACE_ID";

        //巷道宽度
        public const string TUNNEL_WID = "TUNNEL_WID";
    }
}
