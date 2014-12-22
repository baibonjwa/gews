// ******************************************************************
// 概  述：
// 作  者：
// 日  期：2014-8-15
// 版本号：
// 版本信息：
// V1.0 新建
// ******************************************************************

using System;
using Castle.ActiveRecord;
using Castle.ActiveRecord.Framework;

namespace LibEntity
{
    [ActiveRecord("T_TUNNEL_HENGCHUAN")]
    public class TunnelHChuan : ActiveRecordLinqBase<TunnelHChuan>
    {
        //主键

        private DateTime _startDate;
        private DateTime _stopDate;

        /// <summary>
        ///     设置或获取主键
        /// </summary>
        [PrimaryKey(PrimaryKeyType.Identity, "ID")]
        public int Id { get; set; }

        // 横川名称

        /// <summary>
        ///     设置或获取横川名称
        /// </summary>
        [Property("NAME_HCHUAN")]
        public string NameHChuan { get; set; }

        // 关联巷道1ID

        /// <summary>
        ///     设置或获取巷道ID
        /// </summary>
        [Property("TUNNEL_ID1")]
        public int TunnelId1 { get; set; }

        // 关联巷道1ID

        /// <summary>
        ///     设置或获取巷道ID
        /// </summary>
        [Property("TUNNEL_ID2")]
        public int TunnelId2 { get; set; }

        //导线点X1

        /// <summary>
        ///     导线点X1
        /// </summary>
        [Property("X_1")]
        public double X1 { get; set; }

        //导线点Y1

        /// <summary>
        ///     导线点X1
        /// </summary>
        [Property("Y_1")]
        public double Y1 { get; set; }

        //导线点Z1

        /// <summary>
        ///     导线点Z1
        /// </summary>
        [Property("Z_1")]
        public double Z1 { get; set; }

        //导线点X2

        /// <summary>
        ///     导线点X1
        /// </summary>
        [Property("X_2")]
        public double X2 { get; set; }

        //导线点Y1

        /// <summary>
        ///     导线点X1
        /// </summary>
        [Property("Y_2")]
        public double Y2 { get; set; }

        //导线点Z1

        /// <summary>
        ///     导线点Z1
        /// </summary>
        [Property("Z_2")]
        public double Z2 { get; set; }

        //方位角

        /// <summary>
        ///     设置或获取方位角
        /// </summary>
        [Property("AZIMUTH")]
        public double Azimuth { get; set; }

        // 队别编号

        /// <summary>
        ///     设置或获取队别编号
        /// </summary>
        [BelongsTo("TEAM_NAME_ID")]
        public TeamInfo TeamInfo { get; set; }

        // 开工日期

        /// <summary>
        ///     设置或获取开工日期
        /// </summary>
        [Property("START_DATE")]
        public DateTime StartDate
        {
            get { return _startDate; }
            set { _startDate = value; }
        }

        // 是否施工完毕

        /// <summary>
        ///     设置或获取是否掘进完毕
        /// </summary>
        [Property("IS_FINISH")]
        public int IsFinish { get; set; }

        // 停工日期

        /// <summary>
        ///     设置或获取停工日期
        /// </summary>
        [Property("STOP_DATE")]
        public DateTime StopDate
        {
            get { return _stopDate; }
            set { _stopDate = value; }
        }

        // 工作制式

        /// <summary>
        ///     设置或获取工作制式
        /// </summary>
        [Property("WORK_STYLE")]
        public string WorkStyle { get; set; }

        // 班次

        /// <summary>
        ///     设置或获取班次
        /// </summary>
        [Property("WORK_TIME")]
        public string WorkTime { get; set; }

        // 班次

        /// <summary>
        ///     设置或获取巷道状态
        /// </summary>
        [Property("STATE")]
        public string State { get; set; }

        /// <summary>
        ///     宽度
        /// </summary>
        [Property("WIDTH")]
        public double Width { get; set; }
    }
}