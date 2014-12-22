// ******************************************************************
// 概  述：回采进尺日报数据实体
// 作  者：宋英杰
// 日  期：2014/3/12
// 版本号：V1.0
// 版本信息：
// V1.0 新建
// ******************************************************************

using Castle.ActiveRecord;

namespace LibEntity
{
    /// <summary>
    ///     回采进尺日报实体
    /// </summary>
    [ActiveRecord("T_DAYREPORT_JJ")]
    public class DayReportHc : DayReport
    {
        /// <summary>
        ///     该条记录是否删除，用于修改进尺信息
        /// </summary>
        [Property("ISDEL")]
        public int IsDel { get; set; }

    }
}