// ******************************************************************
// 概  述：
// 作  者：
// 日  期：
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
    public class CoalExistenceEntity:MineDataEntity
    {
        //是否层理紊乱

        /// <summary>
        /// 设置或获取是否层理紊乱
        /// </summary>
        public int IsLevelDisorder { get; set; }

        // 煤厚变化

        /// <summary>
        /// 设置或获取煤厚变化
        /// </summary>
        public double CoalThickChange { get; set; }

        // 软分层（构造煤）厚度

        /// <summary>
        /// 设置或获取软分层（构造煤）厚度
        /// </summary>
        public double TectonicCoalThick { get; set; }

        // 软分层（构造煤）层位是否发生变化

        /// <summary>
        /// 设置或获取软分层（构造煤）层位是否发生变化
        /// </summary>
        public int IsLevelChange { get; set; }

        // 煤体破坏类型

        /// <summary>
        /// 设置或获取煤体破坏类型
        /// </summary>
        public string CoalDistoryLevel { get; set; }

        // 是否煤层走向、倾角突然急剧变化

        /// <summary>
        /// 设置或获取是否煤层走向、倾角突然急剧变化
        /// </summary>
        public int IsTowardsChange { get; set; }

        // 工作面煤层是否处于分叉、合层状态

        /// <summary>
        /// 设置或获取工作面煤层是否处于分叉、合层状态
        /// </summary>
        public int IsCoalMerge { get; set; }

        // 煤层是否松软

        /// <summary>
        /// 设置或获取煤层是否松软
        /// </summary>
        public int IsCoalSoft { get; set; }
    }
}
