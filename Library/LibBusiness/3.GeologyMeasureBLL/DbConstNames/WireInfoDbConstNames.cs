namespace LibBusiness
{
    public static class WireInfoDbConstNames
    {
        //表名
        public const string TABLE_NAME = "T_WIRE_INFO";//导线信息表

        // 导线编号
        public const string ID = "OBJECTID";

        //导线名称
        public const string WIRE_NAME = "WIRE_NAME";

        //导线点
        public const string WIRE_POINT = "WIRE_POINT";//一条导线由多个导线点组成。

        //导线级别
        public const string WIRE_LEVEL = "WIRE_LEVEL";

        //测量日期
        public const string MEASURE_DATE = "MEASURE_DATE";//格式：YYYYMMDD

        //观测者
        public const string VOBSERVER = "VOBSERVER";

        //计算者
        public const string COUNTER = "COUNTER";

        //计算日期
        public const string COUNT_DATE = "COUNT_DATE";

        //校核者
        public const string CHECKER = "CHECKER";

        //校核日期
        public const string CHECK_DATE = "CHECK_DATE";

        //巷道编号
        public const string TUNNEL_ID = "TUNNEL_ID";
    }
}
