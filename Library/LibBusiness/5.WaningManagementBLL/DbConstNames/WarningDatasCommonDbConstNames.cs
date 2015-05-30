// ******************************************************************
// 概  述：预警数据通用数据库字段名。注意：新增预警数据表时，下列字段名必须一致！
// 作  者：杨小颖
// 创建日期：2014/01/17
// 版本号：V1.0
// 版本信息：
// V1.0 新建
// ******************************************************************
namespace LibBusiness
{
    public static class WarningDatasCommonDbConstNames
    {
        //巷道编号
        public const string TUNNEL_ID = "TUNNEL_ID";

        //日期
        public const string DATETIME = "DATETIME";

        //坐标X
        public const string X = "COORDINATE_X";

        //坐标Y
        public const string Y = "COORDINATE_Y";

        //坐标Z
        public const string Z = "COORDINATE_Z";
    }
}
