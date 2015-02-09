// ******************************************************************
// 概  述：系统二常量名
// 作  者：宋英杰
// 创建日期：2014/03/12
// 版本号：V1.0
// 版本信息：
// V1.0 新建
// ******************************************************************
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LibCommon
{
    public class Const_MS
    {
        public const string ADD = "添加";
        public const string CHANGE = "修改";

        public const char SIGN_EXCLAMATION_MARK = '！';

        public const string TEAM_NAME = "队别名称";
        public const string SUBMITTER = "填报人";
        public const string JC = "进尺";
        public const string JJ = "掘进";
        public const string HC = "回采";
        public const string TUNNEL = "巷道";
        public const string WORKINGFACE = "工作面";
        public const string WORKINGFACEEXIST = "该工作面已存在";
        public const string DAY_REPORT_HC_EXIST = "该条回采记录已经存在，不能重复添加，请选择其他日期或班次，或者对该条记录进行修改";
        public const string OFC = "距切眼距离";
        public const string DFW = "距参考导线点距离";
        public const string OTHER = "备注";
        public const string STOP_LINE_NAME = "停采线名称";
        public const string X = "X";
        public const string Y = "Y";
        public const string Z = "Z";
        public const string SCOORDINATE = "起点坐标";
        public const string FCOORDINATE = "终点坐标";
        public const string NAME = "姓名";
        public const string TEAM_LEADER = "队长";
        public const string TEAM_MEMBER = "队员";
        public const string WORK_TIME_NAME = "班次名";
        public const string WORK_TIME_FROM = "开始时间";
        public const string WORK_TIME_TO = "结束时间";
        public const string CANNOT_BIGGER_THAN = "不能大于";


        public const string MSG_UPDATE_SUCCESS = "提交成功";
        public const string MSG_UPDATE_FAILURE = "提交失败";
        public const string MSG_DELETE_SUCCESS = "删除成功";
        public const string MSG_DELETE_FAILURE = "删除失败";
        public const string MSG_EXPORT_SUCCESS = "导出成功";
        public const string MSG_EXPORT_FAILURE = "导出失败";

        /**掘进日报**/
        public const string DAY_REPORT_JJ_ADD = "添加掘进进尺日报";
        public const string DAY_REPORT_JJ_CHANGE = "修改掘进进尺日报";
        public const string DAY_REPORT_JJ_MANAGEMENT = "掘进进尺日报";
        public const string DAY_REPORT_JJ_FARPOINT_TITLE = "掘进进尺日报";
        public const string DAY_REPORT_JJ_DEFAULT_TIME = "2013/1/1 00:00";
        public const string DAY_REPORT_HC_MSG_TUNNEL_CHOOSED_AS_JJ = "所选巷道正在掘进,请重新选择巷道或修改已有掘进日报信息";

        /**回采日报**/
        public const string DAY_REPORT_HC = "回采进尺日报";
        public const string DAY_REPORT_HC_ADD = "添加回采进尺日报";
        public const string DAY_REPORT_HC_CHANGE = "修改回采进尺日报";
        public const string DAY_REPORT_HC_MANAGEMENT = "回采进尺日报";
        public const string DAY_REPORT_HC_FARPOINT_TITLE = "回采进尺日报";
        public const string DAY_REPORT_HC_DEFAULT_TIME = "2013/1/1 00:00";
        public const string DAY_REPORT_HC_MSG_TUNNEL_CHOOSED_AS_HC = "所选巷道正在回采,请重新选择巷道或修改已有回采日报信息";

        /**停采线**/
        public const string STOP_LINE_ADD = "添加停采线";
        public const string STOP_LINE_CHANGE = "修改停采线";
        public const string STOP_LINE_MANAGEMENT = "停采线";
        public const string STOP_LINE_FARPOINT_TITLE = "停采线";
        public const string STOP_LINE = "停采线";

        /**队别**/
        public const string TEAM_INFO_ADD = "添加队别";
        public const string TEAM_INFO_CHANGE = "修改队别";
        public const string TEAM_INFO_MANAGEMENT = "队别管理";
        public const string TEAM_INFO_FARPOINT_TITLE = "队别管理";
        public const string TEAM_INFO_MSG_NEED_AT_LEAST_ONE = "请至少添加一名队员";
        public const string TEAM_INFO_MSG_TEAM_NAME_EXIST = "该队别已存在！";
        public const char TEAM_INFO_MEMBER_BREAK_SIGN = ',';

        /**添加队员姓名**/
        public const string INPUT_NAME = "请输入姓名";

        /**工作制式**/
        public const string WORK_TIME_MANAGEMENT = "班次管理";
        public const string WORK_TIME_38 = "三八制";
        public const string WORK_TIME_46 = "四六制";
        public const string WORK_TIME_FORMAT = "HH:mm:ss";
        public const string DEFAULT_TIME_MORNING = "早班";
        public const string DEFAULT_TIME_MORNING_FROM = "00:00:00";
        public const string DEFAULT_TIME_MORNING_TO = "07:59:59";
        public const string DEFAULT_TIME_NOON = "中班";
        public const string DEFAULT_TIME_NOON_FROM = "08:00:00";
        public const string DEFAULT_TIME_NOON_TO = "15:59:59";
        public const string DEFAULT_TIME_EVENING = "晚班";
        public const string DEFAULT_TIME_EVENING_FROM = "16:00:00";
        public const string DEFAULT_TIME_EVENING_TO = "23:59:59";
        public const string DEFAULT_TIME_0 = "0点班";
        public const string DEFAULT_TIME_0_FROM = "00:00:00";
        public const string DEFAULT_TIME_0_TO = "05:59:59";
        public const string DEFAULT_TIME_6 = "6点班";
        public const string DEFAULT_TIME_6_FROM = "06:00:00";
        public const string DEFAULT_TIME_6_TO = "11:59:59";
        public const string DEFAULT_TIME_12 = "12点班";
        public const string DEFAULT_TIME_12_FROM = "12:00:00";
        public const string DEFAULT_TIME_12_TO = "17:59:59";
        public const string DEFAULT_TIME_18 = "18点班";
        public const string DEFAULT_TIME_18_FROM = "18:00:00";
        public const string DEFAULT_TIME_18_TO = "23:59:59";

        public const string WORK_GROUP_ID_38 = "1";
        public const string WORK_GROUP_ID_46 = "2";

        public const string WORK_TIME_MSG_IS_TRUNCATE = "确认要恢复初始设置吗？（恢复操作会清空当前设置的班次信息，并恢复为初始班次信息。）";
        public const string WORK_TIME_MSG_CHANGE_DEFAULT_WORK_TIME_SUCCESS = "成功修改默认工作制式为：";

        /** 帮助文件 **/
        public const string System2_Help_File = "\\工作面采掘进度管理系统帮助文件.chm";
        /** 关于图片 **/
        public const string Picture_Name = "\\系统二关于图片.jpg";
    }
}
