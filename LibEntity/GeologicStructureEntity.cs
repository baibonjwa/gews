// ******************************************************************
// 概  述：地质构造实体
// 作  者：宋英杰
// 创建日期：2014/3/25
// 版本号：1.0
// ******************************************************************

namespace LibEntity
{
    public class GeologicStructureEntity : MineDataEntity
    {
        //无计划揭露构造

        /// <summary>
        ///     无计划揭露构造
        /// </summary>
        public int NoPlanStructure { get; set; }

        //过构造时措施无效

        /// <summary>
        ///     过构造时措施无效
        /// </summary>
        public int PassedStructureRuleInvalid { get; set; }

        //黄色预警措施无效

        /// <summary>
        ///     黄色预警措施无效
        /// </summary>
        public int YellowRuleInvalid { get; set; }

        //顶板破碎

        /// <summary>
        ///     顶板破碎
        /// </summary>
        public int RoofBroken { get; set; }

        //煤层松软

        /// <summary>
        ///     煤层松软
        /// </summary>
        public int CoalSeamSoft { get; set; }

        //工作面煤层处于分叉、合层状态

        /// <summary>
        ///     工作面煤层处于分叉、合层状态
        /// </summary>
        public int CoalSeamBranch { get; set; }

        //顶板条件发生变化

        /// <summary>
        ///     顶板条件发生变化
        /// </summary>
        public int RoofChange { get; set; }

        //工作面夹矸突然变薄或消失

        /// <summary>
        ///     工作面夹矸突然变薄或消失
        /// </summary>
        public int GangueDisappear { get; set; }

        //夹矸位置急剧变化

        /// <summary>
        ///     夹矸位置急剧变化
        /// </summary>
        public int GangueLocationChange { get; set; }
    }
}