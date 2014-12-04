// ******************************************************************
// 概  述：停采线数据实体
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
    public class StopLineEntity
    {
        // 主键
        private int iD;

        /// <summary>
        /// 设置或获取主键
        /// </summary>
        public int ID
        {
            get { return iD; }
            set { iD = value; }
        }

        // 停采线名称
        private string stopLineName;

        /// <summary>
        /// 设置或获取停采线名称
        /// </summary>
        public string StopLineName
        {
            get { return stopLineName; }
            set { stopLineName = value; }
        }

        // 起点坐标X
        private double sCoordinateX;

        /// <summary>
        /// 设置或获取起点坐标X
        /// </summary>
        public double SCoordinateX
        {
            get { return sCoordinateX; }
            set { sCoordinateX = value; }
        }

        // 起点坐标Y
        private double sCoordinateY;

        /// <summary>
        /// 设置或获取起点坐标Y
        /// </summary>
        public double SCoordinateY
        {
            get { return sCoordinateY; }
            set { sCoordinateY = value; }
        }

        // 起点坐标Z
        private double sCoordinateZ;

        /// <summary>
        /// 设置或获取起点坐标Z
        /// </summary>
        public double SCoordinateZ
        {
            get { return sCoordinateZ; }
            set { sCoordinateZ = value; }
        }

        // 终点坐标X
        private double fCoordinateX;

        /// <summary>
        /// 设置或获取终点坐标X
        /// </summary>
        public double FCoordinateX
        {
            get { return fCoordinateX; }
            set { fCoordinateX = value; }
        }

        // 终点坐标Y
        private double fCoordinateY;

        /// <summary>
        /// 设置或获取终点坐标Y
        /// </summary>
        public double FCoordinateY
        {
            get { return fCoordinateY; }
            set { fCoordinateY = value; }
        }

        // 终点坐标Z
        private double fCoordinateZ;

        /// <summary>
        /// 设置或获取终点坐标Z
        /// </summary>
        public double FCoordinateZ
        {
            get { return fCoordinateZ; }
            set { fCoordinateZ = value; }
        }

        // BID
        private string bindingID;

        /// <summary>
        /// BID
        /// </summary>
        public string BindingID
        {
            get { return bindingID; }
            set { bindingID = value; }
        }

    }
}
