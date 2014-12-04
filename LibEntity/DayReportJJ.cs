// ******************************************************************
// 概  述：掘进进尺日报数据实体
// 作  者：宋英杰
// 日  期：2014/3/12
// 版本号：V1.0
// 版本信息：
// V1.0 新建
// ******************************************************************

namespace LibEntity
{
    public class DayReportJJ : DayReport
    {
        //距参考导线点距离

        /// <summary>
        ///     距参考导线点距离
        /// </summary>
        public double DistanceFromWirepoint { get; set; }

        //参考导线点ID

        /// <summary>
        ///     参考导线点ID
        /// </summary>
        public int ConsultWirepoint { get; set; }
    }
}