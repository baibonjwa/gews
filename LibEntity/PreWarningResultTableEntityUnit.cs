// ******************************************************************
// 概  述：预警实体（预警结果表实体用）
// 作  者：伍鑫
// 创建日期：2013/12/29
// 版本号：1.0
// ******************************************************************
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LibEntity
{
    public class PreWarningResultTableEntityUnit
    {
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
            switch(warningLevel)
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
        /// <summary>
        /// 超限预警
        /// </summary>
        private string _ultralimitPreWarning;

        /// <summary>
        /// 超限预警
        /// </summary>
        public string UltralimitPreWarning
        {
            get { return _ultralimitPreWarning; }
            set { _ultralimitPreWarning = value; }
        }

        /// <summary>
        /// 超限预警-说明
        /// </summary>
        private string _ultralimitPreWarningEX;

        /// <summary>
        /// 超限预警-说明
        /// </summary>
        public string UltralimitPreWarningEX
        {
            get { return _ultralimitPreWarningEX; }
            set { _ultralimitPreWarningEX = value; }
        }

        /// <summary>
        /// 突出预警
        /// </summary>
        private string _outburstPreWarning;

        /// <summary>
        /// 突出预警
        /// </summary>
        public string OutburstPreWarning
        {
            get { return _outburstPreWarning; }
            set { _outburstPreWarning = value; }
        }

        /// <summary>
        /// 突出预警-说明
        /// </summary>
        private string _outburstPreWarningEX;

        /// <summary>
        /// 突出预警-说明
        /// </summary>
        public string OutburstPreWarningEX
        {
            get { return _outburstPreWarningEX; }
            set { _outburstPreWarningEX = value; }
        }
    }
}
