// ******************************************************************
// 概  述：掘进进尺日报数据实体
// 作  者：宋英杰
// 日  期：2014/3/12
// 版本号：V1.0
// 版本信息：
// V1.0 新建
// ******************************************************************

using Castle.ActiveRecord;

namespace LibEntity
{
    [ActiveRecord("T_DAYREPORT_HC")]
    public class DayReportJj : DayReport
    {
        /// <summary>
        ///     距参考导线点距离
        /// </summary>
        [Property("DISTANCE_FROM_WIREPOINT")]
        public double DistanceFromWirepoint { get; set; }

        /// <summary>
        ///     参考导线点ID
        /// </summary>
        [Property("CONSULT_WIREPOINT_ID")]
        public int ConsultWirepoint { get; set; }
    }
}