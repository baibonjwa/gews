using System;
using Castle.ActiveRecord;
using NHibernate.Criterion;

namespace LibEntity
{
    [ActiveRecord("T_EARLY_WARNING_RESULT")]
    public class EarlyWarningResult : ActiveRecordBase<EarlyWarningResult>
    {
        public EarlyWarningResult()
        {
            Gas = 2;
            Coal = 2;
            Geology = 2;
            Ventilation = 2;
            Management = 2;
            WarningResult = 2;
        }

        [PrimaryKey(PrimaryKeyType.Identity, "ID")]
        public int Id { get; set; }

        /// <summary>
        ///     设置或获取巷道编号
        /// </summary>
        [BelongsTo("TUNNEL_ID")]
        public Tunnel Tunnel { get; set; }

        /// <summary>
        ///     班次
        /// </summary>
        [Property("SHIFT")]
        public string Shift { get; set; }

        /// <summary>
        ///     时间
        /// </summary>
        [Property("DATE_TIME")]
        public DateTime DateTime { get; set; }

        /// <summary>
        ///     预警类型
        /// </summary>
        [Property("WARNING_TYPE")]
        public int WarningType { get; set; }

        /// <summary>
        ///     预警结果
        /// </summary>
        [Property("WARNING_RESULT")]
        public int WarningResult { get; set; }

        /// <summary>
        ///     瓦斯
        /// </summary>
        [Property("GAS")]
        public int Gas { get; set; }

        /// <summary>
        ///     煤层赋存
        /// </summary>
        [Property("COAL")]
        public int Coal { get; set; }

        /// <summary>
        ///     地质构造
        /// </summary>
        [Property("GEOLOGY")]
        public int Geology { get; set; }

        /// <summary>
        ///     通风
        /// </summary>
        [Property("VENTILATION")]
        public int Ventilation { get; set; }

        /// <summary>
        ///     管理
        /// </summary>
        [Property("MANAGEMENT")]
        public int Management { get; set; }

        /// <summary>
        ///     解除状态
        /// </summary>
        [Property("HANDLE_STATUS")]
        public int HandleStatus { get; set; }

        public static EarlyWarningResult[] FindAllByWarningResultsIsRedAndYellowAndHandleStatusLtThree()
        {
            var criterion = new ICriterion[]
            {
                Restrictions.Or(Restrictions.Eq("WarningResult", 0), Restrictions.Eq("WarningResult", 1)),
                Restrictions.Lt("HandleStatus", 3)
            };
            return FindAll(criterion);
        }

    }
}