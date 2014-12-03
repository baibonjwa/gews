// ******************************************************************
// 概  述：井下数据瓦斯信息数据库常量名
// 作  者：梁俊丽
// 创建日期：2014/01/16
// 版本号：V1.0
// 版本信息：
// V1.0 新建
// ******************************************************************
namespace LibBusiness
{
    public static class GasDataDbConstNames
    {
        //表名
        public const string TABLE_NAME = "T_GASDATA";//井下数据瓦斯信息表

        //编号
        public const string ID = "ID";

        //巷道编号
        public const string TUNNEL_ID = "TUNNEL_ID";

        //坐标X
        public const string X = "COORDINATE_X";

        //坐标Y
        public const string Y = "COORDINATE_Y";

        //坐标Z
        public const string Z = "COORDINATE_Z";

        //日期
        public const string DATETIME = "DATETIME";

        //工作制式
        public const string WORK_STYLE = "WORK_STYLE";

        //班次
        public const string WORK_TIME = "WORK_TIME";

        //队别名称
        public const string TEAM_NAME = "TEAM_NAME";

        //填报人
        public const string SUBMITTER = "SUBMITTER";

        //瓦斯探头断电次数
        public const string POWER_FALIURE = "POWER_FALIURE";

        //吸钻预兆次数
        public const string DRILL_TIMES = "DRILL_TIMES";

        //瓦斯忽大忽小预兆次数
        public const string GAS_TIMES = "GAS_TIMES";

        //气温下降预兆次数
        public const string TEMP_DOWN_TIMES = "TEMP_DOWN_TIMES";

        //煤炮频繁预兆次数
        public const string COAL_BANG_TIMES = "COAL_BANG_TIMES";

        //喷孔次数
        public const string CRATER_TIMES = "CRATER_TIMES";

        //顶钻次数
        public const string STOPER_TIMES = "STOPER_TIMES";

        //瓦斯浓度
        public const string GAS_THICKNESS = "GAS_THICKNESS";
    }
}
