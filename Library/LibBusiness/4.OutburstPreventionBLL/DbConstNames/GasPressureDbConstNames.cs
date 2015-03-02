// ******************************************************************
// 概  述：瓦斯压力点数据库常量名
// 作  者：杨小颖
// 创建日期：2014/01/15
// 版本号：V1.0
// 版本信息：
// V1.0 新建
// ******************************************************************

using System.Collections.Generic;
using Microsoft.Office.Interop.Excel;

namespace LibBusiness
{
    public static class GasPressureDbConstNames
    {
        // 表名
        public const string TABLE_NAME = "T_GAS_PRESSURE"; // 瓦斯压力表

        // 主键
        public const string ID = "PRIMARY_KEY";

        // 坐标X
        public const string X = "COORDINATE_X";

        // 坐标Y
        public const string Y = "COORDINATE_Y";

        // 坐标Z
        public const string Z = "COORDINATE_Z"; // 测点标高,单位：m

        // 埋深
        public const string DEPTH = "DEPTH"; // 单位：m

        // 瓦斯压力值
        public const string GAS_PRESSURE_VALUE = "GAS_PRESSURE_VALUE";

        // 测定时间
        public const string MEASURE_DATE_TIME = "MEASURE_DATE_TIME"; // 精确到时分秒

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
