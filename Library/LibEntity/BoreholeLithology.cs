using System.Collections.Generic;
using System.Linq;
using Castle.ActiveRecord;
using NHibernate.Criterion;

namespace LibEntity
{
    [ActiveRecord("T_BOREHOLE_LITHOLOGY")]
    public class BoreholeLithology : ActiveRecordBase<BoreholeLithology>
    {
        /// <summary>
        ///     编号
        /// </summary>
        [PrimaryKey(PrimaryKeyType.Identity, "BOREHOLE_LITHOLOGY_ID")]
        public int BoreholeLithhologyId { get; set; }

        /// <summary>
        ///     钻孔编号
        /// </summary>
        [BelongsTo("BOREHOLE_ID")]
        public Borehole Borehole { get; set; }

        /// <summary>
        ///     岩性编号
        /// </summary>
        [BelongsTo("LITHOLOGY_ID")]
        public Lithology Lithology { get; set; }

        /// <summary>
        ///     底板标高
        /// </summary>
        [Property("FLOOR_ELEVATION")]
        public double FloorElevation { get; set; }

        /// <summary>
        ///     厚度
        /// </summary>
        [Property("THICKNESS")]
        public double Thickness { get; set; }

        /// <summary>
        ///     煤层名称
        /// </summary>
        [Property("COAL_SEAMS_NAME")]
        public string CoalSeamsName { get; set; }

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

        public static BoreholeLithology[] FindAllByBoreholeId(int boreholeId)
        {
            var criterion = new List<ICriterion> { Restrictions.Eq("Borehole.BoreholeId", boreholeId) };
            return FindAll(criterion.ToArray());
        }

        public static void DeleteAllByBoreholeId(int boreholeId)
        {
            DeleteAll(FindAllByBoreholeId(boreholeId).Select(u => u.BoreholeLithhologyId));
        }
    }
}