// ******************************************************************
// 概  述：井下数据管理信息实体
// 作  者：宋英杰
// 创建日期：2014/4/15
// 版本号：V1.0
// 版本信息：
// V1.0 新建
// ******************************************************************

namespace LibEntity
{
    public class Management : MineData
    {
        //是否存在瓦斯异常不汇报

        /// <summary>
        ///     设置或获取是否存在瓦斯异常不汇报
        /// </summary>
        public int IsGasErrorNotReport { get; set; }

        //是否存在工作面出现地质构造不汇报

        /// <summary>
        ///     设置或获取是否存在工作面出现地质构造不汇报
        /// </summary>
        public int IsWFNotReport { get; set; }

        //是否存在强化瓦斯措施执行不到位

        /// <summary>
        ///     设置或获取是否存在强化瓦斯措施执行不到位
        /// </summary>
        public int IsStrgasNotDoWell { get; set; }

        //是否存在进回风巷隅角、尾巷管理不到位

        /// <summary>
        ///     设置或获取是否存在进回风巷隅角、尾巷管理不到位
        /// </summary>
        public int IsRwmanagementNotDoWell { get; set; }

        //是否存在通风设施人为损坏

        /// <summary>
        ///     设置或获取是否存在通风设施人为损坏
        /// </summary>
        public int IsVFBrokenByPeople { get; set; }

        //是否存在甲烷传感器位置不当、误差大、调校超过规定

        /// <summary>
        ///     设置或获取是否存在甲烷传感器位置不当、误差大、调校超过规定
        /// </summary>
        public int IsElementPlaceNotGood { get; set; }

        //是否存在瓦检员空漏假检

        /// <summary>
        ///     设置或获取是否存在瓦检员空漏假检
        /// </summary>
        public int IsReporterFalseData { get; set; }

        //钻孔未按设计施工次数

        /// <summary>
        ///     设置或获取钻孔未按设计施工次数
        /// </summary>
        public int IsDrillWrongBuild { get; set; }

        //钻孔施工不到位次数

        /// <summary>
        ///     设置或获取钻孔施工不到位次数
        /// </summary>
        public int IsDrillNotDoWell { get; set; }

        //防突措施执行不到位次数

        /// <summary>
        ///     设置或获取防突措施执行不到位次数
        /// </summary>
        public int IsOPNotDoWell { get; set; }

        //防突异常情况未汇报次数

        /// <summary>
        ///     设置或获取防突异常情况未汇报次数
        /// </summary>
        public int IsOPErrorNotReport { get; set; }

        //是否存在局部通风机单回路供电或不能正常切换

        /// <summary>
        ///     设置或获取是否存在局部通风机单回路供电或不能正常切换
        /// </summary>
        public int IsPartWindSwitchError { get; set; }

        //是否存在安全监测监控系统未及时安装

        /// <summary>
        ///     设置或获取是否存在安全监测监控系统未及时安装
        /// </summary>
        public int IsSafeCtrlUninstall { get; set; }

        //是否存在监测监控停运

        /// <summary>
        ///     设置或获取是否存在监测监控停运
        /// </summary>
        public int IsCtrlStop { get; set; }

        //是否存在不执行瓦斯治理措施、破坏通风设施

        /// <summary>
        ///     设置或获取是否存在不执行瓦斯治理措施、破坏通风设施
        /// </summary>
        public int IsGasNotDowWell { get; set; }

        //是否高、突矿井工作面无专职瓦斯检查员

        /// <summary>
        ///     设置或获取是否高、突矿井工作面无专职瓦斯检查员
        /// </summary>
        public int IsMineNoChecker { get; set; }
    }
}