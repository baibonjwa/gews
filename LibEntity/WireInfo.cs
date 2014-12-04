// ******************************************************************
// 概  述：导线实体
// 作  者：宋英杰  
// 创建日期：2013/11/2
// 版本号：1.0
// ******************************************************************

using System;

namespace LibEntity
{
    public class WireInfo
    {
        //巷道编号
        //导线名称
        //导线点（wirePointID字符串的集合，中间用逗号分隔）
        //private string wirePoint;
        //导线级别
        //测量日期
        //校核者
        //校核日期
        private DateTime checkDate;
        private DateTime countDate;
        private DateTime measureDate;

        /// <summary>
        ///     校核日期
        /// </summary>
        public DateTime CheckDate
        {
            get { return checkDate; }
            set { checkDate = value; }
        }

        /// <summary>
        ///     校核者
        /// </summary>
        public string Checker { get; set; }

        /// <summary>
        ///     计算日期
        /// </summary>
        public DateTime CountDate
        {
            get { return countDate; }
            set { countDate = value; }
        }

        /// <summary>
        ///     计算者
        /// </summary>
        public string Counter { get; set; }

        /// <summary>
        ///     导线编号
        /// </summary>
        public int WireInfoID { get; set; }

        /// <summary>
        ///     巷道编号
        /// </summary>
        public int TunnelID { get; set; }

        /// <summary>
        ///     导线名称
        /// </summary>
        public string WireName { get; set; }

        /// <summary>
        ///     导线级别
        /// </summary>
        public string WireLevel { get; set; }

        /// <summary>
        ///     测试日期
        /// </summary>
        public DateTime MeasureDate
        {
            get { return measureDate; }
            set { measureDate = value; }
        }

        /// <summary>
        ///     观测者
        /// </summary>
        public string Vobserver { get; set; }
    }
}