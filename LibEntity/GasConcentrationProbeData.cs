// ******************************************************************
// 概  述：瓦斯浓度探头数据实体
// 作  者：伍鑫
// 日  期：2013/12/01
// 版本号：V1.0
// 版本信息：
// V1.0 新建
// ******************************************************************

using System;

namespace LibEntity
{
    public class GasConcentrationProbeData
    {
        // 探头数据编号

        /// <summary>
        ///     探头数据编号
        /// </summary>
        public int ProbeDataId { get; set; }

        // 探头编号

        /// <summary>
        ///     探头编号
        /// </summary>
        public string ProbeId { get; set; }

        // 探头数值

        /// <summary>
        ///     探头数值
        /// </summary>
        public double ProbeValue { get; set; }

        // 记录时间

        /// <summary>
        ///     记录时间
        /// </summary>
        public DateTime RecordTime { get; set; }

        // 记录类型

        /// <summary>
        ///     记录类型
        /// </summary>
        public string RecordType { get; set; }
    }
}