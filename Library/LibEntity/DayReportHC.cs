// ******************************************************************
// 概  述：回采进尺日报数据实体
// 作  者：宋英杰
// 日  期：2014/3/12
// 版本号：V1.0
// 版本信息：
// V1.0 新建
// ******************************************************************

using System;
using System.Collections.Generic;
using System.Linq;
using Castle.ActiveRecord;
using NHibernate.Criterion;


namespace LibEntity
{
    /// <summary>
    ///     回采进尺日报实体
    /// </summary>
    [ActiveRecord("T_DAYREPORT_HC")]
    public class DayReportHc : DayReport
    {

        public const String TableName = "T_DAYREPORT_HC";
        /// <summary>
        ///     该条记录是否删除，用于修改进尺信息
        /// </summary>
        [Property("ISDEL")]
        public int IsDel { get; set; }

        public static DayReportHc[] FindAll()
        {
            return (DayReportHc[])FindAll(typeof(DayReportHc));
        }

        public static DayReportHc FindByBid(string bid)
        {
            var criterion = new List<ICriterion>
            {
                Restrictions.Eq("BindingId", bid)
            };
            return (DayReportHc)FindFirst(typeof(DayReportHc), criterion.ToArray());
        }

        public static int GetTotalCount()
        {
            return Count(typeof(DayReportHc));
        }

        public static DayReportHc[] FindAllByDatetime(DateTime dtFrom, DateTime dtTo)
        {
            var criterion = new List<ICriterion>
            {
                Restrictions.Between("DateTime", dtFrom,dtTo)
            };
            return (DayReportHc[])FindAll(typeof(DayReportHc), criterion.ToArray());
        }


        public static DayReportHc[] SlicedFindByDatetime(int firstResult, int maxResult,
            DateTime startTime, DateTime endTime)
        {
            var criterion = new ICriterion[]
            {
                Restrictions.Between("Datetime", startTime, endTime)
            };
            var results = (DayReportHc[])SlicedFindAll(typeof(DayReportHc), firstResult, maxResult, criterion);
            return results;
        }

        public static DayReportHc[] SlicedFind(int firstResult, int maxResult)
        {
            var results = (DayReportHc[])SlicedFindAll(typeof(DayReportHc), firstResult, maxResult);
            return results;
        }

        public static void DeleteByWorkingFaceId(int workingFaceId)
        {
            var criterion = new ICriterion[]
            {
                Restrictions.Eq("WorkingFace.WorkingFaceId", workingFaceId)
            };
            var result = (DayReportHc[])FindAll(typeof(DayReportHc), criterion);
            DeleteAll(typeof(DayReportHc), result.Select(u => u.Id));
        }

    }
}