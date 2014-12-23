using System;
using System.Collections.Generic;
using System.Linq;
using Castle.ActiveRecord;
using NHibernate.Criterion;
using Remotion.Data.Linq.Clauses.ResultOperators;

namespace LibEntity
{
    [ActiveRecord("T_GAS_CONCENTRATION_PROBE_DATA")]
    public class GasConcentrationProbeData : ActiveRecordBase<GasConcentrationProbeData>
    {
        public const String TableName = "T_GAS_CONCENTRATION_PROBE_DATA";
        /// <summary>
        ///     探头数据编号
        /// </summary>
        [PrimaryKey(PrimaryKeyType.Identity, "PROBE_DATA_ID")]
        public int ProbeDataId { get; set; }

        // 探头编号

        /// <summary>
        ///     探头编号
        /// </summary>
        [BelongsTo("PROBE_ID")]
        public Probe Probe { get; set; }

        // 探头数值

        /// <summary>
        ///     探头数值
        /// </summary>
        [Property("PROBE_VALUE")]
        public double ProbeValue { get; set; }

        // 记录时间

        /// <summary>
        ///     记录时间
        /// </summary>
        [Property("RECORD_TIME")]
        public DateTime RecordTime { get; set; }

        // 记录类型

        /// <summary>
        ///     记录类型
        /// </summary>
        [Property("RECORD_TYPE")]
        public string RecordType { get; set; }


        public static GasConcentrationProbeData[] SelectAllGasConcentrationProbeDataByProbeIdAndTime(string probeId,
            DateTime startTime, DateTime endTime)
        {
            var criterion = new List<ICriterion> { Restrictions.Eq("Probe.ProbeId", probeId) };
            if (startTime != DateTime.MinValue && endTime != DateTime.MinValue)
            {
                criterion.Add(Restrictions.Between("Datetime", startTime, endTime));
            }
            return FindAll(criterion.ToArray());
        }


        public static GasConcentrationProbeData[] SlicedSelectAllGasConcentrationProbeDataByProbeIdAndTime(int firstResult, int maxResult, string probeId,
            DateTime startTime, DateTime endTime)
        {
            var criterion = new List<ICriterion> { Restrictions.Eq("Probe.ProbeId", probeId) };
            if (startTime != DateTime.MinValue && endTime != DateTime.MinValue)
            {
                criterion.Add(Restrictions.Between("Datetime", startTime, endTime));
            }
            return SlicedFindAll(firstResult, maxResult, criterion.ToArray());
        }

        /// <summary>
        /// 获取指定探头的最新实时数据
        /// </summary>
        /// <param name="probeId"></param>
        /// <returns></returns>
        public static GasConcentrationProbeData FindNewRealData(string probeId)
        {
            var criterion = new List<ICriterion> { Restrictions.Eq("Probe.ProbeId", probeId) };
            return FindFirst(Order.Desc("ProbeDataId"), criterion.ToArray());
        }

        /// <summary>
        /// 获取指定探头的最新N条实时数据
        /// </summary>
        /// <param name="probeId"></param>
        /// <param name="n"></param>
        /// <returns></returns>
        public static GasConcentrationProbeData[] FindNewRealData(string probeId, int n)
        {
            var criterion = new List<ICriterion> { Restrictions.Eq("Probe.ProbeId", probeId) };
            Order[] orders = { Order.Desc("ProbeDataId") };
            return SlicedFindAll(0, n, orders, criterion.ToArray());
        }

        public static GasConcentrationProbeData[] FindHistaryData(string probeId)
        {
            var criterion = new List<ICriterion>
            {
                Restrictions.Eq("Probe.ProbeId", probeId),
                Restrictions.Le("Datetime", DateTime.Now)
            };
            return FindAll(criterion.ToArray());
        }

        public static GasConcentrationProbeData[] FindHistaryData(string probeId, string startTime, string endTime)
        {
            var criterion = new List<ICriterion>
            {
                Restrictions.Eq("Probe.ProbeId", probeId),
                Restrictions.Between("Datetime", startTime, endTime)
            };
            return FindAll(criterion.ToArray());
        }

        public static GasConcentrationProbeData[] FindHistaryData(string probeId, string startTime)
        {
            var criterion = new List<ICriterion>
            {
                Restrictions.Eq("Probe.ProbeId", probeId),
                Restrictions.Gt("Datetime", startTime)
            };
            return FindAll(criterion.ToArray());
        }
    }
}