// ******************************************************************
// 概  述：钻孔数据库常量名
// 作  者：梁俊丽
// 创建日期：2014/01/17
// 版本号：1.0
// 版本号：V1.0
// 版本信息：
// V1.0 新建
// ******************************************************************
namespace LibBusiness
{
    public static class BoreholeDBConstNames
    {
        // 表名
        public const string TABLE_NAME = "T_BOREHOLE"; // 钻孔表

        // 钻孔编号
        public const string BOREHOLE_ID = "BOREHOLE_ID"; // 唯一标识

        // 孔号
        public const string BOREHOLE_NUMBER = "BOREHOLE_NUMBER"; // 单位：m

        // 地面标高
        public const string GROUND_ELEVATION = "GROUND_ELEVATION"; // 单位：m

        // X坐标
        public const string COORDINATE_X = "COORDINATE_X";

        // Y坐标
        public const string COORDINATE_Y = "COORDINATE_Y";

        // Z坐标
        public const string COORDINATE_Z = "COORDINATE_Z";

        // 煤层结构
        public const string COAL_SEAMS_TEXTURE = "COAL_SEAMS_TEXTURE"; // 参照：钻孔岩层表

        // BID
        public const string BID = "BID";

        //※岩性信息管理中可对岩性进行增删查改。
    }
}
