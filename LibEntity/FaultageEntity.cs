// ******************************************************************
// 概  述：揭露断层实体
// 作  者：伍鑫
// 创建日期：2013/11/27
// 版本号：V1.0
// 版本信息：
// V1.0 新建
// ******************************************************************
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using LibGeometry;

namespace LibEntity
{
    public class FaultageEntity
    {
        /** 断层编号 **/
        private int faultageId;

        /// <summary>
        /// 断层编号
        /// </summary>
        public int FaultageId
        {
            get { return faultageId; }
            set { faultageId = value; }
        }

        /** 断层名称 **/
        private string faultageName;

        /// <summary>
        /// 断层名称
        /// </summary>
        public string FaultageName
        {
            get { return faultageName; }
            set { faultageName = value; }
        }

        /** 落差 **/
        private String gap;

        /// <summary>
        /// 落差
        /// </summary>
        public String Gap
        {
            get { return gap; }
            set { gap = value; }
        }

        /** 倾角 **/
        private double angle;

        /// <summary>
        /// 倾角
        /// </summary>
        public double Angle
        {
            get { return angle; }
            set { angle = value; }
        }

        /** 类型 **/
        private string type;

        /// <summary>
        /// 类型
        /// </summary>
        public string Type
        {
            get { return type; }
            set { type = value; }
        }

        /** 走向 **/
        private String trend;

        /// <summary>
        /// 走向
        /// </summary>
        public String Trend
        {
            get { return trend; }
            set { trend = value; }
        }

        /** 断距 **/
        private String separation;

        /// <summary>
        /// 断距
        /// </summary>
        public String Separation
        {
            get { return separation; }
            set { separation = value; }
        }

        /** 坐标X **/
        private double coordinateX;

        /// <summary>
        /// 坐标X
        /// </summary>
        public double CoordinateX
        {
            get { return coordinateX; }
            set { coordinateX = value; }
        }

        /** 坐标Y **/
        private double coordinateY;

        /// <summary>
        ///  坐标Y
        /// </summary>
        public double CoordinateY
        {
            get { return coordinateY; }
            set { coordinateY = value; }
        }

        /** 坐标Z **/
        private double coordinateZ;

        /// <summary>
        ///  坐标Z
        /// </summary>
        public double CoordinateZ
        {
            get { return coordinateZ; }
            set { coordinateZ = value; }
        }

        /** BID **/
        private string bindingId;

        /// <summary>
        /// BID
        /// </summary>
        public string BindingId
        {
            get { return bindingId; }
            set { bindingId = value; }
        }
    }
}
