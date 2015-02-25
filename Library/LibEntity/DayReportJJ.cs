using System;
using System.Collections.Generic;
using System.Linq;
using Castle.ActiveRecord;
using NHibernate.Criterion;

namespace LibEntity
{
    [ActiveRecord("T_DAYREPORT_JJ")]
    public class DayReportJj : DayReport
    {

        public const String TableName = "T_DAYREPORT_JJ";
        /// <summary>
        ///     距参考导线点距离
        /// </summary>
        [Property("DISTANCE_FROM_WIREPOINT")]
        public double DistanceFromWirepoint { get; set; }

        /// <summary>
        ///     参考导线点ID
        /// </summary>
        [Property("CONSULT_WIREPOINT_ID")]
        public int ConsultWirepoint { get; set; }

        public static DayReportJj[] FindAll()
        {
            return (DayReportJj[])FindAll(typeof(DayReportJj));
        }

        public static DayReportJj FindByBid(string bid)
        {
            var criterion = new List<ICriterion>
            {
                Restrictions.Eq("Bid", bid)
            };
            return (DayReportJj)FindFirst(typeof(DayReportJj), criterion.ToArray());
        }

        public static int GetTotalCount()
        {
            return Count(typeof(DayReportJj));
        }

        public static void DeleteByWorkingFaceId(int workingFaceId)
        {
            var criterion = new ICriterion[]
            {
                Restrictions.Eq("WorkingFace.WorkingFaceId", workingFaceId)
            };
            var result = (DayReportJj[])FindAll(typeof(DayReportJj), criterion);
            DeleteAll(typeof(DayReportJj), result.Select(u => u.Id));
        }
    }
}