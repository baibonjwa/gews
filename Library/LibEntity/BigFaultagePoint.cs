using Castle.ActiveRecord;

namespace LibEntity
{
    [ActiveRecord("T_BIG_FAULTAGE_POINT", Lazy = true)]
    public class BigFaultagePoint : ActiveRecordBase<BigFaultagePoint>
    {
        private BigFaultage _bigFaultage = new BigFaultage();

        /// <summary>
        ///     断层编号
        /// </summary>
        [PrimaryKey(PrimaryKeyType.Identity, "ID")]
        public int Id { get; set; }

        /** 断层名称 **/
        [Property("UPORDOWN")]
        public string UpOrDown { get; set; }

        [Property("COORDINATE_X")]
        public double CoordinateX { get; set; }

        [Property("COORDINATE_Y")]
        public double CoordinateY { get; set; }

        [Property("COORDINATE_Z")]
        public double CoordinateZ { get; set; }

        [BelongsTo("FAULTAGE_ID")]
        public BigFaultage BigFaultage
        {
            get { return _bigFaultage; }
            set { _bigFaultage = value; }
        }

        [Property("BINDINGID")]
        public string Bid { get; set; }
    }
}