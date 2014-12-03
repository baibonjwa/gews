// ******************************************************************
// 概  述：大断层实体
// 作  者：伍鑫
// 创建日期：2013/11/30
// 版本号：1.0
// ******************************************************************

using System;

namespace LibEntity
{
    public class BigFaultagePointEntity
    {
        private BigFaultageEntity _bigFaultage = new BigFaultageEntity();

        /// <summary>
        /// 断层编号
        /// </summary>
        public int Id { get; set; }

        /** 断层名称 **/
        public string UpOrDown { get; set; }

        public double CoordinateX { get; set; }

        public double CoordinateY { get; set; }

        public double CoordinateZ { get; set; }

        public BigFaultageEntity BigFaultage
        {
            get { return _bigFaultage; }
            set { _bigFaultage = value; }
        }

        public string Bid { get; set; }

    }
}
