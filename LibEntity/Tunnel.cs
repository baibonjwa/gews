// ******************************************************************
// 概  述：巷道实体
// 作  者：宋英杰
// 日  期：2013/11/28
// 版本号：V1.0
// 版本信息：
// V1.0 新建
// ******************************************************************

using Castle.ActiveRecord;

namespace LibEntity
{
    [ActiveRecord("T_TUNNEL_INFO")]
    public class Tunnel : ActiveRecordBase
    {
        /// <summary>
        ///     巷道编号
        /// </summary>
        [PrimaryKey]
        [Property("TUNNEL_ID")]
        public int TunnelID { get; set; }

        /// <summary>
        ///     巷道名称
        /// </summary>
        [Property("TUNNEL_NAME")]
        public string TunnelName { get; set; }

        /// <summary>
        ///     支护方式
        /// </summary>
        [Property("SUPPORT_PATTERN")]
        public string TunnelSupportPattern { get; set; }

        // 围岩类型

        /// <summary>
        ///     围岩类型
        /// </summary>
        [Property("LITHOLOGY_ID")]
        public int TunnelLithologyID { get; set; }

        // 断面类型

        /// <summary>
        ///     断面类型
        /// </summary>
        [Property("SECTION_TYPE")]
        public string TunnelSectionType { get; set; }

        // 断面参数

        /// <summary>
        ///     断面参数
        /// </summary>
        [Property("PARAM")]
        public string TunnelParam { get; set; }

        // 设计长度

        /// <summary>
        ///     设计长度
        /// </summary>
        [Property("DESIGNLENGTH")]
        public double TunnelDesignLength { get; set; }

        // 设计面积

        /// <summary>
        ///     设计长度
        /// </summary>
        [Property("DESIGNAREA")]
        public double TunnelDesignArea { get; set; }

        //巷道类型

        /// <summary>
        ///     巷道类型
        /// </summary>
        [Property("TUNNEL_TYPE")]
        public TunnelTypeEnum TunnelType { get; set; }

        // 工作面

        /// <summary>
        ///     工作面
        /// </summary>
        [BelongsTo("WORKINGFACE_ID")]
        public WorkingFace WorkingFace { get; set; }

        // 煤巷岩巷

        /// <summary>
        ///     煤巷岩巷
        /// </summary>
        [Property("COAL_OR_STONE")]
        public string CoalOrStone { get; set; }

        // 绑定煤层ID

        /// <summary>
        ///     绑定煤层ID
        /// </summary>
        [Property("COAL_LAYER_ID")]
        public int CoalLayerID { get; set; }

        // BID

        /// <summary>
        ///     BID
        /// </summary>
        [Property("BINDINGID")]
        public string BindingID { get; set; }

        /// <summary>
        ///     巷道使用次数，用来缓存信息，此外无实际意义。
        /// </summary>
        public int UsedTimes { get; set; }

        /// <summary>
        ///     巷道宽度
        /// </summary>
        [Property("Tunnel_Wid")]
        public double TunnelWid { get; set; }
    }
}