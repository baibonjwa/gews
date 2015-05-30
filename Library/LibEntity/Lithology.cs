using Castle.ActiveRecord;
using NHibernate.Criterion;

namespace LibEntity
{
    [ActiveRecord("T_LITHOLOGY")]
    public class Lithology : ActiveRecordBase<Lithology>
    {
        [PrimaryKey(PrimaryKeyType.Identity, "LITHOLOGY_ID")]
        public int LithologyId { get; set; }

        [Property("LITHOLOGY_NAME")]
        public string LithologyName { get; set; }

        [Property("LITHOLOGY_DESCRIBE")]
        public string LithologyDescribe { get; set; }

        public static Lithology FindOneByCoal()
        {
            var criterion = new ICriterion[]
            {
                Restrictions.Eq("LithologyName", "煤层")
            };
            return FindOne(criterion);
        }

        public static Lithology FindOneByLithologyName(string lithologyName)
        {
            var criterion = new ICriterion[]
            {
                Restrictions.Eq("LithologyName", lithologyName)
            };
            return FindOne(criterion);
        }
    }
}