// ******************************************************************
// 概  述：停采线数据实体
// 作  者：宋英杰
// 日  期：2014/3/12
// 版本号：V1.0
// 版本信息：
// V1.0 新建
// ******************************************************************

namespace LibEntity
{
    public class GeologySpace
    {
        // 主键

        /// <summary>
        ///     设置或获取主键
        /// </summary>
        public int ID { get; set; }

        // 工作面ID

        /// <summary>
        /// </summary>
        public int WorkSpaceID { get; set; }

        // 距离

        /// <summary>
        /// </summary>
        public string TectonicID { get; set; }

        // 起点坐标Y

        /// <summary>
        ///     设置或获取起点坐标Y
        /// </summary>
        public double Distance { get; set; }

        // 类型

        /// <summary>
        ///     类型
        /// </summary>
        public int TectonicType { get; set; }

        //时间 

        /// <summary>
        /// </summary>
        public string onDateTime { get; set; }
    }
}