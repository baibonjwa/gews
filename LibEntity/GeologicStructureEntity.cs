// ******************************************************************
// 概  述：地质构造实体
// 作  者：宋英杰
// 创建日期：2014/3/25
// 版本号：1.0
// ******************************************************************
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LibEntity
{
    public class GeologicStructureEntity : MineDataEntity
    {
        //无计划揭露构造
        private int noPlanStructure;

        /// <summary>
        /// 无计划揭露构造
        /// </summary>
        public int NoPlanStructure
        {
            get { return noPlanStructure; }
            set { noPlanStructure = value; }
        }

        //过构造时措施无效
        private int passedStructureRuleInvalid;

        /// <summary>
        /// 过构造时措施无效
        /// </summary>
        public int PassedStructureRuleInvalid
        {
            get { return passedStructureRuleInvalid; }
            set { passedStructureRuleInvalid = value; }
        }

        //黄色预警措施无效
        private int yellowRuleInvalid;

        /// <summary>
        /// 黄色预警措施无效
        /// </summary>
        public int YellowRuleInvalid
        {
            get { return yellowRuleInvalid; }
            set { yellowRuleInvalid = value; }
        }

        //顶板破碎
        private int roofBroken;

        /// <summary>
        /// 顶板破碎
        /// </summary>
        public int RoofBroken
        {
            get { return roofBroken; }
            set { roofBroken = value; }
        }

        //煤层松软
        private int coalSeamSoft;

        /// <summary>
        /// 煤层松软
        /// </summary>
        public int CoalSeamSoft
        {
            get { return coalSeamSoft; }
            set { coalSeamSoft = value; }
        }

        //工作面煤层处于分叉、合层状态
        private int coalSeamBranch;

        /// <summary>
        /// 工作面煤层处于分叉、合层状态
        /// </summary>
        public int CoalSeamBranch
        {
            get { return coalSeamBranch; }
            set { coalSeamBranch = value; }
        }

        //顶板条件发生变化
        private int roofChange;

        /// <summary>
        /// 顶板条件发生变化
        /// </summary>
        public int RoofChange
        {
            get { return roofChange; }
            set { roofChange = value; }
        }

        //工作面夹矸突然变薄或消失
        private int gangueDisappear;

        /// <summary>
        /// 工作面夹矸突然变薄或消失
        /// </summary>
        public int GangueDisappear
        {
            get { return gangueDisappear; }
            set { gangueDisappear = value; }
        }

        //夹矸位置急剧变化
        private int gangueLocationChange;

        /// <summary>
        /// 夹矸位置急剧变化
        /// </summary>
        public int GangueLocationChange
        {
            get { return gangueLocationChange; }
            set { gangueLocationChange = value; }
        }
    }
}
