using System.Collections.Generic;
using Castle.ActiveRecord;
using NHibernate.Criterion;

namespace LibEntity
{
    [ActiveRecord("T_FAULTAGE")]
    public class Faultage : ActiveRecordBase<Faultage>
    {

        /// <summary>
        ///     断层编号
        /// </summary>
        [PrimaryKey(PrimaryKeyType.Identity, "FAULTAGE_ID")]
        public int FaultageId { get; set; }

        /// <summary>
        ///     断层名称
        /// </summary>
        [Property("FAULTAGE_NAME")]
        public string FaultageName { get; set; }

        /// <summary>
        ///     落差
        /// </summary>
        [Property("GAP")]
        public string Gap { get; set; }

        /// <summary>
        ///     倾角
        /// </summary>
        [Property("ANGLE")]
        public double Angle { get; set; }

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
        ///     断距
        /// </summary>
        [Property("SEPARATION")]
        public string Separation { get; set; }

        /// <summary>
        /// 长度
        /// </summary>
        [Property("LENGTH")]
        public double Length { get; set; }

        /// <summary>
        ///     坐标X
        /// </summary>
        [Property("COORDINATE_X")]
        public double CoordinateX { get; set; }

        /// <summary>
        ///     坐标Y
        /// </summary>
        [Property("COORDINATE_Y")]
        public double CoordinateY { get; set; }

        /// <summary>
        ///     坐标Z
        /// </summary>
        [Property("COORDINATE_Z")]
        public double CoordinateZ { get; set; }

        /// <summary>
        ///     BID
        /// </summary>
        [Property("BID")]
        public string BindingId { get; set; }

        public static bool ExistsByFaultageName(string faultageName)
        {
            var criterion = new List<ICriterion>
            {
                Restrictions.Eq("FaultageName", faultageName)
            };
            return Exists(criterion.ToArray());
        }
    }
}