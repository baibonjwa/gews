// ******************************************************************
// 概  述：钻孔岩性数据库常量名
// 作  者：梁俊丽
// 创建日期：2014/01/17
// 版本号：V1.0
// 版本信息：
// V1.0 新建
// ******************************************************************
namespace LibBusiness
{
    public static class BoreholeLithologyDbConstNames
    {
        //表名
        public const string TABLE_NAME = "T_BOREHOLE_LITHOLOGY";//钻孔岩性表

        //钻孔编号
        public const string BOREHOLE_ID = "BOREHOLE_ID";//关联表为：钻孔表

        //岩性编号
        public const string LITHOLOGY_ID = "LITHOLOGY_ID";//关联表为：岩性表

        //底板标高
        public const string FLOOR_ELEVATION = "FLOOR_ELEVATION";//单位：m

        //厚度
        public const string THICKNESS = "THICKNESS";//单位：m		

        //煤层名称
        public const string COAL_SEAMS_NAME = "COAL_SEAMS_NAME";

        //坐标X
        public const string COORDINATE_X = "COORDINATE_X";

        //坐标Y
        public const string COORDINATE_Y = "COORDINATE_Y";

        //坐标Z
        public const string COORDINATE_Z = "COORDINATE_Z";	
    }
}
