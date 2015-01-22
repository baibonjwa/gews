using System;
using System.Collections;
using System.Collections.Generic;
using Castle.ActiveRecord;
using NHibernate.Criterion;

namespace LibEntity
{
    [ActiveRecord("T_COAL_EXISTENCE")]
    public class CoalExistence : MineData
    {
        private string _coalDistoryLevel = "";

        public CoalExistence()
        {
            IsTowardsChange = 0;
        }

        public const String TableName = "T_COAL_EXISTENCE";

        /// <summary>
        ///     设置或获取是否层理紊乱
        /// </summary>
        [Property("IS_LEVEL_DISORDER")]
        public int IsLevelDisorder { get; set; }


        /// <summary>
        ///     设置或获取煤厚变化
        /// </summary>
        [Property("COAL_THICK_CHANGE")]
        public double CoalThickChange { get; set; }

        /// <summary>
        ///     设置或获取软分层（构造煤）厚度
        /// </summary>
        [Property("TECTONIC_COAL_THICK")]
        public double TectonicCoalThick { get; set; }

        /// <summary>
        ///     设置或获取软分层（构造煤）层位是否发生变化
        /// </summary>
        [Property("IS_LEVEL_CHANGE")]
        public int IsLevelChange { get; set; }

        /// <summary>
        ///     设置或获取煤体破坏类型
        /// </summary>
        [Property("COAL_DISTORY_LEVEL")]
        public string CoalDistoryLevel
        {
            get { return _coalDistoryLevel; }
            set { _coalDistoryLevel = value; }
        }

        /// <summary>
        ///     设置或获取是否煤层走向、倾角突然急剧变化
        /// </summary>
        [Property("IS_TOWARDS_CHANGE")]
        public int IsTowardsChange { get; set; }

        /// <summary>
        ///     设置或获取工作面煤层是否处于分叉、合层状态
        /// </summary>
        [Property("IS_COAL_MERGE")]
        public int IsCoalMerge { get; set; }

        /// <summary>
        ///     设置或获取煤层是否松软
        /// </summary>
        [Property("IS_COAL_SOFT")]
        public int IsCoalSoft { get; set; }

        public static void DeleteByIds(IEnumerable ids)
        {
            DeleteAll(typeof(CoalExistence), ids);
        }

        public CoalExistence FindById(int id)
        {
            return (CoalExistence)FindByPrimaryKey(typeof(CoalExistence), id);
        }

        public static int GetRecordCount()
        {
            return FindAll(typeof(CoalExistence)).Length;
        }


        public static CoalExistence[] SlicedFindByTunnelIdAndTime(int firstResult, int maxResult, int tunnelId,
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

        public static CoalExistence[] FindAll()
        {
            return (CoalExistence[])FindAll(typeof(CoalExistence));
        }



    }
}