using System;
using Castle.ActiveRecord;

namespace LibEntity
{
    [ActiveRecord("T_GAS_CONCENTRATION_PROBE_DATA")]
    public class GasConcentrationProbeData : ActiveRecordBase<GasConcentrationProbeData>
    {
        // 探头数据编号

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
    }
}