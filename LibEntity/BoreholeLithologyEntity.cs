// ******************************************************************
// 概  述：钻孔岩性实体
// 作  者：伍鑫
// 创建日期：2013/11/26
// 版本号：1.0
// ******************************************************************
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LibEntity
{
    public class BoreholeLithologyEntity
    {
        /** 钻孔编号 **/
        private int boreholeId;

        /// <summary>
        /// 钻孔编号
        /// </summary>
        public int BoreholeId
        {
            get { return boreholeId; }
            set { boreholeId = value; }
        }

        /** 岩性编号 **/
        private int lithologyId;

        /// <summary>
        /// 岩性编号
        /// </summary>
        public int LithologyId
        {
            get { return lithologyId; }
            set { lithologyId = value; }
        }

        /** 底板标高 **/
        private double floorElevation;

        /// <summary>
        /// 底板标高
        /// </summary>
        public double FloorElevation
        {
            get { return floorElevation; }
            set { floorElevation = value; }
        }

        /** 厚度 **/
        private double thickness;

        /// <summary>
        /// 厚度
        /// </summary>
        public double Thickness
        {
            get { return thickness; }
            set { thickness = value; }
        }

        /** 煤层名称 **/
        private string coalSeamsName;

        /// <summary>
        /// 煤层名称
        /// </summary>
        public string CoalSeamsName
        {
            get { return coalSeamsName; }
            set { coalSeamsName = value; }
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
        
    }
}
