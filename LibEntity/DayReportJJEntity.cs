// ******************************************************************
// 概  述：掘进进尺日报数据实体
// 作  者：宋英杰
// 日  期：2014/3/12
// 版本号：V1.0
// 版本信息：
// V1.0 新建
// ******************************************************************
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LibEntity
{
    public class DayReportJJEntity :DayReportEntity
    {
        //距参考导线点距离
        private double distanceFromWirepoint;

        /// <summary>
        /// 距参考导线点距离
        /// </summary>
        public double DistanceFromWirepoint
        {
            get { return distanceFromWirepoint; }
            set { distanceFromWirepoint = value; }
        }

        //参考导线点ID
        private int consultWirepoint;

        /// <summary>
        /// 参考导线点ID
        /// </summary>
        public int ConsultWirepoint
        {
            get { return consultWirepoint; }
            set { consultWirepoint = value; }
        }
    }
}
