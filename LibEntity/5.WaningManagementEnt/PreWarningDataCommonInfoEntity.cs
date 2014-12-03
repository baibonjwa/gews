// ******************************************************************
// 概  述：预警数据通用信息
// 作  者：杨小颖  
// 创建日期：2013/12/28
// 版本号：1.0
// ******************************************************************
using System;
using System.Collections;


namespace LibEntity
{

    public class PreWarningDataCommonInfoEntity
    {
        //坐标
        private double _x;

        public double X
        {
            get { return _x; }
            set { _x = value; }
        }
        private double _y;

        public double Y
        {
            get { return _y; }
            set { _y = value; }
        }
        private double _z;

        public double Z
        {
            get { return _z; }
            set { _z = value; }
        }
        //录入时间
        private DateTime _date;

        public DateTime Date
        {
            get { return _date; }
            set { _date = value; }
        }
        //值
        private double _value;

        public double Value
        {
            get { return _value; }
            set { _value = value; }
        }

        //巷道ID
        private int _tunnelID;

        public int TunnelID
        {
            get { return _tunnelID; }
            set { _tunnelID = value; }
        }

    }
}
