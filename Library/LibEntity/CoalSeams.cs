using Castle.ActiveRecord;
using NHibernate.Criterion;

namespace LibEntity
{
    [ActiveRecord("T_COAL_SEAMS_INFO")]
    public class CoalSeams : ActiveRecordBase<CoalSeams>
    {
        /// <summary>
        ///     煤层编号
        /// </summary>
        [PrimaryKey(PrimaryKeyType.Identity, "COAL_SEAMS_ID")]
        public int CoalSeamsId { get; set; }

        /// <summary>
        ///     煤层名称
        /// </summary>
        [Property("COAL_SEAMS_NAME")]
        public string CoalSeamsName { get; set; }

        public static CoalSeams FindOneByCoalSeamsName(string coalSeamsName)
        {
            var criterion = new ICriterion[]
            {
                Restrictions.Eq("CoalSeamsName",coalSeamsName)
            };
            return FindOne(criterion);
        }

    }
}