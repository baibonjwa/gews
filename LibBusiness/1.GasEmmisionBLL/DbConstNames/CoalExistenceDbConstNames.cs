// ******************************************************************
// 概  述：井下数据煤层赋存信息数据库常量名
// 作  者：梁俊丽
// 创建日期：2014/01/17
// 版本号：V1.0
// 版本信息：
// V1.0 新建
// ******************************************************************
namespace LibBusiness
{
    public static class CoalExistenceDbConstNames
    {
        //表名
        public const string TABLE_NAME = "T_COAL_EXISTENCE";//井下数据煤层赋存信息表

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


        //是否层理紊乱
        public const string IS_LEVEL_DISORDER = "IS_LEVEL_DISORDER";

        //煤厚变化
        public const string COAL_THICK_CHANGE = "COAL_THICK_CHANGE";

        //软分层（构造煤）厚度
        public const string TECTONIC_COAL_THICK = "TECTONIC_COAL_THICK";//黄色预警、红色预警

        //是否软分层（构造煤）层位是否发生变化
        public const string IS_LEVEL_CHANGE = "IS_LEVEL_CHANGE";

        //工作面煤层是否处于分叉、合层状态
        public const string IS_COAL_MERGE = "IS_COAL_MERGE";

        //煤层是否松软
        public const string IS_COAL_SOFT = "IS_COAL_SOFT";

        //煤体破坏类型
        public const string COAL_DISTORY_LEVEL = "COAL_DISTORY_LEVEL";

        //是否煤层走向、倾角突然急剧变化
        public const string IS_TOWARDS_CHANGE = "IS_TOWARDS_CHANGE";
    }
}
