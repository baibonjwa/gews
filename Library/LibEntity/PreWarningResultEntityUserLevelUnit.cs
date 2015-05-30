// ******************************************************************
// 概  述：预警实体单元（用户层）
// 作  者：伍鑫
// 创建日期：2013/12/29
// 版本号：1.0
// ******************************************************************

using System.Collections.Generic;

namespace LibEntity
{
    public class PreWarningResultEntityUserLevelUnit
    {
        //规则类别

        public string RuleType { get; set; }

        /// <summary>
        ///     超限预警
        /// </summary>
        public string OutOfLimitResult { get; set; }

        /// <summary>
        ///     超限预警-说明
        /// </summary>
        public string OutOfLimitNotes { get; set; }

        /// <summary>
        ///     突出预警
        /// </summary>
        public string OutburstResult { get; set; }

        /// <summary>
        ///     突出预警-说明
        /// </summary>
        public string OutburstNotes { get; set; }

        public static string ConvertWarningDetails2UserStr(List<PreWarningReasonUnit> units)
        {
            string ret = "";
            int n = units.Count;
            for (int i = 0; i < n; i++)
            {
                ret += units[i].Notes;
                if (i != n - 1)
                {
                    ret += "；";
                    ret += "\r\n";
                }
                else
                {
                    ret += "。";
                }
            }
            return ret;
        }

        public static string ConvertWarningLevel2UserStr(WARNING_LEVEL_RESULT warningLevel)
        {
            string ret = "";
            switch (warningLevel)
            {
                case WARNING_LEVEL_RESULT.NODATA:
                    ret = "正常";
                    break;
                case WARNING_LEVEL_RESULT.RED:
                    ret = "红色";
                    break;
                case WARNING_LEVEL_RESULT.YELLOW:
                    ret = "黄色";
                    break;
                case WARNING_LEVEL_RESULT.GREEN:
                    ret = "正常";
                    break;
                default:
                    break;
            }
            return ret;
        }
    }
}