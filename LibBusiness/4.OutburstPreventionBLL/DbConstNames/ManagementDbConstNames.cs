// ******************************************************************
// 概  述：井下数据管理信息数据库常量名
// 作  者：梁俊丽
// 创建日期：2014/01/16
// 版本号：V1.0
// 版本信息：
// V1.0 新建
// ******************************************************************
namespace LibBusiness
{
    public static class ManagementDbConstNames
    {
        //表名
        public const string TABLE_NAME = "T_MANAGEMENT";//井下数据管理信息表

        //编号
        public const string ID = "ID";

        #region 预警数据通用数据库字段名
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
        #endregion

        //是否存在瓦斯异常不汇报
        public const string IS_GAS_ERROR_NOT_REPORT = "IS_GAS_ERROR_NOT_REPORT";

        //是否存在工作面出现地质构造不汇报
        public const string IS_WF_NOT_REPORT = "IS_WF_NOT_REPORT";

        //是否存在强化瓦斯措施执行不到位
        public const string IS_STRGAS_NOT_DO_WELL = "IS_STRGAS_NOT_DO_WELL";

        //是否存在进回风巷隅角、尾巷管理不到位
        public const string IS_RWMANAGEMENT_NOT_DO_WELL = "IS_RWMANAGEMENT_NOT_DO_WELL";

        //是否存在通风设施人为损坏
        public const string IS_VF_BROKEN_BY_PEOPLE = "IS_VF_BROKEN_BY_PEOPLE";

        //是否存在甲烷传感器位置不当、误差大、调校超过规定
        public const string IS_ELEMENT_PLACE_NOT_GOOD = "IS_ELEMENT_PLACE_NOT_GOOD";

        //是否存在瓦检员空漏假检
        public const string IS_REPORTER_FALSE_DATA = "IS_REPORTER_FALSE_DATA";

        //钻孔未按设计施工次数
        public const string IS_DRILL_WRONG_BUILD = "IS_DRILL_WRONG_BUILD";

        //钻孔施工不到位次数
        public const string IS_DRILL_NOT_DO_WELL = "IS_DRILL_NOT_DO_WELL";

        //防突措施执行不到位次数
        public const string IS_OP_NOT_DO_WELL = "IS_OP_NOT_DO_WELL";

        //防突异常情况未汇报次数
        public const string IS_OP_ERROR_NOT_REPORT = "IS_OP_ERROR_NOT_REPORT";

        //是否存在局部通风机单回路供电或不能正常切换
        public const string IS_PART_WIND_SWITCH_ERROR = "IS_PART_WIND_SWITCH_ERROR";

        //是否存在安全监测监控系统未及时安装
        public const string IS_SAFE_CTRL_UNINSTALL = "IS_SAFE_CTRL_UNINSTALL";

        //是否存在监测监控停运
        public const string IS_CTRL_STOP = "IS_CTRL_STOP";

        //是否存在不执行瓦斯治理措施、破坏通风设施
        public const string IS_GAS_NOT_DO_WELL = "IS_GAS_NOT_DO_WELL";

        //是否高、突矿井工作面无专职瓦斯检查员
        public const string IS_MINE_NO_CHECKER = "IS_MINE_NO_CHECKER";
    }
}
