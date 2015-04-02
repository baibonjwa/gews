using Castle.ActiveRecord;
using NHibernate.Criterion;

namespace LibEntity
{
    [ActiveRecord("T_PROBE_TYPE")]
    public class ProbeType : ActiveRecordBase<ProbeType>
    {
        /** 探头类型编号 **/

        /// <summary>
        ///     探头类型编号
        /// </summary>
        [PrimaryKey(PrimaryKeyType.Identity, "PROBE_TYPE_ID")]
        public int ProbeTypeId { get; set; }

        /** 探头类型名称 **/

        /// <summary>
        ///     探头类型名称
        /// </summary>
        [Property("PROBE_TYPE_NAME")]
        public string ProbeTypeName { get; set; }

        public static ProbeType FindProbeTypeByProbeTypeName(string probeTypeName)
        {
            var criterion = new ICriterion[]
            {
                Restrictions.Eq("ProbeTypeName", probeTypeName)
            };
            return FindOne(criterion);
        }
    }
}