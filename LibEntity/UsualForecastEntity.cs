using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LibEntity
{
    public class UsualForecastEntity : MineDataEntity
    {
        //是否有顶板下沉
        private int isRoofDown;
        /// <summary>
        /// 设置或获取是否有顶板下沉
        /// </summary>
        public int IsRoofDown
        {
            get { return isRoofDown; }
            set { isRoofDown = value; }
        }
        //是否支架变形与折损
        private int isSupportBroken;
        /// <summary>
        /// 设置或获取是否支架变形与折损
        /// </summary>
        public int IsSupportBroken
        {
            get { return isSupportBroken; }
            set { isSupportBroken = value; }
        }
        //是否煤壁片帮
        private int isCoalWallDrop;
        //设置或获取是否煤壁片帮
        public int IsCoalWallDrop
        {
            get { return isCoalWallDrop; }
            set { isCoalWallDrop = value; }
        }
        //是否局部冒顶
        private int isPartRoolFall;
        /// <summary>
        /// 设置或获取是否局部冒顶
        /// </summary>
        public int IsPartRoolFall
        {
            get { return isPartRoolFall; }
            set { isPartRoolFall = value; }
        }
        //是否顶板沿工作面煤壁切落（大冒顶）
        private int isBigRoofFall;
        /// <summary>
        /// 设置或获取是否顶板沿工作面煤壁切落（大冒顶）
        /// </summary>
        public int IsBigRoofFall
        {
            get { return isBigRoofFall; }
            set { isBigRoofFall = value; }
        }
    }
}
