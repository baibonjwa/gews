
using System;
using System.Collections.Generic;
using Castle.ActiveRecord;
using NHibernate.Criterion;

namespace LibEntity
{
    [ActiveRecord("T_PROBE_MANAGE")]
    public class Probe : ActiveRecordBase<Probe>
    {
        public const String TableName = "T_PROBE_MANAGE";
        /// <summary>
        ///     探头编号
        /// </summary>
        [PrimaryKey(PrimaryKeyType.Assigned, "PROBE_ID")]
        public string ProbeId { get; set; }

        /** 探头名称 **/

        /// <summary>
        ///     探头名称
        /// </summary>
        [Property("PROBE_NAME")]
        public string ProbeName { get; set; }

        /** 探头类型编号 **/

        /// <summary>
        ///     探头类型编号
        /// </summary>
        [BelongsTo("PROBE_TYPE_ID")]
        public ProbeType ProbeType { get; set; }

        /// <summary>
        ///     探头类型显示名称
        /// </summary>
        [Property("PROBE_TYPE_DISPLAY_NAME")]
        public string ProbeTypeDisplayName { get; set; }

        /// <summary>
        ///     测量类型
        /// </summary>
        [Property("PROBE_MEASURE_TYPE")]
        public int ProbeMeasureType { get; set; }

        /// <summary>
        ///     使用方式
        /// </summary>
        [Property("PROBE_USE_TYPE")]
        public string ProbeUseType { get; set; }

        /// <summary>
        ///     单位
        /// </summary>
        [Property("UNIT")]
        public string Unit { get; set; }

        /** 所在巷道编号 **/

        /// <summary>
        ///     所在巷道编号
        /// </summary>
        [BelongsTo("TUNNEL_ID")]
        public Tunnel Tunnel { get; set; }

        /** 探头描述 **/

        /// <summary>
        ///     探头描述
        /// </summary>
        [Property("PROBE_DESCRIPTION")]
        public string ProbeDescription { get; set; }

        /** 是否自动位移 **/

        /// <summary>
        ///     是否自动位移
        /// </summary>
        [Property("IS_MOVE")]
        public int IsMove { get; set; }

        /** 距迎头距离 **/

        /// <summary>
        ///     距迎头距离
        /// </summary>
        [Property("FAR_FROM_FRONTAL")]
        public double FarFromFrontal { get; set; }

        [Property("IS_DEBUG", Default = "0")]
        public int IsDebug { get; set; }

        /// <summary>
        /// 判断【探头名称】是否存在
        /// </summary>
        /// <param name="iProbeTypeId"></param>
        /// <param name="sProbeName"></param>
        /// <param name="iTunnelId"></param>
        /// <returns>存在与否：true存在，false不存在</returns>
        public static Probe FindFirstByProbeTypeIdAndProbeNameAndTunnelId(int iProbeTypeId, string sProbeName, int iTunnelId)
        {
            var criterion = new ICriterion[]
            {
                Restrictions.Eq("ProbeName", sProbeName),
                Restrictions.Eq("ProbeType.ProbeTypeId", iProbeTypeId),
                Restrictions.Eq("Tunnel.TunnelId", iTunnelId)
            };
            return FindFirst(criterion);
        }

        /// <summary>
        /// 根据巷道编号和探头类型编号,获取全部探头信息
        /// </summary>
        /// <param name="iTunnelId">巷道编号</param>
        /// <param name="iProbeTypeId">探头类型编号</param>
        /// <returns>探头信息</returns>
        public static Probe[] FindAllByTunnelIdAndProbeTypeId(int iTunnelId, int iProbeTypeId)
        {
            var criterion = new ICriterion[]
            {
                Restrictions.Eq("Tunnel.TunnelId", iTunnelId),
                Restrictions.Eq("ProbeType.ProbeTypeId", iProbeTypeId)
            };
            return FindAll(criterion);
        }

        /// <summary>
        /// 根据巷道编号编号,获取全部探头信息
        /// </summary>
        /// <param name="iTunnelId">巷道编号</param>
        /// <returns>探头信息</returns>
        public static Probe[] FindAllByTunnelId(int iTunnelId)
        {
            var criterion = new ICriterion[]
            {
                Restrictions.Eq("Tunnel.TunnelId", iTunnelId)
            };
            return FindAll(criterion);
        }

        public static Probe[] FindAllWithGasOrVentilation()
        {
            var criterion = new ICriterion[]
            {
                Restrictions.Or(Restrictions.Or(Restrictions.Like("ProbeTypeDisplayName", "风速", MatchMode.Anywhere),
                    Restrictions.Like("ProbeTypeDisplayName", "CH4", MatchMode.Anywhere)),Restrictions.Like("ProbeTypeDisplayName","甲烷"))
            };
            return FindAll(criterion);
        }

        /// <summary>
        /// 获取指定探头的最新实时数据
        /// </summary>
        /// <param name="iTunnelId"></param>
        /// <returns></returns>
        public static string GetT2Id(int iTunnelId)
        {
            var criterion = new ICriterion[]
            {
                Restrictions.Eq("Tunnel.TunnelId", iTunnelId),
                 Restrictions.Eq("PROBE_NAME", "T2"),
            };
            return FindFirst(criterion).ProbeId;
        }
    }
}