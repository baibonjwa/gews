using System;
using System.Collections.Generic;
using Castle.ActiveRecord;
using NHibernate.Criterion;

namespace LibEntity
{
    [ActiveRecord("T_GASDATA")]
    public class GasData : MineData
    {
        public const String TableName = "T_GASDATA";

        /// <summary>
        ///     设置或获取瓦斯探头断电次数
        /// </summary>
        [Property("POWER_FALIURE")]
        public double PowerFailure { get; set; }

        /// <summary>
        ///     设置或获取吸钻预兆次数
        /// </summary>
        [Property("DRILL_TIMES")]
        public double DrillTimes { get; set; }

        /// <summary>
        ///     设置或获取瓦斯忽大忽小预兆次数
        /// </summary>
        [Property("GAS_TIMES")]
        public double GasTimes { get; set; }

        /// <summary>
        ///     设置或获取气温下降预兆次数
        /// </summary>
        [Property("TEMP_DOWN_TIMES")]
        public double TempDownTimes { get; set; }

        /// <summary>
        ///     设置或获取煤炮频繁预兆次数
        /// </summary>
        [Property("COAL_BANG_TIMES")]
        public double CoalBangTimes { get; set; }

        /// <summary>
        ///     设置或获取喷孔次数
        /// </summary>
        [Property("CRATER_TIMES")]
        public double CraterTimes { get; set; }

        /// <summary>
        ///     设置或获取顶钻次数
        /// </summary>
        [Property("STOPER_TIMES")]
        public double StoperTimes { get; set; }

        /// <summary>
        ///     瓦斯浓度
        /// </summary>
        [Property("GAS_THICKNESS")]
        public double GasThickness { get; set; }

        public static void FindById(int id)
        {
            FindByPrimaryKey(typeof(GasData), id);
        }

        public static int GetRecordCount()
        {
            return FindAll(typeof(GasData)).Length;
        }


        public static GasData[] SlicedFindByCondition(int firstResult, int maxResult, int tunnelId,
            DateTime startTime, DateTime endTime)
        {
            GasData[] results;
            var criterion = new List<ICriterion> { Restrictions.Eq("Tunnel.Tunnel", tunnelId) };
            if (startTime != DateTime.MinValue && endTime != DateTime.MinValue)
            {
                criterion.Add(Restrictions.Between("Datetime", startTime, endTime));
            }
            results = (GasData[])SlicedFindAll(typeof(GasData), firstResult, maxResult, criterion.ToArray());
            return results;
        }

    }


}