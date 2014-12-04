// ******************************************************************
// 概  述：预警数据通用信息
// 作  者：杨小颖  
// 创建日期：2013/12/28
// 版本号：1.0
// ******************************************************************

using System;

namespace LibEntity
{
    public class PreWarningDataCommonInfoEntity
    {
        //坐标

        private DateTime _date;
        public double X { get; set; }

        public double Y { get; set; }

        public double Z { get; set; }

        //录入时间

        public DateTime Date
        {
            get { return _date; }
            set { _date = value; }
        }

        //值

        public double Value { get; set; }

        //巷道ID

        public int TunnelID { get; set; }
    }
}