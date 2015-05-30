// ******************************************************************
// 概  述：系统一常量名
// 作  者：伍鑫
// 创建日期：2014/01/21
// 版本号：V1.0
// 版本信息：
// V1.0 新建
// ******************************************************************
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Text.RegularExpressions;

namespace LibCommon
{
    public static class Const_GE
    {
        /** 探头 **/
        public const string PROBE_ID = "探头编号";
        public const string PROBE_NAME = "探头名称";
        public const string PROBE_TYPE = "探头类型";
        public const string PROBE_LOCATION_X = "坐标X";
        public const string PROBE_LOCATION_Y = "坐标Y";
        public const string PROBE_LOCATION_Z = "坐标Z";

        public const string ALARMTHRESHOLD = "报警临界值";

        /** 探头信息添加窗口名 **/
        public const string INSERT_PROBE_INFO = "添加传感器";
        /** 探头信息修改窗口名 **/
        public const string UPDATE_PROBE_INFO = "修改传感器";
        /** 探头信息管理窗口名 **/
        public const string MANAGE_PROBE_INFO = "传感器管理";

        /** 探头编号存在提示信息 **/
        public const string PROBE_ID_EXIST_MSG = "探头编号已存在，请重新录入！";

        /** 探头名称存在提示信息 **/
        public const string PROBE_NAME_EXIST_MSG = "探头名称已存在，请重新录入！";

        /** 所属巷道必须选择提示信息 **/
        public const string TUNNEL_NAME_MUST_INPUT = "请选择所属巷道！";

        /** 该巷道内无探头提示信息 **/
        public const string PROBE_NOT_EXIST_IN_THIS_TUNNEL_MSG = "该巷道内无探头！";

        /** 瓦斯浓度报警信息修改窗口名 **/
        public const string UPDATE_GAS_CONCENTRATION_ALARM_INFO = "瓦斯浓度报警数据修改";

        /** 探头必须选择提示信息 **/
        public const string PROBE_MUST_CHOOSE = "请选择传感器！";

        /** 删除确认提示信息 **/
        public const string DEL_CONFIRM_MSG = "确认要删除所选探头数据吗？";

        /** 传感器数值 **/
        public const string PROBE_VALUE = "传感器数值";

        /** 传感器数据录入窗口名 **/
        public const string INSERT_GAS_CONCENTRATION_PROBE_DATA = "添加传感器数据";
        /** 传感器数据录入窗口名 **/
        public const string UPDATE_GAS_CONCENTRATION_PROBE_DATA = "修改传感器数据";
        /** 传感器数据管理窗口名 **/
        public const string MANAGE_GAS_CONCENTRATION_PROBE_DATA = "传感器数据查询";

        /** 智能识别窗口名 **/
        public const string BADDATE_GAS_CONCENTRATION_PROBE_DATA = "智能识别无效数据";

        /** 瓦斯浓度探头数据取得方式 **/
        public const string RECORDTYPE_PEOPLE = "瓦斯员上传数据";
        public const string RECORDTYPE_COMPUTER = "矿监控系统读取";

        /**井下数据**/
        //共通
        public const string COORDINATE_X = "坐标X";
        public const string COORDINATE_Y = "坐标Y";
        public const string COORDINATE_Z = "坐标Z";

        //通风
        public const string VENTILATION = "通风";
        public const string VENTILATION_ADD = "添加通风";
        public const string VENTILATION_CHANGE = "修改通风";
        public const string VENTILATION_MANAGEMENT = "通风信息";
        public const string VENTILATION_FARPOINT = "通风信息";

        //煤层赋存
        public const string COAL_EXISTENCE = "煤层赋存";
        public const string COAL_EXISTENCE_ADD = "添加煤层赋存";
        public const string COAL_EXISTENCE_CHANGE = "修改煤层赋存";
        public const string COAL_EXISTENCE_MANAGEMENT = "煤层赋存信息";
        public const string COAL_EXISTENCE_FARPOINT = "煤层赋存信息";

        public const string COAL_THICK_CHANGE = "煤厚变化";
        public const string TECTONIC_COAL_THICK = "软分层（构造煤）厚度";

        public const string CHOOSE_TUNNEL = "选择巷道";

        /** 距迎头距离必须输入 **/
        public const string FAR_FROM_FRONTAL_MUST_INPUT = "请输入距迎头距离！";
        /** 距迎头距离 **/
        public const string FAR_FROM_FRONTAL = "距迎头距离";
        /** 帮助文件 **/
        public const string System1_Help_File = "\\工作面瓦斯涌出动态特征管理系统帮助文件.chm";
        /** 关于图片 **/
        public const string Picture_Name = "\\系统一关于图片.jpg";
    }
}
