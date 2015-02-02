// ******************************************************************
// 概  述：勘探线实体
// 作  者：伍鑫
// 创建日期：2014/03/05
// 版本号：1.0
// ******************************************************************

using System.Collections.Generic;
using Castle.ActiveRecord;
using NHibernate.Criterion;

namespace LibEntity
{
    [ActiveRecord("T_PROSPECTING_LINE_INFO")]
    public class ProspectingLine : ActiveRecordBase<ProspectingLine>
    {
        /// <summary>
        ///     勘探线编号
        /// </summary>
        [PrimaryKey(PrimaryKeyType.Identity, "PROSPECTING_LINE_ID")]
        public int ProspectingLineId { get; set; }

        /// <summary>
        ///     勘探线名称
        /// </summary>
        [Property("PROSPECTING_LINE_NAME")]
        public string ProspectingLineName { get; set; }

        /// <summary>
        ///     勘探钻孔
        /// </summary>
        [Property("PROSPECTING_BOREHOLE")]
        public string ProspectingBorehole { get; set; }

        /// <summary>
        ///     BID
        /// </summary>
        [Property("BID")]
        public string BindingId { get; set; }

        public static bool ExistsByProspectingLineName(string prospectingLineName)
        {
            var criterion = new List<ICriterion>
            {
                Restrictions.Eq("ProspectingLineName", prospectingLineName)
            };
            return Exists(criterion.ToArray());
        }
    }
}