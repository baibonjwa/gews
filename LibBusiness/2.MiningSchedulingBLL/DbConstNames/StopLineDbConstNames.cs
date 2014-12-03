// ******************************************************************
// 概  述：停采线数据管理数据库常量名
// 作  者：梁俊丽
// 创建日期：2014/01/17
// 版本号：V1.0
// 版本信息：
// V1.0 新建
// ******************************************************************
namespace LibBusiness
{
    public static class StopLineDbConstNames
    {
        //表名
        public const string TABLE_NAME = "T_STOP_LINE_INFO";//停采线数据管理表

        //主键
        public const string ID = "OBJECTID";//主键，内部识别ID

        //停采线名称
        public const string STOP_LINE_NAME = "STOP_LINE_NAME";

        //起点坐标X
        public const string S_COORDINATE_X = "S_COORDINATE_X";

        //起点坐标Y
        public const string S_COORDINATE_Y = "S_COORDINATE_Y";

        //起点坐标Z
        public const string S_COORDINATE_Z = "S_COORDINATE_Z";

        //终点坐标X
        public const string F_COORDINATE_X = "F_COORDINATE_X";

        //终点坐标Y
        public const string F_COORDINATE_Y = "F_COORDINATE_Y";

        //起终点坐标Z
        public const string F_COORDINATE_Z = "F_COORDINATE_Z";

        //BID
        public const string BINDINGID = "BINDINGID";
    }
}
