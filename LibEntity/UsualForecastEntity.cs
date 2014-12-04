namespace LibEntity
{
    public class UsualForecastEntity : MineDataEntity
    {
        //是否有顶板下沉

        /// <summary>
        ///     设置或获取是否有顶板下沉
        /// </summary>
        public int IsRoofDown { get; set; }

        //是否支架变形与折损

        /// <summary>
        ///     设置或获取是否支架变形与折损
        /// </summary>
        public int IsSupportBroken { get; set; }

        //是否煤壁片帮
        //设置或获取是否煤壁片帮
        public int IsCoalWallDrop { get; set; }

        //是否局部冒顶

        /// <summary>
        ///     设置或获取是否局部冒顶
        /// </summary>
        public int IsPartRoolFall { get; set; }

        //是否顶板沿工作面煤壁切落（大冒顶）

        /// <summary>
        ///     设置或获取是否顶板沿工作面煤壁切落（大冒顶）
        /// </summary>
        public int IsBigRoofFall { get; set; }
    }
}