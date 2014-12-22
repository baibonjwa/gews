// ******************************************************************
// 概  述：工作面实体
// 作  者：伍鑫
// 创建日期：2014/02/25
// 版本号：1.0
// ******************************************************************
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using LibGeometry;

namespace LibEntity
{
    public class WorkingFaceEntityNew : PreWarningSpecialDataCalcBase
    {
        /** 工作面编号 **/
        private int workingfaceId;

        /// <summary>
        /// 工作面编号
        /// </summary>
        public int WorkingfaceId
        {
            get { return workingfaceId; }
            set { workingfaceId = value; }
        }

        /** 工作面名称 **/
        private string workingFaceName;

        /// <summary>
        /// 工作面名称
        /// </summary>
        public string WorkingFaceName
        {
            get { return workingFaceName; }
            set { workingFaceName = value; }
        }

        /** 采区编号 **/
        private int miningareaId;

        /// <summary>
        /// 采区编号
        /// </summary>
        public int MiningareaId
        {
            get { return miningareaId; }
            set { miningareaId = value; }
        }
        /**坐标X**/
        private double coordinateX;
        /// <summary>
        /// 坐标X
        /// </summary>
        public double CoordinateX
        {
            get { return coordinateX; }
            set { coordinateX = value;}
        }
        /**坐标X**/
        private double coordinateY;
        /// <summary>
        /// 坐标X
        /// </summary>
        public double CoordinateY
        {
            get { return coordinateY; }
            set { coordinateY = value; }
        }
        /**坐标X**/
        private double coordinateZ;
        /// <summary>
        /// 坐标X
        /// </summary>
        public double CoordinateZ
        {
            get { return coordinateZ; }
            set { coordinateZ = value; }
        }
        //采掘区的GUID
        private string bid;
        /// <summary>
        /// 采掘区GUID
        /// </summary>
        public string BID
        {
            get { return bid; }
            set { bid = value; }
        }
    }
}