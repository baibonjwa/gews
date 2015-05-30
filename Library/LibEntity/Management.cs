// ******************************************************************
// 概  述：井下数据管理信息实体
// 作  者：宋英杰
// 创建日期：2014/4/15
// 版本号：V1.0
// 版本信息：
// V1.0 新建
// ******************************************************************

using System;
using System.Collections;
using System.Collections.Generic;
using Castle.ActiveRecord;
using NHibernate.Criterion;

namespace LibEntity
{
    [ActiveRecord("T_MANAGEMENT")]
    public class Management : MineData
    {
        public const String TableName = "T_MANAGEMENT";

        /// <summary>
        ///     设置或获取是否存在瓦斯异常不汇报
        /// </summary>
        [Property("IS_GAS_ERROR_NOT_REPORT")]
        public int IsGasErrorNotReport { get; set; }

        /// <summary>
        ///     设置或获取是否存在工作面出现地质构造不汇报
        /// </summary>
        [Property("IS_WF_NOT_REPORT")]
        public int IsWfNotReport { get; set; }

        /// <summary>
        ///     设置或获取是否存在强化瓦斯措施执行不到位
        /// </summary>
        [Property("IS_STRGAS_NOT_DO_WELL")]
        public int IsStrgasNotDoWell { get; set; }

        /// <summary>
        ///     设置或获取是否存在进回风巷隅角、尾巷管理不到位
        /// </summary>
        [Property("IS_RWMANAGEMENT_NOT_DO_WELL")]
        public int IsRwmanagementNotDoWell { get; set; }

        /// <summary>
        ///     设置或获取是否存在通风设施人为损坏
        /// </summary>
        [Property("IS_VF_BROKEN_BY_PEOPLE")]
        public int IsVfBrokenByPeople { get; set; }

        /// <summary>
        ///     设置或获取是否存在甲烷传感器位置不当、误差大、调校超过规定
        /// </summary>
        [Property("IS_ELEMENT_PLACE_NOT_GOOD")]
        public int IsElementPlaceNotGood { get; set; }

        /// <summary>
        ///     设置或获取是否存在瓦检员空漏假检
        /// </summary>
        [Property("IS_REPORTER_FALSE_DATA")]
        public int IsReporterFalseData { get; set; }

        /// <summary>
        ///     设置或获取钻孔未按设计施工次数
        /// </summary>
        [Property("IS_DRILL_WRONG_BUILD")]
        public int IsDrillWrongBuild { get; set; }

        /// <summary>
        ///     设置或获取钻孔施工不到位次数
        /// </summary>
        [Property("IS_DRILL_NOT_DO_WELL")]
        public int IsDrillNotDoWell { get; set; }

        /// <summary>
        ///     设置或获取防突措施执行不到位次数
        /// </summary>
        [Property("IS_OP_NOT_DO_WELL")]
        public int IsOpNotDoWell { get; set; }

        /// <summary>
        ///     设置或获取防突异常情况未汇报次数
        /// </summary>
        [Property("IS_OP_ERROR_NOT_REPORT")]
        public int IsOpErrorNotReport { get; set; }

        /// <summary>
        ///     设置或获取是否存在局部通风机单回路供电或不能正常切换
        /// </summary>
        [Property("IS_PART_WIND_SWITCH_ERROR")]
        public int IsPartWindSwitchError { get; set; }

        /// <summary>
        ///     设置或获取是否存在安全监测监控系统未及时安装
        /// </summary>
        [Property("IS_SAFE_CTRL_UNINSTALL")]
        public int IsSafeCtrlUninstall { get; set; }

        /// <summary>
        ///     设置或获取是否存在监测监控停运
        /// </summary>
        [Property("IS_CTRL_STOP")]
        public int IsCtrlStop { get; set; }

        /// <summary>
        ///     设置或获取是否存在不执行瓦斯治理措施、破坏通风设施
        /// </summary>
        [Property("IS_GAS_NOT_DO_WELL")]
        public int IsGasNotDowWell { get; set; }

        /// <summary>
        ///     设置或获取是否高、突矿井工作面无专职瓦斯检查员
        /// </summary>
        [Property("IS_MINE_NO_CHECKER")]
        public int IsMineNoChecker { get; set; }

        public static void DeleteByIds(IEnumerable ids)
        {
            DeleteAll(typeof(Management), ids);
        }

        public static void FindById(int id)
        {
            FindByPrimaryKey(typeof(Management), id);
        }

        public static Management[] FindAll()
        {
            return (Management[])FindAll(typeof(Management));
        }

    }
}