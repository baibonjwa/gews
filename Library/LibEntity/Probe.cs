
using System.Collections.Generic;
using Castle.ActiveRecord;
using NHibernate.Criterion;

namespace LibEntity
{
    [ActiveRecord("T_PROBE_MANAGE")]
    public class Probe : ActiveRecordBase<Probe>
    {
        /** 探头编号 **/

        /// <summary>
        ///     探头编号
        /// </summary>
        [PrimaryKey(PrimaryKeyType.Native, "PROBE_ID")]
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

        /** 探头位置坐标X **/

        /// <summary>
        ///     探头位置坐标X
        /// </summary>
        [Property("PROBE_LOCATION_X")]
        public double ProbeLocationX { get; set; }

        /** 探头位置坐标Y **/

        /// <summary>
        ///     探头位置坐标Y
        /// </summary>
        [Property("PROBE_LOCATION_Y")]
        public double ProbeLocationY { get; set; }

        /** 探头位置坐标Z **/

        /// <summary>
        ///     探头位置坐标Z
        /// </summary>
        [Property("PROBE_LOCATION_Z")]
        public double ProbeLocationZ { get; set; }

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

        /// <summary>
        /// 判断【探头编号】是否存在
        /// </summary>
        /// <param name="strProbeId">【探头编号】</param>
        /// <returns>存在与否：true存在，false不存在</returns>
        public static bool IsProbeIdExist(string strProbeId)
        {
            var probe = FindByPrimaryKey(strProbeId, false);
            return probe != null;
        }


        /// <summary>
        /// 判断【探头名称】是否存在
        /// </summary>
        /// <param name="strProbeName">【探头名称】</param>
        /// <returns>存在与否：true存在，false不存在</returns>
        public static bool IsProbeNameExist(string strProbeId)
        {
            var criterion = new List<ICriterion> { Restrictions.Eq("ProbeName", strProbeId) };
            var probe = FindFirst(criterion.ToArray());
            return probe != null;
        }

        /// <summary>
        /// 获取指定【巷道】下，指定【探头类型】的探头信息
        /// </summary>
        /// <param name="iProbeTypeId">【探头类型】</param>
        /// <param name="iTunnelId">【巷道】</param>
        /// <returns>探头信息</returns>
        //public static Probe FindFirstByProbeTypeIdAndTunnelId(int iProbeTypeId, int iTunnelId)
        //{
        //    var criterion = new List<ICriterion>
        //    {
        //        Restrictions.Eq("Tunnel.TunnelId", iTunnelId),
        //        Restrictions.Eq("ProbeType.ProbeTypeId", iProbeTypeId)
        //    };
        //    return FindFirst(criterion.ToArray());
        //}
    }
}