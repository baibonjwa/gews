// ******************************************************************
// 概  述：勘探线实体
// 作  者：伍鑫
// 创建日期：2014/03/05
// 版本号：1.0
// ******************************************************************

namespace LibEntity
{
    public class ProspectingLineEntity
    {
        /**  勘探线编号 **/

        /// <summary>
        ///     勘探线编号
        /// </summary>
        public int ProspectingLineId { get; set; }

        /**  勘探线名称 **/

        /// <summary>
        ///     勘探线名称
        /// </summary>
        public string ProspectingLineName { get; set; }

        /** 勘探钻孔 **/

        /// <summary>
        ///     勘探钻孔
        /// </summary>
        public string ProspectingBorehole { get; set; }

        /** BID **/

        /// <summary>
        ///     BID
        /// </summary>
        public string BindingId { get; set; }
    }
}