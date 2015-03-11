namespace LibEntity
{
    /// <summary>
    ///     注意各个枚举值的大小，值越大，越危险
    /// </summary>
    public enum WARNING_LEVEL_RESULT
    {
        NODATA = 0, //无数据
        GREEN = 1, //绿色
        YELLOW = 2, //黄色
        RED = 3, //红色
    }

    /// <summary>
    ///     预警结果实体
    ///     目前只存储预警规则实体，日后扩展时再添加
    /// </summary>
    public class PreWarningResult
    {
        //预警规则实体

        public PreWarningRules PreWarningRulesEntity { get; set; }

        //预警数据通用信息实体

        public PreWarningDataCommonInfo WarningDataCommonInfoEnt { get; set; }
    }
}