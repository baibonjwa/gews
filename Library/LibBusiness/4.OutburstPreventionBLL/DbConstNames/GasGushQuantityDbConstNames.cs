// ******************************************************************
// 概  述：瓦斯涌出量点数据库常量名
// 作  者：梁俊丽
// 创建日期：2014/01/16
// 版本号：V1.0
// 版本信息：
// V1.0 新建
// ******************************************************************
namespace LibBusiness
{
    public static class GasGushQuantityDbConstNames
    {
        // 表名
        public const string TABLE_NAME = "T_GAS_GUSH_QUANTITY"; // 回采工作面瓦斯涌出量表

        // 主键
        public const string ID = "PRIMARY_KEY";

        //坐标X
        public const string X = "COORDINATE_X";

        // 坐标Y
        public const string Y = "COORDINATE_Y";

        //坐标Z
        public const string Z = "COORDINATE_Z";

        //绝对瓦斯涌出量
        public const string ABSOLUTE_GAS_GUSH_QUANTITY = 
            "ABSOLUTE_GAS_GUSH_QUANTITY"; // 单位：m³/min

        // 相对瓦斯涌出量
        public const string RELATIVE_GAS_GUSH_QUANTITY = 
            "RELATIVE_GAS_GUSH_QUANTITY"; // 单位：m³/t

        //工作面日产量
        public const string WORKING_FACE_DAY_OUTPUT = 
            "WORKING_FACE_DAY_OUTPUT"; // 单位：t

        //回采年月
        public const string STOPE_DATE = "STOPE_DATE"; // 时间只精确到年月。格式：YYYY年MM月

        // 巷道编号
        public const string TUNNEL_ID = "TUNNEL_ID";

        // 煤层编号
        public const string COAL_SEAMS_ID = "COAL_SEAMS_ID";

        // BID
        public const string BID = "BID";

        /* 备注
         * ※同一位置、同一时间不能重复录入。其中，坐标误差允许范围0.1，如：坐标p1（42.3,6,6）和坐标p2（42.2,6,6）,
             则认为两个坐标相同。
         * ※时间误差允许范围10分钟
         */
    }
}
