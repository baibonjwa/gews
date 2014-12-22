// ******************************************************************
// 概  述：探头管理数据库常量名
// 作  者：梁俊丽
// 创建日期：2014/01/17
// 版本号：V1.0
// 版本信息：
// V1.0 新建
// ******************************************************************

namespace LibBusiness
{
    public static class ProbeManageDbConstNames
    {
        // 表名
        public const string TABLE_NAME = "T_PROBE_MANAGE"; // 探头管理表
        
        // 探头编号
        public const string PROBE_ID = "PROBE_ID";

        // 探头名称
        public const string PROBE_NAME = "PROBE_NAME";

        // 探头类型编号
        public const string PROBE_TYPE_ID = "PROBE_TYPE_ID";

        // 2014/5/29 del by wuxin - Start
        //// 探头类型编号
        //public const string PROBE_TYPE = "PROBE_TYPE";
        // 2014/5/29 del by wuxin - End

        // 2014/5/29 add by wuxin - Start
        // 探头类型显示名称
        public const string PROBE_TYPE_DISPLAY_NAME = "PROBE_TYPE_DISPLAY_NAME";

        // 测量类型
        public const string PROBE_MEASURE_TYPE = "PROBE_MEASURE_TYPE";

        // 使用方式
        public const string PROBE_USE_TYPE = "PROBE_USE_TYPE";

        // 单位
        public const string UNIT = "UNIT";
        // 2014/5/29 add by wuxin - End

        // 所在巷道编号
        public const string TUNNEL_ID = "TUNNEL_ID";

        // 探头位置坐标X
        public const string PROBE_LOCATION_X = "PROBE_LOCATION_X";

        // 探头位置坐标Y
        public const string PROBE_LOCATION_Y = "PROBE_LOCATION_Y";

        // 探头位置坐标Z
        public const string PROBE_LOCATION_Z = "PROBE_LOCATION_Z";

        // 探头描述
        public const string PROBE_DESCRIPTION = "PROBE_DESCRIPTION";

        // 是否自动位移
        public const string IS_MOVE = "IS_MOVE";

        // 距迎头距离
        public const string FAR_FROM_FRONTAL = "FAR_FROM_FRONTAL";
    }
}
