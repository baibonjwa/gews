// ******************************************************************
// 概  述：最终预警结果单元
// 作  者：杨小颖  
// 创建日期：2013/12/28
// 版本号：1.0
// ******************************************************************

using System.Collections.Generic;

namespace LibEntity
{
    public class PreWarningReasonUnit
    {
        //预警等级

        public WARNING_LEVEL_RESULT WarningLevelResult { get; set; }

        //说明

        public string Notes { get; set; }
    }


    /// <summary>
    ///     预警结果（按类型分），如：超限预警的最终预警结果
    /// </summary>
    public class PreWarningReasonsByType
    {
        //预警等级
        private List<PreWarningReasonUnit> _lstWarningResultDetails = new List<PreWarningReasonUnit>();
        private WARNING_LEVEL_RESULT _warningLevel = WARNING_LEVEL_RESULT.NODATA; //注意：默认值不能删除！

        public WARNING_LEVEL_RESULT WarningLevel
        {
            get { return _warningLevel; }
            set { _warningLevel = value; }
        }

        //预警结果明细

        public List<PreWarningReasonUnit> LstWarningResultDetails
        {
            get { return _lstWarningResultDetails; }
            set { _lstWarningResultDetails = value; }
        }
    }
}