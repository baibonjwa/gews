using System;
using System.Collections.Generic;
using System.Linq;
using Castle.ActiveRecord;
using NHibernate.Criterion;

namespace LibEntity
{
    [ActiveRecord("T_BIG_FAULTAGE")]
    public class BigFaultage : ActiveRecordBase<BigFaultage>
    {

        public const String TableName = "T_BIG_FAULTAGE";
        public const String CFaultageName = "BigFaultageName";

        /// <summary>
        ///     断层编号
        /// </summary>
        [PrimaryKey(PrimaryKeyType.Identity, "FAULTAGE_ID")]
        public int BigFaultageId { get; set; }


        [HasMany(typeof(BigFaultagePoint), Table = "T_BIG_FAULTAGE_POINT", ColumnKey = "BIG_FAULTAGE_ID",
    Cascade = ManyRelationCascadeEnum.All, Lazy = true)]
        public IList<BigFaultagePoint> BigFaultagePoints { get; set; }

        /// <summary>
        ///     断层名称
        /// </summary>
        [Property("FAULTAGE_NAME")]
        public string BigFaultageName { get; set; }

        /// <summary>
        ///     落差
        /// </summary>
        [Property("GAP")]
        public string Gap { get; set; }

        /// <summary>
        ///     倾角
        /// </summary>
        [Property("ANGLE")]
        public string Angle { get; set; }

        /// <summary>
        ///     类型
        /// </summary>
        [Property("TYPE")]
        public string Type { get; set; }

        /// <summary>
        ///     走向
        /// </summary>
        [Property("TREND")]
        public string Trend { get; set; }

        /// <summary>
        ///     BID
        /// </summary>
        [Property("BID")]
        public string BindingId { get; set; }

        public static BigFaultage FindOneByBigFaultageName(string bigFaultageName)
        {
            var criterion = new ICriterion[]
            {
                Restrictions.Eq("BigFaultageName", bigFaultageName)
            };
            return FindOne(criterion);
        }

        //public override void Delete()
        //{
        //    var bigFaultagePoints = BigFaultagePoint.FindAllByFaultageId(BigFaultageId);
        //    BigFaultagePoint.DeleteAll(bigFaultagePoints.Select(u => u.Id));
        //    base.Delete();
        //}
    }
}