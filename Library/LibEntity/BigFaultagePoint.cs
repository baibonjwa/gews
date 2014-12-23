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
        public virtual int Id { get; set; }

        /** 断层名称 **/
        [Property("UPORDOWN")]
        public virtual string UpOrDown { get; set; }

        [Property("COORDINATE_X")]
        public virtual double CoordinateX { get; set; }

        [Property("COORDINATE_Y")]
        public virtual double CoordinateY { get; set; }

        [Property("COORDINATE_Z")]
        public virtual double CoordinateZ { get; set; }

        [BelongsTo("FAULTAGE_ID")]
        public virtual BigFaultage BigFaultage
        {
            get { return _bigFaultage; }
            set { _bigFaultage = value; }
        }

        [Property("BINDINGID")]
        public virtual string Bid { get; set; }
    }
}