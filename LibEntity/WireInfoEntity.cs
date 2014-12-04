// ******************************************************************
// 概  述：导线实体
// 作  者：宋英杰  
// 创建日期：2013/11/2
// 版本号：1.0
// ******************************************************************
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LibEntity
{
    public class WireInfoEntity
    {
        private int wireInfoID;
        //巷道编号
        private int tunnelID;
        //导线名称
        private string wireName;
        //导线点（wirePointID字符串的集合，中间用逗号分隔）
        //private string wirePoint;
        //导线级别
        private string wireLevel;
        //测量日期
        private DateTime measureDate;
        //观测者
        private string vobserver;
        //计算者
        private string counter;
        //计算日期
        private DateTime countDate;
        //校核者
        private string checker;
        //校核日期
        private DateTime checkDate;

        /// <summary>
        /// 校核日期
        /// </summary>
        public DateTime CheckDate
        {
            get { return checkDate; }
            set { checkDate = value; }
        }

        /// <summary>
        /// 校核者
        /// </summary>
        public string Checker
        {
            get { return checker; }
            set { checker = value; }
        }

        /// <summary>
        /// 计算日期
        /// </summary>
        public DateTime CountDate
        {
            get { return countDate; }
            set { countDate = value; }
        }

        /// <summary>
        /// 计算者
        /// </summary>
        public string Counter
        {
            get { return counter; }
            set { counter = value; }
        }

        /// <summary>
        /// 导线编号
        /// </summary>
        public int WireInfoID
        {
            get { return wireInfoID; }
            set { wireInfoID = value; }
        }

        /// <summary>
        /// 巷道编号
        /// </summary>
        public int TunnelID
        {
            get { return tunnelID; }
            set { tunnelID = value; }
        }

        /// <summary>
        /// 导线名称
        /// </summary>
        public string WireName
        {
            get { return wireName; }
            set { wireName = value; }
        }           

        /// <summary>
        /// 导线级别
        /// </summary>
        public string WireLevel
        {
            get { return wireLevel; }
            set { wireLevel = value; }
        }        

        /// <summary>
        /// 测试日期
        /// </summary>
        public DateTime MeasureDate
        {
            get { return measureDate; }
            set { measureDate = value; }
        }

        /// <summary>
        /// 观测者
        /// </summary>
        public string Vobserver
        {
            get { return vobserver; }
            set { vobserver = value; }
        }
    }
}
