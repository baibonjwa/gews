using System.Collections.Generic;

namespace LibEntity
{
    /// <summary>
    ///     柱状图
    /// </summary>
    public class histogramEnt
    {
        //主键

        public List<historgramlist> listMY;

        /// <summary>
        ///     设置或获取主键
        /// </summary>
        public string ID { get; set; }

        //柱状图名称

        /// <summary>
        ///     设置或获取柱状图名称
        /// </summary>
        public string HistogramEntName { get; set; }

        /// <summary>
        ///     设置或获取比例尺
        /// </summary>
        public double BLC { get; set; }

        //煤岩信息

        /// <summary>
        ///     设置或获取煤岩信息
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

        /// <summary>
        ///     设置或获取主键
        /// </summary>
        public string BID { get; set; }

        //序号

        /// <summary>
        ///     设置或获取序号
        /// </summary>
        public int Index { get; set; }

        //煤岩名称

        /// <summary>
        ///     设置或获取煤岩名称
        /// </summary>
        public string MYName { get; set; }

        //厚度

        /// <summary>
        ///     设置或获取厚度
        /// </summary>
        public double Height { get; set; }

        //厚度累计

        /// <summary>
        ///     设置或获取厚度累计
        /// </summary>
        public double SHeight { get; set; }

        //柱状类型

        /// <summary>
        ///     设置或获取柱状类型
        /// </summary>
        public string ZZType { get; set; }

        //描述

        /// <summary>
        ///     设置或获取描述
        /// </summary>
        public string Describe { get; set; }
    }
}