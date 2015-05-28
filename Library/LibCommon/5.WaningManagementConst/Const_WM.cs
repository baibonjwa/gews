// ******************************************************************
// 概  述：系统五常量名
// 作  者：伍鑫
// 创建日期：2013/12/1
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
    public static class Const_WM
    {
        /** 突出预警结果查询 **/
        public const string RESULT_QUERY_PREWARNING = "突出预警结果查询";


        //规则编码比较符,比较符个数与参数个数一致，多个比较符用逗号分隔
        //注：此处字符串值全部为中文字符
        public const string GREATER_THAN = "＞";
        public const string LESS_THAN = "＜";
        public const string GREATER_TAHN_OR_EQUAL2 = "≥";
        public const string LESS_THAN_OR_EQUAL2 = "≤";
        public const string EQUAL2 = "＝";
        public const string NEQUAL2 = "≠";

        //瓦斯,地质构造,煤层赋存,通风,采掘影响,防突措施,日常预测,管理因素,其他
        public const string RULE_TYPE_GAS = "瓦斯";
        public const string RULE_TYPE_GEOLOGIC_STRUCTURE = "地质构造";
        public const string RULE_TYPE_COAL_OCURRENCE = "煤层赋存";
        public const string RULE_TYPE_VENTILATION = "通风";
        public const string RULE_TYPE_MINING_INFLUENCE = "采掘影响";
        public const string RULE_TYPE_SOLUTION = "防突措施";
        //public const string RULE_TYPE_PREDICTION = "日常预测";
        public const string RULE_TYPE_MANAGE_FACTOR = "管理因素";
        public const string RULE_TYPE_OTHERS = "其他";


        private static string[] _ruleTypes = null;
        /// <summary>
        /// 获取所有定义的规则类别
        /// </summary>
        /// <returns></returns>
        public static string[] GetRuleTypeConstStrings()
        {
            if (_ruleTypes == null)
            {
                _ruleTypes = new string[]
                {
                    RULE_TYPE_GAS,
                    RULE_TYPE_GEOLOGIC_STRUCTURE,
                    RULE_TYPE_COAL_OCURRENCE,
                    RULE_TYPE_VENTILATION,
                    RULE_TYPE_MINING_INFLUENCE,
                    RULE_TYPE_SOLUTION,
                    //RULE_TYPE_PREDICTION,
                    RULE_TYPE_MANAGE_FACTOR,
                    RULE_TYPE_OTHERS,
                 };
            }
            return _ruleTypes;
        }

        public static string[] GetSuitableLocations()
        {
            string[] ret = new string[]
            {
                SUITABLE_LOCATION_JUE_JIN,
                SUITABLE_LOCATION_HUI_CAI,
                SUITABLE_LOCATION_JUE_JIN_HUI_CAI_COMMON,
                SUITABLE_LOCATION_OTHERS,
                SUITABLE_LOCATION_WHOLE,
            };
            return ret;
        }
        //适用位置
        public const string SUITABLE_LOCATION_JUE_JIN = "掘进工作面";
        public const string SUITABLE_LOCATION_HUI_CAI = "回采工作面";
        public const string SUITABLE_LOCATION_JUE_JIN_HUI_CAI_COMMON = "掘进和回采工作面";
        public const string SUITABLE_LOCATION_OTHERS = "其他地点";
        public const string SUITABLE_LOCATION_WHOLE = "整个矿井";

        //巷道预警规则设置窗体标题
        public const string TUNNEL_RULES_SETTING = "设置巷道预警规则";

        //预警类型
        public const string WARNING_TYPE_OUT_OF_LIMIT = "超限预警";
        public const string WARNING_TYPE_GAS_OUTBURST = "突出预警";
        public const string WARNING_TYPE_FILTER_ALL = "所有";
        static public string[] GetWarningTypeConstStrings()
        {
            return new string[]
            {
                WARNING_TYPE_OUT_OF_LIMIT,
                WARNING_TYPE_GAS_OUTBURST,
            };
        }

        //使用状态
        public const string USING_STATE_USING = "正在使用";
        public const string USING_STATE_NOT_USING = "未使用";
        public const string USING_STATE_ALL = "所有";
        static public string[] GetUsingStateConstStrs()
        {
            return new string[]
            {
                USING_STATE_USING,
                USING_STATE_NOT_USING,
                USING_STATE_ALL,
            };
        }

        public const string WARNING_LEVEL_STR_RED = "红色预警";
        public const string WARNING_LEVEL_STR_YELLOW = "黄色预警";
        public const string WARNING_LEVEL_STR_GREEN = "正常";

        //指标类型
        public const string YES_NO_INDICATOR = "定性指标";
        public const string QUANTIFIED_INDICATOR = "定量指标";


        //public const string PARAM_START = @"\[";
        //public const string PARAM_END = @"\]";
        //public const string VALUE_START = @"\(";
        //public const string VALUE_END = @"\)";

        public const string PARAM_START_SEPERATOR = @"[";
        public const string PARAM_END_SEPERATOR = @"]";
        public const string VALUE_START_SEPERATOR = @"(";
        public const string VALUE_END_SEPERATOR = @")";


        //多条预警规则中规则编码和参数间的分隔符
        public const char PRE_WARNING_RULE_CODE_AND_PARAMS_SEPERATOR_MULTI = ';';
        //单条预警规则中规则编码和参数间的分隔符
        public const char PRE_WARNING_RULE_CODE_AND_PARAMS_SEPERATOR_SINGLE = ',';
        //复合预警规则中，单条规则间的分隔符
        public const char PRE_WARNING_RULE_CODE_AND_PARAMS_SEPERATOR_COMPLEX = '/';
        //预警原因中，多条原因的分隔符
        public const char PRE_WARNING_RULE_CODE_AND_PARAMS_SEPERATOR_REASONS = '|';

        private static char[] _ruleCodeAndParamSeperator = null;
        public static char[] GetPreWarningRuleIdAndParamsSeperatorArr()
        {
            if (_ruleCodeAndParamSeperator == null)
            {
                _ruleCodeAndParamSeperator = new char[] { PRE_WARNING_RULE_CODE_AND_PARAMS_SEPERATOR_MULTI };
            }
            return _ruleCodeAndParamSeperator;
        }

        //预警规则比较符分隔符
        public const char OPERATOR_SEPERATOR = ',';
        private static char[] _ruleOperatorSeperator = null;
        public static char[] GetRuleOperatorSeperator()
        {
            if (_ruleOperatorSeperator == null)
            {
                _ruleOperatorSeperator = new char[] { OPERATOR_SEPERATOR };
            }
            return _ruleOperatorSeperator;
        }



        public const string MSG01 = "预警规则将应用至<";
        public const string MSG02 = ">中所有巷道。";
        public const string MSG03 = "预警规则将应用至所选巷道。";

        public const string CONST_MINE = "矿井";
        public const string CONST_HORIZONTAL = "水平";
        public const string CONST_MININGAREA = "采区";
        public const string CONST_WORKINGFACE = "工作面";

        public const string SPECIAL_WARNING_DATA_TABLE_FAULTAGE = "T_FAULTAGE";//特殊预警数据表（断层）
        public const string SPECIAL_WARNING_DATA_TABLE_COLLAPSE_PILLARS = "T_COLLAPSE_PILLARS";//特殊预警数据表（陷落柱）
        private static string[] _specialWarningDataTableNames = null;

        /// <summary>
        /// 获取所有特殊预警数据表名称
        /// </summary>
        /// <returns></returns>
        public static string[] GetSpecialWarningDataTableNames()
        {
            if (_specialWarningDataTableNames == null)
            {
                _specialWarningDataTableNames = new string[]
                {
                    SPECIAL_WARNING_DATA_TABLE_FAULTAGE,
                    SPECIAL_WARNING_DATA_TABLE_COLLAPSE_PILLARS,
                };
            }
            return _specialWarningDataTableNames;
        }

        #region 预警结果表中所用拆分字符串
        public const string WARNING_REASON_COLON = "：";
        public const string WARNING_REASON_SEPERATOR_SEMICOLON = "；";
        public const string WARNING_REASON_SEPERATOR_RETURN = "\r\n";
        public const string WARNING_REASON_END = "。";
        #endregion

        #region 短信管理
        public const string SHORT_MESSAGE_MANUALSEND = "手动发送短信";
        #endregion

        #region 显示图片的位置
        public const string PICTURE_RED = "\\red.png";
        public const string PICTURE_ORANGE = "\\orange.png";
        public const string PICTURE_GREEN = "\\green.png";
        public const string PICTURE_NULL = "\\grey.png";
        #endregion

        #region 历史预警信息查询
        public const string WRONG_DATETIME = "输入的起始日期必须小于结束日期！";
        public const string FARPOINT_WARNING_HISTORY_DETAILS = "工作面预警结果详细信息";
        #endregion


        public const string LASTED_PREWARNING_RESULT_QUERY = "实时预警结果";

        public const string PREWARNING_RESULT_QUERY = "历史预警结果查询";

        public const string PREWARNING_RESULT_DETAILS_QUERY = "工作面预警结果详细信息查询";

        public const string UPDATE_RULE_DESCRIPTION_FAILED = "更新规则描述失败！";

        public const string RULE_DESCRIPTION_PLUS_INFO = ", 启动";

        /** 帮助文件 **/
        public const string System5_Help_File = "\\瓦斯预警系统管理平台帮助文件.chm";
        /** 关于图片 **/
        public const string Picture_Name = "\\系统五关于图片.jpg";

    }

    /// <summary>
    /// 定义预警类型枚举
    /// </summary>
    public enum WarningType
    {
        OVERLIMIT = 0,
        OUTBURST = 1
    }

    /// <summary>
    /// 定义预警类型枚举
    /// </summary>
    public enum WarningTypeCHN
    {
        超限预警,
        突出预警
    }

    /// <summary>
    /// 预警结果类型
    /// </summary>
    public enum WarningResult
    {
        RED = 0,
        YELLOW = 1,
        GREEN = 2,
        NOT_AVAILABLE = 3,
        NULL = 4
    }

    /// <summary>
    /// 预警结果类型
    /// </summary>
    public enum WarningResultCHN
    {
        红色预警 = 0,
        黄色预警 = 1,
        正常 = 2,
        无预警内容 = 3,
        预警结果为空 = 4
    }

    /// <summary>
    /// 预警依据
    /// </summary>
    public enum WarningReasonItems
    {
        瓦斯 = 0,
        煤层赋存 = 1,
        地质构造 = 2,
        通风 = 3,
        管理因素 = 4,
        //其他 = 5
    }

    /// <summary>
    /// 规则类型
    /// </summary>
    public enum RuleType
    {
        单一规则 = 0,
        组合规则 = 1
    }
}
