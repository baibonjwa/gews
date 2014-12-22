// ******************************************************************
// 概  述：
// 作  者：
// 日  期：2014-8-15
// 版本号：
// 版本信息：
// V1.0 新建
// ******************************************************************

using System;

namespace LibEntity
{
    public class TunnelHChuan
    {
        //主键

        private DateTime startDate;
        private DateTime stopDate;

        /// <summary>
        ///     设置或获取主键
        /// </summary>
        public int ID { get; set; }

        // 横川名称

        /// <summary>
        ///     设置或获取横川名称
        /// </summary>
        public string NameHChuan { get; set; }

        // 关联巷道1ID

        /// <summary>
        ///     设置或获取巷道ID
        /// </summary>
        public int TunnelID1 { get; set; }

        // 关联巷道1ID

        /// <summary>
        ///     设置或获取巷道ID
        /// </summary>
        public int TunnelID2 { get; set; }

        //导线点X1

        /// <summary>
        ///     导线点X1
        /// </summary>
        public double X_1 { get; set; }

        //导线点Y1

        /// <summary>
        ///     导线点X1
        /// </summary>
        public double Y_1 { get; set; }

        //导线点Z1

        /// <summary>
        ///     导线点Z1
        /// </summary>
        public double Z_1 { get; set; }

        //导线点X2

        /// <summary>
        ///     导线点X1
        /// </summary>
        public double X_2 { get; set; }

        //导线点Y1

        /// <summary>
        ///     导线点X1
        /// </summary>
        public double Y_2 { get; set; }

        //导线点Z1

        /// <summary>
        ///     导线点Z1
        /// </summary>
        public double Z_2 { get; set; }

        //方位角

        /// <summary>
        ///     设置或获取方位角
        /// </summary>
        public double Azimuth { get; set; }

        // 队别编号

        /// <summary>
        ///     设置或获取队别编号
        /// </summary>
        public int TeamNameID { get; set; }

        // 开工日期

        /// <summary>
        ///     设置或获取开工日期
        /// </summary>
        public DateTime StartDate
        {
            get { return startDate; }
            set { startDate = value; }
        }

        // 是否施工完毕

        /// <summary>
        ///     设置或获取是否掘进完毕
        /// </summary>
        public int IsFinish { get; set; }

        // 停工日期

        /// <summary>
        ///     设置或获取停工日期
        /// </summary>
        public DateTime StopDate
        {
            get { return stopDate; }
            set { stopDate = value; }
        }

        // 工作制式

        /// <summary>
        ///     设置或获取工作制式
        /// </summary>
        public string WorkStyle { get; set; }

        // 班次

        /// <summary>
        ///     设置或获取班次
        /// </summary>
        public string WorkTime { get; set; }

        // 班次

        /// <summary>
        ///     设置或获取巷道状态
        /// </summary>
        public string State { get; set; }

        /// <summary>
        ///     宽度
        /// </summary>
        public double Width { get; set; }
    }
}