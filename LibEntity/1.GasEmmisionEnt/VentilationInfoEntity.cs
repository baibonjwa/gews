// ******************************************************************
// 概  述：
// 作  者：
// 日  期：
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
    public class VentilationInfoEntity : MineDataEntity
    {
        // 是否有无风区域
        private int isNoWindArea;

        /// <summary>
        /// 设置或获取是否有无风区域
        /// </summary>
        public int IsNoWindArea
        {
            get { return isNoWindArea; }
            set { isNoWindArea = value; }
        }

        // 是否有微风区域
        private int isLightWindArea;

        /// <summary>
        /// 设置或获取是否有微风区域
        /// </summary>
        public int IsLightWindArea
        {
            get { return isLightWindArea; }
            set { isLightWindArea = value; }
        }

        // 是否有风流反向区域
        private int isReturnWindArea;

        /// <summary>
        /// 设置或获取是否有风流反向区域
        /// </summary>
        public int IsReturnWindArea
        {
            get { return isReturnWindArea; }
            set { isReturnWindArea = value; }
        }

        // 是否通风断面小于设计断面的2/3
        private int isSmall;

        /// <summary>
        /// 设置或获取是否通风断面小于设计断面的2/3
        /// </summary>
        public int IsSmall
        {
            get { return isSmall; }
            set { isSmall = value; }
        }

        // 是否工作面风量低于计划风量，风速与《煤矿安全规程》规定不符
        private int isFollowRule;

        /// <summary>
        /// 设置或获取是否工作面风量低于计划风量，风速与《煤矿安全规程》规定不符
        /// </summary>
        public int IsFollowRule
        {
            get { return isFollowRule; }
            set { isFollowRule = value; }
        }

        //通风断面
        private double faultageArea;

        /// <summary>
        /// 通风断面
        /// </summary>
        public double FaultageArea
        {
            get { return faultageArea; }
            set { faultageArea = value; }
        }

        //风量
        private double airFlow;

        /// <summary>
        /// 风量
        /// </summary>
        public double AirFlow
        {
            get { return airFlow; }
            set { airFlow = value; }
        }
    }
}
