// ******************************************************************
// 概  述：井下数据管理信息实体
// 作  者：宋英杰
// 创建日期：2014/4/15
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
    public class ManagementEntity : MineDataEntity
    {
        //是否存在瓦斯异常不汇报
        private int isGasErrorNotReport;
        /// <summary>
        /// 设置或获取是否存在瓦斯异常不汇报
        /// </summary>
        public int IsGasErrorNotReport
        {
            get { return isGasErrorNotReport; }
            set { isGasErrorNotReport = value; }
        }
        //是否存在工作面出现地质构造不汇报
        private int isWFNotReport;
        /// <summary>
        /// 设置或获取是否存在工作面出现地质构造不汇报
        /// </summary>
        public int IsWFNotReport
        {
            get { return isWFNotReport; }
            set { isWFNotReport = value; }
        }
        //是否存在强化瓦斯措施执行不到位
        private int isStrgasNotDoWell;
        /// <summary>
        /// 设置或获取是否存在强化瓦斯措施执行不到位
        /// </summary>
        public int IsStrgasNotDoWell
        {
            get { return isStrgasNotDoWell; }
            set { isStrgasNotDoWell = value; }
        }
        //是否存在进回风巷隅角、尾巷管理不到位
        private int isRwmanagementNotDoWell;
        /// <summary>
        /// 设置或获取是否存在进回风巷隅角、尾巷管理不到位
        /// </summary>
        public int IsRwmanagementNotDoWell
        {
            get { return isRwmanagementNotDoWell; }
            set { isRwmanagementNotDoWell = value; }
        }
        //是否存在通风设施人为损坏
        private int isVFBrokenByPeople;
        /// <summary>
        /// 设置或获取是否存在通风设施人为损坏
        /// </summary>
        public int IsVFBrokenByPeople
        {
            get { return isVFBrokenByPeople; }
            set { isVFBrokenByPeople = value; }
        }
        //是否存在甲烷传感器位置不当、误差大、调校超过规定
        private int isElementPlaceNotGood;
        /// <summary>
        /// 设置或获取是否存在甲烷传感器位置不当、误差大、调校超过规定
        /// </summary>
        public int IsElementPlaceNotGood
        {
            get { return isElementPlaceNotGood; }
            set { isElementPlaceNotGood = value; }
        }
        //是否存在瓦检员空漏假检
        private int isReporterFalseData;
        /// <summary>
        /// 设置或获取是否存在瓦检员空漏假检
        /// </summary>
        public int IsReporterFalseData
        {
            get { return isReporterFalseData; }
            set { isReporterFalseData = value; }
        }
        //钻孔未按设计施工次数
        private int isDrillWrongBuild;
        /// <summary>
        /// 设置或获取钻孔未按设计施工次数
        /// </summary>
        public int IsDrillWrongBuild
        {
            get { return isDrillWrongBuild; }
            set { isDrillWrongBuild = value; }
        }
        //钻孔施工不到位次数
        private int isDrillNotDoWell;
        /// <summary>
        /// 设置或获取钻孔施工不到位次数
        /// </summary>
        public int IsDrillNotDoWell
        {
            get { return isDrillNotDoWell; }
            set { isDrillNotDoWell = value; }
        }
        //防突措施执行不到位次数
        private int isOPNotDoWell;
        /// <summary>
        /// 设置或获取防突措施执行不到位次数
        /// </summary>
        public int IsOPNotDoWell
        {
            get { return isOPNotDoWell; }
            set { isOPNotDoWell = value; }
        }
        //防突异常情况未汇报次数
        private int isOPErrorNotReport;
        /// <summary>
        /// 设置或获取防突异常情况未汇报次数
        /// </summary>
        public int IsOPErrorNotReport
        {
            get { return isOPErrorNotReport; }
            set { isOPErrorNotReport = value; }
        }
        //是否存在局部通风机单回路供电或不能正常切换
        private int isPartWindSwitchError;
        /// <summary>
        /// 设置或获取是否存在局部通风机单回路供电或不能正常切换
        /// </summary>
        public int IsPartWindSwitchError
        {
            get { return isPartWindSwitchError; }
            set { isPartWindSwitchError = value; }
        }
        //是否存在安全监测监控系统未及时安装
        private int isSafeCtrlUninstall;
        /// <summary>
        /// 设置或获取是否存在安全监测监控系统未及时安装
        /// </summary>
        public int IsSafeCtrlUninstall
        {
            get { return isSafeCtrlUninstall; }
            set { isSafeCtrlUninstall = value; }
        }
        //是否存在监测监控停运
        private int isCtrlStop;
        /// <summary>
        /// 设置或获取是否存在监测监控停运
        /// </summary>
        public int IsCtrlStop
        {
            get { return isCtrlStop; }
            set { isCtrlStop = value; }
        }
        //是否存在不执行瓦斯治理措施、破坏通风设施
        private int isGasNotDowWell;
        /// <summary>
        /// 设置或获取是否存在不执行瓦斯治理措施、破坏通风设施
        /// </summary>
        public int IsGasNotDowWell
        {
            get { return isGasNotDowWell; }
            set { isGasNotDowWell = value; }
        }
        //是否高、突矿井工作面无专职瓦斯检查员
        private int isMineNoChecker;
        /// <summary>
        /// 设置或获取是否高、突矿井工作面无专职瓦斯检查员
        /// </summary>
        public int IsMineNoChecker
        {
            get { return isMineNoChecker; }
            set { isMineNoChecker = value; }
        }
    }
}
