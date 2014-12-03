using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LibEntity
{
    /// <summary>
    /// 柱状图
    /// </summary>
    public class histogramEnt
    {
        //主键
        private string iD;
        /// <summary>
        /// 设置或获取主键
        /// </summary>
        public string ID
        {
            get { return iD; }
            set { iD = value; }
        }

        //柱状图名称
        private string histogramEntName;
        /// <summary>
        /// 设置或获取柱状图名称
        /// </summary>
        public string HistogramEntName
        {
            get { return histogramEntName; }
            set { histogramEntName = value; }
        }
        private double bLC;
        /// <summary>
        /// 设置或获取比例尺
        /// </summary>
        public double BLC
        {
            get { return bLC; }
            set { bLC = value; }
        }
        //煤岩信息
        public List<historgramlist> listMY;
        /// <summary>
        /// 设置或获取煤岩信息
        /// </summary>
        public List<historgramlist> ListMY
        {
            get { return listMY; }
            set { listMY = value; }
        }

    }
    public class historgramlist
    {
        //主键
        private string biD;
        /// <summary>
        /// 设置或获取主键
        /// </summary>
        public string BID
        {
            get { return biD; }
            set { biD = value; }
        }
        //序号
        private int index;
        /// <summary>
        /// 设置或获取序号
        /// </summary>
        public int Index
        {
            get { return index; }
            set { index = value; }
        }
        //煤岩名称
        private string myName;
        /// <summary>
        /// 设置或获取煤岩名称
        /// </summary>
        public string MYName
        {
            get { return myName; }
            set { myName = value; }
        }
        //厚度
        private double height;
        /// <summary>
        /// 设置或获取厚度
        /// </summary>
        public double Height
        {
            get { return height; }
            set { height = value; }
        }
        //厚度累计
        private double sheight;
        /// <summary>
        /// 设置或获取厚度累计
        /// </summary>
        public double SHeight
        {
            get { return sheight; }
            set { sheight = value; }
        }
        //柱状类型
        private string zzType;
        /// <summary>
        /// 设置或获取柱状类型
        /// </summary>
        public string ZZType
        {
            get { return zzType; }
            set { zzType = value; }
        }
        //描述
        private string describe;
        /// <summary>
        /// 设置或获取描述
        /// </summary>
        public string Describe
        {
            get { return describe; }
            set { describe = value; }
        }
    }
}
