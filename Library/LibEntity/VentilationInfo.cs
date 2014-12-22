﻿// ******************************************************************
// 概  述：
// 作  者：
// 日  期：
// 版本号：V1.0
// 版本信息：
// V1.0 新建
// ******************************************************************

using System;
using System.Collections.Generic;
using Castle.ActiveRecord;
using NHibernate.Criterion;

namespace LibEntity
{
    [ActiveRecord("T_VENTILATION_INFO")]
    public class VentilationInfo : MineData
    {
        public const String TableName = "T_VENTILATION_INFO";

        /// <summary>
        ///     设置或获取是否有无风区域
        /// </summary>
        [Property("IS_NO_WIND_AREA")]
        public int IsNoWindArea { get; set; }

        // 是否有微风区域

        /// <summary>
        ///     设置或获取是否有微风区域
        /// </summary>
        [Property("IS_LIGHT_WIND_AREA")]
        public int IsLightWindArea { get; set; }

        // 是否有风流反向区域

        /// <summary>
        ///     设置或获取是否有风流反向区域
        /// </summary>
        [Property("IS_RETURN_WIND_AREA")]
        public int IsReturnWindArea { get; set; }

        // 是否通风断面小于设计断面的2/3

        /// <summary>
        ///     设置或获取是否通风断面小于设计断面的2/3
        /// </summary>
        [Property("IS_SMALL")]
        public int IsSmall { get; set; }

        // 是否工作面风量低于计划风量，风速与《煤矿安全规程》规定不符

        /// <summary>
        ///     设置或获取是否工作面风量低于计划风量，风速与《煤矿安全规程》规定不符
        /// </summary>
        [Property("IS_FOLLOW_RULE")]
        public int IsFollowRule { get; set; }

        //通风断面

        /// <summary>
        ///     通风断面
        /// </summary>
        [Property("FAULTAGE_AREA")]
        public double FaultageArea { get; set; }

        //风量

        /// <summary>
        ///     风量
        /// </summary>
        [Property("AIR_FLOW")]
        public double AirFlow { get; set; }


        public static void FindById(int id)
        {
            FindByPrimaryKey(typeof(VentilationInfo), id);
        }

        public static int GetRecordCount()
        {
            return FindAll(typeof(VentilationInfo)).Length;
        }


        public static VentilationInfo[] SlicedFindByCondition(int firstResult, int maxResult, int tunnelId,
            DateTime startTime, DateTime endTime)
        {
            VentilationInfo[] results;
            var criterion = new List<ICriterion> { Restrictions.Eq("Tunnel.Tunnel", tunnelId) };
            if (startTime != DateTime.MinValue && endTime != DateTime.MinValue)
            {
                criterion.Add(Restrictions.Between("Datetime", startTime, endTime));
            }
            results = (VentilationInfo[])SlicedFindAll(typeof(VentilationInfo), firstResult, maxResult, criterion.ToArray());
            return results;
        }
    }
}