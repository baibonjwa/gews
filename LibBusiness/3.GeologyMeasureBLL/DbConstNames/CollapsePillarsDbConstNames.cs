// ******************************************************************
// 概  述：陷落柱数据数据库常量名
// 作  者：梁俊丽
// 创建日期：2014/01/17
// 版本号：1.0
// 版本号：V1.0
// 版本信息：
// V1.0 新建
// ******************************************************************
namespace LibBusiness
{
    public static class CollapsePillarsDbConstNames
    {
        //表名
        public const string TABLE_NAME = "T_COLLAPSE_PILLARS";//陷落柱数据表

        //陷落柱编号
        public const string COLLAPSE_PILLAR_ID = "COLLAPSE_PILLAR_ID";

        //陷落柱名称
        public const string COLLAPSE_PILLAR_NAME = "COLLAPSE_PILLAR_NAME";

        //位置X
        public const string LOCATION_X = "LOCATION_X";//一条导线由多个导线点组成。

        //位置Y
        public const string LOCATION_Y = "LOCATION_Y";

        //位置Z
        public const string LOCATION_Z = "LOCATION_Z";//格式：YYYYMMDD

        //长轴长
        public const string LONG_AXIS_LENGTH = "LONG_AXIS_LENGTH";//单位：m

        //短轴长
        public const string SHORT_AXIAL_LENGTH = "SHORT_AXIAL_LENGTH";//单位：m

        //揭露点
        public const string EXPOSE_POINTS = "EXPOSE_POINTS";//揭露点1 - 揭露点n

        //描述
        public const string DESCRIBE = "DESCRIBE";
    }
}
