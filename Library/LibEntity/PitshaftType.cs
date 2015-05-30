using Castle.ActiveRecord;

namespace LibEntity
{
    public class PitshaftType : ActiveRecordBase<PitshaftType>
    {
        /// <summary>
        ///     井筒类型编号
        /// </summary>
        [PrimaryKey(PrimaryKeyType.Identity, "PITSHAFT_TYPE_ID")]
        public int PitshaftTypeId { get; set; }

        /// <summary>
        ///     井筒类型名称
        /// </summary>
        [Property("PITSHAFT_TYPE_NAME")]
        public string PitshaftTypeName { get; set; }
    }
}