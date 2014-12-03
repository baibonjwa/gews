// ******************************************************************
// 概  述：采掘掘进进度日报数据库常量名
// 作  者：梁俊丽
// 创建日期：2014/01/17
// 版本号：1.0
// 版本号：V1.0
// 版本信息：
// V1.0 新建
// ******************************************************************
namespace LibBusiness
{
    public static class TeamDbConstNames
    {
        //表名
        public const string TABLE_NAME = "T_TEAM_INFO";//采掘掘进进度日报表

        //掘进编号
        public const string ID = "TEAM_ID";//主键，内部识别ID

        //队别名称
        public const string TEAM_NAME = "TEAM_NAME";

        //队长名称
        public const string TEAM_LEADER = "TEAM_LEADER";

        //队员名称
        public const string TEAM_MEMBER = "TEAM_MEMBER";
    }
}
