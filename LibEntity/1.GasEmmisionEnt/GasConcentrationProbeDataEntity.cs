// ******************************************************************
// 概  述：瓦斯浓度探头数据实体
// 作  者：伍鑫
// 日  期：2013/12/01
// 版本号：V1.0
// 版本信息：
// V1.0 新建
// ******************************************************************
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LibEntity
{
    public class GasConcentrationProbeDataEntity
    {
        // 探头数据编号
        private int probeDataId;

        /// <summary>
        /// 探头数据编号
        /// </summary>
        public int ProbeDataId
        {
            get { return probeDataId; }
            set { probeDataId = value; }
        }

        // 探头编号
        private string probeId;

        /// <summary>
        /// 探头编号
        /// </summary>
        public string ProbeId
        {
            get { return probeId; }
            set { probeId = value; }
        }

        // 探头数值
        private double probeValue;

        /// <summary>
        /// 探头数值
        /// </summary>
        public double ProbeValue
        {
            get { return probeValue; }
            set { probeValue = value; }
        }

        // 记录时间
        private DateTime recordTime;

        /// <summary>
        /// 记录时间
        /// </summary>
        public DateTime RecordTime
        {
            get { return recordTime; }
            set { recordTime = value; }
        }

        // 记录类型
        private string recordType;

        /// <summary>
        /// 记录类型
        /// </summary>
        public string RecordType
        {
            get { return recordType; }
            set { recordType = value; }
        }
    }
}
