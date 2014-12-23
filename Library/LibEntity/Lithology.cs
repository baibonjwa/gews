using Castle.ActiveRecord;

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
    }
}