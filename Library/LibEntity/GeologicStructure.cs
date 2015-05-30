// ******************************************************************
// 概  述：地质构造实体
// 作  者：宋英杰
// 创建日期：2014/3/25
// 版本号：1.0
// ******************************************************************

using System;
using System.Collections.Generic;
using Castle.ActiveRecord;
using NHibernate.Criterion;

namespace LibEntity
{
    [ActiveRecord("T_GEOLOGIC_STRUCTURE")]
    public class GeologicStructure : MineData
    {
        public const String TableName = "T_GEOLOGIC_STRUCTURE";

        /// <summary>
        ///     无计划揭露构造
        /// </summary>
        [Property("NO_PLAN_STRUCTURE")]
        public int NoPlanStructure { get; set; }

        /// <summary>
        ///     过构造时措施无效
        /// </summary>
        [Property("PASSED_STRUCTURE_RULE_INVALID")]
        public int PassedStructureRuleInvalid { get; set; }

        /// <summary>
        ///     黄色预警措施无效
        /// </summary>
        [Property("YELLOW_RULE_INVALID")]
        public int YellowRuleInvalid { get; set; }

        /// <summary>
        ///     顶板破碎
        /// </summary>
        [Property("ROOF_BROKEN")]
        public int RoofBroken { get; set; }

        /// <summary>
        ///     煤层松软
        /// </summary>
        [Property("COAL_SEAM_SOFT")]
        public int CoalSeamSoft { get; set; }

        /// <summary>
        ///     工作面煤层处于分叉、合层状态
        /// </summary>
        [Property("COAL_SEAM_BRANCH")]
        public int CoalSeamBranch { get; set; }

        //顶板条件发生变化

        /// <summary>
        ///     顶板条件发生变化
        /// </summary>
        [Property("ROOF_CHANGE")]
        public int RoofChange { get; set; }

        /// <summary>
        ///     工作面夹矸突然变薄或消失
        /// </summary>
        [Property("GANGUE_DISAPPEAR")]
        public int GangueDisappear { get; set; }

        /// <summary>
        ///     夹矸位置急剧变化
        /// </summary>
        [Property("GANGUE_LOCATION_CHANGE")]
        public int GangueLocationChange { get; set; }

        public static void FindById(int id)
        {
            FindByPrimaryKey(typeof(GeologicStructure), id);
        }

        public static GeologicStructure[] FindAll()
        {
            return (GeologicStructure[])FindAll(typeof(GeologicStructure));
        }


    }
}