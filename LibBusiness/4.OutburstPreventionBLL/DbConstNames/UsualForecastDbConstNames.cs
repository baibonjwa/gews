// ******************************************************************
// 概  述：井下数据日常预测信息数据库常量名
// 作  者：梁俊丽
// 创建日期：2014/01/16
// 版本号：V1.0
// 版本信息：
// V1.0 新建
// ******************************************************************
namespace LibBusiness
{
    public static class UsualForecastDbConstNames
    {
        //表名
        public const string TABLE_NAME = "T_USUAL_FORECAST";//井下数据日常预测信息表

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

        //是否有顶板下沉
        public const string IS_ROOF_DOWN = "IS_ROOF_DOWN";

        //是否支架变形与折损
        public const string IS_SUPPORT_BROKEN = "IS_SUPPORT_BROKEN";

        //是否煤壁片帮
        public const string IS_COAL_WALL_DROP = "IS_COAL_WALL_DROP";

        //是否局部冒顶
        public const string IS_PART_ROOF_FALL = "IS_PART_ROOF_FALL";

        //是否顶板沿工作面煤壁切落
        public const string IS_BIG_ROOF_FALL = "IS_BIG_ROOF_FALL";
    }
}
