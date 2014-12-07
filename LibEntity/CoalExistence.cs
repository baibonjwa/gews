// ******************************************************************
// 概  述：
// 作  者：
// 日  期：
// 版本号：V1.0
// 版本信息：
// V1.0 新建
// ******************************************************************

using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using Castle.ActiveRecord;
using NHibernate.Criterion;
using NHibernate.Id;
using NHibernate.Impl;

namespace LibEntity
{
    [ActiveRecord("T_COAL_EXISTENCE")]
    public class CoalExistence : MineData
    {
        private string _coalDistoryLevel = "";
        private int _isTowardsChange = 0;

        //是否层理紊乱
        /// <summary>
        ///     设置或获取是否层理紊乱
        /// </summary>
        [Property("IS_LEVEL_DISORDER")]
        public int IsLevelDisorder { get; set; }



        // 煤厚变化

        /// <summary>
        ///     设置或获取煤厚变化
        /// </summary>
        [Property("COAL_THICK_CHANGE")]
        public double CoalThickChange { get; set; }

        // 软分层（构造煤）厚度

        /// <summary>
        ///     设置或获取软分层（构造煤）厚度
        /// </summary>
        [Property("TECTONIC_COAL_THICK")]
        public double TectonicCoalThick { get; set; }

        // 软分层（构造煤）层位是否发生变化

        /// <summary>
        ///     设置或获取软分层（构造煤）层位是否发生变化
        /// </summary>
        [Property("IS_LEVEL_CHANGE")]
        public int IsLevelChange { get; set; }

        // 煤体破坏类型

        /// <summary>
        ///     设置或获取煤体破坏类型
        /// </summary>
        [Property("COAL_DISTORY_LEVEL")]
        public string CoalDistoryLevel
        {
            get { return _coalDistoryLevel; }
            set { _coalDistoryLevel = value; }
        }

        // 是否煤层走向、倾角突然急剧变化

        /// <summary>
        ///     设置或获取是否煤层走向、倾角突然急剧变化
        /// </summary>
        [Property("IS_TOWARDS_CHANGE")]
        public int IsTowardsChange
        {
            get { return _isTowardsChange; }
            set { _isTowardsChange = value; }
        }

        // 工作面煤层是否处于分叉、合层状态

        /// <summary>
        ///     设置或获取工作面煤层是否处于分叉、合层状态
        /// </summary>
        [Property("IS_COAL_MERGE")]
        public int IsCoalMerge { get; set; }

        // 煤层是否松软

        /// <summary>
        ///     设置或获取煤层是否松软
        /// </summary>
        [Property("IS_COAL_SOFT")]
        public int IsCoalSoft { get; set; }


        public static void DeleteByIds(IEnumerable ids)
        {
            DeleteAll(typeof(CoalExistence), ids);
        }

        public static void FindById(int id)
        {
            FindByPrimaryKey(typeof(CoalExistence), id);
        }

        public static CoalExistence[] SlicedFindByCondition(int firstResult, int maxResult, int tunnelId,
            DateTime startTime, DateTime endTime)
        {
            CoalExistence[] results;
            var criterion = new List<ICriterion> { Restrictions.Eq("Tunnel.TunnelId", tunnelId) };
            if (startTime != DateTime.MinValue && endTime != DateTime.MinValue)
            {
                criterion.Add(Restrictions.Between("Datetime", startTime, endTime));
            }
            results = (CoalExistence[])SlicedFindAll(typeof(CoalExistence), firstResult, maxResult, criterion.ToArray());
            return results;
        }



    }
}