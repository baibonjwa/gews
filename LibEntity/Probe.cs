// ******************************************************************
// 概  述：探头管理实体
// 作  者：伍鑫
// 日  期：2013/12/01
// 版本号：V1.0
// 版本信息：
// V1.0 新建
// ******************************************************************

using Castle.ActiveRecord;

namespace LibEntity
{
    [ActiveRecord("T_GAS_CONCENTRATION_PROBE_DATA")]
    public class Probe : ActiveRecordBase<Probe>
    {
        /** 探头编号 **/

        /// <summary>
        ///     探头编号
        /// </summary>
        [PrimaryKey(PrimaryKeyType.Native, "PROBE_DATA_ID")]
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
        [BelongsTo("PROBE_NAME")]
        public int ProbeTypeId { get; set; }

        /// <summary>
        ///     探头类型显示名称
        /// </summary>
        public string ProbeTypeDisplayName { get; set; }

        /// <summary>
        ///     测量类型
        /// </summary>
        public int ProbeMeasureType { get; set; }

        /// <summary>
        ///     使用方式
        /// </summary>
        public string ProbeUseType { get; set; }

        /// <summary>
        ///     单位
        /// </summary>
        public string Unit { get; set; }

        /** 所在巷道编号 **/

        /// <summary>
        ///     所在巷道编号
        /// </summary>
        public int TunnelId { get; set; }

        /** 探头位置坐标X **/

        /// <summary>
        ///     探头位置坐标X
        /// </summary>
        public double ProbeLocationX { get; set; }

        /** 探头位置坐标Y **/

        /// <summary>
        ///     探头位置坐标Y
        /// </summary>
        public double ProbeLocationY { get; set; }

        /** 探头位置坐标Z **/

        /// <summary>
        ///     探头位置坐标Z
        /// </summary>
        public double ProbeLocationZ { get; set; }

        /** 探头描述 **/

        /// <summary>
        ///     探头描述
        /// </summary>
        public string ProbeDescription { get; set; }

        /** 是否自动位移 **/

        /// <summary>
        ///     是否自动位移
        /// </summary>
        public int IsMove { get; set; }

        /** 距迎头距离 **/

        /// <summary>
        ///     距迎头距离
        /// </summary>
        public double FarFromFrontal { get; set; }
    }
}