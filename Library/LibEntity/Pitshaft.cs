using System.Collections.Generic;
using Castle.ActiveRecord;
using NHibernate.Criterion;

namespace LibEntity
{
    public class Pitshaft : ActiveRecordBase<Pitshaft>
    {
        /// <summary>
        ///     井筒编号
        /// </summary>
        [PrimaryKey(PrimaryKeyType.Identity, "PITSHAFT_ID")]
        public int PitshaftId { get; set; }

        /// <summary>
        ///     井筒名称
        /// </summary>
        [Property("PITSHAFT_NAME")]
        public string PitshaftName { get; set; }

        /// <summary>
        ///     井筒类型
        /// </summary>
        [BelongsTo("PITSHAFT_TYPE_ID")]
        public PitshaftType PitshaftType { get; set; }

        /** 井口标高 **/

        /// <summary>
        ///     井口标高
        /// </summary>
        [Property("WELLHEAD_ELEVATION")]
        public double WellheadElevation { get; set; }

        /** 井底标高 **/

        /// <summary>
        ///     井底标高
        /// </summary>
        [Property("WELLBOTTOM_ELEVATION")]
        public double WellbottomElevation { get; set; }

        /** 井筒坐标X **/

        /// <summary>
        ///     井筒坐标X
        /// </summary>
        [Property("PITSHAFT_COORDINATE_X")]
        public double PitshaftCoordinateX { get; set; }

        /** 井筒坐标Y **/

        /// <summary>
        ///     井筒坐标Y
        /// </summary>
        [Property("PITSHAFT_COORDINATE_Y")]
        public double PitshaftCoordinateY { get; set; }

        /** 图形坐标X **/

        /// <summary>
        ///     图形坐标X
        /// </summary>
        [Property("FIGURE_COORDINATE_X")]
        public double FigureCoordinateX { get; set; }

        /** 图形坐标Y **/

        /// <summary>
        ///     图形坐标Y
        /// </summary>
        [Property("FIGURE_COORDINATE_Y")]
        public double FigureCoordinateY { get; set; }

        /** 图形坐标Z **/

        /// <summary>
        ///     图形坐标ZZ
        /// </summary>
        [Property("FIGURE_COORDINATE_Z")]
        public double FigureCoordinateZ { get; set; }

        /** BID **/

        /// <summary>
        ///     BID
        /// </summary>
        [Property("BID")]
        public string BindingId { get; set; }


        public static bool ExistsByPitshaftName(string pitshaftName)
        {
            var criterion = new List<ICriterion>
            {
                Restrictions.Eq("PitshaftName", pitshaftName)
            };
            return Exists(criterion.ToArray());
        }
    }
}