// ******************************************************************
// 概  述：探头管理实体
// 作  者：伍鑫
// 日  期：2013/12/01
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
    public class ProbeManageEntity
    {
        /** 探头编号 **/
        private string probeId;

        /// <summary>
        /// 探头编号
        /// </summary>
        public string ProbeId
        {
            get { return probeId; }
            set { probeId = value; }
        }

        /** 探头名称 **/
        private string probeName;

        /// <summary>
        /// 探头名称
        /// </summary>
        public string ProbeName
        {
            get { return probeName; }
            set { probeName = value; }
        }

        /** 探头类型编号 **/
        private int probeTypeId;

        /// <summary>
        /// 探头类型编号
        /// </summary>
        public int ProbeTypeId
        {
            get { return probeTypeId; }
            set { probeTypeId = value; }
        }

        /// <summary>
        /// 探头类型显示名称
        /// </summary>
        private string probeTypeDisplayName;

        /// <summary>
        /// 探头类型显示名称
        /// </summary>
        public string ProbeTypeDisplayName
        {
            get { return probeTypeDisplayName; }
            set { probeTypeDisplayName = value; }
        }

        /// <summary>
        /// 测量类型
        /// </summary>
        private int probeMeasureType;

        /// <summary>
        /// 测量类型
        /// </summary>
        public int ProbeMeasureType
        {
            get { return probeMeasureType; }
            set { probeMeasureType = value; }
        }

        /// <summary>
        /// 使用方式
        /// </summary>
        private string probeUseType;

        /// <summary>
        /// 使用方式
        /// </summary>
        public string ProbeUseType
        {
            get { return probeUseType; }
            set { probeUseType = value; }
        }

        /// <summary>
        /// 单位
        /// </summary>
        private string unit;

        /// <summary>
        /// 单位
        /// </summary>
        public string Unit
        {
            get { return unit; }
            set { unit = value; }
        }

        /** 所在巷道编号 **/
        private int tunnelId;

        /// <summary>
        /// 所在巷道编号
        /// </summary>
        public int TunnelId
        {
            get { return tunnelId; }
            set { tunnelId = value; }
        }

        /** 探头位置坐标X **/
        private double probeLocationX;

        /// <summary>
        /// 探头位置坐标X
        /// </summary>
        public double ProbeLocationX
        {
            get { return probeLocationX; }
            set { probeLocationX = value; }
        }

        /** 探头位置坐标Y **/
        private double probeLocationY;

        /// <summary>
        /// 探头位置坐标Y
        /// </summary>
        public double ProbeLocationY
        {
            get { return probeLocationY; }
            set { probeLocationY = value; }
        }

        /** 探头位置坐标Z **/
        private double probeLocationZ;

        /// <summary>
        /// 探头位置坐标Z
        /// </summary>
        public double ProbeLocationZ
        {
            get { return probeLocationZ; }
            set { probeLocationZ = value; }
        }

        /** 探头描述 **/
        private string probeDescription;

        /// <summary>
        /// 探头描述
        /// </summary>
        public string ProbeDescription
        {
            get { return probeDescription; }
            set { probeDescription = value; }
        }

        /** 是否自动位移 **/
        private int isMove;

        /// <summary>
        /// 是否自动位移
        /// </summary>
        public int IsMove
        {
            get { return isMove; }
            set { isMove = value; }
        }

        /** 距迎头距离 **/
        private double farFromFrontal;

        /// <summary>
        /// 距迎头距离
        /// </summary>
        public double FarFromFrontal
        {
            get { return farFromFrontal; }
            set { farFromFrontal = value; }
        }
    }
}
