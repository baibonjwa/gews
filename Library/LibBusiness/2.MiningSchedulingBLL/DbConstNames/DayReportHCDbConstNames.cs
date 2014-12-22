// ******************************************************************
// 概  述：采掘回采进度日报数据库常量名
// 作  者：梁俊丽
// 创建日期：2014/01/17
// 版本号：V1.0
// 版本信息：
// V1.0 新建
// ******************************************************************
namespace LibBusiness
{
    public static class DayReportHCDbConstNames
    {
        //表名
        public const string TABLE_NAME = "T_DAYREPORT_HC";//采掘掘进进度日报表

        //掘进编号
        public const string ID = "OBJECTID";//主键，内部识别ID

        //队别名称
        public const string TEAM_NAME_ID = "TEAM_NAME_ID";

        //巷道编号
        public const string WORKINGFACE_ID = "WORKINGFACE_ID";

        //班次
        public const string WORK_TIME = "WORK_TIME";

        //工作制式
        public const string WORK_TIME_SYTLE = "WORK_TIME_SYTLE";

        //工作内容
        public const string WORK_INFO = "WORK_INFO";

        //进尺
        public const string JIN_CHI = "JIN_CHI";

        //距切眼距离
        public const string OPEN_OFF_CUT_DISTANCE = "OPEN_OFF_CUT_DISTANCE";

        //日期
        public const string DATETIME = "DATETIME";

        //填报人
        public const string SUBMITTER = "SUBMITTER";

        //备注
        public const string OTHER = "OTHER";

        //BID
        public const string BINDINGID = "BINDINGID";
    }
}
