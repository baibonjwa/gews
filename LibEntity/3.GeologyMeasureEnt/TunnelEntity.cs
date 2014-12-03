// ******************************************************************
// 概  述：巷道实体
// 作  者：宋英杰
// 日  期：2013/11/28
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
    [Serializable]
    public class TunnelEntity
    {
        // 巷道编号
        private int tunnelID;

        /// <summary>
        /// 巷道编号
        /// </summary>
        public int TunnelID
        {
            get { return tunnelID; }
            set { tunnelID = value; }
        }

        // 巷道名称
        private string tunnelName;

        /// <summary>
        /// 巷道名称
        /// </summary>
        public string TunnelName
        {
            get { return tunnelName; }
            set { tunnelName = value; }
        }

        // 支护方式
        private string tunnelSupportPattern;

        /// <summary>
        /// 支护方式
        /// </summary>
        public string TunnelSupportPattern
        {
            get { return tunnelSupportPattern; }
            set { tunnelSupportPattern = value; }
        }

        // 围岩类型
        private int tunnelLithologyID;

        /// <summary>
        /// 围岩类型
        /// </summary>
        public int TunnelLithologyID
        {
            get { return tunnelLithologyID; }
            set { tunnelLithologyID = value; }
        }

        // 断面类型
        private string tunnelSectionType;

        /// <summary>
        /// 断面类型
        /// </summary>
        public string TunnelSectionType
        {
            get { return tunnelSectionType; }
            set { tunnelSectionType = value; }
        }

        // 断面参数
        private string tunnelParam;

        /// <summary>
        /// 断面参数
        /// </summary>
        public string TunnelParam
        {
            get { return tunnelParam; }
            set { tunnelParam = value; }
        }

        // 设计长度
        private double tunnelDesignLength;

        /// <summary>
        /// 设计长度
        /// </summary>
        public double TunnelDesignLength
        {
            get { return tunnelDesignLength; }
            set { tunnelDesignLength = value; }
        }

        // 设计面积
        private double tunnelDesignArea;

        /// <summary>
        /// 设计长度
        /// </summary>
        public double TunnelDesignArea
        {
            get { return tunnelDesignArea; }
            set { tunnelDesignArea = value; }
        }

        //巷道类型
        private TunnelTypeEnum tunnelType;

        /// <summary>
        /// 巷道类型
        /// </summary>
        public TunnelTypeEnum TunnelType
        {
            get { return tunnelType; }
            set { tunnelType = value; }
        }

        // 工作面
        private WorkingFaceEntity workingFace;

        /// <summary>
        /// 工作面
        /// </summary>
        public WorkingFaceEntity WorkingFace        {
            get { return workingFace; }
            set { workingFace = value; }
        }

        // 煤巷岩巷
        private string coalOrStone;

        /// <summary>
        /// 煤巷岩巷
        /// </summary>
        public string CoalOrStone
        {
            get { return coalOrStone; }
            set { coalOrStone = value; }
        }

        // 绑定煤层ID
        private int coalLayerID;

        /// <summary>
        /// 绑定煤层ID
        /// </summary>
        public int CoalLayerID
        {
            get { return coalLayerID; }
            set { coalLayerID = value; }
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

        private int usedTimes;
        /// <summary>
        /// 巷道使用次数，用来缓存信息，此外无实际意义。
        /// </summary>
        public int UsedTimes
        {
            get
            {
                return usedTimes;
            }
            set { usedTimes = value; }
        }

        private double tunnelWid;
        /// <summary>
        /// 巷道宽度
        /// </summary>
        public double TunnelWid
        {
            get { return tunnelWid; }
            set { tunnelWid = value; }
        }
    }
}
