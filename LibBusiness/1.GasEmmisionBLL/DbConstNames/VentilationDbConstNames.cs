// ******************************************************************
// 概  述：井下数据通风信息数据库常量名
// 作  者：梁俊丽
// 创建日期：2014/01/17
// 版本号：V1.0
// 版本信息：
// V1.0 新建
// ******************************************************************
namespace LibBusiness
{
    public static class VentilationDbConstNames
    {
        //表名
        public const string TABLE_NAME = "T_VENTILATION_INFO";//井下数据通风信息表

        //编号
        public const string ID = "ID";

        //巷道编号
        public const string TUNNEL_ID = "TUNNEL_ID";

        //坐标X
        public const string COORDINATE_X = "COORDINATE_X";

        //坐标Y
        public const string COORDINATE_Y = "COORDINATE_Y";

        //坐标Z
        public const string COORDINATE_Z = "COORDINATE_Z";

        //工作制式
        public const string WORK_STYLE = "WORK_STYLE";

        //班次
        public const string WORK_TIME = "WORK_TIME";

        //队别名称
        public const string TEAM_NAME = "TEAM_NAME";

        //填报人
        public const string SUBMITTER = "SUBMITTER";

        //日期
        public const string DATETIME = "DATETIME";

        //是否有无风区域
        public const string IS_NO_WIND_AREA = "IS_NO_WIND_AREA";

        //是否有微风区域
        public const string IS_LIGHT_WIND_AREA = "IS_LIGHT_WIND_AREA";

        //是否有风流反向区域
        public const string IS_RETURN_WIND_AREA = "IS_RETURN_WIND_AREA";//黄色预警、红色预警

        //是否通风断面小于设计断面的2/3
        public const string IS_SMALL = "IS_SMALL";

        //是否工作面风量低于计划风量，风速与《煤矿安全规程》规定不符
        public const string IS_FOLLOW_RULE = "IS_FOLLOW_RULE";

        //通风断面
        public const string FAULTAGE_AREA = "FAULTAGE_AREA";

        //风量
        public const string AIR_FLOW = "AIR_FLOW";
    }
}
