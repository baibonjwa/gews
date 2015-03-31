using System.Collections.Generic;
using Castle.ActiveRecord;
using NHibernate.Criterion;

namespace LibEntity
{
    [ActiveRecord("T_BIG_FAULTAGE_POINT")]
    public class BigFaultagePoint : ActiveRecordBase<BigFaultagePoint>
    {

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

        [BelongsTo("BIG_FAULTAGE_ID")]
        public BigFaultage BigFaultage { get; set; }

        [Property("BINDINGID")]
        public virtual string Bid { get; set; }

        public static BigFaultagePoint[] FindAllByFaultageId(int bigFaultageId)
        {
            var criterion = new List<ICriterion> { Restrictions.Eq("BigFaultage.BigFaultageId", bigFaultageId) };
            return FindAll(criterion.ToArray());
        }
    }
}