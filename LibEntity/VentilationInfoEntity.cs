// ******************************************************************
// 概  述：
// 作  者：
// 日  期：
// 版本号：V1.0
// 版本信息：
// V1.0 新建
// ******************************************************************

namespace LibEntity
{
    public class VentilationInfoEntity : MineDataEntity
    {
        // 是否有无风区域

        /// <summary>
        ///     设置或获取是否有无风区域
        /// </summary>
        public int IsNoWindArea { get; set; }

        // 是否有微风区域

        /// <summary>
        ///     设置或获取是否有微风区域
        /// </summary>
        public int IsLightWindArea { get; set; }

        // 是否有风流反向区域

        /// <summary>
        ///     设置或获取是否有风流反向区域
        /// </summary>
        public int IsReturnWindArea { get; set; }

        // 是否通风断面小于设计断面的2/3

        /// <summary>
        ///     设置或获取是否通风断面小于设计断面的2/3
        /// </summary>
        public int IsSmall { get; set; }

        // 是否工作面风量低于计划风量，风速与《煤矿安全规程》规定不符

        /// <summary>
        ///     设置或获取是否工作面风量低于计划风量，风速与《煤矿安全规程》规定不符
        /// </summary>
        public int IsFollowRule { get; set; }

        //通风断面

        /// <summary>
        ///     通风断面
        /// </summary>
        public double FaultageArea { get; set; }

        //风量

        /// <summary>
        ///     风量
        /// </summary>
        public double AirFlow { get; set; }
    }
}