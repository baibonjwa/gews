// ******************************************************************
// 概  述：瓦斯浓度探头数据数据库常量名
// 作  者：梁俊丽
// 创建日期：2014/01/17
// 版本号：V1.0
// 版本信息：
// V1.0 新建
// ******************************************************************
namespace LibBusiness
{
    public static class GasConcentrationProbeDataDbConstNames
    {
        // 表名
        public const string TABLE_NAME = "T_GAS_CONCENTRATION_PROBE_DATA"; // 瓦斯浓度探头数据表

        // 探头数据编号
        public const string PROBE_DATA_ID = "PROBE_DATA_ID";

        // 探头编号
        public const string PROBE_ID = "PROBE_ID";

        // 探头数值
        public const string PROBE_VALUE = "PROBE_VALUE";

        // 记录时间
        public const string RECORD_TIME = "RECORD_TIME";

        // 记录类型
        public const string RECORD_TYPE = "RECORD_TYPE"; // 矿监控系统读取、瓦斯员上传数据
    }
}
